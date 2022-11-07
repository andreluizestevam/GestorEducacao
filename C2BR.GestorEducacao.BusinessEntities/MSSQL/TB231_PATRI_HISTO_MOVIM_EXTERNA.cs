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
    public partial class TB231_PATRI_HISTO_MOVIM_EXTERNA
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
        /// Exclue o registro da tabela TB231_PATRI_HISTO_MOVIM_EXTERNA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB231_PATRI_HISTO_MOVIM_EXTERNA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB231_PATRI_HISTO_MOVIM_EXTERNA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB231_PATRI_HISTO_MOVIM_EXTERNA.</returns>
        public static TB231_PATRI_HISTO_MOVIM_EXTERNA Delete(TB231_PATRI_HISTO_MOVIM_EXTERNA entity)
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
        public static int SaveOrUpdate(TB231_PATRI_HISTO_MOVIM_EXTERNA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB231_PATRI_HISTO_MOVIM_EXTERNA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB231_PATRI_HISTO_MOVIM_EXTERNA.</returns>
        public static TB231_PATRI_HISTO_MOVIM_EXTERNA SaveOrUpdate(TB231_PATRI_HISTO_MOVIM_EXTERNA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB231_PATRI_HISTO_MOVIM_EXTERNA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB231_PATRI_HISTO_MOVIM_EXTERNA.</returns>
        public static TB231_PATRI_HISTO_MOVIM_EXTERNA GetByEntityKey(EntityKey entityKey)
        {
            return (TB231_PATRI_HISTO_MOVIM_EXTERNA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB231_PATRI_HISTO_MOVIM_EXTERNA, ordenados pela data de movimentação "DT_MOVIM_PATRI_EXT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB231_PATRI_HISTO_MOVIM_EXTERNA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB231_PATRI_HISTO_MOVIM_EXTERNA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB231_PATRI_HISTO_MOVIM_EXTERNA.OrderBy( p => p.DT_MOVIM_PATRI_EXT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB231_PATRI_HISTO_MOVIM_EXTERNA pela chave primária "IDE_HISTO_MOVIM_PATRI_EXT".
        /// </summary>
        /// <param name="IDE_HISTO_MOVIM_PATRI_EXT">Id da chave primária</param>
        /// <returns>Entidade TB231_PATRI_HISTO_MOVIM_EXTERNA</returns>
        public static TB231_PATRI_HISTO_MOVIM_EXTERNA RetornaPelaChavePrimaria(int IDE_HISTO_MOVIM_PATRI_EXT)
        {
            return (from tb231 in RetornaTodosRegistros()
                    where tb231.IDE_HISTO_MOVIM_PATRI_EXT == IDE_HISTO_MOVIM_PATRI_EXT
                    select tb231).FirstOrDefault();
        }

        #endregion
    }
}