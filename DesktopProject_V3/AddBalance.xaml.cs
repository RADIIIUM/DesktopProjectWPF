using DesktopProject_V3.DataBaseClass;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddBalance.xaml
    /// </summary>
    public partial class AddBalance : Window
    {
        public AddBalance()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Balance.Text) || (int.Parse(Balance.Text) > 2000000000))
            {
                MessageBox.Show("Нельзя добавить пустоту\nИли превзойти лимит");
            }
            else
            {
                using(Model1 db = new Model1())
                {
                    foreach(var user in db.Users)
                    {
                        if(user.LoginOfUser == Initial.login)
                        {
                            user.Balance = int.Parse(Balance.Text);
                            MessageBox.Show("Деньги зачислены");
                            break;
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
