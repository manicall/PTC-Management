using PTC_Management.Commands;
using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.Views.Windows;

using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace PTC_Management.ViewModel
{
    internal class SelectWindowViewModel : ViewModelBaseWindow
    {
        public Entity ReturnedItem { get; set; }

        public IList ReturnedItems { get; set; }

        public Command SelectWindowCommand { get; private set; }

        public SelectWindowViewModel()
        {
            CanShow = true;
            SelectWindowCommand = new Command(OnSelectCommand);
        }

        public SelectWindowViewModel(string destination) : this()
        {
            CurrentViewModel = GetViewModel(viewModels.GetViewModel(destination));
            Title = ViewModels.GetTitle("Выбор", destination);

            CurrentViewModel.WindowParameters.WindowSize.HeightDiff = Size.selectHeightDiff;
        }

        public bool CanShow { get; set; }

        public SelectWindowViewModel(
            List<List<Date>> datesList,
            ScheduleOfEmployeeViewModel scheduleVM) : this(Destinations.employee)
        {
            var employees = (CurrentViewModel as EmployeeViewModel).ItemsList;

            foreach (var dates in datesList)
            {
                var employee = employees.Find(e => e.Id == dates[0].Employee.Id);
                employees.Remove(employee);
            }

            if (employees.Count == 0)
            {
                scheduleVM.WindowParameters.StatusBarMessage = "Невозможно добавить новых сотрудников";
                CanShow = false;
            }

            (CurrentViewModel as EmployeeViewModel).Items = CollectionViewSource.GetDefaultView(employees);
            (CurrentViewModel as EmployeeViewModel).Items.Refresh();
        }

        public SelectWindowViewModel(int idTransport, List<MaintanceLog> maintanceLogs) : this()
        {
            Title = ViewModels.GetTitle("Выбор", Destinations.itinerary);

            CurrentViewModel = GetViewModel(viewModels.GetItineraryVM(idTransport));
            var itineraries = (CurrentViewModel as ItineraryViewModel).ItemsList;

            foreach (var maintance in maintanceLogs)
            {
                var itinerary = itineraries.Find(i => i.Id == maintance.IdItinerary);
                itineraries.Remove(itinerary);
            }

            if (itineraries.Count == 0)
            {
                CanShow = false;
            }

            (CurrentViewModel as ItineraryViewModel).Items = CollectionViewSource.GetDefaultView(itineraries);
            (CurrentViewModel as ItineraryViewModel).Items.Refresh();

            CurrentViewModel.WindowParameters.WindowSize.HeightDiff = Size.transportInfoHeightDiff;
        }

        public SelectWindowViewModel(int idTransport, List<LogOfDepartureAndEntry> maintanceLogs) : this()
        {
            Title = ViewModels.GetTitle("Выбор", Destinations.itinerary);

            CurrentViewModel = GetViewModel(viewModels.GetItineraryVM(idTransport));
            var itineraries = (CurrentViewModel as ItineraryViewModel).ItemsList;

            foreach (var maintance in maintanceLogs)
            {
                var itinerary = itineraries.Find(i => i.Id == maintance.IdItinerary);
                itineraries.Remove(itinerary);
            }

            if (itineraries.Count == 0)
            {
                CanShow = false;
            }

            (CurrentViewModel as ItineraryViewModel).Items = CollectionViewSource.GetDefaultView(itineraries);
            (CurrentViewModel as ItineraryViewModel).Items.Refresh();

            CurrentViewModel.WindowParameters.WindowSize.HeightDiff = Size.transportInfoHeightDiff;
        }

        private ViewModelBaseEntity GetViewModel(ViewModelBaseEntity viewModel)
        {
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
                        OnSelectCommand();
                });

            viewModel.SelectWindowCommand = SelectWindowCommand;
            viewModel.Visibility.Field["SelectButtonVisibility"] = Visibility.visible;

            // отключение видимости кнопок с журналом ТО и
            // журналом въезда и выезда у окна со списком транспорта
            if (viewModel is TransportViewModel transportVM)
                transportVM.TansportInfoButtonsVisibility = Visibility.collapsed;

            return viewModel;
        }

        public void OnSelectCommand()
        {
            ReturnedItem = CurrentViewModel.SelectedItem;

            if (CurrentViewModel is EmployeeViewModel employeeVM)
                ReturnedItems = employeeVM.SelectedItemsList;

            Close();
        }

        public void Show()
        {
            window = new SelectWindow();
            window.DataContext = this;
            window.Closed += (sender, e) => Closed();
            window.ShowDialog();
        }
    }
}

