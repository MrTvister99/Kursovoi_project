﻿using KP.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace KP.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}