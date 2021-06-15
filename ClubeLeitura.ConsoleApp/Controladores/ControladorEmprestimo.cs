using ClubeLeitura.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Controladores
{
    public class ControladorEmprestimo
    {
        private List<Emprestimo> registros;
        private int ultimoId;

        public ControladorEmprestimo()
        {
            registros = new List<Emprestimo>();
        }

        public string RegistrarEmprestimo(Amigo amigo, Revista revista, DateTime data)
        {
            Emprestimo emprestimo = new Emprestimo(amigo, revista, data);

            string resultadoValidacao = emprestimo.Validar();

            if (resultadoValidacao == "EMPRESTIMO_VALIDO")
            {
                amigo.RegistrarEmprestimo(emprestimo);
                revista.RegistrarEmprestimo(emprestimo);

                emprestimo.id = NovoIdEmprestimo();
                registros.Add(emprestimo);
            }

            return resultadoValidacao;
        }

        internal bool RegistrarDevolucao(int idEmprestimo, DateTime data)
        {
            Emprestimo emprestimo = (Emprestimo)SelecionarEmprestimoPorId(idEmprestimo);

            emprestimo.Fechar(data);

            return true;
        }

        public bool ExisteEmprestimoComEsteId(int idEmprestimo)
        {
            return SelecionarEmprestimoPorId(idEmprestimo) != null;
        }

        internal List<Emprestimo> SelecionarEmprestimosEmAberto()
        {
            List<Emprestimo> emprestimosEmAberto = new List<Emprestimo>(QtdEmprestimosEmAberto());

            List<Emprestimo> todosEmprestimos = SelecionarTodosEmprestimos();

            foreach (Emprestimo e in todosEmprestimos)
            {
                if (e.estaAberto)
                {
                    emprestimosEmAberto.Add(e);
                }
            }

            return emprestimosEmAberto;
        }

        private int QtdEmprestimosEmAberto()
        {
            List<Emprestimo> todosEmprestimos = SelecionarTodosEmprestimos();

            int numeroEmprestimosEmAberto = 0;

            foreach (Emprestimo emprestimo in todosEmprestimos)
            {
                if (emprestimo.estaAberto)
                {
                    numeroEmprestimosEmAberto++;
                }
            }

            return numeroEmprestimosEmAberto;
        }

        internal List<Emprestimo> SelecionarEmprestimosFechados(int mes)
        {
            List<Emprestimo> emprestimosFechados = new List<Emprestimo>(QtdEmprestimosFechados(mes));

            List<Emprestimo> todosEmprestimos = SelecionarTodosEmprestimos();

            foreach (Emprestimo e in todosEmprestimos)
            {
                if (e.EstaFechado() && e.Mes == mes)
                {
                    emprestimosFechados.Add(e);
                }
            }

            return emprestimosFechados;
        }

        private int QtdEmprestimosFechados(int mes)
        {
            List<Emprestimo> todosEmprestimos = SelecionarTodosEmprestimos();

            int numeroEmprestimosFechado = 0;

            foreach (Emprestimo emprestimo in todosEmprestimos)
            {
                if (emprestimo.EstaFechado() && emprestimo.Mes == mes)
                {
                    numeroEmprestimosFechado++;
                }
            }

            return numeroEmprestimosFechado;
        }

        public Emprestimo SelecionarEmprestimoPorId(int id)
        {
            Emprestimo selecionado = null;

            foreach (Emprestimo item in registros)
            {
                if (item.id == id)
                {
                    selecionado = item;
                }
            }
            return selecionado;
        }

        public List<Emprestimo> SelecionarTodosEmprestimos()
        {
            return registros;
        }

        protected int NovoIdEmprestimo()
        {
            return ++ultimoId;
        }

    }
}