using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Aspects;
using Bing.Datas.UnitOfWorks;
using Bing.Samples.Datas;
using Bing.Samples.Domains.Models;
using Bing.Samples.Domains.Repositories;
using Bing.Samples.Domains.Request.Act;

namespace Bing.Samples.Services.Impl
{
    public class LoginService:ILoginService
    {
        private ILoginRepository _loginRepository;
        private IUnitOfWork _unitOfWork;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            //_unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="act">注册操作</param>
        [UnitOfWork]
        public Guid Register(RegisterAct act)
        {
            Login entity=new Login(Guid.NewGuid());
            entity.LoginName = act.LoginName;
            entity.Name = act.UserName;
            entity.Mobile = act.Mobile;
            entity.PassWord = act.Password;
            entity.Note = "测试";
            entity.Status = 0;

            this._loginRepository.Add(entity);
            TestException(act);

            return entity.Id;
            //this._unitOfWork.Commit();
        }

        [UnitOfWork]
        public void TestException(RegisterAct act)
        {
            Login entity = new Login(Guid.NewGuid());
            entity.LoginName = act.LoginName;
            entity.Name = act.UserName;
            entity.Mobile = act.Mobile;
            entity.PassWord = act.Password;
            entity.Note = "测试";
            entity.Status = 0;

            this._loginRepository.Add(entity);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有登录信息
        /// </summary>
        /// <returns></returns>
        public List<Login> GetAllLogin()
        {
            return this._loginRepository.FindAll();
        }

        public List<Login> GetListByName(string name)
        {
            return this._loginRepository.GetListByName(name);
        }
    }
}
