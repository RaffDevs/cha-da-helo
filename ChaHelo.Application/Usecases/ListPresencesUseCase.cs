using System;
using ChaHelo.Application.Models;
using ChaHelo.Application.Services;
using ChaHelo.Domain.Entities;
using ChaHelo.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChaHelo.Application.Usecases;

public class ListPresencesUseCase
{
    private readonly PresenceRepository _repository;

    public ListPresencesUseCase(PresenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<Presence>>> Call()
    {
        try
        {
            var presences = await _repository.Presences.ToListAsync();
            return Result<List<Presence>>.Success(presences);
        }
        catch (Exception ex)
        {
            LogService.LogHere(typeof(ListPresencesUseCase), "Erro ao buscar lista de presenças!", ex);
            return Result<List<Presence>>.Failure(ex);
        }
    }
}
