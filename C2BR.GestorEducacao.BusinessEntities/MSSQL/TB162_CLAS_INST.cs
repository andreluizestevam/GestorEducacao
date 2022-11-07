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
    public partial class TB162_CLAS_INST
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
        /// Exclue o registro da tabela TB162_CLAS_INST do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB162_CLAS_INST entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB162_CLAS_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB162_CLAS_INST.</returns>
        public static TB162_CLAS_INST Delete(TB162_CLAS_INST entity)
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
        public static int SaveOrUpdate(TB162_CLAS_INST entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB162_CLAS_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB162_CLAS_INST.</returns>
        public static TB162_CLAS_INST SaveOrUpdate(TB162_CLAS_INST entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB162_CLAS_INST de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB162_CLAS_INST.</returns>
        public static TB162_CLAS_INST GetByEntityKey(EntityKey entityKey)
        {
            return (TB162_CLAS_INST)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB162_CLAS_INST, ordenados pelo nome "NO_CLAS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB162_CLAS_INST de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB162_CLAS_INST> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB162_CLAS_INST.OrderBy( c => c.NO_CLAS ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB162_CLAS_INST pela chave primária "CO_CLAS".
        /// </summary>
        /// <param name="CO_CLAS">Id da chave primária</param>
        /// <returns>Entidade TB162_CLAS_INST</returns>
        public static TB162_CLAS_INST RetornaPelaChavePrimaria(int CO_CLAS)
        {
            return (from tb162 in RetornaTodosRegistros()
                    where tb162.CO_CLAS == CO_CLAS
                    select tb162).FirstOrDefault();
        }

        #endregion
    }
}