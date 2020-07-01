using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30

{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/produto.csv";

        /// <summary>
        /// Metodo que mostra se existe alguma pasta
        /// </summary>
        public Produto()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public void Cadastrar(Produto prod)
        {
            string [] linha = new string [] {PrepararLinha(prod)};
            File.AppendAllLines(PATH, linha);
        }
            
            
            //List também pode ser usada, assim como o void, string, bool...
        public List<Produto> Ler()
        {
            List<Produto> prod = new List<Produto>();

            //Vai ler o arquivo .csv com o array e varrer essas linhas
            string[] linhas = File.ReadAllLines(PATH);
            foreach(string linha in linhas)
            {
                string[] dado = linha.Split(";");
                
                //Dado [0]= codigo
                //Dado [1]= Nome
                //Dado [2]= Preço

                Produto p = new Produto();
                p.Codigo = Int32.Parse(Separar(dado[0]));
                p.Nome = Separar(dado[1]);
                p.Preco = float.Parse(Separar(dado[2]));

                prod.Add(p);
            }
        
            prod = prod.OrderBy(z=> z.Nome).ToList();

            return prod;

        }

        public List<Produto> Filtrar(string _nome)
        {
            return Ler().FindAll(x => x.Nome == _nome);
        }

        public void Remover(string _termo)
        {

            //criamos uma lista de linhase faz a função de um backup na memoria do sistema
            List<string> linhas = new List<string>();

            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }

                linhas.RemoveAll(z => z.Contains(_termo));
            }

            using(StreamWriter output = new StreamWriter(PATH))
            {
                output.Write(string.Join(Environment.NewLine, linhas.ToArray()));
            }


        }

            /// <summary>
            /// Metodo que separa o = da string do .csv
            /// </summary>
            /// <param name="dado">Separou a coluna do csv</param>
            /// <returns>string somento com o valor da colna</returns>
        public string Separar(string dado)
        {
            return dado.Split("=")[1];
        }

        private string PrepararLinha(Produto p)
        {
            return $"codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }
    }
}