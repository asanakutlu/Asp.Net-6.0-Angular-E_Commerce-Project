using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Role.UpdateRole
{
    public class UpdateRoleQueryHandler : IRequestHandler<UpdateRoleQueryRequest, UpdateRoleQueryResponse>
    {
        readonly IRoleService _roleService;

        public UpdateRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleQueryResponse> Handle(UpdateRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.UpdateRole(request.Id, request.Name);
            return new()
            {
                Succeeded=result
            };
        }
    }
}
