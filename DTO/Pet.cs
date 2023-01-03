namespace DTO
{
    [Serializable]
    public class Pet 
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public int QuantidadeVacinas { get; set; }
        public Guid RacaIde { get; set; }
        public int CodigoRaca { get; set; }
        public Guid Ide { get; set; }
        public bool Ativo { get; set; }
        public bool IsAdotado { get; set; }
    }
}