using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaRevista : TelaCadastravel<Revista>, ICadastravel
    { 
        private readonly TelaCaixa telaCaixa;
        private readonly ControladorCaixa controladorCaixa;        

        private readonly ControladorRevista controladorRevista;

        public TelaRevista(ControladorRevista ctrlRevista, ControladorCaixa ctrlCaixa, TelaCaixa tlCaixa) : base ("Cadastro de Revistas", ctrlRevista)
        {
            controladorRevista = ctrlRevista;
            telaCaixa = tlCaixa;
            controladorCaixa = ctrlCaixa;
        }

        public override Revista ObterRegistro(TipoAcao tipoAcao)
        {
            telaCaixa.VisualizarRegistros(TipoVisualizacao.Pesquisando);

            Console.Write("\nDigite o número da caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());

            Caixa numeroEncontrado = controladorCaixa.SelecionarRegistroPorId(idCaixa);
            if (numeroEncontrado == default(Caixa))
            {
                ApresentarMensagem("Nenhuma revista foi encontrada com este número: "
                    + idCaixa, TipoMensagem.Erro);

                ConfigurarTela($"{tipoAcao} uma revista...");

                return ObterRegistro(tipoAcao);
            }

            Caixa caixa = controladorCaixa.SelecionarRegistroPorId(idCaixa);

            Console.Write("Digite a nome da revista: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite o número de edição da revista: ");
            int numeroEdicao = Convert.ToInt32(Console.ReadLine());

            return new Revista(nome, colecao, numeroEdicao, caixa);
        }

        public override void ApresentarTabela(List<Revista> revistas)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Coleção", "Caixa");

            foreach (Revista revista in revistas)
            {
                Console.WriteLine(configuracaColunasTabela, revista.id, revista.nome,
                    revista.colecao, revista.caixa.cor);
            }
        }   
    }
}
