unit U_FrmRelItensPatrimonio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelItensPatrimonio = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRBand1: TQRBand;
    QRL_SG_UNIDADE: TQRDBText;
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
    QRLabel11: TQRLabel;
    QRLTpPatr: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel4: TQRLabel;
    QRLValor: TQRLabel;
    QRLabel12: TQRLabel;
    QRLDtCadas: TQRLabel;
    QRLNumPatr: TQRLabel;
    QRLNumProc: TQRLabel;
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
  FrmRelItensPatrimonio: TFrmRelItensPatrimonio;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelItensPatrimonio.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalClass.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelItensPatrimonio.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalClass.Caption := '0';
end;

procedure TFrmRelItensPatrimonio.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
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
  RegisterClasses([TFrmRelItensPatrimonio]);

end.

