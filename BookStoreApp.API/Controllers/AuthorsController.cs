using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookAPIContext _context;
        private readonly IMapper mapper;

        public AuthorsController(BookAPIContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllAuthorsWithNoBooks>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();

            if (authors == null)
            {
                return NotFound();
            }

            var allAuthorWithNoBooks = mapper.Map<GetAllAuthorsWithNoBooks>(authors);


            return Ok(authors);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OneAuthorWithNoBooks>> GetOneAuthorWithNoBooks(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var oneauthorDto = mapper.Map<OneAuthorWithNoBooks>(author);

            return Ok(oneauthorDto);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor([FromBody] AuthorCreateDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOneAuthorWithNoBooks), new { id = author.Id }, author);
        }

        //POST for sending info to server without books in there


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
