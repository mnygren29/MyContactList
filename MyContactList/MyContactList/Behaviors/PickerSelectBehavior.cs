using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyContactList.Behaviors
{
    public class PickerSelectBehavior : Behavior<Picker>
    {

        static readonly BindableProperty ValidValuesProperty =

            BindableProperty.Create(nameof(ValidValues), typeof(string[]), typeof(PickerBorrowerBehavior));

        public string[] ValidValues

        {

            get => (string[])GetValue(ValidValuesProperty);

            set => SetValue(ValidValuesProperty, value);

        }

        protected override void OnAttachedTo(Picker bindable)

        {

            bindable.SelectedIndexChanged += Bindable_SelectedIndexChanged;

        }

        protected override void OnDetachingFrom(Picker bindable)

        {

            bindable.SelectedIndexChanged -= Bindable_SelectedIndexChanged;

        }

        void Bindable_SelectedIndexChanged(object sender, EventArgs e)

        {

        }
    }
}
