namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class EntidadeBase
    {
        public int id;
    }

    public interface IValidavel
    {
        string Validar();
    }
}