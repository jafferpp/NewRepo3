using System;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Grocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class openingpage : ContentPage
    {
        Grid g = new Grid();
        Xamarin.Forms.Image ii = new Xamarin.Forms.Image();
        MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        MySqlConnection con1 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");

        int rowcount = 0;
        MySqlDataReader reader;
        int a = 1;
        public openingpage()
        {
            InitializeComponent();
            backgrounding();
        }
        private async void backgrounding()
        {
            Task<Xamarin.Forms.Image> task1 = new Task<Xamarin.Forms.Image>(one);
            task1.Start();
            Xamarin.Forms.Image cmg = await task1;
            img.Source = cmg.Source;
            label0.Text += reader["name"].ToString();
            label1.Text += reader["price"].ToString();
            label2.Text += reader["description"].ToString();
            details.Text = reader["details"].ToString();
            specification.Text = reader["specification"].ToString();
            con.Close();
            Task<Grid> tt = new Task<Grid>(two);
            tt.Start();
            Grid sd = await tt;
            commentgrid.Children.Add(sd);
        }
        Grid two()
        {
            con.Open();
            string df = "SELECT * FROM comment WHERE code='"+Class1.code+"'";
            MySqlCommand cn = new MySqlCommand(df, con);
            MySqlDataReader vbb;
            vbb = cn.ExecuteReader();
            
            g.ColumnDefinitions.Add(new ColumnDefinition { Width =50});
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            while (vbb.Read())
            {
                Xamarin.Forms.Image ii = new Xamarin.Forms.Image() { Source = "defaultimage.png", HeightRequest = 50, WidthRequest = 50 };
                g.Children.Add(ii, 0, rowcount);
                StackLayout kll = new StackLayout();
                Xamarin.Forms.Label gg = new Xamarin.Forms.Label() { Text = login.strl };
                Xamarin.Forms.Label ggg = new Xamarin.Forms.Label() { Text = vbb["comment"].ToString() };
                kll.Children.Add(gg); kll.Children.Add(ggg);
                g.Children.Add(kll, 1, rowcount);rowcount++;
            }
            con.Close();
            return g;
        }
        Xamarin.Forms.Image one()
        {
            con.Close(); con.Open();
            string gh = "SELECT * FROM product_table WHERE bar_code='" + Class1.code + "'";
            MySqlCommand cmd = new MySqlCommand(gh, con);
            reader = cmd.ExecuteReader();
            reader.Read();
            byte[] imm = (byte[])reader["image"];
            var img1 = new Xamarin.Forms.Image() { HorizontalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFill };
            img1.Source = ImageSource.FromStream(() => new MemoryStream(imm));
            
            return img1;
        }
        private async void addtocart(object sender, EventArgs e)
        {
            try
            {
                con.Open();con.Close();
                
                cartbutton.IsVisible = false; cartstack.IsVisible = false; gify.IsVisible = true;
                Task<bool> tas = new Task<bool>(qwe);
                tas.Start();
                bool asd = await tas;
                gify.IsVisible = false;
                cartbutton.IsVisible = true; cartstack.IsVisible = true;
                await DisplayAlert("item added to your cart", "Qty=" + a.ToString(), "okey");
                Page1.cl2 = false;
            }
            catch
            {
                gify.IsVisible = false; cartbutton.IsVisible = true; cartstack.IsVisible = true;
                await DisplayAlert("content is loading...", "please wait. ", "ok");
            }
        }
        private bool qwe()
        {
            string str = "INSERT INTO cart(`mobile`,`code`,`name`,`price`,`qty`,`image`) SELECT @mob, `bar_code`, `name`, `price`, @qty, `image` FROM product_table WHERE bar_code='" + Class1.code + "'";
            
            con1.Open();
                MySqlCommand cmd = new MySqlCommand(str, con1);
                cmd.Parameters.Add("@mob", MySqlDbType.VarChar);
                cmd.Parameters["@mob"].Value = login.mobileno;
                cmd.Parameters.Add("@qty", MySqlDbType.VarChar);
                cmd.Parameters["@qty"].Value = qtylabel.Text;

                cmd.ExecuteNonQuery();
                con1.Close();
               
            
           
            return true;
           
        }
        private void mclic(object sender, EventArgs e)
        {
            if (a > 1)
            {
                a = a - 1;
                qtylabel.Text = a.ToString();

            }
        }

        private void pclic(object sender, EventArgs e)
        {
            a = a + 1;
            qtylabel.Text = a.ToString();
        }

        private void prductdetails(object sender, EventArgs e)
        {
            scr.ScrollToAsync(0, 0, true);one1.BackgroundColor = Color.White;two2.BackgroundColor = Color.LightBlue;
            specification.IsVisible = false;details.IsVisible = true;
            l11.IsVisible = true; l22.IsVisible = false;
        }

        private void specifications(object sender, EventArgs e)
        {
            scr.ScrollToAsync(374, 0, true);one1.BackgroundColor = Color.LightBlue;two2.BackgroundColor = Color.White;
            specification.IsVisible = true; details.IsVisible = false;l11.IsVisible = false;l22.IsVisible = true;
        }

        private void asdf(object sender, ScrolledEventArgs e)
        {

        }

        private void com(object sender, EventArgs e)
        {
            Xamarin.Forms.Label hj = new Xamarin.Forms.Label();
            hj.Text = ed.Text; Xamarin.Forms.Label hjj = new Xamarin.Forms.Label() { FontSize = 15, TextColor = Color.DarkBlue };
            hjj.Text = login.strl;
            StackLayout stk = new StackLayout();
            stk.Children.Add(hjj); stk.Children.Add(hj);
            Xamarin.Forms.Image ll = new Xamarin.Forms.Image() { Source = "pro.png",WidthRequest=50 };
            g.Children.Add(ll, 0, rowcount); g.Children.Add(stk, 1, rowcount);rowcount++;
        }
    }
}