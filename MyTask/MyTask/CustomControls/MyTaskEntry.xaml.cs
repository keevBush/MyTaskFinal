using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.CustomControls
{
    public class MyTaskEntryEventArgs : EventArgs
    {
        public string Text { get; set; }
    }


    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTaskEntry : Grid
    {
        public event EventHandler<MyTaskEntryEventArgs> TextChanged;   
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
                                    nameof(Label),
                                    typeof(string),
                                    typeof(MyTaskEntry),
                                    "",
                                    BindingMode.TwoWay,
                                    propertyChanged: (bindable, oldVal, newVal) => ((MyTaskEntry)bindable).OnLabelChange((string)newVal));
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }


        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                    nameof(Text),
                                    typeof(string),
                                    typeof(MyTaskEntry),
                                    "",
                                    BindingMode.TwoWay,
                                    propertyChanged: (bindable, oldVal, newVal) => ((MyTaskEntry)bindable).OnTextChanged((string)newVal));
        public string Text {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color BaseColor { get; set; } = Color.FromHex("CDD5E0");
        public Color FocusedColor { get; set; } = Color.RoyalBlue;

        public MyTaskEntry()
        {
            InitializeComponent();
        }

       
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            border.BorderColor = FocusedColor;
            label.FontAttributes = FontAttributes.Bold;
            label.TextColor = FocusedColor;
            label.IsVisible = true;
            label.FadeTo(1);

        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            border.BorderColor = BaseColor;
            label.FontAttributes = FontAttributes.None;
            label.TextColor = BaseColor;
            if (string.IsNullOrEmpty(Text))
                label.FadeTo(0);
            else
                label.FadeTo(1);
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        { 
            OnTextChanged(textbox.Text);
            
        }

        public void OnTextChanged(string value)  
        {

            Text = value;
            textbox.Text = value;
            if (string.IsNullOrEmpty(Text))
                label.FadeTo(0);
            else
                label.FadeTo(1);
        }

        public void OnLabelChange(string value)
        {
            
            textbox.Placeholder = value;
            label.Text = value;
        }

    }
}