﻿using Core.Application;
using Core.Application.Rpc;
using Core.Application.Services;
using Core.ConventionalServices;
using EMS.Witness.Platforms.Services;
using EMS.Witness.Shared.Toasts;

namespace EMS.Witness.Services;

public class RpcInitializer : IRpcInitalizer
{
	public const string ANDROID_EMULATOR_HOST_LOOPBACK = "10.0.2.2";

    private readonly IToaster toaster;
	private readonly IWitnessState _witnessState;
	private readonly IHandshakeService _handshakeService;
    private readonly IRpcSocket _rpcSocket;
	private readonly IPermissionsService permissionsService;
	private readonly WitnessContext context;
	
	public RpcInitializer(
		IWitnessState witnessState,
        IHandshakeService handshakeService,
        IWitnessContext context,
		IRpcSocket rpcSocket,
		IPermissionsService permissionsService,
		IToaster toaster)
    {
		this.context = (WitnessContext)context;
		_witnessState = witnessState;
		_handshakeService = handshakeService;
        _rpcSocket = rpcSocket;
		this.permissionsService = permissionsService;
		this.toaster = toaster;
    }

	public Task Disconnect()
	{
		_rpcSocket.Disconnect();
		return Task.CompletedTask;
	}

	public async Task StartConnections()
	{
		try
		{
			if (!await this.permissionsService.HasNetworkPermissions())
			{
				this.toaster.Add(
					"Network permission rejected",
					"eWitness app cannot operate without Network permissions. Grant permissions in device settings.",
					UiColor.Danger);
				return;
			}
			if (_rpcSocket.IsConnected)
			{
				return;
			}

			var host = _witnessState.HostIp ??= await Handshake();
            await _rpcSocket.Connect(host);
		}
		catch (Exception exception)
		{
			this.ToastError(exception);
		}
	}

	private async Task<string> Handshake()
	{
#if DEBUG
		return ANDROID_EMULATOR_HOST_LOOPBACK;
#else

		this.context.RaiseIsHandshakingEvent(true);

		var hostIp = await _handshakeService.Handshake(CoreApplicationConstants.Apps.WITNESS, CancellationToken.None);
		if (hostIp == null)
		{
			this.context.RaiseIsHandshakingEvent(false);
			throw new Exception("Server broadcast received, but payload does not contain an IP address");
		}
		
		this.context.RaiseIsHandshakingEvent(false);
		return hostIp.ToString();
#endif
	}

	private void ToastError(Exception exception)
	{
		this.toaster.Add(exception.Message, exception?.StackTrace, UiColor.Danger, 30);
	}
}

public interface IRpcInitalizer : ISingletonService
{
	Task StartConnections();
	Task Disconnect();
}
