using System.Windows.Controls;
using System.Windows.Data;

namespace PTC_Management.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeCreateView.xaml
    /// </summary>
    public partial class EmployeeDialogView : UserControl
    {
        public EmployeeDialogView()
        {
            InitializeComponent();
        }

        private void buttonWrite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            changeText(textBoxSurname);
            changeText(textBoxName);
            changeText(textBoxMiddleName);
            changeText(textBoxDriverLicense);
        }
        private void changeText(TextBox textBox) {
            if (textBox == null) return;
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
