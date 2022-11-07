unit U_FrmRelInadimplenciaTotal;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelInadimplenciaTotal = class(TFrmRelTemplate)
    QRLabel34: TQRLabel;
    QrlAno: TQRLabel;
    QRBand1: TQRBand;
    QrlValorCorrigido: TQRLabel;
    QRBand2: TQRBand;
    QRLabel9: TQRLabel;
    QRShape3: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLCurso: TQRLabel;
    QRDBNO_CUR: TQRDBText;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioVR_DEBITO: TBCDField;
    QryRelatorioVR_PAG: TBCDField;
    QryRelatorioVR_PAR_DOC: TBCDField;
    QryRelatorioNU_CPF_ALU: TStringField;
    QryRelatorioCO_RG_ALU: TStringField;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QryRelatorioVR_MUL_DOC: TBCDField;
    QryRelatorioVR_JUR_DOC: TBCDField;
    QrlTelefone: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLTotal: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
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
  FrmRelInadimplenciaTotal: TFrmRelInadimplenciaTotal;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelInadimplenciaTotal.QRBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  (*
    Zebrar o relat�rio!
  *)
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  //Telefone  
  if (not QryRelatorioNU_TELE_RESI_ALU.IsNull) and (QryRelatorioNU_TELE_RESI_ALU.AsString <> '') then
  begin
    QrlTelefone.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioNU_TELE_RESI_ALU.AsString);
  end
  else
  begin
    QrlTelefone.Caption := ' - ';
  end;


  //calcula os totais
  QrlValorCorrigido.Caption := '0,00';

  if QryRelatorioVR_PAR_DOC.IsNull = False then
  begin
    QrlValorCorrigido.Caption := FormatFloat('#,##0.00',QryRelatorioVR_PAR_DOC.AsFloat);
    QRLTotal.Caption := FormatFloat('#0.00',StrToFloat(QRLTotal.Caption) + QryRelatorioVR_PAR_DOC.AsFloat);
  end;
end;

procedure TFrmRelInadimplenciaTotal.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelInadimplenciaTotal.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotal.Caption := FloatToStrF(StrToFloat(QRLTotal.Caption),ffNumber,15,2);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelInadimplenciaTotal]);

end.