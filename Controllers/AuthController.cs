using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCamisa10.DB;
using ApiCamisa10.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiCamisa10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public AuthController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]        
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody]User user)
        {
            bool result = _context.Users.AnyAsync(u => u.email == user.email).Result;

            if (!result)
            {
                return NotFound(new { message = "Usuario inexistente!" });
            }

            var usuario = _context.Users.Where(u => u.email == user.email).FirstOrDefault();

            if (usuario.password != user.password)
            {
                return NotFound(new { message = "Senha incorreta!" });
            }


            var token = Services.TokenServices.GerarToken(usuario);

            user.password = "";
            usuario.password = "";

            return new
            {
                Usuario = new User
                {
                    id = usuario.id,
                    name = usuario.name,
                    email = usuario.email

                },

                Token = token
            };
        }

        [HttpPost]
        [Route("Cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Cadastrar([FromBody]User user)
        {
            if (UserExists(user.email))
            {
                ModelState.AddModelError(string.Empty, "Usuario já cadastrado!");
                return BadRequest(ModelState);
            }


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.password = "";
            return user;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Teste()
        {
            return Ok(new { message = "Bem vindo ao camisa10, seu app para de escolha de time!" });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(u => u.email == email);
        }
    }
}