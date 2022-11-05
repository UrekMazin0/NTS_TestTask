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

using NTS_Test.DataManager;

namespace NTS_Test.Pages
{
	public partial class DataGridPage : Page
	{
		public event EventHandler<FilterUpdateEventArgs> filterUpdate;

		public DataGridPage()
		{
			InitializeComponent();
		}

		private void nameFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Return)
				return;

			var textBox = sender as TextBox;
			if (sender == null)
				return;

			FilterUpdateEventArgs args = new FilterUpdateEventArgs
			{
				name = textBox.Text,
				code = -1,
				bar_code = "",
				price = -1
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
