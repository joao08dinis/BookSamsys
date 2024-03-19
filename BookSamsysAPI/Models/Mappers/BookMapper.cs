using AutoMapper;
using BookSamsysAPI.Models.Doman;
using BookSamsysAPI.Models.DTO;

namespace BookSamsysAPI.Models.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.iSBN, opt => opt.MapFrom(src => src.iSBN))
                .ForMember(dest => dest.author, opt => opt.MapFrom(src => src.author))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.price));

        }
    }
}
