using FluentValidation;
using System;
using System.ComponentModel;
using ViewModels;

namespace UI.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorVM>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MinimumLength(4).Matches(@"[A-Za-z]*").WithMessage("Only letters are allowed");
            RuleFor(a => a.Surname).NotEmpty().MinimumLength(4).Matches(@"[A-Za-z]*").WithMessage("Only letters are allowed");

            var type = typeof(AuthorVM);

            var attr = TypeDescriptor.GetProperties(typeof(AuthorVM))["CountOfBooks"].Attributes[typeof(Attribute)] as Attribute;
            attr.GetType().GetField("DisplayName").SetValue(attr, "Amount of books");
        }
    }
}