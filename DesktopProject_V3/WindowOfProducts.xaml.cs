using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для WindowOfProducts.xaml
    /// </summary>
    /// 
    public class Prod
    {
        public Prod(int id, string name, string desc, string spec, int cena, string akcia, ImageBrush img, int count, string type) 
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.Specification = spec;
            this.Cena = cena;
            this.Akcia = akcia;
            this.Count = count;
            this.im = img;
            this.Type = type;
        }

        public Prod() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public int Cena { get; set; }
        public string Akcia { get; set; }

        public string Type { get; set; }

        public int Count { get; set;  }

        public ImageBrush im { get; set; }
    }
    public partial class WindowOfProducts : Window
    {
        public ObservableCollection<Prod> NP { get; set; }
        public WindowOfProducts()
        {
            InitializeComponent();
            ProductList.Items.Clear();
            Avatar av = new Avatar();
            using (Model1 db = new Model1())
            {
                
                NP = new ObservableCollection<Prod>();
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
                                            if (prod.Discount > 0)
                                            {
                                                int cena = prod.Discount ?? 0;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                int count;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType; 
                                                try
                                                {
                                                   count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                int count;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch(System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()),tip);
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
                                                int cena = prod.Discount ?? 0;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
                                                int cena = prod.Discount ?? 0;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
                                                int cena = prod.Discount ?? 0;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Product ?? 0;
                                                string tip = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(av.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
            Initial.IDProduct = -1;
            Product pr = new Product();
            pr.ShowDialog();
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                Prod p = (Prod)ProductList.SelectedItem;
                Model1 db = new Model1();
                int id = db.Products.First(x => x.ID_Product == p.ID).ID_Product;
                Initial.IDProduct = id;
                Product pr = new Product();
                pr.ShowDialog();

        }
    }
}
