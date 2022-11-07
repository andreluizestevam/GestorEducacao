//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.Auxiliares
{
    public class DynamicWhereClauseHelper
    {
        #region Propriedades

        public String Predicate { get { return strBuilder.ToString(); } }
        public object[] Params { get { return parametros.ToArray(); } }
        #endregion

        #region Variáveis

        int indiceParametro = 0;
        List<object> parametros = new List<object>();
        StringBuilder strBuilder = new StringBuilder(1000);
        #endregion

        #region Métodos

        void AddParam(string strColuna, string strValor, DataBaseOperators dataBaseOperator)
        {
            if (String.IsNullOrEmpty(strColuna)) throw new ArgumentNullException("column");

            if (!String.IsNullOrEmpty(strValor))
                if (indiceParametro == 0)
                    AddFirstParam(strColuna, strValor);
                else
                    switch (dataBaseOperator)
                    {
                        case DataBaseOperators.Or:
                            strBuilder.AppendFormat(" || {0} = @{1}", strColuna, indiceParametro);
                            parametros.Add(strValor);
                            indiceParametro++;
                            break;
                        case DataBaseOperators.And:
                            strBuilder.AppendFormat(" && {0} = @{1}", strColuna, indiceParametro);
                            parametros.Add(strValor);
                            indiceParametro++;
                            break;
                        default:
                            break;
                    }
        }

        void AddFirstParam(string strColuna, string strValor)
        {
            strBuilder.AppendFormat("{0} = @{1}", strColuna, indiceParametro);
            parametros.Add(strValor);
            indiceParametro++;
        }

        /// <summary>
        /// Add a parameter with Or operator, if no parameter was added yet, it will be added first parameter without operator.
        /// </summary>
        /// <param name="column">The database table column name</param>
        /// <param name="value">The value to peform the Or operation</param>
        public void AddOrEqualsParameter(string strColuna, string strValor)
        {
            AddParam(strColuna, strValor, DataBaseOperators.Or);
        }

        /// <summary>
        /// Add a parameter with Or operator, if no parameter was added yet, it will be added first parameter without operator.
        /// </summary>
        /// <param name="column">The database table column name</param>
        /// <param name="value">The value to peform the And operation</param>
        public void AddAndEqualsParameter(string strColuna, string strValor)
        {
            AddParam(strColuna, strValor, DataBaseOperators.And);
        }
        #endregion        
    }

    #region Enumerador

    enum DataBaseOperators
    {
        Or,
        And
    }
    #endregion
}
