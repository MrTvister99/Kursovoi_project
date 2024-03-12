using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KP
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List1 : ContentPage
    {
        public static string Name;
        public List1()
        {
            InitializeComponent();
        }

        private void bus(object sender, EventArgs e)
        {
            Name = "минивэны";
            Navigation.PushModalAsync(new page_selection());
            
        }
        private void bus1(object sender, EventArgs e)
        {
            Name = "маршрутки";
            Navigation.PushModalAsync(new page_selection());
        }
        private void bus2(object sender, EventArgs e)
        {
            Name = "автобусы";
            Navigation.PushModalAsync(new page_selection());
        }

        private void SquareButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Basket());
        }
    }
}