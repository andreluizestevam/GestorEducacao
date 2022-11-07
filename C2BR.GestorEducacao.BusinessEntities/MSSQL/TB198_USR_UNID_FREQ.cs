//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB198_USR_UNID_FREQ
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
        /// Exclue o registro da tabela TB198_USR_UNID_FREQ do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB198_USR_UNID_FREQ entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB198_USR_UNID_FREQ na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB198_USR_UNID_FREQ.</returns>
        public static TB198_USR_UNID_FREQ Delete(TB198_USR_UNID_FREQ entity)
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
        public static int SaveOrUpdate(TB198_USR_UNID_FREQ entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB198_USR_UNID_FREQ na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB198_USR_UNID_FREQ.</returns>
        public static TB198_USR_UNID_FREQ SaveOrUpdate(TB198_USR_UNID_FREQ entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB198_USR_UNID_FREQ de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB198_USR_UNID_FREQ.</returns>
        public static TB198_USR_UNID_FREQ GetByEntityKey(EntityKey entityKey)
        {
            return (TB198_USR_UNID_FREQ)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB198_USR_UNID_FREQ, ordenados pelo Id do funcionário "CO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB198_USR_UNID_FREQ de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB198_USR_UNID_FREQ> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB198_USR_UNID_FREQ.OrderBy( u => u.CO_COL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB198_USR_UNID_FREQ pelas chaves primárias "CO_COL", "CO_EMP" e "ID_UNID_FREQ".
        /// </summary>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="ID_UNID_FREQ">Id auto-incremento</param>
        /// <returns>Entidade TB198_USR_UNID_FREQ</returns>
        public static TB198_USR_UNID_FREQ RetornaPelaChavePrimaria(int CO_COL, int CO_EMP, int ID_UNID_FREQ)
        {
            return (from tb198 in RetornaTodosRegistros()
                    where tb198.CO_COL == CO_COL && tb198.CO_EMP == CO_EMP && tb198.ID_UNID_FREQ == ID_UNID_FREQ
                    select tb198).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB198_USR_UNID_FREQ de acordo com o funcionário "CO_EMP", "CO_COL" e a unidade de frequência "CO_EMP_FREQ".
        /// </summary>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="CO_EMP">Id da unidade do funcionário</param>
        /// <param name="CO_EMP_FREQ">Id da unidade de frequência</param>
        /// <returns>Entidade TB198_USR_UNID_FREQ</returns>
        public static TB198_USR_UNID_FREQ RetornaPeloColaboradorUnidadeFreq(int CO_COL, int CO_EMP, int CO_EMP_FREQ)
        {
            return (from tb198 in RetornaTodosRegistros()
                    where tb198.CO_COL == CO_COL && tb198.CO_EMP == CO_EMP && tb198.TB25_EMPRESA.CO_EMP == CO_EMP_FREQ
                    select tb198).OrderBy( u => u.CO_SITU_UNID_FREQ == "A" ? 0 : 1).FirstOrDefault();
        }

        #endregion
    }
}