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
    public partial class TB122_ALUNO_CREDI
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
        /// Exclue o registro da tabela TB122_ALUNO_CREDI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB122_ALUNO_CREDI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB122_ALUNO_CREDI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB122_ALUNO_CREDI.</returns>
        public static TB122_ALUNO_CREDI Delete(TB122_ALUNO_CREDI entity)
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
        public static int SaveOrUpdate(TB122_ALUNO_CREDI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB122_ALUNO_CREDI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB122_ALUNO_CREDI.</returns>
        public static TB122_ALUNO_CREDI SaveOrUpdate(TB122_ALUNO_CREDI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB122_ALUNO_CREDI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB122_ALUNO_CREDI.</returns>
        public static TB122_ALUNO_CREDI GetByEntityKey(EntityKey entityKey)
        {
            return (TB122_ALUNO_CREDI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB122_ALUNO_CREDI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB122_ALUNO_CREDI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB122_ALUNO_CREDI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB122_ALUNO_CREDI.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Método que retorna os registros da TB122_ALUNO_CREDI, pesquisando pelo aluno que recebeu o crédito ou que está transferindo o crédito.
        /// </summary>
        /// <param name="coAlu">Código do aluno</param>
        /// <param name="tpAlu">Tipo do aluno ("A" - Aluno que está transferindo o crédito; "C" - Aluno que está recebendo o crédito)</param>
        /// <returns></returns>
        public static ObjectQuery<TB122_ALUNO_CREDI> RetornaRegistrosPorAluno(int coAlu, string tpAlu = "A")
        {
            ObjectQuery<TB122_ALUNO_CREDI> oq = null;

            switch (tpAlu)
            {
                case "A":
                    oq = GestorEntities.CurrentContext.TB122_ALUNO_CREDI.Where(w => w.CO_ALU == coAlu).AsObjectQuery();
                    break;
                case "C":
                    oq = GestorEntities.CurrentContext.TB122_ALUNO_CREDI.Where(w => w.CO_ALU_CRED == coAlu).AsObjectQuery();
                    break;
            }

            return oq;
        }
    }
}
