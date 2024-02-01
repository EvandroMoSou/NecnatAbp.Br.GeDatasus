namespace NecnatAbp.Br.GeDatasus.Converters
{
    public interface ISexoConverter
    {
        GePessoaFisica.Sexo FromCodigoCadSusDoc(string codigoDataSusDoc);
    }
}
