using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Online_Grocery
{
    class Class2
    { 
        MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        public Grid cartgrid = new Grid() { };
        int row = 0;
        Label ll = new Label();
        string st;
        float subtotal = 0;
        float totalitems = 0;
        MySqlDataReader reader;
        StackLayout stk; Button remove;
        public Class2()
        {
            cartgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            cartgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            cartgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            backgrounding();

        }
        private async void backgrounding()
        {
            Task<bool> task1 = new Task<bool>(first);
            task1.Start();
            bool x = await task1;
            while (reader.Read())
            {
                Task<Image> task2 = new Task<Image>(second);
                task2.Start();
                Image y = await task2;
                cartgrid.RowDefinitions.Add(new RowDefinition { Height = 65 });
                cartgrid.Children.Add(y, 0, row);
                cartgrid.Children.Add(stk, 1, row);
                cartgrid.Children.Add(remove, 2, row);
                row++;
            }
            if (row == 0)
            {

                ll.Text = "Your Cart is Empty"; cartgrid.Children.Add(ll, 1, row);
            }
            else
            {
                ll.Text = "Subtotal=" + subtotal.ToString() + " (Total items=" + totalitems + " )";
                Button bnm = new Button() { Text = "Order Now", BackgroundColor = Color.DarkBlue, TextColor = Color.White, CornerRadius = 5, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Center };
               bnm.Clicked+= new System.EventHandler(this.butncli);
                StackLayout vbnm = new StackLayout() { Padding = 0, Spacing = 5, Margin = 0 };
                vbnm.Children.Add(ll); vbnm.Children.Add(bnm);
                cartgrid.Children.Add(vbnm, 1, row);
            }
            con.Close();
        }
        bool first()
        {
            string str = "SELECT * FROM cart WHERE mobile='" + login.mobileno + "'";
            MySqlCommand cmd = new MySqlCommand(str, con);
            con.Open();
            reader = cmd.ExecuteReader();
            return true;
        }
        Image second()
        {
            byte[] imm = (byte[])reader["image"];
            var img1 = new Image() { HorizontalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFill,HeightRequest=65,VerticalOptions=LayoutOptions.Start };
            img1.Source = ImageSource.FromStream(() => new MemoryStream(imm));

            stk = new StackLayout(); stk.Spacing = 0;
            Label l = new Label() { FontSize = 14, FontAttributes = FontAttributes.Bold, Text = reader["name"].ToString() };
            Label l1 = new Label() { FontSize = 15, FontAttributes = FontAttributes.Bold, Text = "AED " + reader["price"].ToString() };
            Label l2 = new Label() { FontSize = 12, Text = "Quantity: " + reader["qty"].ToString() };
            float xx = float.Parse(reader["qty"].ToString());
            float yy = float.Parse(reader["price"].ToString());
           float zz = xx * yy;
            subtotal = subtotal + zz;
            totalitems = totalitems + xx;
            remove = new Button() { Text = "Remove", BackgroundColor = Color.DarkOrange, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, FontSize = 13, TextColor = Color.White, WidthRequest = 80, CornerRadius = 6, HeightRequest = 40 };
            remove.StyleId = reader["code"].ToString();
            remove.Clicked += new System.EventHandler(this.butnclik);
            stk.Children.Add(l); stk.Children.Add(l1); stk.Children.Add(l2);


            return img1;
        }
        private  void butncli(object sender, EventArgs eventArgs)
        {
            Shell.Current.Navigation.PushModalAsync(new Page2());
        }
            private async void butnclik(object sender, EventArgs eventArgs)
        {
            Label jkl = new Label() { Text = "please wait.loading...",HorizontalOptions=LayoutOptions.Center };
            cartgrid.Children.Clear();
            cartgrid.Children.Add(jkl, 1, 0);
            Page1.cl2 = false;
            Button b = (Button)sender;
            st = b.StyleId;
            Task<bool> taskk = new Task<bool>(rem);
            taskk.Start();
           // cartgrid.Children.Clear(); row = 0;
           // gify.IsVisible = true;
            bool sa = await taskk;
             cartgrid.Children.Clear();row = 0;
            backgrounding();
           // gify.IsVisible = false;
         
            
        }
        bool rem()
        {
            string bnm = "DELETE  FROM cart WHERE code='" + st + "'";
            MySqlCommand cm = new MySqlCommand(bnm, con);
            con.Open();
            cm.ExecuteNonQuery(); con.Close();totalitems = 0;subtotal = 0;
            return true;
        }

        private void main(object sender, EventArgs e)
        {
            backgrounding();
        }
    }
}
