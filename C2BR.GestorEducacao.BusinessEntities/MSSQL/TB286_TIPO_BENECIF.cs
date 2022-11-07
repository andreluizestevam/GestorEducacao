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
    public partial class TB286_TIPO_BENECIF
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
        /// Exclue o registro da tabela TB286_TIPO_BENECIF do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB286_TIPO_BENECIF entity, bool saveChanges) 
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB286_TIPO_BENECIF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB286_TIPO_BENECIF.</returns>
        public static TB286_TIPO_BENECIF Delete(TB286_TIPO_BENECIF entity) 
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
        public static int SaveOrUpdate(TB286_TIPO_BENECIF entity, bool saveChanges) 
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB286_TIPO_BENECIF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB286_TIPO_BENECIF.</returns>
        public static TB286_TIPO_BENECIF SaveOrUpdate(TB286_TIPO_BENECIF entity) 
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB286_TIPO_BENECIF de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB286_TIPO_BENECIF.</returns>
        public static TB286_TIPO_BENECIF GetByEntityKey(EntityKey entityKey)
        {
            return (TB286_TIPO_BENECIF)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB286_TIPO_BENECIF, ordenados pela descrição "DE_BENEFICIO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB286_TIPO_BENECIF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB286_TIPO_BENECIF> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB286_TIPO_BENECIF.OrderBy( t => t.DE_BENEFICIO ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB286_TIPO_BENECIF pela chave primária "ID_BENEFICIO".
        /// </summary>
        /// <param name="ID_BENEFICIO">Id da chave primária</param>
        /// <returns>Entidade TB286_TIPO_BENECIF</returns>
        public static TB286_TIPO_BENECIF RetornaPelaChavePrimaria(int ID_BENEFICIO) 
        {
            return (from tb286 in RetornaTodosRegistros()
                    where tb286.ID_BENEFICIO == ID_BENEFICIO
                    select tb286).FirstOrDefault();
        }
        #endregion
    }
}
