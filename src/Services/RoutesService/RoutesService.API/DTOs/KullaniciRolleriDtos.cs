namespace RoutesService.API.DTOs;

public record KullaniciRolleriListDto(int Id, int KullaniciId, int RolId);
public record KullaniciRolleriDetailDto(int Id, int KullaniciId, int RolId);

public class KullaniciRolleriCreateDto { public int KullaniciId { get; set; } public int RolId { get; set; } }
public class KullaniciRolleriUpdateDto { public int KullaniciId { get; set; } public int RolId { get; set; } }
