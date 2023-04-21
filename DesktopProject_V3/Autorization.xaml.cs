using DesktopProject_V3.DataBaseClass;
using MaterialDesignColors.Recommended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
            TextSuccess.Foreground = new SolidColorBrush(Colors.Transparent);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RegAcc_Click(object sender, RoutedEventArgs e)
        {
            RegAcc ra = new RegAcc();
            ra.Show();
            this.Close();
        }

        private void RetPass_Click(object sender, RoutedEventArgs e)
        {
            ReturnPass rt = new ReturnPass();
            rt.Show();
            this.Close();
        }

        private void AutButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                IconSuccess.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloseCircleOutline;
                TextSuccess.Foreground = new SolidColorBrush(Colors.Red);
                TextSuccess.Text = "Пустые поля!";
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    foreach(var client in db.Users)
                    {
                        if(LoginBox.Text == client.LoginOfUser)
                        {
                            if(PasswordBox.Password == client.PasswordOfUser)
                            {
                                Avatar ava = new Avatar();
                                BitmapImage av = new BitmapImage();
                                IconSuccess.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckCircleOutline;
                                TextSuccess.Foreground = new SolidColorBrush(Colors.Green);
                                TextSuccess.Text = "Успешная авторизация !";
                                av = ava.ToImage(client.Avatar);
                                Avatar.ava = av;
                                Initial.login = client.LoginOfUser;
                                Menu menu = new Menu();
                                menu.Show();
                                this.Close();
                            }
                            else
                            {
                                IconSuccess.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloseCircleOutline;
                                TextSuccess.Foreground = new SolidColorBrush(Colors.Red);
                                TextSuccess.Text = "Неверный пароль!";
                               
                            }
                        }
                        else
                        {
                            IconSuccess.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloseCircleOutline;
                            TextSuccess.Foreground = new SolidColorBrush(Colors.Red);
                            TextSuccess.Text = "Неверный логин!";
                        }
                    }
                }
            }

        }
    }
}
