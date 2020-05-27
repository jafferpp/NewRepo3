using System;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Grocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class search : ContentPage
    {
        
        StackLayout st1 = new StackLayout() { Margin = 0, Padding = 0, Spacing = 0 };
        MySqlDataReader rdr;
        public search()
        {
            InitializeComponent();
            backgrounding();
        }
       private async void  backgrounding()
        {
            Task<MySqlDataReader> task1 = new Task<MySqlDataReader>(one);
            task1.Start();
            rdr = await task1;
            if (rdr.Read())
            {
                asdf(); fone1.Content = st1;fone1.IsVisible = true;
                fone1.StyleId = rdr["bar_code"].ToString();
            }
            else
            {
              await  DisplayAlert("No Result found", "no result found for '" + Class1.searchtext + "'  (Please check the spelling is corret)", "ok");
              await Shell.Current.Navigation.PopModalAsync();
            }
            
            if (rdr.Read())
            {
                asdf(); fone2.Content = st1; fone2.IsVisible = true;
                fone2.StyleId = rdr["bar_code"].ToString();
            }
            
            if (rdr.Read())
            {
                asdf(); fone3.Content = st1; fone3.IsVisible = true;
                fone3.StyleId = rdr["bar_code"].ToString();
            }
        
            if (rdr.Read())
            {
                asdf(); fone4.Content = st1; fone4.IsVisible = true;
                fone4.StyleId = rdr["bar_code"].ToString();
            }
            

        }
        MySqlDataReader one()
        {
            MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlDataReader reader1;
            con.Open();
            string str = "SELECT * FROM product_table WHERE NAME LIKE '%" + Class1.searchtext + "%'" + " OR section LIKE '%" + Class1.searchtext + "%'";
            MySqlCommand cmd = new MySqlCommand(str,con);
            reader1 = cmd.ExecuteReader();
            return reader1;
        }
        void asdf()
        {
            st1 = new StackLayout() { Margin = 0, Padding = 0, Spacing = 0 };
            Label labe1 = new Label(); Label labe2 = new Label(); Label labe3 = new Label();
            labe1.FontAttributes = FontAttributes.Bold; labe1.FontSize = 15;
            labe2.FontSize = 11; labe3.FontSize = 15; labe3.VerticalOptions = LayoutOptions.End; labe2.LineBreakMode = LineBreakMode.TailTruncation;
            labe3.LineBreakMode = LineBreakMode.TailTruncation; labe3.VerticalOptions = LayoutOptions.End;
            labe1.Text ="AED "+ rdr["price"].ToString();
            labe2.Text = rdr["name"].ToString();
            labe3.Text = rdr["description"].ToString();
            Image imag1 = new Image(); imag1.Aspect = Aspect.AspectFill; imag1.WidthRequest = 113; imag1.HeightRequest = 150;
            byte[] imm = (byte[])rdr["image"];
            imag1.Source = ImageSource.FromStream(() => new MemoryStream(imm));
            st1.Children.Add(imag1);
            st1.Children.Add(labe1);
            st1.Children.Add(labe2);
            st1.Children.Add(labe3);
        }

        private void one(object sender, EventArgs e)
        {
            Class1.code = fone1.StyleId;
            Shell.Current.Navigation.PushModalAsync(new openingpage());
            
        }
        private void two(object sender, EventArgs e)
        {
            Class1.code = fone2.StyleId;
            Shell.Current.Navigation.PushModalAsync(new openingpage());
            
        }
        private void three(object sender, EventArgs e)
        {
            Class1.code = fone3.StyleId;
            Shell.Current.Navigation.PushModalAsync(new openingpage());
            
        }
        private void four(object sender, EventArgs e)
        {
            Class1.code = fone4.StyleId;
            Shell.Current.Navigation.PushModalAsync(new openingpage());
           
        }
    }
}