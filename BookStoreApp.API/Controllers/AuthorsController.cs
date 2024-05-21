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
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookAPIContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDTO>>> GetAllAuthors()
        {
            try
            {
                var authors = mapper.Map<IEnumerable<AuthorReadOnlyDTO>>(await _context.Authors.ToListAsync());

                if (authors == null)
                {
                    return NotFound();
                }

                return Ok(authors);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Performing GET");
                return StatusCode(500, "Internal server error");
            }

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDTO>> GetOneAuthorWithNoBooks(int id)
        {

            AuthorReadOnlyDTO authorDto = null;
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                authorDto = mapper.Map<AuthorReadOnlyDTO>(author);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Performing GET");
                return StatusCode(500, "Internal server error");
            }

            return Ok(authorDto);

        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                var author = mapper.Map<Author>(authorDTO);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOneAuthorWithNoBooks), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Performing POST");
                return StatusCode(500, "Internal server error");
            }

        }

        //POST for sending info to server without books in there


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] AuthorUpdateDTO authorDto)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                if (id != authorDto.Id)
                {
                    return BadRequest();
                }

                mapper.Map(authorDto, author);
                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Performing PUT");
                return StatusCode(500, "Internal server error");
            }


        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {

            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Performing DELETE");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
