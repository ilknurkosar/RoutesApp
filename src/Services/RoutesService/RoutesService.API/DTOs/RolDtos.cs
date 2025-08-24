namespace RoutesService.API.DTOs;

public record RolListDto(int Id, string? Ad);
public record RolDetailDto(int Id, string? Ad, string? Aciklama);

public class RolCreateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } }
public class RolUpdateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } }
