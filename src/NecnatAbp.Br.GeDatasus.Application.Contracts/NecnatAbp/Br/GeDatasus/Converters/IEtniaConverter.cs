namespace NecnatAbp.Br.GeDatasus.Converters
{
    public interface IEtniaConverter
    {
        GePessoaFisica.EtniaDto FromCodigoCadSus(string codigoEtnia);
    }
}
