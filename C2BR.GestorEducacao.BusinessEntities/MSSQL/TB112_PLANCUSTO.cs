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
    public partial class TB112_PLANCUSTO
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
        /// Exclue o registro da tabela TB112_PLANCUSTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB112_PLANCUSTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB112_PLANCUSTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB112_PLANCUSTO.</returns>
        public static TB112_PLANCUSTO Delete(TB112_PLANCUSTO entity)
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
        public static int SaveOrUpdate(TB112_PLANCUSTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB112_PLANCUSTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB112_PLANCUSTO.</returns>
        public static TB112_PLANCUSTO SaveOrUpdate(TB112_PLANCUSTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB112_PLANCUSTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB112_PLANCUSTO.</returns>
        public static TB112_PLANCUSTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB112_PLANCUSTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB112_PLANCUSTO, ordenados pelo ano de referência "CO_ANO_REF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB112_PLANCUSTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB112_PLANCUSTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB112_PLANCUSTO.OrderBy( p => p.CO_ANO_REF ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB112_PLANCUSTO pelas chaves primárias "CO_ANO_REF", "CO_CENT_CUSTO", "CO_EMP", "CO_SEQU_PC" e "CO_GRUP_CTA".
        /// </summary>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <param name="CO_CENT_CUSTO">Id do centro de custo</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_SEQU_PC">Id sequencial</param>
        /// <param name="CO_GRUP_CTA">Id do grupo de contas</param>
        /// <returns>Entidade TB112_PLANCUSTO</returns>
        public static TB112_PLANCUSTO RetornaPelaChavePrimaria(int CO_ANO_REF, int CO_CENT_CUSTO, int CO_EMP, int CO_SEQU_PC, int CO_GRUP_CTA)
        {
            return (from tb112 in RetornaTodosRegistros()
                    where tb112.CO_ANO_REF == CO_ANO_REF && tb112.CO_CENT_CUSTO == CO_CENT_CUSTO && tb112.CO_EMP == CO_EMP
                    && tb112.CO_SEQU_PC == CO_SEQU_PC && tb112.CO_GRUP_CTA == CO_GRUP_CTA
                    select tb112).FirstOrDefault();
        }

        #endregion
    }
}