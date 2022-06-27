﻿using PTC_Management.EntityFramework;
using PTC_Management.Model;
using PTC_Management.ViewModel.Helpers;

using System.Collections.Generic;

namespace PTC_Management.ViewModel
{
    public class ItineraryViewModel : ViewModelBaseEntity
    {
        ViewModelHelper<Itinerary> viewModelHelper;

        public List<Itinerary> ItemsList { get; set; }


        public ItineraryViewModel()
        {
            Itinerary itinerary = new Itinerary();

            viewModelHelper = new ViewModelHelper<Itinerary>(Itinerary.repository);

            ItemsList = viewModelHelper.ItemsList;

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        public ItineraryViewModel(int id)
        {
            Itinerary itinerary = new Itinerary();

            viewModelHelper =
                new ViewModelHelper<Itinerary>(
                    Itinerary.repository,

                    Destinations.itinerary,
                    id);

            ItemsList = viewModelHelper.ItemsList;

            Items = viewModelHelper.GetItems();
            Items.Filter = Filter;
        }

        #region FilterText

        /// <summary>
        /// Проверка подходит ли заданный текст под условие фильтра.
        /// </summary>
        protected override bool Filter(object entity)
        {
            Itinerary current = entity as Itinerary;

            // Если в поле фильтра было введено ФИО
            if (FilterText.Contains(" "))
            {
                return CheckFIO(current);
            }

            return string.IsNullOrWhiteSpace(FilterText)
                 || current.Id.ToString().Contains(FilterText)
                 || current.Date.ToString().Contains(FilterText)
                 || current.Employee.DriverLicense.Contains(FilterText)
                 || current.Employee.Surname.Contains(FilterText)
                 || current.Employee.Name.Contains(FilterText)
                 || current.Employee.Patronymic != null &&
                     current.Employee.Patronymic.Contains(FilterText)
                 || current.Transport.Name.Contains(FilterText)
                 || current.Transport.LicensePlate.Contains(FilterText)
                 || current.Route.Name.Contains(FilterText);
        }
        #endregion

        bool CheckFIO(Itinerary current)
        {
            string[] split = FilterText.Split();

            if (split.Length > 3 || current.Employee.Patronymic == null && split.Length > 2) return false;

            string[] FIO = new string[] {
                    current.Employee.Surname,
                    current.Employee.Name,
                    current.Employee.Patronymic
            };

            bool result = true;
            for (int i = 0; i < split.Length; i++)
            {
                if (split[i] != "" && !FIO[i].Contains(split[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }



        #region Методы

        /// <summary>
        /// Выполняет заданное действие для вызывающей кнопки.
        /// </summary>
        public override void OnTableAction(string action)
        {
            // инициализация модели представления диалогового окна
            var dialogViewModel = GetDialogViewModel<ItineraryDialogViewModel>(action, Destinations.itinerary);
            dialogViewModel.ViewModelHelper = viewModelHelper;

            dialogViewModel.SelectedIndex = SelectedIndex;
            dialogViewModel.SelectedItem = SelectedItem;

            dialogViewModel.ChangeIsReadOnly();

            SelectedItem = dialogViewModel.SelectedItem;

            var actionPerformer = new ActionPerformer<Itinerary>
                 (this, dialogViewModel, viewModelHelper.ItemsList);

            actionPerformer.DoAction(action);
        }


        #endregion
    }
}
