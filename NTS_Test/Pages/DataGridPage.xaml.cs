using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using SQLiteManager.DAL.Model;

namespace NTS_Test.Pages
{
	public partial class DataGridPage : Page
	{
		public event EventHandler<FilterUpdateEventArgs> filterUpdate;
		public event EventHandler<EntityUpdateEventArgs> entityUpdate;

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
			productsTitle.Text = $"{e.products.Count} товаров";
			productsDataGrid.ItemsSource = e.products;
		}

		private void productsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if (e.EditAction != DataGridEditAction.Commit)
				return;

			var column = e.Column as DataGridBoundColumn;
			
			var bindingPath = (column.Binding as Binding).Path.Path;
			int rowIndex = e.Row.GetIndex();
			var element = e.EditingElement as TextBox;

			Product p = (Product)productsDataGrid.Items[rowIndex];

			EntityUpdateEventArgs args = new EntityUpdateEventArgs
			{
				index = rowIndex,
				fieldName = bindingPath,
				value = element.Text,
				product = p
			};
			EventHandler<EntityUpdateEventArgs> handler = entityUpdate;

			if (handler != null)
				handler(this, args);
		}

		private DataGridRow GetRow(int index)
		{
			DataGridRow row = (DataGridRow)productsDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
			if (row == null)
			{
				productsDataGrid.UpdateLayout();
				productsDataGrid.ScrollIntoView(productsDataGrid.Items[index]);
				row = (DataGridRow)productsDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
			}
			return row;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
