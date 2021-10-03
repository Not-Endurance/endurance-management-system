﻿using EnduranceJudge.Application.Events.Commands.Horses;
using EnduranceJudge.Application.Events.Models;
using EnduranceJudge.Application.Events.Queries.GetHorse;
using EnduranceJudge.Core.Models;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Events.Horses;
using Prism.Events;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Horses
{
    public class HorseViewModel : RootFormBase<GetHorse, UpdateHorse, HorseRootModel, HorseView>,
        IHorseState,
        IListable
    {
        private readonly IEventAggregator eventAggregator;

        private HorseViewModel(IApplicationService application, IEventAggregator eventAggregator) : base(application)
        {
            this.eventAggregator = eventAggregator;
        }

        private int isStallionValue;
        private string feiId;
        private string name;
        private string breed;
        private string club;
        private string trainerFeiId;
        private string trainerFirstName;
        private string trainerLastName;

        public string FeiId
        {
            get => this.feiId;
            set => this.SetProperty(ref this.feiId, value);
        }
        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }
        public string Club
        {
            get => this.club;
            set => this.SetProperty(ref this.club, value);
        }
        public int IsStallionValue
        {
            get => this.isStallionValue;
            set => this.SetProperty(ref this.isStallionValue, value);
        }
        public string Breed
        {
            get => this.breed;
            set => this.SetProperty(ref this.breed, value);
        }
        public string TrainerFeiId
        {
            get => this.trainerFeiId;
            set => this.SetProperty(ref this.trainerFeiId, value);
        }
        public string TrainerFirstName
        {
            get => this.trainerFirstName;
            set => this.SetProperty(ref this.trainerFirstName, value);
        }
        public string TrainerLastName
        {
            get => this.trainerLastName;
            set => this.SetProperty(ref this.trainerLastName, value);
        }

        public bool IsStallion => this.isStallionValue != 0;

        protected override async Task SubmitAction()
        {
            await base.SubmitAction();
            this.eventAggregator
                .GetEvent<HorseUpdatedEvent>()
                .Publish(this);
        }
    }
}
