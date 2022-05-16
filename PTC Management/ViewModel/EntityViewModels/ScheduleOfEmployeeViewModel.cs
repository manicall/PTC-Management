using PTC_Management.ViewModel.Base;

using System.Data;

namespace PTC_Management.ViewModel
{

    internal class ScheduleOfEmployeeViewModel : BindableBase
    {
        private string dataPickerValue;
        public string DataPickerValue
        {
            get => dataPickerValue;
            set => SetProperty(ref dataPickerValue, value);
        }

        private DataTable sizeQuantityTable;

        public DataTable SizeQuantityTable
        {
            get => sizeQuantityTable;
            set => SetProperty(ref sizeQuantityTable, value);
        }

        public ScheduleOfEmployeeViewModel()
        {

            //DataColumn mColumn = new DataColumn();
            //mColumn.ColumnName = "M";
            //this.sizeQuantityTable.Columns.Add(mColumn);

            //DataRow row1 = this.sizeQuantityTable.NewRow();
            //row1[sizeQuantityColumn] = "Blue";
            //row1[sColumn] = "12";
            //row1[mColumn] = "15";
            //this.sizeQuantityTable.Rows.Add(row1);


            this.sizeQuantityTable = new DataTable();

            DataColumn sizeQuantityColumn = new DataColumn();
            sizeQuantityColumn.ColumnName = "Сотрудник";
            sizeQuantityTable.Columns.Add(sizeQuantityColumn);

            for (int i = 1, length = 31; i <= length; i++)
            {
                DataColumn sColumn = new DataColumn
                {
                    ColumnName = i.ToString(),
                };
                sizeQuantityTable.Columns.Add(sColumn);
            }








        }

    }


}
