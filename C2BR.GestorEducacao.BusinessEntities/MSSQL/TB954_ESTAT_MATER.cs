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
    public partial class TB954_ESTAT_MATER
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
        /// Exclue o registro da tabela TB954_ESTAT_MATER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB954_ESTAT_MATER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB954_ESTAT_MATER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB954_ESTAT_MATER.</returns>
        public static TB954_ESTAT_MATER Delete(TB954_ESTAT_MATER entity)
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
        public static int SaveOrUpdate(TB954_ESTAT_MATER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB954_ESTAT_MATER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB954_ESTAT_MATER.</returns>
        public static TB954_ESTAT_MATER SaveOrUpdate(TB954_ESTAT_MATER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB954_ESTAT_MATER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB954_ESTAT_MATER.</returns>
        public static TB954_ESTAT_MATER GetByEntityKey(EntityKey entityKey)
        {
            return (TB954_ESTAT_MATER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB954_ESTAT_MATER, ordenados pelo Id "ID_ESTAT_MATER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB954_ESTAT_MATER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB954_ESTAT_MATER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB954_ESTAT_MATER.OrderBy( e => e.ID_ESTAT_MATER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB954_ESTAT_MATER pela chave primária "ID_ESTAT_MATER".
        /// </summary>
        /// <param name="ID_ESTAT_MATER">Id da chave primária</param>
        /// <returns>Entidade TB954_ESTAT_MATER</returns>
        public static TB954_ESTAT_MATER RetornaPelaChavePrimaria(int ID_ESTAT_MATER)
        {
            return (from tb954 in RetornaTodosRegistros()
                    where tb954.ID_ESTAT_MATER == ID_ESTAT_MATER
                    select tb954).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB954_ESTAT_MATER pela unidade "CO_EMP", pela matéria "ID_MATERIA" e ano de referência "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da Unidade</param>
        /// <param name="ID_MATERIA">Id da matéria</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB954_ESTAT_MATER</returns>
        public static TB954_ESTAT_MATER RetornaPeloOcorrEstatMater(int CO_EMP, int ID_MATERIA, int CO_ANO_REF)
        {
            return (from tb954 in RetornaTodosRegistros()
                    where tb954.TB107_CADMATERIAS.CO_EMP == CO_EMP
                    && tb954.TB107_CADMATERIAS.ID_MATERIA == ID_MATERIA
                    && tb954.CO_ANO_REF == CO_ANO_REF
                    select tb954).FirstOrDefault();
        }

        #endregion
    }
}