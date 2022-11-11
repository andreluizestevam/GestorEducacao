//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS390_ATEND_AGEND
    {
        #region Métodos Básicos

        /// <summary>
        /// Salva as alterações do contexto na base de dados.
        /// </summary>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        /// <summary>
        /// Exclue o registro da tabela TBS390_ATEND_AGEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS390_ATEND_AGEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS390_ATEND_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS390_ATEND_AGEND.</returns>
        public static TBS390_ATEND_AGEND Delete(TBS390_ATEND_AGEND entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TBS390_ATEND_AGEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS390_ATEND_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS390_ATEND_AGEND.</returns>
        public static TBS390_ATEND_AGEND SaveOrUpdate(TBS390_ATEND_AGEND entity, string NU_PRESSAO = "", string NU_SATURACAO = "")
        {
            SaveOrUpdate(entity, true);

            if (string.IsNullOrEmpty(NU_PRESSAO)){ NU_PRESSAO = "0"; }
            if (string.IsNullOrEmpty(NU_SATURACAO)) { NU_SATURACAO = "0"; }

            TBS390_ATEND_AGEND tmp = new TBS390_ATEND_AGEND();
            tmp = entity;
            BusinessEntities.Auxiliar.SQLDirectAcess dir = new BusinessEntities.Auxiliar.SQLDirectAcess();
            dir.InsereAltera("UPDATE TBS390_ATEND_AGEND SET NU_PRESSAO = '" + NU_PRESSAO + "', NU_SATURACAO = '" + NU_SATURACAO + "' where ID_ATEND_AGEND = '" + entity.EntityKey.EntityKeyValues[0].Value + "'") ;

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS390_ATEND_AGEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS390_ATEND_AGEND.</returns>
        public static TBS390_ATEND_AGEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS390_ATEND_AGEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS390_ATEND_AGEND, ordenados pelo Id da unidade "CO_EMP" e pela data de cadastro "DT_CAD_DOC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW_CONTR_MATRI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS390_ATEND_AGEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS390_ATEND_AGEND.AsObjectQuery();
        }

        /// <summary>
        /// Retornar os registros de TBS390_ATEND_AGEND conforme filtro passado como parametro
        /// </summary>
        public static IQueryable<TBS390_ATEND_AGEND> RetornarRegistros(Expression<Func<TBS390_ATEND_AGEND, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS390_ATEND_AGEND.Where(predicate);
        }

        public static TBS390_ATEND_AGEND RetornaUmRegistro(Expression<Func<TBS390_ATEND_AGEND, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS390_ATEND_AGEND.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Retorna um registro da entidade TBS390_ATEND_AGEND pela chave primária "ID_ATEND_AGEND".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS390_ATEND_AGEND</returns>
        public static TBS390_ATEND_AGEND RetornaPelaChavePrimaria(int ID_ATEND_AGEND)
        {
            return GestorEntities.CurrentContext.TBS390_ATEND_AGEND.FirstOrDefault(p => p.ID_ATEND_AGEND == ID_ATEND_AGEND);
        }
        public static DataTable RetornaCamposNovosTBS390(int? ID_ATEND_AGEND, string CO_ALU)
        {
            BusinessEntities.Auxiliar.SQLDirectAcess dir = new BusinessEntities.Auxiliar.SQLDirectAcess();
            DataTable dt = new DataTable();
            dt = dir.retornacolunas("select NU_PRESSAO, NU_SATURACAO from TBS390_ATEND_AGEND where ID_AGEND_HORAR = '" + ID_ATEND_AGEND + "',' and CO_ALU = '"+ CO_ALU + "'");
            return dt;
        }

        #endregion
    }
}
