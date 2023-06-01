using FluentValidation;
using Reco.Shared.Dtos.User;

namespace Reco.Shared.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).Length(8, 32);
        }
    }
}
