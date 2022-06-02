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
using System.Windows.Shapes;

namespace UI.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		public ShellView()
		{
			InitializeComponent();
		} 

		private void SignUpBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void SignUpOptBtn_Click(object sender, RoutedEventArgs e)
		{
			WelcomeGrid.Visibility = Visibility.Collapsed;
			SignUpGrid.Visibility = Visibility.Visible;
			UsernameTextBox.Focus();
		}

		private void SignUpBack_Click(object sender, RoutedEventArgs e)
		{
			SignUpGrid.Visibility = Visibility.Collapsed;
			WelcomeGrid.Visibility = Visibility.Visible;
		}

		private void LogInOptBtn_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Loggen in successfuly!");
		}

		private void QuitBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
