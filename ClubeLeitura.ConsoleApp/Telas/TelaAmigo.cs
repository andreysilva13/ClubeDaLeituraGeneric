using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaAmigo : TelaCadastravel<Amigo>, ICadastravel
    {
        private readonly ControladorAmigo controladorAmigo;

        public TelaAmigo (ControladorAmigo controlador) : base ("Cadastro de Amigos", controlador)
        {
            controladorAmigo = controlador;
        }

        public override Amigo ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o nome do amiguinho: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do amiguinho: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite da onde é o amiguinho: ");
            string deOndeEh = Console.ReadLine();

            return new Amigo(nome, nomeResponsavel, telefone, deOndeEh);
        }

        public override void ApresentarTabela(List<Amigo> amigos)
        {
            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Local");

            foreach (Amigo amigo in amigos)
            {
                Console.WriteLine(configuracaoColunasTabela, amigo.id, amigo.nome, amigo.deOndeEh);
            }
        }
    }
}
