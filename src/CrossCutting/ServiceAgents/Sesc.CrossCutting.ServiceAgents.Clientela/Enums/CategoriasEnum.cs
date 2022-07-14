using System;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Enums
{
   
    public class CategoriaSca
    {

        const string TrabalhadorDoComercio = "Trabalhador do Comércio";
        const string BeneficiarioDoComercio = "Beneficiário do Comércio";
        const string Conveniado = "Convêniado";
        const string PublicoGeral = "Público Geral";

        static public string DescricaoCategoriaSca(int codCategoria)
        {
            switch (codCategoria)
            {
                case 1:
                case 3:
                case 4:
                case 5:
                case 9:
                case 11:
                case 18:
                case 21:
                case 22:
                case 28:
                    return "Trabalhador do Comércio";

                case 2:
                case 10:
                case 14:
                case 16:
                case 19:
                case 23:
                case 26:
                case 31:
                    return "Beneficiário do Comércio";

                case 7:
                case 8:
                case 12:
                case 13:
                case 20:
                case 27:
                case 30:
                    return "Convêniado";

                default:
                    return "Público Geral";
            }
        }
    }
}
