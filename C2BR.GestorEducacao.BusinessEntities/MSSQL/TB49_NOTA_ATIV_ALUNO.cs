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
    public partial class TB49_NOTA_ATIV_ALUNO
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
        /// Exclue o registro da tabela TB49_NOTA_ATIV_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB49_NOTA_ATIV_ALUNO entity, bool saveChanges) 
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB49_NOTA_ATIV_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB49_NOTA_ATIV_ALUNO.</returns>
        public static TB49_NOTA_ATIV_ALUNO Delete(TB49_NOTA_ATIV_ALUNO entity) 
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
        public static int SaveOrUpdate(TB49_NOTA_ATIV_ALUNO entity, bool saveChanges) 
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB49_NOTA_ATIV_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB49_NOTA_ATIV_ALUNO.</returns>
        public static TB49_NOTA_ATIV_ALUNO SaveOrUpdate(TB49_NOTA_ATIV_ALUNO entity) 
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB49_NOTA_ATIV_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB49_NOTA_ATIV_ALUNO.</returns>
        public static TB49_NOTA_ATIV_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB49_NOTA_ATIV_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB49_NOTA_ATIV_ALUNO, ordenados pelo Id "ID_NOTA_ATIV".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB49_NOTA_ATIV_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB49_NOTA_ATIV_ALUNO> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB49_NOTA_ATIV_ALUNO.OrderBy( n => n.ID_NOTA_ATIV ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB49_NOTA_ATIV_ALUNO pela chave primária "ID_NOTA_ATIV".
        /// </summary>
        /// <param name="ID_NOTA_ATIV">Id da chave primária</param>
        /// <returns>Entidade TB49_NOTA_ATIV_ALUNO</returns>
        public static TB49_NOTA_ATIV_ALUNO RetornaPelaChavePrimaria(int ID_NOTA_ATIV) 
        {
            return (from tb49 in RetornaTodosRegistros()
                    where tb49.ID_NOTA_ATIV == ID_NOTA_ATIV
                    select tb49).FirstOrDefault();
        }
        #endregion
    }
}
