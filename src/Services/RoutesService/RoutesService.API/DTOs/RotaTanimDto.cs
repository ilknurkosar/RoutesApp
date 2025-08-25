namespace RoutesService.API.DTOs;
public class RotaTanimListDto
{
    public int Id { get; set; }
    public string? Adi { get; set; }
    public string? Renk { get; set; }
    public double? Mesafe { get; set; }
    public int? TahminiSureDakika { get; set; }
}

public class RotaTanimDetailDto
{
    public int Id { get; set; }
    public string? Adi { get; set; }
    public string? Renk { get; set; }
    public double? Mesafe { get; set; }
    public int? TahminiSureDakika { get; set; }
    public string? GeometryWkt { get; set; }
}


public class RotaTanimCreateDto
    {
        public string? Adi { get; set; }
        public string? Renk { get; set; }
        public string? GeometryWkt { get; set; }
        public double? Mesafe { get; set; }
        public int? TahminiSureDakika { get; set; }
    }

    public class RotaTanimUpdateDto
    {
        public string? Adi { get; set; }
        public string? Renk { get; set; }
        public string? GeometryWkt { get; set; }
        public double? Mesafe { get; set; }
        public int? TahminiSureDakika { get; set; }
    }

