using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel;

namespace PTC_Management
{

    class MainWindowViewModel : ViewModelBase
    {
        #region поля и свойства
        private Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;


        private BindableBase _CurrentViewModel;
        public BindableBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set => SetProperty(ref _CurrentViewModel, value);
        }
        #endregion


        public MainWindowViewModel()
        {
            // создаем команду перехватывающую сообщения от кнопки
            NavCommand = new ParameterizedCommand<string>(OnNav);
            // установка представления по умолчанию
            CurrentViewModel = ViewModels._employee;
        }

        public ParameterizedCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case Destinations._employee:
                    CurrentViewModel = ViewModels._employee;
                    break;
                case Destinations._routes:
                    CurrentViewModel = ViewModels._route;
                    break;
                case Destinations._transport:
                    CurrentViewModel = ViewModels._transport;
                    break;
                case Destinations._itinerary:
                    CurrentViewModel = ViewModels._itinerary;
                    break;
                case Destinations._schedule:
                    CurrentViewModel = ViewModels._scheduleOfEmployee;
                    break;
                default:
                    CurrentViewModel = null;
                    break;
            }
        }
    }
}