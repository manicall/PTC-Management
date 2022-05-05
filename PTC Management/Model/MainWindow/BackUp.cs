using System.Configuration;
using System.Data.SqlClient;

namespace PTC_Management.Model.MainWindow
{

    internal class Backup
    {
        // подключение к базе данных через строку подключения 
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["PTC_ManagementConnection"].ConnectionString;

        internal const string _restore = "restore";
        internal const string _create = "create";

        public string Restore => _restore;
        public string Create => _create;

        internal static void CreateBackup(string backupFile = @"D:\test.bak")
        {
            string stringCommands =
                $"BACKUP DATABASE[PTC Management] " +
                $"TO DISK = N'{backupFile}' WITH init;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(stringCommands, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void RestoreBackup(string backupFile = @"D:\test.bak")
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string[] stringCommands = new string[3];

            // устанавливает однопользовательский режим базы данных
            stringCommands[0] =
                "ALTER DATABASE [PTC Management]" +
                " SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            // восстанавливает базу данных из файла бекапа
            stringCommands[1] =
                $"USE MASTER " +
                $"RESTORE DATABASE [PTC Management] " +
                $"FROM DISK='{backupFile}'";
            // устанавливает многопользовательский режим базы данных
            stringCommands[2] =
                "ALTER DATABASE [PTC Management] " +
                "SET MULTI_USER";

            foreach (var stringCommand in stringCommands)
            {
                new SqlCommand(stringCommand, connection).ExecuteNonQuery();
            }

            connection.Close();
        }
    }


}
