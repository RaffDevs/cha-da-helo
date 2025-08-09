using System;

namespace ChaHelo.Application.Models;

public sealed record Unit
{
    public static readonly Unit Value = new();
    private Unit() {}
}