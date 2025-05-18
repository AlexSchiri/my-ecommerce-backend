using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using AutoMapper;

namespace ArticoliWebService.Profiles
{
    public class ArticoliProfile : Profile
    {
        public ArticoliProfile()
        {
            CreateMap<Articoli, ArticoliDto>()
            .ForMember
            (
                dest => dest.Categoria,
                opt => opt.MapFrom(src => $"{src.IdFamAss} {src.famAssort.Descrizione}")
            )
            .ForMember
            (
                dest => dest.CodStat,
                opt => opt.MapFrom(src => src.CodStat.Trim())
            )
            .ForMember
            (
                dest => dest.Um,
                opt => opt.MapFrom(src => src.Um.Trim())
            )
            .ForMember
            (
                dest => dest.IdStatoArt,
                opt => opt.MapFrom(src => src.IdStatoArt.Trim())
            )
            /*
            .ForMember
            (
                dest => dest.Iva,
                opt => opt.MapFrom(src => new IvaDto(src.iva.IdIva, src.iva.Descrizione, src.iva.Aliquota))
            )
            */
            .ForMember
            (
                dest => dest.PzCart,
                opt => opt.MapFrom(src => (src.PzCart == null) ? 0 : src.PzCart)
            );

            CreateMap<Iva, IvaDto>();
            CreateMap<FamAssort, CategoriaDto>();

            CreateMap<DettListini, PrezziDto>()
            .ForMember
            (
                dest => dest.Listino,
                opt => opt.MapFrom(src => src.IdList)
            );
            
        }
    }
}