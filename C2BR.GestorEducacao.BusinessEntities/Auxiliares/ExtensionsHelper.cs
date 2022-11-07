//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Linq.Dynamic;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.Auxiliares
{
    public static class ExtensionsHelper
    {
        #region Métodos

        public static ObjectQuery<TSource> AsObjectQuery<TSource>(this IQueryable<TSource> source)
        {
            return (ObjectQuery<TSource>)source;
        }

        public static ObjectQuery<TSource> Where<TSource>(this IQueryable<TSource> source, DynamicWhereClauseHelper whereClause)
        {
            if (whereClause.Params.Length > 0)
                return source.Where(whereClause.Predicate, whereClause.Params).AsObjectQuery();
            else
                return source.AsObjectQuery();
        }
        #endregion
    }
}
