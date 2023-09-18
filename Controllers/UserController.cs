using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISer_dhiya.Models;

namespace APISer_dhiya.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Nama = "John Doe", Email = "johndoe@example.com", Alamat = "123 Main St" },
            new User { Id = 2, Nama = "Jane Smith", Email = "janesmith@example.com", Alamat = "456 Elm St" }
        };

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            // Mengembalikan semua data pengguna
            return Ok(_users);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            // Cari pengguna berdasarkan ID
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                // Jika pengguna tidak ditemukan, kembalikan NotFound
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/user
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            // Membuat pengguna baru
            newUser.Id = _users.Max(u => u.Id) + 1;
            _users.Add(newUser);

            // Kembalikan pengguna yang baru dibuat dengan kode status 201 (Created)
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            // Cari pengguna berdasarkan ID
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                // Jika pengguna tidak ditemukan, kembalikan NotFound
                return NotFound();
            }

            // Perbarui properti pengguna yang ada
            existingUser.Nama = updatedUser.Nama;
            existingUser.Email = updatedUser.Email;
            existingUser.Alamat = updatedUser.Alamat;

            return NoContent();
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Cari pengguna berdasarkan ID
            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove == null)
            {
                // Jika pengguna tidak ditemukan, kembalikan NotFound
                return NotFound();
            }

            // Hapus pengguna dari daftar
            _users.Remove(userToRemove);

            return NoContent();
        }
    }
}
