using System.Windows;
using BLL.Repository;
using DAL.Enum.User;
using DAL.Entity.User;

namespace UI.Views
{
	public partial class ShellView
	{
		User? CurrentUser;
		private bool KeepSignedIn = false;

		public ShellView()
		{
			InitializeComponent();
		}

		private static bool ValidEmail(string email)
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
				UserNameError.Text = "* Field is required";
				UserNameError.Visibility = Visibility.Visible;
				res = false;
			}
			else if(UsernameTextBox.Text.Length < 5)
			{
				UserNameError.Text = "* at least 5 characters";
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
			else
			{
				PassError.Visibility = Visibility.Hidden;
				ConfrimPassError.Visibility = Visibility.Hidden;
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
			if (LogInTextBox.Text.Length == 0 || (!UserRepository.UsernameExists(LogInTextBox.Text)) && (!UserRepository.UserEmailExists(LogInTextBox.Text)))
			{
				LogInError.Visibility = Visibility.Hidden;
				LogInWrongUserNameError.Visibility = Visibility.Visible;
				return false;
			}
			else if (LogInPassBox.Password.Length == 0 || !PasswordRepository.CheckPassword(LogInTextBox.Text, LogInPassBox.Password))
			{
				LogInWrongUserNameError.Visibility = Visibility.Hidden;
				LogInError.Visibility = Visibility.Visible;
				return false;
			}
			return true;
		}

		private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
		{
			SignUpOptBtn.Focus();
			UserRepository.CreateUser("Amir", "XD", "AmirAdmin@Gmail.com", UserRole.admin);
			if (KeepSignedIn)
			{
				//WelcomePage.Hide();
				//ShopPage.Active();
			}
		}

		private void SignUpOptBtn_Click(object sender, RoutedEventArgs e)
		{
			Fresh("SignUp");
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
				AgreeMentCheckBox.IsChecked = (bool)!AgreeMentCheckBox.IsChecked;
				AgreeMentCheckBox_Click(sender, e);
			}
		}

		private void AgreeMentCheckBox_Click(object sender, RoutedEventArgs e)
		{
			SignUpBtn.IsEnabled = (bool)AgreeMentCheckBox.IsChecked;
		}

		private void SignUpBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ValidateSignUp())
			{
				CurrentUser = UserRepository.CreateUser(UsernameTextBox.Text, NewUserPassBox.Password, NewUserEmail.Text, UserRole.customer);
				StoreView StoreViewWindow = new StoreView(CurrentUser);
				Close();
				StoreViewWindow.Show();
			}
		}

		private void SignUpBack_Click(object sender, RoutedEventArgs e)
		{
			SignUpGrid.Visibility = Visibility.Hidden;
			Fresh();
			WelcomeGrid.Visibility = Visibility.Visible;
			SignUpOptBtn.Focus();
		}

		private void LogInBtn_Click(object sender, RoutedEventArgs e)
		{
			if (KeepSignCheckBox.IsFocused == false)
			{
				if (ValidateLogIn())
				{
					CurrentUser = UserRepository.SearchUser(LogInTextBox.Text);
					KeepSignedIn = (bool)KeepSignCheckBox.IsChecked;
					StoreView StoreViewWindow = new StoreView(CurrentUser);
					Close();
					StoreViewWindow.Show();
					MessageBox.Show("Seccessfully logged in! ♥");
				}
			}
			else if (KeepSignCheckBox.IsChecked == true)
				KeepSignCheckBox.IsChecked = false;
			else
				KeepSignCheckBox.IsChecked = true;
		}

		private void LogInBack_Click(object sender, RoutedEventArgs e)
		{
			LogInGrid.Visibility = Visibility.Hidden;
			Fresh();
			WelcomeGrid.Visibility = Visibility.Visible;
			SignUpOptBtn.Focus();
		}

		private void LogInShowHideBtn_Click(object sender, RoutedEventArgs e)
		{
			if(LogInPassBlock.Visibility == Visibility.Hidden)
			{
				LogInShowHideBtn.ClickMode = System.Windows.Controls.ClickMode.Release;
				ShowHideIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
				LogInPassBlock.Text = LogInPassBox.Password;
				if (LogInPassBlock.Text == string.Empty)
					LogInPassBlock.Opacity = 0;
				LogInPassBlock.Visibility = Visibility.Visible;
			}
			else
			{
				LogInShowHideBtn.ClickMode = System.Windows.Controls.ClickMode.Press;
				ShowHideIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
				LogInPassBlock.Opacity = 1;
				LogInPassBlock.Visibility = Visibility.Hidden;
			}
		}

		private void Fresh()
		{
			UserNameError.Visibility = Visibility.Hidden;
			PassError.Visibility = Visibility.Hidden;
			ConfrimPassError.Visibility = Visibility.Hidden;
			EmailError.Visibility = Visibility.Hidden;
			LogInWrongUserNameError.Visibility = Visibility.Hidden;
			LogInError.Visibility = Visibility.Hidden;
		}

		private void Fresh(string mode)
		{
			Fresh();
			if(mode == "SignUp")
			{
				LogInTextBox.Text = string.Empty;
				LogInPassBox.Password = "";
				LogInPassBlock.Text = "";
			}
		}
	}
}
