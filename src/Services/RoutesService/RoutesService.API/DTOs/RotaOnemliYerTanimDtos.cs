namespace RoutesService.API.DTOs;

public record RotaOnemliYerTanimListDto(int Id, int RotaId, string? Ad);
public record RotaOnemliYerTanimDetailDto(int Id, int RotaId, string? Ad, string? Aciklama);

public class RotaOnemliYerTanimCreateDto { public int RotaId { get; set; } public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
public class RotaOnemliYerTanimUpdateDto { public int RotaId { get; set; } public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
