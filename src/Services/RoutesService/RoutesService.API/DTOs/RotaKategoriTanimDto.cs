namespace RoutesService.API.DTOs;

public class RotaKategoriTanimListDto
{
    public int Id { get; set; }
    public string? Ad { get; set; }
}

public class RotaKategoriTanimDetailDto
{
    public int Id { get; set; }
    public string? Ad { get; set; }
}

public class RotaKategoriTanimCreateDto
{
    public string? Ad { get; set; } 
}

public class RotaKategoriTanimUpdateDto
{
    public string? Ad { get; set; }
}