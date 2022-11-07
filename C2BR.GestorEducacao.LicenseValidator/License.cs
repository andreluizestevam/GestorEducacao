using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2BR.GestorEducacao.LicenseValidator
{
    [Serializable]
    public class License
    {
        public string Cnpj { get; set; }
        public string ContatoCpf { get; set; }
        public DateTime ContatoDataNasc { get; set; }
        public string ContratoNumero { get; set; }
        public DateTime ContratoData { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string BiosName { get; set; }
        public string BiosElementId { get; set; }
        public string BiosVersion { get; set; }
        public bool Local { get; set; }
    }

}
