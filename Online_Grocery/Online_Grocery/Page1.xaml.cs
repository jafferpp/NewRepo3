using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Grocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        bool cl1;public static bool cl2;
        StackLayout vb = new StackLayout();
        Grid vbb = new Grid();

        Grid gr = new Grid();
       // Class1 cs = new Class1();
       // Class2 cs2 = new Class2();
        public Page1()
        {
            InitializeComponent();
            Class1 cs = new Class1();
            vb = cs.firststack;
            qwer.Content = vb;cl1 = true;
        }

        private void home(object sender, EventArgs e)
        {
            one.BackgroundColor = Color.White;
            one1.TextColor = Color.Black;
            two2.TextColor = Color.White;
            two.BackgroundColor = Color.LimeGreen;
            three3.TextColor = Color.White;
            three.BackgroundColor = Color.LimeGreen;

            qwer.Content = null;
            if(cl1)
            {
                qwer.Content = vb;
            }
            else
            {
                Class1 cs = new Class1();
                vb = cs.firststack;
                qwer.Content = vb;
                cl1 = true;
            }
            
            
        }

        private void cart(object sender, EventArgs e)
        {
            one.BackgroundColor = Color.LimeGreen;
            one1.TextColor = Color.White;
            two2.TextColor = Color.Black;
            two.BackgroundColor = Color.White;
            three3.TextColor = Color.White;
            three.BackgroundColor = Color.LimeGreen;
            qwer.Content = null;
            if (cl2)
            {
                qwer.Content = vbb;
            }
            else
            {
               
                Class2 cs2 = new Class2();
                vbb= cs2.cartgrid;
                qwer.Content = vbb;
                cl2 = true;
            }
            
        }

        private void notification(object sender, EventArgs e)
        {
            one.BackgroundColor = Color.LimeGreen;
            one1.TextColor = Color.White;
            two2.TextColor = Color.White;
            two.BackgroundColor = Color.LimeGreen;
            three3.TextColor = Color.Black;
            three.BackgroundColor = Color.White;
            qwer.Content = null;
            Label ml = new Label() { Text = "No notifications", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            qwer.Content = ml;
        }
    }
}