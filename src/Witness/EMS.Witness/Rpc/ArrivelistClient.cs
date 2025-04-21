using Core.Application.Rpc;
using Core.Application.Rpc.Procedures;
using Core.Domain.AggregateRoots.Manager;
using Core.Domain.AggregateRoots.Manager.Aggregates.Participants;
using Core.Enums;
using EMS.Witness.Models;
using EMS.Witness.Services;
using EMS.Witness.Shared.Toasts;

namespace EMS.Witness.Rpc;

public class ParticipantsClient : RpcClient, IParticipantsClient, IParticipantsClientProcedures
{
    private readonly SignalRSocket _socket;
    private readonly IWitnessState _witnessState;
    private readonly IToaster _toaster;

    public event EventHandler<(ParticipantEntry entry, CollectionAction action)>? Updated;
	public event EventHandler<IEnumerable<ParticipantEntry>>? Loaded;

	public ParticipantsClient(SignalRSocket socket, IWitnessState witnessState, IToaster toaster) : base(socket)
    {
        _socket = socket;
        _witnessState = witnessState;
        _toaster = toaster;
        RegisterClientProcedure<ParticipantEntry, CollectionAction>(nameof(this.ReceiveEntryUpdate), this.ReceiveEntryUpdate);
    }

    public Task ReceiveEntryUpdate(ParticipantEntry entry, CollectionAction action)
    {
        this.Updated?.Invoke(this, (entry, action));
        return Task.CompletedTask;
    }

    public async Task<RpcInvokeResult<ParticipantsPayload>> Load()
	{
        if (_witnessState.EventId == null)
        {
            _toaster.Add("Not connected", "Connect to an event from Config page", UiColor.Warning, 20);
            return RpcInvokeResult<ParticipantsPayload>.Error;
        }
        var request = WarpRequest.Create(_witnessState.EventId.ToString()!);
		return await InvokeInputOutputProcedure<WarpRequest, ParticipantsPayload>(nameof(IParticipantstHubProcedures.SendParticipants), request);
	}

    public async Task<RpcInvokeResult> Send(IEnumerable<ParticipantEntry> entries, WitnessEventType type)
    {
        if (_witnessState.EventId == null)
        {
            _toaster.Add("Not connected", "Connect to an event from Config page", UiColor.Warning, 20);
            return RpcInvokeResult.Error;
        }

        var payload = new ProcessSnapshotsPayload { Entries = entries, Type = type };
        var request = WarpRequest.Create(_witnessState.EventId.ToString()!, payload);
		return await InvokeInputProcedure(nameof(IParticipantstHubProcedures.ReceiveWitnessEvent), request);
    }
}

public interface IParticipantsClient
{
	event EventHandler<(ParticipantEntry entry, CollectionAction action)>? Updated;
	Task<RpcInvokeResult<ParticipantsPayload>> Load();
	Task<RpcInvokeResult> Send(IEnumerable<ParticipantEntry> entries, WitnessEventType type);
}
