using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS416_EXAME_RESUL
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
        /// Exclue o registro da tabela TBS416_EXAME_RESUL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS416_EXAME_RESUL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS416_EXAME_RESUL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS416_EXAME_RESUL.</returns>
        public static TBS416_EXAME_RESUL Delete(TBS416_EXAME_RESUL entity)
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
        public static int SaveOrUpdate(TBS416_EXAME_RESUL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS416_EXAME_RESUL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS416_EXAME_RESUL.</returns>
        public static TBS416_EXAME_RESUL SaveOrUpdate(TBS416_EXAME_RESUL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS416_EXAME_RESUL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS416_EXAME_RESUL.</returns>
        public static TBS416_EXAME_RESUL GetByEntityKey(EntityKey entityKey)
        {
            return (TBS416_EXAME_RESUL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS416_EXAME_RESUL.
        /// </summary>
        public static ObjectQuery<TBS416_EXAME_RESUL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS416_EXAME_RESUL.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS416_EXAME_RESUL pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS416_EXAME_RESUL</returns>
        public static TBS416_EXAME_RESUL RetornaPelaChavePrimaria(int ID_EXAME_RESUL)
        {
            return (from tbs416 in RetornaTodosRegistros()
                    where tbs416.ID_EXAME_RESUL == ID_EXAME_RESUL
                    select tbs416).FirstOrDefault();
        }

        #endregion
    }
}

