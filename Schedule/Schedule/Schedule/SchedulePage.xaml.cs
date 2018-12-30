using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        Service service;
        public Picker pickerInstance;
        public SchedulePage()
        {
            InitializeComponent();
            service = new Service();
            BindingContext = service;
            service.refreshTime();
            pickerInstance = picker;
            picker.SelectedIndex = service.Week.No - 1;
        }


        void creatView(List<Lesson> lessons, int week)
        {
            int count = 0;
            foreach (Lesson lesson in service.Lessons)
            {
                try
                {
                    if (int.Parse(lesson.BeginWeek) <= week &&
                        int.Parse(lesson.EndWeek) >= week)
                    {
                        if (int.Parse(lesson.WeekInterval) == 1 || (week - int.Parse(lesson.BeginWeek)) % 2 == 0)
                        {
                            count++;
                            BoxView box = new BoxView();
                            box.CornerRadius = new CornerRadius(20);
                            switch (count % 4)
                            {
                                case 0:
                                    box.BackgroundColor = Color.LightPink;
                                    break;
                                case 1:
                                    box.BackgroundColor = Color.LightBlue;
                                    break;
                                case 2:
                                    box.BackgroundColor = Color.LightGoldenrodYellow;
                                    break;
                                case 3:
                                    box.BackgroundColor = Color.LightCoral;
                                    break;

                            }
                            Label label = new Label();
                            label.FontSize = 12;
                            var tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += (s, e) => {
                                // handle the tap
                                Navigation.PushAsync(new LessonDetail(lesson));
                            };
                            label.GestureRecognizers.Add(tapGestureRecognizer);
                            label.Text = lesson.LessonName + lesson.AreaName + lesson.ClassRoom;
                            grid.Children.Add(box, int.Parse(lesson.Day) + 1, int.Parse(lesson.BeginTime));
                            Grid.SetRowSpan(box, int.Parse(lesson.EndTime) - int.Parse(lesson.BeginTime) + 1);
                            grid.Children.Add(label, int.Parse(lesson.Day) + 1, int.Parse(lesson.BeginTime));
                            Grid.SetRowSpan(label, int.Parse(lesson.EndTime) - int.Parse(lesson.BeginTime) + 1);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        void setTable()
        {
            BoxView view1 = new BoxView();
            view1.Color = Color.FromHex("#eee9cd");
            BoxView view2 = new BoxView();
            view2.Color = Color.FromHex("#eee9cd");
            grid.Children.Add(view1, 0, 0);
            grid.Children.Add(view2, 0, 0);
            Grid.SetColumnSpan(view1, 8);
            Grid.SetRowSpan(view2, 14);
            for (int i = 1; i <= 13; i++)
            {
                grid.Children.Add(new Label { Text = "" + i }, 0, i);
            }
            grid.Children.Add(new Label { Text = "周日" }, 1, 0);
            grid.Children.Add(new Label { Text = "周一" }, 2, 0);
            grid.Children.Add(new Label { Text = "周二" }, 3, 0);
            grid.Children.Add(new Label { Text = "周三" }, 4, 0);
            grid.Children.Add(new Label { Text = "周四" }, 5, 0);
            grid.Children.Add(new Label { Text = "周五" }, 6, 0);
            grid.Children.Add(new Label { Text = "周六" }, 7, 0);
        }
        async void RenewOnCall(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            flashGrid(picker.SelectedIndex+1);
        }
      
        public void flashGrid(int week)
        {
            grid.Children.Clear();
            setTable();
            List<Lesson> lessons = service.reserializeMethod<List<Lesson>>("list.xml");
            if (lessons != null)
                service.Lessons = lessons;
            try
            {
                creatView(service.Lessons, week);
            }
            catch (Exception)
            {

            }
        }

      
    }
}