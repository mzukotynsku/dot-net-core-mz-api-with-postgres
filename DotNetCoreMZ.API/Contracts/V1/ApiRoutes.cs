﻿namespace DotNetCoreMZ.API.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Todos
        {
            public const string GetAll = Base + "/todos";
            public const string Create = Base + "/todos";
            public const string GetById = Base + "/todos/{todoId}";
            public const string Update = Base + "/todos/{todoId}";
            public const string Delete = Base + "/todos/{todoId}";

        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";
           
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}