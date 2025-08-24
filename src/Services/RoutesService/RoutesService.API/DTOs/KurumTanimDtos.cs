namespace RoutesService.API.DTOs;

public record KurumTanimListDto(int Id, string? Ad);
public record KurumTanimDetailDto(int Id, string? Ad, string? Aciklama);

public class KurumTanimCreateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } }
public class KurumTanimUpdateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } }
