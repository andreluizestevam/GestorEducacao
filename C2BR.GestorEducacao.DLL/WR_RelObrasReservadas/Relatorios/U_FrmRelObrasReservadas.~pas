unit U_FrmRelObrasReservadas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelObrasReservadas = class(TFrmRelTemplate)
    QrlParametros: TQRLabel;
    QRBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText1: TQRDBText;
    QRShape6: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText7: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel6: TQRLabel;
    QRLTotal: TQRLabel;
    QRDBText6: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRLDatas: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLPrevisao: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    strP_CO_AREACON, strP_CO_CLAS, strP_DT_INI, strP_DT_FIM : String;
  end;

var
  FrmRelObrasReservadas: TFrmRelObrasReservadas;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelObrasReservadas.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('DT_RESERVA').IsNull then
    QRLDatas.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_RESERVA').AsDateTime) + ' / ';

  if not QryRelatorio.FieldByName('DT_LIMITE_NECESSI_RESERVA').IsNull then
  begin
    QRLPrevisao.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_LIMITE_NECESSI_RESERVA').AsDateTime);

    if (QryRelatorio.FieldByName('DT_LIMITE_NECESSI_RESERVA').AsDateTime < Now) and (QryRelatorio.FieldByName('DT_LIMITE_NECESSI_RESERVA').IsNull) then
      QRLPrevisao.Font.Color := clRed
    else
      QRLPrevisao.Font.Color := clBlack;
  end
  else
    QRLPrevisao.Caption := '-';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  if not QryRelatorio.FieldByName('CO_MAT_COL').IsNull then
    QRLMatricula.Caption := FormatMaskText('99.999-9;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString)
  else
    QRLMatricula.Caption := '-';

  if qrBand1.Color = clWhite then
    qrBand1.Color := $00D8D8D8
  else
    qrBand1.Color := clWhite;

end;

procedure TFrmRelObrasReservadas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelObrasReservadas.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if strP_CO_AREACON <> 'T' then
    QrlParametros.Caption := '( Área Conhecimento: ' + QryRelatorio.FieldByName('NO_AREACON').AsString
  else
    QrlParametros.Caption := '( Área Conhecimento: Todas';

  if strP_CO_CLAS <> 'T' then
    QrlParametros.Caption := QrlParametros.Caption +  ' - Classificação: ' + QryRelatorio.FieldByName('NO_CLAS_ACER').AsString
  else
    QrlParametros.Caption := QrlParametros.Caption +  ' - Classificação: Todas';

  QrlParametros.Caption := QrlParametros.Caption + ' - Período: ' + strP_DT_INI + ' à ' + strP_DT_FIM + ' )';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelObrasReservadas]);

end.
