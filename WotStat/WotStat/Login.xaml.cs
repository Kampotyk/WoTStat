using System;
using System.Windows;
using WotStatService.Models;

namespace WotStat
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        const string status = "status";
        const string accessToken = "access_token";
        const string nickname = "nickname";
        const string accountId = "account_id";
        const string expiresAt = "expires_at";

        private PrivateData userData = null;

        public Login(PrivateData privateData)
        {
            InitializeComponent();
            userData = privateData;
        }

        private void WebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string query = webBrowser.Source.Query;

            if (query.Contains(status) && query.Contains(accessToken)
               && query.Contains(nickname) && query.Contains(accountId) && query.Contains(expiresAt))
            {
                var parameters = query.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var param in parameters)
                {
                    var key = param.Split(new char[] { '=' })[0];
                    var value = param.Split(new char[] { '=' })[1];

                    switch (key)
                    {
                        case status:
                            userData.Status = value;
                            break;

                        case accessToken:
                            userData.AccessToken = value;
                            break;

                        case nickname:
                            userData.Nickname = value;
                            break;

                        case accountId:
                            userData.AccountId = value;
                            break;

                        case expiresAt:
                            userData.ExpiresAt = value;
                            break;
                    }
                }

                DialogResult = true;
            }
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string address = StatService.OpenIdLogin();
            if (address != null)
            {
                webBrowser.Visibility = Visibility.Visible;
                webBrowser.Navigate(address);
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
