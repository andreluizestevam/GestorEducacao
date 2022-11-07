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
    public partial class TB201_AVAL_MASTER
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
        /// Exclue o registro da tabela TB201_AVAL_MASTER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB201_AVAL_MASTER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB201_AVAL_MASTER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB201_AVAL_MASTER.</returns>
        public static TB201_AVAL_MASTER Delete(TB201_AVAL_MASTER entity)
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
        public static int SaveOrUpdate(TB201_AVAL_MASTER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB201_AVAL_MASTER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB201_AVAL_MASTER.</returns>
        public static TB201_AVAL_MASTER SaveOrUpdate(TB201_AVAL_MASTER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB201_AVAL_MASTER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB201_AVAL_MASTER.</returns>
        public static TB201_AVAL_MASTER GetByEntityKey(EntityKey entityKey)
        {
            return (TB201_AVAL_MASTER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB201_AVAL_MASTER, ordenados pelo Id "NU_AVAL_MASTER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB201_AVAL_MASTER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB201_AVAL_MASTER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB201_AVAL_MASTER.OrderBy( a => a.NU_AVAL_MASTER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB201_AVAL_MASTER pela chave primária "NU_AVAL_MASTER".
        /// </summary>
        /// <param name="NU_AVAL_MASTER">Id da chave primária</param>
        /// <returns>Entidade TB201_AVAL_MASTER</returns>
        public static TB201_AVAL_MASTER RetornaPelaChavePrimaria(int NU_AVAL_MASTER)
        {
            return (from tb201 in RetornaTodosRegistros()
                    where tb201.NU_AVAL_MASTER == NU_AVAL_MASTER
                    select tb201).FirstOrDefault();
        }

        #endregion
    }
}
