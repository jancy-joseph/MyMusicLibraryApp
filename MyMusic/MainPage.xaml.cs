using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = await LibraryUser.GetLibraryUsers();
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
             this.Frame.Navigate(typeof(AddUser));
        }

        private  async void Login_Click(object sender, RoutedEventArgs e)
        {
            var myUser = new LibraryUser()
            {
                UserName = txtUserName.Text,
                USerPassword = txtPassword.Password
            };
            if(await LibraryUser.ValidateLibraryUser(myUser))
            {
                this.Frame.Navigate(typeof(MyMusicCollection), myUser);
            }
            else
            {
                MainStatusText.Text = @" This login account doesnot exist.Enter a different account or create a new user";
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "Password")
            {
                MainStatusText.Text = @"'Password' is not allowed as a password.";
            }
            else
            {
                MainStatusText.Text = string.Empty;
            }

        }
    }
}
