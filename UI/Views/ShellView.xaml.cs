using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using UI.ViewModels;

namespace UI.Views
{
	public partial class ShellView
	{
		public ShellView()
		{
			InitializeComponent();
		}

		private bool ValidEmail(string email)
		{
			var trimmedEmail = email.Trim();

			if (trimmedEmail.EndsWith("."))
			{
				return false;
			}
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == trimmedEmail;
			}
			catch
			{
				return false;
			}
		}

		private bool ValidateSignUp()
		{
			bool res = true;
			if(UsernameTextBox.Text == "")
			{
				UserNameError.Visibility = Visibility.Visible;
			}
			else
			{
				UserNameError.Visibility = Visibility.Hidden;
			}
			if (NewUserPassBox.Password.Length == 0)
			{
				ConfrimPassError.Visibility = Visibility.Hidden;
				PassError.Visibility = Visibility.Visible;
			}
			else if(NewUserConfirmPassBox.Password != NewUserPassBox.Password)
			{
				PassError.Visibility = Visibility.Hidden;
				ConfrimPassError.Visibility = Visibility.Visible;
			}
			if(!ValidEmail(NewUserEmail.Text))
			{
				EmailError.Visibility = Visibility.Visible;
			}
			else
			{
				EmailError.Visibility = Visibility.Hidden;
			}
			return res;
		}

		private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
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
			if (ValidateSignUp())
			{
				//ShopView ShopViewWindow = new ShopView();
				//ShopViewWindow.Show();
			}
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
			{
				//ShopView ShopViewWindow = new ShopView();
				//ShopViewWindow.Show();
			}
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
