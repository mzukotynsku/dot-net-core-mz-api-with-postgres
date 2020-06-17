﻿using System.Collections;
using System.Collections.Generic;

namespace DotNetCoreMZ.API.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get;  set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }

    }
}
