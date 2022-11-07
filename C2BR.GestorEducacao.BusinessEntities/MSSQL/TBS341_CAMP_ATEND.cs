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
    public partial class TBS341_CAMP_ATEND
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
        /// Exclue o registro da tabela TBS341_CAMP_ATEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS341_CAMP_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS335_ISDA_TIPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS341_CAMP_ATEND Delete(TBS341_CAMP_ATEND entity)
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
        public static int SaveOrUpdate(TBS341_CAMP_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS336_ISDA_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS341_CAMP_ATEND SaveOrUpdate(TBS341_CAMP_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS341_CAMP_ATEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS341_CAMP_ATEND.</returns>
        public static TBS341_CAMP_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS341_CAMP_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS341_CAMP_ATEND, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS341_CAMP_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS341_CAMP_ATEND.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS341_CAMP_ATEND pela chave primária "ID_CAMP_ATEND".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS341_CAMP_ATEND</returns>
        public static TBS341_CAMP_ATEND RetornaPelaChavePrimaria(int ID_CAMP_ATEND)
        {
            return (from tbs341 in RetornaTodosRegistros()
                    where tbs341.ID_CAMP_ATEND == ID_CAMP_ATEND
                    select tbs341).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS341_CAMP_ATEND pela chave primária "ID_CAMP_ATEND".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS341_CAMP_ATEND</returns>
        public static TBS341_CAMP_ATEND RetornaPeloUsuarioECampanha(int CO_ALU, int ID_CAMPAN)
        {
            return (from tbs341 in RetornaTodosRegistros()
                    where tbs341.CO_ALU == CO_ALU
                    && tbs341.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                    select tbs341).FirstOrDefault();
        }

        #endregion
    }
}
