﻿using Application.DTOs.Currency;
using FluentValidation;
using SharedKernel.Constants;
using SharedKernel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Currency
{
    public class CurrencyCreateValidator : AbstractValidator<CurrencyCreateDto>
    {
        public CurrencyCreateValidator() {
             RuleFor(x => x.BusinessId)
                .GreaterThan(0).WithMessage("El negocio es obligatorio.")
                .WithErrorCode(ErrorCodes.ValidationCharacterNegative);

            RuleFor(x => x.CodeSunat)
                .NotEmpty().WithMessage("El código SUNAT es obligatorio.")
                .WithErrorCode(ErrorCodes.ValidationEmpty)

                .MaximumLength(4)
                .WithMessage("El código de SUNAT no debe exceder de los 4 caracteres.")
                .WithErrorCode(ErrorCodes.ValidationLength)

                .Matches(@"^[a-zA-Z0-9\s\-,.áéíóúÁÉÍÓÚñÑ()]+$")
                .WithMessage("El codigo sunat tiene caracteres especiales.")
                .WithErrorCode(ErrorCodes.ValidationCharacterInvalid)

                .Must(InputValidationHelpers.IsSafe)
                .WithMessage("El código SUNAT contiene caracteres peligrosos.")
                .WithErrorCode(ErrorCodes.ValidationIllegalChar);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("El nombre de la moneda  es obligatoria.")
                .WithErrorCode(ErrorCodes.ValidationEmpty)

                .MaximumLength(150)
                .WithMessage("La descripción no debe exceder los 150 caracteres.")
                .WithErrorCode(ErrorCodes.ValidationLength)

                 .Matches(@"^[a-zA-Z0-9\s\-,.áéíóúÁÉÍÓÚñÑ()]+$")
                 .WithMessage("La descripción tiene caracteres especiales.")
                 .WithErrorCode(ErrorCodes.ValidationCharacterInvalid)

                .Must(InputValidationHelpers.IsSafe)
                .WithMessage("La descripción contiene caracteres peligrosos.")
                .WithErrorCode(ErrorCodes.ValidationIllegalChar);

            RuleFor(x => x.symbol)
                .NotEmpty().WithMessage("El simbolo de la moneda  es obligatoria.")
                .WithErrorCode(ErrorCodes.ValidationEmpty)

                .MaximumLength(5)
                .WithMessage("El simbolo no debe exceder los 5 caracteres.")
                .WithErrorCode(ErrorCodes.ValidationLength)

                .Must(InputValidationHelpers.IsSafe)
                .WithMessage("La descripción contiene caracteres peligrosos.")
                .WithErrorCode(ErrorCodes.ValidationIllegalChar);

        }
    }
}
