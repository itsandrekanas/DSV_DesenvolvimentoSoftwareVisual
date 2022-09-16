using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Folha.Models;

namespace Folha.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private DataContext _context;
        public FuncionarioController(DataContext context) => _context = context;
        
        private static List<Funcionario> funcionarios = new List<Funcionario>();

        // GET: /api/funcionario/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() => Ok(_context.Funcionarios.ToList());

        // POST: /api/funcionario/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
            return Created("", funcionario);
        }

        // GET: /api/funcionario/buscar/{cpf}
        [HttpGet]
        [Route("buscar/{cpf}")]
        public IActionResult Buscar([FromRoute] string cpf)
        {
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf));
            return funcionario != null ? Ok(funcionario) : NotFound();
        }

        // DELETE: /api/funcionario/deletar/{cpf}
        [HttpDelete]
        [Route("deletar/{cpf}")]
        public IActionResult Deletar([FromRoute] string cpf)
        {
            Funcionario funcionario = funcionarios.FirstOrDefault(f => f.Cpf.Equals(cpf));
            if (funcionario != null)
            {
                funcionarios.Remove(funcionario);
                return Ok(funcionario);
            }
            return NotFound();
        }

        // PATCH: /api/funcionario/alterar
        [HttpPatch]
        [Route("alterar")]
        public IActionResult Alterar([FromBody] Funcionario funcionario)
        {
            Funcionario funcionarioBuscado = funcionarios.FirstOrDefault(f => f.Cpf.Equals(funcionario.Cpf));
            if (funcionarioBuscado != null)
            {
                funcionarioBuscado.Nome = funcionario.Nome;
                return Ok(funcionarioBuscado);
            }
            return NotFound();
        }
    }
}
