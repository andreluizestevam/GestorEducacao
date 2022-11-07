unit U_DataModuleSGE;

interface

uses
  Windows, Messages, UTPersistente,SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, StdCtrls, Buttons, RxGIF, jpeg, DB, ADODB, ShellAPI, IniFiles,
  TeeProcs, TeEngine, Chart, DbChart, RXCtrls, ComCtrls;

type
  TDataModuleSGE = class(TDataModule)
    Conn: TADOConnection;
    QrySql: TADOQuery;
    QryCurso: TADOQuery;
    QryCursoCO_CUR: TAutoIncField;
    QryCursoNO_CUR: TStringField;
    QryCursoQT_CARG_HORA_CUR: TIntegerField;
    QryCursoNO_MENT_CUR: TStringField;
    QryCursoDT_CRIA_CUR: TDateTimeField;
    QryCursoVL_TOTA_CUR: TBCDField;
    QryCursoQT_OCOR_CUR: TIntegerField;
    QryCursoPE_CERT_CUR: TBCDField;
    QryCursoDT_SITU_CUR: TDateTimeField;
    QryPesqCurso: TADOQuery;
    AutoIncField1: TAutoIncField;
    StringField3: TStringField;
    QryMateria: TADOQuery;
    QryMateriaCO_CUR: TIntegerField;
    QryMateriaCO_MAT: TAutoIncField;
    QryMateriaQT_CARG_HORA_MAT: TIntegerField;
    QryMateriaDT_INCL_MAT: TDateTimeField;
    QryMateriaDT_SITU_MAT: TDateTimeField;
    QryPesqMateria: TADOQuery;
    QryMateriaLkpCurso: TStringField;
    QryPesqMateriaCO_CUR: TIntegerField;
    QryPesqMateriaCO_MAT: TAutoIncField;
    QryPesqMateriaDE_MAT: TStringField;
    QryPesqMateriaQT_CARG_HORA_MAT: TIntegerField;
    QryPesqMateriaDT_INCL_MAT: TDateTimeField;
    QryPesqMateriaDT_SITU_MAT: TDateTimeField;
    QryPesqPreRequisito: TADOQuery;
    QryPesqTipoColaborador: TADOQuery;
    QryPesqTipoCalc: TADOQuery;
    QryPesqFuncao: TADOQuery;
    QryPesqDepartamento: TADOQuery;
    QryPesqGrauInst: TADOQuery;
    QryPesqContrato: TADOQuery;
    QryPesqUFCidade: TADOQuery;
    QryPesqTipoCalcCO_TPCAL: TAutoIncField;
    QryPesqTipoCalcNO_TPCAL: TStringField;
    QryPesqFuncaoCO_FUN: TAutoIncField;
    QryPesqGrauInstCO_INST: TAutoIncField;
    QryPesqGrauInstNO_INST: TStringField;
    QryPesqContratoCO_TPCON: TAutoIncField;
    QryPesqContratoNO_TPCON: TStringField;
    QryPesqUFCidadeCODUF: TStringField;
    QryPesqUFCidadeDESCRICAOUF: TStringField;
    QryPesqUFRG: TADOQuery;
    QryPesqUFRGCODUF: TStringField;
    QryPesqUFRGDESCRICAOUF: TStringField;
    QryPesqFuncaoNO_FUN: TStringField;
    QryPesqTipoPag: TADOQuery;
    QryPesqTipoPagCO_TIPO_PAGA: TAutoIncField;
    QryPesqTipoPagNO_TIPO_PAGA: TStringField;
    QryPesqTipoPagNU_PARC_PAGA: TIntegerField;
    QrySituacao: TADOQuery;
    QryFuncao: TADOQuery;
    QryDerpatamento: TADOQuery;
    QryGrauInst: TADOQuery;
    QryTipoColaborador: TADOQuery;
    QryTipoContrato: TADOQuery;
    QryTipoCalculo: TADOQuery;
    QryTipoParcela: TADOQuery;
    QryGrauDificuldade: TADOQuery;
    QrySituacaoCO_SITU: TAutoIncField;
    QrySituacaoNO_SITU: TStringField;
    QrySituacaoDT_SITU_SIT: TDateTimeField;
    QryFuncaoCO_FUN: TAutoIncField;
    QryFuncaoNO_FUN: TStringField;
    QryGrauInstCO_INST: TAutoIncField;
    QryGrauInstNO_INST: TStringField;
    QryTipoColaboradorCO_TPCOL: TAutoIncField;
    QryTipoColaboradorNO_TPCOL: TStringField;
    QryTipoContratoCO_TPCON: TAutoIncField;
    QryTipoContratoNO_TPCON: TStringField;
    QryTipoCalculoCO_TPCAL: TAutoIncField;
    QryTipoCalculoNO_TPCAL: TStringField;
    QryTipoParcelaCO_TIPO_PAGA: TAutoIncField;
    QryTipoParcelaNO_TIPO_PAGA: TStringField;
    QryTipoParcelaNU_PARC_PAGA: TIntegerField;
    QryGrauDificuldadeCO_GRAUDIF: TAutoIncField;
    QryGrauDificuldadeNO_GRAUDIF: TStringField;
    QryPesqGrauDif: TADOQuery;
    QryPesqGrauDifCO_GRAUDIF: TAutoIncField;
    QryPesqGrauDifNO_GRAUDIF: TStringField;
    QryTipoEmpresa: TADOQuery;
    QryTipoEmpresaCO_TIPOEMP: TIntegerField;
    QryTipoEmpresaNO_TIPOEMP: TStringField;
    QryPesqTipoEmpresa: TADOQuery;
    QryPesqMotivoCancel: TADOQuery;
    QryMotivoCancel: TADOQuery;
    QryTipoReserva: TADOQuery;
    QryPesqTipoReserva: TADOQuery;
    QryTipoReservaCO_RESERVA: TAutoIncField;
    QryTipoReservaNO_RESERVA: TStringField;
    QryPesqTipoReservaCO_RESERVA: TAutoIncField;
    QryPesqTipoReservaNO_RESERVA: TStringField;
    QryAutor: TADOQuery;
    QryClassificacao: TADOQuery;
    QryAreaConhecimento: TADOQuery;
    QryEditora: TADOQuery;
    QryAutorCO_AUTOR: TIntegerField;
    QryAutorNO_AUTOR: TStringField;
    QryEditoraCO_EDITORA: TIntegerField;
    QryEditoraNO_EDITORA: TStringField;
    QryClassificacaoCO_CLAS_ACER: TAutoIncField;
    QryClassificacaoNO_CLAS_ACER: TStringField;
    QryAreaConhecimentoCO_AREACON: TAutoIncField;
    QryAreaConhecimentoNO_AREACON: TStringField;
    QryPesqAutor: TADOQuery;
    IntegerField5: TIntegerField;
    StringField2: TStringField;
    QryPesqClass: TADOQuery;
    AutoIncField2: TAutoIncField;
    StringField6: TStringField;
    QryPesqAreaCon: TADOQuery;
    AutoIncField3: TAutoIncField;
    StringField7: TStringField;
    QryPesqEditora: TADOQuery;
    IntegerField6: TIntegerField;
    StringField8: TStringField;
    QryPesqTipoEmpresaCO_TIPOEMP: TIntegerField;
    QryPesqTipoEmpresaNO_TIPOEMP: TStringField;
    QryMotivoCancelCO_MOTI_CANC: TIntegerField;
    QryMotivoCancelDE_MOTI_CANC: TStringField;
    QryPesqMotivoCancelCO_MOTI_CANC: TIntegerField;
    QryPesqMotivoCancelDE_MOTI_CANC: TStringField;
    QryTipoParcelaPE_CORR_PARC: TBCDField;
    QryTipoParcelaCO_INDIENT_PARC: TStringField;
    QryPesqTipoPagPE_CORR_PARC: TBCDField;
    QryPesqTipoPagCO_INDIENT_PARC: TStringField;
    QryTipoParcelaQT_DIAS_PARC: TIntegerField;
    QryPesqTipoPagQT_DIAS_PARC: TIntegerField;
    QryRecQtdPendFin: TADOQuery;
    QryRecQtdPendBibli: TADOQuery;
    QryRecQtdPendFinTOTPENDFIN: TIntegerField;
    QryRecQtdPendBibliTOTPENDBIBLI: TIntegerField;
    QryTotMatRes: TADOQuery;
    QryTotMatResTOTMAT: TIntegerField;
    QryTotMatResTOTRES: TIntegerField;
    QryMotTrancamento: TADOQuery;
    QryPesqMotTrancamento: TADOQuery;
    QryMotTrancamentoCO_MOTI_TRAN_MAT: TIntegerField;
    QryMotTrancamentoDE_MOTI_TRAN_MAT: TStringField;
    QryPesqMotTrancamentoCO_MOTI_TRAN_MAT: TIntegerField;
    QryPesqMotTrancamentoDE_MOTI_TRAN_MAT: TStringField;
    QryDerpatamentoCO_DEPTO: TAutoIncField;
    QryDerpatamentoNO_DEPTO: TStringField;
    QryPesqDepartamentoCO_DEPTO: TAutoIncField;
    QryPesqDepartamentoNO_DEPTO: TStringField;
    QryModuloCurso: TADOQuery;
    QryModuloCursoCO_MODU_CUR: TIntegerField;
    QryModuloCursoDE_MODU_CUR: TStringField;
    QryPesqModuloCurso: TADOQuery;
    IntegerField1: TIntegerField;
    StringField1: TStringField;
    QryMateriaCO_SITU_MAT: TStringField;
    QryCursoCO_SITU: TStringField;
    QryCursoCO_IDENDES_CUR: TStringField;
    QryPesqMateriaCO_SITU_MAT: TStringField;
    QryDataAtualServidor: TADOQuery;
    QryDataAtualServidorDataAtual: TDateTimeField;
    QryPesquisaCurso: TADOQuery;
    QryPesquisaTurma: TADOQuery;
    QryTipoIdioma: TADOQuery;
    QryCursoFormacao: TADOQuery;
    QryTipoIdiomaNO_IDIOM: TStringField;
    QryTipoIdiomaCO_IDIOM: TAutoIncField;
    QryCursoCO_NIVEL_CUR: TStringField;
    QryMateriaQT_CRED_MAT: TIntegerField;
    QryPesqMateriaCO_SIGL_MAT: TStringField;
    QryPesqMateriaQT_CRED_MAT: TIntegerField;
    QryPesqCursoTurma: TADOQuery;
    QryPesqCursoTurmaCO_CUR: TIntegerField;
    QryPesqCursoTurmaNO_CUR: TStringField;
    QryPesqCursoMatricula: TADOQuery;
    QryPesqCursoMatriculaCO_CUR: TIntegerField;
    QryPesqCursoMatriculaNO_CUR: TStringField;
    QryPesqTurmaMatricula: TADOQuery;
    QryPesqTurmaMatriculaCO_TUR: TAutoIncField;
    QryPesqTurmaMatriculaNO_TUR: TStringField;
    QryPesqTipoAvaliacao: TADOQuery;
    QryPesqTipoQuestao: TADOQuery;
    QryPesqTipoAvaliacaoCO_TIPO_AVAL: TIntegerField;
    QryPesqTipoAvaliacaoNO_TIPO_AVAL: TStringField;
    QryPesqTipoQuestaoCO_TITU_QUES: TIntegerField;
    QryPesqTipoQuestaoNO_TITU_QUES: TStringField;
    QryMateriaNO_MAT: TStringField;
    QryPesqMateriaNO_MAT: TStringField;
    QryPesqDeparCurso: TADOQuery;
    QryPesqCoordCurso: TADOQuery;
    QryPesqDeparCursoCO_DPTO_CUR: TAutoIncField;
    QryPesqDeparCursoSG_DPTO_CUR: TStringField;
    QryPesqDeparCursoNO_DPTO_CUR: TStringField;
    QryPesqCoordCursoCO_DPTO_CUR: TIntegerField;
    QryPesqCoordCursoCO_COOR_CUR: TAutoIncField;
    QryPesqCoordCursoNO_COOR_CUR: TStringField;
    QryCursoLkpDepartamento: TStringField;
    QryCursoLkpSubDepto: TStringField;
    QryPesqDepto: TADOQuery;
    AutoIncField6: TAutoIncField;
    StringField4: TStringField;
    StringField5: TStringField;
    QryPesqCoordenacao: TADOQuery;
    IntegerField2: TIntegerField;
    AutoIncField7: TAutoIncField;
    StringField10: TStringField;
    QryPesqCursoVL_TOTA_CUR: TBCDField;
    QryPesqCursoCO_IDENDES_CUR: TStringField;
    QryPesqQtdeEmpAcervo: TADOQuery;
    QryPesqQtdeEmpAcervoQtdeEmprestimo: TIntegerField;
    QryPesqTipoSolicitacao: TADOQuery;
    QryPesqTipoSolicitacaoCO_TIPO_SOLI: TIntegerField;
    QryPesqTipoSolicitacaoNO_TIPO_SOLI: TStringField;
    QryPesqTipoSolicitacaoVL_UNIT_SOLI: TBCDField;
    QryPesqTipoSolicitacaoCO_SITU_SOLI: TStringField;
    QryPesqTipoSolicitacaoDT_SITU_SOLI: TDateTimeField;
    QryPesqTipoSolicitacaoNO_PROC_EXTE_SOLI: TStringField;
    QryTipoSolicitacao: TADOQuery;
    IntegerField3: TIntegerField;
    StringField11: TStringField;
    BCDField1: TBCDField;
    StringField13: TStringField;
    DateTimeField1: TDateTimeField;
    StringField14: TStringField;
    QryPesqTipoColaboradorCO_TPCAL: TAutoIncField;
    QryPesqTipoColaboradorNO_TPCAL: TStringField;
    QryCursoCO_DPTO_CUR: TIntegerField;
    QryCursoCO_SIGL_CUR: TStringField;
    QryCursoCO_SUB_DPTO_CUR: TIntegerField;
    QryPesqAval: TADOQuery;
    QryPesqAvalCO_PESQ_AVAL: TIntegerField;
    QryPesqAvalDT_AVAL: TDateTimeField;
    QryPesqAvalCO_CUR: TIntegerField;
    QryPesqAvalCO_TUR: TIntegerField;
    QryPesqAvalCO_MAT: TIntegerField;
    QryPesqAvalCO_COL: TIntegerField;
    QryPesqAvalCO_ALU: TIntegerField;
    QryPesqAvalDE_SUGE_AVAL: TStringField;
    QryPesqDisciplina: TADOQuery;
    QryPesqDisciplinaCO_CUR: TIntegerField;
    QryPesqDisciplinaCO_MAT: TIntegerField;
    QrySql2: TADOQuery;
    QrySqlInscricao: TADOQuery;
    QrySql3: TADOQuery;
    QryPesqTipoAvaliacaoCO_ESTI_AVAL: TStringField;
    QryTipoDocumento: TADOQuery;
    QryTipoDocumentoCO_TIPO_DOC: TIntegerField;
    QrySqlEstoque: TADOQuery;
    QryTipoDocumentoSIG_TIPO_DOC: TStringField;
    QryPesqEspecializacao: TADOQuery;
    QryPesqEspecializacaoCO_ESPEC: TIntegerField;
    QryPesqEspecializacaoDE_ESPEC: TStringField;
    QryCursoCO_EMP: TIntegerField;
    QryCursoDE_INF_LEG_CUR: TStringField;
    QryCursoPE_FALT_CUR: TBCDField;
    QryCursoQT_MATE_MAT: TIntegerField;
    QryCursoQT_MAT_DEP_MAT: TIntegerField;
    QryMateriaCO_EMP: TIntegerField;
    QryMateriaVL_CRED_MAT: TBCDField;
    QryPesqCursoCO_EMP: TIntegerField;
    QryRecQtdPendFinCO_EMP: TIntegerField;
    QryPesqCoordCursoSG_COOR_CUR: TStringField;
    QryPesqCoordCursoCO_EMP: TIntegerField;
    QryPesqMateriaCO_EMP: TIntegerField;
    QryPesqMateriaVL_CRED_MAT: TBCDField;
    QryPesqCursoTurmaCO_EMP: TIntegerField;
    QryPesqDeptoCO_EMP: TIntegerField;
    QryPesqTurmaMatriculaCO_EMP: TIntegerField;
    QryPesqCoordenacaoCO_EMP: TIntegerField;
    QryPesqCoordenacaoSG_COOR_CUR: TStringField;
    QryPesqCursoMatriculaCO_EMP: TIntegerField;
    QryPesqAluno: TADOQuery;
    QryPesqAlunoCO_ALU: TIntegerField;
    QryPesqAlunoCO_EMP: TIntegerField;
    QryPesqAlunoNO_ALU: TStringField;
    QryPesqAlunoCO_ALU_CAD: TStringField;
    QryPesqAlunoNO_APE_ALU: TStringField;
    QryPesqAlunoCO_INST: TIntegerField;
    QryPesqAlunoDT_NASC_ALU: TDateTimeField;
    QryPesqAlunoNU_CPF_ALU: TStringField;
    QryPesqAlunoCO_SEXO_ALU: TStringField;
    QryPesqAlunoCO_RG_ALU: TStringField;
    QryPesqAlunoCO_ORG_RG_ALU: TStringField;
    QryPesqAlunoCO_ESTA_RG_ALU: TStringField;
    QryPesqAlunoDT_EMIS_RG_ALU: TDateTimeField;
    QryPesqAlunoDE_ENDE_ALU: TStringField;
    QryPesqAlunoNU_ENDE_ALU: TIntegerField;
    QryPesqAlunoDE_COMP_ALU: TStringField;
    QryPesqAlunoNO_BAIR_ALU: TStringField;
    QryPesqAlunoNO_CIDA_ALU: TStringField;
    QryPesqAlunoCO_ESTA_ALU: TStringField;
    QryPesqAlunoCO_CEP_ALU: TStringField;
    QryPesqAlunoNU_TELE_RESI_ALU: TStringField;
    QryPesqAlunoNU_TELE_CELU_ALU: TStringField;
    QryPesqAlunoNO_PROF_ALU: TStringField;
    QryPesqAlunoNO_EMPR_ALU: TStringField;
    QryPesqAlunoNO_CARG_EMPR_ALU: TStringField;
    QryPesqAlunoDT_ADMI_EMPR_ALU: TDateTimeField;
    QryPesqAlunoNU_TELE_COME_ALU: TStringField;
    QryPesqAlunoNU_RAMA_COME_ALU: TStringField;
    QryPesqAlunoNO_PAI_ALU: TStringField;
    QryPesqAlunoNO_MAE_ALU: TStringField;
    QryPesqAlunoCO_NACI_ALU: TStringField;
    QryPesqAlunoDE_NACI_ALU: TStringField;
    QryPesqAlunoDE_NATU_ALU: TStringField;
    QryPesqAlunoNO_ENDE_ELET_ALU: TStringField;
    QryPesqAlunoNO_WEB_ALU: TStringField;
    QryPesqAlunoDT_CADA_ALU: TDateTimeField;
    QryPesqAlunoIM_FOTO_ALU: TBlobField;
    QryPesqAlunoCO_SITU_ALU: TStringField;
    QryPesqAlunoDT_SITU_ALU: TDateTimeField;
    QryPesqAlunoNU_TIT_ELE: TStringField;
    QryPesqAlunoNU_ZONA_ELE: TStringField;
    QryPesqAlunoNU_SEC_ELE: TStringField;
    QryPesqAlunoFLA_BOLSISTA: TStringField;
    QryPesqAlunoNU_PEC_DESBOL: TBCDField;
    QryTipoDocumentoDES_TIPO_DOC: TStringField;
    QryMateriaCO_SIGL_MAT: TStringField;
    QryMateriaDE_MAT: TStringField;
    QryPesquisaCursoCO_EMP: TIntegerField;
    QryPesquisaCursoCO_CUR: TAutoIncField;
    QryPesquisaCursoNO_CUR: TStringField;
    QryPesquisaTurmaCO_EMP: TIntegerField;
    QryPesquisaTurmaCO_TUR: TAutoIncField;
    comando: TADOCommand;
    qryClassInst: TADOQuery;
    qryInstEsp: TADOQuery;
    QryQuilombo: TADOQuery;
    QryQuilomboCO_QUI: TAutoIncField;
    QryQuilomboNO_QUI: TStringField;
    QryPredio: TADOQuery;
    QryPredioCO_PREDIO: TAutoIncField;
    QryPredioNO_PREDIO: TStringField;
    QryAbastAgua: TADOQuery;
    QryAbastAguaCO_ABAST: TAutoIncField;
    QryAbastAguaNO_ABAST: TStringField;
    QryPesquisaTurmaCO_PERI_TUR: TStringField;
    QryPesqPreRequisitoCO_EMP: TIntegerField;
    QryPesqPreRequisitoCO_MAT: TAutoIncField;
    QryPesqPreRequisitoQT_CARG_HORA_MAT: TIntegerField;
    QryPesqPreRequisitoQT_CRED_MAT: TIntegerField;
    QryPesqPreRequisitoCO_CUR: TIntegerField;
    QryPesqPreRequisitoDT_INCL_MAT: TDateTimeField;
    QryPesqPreRequisitoCO_SITU_MAT: TStringField;
    QryPesqPreRequisitoDT_SITU_MAT: TDateTimeField;
    QryPesqPreRequisitoVL_CRED_MAT: TBCDField;
    QryPesqPreRequisitoID_MATERIA: TIntegerField;
    QryPesqPreRequisitoCO_EMP_1: TIntegerField;
    QryPesqPreRequisitoID_MATERIA_1: TAutoIncField;
    QryPesqPreRequisitoNO_SIGLA_MATERIA: TStringField;
    QryPesqPreRequisitoNO_MATERIA: TStringField;
    QryPesqPreRequisitoDE_MATERIA: TStringField;
    QryPesqPreRequisitoCO_STATUS: TStringField;
    QryPesqPreRequisitoDT_STATUS: TDateTimeField;
    QryPesqDisciplinaNO_MATERIA: TStringField;
    QryCor: TADOQuery;
    QryCorCO_COR: TIntegerField;
    QryCorDES_COR: TStringField;
    QryCorno_sigla: TStringField;
    qryTamanho: TADOQuery;
    qryTamanhoCO_TAMANHO: TIntegerField;
    qryTamanhoDES_TAMANHO: TStringField;
    qryTamanhono_sigla: TStringField;
    QryPesquisaTurmaTURNO: TStringField;
    QryNucleoInst: TADOQuery;
    QryNucleoInstCO_NUCLEO: TAutoIncField;
    QryNucleoInstNO_SIGLA_NUCLEO: TStringField;
    QryFuncaoCO_FLAG_CLASSI_MAGIST: TBooleanField;
    QryFuncaoCO_FLAG_CLASSI_ADMINI: TBooleanField;
    QryFuncaoCO_FLAG_CLASSI_OPERAC: TBooleanField;
    QryFuncaoCO_FLAG_CLASSI_NUCLEO: TBooleanField;
    QryFuncaoMAG: TStringField;
    QryFuncaoOPE: TStringField;
    QryFuncaoADM: TStringField;
    QryFuncaoNUC: TStringField;
    QryNucleoInstDE_NUCLEO: TStringField;
    QryPesqGrauInstCO_SIGLA_INST: TStringField;
    QryPesqTipoCalcCO_SIGLA_TPCAL: TStringField;
    QryDerpatamentoCO_SIGLA_DEPTO: TStringField;
    qryPesqCadTurmas: TADOQuery;
    qryPesqCadTurmasNO_TURMA: TStringField;
    qryPesqCadTurmasCO_SIGLA_TURMA: TStringField;
    qryPesqCadTurmasCO_EMP: TIntegerField;
    qryPesqCadTurmasCO_STATUS_TURMA: TStringField;
    qryPesqCadTurmasDT_STATUS_TURMA: TDateTimeField;
    qryPesqCadTurmasCO_TUR: TAutoIncField;
    QryPesquisaTurmaNO_TURMA: TStringField;
    QryCursoCO_MODU_CUR: TIntegerField;
    QryCursoNO_REFER: TStringField;
    QryCursoCO_SIGL_REFER: TStringField;
    QryCursoCO_COOR: TIntegerField;
    QryCursoMED_FINAL_CUR: TBCDField;
    QryCursoFLA_CAL_PTES: TStringField;
    QryCursoTIP_OPE_PTES: TStringField;
    QryCursoVL_OPE_CTES: TBCDField;
    QryCursoFLA_CAL_PMEN: TStringField;
    QryCursoTIP_OPE_PMEN: TStringField;
    QryCursoVL_OPE_CMEN: TBCDField;
    QryCursoFLA_CAL_PBIM: TStringField;
    QryCursoTIP_OPE_PBIM: TStringField;
    QryCursoVL_OPE_CBIM: TBCDField;
    QryCursoFLA_CAL_PFIN: TStringField;
    QryCursoTIP_OPE_PFIN: TStringField;
    QryCursoVL_OPE_CFIN: TBCDField;
    QryCursoTIP_OPE_MFIN: TStringField;
    QryCursoVL_OPE_MFIN: TBCDField;
    QryCursoQT_AULA_CUR: TIntegerField;
    QryCursoNU_PORTA_CUR: TStringField;
    QryCursoNU_DOU_CUR: TStringField;
    QryCursoVL_PC_DECPONTO: TBCDField;
    QryCursoVL_PC_DESPROMO: TBCDField;
    QryCursoCO_MDP_VEST: TIntegerField;
    QryCursoCO_PREDEC_CUR: TIntegerField;
    QryCursoSEQ_IMPRESSAO: TIntegerField;
    QryPesquisaTurmaCO_SIGLA_TURMA: TStringField;
    QryPesqCursoCO_PARAM_FREQUE: TStringField;
    QryTipoEmpresaCL_CLAS_EMP: TStringField;
    QryPesqTipoEmpresaCL_CLAS_EMP: TStringField;
    QryCursoFormacaoCO_EMP: TIntegerField;
    QryCursoFormacaoCO_COL: TIntegerField;
    QryCursoFormacaoCO_ESPEC: TIntegerField;
    QryCursoFormacaoNU_CARGA_HORARIA: TIntegerField;
    QryCursoFormacaoCO_MESANO_INICIO: TStringField;
    QryCursoFormacaoCO_MESANO_FIM: TStringField;
    QryCursoFormacaoNO_INSTIT_CURSO: TStringField;
    QryCursoFormacaoNO_SIGLA_INSTIT_CURSO: TStringField;
    QryCursoFormacaoNO_CIDADE_CURSO: TStringField;
    QryCursoFormacaoCO_UF_CURSO: TStringField;
    QryCursoFormacaoCO_FLAG_CURSO_PRINCIPAL: TStringField;
    procedure QryFuncaoCalcFields(DataSet: TDataSet);
    procedure DataModuleCreate(Sender: TObject);
  private
    { Private declarations }

  public
    { Public declarations }
    bancoDeDados : TPersistente;
  end;

var
  DataModuleSGE: TDataModuleSGE;

  { Variavéis usadas internamente pelo o sistema, não é permitido criar variavéis
    comerçadas por "Sys" no sistema. }
  Sys_PontoFuncionario: string;
  Sys_PontoProfessor: string;
  Sys_TipoUsuario: string;
  Sys_Caption: String;
  Sys_CodigoMatrizAtiva : Integer;
  Sys_DescricaoMatrizAtiva : String;

  Sys_SaiDoSistema: Boolean;
  Sys_IdeAdmUsuario: Integer;
  Sys_CodigoUsuario: String;
  Sys_CodigoUsuarioSistema: String;
  Sys_TipoUsuarioSistema: String;
  Sys_NomeDoUsuario: string;
  Sys_SenhaUsuario: string;
  Aspas: String;
  Sys_FlaAccSelect: string;
  Sys_FlaAccInsert: string;
  Sys_FlaAccUpDate: string;
  Sys_FlaAccDelete: string;
  Sys_ConexaoServidorBD: Integer;
  Sys_NomeServidorBD: String;
  Sys_FlaPeriodoReservaMat: Boolean;
  Sys_FlaPeriodoInscMat: Boolean;
  Sys_FlaPeriodoMatricula: Boolean;
  Sys_FlaPeriodoTranMat: Boolean;
  Sys_FlaPeriodoTransfMat: Boolean;
  Sys_FlaPeriodoManGradeAluno: Boolean;
  Sys_FlaPeriodoAlteracaoMat: Boolean;
  Sys_FlaTrancaMatFin: Boolean;
  Sys_FlaGeraMatriculaAuto: Boolean;
  Sys_CodigoEmpresaAtiva: Integer;
  Sys_PrimeiraEmpresaAtiva: Integer;
  Sys_DescricaoEmpresaAtiva: String;
  Sys_FlaEscolaParticular: String;
  Sys_FlaFaculdade: String;
  Sys_DescricaoTipoCurso: String;
  Sys_DadosBoletoBancario: Boolean;
  Sys_TipoEnsino : String;

  { Variaveis de permissão de acesso do usuario em alguns modulo para alterar/incluir campos }
  Sys_FlaPermBolsaCadastroAluno: Boolean;
  Sys_FlaPermBloqueiaAluno: Boolean;
  Sys_FlaPermAlteraMenMatGradeAberta: Boolean;  

  { Codigo de lançamento automatico da biblioteca }
  Sys_CodigoHistoricoBiblioteca: Integer;
  Sys_CodigoLancCtaReceberBiblioteca: Integer;
  Sys_CodigoCentroCustoBiblioteca: Integer;

  { Codigo de lançamento automatico do Contas a receber }
  Sys_CodigoHistoricoCR: Integer;
  Sys_CodigoLancCtaContabilCR: Integer;
  Sys_CodigoCentroCustoCR: Integer;

  { Codigo de lançamento automatico de Solicitações }
  Sys_CodigoHistoricoSolicitacoes: Integer;
  Sys_CodigoLancCtaContabilSolicitacoes: Integer;
  Sys_CodigoCentroCustoSolicitacoes: Integer;

  { Codigo de lançamento automatico de inscrição vestibular }
  Sys_CodigoHistoricoLancVestibular: Integer;
  Sys_CodigoLancCtaContabilVestibular: Integer;
  Sys_CodigoCentroCustoVestibular: Integer;

implementation

//uses U_DMFinanceiro;

{$R *.dfm}
procedure TDataModuleSGE.QryFuncaoCalcFields(DataSet: TDataSet);
begin
  if DataSet.FieldByName('CO_FLAG_CLASSI_MAGIST').AsBoolean then
    DataSet.FieldByName('MAG').AsString := 'Sim'
  else
    DataSet.FieldByName('MAG').AsString := 'Não';

  if DataSet.FieldByName('CO_FLAG_CLASSI_NUCLEO').AsBoolean then
    DataSet.FieldByName('NUC').AsString := 'Sim'
  else
    DataSet.FieldByName('NUC').AsString := 'Não';

  if DataSet.FieldByName('CO_FLAG_CLASSI_OPERAC').AsBoolean then
    DataSet.FieldByName('OPE').AsString := 'Sim'
  else
    DataSet.FieldByName('OPE').AsString := 'Não';

  if DataSet.FieldByName('CO_FLAG_CLASSI_ADMINI').AsBoolean then
    DataSet.FieldByName('ADM').AsString := 'Sim'
  else
    DataSet.FieldByName('ADM').AsString := 'Não';
end;

procedure TDataModuleSGE.DataModuleCreate(Sender: TObject);
begin

  {
  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';

  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=BDPGEBARRIOBRA;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';

  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=10.0.88.2\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';

  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=conexao@aquarela;Persist Security Info=True;User ID=conexao@aquarela;'+
  'Initial Catalog=BDPGECONAQU;Data Source=(local);Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';

  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=gestoradmin;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=ip-0A530E71\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';

  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=@meninar#2011;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';    }  
                                                                                      
  Sys_NomeServidorBD := 'Provider=SQLOLEDB.1;Password=@meninar#2011;Persist Security Info=True;User ID=gestoradmin;'+
  'Initial Catalog=Gestor;Data Source=.\SQLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;'+
  'Use Encryption for Data=False;Tag with column collation when possible=False';     
  
  Conn.Connected := false;
  Conn.ConnectionString := Sys_NomeServidorBD;
end;

end.
