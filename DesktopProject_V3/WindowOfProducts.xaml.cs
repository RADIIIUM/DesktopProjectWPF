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
    /// Логика взаимодействия для WindowOfProducts.xaml
    /// </summary>
    /// 
    public class Prod
    {
        public Prod(int id, string name, string desc, string spec, int cena, string akcia, ImageBrush img) 
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.Specification = spec;
            this.Cena = cena;
            this.Akcia = akcia;
        }

        public Prod() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public int Cena { get; set; }
        public string Akcia { get; set; }

        public ImageBrush img { get; set; }
    }
    public partial class WindowOfProducts : Window
    {
        public ObservableCollection<Prod> NP { get; set; }
        public WindowOfProducts()
        {
            InitializeComponent();
            using(Model1 db = new Model1())
            {
                NP = new ObservableCollection<Prod>();
                Avatar av = new Avatar();
                foreach (var user in db.Users)
                {
                    if (user.LoginOfUser == Initial.login)
                    {
                        UserName.Text = user.NameOfUser;
                        BalanceUser.Text = (user.Balance == null ? 0 : user.Balance).ToString();
                    }
                }
                switch(Initial.NameOfCategoryOfProduct)
                {
                    case "Computers":
                        var type = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x.ID_Type).ToList();
                        foreach(var t in type)
                        {
                            foreach (var tp in db.Types_Products)
                            {
                                if (t == tp.ID_Type)
                                {
                                    foreach(var prod in db.Products)
                                    {
                                        if(prod.ID_Product ==  tp.ID_Product)
                                        {
                                            if(prod.Discount > 0)
                                            {
                                                int discount = prod.Discount ?? 0;
                                                int cena = (prod.Price * discount) / 100;
                                                
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Notebook":
                        var type1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x.ID_Type).ToList();
                        foreach (var t in type1)
                        {
                            foreach (var tp in db.Types_Products)
                            {
                                if (t == tp.ID_Type)
                                {
                                    foreach (var prod in db.Products)
                                    {
                                        if (prod.ID_Product == tp.ID_Product)
                                        {
                                            if (prod.Discount > 0)
                                            {
                                                int discount = prod.Discount ?? 0;
                                                int cena = (prod.Price * discount) / 100;

                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Accessory":
                        var type2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x.ID_Type).ToList();
                        foreach (var t in type2)
                        {
                            foreach (var tp in db.Types_Products)
                            {
                                if (t == tp.ID_Type)
                                {
                                    foreach (var prod in db.Products)
                                    {
                                        if (prod.ID_Product == tp.ID_Product)
                                        {
                                            if (prod.Discount > 0)
                                            {
                                                int discount = prod.Discount ?? 0;
                                                int cena = (prod.Price * discount) / 100;

                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Complect":
                        var type3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x.ID_Type).ToList();
                        foreach (var t in type3)
                        {
                            foreach (var tp in db.Types_Products)
                            {
                                if (t == tp.ID_Type)
                                {
                                    foreach (var prod in db.Products)
                                    {
                                        if (prod.ID_Product == tp.ID_Product)
                                        {
                                            if (prod.Discount > 0)
                                            {
                                                int discount = prod.Discount ?? 0;
                                                int cena = (prod.Price * discount) / 100;

                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct, prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)));
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    default:
                        break;

                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ProfileIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserProfile up = new UserProfile();
            up.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Emblema_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Menu mn = new Menu();
            this.Close();
            mn.ShowDialog();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Product pr = new Product();
            pr.ShowDialog();
        }
    }
}
