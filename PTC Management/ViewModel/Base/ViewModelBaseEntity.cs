using PTC_Management.Commands;
using PTC_Management.Model.MainWindow;

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Size = PTC_Management.Model.MainWindow.Size;

namespace PTC_Management.ViewModel.Base
{
    class ViewModelBaseEntity : ViewModelBase
    {
        private int selectedIndex;

        public static readonly DependencyProperty filterTextProperty =
            DependencyProperty.Register(
                "FilterText", typeof(string), typeof(ViewModelBaseEntity),
                new PropertyMetadata("", FilterText_Changed));

        public ICollectionView Items { get; set; }

        /// <summary>
        /// Индекс выбранной записи в таблице
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        /// <summary>
        /// Вызывается когда нажата одна 
        /// из кнопок, выполняющая изменения записей в таблице
        /// </summary>
        public ICommand DialogCommand { get; set; }

        public ViewModelBaseEntity()
        {
            DialogCommand = new Command<string>(OnDialog);
        }

        /// <summary>
        /// Вызывается при вызове DialogCommand
        /// </summary>
        public virtual void OnDialog(string action) { }

        public string FilterText
        {
            get { return (string)GetValue(filterTextProperty); }
            set { SetValue(filterTextProperty, value); }
        }


        /// <summary>
        /// Событие вызываемое при изменение текста в поле фильтра
        /// </summary>
        private static void FilterText_Changed(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ViewModelBaseEntity current = d as ViewModelBaseEntity;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.Filter;
            }
        }

        /// <summary>
        /// Правило фильтрации
        /// </summary>
        protected virtual bool Filter(object entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Выполняет инициализацию диалогового окна и возвращает его экземпляр
        /// </summary>
        public DialogViewModel GetDialogViewModel<T>(string action, string destination) where T : DialogViewModel, new()
        {
            return new T()
            {
                MainWindowAction = action,
                Title = ViewModels.GetDialogTitle(action, destination),
                WindowParameters = WindowParameters,
            };
        }
    }
}
