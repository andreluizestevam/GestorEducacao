unit U_FrmRelInformacaoCurso;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
    TFrmRelInformacaoCurso = class(TFrmRelTemplate)
    QRGroup2: TQRGroup;
    QrlTitRel: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLSituacao: TQRLabel;
    QRDBText1: TQRDBText;
    QRLNivel: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRShape1: TQRShape;
    QRGroup3: TQRGroup;
    QRDBText8: TQRDBText;
    QRLabel13: TQRLabel;
    QRDBText9: TQRDBText;
    QRLabel8: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText11: TQRDBText;
    QRLabel16: TQRLabel;
    QRDBText12: TQRDBText;
    QRLabel3: TQRLabel;
    QRDBText13: TQRDBText;
    QRLabel18: TQRLabel;
    QRDBText23: TQRDBText;
    QRGroup4: TQRGroup;
    QRDBRichText2: TQRDBRichText;
    QryRelatorioCO_CUR: TAutoIncField;
    QryRelatorioCO_DPTO_CUR: TIntegerField;
    QryRelatorioCO_SUB_DPTO_CUR: TIntegerField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioQT_CARG_HORA_CUR: TIntegerField;
    QryRelatorioNO_MENT_CUR: TStringField;
    QryRelatorioDT_CRIA_CUR: TDateTimeField;
    QryRelatorioVL_TOTA_CUR: TBCDField;
    QryRelatorioCO_IDENDES_CUR: TStringField;
    QryRelatorioQT_OCOR_CUR: TIntegerField;
    QryRelatorioPE_CERT_CUR: TBCDField;
    QryRelatorioPE_FALT_CUR: TBCDField;
    QryRelatorioDT_SITU_CUR: TDateTimeField;
    QryRelatorioCO_SITU: TStringField;
    QryRelatorioCO_NIVEL_CUR: TStringField;
    QryRelatorioQT_MATE_MAT: TIntegerField;
    QryRelatorioDE_INF_LEG_CUR: TStringField;
    QryRelatorioNO_DPTO_CUR: TStringField;
    QryRelatorioDE_OBJE_CUR: TMemoField;
    QryRelatorioDE_PUBL_ALV_CUR: TMemoField;
    QryRelatorioDE_PRE_REQU_CUR: TMemoField;
    QryRelatorioDE_METO_CUR: TMemoField;
    QryRelatorioDE_PROG_CUR: TMemoField;
    QryRelatorioDE_MATE_FORN_CUR: TMemoField;
    QryRelatorioDE_CERT_CUR: TMemoField;
    QryRelatorioDE_CONT_PROG_CUR: TMemoField;
    QRLabel1: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioQT_MAT_DEP_MAT: TIntegerField;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRLCoordenador: TQRLabel;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioNO_REFER: TStringField;
    QryRelatorioCO_SIGL_REFER: TStringField;
    QryRelatorioCO_COOR: TIntegerField;
    QryRelatorioMED_FINAL_CUR: TBCDField;
    QryRelatorioFLA_CAL_PTES: TStringField;
    QryRelatorioTIP_OPE_PTES: TStringField;
    QryRelatorioVL_OPE_CTES: TBCDField;
    QryRelatorioFLA_CAL_PMEN: TStringField;
    QryRelatorioTIP_OPE_PMEN: TStringField;
    QryRelatorioVL_OPE_CMEN: TBCDField;
    QryRelatorioFLA_CAL_PBIM: TStringField;
    QryRelatorioTIP_OPE_PBIM: TStringField;
    QryRelatorioVL_OPE_CBIM: TBCDField;
    QryRelatorioFLA_CAL_PFIN: TStringField;
    QryRelatorioTIP_OPE_PFIN: TStringField;
    QryRelatorioVL_OPE_CFIN: TBCDField;
    QryRelatorioTIP_OPE_MFIN: TStringField;
    QryRelatorioVL_OPE_MFIN: TBCDField;
    QryRelatorioQT_AULA_CUR: TIntegerField;
    QryRelatorioNU_PORTA_CUR: TStringField;
    QryRelatorioNU_DOU_CUR: TStringField;
    QryRelatorioVL_PC_DECPONTO: TBCDField;
    QryRelatorioVL_PC_DESPROMO: TBCDField;
    QryRelatorioCO_MDP_VEST: TIntegerField;
    QryRelatorioCO_PREDEC_CUR: TIntegerField;
    QryRelatorioSEQ_IMPRESSAO: TIntegerField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QryRelatorioNO_SUBDPTO_CUR: TStringField;
    procedure QryRelatorioAfterScroll(DataSet: TDataSet);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup4BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelInformacaoCurso: TFrmRelInformacaoCurso;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelInformacaoCurso.QryRelatorioAfterScroll(
  DataSet: TDataSet);
begin
  LblTituloRel.Caption := '';
end;

procedure TFrmRelInformacaoCurso.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  inherited;
  QuickRep1.Free;
  QuickRep1.Destroy;
end;

procedure TFrmRelInformacaoCurso.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  Nivel: String;
begin

  QrlTitRel.Caption := {AnsiUpperCase(Sys_DescricaoTipoCurso)+} 'INFORMAÇÕES SOBRE  ' + AnsiUpperCase(QryRelatorio.FieldByName('NO_CUR').AsString);

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'U' then
    Nivel := 'Pós-Doutorado';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'O' then
    Nivel := 'Outros';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'R' then
    Nivel := 'Preparatório';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'T' then
    Nivel := 'Técnico';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'M' then
    Nivel := 'Ensino Médio';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'G' then
    Nivel := 'Graduação';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'S' then
    Nivel := 'Especialização';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'P' then
    Nivel := 'Pos-Graduação';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'E' then
    Nivel := 'Mestrado';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'D' then
    Nivel := 'Doutorado';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'F' then
    Nivel := 'Fundamental';

  if QryRelatorio.FieldByName('CO_NIVEL_CUR').AsString = 'I' then
    Nivel := 'Infantil';

  if QryRelatorio.FieldByName('CO_SITU').AsString = 'A' then
    QRLSituacao.Caption := 'Ativo';

  if QryRelatorio.FieldByName('CO_SITU').AsString = 'S' then
    QRLSituacao.Caption := 'Suspenso';

  if QryRelatorio.FieldByName('CO_SITU').AsString = 'C' then
    QRLSituacao.Caption := 'Cancelado';

  QRLNivel.Caption := Nivel;

  if not QryRelatorioCO_COOR.IsNull then
  begin

    //with DataModuleSGE.QrySql do
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select no_col from tb03_colabor ' +
                  'where co_col =' + QryRelatorioCO_COOR.AsString +
                  ' and co_emp =' + IntToStr(Sys_CodigoEmpresaAtiva);
      Open;
      if not IsEmpty then
        QRLCoordenador.Caption := FieldByName('no_col').AsString
      else
        QRLCoordenador.Caption := '-';
    end;
  end;

  QrlTitRel.Caption := Sys_DescricaoTipoCurso;


end;

procedure TFrmRelInformacaoCurso.QRGroup4BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  if QryRelatorioDE_CONT_PROG_CUR.AsString = '' then
  begin
    PrintBand := False;
    exit;
  end;

  LblTituloRel.Caption := 'CONTEÚDO PROGRAMÁTICO';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelInformacaoCurso]);

end.



