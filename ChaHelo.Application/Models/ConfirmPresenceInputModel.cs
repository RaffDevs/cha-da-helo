using System;
using System.ComponentModel.DataAnnotations;

namespace ChaHelo.Application.Models;

public class ConfirmPresenceInputModel
{
    [Required(ErrorMessage = "Por favor, insira seu nome completo.")]
    public string FullName { get; set; } = string.Empty;
}
