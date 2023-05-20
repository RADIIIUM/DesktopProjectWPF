using DesktopProject_V3.DataBaseClass;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    /// 
    public class Commnt
    {
        public Commnt() { }

        public Commnt(string Login, string User, string Role, string Desc, int Rating, ImageBrush avatar) 
        { 
            this.avatar = avatar;
            this.UserRole = Role;
            this.UserName = User;
            this.Desc = Desc;
            this.Rating = Rating;
            this.Login = Login;
        }
        public ImageBrush avatar { get; set; }

        public string Login { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Desc { get; set; }
        public int Rating { get; set; }
    }
    public partial class Product : Window
    {
        public static byte[] bt { get; set; }
        public ObservableCollection<Commnt> NP { get; set; }
        public Product()
        {
            InitializeComponent();
            using(Model1 db = new Model1())
            {
                string role = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
                if ((role.Replace(" ", "") == "Администратор" || role.Replace(" ", "") == "Модератор"))
                {
                    SaveBtn.Visibility = Visibility.Visible;
                    NameOfProduct.IsReadOnly = false;
                    DescriptionOfProduct.IsReadOnly = false;
                    SpecificOfProduct.IsReadOnly = false;
                    PriceOfProduct.IsReadOnly = false;
                    AkciaText.IsReadOnly = false;

                }
                else
                {
                    SaveBtn.Visibility = Visibility.Collapsed;
                    NameOfProduct.IsReadOnly = true;
                    DescriptionOfProduct.IsReadOnly = true;
                    SpecificOfProduct.IsReadOnly = true;
                    PriceOfProduct.IsReadOnly = true;
                    AkciaText.IsReadOnly = true;

                }
                if(role.Replace(" ", "") == "Администратор")
                {
                    DeleteBtn.Visibility = Visibility.Visible;
                    NumSup.Visibility = Visibility.Visible;
                    CountInWare.Visibility = Visibility.Visible;
                    CountInWare.IsReadOnly = false;
                    NumSup.IsReadOnly = false;
                    ComboType.Visibility = Visibility.Visible;
                    CountInWare.IsReadOnly = false;
                    NumSup.IsReadOnly = false;
                    ComboType.Visibility = Visibility.Visible;
                    ComboStackPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    DeleteBtn.Visibility = Visibility.Collapsed;
                    NumSup.Visibility = Visibility.Collapsed;
                    CountInWare.Visibility = Visibility.Collapsed;
                    CountInWare.IsReadOnly = true;
                    NumSup.IsReadOnly = true;
                    ComboType.Visibility = Visibility.Collapsed;
                    ComboStackPanel.Visibility = Visibility.Collapsed;
                }

                switch (Initial.NameOfCategoryOfProduct)
                {
                    case "Computers":
                        var type = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CMP") >= 0).Select(x => x);
                        foreach (var t in type)
                        {
                            ComboType.Items.Add(t.NameOfType);
                        }
                        break;

                    case "Notebook":
                        var type1 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_NTB") >= 0).Select(x => x);
                        foreach (var t in type1)
                        {
                            ComboType.Items.Add(t.NameOfType);
                        }
                        break;


                    case "Accessory":
                        var type2 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_ACS") >= 0).Select(x => x);
                        foreach (var t in type2)
                        {
                            ComboType.Items.Add(t.NameOfType);
                        }
                        break;

                    case "Complect":
                        var type3 = db.TypesOfProducts.Where(x => x.NameOfType.IndexOf("_CKT") >= 0).Select(x => x);
                        foreach (var t in type3)
                        {
                            ComboType.Items.Add(t.NameOfType);
                        }
                        break;

                    default:
                        break;

                }

                if (Initial.IDProduct != -1)
                {
                    var pr = db.Products.Where(x => x.ID_Product == Initial.IDProduct);
                    int idtp = db.Types_Products.First(x => x.ID_Product == Initial.IDProduct).ID_Type ?? 0;
                    try
                    {
                        var w = db.Warehouses.First(x => x.ID_Product == Initial.IDProduct);
                        var s = db.Suppliers.First(x => x.ID_Product == Initial.IDProduct);
                        NumSup.Text = s.NameOfSupplier ?? "0";
                        CountInWare.Text = w.CountOfProduct.ToString() ?? "Не найден";
                    }
                    catch(System.InvalidOperationException ex)
                    {
                        NumSup.Text = "0";
                        CountInWare.Text = "Не найден";
                    }
                    foreach (var p in pr)
                {
                    NameOfProduct.Text = p.NameOfProduct;
                        DescriptionOfProduct.Text += p.DescriptionOfProduct; 
                        SpecificOfProduct.Text = p.Specifications;
                        Product.bt = p.AvatarOfProduct;
                        ComboType.SelectedIndex = (ComboType.Items.IndexOf(db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType));
                        TypeProductText.Text = (db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType).Split('_')[0];
                        if (p.Discount != 0)
                    {
                        PriceOfProduct.Text = p.Discount.ToString();
                        AkciaText.Text = p.Price.ToString();
                            if ((role.Replace(" ", "") == "Администратор" || role.Replace(" ", "") == "Модератор"))
                            {
                                AkciaText.Visibility = Visibility.Visible;
                                Akcia.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                AkciaText.Visibility = Visibility.Collapsed;
                                Akcia.Visibility = Visibility.Collapsed;
                            }
                        }
                    else
                    {
                        PriceOfProduct.Text = p.Price.ToString();
                            if ((role.Replace(" ", "") == "Администратор" || role.Replace(" ", "") == "Модератор"))
                            {
                                AkciaText.Visibility = Visibility.Visible;
                                Akcia.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                AkciaText.Visibility = Visibility.Collapsed;
                                Akcia.Visibility = Visibility.Collapsed;
                            }
                        }
                        Avatar av = new Avatar();
                        this.ProductIcon.Fill = new ImageBrush(av.ToImage(p.AvatarOfProduct));
                        break;
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
            this.Close();

        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Initial.IDProduct != -1)
            {
                Model1 db = new Model1();
                Products pr = db.Products.First(x => x.ID_Product == Initial.IDProduct);
                db.Products.Remove(pr);
                MessageBox.Show("Продукт удален!");
                await db.SaveChangesAsync();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ваш товар не создан, чтобы его удалять");
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Model1 db = new Model1();
            string role = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
            if (Initial.IDProduct == -1 && ((role.Replace(" ", "") == "Администратор" || role.Replace(" ", "") == "Модератор")))
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
                        Product.bt = av.getJPGFromImageControl(img);
                        ImageBrush im = new ImageBrush();
                        im.ImageSource = img;
                        ProductIcon.Fill = im;
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка формата файла");
                    }

                }
            }
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
          if (string.IsNullOrEmpty(NumSup.Text) || string.IsNullOrEmpty(CountInWare.Text)) MessageBox.Show("Поставщик и кол-во товара не должны быть пустыми");
            if (string.IsNullOrEmpty(NameOfProduct.Text) ||
    string.IsNullOrEmpty(PriceOfProduct.Text) ||
    string.IsNullOrEmpty(DescriptionOfProduct.Text) ||
    string.IsNullOrEmpty(SpecificOfProduct.Text) || string.IsNullOrEmpty(AkciaText.Text) ||
    Product.bt == null || string.IsNullOrEmpty(ComboType.Text))
 
            {
                MessageBox.Show("Такие поля как: Название, Цена, Описание, Акция, Характеристики, Фото, Тип товара \n" +
                    "Не должны быть пустыми\nПожалуйста заполните их!");
            }
            else
            {
                using(Model1 db = new Model1())
                {
                    try { 
                    if (db.Products.First(x => x.ID_Product == Initial.IDProduct) != null)
                    {
                        try
                        {
                            var prd = db.Products.First(x => x.ID_Product == Initial.IDProduct);
                            var t = db.Types_Products.First(x => x.ID_Product == Initial.IDProduct);
                            var w = db.Warehouses.First(x => x.ID_Product == Initial.IDProduct);
                            var s = db.Suppliers.First(x => x.ID_Product == Initial.IDProduct);
                            prd.NameOfProduct = NameOfProduct.Text;
                            prd.AvatarOfProduct = Product.bt;
                            prd.Price = int.Parse(PriceOfProduct.Text.Replace(" ", ""));
                            int type = db.TypesOfProducts.First(x => x.NameOfType == ComboType.Text).ID_Type;
                            t.ID_Type = type;
                            w.CountOfProduct = int.Parse(CountInWare.Text);
                            s.NameOfSupplier = NumSup.Text;
                            prd.Discount = int.Parse(AkciaText.Text.Replace(" ", ""));
                            prd.Specifications = SpecificOfProduct.Text;
                            prd.DescriptionOfProduct = DescriptionOfProduct.Text;
                            MessageBox.Show("Все сохранилось");
                            this.Close();
                            await db.SaveChangesAsync();
                        }
                        catch
                        {
                            MessageBox.Show("Ввели неверный формат текста!\nПерепроверьте данные");
                        }
                      }
                    }
                    catch
                    {
                            try
                            {

                                Products prd = new Products(NameOfProduct.Text, Product.bt, int.Parse(PriceOfProduct.Text),
                                                                DescriptionOfProduct.Text, 0, SpecificOfProduct.Text);
                                db.Products.Add(prd);
                                int type = db.TypesOfProducts.First(x => x.NameOfType == ComboType.Text).ID_Type;
                                Types_Products tp = new Types_Products(type, prd.ID_Product);
                                Warehouses w = new Warehouses(prd.ID_Product, int.Parse(CountInWare.Text));
                                Suppliers s = new Suppliers(NumSup.Text, prd.ID_Product);
                                db.Warehouses.Add(w);
                                db.Suppliers.Add(s);
                                db.Types_Products.Add(tp);
                                MessageBox.Show("Все сохранено");
                            await db.SaveChangesAsync();
                            this.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Возникла ошибка! Проверьте все ли поля заполнены");
                            }
                    }
                }
            }
        }
    }
}
