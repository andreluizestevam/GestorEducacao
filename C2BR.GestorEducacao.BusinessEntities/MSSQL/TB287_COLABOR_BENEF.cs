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
    public partial class TB287_COLABOR_BENEF
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
        /// Exclue o registro da tabela TB287_COLABOR_BENEF do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB287_COLABOR_BENEF entity, bool saveChanges) 
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB287_COLABOR_BENEF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB287_COLABOR_BENEF.</returns>
        public static TB287_COLABOR_BENEF Delete(TB287_COLABOR_BENEF entity) 
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
        public static int SaveOrUpdate(TB287_COLABOR_BENEF entity, bool saveChanges) 
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB287_COLABOR_BENEF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB287_COLABOR_BENEF.</returns>
        public static TB287_COLABOR_BENEF SaveOrUpdate(TB287_COLABOR_BENEF entity) 
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB287_COLABOR_BENEF de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB287_COLABOR_BENEF.</returns>
        public static TB287_COLABOR_BENEF GetByEntityKey(EntityKey entityKey)
        {
            return (TB287_COLABOR_BENEF)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB287_COLABOR_BENEF, ordenados pelo Id "ID_COLABOR_BENEF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB287_COLABOR_BENEF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB287_COLABOR_BENEF> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB287_COLABOR_BENEF.OrderBy( c => c.ID_COLABOR_BENEF ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB287_COLABOR_BENEF pela chave primária "ID_COLABOR_BENEF".
        /// </summary>
        /// <param name="ID_COLABOR_BENEF">Id da chave primária</param>
        /// <returns>Entidade TB287_COLABOR_BENEF</returns>
        public static TB287_COLABOR_BENEF RetornaPelaChavePrimaria(int ID_COLABOR_BENEF) 
        {
            return (from tb287 in RetornaTodosRegistros()
                    where tb287.ID_COLABOR_BENEF == ID_COLABOR_BENEF
                    select tb287).FirstOrDefault();
        }
        #endregion
    }
}
