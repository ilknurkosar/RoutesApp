namespace RoutesService.API.DTOs;

public record IzinListDto(int Id, string? Name);
public record IzinDetailDto(int Id, string? Name);

public class IzinCreateDto { public string Name { get; set; } = null!; }
public class IzinUpdateDto { public string Name { get; set; } = null!; }

