using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreMZ.API.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
