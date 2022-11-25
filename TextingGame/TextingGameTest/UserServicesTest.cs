using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Service.Interface;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextingGameTest
{
    public class UserServiceTest
    {
        private readonly DbContextOptions<DbTextingGameContext> dbContextOptions = new DbContextOptionsBuilder<DbTextingGameContext>()
        .UseInMemoryDatabase(databaseName: "db_TextingGame")
         .Options;
        DbTextingGameContext context;
        UserServices Services;
        Mock<IEncryptServices> encrypt;
        public UserServiceTest()
        {
            encrypt = new Mock<IEncryptServices>();
            context = new DbTextingGameContext(dbContextOptions);
            context.Database.EnsureCreated();
            //SeedDatabase();
            Services = new UserServices(context, encrypt.Object);
        }
        public void SeeDatabase()
        {
            var user = new List<TblUserDetail>()
            {
                new TblUserDetail(){UserId = 22,UserName = "Anshika",EmailId = "anshika@1gmail.com",Password = "anshi12",CreatedDate = DateTime.Now,UpdatedDate = null,IsActive = true},
                new TblUserDetail(){UserId = 23,UserName = "Pavan",EmailId = "pavan@1gmail.com",Password = "pavan12",CreatedDate = DateTime.Now,UpdatedDate = null,IsActive = true}
            };
            context.TblUserDetails.AddRange(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetAll_Test()
        {
            SeeDatabase();
            var result = Services.GetUsers();
            var items = Assert.IsType<List<TblUserDetail>>(result);
            Assert.Equal(2, items.Count);
            Dispose();
        }

        [Fact]
        public void Login()
        {
            SeeDatabase();
            var login = new UserLogin()
            {
                EmailId = "anshika@1gmail.com",
                Password = "anshi12",
                ConfirmPassword = "anshi12"
            };
            encrypt.Setup(method => method.EncodePasswordToBase64(login.Password)).Returns(login.Password);
            var result = Services.UserLogIn(login);
            Assert.True(result);
            Dispose();
        }
    }
}

