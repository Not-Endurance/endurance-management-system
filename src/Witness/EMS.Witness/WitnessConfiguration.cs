using Core;
using Core.Application;
using Core.Application.Services;
using Core.Domain;
using Core.Localization;
using EMS.Witness.Services;
using EMS.Witness.Platforms.Services;
using EMS.Witness.Rpc;
using Core.Application.Rpc;
using static Core.Application.CoreApplicationConstants;

namespace EMS.Witness;

public static class WitnessConfiguration
{
    public static IServiceCollection AddWitnessServices(this IServiceCollection services)
    {
        var assemblies = CoreConstants.Assemblies
            .Concat(LocalizationConstants.Assemblies)
            .Concat(DomainConstants.Assemblies)
            .Concat(CoreApplicationConstants.Assemblies)
            .Concat(WitnessConstants.Assemblies)
            .ToArray();

        var rpcContext = new RpcContext("nts-nexus-warp-dev-bbajctffatawefea.westeurope-01.azurewebsites.net",  RPC_ENDPOINT, RPC_PORT);

        services
            .AddCore(assemblies)
            .AddSingleton(new Toaster())
            .AddSingleton<IToaster>(x => x.GetRequiredService<Toaster>())
            .AddSingleton<INotificationService>(x => x.GetRequiredService<Toaster>())
            .AddTransient<IPermissionsService, PermissionsService>()
            .AddSingleton<WitnessContext>()
            .AddSingleton<IWitnessContext>(provider => provider.GetRequiredService<WitnessContext>())
            .AddTransient<IDateService, DateService>()
            .AddSingleton<SignalRSocket>()
            .AddSingleton<IRpcSocket, SignalRSocket>(x => x.GetRequiredService<SignalRSocket>())
            .AddSingleton<IStartlistClient, StartlistClient>()
            .AddSingleton<IParticipantsClient, ParticipantsClient>()
            .AddSingleton<WitnessState>()
            .AddSingleton<IWitnessState>(x => x.GetRequiredService<WitnessState>())
            .AddTransient<IPersistenceService, PersistenceService>()
            .AddTransient<IRpcInitalizer, RpcInitializer>()
            .AddSingleton<LoggingClient>()
            .AddSingleton<IWitnessLogger, LoggingClient>()
            .AddSingleton<IRpcContext>(rpcContext)
            .AddTransient<IUpcomingEventRepository, UpcomingEventHttpRepository>()
            .AddTransient<NHttpClient>()
            .AddSingleton<IRpcMetadata>(x => x.GetRequiredService<WitnessState>())
            .AddHttpClient();

        return services;
    }
}
