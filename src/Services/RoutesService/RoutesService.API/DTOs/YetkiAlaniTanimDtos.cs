namespace RoutesService.API.DTOs;

public record YetkiAlaniTanimListDto(int Id, string? Ad, int KurumId);
public record YetkiAlaniTanimDetailDto(int Id, string? Ad, string? Aciklama, int KurumId);

public class YetkiAlaniTanimCreateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } public int KurumId { get; set; } }
public class YetkiAlaniTanimUpdateDto { public string? Ad { get; set; } public string? Aciklama { get; set; } public int KurumId { get; set; } }
