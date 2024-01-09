﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public class DomainModelBase : IDomainModel
    {
        [Key]
        public int ID { get; set; }
    }
}