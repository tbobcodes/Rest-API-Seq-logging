using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookAPIContext _context;
        private readonly IMapper mapper;

        public BooksController(BookAPIContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                var books = await _context.Books.Include(q => q.Author).ToListAsync();
                var booksDtos = mapper.Map<IEnumerable<BookReadOnlyDto>>(books);
                return Ok(booksDtos);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Internal server error");
            }

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
           var book = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            var book = mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new {id = book.Id}, book);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if ( id != bookDto.Id )
            {
                return BadRequest();
            }

            var book = await _context.Books.FindAsync(id);

            if( book == null )
            {
                return NotFound();
            }

            mapper.Map(bookDto, book);
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult>  DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if ( book == null )
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
