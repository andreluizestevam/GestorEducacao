using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS417_EXAME_RESUL_ITENS
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
        /// Exclue o registro da tabela TBS417_EXAME_RESUL_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS417_EXAME_RESUL_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS417_EXAME_RESUL_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS417_EXAME_RESUL_ITENS.</returns>
        public static TBS417_EXAME_RESUL_ITENS Delete(TBS417_EXAME_RESUL_ITENS entity)
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
        public static int SaveOrUpdate(TBS417_EXAME_RESUL_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS417_EXAME_RESUL_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS417_EXAME_RESUL_ITENS.</returns>
        public static TBS417_EXAME_RESUL_ITENS SaveOrUpdate(TBS417_EXAME_RESUL_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS417_EXAME_RESUL_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS417_EXAME_RESUL_ITENS.</returns>
        public static TBS417_EXAME_RESUL_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS417_EXAME_RESUL_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS417_EXAME_RESUL_ITENS.
        /// </summary>
        public static ObjectQuery<TBS417_EXAME_RESUL_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS417_EXAME_RESUL_ITENS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS417_EXAME_RESUL_ITENS pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS417_EXAME_RESUL_ITENS</returns>
        public static TBS417_EXAME_RESUL_ITENS RetornaPelaChavePrimaria(int ID_EXAME_RESUL_ITENS)
        {
            return (from tbs417 in RetornaTodosRegistros()
                    where tbs417.ID_EXAME_RESUL_ITENS == ID_EXAME_RESUL_ITENS
                    select tbs417).FirstOrDefault();
        }

        #endregion
    }
}

