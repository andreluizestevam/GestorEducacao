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
    public partial class TB310_REFER_CONTEUDO
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
        /// Exclue o registro da tabela TB310_REFER_CONTEUDO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB310_REFER_CONTEUDO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB310_REFER_CONTEUDO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB310_REFER_CONTEUDO.</returns>
        public static TB310_REFER_CONTEUDO Delete(TB310_REFER_CONTEUDO entity)
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
        public static int SaveOrUpdate(TB310_REFER_CONTEUDO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB310_REFER_CONTEUDO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB310_REFER_CONTEUDO.</returns>
        public static TB310_REFER_CONTEUDO SaveOrUpdate(TB310_REFER_CONTEUDO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB310_REFER_CONTEUDO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB310_REFER_CONTEUDO.</returns>
        public static TB310_REFER_CONTEUDO GetByEntityKey(EntityKey entityKey)
        {
            return (TB310_REFER_CONTEUDO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB310_REFER_CONTEUDO, ordenados pelo nome "NO_TITUL_REFER_CONTE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB310_REFER_CONTEUDO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB310_REFER_CONTEUDO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB310_REFER_CONTEUDO.OrderBy(d => d.NO_TITUL_REFER_CONTE).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB310_REFER_CONTEUDO pela chave primária "ID_CONTEUDO".
        /// </summary>
        /// <param name="ID_CONTEUDO">Id da chave primária</param>
        /// <returns>Entidade TB310_REFER_CONTEUDO</returns>
        public static TB310_REFER_CONTEUDO RetornaPelaChavePrimaria(int ID_REFER_CONTE)
        {
            return (from tb310 in RetornaTodosRegistros()
                    where tb310.ID_REFER_CONTE == ID_REFER_CONTE
                    select tb310).FirstOrDefault();
        }

        #endregion
    }
}