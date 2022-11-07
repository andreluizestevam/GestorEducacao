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
    public partial class TB200_EQUIV_NOTA_CONCEITO
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
        /// Exclue o registro da tabela TB200_EQUIV_NOTA_CONCEITO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB200_EQUIV_NOTA_CONCEITO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB200_EQUIV_NOTA_CONCEITO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB200_EQUIV_NOTA_CONCEITO.</returns>
        public static TB200_EQUIV_NOTA_CONCEITO Delete(TB200_EQUIV_NOTA_CONCEITO entity)
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
        public static int SaveOrUpdate(TB200_EQUIV_NOTA_CONCEITO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB200_EQUIV_NOTA_CONCEITO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB200_EQUIV_NOTA_CONCEITO.</returns>
        public static TB200_EQUIV_NOTA_CONCEITO SaveOrUpdate(TB200_EQUIV_NOTA_CONCEITO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB200_EQUIV_NOTA_CONCEITO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB200_EQUIV_NOTA_CONCEITO.</returns>
        public static TB200_EQUIV_NOTA_CONCEITO GetByEntityKey(EntityKey entityKey)
        {
            return (TB200_EQUIV_NOTA_CONCEITO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB200_EQUIV_NOTA_CONCEITO, ordenados pela sigla "CO_SIGLA_CONCEITO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB200_EQUIV_NOTA_CONCEITO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB200_EQUIV_NOTA_CONCEITO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB200_EQUIV_NOTA_CONCEITO.OrderBy( e => e.CO_SIGLA_CONCEITO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB200_EQUIV_NOTA_CONCEITO pelas chaves primárias "ORG_CODIGO_ORGAO" e "CO_SIGLA_CONCEITO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_SIGLA_CONCEITO">Sigla do conceito</param>
        /// <returns>Entidade TB200_EQUIV_NOTA_CONCEITO</returns>
        public static TB200_EQUIV_NOTA_CONCEITO RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO, string CO_SIGLA_CONCEITO)
        {
            return (from tb200 in RetornaTodosRegistros()
                    where tb200.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO && tb200.CO_SIGLA_CONCEITO == CO_SIGLA_CONCEITO
                    select tb200).FirstOrDefault();
        }
        
        #endregion
    }
}
