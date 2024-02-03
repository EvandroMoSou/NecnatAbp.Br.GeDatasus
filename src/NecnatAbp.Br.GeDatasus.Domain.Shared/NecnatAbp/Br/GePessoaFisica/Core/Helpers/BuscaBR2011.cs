using NecnatAbp.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NecnatAbp.Br.GePessoaFisica.Helpers
{
    public partial class BuscaBR2011 : IBuscaFonetica
    {
        public string? Fonetizar(string? s)
        {
            if (s == null)
                return null;

            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            s = s.ToUpper();

            s = Regex.Replace(s, "Ç", "S");

            s = s.RemoveAccents();

            s = s.OnlyLettersDigitsOrSpace();

            s = s.RemoveDuplicateSpaces();

            var dict = new Dictionary<string, string>();
            dict["BL|BR"] = "B";
            dict["PH"] = "F";
            dict["GL|GR|MG|NG|RG"] = "G";
            dict["Y"] = "I";
            dict["GE|GI|RJ|MJ"] = "J";
            dict["CA|CO|CU|CK|Q"] = "K";
            dict["N"] = "M";
            dict["AO|AUM|GM|MD|OM|ON"] = "M";
            dict["PR"] = "P";
            dict["L"] = "R";
            dict["CE|CI|CH|CS|RS|TS|X|Z"] = "S";
            dict["TR|TL"] = "T";
            dict["CT|RT|ST|PT"] = "T";
            dict[@"\b[UW]"] = "V";
            dict["RM"] = "SM";
            dict[@"[MRS]+\b"] = "";
            dict["[AEIOUH]"] = "";

            foreach (string iRegex in dict.Keys)
                s = Regex.Replace(s, iRegex, dict[iRegex]);

            s = Regex.Replace(s, "(.)(?<=\\1\\1)", string.Empty);

            return s;
        }
    }
}
