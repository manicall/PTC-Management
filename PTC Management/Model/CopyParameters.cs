
using System;
using System.ComponentModel;

namespace PTC_Management.Model
{
    class CopyParameters : IDataErrorInfo
    {
        private int count;
        public int Count
        {
            get => count;
            set => count = value;
        }

        private string countVisibility;
        public string CountVisibility
        {
            get => countVisibility;
            set => countVisibility = value;
        }

        public CopyParameters(int count = 1,
            string countVisibility = "Collapsed")
        {
            this.count = count;
            this.countVisibility = countVisibility;
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Count":
                        if (Count <= 0)
                        {
                            error = "Число копирований должно быть больше нуля";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
}
