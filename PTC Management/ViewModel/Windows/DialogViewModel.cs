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

        /// <summary>
        /// Изменяет записи в базе данных
        /// </summary>
        protected void DoAction(string action)
        {
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

        /// <summary>
        /// Возвращает настроенную модель представления
        /// </summary>
        protected SelectWindowViewModel GetSelectWindow(string destination)
        {
            var selectWindow = new SelectWindowViewModel();

            selectWindow.CurrentViewModel = GetViewModel(selectWindow, destination);
            selectWindow.Title = ViewModels.GetTitle("Выбор", destination);

            return selectWindow;
        }

        private ViewModelBaseEntity GetViewModel(SelectWindowViewModel selectWindow, string destination) 
        {
            var viewModel = viewModels.GetViewModel(destination);

            // скрытие кнопок позволяющих взаимодействовать с таблицей
            viewModel.TableActionButtonsVisible = Visibility.collapsed;

            // переопределяем команду, чтобы при двойном клике мыши
            // вызывался метод подтверждающий выбор записи из таблицы
            viewModel.TableAction = new Command<string>(
                (action) =>
                {
                    // если параметр соответствует параметру,
                    // передаваемому двойным кликом
                    if (action == Actions.update)
                        selectWindow.OnDialogSelectCommand();
                });

            // отключение видимости кнопок с журналом ТО и
            // журналом въезда и выезда у окна со списком транспорта
            if (viewModel is TransportViewModel transportVM)
                transportVM.TansportInfoButtonsVisibility = Visibility.collapsed;

            return viewModel;
        }

        /// <summary> Метод показа модели представления в окне </summary>
        public void Show()
        {
            window = new Dialog();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}
