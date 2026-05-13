using System.ComponentModel.DataAnnotations;

namespace Api;

public class AppOptions
{
    [Required]
    [Length(1, 1000)]
    public string DBConnectionString { get; set; } = string.Empty;
}