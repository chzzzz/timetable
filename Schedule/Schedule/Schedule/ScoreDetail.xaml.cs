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
	public partial class ScoreDetail : ContentPage
	{
		public ScoreDetail (Score score)
		{
            BindingContext = score;
			InitializeComponent ();
		}
	}
}