using DesktopProject_V3.DataBaseClass;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public static byte[] bt { get; set; }
        public Product()
        {
            InitializeComponent();
            using(Model1 db = new Model1())
            {
                string role = db.Roles.First(x => x.LoginOfUsers == Initial.login).NameOfRole;
                if ((role.Replace(" ", "") == "Администратор" || role.Replace(" ", "") == "Модератор"))
                {
                    DeleteBtn.Visibility = Visibility.Visible;
                    SaveBtn.Visibility = Visibility.Visible;
                    ComboStackPanel.Visibility = Visibility.Visible;
                    NameOfProduct.IsReadOnly = false;
                    DescriptionOfProduct.IsReadOnly = false;
                    SpecificOfProduct.IsReadOnly = false;
                    PriceOfProduct.IsReadOnly = false;
                    AkciaText.IsReadOnly = false;
                    CountInWare.IsReadOnly = false;
                    NumSup.IsReadOnly = false;
                    
                    
                }
                else
                {
                    DeleteBtn.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Collapsed;
                    ComboStackPanel.Visibility = Visibility.Collapsed;
                    NameOfProduct.IsReadOnly = true;
                    DescriptionOfProduct.IsReadOnly = true;
                    SpecificOfProduct.IsReadOnly = true;
                    PriceOfProduct.IsReadOnly = true;
                    AkciaText.IsReadOnly = true;
                    CountInWare.IsReadOnly = true;
                    NumSup.IsReadOnly = true;

                }
                    if (Initial.IDProduct != -1)
                {

                var pr = db.Products.Where(x => x.ID_Product == Initial.IDProduct);
                    int idtp = db.Types_Products.First(x => x.ID_Product == Initial.IDProduct).ID_Product ?? 0;
                    try
                    {
                        var t = db.Types_Products.First(x => x.ID_Product == Initial.IDProduct);
                        var w = db.Warehouses.First(x => x.ID_Product == Initial.IDProduct);
                        var s = db.Suppliers.First(x => x.ID_Product == Initial.IDProduct);
                        NumSup.Text = s.NameOfSupplier;
                        CountInWare.Text = w.CountOfProduct.ToString();
                    }
                    catch(System.InvalidOperationException ex)
                    {
                        NumSup.Visibility = Visibility.Collapsed;
                        CountInWare.Visibility = Visibility.Collapsed;

                        
                    }
                    foreach (var p in pr)
                {
                    NameOfProduct.Text = p.NameOfProduct;
                        DescriptionOfProduct.Text += p.DescriptionOfProduct; 
                        SpecificOfProduct.Text = p.Specifications;
                        Product.bt = p.AvatarOfProduct;
                        ComboType.SelectedIndex = ComboType.Items.IndexOf(db.TypesOfProducts.First(x=>x.ID_Type == idtp).NameOfType);
                        TypeProductText.Text = db.TypesOfProducts.First(x => x.ID_Type == idtp).NameOfType;
                        if (p.Discount != 0 || p.Discount != null)
                    {
                        PriceOfProduct.Text = p.Discount.ToString();
                        AkciaText.Text = p.Price.ToString();
                    }
                    else
                    {
                        PriceOfProduct.Text = p.Price.ToString();
                        AkciaText.Visibility = Visibility.Collapsed;
                        Akcia.Visibility = Visibility.Collapsed;
                    }
                        Avatar av = new Avatar();
                        this.ProductIcon.Fill = new ImageBrush(av.ToImage(p.AvatarOfProduct));
                        break;
                    }

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


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Initial.IDProduct != -1)
            {
                Model1 db = new Model1();
                Products pr = db.Products.First(x => x.ID_Product == Initial.IDProduct);
                db.Products.Remove(pr);
                MessageBox.Show("Продукт удален!");
                db.SaveChanges();
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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
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
                            prd.Price = int.Parse(PriceOfProduct.Text);
                            int type = db.TypesOfProducts.First(x => x.NameOfType == ComboType.Text).ID_Type;
                            t.ID_Type = type;
                            w.CountOfProduct = int.Parse(CountInWare.Text);
                            s.NameOfSupplier = NumSup.Text;
                            prd.Discount = int.Parse(Akcia.Text);
                            prd.Specifications = SpecificOfProduct.Text;
                            prd.DescriptionOfProduct = DescriptionOfProduct.Text;
                            MessageBox.Show("Все сохранилось");
                            this.Close();
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Ввели неверный формат текста!\nПерепроверьте данные");
                        }
                    }
                    else
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
                            
                            db.SaveChanges();
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
