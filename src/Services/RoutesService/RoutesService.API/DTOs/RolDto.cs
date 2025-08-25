namespace RoutesService.API.DTOs;

public class RolListDto
{
    public int Id { get; set; }

    public string Ad { get; set; }
}

public class RolDetailDto
{
    public int Id { get; set; }

    public string Ad { get; set; }
}
public class RolCreateDto
{
    public string Ad { get; set; } = null!;
}

public class RolUpdateDto
{
    public string Ad { get; set; } = null!;
}
