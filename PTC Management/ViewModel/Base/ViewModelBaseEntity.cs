using PTC_Management.Commands;
using PTC_Management.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Visibility = PTC_Management.Model.Visibility;

namespace PTC_Management.ViewModel
{
    class ViewModelBaseEntity : ViewModelBase
    {
        private Visibility visibility;
        private int selectedIndex;
        private string copyButtonVisibility;
        private string tableActionButtonsVisible;

        public Visibility Visibility
        {
            get
            {
                if (visibility == null)
                {
                    Visibility = new Visibility()
                    {
                        Field = new Dictionary<string, string>()
                        {
                            ["SelectButtonVisibility"] = Visibility.collapsed,
                        }
                    };
                }
                return visibility;
            }
            set => visibility = value;
        }


        /// <summary>
        /// Поле, изменяемое свойством FilterText
        /// </summary>
        public static readonly DependencyProperty filterText =
            DependencyProperty.Register(
                "FilterText", typeof(string), typeof(ViewModelBaseEntity),
                new PropertyMetadata("", FilterText_Changed));

        /// <summary>
        /// Выполняет действие по выбору записи из таблицы
        /// </summary>
        public ICommand SelectWindowCommand { get; set; }

        /// <summary>
        /// Представление элементов. 
        /// Используется для предоставления 
        /// возможности фильтрации элементов в таблице.
        /// </summary>
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
        public ICommand TableAction { get; set; }

        /// <summary>
        /// Определяет видимость кнопок, изменяющих записи таблицы
        /// </summary>
        public string TableActionButtonsVisible
        {
            get => tableActionButtonsVisible ?? Visibility.visible;
            set => tableActionButtonsVisible = value;
        }

        public string CopyButtonVisibility
        {
            get => copyButtonVisibility ?? Visibility.visible;
            set => copyButtonVisibility = value;
        }

        public ViewModelBaseEntity()
        {
            TableAction = new Command<string>(OnTableAction);
        }

        /// <summary>
        /// Вызывается при вызове TableAction
        /// </summary>
        public virtual void OnTableAction(string action) { }

        /// <summary>
        /// Свойство устанавливаемое в текстовое поле фильтра
        /// </summary>
        public string FilterText
        {
            get { return (string)GetValue(filterText); }
            set { SetValue(filterText, value); }
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
        public T GetDialogViewModel<T>(string action, string destination) where T : DialogViewModel, new()
        {
            return new T()
            {
                MainWindowAction = action,
                Title = ViewModels.GetTitle(Actions.GetGenetiveName(action), destination),
                WindowParameters = WindowParameters,
            };
        }

        /// <summary>
        /// Устанавливает текст строки состояния
        /// </summary>
        /// <param name="message"></param>
        public void SetStatusBarMessage(string message)
        {
            WindowParameters.StatusBarMessage = message;
        }
    }
}
