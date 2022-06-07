using PTC_Management.Commands;
using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.Views.Windows;

namespace PTC_Management.ViewModel
{
    internal class SelectWindowViewModel : ViewModelBaseWindow
    {
        private Entity returnedItem;
        public Entity ReturnedItem
        {
            get => returnedItem;
            set => SetProperty(ref returnedItem, value);
        }

        public Command DialogSelectCommand { get; private set; }

        public SelectWindowViewModel()
        {
            DialogSelectCommand = new Command(OnDialogSelectCommand);
        }

        public SelectWindowViewModel(string destination) : this()
        {
            CurrentViewModel = GetViewModel(viewModels.GetViewModel(destination));
            Title = ViewModels.GetTitle("Выбор", destination);
        }


        public SelectWindowViewModel(string destination, int idTransport) : this()
        {
            switch (destination)
            {
                case Destinations.itinerary:
                    CurrentViewModel = GetViewModel(viewModels.GetItineraryVM(idTransport));
                    Title = ViewModels.GetTitle("Выбор", destination);
                    break;
                case Destinations.maintanceLog:
                    CurrentViewModel = GetViewModel(viewModels.GetMaintanceLogVM(idTransport));
                    Title = ViewModels.GetTitle("Выбор", destination);
                    break;
                case Destinations.logOfDepartureAndEntry:
                    CurrentViewModel = GetViewModel(viewModels.GetLogOfDepartureAndEntryVM(idTransport));
                    Title = ViewModels.GetTitle("Выбор", destination);
                    break;
            }


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
                        OnDialogSelectCommand();
                });

            // отключение видимости кнопок с журналом ТО и
            // журналом въезда и выезда у окна со списком транспорта
            if (viewModel is TransportViewModel transportVM)
                transportVM.TansportInfoButtonsVisibility = Visibility.collapsed;

            return viewModel;
        }


        public void OnDialogSelectCommand()
        {
            returnedItem = CurrentViewModel.SelectedItem;
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

