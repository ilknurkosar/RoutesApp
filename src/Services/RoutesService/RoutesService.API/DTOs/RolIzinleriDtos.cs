namespace RoutesService.API.DTOs;

public record RolIzinleriListDto(int Id, int RolId, int IzinId);
public record RolIzinleriDetailDto(int Id, int RolId, int IzinId);

public class RolIzinleriCreateDto { public int RolId { get; set; } public int IzinId { get; set; } }
public class RolIzinleriUpdateDto { public int RolId { get; set; } public int IzinId { get; set; } }
