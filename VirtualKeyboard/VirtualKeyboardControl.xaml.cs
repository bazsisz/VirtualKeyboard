using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using VirtualKeyboard.ControlHandlers;
using VirtualKeyboard.InputChecks;

namespace VirtualKeyboard
{
    /// <summary>
    /// Interaction logic for VirtualKeyboardTextBoxControl.xaml
    /// </summary>
    public partial class VirtualKeyboardControl : UserControl
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

        public InputCheckType CurrentInputCheckType { get; private set; }
        public IControlHandler ControlHandler { get; private set; }

        public VirtualKeyboardControl()
        {
            InitializeComponent();
        }

        private void EditTextButton_Click(object sender, RoutedEventArgs e)
        {
            SetControlHandler();
            WpfInputKeyboard dialog = new WpfInputKeyboard(CharSet, CurrentInputCheckType)
            {
                FieldTitle = Title,
                Text = ControlHandler.TextValue ?? "",
                CaretPos = ControlHandler.TextValue != null ? ControlHandler.TextValue.Length : 0,
            };
            if ((bool)dialog.ShowDialog())
            {
                ControlHandler.TextValue = dialog.Text;
            }
        }

        private void SetControlHandler()
        {
            if (ControlItem is TextBox textBox)
            {
                ControlHandler = new TextBoxControlHandler(textBox);
            }
            else if (ControlItem is ComboBox comboBox && comboBox.IsEditable)
            {
                ControlHandler = new ComboBoxControlHandler(comboBox);
            }
            else if (ControlItem is NumericUpDown numericUpDown)
            {
                ControlHandler = new NumericUpDownControlHandler(numericUpDown);
            }
            else
            {
                throw new Exception("ControlItem dependency property needs to be either TextBox, NumericUpDown or editable ComboBox!");
            }
        }
        
        #region ControlItem
        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register(nameof(ControlItem),
                                                                                                  typeof(Control),
                                                                                                  typeof(VirtualKeyboardControl),
                                                                                                  new FrameworkPropertyMetadata(null,
                                                                                                                                  FrameworkPropertyMetadataOptions.None));
        public Control ControlItem
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }
        #endregion

        #region InputCheck
        public static readonly DependencyProperty InputCheckProperty = DependencyProperty.Register(nameof(InputCheck),
                                                                                                   typeof(InputCheckType),
                                                                                                   typeof(VirtualKeyboardControl),
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
            if (d is VirtualKeyboardControl control &&
                 e.NewValue is InputCheckType value)
            {
                control.CurrentInputCheckType = value;
            }
        }
        #endregion

        #region Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
                                                                                              typeof(string),
                                                                                              typeof(VirtualKeyboardControl),
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
                                                                                                typeof(VirtualKeyboardControl),
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