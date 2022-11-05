using NTS_Test.DataManager;
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

namespace NTS_Test.Pages
{
	public partial class SearchPage : Page
	{
		public event EventHandler<FilterUpdateEventArgs> filterUpdate;

		public SearchPage()
		{
			InitializeComponent();
		}

		private void Filters_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Return)
				return;

			FilterUpdateEventArgs args = new FilterUpdateEventArgs
			{
				name = nameFilter.Text,
				code = codeFilter.Text == "" ? -1 : Int32.Parse(codeFilter.Text),
				bar_code = barcodeFilter.Text,
				price = priceFilter.Text == "" ? -1 : Int32.Parse(priceFilter.Text)
			};

			EventHandler<FilterUpdateEventArgs> handler = filterUpdate;
			if (handler != null)
				handler(this, args);

		}

		public void DataUpdateEventHandler(object sender, DataUpdateEventArgs e)
		{
			productsDataGrid.ItemsSource = e.products;
		}
	}
}
