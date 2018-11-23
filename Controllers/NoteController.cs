using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Infrastructure;
using System;
using System.Collections.Generic;

namespace TodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NoteController
    {

        private readonly INoteRepository repository;

        public NoteController(INoteRepository repository)
        {
            this.repository = repository;
        }


        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Note>> GetAll()
        {
            return await repository.GetAllNotes();
        }
    }
}
