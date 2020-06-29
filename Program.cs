using System;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p1 = new Produto();
            p1.Codigo = 4;
            p1.Nome = "Motorola";
            p1.Preco = 1200;
            p1.Cadastrar(p1);
        }
    }
}
