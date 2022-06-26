
using PTC_Management.Commands;
using PTC_Management.EntityFramework;
using PTC_Management.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Dynamic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PTC_Management.ViewModel
{
    public class ScheduleDataColumns
    {
        public DataColumn Employee { get; set; } 

        public DataColumn[] Days { get; set; }

        public ScheduleDataColumns(string datePickerValue)
        {
            Employee = new DataColumn
            {
                ColumnName = "Сотрудник"
            };

            CreateDayColumns(datePickerValue);
        }

        private void CreateDayColumns(string datePickerValue)
        {
            DateTime.TryParse(datePickerValue, out DateTime dateTime);

            Days = new DataColumn[DateTime.DaysInMonth(dateTime.Year, dateTime.Month)];
            for (int i = 1; i <= Days.Length; i++, dateTime = dateTime.AddDays(1))
            {
                Days[i - 1] = new DataColumn
                {
                    ColumnName = i.ToString() + "\n" + dateTime.ToString("ddd")
                };
            }
        }
    }

    public class ScheduleOfEmployeeViewModel : ViewModelBaseEntity
    {
        public IList<DataGridCellInfo> SelectedCells { get; set; }
        public new ItemCollection Items { get; set; }

        private string datePickerValue;
        private Status status;

        private List<List<Date>> datesList;

        public string DatePickerValue
        {
            get => datePickerValue;
            set
            {
                if (datePickerValue != value && value.Length == 7)
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
                datesList = new List<List<Date>>();
                ScheduleTable = new DataTable();
                Columns = new ScheduleDataColumns(DatePickerValue);
                
                ScheduleTable.Columns.Add(Columns.Employee);
                ScheduleTable.Columns.AddRange(Columns.Days);

                DateTime.TryParse(datePickerValue, out DateTime dateTime);
                var groupedDates = Date.repository.GetDates(dateTime);

                foreach (var item in groupedDates)
                {
                    DataRow row = ScheduleTable.NewRow();
                    var dates = item.ToList();

                    row[0] = dates[0].Employee.GetFullName();
                    for (int i = 1; i < dates.Count + 1; i++)
                    {
                        row[i] = dates[i - 1].Status;
                    }

                    datesList.Add(dates);
                    ScheduleTable.Rows.Add(row);
                }

                RaisePropertyChanged(nameof(ScheduleTable));      
            }
        }

        private void OnSetStatus(string status)
        {
            if (SelectedCells == null) {
                WindowParameters.StatusBarMessage = "Дни не выбраны";
                return;
            }

            foreach (DataGridCellInfo cellInfo in SelectedCells)
            {
                if (cellInfo.Column.Header.ToString() == Columns.Employee.ColumnName) continue;
                if (cellInfo.IsValid)
                {
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item);

                    var rowIndex = Items.IndexOf(cellInfo.Item);
                    var columnIndex = cellInfo.Column.DisplayIndex;

                    if (content is TextBlock textBlock)
                    {
                        if (textBlock.Background == null)
                        {
                            textBlock.Margin = new Thickness(-1, -1, -1, -1);
                            textBlock.TextAlignment = TextAlignment.Center;
                        }

                        textBlock.Background = new SolidColorBrush(GetColor(status));
                    }

                    var row = (DataRowView)content.DataContext;
                    row[cellInfo.Column.Header.ToString()] = status;


                    // изменение базы данных
                    datesList[rowIndex][columnIndex - 1].Status = status;
                    datesList[rowIndex][columnIndex - 1].Update();
                }
            }
        }

        Color GetColor(string status)
        {
            switch (status)
            {
                case Status.working:
                    return (Color)ColorConverter.ConvertFromString("LimeGreen");
                case Status.noWorking:
                    return (Color)ColorConverter.ConvertFromString("DeepPink");
                case Status.free:
                    return (Color)ColorConverter.ConvertFromString("DarkGray");
                case Status.vacation:
                    return (Color)ColorConverter.ConvertFromString("DeepSkyBlue");

                default:
                    return new Color();
            }
        }

        private void OnRemoveEmployee()
        {
            List<DataRowView> rows = new List<DataRowView>();
            List<int> rowIndexes = new List<int>();
            foreach (DataGridCellInfo cellInfo in SelectedCells)
            {
                if (cellInfo.Column.Header.ToString() != Columns.Employee.ColumnName) continue;
                if (cellInfo.IsValid)
                {
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                    rows.Add((DataRowView)content.DataContext);
                    rowIndexes.Add(Items.IndexOf(cellInfo.Item));
                }
            }

            if (Date.RemoveRange(datesList, rowIndexes))
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    ScheduleTable.Rows.Remove(rows[i].Row);
                    datesList.RemoveAt(rowIndexes[i]);
                }
            }


            // todo изменение базы данных
        }

        private void OnSelectEmployees()
        {

            // todo изменять цвет добавленных записей


            var selectWindow = new SelectWindowViewModel(datesList, this);
            if (selectWindow.CanShow) selectWindow.Show();

            if (selectWindow.ReturnedItem != null) // если были выбраны элементы списка
            {
                if (selectWindow.ReturnedItems == null)
                {
                    selectWindow.ReturnedItems = new List<Employee>();
                    selectWindow.ReturnedItems.Add(selectWindow.ReturnedItem);
                }

                foreach (var item in selectWindow.ReturnedItems)
                {
                    FillDataTable(item);
                    var dates = new List<Date>();
                    foreach (var day in Columns.Days)
                    {
                        var date = new Date();

                        if (item is Employee employee)
                        {
                            date.Employee = employee;
                            date.IdEmployee = employee.Id;
                        }

                        DateTime.TryParse(day.ToString() + DatePickerValue, out DateTime dateTime);
                        date.Date1 = dateTime;
                        date.Status = Status.Free;

                        date.Add();
                        dates.Add(date);
                    }
                    datesList.Add(dates);
                }
            }

            // todo изменение базы данных
        }

        void FillDataTable(object item) 
        {
            DataRow row = ScheduleTable.NewRow();
            if (item is Employee employee) row[Columns.Employee] = employee.GetFullName();
            foreach (var day in Columns.Days)
            {
                row[day] = Status.Free;
            }
            ScheduleTable.Rows.Add(row);
        }

    }
}
