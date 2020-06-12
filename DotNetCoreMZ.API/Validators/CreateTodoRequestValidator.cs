using DotNetCoreMZ.API.Contracts.V1.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Validators
{
    public class CreateTodoRequestValidator :  AbstractValidator<CreateTodoRequest>
    {
        public CreateTodoRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

        }
    }
}
