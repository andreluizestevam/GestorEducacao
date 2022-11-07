using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB009_RTF_DOCTOS
    {

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
        /// Exclue o registro da tabela TB07_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB009_RTF_DOCTOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB07_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB009_RTF_DOCTOS Delete(TB009_RTF_DOCTOS entity)
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
        public static int SaveOrUpdate(TB009_RTF_DOCTOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB07_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB009_RTF_DOCTOS SaveOrUpdate(TB009_RTF_DOCTOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB07_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB009_RTF_DOCTOS GetByEntityKey(EntityKey entityKey)
        {
            return (TB009_RTF_DOCTOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB009_DOCTOS_RTF, ordenados pelo nome "NM_DOCUM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB009_DOCTOS_RTF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB009_RTF_DOCTOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB009_RTF_DOCTOS.OrderBy(x => x.NM_DOCUM).AsObjectQuery();
        }

        #endregion


        /// <summary>
        /// Retorna um registro da entidade TB009_DOCTOS_RTF pelas chaves primárias "ID_DOCUM".
        /// </summary>
        /// <param name="ID">Id do Documento</param>       
        /// <returns>Entidade TB009_DOCTOS_RTF</returns>
        public static TB009_RTF_DOCTOS RetornaPelaChavePrimaria(int ID_DOCUM)
        {
            return (from tb009 in RetornaTodosRegistros()
                    where tb009.ID_DOCUM == ID_DOCUM
                    select tb009).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB009_DOCTOS_RTF de acordo com a unidade de cadastro "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB009_DOCTOS_RTF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB009_RTF_DOCTOS> RetornaPelaEmpresa(int CO_EMP)
        {
            return (from tb07 in RetornaTodosRegistros()
                    where tb07.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb07).OrderBy(x => x.NM_DOCUM).AsObjectQuery();
        }
    }
}
