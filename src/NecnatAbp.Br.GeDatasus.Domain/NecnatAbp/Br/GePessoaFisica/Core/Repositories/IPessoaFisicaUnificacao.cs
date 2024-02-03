namespace NecnatAbp.Br.GePessoaFisica
{
    public partial interface IPessoaFisicaUnificacao<TKey> where TKey : struct
    {
        TKey PessoaFisicaId { get; set; }
        TKey? PessoaFisicaIdUnificacao { get; set; }
    }
}
