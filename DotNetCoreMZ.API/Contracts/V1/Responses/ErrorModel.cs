﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Contracts.V1.Responses
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string  Message { get; set; }
    }
}
