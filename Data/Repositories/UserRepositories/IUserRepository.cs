using Data.Entities;
using Data.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.UserRepositories
{
    public interface IUserRepository :IGenericRepository<User>
    {

    }
}
