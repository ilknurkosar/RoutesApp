namespace RoutesService.API.DTOs;

public record RotaResimTanimListDto(int Id, int RotaId, string? Url);
public record RotaResimTanimDetailDto(int Id, int RotaId, string? Url, string? Aciklama);

public class RotaResimTanimCreateDto { public int RotaId { get; set; } public string Url { get; set; } = null!; public string? Aciklama { get; set; } }
public class RotaResimTanimUpdateDto { public int RotaId { get; set; } public string Url { get; set; } = null!; public string? Aciklama { get; set; } }
