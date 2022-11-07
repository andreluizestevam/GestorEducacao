//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB045_NOS_NUM
    {
        #region Métodos

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
        /// Exclue o registro da tabela TB045_NOS_NUM do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB045_NOS_NUM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB045_NOS_NUM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB045_NOS_NUM.</returns>
        public static TB045_NOS_NUM Delete(TB045_NOS_NUM entity)
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
        public static int SaveOrUpdate(TB045_NOS_NUM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB045_NOS_NUM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB045_NOS_NUM.</returns>
        public static TB045_NOS_NUM SaveOrUpdate(TB045_NOS_NUM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB045_NOS_NUM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB045_NOS_NUM.</returns>
        public static TB045_NOS_NUM GetByEntityKey(EntityKey entityKey)
        {
            return (TB045_NOS_NUM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB045_NOS_NUM.
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB045_NOS_NUM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB045_NOS_NUM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB045_NOS_NUM.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB045_NOS_NUM pelas chaves primárias "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" e "CO_NOS_NUM".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <param name="DT_CAD_DOC">Data de cadastro do documento</param>
        /// <param name="CO_NOS_NUM">Nosso Número</param>
        /// <returns>Entidade TB079_HIST_ALUNO</returns>
        public static TB045_NOS_NUM RetornaPelaChavePrimaria(int CO_EMP, string NU_DOC, int NU_PAR, DateTime DT_CAD_DOC, string CO_NOS_NUM)
        {
            return (from tb045 in RetornaTodosRegistros()
                    where tb045.CO_EMP == CO_EMP
                    && tb045.NU_DOC == NU_DOC
                    && tb045.NU_PAR == NU_PAR
                    && tb045.DT_CAD_DOC == DT_CAD_DOC
                    && tb045.CO_NOS_NUM == CO_NOS_NUM
                    select tb045).FirstOrDefault();
        }

        public static TB045_NOS_NUM RetornaPeloTitulo(TB47_CTA_RECEB tb47)
        {
            return RetornaPelaChavePrimaria(tb47.CO_EMP, tb47.NU_DOC, tb47.NU_PAR, tb47.DT_CAD_DOC, tb47.CO_NOS_NUM);
        }

        public static TB045_NOS_NUM RetornaPeloTitulo(TBS47_CTA_RECEB tbs47)
        {
            return RetornaPelaChavePrimaria(tbs47.CO_EMP, tbs47.NU_DOC, tbs47.NU_PAR, tbs47.DT_CAD_DOC, tbs47.CO_NOS_NUM);
        }

        #endregion
    }
}
