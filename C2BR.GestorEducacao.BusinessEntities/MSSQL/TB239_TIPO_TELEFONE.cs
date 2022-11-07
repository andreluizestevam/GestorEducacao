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
    public partial class TB239_TIPO_TELEFONE
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
        /// Exclue o registro da tabela TB239_TIPO_TELEFONE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB239_TIPO_TELEFONE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB239_TIPO_TELEFONE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB239_TIPO_TELEFONE.</returns>
        public static TB239_TIPO_TELEFONE Delete(TB239_TIPO_TELEFONE entity)
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
        public static int SaveOrUpdate(TB239_TIPO_TELEFONE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB239_TIPO_TELEFONE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB239_TIPO_TELEFONE.</returns>
        public static TB239_TIPO_TELEFONE SaveOrUpdate(TB239_TIPO_TELEFONE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB239_TIPO_TELEFONE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB239_TIPO_TELEFONE.</returns>
        public static TB239_TIPO_TELEFONE GetByEntityKey(EntityKey entityKey)
        {
            return (TB239_TIPO_TELEFONE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB239_TIPO_TELEFONE, ordenados pelo nome "NM_TIPO_TELEFONE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB239_TIPO_TELEFONE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB239_TIPO_TELEFONE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB239_TIPO_TELEFONE.OrderBy( t => t.NM_TIPO_TELEFONE ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB239_TIPO_TELEFONE pela chave primária "ID_TIPO_TELEFONE".
        /// </summary>
        /// <param name="ID_TIPO_TELEFONE">Id da chave primária</param>
        /// <returns>Entidade TB239_TIPO_TELEFONE</returns>
        public static TB239_TIPO_TELEFONE RetornaPelaChavePrimaria(int ID_TIPO_TELEFONE)
        {
            return (from tb239 in RetornaTodosRegistros()
                    where tb239.ID_TIPO_TELEFONE == ID_TIPO_TELEFONE
                    select tb239).FirstOrDefault();
        }

        #endregion
    }
}