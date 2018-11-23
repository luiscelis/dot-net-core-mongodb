using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IUserRepository
    {
        //
        Task<IEnumerable<User>> GetAllUsers();

        //
        Task<User> GetUser(string id);

        // query after multiple parameters
        Task<IEnumerable<User>> GetUser(string id, string bodyText);

        // add new note document
        Task AddUser(User item);

        // remove a single document / note
        Task<bool> RemoveUser(string id);

        // update just a single document / note
        Task<bool> UpdateUser(string id, string body);

        Task<bool> UpdateUser(string id, User item);

        // demo interface - full document update
        Task<bool> UpdateUserDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllUsers();
    }
}
