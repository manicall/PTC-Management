using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTC_Management.EntityFramework
{
    [Table("Date")]
    public partial class Date : Entity
    {
        public override string this[string columnName] => throw new NotImplementedException();

        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Column("Date", TypeName = "date")]
        public DateTime Date1 { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get => status; set => SetProperty(ref status, value); }

        public int IdEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public override string Error => throw new NotImplementedException();


        public static readonly Repository<Date> repository =
            new Repository<Date>();
        private string status;

        public override bool Add() => repository.Add(this);

        public override Entity Clone()
        {
            throw new NotImplementedException();
        }

        public override bool Copy(Entity selectedItem, int count)
        {
            throw new NotImplementedException();
        }

        public override bool Remove() => repository.Remove(this);
        public static bool RemoveRange(List<List<Date>> datesList, List<int> rowIndexes) => repository.RemoveRange(datesList, rowIndexes);

        public override void SetFields(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update() => repository.Update(this);

        internal void SetEntities()
        {
            Employee = repository.GetSingle<Employee>(IdEmployee);
        }
    }
}
