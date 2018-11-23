using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class UserRepository : IUserRepository
    {
        /*
         * 
         */
        private readonly NoteContext context = null;

        /*
         *
         */
        public UserRepository(IOptions<Settings> settings)
        {
            try
            {
                this.context = new NoteContext(settings);

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw e;
            }
        }


        /*
         * 
         */
        public async Task AddUser( User item)
        {
            try
            {
                await context.Users.InsertOneAsync(item);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw e;
            }
        }


        /*
         * 
         */
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        /*
         * 
         */
        public async Task<IEnumerable<User>> GetUser(string id, string body)
        {
            try 
            {
                var query = context.Users.Find(user => user.Id.Equals(id) && user.Body.Contains(body));
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        /*
         * 
         */
        public async Task<User> GetUser(string id)
        {
            ObjectId internalId = GetInternalId(id);
            return await context.Users.Find(user => user.Id == id || user.InternalId.Equals(internalId)).FirstOrDefaultAsync();
        }


        /*
         * 
         */
        public async Task<bool> RemoveAllUsers()
        {
            DeleteResult actionResult = await context.Users.DeleteManyAsync(new BsonDocument());
            return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
        }


        /*
         * 
         */
        public async Task<bool> RemoveUser(string id)
        {
            DeleteResult actionResult = await context.Users.DeleteOneAsync(
                                Builders<User>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }


        /*
         * 
         */
        public async Task<bool> UpdateUser(string id, string body)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, id);
            var update = Builders<User>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await context.Users.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        /*
         * 
         */
        public async Task<bool> UpdateUser(string id, User item)
        {
            try
            {
                ReplaceOneResult actionResult = await context.Users
                                                .ReplaceOneAsync(n => n.Id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        /*
         * 
         */
        public async Task<bool> UpdateUserDocument(string id, string body)
        {
            var item = await GetUser(id) ?? new User();
            item.Body = body;
            item.UpdatedOn = DateTime.Now;

            return await UpdateUser(id, item);
            // throw new NotImplementedException();
        }

        // Try to convert the Id to a BSonId value
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
    }
}
