using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Role.DeleteRole
{
    public class DeleteRoleQueryHandler : IRequestHandler<DeleteRoleQueryRequest, DeleteRoleQueryResponse>
    {
        readonly IRoleService _roleService;

        public DeleteRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<DeleteRoleQueryResponse> Handle(DeleteRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var result=await _roleService.DeleteRole(request.Id);
            return new()
            {
                Succeeded = result
            };
        }
    }
}
