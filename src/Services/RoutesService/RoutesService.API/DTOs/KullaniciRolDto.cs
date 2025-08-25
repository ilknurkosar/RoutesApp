namespace RoutesService.API.DTOs;

public class KullaniciRolListDto
{
    public int Id { get; set; }
    public int KullaniciId { get; set; }
    public int RolId { get; set; }
    public string? KullaniciAd { get; set; }
    public string? RolAd { get; set; }
}

public class KullaniciRolDetailDto
{
    public int Id { get; set; }
    public int KullaniciId { get; set; }
    public int RolId { get; set; }
    public string? KullaniciAd { get; set; }
    public string? RolAd { get; set; }
}


public class KullaniciRolCreateDto
{
    public int KullaniciId { get; set; }
    public int RolId { get; set; }
}

public class KullaniciRolUpdateDto
{
    public int KullaniciId { get; set; }
    public int RolId { get; set; }
}
