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
using System.Collections.ObjectModel;
using System.Diagnostics;
using SQLiteManager.DAL.Implementation;
using SQLiteManager;
using SQLiteManager.DAL.Model;
using NTS_Test.Pages;

namespace NTS_Test
{
	public partial class MainWindow : Window
	{
        private DataGridPage _dataGridPage;
        private SearchPage   _searchPage;
        private AboutPage    _aboutPage;
        private Page _currentPage = null;

        private Button _currentButton = null;

		public MainWindow()
		{
			InitializeComponent();
            LoadContent();

            SwitchSelectedButton(dataGridPageSwitchButton);
            SwitchFrame(_dataGridPage);
            MainView.Content = _currentPage;

			var dal = new ProductDAL();
            var data = dal.GetByLimitAsync(100);

			var products = new ObservableCollection<Product>();
			foreach (var item in data.Result)
			{
                products.Add(item);
			}

			_dataGridPage.productsDataGrid.ItemsSource = products;
		}

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SwitchFrame_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == _currentButton || button == null)
                return;

            SwitchSelectedButton(button);

            switch (button.Name)
			{
                case "dataGridPageSwitchButton":
                    SwitchFrame(_dataGridPage);
                    break;
                case "searchPageSwitchButton":
                    SwitchFrame(_searchPage);
                    break;
                case "aboutPageSwitchButton":
                    SwitchFrame(_aboutPage);
                    break;
                default:
					break;
			}
		}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
        }

        private void SwitchFrame(Page page)
        {
            if (_currentPage == page)
                return;

            _currentPage = page;
            MainView.Content = _currentPage;
		}

        private void LoadContent()
        {
            _dataGridPage = new DataGridPage();
            _searchPage = new SearchPage();
            _aboutPage = new AboutPage();
		}

        private void SwitchSelectedButton(Button b)
        {
            if (b == null)
                return;

            if (_currentButton != null)
            {
                _currentButton.Background = Brushes.Transparent;
                _currentButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xD0, 0xC0, 0xFF)); // #D0C0FF
            }

            _currentButton = b;

            _currentButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x7B, 0x5C, 0xD6)); // #7B5CD6
            _currentButton.Foreground = Brushes.White;
        }
	}
}
