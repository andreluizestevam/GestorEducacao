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
    public partial class TB136_ALU_PROG_SOCIAIS
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
        /// Exclue o registro da tabela TB136_ALU_PROG_SOCIAIS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB136_ALU_PROG_SOCIAIS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB136_ALU_PROG_SOCIAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB136_ALU_PROG_SOCIAIS.</returns>
        public static TB136_ALU_PROG_SOCIAIS Delete(TB136_ALU_PROG_SOCIAIS entity)
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
        public static int SaveOrUpdate(TB136_ALU_PROG_SOCIAIS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB136_ALU_PROG_SOCIAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB136_ALU_PROG_SOCIAIS.</returns>
        public static TB136_ALU_PROG_SOCIAIS SaveOrUpdate(TB136_ALU_PROG_SOCIAIS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB136_ALU_PROG_SOCIAIS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB136_ALU_PROG_SOCIAIS.</returns>
        public static TB136_ALU_PROG_SOCIAIS GetByEntityKey(EntityKey entityKey)
        {
            return (TB136_ALU_PROG_SOCIAIS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB136_ALU_PROG_SOCIAIS, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB136_ALU_PROG_SOCIAIS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB136_ALU_PROG_SOCIAIS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB136_ALU_PROG_SOCIAIS.OrderBy( a => a.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB136_ALU_PROG_SOCIAIS pelas chaves primárias "ORG_CODIGO_ORGAO", "CO_EMP", "CO_ALU" e "CO_IDENT_PROGR_SOCIA".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_IDENT_PROGR_SOCIA">Id do programa social</param>
        /// <returns>Entidade TB136_ALU_PROG_SOCIAIS</returns>
        public static TB136_ALU_PROG_SOCIAIS RetornaPelaChavePrimaria(int ORG_CODIGO_ORGAO, int CO_EMP, int CO_ALU, int CO_IDENT_PROGR_SOCIA)
        {
            return (from tb136 in RetornaTodosRegistros()
                    where tb136.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO && tb136.CO_EMP == CO_EMP && tb136.CO_ALU == CO_ALU 
                    && tb136.CO_IDENT_PROGR_SOCIA == CO_IDENT_PROGR_SOCIA
                    select tb136).FirstOrDefault();
        }

        #endregion
    }
}