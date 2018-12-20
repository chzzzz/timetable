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

namespace ScheduleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mainPageMaster : ContentPage
    {
        public ListView ListView;

        public mainPageMaster()
        {
            InitializeComponent();

            BindingContext = new mainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class mainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<mainPageMenuItem> MenuItems { get; set; }
            
            public mainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<mainPageMenuItem>(new[]
                {
                    new mainPageMenuItem (typeof(logIn)){ Id = 0, Title = "登陆" },
                    new mainPageMenuItem(typeof(table)) { Id = 1, Title = "课程表" },
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