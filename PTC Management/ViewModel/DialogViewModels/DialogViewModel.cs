using PTC_Management.EF;
using PTC_Management.Model.Dialog;
using PTC_Management.SupportClass;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PTC_Management.ViewModel
{
    class DialogViewModel : ViewModelBase {

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }


        #region CopyCount
        public int CopyCount
        {
            get { return (int)GetValue(copyCount); }
            set { SetValue(copyCount, value); }
        }

        public static readonly DependencyProperty copyCount =
            DependencyProperty.Register("CopyCount", typeof(int), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        #region CopyCountVisibility
        private string _copyCountVisibility;
        public string CopyCountVisibility
        {
            get => _copyCountVisibility;
            set => _copyCountVisibility = value;
        }
        #endregion

        #region DialogItem
        public Entity DialogItem
        {
            get { return (Entity)GetValue(dialogItem); }
            set { SetValue(dialogItem, value); }
        }
        
        public static readonly DependencyProperty dialogItem =
            DependencyProperty.Register("DialogItem", typeof(Entity), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion


        public ParameterizedCommand<string> DialogActionCommand { get; private set; }

        public DialogViewModel()
        {
            DialogActionCommand = new ParameterizedCommand<string>(OnDialogActionCommand);
            _currentViewModel = null;
        }

        protected virtual void OnDialogActionCommand(string action) {
            switch (action)
            {
                case Actions._writeAndClose:
                    DoAction(CurrentAction);
                    Close();
                    break;
                case Actions._write:
                    DoAction(CurrentAction);
                    break;
                case Actions._close:
                    Close();
                    break;
            }
        }

        #region Items
        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(_itemsProperty); }
            set { SetValue(_itemsProperty, value); }
        }

        public static readonly DependencyProperty _itemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(DialogViewModel), new PropertyMetadata(null));

        #endregion

        protected void DoAction(string action) {
            switch (action)
            {
                case Actions._add:
                    DialogItem.Add();
                    break;
                case Actions._update:
                    DialogItem.Update();
                    break;
                case Actions._copy:
                    DialogItem.Copy(CopyCount);
                    break;
                default: 
                    Console.WriteLine("Неизвестное действие"); 
                    break;

            }
        }


    }
}
