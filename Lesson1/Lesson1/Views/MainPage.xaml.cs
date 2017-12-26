using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lesson1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding("Title"));
            Content = new StackLayout
            {
                Children = { label }
            };
        }
    }
}
