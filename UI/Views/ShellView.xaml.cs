using System.Windows;
using BLL.Repository;
using BLL.data_base;
using DAL.Enum.User;
using DAL.Entity;
using System.Windows.Media;

namespace UI.Views
{
	public partial class ShellView : Window
	{
		#region Properties
		private User? CurrentUser;
		private bool KeepSignedIn = false;
		#endregion

		public ShellView()
		{
			InitializeComponent();
			Data_Access.ReadAllData();
			CurrentUser = UserRepository.StayLoggedInUser();
			if (CurrentUser != null)
			{
				StoreView StoreViewWindow = new(CurrentUser);
				Close();
				StoreViewWindow.Show();
			}
			try { UserRepository.CreateUser("Admin420", "XD", "SuffAdmin420@Gmail.com", UserRole.admin); } catch { }
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
				UserNameError.Foreground = Brushes.Red;
				UserNameError.Opacity = 1;
				res = false;
			}
			else if(UsernameTextBox.Text.Length < 5)
			{
				UserNameError.Text = "* at least 5 characters";
				UserNameError.Foreground = Brushes.Red;
				UserNameError.Opacity = 1;
				res = false;
			}
			else if(UserRepository.UsernameExists(UsernameTextBox.Text))
			{
				UserNameError.Text = "* username already taken";
				UserNameError.Foreground = Brushes.Red;
				UserNameError.Opacity = 1;
				res = false;
			}
			else
			{
				UserNameError.Opacity = 0.5;
				UserNameError.Foreground = Brushes.Black;
				UserNameError.Text = "at least 5 characters";
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
				EmailError.Text = "* Invalid email";
				EmailError.Visibility = Visibility.Visible;
				res = false;
			}
			else if (UserRepository.UserEmailExists(NewUserEmail.Text))
			{
				EmailError.Text = "* another account is using this email";
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
			if (LogInTextBox.Text.Length == 0 || (!UserRepository.UsernameExists(LogInTextBox.Text) && !UserRepository.UserEmailExists(LogInTextBox.Text)) || UserRepository.IsUserDeleted(LogInTextBox.Text))
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
		}

		private void WelcomePage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				DragMove();
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
			Application.Current.Shutdown();
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
				AgreeMentCheckBox.IsChecked = !AgreeMentCheckBox.IsChecked;
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
				try
				{
					CurrentUser = UserRepository.CreateUser(UsernameTextBox.Text, NewUserPassBox.Password, NewUserEmail.Text, UserRole.customer);
					StoreView StoreViewWindow = new(CurrentUser);
					Close();
					StoreViewWindow.ShowActivated = true;
					StoreViewWindow.Show();
				}
				catch
				{
					MessageBox.Show("Sign-Up failed, please try again");
				}
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
					try
					{
						CurrentUser = UserRepository.SearchUser(LogInTextBox.Text);
						KeepSignedIn = (bool)KeepSignCheckBox.IsChecked;
						if (KeepSignedIn)
						{
							CurrentUser.StayLoggedIn = true;
							UserRepository.UpdateUser(CurrentUser);
						}
						StoreView StoreViewWindow = new(CurrentUser);
						Close();
						StoreViewWindow.ShowActivated = true;
						StoreViewWindow.Show();
					}
					catch
					{
						MessageBox.Show("Login failed, please try again");
					}
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
			UserNameError.Opacity = 0.5;
			UserNameError.Foreground = Brushes.Black;
			UserNameError.Text = "at least 5 characters";
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
