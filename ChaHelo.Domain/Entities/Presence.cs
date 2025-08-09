using System;

namespace ChaHelo.Domain.Entities;

public class Presence
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
