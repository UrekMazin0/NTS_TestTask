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
	/// <summary>
	/// Interaction logic for DataGridPage.xaml
	/// </summary>
	public partial class DataGridPage : Page
	{
		
		public DataGridPage()
		{
			InitializeComponent();
		}

		private void nameFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Return)
				return;

			var textBox = sender as TextBox;
			if (String.IsNullOrEmpty(textBox.Text))
				return;


		}
	}
}
