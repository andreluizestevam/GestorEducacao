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
    public partial class TB_RESPON_MATERIA
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
        /// Exclue o registro da tabela TB_RESPON_MATERIA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB_RESPON_MATERIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB_RESPON_MATERIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_RESPON_MATERIA.</returns>
        public static TB_RESPON_MATERIA Delete(TB_RESPON_MATERIA entity)
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
        public static int SaveOrUpdate(TB_RESPON_MATERIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_RESPON_MATERIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_RESPON_MATERIA.</returns>
        public static TB_RESPON_MATERIA SaveOrUpdate(TB_RESPON_MATERIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB_RESPON_MATERIA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB_RESPON_MATERIA.</returns>
        public static TB_RESPON_MATERIA GetByEntityKey(EntityKey entityKey)
        {
            return (TB_RESPON_MATERIA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB_RESPON_MATERIA, ordenados pelo Id "ID_RESP_MAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_RESPON_MATERIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB_RESPON_MATERIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB_RESPON_MATERIA.OrderBy( r => r.ID_RESP_MAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB_RESPON_MATERIA onde o Id "ID_RESP_MAT" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_RESP_MAT">Id da chave primária</param>
        /// <returns>Entidade TB_RESPON_MATERIA</returns>
        public static TB_RESPON_MATERIA RetornaPelaChavePrimaria(int ID_RESP_MAT)
        {
            return (from tbRespMate in RetornaTodosRegistros()
                    where tbRespMate.ID_RESP_MAT == ID_RESP_MAT
                    select tbRespMate).FirstOrDefault();
        }

        #endregion
    }
}