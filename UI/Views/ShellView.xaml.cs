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

		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			SignUpOptBtn.Focus();
		}

		private void SignUpOptBtn_Click(object sender, RoutedEventArgs e)
		{
			WelcomeGrid.Visibility = Visibility.Hidden;
			SignUpGrid.Visibility = Visibility.Visible;
			UsernameTextBox.Focus();
		}

		private void LogInOptBtn_Click(object sender, RoutedEventArgs e)
		{
			WelcomeGrid.Visibility = Visibility.Hidden;
			LogInGrid.Visibility = Visibility.Visible;
			LogInTextBox.Focus();
		}

		private void QuitBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void TempBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!AgreeMentCheckBox.IsFocused)
			{
				if (SignUpBtn.IsEnabled)
					SignUpBtn_Click(sender, e);
			}
			else
			{
				AgreeMentCheckBox.IsChecked = (bool)(!AgreeMentCheckBox.IsChecked);
				AgreeMentCheckBox_Click(sender, e);
			}
		}

		private void AgreeMentCheckBox_Click(object sender, RoutedEventArgs e)
		{
			SignUpBtn.IsEnabled = (bool)(AgreeMentCheckBox.IsChecked);
		}

		private void SignUpBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void SignUpBack_Click(object sender, RoutedEventArgs e)
		{
			SignUpGrid.Visibility = Visibility.Hidden;
			WelcomeGrid.Visibility = Visibility.Visible;
			SignUpOptBtn.Focus();
		}

		private void LogInBtn_Click(object sender, RoutedEventArgs e)
		{
			if (KeepSignCheckBox.IsFocused == false)
				Close();
			else if (KeepSignCheckBox.IsChecked == true)
				KeepSignCheckBox.IsChecked = false;
			else
				KeepSignCheckBox.IsChecked = true;
		}

		private void LogInBack_Click(object sender, RoutedEventArgs e)
		{
			LogInGrid.Visibility = Visibility.Hidden;
			WelcomeGrid.Visibility = Visibility.Visible;
			SignUpOptBtn.Focus();
		}
	}
}
