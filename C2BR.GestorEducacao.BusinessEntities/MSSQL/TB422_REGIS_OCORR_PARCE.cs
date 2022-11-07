using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB422_REGIS_OCORR_PARCE
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
        /// Exclue o registro da tabela TB422_REGIS_OCORR_PARCE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB422_REGIS_OCORR_PARCE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB422_REGIS_OCORR_PARCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB422_REGIS_OCORR_PARCE.</returns>
        public static TB422_REGIS_OCORR_PARCE Delete(TB422_REGIS_OCORR_PARCE entity)
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
        public static int SaveOrUpdate(TB422_REGIS_OCORR_PARCE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB422_REGIS_OCORR_PARCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB422_REGIS_OCORR_PARCE.</returns>
        public static TB422_REGIS_OCORR_PARCE SaveOrUpdate(TB422_REGIS_OCORR_PARCE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB422_REGIS_OCORR_PARCE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB422_REGIS_OCORR_PARCE.</returns>
        public static TB422_REGIS_OCORR_PARCE GetByEntityKey(EntityKey entityKey)
        {
            return (TB422_REGIS_OCORR_PARCE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB422_REGIS_OCORR_PARCE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB422_REGIS_OCORR_PARCE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB422_REGIS_OCORR_PARCE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB422_REGIS_OCORR_PARCE pela chave primária "ID_ATEND_MEDICAMENTOS".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB422_REGIS_OCORR_PARCE</returns>
        public static TB422_REGIS_OCORR_PARCE RetornaPelaChavePrimaria(int ID_OCORR)
        {
            return (from tb422 in RetornaTodosRegistros()
                    where tb422.CO_OCORR == ID_OCORR
                    select tb422).FirstOrDefault();
        }

        #endregion

    }
}
