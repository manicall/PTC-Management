using Microsoft.Win32;

using PTC_Management.Commands;
using PTC_Management.Model;
using PTC_Management.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PTC_Management
{
    class MainWindowViewModel : ViewModelBaseWindow
    {

        private Backup backup;
        private Destinations destinations;
        private IsEnabled isEnabled;

        public Destinations Destinations
        {
            get => destinations ?? new Destinations();
            set => destinations = value;
        }

        public Backup Backup
        {
            get => backup ?? new Backup();
            set => backup = value;
        }

        public IsEnabled IsEnabled {
            get {
                if (isEnabled == null)
                {
                    IsEnabled = new IsEnabled()
                    {
                        Field = new Dictionary<string, string>()
                        {
                            ["Employee"] = "True",
                            ["Route"] = "True",
                            ["Transport"] = "True",
                            ["Itinerary"] = "True",
                            ["Schedule"] = "True"
                        }
                    };
                }
                return isEnabled;
            }
            set => isEnabled = value; }

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
            IsEnabled.Field["Employee"] = "False";

            // для отображения времени запуска
            RunTime.Stop();
        }

        #region Команды
        public Command<string> NavigationCommand { get; private set; }
        public Command<string> BackUpCommand { get; private set; }

        #endregion

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
            // обнуление состояния кнопки
            IsEnabled.Field = new Dictionary<string, string>()
            {
                ["Employee"] = "True",
                ["Route"] = "True",
                ["Transport"] = "True",
                ["Itinerary"] = "True",
                ["Schedule"] = "True"
            };

            // изменение состояния кнопки
            switch (destination)
            {
                case Destinations.employee:
                    IsEnabled.Field["Employee"] = "False";
                    break;
                case Destinations.route:
                    IsEnabled.Field["Route"] = "False";
                    break;
                case Destinations.transport:
                    IsEnabled.Field["Transport"] = "False";
                    break;
                case Destinations.itinerary:
                    IsEnabled.Field["Itinerary"] = "False";
                    break;
                case Destinations.schedule:
                    IsEnabled.Field["Schedule"] = "False";
                    break;

            }

            // вызов события OnPropertyChanged
            IsEnabled.Field = new Dictionary<string, string>()
            {
                ["Employee"] = IsEnabled.Field["Employee"],
                ["Route"] = IsEnabled.Field["Route"],
                ["Transport"] = IsEnabled.Field["Transport"],
                ["Itinerary"] = IsEnabled.Field["Itinerary"],
                ["Schedule"] = IsEnabled.Field["Schedule"]
            };


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
                default: break;
            }
        }

        /// <summary>
        /// Создание файла восстановления
        /// </summary>
        private void CreateBackUp()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = GetFilter();
            dialog.Title = "Создание файла восстановления";

            if (dialog.ShowDialog() == true) Backup.CreateBackup(dialog.FileName);
        }

        /// <summary>
        /// Открытие файла восстановления
        /// </summary>
        private void RestoreBackUp()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = GetFilter();
            dialog.Title = "Открытие файла восстановления";

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