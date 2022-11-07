using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS415_EXAME_ITENS_REFER
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
        /// Exclue o registro da tabela TBS415_EXAME_ITENS_REFER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS415_EXAME_ITENS_REFER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS415_EXAME_ITENS_REFER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS415_EXAME_ITENS_REFER.</returns>
        public static TBS415_EXAME_ITENS_REFER Delete(TBS415_EXAME_ITENS_REFER entity)
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
        public static int SaveOrUpdate(TBS415_EXAME_ITENS_REFER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS415_EXAME_ITENS_REFER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS415_EXAME_ITENS_REFER.</returns>
        public static TBS415_EXAME_ITENS_REFER SaveOrUpdate(TBS415_EXAME_ITENS_REFER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS415_EXAME_ITENS_REFER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS415_EXAME_ITENS_REFER.</returns>
        public static TBS415_EXAME_ITENS_REFER GetByEntityKey(EntityKey entityKey)
        {
            return (TBS415_EXAME_ITENS_REFER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS415_EXAME_ITENS_REFER.
        /// </summary>
        public static ObjectQuery<TBS415_EXAME_ITENS_REFER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS415_EXAME_ITENS_REFER.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS415_EXAME_ITENS_REFER pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS415_EXAME_ITENS_REFER</returns>
        public static TBS415_EXAME_ITENS_REFER RetornaPelaChavePrimaria(int ID_ITENS_REFER)
        {
            return (from tbs415 in RetornaTodosRegistros()
                    where tbs415.ID_ITENS_REFER == ID_ITENS_REFER
                    select tbs415).FirstOrDefault();
        }

        #endregion
    }
}

