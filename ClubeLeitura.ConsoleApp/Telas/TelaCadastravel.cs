using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.Controladores;
using ClubeLeitura.ConsoleApp.Dominio;

namespace ClubeLeitura.ConsoleApp.Telas
{
    public abstract class TelaCadastravel<T> : TelaBase where T : EntidadeBase, IValidavel
    {
        protected Controlador<T> controlador;

        public TelaCadastravel(string titulo, Controlador<T> controlador) : base(titulo)
        {
            this.controlador = controlador;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo registro...");

            T item = ObterRegistro(TipoAcao.Inserindo);

            string resultadoValidacao = controlador.Adicionar(item);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Registro inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um registro...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registro que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T numeroEncontrado = controlador.SelecionarRegistroPorId(id);
            if (numeroEncontrado == default(T))
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            T t = ObterRegistro(TipoAcao.Inserindo);

            string resultadoValidacao = controlador.Editar(id, t);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Registro editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void Excluir()
        {
            ConfigurarTela("Excluindo um registros...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do registros que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            T numeroEncontrado = controlador.SelecionarRegistroPorId(id);
            if (numeroEncontrado == default(T))
            {
                ApresentarMensagem("Nenhum registro foi encontrado com este número: " + id, TipoMensagem.Erro);
                Excluir();
                return;
            }

            bool conseguiuExcluir = controlador.Excluir(id, numeroEncontrado);

            if (conseguiuExcluir)
                ApresentarMensagem("Registro excluído com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir o registro", TipoMensagem.Erro);
                Excluir();
            }
        }

        protected void MontarCabecalhoTabela(string configuracaoColunasTabela, params object[] colunas)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(configuracaoColunasTabela, colunas);

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();
        }

        protected void ApresentarMensagem(string mensagem, TipoMensagem tm)
        {
            switch (tm)
            {
                case TipoMensagem.Sucesso:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case TipoMensagem.Atencao:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                case TipoMensagem.Erro:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        protected void ConfigurarTela(string subtitulo)
        {
            Console.Clear();

            Console.WriteLine();

            Console.WriteLine(subtitulo);

            Console.WriteLine();
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("VISUALIZANDO REGISTROS");

            var registros = controlador.SelecionarTodosRegistros();

            if (registros.Count == 0)
            {
                ApresentarMensagem("Não há registros cadastrados", TipoMensagem.Erro);
                return false;
            }

            ApresentarTabela(registros);

            return true;
        }

        public abstract T ObterRegistro(TipoAcao tipoAcao);
        public abstract void ApresentarTabela(List<T> registros);
    }
}
