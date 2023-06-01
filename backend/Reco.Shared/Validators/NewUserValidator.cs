using FluentValidation;
using Reco.Shared.Dtos.User;

namespace Reco.Shared.Validators
{
    public class NewUserValidator : AbstractValidator<NewUserDTO>
    {
        public NewUserValidator()
        {
            RuleFor(x => x.WorkspaceName).Length(2,20);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 32);
        }
    }
}
