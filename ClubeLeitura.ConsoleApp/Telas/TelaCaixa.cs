using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaCaixa : TelaCadastravel<Caixa>, ICadastravel
    {
        private readonly ControladorCaixa controladorCaixa;
        public TelaCaixa(ControladorCaixa controlador): base ("Cadastro de Caixas", controlador)
        {
            controladorCaixa = controlador;
        }

        public override Caixa ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            return new Caixa(cor, etiqueta);
        }

        public override void ApresentarTabela(List<Caixa> caixas)
        {
            string configuracaColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Etiqueta", "Cor");

            foreach (Caixa caixa in caixas)
            {
                Console.WriteLine(configuracaColunasTabela, caixa.id, caixa.etiqueta, caixa.cor);
            }
        }
    }
}