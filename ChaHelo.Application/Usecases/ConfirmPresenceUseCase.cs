using System;
using ChaHelo.Application.Models;
using ChaHelo.Application.Services;
using ChaHelo.Domain.Entities;
using ChaHelo.Infra.Repositories;

namespace ChaHelo.Application.Usecases;

public class ConfirmPresenceUseCase
{
    private readonly PresenceRepository _repository;

    public ConfirmPresenceUseCase(PresenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Unit>> Call(string name)
    {
        try
        {
            var presence = new Presence
            {
                FullName = name
            };
            _repository.Presences.Add(presence);
            await _repository.SaveChangeAsync();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            LogService.LogHere(typeof(ConfirmPresenceUseCase), "Erro ao confirmar presença!", ex);
            return Result<Unit>.Failure(ex);
        }
    
    }
}
