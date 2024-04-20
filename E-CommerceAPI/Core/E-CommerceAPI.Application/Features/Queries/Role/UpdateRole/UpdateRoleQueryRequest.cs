using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.Role.UpdateRole
{
    public class UpdateRoleQueryRequest:IRequest<UpdateRoleQueryResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}