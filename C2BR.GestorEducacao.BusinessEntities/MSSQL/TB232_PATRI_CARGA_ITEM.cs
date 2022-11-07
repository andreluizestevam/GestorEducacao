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
    public partial class TB232_PATRI_CARGA_ITEM
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
        /// Exclue o registro da tabela TB232_PATRI_CARGA_ITEM do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB232_PATRI_CARGA_ITEM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB232_PATRI_CARGA_ITEM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB232_PATRI_CARGA_ITEM.</returns>
        public static TB232_PATRI_CARGA_ITEM Delete(TB232_PATRI_CARGA_ITEM entity)
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
        public static int SaveOrUpdate(TB232_PATRI_CARGA_ITEM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB232_PATRI_CARGA_ITEM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB232_PATRI_CARGA_ITEM.</returns>
        public static TB232_PATRI_CARGA_ITEM SaveOrUpdate(TB232_PATRI_CARGA_ITEM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB232_PATRI_CARGA_ITEM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB232_PATRI_CARGA_ITEM.</returns>
        public static TB232_PATRI_CARGA_ITEM GetByEntityKey(EntityKey entityKey)
        {
            return (TB232_PATRI_CARGA_ITEM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB232_PATRI_CARGA_ITEM, ordenados pela data de movimentação "DH_CARGA_PATR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB232_PATRI_CARGA_ITEM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB232_PATRI_CARGA_ITEM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB232_PATRI_CARGA_ITEM.OrderBy(p => p.DH_CARGA_PATR).AsObjectQuery();
        }
        /// <summary>
        /// Retorna um registro da entidade TB232_PATRI_CARGA_ITEM pela chave primária "COD_PATR".
        /// </summary>
        /// <param name="COD_PATR">Id da chave primária</param>
        /// <returns>Entidade TB232_PATRI_CARGA_ITEM</returns>
        public static TB232_PATRI_CARGA_ITEM RetornaPelaChavePrimaria(decimal ID_CARGA_PATR)
        {
            return (from tb232 in RetornaTodosRegistros()
                    where tb232.ID_CARGA_PATR == ID_CARGA_PATR
                    select tb232).FirstOrDefault();
        }
        #endregion

        #endregion
    }
}