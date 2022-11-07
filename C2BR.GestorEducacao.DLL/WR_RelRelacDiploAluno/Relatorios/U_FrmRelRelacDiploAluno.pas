unit U_FrmRelRelacDiploAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacDiploAluno = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRGroup1: TQRGroup;
    QRLParam: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLColResp: TQRLabel;
    QRLabel23: TQRLabel;
    QRShape3: TQRShape;
    QRLObs: TQRLabel;
    QRLNumProMEC: TQRLabel;
    QRLNumLivProMEC: TQRLabel;
    QRLNumPagProMEC: TQRLabel;
    QRLDtRegProMEC: TQRLabel;
    QRLDtIniDip: TQRLabel;
    QRLDtFimDip: TQRLabel;
    QRLDtSolDip: TQRLabel;
    QRLDtEntDip: TQRLabel;
    QRLDtEncDip: TQRLabel;
    QRLDtColGraDip: TQRLabel;
    QRLAnoConDip: TQRLabel;
    QRLCHSerDip: TQRLabel;
    QRLCHCumDip: TQRLabel;
    QRShape4: TQRShape;
    QRLNumProEsc: TQRLabel;
    QRLNumLivProEsc: TQRLabel;
    QRLNumPagProEsc: TQRLabel;
    QRLDtRegProEsc: TQRLabel;
    QRShape5: TQRShape;
    QRLAluno: TQRLabel;
    QRBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape2: TQRShape;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacDiploAluno: TFrmRelRelacDiploAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacDiploAluno.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NU_PROC_ESC_DIP').IsNull then
    QRLNumProEsc.Caption := QryRelatorio.FieldByName('NU_PROC_ESC_DIP').AsString
  else
    QRLNumProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_LIVR_ESC_DIP').IsNull then
    QRLNumLivProEsc.Caption := QryRelatorio.FieldByName('NU_LIVR_ESC_DIP').AsString
  else
    QRLNumLivProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PAGI_ESC_DIP').IsNull then
    QRLNumPagProEsc.Caption := QryRelatorio.FieldByName('NU_PAGI_ESC_DIP').AsString
  else
    QRLNumPagProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('DT_REGI_ESC_DIP').IsNull then
    QRLDtRegProEsc.Caption := QryRelatorio.FieldByName('DT_REGI_ESC_DIP').AsString
  else
    QRLDtRegProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PROC_MEC_DIP').IsNull then
    QRLNumProMEC.Caption := QryRelatorio.FieldByName('NU_PROC_MEC_DIP').AsString
  else
    QRLNumProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('NU_LIVR_MEC_DIP').IsNull then
    QRLNumLivProMEC.Caption := QryRelatorio.FieldByName('NU_LIVR_MEC_DIP').AsString
  else
    QRLNumLivProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PAGI_MEC_DIP').IsNull then
    QRLNumPagProMEC.Caption := QryRelatorio.FieldByName('NU_PAGI_MEC_DIP').AsString
  else
    QRLNumPagProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('DT_REGI_MEC_DIP').IsNull then
    QRLDtRegProMEC.Caption := QryRelatorio.FieldByName('DT_REGI_MEC_DIP').AsString
  else
    QRLDtRegProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('DT_INIC_CURS_DIP').IsNull then
    QRLDtIniDip.Caption := QryRelatorio.FieldByName('DT_INIC_CURS_DIP').AsString
  else
    QRLDtIniDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_TERM_CURS_DIP').IsNull then
    QRLDtFimDip.Caption := QryRelatorio.FieldByName('DT_TERM_CURS_DIP').AsString
  else
    QRLDtFimDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_SOLI_DIP').IsNull then
    QRLDtSolDip.Caption := QryRelatorio.FieldByName('DT_SOLI_DIP').AsString
  else
    QRLDtSolDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ENTR_DIP').IsNull then
    QRLDtEntDip.Caption := QryRelatorio.FieldByName('DT_ENTR_DIP').AsString
  else
    QRLDtEntDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_PART_ENC').IsNull then
    QRLDtEncDip.Caption := QryRelatorio.FieldByName('DT_PART_ENC').AsString
  else
    QRLDtEncDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_COLACAO_GRAU').IsNull then
    QRLDtColGraDip.Caption := QryRelatorio.FieldByName('DT_COLACAO_GRAU').AsString
  else
    QRLDtColGraDip.Caption := '-';

  if not QryRelatorio.FieldByName('CO_ANO_SEM_CURSO').IsNull then
    QRLAnoConDip.Caption := QryRelatorio.FieldByName('CO_ANO_SEM_CURSO').AsString
  else
    QRLAnoConDip.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CARGA_HOR_CURSO').IsNull then
    QRLCHSerDip.Caption := QryRelatorio.FieldByName('NU_CARGA_HOR_CURSO').AsString
  else
    QRLCHSerDip.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CARGA_HOR_CUMP').IsNull then
    QRLCHCumDip.Caption := QryRelatorio.FieldByName('NU_CARGA_HOR_CUMP').AsString
  else
    QRLCHCumDip.Caption := '-';

  if not QryRelatorio.FieldByName('OBS_HISTORICO').IsNull then
    QRLObs.Caption := QryRelatorio.FieldByName('OBS_HISTORICO').AsString
  else
    QRLObs.Caption := '-';
    
  QRLColResp.Caption := 'Funcionário Responsável: ' + FormatMaskText('00.000-0;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString) + ' / ' + QryRelatorio.FieldByName('NO_COL').AsString;
end;

procedure TFrmRelRelacDiploAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLAluno.Caption := 'Aluno: ' + UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);
end;

end.
