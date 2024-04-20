using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Eetities;
using E_CommerceAPI.Persistence.Contexts;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompetedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(E_CommerceAPIDbContext context) : base(context)
        {
        }
    }
}
