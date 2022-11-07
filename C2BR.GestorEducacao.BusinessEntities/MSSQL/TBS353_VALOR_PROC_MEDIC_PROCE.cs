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
    public partial class TBS353_VALOR_PROC_MEDIC_PROCE
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
        /// Exclue o registro da tabela TBS353_VALOR_PROC_MEDIC_PROCE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS353_VALOR_PROC_MEDIC_PROCE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS353_VALOR_PROC_MEDIC_PROCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS353_VALOR_PROC_MEDIC_PROCE.</returns>
        public static TBS353_VALOR_PROC_MEDIC_PROCE Delete(TBS353_VALOR_PROC_MEDIC_PROCE entity)
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
        public static int SaveOrUpdate(TBS353_VALOR_PROC_MEDIC_PROCE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS353_VALOR_PROC_MEDIC_PROCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS353_VALOR_PROC_MEDIC_PROCE.</returns>
        public static TBS353_VALOR_PROC_MEDIC_PROCE SaveOrUpdate(TBS353_VALOR_PROC_MEDIC_PROCE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS353_VALOR_PROC_MEDIC_PROCE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS353_VALOR_PROC_MEDIC_PROCE.</returns>
        public static TBS353_VALOR_PROC_MEDIC_PROCE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS353_VALOR_PROC_MEDIC_PROCE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS353_VALOR_PROC_MEDIC_PROCE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS353_VALOR_PROC_MEDIC_PROCE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS353_VALOR_PROC_MEDIC_PROCE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS353_VALOR_PROC_MEDIC_PROCE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS353_VALOR_PROC_MEDIC_PROCE pela chave primária "ID_VACINA".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS353_VALOR_PROC_MEDIC_PROCE</returns>
        public static TBS353_VALOR_PROC_MEDIC_PROCE RetornaPelaChavePrimaria(int ID_VALOR_PROC_MEDIC_PROCE)
        {
            return (from tbs353 in RetornaTodosRegistros()
                    where tbs353.ID_VALOR_PROC_MEDIC_PROCE == ID_VALOR_PROC_MEDIC_PROCE
                    select tbs353).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS353_VALOR_PROC_MEDIC_PROCE pela pelo Código do Procedimento
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id do Procedimento</param>
        /// <returns>Entidade TBS353_VALOR_PROC_MEDIC_PROCE</returns>
        public static TBS353_VALOR_PROC_MEDIC_PROCE RetornaPeloProcedimento(int ID_PROC_MEDI_PROCE)
        {
            return (from tbs353 in RetornaTodosRegistros()
                    where tbs353.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE
                    && tbs353.FL_STATU == "A"
                    select tbs353).OrderByDescending(w => w.DT_LANC).FirstOrDefault();
        }

        #endregion
    }
}
