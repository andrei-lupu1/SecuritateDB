﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformationObjects.Payloads
{
    public class RegisterPayload
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

    }
}
