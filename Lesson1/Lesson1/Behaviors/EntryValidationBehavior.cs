using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lesson1.Behaviors
{
    public class EntryValidationBehavior : Behavior<Entry>
    {
        private Color recentColor;

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += TextChanged;
            recentColor = bindable.TextColor;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= TextChanged;
            bindable.TextColor = recentColor;
            base.OnDetachingFrom(bindable);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (!Helpers.ValidationHelper.ValidateEmail(entry.Text))
                entry.TextColor = Color.Red;
            else
                entry.TextColor = recentColor;
        }
    }
}
