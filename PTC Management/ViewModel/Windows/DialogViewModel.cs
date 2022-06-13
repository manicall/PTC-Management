using PTC_Management.Commands;
using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.Windows;

using System;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBaseWindow
    {
        private CopyParameters copyParameters;
        private Entity dialogItem;

        /// <summary>
        /// Содержит константы, которые определяют 
        /// с какой моделью представления взаимодействовать
        /// </summary>
        public Destinations Destinations => new Destinations();

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
        public string MainWindowAction { get; set; }

        /// <summary>
        /// Выбранный индекс в таблице 
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Команда, вызываемая когда необходимо выполнить,
        /// определяемое кнопкой на диалоговом окне
        /// </summary>
        public Command<string> DialogActionCommand { get; private set; }

        /// <summary>
        /// Команда, вызываемая когда необходимо выбрать существующую запись
        /// </summary>
        public Command<string> DialogSelectСommand { get; private set; }

        public DialogViewModel()
        {
            DialogActionCommand = new Command<string>(OnDialogActionCommand);
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            CopyParameters = new CopyParameters();
        }

        /// <summary>
        /// Выполняет действие заданное кнопкой на диалоговом окне
        /// </summary>
        protected virtual void OnDialogActionCommand(string action)
        {
            DoDialogActionCommand(action);
        }

        protected bool DoDialogActionCommand(string action)
        {
            var result = true;
            switch (action)
            {
                case Actions.writeAndClose:
                    result = DoAction(MainWindowAction);
                    Close();
                    break;
                case Actions.write:
                    result = DoAction(MainWindowAction);
                    break;
                case Actions.close:
                    Close();
                    break;
            }
            return result;
        }

        /// <summary>
        /// Изменяет записи в базе данных
        /// </summary>
        protected bool DoAction(string action)
        {
            bool result;
            Entity entity = DialogItem.Clone();

            switch (action)
            {
                case Actions.add:
                    result = entity.Add();
                    break;
                case Actions.update:
                    SelectedItem.SetFields(DialogItem);
                    result = SelectedItem.Update();
                    break;
                case Actions.copy:
                    result = entity.Copy(SelectedItem, CopyParameters.Count);
                    break;
                default:
                    throw new ArgumentException("Действие не обработано");
            }

            WindowParameters.StatusBarMessage = GetStatusBarMessage(action, result);

            DialogItem.Id = entity.Id;
            return result;
        }

        /// <summary>
        /// Возвращает сообщение которое необходимо передать строке состояния
        /// </summary>
        private string GetStatusBarMessage(string action, bool result)
        {
            switch (action)
            {
                case Actions.add:
                    if (result) return "Запись успешно добавлена";
                    else return "Не удалось добавить запись";

                case Actions.update:
                    if (result) return "Запись успешно изменена";
                    else return "Не удалось изменить запись";

                case Actions.copy:
                    if (result) return "Запись успешно скопирована";
                    else return "Не удалось скопировать запись";

                default:
                    throw new ArgumentException("Действие не обработано");
            }

        }

        /// <summary>
        /// Используется для выбора существующих записей
        /// </summary>
        protected virtual void OnDialogSelectСommand(string destination) { }

        /// <summary> Метод показа модели представления в окне </summary>
        public void Show()
        {
            window = new Dialog { DataContext = this };
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}
