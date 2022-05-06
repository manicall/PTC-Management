﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTC_Management.Model
{
    public class MyLiterals<T> where T : class, new() {

        public static string FilterText { get => $"Filter{new T().GetType().Name}Text" ; }
        public static string Items { get => $"{new T().GetType().Name}Items" ; }
    }
}