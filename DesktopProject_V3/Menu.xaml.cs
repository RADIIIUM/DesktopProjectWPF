using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 

    public class UserItem
    {
        public UserItem(string Login, string Name, string Role, ImageBrush ava)
        {
            this.Login = Login;
            this.Name = Name;
            this.Role = Role;
            this.Avatar = ava;
        }
        public ImageBrush Avatar { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
    public class ProductItem
    {
        public ProductItem()
        {

        }

        public ProductItem(string name, int count, ImageBrush avatar, int price, string adress)
        {
            this.Name = name;
            this.Count = count;
            this.avatar = avatar;
            this.Price = price;
            this.AdressDelivery = adress;



        }

        public string Name { get; set; }
        public int Count { get; set; }
        public ImageBrush avatar { get; set; }

        public int Price { get; set; }

        public string AdressDelivery { get; set; }

    }
   
    public partial class Menu : Window
    {
        public static Menu menu;
        public ObservableCollection<ProductItem> ProductsList { get; set; }

        public ObservableCollection<UserItem> UserList { get; set; }
        public Menu()
        {
            InitializeComponent();
            Avatar ava = new Avatar();
            using (Model1 db = new Model1())
            {
                try
                {
                    var orders = db.Orders_Users.Where(x => x.LoginOfUser == Initial.login).Select(x => x.ID_Order).ToList();
                    var orders2 = db.Orders.Where(x => orders.FindAll(t => t == x.ID_Order) != null).ToList();
                    var orders_product = db.Orders_Products.Where(x => orders.FindAll(t => t == x.ID_Order) != null).ToList();
                    var productsInOrder = db.Products.Where(x => orders_product.FindAll(t => t.ID_Product == x.ID_Product) != null).ToList();
                    foreach(var p in orders2)
                    {
                      ListOrders.Items.Add($"{p.ID_Order} - {p.DataOrder} - {p.TypeOfDelivery} - {p.TypeOfPayment}");
                    }  
                }

                catch
                {

                }
                UserList = new ObservableCollection<UserItem>();
                foreach (var user in db.Users)
                    {
                    if (user != null)
                    {
                        try
                        {
                            string role = db.Roles.FirstOrDefault(x => x.LoginOfUsers == user.LoginOfUser).NameOfRole ?? null;
                            UserItem ui = new UserItem(user.LoginOfUser, user.NameOfUser, role, new ImageBrush(ava.ToImage(user.Avatar)));
                            UserList.Add(ui);
                        }
                        catch (System.NullReferenceException ex)
                        { 
                            
                        }
                    }
                    }
                    MenuUserList.ItemsSource = UserList;

                string role2 = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
                if ((role2.Replace(" ", "") == "Администратор") || (role2.Replace(" ", "") == "Модератор"))
                {
                    AddNews.Visibility = Visibility.Visible;
                    UsersTabItem.Visibility = Visibility.Visible;
                }
                else
                {
                    AddNews.Visibility = Visibility.Collapsed;
                    UsersTabItem.Visibility = Visibility.Collapsed;
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
            up.Owner = this;
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

        private void ListOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (Model1 db = new Model1())
                {
                string item = ListOrders.SelectedItem.ToString();
                string id = item.Split('-')[0];
                Orders or = db.Orders.FirstOrDefault(x => x.ID_Order == int.Parse(id));
                    AddressUser.Text = or.DeliveryAdress;
                    PaymentMethod.SelectedIndex = (PaymentMethod.Items.IndexOf(or.TypeOfPayment));
                    var products = db.Products.Where(x =>(db.Orders_Products.Where(y => y.ID_Order == int.Parse(id))) != null).ToList();
                    foreach(var pr in products)
                    {
                        Avatar avat = new Avatar();
                        int countOfProduct = db.Orders_Products.FirstOrDefault(x => x.ID_Product == pr.ID_Product).CountOfProduct ?? 0;
                        Orders ord = db.Orders.FirstOrDefault(x => x.ID_Order == int.Parse(id));
                        ProductItem p = new ProductItem(pr.NameOfProduct, countOfProduct, new ImageBrush(avat.ToImage(pr.AvatarOfProduct)),pr.Price*countOfProduct,ord.DeliveryAdress);
                        ProductsList.Add(p);
                    }
                    ListOfProducts.ItemsSource = ProductsList;
                }
            }
            catch
            {

            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                if(ListOfProducts.Items.Count <= 0)
                {
                    MessageBox.Show(" Нельзя оформить пустой заказ ");
                }
                if (string.IsNullOrEmpty(AddressUser.Text) || 
                    string.IsNullOrEmpty(PaymentMethod.SelectedItem.ToString()) && ListOfProducts.Items.Count > 0)
                {
                    MessageBox.Show(" Строки адреса и выбор метода оплаты не должны быть пустыми ");
                }
                else
                {
                    Orders ord = new Orders();
                }
            }
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BanUserBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1())
            {
                UserItem ui = (UserItem)MenuUserList.SelectedItem;
                string r = db.Roles.FirstOrDefault(x => x.LoginOfUsers == Initial.login).NameOfRole;
                if ((ui.Role == "Модератор" || ui.Role == "Администратор") && r != "Администратор")
                {
                    MessageBox.Show("Нельзя заблокировать модератора/администратора");
                    return;
                }
            if(ui.Role == "Администратор")
                {
                    MessageBox.Show("Нельзя заблокировать администратора!");
                    return;
                }
                if (ui.Login != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите\nзаблокировать пользователя? {ui.Login}", "Заблокировать пользователя?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                            Roles roles = new Roles("Заблокированный", ui.Login);
                            db.Roles.Add(roles);
                            db.SaveChanges();
                            MessageBox.Show($"{ui.Login} был заблокирован");
                        
                    }
                }
                else
                {
                    MessageBox.Show($"Выберите пользователя");
                }
        }
        }

        private void OpenProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            UserItem ui = (UserItem)MenuUserList.SelectedItem;
            if (ui.Login != null)
            {
                Initial.ShowProfile = true;
                Initial.OldLogin = Initial.login;
                Initial.login = ui.Login;
                UserProfile up = new UserProfile();
                up.Show();
            }
            else
            {
                MessageBox.Show($"Выберите пользователя");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                if (!string.IsNullOrEmpty(SearchBox.Text))
                {


                    Avatar ava = new Avatar();
                    UserList = new ObservableCollection<UserItem>();
                    var users = from t in db.Users
                                where t.NameOfUser.ToLower().IndexOf(SearchBox.Text.ToLower()) >= 0
                                select t;

                    foreach (var user in users)
                    {
                        if (user != null)
                        {
                            try
                            {
                                string role = db.Roles.FirstOrDefault(x => x.LoginOfUsers == user.LoginOfUser).NameOfRole ?? null;
                                UserItem ui = new UserItem(user.LoginOfUser, user.NameOfUser, role, new ImageBrush(ava.ToImage(user.Avatar)));
                                UserList.Add(ui);
                            }
                            catch (System.NullReferenceException ex)
                            {

                            }
                        }
                    }
                    MenuUserList.ItemsSource = UserList;
                }
                else
                {
                    Avatar ava = new Avatar();
                    UserList = new ObservableCollection<UserItem>();
                    foreach (var user in db.Users)
                    {
                        if (user != null)
                        {
                            try
                            {
                                string role = db.Roles.FirstOrDefault(x => x.LoginOfUsers == user.LoginOfUser).NameOfRole ?? null;
                                UserItem ui = new UserItem(user.LoginOfUser, user.NameOfUser, role, new ImageBrush(ava.ToImage(user.Avatar)));
                                UserList.Add(ui);
                            }
                            catch (System.NullReferenceException ex)
                            {

                            }
                        }
                    }
                    MenuUserList.ItemsSource = UserList;
                }
            }
        }
    }
}