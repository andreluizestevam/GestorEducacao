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
    public partial class TB67_MOTIVTRANC
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
        /// Exclue o registro da tabela TB67_MOTIVTRANC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB67_MOTIVTRANC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB67_MOTIVTRANC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB67_MOTIVTRANC.</returns>
        public static TB67_MOTIVTRANC Delete(TB67_MOTIVTRANC entity)
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
        public static int SaveOrUpdate(TB67_MOTIVTRANC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB67_MOTIVTRANC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB67_MOTIVTRANC.</returns>
        public static TB67_MOTIVTRANC SaveOrUpdate(TB67_MOTIVTRANC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB67_MOTIVTRANC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB67_MOTIVTRANC.</returns>
        public static TB67_MOTIVTRANC GetByEntityKey(EntityKey entityKey)
        {
            return (TB67_MOTIVTRANC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB67_MOTIVTRANC, ordenados pela descrição "DE_MOTI_TRAN_MAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB67_MOTIVTRANC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB67_MOTIVTRANC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB67_MOTIVTRANC.OrderBy( m => m.DE_MOTI_TRAN_MAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB67_MOTIVTRANC pela chave primária "CO_MOTI_TRAN_MAT".
        /// </summary>
        /// <param name="CO_MOTI_TRAN_MAT">Id da chave primária</param>
        /// <returns>Entidade TB67_MOTIVTRANC</returns>
        public static TB67_MOTIVTRANC RetornaPelaChavePrimaria(int CO_MOTI_TRAN_MAT)
        {
            return (from tb67 in RetornaTodosRegistros()
                    where tb67.CO_MOTI_TRAN_MAT == CO_MOTI_TRAN_MAT
                    select tb67).FirstOrDefault();
        }

        #endregion
    }
}
