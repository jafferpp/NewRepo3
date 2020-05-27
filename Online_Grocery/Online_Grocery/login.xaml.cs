using System;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Online_Grocery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        
        MySqlConnection con = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");

        public static string strl;
        public static string mobileno;
       
        MemoryStream mn = new MemoryStream();
        byte[] imgs;
        public login()
        {
            InitializeComponent();
        }
        private async void logincutomer(object sender, EventArgs e)
        {
            secondstack.IsVisible = false;imagestack.IsVisible = true;
            if ((entry1.Text == null) || (entry2.Text == null))
            {
                await DisplayAlert("Please enter mobile no and password", "", "okey");
            }
            else
            {
                try
                {
                    Task<bool> task = new Task<bool>(lgcustomer);
                    task.Start();
                    firststack.IsVisible = false; imagestack.IsVisible = true;
                    bool rdrw = await task;
                    imagestack.IsVisible = false; firststack.IsVisible = true;
                    if (rdrw)
                    { await Shell.Current.GoToAsync("//start/home"); }
                    else
                    {
                        imagestack.IsVisible = false; firststack.IsVisible = true;
                        await DisplayAlert("Incorrect mobile or password", "", "okey");
                    }
                }
                catch
                {
                   await DisplayAlert("Connection error", "try again", "ok");
                }
            }
            imagestack.IsVisible = false;firststack.IsVisible = true;

        }
        bool lgcustomer()
        {
            
                string str = "SELECT * FROM registration WHERE mobile= '" + entry1.Text + "'" + " AND " + "password= '" + entry2.Text + "'";
                MySqlDataReader uo;
           
                con.Open();
                MySqlCommand cmd = new MySqlCommand(str, con);
                uo = cmd.ExecuteReader();
                if (uo.Read())
                {
                    strl = uo["name"].ToString();
                    mobileno = uo["mobile"].ToString();
                    con.Close();
                    return true;
                }
            
                con.Close();
               
            return false;
        }
        private void signup(object sender, EventArgs e)
        {
            firststack.IsVisible = false;
            secondstack.IsVisible = true;
        }

        private void cancel(object sender, EventArgs e)
        {
            secondstack.IsVisible = false;
            firststack.IsVisible = true;

        }
        private async void imgclick(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("message", "photo capture not supported", "ok");
                return;
            }
            else
            {


                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    CompressionQuality = 10,

                });
                imgbutton.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();

                    return stream;
                });
                file.GetStream().CopyTo(mn);
                imgs = mn.ToArray();
            }
        }
        private async void sign_up(object sender, EventArgs e)
        {
            Task<bool> task1 = new Task<bool>(countc);
            task1.Start();

            if (entry11.Text == "" && entry22.Text == "")
            {
                await DisplayAlert("fill the mandatory fields", "name and mobile no", "yes");
            }
            else
            {
                if (entry66.Text == entry77.Text)
                {
                    secondstack.IsVisible = false; imagestack.IsVisible = true;
                    try
                    {
                        bool a = await task1; imagestack.IsVisible = false; firststack.IsVisible = true;
                    }
                    catch
                    {
                        imagestack.IsVisible = false; firststack.IsVisible = true;
                        await  DisplayAlert("Some error occured", "possible cases:1) no internet/wifi connection  2) No profile picture selected(click on the image button)", "ok");
                    }
                }
                else
                {
                    await DisplayAlert("password not matching", "conform your password", "yes");
                }
            }
        }
        bool countc()
        {
            
                con.Open();
                string str = "INSERT INTO `registration`(`mobile`, `password`, `name`, `image`) VALUES (@mobile,@password,@name,@image)";
                MySqlCommand cmd = new MySqlCommand(str, con);
                cmd.Parameters.Add("@mobile", MySqlDbType.VarChar);
                cmd.Parameters["@mobile"].Value = entry22.Text;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar);
                cmd.Parameters["@password"].Value = entry66.Text;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar);
                cmd.Parameters["@name"].Value = entry11.Text;
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob);
                cmd.Parameters["@image"].Value = imgs;
                cmd.ExecuteNonQuery();
                con.Close();
            
           
            return true;
        }
    }
}