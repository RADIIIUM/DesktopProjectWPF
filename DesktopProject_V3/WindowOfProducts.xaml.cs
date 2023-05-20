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
        public string ctg {get;set;}
        public WindowOfProducts()
        {
            InitializeComponent();
            ProductList.Items.Clear();
            Avatar avat = new Avatar();
            using (Model1 db = new Model1())
            {
                string role = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
                if ((role.Replace(" ", "") == "Администратор"))
                {
                    AddProduct.Visibility = Visibility.Visible;
                }
                else
                {
                    AddProduct.Visibility = Visibility.Collapsed;
                }
                    NP = new ObservableCollection<Prod>();
                foreach (var user in db.Users)
                {
                    if (user.LoginOfUser == Initial.login)
                    {
                        UserName.Text = user.NameOfUser;
                        BalanceUser.Text = (user.Balance == null ? 0 : user.Balance).ToString();
                    }
                }
                int max;
                int min;
                switch (Initial.NameOfCategoryOfProduct)
                {
                    case "Computers":
                        var type = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x.ID_Type).ToList();
                        var typesearch = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x);
                        this.ctg = "_CMP";
                        foreach (var t in typesearch)
                        {
                            TypeOfTechnikCombo.Items.Add(t.NameOfType.Split('_')[0]);
                        }

                        foreach (var t in type)
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
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                int count;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0]; 
                                                try
                                                {
                                                   count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price,cena.ToString() , new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                int count;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch(System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()),tip);
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            max = NP.Select(x => x.Cena).Max();
                            min = NP.Select(x => x.Cena).Min();
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        catch
                        {
                            max = 10;
                            min = 0;
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Notebook":
                        var type1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x.ID_Type).ToList();
                        var typesearch1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x);
                        this.ctg = "_NTB";
                        foreach (var t in typesearch1)
                        {
                            TypeOfTechnikCombo.Items.Add(t.NameOfType.Split('_')[0]);
                        }
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
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            max = NP.Select(x => x.Cena).Max();
                            min = NP.Select(x => x.Cena).Min();
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        catch
                        {
                            max = 10;
                            min = 0;
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Accessory":
                        var type2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x.ID_Type).ToList();
                        var typesearch2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x);
                        this.ctg = "_ACS";
                        foreach (var t in typesearch2)
                        {
                            TypeOfTechnikCombo.Items.Add(t.NameOfType.Split('_')[0]);
                        }
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
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            max = NP.Select(x => x.Cena).Max();
                            min = NP.Select(x => x.Cena).Min();
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        catch
                        {
                            max = 10;
                            min = 0;
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    case "Complect":
                        var type3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x.ID_Type).ToList();
                        var typesearch3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x);
                        this.ctg = "_CKT";
                        foreach (var t in typesearch3)
                        {
                            TypeOfTechnikCombo.Items.Add(t.NameOfType.Split('_')[0]);
                        }
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
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                            else
                                            {
                                                int cena = prod.Price;
                                                int count;
                                                int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                try
                                                {
                                                    count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                }
                                                catch (System.InvalidOperationException ex)
                                                {
                                                    count = 0;
                                                }
                                                Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                    prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                NP.Add(p);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            max = NP.Select(x => x.Cena).Max();
                            min = NP.Select(x => x.Cena).Min();
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        catch
                        {
                            max = 10;
                            min = 0;
                            MaxPriceSlider.Maximum = max;
                            MaxPriceSlider.Minimum = min;
                            MinPriceSlider.Maximum = max;
                            MinPriceSlider.Minimum = min;
                            MaxPriceSlider.Value = max;
                            MinPriceSlider.Value = min;
                        }
                        ProductList.ItemsSource = NP;
                        break;

                    default:
                        break;

                }
                TypeOfTechnikCombo.Items.Add("Все товары");
                ImageBrush av = new ImageBrush();
                av.ImageSource = Avatar.ava;
                ProfileIcon.Fill = av;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ProfileIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserProfile up = new UserProfile();
            up.Owner = this;
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ProductList.ItemsSource = NP.Where(x => (x.Cena <= int.Parse(MaxText.Text)) && (x.Cena >= int.Parse(MinText.Text)) && (x.Name.ToLower().IndexOf(SearchBox.Text.ToLower()) >= 0)).ToList();
            }
            catch
            {
                return;
            }
        }

        private void TypeOfTechnikCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NP.Clear();
            Avatar avat = new Avatar();
            using (Model1 db = new Model1())
            {
                string typeofCombo = TypeOfTechnikCombo.SelectedItem.ToString()
                    + this.ctg;
                if (TypeOfTechnikCombo.SelectedItem.ToString() != "Все товары")
                {
                    var tmp = (db.TypesOfProducts.FirstOrDefault(y => y.NameOfType.IndexOf(typeofCombo) >= 0).ID_Type);
                    var idproducts = db.Types_Products.Where(x => x.ID_Type == tmp).Select(x => x.ID_Product).ToList();
                    foreach (var pr in idproducts)
                    {
                        foreach (var produt in db.Products)
                        {
                            if (produt.ID_Product == pr)
                            {
                                if (produt.Discount > 0)
                                {
                                    int cena = produt.Discount ?? 0;
                                    int idtp = db.Types_Products.First(x => x.ID_Product == produt.ID_Product).ID_Type ?? 0;
                                    int count;
                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                    try
                                    {
                                        count = db.Warehouses.First(x => x.ID_Product == produt.ID_Product).CountOfProduct ?? 0;
                                    }
                                    catch (System.InvalidOperationException ex)
                                    {
                                        count = 0;
                                    }
                                    Prod p = new Prod(produt.ID_Product, produt.NameOfProduct, produt.DescriptionOfProduct,
                                        produt.Specifications, produt.Price, cena.ToString(), new ImageBrush(avat.ToImage(produt.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                    NP.Add(p);
                                }
                                else
                                {
                                    int cena = produt.Price;
                                    int idtp = db.Types_Products.First(x => x.ID_Product == produt.ID_Product).ID_Type ?? 0;
                                    int count;
                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                    try
                                    {
                                        count = db.Warehouses.First(x => x.ID_Product == produt.ID_Product).CountOfProduct ?? 0;
                                    }
                                    catch (System.InvalidOperationException ex)
                                    {
                                        count = 0;
                                    }
                                    Prod p = new Prod(produt.ID_Product, produt.NameOfProduct, produt.DescriptionOfProduct,
                                        produt.Specifications, produt.Price, "", new ImageBrush(avat.ToImage(produt.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                    NP.Add(p);
                                }
                            }
                        }
                    }
                }
                else
                {
                    switch (Initial.NameOfCategoryOfProduct)
                    {
                        case "Computers":
                            var type = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x.ID_Type).ToList();
                            this.ctg = "_CMP";
                            foreach (var t in type)
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
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    int count;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, cena.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    int count;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
                            this.ctg = "_NTB";
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
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
                            this.ctg = "_ACS";

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
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
                            this.ctg = "_CKT";
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
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, cena, prod.Price.ToString(), new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
                                                    NP.Add(p);
                                                }
                                                else
                                                {
                                                    int cena = prod.Price;
                                                    int count;
                                                    int idtp = db.Types_Products.First(x => x.ID_Product == prod.ID_Product).ID_Type ?? 0;
                                                    string tip = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                                                    try
                                                    {
                                                        count = db.Warehouses.First(x => x.ID_Product == prod.ID_Product).CountOfProduct ?? 0;
                                                    }
                                                    catch (System.InvalidOperationException ex)
                                                    {
                                                        count = 0;
                                                    }
                                                    Prod p = new Prod(prod.ID_Product, prod.NameOfProduct, prod.DescriptionOfProduct,
                                                        prod.Specifications, prod.Price, "", new ImageBrush(avat.ToImage(prod.AvatarOfProduct)), int.Parse(count.ToString()), tip);
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
        }

        private void MaxMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(SearchBox.Text))
                { 
                    ProductList.ItemsSource = NP.Where(x => (x.Cena <= int.Parse(MaxText.Text)) && (x.Cena >= int.Parse(MinText.Text))).ToList();
                }
                else
                {
                    ProductList.ItemsSource = NP.Where(x => (x.Cena <= int.Parse(MaxText.Text)) && (x.Cena >= int.Parse(MinText.Text)) && (x.Name.ToLower().IndexOf(SearchBox.Text.ToLower()) >= 0)).ToList();
                }
            }
            catch
            {
            }
        }
    }
}
