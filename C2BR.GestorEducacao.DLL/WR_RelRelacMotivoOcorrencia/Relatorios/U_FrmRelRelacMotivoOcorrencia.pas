unit U_FrmRelRelacMotivoOcorrencia;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacMotivoOcorrencia = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLabel1: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText3: TQRDBText;
    QRLSitu: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QRLParam: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelRelacMotivoOcorrencia: TFrmRelRelacMotivoOcorrencia;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacMotivoOcorrencia.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if QryRelatorio.FieldByName('CO_SITUA_MOTIV_OCORR').AsString = 'A' then
    QRLSitu.Caption := 'Ativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUA_MOTIV_OCORR').AsDateTime)
  else
    QRLSitu.Caption := 'Inativo - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_SITUA_MOTIV_OCORR').AsDateTime);

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  total := total + 1;
end;

procedure TFrmRelRelacMotivoOcorrencia.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  total := 0;
end;

procedure TFrmRelRelacMotivoOcorrencia.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacMotivoOcorrencia]);

end.
