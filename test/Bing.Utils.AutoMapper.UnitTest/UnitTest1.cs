using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bing.Utils.Json;

namespace Bing.Utils.AutoMapper.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<UserInfo, UserInfoViewDto>();
            });
        }

        [TestMethod]
        public void TestMethod1()
        {
            var user=new UserInfoDto()
            {
                Id = Guid.NewGuid(),
                Name = "Test001"
            };
            var result = user.MapTo<UserInfo>();
            Console.WriteLine(result.ToJson());

            var other = result.MapTo<UserInfoViewDto>();
            Console.WriteLine(other.ToJson());
        }
    }
}
