using Microsoft.Win32;

using PTC_Management.Commands;
using PTC_Management.Model;
using PTC_Management.ViewModel;

using System.Collections.Generic;
using System.Windows;

namespace PTC_Management
{
    class MainWindowViewModel : ViewModelBaseWindow
    {
        private Backup backup;
        private Destinations destinations;
        private IsEnabled isEnabled;

        public Destinations Destinations
        {
            get => destinations ?? (destinations = new Destinations());
            set => destinations = value;
        }

        public Backup Backup
        {
            get => backup ?? (backup = new Backup());
            set => backup = value;
        }

        public IsEnabled IsEnabled
        {
            get
            {
                if (isEnabled == null)
                {
                    IsEnabled = new IsEnabled()
                    {
                        Field = new Dictionary<string, string>()
                        {
                            [Destinations.Employee] = "True",
                            [Destinations.Route] = "True",
                            [Destinations.Transport] = "True",
                            [Destinations.Itinerary] = "True",
                            [Destinations.Schedule] = "True"
                        }
                    };
                }
                return isEnabled;
            }
            set => isEnabled = value;
        }

        public Command<string> NavigationCommand { get; private set; }
        public Command<string> BackUpCommand { get; private set; }

        public MainWindowViewModel()
        {
            // сохраняем экземпляр главного окна
            // для корректной работы метода Close()
            window = Application.Current.MainWindow;

            // создание команды перехватывающей сообщения от кнопки
            NavigationCommand = new Command<string>(OnNavigation);

            // создание команды работы с бекапом базы данных
            BackUpCommand = new Command<string>(OnBackUp);

            // установка представления по умолчанию
            CurrentViewModel = viewModels.GetViewModel(Destinations.employee);
            IsEnabled.Field[Destinations.employee] = "False";

            RunTime.Stop();
        }

        #region Методы
        /// <summary>
        /// Управление содержимым главного окна 
        /// </summary>
        private void OnNavigation(string destination)
        {
            CurrentViewModel = viewModels.GetViewModel(destination);
            LockCurrentViewModelButton(destination);
        }

        /// <summary>
        /// Блокирует нажатую кнопку, которая изменяет текущую модель представления
        /// </summary>
        private void LockCurrentViewModelButton(string destination)
        {
            // обнуление состояния кнопок
            IsEnabled.Field = new Dictionary<string, string>()
            {
                [Destinations.Employee] = "True",
                [Destinations.Route] = "True",
                [Destinations.Transport] = "True",
                [Destinations.Itinerary] = "True",
                [Destinations.Schedule] = "True"
            };

            // блокировка нажатой кнопки
            IsEnabled.Field[destination] = "False";

            // вызов события OnPropertyChanged
            IsEnabled.RaisePropertyChanged();
        }

        /// <summary>
        ///  управление резервным копированием базы данных
        /// </summary>
        private void OnBackUp(string command)
        {
            switch (command)
            {
                case Backup._create: CreateBackUp(); break;
                case Backup._restore: RestoreBackUp(); break;
                default: return;
            }

            CurrentViewModel = null;
        }

        /// <summary>
        /// Создание файла восстановления
        /// </summary>
        private void CreateBackUp()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = GetFilter(),
                Title = "Сохранения файла резервного копирования"
            };

            if (dialog.ShowDialog() == true) Backup.CreateBackup(dialog.FileName);
        }

        /// <summary>
        /// Открытие файла восстановления
        /// </summary>
        private void RestoreBackUp()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = GetFilter(),
                Title = "Открытие файла резервного копирования"
            };

            if (dialog.ShowDialog() == true) Backup.RestoreBackup(dialog.FileName);
        }

        /// <summary>
        /// Фильтр файлов в открытом диалоговом окне
        /// </summary>
        private string GetFilter()
        {
            return "Файлы восстановления(*.bak;*.trn;*.log)" +
                "|*.bak;*.trn;*.log" + "|Все файлы|*";
        }

        #endregion
    }
}