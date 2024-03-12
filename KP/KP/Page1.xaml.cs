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
using FirebaseAdmin.Auth;
using System.Text.RegularExpressions;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


namespace KP
{

    public partial class Page1 : ContentPage
    { 
        public static string login;
        public string password;
        public string password1;
        public static string Name;
        public Page1()
        {

            InitializeComponent();
            ButtenAccount_registration.IsEnabled = false;
            ButtenAccount_registration.BackgroundColor = Color.Gray;



        }

        private void Login_Completed1(object sender, EventArgs e)
        {

            
            login = entry1.Text;


        }

        private void Password_Completed2(object sender, EventArgs e)
        {
            
            password = entry2.Text;
        }

        private void Password_Completed3(object sender, EventArgs e)
        {
            
            password1 = entry3.Text;
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;


            ButtenAccount_registration.IsEnabled = e.Value;
            if(ButtenAccount_registration.IsEnabled==false)
            {
                ButtenAccount_registration.BackgroundColor= Color.Gray;
            }
            else
            {
                ButtenAccount_registration.BackgroundColor = Color.LightSkyBlue;
            }
        }

        private async void ButtenAccount_registration_Clicked(object sender, EventArgs e)
        {

            registration();
     }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            return Regex.IsMatch(email, emailPattern);
        }

        private async void registration()
        {
           
            login = entry1.Text + "";
            password = entry2.Text + "";
            password1 = entry3.Text + "";
            Name = entryName.Text + "";
            login = entry1.Text;
            password = entry2.Text;
            Name = entryName.Text;
            password1 = entry3.Text;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password1) || string.IsNullOrWhiteSpace(Name) || password.Length < 6
                || password1 != password || Name.Length < 2)
            {
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password1) || string.IsNullOrWhiteSpace(Name))
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        ac.BorderColor = Color.Red;
                    }
                    else
                    {
                        ac.BorderColor = Color.White;
                    }
                    if (string.IsNullOrWhiteSpace(password1))
                    {
                        ad.BorderColor = Color.Red;
                    }
                    else
                    {
                        ad.BorderColor = Color.White;
                    }
                    if (string.IsNullOrWhiteSpace(login))
                    {
                        ab.BorderColor = Color.Red;
                    }
                    else
                    {
                        ab.BorderColor = Color.White;
                    }
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        NameFr.BorderColor = Color.Red;
                    }
                
                    else
                    {
                    NameFr.BorderColor = Color.White;
                    }
                    ErrorName.Text = "Поля не должны быть пустыми";
                    return;
                    }
              
                    if (password.Length < 6)
                    {
                    ac.BorderColor = Color.Red;
                    ErrorName.Text = "Пароль должно содержать минимум 6 символов";
                    }
                    else
                    {
                    ac.BorderColor = Color.White;
                    ErrorName.Text = "";
                    }
               
                    if (password1 != password)
                    {
                        ad.BorderColor = Color.Red;
                        ErrorName.Text = "Пароли не совпадают";
                    }
                    else
                    {
                        ad.BorderColor = Color.White;
                        ErrorName.Text = "";
                    }
                  
                    if (Name.Length < 2)
                    {
                        NameFr.BorderColor = Color.Red;
                        ErrorName.Text = "Имя должно содержать минимум 2 символа";
                    }
                    else
                    {
                        NameFr.BorderColor = Color.White;
                        ErrorName.Text = "";
                    }
                   
                    
                    if (!IsValidEmail(login))
                    {
                  
                    ab.BorderColor = Color.Red;
                    ErrorName.Text = "Некорректный Email";
                   
                    }
                    else
                    {
                     ab.BorderColor = Color.White;
                     }
            }
            else
                {

                string apiKey = "AIzaSyDh4J-cAw60c5IyCK89C-B9a7M6_aKf8eU";
                FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                FirebaseAuthLink link1 = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(login, password);
                await Navigation.PushModalAsync(new List1());

                }
        }
    
        

        private async void ButtonRegistration_Click(object sender, EventArgs e)
        { 
            await Navigation.PushModalAsync(new loginPage());

        }
    }
}
