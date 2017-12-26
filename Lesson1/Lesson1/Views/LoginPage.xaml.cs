using Lesson1.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lesson1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var usernameEntry = new Entry();
            usernameEntry.Margin = new Thickness(0, 0, 0, 3);
            usernameEntry.SetBinding(Entry.TextProperty, new Binding("Username"));
            usernameEntry.Behaviors.Add(new EntryValidationBehavior());

            var passwordEntry = new Entry { IsPassword = true };
            passwordEntry.Margin = new Thickness(0, 0, 0, 3);
            passwordEntry.SetBinding(Entry.TextProperty, new Binding("Password"));

            var applyButton = new Button { Text = "Sign in/up" };
            applyButton.Margin = new Thickness(0, 0, 0, 3);
            applyButton.SetBinding(Button.CommandProperty, new Binding("LoginCommand"));

            var errorLabel = new Label { TextColor = Color.Red };
            errorLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsErrorVisible"));
            errorLabel.SetBinding(Label.TextProperty, new Binding("Error"));

            Content = new StackLayout
            {
                Children =
                {
                    usernameEntry,
                    passwordEntry,
                    applyButton,
                    errorLabel
                }
            };
        }
    }
}