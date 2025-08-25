namespace RoutesService.API.DTOs
{
    public class AuthDtos
    {
        public class LoginRequestDto
        {
            public string KullaniciAdiOrEmail { get; set; } = null!;
            public string Sifre { get; set; } = null!;
        }

        public class LoginResponseDto
        {
            public int UserId { get; set; }
            public string KullaniciAdi { get; set; } = null!;
            public string Token { get; set; } = null!;
            public List<string> Roles { get; set; } = new();
        }

        public class KullaniciCreateDto
        {
            public string Ad { get; set; } = null!;
            public string Soyad { get; set; } = null!;
            public string KullaniciAdi { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Sifre { get; set; } = null!;
        }

    }
}
