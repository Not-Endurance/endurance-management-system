﻿using EnduranceJudge.Application.Events.Commands.Horses;
using EnduranceJudge.Application.Events.Queries.GetHorseList;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Horses.Listing
{
    public class HorseListViewModel : SearchableListViewModelBase<GetHorseList, RemoveHorse, HorseView>
    {
        public HorseListViewModel(IApplicationService application, INavigationService navigation)
            : base(application, navigation)
        {
        }
    }
}
