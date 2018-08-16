using System;
using ACIA.Views.UILayer;
using Xamarin.Forms;

namespace ACIA.Behaviors
{
    /// <summary>
    /// Behavior that restricts the length of an entry
    /// </summary>
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try{
                var entry = (Entry)sender;
                if (entry.Text.Length > this.MaxLength)
                {
                    string entryText = entry.Text;
                    entry.TextChanged -= OnEntryTextChanged;
                    entry.Text = e.OldTextValue;
                    entry.TextChanged += OnEntryTextChanged;
                } 
            }catch
            {
                ACIANavigation.GetCurrentPage().DisplayAlert("Alert", "Please enter valid data", "Ok");
            }

        }
    }
}
