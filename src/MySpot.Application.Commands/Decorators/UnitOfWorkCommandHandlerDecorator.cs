﻿using MySpot.Data.EF.UnitOfWork.Interfaces;
using MySpot.Services.UseCases;

namespace MySpot.Services.Decorators;

public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand: class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(
    ICommandHandler<TCommand> commandHandler,
    IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(TCommand command)
    {
        await _unitOfWork.ExecuteInTransactionAsync(() => _commandHandler.HandleAsync(command));
    }
}