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
        private SearchPage   _seatchPage;
        private AboutPage    _aboutPage;

		public MainWindow()
		{
			InitializeComponent();
            LoadContent();
            
            MainView.Content = _dataGridPage;

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

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
        }

        private void LoadContent()
        {
            _dataGridPage = new DataGridPage();
            _seatchPage = new SearchPage();
            _aboutPage = new AboutPage();
		}
	}
}
