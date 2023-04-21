using DesktopProject_V3.DataBaseClass;
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

namespace DesktopProject_V3
{
    /// <summary>
    /// Логика взаимодействия для WndowOfNew.xaml
    /// </summary>
    public partial class WndowOfNew : Window
    {
        public bool redact { get; set; }
        public static byte[] bytes { get; set; }
        public WndowOfNew()
        {
            InitializeComponent();
            using(Model1 db = new Model1())
            {
                foreach(var role in db.Roles)
                {
                    if(role.LoginOfUsers.Replace(" ", "") == Initial.login.Replace(" ", "") && (role.NameOfRole.Replace(" ", "") == "Администратор" || role.NameOfRole.Replace(" ", "") == "Модератор"))
                    {
                        this.Paragraph.IsReadOnly = false;
                        this.RedactDesc.IsReadOnly = false;
                        this.RedactDesc.Height = 200;
                        this.RedactDesc.Visibility = Visibility.Visible;
                        this.DescText.Visibility = Visibility.Collapsed;
                        this.DescText.Text = "";
                        this.Save.Visibility = Visibility.Visible;
                        this.Fix.Visibility = Visibility.Visible;
                        foreach (var n in db.News)
                        {
                            if(n.ID_New == Initial.NumberOfNews)
                            {
                                this.RedactDesc.Text = n.DescriptionOfNews;
                                this.Paragraph.Text = n.Paragraph;
                                Avatar av = new Avatar();
                                this.Image.Fill = new ImageBrush(av.ToImage(n.MainImage));
                            }
                        }
                        this.redact = true;
                    }
                    else
                    {
                        this.Paragraph.IsReadOnly = true;
                        this.RedactDesc.IsReadOnly = true;
                        this.RedactDesc.Height = 0;
                        this.RedactDesc.Visibility = Visibility.Collapsed;
                        this.DescText.Visibility = Visibility.Visible;
                        this.Save.Visibility = Visibility.Collapsed;
                        this.Fix.Visibility = Visibility.Collapsed;
                        foreach (var n in db.News)
                        {
                            if (n.ID_New == Initial.NumberOfNews)
                            {
                                this.DescText.Text = n.DescriptionOfNews;
                                this.Paragraph.Text = n.Paragraph;
                                Avatar av = new Avatar();
                                this.Image.Fill = new ImageBrush(av.ToImage(n.MainImage));
                            }
                        }
                        this.redact = false;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                foreach (var n in db.News)
                {
                    if (Initial.NumberOfNews == n.ID_New)
                    {
                        n.Paragraph = this.Paragraph.Text;
                        n.DescriptionOfNews = this.RedactDesc.Text;
                        if (WndowOfNew.bytes != null)
                        {
                            n.MainImage = WndowOfNew.bytes;
                            MessageBox.Show("Новость сохранена ");
                        }
                        break;
                    }
                }
                    if(Initial.NumberOfNews == -1)
                    {
                        if(string.IsNullOrEmpty(this.RedactDesc.Text) || string.IsNullOrEmpty(this.Paragraph.Text) || WndowOfNew.bytes == null)
                        {
                            MessageBox.Show("У нвоости должно быть фото, а также поля не должны быть пустыми");
                        }
                        else
                        {
                            News ne = new News(this.Paragraph.Text, this.RedactDesc.Text, WndowOfNew.bytes, false);
                            db.News.Add(ne);
                            MessageBox.Show("Новость добавлена");
                        }
                    }
                db.SaveChanges();
            }
            
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (redact)
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
                        WndowOfNew.bytes = av.getJPGFromImageControl(img);
                        ImageBrush im = new ImageBrush();
                        im.ImageSource = img;
                        Image.Fill = im;
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка формата файла");
                    }

                }
            }
        }

        private void Fix_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                foreach(var v in db.News)
                {
                    int count = db.News.Where(x => x.Fix == true).Select(x => x.Fix).Count();
                    if(v.ID_New == Initial.NumberOfNews && count < 3)
                    {
                        if (v.Fix == false || v.Fix == null)
                        {
                            v.Fix = true;
                            MessageBox.Show("Новость закреплена");
                            break;
                        }
                        if (v.Fix == true)
                        {
                            v.Fix = false;
                            MessageBox.Show("Новость удалена из закрепленных");
                            break;
                        }
                    }
                    if(Initial.NumberOfNews == -1)
                    {
                        MessageBox.Show("Для начала нужно создать новость \n Нажмите 'Сохранить' ");
                        break;
                    }
                    if(count >= 3)
                    {
                        MessageBox.Show("Сликом много закрепленных новостей");
                        break;
                    }
                }
                db.SaveChanges();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1())
            {
                foreach (var v in db.News)
                {
                    if (v.ID_New == Initial.NumberOfNews)
                    {
                        db.News.Remove(v);
                        MessageBox.Show("Новость удалена");
                        break;
                    }
                    if (Initial.NumberOfNews == -1)
                    {
                        MessageBox.Show("Нельзя удалить не созданную новость ");
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
