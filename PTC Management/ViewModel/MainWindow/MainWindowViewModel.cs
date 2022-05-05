using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel;


namespace PTC_Management
{

    class MainWindowViewModel : ViewModelBase
    {
        #region Поля и свойства
        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;

        private readonly Backup _Backup = new Backup();
        public Backup Backup => _Backup;

        public bool AutoSaveChanges
        {
            get { return Repository<Entity>.AutoSaveChanges; }
            set { Repository<Entity>.AutoSaveChanges = value; }
        }


        private BindableBase _CurrentViewModel;
        public BindableBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set => SetProperty(ref _CurrentViewModel, value);
        }

        #endregion

        public MainWindowViewModel()
        {
            // создание команды перехватывающей сообщения от кнопки
            NavigationCommand = new ParameterizedCommand<string>(OnNavigation);
            // создание команды работы с бекапом базы данных
            BackUpCommand = new ParameterizedCommand<string>(OnBackUp);
            // установка представления по умолчанию
            CurrentViewModel = ViewModels._employee;
        }

        #region Команды
        public ParameterizedCommand<string> NavigationCommand { get; private set; }
        public ParameterizedCommand<string> BackUpCommand { get; private set; }

        #endregion

        #region Методы
        private void OnNavigation(string destination)
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

        private void OnBackUp(string command)
        {
            switch (command)
            {
                case Backup._create: 
                    Backup.CreateBackup();
                    break;                 
                case Backup._restore: 
                    Backup.RestoreBackup();
                    break;

                default: break;
            }
        }

        #endregion
    }
}