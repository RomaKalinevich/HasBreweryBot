namespace Application.Contracts;

public class OrganizationDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> Addresses { get; set; } = new();
}