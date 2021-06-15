namespace ClubeLeitura.ConsoleApp.Telas
{
    public interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void Excluir();

        bool VisualizarRegistros(TipoVisualizacao tipo);

        string ObterOpcao();
    }
}
