//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.ServiceModel;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces
{
    [ServiceContract]
    public interface IRelatorioWeb
    {
        [OperationContract(Name = "RelCustoFinFunc")]
        int RelCustoFinFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_EMP_SELEC, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [OperationContract(Name = "RelCustoFinFuncDept")]
        int RelCustoFinFuncDept(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_DEPTO, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [OperationContract(Name = "RelCustoFinFuncFunc")]
        int RelCustoFinFuncFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [OperationContract(Name = "RelCustoFinFuncTpcon")]
        int RelCustoFinFuncTpcon(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_TPCON, string strP_FLA_PROFESSOR, string strP_CO_SEXO_COL, string strP_tp_def);

        [OperationContract(Name = "RelPlanRealizCentroCusto")]
        int RelPlanRealizCentroCusto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM, string strP_CO_DEPTO);

        [OperationContract(Name = "RelPlanRealizado")]
        int RelPlanRealizado(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_CONTA, string strP_TP_RELATORIO, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [OperationContract(Name = "RelRelacFuncionario")]
        int RelRelacFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_FUN, string strP_CO_INST, string strP_TP_DEF, string strP_CO_SEXO_COL, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [OperationContract(Name = "RelFicCadFuncionario")]
        int RelFicCadFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_BASE, string strP_FLA_PROFESSOR, string strP_TP_PONTO);

        [OperationContract(Name = "RelAniversarioFuncionario")]
        int RelAniversarioFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_MES);

        [OperationContract(Name = "RelGerMapaFrequenciaFuncionario")]
        int RelGerMapaFrequenciaFuncionario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelGerMapaFrequenciaProfessor")]
        int RelGerMapaFrequenciaProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_FLA_PROFESSOR, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelExtratoFreqLivre")]
        int RelExtratoFreqLivre(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER);

        [OperationContract(Name = "RelAvaliacao")]
        int RelAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelCanhoto")]
        int RelCanhoto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelAvaliacaoModelo")]
        int RelAvaliacaoModelo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NUM_PESQ);

        [OperationContract(Name = "RelListagemProva")]
        int RelListagemProva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_MES_MAT, string strP_CO_BIMESTRE);

        [OperationContract(Name = "RelAnaliticoResAvaliacao")]
        int RelAnaliticoResAvaliacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_TIPO_AVAL, string strP_CO_PESQ_AVAL, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_MAT, string strP_DT_INI, string strP_DT_FIM, string strP_CO_COL, string strP_NO_CUR, string strP_NO_TUR, string strP_NO_MAT);

        [OperationContract(Name = "RelCurvaABCFreqFunc")]
        int RelCurvaABCFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [OperationContract(Name = "RelCurvaABCFreqFuncInst")]
        int RelCurvaABCFreqFuncInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [OperationContract(Name = "RelCurvaABCFreqProf")]
        int RelCurvaABCFreqProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_FUN, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [OperationContract(Name = "RelCurvaABCFreqProfInst")]
        int RelCurvaABCFreqProfInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM, string strP_TP_PONTO);

        [OperationContract(Name = "RelMapadePlanejAnualMatricula")]
        int RelMapadePlanejAnualMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_DPTO_CUR, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelExtratoFreqFunc")]
        int RelExtratoFreqFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_ANO_REFER, string strP_FLA_PROFESSOR);

        [OperationContract(Name = "RelGradeHorario")]
        int RelGradeHorario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR);

        [OperationContract(Name = "RelRelacAlunoTurma")]
        int RelRelacAlunoTurma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelDemonstrativoReserva")]
        int RelDemonstrativoReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelDemonstrativoInscricao")]
        int RelDemonstrativoInscricao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelDemonstrativoMatricula")]
        int RelDemonstrativoMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelMapaGeralMatricula")]
        int RelMapaGeralMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ANO_REFER);

        [OperationContract(Name = "RelMatricEfetivadas")]
        int RelMatricEfetivadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_CO_MODU_CUR, string strP_DES_MODU_CUR, string strP_CO_CUR);

        [OperationContract(Name = "RelMapaCaracteristicaMatricula")]
        int RelMapaCaracteristicaMatricula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelExtSolicitAluno")]
        int RelExtSolicitAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_SITU);

        [OperationContract(Name = "RelSolicRealiz")]
        int RelSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPO_SOLI, string strP_DT_INI, string strP_DT_FIM, string strP_SITU);

        [OperationContract(Name = "RelMapaSolicRealiz")]
        int RelMapaSolicRealiz(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER);

        [OperationContract(Name = "RelMapaSolicRealizAnual")]
        int RelMapaSolicRealizAnual(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [OperationContract(Name = "RelPosicReserva")]
        int RelPosicReserva(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelGradeCurricular")]
        int RelGradeCurricular(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_INI, string strP_CO_ANO_FIM);

        [OperationContract(Name = "RelInformacaoCurso")]
        int RelInformacaoCurso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_NIVEL_CUR, string strP_CO_DPTO_CUR, string strP_CO_SUB_DPTO_CUR, string strP_CO_SITU);

        [OperationContract(Name = "RelRelacAluno")]
        int RelRelacAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_REFER, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelMapaEstaticSolic")]
        int RelMapaEstaticSolic(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP, string strP_TP_SIT, string strP_DE_SIT, string strP_CO_ANO_REF, string strP_TP_VISU);

        [OperationContract(Name = "RelCurvaABCSolicitacoes")]
        int RelCurvaABCSolicitacoes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_TP_SOLIC, string strP_DT_INI, string strP_DT_FIM, string strP_ISENT);

        [OperationContract(Name = "RelHistFreqAlu")]
        int RelHistFreqAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_NO_ALU, string strP_CO_PARAM_FREQUE, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelFreqAluno")]
        int RelFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR,
        string strP_CO_TUR, string strP_CO_MAT, string strP_CO_ANO_REF, string strP_CO_PARAM_FREQUE, string strP_MES, string strP_DE_MES, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelEvasaoEscolar")]
        int RelEvasaoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_MAT, string strP_DDL_SEL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelMapaAnualFaltas")]
        int RelMapaAnualFaltas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT);

        [OperationContract(Name = "RelMapaFreqAluno")]
        int RelMapaFreqAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_ALU, string strP_CO_MAT, string strP_CO_PARAM_FREQ, string strP_CO_PARAM_FREQ_TIPO);

        [OperationContract(Name = "RelPautaChamadaFrente")]
        int RelPautaChamadaFrente(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_REF, string strP_CO_MAT, string strP_NUM_MES, string strP_DES_MES);

        [OperationContract(Name = "RelPautaChamadaVerso")]
        int RelPautaChamadaVerso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [OperationContract(Name = "relCurvaABCFreq")]
        int relCurvaABCFreq(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR,
            string strP_CO_ANO_REF, string strP_TP_PRESENCA, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelHistAluno")]
        int RelHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [OperationContract(Name = "RelHistoricoEscolar")]
        int RelHistoricoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR);

        [OperationContract(Name = "RelFicIndAluno")]
        int RelFicIndAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_NO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelRendimentoEscolar")]
        int RelRendimentoEscolar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelBolEscAlu")]
        int RelBolEscAlu(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string strP_CNPJ_INSTI);
        /*[OperationContract(Name = "RelBolEscAluBARRIOBRA")]
        int RelBolEscAluBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO);
        [OperationContract(Name = "RelBolEscAluEDUCAMARIA")]
        int RelBolEscAluEDUCAMARIA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO);
        */
        [OperationContract(Name = "RelBolEscAluModelo2")]
        int RelBolEscAluModelo2(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_HABIL_FOTO, string str_CO_BIMESTRE, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelFinalAlunos")]
        int RelFinalAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_Classificacao);

        [OperationContract(Name = "RelMapaCursoTurmaProf")]
        int RelMapaCursoTurmaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [OperationContract(Name = "RelFicCadIndRes")]
        int RelFicCadIndRes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelMapaDistResp")]
        int RelMapaDistResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [OperationContract(Name = "RelFicInfoAluno")]
        int RelFicInfoAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [OperationContract(Name = "RelMapaCaracteristicaMatriculaSerie")]
        int RelMapaCaracteristicaMatriculaSerie(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelDistAluCaract")]
        int RelDistAluCaract(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);
        [OperationContract(Name = "RelDistAluCaractBARRIOBRA")]
        int RelDistAluCaractBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelDistAluCarAno")]
        int RelDistAluCarAno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);
        [OperationContract(Name = "RelDistAluCarAnoBARRIOBRA")]
        int RelDistAluCarAnoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [OperationContract(Name = "RelFicCadInst")]
        int RelFicCadInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [OperationContract(Name = "RelUnidEsc")]
        int RelUnidEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP);

        [OperationContract(Name = "RelUnidEscNuc")]
        int RelUnidEscNuc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [OperationContract(Name = "RelUnidEscBairro")]
        int RelUnidEscBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [OperationContract(Name = "RelDemUnidEscBai")]
        int RelDemUnidEscBai(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO);

        [OperationContract(Name = "RelDemAluUnid")]
        int RelDemAluUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIPOEMP, string strP_CO_ANO_REF, string strP_CO_SITU_MAT);

        [OperationContract(Name = "RelDistAluGeo")]
        int RelDistAluGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_REL);

        [OperationContract(Name = "RelGradeNotasAluno")]
        int RelGradeNotasAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);
        [OperationContract(Name = "RelGradeNotasAlunoBARRIOBRA")]
        int RelGradeNotasAlunoBARRIOBRA(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [OperationContract(Name = "RelDistAluCar")]
        int RelDistAluCar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE);

        [OperationContract(Name = "RelDistAluCarBairro")]
        int RelDistAluCarBairro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_TP_RACA, string strP_RENDA, string strP_TP_DEF, string strP_BOLSA, string strP_PASSE);

        [OperationContract(Name = "RelDistAluBolEscola")]
        int RelDistAluBolEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_BOLSA);

        [OperationContract(Name = "RelPlanoAula")]
        int RelPlanoAula(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_DT_INI, string strP_DT_FIM, string strP_TP_ATIV, string strP_CO_COL);

        [OperationContract(Name = "RelAtivRealizProfessor")]
        int RelAtivRealizProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_COL, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);
        
        [OperationContract(Name = "RelMapaLocalizacao")]
        int RelMapaLocalizacao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DE_EMP);
                
        [OperationContract(Name = "RelAniversarioProfessor")]
        int RelAniversarioProfessor(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_MES, string strP_CO_DEPTO);
                
        [OperationContract(Name = "RelRespAluPar")]
        int RelRespAluPar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_DE_GRAU_PAREN);

        [OperationContract(Name = "RelDistRespGrauInstrucao")]
        int RelDistRespGrauInstrucao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_GRAU_INST, string strP_TP_DEF);

        [OperationContract(Name = "RelGradeAlunos")]
        int RelGradeAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT);

        [OperationContract(Name = "RelLisAluInstEspecializadas")]
        int RelLisAluInstEspecializadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT);

        [OperationContract(Name = "RelAluPorInsEsp")]
        int RelAluPorInsEsp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_INST_ESP, string strP_CO_ANO_MES_MAT);

        [OperationContract(Name = "RelAniversarioAluno")]
        int RelAniversarioAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_MES);

        [OperationContract(Name = "RelMapaHistAluno")]
        int RelMapaHistAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL);

        [OperationContract(Name = "RelMapaHistAlunoSerTur")]
        int RelMapaHistAlunoSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR,
                           string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_MAT, string strP_CO_ALU, string strP_TP_AVAL);

        [OperationContract(Name = "RelItensAcervo")]        
        int RelItensAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelObrasEmprestadas")]
        int RelObrasEmprestadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelObrasEmAtraso")]
        int RelObrasEmAtraso(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_DE_ARECON, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelItensEstoque")]
        int RelItensEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [OperationContract(Name = "RelPosicaoEstoque")]
        int RelPosicaoEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [OperationContract(Name = "RelClientes")]
        int RelClientes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_CLI, string strP_NO_FAN_CLI);

        [OperationContract(Name = "RelResumoContasReceber")]
        int RelResumoContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_NU_DOC, string strP_CO_ALU,
                                   string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_ANO_MES_MAT, string strP_CO_TUR);

        [OperationContract(Name = "RelContasPagar")]
        int RelContasPagar(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_CO_FORN, string strP_NU_DOC, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_DOC);

        [OperationContract(Name = "RelContagemEstoque")]
        int RelContagemEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TIP_PROD, string strP_DE_TIP_PROD, string strP_CO_GRUPO_ITEM, string strP_CO_SUBGRP_ITEM);

        [OperationContract(Name = "RelFornecedores")]
        int RelFornecedores(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CPFCGC_FORN, string strP_NO_FAN_FOR);

        [OperationContract(Name = "RelBoletimAluno")]
        int RelBoletimAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR);

        [OperationContract(Name = "RelMapaDisciplinaProf")]
        int RelMapaDisciplinaProf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI);
        [OperationContract(Name = "RelMapaDisciplinaProfBarao")]
        int RelMapaDisciplinaProfBarao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ID_MATERIA, string strP_CLASSI);

        [OperationContract(Name = "RelAlunosBolsistas")]
        int RelAlunosBolsistas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_CO_TIPO_BOLSA);

        [OperationContract(Name = "RelAluPasEsc")]
        int RelAluPasEsc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ANO_MES_MAT, string strP_LIN_ONI);

        [OperationContract(Name = "RelMapaEstaticEmpr")]
        int RelMapaEstaticEmpr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelReceitasFixas")]
        int RelReceitasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES);

        [OperationContract(Name = "RelRelacaoInativo")]
        int RelRelacaoInativo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_MES_MAT, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_SIT_MAT);

        [OperationContract(Name = "RelContasReceber")]
        int RelContasReceber(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_NU_DOC, string strP_CO_ALU, string strP_DT_INI, string strP_DT_FIM, string strP_DT_VEN_INI, string strP_DT_VEN_FIM,
            string strP_CO_MODU_CUR, string strP_DE_MODU_CUR, string strP_CO_CUR, string strP_DE_CUR, string strP_CO_TUR, string strP_DE_TUR, string strP_CO_ANO_MES_MAT);

        [OperationContract(Name = "RelDespesasFixas")]
        int RelDespesasFixas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_RECDES, string strP_CO_CON_RECDES, string strP_DT_INI, string strP_DT_FIM, string strP_CO_CPF_CNPJ_RECDES, string strP_NO_CLI_RECDES);

        [OperationContract(Name = "RelValorAcervo")]
        int RelValorAcervo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelDebitoDocumento")]
        int RelDebitoDocumento(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelHistEscAvalProg")]
        int RelHistEscAvalProg(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelMapaFinanceiro")]
        int RelMapaFinanceiro(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_IC_SIT_DOC);

        [OperationContract(Name = "RelGradeFinanceira")]
        int RelGradeFinanceira(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU);

        [OperationContract(Name = "RelInadimplenciaTotal")]
        int RelInadimplenciaTotal(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelInadimplenciaPorResp")]
        int RelInadimplenciaPorResp(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP);

        [OperationContract(Name = "RelInadimplencia")]
        int RelInadimplencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_RESP);

        [OperationContract(Name = "RelInadimplenciaSerTur")]
        int RelInadimplenciaSerTur(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelCurvaABCCR")]
        int RelCurvaABCCR(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelEspenhoDiscCursadas")]
        int RelEspenhoDiscCursadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ALU, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR);

        [OperationContract(Name = "RelCurvaABCCP")]
        int RelCurvaABCCP(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_IC_SIT_DOC, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelAtivExtraAluno")]
        int RelAtivExtraAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ATIV_EXTRA);

        [OperationContract(Name = "RelCalendario")]
        int RelCalendario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REFER, string strP_TP_CALEND);

        [OperationContract(Name = "RelFicPerfilInst")]
        int RelFicPerfilInst(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP);

        [OperationContract(Name = "RelRelacTarefAgendadas")]
        int RelRelacTarefAgendadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC);

        [OperationContract(Name = "RelHistoTarefAgendada")]
        int RelHistoTarefAgendada(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_DT_INI, string strP_DT_FIM, string strP_PRIOR, string strP_CO_SOLIC);

        [OperationContract(Name = "RelFicEstagio")]
        int RelFicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG);

        [OperationContract(Name = "RelLinhaTranspPorAluno")]
        int RelLinhaTranspPorAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_CO_ALU);

        [OperationContract(Name = "RelMovimEstoque")]
        int RelMovimEstoque(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_PROD, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelComprEmprBibli")]
        int RelComprEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_DESC_USU, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelObrasReservadas")]
        int RelObrasReservadas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_AREACON, string strP_CO_CLAS, string strP_CO_ISBN_ACER, string strP_CO_TP_USU, string strP_CO_USU, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelRelacUsuBiblioteca")]
        int RelRelacUsuBiblioteca(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelExtUsuario")]
        int RelExtUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_USU, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelEvolRecDes")]
        int RelEvolRecDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelEvolRecEDes")]
        int RelEvolRecEDes(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelComprReserMatric")]
        int RelComprReserMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_NU_RESERVA);

        [OperationContract(Name = "RelRelacOfertEstagio")]
        int RelRelacOfertEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ESTAG);

        [OperationContract(Name = "RelRelacSolicEstagio")]
        int RelRelacSolicEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_OFERT_ESTAG, string strP_ID_CANDID_ESTAG);

        [OperationContract(Name = "RelExtOcorEstagAluno")]
        int RelExtOcorEstagAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO, string strP_TP_OCORR);

        [OperationContract(Name = "RelFicEntreEstagio")]
        int RelFicEntreEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTRE_ESTAGIO);

        [OperationContract(Name = "RelExtIndEstagio")]
        int RelExtIndEstagio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_EFETI_ESTAGIO);

        [OperationContract(Name = "RelMapaEstatDistrDiplo")]
        int RelMapaEstatDistrDiplo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO);

        [OperationContract(Name = "RelMapaEvolutProgSocia")]
        int RelMapaEvolutProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ANO_REFER, string strP_CO_PROGR_TP_SOCEDU);

        [OperationContract(Name = "RelRelacDiploAluno")]
        int RelRelacDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU);

        [OperationContract(Name = "RelMapaDistrProgSocia")]
        int RelMapaDistrProgSocia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_MODU_CUR, string strP_CO_ANO_GRADE, string strP_CO_PROGR_TP_SOCEDU);

        [OperationContract(Name = "RelHistAcompDiploAluno")]
        int RelHistAcompDiploAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_CUR, string strP_CO_ALU, string strP_CO_DIPLOMA);

        [OperationContract(Name = "RelItensPatrimonio")]
        int RelItensPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_CLASSIF_PATR, string strP_TP_PATR);

        [OperationContract(Name = "RelReciboEntregDiploma")]
        int RelReciboEntregDiploma(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_ENTR_DOCUM);

        [OperationContract(Name = "RelFicTecItemPatrimonio")]
        int RelFicTecItemPatrimonio(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_COD_PATR);

        [OperationContract(Name = "RelComprMatric")]
        int RelComprMatric(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU_CAD);

        [OperationContract(Name = "RelComprBaixaEmprBibli")]
        int RelComprBaixaEmprBibli(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUM_EMP, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelRelacPlanoContas")]
        int RelRelacPlanoContas(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_GRUP_CTA, string strP_CO_SGRUP_CTA);

        [OperationContract(Name = "RelTarefIncons")]
        int RelTarefIncons(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strParametrosRelatorio, string strP_CO_EMP, string strP_CO_RESP, string strP_CO_SOLIC);

        [OperationContract(Name = "RelDistFuncGeo")]
        int RelDistFuncGeo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_SITU_COL);

        [OperationContract(Name = "RelRelacCtasUnidade")]
        int RelRelacCtasUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ORG_CODIGO_ORGAO, string strP_CO_EMP_CTA);

        [OperationContract(Name = "RelRelacItensPatrInven")]
        int RelRelacItensPatrInven(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_TP_PATR);

        [OperationContract(Name = "RelRelacaoAcervoObras")]
        int RelRelacaoAcervoObras(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ACER, string strP_CO_AREACON, string strP_DE_AREACON, string strP_CO_EDITORA,
            string strP_DE_EDITORA, string strP_CO_CLAS_ACER, string strP_DE_CLAS_ACER, string strP_CO_AUTOR, string strP_DE_AUTOR, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelLogAtividUsuario")]
        int RelLogAtividUsuario(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_FUNCIO, string strP_CO_COL, string strP_ACAO, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelRelacCEPs")]
        int RelRelacCEPs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_UF, string strP_CO_CIDADE, string strP_CO_BAIRRO, string strP_CO_IMPRESSAO);

        [OperationContract(Name = "RelRelacTipoPessoa")]
        int RelRelacTipoPessoa(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_PESSOA, string strP_CO_SITU);

        [OperationContract(Name = "RelRelacTipoEndereco")]
        int RelRelacTipoEndereco(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_ENDERECO, string strP_CO_SITU);

        [OperationContract(Name = "RelRelacTipoTelefone")]
        int RelRelacTipoTelefone(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_ID_TIPO_TELEFONE, string strP_CO_SITU);

        [OperationContract(Name = "RelEndereAlunos")]
        int RelEndereAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [OperationContract(Name = "RelTelefoAlunos")]
        int RelTelefoAlunos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_ALU);

        [OperationContract(Name = "RelRelacMotivoOcorrencia")]
        int RelRelacMotivoOcorrencia(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CLASSIF, string strP_CO_TP_OCORR, string strP_CO_SITU);

        [OperationContract(Name = "RelMapaPerFilOcupAluno")]
        int RelMapaPerFilOcupAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF);

        [OperationContract(Name = "RelMapaPerfiDesempAluno")]
        int RelMapaPerfiDesempAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REF, string strP_CO_MODU_CUR, string strP_CO_CUR, string strP_CO_TUR, string strP_ID_MATERIA, string strP_NOTA_MIN, string strP_NOTA_MAX);

        [OperationContract(Name = "RelMapaPerfilSalaAulaAluno")]
        int RelMapaPerfilSalaAulaAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_TIPO_SALA);

        [OperationContract(Name = "RelMensagensSMS")]
        int RelMensagensSMS(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_CONTAT_SMS, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM, string strP_STATUS);

        [OperationContract(Name = "RelHistMovimFunc")]
        int RelHistMovimFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelMapaQuantMovFunc")]
        int RelMapaQuantMovFunc(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_ANO_REFER);

        [OperationContract(Name = "RelMeusAcessos")]
        int RelMeusAcessos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelMinhasMsgs")]
        int RelMinhasMsgs(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelMovimFuncTipoUnid")]
        int RelMovimFuncTipoUnid(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DT_INI, string strP_DT_FIM, string strP_TP_MOV);

        [OperationContract(Name = "RelMinhaBibliot")]
        int RelMinhaBibliot(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_COL);

        [OperationContract(Name = "RelFicInfoNucGestao")]
        int RelFicInfoNucGestao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [OperationContract(Name = "RelRelacResumAval")]
        int RelRelacResumAval(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_TP_AVAL_INST);

        [OperationContract(Name = "RelMapaAvalResulNucleo")]
        int RelMapaAvalResulNucleo(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [OperationContract(Name = "RelRelacNucleos")]
        int RelRelacNucleos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [OperationContract(Name = "RelRegisOcorr")]
        int RelRegisOcorr(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_NUCLEO);

        [OperationContract(Name = "RelRelacAlunoPorEscola")]
        int RelRelacAlunoPorEscola(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_CO_ANO_REFER);

        [OperationContract(Name = "RelRelacAlunoTransf")]
        int RelRelacAlunoTransf(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_REF, string strP_DE_EMP_REF, string strP_TP_TRANSF, string strP_DT_INI, string strP_DT_FIM, string strP_CNPJ_INSTI);

        [OperationContract(Name = "RelMapaTransfAluno")]
        int RelMapaTransfAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelRelacFuncPorDepto")]
        int RelRelacFuncPorDepto(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_DEPTO);

        [OperationContract(Name = "RelRelacFuncPorFuncao")]
        int RelRelacFuncPorFuncao(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL, string strP_CO_FUN);

        [OperationContract(Name = "RelRelacFuncPorUnidade")]
        int RelRelacFuncPorUnidade(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_COL, string strP_CO_SITU_COL);

        [OperationContract(Name = "RelInforOcorrAluno")]
        int RelInforOcorrAluno(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_EMP_ALU, string strP_CO_ALU, string strP_CO_SIGL_OCORR, string strP_DT_INI, string strP_DT_FIM);

        [OperationContract(Name = "RelAgendaContatos")]
        int RelAgendaContatos(string strIdentFunc, string strCaminhoRelatorioGerado, string strNomeRelatorio, string strP_CO_EMP, string strP_CO_COL);
    }
}