using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1;
namespace Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LessonDetail : ContentPage
	{
		public LessonDetail (Lesson lesson)
		{
			InitializeComponent ();
            BindingContext = lesson;
		}
	}
}