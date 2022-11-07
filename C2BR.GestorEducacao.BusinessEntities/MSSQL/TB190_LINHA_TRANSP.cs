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
    public partial class TB190_LINHA_TRANSP
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
        /// Exclue o registro da tabela TB190_LINHA_TRANSP do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB190_LINHA_TRANSP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB190_LINHA_TRANSP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB190_LINHA_TRANSP.</returns>
        public static TB190_LINHA_TRANSP Delete(TB190_LINHA_TRANSP entity)
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
        public static int SaveOrUpdate(TB190_LINHA_TRANSP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB190_LINHA_TRANSP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB190_LINHA_TRANSP.</returns>
        public static TB190_LINHA_TRANSP SaveOrUpdate(TB190_LINHA_TRANSP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB190_LINHA_TRANSP de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB190_LINHA_TRANSP.</returns>
        public static TB190_LINHA_TRANSP GetByEntityKey(EntityKey entityKey)
        {
            return (TB190_LINHA_TRANSP)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB190_LINHA_TRANSP, ordenados pela descrição "NO_TITUL_LINHA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB190_LINHA_TRANSP de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB190_LINHA_TRANSP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB190_LINHA_TRANSP.OrderBy(t => t.NO_TITUL_LINHA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB190_LINHA_TRANSP pela chaves primária "ID_LINHA_TRANSP".
        /// </summary>
        /// <param name="ID_LINHA_TRANSP">Id da chave primária</param>
        /// <returns>Entidade TB190_LINHA_TRANSP</returns>
        public static TB190_LINHA_TRANSP RetornaPeloCoTipoDoc(int ID_LINHA_TRANSP)
        {
            return (from tb190 in RetornaTodosRegistros()
                    where tb190.ID_LINHA_TRANSP == ID_LINHA_TRANSP
                    select tb190).FirstOrDefault();
        }

        #endregion
    }
}