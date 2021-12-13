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
        public VirtualKeyboardTextBoxControl()
        {
            InitializeComponent();
        }

        private void EditTextButton_Click(object sender, RoutedEventArgs e)
        {
            WpfInputKeyboard dialog = new WpfInputKeyboard(CharSet)
            {
                FieldTitle = Title,
                InputText = InputCheck,
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
                                                                                                   typeof(InputCheck_Base),
                                                                                                   typeof(VirtualKeyboardTextBoxControl),
                                                                                                   new FrameworkPropertyMetadata(null,
                                                                                                                          FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public InputCheck_Base InputCheck
        {
            get { return (InputCheck_Base)GetValue(InputCheckProperty); }
            set { SetValue(InputCheckProperty, value); }
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
                                                                                                typeof(string),
                                                                                                typeof(VirtualKeyboardTextBoxControl),
                                                                                                new FrameworkPropertyMetadata("",
                                                                                                                          FrameworkPropertyMetadataOptions.None));
        public string CharSet
        {
            get { return (string)GetValue(CharSetProperty); }
            set { SetValue(CharSetProperty, value); }
        }
        #endregion
    }
}
