using AutoMapper;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;

namespace RoutesService.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RoutesService.Domain.Entities.Kullanici, RoutesService.API.DTOs.KullaniciListDto>();
            CreateMap<RoutesService.Domain.Entities.Kullanici, RoutesService.API.DTOs.KullaniciDetailDto>();

            CreateMap<RoutesService.API.DTOs.KullaniciCreateDto, RoutesService.Domain.Entities.Kullanici>()
                .ForMember(d => d.Sifre, o => o.Ignore()); // hash'i controller'da set edeceğiz

            CreateMap<RoutesService.API.DTOs.KullaniciUpdateDto, RoutesService.Domain.Entities.Kullanici>()
                .ForMember(d => d.Sifre, o => o.Ignore()); // hash controller'da


            CreateMap<RoutesService.Domain.Entities.Rol, RoutesService.API.DTOs.RolListDto>();
            CreateMap<RoutesService.Domain.Entities.Rol, RoutesService.API.DTOs.RolDetailDto>();
            CreateMap<RoutesService.API.DTOs.RolCreateDto, RoutesService.Domain.Entities.Rol>();
            CreateMap<RoutesService.API.DTOs.RolUpdateDto, RoutesService.Domain.Entities.Rol>();


            // KullaniciRolleri
            CreateMap<RoutesService.Domain.Entities.KullaniciRolleri, RoutesService.API.DTOs.KullaniciRolListDto>()
                .ForMember(d => d.KullaniciAd, o => o.MapFrom(s => s.Kullanici == null ? null : s.Kullanici.Ad))
                .ForMember(d => d.RolAd, o => o.MapFrom(s => s.Rol == null ? null : s.Rol.Ad));

            CreateMap<RoutesService.Domain.Entities.KullaniciRolleri, RoutesService.API.DTOs.KullaniciRolDetailDto>()
                .ForMember(d => d.KullaniciAd, o => o.MapFrom(s => s.Kullanici == null ? null : s.Kullanici.Ad))
                .ForMember(d => d.RolAd, o => o.MapFrom(s => s.Rol == null ? null : s.Rol.Ad));

            CreateMap<RoutesService.API.DTOs.KullaniciRolCreateDto, RoutesService.Domain.Entities.KullaniciRolleri>();
            CreateMap<RoutesService.API.DTOs.KullaniciRolUpdateDto, RoutesService.Domain.Entities.KullaniciRolleri>();



            CreateMap<RoutesService.Domain.Entities.RotaTanim, RoutesService.API.DTOs.RotaTanimListDto>();

            CreateMap<RoutesService.Domain.Entities.RotaTanim, RoutesService.API.DTOs.RotaTanimDetailDto>()
                .ForMember(d => d.GeometryWkt,
                    opt => opt.MapFrom(src =>
                        src.Geometry == null ? null : new NetTopologySuite.IO.WKTWriter().Write(src.Geometry)));

            CreateMap<RoutesService.API.DTOs.RotaTanimCreateDto, RoutesService.Domain.Entities.RotaTanim>()
                .ForMember(d => d.Geometry, opt => opt.Ignore()); // geomtrye controllerda çevireceğiz

            CreateMap<RoutesService.API.DTOs.RotaTanimUpdateDto, RoutesService.Domain.Entities.RotaTanim>()
                .ForMember(d => d.Geometry, opt => opt.Ignore());



            CreateMap<RoutesService.Domain.Entities.RotaKategoriTanim, RoutesService.API.DTOs.RotaKategoriTanimListDto>();
            CreateMap<RoutesService.Domain.Entities.RotaKategoriTanim, RoutesService.API.DTOs.RotaKategoriTanimDetailDto>();

            CreateMap<RoutesService.API.DTOs.RotaKategoriTanimCreateDto, RoutesService.Domain.Entities.RotaKategoriTanim>();
            CreateMap<RoutesService.API.DTOs.RotaKategoriTanimUpdateDto, RoutesService.Domain.Entities.RotaKategoriTanim>();



            CreateMap<RoutesService.Domain.Entities.RotaKategoriAtama, RoutesService.API.DTOs.RotaKategoriAtamaListDto>()
                .ForMember(d => d.RotaAdi, opt => opt.MapFrom(src => src.Rota != null ? src.Rota.Adi : null))
                .ForMember(d => d.KategoriAd, opt => opt.MapFrom(src => src.Kategori != null ? src.Kategori.Ad : null));

            CreateMap<RoutesService.Domain.Entities.RotaKategoriAtama, RoutesService.API.DTOs.RotaKategoriAtamaDetailDto>()
                .ForMember(d => d.RotaAdi, opt => opt.MapFrom(src => src.Rota != null ? src.Rota.Adi : null))
                .ForMember(d => d.KategoriAd, opt => opt.MapFrom(src => src.Kategori != null ? src.Kategori.Ad : null));

            CreateMap<RoutesService.API.DTOs.RotaKategoriAtamaCreateDto, RoutesService.Domain.Entities.RotaKategoriAtama>();
            CreateMap<RoutesService.API.DTOs.RotaKategoriAtamaUpdateDto, RoutesService.Domain.Entities.RotaKategoriAtama>();



            CreateMap<RoutesService.Domain.Entities.RotaOnemliYerTanim, RoutesService.API.DTOs.RotaOnemliYerTanimListDto>();

            CreateMap<RoutesService.Domain.Entities.RotaOnemliYerTanim, RoutesService.API.DTOs.RotaOnemliYerTanimDetailDto>()
                .ForMember(d => d.GeometryWkt,
                    opt => opt.MapFrom(s => s.Geometry == null ? null : new NetTopologySuite.IO.WKTWriter().Write(s.Geometry)));

            CreateMap<RoutesService.API.DTOs.RotaOnemliYerTanimCreateDto, RoutesService.Domain.Entities.RotaOnemliYerTanim>()
                .ForMember(d => d.Geometry, opt => opt.Ignore());

            CreateMap<RoutesService.API.DTOs.RotaOnemliYerTanimUpdateDto, RoutesService.Domain.Entities.RotaOnemliYerTanim>()
                .ForMember(d => d.Geometry, opt => opt.Ignore());


            CreateMap<RoutesService.Domain.Entities.RotaResimTanim, RoutesService.API.DTOs.RotaResimTanimListDto>();

            CreateMap<RoutesService.Domain.Entities.RotaResimTanim, RoutesService.API.DTOs.RotaResimTanimDetailDto>()
                .ForMember(d => d.ResimBase64,
                    opt => opt.MapFrom(s => s.Resim == null ? null : Convert.ToBase64String(s.Resim)));

            CreateMap<RoutesService.API.DTOs.RotaResimTanimCreateDto, RoutesService.Domain.Entities.RotaResimTanim>()
                .ForMember(d => d.Resim, opt => opt.Ignore()); // Base64 -> byte[] controller'da

            CreateMap<RoutesService.API.DTOs.RotaResimTanimUpdateDto, RoutesService.Domain.Entities.RotaResimTanim>()
                .ForMember(d => d.Resim, opt => opt.Ignore());


            CreateMap<RotaYorumTanim, RotaYorumTanimListDto>()
                .ForMember(d => d.RotaAdi, o => o.MapFrom(s => s.Rota == null ? null : s.Rota.Adi))
                .ForMember(d => d.KullaniciAd, o => o.MapFrom(s => s.Kullanici == null ? null : s.Kullanici.Ad));

            CreateMap<RotaYorumTanim, RotaYorumTanimDetailDto>()
                .ForMember(d => d.RotaAdi, o => o.MapFrom(s => s.Rota == null ? null : s.Rota.Adi))
                .ForMember(d => d.KullaniciAd, o => o.MapFrom(s => s.Kullanici == null ? null : s.Kullanici.Ad));

            CreateMap<RotaYorumTanimCreateDto, RotaYorumTanim>();
            CreateMap<RotaYorumTanimUpdateDto, RotaYorumTanim>();

        }
    }
}
