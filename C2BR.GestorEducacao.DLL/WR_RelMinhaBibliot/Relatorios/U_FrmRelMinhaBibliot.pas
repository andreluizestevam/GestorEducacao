unit U_FrmRelMinhaBibliot;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMinhaBibliot = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLParametros: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel5: TQRLabel;
    QRLISBN: TQRLabel;
    QRLabel6: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLDatas: TQRLabel;
    QRLabel7: TQRLabel;
    QRLDtEntrega: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelMinhaBibliot: TFrmRelMinhaBibliot;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelMinhaBibliot.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',FormatFloat('0000000000000',QryRelatorio.FieldByName('CO_ISBN_ACER').AsFloat))
  else
    QRLISBN.Caption := '-';
    
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').IsNull then
    QRLDatas.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').AsDateTime);

  if not QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').IsNull then
    QRLDatas.Caption := QRLDatas.Caption + ' - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime);

  if not QryRelatorio.FieldByName('DT_REAL_DEVO_ACER').IsNull then
    QRLDtEntrega.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_REAL_DEVO_ACER').AsDateTime)
  else
    QRLDtEntrega.Caption := '-';

  total := total + 1;
end;

procedure TFrmRelMinhaBibliot.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  total := 0;
end;

procedure TFrmRelMinhaBibliot.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

procedure TFrmRelMinhaBibliot.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLParametros.Caption := 'Mat.: ' + FormatMaskText('99.999-9;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString);

  QRLParametros.Caption := QRLParametros.Caption + ' / ' + UpperCase(QryRelatorio.FieldByName('NO_COL').AsString);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMinhaBibliot]);

end.
