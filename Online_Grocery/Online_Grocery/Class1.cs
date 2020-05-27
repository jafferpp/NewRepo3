using System;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Xamarin.Forms;

namespace Online_Grocery
{
    class Class1
    {
        int s = 0;
        SearchBar sr = new SearchBar() { HeightRequest = 60 };
        public static string searchtext;
        MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
        public static string code; int something = 0;
        public StackLayout firststack = new StackLayout() { Margin = 4, Spacing = 2 };
        MySqlDataReader reader;
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        TapGestureRecognizer tapGestureRecognizer1 = new TapGestureRecognizer();
        Label labe1 = new Label();
        Label labe2 = new Label();
        Label labe3 = new Label();
        Label l2 = new Label();
        Label[] mainlabelarray = new Label[100];
        ScrollView[] scrarray = new ScrollView[20]; int scrarraycount = 0;
        StackLayout[] stacklist1 = new StackLayout[20];
        string str;
        private ScrollView scr = new ScrollView();
        private StackLayout stk = new StackLayout();
        private StackLayout stk2 = new StackLayout();
        Frame lastframe = new Frame();
        public Class1()
        {
            tapGestureRecognizer1.Tapped += (sender, e) =>
            {
                try
                {
                    lastframe.BackgroundColor = Color.White;
                    lastframe = (Frame)sender;
                    Frame f = (Frame)sender;
                    f.BackgroundColor = Color.LightBlue;
                    string frs = f.StyleId.ToString();
                    int asdf = Int32.Parse(f.ClassId.ToString());
                    scrarr(frs, asdf);
                }
                catch { }
            };
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                try
                {
                    Frame f1 = (Frame)sender;
                    code = f1.StyleId.ToString();
                    Shell.Current.Navigation.PushModalAsync(new openingpage());
                }
                catch { }
            };
            startbackgrounding();
        }
        private async void startbackgrounding()
        {
            Frame ffr = new Frame() { Padding = 0, Margin = 0, CornerRadius = 6, HeightRequest = 60, BorderColor = Color.LightBlue };
            sr.Placeholder = "Search";
            sr.PlaceholderColor = Color.LightGray;
            sr.SearchButtonPressed += new System.EventHandler(this.butnclik);
            ffr.Content = sr;
            firststack.Children.Add(ffr);
            Task<int> task1 = new Task<int>(firsttask);
            task1.Start();
            int catlen = await task1;
           // gify2.IsVisible = false;
            int somecount = 0;
        goline:
            if (somecount < s)
            {
                Frame g = new Frame() {Padding=3,Margin=0, HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.DodgerBlue, CornerRadius = 5 };
                g.Content = mainlabelarray[somecount];
                firststack.Children.Add(g);
                Task<ScrollView> task2 = new Task<ScrollView>(secondtask);
                task2.Start();
                ScrollView som = await task2;
                firststack.Children.Add(som);
                Task<ScrollView> task3 = new Task<ScrollView>(thirdtask);
                task3.Start();
                ScrollView some = await task3;
                scrarraycount++;
                firststack.Children.Add(some);
                somecount++; something++;
                goto goline;
            }
        }
        private ScrollView thirdtask()
        {
            con.Open();
            str = "SELECT * FROM product_table WHERE category='" + (mainlabelarray[something].Text) + "'" + " ORDER BY RAND()";
            MySqlCommand cmd = new MySqlCommand(str, con);


            reader = cmd.ExecuteReader();

            scrarray[scrarraycount] = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                HeightRequest = 210
            };

            stk2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 210
            };

            scrarray[scrarraycount].Content = stk2;

            for (int i = 0; i < 7; i++)
            {
                if (reader.Read())
                { reader3call(); }

            }
            con.Close(); return scrarray[scrarraycount];
        }

        private ScrollView secondtask()
        {
            con.Open();

            str = "SELECT DISTINCT section FROM product_table WHERE category='" + (mainlabelarray[something].Text) + "'";
            MySqlCommand cmd = new MySqlCommand(str, con);
            reader = cmd.ExecuteReader();
            stk = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 50
            };
            scr = new ScrollView()
            {
                Orientation = ScrollOrientation.Horizontal,
                HeightRequest = 50,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never
            };
            scr.Content = stk;


            while (reader.Read())
            {
                reader2call();
            }
            con.Close();
            return scr;
        }
        private  void butnclik(object sender, EventArgs eventArgs)
        {
            searchtext = sr.Text;
            Shell.Current.Navigation.PushModalAsync(new search());
        }
            private int firsttask()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT category FROM product_table ORDER BY RAND()",con);

            reader = cmd.ExecuteReader(); int la = 0;
            while (reader.Read())
            {
                mainlabelarray[la] = new Label() {Margin=5, FontSize = 22, TextColor = Color.White, FontAttributes = FontAttributes.Bold };
                mainlabelarray[la].Text = reader["category"].ToString(); la++;s++;
            }
             con.Close();
            return s;
        }
        private void reader3call()
        {
            var ff = new Frame();
            ff.HeightRequest = 210; ff.WidthRequest = 113; ff.Margin = 0; ff.Padding = 0; ff.CornerRadius = 5;
            ff.BorderColor = Color.LightGray;

            ff.GestureRecognizers.Add(tapGestureRecognizer);
            ff.StyleId = reader["bar_code"].ToString();
            ff.ClassId = reader["name"].ToString();
            var ll = new StackLayout();
            ll.Padding = 0; ll.Spacing = 0; ll.Margin = 3; ll.HorizontalOptions = LayoutOptions.Start; ll.VerticalOptions = LayoutOptions.Start;
            Image imag1 = new Image(); imag1.Aspect = Aspect.AspectFill; imag1.WidthRequest = 113; imag1.HeightRequest = 150;

            byte[] imm = (byte[])reader["image"];

            imag1.Source = ImageSource.FromStream(() => new MemoryStream(imm));
            var labe1 = new Label(); var labe2 = new Label(); var labe3 = new Label();
            labe1.FontAttributes = FontAttributes.Bold; labe1.FontSize = 15;
            labe2.FontSize = 11; labe3.FontSize = 15; labe3.VerticalOptions = LayoutOptions.End; labe2.LineBreakMode = LineBreakMode.TailTruncation;
            labe3.LineBreakMode = LineBreakMode.TailTruncation; labe3.VerticalOptions = LayoutOptions.End;

            labe1.Text ="AED "+ reader["price"].ToString();
            labe2.Text = reader["name"].ToString();
            labe3.Text = reader["description"].ToString();
            ll.Children.Add(imag1); ll.Children.Add(labe1); ll.Children.Add(labe2); ll.Children.Add(labe3);
            ff.Content = ll; stk2.Children.Add(ff);

        }

        private void reader2call()
        {
            Image tempimg = new Image()
            {
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,
                Source = "cat.png"
            };



            var l2 = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = reader["section"].ToString() + "  "
            };

            var fr1 = new Frame
            {
                CornerRadius = 50,
                HeightRequest = 50,
                Padding = 0,
                Margin = 0,
                BorderColor = Color.LightBlue
            };

            var fr2 = new Frame
            {
                CornerRadius = 45,
                HeightRequest = 45,
                WidthRequest = 45,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Padding = 0,
                Margin = 4,
                BorderColor = Color.LightGreen
            };

            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 1,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            fr2.Content = tempimg;
            st.Children.Add(fr2);
            st.Children.Add(l2);
            fr1.Content = st;
            fr1.GestureRecognizers.Add(tapGestureRecognizer1);
            fr1.ClassId = scrarraycount.ToString();
            fr1.StyleId = reader["section"].ToString();
            stk.Children.Add(fr1);
        }

        private void scrarr(string a, int b)
        {
            con.Open();
            str = "SELECT * FROM product_table WHERE section = '" + a + "'";
            MySqlCommand cmd = new MySqlCommand(str, con); reader = cmd.ExecuteReader();
            stk2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 210
            };
            scrarray[b].Content = stk2;
            for (int iq = 0; iq < 4; iq++)
            {
                if (reader.Read())
                { reader3call(); }

            }
            con.Close();
        }
    }
}
