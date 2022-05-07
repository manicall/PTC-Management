using PTC_Management.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.ViewModel.DialogViewModels
{
    internal class TransportDialogViewModel : DialogViewModel
    {
        public TransportDialogViewModel(string action)
        {
            Title = $"Окно {Actions.GetGenetiveName(action)} транспорта";
            CurrentViewModel = this;
            DialogItem = new Employee();
            CurrentAction = action;
        }

        public TransportDialogViewModel(Transport employee, string action) : this(action)
        {
            DialogItem = employee;
        }
    }
}
