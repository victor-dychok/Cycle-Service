using FluentValidation;

namespace ServiceCenter.Application.Requests.Comands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        
    }
}