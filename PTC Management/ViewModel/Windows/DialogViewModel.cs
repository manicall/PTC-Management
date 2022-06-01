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
        /// <summary>
        /// Используется для отображения поля,
        /// в которое воодится количество копий выбранной записи,
        /// которые необходимо создать 
        /// </summary>
        private CopyParameters copyParameters;
        /// <summary>
        /// Необходим для вызова события PropertyChanged 
        /// у свойства DialogItem
        /// </summary>
        private Itinerary displayedDialogItem;

        /// <summary>
        /// Действие которое было выбрано в главном окне
        /// </summary>
        private string mainWindowAction;
        public string MainWindowAction
        {
            get => mainWindowAction;
            set => mainWindowAction = value;
        }

       
        public CopyParameters CopyParameters
        {
            get => copyParameters;
            set => SetProperty(ref copyParameters, value);
        }

        /// <summary>
        /// Копия выбранного элемента таблицы
        /// </summary>
        private Entity dialogItem;
        public Entity DialogItem
        {
            get => dialogItem;
            set => SetProperty(ref dialogItem, value);
        }

        /// <summary>
        /// Выбранный индекс в таблице 
        /// </summary>
        public int SelectedIndex { get; set; }
        public Command<string> DialogActionCommand { get; private set; }

        public Command<string> DialogSelectСommand { get; private set; }

        public Itinerary DisplayedDialogItem
        {
            get { return displayedDialogItem; }
            set { SetProperty(ref displayedDialogItem, value); }
        }


        public DialogViewModel()
        {
            DialogActionCommand = new Command<string>(OnDialogActionCommand);
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);
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
                    break;
                case Actions.update:
                    SelectedItem.SetFields(DialogItem);
                    SelectedItem.Update();
                    break;
                case Actions.copy:
                    entity.Copy(CopyParameters.Count);
                    break;
                default:
                    throw new ArgumentException("Действие не обработано");
            }

            DialogItem.Id = entity.Id;
        }

        public void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            selectWindow.CurrentViewModel = viewModels.GetViewModel(destination);

            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                Itinerary tempDialogItem = (Itinerary)DisplayedDialogItem.Clone();
                switch (destination)
                {
                    case Destinations.employee:
                        tempDialogItem.Employee = (Employee)selectWindow.ReturnedItem;
                        ((Itinerary)DialogItem).IdEmployee = ((Employee)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.route:
                        tempDialogItem.Route = (Route)selectWindow.ReturnedItem;
                        ((Itinerary)DialogItem).IdRoute = ((Route)selectWindow.ReturnedItem).Id;

                        break;
                    case Destinations.transport:
                        tempDialogItem.Transport = (Transport)selectWindow.ReturnedItem;
                        ((Itinerary)DialogItem).IdTransport = ((Transport)selectWindow.ReturnedItem).Id;

                        break;

                    default: break;
                }

                DisplayedDialogItem = tempDialogItem;
            }

        }

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
