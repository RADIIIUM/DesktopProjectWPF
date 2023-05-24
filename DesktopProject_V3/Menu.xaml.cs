using DesktopProject_V3.DataBaseClass;
using MaterialDesignThemes.Wpf;
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
                    foreach(var o in db.Orders_Users)
                    {
                        if(o.LoginOfUser == Initial.login)
                        {
                            foreach (var or in db.Orders)
                            {
                                if (o.ID_Order == or.ID_Order )
                                {
                                    ListOrders.Items.Add($"{or.ID_Order} | {or.DataOrder.Date} | {or.TypeOfDelivery} | {or.StatusOfOrder}");
                                }
                            }
                        }
                    }
                    AllOrdersText.Text = ListOrders.Items.Count.ToString();

                }

                catch
                {

                }

                try
                {
                    ObservableCollection<ProductItem> newList = new ObservableCollection<ProductItem>();
                    List<Products> lp = Initial.Cart;
                    AddressUser.Text = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login).AddressOfUser ?? null;
                    foreach (var item in lp)
                    {
                        int count = lp.Where(x => x.ID_Product == item.ID_Product).Count();
                        ProductItem newp = new ProductItem(item.NameOfProduct, lp.Where(x => x.ID_Product == item.ID_Product).Count(),
                                        new ImageBrush(ava.ToImage(item.AvatarOfProduct)), item.Price * count, AddressUser.Text);
                        if (newList.Where(x => x.Name == item.NameOfProduct).Count() >= 1)
                        {
                            continue;
                        }
                        else
                        {
                            newList.Add(newp);
                        }
                    }

                    if (newList != null)
                    {
                        ListOfProducts.ItemsSource = newList;
                        foreach (var i in newList)
                        {
                            AllSum.Text = $"{(int.Parse(AllSum.Text) + i.Price)}";
                        }
                        AllProducts.Text = newList.Count().ToString();
                    }
                    else MessageBox.Show("Не удалось восстановить старый заказ");
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

        //==============================================
        // Методы для работы с Новостями 
        //==============================================
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

        //==============================================
        // Методы для работы с Магазином 
        //==============================================

        private void Details_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tx = (TextBlock)sender;
            Initial.NameOfCategoryOfProduct = tx.Name;
            WindowOfProducts wp = new WindowOfProducts();
            this.Close();
            wp.ShowDialog();
        }



        //==============================================
        // Методы для работы с Юзерами 
        //==============================================
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

        private void OpenUserOrder_Click(object sender, RoutedEventArgs e)
        {
            UserItem ui = (UserItem)MenuUserList.SelectedItem;
            if (ui.Login != null)
            {
                Initial.OldLogin = Initial.login;
                Initial.login = ui.Login;
                UserOrders up = new UserOrders();
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

        //==============================================
        // Методы для работы с Корзиной 
        //==============================================
        private void HideOrder_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1())
            {
                ButtonCartStack.Visibility = Visibility.Visible;
                Avatar ava = new Avatar();
                ObservableCollection<ProductItem> newList = new ObservableCollection<ProductItem>();
                List<Products> lp = Initial.Cart;
                AddressUser.Text = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login).AddressOfUser ?? null;
                foreach (var item in lp)
                {
                    int count = lp.Where(x => x.ID_Product == item.ID_Product).Count();
                    ProductItem newp = new ProductItem(item.NameOfProduct, lp.Where(x => x.ID_Product == item.ID_Product).Count(),
                                    new ImageBrush(ava.ToImage(item.AvatarOfProduct)), item.Price * count, AddressUser.Text);
                    newList.Add(newp);
                }

                if (newList != null)
                {
                    ListOfProducts.ItemsSource = newList;
                    foreach (var i in newList)
                    {
                        AllSum.Text = $"{(int.Parse(AllSum.Text) + i.Price)}";
                        AllProducts.Text = $"{int.Parse(AllProducts.Text) + newList.Count}";
                    }
                }
                else MessageBox.Show("Не удалось восстановить старый заказ");
            }
        }
        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            AllSum.Text = "0";
            AllProducts.Text = "0";
                using (Model1 db = new Model1())
                {
                    int ido = int.Parse(ListOrders.SelectedItem.ToString().Split('|')[0].Replace(" ", ""));
                    int id = db.Orders.FirstOrDefault(x => x.ID_Order == ido).ID_Order;
                    ObservableCollection<ProductItem> newList = FillListProduct(id);
                    if (newList != null)
                    {
                        ListOfProducts.ItemsSource = newList;
                        foreach(var i in newList)
                        {
                            AllSum.Text = $"{(int.Parse(AllSum.Text) + i.Price)}";
                            AllProducts.Text = $"{int.Parse(AllProducts.Text) + newList.Count}";
                        }
                        ButtonCartStack.Visibility = Visibility.Collapsed;
                    }
                    else MessageBox.Show("Не удалось открыть заказ");
                }
        }

        public ObservableCollection<ProductItem> FillListProduct(int idorder)
        {
            using (Model1 db = new Model1())
            {
                Avatar ava = new Avatar();
                ObservableCollection<ProductItem> newList = new ObservableCollection<ProductItem>();
                foreach (var item in db.Orders_Products)
                {
                    if (idorder == item.ID_Order)
                    {
                        foreach (var item2 in db.Products)
                        {
                            if(item2.ID_Product == item.ID_Product)
                            {
                                int count = db.Orders_Products.Where(x => x.ID_Product == item2.ID_Product && idorder == item.ID_Order).Count();
                                string adress = db.Orders.FirstOrDefault(x => x.ID_Order == idorder).DeliveryAdress;
                                ProductItem newp = new ProductItem(item2.NameOfProduct, count,
                                    new ImageBrush(ava.ToImage(item2.AvatarOfProduct)), item2.Price * count, adress);
                                if (newList.Where(x => x.Name == item2.NameOfProduct).Count() >= 1)
                                {
                                    continue;
                                }
                                else
                                {
                                    newList.Add(newp);
                                }
                            }
                        }
                    }
                }
                return newList;
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
                using(Model1 db = new Model1())
                {

            
                    int? ido = int.Parse(ListOrders.SelectedItem.ToString().Split('|')[0].Replace(" ", "") ?? null);
                    int id = db.Orders.FirstOrDefault(x => x.ID_Order == ido).ID_Order;

                    if(id != null)
                    {

                        Orders ord = db.Orders.FirstOrDefault(x => x.ID_Order == id);
                        ord.StatusOfOrder = "Отменен пользователем";
                        db.SaveChanges();
                        MessageBox.Show("Заказ был отменен");
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                try
                {
                    ProductItem item = (ProductItem)ListOfProducts.SelectedItem;
                    foreach(var pr in Initial.Cart)
                    {
                        if(pr.NameOfProduct == item.Name)
                        {
                            Initial.Cart.Remove(pr); 
                            break;
                        }
                    }
                    try
                    {
                        Avatar ava = new Avatar();
                        ObservableCollection<ProductItem> newList = new ObservableCollection<ProductItem>();
                        List<Products> lp = Initial.Cart;
                        AddressUser.Text = db.Users.FirstOrDefault(x => x.LoginOfUser == Initial.login).AddressOfUser ?? null;
                        foreach (var i in lp)
                        {
                            int count = lp.Where(x => x.ID_Product == i.ID_Product).Count();
                            ProductItem newp = new ProductItem(i.NameOfProduct, lp.Where(x => x.ID_Product == i.ID_Product).Count(),
                                            new ImageBrush(ava.ToImage(i.AvatarOfProduct)), i.Price * count, AddressUser.Text);
                            if (newList.Where(x => x.Name == i.NameOfProduct).Count() >= 1)
                            {
                                continue;
                            }
                            else
                            {
                                newList.Add(newp);
                            }
                        }

                        if (newList != null)
                        {
                            ListOfProducts.ItemsSource = newList;
                            foreach (var i in newList)
                            {
                                AllSum.Text = $"{(int.Parse(AllSum.Text) + i.Price)}";
                            }
                            AllProducts.Text = newList.Count().ToString();
                        }
                        else MessageBox.Show("Не удалось восстановить старый заказ");
                    }
                    catch
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("Вы не выбрали товар для удаления");
                }

            }
        }

        private void NewOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1())
            {
                if (ListOfProducts.Items.Count <= 0)
                {
                    MessageBox.Show(" Нельзя оформить пустой заказ ");
                }
                if (string.IsNullOrEmpty(AddressUser.Text) && ListOfProducts.Items.Count > 0)
                {
                    MessageBox.Show(" Строки адреса и выбор метода оплаты не должны быть пустыми ");
                }
                else
                {
                    if (int.Parse(BalanceUser.Text) < int.Parse(AllSum.Text))
                    {
                        MessageBox.Show("На вашем кошельке недостаточно средств");
                        return;
                    }

                    Orders ord = new Orders("В доставке", "Электронный кошелек", "Доставка по адресу", DateTime.Today.Date, AddressUser.Text);
                    Orders_Users ou = new Orders_Users(Initial.login, ord.ID_Order);
                    foreach(var pr in Initial.Cart)
                    {
                        int count = Initial.Cart.Select(x => x.NameOfProduct == pr.NameOfProduct).Count();
                        Orders_Products op = new Orders_Products(ord.ID_Order, pr.ID_Product, count);
                        db.Orders_Products.Add(op);
                    }
                    foreach(var user in db.Users)
                    {
                        if(user.LoginOfUser == Initial.login)
                        {
                            user.Balance = -user.Balance - int.Parse(AllSum.Text);
                        }
                    }
                    db.Orders.Add(ord);
                    db.Orders_Users.Add(ou);
                    db.SaveChanges();

                    MessageBox.Show("Заказ был добавлен");
                    Initial.Cart = new List<Products>();
                }
            }
        }

        private void AddMoney_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddBalance ab = new AddBalance();
            ab.Show();
        }
    }
}