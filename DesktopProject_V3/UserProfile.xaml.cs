﻿using DesktopProject_V3.DataBaseClass;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
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
using System.Windows.Shapes;

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        public static byte[] bytt;
        public UserProfile()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                if (Initial.ShowProfile)
                {
                    Address.IsReadOnly = true;
                    Username.IsReadOnly = true;
                    Email.IsReadOnly = true;
                    BankCardBox.Visibility = Visibility.Collapsed;
                    Telephone.IsReadOnly = true;
                    Passport.IsReadOnly = true;
                    ChangeAvatar.Visibility = Visibility.Collapsed;
                    ChangePassStackPanel.Visibility = Visibility.Collapsed;
                    ChangePassStackPanel.Visibility = Visibility.Collapsed;
                    LeaveAccount.Visibility = Visibility.Collapsed;
                    DeleteProfile.Visibility = Visibility.Collapsed;
                    BalanceBorder.Visibility = Visibility.Collapsed;
                    string r = db.Roles.FirstOrDefault(x => x.LoginOfUsers == Initial.OldLogin).NameOfRole;
                    if (r == "Администратор") ChangeStackPanel.Visibility = Visibility.Visible;
                    else ChangeStackPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Address.IsReadOnly = false;
                    Username.IsReadOnly = false;
                    Email.IsReadOnly = false;
                    BankCardBox.Visibility = Visibility.Visible;
                    Telephone.IsReadOnly = false;
                    Passport.IsReadOnly = false;
                    ChangePassStackPanel.Visibility = Visibility.Visible;
                    ChangePassStackPanel.Visibility = Visibility.Visible;
                    ChangeAvatar.Visibility = Visibility.Visible;
                    LeaveAccount.Visibility = Visibility.Visible;
                    DeleteProfile.Visibility = Visibility.Visible;
                    BalanceBorder.Visibility = Visibility.Visible;
                    ChangeStackPanel.Visibility = Visibility.Collapsed;
                }
                foreach (var user in db.Users)
                {
                    if(user.LoginOfUser == Initial.login)
                    {
                        Username.Text = user.NameOfUser;
                        Passport.Text = user.Passport;
                        Address.Text = user.AddressOfUser;
                        Telephone.Text = user.Telephone;
                        Email.Text = user.Email;
                        BankCardBox.Password = user.BankCard;
                        Description.Text = user.DescriptionOfUser;
                        Avatar ava = new Avatar();
                        ProfileAva.Fill = new ImageBrush(ava.ToImage(user.Avatar));
                        BalanceTB.Text = (user.Balance == null ? 0 : user.Balance).ToString();
                        foreach (var privilege in db.Privilege_Users)
                        {
                            if(privilege.LoginUser == Initial.login)
                            {
                                foreach(var pr in db.Privilege)
                                {
                                    if(pr.ID_Status == privilege.ID_Provolege)
                                    {
                                        UserStatus_TB.Text = pr.NameOfStatus;
                                    }
                                }
                            }
                        }
                        foreach(var roles in db.Roles)
                        {
                            if(roles.LoginOfUsers == Initial.login)
                            {
                                foreach(var r in db.Roles)
                                {
                                    if(Initial.login == r.LoginOfUsers)
                                    {
                                        RoleUser_TB.Text = r.NameOfRole;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                if (!Initial.ShowProfile)
                {
                    foreach (var save in db.Users)
                    {
                        if (Initial.login == save.LoginOfUser)
                        {
                            if (UserProfile.bytt != null)
                            {
                                save.Avatar = UserProfile.bytt;
                            }
                            save.NameOfUser = Username.Text;
                            save.BankCard = BankCardBox.Password;
                            save.Passport = Passport.Text;
                            save.AddressOfUser = Address.Text;
                            save.DescriptionOfUser = Description.Text;
                            save.Email = Email.Text;
                            save.Telephone = Telephone.Text;
                            MessageBox.Show("Изменения сохранены");
                        }
                    }
                    db.SaveChanges();
                }
                Initial.login = Initial.OldLogin;
                Initial.ShowProfile = false;
                    this.Close();
            }


        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            if((string.IsNullOrEmpty(NewPassword.Password) || string.IsNullOrEmpty(RepeatPass.Password)) || 
                NewPassword.Password != RepeatPass.Password)
            {
                MessageBox.Show("Поля не должны быть пустыми \n А также пароли должны совпадать");
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    foreach (var ch in db.Users)
                    {
                        if (ch.LoginOfUser == Initial.login)
                        {
                            ch.PasswordOfUser = NewPassword.Password;
                            MessageBox.Show("Пароль изменен");
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            Bitmap image; //Bitmap для открываемого изображения
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == true) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    Avatar av = new Avatar();
                    BitmapImage img = new BitmapImage();
                    img = av.BitmapToBitmapImage(image);
                    UserProfile.bytt = av.getJPGFromImageControl(img);
                    ImageBrush im = new ImageBrush();
                    im.ImageSource = img;
                    ProfileAva.Fill = im;
                }
                catch
                {
                    MessageBox.Show("Ошибка формата файла");
                }

            }
        }

        private void LeaveAccount_Click(object sender, RoutedEventArgs e)
        {
            Initial.login = null;
            Autorization aut = new Autorization();
            this.Close();
            this.Owner.Close();
            aut.Show();
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены в своем решении? \n Восстановить аккаунт будет невозможно!", "Вы уверены?", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                using(Model1 db = new Model1())
                {
                    Users u = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login);
                    db.Users.Remove(u);
                    MessageBox.Show($"Пользователь {Initial.login} был удален");
                    db.SaveChanges();
                    Initial.login = null;
                    Autorization aut = new Autorization();
                    this.Close();
                    this.Owner.Close();
                    aut.Show();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

                MessageBoxResult mgr = MessageBox.Show("Вы уверены, что хотите изменить роль?", "Сменить роль?", MessageBoxButton.YesNo);
                if(mgr == MessageBoxResult.Yes)
                {
                    using(Model1 db = new Model1())
                    {
                        foreach(var roles in db.Roles)
                        {
                            if(roles.LoginOfUsers == Initial.login)
                            {
                            string role = ChangeRoleCombo.Text;
                                
                                if (!string.IsNullOrEmpty(role))
                                {
                                    roles.NameOfRole = role;
                                    roles.LoginOfUsers = Initial.login;
                                    MessageBox.Show("Роль пользователя изменена");
                                    break;
                                }
                            }
                        }
                        db.SaveChanges();
                }
            }
        }
    }
}
