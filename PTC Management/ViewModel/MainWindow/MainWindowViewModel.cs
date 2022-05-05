using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel;
using System.Configuration;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

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

            Backup.RestoreBackup();
        }

        class Backup
        {
            // подключение к базе данных через строку подключения 
            static readonly string connectionString = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;

            public static void CreateBackup(string backupFile = @"D:\test.bak")
            {
                string connectionString = ConfigurationManager
                    .ConnectionStrings["AppContext"]
                    .ConnectionString;

                string stringCommands = $"BACKUP DATABASE[PTC Management] " +
                    $"TO DISK = N'{backupFile}' WITH init;";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(stringCommands, connection);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            public static void RestoreBackup(string backupFile = @"D:\test.bak")
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string[] stringCommands = new string[3];

                // устанавливает однопользовательский режим базы данных
                stringCommands[0] = "ALTER DATABASE [PTC Management]" +
                    " SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                // восстанавливает базу данных из файла бекапа
                stringCommands[1] = $"USE MASTER " +
                    $"RESTORE DATABASE [PTC Management] " +
                    $"FROM DISK='{backupFile}'";
                // устанавливает многопользовательский режим базы данных
                stringCommands[2] = "ALTER DATABASE [PTC Management] " +
                    "SET MULTI_USER";

                foreach (var stringCommand in stringCommands)
                {
                    new SqlCommand(stringCommand, connection).ExecuteNonQuery();
                }

                connection.Close();
            }
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