using AutoMapper;
using PlantillaMicroservicio.Application.Features.Products.DTOs;
using PlantillaMicroservicio.Domain.Entities;

namespace PlantillaMicroservicio.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // De Entidad a DTO (para leer datos)
        CreateMap<Product, ProductDto>();

        // De DTO a Entidad (para crear/actualizar datos)
        CreateMap<CreateProductDto, Product>();
    }
}