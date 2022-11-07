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
    public partial class TB138_INFORMATIVOS
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
        /// Exclue o registro da tabela TB138_INFORMATIVOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB138_INFORMATIVOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB138_INFORMATIVOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB138_INFORMATIVOS.</returns>
        public static TB138_INFORMATIVOS Delete(TB138_INFORMATIVOS entity)
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
        public static int SaveOrUpdate(TB138_INFORMATIVOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB138_INFORMATIVOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB138_INFORMATIVOS.</returns>
        public static TB138_INFORMATIVOS SaveOrUpdate(TB138_INFORMATIVOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB138_INFORMATIVOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB138_INFORMATIVOS.</returns>
        public static TB138_INFORMATIVOS GetByEntityKey(EntityKey entityKey)
        {
            return (TB138_INFORMATIVOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB138_INFORMATIVOS, ordenados pelo Id "ID_INFOR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB138_INFORMATIVOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB138_INFORMATIVOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB138_INFORMATIVOS.OrderBy(a => a.ID_INFOR).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB138_INFORMATIVOS pela chave primária "ID_INFOR".
        /// </summary>
        /// <param name="ID_INFOR">Id da chave primária</param>
        /// <returns>Entidade TB138_INFORMATIVOS</returns>
        public static TB138_INFORMATIVOS RetornaPelaChavePrimaria(int ID_INFOR)
        {
            return (from tb138 in RetornaTodosRegistros()
                    where tb138.ID_INFOR == ID_INFOR
                    select tb138).FirstOrDefault();
        }

        #endregion
    }
}
