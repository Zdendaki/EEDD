using Communication;
using Communication.Data;
using Communication.Procedures;
using Communication.Procedures.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Manager
{
    /// <summary>
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Trim().Length == 0 || Password.Password.Trim().Length == 0)
            {
                MessageBox.Show("Musíte zadat uživatelské jméno i heslo.", "Nevyplněna povinná pole", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string password = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password.Password.Trim()))).ToLower().Replace("-", null);
                string username = Username.Text.Trim();

                MessageReceivedEventHandler? message = null;
                message = (proc) =>
                {
                    if (proc.Type == ProcedureType.LoginResponse)
                    {
                        App.Client.MessageReceived -= message;

                        LoginResponse res = (LoginResponse)proc;
                        switch (res.State)
                        {
                            case LoginState.Success:
                                App.Token = res.LoginToken;
                                App.TokenGenerated = DateTime.Now;
                                App.UserName = res.Name;
                                Dispatcher.Invoke(() =>
                                {
                                    new MainWindow().Show();
                                    Close();
                                });
                                break;
                            case LoginState.UnsufficentRights:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Nemáte dostatečná práva k přihlášení do aplikace.", "Nedostatečná práva", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                            case LoginState.UserBanned:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Uživatelský přístup vám byl zablokován.", "Uživatel zablokován", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                            case LoginState.BadPassword:
                                Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show("Zadali jste nesprávné přihlašovací údaje.", "Nesprávné údaje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    LoginButton.IsEnabled = true;
                                });
                                break;
                        }
                    }
                };

                App.Client.MessageReceived += message;

                LoginButton.IsEnabled = false;

                Task.Run(() =>
                {
                    App.Client.SendMessageAsync(new LoginRequest(username, password));
                });
            }
        }
    }
}
