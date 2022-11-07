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
    public partial class TB80_MASTERMATR
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
        /// Exclue o registro da tabela TB80_MASTERMATR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB80_MASTERMATR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB80_MASTERMATR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB80_MASTERMATR.</returns>
        public static TB80_MASTERMATR Delete(TB80_MASTERMATR entity)
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
        public static int SaveOrUpdate(TB80_MASTERMATR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB80_MASTERMATR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB80_MASTERMATR.</returns>
        public static TB80_MASTERMATR SaveOrUpdate(TB80_MASTERMATR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB80_MASTERMATR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB80_MASTERMATR.</returns>
        public static TB80_MASTERMATR GetByEntityKey(EntityKey entityKey)
        {
            return (TB80_MASTERMATR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB80_MASTERMATR, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB80_MASTERMATR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB80_MASTERMATR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB80_MASTERMATR.OrderBy( m => m.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB80_MASTERMATR pelas chaves primárias "CO_MODU_CUR", "CO_ALU" "CO_ANO_MES_MAT" e "CO_CUR".
        /// </summary>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_ANO_MES_MAT">Ano de matrícula</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB80_MASTERMATR</returns>
        public static TB80_MASTERMATR RetornaPelaChavePrimaria(int CO_MODU_CUR, int CO_ALU, string CO_ANO_MES_MAT, int? CO_CUR)
        {
            return (from tb80 in RetornaTodosRegistros()
                    where tb80.CO_MODU_CUR == CO_MODU_CUR && tb80.CO_ALU == CO_ALU && CO_CUR != null ? tb80.CO_CUR == CO_CUR : CO_CUR == null
                    && tb80.CO_ANO_MES_MAT == CO_ANO_MES_MAT
                    select tb80).FirstOrDefault();
        }

        #endregion
    }
}
