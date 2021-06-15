﻿using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public class TelaEmprestimo : TelaBase
    {
        private readonly TelaRevista telaRevista;
        private readonly ControladorRevista controladorRevista;
        private readonly TelaAmigo telaAmigo;
        private readonly ControladorAmigo controladorAmigo;
        private readonly ControladorEmprestimo controladorEmprestimo;

        public TelaEmprestimo(
            ControladorEmprestimo ctrlEmprestimo,
            ControladorAmigo ctrlAmigo,
            ControladorRevista ctrlRevista,
            TelaRevista tlRevista, TelaAmigo tlAmigo) : base("Controle de Empréstimos")
        {
            controladorAmigo = ctrlAmigo;
            controladorRevista = ctrlRevista;
            controladorEmprestimo = ctrlEmprestimo;

            telaRevista = tlRevista;
            telaAmigo = tlAmigo;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para registrar empréstimos");
            Console.WriteLine("Digite 2 para registrar devoluções");
            Console.WriteLine("Digite 3 para visualizar empréstimos em aberto");
            Console.WriteLine("Digite 4 para visualizar empréstimos fechados em um mês");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarEmprestimo()
        {
            ConfigurarTela("Registro de Empréstimos...");

            bool temRevistas = telaRevista.VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRevistas == false)
                return;

            Console.Write("\nDigite o id da revista: ");
            int idRevista = Convert.ToInt32(Console.ReadLine());

            Revista revistaEncontrada = controladorRevista.SelecionarRegistroPorId(idRevista);
            if (revistaEncontrada == default(Revista))
            {
                ApresentarMensagem("Nenhuma revista foi encontrado com este id: " + idRevista, TipoMensagem.Erro);
                RegistrarEmprestimo();
                return;
            }

            Console.WriteLine();

            bool temAmigos = telaAmigo.VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temAmigos == false)
                return;

            Console.Write("\nDigite o id do amiguinho: ");
            int idAmigo = Convert.ToInt32(Console.ReadLine());

            Amigo amigoEncontrado = controladorAmigo.SelecionarRegistroPorId(idAmigo);
            if (amigoEncontrado == default(Amigo))
            {
                ApresentarMensagem("Nenhum amiguinho foi encontrado com este id: " + idAmigo, TipoMensagem.Erro);
                RegistrarEmprestimo();
                return;
            }

            Amigo amigo = controladorAmigo.SelecionarRegistroPorId(idAmigo);
            Revista revista = controladorRevista.SelecionarRegistroPorId(idRevista);

            Console.Write("Digite a data do empréstimo: ");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());

            string resultadoValidacao =
                controladorEmprestimo.RegistrarEmprestimo(amigo, revista, dataEmprestimo);

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
                ApresentarMensagem("Empréstimo realizado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
            }
        }

        public void RegistrarDevolucao()
        {
            ConfigurarTela("Registro de Devoluções...");

            bool temEmprestimosEmAberto = VisualizarEmprestimosAbertos();

            if (temEmprestimosEmAberto == false)
                return;

            Console.WriteLine();

            Console.Write("Digite o id do empréstimo: ");
            int idEmprestimo = Convert.ToInt32(Console.ReadLine());

            bool emprestimoEncontrado = controladorEmprestimo.ExisteEmprestimoComEsteId(idEmprestimo);
            if (emprestimoEncontrado == false)
            {
                ApresentarMensagem("Nenhum empréstimo foi encontrado com este id: " + idEmprestimo, TipoMensagem.Erro);
                RegistrarDevolucao();
                return;
            }

            Console.Write("Digite a data da devolução: ");
            DateTime dataDevolucao = Convert.ToDateTime(Console.ReadLine());

            bool conseguiuDevolver = controladorEmprestimo.RegistrarDevolucao(idEmprestimo, dataDevolucao);
            if (conseguiuDevolver)
            {
                ApresentarMensagem("Devolução realizada com sucesso", TipoMensagem.Sucesso);
            }
        }

        public bool VisualizarEmprestimosAbertos()
        {
            ConfigurarTela("Visualizando empréstimos em aberto...");

            Emprestimo[] emprestimos = controladorEmprestimo.SelecionarEmprestimosEmAberto();

            if (emprestimos.Length == 0)
            {
                ApresentarMensagem("Nenhum empréstimo em aberto", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            Console.WriteLine();

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Data de Empréstimo", "Amiguinho", "Revistinha");

            foreach (Emprestimo emprestimo in emprestimos)
            {
                Console.WriteLine(configuracaColunasTabela, emprestimo.id, emprestimo.dataEmprestimo,
                    emprestimo.amiguinho.nome, emprestimo.revistinha.nome);
            }

            return true;
        }

        public bool VisualizarEmprestimosFechados()
        {
            ConfigurarTela("Visualizando empréstimos fechados");

            Console.Write("Digite o número do mês: ");
            int numeroMes = Convert.ToInt32(Console.ReadLine());

            List<Emprestimo> emprestimos = controladorEmprestimo.SelecionarEmprestimosFechados(numeroMes);

            if (emprestimos.Count == 0)
            {
                ApresentarMensagem("Nenhum empréstimo fechado neste mês", TipoMensagem.Atencao);
                return false;
            }

            string configuracaColunasTabela = "{0,-10} | {1,-25} | {2,-25} | {3,-25}";

            Console.WriteLine();

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Data de Devolução", "Amiguinho", "Revistinha");

            foreach (Emprestimo emprestimo in emprestimos)
            {
                Console.WriteLine(configuracaColunasTabela, emprestimo.id, emprestimo.dataDevolucao,
                    emprestimo.amiguinho.nome, emprestimo.revistinha.nome);
            }

            return true;
        }
    }
}
