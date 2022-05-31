﻿using Microsoft.Win32;

using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;

using System.Windows.Input;

namespace PTC_Management
{
    class MainWindowViewModel : ViewModelBaseWindow
    {
        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;

        private readonly Backup _Backup = new Backup();
        public Backup Backup => _Backup;

        public MainWindowViewModel()
        {
            // создание команды перехватывающей сообщения от кнопки
            NavigationCommand = new Command<string>(OnNavigation);
            // создание команды работы с бекапом базы данных
            BackUpCommand = new Command<string>(OnBackUp);

            // установка представления по умолчанию
            CurrentViewModel = viewModels.employee;

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
            //Size.Width = 1000;
            //Size.Width = Size.defaultWidth;
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
            
            viewModels = new ViewModels(Size);
        }

        private void CreateBackUp() {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = GetFilter();
            dialog.Title = "Создание файла восстановления";

            if (dialog.ShowDialog() == true) Backup.CreateBackup(dialog.FileName);
        }

        private void RestoreBackUp()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = GetFilter();
            dialog.Title = "Открытие файла восстановления";
            
            if (dialog.ShowDialog() == true) Backup.RestoreBackup(dialog.FileName);
        }

        private string GetFilter() {
            return "Файлы восстановления(*.bak;*.trn;*.log)" +
                "|*.bak;*.trn;*.log" + "|Все файлы|*";
        }
        
        #endregion
    }

}