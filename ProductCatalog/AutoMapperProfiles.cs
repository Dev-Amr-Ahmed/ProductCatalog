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
                .ForMember(d => d.Duration, o => o.MapFrom(s => s.Duration.Ticks))
                .ForMember(d=> d.EndDate, o=>o.MapFrom(s=>s.StartDate.Add(s.Duration)));

            CreateMap<Product, ProductVM>()
                .ForMember(d => d.Duration, o => o.MapFrom(s => TimeSpan.FromTicks(s.Duration)));
        }
    }
}
