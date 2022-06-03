using System;
using System.ComponentModel;

namespace PTC_Management.Model
{
    /// <summary>
    /// Параметры копирования
    /// </summary>
    class CopyParameters : IDataErrorInfo
    {
        /// <summary>
        /// Количество копирований
        /// </summary>
        private int count;
        public int Count
        {
            get => count;
            set => count = value;
        }

        /// <summary>
        /// Отображается ли поле для ввода
        /// </summary>
        private string countVisibility;
        public string CountVisibility
        {
            get => countVisibility;
            set => countVisibility = value;
        }

        public CopyParameters(int count = 1, string countVisibility = Visibility.collapsed)
        {
            this.count = count;
            this.countVisibility = countVisibility;
        }

        /// <summary>
        /// Правило валидации текстового поля
        /// </summary>
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
