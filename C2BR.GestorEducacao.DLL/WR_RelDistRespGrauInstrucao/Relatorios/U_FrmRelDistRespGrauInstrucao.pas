unit U_FrmRelDistRespGrauInstrucao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, StrUtils,
  StdCtrls, Mask, DBCtrls;

type
  TFrmRelDistRespGrauInstrucao = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRDBText1: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText8: TQRDBText;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioNO_RESP: TStringField;
    QryRelatorioDT_NASC_RESP: TDateTimeField;
    QryRelatorioNU_CPF_RESP: TStringField;
    QryRelatorioCO_SEXO_RESP: TStringField;
    QryRelatorioCO_RG_RESP: TStringField;
    QryRelatorioCO_ORG_RG_RESP: TStringField;
    QryRelatorioCO_ESTA_RG_RESP: TStringField;
    QryRelatorioDT_EMIS_RG_RESP: TDateTimeField;
    QryRelatorioDE_ENDE_RESP: TStringField;
    QryRelatorioNU_ENDE_RESP: TIntegerField;
    QryRelatorioCO_BAIRRO: TIntegerField;
    QryRelatorioCO_CIDADE: TIntegerField;
    QryRelatorioDE_COMP_RESP: TStringField;
    QryRelatorioNO_CIDA_RESP: TStringField;
    QryRelatorioCO_ESTA_RESP: TStringField;
    QryRelatorioCO_CEP_RESP: TStringField;
    QryRelatorioNU_TELE_RESI_RESP: TStringField;
    QryRelatorioNU_TELE_CELU_RESP: TStringField;
    QryRelatorioDES_EMAIL_RESP: TStringField;
    QryRelatorioNO_EMPR_RESP: TStringField;
    QryRelatorioNO_SETOR_RESP: TStringField;
    QryRelatorioNO_FUNCAO_RESP: TStringField;
    QryRelatorioDES_EMAIL_EMP: TStringField;
    QryRelatorioNU_TELE_COME_RESP: TStringField;
    QryRelatorioNU_RAMA_COME_RESP: TStringField;
    QryRelatorioNU_TIT_ELE: TStringField;
    QryRelatorioNU_ZONA_ELE: TStringField;
    QryRelatorioNU_SEC_ELE: TStringField;
    QryRelatorioNOM_USUARIO: TStringField;
    QryRelatorioDT_ALT_REGISTRO: TDateTimeField;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_cpf_alu: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriono_cur: TStringField;
    QRLabel10: TQRLabel;
    QRLPage: TQRLabel;
    QRLIdade: TQRLabel;
    QryRelatorioMATRICULA: TStringField;
    RespIdade: TQRLabel;
    GrauResp: TQRLabel;
    QRShape7: TQRShape;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel6: TQRLabel;
    qrllabelinstrucao: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape5: TQRShape;
    QRLabel7: TQRLabel;
    QRLabel11: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRShape3: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape2: TQRShape;
    SummaryBand1: TQRBand;
    qrTotal: TQRLabel;
    qrtotalnumero: TQRLabel;
    QRLabel14: TQRLabel;
    qrtotal2: TQRLabel;
    QryRelatorioTotResp: TIntegerField;
    QRLabel2: TQRLabel;
    qrldef: TQRLabel;
    QryRelatoriotp_def: TStringField;
    QRLTEL_RESI: TQRLabel;
    qrlSerieTurma: TQRLabel;
    qrlMatricula: TQRLabel;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioCO_RESP: TIntegerField;
    QryRelatorioNU_NIS_RESP: TBCDField;
    QryRelatorioCO_SIGLA_TURMA: TStringField;
    QryRelatorioNO_INST: TStringField;
    DBEdit1: TDBEdit;
    QRDBText2: TQRDBText;
    QryRelatorioGrauParen: TStringField;
    QRLNoAlu: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDistRespGrauInstrucao: TFrmRelDistRespGrauInstrucao;
  total : Integer;

implementation

uses U_DataModuleSGE, U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelDistRespGrauInstrucao.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
  var diasnoano: real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  qrlSerieTurma.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatorioCO_SIGLA_TURMA.AsString;
  qrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioMATRICULA.AsString);

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
  If (Not QryRelatorio.FieldByName('NU_TELE_RESI_RESP').IsNull) Then
    Begin
      if(Length(Trim(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString)) = 10) then
        QRLTEL_RESI.Caption := '(' + Copy(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString, 1, 2) + ') ' +
                             Copy(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString, 3, 4) +  '-' +
                             Copy(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString, 7, 4)
      else
        QRLTEL_RESI.Caption := '(  )-' + Copy(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString, 1, 4) + '-' +
                             Copy(QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString, 5, 4);

    End
  Else
    Begin
      QRLTEL_RESI.Caption := '';
    End;

  diasnoano := 365.6;
  QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano));
  if not(QryRelatorio.FieldByName('DT_NASC_RESP').IsNull)then
    RespIdade.Caption:= IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_RESP').AsDateTime) / diasnoano))
  else
    RespIdade.Caption:=' ';

   if not QryRelatorioNO_INST.IsNull then
    GrauResp.Caption := QryRelatorioNO_INST.AsString
   else
    GrauResp.Caption := '';

  if (QryRelatorio.FieldByName('TP_DEF').AsString = '')then
    QRLDEF.Caption:= '--';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'N')then
    QRLDEF.Caption:= 'Nenhuma';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'A')then
    QRLDEF.Caption:= 'Auditivo';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'V')then
    QRLDEF.Caption:= 'Visual';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'F')then
    QRLDEF.Caption:= 'Fisico';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'M')then
    QRLDEF.Caption:= 'Mental';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'I')then
    QRLDEF.Caption:= 'Multiplas';
     if (QryRelatorio.FieldByName('TP_DEF').AsString = 'O')then
    QRLDEF.Caption:= 'Outros';


    total := total+1;

end;

procedure TFrmRelDistRespGrauInstrucao.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
total := 0;
end;

procedure TFrmRelDistRespGrauInstrucao.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  qrtotalnumero.Caption:=QryRelatorio.fieldByName('TotResp').AsString;
  qrtotal2.Caption:=inttostr(total);
end;

end.

