using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Firebase;
using Firebase.Database;
using Google.Cloud.Firestore.V1;
using Grpc.Core;
using Firebase.Database.Query;
using FirebaseAdmin;
using FireSharp.Interfaces;
using Firebase.Auth;
using FireSharp.Response;
using Xamarin.Essentials;
using static Google.Rpc.Context.AttributeContext.Types;
using Google.Api;

namespace KP
{

    public partial class loginPage : ContentPage
    {
     
        public static string login;
        public string password;

        public string loginBD;
        public string passwordBD;

        public loginPage()
        {
            InitializeComponent();
        }
        private void Login_Completed1(object sender, EventArgs e)
        {
            Entry entry1 = (Entry)sender;
            login = entry1.Text;
        }
        private void Password_Completed2(object sender, EventArgs e)
        {
            Entry entry2 = (Entry)sender;
            password = entry2.Text;
        }
        private async void ButtonRegistration_Click1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page1());
        }

        private async void ButtenAccount_registration_Clicked(object sender, EventArgs e)
        {
          
            string apiKey = "AIzaSyDh4J-cAw60c5IyCK89C-B9a7M6_aKf8eU";
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            try
            {
                login = entry1.Text;
                password = entry2.Text;
                FirebaseAuthLink link1 = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(login,password);
              
                await Navigation.PushModalAsync(new List1());
            }
            catch (FirebaseAuthException ex)
            {
                Txt.Text = "Неверный логин или пароль";
                if (ex.Reason == AuthErrorReason.WrongPassword || ex.Reason == AuthErrorReason.UnknownEmailAddress)
                {
                    Txt.Text = "Неверный логин или пароль";
                }
               
    }

        }
    }
}

    
