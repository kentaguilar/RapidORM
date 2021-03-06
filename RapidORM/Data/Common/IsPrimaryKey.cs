﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidORM.Data.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IsPrimaryKey : Attribute
    {
        public bool IsIdentity { get; set; }

        public IsPrimaryKey() { }

        public IsPrimaryKey(bool primaryKey)
        {
            this.IsIdentity = primaryKey;
        }
    }
}
