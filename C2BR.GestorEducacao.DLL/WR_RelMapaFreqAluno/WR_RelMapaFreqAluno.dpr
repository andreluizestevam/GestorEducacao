library WR_RelMapaFreqAluno;

uses
  Windows,
  Forms,
  ComServ,
  Controls,
  Classes,
  SysUtils,
  Dialogs,
  QuickRpt,
  QRPDFFilt,
  bsutils in '..\General\bsutils.pas',
  U_DataModuleSGE in '..\General\U_DataModuleSGE.pas' {DataModuleSGE: TDataModule},
  UTPersistente in '..\General\UTPersistente.pas',
  U_FrmRelTemplate in '..\General\U_FrmRelTemplate.pas' {FrmRelTemplate},
  U_FrmRelMapaFreqAluno in 'Relatorios\U_FrmRelMapaFreqAluno.pas' {FrmRelMapaFreqAluno},
  U_Funcoes in '..\General\U_Funcoes.pas';

// Controle Administrativo > Pesquisa de Opinião / Avaliações Institucionais
// Relatório: EMISSÃO DO MAPA DE RESULTADO DE AVALIAÇÃO
//
function DLLRelMapaFreqAluno(strIdentFunc, strPathReportGenerate, strReportName, strParamRel, strP_CO_EMP, strP_CO_MODU_CUR, strP_CO_CUR,
 strP_CO_TUR, strP_CO_ANO_REF, strP_CO_ALU, strP_CO_MAT, strP_CO_PARAM_FREQ, strP_CO_PARAM_FREQ_TIPO: PChar): Integer; export; cdecl;
var
  intReturn: Integer;
  Relatorio: TFrmRelMapaFreqAluno;
  PDFFilt : TQRPDFDocumentFilter;
  FilePath, SQLString : string;
begin

  intReturn := 0;
  PDFFilt := Nil;
  Relatorio := Nil;

  Try
    Try
      FilePath := '' + strPathReportGenerate + strReportName;
      PDFFilt := TQRPDFDocumentFilter.Create(FilePath);
      PDFFilt.AddFontMap( 'WebDings:ZapfDingBats' );
      PDFFilt.TextOnTop := true;
      PDFFilt.LeftMargin := 0;
      PDFFilt.topMargin := 0;
      PDFFilt.CompressionOn := true;
      PDFFilt.Concatenating := true;

      if strP_CO_ALU = 'T' then
      begin
        if strP_CO_PARAM_FREQ_TIPO = 'M' then
          SqlString := 'Select Distinct G.*, CM.NO_RED_MATERIA, A.NO_ALU,a.nu_nis, mm.co_alu_cad,mm.co_sit_mat,CT.CO_SIGLA_TURMA as NO_TURMA, CM.NO_SIGLA_MATERIA,C.NO_CUR,MO.DE_MODU_CUR,C.CO_PARAM_FREQUE,C.CO_PARAM_FREQ_TIPO, mm.DT_ALT_REGISTRO,' +
                     '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJan],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJanP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotFev],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotFevP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotMar],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotMarP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAbr],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT '+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAbrP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotMai],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotMaiP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotJun], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotJunP], ' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJul],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJulP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAgo],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAgoP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotSet],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotSetP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotOut],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotOutP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotNov],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotNovP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotDez], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotDezP], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotGeral], ' +
                       ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotGeralP] ' +
                     'From TB48_GRADE_ALUNO G, TB02_MATERIA M, TB107_CADMATERIAS CM, ' +
                     '     TB06_TURMAS T, TB07_ALUNO A, TB01_CURSO C ,TB08_MATRCUR MM,TB44_MODULO MO, TB129_CADTURMAS CT ' +
                     'WHERE G.CO_EMP = M.CO_EMP AND ' +
                     '      G.CO_EMP = A.CO_EMP AND ' +
                     '      G.CO_MODU_CUR = MO.CO_MODU_CUR AND'+
                     '      G.CO_EMP = T.CO_EMP AND ' +
                     '      G.CO_CUR = T.CO_CUR AND ' +
                     '      G.CO_TUR = T.CO_TUR AND ' +
                     '      CT.CO_TUR = T.CO_TUR AND ' +
                     '      CT.CO_MODU_CUR = T.CO_MODU_CUR AND ' +
                     '      G.CO_MAT = M.CO_MAT AND ' +
                     '      G.CO_CUR = C.CO_CUR AND ' +
                     '      G.CO_EMP = C.CO_EMP AND ' +
                     '      MM.CO_ALU = G.CO_ALU AND ' +
                     '      MM.CO_EMP = G.CO_EMP AND ' +
                     '      MM.CO_ANO_MES_MAT = G.CO_ANO_MES_MAT AND ' +
                     '      MM.CO_SIT_MAT in ( ' + quotedStr('A') + ',' + quotedStr('F') + ',' + quotedStr('T') + ',' + quotedStr('X') + ')' +
                     '      AND CM.ID_MATERIA = M.ID_MATERIA AND ' + // Incluída a tabela CADMATERIAS - Diego Nobre - 22/12/2008;
                     '      G.CO_ALU = A.CO_ALU ' +
                     ' AND G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                     ' AND G.NU_SEM_LET =  ' + IntToStr(1) +
                     ' AND G.CO_EMP = ' + strP_CO_EMP
        else
          SqlString := 'select distinct mm.co_cur,mm.co_modu_cur,mm.co_tur,mm.co_emp,mm.co_ano_mes_mat,mm.co_alu,a.no_alu,a.nu_nis,mm.co_alu_cad,ct.co_sigla_turma as no_turma,'+
                     'c.no_cur,mo.de_modu_cur,mm.co_sit_mat,c.co_param_freque,C.CO_PARAM_FREQ_TIPO, a.co_bairro as co_mat, a.nu_cpf_alu as no_red_materia, mm.DT_ALT_REGISTRO,' +
                     '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJan],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJanP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotFev],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotFevP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMar],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMarP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAbr],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT '+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAbrP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMai],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMaiP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJun], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJunP], ' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJul],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJulP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAgo],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAgoP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotSet],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotSetP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotOut],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotOutP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotNov],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotNovP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotDez], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotDezP], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotGeral], ' +
                       ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotGeralP] ' +
                     ' from tb08_matrcur mm '+
                     ' join tb07_aluno a on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp '+
                     ' join tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur '+
                     ' join tb01_curso c on mm.co_modu_cur = c.co_modu_cur and c.co_cur = mm.co_cur and c.co_emp = mm.co_emp '+
                     ' join tb06_turmas t on t.co_tur = mm.co_tur and t.co_cur = mm.co_cur and t.co_emp = mm.co_emp '+
                     ' join tb129_cadturmas ct on t.co_tur = ct.co_tur '+
                     ' where mm.co_emp = ' + strP_CO_EMP +
                     ' and MM.CO_SIT_MAT in ( ' + quotedStr('A') + ',' + quotedStr('F') + ',' + quotedStr('T') + ',' + quotedStr('X') + ')' +
                     ' AND MM.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF);

        if strP_CO_MODU_CUR <> 'T' then
        begin
          SqlString := SqlString + ' and MM.co_modu_cur = ' + strP_CO_MODU_CUR;
        end;

        if strP_CO_CUR <> 'T' then
        begin
          SqlString := SqlString + ' and MM.co_cur = ' + strP_CO_CUR;
        end;

        if strP_CO_TUR <> 'T' then
        begin
          SqlString := SqlString + ' and MM.co_tur = ' + strP_CO_TUR;
        end;

        if strP_CO_PARAM_FREQ_TIPO = 'M' then
          SqlString := SqlString + ' Order by A.NO_ALU, CM.NO_RED_MATERIA '
        else
        begin
          SqlString := SqlString + ' Order by A.NO_ALU';
        end;
      end
      else
      begin
        if strP_CO_PARAM_FREQ_TIPO = 'M' then
            SqlString := 'Select Distinct G.*, CM.NO_RED_MATERIA, A.NO_ALU,a.nu_nis, mm.co_alu_cad,mm.co_sit_mat,CT.CO_SIGLA_TURMA as NO_TURMA, CM.NO_SIGLA_MATERIA,C.NO_CUR,MO.DE_MODU_CUR,C.CO_PARAM_FREQUE,C.CO_PARAM_FREQ_TIPO, mm.DT_ALT_REGISTRO,' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJan],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJanP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotFev],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotFevP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotMar],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotMarP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAbr],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT '+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAbrP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotMai],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotMaiP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotJun], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotJunP], ' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJul],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotJulP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAgo],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotAgoP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotSet],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotSetP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotOut],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = G.CO_EMP'+
                        ' and F_PAR.CO_CUR = G.CO_CUR'+
                        ' and F_PAR.CO_TUR = G.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT = G.CO_MAT'+
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotOutP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotNov],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                        ' and F_PAR.CO_CUR = G.CO_CUR' +
                        ' and F_PAR.CO_TUR = G.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT = G.CO_MAT ' +
                        ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = G.CO_ALU) [TotNovP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotDez], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotDezP], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotGeral], ' +
                       ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = G.CO_EMP' +
                       ' and F_PAR.CO_CUR = G.CO_CUR' +
                       ' and F_PAR.CO_TUR = G.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = G.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT = G.CO_MAT' +
                       ' and YEAR(F_PAR.DT_FRE) = G.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = G.CO_ALU) [TotGeralP] ' +
                       'From TB48_GRADE_ALUNO G, TB02_MATERIA M, TB107_CADMATERIAS CM, ' +
                       '     TB06_TURMAS T, TB07_ALUNO A, TB01_CURSO C ,TB08_MATRCUR MM,TB44_MODULO MO, TB129_CADTURMAS CT ' +
                       'WHERE G.CO_EMP = M.CO_EMP AND ' +
                       '      G.CO_EMP = A.CO_EMP AND ' +
                       '      G.CO_MODU_CUR = MO.CO_MODU_CUR AND'+
                       '      G.CO_EMP = T.CO_EMP AND ' +
                       '      G.CO_CUR = T.CO_CUR AND ' +
                       '      G.CO_TUR = T.CO_TUR AND ' +
                       '      CT.CO_TUR = T.CO_TUR AND ' +
                       '      CT.CO_MODU_CUR = T.CO_MODU_CUR AND ' +
                       '      G.CO_MAT = M.CO_MAT AND ' +
                       '      G.CO_CUR = C.CO_CUR AND ' +
                       '      G.CO_EMP = C.CO_EMP AND ' +
                       '      MM.CO_ALU = G.CO_ALU AND ' +
                       '      MM.CO_EMP = G.CO_EMP AND ' +
                       '      MM.CO_ANO_MES_MAT = G.CO_ANO_MES_MAT AND ' +
                       '      MM.CO_SIT_MAT in ( ' + quotedStr('A') + ',' + quotedStr('F') + ',' + quotedStr('T') + ',' + quotedStr('X') + ')' +
                       '      AND CM.ID_MATERIA = M.ID_MATERIA AND ' + // Incluída a tabela CADMATERIAS - Diego Nobre - 22/12/2008;
                       '      G.CO_ALU = A.CO_ALU ' +
                       ' AND G.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                       ' AND G.NU_SEM_LET =  ' + IntToStr(1) +
                       ' AND G.CO_EMP = ' + strP_CO_EMP +
                       ' AND G.CO_ALU = ' + strP_CO_ALU
          else
            SqlString := 'select distinct mm.co_cur,mm.co_modu_cur,mm.co_tur,mm.co_emp,mm.co_ano_mes_mat,mm.co_alu,a.no_alu,a.nu_nis,mm.co_alu_cad,ct.co_sigla_turma as no_turma,'+
                       'c.no_cur,mo.de_modu_cur,mm.co_sit_mat,c.co_param_freque,C.CO_PARAM_FREQ_TIPO, a.co_bairro as co_mat, a.nu_cpf_alu as no_red_materia, mm.DT_ALT_REGISTRO,' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJan],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 1 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJanP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotFev],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 2 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotFevP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMar],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 3' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMarP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAbr],'+
                      '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT '+
                        ' and MONTH(F_PAR.DT_FRE) = 4 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAbrP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMai],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 5 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotMaiP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJun], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 6' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJunP], ' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJul],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 7 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotJulP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAgo],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 8 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotAgoP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotSet],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 9 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotSetP],' +
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotOut],'+
                       '(select Count(*) TotFaltas'+
                      ' From TB132_FREQ_ALU F_PAR'+
                      ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP'+
                        ' and F_PAR.CO_CUR = MM.CO_CUR'+
                        ' and F_PAR.CO_TUR = MM.CO_TUR'+
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR'+
                        ' and F_PAR.CO_MAT is null'+
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT'+
                        ' and MONTH(F_PAR.DT_FRE) = 10 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotOutP],'+
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('N') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotNov],' +
                      ' (select Count(*) TotFaltas'+
                       ' From TB132_FREQ_ALU F_PAR'+
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                        ' and F_PAR.CO_CUR = MM.CO_CUR' +
                        ' and F_PAR.CO_TUR = MM.CO_TUR' +
                        ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                        ' and F_PAR.CO_MAT is null ' +
                        ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT ' +
                        ' and MONTH(F_PAR.DT_FRE) = 11 ' +
                        ' and F_PAR.CO_FLAG_FREQ_ALUNO =' + quotedStr('S') +
                        ' and F_PAR.CO_ALU = MM.CO_ALU) [TotNovP],' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotDez], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and MONTH(F_PAR.DT_FRE) = 12 ' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotDezP], ' +
                      ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('N') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotGeral], ' +
                       ' (select Count(*) TotFaltas' +
                       ' From TB132_FREQ_ALU F_PAR' +
                       ' Where F_PAR.CO_EMP_ALU = MM.CO_EMP' +
                       ' and F_PAR.CO_CUR = MM.CO_CUR' +
                       ' and F_PAR.CO_TUR = MM.CO_TUR' +
                       ' and F_PAR.CO_MODU_CUR = MM.CO_MODU_CUR' +
                       ' and F_PAR.CO_MAT is null' +
                       ' and YEAR(F_PAR.DT_FRE) = MM.CO_ANO_MES_MAT' +
                       ' and F_PAR.CO_FLAG_FREQ_ALUNO = ' + quotedStr('S') +
                       ' and F_PAR.CO_ALU = MM.CO_ALU) [TotGeralP] ' +
                       ' from tb08_matrcur mm '+
                       ' join tb07_aluno a on mm.co_alu = a.co_alu and mm.co_emp = a.co_emp '+
                       ' join tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur '+
                       ' join tb01_curso c on mm.co_modu_cur = c.co_modu_cur and c.co_cur = mm.co_cur and c.co_emp = mm.co_emp '+
                       ' join tb06_turmas t on t.co_tur = mm.co_tur and t.co_cur = mm.co_cur and t.co_emp = mm.co_emp '+
                       ' join tb129_cadturmas ct on t.co_tur = ct.co_tur '+
                       ' where mm.co_emp = ' + strP_CO_EMP +
                       ' and MM.CO_SIT_MAT in ( ' + quotedStr('A') + ',' + quotedStr('F') + ',' + quotedStr('T') + ',' + quotedStr('X') + ')' +
                       ' AND MM.CO_ANO_MES_MAT = ' + QuotedStr(strP_CO_ANO_REF) +
                       ' AND MM.CO_ALU = ' + strP_CO_ALU;

          if strP_CO_PARAM_FREQ_TIPO = 'M' then
            SqlString := SqlString + ' Order by A.NO_ALU, CM.NO_RED_MATERIA '
          else
            SqlString := SqlString + ' Order by A.NO_ALU';

      end;

      // Cria uma instância do Relatório.

      Relatorio := TFrmRelMapaFreqAluno.Create(Nil);

      // Atualiza a Consulta de Detalhe do Relatório.
      Relatorio.QryRelatorio.Close;
      Relatorio.QryRelatorio.Connection := DM.Conn;
      Relatorio.QryRelatorio.SQL.Clear;
      Relatorio.QryRelatorio.SQL.Text := SQLString;
      Relatorio.QryRelatorio.Open;

      if Relatorio.QryRelatorio.IsEmpty then
      begin
        // Retorna -1 para Sem Registros na Consulta.
        intReturn := -1;
      end
      else
      begin
        // Atualiza Campos do Relatório Diretamente.
        Relatorio.QryCabecalhoRel.Close;
        Relatorio.QryCabecalhoRel.Connection := DM.Conn;
        Relatorio.QryCabecalhoRel.Sql.Text := 'Select E.cablinha1,E.cablinha2,E.cablinha3,E.cablinha4,E.NO_FANTAS_EMP,i.ImageStream ' +
                                    ' From TB25_EMPRESA E ' +
                                    ' join image i on E.LOGO_IMAGE_ID = i.ImageId ' +
                                    ' WHERE E.CO_EMP = ' + strP_CO_EMP;
        Relatorio.QryCabecalhoRel.Open;

        Relatorio.Qrl_IdentificacaoRel.Caption := strIdentFunc;
        Relatorio.QRLParametros.Caption := strParamRel;
        Relatorio.codigoEmpresa := strP_CO_EMP;

        // Prepara o Relatório e Gera o PDF.
        Relatorio.QuickRep1.Prepare;
        Relatorio.QRLPage.Caption := FormatFloat('000',Relatorio.QuickRep1.Printer.PageCount);
        Relatorio.QuickRep1.QRPrinter.ExportToFilter(PDFFilt);
        // Retorna 1 para o Relatório Gerado com Sucesso.
        intReturn := 1;
      end;

    Except
      on E : Exception do
        intReturn := 0;
        //ShowMessage(E.ClassName + ' error raised, with message : ' + E.Message);
    end;

  Finally
    Relatorio.QuickRep1.QRPrinter.Free;
    if intReturn = 1 then
      PDFFilt.EndConcat;
    PDFFilt.Free;
    Relatorio.Free;
    Result := intReturn;
  end;
  
end;

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer,
  
  //Relatórios
  DLLRelMapaFreqAluno;

end.
