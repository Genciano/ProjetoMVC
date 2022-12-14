using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoMVC.Data.Repositório.Interface;
using ProjetoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JovemProgramador.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConfiguration _configuration;
        public AlunosController(IAlunoRepositorio alunoRepositorio, IConfiguration configuration)
        {
            _alunoRepositorio = alunoRepositorio;
            _configuration = configuration; 
        }
        public IActionResult Index()
        {
            var alunos = _alunoRepositorio.BuscarAlunos();
            return View(alunos);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
           var aluno = _alunoRepositorio.BuscarId(id);
            return View(aluno);
        }

        public IActionResult Excluir(AlunoModel alunos)
        {
            _alunoRepositorio.ExcluirAluno(alunos);
            return RedirectToAction("Index");
        }

        public IActionResult AlterarAluno(AlunoModel aluno)
        {
            _alunoRepositorio.EditarAluno(aluno);

            TempData["MensagemSucesso"] = "Aluno alterado com sucesso";

            return RedirectToAction("Index");
        }


        public IActionResult InserirAluno(AlunoModel alunos)
        {
            _alunoRepositorio.InserirAluno(alunos);

            TempData["MensagemSucesso"] = "Aluno adicionado com sucesso";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarEndereco(string cep)

        {
            try
            {
                cep = cep.Replace("-", "");

                EnderecoModel enderecoModel = new();

                using var client = new HttpClient();

                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
                }

                return View("Endereco", enderecoModel);
            }
            catch (Exception)
            {

                throw;
            }
             
        }
    }
    
}
