
using PTC_Management.EntityFramework;
using System;
using System.Data;
using PTC_Management.Commands;
using PTC_Management.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.ComponentModel;

namespace PTC_Management.ViewModel
{
    class DateAdapter
    {
        public DateTime Date { get; set; }

        public int Id { get; set; }

        public string Status { get; set; }

        public int IdEmployee { get; set; }

        public virtual Employee Employee { get; set; }

    }

    class ScheduleDataColumns
    {
        public DataColumn employee = new DataColumn();
        public DataColumn[] days = new DataColumn[31];

        public ScheduleDataColumns()
        {
            employee.ColumnName = "Сотрудник";

            CreateDayColumns();
        }

        void CreateDayColumns()
        {
            for (int i = 1, length = 31; i <= length; i++)
            {
                days[i - 1] = new DataColumn
                {
                    ColumnName = i.ToString()
                };
            }
        }
    }

    internal class ScheduleOfEmployeeViewModel : ViewModelBaseEntity
    {
        private string datePickerValue;
        private Status status;

        public string DatePickerValue
        {
            get => datePickerValue;
            set
            {
                if(datePickerValue != value && value.Length == 7)
                    SetProperty(ref datePickerValue, value);
            }
        }

        public DataTable ScheduleTable { get; set; }

        public ScheduleDataColumns Columns { get; set; }

        public Status Status { get => status ?? (status = new Status()); }

        public Command SelectEmployees { get; private set; }

        public Command RemoveEmployee { get; private set; }

        public Command<string> SetStatus { get; private set; }

        public ScheduleOfEmployeeViewModel()
        {
            SelectEmployees = new Command(OnSelectEmployees);
            RemoveEmployee = new Command(OnRemoveEmployee);
            SetStatus = new Command<string>(OnSetStatus);

            PropertyChanged += new PropertyChangedEventHandler(DatePickerValue_PropertyChanged);

            DatePickerValue = DateTime.Now.ToString("MM.yyyy");



        }

        private void DatePickerValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(DatePickerValue))
            {

                Console.WriteLine(DatePickerValue);

                ScheduleTable = new DataTable();
                Columns = new ScheduleDataColumns();

                ScheduleTable.Columns.Add(Columns.employee);
                ScheduleTable.Columns.AddRange(Columns.days);

                RaisePropertyChanged(nameof(ScheduleTable));

                // todo проверка существующих записей в базе данных
            }
        }

        private void OnSetStatus(string status)
        {
            throw new NotImplementedException();
        }

        private void OnRemoveEmployee()
        {
            throw new NotImplementedException();
        }

        private void OnSelectEmployees()
        {
            var selectWindow = new SelectWindowViewModel(Destinations.employee);
            selectWindow.Show();

            if (selectWindow.ReturnedItem != null) // если были выбраны элементы списка
            {
                if (selectWindow.ReturnedItems == null)
                    selectWindow.ReturnedItems = new List<Employee> 
                    { 
                        selectWindow.SelectedItem as Employee
                    };

                foreach (var item in selectWindow.ReturnedItems) {
                    DataRow row = ScheduleTable.NewRow();
                    if (item is Employee employee)
                        row[Columns.employee] = employee.Name;
                    ScheduleTable.Rows.Add(row);
                }
            }
        }

    }
}
