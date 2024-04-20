using E_CommerceAPI.Domain.Eetities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Repositories
{
    public interface ICompetedOrderWriteRepository : IWriteReposityory<CompletedOrder>
    {
       
    }
}
