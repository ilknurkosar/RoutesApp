namespace RoutesService.API.DTOs;

public record RotaTanimListDto(int Id, string? Ad);
public record RotaTanimDetailDto(int Id, string? Ad, string? Aciklama);

public class RotaTanimCreateDto { public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
public class RotaTanimUpdateDto { public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
