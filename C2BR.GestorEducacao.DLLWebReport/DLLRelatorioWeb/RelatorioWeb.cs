//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.DLLRelatorioWeb
{
    public class RelatorioWeb : IRelatorioWeb
    {
        [DllImport("WR_RelCustoFinFunc.dll")]
        public static extern int DLLRelCustoFinFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_EMP_SELEC, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [DllImport("WR_RelCustoFinFuncDept.dll")]
        public static extern int DLLRelCustoFinFuncDept(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_DEPTO, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [DllImport("WR_RelCustoFinFuncFunc.dll")]
        public static extern int DLLRelCustoFinFuncFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [DllImport("WR_RelCustoFinFuncTpcon.dll")]
        public static extern int DLLRelCustoFinFuncTpcon(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_TPCON, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelPlanRealizCentroCusto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM, string strP_CO_DEPTO);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLFrmRelPlanRealizado(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelRelacFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_FUN, string strP_CO_INST, string strP_TP_DEF, string strP_CO_SEXO_COL, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [DllImport("WR_RelFicCadFuncionario.dll")]
        public static extern int DLLRelFicCadFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_BASE, string strP_FLA_PROFESSOR, string strP_TP_PONTO);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelAniversarioFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_MES);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelGerMapaFrequenciaFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelGerMapaFrequenciaProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelExtratoFreqLivre(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER);

        [DllImport("WR_RelExtratoFreqFunc.dll")]
        public static extern int DLLRelExtratoFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER, string strP_FLA_PROFESSOR);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelCanhoto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelAvaliacaoModelo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NUM_PESQ);

        [DllImport("GestorWebReport.dll")]
        public static extern int DLLRelListagemProva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_MES_MAT, string strP_CO_BIMESTRE);

        [DllImport("WR_RelAnaliticoResAvaliacao.dll")]
        public static extern int DLLRelAnaliticoResAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_PESQ_AVAL, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM, string strP_CO_COL, string strP_NO_CUR, string strP_NO_TUR, string strP_NO_MAT);

        [DllImport("WR_RelCurvaABCFreqFunc.dll")]
        public static extern int DLLRelCurvaABCFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [DllImport("WR_RelCurvaABCFreqFuncInst.dll")]
        public static extern int DLLRelCurvaABCFreqFuncInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [DllImport("WR_RelCurvaABCFreqProf.dll")]
        public static extern int DLLRelCurvaABCFreqProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [DllImport("WR_RelCurvaABCFreqProfInst.dll")]
        public static extern int DLLRelCurvaABCFreqProfInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [DllImport("WR_RelMapadePlanejAnualMatricula.dll")]
        public static extern int DLLRelMapadePlanejAnualMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_DPTO_CUR, string strP_CO_ANO_REF);

        [DllImport("WR_RelGradeHorario.dll")]
        public static extern int DLLRelGradeHorario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR);

        [DllImport("WR_RelRelacAlunoTurma.dll")]
        public static extern int DLLRelRelacAlunoTurma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [DllImport("WR_RelDemonstrativoReserva.dll")]
        public static extern int DLLRelDemonstrativoReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [DllImport("WR_RelDemonstrativoInscricao.dll")]
        public static extern int DLLRelDemonstrativoInscricao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [DllImport("WR_RelDemonstrativoMatricula.dll")]
        public static extern int DLLRelDemonstrativoMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [DllImport("WR_RelMapaGeralMatricula.dll")]
        public static extern int DLLRelMapaGeralMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ANO_REFER);

        [DllImport("WR_RelMatricEfetivadas.dll")]
        public static extern int DLLRelMatricEfetivadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_CO_MODU_CUR, string strP_DES_MODU_CUR, string strP_CO_CUR);

        [DllImport("WR_RelMapaCaracteristicaMatricula.dll")]
        public static extern int DLLRelMapaCaracteristicaMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [DllImport("WR_RelExtSolicitAluno.dll")]
        public static extern int DLLRelExtSolicitAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_SITU);

        [DllImport("WR_RelSolicRealiz.dll")]
        public static extern int DLLRelSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_DT_INI, string strP_DT_FIM, string strP_SITU);

        [DllImport("WR_RelMapaSolicRealiz.dll")]
        public static extern int DLLRelMapaSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER);

        [DllImport("WR_RelMapaSolicRealizAnual.dll")]
        public static extern int DLLRelMapaSolicRealizAnual(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [DllImport("WR_RelPosicReserva.dll")]
        public static extern int DLLRelPosicReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelGradeCurricular.dll")]
        public static extern int DLLRelGradeCurricular(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [DllImport("WR_RelInformacaoCurso.dll")]
        public static extern int DLLRelInformacaoCurso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_NIVEL_CUR, string strP_CO_DPTO_CUR, string strP_CO_SUB_DPTO_CUR, string strP_CO_SITU);

        [DllImport("WR_RelRelacAluno.dll")]
        public static extern int DLLRelRelacAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [DllImport("WR_RelMapaEstaticSolic.dll")]
        public static extern int DLLRelMapaEstaticSolic(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP, string strP_TP_SIT, string strP_DE_SIT, string strP_CO_ANO_REF, string strP_TP_VISU);

        [DllImport("WR_RelCurvaABCSolicitacoes.dll")]
        public static extern int DLLRelCurvaABCSolicitacoes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_SOLIC, string strP_DT_INI, string strP_DT_FIM, string strP_ISENT);

        [DllImport("WR_RelHistFreqAlu.dll")]
        public static extern int DLLRelHistFreqAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_NO_ALU, string strP_CO_PARAM_FREQUE, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelFreqAluno.dll")]
        public static extern int DLLRelFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_REF, string strP_CO_PARAM_FREQUE, string strP_MES, string strP_DE_MES, string strP_CNPJ_INSTI);

        [DllImport("WR_RelEvasaoEscolar.dll")]
        public static extern int DLLRelEvasaoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_MAT, string strP_DDL_SEL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelMapaAnualFaltas.dll")]
        public static extern int DLLRelMapaAnualFaltas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT);

        [DllImport("WR_RelMapaFreqAluno.dll")]
        public static extern int DLLRelMapaFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT, string strP_CO_PARAM_FREQ, string strP_CO_PARAM_FREQ_TIPO);

        [DllImport("WR_RelPautaChamadaFrente.dll")]
        public static extern int DLLRelPautaChamadaFrente(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_MAT, string strP_NUM_MES, string strP_DES_MES);

        [DllImport("WR_RelPautaChamadaVerso.dll")]
        public static extern int DLLRelPautaChamadaVerso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [DllImport("WR_relCurvaABCFreq.dll")]
        public static extern int DLLrelCurvaABCFreq(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR,
             string strP_CO_ANO_REF, string strP_TP_PRESENCA, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelHistAluno.dll")]
        public static extern int DLLRelHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [DllImport("WR_RelHistoricoEscolar.dll")]
        public static extern int DLLRelHistoricoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR);

        [DllImport("WR_RelFicIndAluno.dll")]
        public static extern int DLLRelFicIndAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_NO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CNPJ_INSTI);

        [DllImport("WR_RelRendimentoEscolar.dll")]
        public static extern int DLLRelRendimentoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [DllImport("WR_RelBolEscAlu.dll")]
        public static extern int DLLRelBolEscAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string strP_CNPJ_INSTI);
        /*[DllImport("WR_RelBolEscAluBARRIOBRA.dll")]
        public static extern int DLLRelBolEscAluBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO);
        [DllImport("WR_RelBolEscAluEDUCAMARIA.dll")]
        public static extern int DLLRelBolEscAluEDUCAMARIA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO);
        */
        [DllImport("WR_RelBolEscAluModelo2.dll")]
        public static extern int DLLRelBolEscAluModelo2(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string str_CO_BIMESTRE, string strP_CNPJ_INSTI);

        [DllImport("WR_RelFinalAlunos.dll")]
        public static extern int DLLRelFinalAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_Classificacao);

        [DllImport("WR_RelMapaCursoTurmaProf.dll")]
        public static extern int DLLRelMapaCursoTurmaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [DllImport("WR_RelFicCadIndRes.dll")]
        public static extern int DLLRelFicCadIndRes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CNPJ_INSTI);

        [DllImport("WR_RelMapaDistResp.dll")]
        public static extern int DLLRelMapaDistResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [DllImport("WR_RelFicInfoAluno.dll")]
        public static extern int DLLRelFicInfoAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [DllImport("WR_RelMapaCaracteristicaMatriculaSerie.dll")]
        public static extern int DLLRelMapaCaracteristicaMatriculaSerie(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CNPJ_INSTI);

        [DllImport("WR_RelDistAluCaract.dll")]
        public static extern int DLLRelDistAluCaract(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);
        [DllImport("WR_RelDistAluCaractBARRIOBRA.dll")]
        public static extern int DLLRelDistAluCaractBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);        

        [DllImport("WR_RelDistAluCarAno.dll")]
        public static extern int DLLRelDistAluCarAno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);
        [DllImport("WR_RelDistAluCarAnoBARRIOBRA.dll")]
        public static extern int DLLRelDistAluCarAnoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [DllImport("WR_RelFicCadInst.dll")]
        public static extern int DLLRelFicCadInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [DllImport("WR_RelUnidEsc.dll")]
        public static extern int DLLRelUnidEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP);

        [DllImport("WR_RelUnidEscNuc.dll")]
        public static extern int DLLRelUnidEscNuc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [DllImport("WR_RelUnidEscBairro.dll")]
        public static extern int DLLRelUnidEscBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [DllImport("WR_RelDemUnidEscBai.dll")]
        public static extern int DLLRelDemUnidEscBai(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [DllImport("WR_RelDemAluUnid.dll")]
        public static extern int DLLRelDemAluUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP, string strP_CO_ANO_REF, string strP_CO_SITU_MAT);

        [DllImport("WR_RelDistAluGeo.dll")]
        public static extern int DLLRelDistAluGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_REL);

        [DllImport("WR_RelGradeNotasAluno.dll")]
        public static extern int DLLRelGradeNotasAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);
        [DllImport("WR_RelGradeNotasAlunoBARRIOBRA.dll")]
        public static extern int DLLRelGradeNotasAlunoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [DllImport("WR_RelDistAluCar.dll")]
        public static extern int DLLRelDistAluCar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE);

        [DllImport("WR_RelDistAluCarBairro.dll")]
        public static extern int DLLRelDistAluCarBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE);

        [DllImport("WR_RelDistAluBolEscola.dll")]
        public static extern int DLLRelDistAluBolEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_BOLSA);

        [DllImport("WR_RelPlanoAula.dll")]
        public static extern int DLLRelPlanoAula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_DT_INI, string strP_DT_FIM, string strP_TP_ATIV, string strP_CO_COL);

        [DllImport("WR_RelAtivRealizProfessor.dll")]
        public static extern int DLLRelAtivRealizProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [DllImport("WR_RelMapaLocalizacao.dll")]
        public static extern int DLLRelMapaLocalizacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP);

        [DllImport("WR_RelAniversarioProfessor.dll")]
        public static extern int DLLRelAniversarioProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_MES, string strP_CO_DEPTO);

        [DllImport("WR_RelRespAluPar.dll")]
        public static extern int DLLRelRespAluPar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_DE_GRAU_PAREN);

        [DllImport("WR_RelDistRespGrauInstrucao.dll")]
        public static extern int DLLRelDistRespGrauInstrucao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_TP_DEF);

        [DllImport("WR_RelGradeAlunos.dll")]
        public static extern int DLLRelGradeAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT);

        [DllImport("WR_RelLisAluInstEspecializadas.dll")]
        public static extern int DLLRelLisAluInstEspecializadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT);

        [DllImport("WR_RelAluPorInsEsp.dll")]
        public static extern int DLLRelAluPorInsEsp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT);

        [DllImport("WR_RelAniversarioAluno.dll")]
        public static extern int DLLRelAniversarioAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_MES);

        [DllImport("WR_RelMapaHistAluno.dll")]
        public static extern int DLLRelMapaHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL);

        [DllImport("WR_RelMapaHistAlunoSerTur.dll")]
        public static extern int DLLRelMapaHistAlunoSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL);

        [DllImport("WR_RelItensAcervo.dll")]
        public static extern int DLLRelItensAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI);

        [DllImport("WR_RelObrasEmprestadas.dll")]
        public static extern int DLLRelObrasEmprestadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [DllImport("WR_RelObrasEmAtraso.dll")]
        public static extern int DLLRelObrasEmAtraso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [DllImport("WR_RelItensEstoque.dll")]
        public static extern int DLLRelItensEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [DllImport("WR_RelPosicaoEstoque.dll")]
        public static extern int DLLRelPosicaoEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [DllImport("WR_RelClientes.dll")]
        public static extern int DLLRelClientes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_CLI, string strP_NO_FAN_CLI);

        [DllImport("WR_RelResumoContasReceber.dll")]
        public static extern int DLLRelResumoContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_NU_DOC, string strP_CO_ALU,
                                   string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_MES_MAT, string strP_CO_TUR);

        [DllImport("WR_RelContasPagar.dll")]
        public static extern int DLLRelContasPagar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_CO_FORN, string strP_NU_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_DOC);

        [DllImport("WR_RelContagemEstoque.dll")]
        public static extern int DLLRelContagemEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [DllImport("WR_RelFornecedores.dll")]
        public static extern int DLLRelFornecedores(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_FORN, string strP_NO_FAN_FOR);

        [DllImport("WR_RelBoletimAluno.dll")]
        public static extern int DLLRelBoletimAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR);

        [DllImport("WR_RelMapaDisciplinaProf.dll")]
        public static extern int DLLRelMapaDisciplinaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI);
        [DllImport("WR_RelMapaDisciplinaProfBarao.dll")]
        public static extern int DLLRelMapaDisciplinaProfBarao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI);

        [DllImport("WR_RelAlunosBolsistas.dll")]
        public static extern int DLLRelAlunosBolsistas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_TIPO_BOLSA);

        [DllImport("WR_RelAluPasEsc.dll")]
        public static extern int DLLRelAluPasEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_LIN_ONI);

        [DllImport("WR_RelMapaEstaticEmpr.dll")]
        public static extern int DLLRelMapaEstaticEmpr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CNPJ_INSTI);

        [DllImport("WR_RelReceitasFixas.dll")]
        public static extern int DLLRelReceitasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES);

        [DllImport("WR_RelRelacaoInativo.dll")]
        public static extern int DLLRelRelacaoInativo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [DllImport("WR_RelContasReceber.dll")]
        public static extern int DLLRelContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_NU_DOC, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_INI, string strP_DT_VEN_FIM,
            string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR, string strP_CO_ANO_MES_MAT);

        [DllImport("WR_RelDespesasFixas.dll")]
        public static extern int DLLRelDespesasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES);

        [DllImport("WR_RelValorAcervo.dll")]
        public static extern int DLLRelValorAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CNPJ_INSTI);

        [DllImport("WR_RelDebitoDocumento.dll")]
        public static extern int DLLRelDebitoDocumento(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CNPJ_INSTI);

        [DllImport("WR_RelHistEscAvalProg.dll")]
        public static extern int DLLRelHistEscAvalProg(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [DllImport("WR_RelMapaFinanceiro.dll")]
        public static extern int DLLRelMapaFinanceiro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_IC_SIT_DOC);

        [DllImport("WR_RelGradeFinanceira.dll")]
        public static extern int DLLRelGradeFinanceira(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU);

        [DllImport("WR_RelInadimplenciaTotal.dll")]
        public static extern int DLLRelInadimplenciaTotal(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [DllImport("WR_RelInadimplenciaPorResp.dll")]
        public static extern int DLLRelInadimplenciaPorResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP);

        [DllImport("WR_RelInadimplencia.dll")]
        public static extern int DLLRelInadimplencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP);

        [DllImport("WR_RelInadimplenciaSerTur.dll")]
        public static extern int DLLRelInadimplenciaSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelCurvaABCCR.dll")]
        public static extern int DLLRelCurvaABCCR(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelEspenhoDiscCursadas.dll")]
        public static extern int DLLRelEspenhoDiscCursadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [DllImport("WR_RelCurvaABCCP.dll")]
        public static extern int DLLRelCurvaABCCP(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelAtivExtraAluno.dll")]
        public static extern int DLLRelAtivExtraAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ATIV_EXTRA);

        [DllImport("WR_RelCalendario.dll")]
        public static extern int DLLRelCalendario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_TP_CALEND);

        [DllImport("WR_RelFicPerfilInst.dll")]
        public static extern int DLLRelFicPerfilInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [DllImport("WR_RelRelacTarefAgendadas.dll")]
        public static extern int DLLRelRelacTarefAgendadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC);

        [DllImport("WR_RelHistoTarefAgendada.dll")]
        public static extern int DLLRelHistoTarefAgendada(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC);

        [DllImport("WR_RelFicEstagio.dll")]
        public static extern int DLLRelFicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG);

        [DllImport("WR_RelLinhaTranspPorAluno.dll")]
        public static extern int DLLRelLinhaTranspPorAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU);

        [DllImport("WR_RelMovimEstoque.dll")]
        public static extern int DLLRelMovimEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_PROD, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelComprEmprBibli.dll")]
        public static extern int DLLRelComprEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_DESC_USU, string strP_CNPJ_INSTI);

        [DllImport("WR_RelObrasReservadas.dll")]
        public static extern int DLLRelObrasReservadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_CO_CLAS, string strP_CO_ISBN_ACER, string strP_CO_TP_USU, string strP_CO_USU, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [DllImport("WR_RelRelacUsuBiblioteca.dll")]
        public static extern int DLLRelRelacUsuBiblioteca(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI);

        [DllImport("WR_RelExtUsuario.dll")]
        public static extern int DLLRelExtUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI);

        [DllImport("WR_RelEvolRecDes.dll")]
        public static extern int DLLRelEvolRecDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [DllImport("WR_RelEvolRecEDes.dll")]
        public static extern int DLLRelEvolRecEDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [DllImport("WR_RelComprReserMatric.dll")]
        public static extern int DLLRelComprReserMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NU_RESERVA);

        [DllImport("WR_RelRelacOfertEstagio.dll")]
        public static extern int DLLRelRelacOfertEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ESTAG);

        [DllImport("WR_RelRelacSolicEstagio.dll")]
        public static extern int DLLRelRelacSolicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG, string strP_ID_CANDID_ESTAG);

        [DllImport("WR_RelExtOcorEstagAluno.dll")]
        public static extern int DLLRelExtOcorEstagAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO, string strP_TP_OCORR);

        [DllImport("WR_RelFicEntreEstagio.dll")]
        public static extern int DLLRelFicEntreEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTRE_ESTAGIO);

        [DllImport("WR_RelExtIndEstagio.dll")]
        public static extern int DLLRelExtIndEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO);

        [DllImport("WR_RelMapaEstatDistrDiplo.dll")]
        public static extern int DLLRelMapaEstatDistrDiplo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO);

        [DllImport("WR_RelMapaEvolutProgSocia.dll")]
        public static extern int DLLRelMapaEvolutProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CO_PROGR_TP_SOCEDU);

        [DllImport("WR_RelRelacDiploAluno.dll")]
        public static extern int DLLRelRelacDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU);

        [DllImport("WR_RelMapaDistrProgSocia.dll")]
        public static extern int DLLRelMapaDistrProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_ANO_GRADE, string strP_CO_PROGR_TP_SOCEDU);

        [DllImport("WR_RelHistAcompDiploAluno.dll")]
        public static extern int DLLRelHistAcompDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU, string strP_CO_DIPLOMA);

        [DllImport("WR_RelItensPatrimonio.dll")]
        public static extern int DLLRelItensPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CLASSIF_PATR, string strP_TP_PATR);

        [DllImport("WR_RelReciboEntregDiploma.dll")]
        public static extern int DLLRelReciboEntregDiploma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTR_DOCUM);

        [DllImport("WR_RelFicTecItemPatrimonio.dll")]
        public static extern int DLLRelFicTecItemPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_COD_PATR);

        [DllImport("WR_RelComprMatric.dll")]
        public static extern int DLLRelComprMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU_CAD);

        [DllImport("WR_RelComprBaixaEmprBibli.dll")]
        public static extern int DLLRelComprBaixaEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_CNPJ_INSTI);

        [DllImport("WR_RelRelacPlanoContas.dll")]
        public static extern int DLLRelRelacPlanoContas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_GRUP_CTA, string strP_CO_SGRUP_CTA);

        [DllImport("WR_RelTarefIncons.dll")]
        public static extern int DLLRelTarefIncons(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CO_SOLIC);

        [DllImport("WR_RelDistFuncGeo.dll")]
        public static extern int DLLRelDistFuncGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_SITU_COL);

        [DllImport("WR_RelRelacCtasUnidade.dll")]
        public static extern int DLLRelRelacCtasUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ORG_CODIGO_ORGAO, string strP_CO_EMP_CTA);

        [DllImport("WR_RelRelacItensPatrInven.dll")]
        public static extern int DLLRelRelacItensPatrInven(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_TP_PATR);

        [DllImport("WR_RelRelacaoAcervoObras.dll")]
        public static extern int DLLRelRelacaoAcervoObras(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ACER, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI);

        [DllImport("WR_RelLogAtividUsuario.dll")]
        public static extern int DLLRelLogAtividUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUNCIO, string strP_CO_EMP_COL, string strP_CO_COL, string strP_ACAO, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelRelacCEPs.dll")]
        public static extern int DLLRelRelacCEPs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_IMPRESSAO);

        [DllImport("WR_RelRelacTipoPessoa.dll")]
        public static extern int DLLRelRelacTipoPessoa(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_PESSOA, string strP_CO_SITU);

        [DllImport("WR_RelRelacTipoEndereco.dll")]
        public static extern int DLLRelRelacTipoEndereco(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_ENDERECO, string strP_CO_SITU);

        [DllImport("WR_RelRelacTipoTelefone.dll")]
        public static extern int DLLRelRelacTipoTelefone(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_TELEFONE, string strP_CO_SITU);

        [DllImport("WR_RelEndereAlunos.dll")]
        public static extern int DLLRelEndereAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [DllImport("WR_RelTelefoAlunos.dll")]
        public static extern int DLLRelTelefoAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [DllImport("WR_RelRelacMotivoOcorrencia.dll")]
        public static extern int DLLRelRelacMotivoOcorrencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CLASSIF, string strP_CO_TP_OCORR, string strP_CO_SITU);        

        [DllImport("WR_RelMapaPerFilOcupAluno.dll")]
        public static extern int DLLRelMapaPerFilOcupAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF);

        [DllImport("WR_RelMapaPerfiDesempAluno.dll")]
        public static extern int DLLRelMapaPerfiDesempAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_NOTA_MIN, string strP_NOTA_MAX);

        [DllImport("WR_RelMapaPerfilSalaAulaAluno.dll")]
        public static extern int DLLRelMapaPerfilSalaAulaAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_TIPO_SALA);

        [DllImport("WR_RelMensagensSMS.dll")]
        public static extern int DLLRelMensagensSMS(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_CONTAT_SMS, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM, string strP_STATUS);

        [DllImport("WR_RelHistMovimFunc.dll")]
        public static extern int DLLRelHistMovimFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelMapaQuantMovFunc.dll")]
        public static extern int DLLRelMapaQuantMovFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_ANO_REFER);

        [DllImport("WR_RelMeusAcessos.dll")]
        public static extern int DLLRelMeusAcessos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelMinhasMsgs.dll")]
        public static extern int DLLRelMinhasMsgs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelMovimFuncTipoUnid.dll")]
        public static extern int DLLRelMovimFuncTipoUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DT_INI, string strP_DT_FIM, string strP_TP_MOV);

        [DllImport("WR_RelMinhaBibliot.dll")]
        public static extern int DLLRelMinhaBibliot(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL);

        [DllImport("WR_RelFicInfoNucGestao.dll")]
        public static extern int DLLRelFicInfoNucGestao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [DllImport("WR_RelRelacResumAval.dll")]
        public static extern int DLLRelRelacResumAval(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_AVAL_INST);

        [DllImport("WR_RelMapaAvalResulNucleo.dll")]
        public static extern int DLLRelMapaAvalResulNucleo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [DllImport("WR_RelRelacNucleos.dll")]
        public static extern int DLLRelRelacNucleos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [DllImport("WR_RelRegisOcorr.dll")]
        public static extern int DLLRelRegisOcorr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [DllImport("WR_RelRelacAlunoPorEscola.dll")]
        public static extern int DLLRelRelacAlunoPorEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REFER);

        [DllImport("WR_RelRelacAlunoTransf.dll")]
        public static extern int DLLRelRelacAlunoTransf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DE_EMP_REF, string strP_TP_TRANSF, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [DllImport("WR_RelMapaTransfAluno.dll")]
        public static extern int DLLRelMapaTransfAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelRelacFuncPorDepto.dll")]
        public static extern int DLLRelRelacFuncPorDepto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_DEPTO);

        [DllImport("WR_RelRelacFuncPorFuncao.dll")]
        public static extern int DLLRelRelacFuncPorFuncao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_FUN);

        [DllImport("WR_RelRelacFuncPorUnidade.dll")]
        public static extern int DLLRelRelacFuncPorUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL);

        [DllImport("WR_RelInforOcorrAluno.dll")]
        public static extern int DLLRelInforOcorrAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ALU, string strP_CO_ALU, string strP_CO_SIGL_OCORR, string strP_DT_INI, string strP_DT_FIM);

        [DllImport("WR_RelAgendaContatos.dll")]
        public static extern int DLLRelAgendaContatos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL);

        [DllImport("kernel32.dll")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public RelatorioWeb()
        {
        }

        public int RelCustoFinFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_EMP_SELEC, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCustoFinFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_EMP_SELEC, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCustoFinFuncDept(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_DEPTO, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCustoFinFuncDept(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_DEPTO, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCustoFinFuncFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCustoFinFuncFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_FUN, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCustoFinFuncTpcon(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_TPCON, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCustoFinFuncTpcon(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_TPCON, strP_FLA_PROFESSOR, strP_CO_SEXO_COL, strP_tp_def);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPlanRealizCentroCusto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM, string strP_CO_DEPTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPlanRealizCentroCusto(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM, strP_CO_DEPTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPlanRealizado(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLFrmRelPlanRealizado(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_TP_CONTA, strP_TP_RELATORIO, strP_CO_ANO_INI, strP_CO_ANO_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_FUN, string strP_CO_INST, string strP_TP_DEF, string strP_CO_SEXO_COL, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacFuncionario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_FUN, strP_CO_INST, strP_TP_DEF, strP_CO_SEXO_COL, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicCadFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_BASE, string strP_FLA_PROFESSOR, string strP_TP_PONTO)
        {
            int lRetorno;
            try
            {
                lRetorno = 0;
                lRetorno = DLLRelFicCadFuncionario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_COL, strP_CO_ANO_BASE, strP_FLA_PROFESSOR, strP_TP_PONTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAniversarioFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_MES)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAniversarioFuncionario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_FLA_PROFESSOR, strP_MES);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGerMapaFrequenciaFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGerMapaFrequenciaFuncionario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_COL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGerMapaFrequenciaProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {

                int lRetorno = 0;
                lRetorno = DLLRelGerMapaFrequenciaProfessor(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_FLA_PROFESSOR, strP_CO_COL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }

        }

        public int RelExtratoFreqLivre(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtratoFreqLivre(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_COL, strP_ANO_REFER);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelExtratoFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER, string strP_FLA_PROFESSOR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtratoFreqFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_COL, strP_ANO_REFER, strP_FLA_PROFESSOR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAvaliacao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_TIPO_AVAL, strP_CO_EMP, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCanhoto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_MES_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCanhoto(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_CO_ANO_MES_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAvaliacaoModelo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NUM_PESQ)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAvaliacaoModelo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NUM_PESQ);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelListagemProva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_MES_MAT, string strP_CO_BIMESTRE)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelListagemProva(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_CO_ANO_MES_MAT, strP_CO_BIMESTRE);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAnaliticoResAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_PESQ_AVAL, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM, string strP_CO_COL, string strP_NO_CUR, string strP_NO_TUR, string strP_NO_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAnaliticoResAvaliacao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_TIPO_AVAL, strP_CO_PESQ_AVAL, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_MAT, strP_DT_INI, strP_DT_FIM, strP_CO_COL, strP_NO_CUR, strP_NO_TUR, strP_NO_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCFreqFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_FUN, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCFreqFuncInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCFreqFuncInst(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCFreqProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCFreqProf(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_FUN, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCFreqProfInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCFreqProfInst(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_DT_INI, strP_DT_FIM, strP_TP_PONTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapadePlanejAnualMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_DPTO_CUR, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapadePlanejAnualMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_DPTO_CUR, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeHorario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeHorario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacAlunoTurma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacAlunoTurma(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDemonstrativoReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDemonstrativoReserva(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDemonstrativoInscricao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDemonstrativoInscricao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDemonstrativoMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDemonstrativoMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaGeralMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ANO_REFER)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaGeralMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ANO_REFER);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMatricEfetivadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_CO_MODU_CUR, string strP_DES_MODU_CUR, string strP_CO_CUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMatricEfetivadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ANO_REFER, strP_CO_MODU_CUR, strP_DES_MODU_CUR, strP_CO_CUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaCaracteristicaMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaCaracteristicaMatricula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelExtSolicitAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtSolicitAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIPO_SOLI, strP_CO_ALU, strP_DT_INI, strP_DT_FIM, strP_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_DT_INI, string strP_DT_FIM, string strP_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelSolicRealiz(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIPO_SOLI, strP_DT_INI, strP_DT_FIM, strP_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaSolicRealiz(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REFER);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaSolicRealizAnual(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_INI, string strP_CO_ANO_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaSolicRealizAnual(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_INI, strP_CO_ANO_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPosicReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPosicReserva(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeCurricular(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_INI, string strP_CO_ANO_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeCurricular(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_INI, strP_CO_ANO_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInformacaoCurso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_NIVEL_CUR, string strP_CO_DPTO_CUR, string strP_CO_SUB_DPTO_CUR, string strP_CO_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInformacaoCurso(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_NIVEL_CUR, strP_CO_DPTO_CUR, strP_CO_SUB_DPTO_CUR, strP_CO_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_REFER, strP_CO_TUR, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaEstaticSolic(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP, string strP_TP_SIT, string strP_DE_SIT, string strP_CO_ANO_REF, string strP_TP_VISU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaEstaticSolic(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_DE_EMP, strP_TP_SIT, strP_DE_SIT, strP_CO_ANO_REF, strP_TP_VISU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCSolicitacoes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_SOLIC, string strP_DT_INI, string strP_DT_FIM, string strP_ISENT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCSolicitacoes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_TP_SOLIC, strP_DT_INI, strP_DT_FIM, strP_ISENT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistFreqAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_NO_ALU, string strP_CO_PARAM_FREQUE, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistFreqAlu(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_NO_ALU, strP_CO_PARAM_FREQUE, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_REF, string strP_CO_PARAM_FREQUE, string strP_MES, string strP_DE_MES, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFreqAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR,
                    strP_CO_MAT, strP_CO_ANO_REF, strP_CO_PARAM_FREQUE, strP_MES, strP_DE_MES, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelEvasaoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_MAT, string strP_DDL_SEL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelEvasaoEscolar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_MAT, strP_DDL_SEL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaAnualFaltas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaAnualFaltas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT, string strP_CO_PARAM_FREQ, string strP_CO_PARAM_FREQ_TIPO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaFreqAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT, strP_CO_PARAM_FREQ, strP_CO_PARAM_FREQ_TIPO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPautaChamadaFrente(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_MAT, string strP_NUM_MES, string strP_DES_MES)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPautaChamadaFrente(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
                            strP_CO_TUR, strP_CO_ANO_REF, strP_CO_MAT, strP_NUM_MES, strP_DES_MES);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPautaChamadaVerso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPautaChamadaVerso(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int relCurvaABCFreq(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR,
             string strP_CO_ANO_REF, string strP_TP_PRESENCA, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLrelCurvaABCFreq(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR,
                strP_CO_ANO_REF, strP_TP_PRESENCA, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistoricoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistoricoEscolar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_MODU_CUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicIndAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_NO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicIndAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_NO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRendimentoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRendimentoEscolar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelBolEscAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelBolEscAlu(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }/*
        public int RelBolEscAluBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelBolEscAluBARRIOBRA(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public int RelBolEscAluEDUCAMARIA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelBolEscAluEDUCAMARIA(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }*/

        public int RelBolEscAluModelo2(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string str_CO_BIMESTRE, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelBolEscAluModelo2(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_HABIL_FOTO, str_CO_BIMESTRE, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFinalAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_Classificacao)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFinalAlunos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_Classificacao);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaCursoTurmaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaCursoTurmaProf(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicCadIndRes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicCadIndRes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_RESP, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaDistResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaDistResp(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicInfoAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicInfoAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaCaracteristicaMatriculaSerie(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaCaracteristicaMatriculaSerie(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistAluCaract(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCaract(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public int RelDistAluCaractBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCaractBARRIOBRA(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }        

        public int RelDistAluCarAno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCarAno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public int RelDistAluCarAnoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCarAnoBARRIOBRA(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicCadInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicCadInst(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelUnidEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelUnidEsc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIPOEMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelUnidEscNuc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelUnidEscNuc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUCLEO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelUnidEscBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelUnidEscBairro(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDemUnidEscBai(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDemUnidEscBai(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDemAluUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP, string strP_CO_ANO_REF, string strP_CO_SITU_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDemAluUnid(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIPOEMP, strP_CO_ANO_REF, strP_CO_SITU_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistAluGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_REL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluGeo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_TP_REL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeNotasAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeNotasAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeNotasAlunoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeNotasAlunoBARRIOBRA(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistAluCar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_TP_RACA, strP_RENDA, strP_TP_DEF, strP_BOLSA, strP_PASSE);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistAluCarBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluCarBairro(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_TP_RACA, strP_RENDA, strP_TP_DEF, strP_BOLSA, strP_PASSE);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistAluBolEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_BOLSA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistAluBolEscola(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_BOLSA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPlanoAula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_DT_INI, string strP_DT_FIM, string strP_TP_ATIV, string strP_CO_COL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPlanoAula(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_DT_INI, strP_DT_FIM, strP_TP_ATIV, strP_CO_COL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAtivRealizProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAtivRealizProfessor(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_COL, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaLocalizacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaLocalizacao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_DE_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAniversarioProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_MES, string strP_CO_DEPTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAniversarioProfessor(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_MES, strP_CO_DEPTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRespAluPar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_DE_GRAU_PAREN)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRespAluPar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_CO_MODU_CUR,
                              strP_CO_CUR, strP_CO_TUR, strP_CO_GRAU_INST, strP_DE_GRAU_PAREN);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistRespGrauInstrucao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_TP_DEF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistRespGrauInstrucao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR,
                              strP_CO_CUR, strP_CO_TUR, strP_CO_GRAU_INST, strP_TP_DEF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeAlunos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelLisAluInstEspecializadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelLisAluInstEspecializadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_INST_ESP, strP_CO_ANO_MES_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAluPorInsEsp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAluPorInsEsp(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_INST_ESP, strP_CO_ANO_MES_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAniversarioAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_MES)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAniversarioAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_MES);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaHistAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public int RelMapaHistAlunoSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaHistAlunoSerTur(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_MAT, strP_CO_ALU, strP_TP_AVAL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelItensAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelItensAcervo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_DE_AREACON, strP_CO_EDITORA, strP_DE_EDITORA, strP_CO_CLAS_ACER, strP_DE_CLAS_ACER, strP_CO_AUTOR, strP_DE_AUTOR, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelObrasEmprestadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelObrasEmprestadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_DE_ARECON, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelObrasEmAtraso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelObrasEmAtraso(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_DE_ARECON, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelItensEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelItensEstoque(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIP_PROD, strP_DE_TIP_PROD, strP_CO_GRUPO_ITEM, strP_CO_SUBGRP_ITEM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelPosicaoEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelPosicaoEstoque(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIP_PROD, strP_DE_TIP_PROD, strP_CO_GRUPO_ITEM, strP_CO_SUBGRP_ITEM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelClientes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_CLI, string strP_NO_FAN_CLI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelClientes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_CPFCGC_CLI, strP_NO_FAN_CLI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelResumoContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_NU_DOC, string strP_CO_ALU,
                                   string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_MES_MAT, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelResumoContasReceber(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM, strP_NU_DOC, strP_CO_ALU,
                                       strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_ANO_MES_MAT, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelContasPagar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_CO_FORN, string strP_NU_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_DOC)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelContasPagar(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_DOC, strP_CO_FORN, strP_NU_DOC, strP_DT_INI, strP_DT_FIM, strP_DT_VEN_DOC);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelContagemEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelContagemEstoque(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TIP_PROD, strP_DE_TIP_PROD, strP_CO_GRUPO_ITEM, strP_CO_SUBGRP_ITEM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFornecedores(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_FORN, string strP_NO_FAN_FOR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFornecedores(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_CPFCGC_FORN, strP_NO_FAN_FOR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelBoletimAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelBoletimAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR, strP_CO_TUR, strP_DE_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaDisciplinaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaDisciplinaProf(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        public int RelMapaDisciplinaProfBarao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaDisciplinaProfBarao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ID_MATERIA, strP_CLASSI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAlunosBolsistas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_TIPO_BOLSA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAlunosBolsistas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_CO_TIPO_BOLSA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAluPasEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_LIN_ONI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAluPasEsc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ANO_MES_MAT, strP_LIN_ONI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaEstaticEmpr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaEstaticEmpr(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REFER, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelReceitasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelReceitasFixas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_RECDES, strP_CO_CON_RECDES, strP_DT_INI, strP_DT_FIM, strP_CO_CPF_CNPJ_RECDES, strP_NO_CLI_RECDES);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacaoInativo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_SIT_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacaoInativo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_MES_MAT, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_SIT_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_NU_DOC, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_INI, string strP_DT_VEN_FIM,
            string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR, string strP_CO_ANO_MES_MAT)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelContasReceber(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_DOC, strP_NU_DOC, strP_CO_ALU, strP_DT_INI, strP_DT_FIM,
                                 strP_DT_VEN_INI, strP_DT_VEN_FIM, strP_CO_MODU_CUR, strP_DE_MODU_CUR, strP_CO_CUR, strP_DE_CUR, strP_CO_TUR, strP_DE_TUR, strP_CO_ANO_MES_MAT);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDespesasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDespesasFixas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_RECDES, strP_CO_CON_RECDES, strP_DT_INI, strP_DT_FIM, strP_CO_CPF_CNPJ_RECDES, strP_NO_CLI_RECDES);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelValorAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelValorAcervo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDebitoDocumento(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDebitoDocumento(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistEscAvalProg(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistEscAvalProg(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaFinanceiro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_IC_SIT_DOC)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaFinanceiro(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_IC_SIT_DOC);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelGradeFinanceira(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelGradeFinanceira(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInadimplenciaTotal(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInadimplenciaTotal(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInadimplenciaPorResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInadimplenciaPorResp(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInadimplencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInadimplencia(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_RESP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInadimplenciaSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInadimplenciaSerTur(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCCR(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCCR(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelEspenhoDiscCursadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelEspenhoDiscCursadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ALU, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCurvaABCCP(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCurvaABCCP(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_IC_SIT_DOC, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAtivExtraAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ATIV_EXTRA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAtivExtraAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ATIV_EXTRA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelCalendario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_TP_CALEND)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelCalendario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ANO_REFER, strP_TP_CALEND);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicPerfilInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicPerfilInst(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacTarefAgendadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacTarefAgendadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_RESP, strP_DT_INI, strP_DT_FIM, strP_PRIOR, strP_CO_SOLIC);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistoTarefAgendada(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistoTarefAgendada(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_RESP, strP_DT_INI, strP_DT_FIM, strP_PRIOR, strP_CO_SOLIC);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicEstagio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_OFERT_ESTAG);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelLinhaTranspPorAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelLinhaTranspPorAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMovimEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_PROD, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMovimEstoque(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TP_PROD, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelComprEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_DESC_USU, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelComprEmprBibli(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUM_EMP, strP_DESC_USU, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelObrasReservadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_CO_CLAS, string strP_CO_ISBN_ACER, string strP_CO_TP_USU, string strP_CO_USU, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelObrasReservadas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_AREACON, strP_CO_CLAS, strP_CO_ISBN_ACER, strP_CO_TP_USU, strP_CO_USU, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacUsuBiblioteca(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacUsuBiblioteca(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TP_USU, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelExtUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtUsuario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TP_USU, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelTarefIncons(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CO_SOLIC)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelTarefIncons(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_RESP, strP_CO_SOLIC);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacDiploAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_CUR, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistAcompDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU, string strP_CO_DIPLOMA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistAcompDiploAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_CUR, strP_CO_ALU, strP_CO_DIPLOMA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaEstatDistrDiplo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaEstatDistrDiplo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacOfertEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ESTAG)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacOfertEstagio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_ESTAG);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacPlanoContas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_GRUP_CTA, string strP_CO_SGRUP_CTA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacPlanoContas(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_GRUP_CTA, strP_CO_SGRUP_CTA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelItensPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CLASSIF_PATR, string strP_TP_PATR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelItensPatrimonio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_CLASSIF_PATR, strP_TP_PATR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicTecItemPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_COD_PATR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicTecItemPatrimonio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_COD_PATR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaDistrProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_ANO_GRADE, string strP_CO_PROGR_TP_SOCEDU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaDistrProgSocia(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strParametrosRelatorio, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_ANO_GRADE, strP_CO_PROGR_TP_SOCEDU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelReciboEntregDiploma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTR_DOCUM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelReciboEntregDiploma(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_ENTR_DOCUM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaEvolutProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CO_PROGR_TP_SOCEDU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaEvolutProgSocia(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REFER, strP_CO_PROGR_TP_SOCEDU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelComprReserMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NU_RESERVA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelComprReserMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_NU_RESERVA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelComprMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU_CAD)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelComprMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU_CAD);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelComprBaixaEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelComprBaixaEmprBibli(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUM_EMP, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacSolicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG, string strP_ID_CANDID_ESTAG)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacSolicEstagio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_OFERT_ESTAG, strP_ID_CANDID_ESTAG);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelExtOcorEstagAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO, string strP_TP_OCORR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtOcorEstagAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_EFETI_ESTAGIO, strP_TP_OCORR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicEntreEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTRE_ESTAGIO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicEntreEstagio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_ENTRE_ESTAGIO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelExtIndEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelExtIndEstagio(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_EFETI_ESTAGIO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelDistFuncGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_SITU_COL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelDistFuncGeo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_CO_SITU_COL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelEvolRecDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelEvolRecDes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelEvolRecEDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelEvolRecEDes(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacCtasUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ORG_CODIGO_ORGAO, string strP_CO_EMP_CTA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacCtasUnidade(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ORG_CODIGO_ORGAO, strP_CO_EMP_CTA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacItensPatrInven(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_TP_PATR)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacItensPatrInven(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_TP_PATR);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacaoAcervoObras(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ACER, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacaoAcervoObras(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_ACER, strP_CO_AREACON, strP_DE_AREACON, strP_CO_EDITORA, strP_DE_EDITORA, strP_CO_CLAS_ACER, strP_DE_CLAS_ACER, strP_CO_AUTOR, strP_DE_AUTOR, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }        

        public int RelLogAtividUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUNCIO, string strP_CO_EMP_COL, string strP_CO_COL, string strP_ACAO, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelLogAtividUsuario(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_FUNCIO, strP_CO_EMP_COL, strP_CO_COL, strP_ACAO, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacCEPs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_IMPRESSAO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacCEPs(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_UF, strP_CO_CIDADE, strP_CO_BAIRRO, strP_CO_IMPRESSAO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacTipoPessoa(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_PESSOA, string strP_CO_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacTipoPessoa(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_TIPO_PESSOA, strP_CO_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacTipoEndereco(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_ENDERECO, string strP_CO_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacTipoEndereco(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_TIPO_ENDERECO, strP_CO_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacTipoTelefone(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_TELEFONE, string strP_CO_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacTipoTelefone(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_ID_TIPO_TELEFONE, strP_CO_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelEndereAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelEndereAlunos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelTelefoAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelTelefoAlunos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacMotivoOcorrencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CLASSIF, string strP_CO_TP_OCORR, string strP_CO_SITU)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacMotivoOcorrencia(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CLASSIF, strP_CO_TP_OCORR, strP_CO_SITU);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaPerFilOcupAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaPerFilOcupAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REF);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaPerfiDesempAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_NOTA_MIN, string strP_NOTA_MAX)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaPerfiDesempAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REF, strP_CO_MODU_CUR, strP_CO_CUR, strP_CO_TUR, strP_ID_MATERIA, strP_NOTA_MIN, strP_NOTA_MAX);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaPerfilSalaAulaAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_TIPO_SALA)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaPerfilSalaAulaAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_TIPO_SALA);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMensagensSMS(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_CONTAT_SMS, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM, string strP_STATUS)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMensagensSMS(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TP_CONTAT_SMS, strP_CO_EMP_COL, strP_CO_COL, strP_DT_INI, strP_DT_FIM, strP_STATUS);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelHistMovimFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelHistMovimFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_COL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaQuantMovFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_ANO_REFER)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaQuantMovFunc(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_ANO_REFER);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMeusAcessos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMeusAcessos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMinhasMsgs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMinhasMsgs(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMovimFuncTipoUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DT_INI, string strP_DT_FIM, string strP_TP_MOV)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMovimFuncTipoUnid(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_DT_INI, strP_DT_FIM, strP_TP_MOV);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMinhaBibliot(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMinhaBibliot(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_COL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelFicInfoNucGestao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelFicInfoNucGestao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUCLEO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        
        public int RelRelacResumAval(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_AVAL_INST)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacResumAval(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_TP_AVAL_INST);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaAvalResulNucleo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaAvalResulNucleo(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUCLEO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacNucleos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacNucleos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUCLEO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRegisOcorr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRegisOcorr(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_NUCLEO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacAlunoPorEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REFER)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacAlunoPorEscola(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_CO_ANO_REFER);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacAlunoTransf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DE_EMP_REF, string strP_TP_TRANSF, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacAlunoTransf(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_REF, strP_DE_EMP_REF, strP_TP_TRANSF, strP_DT_INI, strP_DT_FIM, strP_CNPJ_INSTI);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelMapaTransfAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelMapaTransfAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacFuncPorDepto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_DEPTO)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacFuncPorDepto(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_SITU_COL, strP_CO_DEPTO);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacFuncPorFuncao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_FUN)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacFuncPorFuncao(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_SITU_COL, strP_CO_FUN);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelRelacFuncPorUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelRelacFuncPorUnidade(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_COL, strP_CO_SITU_COL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelInforOcorrAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ALU, string strP_CO_ALU, string strP_CO_SIGL_OCORR, string strP_DT_INI, string strP_DT_FIM)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelInforOcorrAluno(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_EMP_ALU, strP_CO_ALU, strP_CO_SIGL_OCORR, strP_DT_INI, strP_DT_FIM);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public int RelAgendaContatos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL)
        {
            try
            {
                int lRetorno = 0;
                lRetorno = DLLRelAgendaContatos(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_COL);
                return lRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        } 
    }
}