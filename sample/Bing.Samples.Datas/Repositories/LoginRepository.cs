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
    public class LoginRepository: ApplicationRepositoryBase<Login>,ILoginRepository
    {
        public LoginRepository(IBingSampleUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Login> GetListByName(string name)
        {
            return Context.LoginDs.Where(x => x.Name.Contains(name)).ToList();
        }
    }
}
