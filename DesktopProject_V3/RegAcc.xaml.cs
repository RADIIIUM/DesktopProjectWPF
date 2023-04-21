using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using DesktopProject_V3.DataBaseClass;

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для RegAcc.xaml
    /// </summary>
    public partial class RegAcc : Window
    {
        public static RegAcc RegWin;
            public RegAcc()
        { 
            InitializeComponent();
            RegWin = this;
        }


        public static byte[] byt;

        public void Drag(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                RegAcc.RegWin.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Autor_Click(object sender, RoutedEventArgs e)
        {
            Autorization mw = new Autorization();
            mw.Show();
            this.Close();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                    RegAcc reg = new RegAcc();
                    BitmapImage img = new BitmapImage();
                    img = av.BitmapToBitmapImage(image);
                    RegAcc.byt = av.getJPGFromImageControl(img);
                    ImageBrush im = new ImageBrush();
                    im.ImageSource = img;
                    Avatar.Fill = im;
                }
                catch
                {
                    MessageBox.Show("Ошибка формата файла");
                }

            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            string file = System.IO.Path.GetFullPath("StandartProfile.png");
            if (string.IsNullOrEmpty(EmailBox.Text) || string.IsNullOrEmpty(LoginBox.Text) || string.IsNullOrEmpty(PassnBox.Text) || ((string.IsNullOrEmpty(RepeatPassBox.Text)) &&
                (PassnBox.Text != RepeatPassBox.Text)))
            {
                MessageBox.Show("Ошибка!", "Поля не должны быть пустыми\nили\nПароли введены неверно");
            }
            else
            {
                if (LoginBox.Text.Length > 45 || PassnBox.Text.Length < 6 || PassnBox.Text.Length > 100)
                {
                    MessageBox.Show( "Логин не должен быть больше 45 символов\nТакже пароль не должен быть меньше 6 символов", "Ошибка!");
                }
                else
                {
                using (Model1 de = new Model1())
                {
                    if (RegAcc.byt == null)
                    {
                        Avatar av = new Avatar();
                        BitmapImage img = new BitmapImage(new Uri(file));
                        RegAcc.byt = av.getJPGFromImageControl(img);
                        ImageBrush im = new ImageBrush();
                        im.ImageSource = img;
                        Avatar.Fill = im;
                    }
                        Users us = new Users(LoginBox.Text, PassnBox.Text, EmailBox.Text, RegAcc.byt);
                        Privilege_Users priv = new Privilege_Users(1, LoginBox.Text);
                        Roles rol = new Roles("Пользователь", LoginBox.Text);
                        de.Users.Add(us);
                        de.Privilege_Users.Add(priv);
                        de.Roles.Add(rol);

                    de.SaveChanges();
                    MessageBox.Show($"Пользователь {LoginBox.Text} зарегестрирован");
                   }
                }
            }
        }
    }
}
