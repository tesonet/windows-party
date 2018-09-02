
namespace CaliburnMicro.LoginTestExternal.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Windows;

    using Caliburn.Micro;

    using CaliburnMicro.LoginTestExternal.Framework;
    using CaliburnMicro.LoginTestExternal.Model;

    [Export(typeof(ContentViewModel))]
    public class ContentViewModel : Screen, IHandle<ModelEvents>
    {
        #region Fields

        private IEventAggregator events;

        #endregion Fields
        public BindableCollection<Server> DataCollection { get; set; }
        #region Constructors

   
        [ImportingConstructor]
        public ContentViewModel(IEventAggregator events)
        {
            this.events = events;
            this.events.Subscribe(this);
            this.DisplayName = "Testio.";
            DataCollection = new BindableCollection<Server>();
        }

        #endregion Constructors

        ~ContentViewModel()
        {
        }

        #region Properties

        public string Token
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        
        public void DoLogout()
        {
            this.events.Publish(new LogoutEvent(this));
        }

    
        public void Exit()
        {
            this.events.Publish(new ExitEvent());
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            
        }
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        public void Handle(ModelEvents evnt)
        {
                DataCollection.AddRange(evnt.EventList);
        }
        public override void CanClose(Action<bool> callback)
        {
            base.CanClose(callback);
        }

        #endregion Methods
    }
}