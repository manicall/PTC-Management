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

        private int _copyCount;
        public int CopyCount
        {
            get => _copyCount;
            set => _copyCount = value;
        }

        private string _copyCountVisibility;
        public string CopyCountVisibility
        {
            get => _copyCountVisibility;
            set => _copyCountVisibility = value;
        }

        #region DialogItem
        public Entity DialogItem
        {
            get { return (Entity)GetValue(DialogItemProperty); }
            set { SetValue(DialogItemProperty, value); }
        }


        public static readonly DependencyProperty DialogItemProperty =
            DependencyProperty.Register("DialogItem", typeof(Entity), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        #region CloseCommandDependencyProperty
        public ICommand DialogActionCommand
        {
            get { return (ICommand)GetValue(DialogActionCommandProperty); }
            set { SetValue(DialogActionCommandProperty, value); }
        }
    
        public static readonly DependencyProperty DialogActionCommandProperty =
            DependencyProperty.Register("DialogActionCommand", typeof(ICommand), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        public DialogViewModel()
        {
            DialogActionCommand = new ParameterizedCommand<string>(OnDialogActionCommand);
            _currentViewModel = null;
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


        #region Repository
        public Repository<Entity> Repository
        {
            get { return (Repository<Entity>)GetValue(RepositoryProperty); }
            set { SetValue(RepositoryProperty, value); }
        }


        public static readonly DependencyProperty RepositoryProperty =
            DependencyProperty.Register("Repository", typeof(Repository<Entity>), typeof(DialogViewModel), new PropertyMetadata(null));
        #endregion

        private void OnDialogActionCommand(string action)
        {
            switch (action) {
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

            Items = CollectionViewSource.GetDefaultView(Repository.GetAll());
        }

        private void DoAction(string action) {
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
