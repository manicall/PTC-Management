using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;
using PTC_Management.Views.Windows;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
    /// <summary>
    /// ViewModel Для представления ItineraryDialogView
    /// </summary>
    internal class ItineraryDialogViewModel : DialogViewModel
    {
        #region ObservableCollection
        /// <summary> Поле, содержащее коллекцию объектов класса. </summary>
        private ObservableCollection<Itinerary> observableCollection;

        public ObservableCollection<Itinerary> ObservableCollection
        {
            get => observableCollection;
            set => observableCollection = value;
        }
        #endregion

        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;



        #region repository
        /// <summary>
        /// Поле, обеспечивающее взаимодействие с таблицей в базе данных.
        /// </summary>            
        private Repository<Itinerary> repository;

        public Repository<Itinerary> Repository
        {
            get => repository;
            set => repository = value;
        }
        #endregion

        public Command<string> DialogSelectСommand { get; private set; }

        public ItineraryDialogViewModel()
        {
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            Title = "Окно " +
                Actions.GetGenetiveName(MainWindowAction) +
                " сотрудника";

            CopyParameters = new CopyParameters();
            DialogItem = new Itinerary();
            CurrentViewModel = this;
        }

        public void OnDialogSelectСommand(string destination) {

            SelectWindowViewModel sw = new SelectWindowViewModel();
            switch (destination)
            {
                case Destinations._employee:
                    sw.CurrentViewModel = viewModels.employee;
                    sw.ShowWindow();
                    //((Itinerary)DialogItem).viewModels.employee.SelectedItem;
                    break;
                case Destinations._routes:
                    // TODO: Открытие окна со списком соответствующем destination
                    break;
                case Destinations._transport:
                    // TODO: Открытие окна со списком соответствующем destination
                    break;
            }

            
        }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        /// <remarks> 
        /// Примечание: Для вызова данного метода, кнопка диалогового окна 
        /// должна быть привязанна к команде DialogActionCommand.
        /// </remarks>
        /// <param name="dialogAction">                                         
        /// Действие которое следует выполнить, для вызывающей кнопки.
        /// Значение определяется нажатой кнопкой на диалоговом окне, 
        /// через CommandParameter в xaml файле.
        /// </param>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполнение метода базового класса
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                DoActionForObservableCollection();
            }
        }

        /// <summary>
        /// Выполняет изменнение observableCollection,
        /// на основе заданного действия.                     
        /// </summary>
        private void DoActionForObservableCollection()
        {
            List<Itinerary> List;
            switch (MainWindowAction)
            {
                case Actions.add:
                    List = GetAdded();
                    break;
                case Actions.update:
                    UpdateObservableCollection();
                    return; // выход из функции
                case Actions.copy:
                    List = GetCopied();
                    break;
                default: return;
            }

            foreach (var itinerary in List)
                observableCollection.Add(itinerary);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        /// <returns> 
        /// Сотрудник, ключ которого совпадает с заданным. 
        /// </returns>
        private List<Itinerary> GetAdded()
        {
            return new List<Itinerary>
            {
                repository.GetSingle(DialogItem.Id)
            };
        }

        /// <summary>
        /// Выполняет обновление записи в observableCollection
        /// и обновляет представление, используещее данную коллекцию.
        /// </summary>
        private void UpdateObservableCollection()
        {
            ObservableCollection<Itinerary> ob = observableCollection;

            ob[SelectedIndex].SetFields((Itinerary)DialogItem);
            CollectionViewSource.GetDefaultView(ob).Refresh();
        }

        /// <summary>
        /// Возвращает записи из таблицы,
        /// значение ключа которых больше заданного параметра. 
        /// </summary>
        private List<Itinerary> GetCopied()
        {
            return repository.GetFrom(DialogItem.Id);
        }
        #endregion
    }
}
