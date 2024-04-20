using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.AppUser
{
    public class GetRolesToUserQueryRequest:IRequest<GetRolesToUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}