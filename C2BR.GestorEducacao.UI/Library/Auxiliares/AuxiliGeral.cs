//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: GERAL
// SUBMÓDULO: GERAL
// OBJETIVO: FUNÇÕES GERAIS DO SISTEMA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 03/05/2013| Victor Martins Machado     | Criada a validação da existência da matrícula do aluno
//           |                            | para a modalidade/série/ano selecionaodos.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 04/06/2013| Victor Martins Machado     | Criada a função que retorna a média da turma por
//           |                            | matéria.
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 11/02/2014| Vinicius Reis              | Alterado método que retornas as matrículas atuais,
//           |                            | DefaultIfEmpty -> ToList, assim a condiciona pega o valor correto.
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class AuxiliGeral
    {
        #region Classes para o Cálculo das frequências
        public class TotalFreqAlu
        {
            public int QTD_FREQ { get; set; }
            public int? CO_MAT { get; set; }
            public int CO_EMP { get; set; }
            public decimal CO_ANO_REFER { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int CO_CUR { get; set; }
            public int? CO_TUR { get; set; }
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public string CO_BIMESTRE { get; set; }
        }

        public class DiasFreqFlag
        {
            public int CO_UNID { get; set; }
            public decimal CO_ANO_REFER_FREQ_ALUNO { get; set; }
            public int CO_MODU_CUR { get; set; }
            public int CO_CUR { get; set; }
            public int CO_TUR { get; set; }
            public int CO_ALU { get; set; }
            public int? CO_MAT { get; set; }
            public DateTime DT_FRE { get; set; }
            public string CO_BIMESTRE { get; set; }
            public string CO_FLAG_FREQ_ALUNO { get; set; }
        }

        public class GradeMateria
        {
            public int CO_MAT { get; set; }
        }

        public class DiasFreq
        {
            public DateTime DT_FRE { get; set; }
        }

        public class DiasFreqAlu
        {
            public int QTD_FREQ { get; set; }
            public int CO_ALU { get; set; }
            public string CO_BIMESTRE { get; set; }
        }

        public class MatriculaSitu
        {
            public string CO_SIT_MAT { get; set; }
        }
        #endregion

        /// <summary>
        /// Atualiza a frequência no histórico do aluno
        /// </summary>
        /// <param name="page">Página que chama a função</param>
        /// <param name="CO_EMP">Unidade</param>
        /// <param name="CO_ANO_REFER">Ano de referência</param>
        /// <param name="CO_MODU_CUR">Modalidade</param>
        /// <param name="CO_CUR">Curso/Série</param>
        /// <param name="CO_TUR">Turma</param>
        /// <param name="CO_ALU">Aluno</param>
        /// <returns></returns>
        public static bool AtualizaHistFreqAlu(Page page, int CO_EMP, decimal CO_ANO_REFER, int CO_MODU_CUR, int CO_CUR, int CO_TUR, string CO_BIMESTRE)
        {
            bool t = false;

            var ctx = GestorEntities.CurrentContext;

            //====> Carrega as informações do curso
            TB01_CURSO tb01 = TB01_CURSO.RetornaPeloCoCur(CO_CUR);

            string turmaUnica = "";

            if (CO_MODU_CUR != 0 && CO_CUR != 0 && CO_TUR != 0)
            {
                // Recebe o conteúdo da coluna CO_FLAG_RESP_TURMA, da tabela TB06_TURMAS, que informa se a turma é única ou não
                turmaUnica = TB06_TURMAS.RetornaPelaChavePrimaria(CO_EMP, CO_MODU_CUR, CO_CUR, CO_TUR).CO_FLAG_RESP_TURMA;
            }

            /**
             * Pega o tipo de controle de frequência cadastrado no curso:
             *      >> D - Por Dia
             *      >> M - Por Matéria
             * */
            string CtrlFreq = tb01.CO_PARAM_FREQUE;

            if (CtrlFreq == "M")
            {
                #region Cálculo por matéria

                var res = ctx.ExecuteStoreQuery<TotalFreqAlu>("select COUNT(*) as QTD_FREQ, f.CO_MAT, f.CO_UNID AS CO_EMP, CO_ANO_REFER_FREQ_ALUNO AS CO_ANO_REFER, f.CO_MODU_CUR, f.CO_CUR, f.CO_TUR, f.CO_ALU, a.NO_ALU, CO_BIMESTRE from tb132_freq_alu f inner join TB07_ALUNO a on (a.CO_ALU = f.CO_ALU) where f.CO_UNID = " + CO_EMP + " and CO_ANO_REFER_FREQ_ALUNO = " + CO_ANO_REFER + " and f.CO_MODU_CUR = " + CO_MODU_CUR + " and f.CO_CUR = " + CO_CUR + " and f.CO_TUR = " + CO_TUR + " and CO_FLAG_FREQ_ALUNO = 'N' and (CO_BIMESTRE = '" + CO_BIMESTRE + "' OR '" + CO_BIMESTRE + "' = 'T') group by CO_MAT, f.CO_UNID, CO_ANO_REFER_FREQ_ALUNO, f.CO_MODU_CUR, f.CO_CUR, f.CO_TUR, f.CO_ALU, a.NO_ALU, CO_BIMESTRE");

                foreach (TotalFreqAlu tf in res)
                {
                    TB079_HIST_ALUNO tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(tf.CO_ALU, tf.CO_MODU_CUR, tf.CO_CUR, tf.CO_ANO_REFER.ToString(), tf.CO_MAT.Value);

                    #region Verificação e criação da matéria de turma única
                    //---------> Verifica se a turma será única ou não
                    if (turmaUnica == "S")
                    {
                        //-------------> Verifica se existe uma matéria com sigla "MSR" (Matéria sem Registro), que é a matéria padrão para turma única
                        if (!TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").Any())
                        {
                            //-----------------> Cria uma matéria para ser a padrão de turma única, MSR.
                            TB107_CADMATERIAS cm = new TB107_CADMATERIAS();

                            cm.CO_EMP = LoginAuxili.CO_EMP;
                            cm.NO_SIGLA_MATERIA = "MSR";
                            cm.NO_MATERIA = "Atividades Letivas";
                            cm.NO_RED_MATERIA = "Atividades";
                            cm.DE_MATERIA = "Matéria utilizada no lançamento de atividades para professores de turma única.";
                            cm.CO_STATUS = "A";
                            cm.DT_STATUS = DateTime.Now;
                            cm.CO_CLASS_BOLETIM = 4;
                            //TB107_CADMATERIAS.SaveOrUpdate(cm);
                            ctx.AddToTB107_CADMATERIAS(cm);
                            //ctx.


                            //ctx.SaveChanges();

                            //-----------------> Vincula a matéria MSR ao curso selecionado
                            int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cma => cma.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                            TB02_MATERIA m = new TB02_MATERIA();

                            m.CO_EMP = CO_EMP;
                            m.CO_MODU_CUR = CO_MODU_CUR;
                            m.CO_CUR = CO_CUR;
                            m.TB01_CURSO = tb01;
                            m.ID_MATERIA = idMat;
                            m.QT_CRED_MAT = null;
                            m.QT_CARG_HORA_MAT = 800;
                            m.DT_INCL_MAT = DateTime.Now;
                            m.DT_SITU_MAT = DateTime.Now;
                            m.CO_SITU_MAT = "I";
                            //TB02_MATERIA.SaveOrUpdate(m);
                            ctx.AddToTB02_MATERIA(m);

                            //ctx.SaveChanges();

                            TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                            h.CO_EMP = CO_EMP;
                            h.CO_ALU = tf.CO_ALU;
                            h.CO_MODU_CUR = CO_MODU_CUR;
                            h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                            h.CO_CUR = CO_CUR;
                            h.CO_ANO_REF = CO_ANO_REFER.ToString();
                            h.CO_MAT = tf.CO_MAT.Value;
                            h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                            h.CO_TUR = CO_TUR;
                            h.DT_LANC = DateTime.Now;
                            h.CO_USUARIO = LoginAuxili.CO_COL;
                            h.FL_TIPO_LANC_MEDIA = "N";
                            h.CO_FLAG_STATUS = "I";
                            //TB079_HIST_ALUNO.SaveOrUpdate(h);
                            ctx.AddToTB079_HIST_ALUNO(h);

                            //ctx.SaveChanges();
                        }
                        else
                        {
                            //-----------------> Verifica se a matéria MSR está vinculada ao curso
                            int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                            if (!TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_EMP == CO_EMP && m.CO_MODU_CUR == CO_MODU_CUR && m.CO_CUR == CO_CUR && m.ID_MATERIA == idMat).Any())
                            {
                                //---------------------> Vincula a matéria MSR ao curso selecionado.
                                TB02_MATERIA m = new TB02_MATERIA();

                                m.CO_EMP = CO_EMP;
                                m.CO_MODU_CUR = CO_MODU_CUR;
                                m.CO_CUR = CO_CUR;
                                m.TB01_CURSO = tb01;
                                m.ID_MATERIA = idMat;
                                m.QT_CRED_MAT = null;
                                m.QT_CARG_HORA_MAT = 800;
                                m.DT_INCL_MAT = DateTime.Now;
                                m.DT_SITU_MAT = DateTime.Now;
                                m.CO_SITU_MAT = "I";
                                //TB02_MATERIA.SaveOrUpdate(m);
                                ctx.AddToTB02_MATERIA(m);

                                //ctx.SaveChanges();

                                if (!TB079_HIST_ALUNO.RetornaTodosRegistros().Where(hi => hi.CO_EMP == CO_EMP && hi.CO_MODU_CUR == CO_MODU_CUR && hi.CO_CUR == CO_CUR && hi.CO_MAT == tf.CO_MAT).Any())
                                {
                                    TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                                    h.CO_EMP = CO_EMP;
                                    h.CO_ALU = tf.CO_ALU;
                                    h.CO_MODU_CUR = CO_MODU_CUR;
                                    h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                                    h.CO_CUR = CO_CUR;
                                    h.CO_ANO_REF = CO_ANO_REFER.ToString();
                                    h.CO_MAT = tf.CO_MAT.Value;
                                    h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                                    h.CO_TUR = CO_TUR;
                                    h.DT_LANC = DateTime.Now;
                                    h.CO_USUARIO = LoginAuxili.CO_COL;
                                    h.FL_TIPO_LANC_MEDIA = "N";
                                    h.CO_FLAG_STATUS = "I";
                                    //TB079_HIST_ALUNO.SaveOrUpdate(h);
                                    ctx.AddToTB079_HIST_ALUNO(h);
                                }

                                //ctx.SaveChanges();
                            }
                            else
                            {
                                if (!TB079_HIST_ALUNO.RetornaTodosRegistros().Where(hi => hi.CO_EMP == CO_EMP && hi.CO_MODU_CUR == CO_MODU_CUR && hi.CO_CUR == CO_CUR && hi.CO_MAT == tf.CO_MAT).Any())
                                {
                                    TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                                    h.CO_EMP = CO_EMP;
                                    h.CO_ALU = tf.CO_ALU;
                                    h.CO_MODU_CUR = CO_MODU_CUR;
                                    h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                                    h.CO_CUR = CO_CUR;
                                    h.CO_ANO_REF = CO_ANO_REFER.ToString();
                                    h.CO_MAT = tf.CO_MAT.Value;
                                    h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                                    h.CO_TUR = CO_TUR;
                                    h.DT_LANC = DateTime.Now;
                                    h.CO_USUARIO = LoginAuxili.CO_COL;
                                    h.FL_TIPO_LANC_MEDIA = "N";
                                    h.CO_FLAG_STATUS = "I";
                                    //TB079_HIST_ALUNO.SaveOrUpdate(h);
                                    ctx.AddToTB079_HIST_ALUNO(h);

                                    //ctx.SaveChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    var resM = ctx.ExecuteStoreQuery<MatriculaSitu>("select CO_SIT_MAT from TB08_MATRCUR where CO_EMP = " + tf.CO_EMP + " and CO_ALU = " + tf.CO_ALU + " and CO_CUR = " + tf.CO_CUR + " and CO_ANO_MES_MAT = " + tf.CO_ANO_REFER).ToList();

                    if (resM.Count() > 0)
                    {
                        if (resM.FirstOrDefault().CO_SIT_MAT == "A")
                        {

                            if (tb079 == null)
                            {
                                AuxiliPagina.EnvioMensagemErro(page, "Histórico não encontrado para o aluno '" + tf.NO_ALU + "'");
                            }
                            else
                            {
                                switch (tf.CO_BIMESTRE)
                                {
                                    case "B1":
                                        tb079.QT_FALTA_BIM1 = tf.QTD_FREQ;
                                        break;
                                    case "B2":
                                        tb079.QT_FALTA_BIM2 = tf.QTD_FREQ;
                                        break;
                                    case "B3":
                                        tb079.QT_FALTA_BIM3 = tf.QTD_FREQ;
                                        break;
                                    case "B4":
                                        tb079.QT_FALTA_BIM4 = tf.QTD_FREQ;
                                        break;
                                }
                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region Cálculo por dia

                #region Cria as listas necessárias para o cálculo

                //===> Lista as frequências lançadas para a modalidade/série/turma selecionados
                var res = ctx.ExecuteStoreQuery<DiasFreqFlag>("select f.CO_UNID, CO_ANO_REFER_FREQ_ALUNO, f.CO_MODU_CUR, f.CO_CUR, f.CO_TUR, f.CO_ALU, CO_MAT, DT_FRE, CO_BIMESTRE, f.CO_FLAG_FREQ_ALUNO from TB132_FREQ_ALU f inner join TB07_ALUNO a on (a.CO_ALU = f.CO_ALU) where f.CO_UNID = " + CO_EMP + " and CO_ANO_REFER_FREQ_ALUNO = " + CO_ANO_REFER + " and f.CO_MODU_CUR = " + CO_MODU_CUR + " and f.CO_CUR = " + CO_CUR + " and f.CO_TUR = " + CO_TUR + " and (CO_BIMESTRE = '" + CO_BIMESTRE + "' OR '" + CO_BIMESTRE + "' = 'T')").ToList();

                //===> Lista os dias com frequências lançadas para a modalidade/série/turma selecionados
                var resD = ctx.ExecuteStoreQuery<DiasFreq>("select distinct DT_FRE from TB132_FREQ_ALU f inner join TB07_ALUNO a on (a.CO_ALU = f.CO_ALU) where f.CO_UNID = " + CO_EMP + " and CO_ANO_REFER_FREQ_ALUNO = " + CO_ANO_REFER + " and f.CO_MODU_CUR = " + CO_MODU_CUR + " and f.CO_CUR = " + CO_CUR + " and f.CO_TUR = " + CO_TUR + " and (CO_BIMESTRE = '" + CO_BIMESTRE + "' OR '" + CO_BIMESTRE + "' = 'T')").ToList();

                //===> Pega a primeira matéria da grade de matérias para a série e ano de referência informados pelo usuário
                var resG = ctx.ExecuteStoreQuery<GradeMateria>("select gc.CO_MAT from TB43_GRD_CURSO gc where CO_ANO_GRADE = " + CO_ANO_REFER + " and gc.CO_CUR = " + CO_CUR + " and gc.CO_MODU_CUR = " + CO_MODU_CUR).ToList();
                int primeiraMeteria = resG.FirstOrDefault().CO_MAT;

                #endregion

                #region Contabiliza a frequência com a seguinte regra: se em um dia o aluno não possuir uma presença, conta 1 (uma) falta

                //===> Lista utilizada para contabilizar as frequências
                List<DiasFreqAlu> ldfa = new List<DiasFreqAlu>();

                //===> Passa por todas as datas com lançamento de frequência
                foreach (DiasFreq df in resD.ToList())
                {
                    //===> Passa por todos os lançamentos de frequência para contabilizar a frequência
                    foreach (DiasFreqFlag dff in res.Where(w => w.DT_FRE == df.DT_FRE))
                    {
                        #region Verificação e criação da matéria de turma única
                        //---------> Verifica se a turma será única ou não
                        if (turmaUnica == "S")
                        {
                            //-------------> Verifica se existe uma matéria com sigla "MSR" (Matéria sem Registro), que é a matéria padrão para turma única
                            if (!TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").Any())
                            {
                                //-----------------> Cria uma matéria para ser a padrão de turma única, MSR.
                                TB107_CADMATERIAS cm = new TB107_CADMATERIAS();

                                cm.CO_EMP = LoginAuxili.CO_EMP;
                                cm.NO_SIGLA_MATERIA = "MSR";
                                cm.NO_MATERIA = "Atividades Letivas";
                                cm.NO_RED_MATERIA = "Atividades";
                                cm.DE_MATERIA = "Matéria utilizada no lançamento de atividades para professores de turma única.";
                                cm.CO_STATUS = "A";
                                cm.DT_STATUS = DateTime.Now;
                                cm.CO_CLASS_BOLETIM = 4;
                                //TB107_CADMATERIAS.SaveOrUpdate(cm);
                                ctx.AddToTB107_CADMATERIAS(cm);

                                //ctx.SaveChanges();

                                //-----------------> Vincula a matéria MSR ao curso selecionado
                                int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cma => cma.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                                TB02_MATERIA m = new TB02_MATERIA();

                                m.CO_EMP = CO_EMP;
                                m.CO_MODU_CUR = CO_MODU_CUR;
                                m.CO_CUR = CO_CUR;
                                m.TB01_CURSO = tb01;
                                m.ID_MATERIA = idMat;
                                m.QT_CRED_MAT = null;
                                m.QT_CARG_HORA_MAT = 800;
                                m.DT_INCL_MAT = DateTime.Now;
                                m.DT_SITU_MAT = DateTime.Now;
                                m.CO_SITU_MAT = "I";
                                //TB02_MATERIA.SaveOrUpdate(m);
                                ctx.AddToTB02_MATERIA(m);

                                //ctx.SaveChanges();

                                TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                                h.CO_EMP = CO_EMP;
                                h.CO_ALU = dff.CO_ALU;
                                h.CO_MODU_CUR = CO_MODU_CUR;
                                h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                                h.CO_CUR = CO_CUR;
                                h.CO_ANO_REF = CO_ANO_REFER.ToString();
                                h.CO_MAT = dff.CO_MAT.Value;
                                h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                                h.CO_TUR = CO_TUR;
                                h.DT_LANC = DateTime.Now;
                                h.CO_USUARIO = LoginAuxili.CO_COL;
                                h.FL_TIPO_LANC_MEDIA = "N";
                                h.CO_FLAG_STATUS = "I";
                                //TB079_HIST_ALUNO.SaveOrUpdate(h);
                                ctx.AddToTB079_HIST_ALUNO(h);

                                //ctx.SaveChanges();
                            }
                            else
                            {
                                //-----------------> Verifica se a matéria MSR está vinculada ao curso
                                int idMat = TB107_CADMATERIAS.RetornaTodosRegistros().Where(cm => cm.NO_SIGLA_MATERIA == "MSR").FirstOrDefault().ID_MATERIA;
                                if (!TB02_MATERIA.RetornaTodosRegistros().Where(m => m.CO_EMP == CO_EMP && m.CO_MODU_CUR == CO_MODU_CUR && m.CO_CUR == CO_CUR && m.ID_MATERIA == idMat).Any())
                                {
                                    //---------------------> Vincula a matéria MSR ao curso selecionado.
                                    TB02_MATERIA m = new TB02_MATERIA();

                                    m.CO_EMP = CO_EMP;
                                    m.CO_MODU_CUR = CO_MODU_CUR;
                                    m.CO_CUR = CO_CUR;
                                    m.TB01_CURSO = tb01;
                                    m.ID_MATERIA = idMat;
                                    m.QT_CRED_MAT = null;
                                    m.QT_CARG_HORA_MAT = 800;
                                    m.DT_INCL_MAT = DateTime.Now;
                                    m.DT_SITU_MAT = DateTime.Now;
                                    m.CO_SITU_MAT = "I";
                                    //TB02_MATERIA.SaveOrUpdate(m);
                                    ctx.AddToTB02_MATERIA(m);

                                    //ctx.SaveChanges();

                                    if (!TB079_HIST_ALUNO.RetornaTodosRegistros().Where(hi => hi.CO_EMP == CO_EMP && hi.CO_MODU_CUR == CO_MODU_CUR && hi.CO_CUR == CO_CUR && hi.CO_MAT == dff.CO_MAT).Any())
                                    {
                                        TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                                        h.CO_EMP = CO_EMP;
                                        h.CO_ALU = dff.CO_ALU;
                                        h.CO_MODU_CUR = CO_MODU_CUR;
                                        h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                                        h.CO_CUR = CO_CUR;
                                        h.CO_ANO_REF = CO_ANO_REFER.ToString();
                                        h.CO_MAT = dff.CO_MAT.Value;
                                        h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                                        h.CO_TUR = CO_TUR;
                                        h.DT_LANC = DateTime.Now;
                                        h.CO_USUARIO = LoginAuxili.CO_COL;
                                        h.FL_TIPO_LANC_MEDIA = "N";
                                        h.CO_FLAG_STATUS = "I";
                                        //TB079_HIST_ALUNO.SaveOrUpdate(h);
                                        ctx.AddToTB079_HIST_ALUNO(h);
                                    }

                                    //ctx.SaveChanges();
                                }
                                else
                                {
                                    if (!TB079_HIST_ALUNO.RetornaTodosRegistros().Where(hi => hi.CO_EMP == CO_EMP && hi.CO_MODU_CUR == CO_MODU_CUR && hi.CO_CUR == CO_CUR && hi.CO_MAT == dff.CO_MAT).Any())
                                    {
                                        TB079_HIST_ALUNO h = new TB079_HIST_ALUNO();

                                        h.CO_EMP = CO_EMP;
                                        h.CO_ALU = dff.CO_ALU;
                                        h.CO_MODU_CUR = CO_MODU_CUR;
                                        h.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(CO_MODU_CUR);
                                        h.CO_CUR = CO_CUR;
                                        h.CO_ANO_REF = CO_ANO_REFER.ToString();
                                        h.CO_MAT = dff.CO_MAT.Value;
                                        h.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(CO_EMP);
                                        h.CO_TUR = CO_TUR;
                                        h.DT_LANC = DateTime.Now;
                                        h.CO_USUARIO = LoginAuxili.CO_COL;
                                        h.FL_TIPO_LANC_MEDIA = "N";
                                        h.CO_FLAG_STATUS = "I";
                                        //TB079_HIST_ALUNO.SaveOrUpdate(h);

                                        //ctx.SaveChanges();
                                        ctx.AddToTB079_HIST_ALUNO(h);
                                    }
                                }
                            }
                        }
                        #endregion

                        int c = res.Where(w => w.DT_FRE == df.DT_FRE && w.CO_FLAG_FREQ_ALUNO == "S" && w.CO_ALU == dff.CO_ALU).ToList().Count();
                        if (c == 0)
                        {
                            if (ldfa.Where(w => w.CO_ALU == dff.CO_ALU).Any())
                            {
                                DiasFreqAlu dfa = ldfa.Where(w => w.CO_ALU == dff.CO_ALU).FirstOrDefault();
                                dfa.CO_ALU = dff.CO_ALU;
                                dfa.CO_BIMESTRE = dff.CO_BIMESTRE;
                                dfa.QTD_FREQ = dfa.QTD_FREQ + 1;
                            }
                            else
                            {
                                DiasFreqAlu dfa = new DiasFreqAlu();
                                dfa.CO_ALU = dff.CO_ALU;
                                dfa.CO_BIMESTRE = dff.CO_BIMESTRE;
                                dfa.QTD_FREQ = 1;
                                ldfa.Add(dfa);
                            }
                        }
                    }
                }

                #endregion

                #region Gravação das faltas contabilizadas no histórico

                //===> Passa pela lista de alunos, com as faltas contabilizadas, e grava no histórico
                foreach (DiasFreqAlu dfa in ldfa)
                {
                    TB079_HIST_ALUNO tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(dfa.CO_ALU, CO_MODU_CUR, CO_CUR, CO_ANO_REFER.ToString(), primeiraMeteria);

                    var resM = ctx.ExecuteStoreQuery<MatriculaSitu>("select CO_SIT_MAT from TB08_MATRCUR where CO_EMP = " + CO_EMP + " and CO_ALU = " + dfa.CO_ALU + " and CO_CUR = " + CO_CUR + " and CO_ANO_MES_MAT = " + CO_ANO_REFER.ToString()).ToList();

                    if (resM.Count != 0)
                    {
                        if (resM.FirstOrDefault().CO_SIT_MAT == "A")
                        {

                            if (tb079 == null)
                            {
                                AuxiliPagina.EnvioMensagemErro(page, "Histórico não encontrado para o aluno '" + ctx.TB07_ALUNO.Where(w => w.CO_ALU == dfa.CO_ALU).FirstOrDefault().NO_ALU + "'");
                            }
                            else
                            {
                                switch (dfa.CO_BIMESTRE)
                                {
                                    case "B1":
                                        tb079.QT_FALTA_BIM1 = dfa.QTD_FREQ;
                                        break;
                                    case "B2":
                                        tb079.QT_FALTA_BIM2 = dfa.QTD_FREQ;
                                        break;
                                    case "B3":
                                        tb079.QT_FALTA_BIM3 = dfa.QTD_FREQ;
                                        break;
                                    case "B4":
                                        tb079.QT_FALTA_BIM4 = dfa.QTD_FREQ;
                                        break;
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }

            ctx.SaveChanges();

            return t;
        }

        #region Classe para o Cálculo da Média por Turma
        public class MediaClasse
        {
            public int coMat { get; set; }
            public decimal? MCB1 { get; set; }
            public decimal? MCB2 { get; set; }
            public decimal? MCB3 { get; set; }
            public decimal? MCB4 { get; set; }
        }
        #endregion

        /// <summary>
        /// Retorna a média das notas de todos os alunos de uma turma em uma matéria.
        /// </summary>
        /// <param name="coModuCur">Código da modalidade</param>
        /// <param name="coCur">Código do Curso/Série</param>
        /// <param name="coTur">Código da Turma</param>
        /// <param name="coMat">Código da Matéria</param>
        /// <param name="anoRef">Ano de referência</param>
        /// <param name="Bim">Bimestre utilizado no cálculo</param>
        /// <returns></returns>
        public static decimal? CalculaMediaTurma(int coModuCur, int coCur, int coTur, int coMat, decimal anoRef, int Bim)
        {
            decimal? mt = 0;

            var ctx = GestorEntities.CurrentContext;

            var res = ctx.ExecuteStoreQuery<MediaClasse>("select h.CO_MAT, AVG(h.VL_NOTA_BIM1) as MCB1, AVG(h.VL_NOTA_BIM2) as MCB2, AVG(h.VL_NOTA_BIM3) as MCB3, AVG(h.VL_NOTA_BIM4) as MCB from TB07_ALUNO a inner join TB08_MATRCUR ma on (ma.CO_ALU = a.CO_ALU) inner join TB44_MODULO mo on (ma.CO_MODU_CUR = mo.CO_MODU_CUR) inner join TB01_CURSO c on (ma.CO_CUR = c.CO_CUR) inner join TB06_TURMAS t on (ma.CO_TUR = t.CO_TUR) inner join TB079_HIST_ALUNO h on (h.CO_ALU = a.CO_ALU and h.CO_ANO_REF = ma.CO_ANO_MES_MAT and h.CO_MODU_CUR = ma.CO_MODU_CUR and h.CO_CUR = ma.CO_CUR) where ma.CO_EMP = " + LoginAuxili.CO_EMP + " and ma.CO_ANO_MES_MAT = " + anoRef + " and ma.CO_MODU_CUR = " + coModuCur + " and ma.CO_CUR = " + coCur + " and ma.CO_TUR = " + coTur + " and h.CO_MAT = " + coMat + " group by h.CO_MAT");

            foreach (MediaClasse mc in res)
            {
                switch (Bim)
                {
                    case 1:
                        mt = mc.MCB1;
                        break;
                    case 2:
                        mt = mc.MCB2;
                        break;
                    case 3:
                        mt = mc.MCB3;
                        break;
                    case 4:
                        mt = mc.MCB4;
                        break;
                }
            }

            return mt;
        }

        /// <summary>
        /// Realiza a troca de status de pré-matrícula para matrícula ativa
        /// </summary>
        /// <param name="coAlu">Codigo do aluno</param>
        /// <param name="coSer">codigo da Série</param>
        /// <param name="ano"></param>
        /// <param name="semestreLet"></param>
        /// <returns>Retorna se operação foi realizada ou não com sucesso</returns>
        public static Boolean AtivarPreMatricula(int coAlu, int coSer, string ano, string semestreLet)
        {
            bool retorno = true;
            try
            {
                TB08_MATRCUR mat = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, coSer, ano, semestreLet);
                var matriculasAtuais = (from ma in TB08_MATRCUR.RetornaTodosRegistros()
                                        where ma.CO_ALU == coAlu
                                        && ma.CO_SIT_MAT == "A"
                                        select ma).ToList();
                if (matriculasAtuais == null || matriculasAtuais.Count() <= 0)
                {
                    if (mat != null && mat.CO_SIT_MAT == "R")
                    {
                        ///Histórico
                        TB209_HISTO_MATRICULA tb209 = new TB209_HISTO_MATRICULA();
                        tb209.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(mat.CO_EMP);
                        tb209.CO_ALU_CAD = mat.CO_ALU_CAD;
                        tb209.CO_STA_ALT = mat.CO_SIT_MAT;
                        tb209.CO_STA_ATUAL = "A";
                        tb209.DE_OBS = "Ativação de pré-matrícula";
                        tb209.DT_ALT = DateTime.Now;
                        tb209.TB03_COLABOR = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                        TB209_HISTO_MATRICULA.SaveOrUpdate(tb209, false);
                        ///Matrícula
                        mat.CO_SIT_MAT = "A";
                        TB08_MATRCUR.SaveOrUpdate(mat, false);
                        ///Aluno
                        TB07_ALUNO aluno = TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, mat.CO_EMP);
                        if (aluno.CO_SITU_ALU == "R")
                        {
                            aluno.CO_SITU_ALU = "A";
                            TB07_ALUNO.SaveOrUpdate(aluno, false);
                        }
                        ///Contas a receber
                        var contas = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                      where tb47.CO_ALU == mat.CO_ALU
                                      && tb47.CO_ANO_MES_MAT == "2014"
                                      && tb47.IC_SIT_DOC == "R"
                                      && tb47.CO_CUR == mat.CO_CUR
                                      && tb47.TP_CLIENTE_DOC == "A"
                                      && tb47.CO_EMP == mat.CO_EMP
                                      select tb47).DefaultIfEmpty();
                        if (contas != null && contas.Count() > 0)
                        {
                            foreach (var linha in contas)
                            {
                                linha.IC_SIT_DOC = "A";
                                TB47_CTA_RECEB.SaveOrUpdate((TB47_CTA_RECEB)linha, false);
                            }
                        }
                        ///Grade
                        var grades = (from tb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                      where tb48.CO_ALU == mat.CO_ALU
                                      && tb48.CO_ANO_MES_MAT == mat.CO_ANO_MES_MAT
                                      && tb48.CO_CUR == mat.CO_CUR
                                      && tb48.CO_EMP == mat.CO_EMP
                                      && tb48.CO_STAT_MATE == "R"
                                      select tb48).DefaultIfEmpty();
                        if (grades != null && grades.Count() > 0)
                        {
                            foreach (var linha in grades)
                            {
                                linha.CO_STAT_MATE = "A";
                                TB48_GRADE_ALUNO.SaveOrUpdate((TB48_GRADE_ALUNO)linha, false);
                            }
                        }

                        GestorEntities.CurrentContext.SaveChanges();

                    }
                    else
                        retorno = false;
                }
                else
                    retorno = false;
            }
            catch (Exception e)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Responsável por coletar o nome da funcionalidade cadastrada no banco e retornar o nome correspondente
        /// </summary>
        /// <param name="URLFUNCIONALIDADE"></param>
        /// <returns>Nome da funcionalidade no banco de dados</returns>
        public static string RetornaNomeFuncionalidadeCadastrada(string URLFUNCIONALIDADE)
        {
            var res = (from adm in ADMMODULO.RetornaTodosRegistros()
                       where adm.nomURLModulo == URLFUNCIONALIDADE
                       select new { adm.nomItemMenu }).FirstOrDefault();
            return (res != null ? res.nomItemMenu : "");
        }

        /// <summary>
        /// Retorna o nome correspondente ao código da classificação de risco recebido como parâmetro
        /// </summary>
        /// <param name="CO_CLASS_PROFI"></param>
        /// <returns></returns>
        public static string GetNomeClassificacaoFuncional(string CO_CLASS_PROFI, bool Sigla = false)
        {
            switch (CO_CLASS_PROFI)
            {
                case "E":
                    return (Sigla ? "ENFE" : "Enfermeiro(a)");
                case "M":
                    return (Sigla ? "MEDI" : "Médico(a)");
                case "D":
                    return (Sigla ? "ODON" : "Odontólogo(a)");
                case "S":
                    return (Sigla ? "ESTT" : "Esteticista");
                case "N":
                    return (Sigla ? "NUTR" : "Nutricionista");
                case "I":
                    return (Sigla ? "FISI" : "Fisioterapeuta");
                case "F":
                    return (Sigla ? "FONO" : "Fonoaudiólogo(a)");
                case "P":
                    return (Sigla ? "PSIC" : "Psicólogo(a)");
                case "T":
                    return (Sigla ? "TEOC" : "Terapeuta Ocupacional");
                case "O":
                    return (Sigla ? "TRIA" : "Triagem");
                default:
                    return " - ";
            }
        }

        #region Funções de estoque
        /// <summary>
        /// Valida se existe produto em estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Unidade</param>
        /// <param name="qt">Quantidade da movimentação</param>
        /// <returns>0 - Erro; 1 - Saldo Suficiente; 2 - Saldo Insuficiente; 3 - Produto não cadastrado</returns>
        public static int validaEstoque(int coProd, int coEmp, decimal qt)
        {
            try
            {
                int r = 0;

                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);

                if (est != null)
                {
                    if (est.QT_SALDO_EST <= qt)
                    {
                        r = 1;
                    }
                    else
                    {
                        r = 2;
                    }
                }
                else
                {
                    r = 3;
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza a movimentação do estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Código da unidade em que a movimentação está sendo realizada</param>
        /// <param name="qtd">Quantidade que está sendo movimentada</param>
        /// <param name="tpMov">Tipo de movimentação</param>
        /// <param name="nuDoc">Número do documento</param>
        /// <param name="dtMov">Data da movimentação</param>
        /// <param name="nomUsu">Nome do usuário que realizou a movimentação</param>
        /// <param name="coEmpTrans">Unidade que recebe os produtos movimentados</param>
        /// <returns></returns>
        public static int movimentaEstoque(int coProd, int coEmp, decimal qtd, int tpMov, string nuDoc, DateTime dtMov, int coEmpTrans)
        {
            try
            {
                int r = 1;
                int ve = validaEstoque(coProd, coEmp, qtd);
                RegistroLog log = new RegistroLog();

                TB93_TIPO_MOVIMENTO tp = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(tpMov);
                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);
                string nomUsu = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, LoginAuxili.CO_COL).NO_COL;

                if (ve == 1)
                {

                    TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                    mov.CO_PROD = coProd;
                    mov.CO_EMP = coEmp;
                    mov.QT_MOV_PROD = qtd;
                    mov.TB93_TIPO_MOVIMENTO = tp;
                    mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                    mov.DT_MOV_PROD = dtMov;
                    mov.DT_ALT_REGISTRO = DateTime.Now;
                    mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                    mov.CO_COL = LoginAuxili.CO_COL;
                    mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                    mov.CO_EMP_TRANS_ESTOQ = coEmpTrans;

                    TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                    log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                    decimal totEst = est.QT_SALDO_EST;
                    if (tp.FLA_TP_MOV == "S")
                    {
                        est.QT_SALDO_EST = totEst - qtd;
                    }
                    else
                    {
                        est.QT_SALDO_EST = totEst + qtd;
                    }
                    TB96_ESTOQUE.SaveOrUpdate(est, true);
                    log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                    r = 1;
                }
                else
                {
                    if (tp.FLA_TP_MOV == "E")
                    {
                        TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                        mov.CO_PROD = coProd;
                        mov.CO_EMP = coEmp;
                        mov.QT_MOV_PROD = qtd;
                        mov.TB93_TIPO_MOVIMENTO = tp;
                        mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                        mov.DT_MOV_PROD = dtMov;
                        mov.DT_ALT_REGISTRO = DateTime.Now;
                        mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        mov.CO_COL = LoginAuxili.CO_COL;
                        mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                        mov.CO_EMP_TRANS_ESTOQ = coEmpTrans;

                        TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                        log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                        est = new TB96_ESTOQUE();
                        est.TB90_PRODUTO.CO_PROD = coProd;
                        est.QT_SALDO_EST = qtd;
                        est.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        est.DT_ALT_REGISTRO = DateTime.Now;
                        est.CO_COL = LoginAuxili.CO_COL;
                        est.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB96_ESTOQUE.SaveOrUpdate(est, true);
                        log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                        r = 1;
                    }
                    else
                    {
                        r = ve;
                    }
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza a movimentação do estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Código da unidade em que a movimentação está sendo realizada</param>
        /// <param name="qtd">Quantidade do produto que está sendo movimentada</param>
        /// <param name="tpMov">Tipo de movimentação realizada</param>
        /// <param name="nuDoc">Número do documento referenciado na movimentação</param>
        /// <param name="dtMov">Data da movimentação</param>
        /// <param name="nomUsu">Nome do usuário que realizou a mocimentação</param>
        /// <param name="flInv">Flag que diz se a movimentação veio de um inventário</param>
        /// <returns></returns>
        public static int movimentaEstoque(int coProd, int coEmp, decimal qtd, int tpMov, string nuDoc, DateTime dtMov, bool flInv)
        {
            try
            {
                int r = 1;
                int ve = validaEstoque(coProd, coEmp, qtd);
                RegistroLog log = new RegistroLog();

                TB93_TIPO_MOVIMENTO tp = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(tpMov);
                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);
                string nomUsu = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, LoginAuxili.CO_COL).NO_COL;

                if (ve == 1)
                {

                    TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                    mov.CO_PROD = coProd;
                    mov.CO_EMP = coEmp;
                    mov.QT_MOV_PROD = qtd;
                    mov.TB93_TIPO_MOVIMENTO = tp;
                    mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                    mov.DT_MOV_PROD = dtMov;
                    mov.DT_ALT_REGISTRO = DateTime.Now;
                    mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                    mov.CO_COL = LoginAuxili.CO_COL;
                    mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                    mov.FL_INVEN = flInv ? "S" : "N";

                    TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                    log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                    decimal totEst = est.QT_SALDO_EST;
                    if (tp.FLA_TP_MOV == "S")
                    {
                        est.QT_SALDO_EST = totEst - qtd;
                    }
                    else
                    {
                        est.QT_SALDO_EST = totEst + qtd;
                    }
                    TB96_ESTOQUE.SaveOrUpdate(est, true);
                    log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                    r = 1;
                }
                else
                {
                    if (tp.FLA_TP_MOV == "E")
                    {
                        TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                        mov.CO_PROD = coProd;
                        mov.CO_EMP = coEmp;
                        mov.QT_MOV_PROD = qtd;
                        mov.TB93_TIPO_MOVIMENTO = tp;
                        mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                        mov.DT_MOV_PROD = dtMov;
                        mov.DT_ALT_REGISTRO = DateTime.Now;
                        mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        mov.CO_COL = LoginAuxili.CO_COL;
                        mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                        mov.FL_INVEN = flInv ? "S" : "N";

                        TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                        log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                        est = new TB96_ESTOQUE();
                        est.TB90_PRODUTO.CO_PROD = coProd;
                        est.QT_SALDO_EST = qtd;
                        est.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        est.DT_ALT_REGISTRO = DateTime.Now;
                        est.CO_COL = LoginAuxili.CO_COL;
                        est.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB96_ESTOQUE.SaveOrUpdate(est, true);
                        log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                        r = 1;
                    }
                    else
                    {
                        r = ve;
                    }
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza a movimentação do estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Código da unidade em que a movimentação está seno realizada</param>
        /// <param name="qtd">Quantidade do produto que está sendo movimentada</param>
        /// <param name="tpMov">Tipo de movimentação</param>
        /// <param name="nuDoc">Número do documento referenciado na movimentação</param>
        /// <param name="dtMov">Data da movimentação</param>
        /// <param name="nomUsu">Nome do usuário que realizou a movimentação</param>
        /// <param name="coEmpTrans">Código da unidade que recebeu o produto movimentado</param>
        /// <param name="coDepto">Código do departamento, dentro da unidade de transferência, que recebeu o produto movimentado</param>
        /// <returns></returns>
        public static int movimentaEstoque(int coProd, int coEmp, decimal qtd, int tpMov, string nuDoc, DateTime dtMov, int coEmpTrans, int coDepto)
        {
            try
            {
                int r = 1;
                int ve = validaEstoque(coProd, coEmp, qtd);
                RegistroLog log = new RegistroLog();

                TB93_TIPO_MOVIMENTO tp = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(tpMov);
                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);
                string nomUsu = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, LoginAuxili.CO_COL).NO_COL;

                if (ve == 1)
                {

                    TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                    mov.CO_PROD = coProd;
                    mov.CO_EMP = coEmp;
                    mov.QT_MOV_PROD = qtd;
                    mov.TB93_TIPO_MOVIMENTO = tp;
                    mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                    mov.DT_MOV_PROD = dtMov;
                    mov.DT_ALT_REGISTRO = DateTime.Now;
                    mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                    mov.CO_COL = LoginAuxili.CO_COL;
                    mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                    mov.CO_EMP_TRANS_ESTOQ = coEmpTrans;
                    mov.TB14_DEPTO =  TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);

                    TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                    log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                    decimal totEst = est.QT_SALDO_EST;
                    if (tp.FLA_TP_MOV == "S")
                    {
                        est.QT_SALDO_EST = totEst - qtd;
                    }
                    else
                    {
                        est.QT_SALDO_EST = totEst + qtd;
                    }
                    TB96_ESTOQUE.SaveOrUpdate(est, true);
                    log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                    r = 1;
                }
                else
                {
                    if (tp.FLA_TP_MOV == "E")
                    {
                        TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                        mov.CO_PROD = coProd;
                        mov.CO_EMP = coEmp;
                        mov.QT_MOV_PROD = qtd;
                        mov.TB93_TIPO_MOVIMENTO = tp;
                        mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                        mov.DT_MOV_PROD = dtMov;
                        mov.DT_ALT_REGISTRO = DateTime.Now;
                        mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        mov.CO_COL = LoginAuxili.CO_COL;
                        mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                        mov.CO_EMP_TRANS_ESTOQ = coEmpTrans;
                        mov.TB14_DEPTO = TB14_DEPTO.RetornaPelaChavePrimaria(coDepto);

                        TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                        log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                        est = new TB96_ESTOQUE();
                        est.TB90_PRODUTO.CO_PROD = coProd;
                        est.QT_SALDO_EST = qtd;
                        est.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        est.DT_ALT_REGISTRO = DateTime.Now;
                        est.CO_COL = LoginAuxili.CO_COL;
                        est.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB96_ESTOQUE.SaveOrUpdate(est, true);
                        log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                        r = 1;
                    }
                    else
                    {
                        r = ve;
                    }
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza a movimentação do estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Código da unidade em que a movimentação está sendo realizada</param>
        /// <param name="qtd">Quantidade do produto que está sendo movimentada</param>
        /// <param name="tpMov">Tipo de movimentação</param>
        /// <param name="nuDoc">Número do documento referenciado na movimentação</param>
        /// <param name="dtMov">Data da movimentação</param>
        /// <param name="nomUsu">Nome do usuário que está realizando a movimentação</param>
        /// <param name="coForn">Código do fornecedor do produto movimentado</param>
        /// <returns></returns>
        public static int movimentaEstoque(int coProd, int coEmp, decimal qtd, int tpMov, string nuDoc, int coForn, DateTime dtMov)
        {
            try
            {
                int r = 1;
                int ve = validaEstoque(coProd, coEmp, qtd);
                RegistroLog log = new RegistroLog();

                TB93_TIPO_MOVIMENTO tp = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(tpMov);
                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);
                string nomUsu = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, LoginAuxili.CO_COL).NO_COL;

                if (ve == 1)
                {

                    TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                    mov.CO_PROD = coProd;
                    mov.CO_EMP = coEmp;
                    mov.QT_MOV_PROD = qtd;
                    mov.TB93_TIPO_MOVIMENTO = tp;
                    mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                    mov.DT_MOV_PROD = dtMov;
                    mov.DT_ALT_REGISTRO = DateTime.Now;
                    mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                    mov.CO_COL = LoginAuxili.CO_COL;
                    mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                    mov.CO_FORN = coForn;

                    TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                    log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                    decimal totEst = est.QT_SALDO_EST;
                    if (tp.FLA_TP_MOV == "S")
                    {
                        est.QT_SALDO_EST = totEst - qtd;
                    }
                    else
                    {
                        est.QT_SALDO_EST = totEst + qtd;
                    }
                    TB96_ESTOQUE.SaveOrUpdate(est, true);
                    log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                    r = 1;
                }
                else
                {
                    if (tp.FLA_TP_MOV == "E")
                    {
                        TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                        mov.CO_PROD = coProd;
                        mov.CO_EMP = coEmp;
                        mov.QT_MOV_PROD = qtd;
                        mov.TB93_TIPO_MOVIMENTO = tp;
                        mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                        mov.DT_MOV_PROD = dtMov;
                        mov.DT_ALT_REGISTRO = DateTime.Now;
                        mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        mov.CO_COL = LoginAuxili.CO_COL;
                        mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                        mov.CO_FORN = coForn;

                        TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                        log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                        est = new TB96_ESTOQUE();
                        est.TB90_PRODUTO.CO_PROD = coProd;
                        est.QT_SALDO_EST = qtd;
                        est.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        est.DT_ALT_REGISTRO = DateTime.Now;
                        est.CO_COL = LoginAuxili.CO_COL;
                        est.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB96_ESTOQUE.SaveOrUpdate(est, true);
                        log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                        r = 1;
                    }
                    else
                    {
                        r = ve;
                    }
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que realiza a movimentação do estoque
        /// </summary>
        /// <param name="coProd">Código do produto</param>
        /// <param name="coEmp">Código da unidade em que a movimentação está sendo realizada</param>
        /// <param name="qtd">Quantidade do produto que está sendo movimentada</param>
        /// <param name="tpMov">Tipo de movimentação</param>
        /// <param name="nuDoc">Número do documento referenciado a movimentação</param>
        /// <param name="dtMov">Data da movimentação</param>
        /// <returns></returns>
        public static int movimentaEstoque(int coProd, int coEmp, decimal qtd, int tpMov, string nuDoc, DateTime dtMov)
        {
            try
            {
                int r = 1;
                int ve = validaEstoque(coProd, coEmp, qtd);
                RegistroLog log = new RegistroLog();

                TB93_TIPO_MOVIMENTO tp = TB93_TIPO_MOVIMENTO.RetornaPelaChavePrimaria(tpMov);
                TB96_ESTOQUE est = TB96_ESTOQUE.RetornaPelaChavePrimaria(coEmp, coProd);
                string nomUsu = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, LoginAuxili.CO_COL).NO_COL;

                if (ve == 1)
                {

                    TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                    mov.CO_PROD = coProd;
                    mov.CO_EMP = coEmp;
                    mov.QT_MOV_PROD = qtd;
                    mov.TB93_TIPO_MOVIMENTO = tp;
                    mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                    mov.DT_MOV_PROD = dtMov;
                    mov.DT_ALT_REGISTRO = DateTime.Now;
                    mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                    mov.CO_COL = LoginAuxili.CO_COL;
                    mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                    TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                    log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                    decimal totEst = est.QT_SALDO_EST;
                    if (tp.FLA_TP_MOV == "S")
                    {
                        est.QT_SALDO_EST = totEst - qtd;
                    }
                    else
                    {
                        est.QT_SALDO_EST = totEst + qtd;
                    }
                    TB96_ESTOQUE.SaveOrUpdate(est, true);
                    log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                    r = 1;
                }
                else
                {
                    if (tp.FLA_TP_MOV == "E")
                    {
                        TB91_MOV_PRODUTO mov = new TB91_MOV_PRODUTO();

                        mov.CO_PROD = coProd;
                        mov.CO_EMP = coEmp;
                        mov.QT_MOV_PROD = qtd;
                        mov.TB93_TIPO_MOVIMENTO = tp;
                        mov.NU_DOC_PROD = nuDoc != "" ? nuDoc : null;
                        mov.DT_MOV_PROD = dtMov;
                        mov.DT_ALT_REGISTRO = DateTime.Now;
                        mov.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        mov.CO_COL = LoginAuxili.CO_COL;
                        mov.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB91_MOV_PRODUTO.SaveOrUpdate(mov, true);
                        log.RegistroLOG(mov, RegistroLog.ACAO_GRAVAR);

                        est = new TB96_ESTOQUE();
                        est.TB90_PRODUTO.CO_PROD = coProd;
                        est.QT_SALDO_EST = qtd;
                        est.NOM_USUARIO = nomUsu.ToString().Substring(0, (nomUsu.Length > 30 ? 29 : nomUsu.Length));
                        est.DT_ALT_REGISTRO = DateTime.Now;
                        est.CO_COL = LoginAuxili.CO_COL;
                        est.CO_EMP_COL = LoginAuxili.CO_EMP;

                        TB96_ESTOQUE.SaveOrUpdate(est, true);
                        log.RegistroLOG(est, RegistroLog.ACAO_EDICAO);

                        r = 1;
                    }
                    else
                    {
                        r = ve;
                    }
                }

                return r;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}