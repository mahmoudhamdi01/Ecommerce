using Store.Data.Entities.IDentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.TokenServices
{
    public interface ITokenServices
    {
        string GenerateToken(AppUser appUser);
    }
}
