using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.EntityFramework.Core;
using Bing.Datas.UnitOfWorks;
using Bing.Samples.Domains.Models;
using Bing.Samples.Domains.Repositories;

namespace Bing.Samples.Datas.Repositories
{
    public class LoginRepository:RepositoryBase<Login>,ILoginRepository
    {
        public LoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
