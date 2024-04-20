using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.Role.DeleteRole
{
    public class DeleteRoleQueryRequest:IRequest<DeleteRoleQueryResponse>
    {
        public string Id { get; set; }
    }
}