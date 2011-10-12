using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;

namespace EventTracker.Phone
{
    public class GoToSearchViewModel : EventTrackerViewModelBase
    {
        public GoToSearchViewModel()
        {
            this.PageTitle = "other options";

            BindCommand();
        }

        #region Helpers
        private void BindCommand()
        {
            ItemSelectionChangedCommand = new RelayCommand(() =>
            {
                this.SendNavigationRequestMessage(
                            new Uri("/Views/SearchPage.xaml", UriKind.Relative));
            }, () =>
            {
                return true;
            });
        }
        #endregion

        #region Command
        public RelayCommand ItemSelectionChangedCommand
        {
            get;
            private set;
        }
        #endregion
    }
}
