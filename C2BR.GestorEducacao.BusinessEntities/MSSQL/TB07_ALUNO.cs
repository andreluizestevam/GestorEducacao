//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Data.SqlClient;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB07_ALUNO
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
        /// Exclue o registro da tabela TB07_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB07_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB07_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB07_ALUNO Delete(TB07_ALUNO entity)
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
        public static int SaveOrUpdate(TB07_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB07_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB07_ALUNO SaveOrUpdate(TB07_ALUNO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB07_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB07_ALUNO.</returns>
        public static TB07_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB07_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB07_ALUNO, ordenados pelo nome "NO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB07_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB07_ALUNO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB07_ALUNO.OrderBy( a => a.NO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB07_ALUNO pelas chaves primárias "CO_ALU" e "CO_EMP".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da Unidade</param>
        /// <returns>Entidade TB07_ALUNO</returns>
        public static TB07_ALUNO RetornaPelaChavePrimaria(int CO_ALU, int CO_EMP)
        {
            //return (from tb07 in RetornaTodosRegistros().Include(typeof(TB108_RESPONSAVEL).Name).Include(typeof(TB905_BAIRRO).Name).Include(typeof(TB148_TIPO_BOLSA).Name).Include(typeof(TB164_INST_ESP).Name).Include(typeof(TB136_ALU_PROG_SOCIAIS).Name).Include(typeof(Image).Name)
            //        where tb07.CO_ALU == CO_ALU && tb07.CO_EMP == CO_EMP
            //        select tb07).FirstOrDefault();

            return GestorEntities.CurrentContext.TB07_ALUNO
                .Include(typeof(TB108_RESPONSAVEL).Name)
                .Include(typeof(TB905_BAIRRO).Name)
                .Include(typeof(TB148_TIPO_BOLSA).Name)
                .Include(typeof(TB164_INST_ESP).Name)
                .Include(typeof(TB136_ALU_PROG_SOCIAIS).Name)
                .Include(typeof(Image).Name)
                .FirstOrDefault(p => p.CO_ALU == CO_ALU && p.CO_EMP == CO_EMP);

        }

        /// <summary>
        /// Retorna um registro da entidade TB07_ALUNO pela chave primária "CO_ALU".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <returns>Entidade TB07_ALUNO</returns>
        public static TB07_ALUNO RetornaPeloCoAlu(int CO_ALU)
        {
            return GestorEntities.CurrentContext.TB07_ALUNO.FirstOrDefault(p => p.CO_ALU == CO_ALU);
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB07_ALUNO de acordo com a unidade de cadastro "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB07_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB07_ALUNO> RetornaPelaEmpresa(int CO_EMP)
        {
            return (from tb07 in RetornaTodosRegistros()
                    where tb07.CO_EMP == CO_EMP
                    select tb07).OrderBy( a => a.NO_ALU ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB07_ALUNO de acordo com a unidade presente "TB25_EMPRESA1.CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB07_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB07_ALUNO> RetornaPelaUnid(int CO_EMP)
        {
            return (from tb07 in RetornaTodosRegistros()
                    where tb07.TB25_EMPRESA1.CO_EMP == CO_EMP
                    select tb07).OrderBy( a => a.NO_ALU ).AsObjectQuery();
        }

        public static void AtualizarSituacaoFinanceira(int CO_ALU)
        {
            var tb07 = RetornaPeloCoAlu(CO_ALU);

            tb07.FL_PENDE_FINAN_GER = "N";

            var res = (from tbs47 in TBS47_CTA_RECEB.RetornaTodosRegistros()
                       where tbs47.CO_ALU == CO_ALU
                       && tbs47.IC_SIT_DOC == "A"
                       && tbs47.DT_VEN_DOC < DateTime.Now
                       select tbs47.CO_ALU).ToList();

            if (res != null && res.Count > 0)
                tb07.FL_PENDE_FINAN_GER = "S";

            SaveOrUpdate(tb07);

            /*SqlConnection sqlConnection = new SqlConnection(conn);
            
            SqlCommand cmd = new SqlCommand(@"UPDATE TB07_ALUNO SET FL_PENDE_FINAN_GER = 'S'
                                              WHERE CO_ALU IN (
	                                              SELECT CO_ALU 
	                                              FROM TBS47_CTA_RECEB 
	                                              WHERE IC_SIT_DOC = 'A' AND DT_VEN_DOC < GETDATE()
                                                  AND CO_ALU = " + CO_ALU + ")");

            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 1000;

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();*/
        }

        #endregion
    }
}