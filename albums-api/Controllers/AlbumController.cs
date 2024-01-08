using albums_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace albums_api.Controllers
{
    [Route("albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        // GET: api/album
        [HttpGet]
        public IActionResult Get()
        {
            var albums = Album.GetAll();

            return Ok(albums);
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var album = Album.Get(id);
            return Ok();
        }

        // Search for an album by name, artist, or genre
        [HttpGet("search")]
        public IActionResult Search(string query)
        {
            var albums = Album.GetAll();

            var results = albums.Where(album =>
                album.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                album.Artist.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                album.Genre.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return Ok(results);
        }
        // function that sort albums by name, artist or genre
        
        public IActionResult Sort(string sortBy)
        {
            var albums = Album.GetAll();

            switch (sortBy.ToLower())
            {
                case "name":
                    albums = albums.OrderBy(album => album.Name).ToList();
                    break;
                case "artist":
                    albums = albums.OrderBy(album => album.Artist).ToList();
                    break;
                case "genre":
                    albums = albums.OrderBy(album => album.Genre).ToList();
                    break;
                default:
                    return BadRequest("Invalid sort parameter. Please specify 'name', 'artist', or 'genre'.");
            }

            return Ok(albums);
        }

    }
}
///<summary>
///function that returns a single album by id
///</summary>
///<param name="id">The id of the album to return</param>
///<returns>The album with the given id</returns>