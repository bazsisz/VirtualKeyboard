using System.Windows;
using System.Windows.Controls;

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
                Text = Text ?? "",
                CaretPos = Text != null ? Text.Length : 0,
        };
            if ((bool)dialog.ShowDialog())
            {
                Text = dialog.Text;
            }

        }

        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register(nameof(Text),
                                                                                                    typeof(string),
                                                                                                    typeof(VirtualKeyboardTextBoxControl),
                                                                                                    new FrameworkPropertyMetadata("",
                                                                                                                                  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Text
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

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
    }
}
