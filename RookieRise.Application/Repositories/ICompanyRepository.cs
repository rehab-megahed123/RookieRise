using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
    }
}
