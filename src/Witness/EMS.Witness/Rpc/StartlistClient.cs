using Core.Application.Rpc;
using Core.Application.Rpc.Procedures;
using Core.Domain.AggregateRoots.Manager.Aggregates.Startlists;
using Core.Enums;
using EMS.Witness.Models;
using EMS.Witness.Services;
using EMS.Witness.Shared.Toasts;

namespace EMS.Witness.Rpc;

public class StartlistClient : RpcClient, IStartlistClientProcedures, IStartlistClient 
{
    private readonly IWitnessState _witnessState;
    private readonly IToaster _toaster;
    public event EventHandler<(StartlistEntry entry, CollectionAction action)>? Updated;

    public StartlistClient(SignalRSocket socket, IWitnessState witnessState, IToaster toaster) : base(socket)
    {
        _witnessState = witnessState;
        _toaster = toaster;
        RegisterClientProcedure<StartlistEntry, CollectionAction>(nameof(this.ReceiveEntry), this.ReceiveEntry);
    }

    public Task ReceiveEntry(StartlistEntry entry, CollectionAction action)
	{
		Updated?.Invoke(this, (entry, action));
		return Task.CompletedTask;
	}

	public async Task<RpcInvokeResult<Dictionary<int, Startlist>>> Load()
	{
        if (_witnessState.EventId == null)
        {
            _toaster.Add("Not connected", "Connect to an event from Config page", UiColor.Warning, 20);
            return RpcInvokeResult<Dictionary<int, Startlist>>.Error;
        }
        var request = WarpRequest.Create(_witnessState.EventId.ToString()!);
		return await InvokeInputOutputProcedure<WarpRequest, Dictionary<int, Startlist>>(nameof(IStartlistHubProcedures.SendStartlist), request);
    }
}

public interface IStartlistClient
{
    event EventHandler<(StartlistEntry entry, CollectionAction action)>? Updated;
    Task<RpcInvokeResult<Dictionary<int, Startlist>>> Load();
}