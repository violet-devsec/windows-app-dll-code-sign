using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeSignApp.MathLib;
using CodeSignApp.Security;

namespace CodeSignApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMathService _mathHandler;

        public MainWindow()
        {
            InitializeComponent();

            if (!CodeSecurity.VerifiyAssemblies())
            {
                if (MessageBox.Show("Security exception occured! Cannot initiate the app.", "Confirmation", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                _mathHandler = RequestFactory.GetMathServiceHandler();
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            int firstNum = Convert.ToInt32(text_first_num.Text);
            int secNum = Convert.ToInt32(text_second_num.Text);

            int ans = _mathHandler.Add(firstNum, secNum);

            text_answ.Text = Convert.ToString(ans);
        }

        private void button_sub_Click(object sender, RoutedEventArgs e)
        {
            int firstNum = Convert.ToInt32(text_first_num.Text);
            int secNum = Convert.ToInt32(text_second_num.Text);

            int ans = _mathHandler.Substract(firstNum, secNum);

            text_answ.Text = Convert.ToString(ans);

        }

        private void button_mul_Click(object sender, RoutedEventArgs e)
        {
            int firstNum = Convert.ToInt32(text_first_num.Text);
            int secNum = Convert.ToInt32(text_second_num.Text);

            float ans = _mathHandler.Multiply(firstNum, secNum);

            text_answ.Text = Convert.ToString(ans);
        }

        private void button_divd_Click(object sender, RoutedEventArgs e)
        {
            int firstNum = Convert.ToInt32(text_first_num.Text);
            int secNum = Convert.ToInt32(text_second_num.Text);

            double ans = _mathHandler.Divide(firstNum, secNum);

            //var ans_str = DoFormat(ans);

            text_answ.Text = ans.ToString();
        }

        private string DoFormat(float myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }
    }
}
