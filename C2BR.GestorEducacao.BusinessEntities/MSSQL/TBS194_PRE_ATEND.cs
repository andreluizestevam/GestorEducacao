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
    public partial class TBS194_PRE_ATEND
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
        /// Exclue o registro da tabela TBS194_PRE_ATEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS194_PRE_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS194_PRE_ATEND.</returns>
        public static TBS194_PRE_ATEND Delete(TBS194_PRE_ATEND entity)
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
        public static int SaveOrUpdate(TBS194_PRE_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS194_PRE_ATEND.</returns>
        public static TBS194_PRE_ATEND SaveOrUpdate(TBS194_PRE_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS194_PRE_ATEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS194_PRE_ATEND.</returns>
        public static TBS194_PRE_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS194_PRE_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS194_PRE_ATEND, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS194_PRE_ATEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS194_PRE_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS194_PRE_ATEND.AsObjectQuery();
        }

        public static TBS194_PRE_ATEND RetornarUmRegistro(Expression<Func<TBS194_PRE_ATEND, bool>> predicate) {
            return GestorEntities.CurrentContext.TBS194_PRE_ATEND.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Retorna um registro da entidade TBS194_PRE_ATEND pela chave primária "CO_TIPO_MOV".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS194_PRE_ATEND</returns>
        public static TBS194_PRE_ATEND RetornaPelaChavePrimaria(int ID_PRE_ATEND)
        {
            //return (from tbs194 in RetornaTodosRegistros()
            //        where tbs194.ID_PRE_ATEND == ID_PRE_ATEND
            //        select tbs194).FirstOrDefault();
            
            return GestorEntities.CurrentContext.TBS194_PRE_ATEND.FirstOrDefault(p => p.ID_PRE_ATEND == ID_PRE_ATEND);
        }
        public static bool GravaComplemento(string NU_BATIMENTO, string HR_BATIMENTO, string NU_SATURACAO, string HR_SATURACAO, string DE_SATURACAO)
        {
            DataTable dt = new DataTable();
            BusinessEntities.Auxiliar.SQLDirectAcess tst = new BusinessEntities.Auxiliar.SQLDirectAcess();
            dt = tst.retornacolunas("select max(id_pre_atend) as id_pre_atend from TBS194_PRE_ATEND");
            string id = dt.Rows[0]["id_pre_atend"].ToString();

            String SQL = "update TBS194_PRE_ATEND set NU_BATIMENTO = " + NU_BATIMENTO + ", HR_BATIMENTO = '" + HR_BATIMENTO + "', NU_SATURACAO = " + NU_SATURACAO + ", HR_SATURACAO = '" + HR_SATURACAO + "', DE_SATURACAO = " + DE_SATURACAO + " where id_pre_atend = " + id;

            return tst.InsereAltera(SQL);
        }

        #endregion

    }
}
