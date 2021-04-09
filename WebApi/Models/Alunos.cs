using App.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApi.Models
{
    public class Alunos
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }

        public string data { get; set; }
        public int ra { get; set; }


        public List<Alunos> ListarAluno(int? id = null)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                return alunoBD.ListarAlunosDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Alunos: Erro => {ex.Message}");
            }
        }

        public void Inserir(Alunos aluno)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.InserirAlunoDB(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Inserir Aluno: Erro => {ex.Message}");
            }
        }

        public void Atualizar(Alunos aluno)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.AtualizarAlunoDB(aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Atualizar Aluno: Erro => {ex.Message}");
            }
        }

        public void Deletar(int id)
        {
            try
            {
                var alunoBD = new AlunoDAO();
                alunoBD.DeletarAlunoDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Deletar Aluno: Erro => {ex.Message}");
            }
        }

    }


}   
