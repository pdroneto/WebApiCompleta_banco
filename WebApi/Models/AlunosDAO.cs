﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace App.Repository
{
    public class AlunoDAO
    {
        //private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        private string stringConexao = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\repos\projetoWebApi\WebApiCompleta_banco\WebApi\App_Data\Database.mdf;Integrated Security = True";

        
        //private string stringConexao1 = ConfigurationManager.AppSettings["ConnectionString"];

        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<Alunos> ListarAlunosDB(int? id = null)
        {
            var listaAlunos = new List<Alunos>();

            try
            {
                IDbCommand selectCmd = conexao.CreateCommand();

                if (id == null)
                    selectCmd.CommandText = "select * from Alunos";
                else
                    selectCmd.CommandText = $"select * from Alunos where id = {id}";

                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var alu = new Alunos();
                    
                        alu.id = Convert.ToInt32(resultado["Id"]);
                        alu.nome = Convert.ToString(resultado["nome"]);
                        alu.sobrenome = Convert.ToString(resultado["sobrenome"]);
                        alu.telefone = Convert.ToString(resultado["telefone"]);
                        alu.ra = Convert.ToInt32(resultado["ra"]);
                    

                    listaAlunos.Add(alu);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAlunoDB(Alunos aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                insertCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                insertCmd.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                insertCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);
                insertCmd.Parameters.Add(paramRa);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarAlunoDB(Alunos aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Alunos set nome = @nome, sobrenome = @sobrenome, telefone = @telefone, ra = @ra where id = @id";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);

                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramSobrenome);
                updateCmd.Parameters.Add(paramTelefone);
                updateCmd.Parameters.Add(paramRa);

                IDbDataParameter paramID = new SqlParameter("id", aluno.id);
                updateCmd.Parameters.Add(paramID);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand DeleteCmd = conexao.CreateCommand();
                DeleteCmd.CommandText = "delete from Alunos where id = @id";

                IDbDataParameter paramID = new SqlParameter("id", id);
                DeleteCmd.Parameters.Add(paramID);

                DeleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}