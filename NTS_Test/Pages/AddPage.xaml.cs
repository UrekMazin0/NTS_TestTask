using SQLiteManager.DAL.Implementation;
using SQLiteManager.DAL.Model;
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

namespace NTS_Test.Pages
{
	public partial class AddPage : Page
	{
		public AddPage()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			// Очень плохой код, но нет времени делать иначе

			if (nameBox.Text == "" || 
				codeBox.Text == "" || 
				barcodeBox.Text == "" || 
				quantityBox.Text == "" || 
				priceBox.Text == "")
			{
				MessageBox.Show("Ошибка, поля name, code, barcode, quantity, price должны быть заполнены. \n Перепроверьте эти поля.");
				return;
			}

			var price = new Price { price = Decimal.Parse(priceBox.Text) };
			var dal_price = new PriceDAL();
			var price_id = dal_price.GetIdIfExistAsync(price);

			if (price_id.Result == -1)
				dal_price.AddAsync(price);

			price_id = dal_price.GetIdIfExistAsync(price);


			var product = new Product {
				name = nameBox.Text,
				code = Int32.Parse(codeBox.Text),
				bar_code = barcodeBox.Text,
				quantity = Decimal.Parse(quantityBox.Text),
				model = modelBox.Text == "" ? null : modelBox.Text,
				sort = sortBox.Text == "" ? null : sortBox.Text,
				color = colorBox.Text == "" ? null : colorBox.Text,
				size = sizeBox.Text == "" ? null : sizeBox.Text,
				weight = weightBox.Text == "" ? -1 : Decimal.Parse(weightBox.Text),
				id_price = price_id.Result
			};

			var dal_product = new ProductDAL();

			dal_product.AddAsync(product);
		}
	}
}
