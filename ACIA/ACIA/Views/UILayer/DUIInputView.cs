using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ACIA.Views.UILayer
{
    /// <summary>
    /// DUI nput view.
    /// </summary>
	public abstract class DUInputView : ContentView, INotifyPropertyChanged
	{
        private object _capturedValue;
        public object CapturedValue
		{
			get
			{
				return _capturedValue;
			}
			set
			{
				_capturedValue = value;
				NotifyPropertyChanged();
			}
		}
       
		public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
