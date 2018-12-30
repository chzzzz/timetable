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
            Service service = new Service();
            service.Scores = service.reserializeMethod<List<Score>>("score.xml");
            listView.ItemsSource = service.Scores;
            listView.ItemSelected += async (sender, e) =>
              {
                  if (e.SelectedItem == null)
                      return;
                  await Navigation.PushAsync(new ScoreDetail(e.SelectedItem as Score));
              };
        }

        async void RenewOnCall(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}