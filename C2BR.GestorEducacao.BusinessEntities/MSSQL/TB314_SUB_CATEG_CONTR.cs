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
    public partial class TB314_SUB_CATEG_CONTR
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
        /// Exclue o registro da tabela TB314_SUB_CATEG_CONTR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB314_SUB_CATEG_CONTR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB314_SUB_CATEG_CONTR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB314_SUB_CATEG_CONTR.</returns>
        public static TB314_SUB_CATEG_CONTR Delete(TB314_SUB_CATEG_CONTR entity)
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
        public static int SaveOrUpdate(TB314_SUB_CATEG_CONTR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB314_SUB_CATEG_CONTR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB314_SUB_CATEG_CONTR.</returns>
        public static TB314_SUB_CATEG_CONTR SaveOrUpdate(TB314_SUB_CATEG_CONTR entity)
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB314_SUB_CATEG_CONTR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB314_SUB_CATEG_CONTR.</returns>
        public static TB314_SUB_CATEG_CONTR GetByEntityKey(EntityKey entityKey) 
        {
            return (TB314_SUB_CATEG_CONTR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB314_SUB_CATEG_CONTR, ordenados pelo nome "_NM_SUB_CATEG_CONTR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB314_SUB_CATEG_CONTR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB314_SUB_CATEG_CONTR> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB314_SUB_CATEG_CONTR.OrderBy(g => g.NM_SUB_CATEG_CONTR).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB314_SUB_CATEG_CONTR pela chave primária "ID_SUB_CATEG_CONTR".
        /// </summary>
        /// <param name="ID_SUB_CATEG_CONTR">Id da chave primária</param>
        /// <returns>Entidade TB314_SUB_CATEG_CONTR</returns>
        public static TB314_SUB_CATEG_CONTR RetornaPelaChavePrimaria(int ID_SUB_CATEG_CONTR) 
        {
            return (from tb314 in RetornaTodosRegistros()
                    where tb314.ID_SUB_CATEG_CONTR == ID_SUB_CATEG_CONTR
                    select tb314).FirstOrDefault();
        }
        #endregion
    }
}
