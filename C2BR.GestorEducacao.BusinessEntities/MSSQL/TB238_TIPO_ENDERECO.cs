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
    public partial class TB238_TIPO_ENDERECO
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
        /// Exclue o registro da tabela TB238_TIPO_ENDERECO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB238_TIPO_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB238_TIPO_ENDERECO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB238_TIPO_ENDERECO.</returns>
        public static TB238_TIPO_ENDERECO Delete(TB238_TIPO_ENDERECO entity)
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
        public static int SaveOrUpdate(TB238_TIPO_ENDERECO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB238_TIPO_ENDERECO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB238_TIPO_ENDERECO.</returns>
        public static TB238_TIPO_ENDERECO SaveOrUpdate(TB238_TIPO_ENDERECO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB238_TIPO_ENDERECO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB238_TIPO_ENDERECO.</returns>
        public static TB238_TIPO_ENDERECO GetByEntityKey(EntityKey entityKey)
        {
            return (TB238_TIPO_ENDERECO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB238_TIPO_ENDERECO, ordenados pelo nome "NM_TIPO_ENDERECO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB238_TIPO_ENDERECO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB238_TIPO_ENDERECO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB238_TIPO_ENDERECO.OrderBy( t => t.NM_TIPO_ENDERECO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB238_TIPO_ENDERECO pela chave primária "ID_TIPO_ENDERECO".
        /// </summary>
        /// <param name="ID_TIPO_ENDERECO">Id da chave primária</param>
        /// <returns>Entidade TB238_TIPO_ENDERECO</returns>
        public static TB238_TIPO_ENDERECO RetornaPelaChavePrimaria(int ID_TIPO_ENDERECO)
        {
            return (from tb238 in RetornaTodosRegistros()
                    where tb238.ID_TIPO_ENDERECO == ID_TIPO_ENDERECO
                    select tb238).FirstOrDefault();
        }

        #endregion
    }
}