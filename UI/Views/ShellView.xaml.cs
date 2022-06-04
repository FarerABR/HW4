using System.Windows;
using BLL.Repository;

namespace UI.Views
{
	public partial class ShellView : Window
	{
		public ShellView()
		{
			InitializeComponent();
			UserRepository.CreateUser("Amir", "XD", "a@Gmail.com").Role = DAL.Enum.User.UserRole.admin;
			//if(KeepSignedIn)
			//{
			//	WelcomePage.Hide();
			//	ShopPage.Active();
			//}
		}

		bool KeepSignedIn = false;

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
			if (UsernameTextBox.Text == "")
			{
				UserNameError.Visibility = Visibility.Visible;
				res = false;
			}
			else
			{
				UserNameError.Visibility = Visibility.Hidden;
			}
			if (NewUserPassBox.Password.Length == 0)
			{
				ConfrimPassError.Visibility = Visibility.Hidden;
				PassError.Visibility = Visibility.Visible;
				res = false;
			}
			else if (NewUserConfirmPassBox.Password != NewUserPassBox.Password)
			{
				PassError.Visibility = Visibility.Hidden;
				ConfrimPassError.Visibility = Visibility.Visible;
				res = false;
			}
			if (!ValidEmail(NewUserEmail.Text))
			{
				EmailError.Visibility = Visibility.Visible;
				res = false;
			}
			else
			{
				EmailError.Visibility = Visibility.Hidden;
			}
			return res;
		}

		private bool ValidateLogIn()
		{
			//if(!UserRepository.UserExists(LogInTextBox.Text))
			//{
			//	LogInError.Visibility = Visibility.Hidden;
			//	LogInWrongUserNameError.Visibility = Visibility.Visible;
			//	return false;
			//}
			//else if (UserRepository.SearchUser(LogInTextBox.Text)[0].Password != LogInPassBox.Password)
			//{
			//	LogInWrongUserNameError.Visibility = Visibility.Hidden;
			//	LogInError.Visibility = Visibility.Visible;
			//	return false;
			//}
			return true;
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
				//if (ValidateLogIn())
				//{
				//	//ShopView ShopViewWindow = new ShopView();
				//	//ShopViewWindow.Show();
				//}
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
