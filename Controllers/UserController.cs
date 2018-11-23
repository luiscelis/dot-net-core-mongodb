using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using TodoApi.Models;
using TodoApi.Interfaces;

using TodoApi.Infrastructure;



namespace TodoAPi.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        /*
         * 
         */
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        //
        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return  await repository.GetAllUsers();
        }

        //
        [NoCache]
        [HttpGet("{id}")]
        public async Task<User> GetUser(string id)
        {
            return  await repository.GetUser(id);
        }

        //
        [NoCache]
        [HttpPost]
        public async Task AddUser([FromBody] UserParam param)
        {
            var item = new User{
                Id = param.Id,
                Body = param.Body,
                UpdatedOn = DateTime.Now,
                UserId = param.UserId
            };
            item.Body = param.Body;

             await repository.AddUser(item);
        }

        //
        [NoCache]
        [HttpDelete]
        public async Task<bool> RemoveAllUsers() 
        {
            return await repository.RemoveAllUsers();
        }
    }
}
            