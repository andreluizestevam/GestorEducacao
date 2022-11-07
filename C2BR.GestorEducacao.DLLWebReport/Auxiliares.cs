//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.DLLRelatorioWeb
{
    public class Auxiliares
    {
        private static string lURLRelatorioWeb = "net.tcp://localhost:1234/C2BRDLLRelatorioWeb/";
        private static string lIdentFunc;
        private static string lNomeCliente;

        public static string URLRelatorioWeb
        {
            get { return lURLRelatorioWeb; }
        }

        public static string IdentFunc
        {
            get { return lIdentFunc; }
            set { lIdentFunc = value; }
        }

        public static string NomeCliente
        {
            get { return lNomeCliente; }
            set { lNomeCliente = value; }
        } 

        /// <summary>
        /// Método que retorna o nome do relatório gerado no formato "NomeRelatorio_AnoMesDiaHoraMinutoSegundo.pdf"
        /// </summary>
        /// <param name="strNomeRelatorio">Nome do relatório</param>
        /// <returns>String com o nome do relatório gerado no formato "NomeRelatorio_AnoMesDiaHoraMinutoSegundo.pdf"</returns>
        public static string GeraNomeRelatorio(string strNomeRelatorio)
        {
//--------> Gera o nome do arquivo do relatório no padrão "NomeRelatorio_AnoMesDiaHoraMinutoSegundo.pdf
            return strNomeRelatorio + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Minute.ToString() + ".pdf";
        }

        /// <summary>
        /// Retorna o caminho do relatório criado
        /// </summary>
        /// <param name="strCaminhoPDF">Caminho do relatório</param>
        /// <returns></returns>
        public static string RetornaCaminhoRelatorioPDF(string strCaminhoPDF)
        {
            return strCaminhoPDF;
        }
    }
}
