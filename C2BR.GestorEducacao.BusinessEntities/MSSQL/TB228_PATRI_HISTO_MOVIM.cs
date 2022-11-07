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
    public partial class TB228_PATRI_HISTO_MOVIM
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
        /// Exclue o registro da tabela TB228_PATRI_HISTO_MOVIM do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB228_PATRI_HISTO_MOVIM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB228_PATRI_HISTO_MOVIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB228_PATRI_HISTO_MOVIM.</returns>
        public static TB228_PATRI_HISTO_MOVIM Delete(TB228_PATRI_HISTO_MOVIM entity)
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
        public static int SaveOrUpdate(TB228_PATRI_HISTO_MOVIM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB228_PATRI_HISTO_MOVIM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB228_PATRI_HISTO_MOVIM.</returns>
        public static TB228_PATRI_HISTO_MOVIM SaveOrUpdate(TB228_PATRI_HISTO_MOVIM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB228_PATRI_HISTO_MOVIM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB228_PATRI_HISTO_MOVIM.</returns>
        public static TB228_PATRI_HISTO_MOVIM GetByEntityKey(EntityKey entityKey)
        {
            return (TB228_PATRI_HISTO_MOVIM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB228_PATRI_HISTO_MOVIM, ordenados pela data de movimentação "DT_MOVIM_PATRI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB228_PATRI_HISTO_MOVIM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB228_PATRI_HISTO_MOVIM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB228_PATRI_HISTO_MOVIM.OrderBy( p => p.DT_MOVIM_PATRI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB228_PATRI_HISTO_MOVIM pela chave primária "IDE_HISTO_MOVIM_PATRI".
        /// </summary>
        /// <param name="IDE_HISTO_MOVIM_PATRI">Id da chave primária</param>
        /// <returns>Entidade TB228_PATRI_HISTO_MOVIM</returns>
        public static TB228_PATRI_HISTO_MOVIM RetornaPelaChavePrimaria(int IDE_HISTO_MOVIM_PATRI)
        {
            return (from tb228 in RetornaTodosRegistros()
                    where tb228.IDE_HISTO_MOVIM_PATRI == IDE_HISTO_MOVIM_PATRI
                    select tb228).FirstOrDefault();
        }

        #endregion
    }
}