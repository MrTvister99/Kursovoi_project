using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class appsettingsFlyout : ContentPage
    {
        public ListView ListView;

        public appsettingsFlyout()
        {
            InitializeComponent();

            BindingContext = new appsettingsFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class appsettingsFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<appsettingsFlyoutMenuItem> MenuItems { get; set; }

            public appsettingsFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<appsettingsFlyoutMenuItem>(new[]
                {
                    new appsettingsFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new appsettingsFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new appsettingsFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new appsettingsFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new appsettingsFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}