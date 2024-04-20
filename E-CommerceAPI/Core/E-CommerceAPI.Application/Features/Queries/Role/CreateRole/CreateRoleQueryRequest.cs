using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.Role.CreateRole
{
    public class CreateRoleQueryRequest : IRequest<CreateRoleQueryResponse>
    {
        public string Name { get; set; }
    }
}