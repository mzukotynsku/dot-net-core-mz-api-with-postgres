using System;

namespace DotNetCoreMZ.API.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
