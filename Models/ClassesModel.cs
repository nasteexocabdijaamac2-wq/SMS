using System.ComponentModel.DataAnnotations;

namespace Sms.Models;

public class Classes
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Shift { get; set; }

    public DateTime Date { get; set; }

    public List<Students> Students { get; set; } = new List<Students>();
}