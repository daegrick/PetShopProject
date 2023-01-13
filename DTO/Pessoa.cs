namespace DTO
{
    [Serializable]
    public class Pessoa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid Ide { get; set; }
        public bool Ativo { get; set; }
    }
}