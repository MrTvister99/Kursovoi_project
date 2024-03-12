using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace KP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class order_page : ContentPage
    {
        private string email;
        public order_page()
        {
            InitializeComponent();
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {

            Button button = (Button)sender;



            string fromAddress = fromAddressEntry.Text;
            string toAddress = toAddressEntry.Text;
            DateTime date = datePicker.Date;
            TimeSpan time = timePicker.Time;
            string comment = commentEditor.Text;
            button.IsEnabled = false;
            button.Text = "Заказ отправлен";
            await Task.Delay(3000);
            button.IsEnabled = true;
            button.Text = "Отправить заказ";

            var shipment = new Shipment
            {
                FromAddress = fromAddress,
                ToAddress = toAddress,
                Date = date.ToString(),
                Time = time.ToString(),
                Comment = comment,
                name_auto = page_selection.name_auto
            };
            if (Page1.login != null)
            {
                string mail = Page1.login.ToString();
                email = mail.Replace(".", "");
            }
            else
            {
                string mail = loginPage.login.ToString();
                email = mail.Replace(".", "");
            }

            var firebaseClient = new FirebaseClient("https://mdkkur-d108b-default-rtdb.firebaseio.com/");
            var child = firebaseClient
            .Child("Orders")
                .Child(email)
                .Child(Guid.NewGuid().ToString());

            await child
                .PutAsync(shipment);
        }
        }
        public class Shipment
        {

            public string FromAddress { get; set; }
            public string ToAddress { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Comment { get; set; }
            public string name_auto { get; set; }
        }
    }
