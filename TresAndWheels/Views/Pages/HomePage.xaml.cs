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
using TresAndWheels.Models;

namespace TresAndWheels.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        int i = 0;
        int a = 0;
        public Core db = new Core();
        ///*List<Product> products;*/
        int countPage = 0;
        public int countElement = 10;
        int page = 1;
        public HomePage()
        {
            InitializeComponent();
            Core db = new Core();
            UpdateUI();
            BoxListView.ItemsSource = db.context.Product.ToList();
            SortirovButton.Content = "↑";
            FiltrButton.Content = "↑";
        }


        private void SearchTextBoxGetFocus(object sender, RoutedEventArgs e)
        {
            SortirovButton.Content = "↓";
            a++;
            if (a % 2 == 0)
            {
                SortirovButton.Content = "↑";
            }
        }

        private void FiltrTextBoxGetFocus(object sender, RoutedEventArgs e)
        {
            FiltrButton.Content = "↓";
            i++;
            if (i % 2 == 0)
            {
                FiltrButton.Content = "↑";
            }
        }
        
            
            public class PageItem
            {
                public int Num { get; set; }
                public string Selected { get; set; }
                public string TextColor { get; set; }
                public string FondWeight { get; set; }

                public PageItem()
                {
                    Num = 0;
                    Selected = "None";
                    TextColor = "Black";
                    FondWeight = "Normal";
                }

                public PageItem(int num, bool selected)
                {
                    Num = num;
                    Selected = (selected ? "Underline" : "None");
                    TextColor = (selected ? "Blue" : "Black");
                    FondWeight = (selected ? "Bold" : "Normal");
                }
            }
            private int GetPagesCount()
            {

                int count = GetRows().Count;
                if (count > countElement) countPage = Convert.ToInt32(Math.Ceiling(count * 1.0 / countElement));
                return countPage;
            }
            public void DisplayPagination(int page = 1)
            {
                List<PageItem> source = new List<PageItem>();
                for (int i = 1; i <= GetPagesCount(); i++) source.Add(new PageItem(i, i == page));
                foreach (var item in source)
                {
                    Console.WriteLine(item.Num);
                }
                PaginationListView.ItemsSource = source;
                PrevTextBlock.Visibility = (page <= 1 ? Visibility.Hidden : Visibility.Visible);
                NextTextBlock.Visibility = (page >= GetPagesCount() ? Visibility.Hidden : Visibility.Visible);
            }
            private List<Product> GetRows()
            {
                List<Product> arrayProduct = db.context.Product.ToList();
                return arrayProduct;
            }
            private void UpdateUI()
            {
                if (GetRows().Count > 10)
                {
                    DisplayPagination(page);
                    List<Product> displayProduct = GetRows().Skip((page - 1) * countElement).Take(countElement).ToList();
                    foreach (var item in displayProduct)
                    {
                        Console.WriteLine(item.ID);
                    }
                }
                else
                {
                    PaginationListView.Visibility = Visibility.Collapsed;
                }

            }

            private void NextTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (page >= GetPagesCount())
                {
                    page = GetPagesCount();
                    NextTextBlock.Visibility = Visibility.Hidden;
                    UpdateUI();
                }
                else
                {
                    page++;
                    NextTextBlock.Visibility = Visibility.Visible;
                    UpdateUI();
                }
            }

            private void PrevTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (page <= 1)
                {
                    page = 1;
                    UpdateUI();
                    PrevTextBlock.Visibility = Visibility.Hidden;
                }
                else
                {
                    page--;
                    UpdateUI();
                    PrevTextBlock.Visibility = Visibility.Visible;
                }
            }

            private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                TextBlock activepage = sender as TextBlock;
                page = Convert.ToInt32(activepage.Text);
                UpdateUI();
            }
    }
}

