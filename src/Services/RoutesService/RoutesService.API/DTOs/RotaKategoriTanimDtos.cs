namespace RoutesService.API.DTOs;

public record RotaKategoriTanimListDto(int Id, string? Ad);
public record RotaKategoriTanimDetailDto(int Id, string? Ad, string? Aciklama);

public class RotaKategoriTanimCreateDto { public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
public class RotaKategoriTanimUpdateDto { public string Ad { get; set; } = null!; public string? Aciklama { get; set; } }
