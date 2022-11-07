using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class DadosLogin
    {
        public int idUsuarioApp { get; set; }
        public int CodUsuario { get; set; }
        public string desLogin { get; set; }
        public string colabNome { get; set; }
        public string colabApeli { get; set; }
        public string co_class_funcion { get; set; }
        public string funcCol { get; set; }
        public string colabClassFunci
        {
            get
            {
                string classFunc = C2BR.GestorEducacao.UI.Library.Auxiliares.AuxiliGeral.GetNomeClassificacaoFuncional(this.co_class_funcion);
                classFunc = (classFunc.Replace("Terapeuta Ocupacional", "Terapeuta"));

                string nomeCol = "";

                //Tratamento para coletar os três primeiros nomes do colaborador
                #region Tratamento

                if (!string.IsNullOrEmpty(this.colabNome))
                {
                    var nome = colabNome.Split(' ');
                    string nome1 = nome[0];
                    string nome2 = "";
                    string nome3 = "";

                    //Segundo nome
                    try
                    {
                        nome2 = nome[1];
                    }
                    catch (Exception e)
                    {
                    }

                    //Terceiro nome
                    try
                    {
                        nome3 = nome[2];
                    }
                    catch (Exception e)
                    {

                    }

                    nomeCol = nome1 + " " + nome2 + " " + nome3;
                }
                #endregion

                if (!string.IsNullOrEmpty(this.funcCol))
                {
                    //return classFunc + " - " + nomeCol.ToUpper();
                    return (this.funcCol.Length > 14 ? this.funcCol.Substring(0, 14) : this.funcCol) + " - " + nomeCol.ToUpper();
                }
                else
                    return " - ";
            }
        }
        public int Co_emp { get; set; }
        public string Unidade { get; set; }
        public string Senha { get; set; }
        public string CO_TIPO_USER { get; set; }
        public int ORG_CODIGO_ORGAO { get; set; }
    }
}