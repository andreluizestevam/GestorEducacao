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
    public partial class TB117_CODIGO_INTERNACIONAL_DOENCA
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
        /// Exclue o registro da tabela TB117_CODIGO_INTERNACIONAL_DOENCA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB117_CODIGO_INTERNACIONAL_DOENCA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB117_CODIGO_INTERNACIONAL_DOENCA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB117_CODIGO_INTERNACIONAL_DOENCA.</returns>
        public static TB117_CODIGO_INTERNACIONAL_DOENCA Delete(TB117_CODIGO_INTERNACIONAL_DOENCA entity)
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
        public static int SaveOrUpdate(TB117_CODIGO_INTERNACIONAL_DOENCA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB117_CODIGO_INTERNACIONAL_DOENCA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB117_CODIGO_INTERNACIONAL_DOENCA.</returns>
        public static TB117_CODIGO_INTERNACIONAL_DOENCA SaveOrUpdate(TB117_CODIGO_INTERNACIONAL_DOENCA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB117_CODIGO_INTERNACIONAL_DOENCA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB117_CODIGO_INTERNACIONAL_DOENCA.</returns>
        public static TB117_CODIGO_INTERNACIONAL_DOENCA GetByEntityKey(EntityKey entityKey)
        {
            return (TB117_CODIGO_INTERNACIONAL_DOENCA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB117_CODIGO_INTERNACIONAL_DOENCA, ordenados pelo nome "NO_CID".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB117_CODIGO_INTERNACIONAL_DOENCA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB117_CODIGO_INTERNACIONAL_DOENCA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB117_CODIGO_INTERNACIONAL_DOENCA.OrderBy(c => c.NO_CID).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB117_CODIGO_INTERNACIONAL_DOENCA pela chave primária "IDE_CID".
        /// </summary>
        /// <param name="IDE_CID">Id da chave primária</param>
        /// <returns>Entidade TB117_CODIGO_INTERNACIONAL_DOENCA</returns>
        public static TB117_CODIGO_INTERNACIONAL_DOENCA RetornaPelaChavePrimaria(int IDE_CID)
        {
            return (from tb117 in RetornaTodosRegistros()
                    where tb117.IDE_CID == IDE_CID
                    select tb117).FirstOrDefault();
        }

        #endregion
    }
}
