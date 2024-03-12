
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

namespace KP
{

    public partial class page_selection : ContentPage
    {
        public static string name_auto;
        

      
        public page_selection()
        {
            InitializeComponent();
            
            BindingContext = this;
            LoadDataIntoListView();
        }
         private  void Order(object sender, EventArgs e)
        {
            var auto = new Minivan();
            Xamarin.Forms.Button button = (Xamarin.Forms.Button)sender;
            string itemName = (string)button.CommandParameter;
            name_auto = itemName;
            Navigation.PushModalAsync(new order_page());
        }
        public async Task LoadDataIntoListView()
        {

            var firebaseClient = new FirebaseClient("https://mdkkur-d108b-default-rtdb.firebaseio.com");
            var minivansRef = firebaseClient.Child(List1.Name);
            var minivans = await minivansRef.OnceAsync<Minivan>();
            var minivanList = new List<Minivan>();
            foreach (var minivan in minivans)
            {
                minivanList.Add(minivan.Object);
                
            }
            myListView.ItemsSource = minivanList;
        }

 
        public class Minivan
        {
            public string ImageUrl { get; set; }
            public int Вместимость { get; set; }
            public string Название { get; set; }
            public decimal Цена { get; set; }

           
            public string FormatedPrice => $"Цена в час: {Цена}";
            public string FormatedCount => $"Кол-во мест: {Вместимость}";


        }
    }
   
}