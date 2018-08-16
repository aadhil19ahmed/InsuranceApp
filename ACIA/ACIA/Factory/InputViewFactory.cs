using System;
using ACIA.Views.UILayer;
using Xamarin.Forms;
using ACIA.Model;

namespace ACIA.Factory
{
    /// <summary>
    /// Input view factory.
    /// </summary>
	public static class InputViewFactory
	{
        /// <summary>
        /// Gets the input view.
        /// </summary>
        /// <returns>The input view.</returns>
        /// <param name="view">View.</param>
		public static DUInputView GetInputView(Model.View view)
		{
            switch (view.InputType.ToUpper())
			{
				case "TEXT":

					return new EntryView(view);

				case "PICKER":

                    return new DUIPickerView(view);

				case "SWITCH":

                    return new DUIToggleView(view);

                case "TEXTVIEW":

                    return new DUITextView(view);

                case "DATEPICKERVIEW":

                    return new DUIDatePickerView(view);

				default:

                    throw new InvalidOperationException();
            }
		}
	}
}
