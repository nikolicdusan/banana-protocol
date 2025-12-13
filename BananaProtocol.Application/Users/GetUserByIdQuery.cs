using BananaProtocol.Application.Common.Exceptions;
using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Application.Common.Mappers;
using BananaProtocol.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BananaProtocol.Application.Users;

public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

internal class GetUserByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(request, cancellationToken);

        return user.ToDto();
    }

    private async Task<User> GetUserByIdAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        return user ?? throw new NotFoundException(nameof(User), request.Id);
    }
}