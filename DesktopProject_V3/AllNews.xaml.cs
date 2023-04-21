using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections;
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
    /// Логика взаимодействия для AllNews.xaml
    /// </summary>
    /// 
    public class ItemsInNews
    {
        public ItemsInNews(int ID, string Paragraph, string Desc, ImageBrush Img, bool fix)
        {
            this.id = ID;
            this.paragraph = Paragraph;
            this.desc = Desc;
            this.img = Img;
            this.Fix = fix;

        }

        public int id { get; set; }
        public string paragraph { get; set; }
        public string desc { get; set; }
        public ImageBrush img { get; set;}

        public bool Fix { get; set; }
    }
    public partial class AllNews : Window
    {
        public ObservableCollection<ItemsInNews> NWS { get; set; }
        public AllNews()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                NWS = new ObservableCollection<ItemsInNews>();
                Avatar ava = new Avatar();
                foreach (var news in db.News)
                {
                        ItemsInNews it = new ItemsInNews(news.ID_New, news.Paragraph, news.DescriptionOfNews, new ImageBrush(ava.ToImage(news.MainImage)), Convert.ToBoolean(news.Fix));
                        NWS.Add(it);
                }
                newsList.ItemsSource = NWS;

            }

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void newsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ItemsInNews i = (ItemsInNews)newsList.SelectedItem;
                if(i.id != null)
                {
                    Initial.NumberOfNews = i.id;
                    WndowOfNew wd = new WndowOfNew();
                    wd.ShowDialog();
                }
                
            }
            catch
            {

            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(Search.Text))
            {
                var list = from t in NWS
                           where t.Fix != true
                           select t;
                newsList.ItemsSource = list;
            }
            else
            {
                var list = from t in NWS
                           where t.paragraph.ToLower().IndexOf(Search.Text.ToLower()) >= 0
                           select t;
                newsList.ItemsSource = list;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var list = from t in NWS
                       where ((t.paragraph.ToLower().IndexOf(Search.Text.ToLower()) >= 0) || string.IsNullOrEmpty(Search.Text)) && t.Fix == true
                       select t;
            newsList.ItemsSource = list;
        }

        private void FixCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            var list = from t in NWS
                       where t.paragraph.ToLower().IndexOf(Search.Text.ToLower()) >= 0
                       select t;
            newsList.ItemsSource = list;
        }
    }

}
