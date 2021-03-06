namespace ClubeLeitura.ConsoleApp.Dominio
{
    public class Caixa : EntidadeBase, IValidavel
    {
        public string cor;

        public string etiqueta;

        public Caixa()
        {
        }

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public string Validar()
        {
            return "ESTA_VALIDO";
        }
    }
}
