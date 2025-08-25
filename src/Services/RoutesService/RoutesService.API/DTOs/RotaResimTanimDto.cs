namespace RoutesService.API.DTOs;
public class RotaResimTanimListDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public string? ResimAdi { get; set; }
    public string? ResimUzanti { get; set; }
    public long? ResimBoyut { get; set; }
}

public class RotaResimTanimDetailDto
{
    public int Id { get; set; }
    public int? RotaId { get; set; }
    public string? ResimAdi { get; set; }
    public string? ResimUzanti { get; set; }
    public long? ResimBoyut { get; set; }
    public string? AciklamaDetay { get; set; }
    public string? ResimBase64 { get; set; }
}

public class RotaResimTanimCreateDto
{
    public int? RotaId { get; set; }
    public string ResimAdi { get; set; } = null!;
    public string? ResimUzanti { get; set; }
    public long? ResimBoyut { get; set; }
    public string? AciklamaDetay { get; set; }
    public string? ResimBase64 { get; set; } 
}

public class RotaResimTanimUpdateDto
{
    public int? RotaId { get; set; }
    public string ResimAdi { get; set; } = null!;
    public string? ResimUzanti { get; set; }
    public long? ResimBoyut { get; set; }
    public string? AciklamaDetay { get; set; }
    public string? ResimBase64 { get; set; }
}
