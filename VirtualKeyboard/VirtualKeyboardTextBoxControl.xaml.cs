using System;
using System.Windows;
using System.Windows.Controls;
using VirtualKeyboard.InputChecks;

namespace VirtualKeyboard
{
    /// <summary>
    /// Interaction logic for VirtualKeyboardTextBoxControl.xaml
    /// </summary>
    public partial class VirtualKeyboardTextBoxControl : UserControl
    {
        public enum CharSetLanguage 
        { 
            HU,
            EN
        }

        public enum InputCheckType
        {
            None,
            Double
        }

        public InputCheckType CurrentInputCheckType { get; set; }

        public VirtualKeyboardTextBoxControl()
        {
            InitializeComponent();
        }

        private void EditTextButton_Click(object sender, RoutedEventArgs e)
        {
            WpfInputKeyboard dialog = new WpfInputKeyboard(CharSet, CurrentInputCheckType)
            {
                FieldTitle = Title,
                Text = Text ?? "",
                CaretPos = Text != null ? Text.Length : 0,
            };

            if ((bool)dialog.ShowDialog())
            {
                Text = dialog.Text;
            }
        }

        #region Text
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text),
                                                                                                  typeof(string),
                                                                                                  typeof(VirtualKeyboardTextBoxControl),
                                                                                                  new FrameworkPropertyMetadata("",
                                                                                                                                  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region InputCheck
        public static readonly DependencyProperty InputCheckProperty = DependencyProperty.Register(nameof(InputCheck),
                                                                                                   typeof(InputCheckType),
                                                                                                   typeof(VirtualKeyboardTextBoxControl),
                                                                                                   new FrameworkPropertyMetadata(InputCheckType.None,
                                                                                                                          FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                                          new PropertyChangedCallback(InputCheckTypeChanged)));

        public InputCheckType InputCheck
        {
            get { return (InputCheckType)GetValue(InputCheckProperty); }
            set { SetValue(InputCheckProperty, value); }
        }

        private static void InputCheckTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VirtualKeyboardTextBoxControl control &&
                 e.NewValue is InputCheckType value)
            {
                control.CurrentInputCheckType = value;
            }
        }
        #endregion

        #region Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
                                                                                              typeof(string),
                                                                                              typeof(VirtualKeyboardTextBoxControl),
                                                                                              new FrameworkPropertyMetadata("",
                                                                                                                          FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region CharSet
        public static readonly DependencyProperty CharSetProperty = DependencyProperty.Register(nameof(CharSet),
                                                                                                typeof(CharSetLanguage),
                                                                                                typeof(VirtualKeyboardTextBoxControl),
                                                                                                new FrameworkPropertyMetadata(CharSetLanguage.HU,
                                                                                                                          FrameworkPropertyMetadataOptions.None));
        public CharSetLanguage CharSet
        {
            get { return (CharSetLanguage)GetValue(CharSetProperty); }
            set { SetValue(CharSetProperty, value); }
        }
        #endregion
    }
}
