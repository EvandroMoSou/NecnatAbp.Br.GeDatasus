using NecnatAbp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecnatAbp.Br.GePessoaFisica
{
    public static class AbpGePessoaFisicaDbProperties
    {
        public static string DbTablePrefix { get; set; } = NecnatAbpCommonDbProperties.DbTablePrefix;

        public static string? DbSchema { get; set; } = NecnatAbpCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "AbpHierarchyManagement";
    }
}
