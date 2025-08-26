using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PlantillaMicroservicio.Application.Features.Products.DTOs;

namespace PlantillaMicroservicio.Application.Features.Products.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The product name cannot be empty.")
            .MaximumLength(100).WithMessage("The name cannot exceed 100 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("The description cannot exceed 500 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("The price must be greater than zero.");
    }
}