namespace RoutesService.API.DTOs;

public record RotaYorumTanimListDto(int Id, int RotaId, int KullaniciId, string? Yorum);
public record RotaYorumTanimDetailDto(int Id, int RotaId, int KullaniciId, string? Yorum);

public class RotaYorumTanimCreateDto { public int RotaId { get; set; } public int KullaniciId { get; set; } public string Yorum { get; set; } = null!; }
public class RotaYorumTanimUpdateDto { public int RotaId { get; set; } public int KullaniciId { get; set; } public string Yorum { get; set; } = null!; }
