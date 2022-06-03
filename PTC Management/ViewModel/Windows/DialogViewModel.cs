using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.ViewModel.Base;
using PTC_Management.Windows;

using System;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBaseWindow
    {
        private readonly Destinations destinations = new Destinations();

        private CopyParameters copyParameters;


        private Entity dialogItem;


        private string mainWindowAction;

        public Destinations Destinations => destinations;

        /// <summary>
        /// Копия выбранного элемента таблицы
        /// </summary>
        public Entity DialogItem
        {
            get => dialogItem;
            set => SetProperty(ref dialogItem, value);
        }

        /// <summary>
        /// Используется для отображения поля,
        /// в которое воодится количество копий выбранной записи,
        /// которые необходимо создать 
        /// </summary>
        public CopyParameters CopyParameters
        {
            get => copyParameters;
            set => SetProperty(ref copyParameters, value);
        }

        /// <summary>
        /// Действие которое было выбрано в главном окне
        /// </summary>
        public string MainWindowAction
        {
            get => mainWindowAction;
            set => mainWindowAction = value;
        }

        /// <summary>
        /// Выбранный индекс в таблице 
        /// </summary>
        public int SelectedIndex { get; set; }

        public Command<string> DialogActionCommand { get; private set; }

        public Command<string> DialogSelectСommand { get; private set; }

        public DialogViewModel()
        {
            DialogActionCommand = new Command<string>(OnDialogActionCommand);
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            CopyParameters = new CopyParameters();
        }

        protected virtual void OnDialogActionCommand(string action)
        {
            switch (action)
            {
                case Actions.writeAndClose:
                    DoAction(MainWindowAction);
                    Close();
                    break;
                case Actions.write:
                    DoAction(MainWindowAction);
                    break;
                case Actions.close:
                    Close();
                    break;
            }
        }

        protected void DoAction(string action)
        {
            /* взаимодействие с базой данных */
            Entity entity = DialogItem.Clone();
            switch (action)
            {
                case Actions.add:
                    entity.Add();
                    WindowParameters.StatusBarMessage = "Запись успешно добавлена";
                    break;
                case Actions.update:
                    SelectedItem.SetFields(DialogItem);
                    SelectedItem.Update();
                    WindowParameters.StatusBarMessage = "Запись успешно изменена";
                    break;
                case Actions.copy:
                    entity.Copy(CopyParameters.Count);
                    WindowParameters.StatusBarMessage = "Запись успешно скопирована";
                    break;
                default:
                    throw new ArgumentException("Действие не обработано");
            }

            DialogItem.Id = entity.Id;
        }

        /// <summary>
        /// Используется для выбора существующих записей
        /// </summary>
        protected virtual void OnDialogSelectСommand(string destination) { }

        /// <summary> Метод показа ViewModel в окне </summary>
        public void Show()
        {
            window = new Dialog();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}
