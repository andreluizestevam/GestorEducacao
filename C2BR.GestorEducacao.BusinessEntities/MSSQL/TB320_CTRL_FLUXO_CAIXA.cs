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
    public partial class TB320_CTRL_FLUXO_CAIXA
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
        /// Exclue o registro da tabela TB320_CTRL_FLUXO_CAIXA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB320_CTRL_FLUXO_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB320_CTRL_FLUXO_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB320_CTRL_FLUXO_CAIXA.</returns>
        public static TB320_CTRL_FLUXO_CAIXA Delete(TB320_CTRL_FLUXO_CAIXA entity)
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
        public static int SaveOrUpdate(TB320_CTRL_FLUXO_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB320_CTRL_FLUXO_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB320_CTRL_FLUXO_CAIXA.</returns>
        public static TB320_CTRL_FLUXO_CAIXA SaveOrUpdate(TB320_CTRL_FLUXO_CAIXA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB320_CTRL_FLUXO_CAIXA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB320_CTRL_FLUXO_CAIXA.</returns>
        public static TB320_CTRL_FLUXO_CAIXA GetByEntityKey(EntityKey entityKey)
        {
            return (TB320_CTRL_FLUXO_CAIXA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB320_CTRL_FLUXO_CAIXA, ordenados pela descrição "DE_AGRUP_ATIVEXTRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB320_CTRL_FLUXO_CAIXA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB320_CTRL_FLUXO_CAIXA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB320_CTRL_FLUXO_CAIXA.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB320_CTRL_FLUXO_CAIXA onde o Id "ID_CTRL_FLUXO_CAIXA" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_AGRUP_ATIVEXTRA">Id da chave primária</param>
        /// <returns>Entidade TB320_CTRL_FLUXO_CAIXA</returns>
        public static TB320_CTRL_FLUXO_CAIXA RetornaPelaChavePrimaria(int ID_CTRL_FLUXO_CAIXA)
        {
            return (from tb320 in RetornaTodosRegistros()
                    where tb320.ID_CTRL_FLUXO_CAIXA == ID_CTRL_FLUXO_CAIXA
                    select tb320).FirstOrDefault();
        }

        #endregion
    }
}