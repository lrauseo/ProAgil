using System.Linq;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
            .ForMember(dest => dest.Palestrantes, opt =>
            {
                opt.MapFrom(src => src.PalestranteEventos.Select(s => s.Palestrante).ToList());
            }).ReverseMap();
            
            CreateMap<Palestrante, PalestranteDto>()
            .ForMember(dest => dest.Eventos, opt =>
            {
                opt.MapFrom(src => src.PalestranteEventos.Select(s => s.Evento).ToList());
            }).ReverseMap();

            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
        }
    }
}