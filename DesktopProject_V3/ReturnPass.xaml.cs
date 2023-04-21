using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
    /// Логика взаимодействия для ReturnPass.xaml
    /// </summary>
    public partial class ReturnPass : Window
    {
        public ReturnPass()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void MinWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Autor_Click(object sender, RoutedEventArgs e)
        {
            Autorization mw = new Autorization();
            mw.Show();
            this.Close();
        }

        public class NotEmptyValidationRule : ValidationRule
        {
            public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                return string.IsNullOrWhiteSpace((value ?? "").ToString())
                    ? new System.Windows.Controls.ValidationResult(false, "Поле не должно быть пустым")
                    : System.Windows.Controls.ValidationResult.ValidResult;
            }
        }

        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
           if(string.IsNullOrEmpty(EmailBox.Text))
            {
                MessageBox.Show(
    "Поле для ввода эл. почты \nне должно быть пустым!",
    "Внимание!");
            }
        }

    }
}
