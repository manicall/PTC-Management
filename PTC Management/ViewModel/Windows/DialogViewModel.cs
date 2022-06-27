using PTC_Management.Commands;
using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.Windows;

using System;
using System.Runtime.Remoting.Lifetime;

namespace PTC_Management.ViewModel
{
    public class DialogViewModel : ViewModelBaseWindow
    {
        private Entity dialogItem;

        /// <summary>
        /// Содержит константы, которые определяют 
        /// с какой моделью представления взаимодействовать
        /// </summary>
        public Destinations Destinations => new Destinations();

        /// <summary>
        /// Копия выбранного элемента таблицы
        /// </summary>
        public Entity DialogItem { get => dialogItem; set => SetProperty(ref dialogItem, value); }

        /// <summary>
        /// Используется для отображения поля,
        /// в которое воодится количество копий выбранной записи,
        /// которые необходимо создать 
        /// </summary>
        public CopyParameters CopyParameters { get; set; }

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
        protected virtual void OnDialogActionCommand(string action) { }

        protected bool DoDialogActionCommand(string action)
        {
            var result = true;
            switch (action)
            {
                case Actions.writeAndClose:
                    result = DoAction(MainWindowAction);
                    if (result) Close();
                    break;
                case Actions.write:
                    //if (DialogItem is MaintanceLog m)
                    //{
                    //    if MaintanceLog.repository.FindFirst(m.Id);

                    //}
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

            if (DialogItem is Employee employee)
                if (!employee.GetCanExecute())
                {
                    employee.SetCanExecute();
                    return false;
                }

            if (DialogItem is Route route)
                if (!route.GetCanExecute())
                {
                    route.SetCanExecute();
                    return false;
                }

            if (DialogItem is Transport transport)
                if (!transport.GetCanExecute())
                {
                    transport.SetCanExecute();
                    return false;
                }

            if (DialogItem is Itinerary itinerary)
                if (!itinerary.GetCanExecute())
                {
                    itinerary.SetCanExecute();
                    return false;
                }

            Entity entity = DialogItem.Clone();

            switch (action)
            {
                case Actions.add:
                    result = entity.Add();
                    break;
                case Actions.update:

                    if (SelectedItem is Itinerary i)
                    {
                        if (DialogItem is Itinerary it)
                        {
                            i.IdRoute = it.IdRoute;
                            i.IdTransport = it.IdTransport;
                            i.IdEmployee = it.IdEmployee;
                            i.Employee = (DialogItem as Itinerary).Employee;
                            i.Route = (DialogItem as Itinerary).Route;
                            i.Transport = (DialogItem as Itinerary).Transport;

                            i.TimeOnDeparture = it.TimeOnDeparture;
                            i.TimeWhenReturning = it.TimeWhenReturning;
                            i.Date = it.Date;
                            i.SpeedometerInfoOnDeparture = it.SpeedometerInfoOnDeparture;
                            i.SpeedometerInfoWhenReturning = it.SpeedometerInfoWhenReturning;
                            i.Mileage = it.Mileage;
                        }
                    }
                    else { 
                        SelectedItem.SetFields(DialogItem); 
                    }

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
