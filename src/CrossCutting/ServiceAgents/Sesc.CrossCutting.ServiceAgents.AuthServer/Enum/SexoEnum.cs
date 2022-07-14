using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Enum
{
    public class SexoEnum
    {
        private SexoEnum(string value) { Value = value; }

        public string Value { get; set; }

        public static SexoEnum Masculino { get { return new SexoEnum("M"); } }
        public static SexoEnum Feminino { get { return new SexoEnum("F"); } }

    }
}
