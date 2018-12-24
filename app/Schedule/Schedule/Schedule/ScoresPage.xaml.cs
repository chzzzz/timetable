using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScoresPage : ContentPage
	{
		public ScoresPage ()
		{
			InitializeComponent ();
		}

        async void RenewOnCall(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}