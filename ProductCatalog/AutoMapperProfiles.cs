using AutoMapper;
using ProductCatalog.DAL.Data.Models;
using ProductCatalog.PL.Models;

namespace ProductCatalog.PL
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductVM, Product>()
                .ReverseMap();
        }
    }
}
