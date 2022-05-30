﻿using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.Model.MainWindow;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace PTC_Management.ViewModel.DialogViewModels
{
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

        private readonly Destinations _destinations = new Destinations();
        public Destinations Destinations => _destinations;

        public Command<string> DialogSelectСommand { get; private set; }

        public ItineraryDialogViewModel()
        {
            DialogSelectСommand = new Command<string>(OnDialogSelectСommand);

            CopyParameters = new CopyParameters();
            DialogItem = new Itinerary();
            CurrentViewModel = this;
        }

        /*TODO: связять с ItineraryView*/
        public void OnDialogSelectСommand(string destination)
        {
            var selectWindow = new SelectWindowViewModel();
            selectWindow.CurrentViewModel = viewModels.GetViewModel(destination);

            selectWindow.Show();

            if (selectWindow.ReturnedItem != null)
            {
                switch (destination)
                {
                    case Destinations.employee:
                        ((Itinerary)DialogItem).Employee =
                            (Employee)selectWindow.ReturnedItem;
                        break;
                    case Destinations.route:
                        ((Itinerary)DialogItem).Route =
                            (Route)selectWindow.ReturnedItem;
                        break;
                    case Destinations.transport:
                        ((Itinerary)DialogItem).Transport =
                            (Transport)selectWindow.ReturnedItem;
                        break;

                    default: break;
                }
            }

        }

        #region методы
        /// <summary>
        /// Вызывается при нажатии на кнопку на диалоговом окне.
        /// </summary>
        protected override void OnDialogActionCommand(string dialogAction)
        {
            // выполняет изменения в бд
            base.OnDialogActionCommand(dialogAction);

            if (dialogAction != Actions.close)
            {
                // выполняет изменения в коллекции
                // отображающей записи в таблице
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
                    List = repository.GetFrom(DialogItem.Id);
                    break;
                default: return;
            }

            foreach (var employee in List)
                observableCollection.Add(employee);
        }

        /// <summary>
        /// Выполняет поиск записи в базе данных по ключу. 
        /// </summary>        
        private List<Itinerary> GetAdded() => new List<Itinerary>
        {
            repository.GetSingle(DialogItem.Id)
        };

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

        #endregion
    }
}