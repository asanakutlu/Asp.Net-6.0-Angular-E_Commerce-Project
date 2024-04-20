using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>,IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(E_CommerceAPIDbContext context) : base(context)
        {
        }
    }
}
