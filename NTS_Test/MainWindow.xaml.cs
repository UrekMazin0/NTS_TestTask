﻿using System;
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

namespace NTS_Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			try
			{
				var dal = new ProductDAL();
				var p = dal.GetByIdAsync(5);
				Trace.WriteLine("PRODUCT : " + p.Result);
			}
			catch (Exception ex)			
			{
				Trace.WriteLine(ex.Message);
			}
			InitializeComponent();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
