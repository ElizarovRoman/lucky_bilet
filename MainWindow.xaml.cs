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
using System.Windows.Navigation;
using System.Windows.Shapes;
using evaluate = lucky_bilet.Class_Parsing.Class_Evaluating;
using parse = lucky_bilet.Class_Parsing;

namespace lucky_bilet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_CheckVariant_Click(object sender, RoutedEventArgs e)
        {
            /// prepare variables (объявляем необходимые переменные)
            short[] arr = {-1, -1, -1, -1, -1, -1 };
            short def = -1;

            /// extract up to 6 symbols from TextBox to arr
            /// (извлекаем не более 6 символов из компонента TextBox в переменную arr)
            string str_arr = this.textBox_Input.Text;
            long lg_count = str_arr.Count();
            for (long lg_i = 0; lg_i < lg_count; lg_i++)
            {
                if (lg_i == 6)
                    break;
                string str = str_arr.ElementAt((int)lg_i).ToString();
                arr[lg_i] = (short)parse.StrToShortDef(str, def);
            }
            /// reveal radiobutton index
            /// (устанавливаем индекс, соответствующий радиокнопке)
            short sh_checked = evaluate.DEF_VARIANT_DEFAULT;
            if (this.radioButton_VariantTask.IsChecked == true)
                sh_checked = evaluate.DEF_VARIANT_TASK;
            /// evaluate all 6 digits
            /// (проверяем на соответствие все 6 цифр)
            short sh_answer = evaluate.IsHappyLucky(arr, sh_checked);
            /// generate and show string answer
            /// /// (получаем ответ в виде строки и выводим его пользователю)
            string str_answer = evaluate.GetMessageByLuckyValue(sh_answer);
            MessageBox.Show(str_answer);

        }

        private void button_FindAllLucky_Click(object sender, RoutedEventArgs e)
        {
            /// reveal radiobutton index
            /// (устанавливаем индекс, соответствующий радиокнопке)
            short sh_checked = evaluate.DEF_VARIANT_DEFAULT;
            if (this.radioButton_VariantTask.IsChecked == true)
                sh_checked = evaluate.DEF_VARIANT_TASK;
            /// prepare list to be save sampling
            /// (подготавливаем список, в который будет сохраняться выборка данных)
            List<string> lst_lucky = new List<string>();
            lst_lucky.Clear();
            /// prepare array for 6 digits
            /// (подготавливаем массив для 6 цифр)
            short[] sh_arr = { -1, -1, -1, -1, -1, -1 };
            /// enumerate all accessible ticket values
            /// (создаем цикл, перебирающий все допустимые значения билета)
            long lg_count_i = 0;
            for (long lg_i = 0; lg_i <= 999999; lg_i += 111111)
            {
                parse.ConvertLongToArray(lg_i, ref sh_arr);
                short sh_answer = evaluate.IsHappyLucky(sh_arr, sh_checked);
                if (sh_answer == evaluate.DEF_LUCKY_TICKET)
                {
                    string str = "[" + lg_count_i.ToString() + "] " + parse.ConvertLongToString(lg_i, 6);
                    lst_lucky.Add(str);
                    lg_count_i++;
                }
            }
            /// update ListBox content
            /// (обновляем содержимое компонента listbox)
            listBox_Enumerate.Items.Clear();
            listBox_Enumerate.Items.Add(lst_lucky.ToArray());
        }
    }
}
