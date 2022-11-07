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
    public partial class TB230_OCORR_PATRIMONIO
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
        /// Exclue o registro da tabela TB230_OCORR_PATRIMONIO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB230_OCORR_PATRIMONIO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB230_OCORR_PATRIMONIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB230_OCORR_PATRIMONIO.</returns>
        public static TB230_OCORR_PATRIMONIO Delete(TB230_OCORR_PATRIMONIO entity)
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
        public static int SaveOrUpdate(TB230_OCORR_PATRIMONIO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB230_OCORR_PATRIMONIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB230_OCORR_PATRIMONIO.</returns>
        public static TB230_OCORR_PATRIMONIO SaveOrUpdate(TB230_OCORR_PATRIMONIO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB230_OCORR_PATRIMONIO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB230_OCORR_PATRIMONIO.</returns>
        public static TB230_OCORR_PATRIMONIO GetByEntityKey(EntityKey entityKey)
        {
            return (TB230_OCORR_PATRIMONIO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB230_OCORR_PATRIMONIO, ordenados pela da de ocorrência "DT_OCORR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB230_OCORR_PATRIMONIO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB230_OCORR_PATRIMONIO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB230_OCORR_PATRIMONIO.OrderBy( o => o.DT_OCORR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB230_OCORR_PATRIMONIO pela chave primária "ID_OCORR_PATR".
        /// </summary>
        /// <param name="ID_OCORR_PATR">Id da chave primária</param>
        /// <returns>Entidade TB230_OCORR_PATRIMONIO</returns>
        public static TB230_OCORR_PATRIMONIO RetornaPelaChavePrimaria(int ID_OCORR_PATR)
        {
            return (from tb230 in RetornaTodosRegistros()
                    where tb230.ID_OCORR_PATR == ID_OCORR_PATR
                    select tb230).FirstOrDefault();
        }

        #endregion
    }
}