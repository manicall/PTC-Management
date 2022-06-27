using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Route")]
    public partial class Route : Entity
    {
        public Route()
        {
            Itinerary = new HashSet<Itinerary>();
        }

        [Required]
        public int? Number { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal? Distant { get; set; }

        public virtual ICollection<Itinerary> Itinerary { get; set; }

    }
    public partial class Route : Entity
    {
        public static readonly Repository<Route> repository =
            new Repository<Route>();

        public override bool Add() => repository.Add(this);

        public override bool Update() => repository.Update(this);

        public override bool Remove() => repository.Remove(this);

        public override bool Copy(Entity selectedItem, int count) => repository.Copy(selectedItem, this, count);

        public override void SetFields(Entity entity)
        {
            if (entity is Route route)
            {
                Number = route.Number;
                Name = route.Name;
                Distant = route.Distant;
            }
        }

        public override Entity Clone() => Clone<Route>();



        // используетс€, чтобы пол€ не подсвечивались при открытии окна
        bool[] canExecute = new bool[3];

        public bool GetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                if (canExecute[i] != true) return false;
            }
            return true;
        }

        public void SetCanExecute()
        {
            for (int i = 0; i < canExecute.Length; i++)
            {
                canExecute[i] = true;
            }

            // вызов событи€, на которое среагирует проверка валидации
            RaisePropertyChanged(nameof(Number));
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(Distant));
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Number":
                        if (Number.HasValue)
                            canExecute[0] = true;

                        if (canExecute[0])
                            error = IntError(Number);
                        break;
                    case "Name":
                        if (!string.IsNullOrEmpty(Name))
                            canExecute[1] = true;

                        if (canExecute[1])
                            if (string.IsNullOrEmpty(Name))
                                error = "ѕоле не может быть пустым";
                        break;
                    case "Distant":
                        if (Distant.HasValue)
                            canExecute[2] = true;

                        if (canExecute[2])
                            error = DistantError(Distant);
                        break;

                }
                return error;
            }
        }

        public string IntError(int? number)
        {
            if (!number.HasValue)
                return "ѕоле не может быть пустым";
            if (number <= 0)
                return "Ќомер должен быть больше нул€";
            return null;
        }

        public string DistantError(decimal? distant)
        {
            if (!distant.HasValue)
                return "ѕоле не может быть пустым";
            if (distant <= 0)
                return "ƒистанци€ должна быть больше нул€";
            return null;
        }

        public override string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
