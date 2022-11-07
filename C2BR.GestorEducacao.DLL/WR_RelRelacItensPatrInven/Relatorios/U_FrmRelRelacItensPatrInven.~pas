unit U_FrmRelRelacItensPatrInven;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacItensPatrInven = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRBand1: TQRBand;
    QRL_DES_MARCA: TQRDBText;
    QRBand3: TQRBand;
    QRBand4: TQRBand;
    QRLabel9: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel6: TQRLabel;
    QrlTotalClass: TQRLabel;
    QRShape6: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel3: TQRLabel;
    QRLStatus: TQRLabel;
    QRLabel7: TQRLabel;
    QRLTpPatr: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel4: TQRLabel;
    QRLValor: TQRLabel;
    QRLabel12: TQRLabel;
    QRLDtCadas: TQRLabel;
    QRLNumPatr: TQRLabel;
    QRLNumProc: TQRLabel;
    QRLabel11: TQRLabel;
    QRLEstado: TQRLabel;
    QRLAviso: TQRLabel;
    QRLabel13: TQRLabel;
    QRShape1: TQRShape;
    QRLabel14: TQRLabel;
    QRShape2: TQRShape;
    QRLabel15: TQRLabel;
    QRShape3: TQRShape;
    QRLabel16: TQRLabel;
    QRShape4: TQRShape;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacItensPatrInven: TFrmRelRelacItensPatrInven;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacItensPatrInven.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalClass.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelRelacItensPatrInven.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalClass.Caption := '0';
end;

procedure TFrmRelRelacItensPatrInven.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'N' then
    QRLEstado.Caption := 'Novo'
  else if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'A' then
    QRLEstado.Caption := 'Com avarias'
  else if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'O' then
    QRLEstado.Caption := 'Normal'
  else if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'M' then
    QRLEstado.Caption := 'Em manutenção'
  else
    QRLEstado.Caption := 'Inativo';

  if not QryRelatorio.FieldByName('NU_PATR_ANT').IsNull then
    QRLNumPatr.Caption := QryRelatorio.FieldByName('NU_PATR_ANT').AsString
  else
    QRLNumPatr.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PROCESSO').IsNull then
    QRLNumProc.Caption := QryRelatorio.FieldByName('NU_PROCESSO').AsString
  else
    QRLNumProc.Caption := '-';

  if not QryRelatorio.FieldByName('DT_CADASTRO').IsNull then
    QRLDtCadas.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_CADASTRO').AsDateTime)
  else
    QRLDtCadas.Caption := '-';

  if QryRelatorio.FieldByName('TP_PATR').AsInteger = 1 then
    QRLTpPatr.Caption := 'Móvel'
  else
    QRLTpPatr.Caption := 'Imóvel';

  if QryRelatorio.FieldByName('CO_STATUS').AsString = 'A' then
    QRLStatus.Caption := 'Ativo'
  else
    QRLStatus.Caption := 'Inativo';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('VL_AQUIS').IsNull then
    QRLValor.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_AQUIS').AsFloat,ffNumber,15,2)
  else
    QRLValor.Caption := '-';

  QrlTotalClass.Caption := IntToStr(StrToInt(QrlTotalClass.Caption) +1);
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacItensPatrInven]);

end.

