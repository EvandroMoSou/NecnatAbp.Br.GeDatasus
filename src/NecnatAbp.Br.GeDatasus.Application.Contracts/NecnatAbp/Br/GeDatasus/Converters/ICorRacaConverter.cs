namespace NecnatAbp.Br.GeDatasus.Converters
{
    public interface ICorRacaConverter
    {
        GePessoaFisica.CorRaca FromCodigoCadSus(string codigoDataSus);
    }
}
