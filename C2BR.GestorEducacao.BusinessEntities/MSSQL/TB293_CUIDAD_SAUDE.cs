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
    public partial class TB293_CUIDAD_SAUDE
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
        /// Exclue o registro da tabela TB293_CUIDAD_SAUDE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB293_CUIDAD_SAUDE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB293_CUIDAD_SAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB293_CUIDAD_SAUDE.</returns>
        public static TB293_CUIDAD_SAUDE Delete(TB293_CUIDAD_SAUDE entity)
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
        public static int SaveOrUpdate(TB293_CUIDAD_SAUDE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB293_CUIDAD_SAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB293_CUIDAD_SAUDE.</returns>
        public static TB293_CUIDAD_SAUDE SaveOrUpdate(TB293_CUIDAD_SAUDE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB293_CUIDAD_SAUDE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB293_CUIDAD_SAUDE.</returns>
        public static TB293_CUIDAD_SAUDE GetByEntityKey(EntityKey entityKey)
        {
            return (TB293_CUIDAD_SAUDE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB293_CUIDAD_SAUDE, ordenados pelo Id "ID_MEDICACAO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB293_CUIDAD_SAUDE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB293_CUIDAD_SAUDE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB293_CUIDAD_SAUDE.OrderBy( c => c.ID_MEDICACAO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB293_CUIDAD_SAUDE pela chave primária "ID_MEDICACAO".
        /// </summary>
        /// <param name="ID_MEDICACAO">Id da chave primária</param>
        /// <returns>Entidade TB293_CUIDAD_SAUDE</returns>
        public static TB293_CUIDAD_SAUDE RetornaPelaChavePrimaria(int ID_MEDICACAO)
        {
            return (from tb293 in RetornaTodosRegistros()
                    where tb293.ID_MEDICACAO == ID_MEDICACAO
                    select tb293).FirstOrDefault();
        }

        #endregion
    }
}