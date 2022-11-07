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
    public partial class TB38_CTA_PAGAR
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
        /// Exclue o registro da tabela TB38_CTA_PAGAR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB38_CTA_PAGAR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB38_CTA_PAGAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB38_CTA_PAGAR.</returns>
        public static TB38_CTA_PAGAR Delete(TB38_CTA_PAGAR entity)
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
        public static int SaveOrUpdate(TB38_CTA_PAGAR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB38_CTA_PAGAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB38_CTA_PAGAR.</returns>
        public static TB38_CTA_PAGAR SaveOrUpdate(TB38_CTA_PAGAR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB38_CTA_PAGAR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB38_CTA_PAGAR.</returns>
        public static TB38_CTA_PAGAR GetByEntityKey(EntityKey entityKey)
        {
            return (TB38_CTA_PAGAR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB38_CTA_PAGAR, ordenados pelo Id da unidade "CO_EMP" e pelo código "CO_CON_DESFIX".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB38_CTA_PAGAR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB38_CTA_PAGAR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB38_CTA_PAGAR.OrderBy( c => c.CO_EMP ).ThenBy( c => c.CO_CON_DESFIX ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB38_CTA_PAGAR pelas chaves primárias "CO_EMP", "NU_DOC" e "NU_PAR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <returns>Entidade TB38_CTA_PAGAR</returns>
        public static TB38_CTA_PAGAR RetornaPelaChavePrimaria(int CO_EMP, string NU_DOC, int NU_PAR)
        {
            return (from tb38 in RetornaTodosRegistros()
                    where tb38.CO_EMP == CO_EMP && tb38.NU_DOC == NU_DOC && tb38.NU_PAR == NU_PAR
                    select tb38).OrderByDescending( c => c.DT_CAD_DOC ).FirstOrDefault();
        }

        #endregion
    }
}