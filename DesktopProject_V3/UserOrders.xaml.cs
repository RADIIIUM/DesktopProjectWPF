using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для UserOrders.xaml
    /// </summary>
    public partial class UserOrders : Window
    {
        public UserOrders()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                try
                {
                    foreach (var o in db.Orders_Users)
                    {
                        if (o.LoginOfUser == Initial.login)
                        {
                            foreach (var or in db.Orders)
                            {
                                if (o.ID_Order == or.ID_Order)
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
            }
        }

        private void HideOrder_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1())
            {
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
                    foreach (var i in newList)
                    {
                        AllSum.Text = $"{(int.Parse(AllSum.Text) + i.Price)}";
                        AllProducts.Text = $"{int.Parse(AllProducts.Text) + newList.Count}";
                    }
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
                            if (item2.ID_Product == item.ID_Product)
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
            using (Model1 db = new Model1())
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены?", "Вы уверены, что хотите отменить заказ?", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    int? ido = int.Parse(ListOrders.SelectedItem.ToString().Split('|')[0].Replace(" ", "") ?? null);
                    int id = db.Orders.FirstOrDefault(x => x.ID_Order == ido).ID_Order;

                    if (id != null)
                    {
                        Orders ord = db.Orders.FirstOrDefault(x => x.ID_Order == id);
                        ord.StatusOfOrder = "Отклонен";
                        db.SaveChanges();
                        MessageBox.Show("Заказ был отменен");
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Initial.login = Initial.OldLogin;
        }
    }
}
