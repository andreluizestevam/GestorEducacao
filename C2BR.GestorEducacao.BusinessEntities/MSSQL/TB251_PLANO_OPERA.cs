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
    public partial class TB251_PLANO_OPERA
    {
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
        /// Exclue o registro da tabela TB251_PLANO_OPERA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB251_PLANO_OPERA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB251_PLANO_OPERA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB251_PLANO_OPERA.</returns>
        public static TB251_PLANO_OPERA Delete(TB251_PLANO_OPERA entity)
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
        public static int SaveOrUpdate(TB251_PLANO_OPERA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB251_PLANO_OPERA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB251_PLANO_OPERA.</returns>
        public static TB251_PLANO_OPERA SaveOrUpdate(TB251_PLANO_OPERA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB251_PLANO_OPERA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB251_PLANO_OPERA.</returns>
        public static TB251_PLANO_OPERA GetByEntityKey(EntityKey entityKey)
        {
            return (TB251_PLANO_OPERA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB251_PLANO_OPERA, ordenados pelo nome fantasia "NOM_PLAN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB251_PLANO_OPERA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB251_PLANO_OPERA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB251_PLANO_OPERA.OrderBy(o => o.NOM_PLAN).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB251_PLANO_OPERA pela chave primária "ID_PLAN".
        /// </summary>
        /// <param name="ID_PLAN">Id da chave primária</param>
        /// <returns>Entidade TB251_PLANO_OPERA</returns>
        public static TB251_PLANO_OPERA RetornaPelaChavePrimaria(int ID_PLAN)
        {
            return (from tb251 in RetornaTodosRegistros()
                    where tb251.ID_PLAN == ID_PLAN
                    select tb251).FirstOrDefault();
        }

    }
}
