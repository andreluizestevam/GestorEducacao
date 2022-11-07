//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    /// <summary>
    /// Funções de apoio para uso de enums
    /// </summary>
    public static class EnumAuxili
    {
        /// <summary>
        /// Carrega um DropDownList com o atributo Value de um enum no Text e com seu nome no Value
        /// </summary>
        /// <typeparam name="TEnum">Tipo do enum</typeparam>
        /// <param name="ddl">O próprio DropDownList</param>
        public static void Load<TEnum>(this DropDownList ddl)
        {
            ddl.Items.Clear();
            foreach (Enum item in Enum.GetValues(typeof(TEnum)))
                ddl.Items.Add(new ListItem(item.GetValue(), item.ToString()));
        }

        public  enum TipoManutencao
        {
            Pesquisa = 0,
            Alteracao = 1,
            Insercao = 2
        }

        public  enum FkEnderecoSN
        {
            Profissional = 0,
            Usuario = 1,
            Credenciado = 2

        }

        /// <summary>
        /// Carrega um DropDownList com o atributo Value de um enum no Text e com seu nome no Value
        /// </summary>
        /// <typeparam name="TEnum">Tipo do enum</typeparam>
        /// <param name="ddl">O próprio DropDownList</param>
        /// <param name="optionalText">Texto do primeiro item (sem value). Se nulo, não adiciona. </param>
        public static void Load<TEnum>(this DropDownList ddl, string optionalText)
        {
            ddl.Items.Clear();
            foreach (Enum item in Enum.GetValues(typeof(TEnum)))
                ddl.Items.Add(new ListItem(item.GetValue(), item.ToString()));

            if (optionalText != null)
                ddl.Items.Insert(0, new ListItem(optionalText, ""));
        }

        public static TEnum GetEnum<TEnum>(string enumValue)
        {
            foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
                if (item.ToString() == enumValue)
                    return item;

            throw new Exception("Valor não encontrado no Enumerador");
        }
    }

    /// <summary>
    /// Descrição de um valor de um enumerador
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        public string Value { get; set; }
    }
}
