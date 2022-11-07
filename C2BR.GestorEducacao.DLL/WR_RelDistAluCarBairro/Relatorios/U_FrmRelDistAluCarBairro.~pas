unit U_FrmRelDistAluCarBairro;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, U_DataModuleSGE,
  StdCtrls;

type
  TFrmRelDistAluCarBairro = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLEtnia: TQRLabel;
    QRLRenda: TQRLabel;
    QRLDef: TQRLabel;
    QRLIdade: TQRLabel;
    QRDBText2: TQRDBText;
    QRGroup1: TQRGroup;
    QrlCurso: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLDtNasc: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    qrlMatricula: TQRLabel;
    QrlCidadeBairro: TQRLabel;
    QryRelatorioco_cur: TIntegerField;
    QryRelatoriono_cur: TStringField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatoriono_tur: TStringField;
    QryRelatorionu_cpf_RESP: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriotp_raca: TStringField;
    QryRelatoriotp_def: TStringField;
    QryRelatoriorenda_familiar: TStringField;
    QryRelatoriocoduf: TStringField;
    QryRelatoriono_cidade: TStringField;
    QryRelatoriono_bairro: TStringField;
    QryRelatorioBE: TStringField;
    QryRelatorioPASSE: TStringField;
    SummaryBand1: TQRBand;
    QRLabel25: TQRLabel;
    QrlTotal: TQRLabel;
    QryRelatorioco_alu: TAutoIncField;
    qrlIEDF: TQRLabel;
    QRBand1: TQRBand;
    QRLabel24: TQRLabel;
    QRLTotalBairro: TQRLabel;
    QRLNuNis: TQRLabel;
    QRLParametros: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QryRelatorioco_tur: TAutoIncField;
    QRLabel7: TQRLabel;
    QRDBText6: TQRDBText;
    QryRelatoriode_modu_cur: TStringField;
    QryRelatorionu_nis: TBCDField;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDistAluCarBairro: TFrmRelDistAluCarBairro;

implementation

uses U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelDistAluCarBairro.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatoriono_alu.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatoriono_alu.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  QRLNuNis.Caption := QryRelatorionu_nis.AsString;

  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'SELECT 1 FROM TB165_ALU_INST_ESP '+
                '	WHERE CO_ALU = ' + QryRelatorioco_alu.AsString;
    Open;

    if not IsEmpty then
      qrlIEDF.Caption := 'Sim'
    else
      qrlIEDF.Caption := 'Não';
  end;

  qrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioco_alu_cad.AsString);

  QrlCidadeBairro.Caption := QryRelatoriono_cidade.AsString + '/' + QryRelatoriono_bairro.AsString;

  QRLDtNasc.Caption := FormatDateTime('dd/mm/yy', QryRelatorio.FieldByName('dt_nasc_alu').AsFloat);

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';
  
  QRLEtnia.Caption := '-';
  QRLRenda.Caption := '-';
  QRLDef.Caption := '-';

  with QryRelatorio do
  begin
    if FieldByName('tp_raca').AsString = 'B' then
      QRLEtnia.Caption := 'Branca';
    if FieldByName('tp_raca').AsString = 'P' then
      QRLEtnia.Caption := 'Negra';
    if FieldByName('tp_raca').AsString = 'A' then
      QRLEtnia.Caption := 'Amarela';
    if FieldByName('tp_raca').AsString = 'D' then
      QRLEtnia.Caption := 'Parda';
    if FieldByName('tp_raca').AsString = 'I' then
      QRLEtnia.Caption := 'Indígena';
    if FieldByName('tp_raca').AsString = 'N' then
      QRLEtnia.Caption := 'Não Declarada';

    if FieldByName('RENDA_FAMILIAR').AsString = '1' then
      QRLRenda.Caption := '1 a 3 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '2' then
      QRLRenda.Caption := '3 a 5 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '3' then
      QRLRenda.Caption := '+5 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '4' then
      QRLRenda.Caption := 'Sem Renda';

    if FieldByName('TP_DEF').AsString = 'N' then
      QRLDef.Caption := 'Nenhuma';
    if FieldByName('TP_DEF').AsString = 'A' then
      QRLDef.Caption := 'Auditivo';
    if FieldByName('TP_DEF').AsString = 'V' then
      QRLDef.Caption := 'Visual';
    if FieldByName('TP_DEF').AsString = 'F' then
      QRLDef.Caption := 'Físico';
    if FieldByName('TP_DEF').AsString = 'M' then
      QRLDef.Caption := 'Mental';
    if FieldByName('TP_DEF').AsString = 'I' then
      QRLDef.Caption := 'Múltiplas';
    if FieldByName('TP_DEF').AsString = 'O' then
      QRLDef.Caption := 'Outros';

// se for em branco mostra "-";
    //if  QRLEtnia.Caption = '' then

  end;

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);

  QRLTotalBairro.Caption := IntToStr(StrToInt(QRLTotalBairro.Caption) +1);

end;

procedure TFrmRelDistAluCarBairro.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelDistAluCarBairro.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotalBairro.Caption := '0';
end;

end.
