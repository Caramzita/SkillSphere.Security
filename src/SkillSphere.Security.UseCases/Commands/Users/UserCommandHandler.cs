using AutoMapper;
using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.Security.Contracts.DTOs;
using SkillSphere.Security.Core.Interfaces;

namespace SkillSphere.Security.UseCases.Commands.Users;

public class UserCommandHandler : IRequestHandler<UserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<UserDto>> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            return Result<UserDto>.Invalid("Такой пользователь не существует");
        }

        return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
    }
}
