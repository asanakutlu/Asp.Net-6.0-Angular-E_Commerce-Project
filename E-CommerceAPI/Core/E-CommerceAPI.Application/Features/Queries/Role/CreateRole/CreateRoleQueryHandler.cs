using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Role.CreateRole
{
    public class CreateRoleQueryHandler : IRequestHandler<CreateRoleQueryRequest, CreateRoleQueryResponse>
    {
        readonly IRoleService _roleService;

        public CreateRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<CreateRoleQueryResponse> Handle(CreateRoleQueryRequest request, CancellationToken cancellationToken)
        {
           var result= await _roleService.CreateRole(request.Name);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
