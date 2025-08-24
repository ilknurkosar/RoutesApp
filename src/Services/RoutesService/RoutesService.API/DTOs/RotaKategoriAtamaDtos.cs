namespace RoutesService.API.DTOs;

public record RotaKategoriAtamaListDto(int Id, int RotaId, int KategoriId);
public record RotaKategoriAtamaDetailDto(int Id, int RotaId, int KategoriId);

public class RotaKategoriAtamaCreateDto { public int RotaId { get; set; } public int KategoriId { get; set; } }
public class RotaKategoriAtamaUpdateDto { public int RotaId { get; set; } public int KategoriId { get; set; } }
