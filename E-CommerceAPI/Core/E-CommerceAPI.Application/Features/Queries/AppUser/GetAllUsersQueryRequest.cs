using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.AppUser
{
    public class GetAllUsersQueryRequest:IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}