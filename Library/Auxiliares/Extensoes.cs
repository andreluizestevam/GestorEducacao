//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Reflection;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public static class Extensoes
    {
        #region Propriedades

        public const string ABERTO = "A";
        public const string CANCELADO = "C";
        #endregion


        #region Métodos

        #region Métodos GridView

        /// <summary>
        /// Retorna linha da grid
        /// </summary>
        /// <param name="gridView">Grid</param>
        /// <param name="rowIndex">Index da linha</param>
        /// <returns>Linha da grid informada</returns>
        public static GridViewRow GetRow(this GridView gridView, int rowIndex)
        {
            GridViewRow resultRow = null;

            if (rowIndex >= 0 && rowIndex <= gridView.PageSize)
                resultRow = gridView.Rows[rowIndex];

            return resultRow;
        }

        /// <summary>
        /// Retorna o valor da Coluna selecionada
        /// </summary>
        /// <param name="tableCells">Tabela</param>
        /// <param name="cellHeaderText">Cabeçalho da coluna</param>
        /// <returns>Retorna o valor da coluna solicitada</returns>
        public static string GetCellValue(this TableCellCollection tableCells, string cellHeaderText)
        {
            string cellValue = null;
            cellHeaderText = cellHeaderText.ToLower();

            for (int i = 0; i < tableCells.Count; i++)
                if (cellHeaderText.Equals(((DataControlField)((DataControlFieldCell)(tableCells[i])).ContainingField).HeaderText.ToLower()))
                    cellValue = tableCells[i].Text;

            return cellValue;
        }
        #endregion

        #region Métodos DropDown
        /// <summary>
        /// Seleciona o DropDownList pelo texto
        /// </summary>
        /// <param name="ddl">O DropDownList</param>
        /// <param name="text">Texto a ser selecionado</param>
        /// <returns>True se foi encontrado o texto, caso contrário, false</returns>
        public static bool SelectByText(this DropDownList ddl, string text)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text == text)
                {
                    ddl.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Métodos Enum

        /// <summary>
        /// Obtém o atributo valor de um enum
        /// </summary>
        /// <param name="enumeration">O próprio enum</param>
        /// <returns>O valor (atributo)</returns>
        public static string GetValue(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((EnumDescriptionAttribute)attrs[0]).Value;
            }

            return enumeration.ToString();
        }
        #endregion   
     
        #region Métodos Tratamento Texto

        /// <summary>
        /// Converte o texto para o formato minúsculas com primeira maiúsculas
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>Texto minúsculo com as primeiras letras de cada palavra maiúscula</returns>
        public static string Capitalize(this string text)
        {
            if (string.IsNullOrEmpty(text) || text.ToLower() == "de" || text.ToLower() == "da" || text.ToLower() == "do"
                 || text.ToLower() == "das" || text.ToLower() == "dos")
                return text;
            if (text.Split(' ').Length == 1)
            {
                if (text.Length == 1)
                    return text.ToUpper();
                else
                    return text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1).ToLower();
            }
            else
            {
                string temp = String.Empty;
                string[] words = text.Split(' ');

                foreach (string word in words)
                    temp += " " + word.Capitalize();

                return temp.Substring(1);
            }
        }

        /// <summary>
        /// Remove a acentuação do texto
        /// </summary>
        /// <param name="text">Texto</param>
        /// <returns>Texto sem acentos</returns>
        public static string RemoveAcentuacoes(this string text)
        {
            string comAcentos = "áàâãéêíóôõúüç";
            string semAcentos = "aaaaeeiooouuc";
            string retorno = text;

            for (int i = 0; i < comAcentos.Length; i++)
            {
                retorno = retorno.Replace(comAcentos.ToLower()[i], semAcentos.ToLower()[i]);
                retorno = retorno.Replace(comAcentos.ToUpper()[i], semAcentos.ToUpper()[i]);
            }

            return retorno;
        }
        #endregion

        #region Métodos do Linq

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keys)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (T element in source)
            {
                if (seenKeys.Add(keys(element)))
                    yield return element;
            }
        }

        #endregion

        #endregion
    }

    #region Métodos Tratamento Decimal
    /// <summary>
    /// Extensões de decimal
    /// </summary>
    public static class DecimalExtension
    {
        /// <summary>
        /// Transformar decimal em valor monetário no padrão brasileiro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna a string do valor em padrão brasileiro de real</returns>
        public static string toReal(this Decimal valor)
        {
            return valor.ToString("N2");
        }
    }
    #endregion

    #region Métodos Tratamento Int64
    /// <summary>
    /// Extensões de Inteiros
    /// </summary>
    public static class Int64Extension
    {
        /// <summary>
        /// Formata para o padrão do nire com dez digitos
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string toNire(this Int32 valor)
        {
            return valor.ToString("0000000000");
        }
    }

    #endregion
}
