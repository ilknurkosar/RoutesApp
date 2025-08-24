using AutoMapper;
using RoutesService.API.DTOs;
using RoutesService.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoutesService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Kullanici
        CreateMap<Kullanici, KullaniciListDto>();
        CreateMap<Kullanici, KullaniciDetailDto>();
        CreateMap<KullaniciCreateDto, Kullanici>()
            .ForMember(d => d.Sifre, opt => opt.Ignore()); // hash'i controller'da set edeceğiz
        CreateMap<KullaniciUpdateDto, Kullanici>();

        // Rol
        CreateMap<Rol, RolListDto>();
        CreateMap<Rol, RolDetailDto>();
        CreateMap<RolCreateDto, Rol>();
        CreateMap<RolUpdateDto, Rol>();

        // İzin
        CreateMap<Izin, IzinListDto>();
        CreateMap<Izin, IzinDetailDto>();
        CreateMap<IzinCreateDto, Izin>();
        CreateMap<IzinUpdateDto, Izin>();

        // Kurum
        CreateMap<KurumTanim, KurumTanimListDto>();
        CreateMap<KurumTanim, KurumTanimDetailDto>();
        CreateMap<KurumTanimCreateDto, KurumTanim>();
        CreateMap<KurumTanimUpdateDto, KurumTanim>();

        // Yetki Alanı
        CreateMap<YetkiAlaniTanim, YetkiAlaniTanimListDto>();
        CreateMap<YetkiAlaniTanim, YetkiAlaniTanimDetailDto>();
        CreateMap<YetkiAlaniTanimCreateDto, YetkiAlaniTanim>();
        CreateMap<YetkiAlaniTanimUpdateDto, YetkiAlaniTanim>();

        // Rota Tanım
        CreateMap<RotaTanim, RotaTanimListDto>();
        CreateMap<RotaTanim, RotaTanimDetailDto>();
        CreateMap<RotaTanimCreateDto, RotaTanim>();
        CreateMap<RotaTanimUpdateDto, RotaTanim>();

        // Rota Kategori Tanım
        CreateMap<RotaKategoriTanim, RotaKategoriTanimListDto>();
        CreateMap<RotaKategoriTanim, RotaKategoriTanimDetailDto>();
        CreateMap<RotaKategoriTanimCreateDto, RotaKategoriTanim>();
        CreateMap<RotaKategoriTanimUpdateDto, RotaKategoriTanim>();

        // Rota Kategori Atama
        CreateMap<RotaKategoriAtama, RotaKategoriAtamaListDto>();
        CreateMap<RotaKategoriAtama, RotaKategoriAtamaDetailDto>();
        CreateMap<RotaKategoriAtamaCreateDto, RotaKategoriAtama>();
        CreateMap<RotaKategoriAtamaUpdateDto, RotaKategoriAtama>();

        // Rota Resim
        CreateMap<RotaResimTanim, RotaResimTanimListDto>();
        CreateMap<RotaResimTanim, RotaResimTanimDetailDto>();
        CreateMap<RotaResimTanimCreateDto, RotaResimTanim>();
        CreateMap<RotaResimTanimUpdateDto, RotaResimTanim>();

        // Rota Önemli Yer
        CreateMap<RotaOnemliYerTanim, RotaOnemliYerTanimListDto>();
        CreateMap<RotaOnemliYerTanim, RotaOnemliYerTanimDetailDto>();
        CreateMap<RotaOnemliYerTanimCreateDto, RotaOnemliYerTanim>();
        CreateMap<RotaOnemliYerTanimUpdateDto, RotaOnemliYerTanim>();

        // Rota Yorum
        CreateMap<RotaYorumTanim, RotaYorumTanimListDto>();
        CreateMap<RotaYorumTanim, RotaYorumTanimDetailDto>();
        CreateMap<RotaYorumTanimCreateDto, RotaYorumTanim>();
        CreateMap<RotaYorumTanimUpdateDto, RotaYorumTanim>();

        // Kullanıcı Rolleri
        CreateMap<KullaniciRolleri, KullaniciRolleriListDto>();
        CreateMap<KullaniciRolleri, KullaniciRolleriDetailDto>();
        CreateMap<KullaniciRolleriCreateDto, KullaniciRolleri>();
        CreateMap<KullaniciRolleriUpdateDto, KullaniciRolleri>();

        // Rol İzinleri
        CreateMap<RolIzinleri, RolIzinleriListDto>();
        CreateMap<RolIzinleri, RolIzinleriDetailDto>();
        CreateMap<RolIzinleriCreateDto, RolIzinleri>();
        CreateMap<RolIzinleriUpdateDto, RolIzinleri>();
    }
}
