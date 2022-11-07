unit U_FrmRelObrasEmprestadas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelObrasEmprestadas = class(TFrmRelTemplate)
    QrlParametros: TQRLabel;
    QRBand1: TQRBand;
    QRDBText2: TQRDBText;
    QRDBText1: TQRDBText;
    QRDBText5: TQRDBText;
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
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelObrasEmprestadas: TFrmRelObrasEmprestadas;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelObrasEmprestadas.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').IsNull then
    QRLDatas.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').AsDateTime) + ' / ';

  if not QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').IsNull then
  begin
    QRLPrevisao.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime);

    if (QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime < Now) and (QryRelatorio.FieldByName('DT_REAL_DEVO_ACER').IsNull) then
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

procedure TFrmRelObrasEmprestadas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelObrasEmprestadas]);

end.
