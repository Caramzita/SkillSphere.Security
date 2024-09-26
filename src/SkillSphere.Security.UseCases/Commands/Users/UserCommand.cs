using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.UseCases.Commands.Users;

public class UserCommand : IRequest<Result<UserDto>>
{
    public Guid Id { get; }

    public UserCommand(Guid id)
    {
        Id = id;
    }
}
