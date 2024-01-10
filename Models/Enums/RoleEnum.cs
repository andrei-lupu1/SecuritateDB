﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum RoleEnum
    {
        [Description("ADMIN")]
        ADMIN = 1,
        [Description("COURIER")]
        COURIER = 2,
        [Description("CLIENT")]
        CLIENT = 3
    }
}
