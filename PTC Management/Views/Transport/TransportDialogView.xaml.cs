using System.Windows.Controls;
using System.Windows.Data;

namespace PTC_Management.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeCreateView.xaml
    /// </summary>
    public partial class TransportDialogView : UserControl
    {
        public TransportDialogView()
        {
            InitializeComponent();
        }

        private void buttonWrite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            changeText(textBoxName);
            changeText(textBoxMiddleName);
        }
        private void changeText(TextBox textBox)
        {
            if (textBox == null) return;
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
