﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.EF
{
    public class Entity
    {
        public int Id { get; set; }

        public virtual void Add() { }
        public virtual void Update() { }
        public virtual void Remove() { }
        public virtual void Copy(int count) { }

        public virtual void SetFields(Entity entity) { }
    }
}
