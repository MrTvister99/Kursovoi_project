
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms.Database.Models;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;


using System.Linq;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Essentials;
using Google.Apis.Firestore.v1.Data;

namespace KP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basket : ContentPage
    {
        private ObservableCollection<Auto> auto;
        public Basket()
        {
            InitializeComponent();

            auto = new ObservableCollection<Auto>();
            autoListView.ItemsSource = auto;
            FetchDataFromFirebase();

        }
        private async void FetchDataFromFirebase()
        {
            var firebaseClient = new FirebaseClient("https://mdkkur-d108b-default-rtdb.firebaseio.com/");

            // Получить данные из Firebase
            var items = await firebaseClient
                .Child("Orders")
                .OnceAsync<Auto>();


            auto.Clear();


            foreach (var item in items)
            {
                var order = item.Object;
                order.email = item.Key;



                var nestedItems = await firebaseClient
                    .Child("Orders")
                    .Child(order.email)
                    .OnceAsync<Auto>();
                int atIndex = order.email.IndexOf('@');
                if (atIndex >= 0)
                {
                    if (order.email.Contains("ru") && atIndex < order.email.IndexOf("ru"))
                    {
                        order.email = order.email.Replace("ru", ".ru");
                    }
                    else if (order.email.Contains("com") && atIndex < order.email.IndexOf("com"))
                    {
                        order.email = order.email.Replace("com", ".com");
                    }
                }
                foreach (var nestedItem in nestedItems)
                {
                    var nestedDetail = nestedItem.Object;
                    nestedDetail.email = order.email;
                    if (order.email == Page1.login || order.email == loginPage.login)
                    {
                        auto.Add(nestedDetail);
                    }
                }
            }
        }

        public class Auto
        {
            public string email { get; set; }
            public DateTime Date { get; set; }
            public string Car_make { get; set; }
           

            public string name_auto {  get; set; }
            public string DateString
            {
                get { return Date.ToShortDateString(); }
            }


        }

        private async void Delete_Click(object sender, EventArgs e)
        {
            var imageButton = (Xamarin.Forms.ImageButton)sender;
            var item = (Auto)imageButton.BindingContext;
            int Index = item.email.IndexOf('@');
            bool answer = await DisplayAlert("Подтверждение", "Вы уверены, что хотите отменить этот заказ?", "Да", "Нет");
            if (item.email.Contains("ru") && Index < item.email.IndexOf("ru"))
            {
                item.email = item.email.Replace("ru", ".ru");
            }
            else if (item.email.Contains("com") && Index < item.email.IndexOf("com"))
            {
                item.email = item.email.Replace("com", ".com");
            }

            var firebaseClient = new FirebaseClient("https://mdkkur-d108b-default-rtdb.firebaseio.com/");
            var toDeleteItem = (from Orders in auto
                                where Orders.email == item.email && Orders.name_auto == item.name_auto && Orders.Date == item.Date
                                select Orders).FirstOrDefault();

            if (toDeleteItem != null)
            {
                int atIndex = toDeleteItem.email.IndexOf('@');
                if (toDeleteItem.email.Contains("ru") && atIndex < toDeleteItem.email.IndexOf("ru"))
                {
                    toDeleteItem.email = toDeleteItem.email.Replace(".ru", "ru");
                }
                else if (toDeleteItem.email.Contains("com") && atIndex < toDeleteItem.email.IndexOf("com"))
                {
                    toDeleteItem.email = toDeleteItem.email.Replace(".com", "com");
                }
                var autosSnapshot = await firebaseClient
            .Child("Orders")
            .Child(toDeleteItem.email.Replace(".", ""))
            .OnceAsync<Auto>();

                foreach (var detailSnapshot in autosSnapshot)
                {
                    var aut = detailSnapshot.Object;
                    if (aut.name_auto == item.name_auto && aut.DateString== item.DateString)
                    {
                        // Удаляем деталь из Firebase
                        await firebaseClient
                            .Child("Orders")
                            .Child(toDeleteItem.email.Replace(".", ""))
                            .Child(detailSnapshot.Key)
                            .DeleteAsync();

                        // Удаляем деталь из локального списка
                        auto.Remove(aut);


                        autoListView.ItemsSource = null;
                        autoListView.ItemsSource = auto;

                       
                        break;
                    }
                }







                auto.Remove(toDeleteItem);


                autoListView.ItemsSource = null;
                autoListView.ItemsSource = auto;
            }
        }
    }
}
