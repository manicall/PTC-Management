using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;

using System.Threading.Tasks;
using System.Windows.Input;

namespace PTC_Management
{

    class MainWindowViewModel : BindableBase
    {
        Repository<Entity> repository = new Repository<Entity>(new PTC_ManagementContext());
        #region Поля и свойства
        private ViewModels viewModels;

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


        public ICommand LoadCommand { get; }

        public async void Load()
        {       
            viewModels = new ViewModels(size);

            // установка представления по умолчанию
            CurrentViewModel = viewModels.employee;

            RunTime.Stop();
        }

        public MainWindowViewModel()
        {
            // создание команды перехватывающей сообщения от кнопки
            NavigationCommand = new ParameterizedCommand<string>(OnNavigation);
            // создание команды работы с бекапом базы данных
            BackUpCommand = new ParameterizedCommand<string>(OnBackUp);

            size = new Size(500);

            LoadCommand = new SimpleCommand(Load);
            
        }



        private Size size;
        public Size Size
        {
            get => size;
            set => SetProperty(ref size, value);
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
                    CurrentViewModel = viewModels.employee;
                    break;
                case Destinations._routes:
                    CurrentViewModel = viewModels.route;
                    break;
                case Destinations._transport:
                    CurrentViewModel = viewModels.transport;
                    break;
                case Destinations._itinerary:
                    CurrentViewModel = viewModels.itinerary;
                    break;
                case Destinations._schedule:
                    CurrentViewModel = viewModels.scheduleOfEmployee;
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

    internal class Size
    {
        public Size(int height) { this.height = height; }
        private int height;
        public int Height
        {
            get => height;
            set => height = value - 170;
        }
    }

}