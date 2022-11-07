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
    public partial class TB21_TIPOCAL
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
        /// Exclue o registro da tabela TB21_TIPOCAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB21_TIPOCAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB21_TIPOCAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB21_TIPOCAL.</returns>
        public static TB21_TIPOCAL Delete(TB21_TIPOCAL entity)
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
        public static int SaveOrUpdate(TB21_TIPOCAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB21_TIPOCAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB21_TIPOCAL.</returns>
        public static TB21_TIPOCAL SaveOrUpdate(TB21_TIPOCAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB21_TIPOCAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB21_TIPOCAL.</returns>
        public static TB21_TIPOCAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB21_TIPOCAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB21_TIPOCAL, ordenados pelo nome "NO_TPCAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB21_TIPOCAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB21_TIPOCAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB21_TIPOCAL.OrderBy( t => t.NO_TPCAL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB21_TIPOCAL pela chave primária "CO_TPCAL".
        /// </summary>
        /// <param name="CO_TPCAL">Id da chave primária</param>
        /// <returns>Entidade TB21_TIPOCAL</returns>
        public static TB21_TIPOCAL RetornaPelaChavePrimaria(int CO_TPCAL)
        {
            return (from tb21 in RetornaTodosRegistros()
                    where tb21.CO_TPCAL == CO_TPCAL
                    select tb21).FirstOrDefault();
        }

        #endregion
    }
}
