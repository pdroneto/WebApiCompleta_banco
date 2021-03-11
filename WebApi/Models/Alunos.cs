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
        public int ra{ get; set; }

        // O método abaixo retorna uma lista com cada aluno do meu arquivo base.json
        public List<Alunos> ListaAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\base.json");
            var json = File.ReadAllText(caminhoArquivo);
            var listaAlunos = JsonConvert.DeserializeObject<List<Alunos>>(json);

            return listaAlunos;
        }
        /*O método abaixo é utilizado para reescrever meu arquivo, ou seja, todas as vezes que eu precisar inserir, atualizar ou deletar
         * um aluno no meu arquivo base.json
        
         */
        public bool ReescreverArquivo(List<Alunos> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, json);
            return true;

        }

        /* Esse método faz a inserção dos alunos da seguinte forma:
         Primeiro ele chama a função ListaAlunos() para carregar o conteúdo dentro da variável ListaAlunos
        Eu criei uma outra variável chamada maxId que recebe o maior ID encontrado dentro da ListAlunos
        Eu utilizo a variável que eu estou passando como parâmetro para incrementar essa variável com o valor máximo que eu encontrei
        mais 1
        Eu chamo novamente eu insiro na minha ListaAlunos o aluno que eu acabei de preparar, bom agora eu tenho que colocar
        dentro do meu arquivo json;
        Agora eu chamo minha função ReescreverArquivo(ListaAlunos) e passo a lista que eu criei e adicionei o aluno, se tudo der certo
        ela retorna um true sinalizando que a inserção foi feita com sucesso.
         
         */

        public Alunos Inserir(Alunos Aluno)
        {
            var ListaAlunos = this.ListaAlunos();
            var maxId = ListaAlunos.Max(aluno => aluno.id);
            Aluno.id = maxId + 1;
            ListaAlunos.Add(Aluno);
            ReescreverArquivo(ListaAlunos);
            return Aluno;

        }
        /*
         Primeira coisa que o meu método de atualizar faz é criar uma variável ListaAluos para receber os dados da função ListaAlunos();
        eu criei uma variável itemIndex que recebe a busca dentro da minha lista do id do aluno que foi passado por parâmetro
        Eu faço um teste para verificar se esse id que eu busquei é maior ou igual a 0
        Bom, se ele for maior o segundo parâmetro que eu passei na minha função que é um objeto do tipo aluno terá uma atualização
        no seu id.
        eu insiro na minha ListaAlunos[itemIndex], ou seja, na posição onde está localizado o aluno que eu quero atualizar os dados do
        Aluno que eu passei como parâmetro.
        Agora se o itemIndex for menor que zero, significa que minha lista é vazia e não há o que atualizar.
        Por fim, eu chamo a minha função de ReescreverArquivo(ListaAlunos) e mando atualizar no arquivo base.json todas as modificações
        que eu fiz
         
         */
        public Alunos Atualizar(int id, Alunos Aluno)
        {
            var ListaAlunos = this.ListaAlunos();
            var itemIndex = ListaAlunos.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                Aluno.id = id;
                ListaAlunos[itemIndex] = Aluno;

            }
            else
            {
                return null;
            }
            ReescreverArquivo(ListaAlunos);
            return Aluno;
        }
        /*
         No meu método para excluir eu passo apenas o id do aluno que eu quero excluir
        novamente eu listo todos os alunos através da função ListaAlunos() e coloco o resultado numa variável que eu criei e também
        chamei de ListAlunos
        Eu localizo o aluno que eu quero excluir
        Comparo no if (itemIndex >= 0) se o aluno realmente existe
        Se esse aluno existir eu o removo da minha lista
        se o aluno não existir eu retorno false
        Agora que eu já retirei o aluno que eu queria da minha eu vou reescrever a nova lista dentro do meu json sem o aluno que eu 
        queria ter excluído.]
         
         */
        public bool Deletar(int id)
        {
            var ListaAlunos = this.ListaAlunos();
            var itemIndex = ListaAlunos.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                ListaAlunos.RemoveAt(itemIndex);

            }
            else
            {
                return false;
            }
            ReescreverArquivo(ListaAlunos);
            return true;
        }
    }
}
