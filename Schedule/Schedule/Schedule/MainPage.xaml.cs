using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Schedule
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void ScheduleOnCall(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SchedulePage());
        }

        async void ScoresOnCall(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoresPage());
        }
    }
}
