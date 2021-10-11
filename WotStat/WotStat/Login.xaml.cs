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

        public PrivateData UserData = new PrivateData();

        public Login()
        {
            InitializeComponent();
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
                            UserData.Status = value;
                            break;

                        case accessToken:
                            UserData.AccessToken = value;
                            break;

                        case nickname:
                            UserData.Nickname = value;
                            break;

                        case accountId:
                            UserData.AccountId = value;
                            break;

                        case expiresAt:
                            UserData.ExpiresAt = value;
                            break;
                    }
                }

                DialogResult = true;
            }
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string loginPage = StatService.OpenIdLogin(new WotStatService.Models.Region { Name = "Russia", UrlSuffix = "ru" });
            webBrowser.Visibility = Visibility.Visible;
            webBrowser.Navigate(loginPage);
        }
    }
}
