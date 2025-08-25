namespace RoutesService.API.DTOs;

public class RotaOnemliYerTanimListDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public string? Ad { get; set; }
    public string? AciklamaDetay { get; set; }
}

public class RotaOnemliYerTanimDetailDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public string? Ad { get; set; }
    public string? AciklamaDetay { get; set; }
    public string? GeometryWkt { get; set; }
}


public class RotaOnemliYerTanimCreateDto
{
    public int? RotaId { get; set; }
    public string? Ad { get; set; }
    public string? AciklamaDetay { get; set; }
    public string? GeometryWkt { get; set; }
}

public class RotaOnemliYerTanimUpdateDto
{
    public int? RotaId { get; set; }
    public string? Ad { get; set; } 
    public string? AciklamaDetay { get; set; }
    public string? GeometryWkt { get; set; } 
}