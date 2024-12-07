using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Wpf.Ui.Controls;

namespace MoshimoBox.Helpers
{
    public static class BindingHelper
    {
        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.RegisterAttached(
                "StringFormat",
                typeof(string),
                typeof(BindingHelper),
                new PropertyMetadata(null, OnStringFormatChanged));

        public static string GetStringFormat(DependencyObject obj) =>
            (string)obj.GetValue(StringFormatProperty);

        public static void SetStringFormat(DependencyObject obj, string value) =>
            obj.SetValue(StringFormatProperty, value);

        private static void OnStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox && e.NewValue is string format)
            {
                // 既存のバインディングを取得
                Binding binding = BindingOperations.GetBinding(textBox, TextBox.TextProperty);
                if (binding != null)
                {
                    // バインディングをクリアして再設定
                    BindingOperations.ClearBinding(textBox, TextBox.TextProperty);
                    binding.StringFormat = format;
                    BindingOperations.SetBinding(textBox, TextBox.TextProperty, binding);
                }
            }
        }
    }
}
