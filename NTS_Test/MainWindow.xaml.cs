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
using SQLiteManager.DAL.Model;
using NTS_Test.Pages;
using NTS_Test.DataManager;

namespace NTS_Test
{
	public partial class MainWindow : Window
	{
        private DataGridPage _dataGridPage;
        private SearchPage   _searchPage;
        private AboutPage    _aboutPage;
        private AddPage      _addPage;

        private Page _currentPage = null;
        private Button _currentButton = null;

        private DataAccessManager _dataManager;

		public MainWindow()
		{
			InitializeComponent();
            LoadContent();

            _dataManager = new DataAccessManager();

            EventSlotsConnections();

            SwitchSelectedButton(dataGridPageSwitchButton);
            SwitchFrame(_dataGridPage);
            MainView.Content = _currentPage;

            _dataManager.GetByLimit(100);
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
                case "addPageSwitchButton":
                    SwitchFrame(_addPage);
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
            _searchPage   = new SearchPage();
            _aboutPage    = new AboutPage();
            _addPage      = new AddPage();
		}

        private void EventSlotsConnections()
        {
            _dataManager.dataUpdate += _dataGridPage.DataUpdateEventHandler;
            _dataManager.dataUpdate += _searchPage.DataUpdateEventHandler;

            _dataGridPage.filterUpdate += _dataManager.FilterUpdateEventHandler;
            _searchPage.filterUpdate += _dataManager.FilterUpdateEventHandler;
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
    }
}
