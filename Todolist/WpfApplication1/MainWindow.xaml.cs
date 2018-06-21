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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            todolist Item = new todolist();

            // 增加物品
            ToDoStack.Children.Add(Item);
        }

        private void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 拖拉視窗
                this.DragMove();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // 新增一個串列裝每個項目轉成的文字
            List<string> datas = new List<string>();

            // 轉換每一個項目
          foreach(todolist item in ToDoStack.Children)
            {
                string line = "";
                line += "|" + item.date.Text + "|" + item.itemName.Text + "|" + item.moneyCount.Text ;

                datas.Add(line);
            }
            // 存檔
            System.IO.File.WriteAllLines(@"C:\temp\data.txt", datas);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 開檔
            string[] lines = System.IO.File.ReadAllLines(@"C:\temp\data.txt");

            foreach(string line in lines)
            {
                // 用 | 符號拆開
                string[] parts = line.Split('|');
                // 建立 TodoItem
                todolist item = new todolist();

                item.date.Text = parts[1];
                item.itemName.Text = parts[2];
                item.moneyCount.Text = parts[3];
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int sum =0;
            
            foreach(todolist item in ToDoStack.Children)
            {
                sum += int.Parse(item.moneyCount.Text   );
                Total.Text = sum.ToString();
            }
        }
    }
}
