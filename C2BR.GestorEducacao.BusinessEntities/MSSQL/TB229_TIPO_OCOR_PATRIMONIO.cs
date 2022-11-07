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
    public partial class TB229_TIPO_OCOR_PATRIMONIO
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
        /// Exclue o registro da tabela TB229_TIPO_OCOR_PATRIMONIO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB229_TIPO_OCOR_PATRIMONIO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB229_TIPO_OCOR_PATRIMONIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB229_TIPO_OCOR_PATRIMONIO.</returns>
        public static TB229_TIPO_OCOR_PATRIMONIO Delete(TB229_TIPO_OCOR_PATRIMONIO entity)
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
        public static int SaveOrUpdate(TB229_TIPO_OCOR_PATRIMONIO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB229_TIPO_OCOR_PATRIMONIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB229_TIPO_OCOR_PATRIMONIO.</returns>
        public static TB229_TIPO_OCOR_PATRIMONIO SaveOrUpdate(TB229_TIPO_OCOR_PATRIMONIO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB229_TIPO_OCOR_PATRIMONIO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB229_TIPO_OCOR_PATRIMONIO.</returns>
        public static TB229_TIPO_OCOR_PATRIMONIO GetByEntityKey(EntityKey entityKey)
        {
            return (TB229_TIPO_OCOR_PATRIMONIO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB229_TIPO_OCOR_PATRIMONIO, ordenados pela descrição "DE_TIPO_OCORR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB229_TIPO_OCOR_PATRIMONIO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB229_TIPO_OCOR_PATRIMONIO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB229_TIPO_OCOR_PATRIMONIO.OrderBy( t => t.DE_TIPO_OCORR ).AsObjectQuery();
        }

        #endregion  

        /// <summary>
        /// Retorna um registro da entidade TB229_TIPO_OCOR_PATRIMONIO pela chave primária "ID_TIPO_OCORR_PATR".
        /// </summary>
        /// <param name="ID_TIPO_OCORR_PATR">Id da chave primária</param>
        /// <returns>Entidade TB229_TIPO_OCOR_PATRIMONIO</returns>
        public static TB229_TIPO_OCOR_PATRIMONIO RetornaPelaChavePrimaria(int ID_TIPO_OCORR_PATR)
        {
            return (from tb229 in RetornaTodosRegistros()
                    where tb229.ID_TIPO_OCORR_PATR == ID_TIPO_OCORR_PATR
                    select tb229).FirstOrDefault();
        }

        #endregion
    }
}