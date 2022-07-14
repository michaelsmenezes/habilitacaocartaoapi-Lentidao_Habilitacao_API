using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AntiCorruption.Sca2Habilitacao
{
    public class PessoaAntiCorruption
    {
        static private readonly IDictionary<int, int> ParentescoSca2Habilitacao = new Dictionary<int, int>()
        {
            { 1, 1 },
            { 2, 12 },
            { 3, 2 },
            { 4, 7 },
            { 5, 3 },
            { 6, 8 },
            { 7, 4 },
            { 8, 5 },
            { 9, 9 },
            { 10, 10 },
            { 11, 6 },
            { 12, 6 },
            { 13, 11 },
        };

        static private readonly IDictionary<int, int> EstadoCivilSca2Habilitacao = new Dictionary<int, int>()
        {
            { 0, 1 },
            { 1, 2 },
            { 2, 3 },
            { 3, 4 },
            { 4, 6 },
            { 5, 5 }
        };

        static private readonly IDictionary<int, int> EscolaridadeSca2Habilitacao = new Dictionary<int, int>()
        {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 4 },
            { 5, 5 },
            { 6, 6 },
            { 7, 7 },
            { 8, 8 },
            { 9, 9 },
            { 10, 10 },
            { 11, 11 },
            { 12, 13 },
            { 13, 13 }
        };

        static private readonly IDictionary<int, string> SexoSca2Habilitacao = new Dictionary<int, string>()
        {
            { 0, "M" },
            { 1, "F" }
        };

        static private readonly int DefaultCategoriaHabilitacao = 4;

        static private readonly IDictionary<int, int> CategoriaSca2Habilitacao = new Dictionary<int, int>()
        {
            { 1, 1 },
            { 3, 1 },
            { 4, 1 },
            { 5, 1 },
            { 9, 1 },
            { 11, 1 },
            { 18, 1 },
            { 21, 1 },
            { 22, 1 },
            { 28, 1 },
            { 2, 2 },
            { 10, 2 },
            { 14, 2 },
            { 16, 2 },
            { 19, 2 },
            { 23, 2 },
            { 26, 2 },
            { 31, 2 },
            { 7, 3 },
            { 8, 3 },
            { 12, 3 },
            { 13, 3 },
            { 20, 3 },
            { 27, 3 },
            { 30, 3 }
        };

        static public string SexoFromSca2Habilitacao(int keySca)
        {
            string keyHabilitacao = "";
            try
            {
                SexoSca2Habilitacao.TryGetValue(keySca, out keyHabilitacao);
            }
            catch (Exception e){}

            return keyHabilitacao;
        }

        static public int ParentescoFromSca2Habilitacao(int keySca)
        {
            int keyHabilitacao = 0;
            try
            {
                ParentescoSca2Habilitacao.TryGetValue(keySca, out keyHabilitacao);
            } catch (Exception e){}

            return keyHabilitacao;
        }

        static public int EscolaridadeFromSca2Habilitacao(int keySca)
        {
            int keyHabilitacao = 0;
            try
            {
                EscolaridadeSca2Habilitacao.TryGetValue(keySca, out keyHabilitacao);
            }
            catch (Exception e) { }

            return keyHabilitacao;
        }

        static public int EstadoCivilFromSca2Habilitacao(int keySca)
        {
            int keyHabilitacao = 0;
            try
            {
                EstadoCivilSca2Habilitacao.TryGetValue(keySca, out keyHabilitacao);
            }
            catch (Exception e) { }

            return keyHabilitacao;
        }

        static public int CategoriaFromSca2Habilitacao(int keySca)
        {
            int keyHabilitacao = 0;
            try
            {
                if (!CategoriaSca2Habilitacao.TryGetValue(keySca, out keyHabilitacao))
                {
                    keyHabilitacao = DefaultCategoriaHabilitacao;
                }
            }
            catch (Exception e) {
                keyHabilitacao = DefaultCategoriaHabilitacao;
            }

            return keyHabilitacao;
        }
    }
}
