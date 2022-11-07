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
    public partial class TB286_MOVIM_TRANSF_FUNCI
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
        /// Exclue o registro da tabela TB286_MOVIM_TRANSF_FUNCI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB286_MOVIM_TRANSF_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB286_MOVIM_TRANSF_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB286_MOVIM_TRANSF_FUNCI.</returns>
        public static TB286_MOVIM_TRANSF_FUNCI Delete(TB286_MOVIM_TRANSF_FUNCI entity)
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
        public static int SaveOrUpdate(TB286_MOVIM_TRANSF_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB286_MOVIM_TRANSF_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB286_MOVIM_TRANSF_FUNCI.</returns>
        public static TB286_MOVIM_TRANSF_FUNCI SaveOrUpdate(TB286_MOVIM_TRANSF_FUNCI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB286_MOVIM_TRANSF_FUNCI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB286_MOVIM_TRANSF_FUNCI.</returns>
        public static TB286_MOVIM_TRANSF_FUNCI GetByEntityKey(EntityKey entityKey)
        {
            return (TB286_MOVIM_TRANSF_FUNCI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB286_MOVIM_TRANSF_FUNCI, ordenados pela data de cadastro "DT_CADAST".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB286_MOVIM_TRANSF_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB286_MOVIM_TRANSF_FUNCI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB286_MOVIM_TRANSF_FUNCI.OrderBy( m => m.DT_CADAST ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB286_MOVIM_TRANSF_FUNCI pela chave primária "ID_MOVIM_TRANSF_FUNCI".
        /// </summary>
        /// <param name="ID_MOVIM_TRANSF_FUNCI">Id da chave primária</param>
        /// <returns>Entidade TB286_MOVIM_TRANSF_FUNCI</returns>
        public static TB286_MOVIM_TRANSF_FUNCI RetornaPelaChavePrimaria(int ID_MOVIM_TRANSF_FUNCI)
        {
            return (from tb286 in RetornaTodosRegistros()
                    where tb286.ID_MOVIM_TRANSF_FUNCI == ID_MOVIM_TRANSF_FUNCI
                    select tb286).FirstOrDefault();
        }

        #endregion
    }
}