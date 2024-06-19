using FluentValidation;

namespace ServiceCenter.Application.Requests.Comands.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        
    }
}