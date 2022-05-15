using PTC_Management.Commands;
using PTC_Management.EF;
using PTC_Management.Model;
using PTC_Management.Model.Dialog;
using PTC_Management.ViewModel.Base;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBaseDialog
    {

        private CopyParameters copyParameters;
        public CopyParameters CopyParameters
        {
            get => copyParameters;
            set => SetProperty(ref copyParameters, value);
        }

        private ViewModelBaseDialog currentViewModel;
        public ViewModelBaseDialog CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }


        private Entity dialogItem;
        public Entity DialogItem
        {
            get => dialogItem;
            set => SetProperty(ref dialogItem, value);
        }

        private Entity selectedItem;
        public Entity SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public int SelectedIndex { get; set; }
        public Command<string> DialogActionCommand { get; private set; }

        public DialogViewModel()
        {
            DialogActionCommand = new Command<string>(OnDialogActionCommand);
            currentViewModel = null;
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
            Entity entity = DialogItem.Clone();
            switch (action)
            {
                case Actions.add:
                    entity.Add();
                    break;
                case Actions.update:
                    // чтобы содержимое таблицы не менялось
                    // одновременно с содержимым диалогового окна
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
    }
}
