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
    public partial class TB62_CURSO_FORM
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
        /// Exclue o registro da tabela TB62_CURSO_FORM do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB62_CURSO_FORM entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB62_CURSO_FORM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB62_CURSO_FORM.</returns>
        public static TB62_CURSO_FORM Delete(TB62_CURSO_FORM entity)
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
        public static int SaveOrUpdate(TB62_CURSO_FORM entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB62_CURSO_FORM na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB62_CURSO_FORM.</returns>
        public static TB62_CURSO_FORM SaveOrUpdate(TB62_CURSO_FORM entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB62_CURSO_FORM de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB62_CURSO_FORM.</returns>
        public static TB62_CURSO_FORM GetByEntityKey(EntityKey entityKey)
        {
            return (TB62_CURSO_FORM)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB62_CURSO_FORM, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB62_CURSO_FORM de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB62_CURSO_FORM> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB62_CURSO_FORM.OrderBy( c => c.CO_EMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB62_CURSO_FORM pelas chaves primárias "CO_EMP", "CO_COL", "CO_ESPEC" e "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_COL">Id do funcionário</param>
        /// <param name="CO_ESPEC">Id da especialização</param>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>Entidade TB62_CURSO_FORM</returns>
        public static TB62_CURSO_FORM RetornaPelaChavePrimaria(int CO_EMP, int CO_COL, int CO_ESPEC , int ORG_CODIGO_ORGAO)
        {
            return (from tb62 in RetornaTodosRegistros()
                    where tb62.CO_EMP == CO_EMP && tb62.CO_COL == CO_COL && tb62.CO_ESPEC == CO_ESPEC && tb62.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb62).FirstOrDefault();
        }

        #endregion
    }
}
