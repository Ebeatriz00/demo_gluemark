﻿using Application.DTOs.DocumentType;
using FluentValidation;
using SharedKernel.Constants;
using SharedKernel.Helpers;

namespace Application.Validators.DocumentType;

public class DocumentTypeUpdateValidator : AbstractValidator<DocumentTypeUpdateDto>
{
    public DocumentTypeUpdateValidator()
    {
        RuleFor(x => x.DocumentTypeId)
            .GreaterThan(0).WithMessage("El ID es obligatorio.")
            .WithErrorCode(ErrorCodes.Conflict);

        RuleFor(x => x.CodeSunat)
             .NotEmpty()
             .WithMessage("El código SUNAT es obligatorio.")
             .WithErrorCode(ErrorCodes.ValidationEmpty)

             .MaximumLength(1)
             .WithMessage("El código de SUNAT no debe exceder 1 caracter.")
             .WithErrorCode(ErrorCodes.ValidationLength)

             .Matches(@"^[a-zA-Z0-9\s\-,.áéíóúÁÉÍÓÚñÑ()]+$")
             .WithMessage("El codigo sunat tiene caracteres especiales.")
             .WithErrorCode(ErrorCodes.ValidationCharacterInvalid)

             .Must(InputValidationHelpers.IsSafe)
             .WithMessage("El código SUNAT contiene caracteres peligrosos.")
             .WithErrorCode(ErrorCodes.ValidationIllegalChar);

        RuleFor(x => x.Description)
             .NotEmpty()
             .WithMessage("La descripción es obligatoria.")
             .WithErrorCode(ErrorCodes.ValidationEmpty)

             .Matches(@"^[a-zA-Z0-9\s\-,.áéíóúÁÉÍÓÚñÑ()]+$")
             .WithMessage("La descripción tiene caracteres especiales.")
             .WithErrorCode(ErrorCodes.ValidationCharacterInvalid)

             .MaximumLength(50)
             .WithMessage("La descripción no debe exceder los 50 caracteres.")
             .WithErrorCode(ErrorCodes.ValidationLength)

             .Must(InputValidationHelpers.IsSafe)
             .WithMessage("La descripción contiene caracteres peligrosos.")
             .WithErrorCode(ErrorCodes.ValidationIllegalChar);


        RuleFor(x => x.UsersBy)
            .GreaterThan(0)
            .WithMessage("El campo UsersBy debe ser mayor que 0.")
            .WithErrorCode(ErrorCodes.ValidationCharacterNegative);
    }
}
