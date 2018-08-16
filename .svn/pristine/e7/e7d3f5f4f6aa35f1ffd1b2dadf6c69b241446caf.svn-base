using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ACIA.Model;

namespace ACIA.Views.UILayer
{
    public class DUITextView : DUInputView
    {
        public DUITextView(Model.View view)
        {
            var editor = new Editor
            {
                HeightRequest = 50,
                BackgroundColor = Color.FromRgb(237, 237, 237),
            };
            editor.SetBinding(Editor.TextProperty, "CapturedValue", BindingMode.TwoWay);
            editor.BindingContext = this;
            var editorStack = new StackLayout();
            //editor.IsVisible = view.default_visibility;
            editorStack.Children.Add(editor);
            Content = editor;
        }
    }
}
