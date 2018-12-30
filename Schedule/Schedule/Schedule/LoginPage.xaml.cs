using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using App1;
using System.Runtime.Serialization.Formatters.Binary;
namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        Service service;
        public LoginPage()
        {
            InitializeComponent();
            service = new Service();
            service.loadImage();
            BindingContext = service;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                // handle the tap
                pId.Source=service.loadImage();
            };
            pId.GestureRecognizers.Add(tapGestureRecognizer);
        }

        void LoginOnCall(object sender, EventArgs e)
        {
            string webDate = service.logIn(iD.Text, passWord.Text, key.Text);
           if (webDate[0] == '\r')
            {
                pId.Source = service.loadImage();
                passWord.Text = "";
                key.Text = "";
                DisplayAlert("Alert", "请输入正确得用户名，密码或验证码", "OK");
            }
            else
            {
                service.Week.No = service.getWeek(webDate);
                service.Week.setName();
                service.Time = DateTime.Now;
                service.serializeMethod<Week>(service.Week, "week.xml");
                service.serializeMethod<DateTime>(service.Time, "time.xml");
                string token = service.getToken(webDate);
                string table = service.getTable(token);
                service.Match(service.getInfo(table));
                service.serializeMethod<List<Lesson>>(service.Lessons, "list.xml");
                service.matchGrade(service.getGrade(token, "", ""));
                service.serializeMethod<List<Score>>(service.Scores, "score.xml");
                Navigation.PopToRootAsync();
            }

        }

    }
}