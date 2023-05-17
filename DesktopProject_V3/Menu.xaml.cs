using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public static Menu menu;
        public Menu()
        {
            InitializeComponent();
            Avatar ava = new Avatar();
            using (Model1 db = new Model1())
            {
                foreach (var r in db.Roles)
                {
                    if (r.LoginOfUsers.Replace(" ", "") == Initial.login.Replace(" ", "") && (r.NameOfRole.Replace(" ", "") == "Администратор" || r.NameOfRole.Replace(" ", "") == "Модератор"))
                    {
                        this.AddNews.Visibility = Visibility.Visible; 
                    }
                    else
                    {
                        this.AddNews.Visibility = Visibility.Collapsed; 
                    }

                }
                foreach (var user in db.Users)
                {
                    if (user.LoginOfUser == Initial.login)
                    {
                        UserName.Text = user.NameOfUser;
                        BalanceUser.Text = (user.Balance == null ? 0 : user.Balance).ToString();
                    }
                }
                var sortList = from n in db.News
                               orderby n.ID_New descending
                               select n;
                foreach(var n in sortList) 
                {
                    if(n.Fix == true)
                    {
                        
                        if(MainOneNews.Name == "MainOneNews")
                        {
                            MainOneNews.Background = new ImageBrush(ava.ToImage(n.MainImage));
                            MainOneNews.Name = "Name" + n.ID_New.ToString();
                            continue;
                        }
                        if (MainTwoNews.Name == "MainTwoNews")
                        {
                            
                            MainTwoNews.Background = new ImageBrush(ava.ToImage(n.MainImage));
                            MainTwoNews.Name = "Name" + n.ID_New.ToString();
                            continue;
                        }
                        if (MainThreeNews.Name == "MainThreeNews")
                        {
                            MainThreeNews.Background = new ImageBrush(ava.ToImage(n.MainImage));
                            MainThreeNews.Name = "Name" + n.ID_New.ToString();
                            continue;
                        }

                    }
                    if(n.Fix == false || n.Fix == null)
                    {
                        if (Paragraph1.Name == "Paragraph1")
                        {

                                Paragraph1.Text = n.Paragraph;
                                Img1.Fill = new ImageBrush(ava.ToImage(n.MainImage));
                            Desc1.Text = n.DescriptionOfNews.ToString();
                            N1.Name = "Name" + n.ID_New.ToString();
                            Paragraph1.Name = "News1";
                            continue;


                    }
                        if (Paragraph2.Name == "Paragraph2")
                        {

                                Paragraph2.Text = n.Paragraph;
                                Img2.Fill = new ImageBrush(ava.ToImage(n.MainImage));
                                Desc2.Text = n.DescriptionOfNews.ToString();
                            N2.Name = "Name" + n.ID_New.ToString();
                            Paragraph2.Name = "News2";
                            continue;


                        }
                        if (Paragraph3.Name == "Paragraph3")
                        {

                                Paragraph3.Text = n.Paragraph;
                                Img3.Fill = new ImageBrush(ava.ToImage(n.MainImage));
                                Desc3.Text = n.DescriptionOfNews.ToString();
                            N3.Name = "Name" + n.ID_New.ToString();
                            Paragraph3.Name = "News3";
                            continue;


                        }
                        if (Paragraph4.Name == "Paragraph4")
                        {

                                Paragraph4.Text = n.Paragraph;
                                Img4.Fill = new ImageBrush(ava.ToImage(n.MainImage));
                                Desc4.Text = n.DescriptionOfNews.ToString();
                            N4.Name = "Name" + n.ID_New.ToString();
                            Paragraph4.Name = "News4";
                            continue;

                        }

                    }
                }
            }
            ImageBrush av = new ImageBrush();
            av.ImageSource = Avatar.ava;
            ProfileIcon.Fill = av;
            menu = this;


        }

        public void Drag(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Menu.menu.DragMove();
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

        private void TextBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock txt = (TextBlock)sender;
            txt.Foreground = new SolidColorBrush(Colors.White);
        }

        private void ProfileIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserProfile up = new UserProfile();
            up.ShowDialog();
        }

        private void AddNews_Click(object sender, RoutedEventArgs e)
        {
            WndowOfNew wn = new WndowOfNew();
            Initial.NumberOfNews = -1;
            wn.ShowDialog();
            
        }

        private void AllNews_Click(object sender, RoutedEventArgs e)
        {
            AllNews an = new AllNews();
            an.ShowDialog();
        }

        private void MainTwoNews_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid g = (Grid)sender;
            try {
            using(Model1 db = new Model1())
            {
                foreach(var n in db.News)
                {
                    if(n.ID_New == int.Parse(g.Name.ToString().Replace("Name", "")))
                        
                    {
                        
                        Initial.NumberOfNews = n.ID_New;
                        WndowOfNew wn = new WndowOfNew();
                        wn.ShowDialog();
                    }
                }
            }
            }
            catch
            {
                MessageBox.Show("Возникла какая-то ошибка");
            }
        }

        private void OpenMiniNews_Click(object sender, RoutedEventArgs e)
        {
            try { 
            Button b = (Button)sender;
            using (Model1 db = new Model1())
            {
                foreach (var n in db.News)
                {
                    if (n.ID_New == int.Parse(b.Name.ToString().Replace("Name", "")))

                    {
                        Initial.NumberOfNews = n.ID_New;
                            WndowOfNew wn = new WndowOfNew();
                            wn.ShowDialog();
                    }
                }
            }
            }
            catch
            {
                MessageBox.Show("Возникла какая-то ошибка");
            }
        }

        private void Details_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tx = (TextBlock)sender;
            Initial.NameOfCategoryOfProduct = tx.Name;
            WindowOfProducts wp = new WindowOfProducts();
            this.Close();
            wp.ShowDialog();
        }
    }
}