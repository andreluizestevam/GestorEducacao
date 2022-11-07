unit U_FrmRelHistMovimFunc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelHistMovimFunc = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLDescTot: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel6: TQRLabel;
    QRLParam: TQRLabel;
    QRLDestino: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel7: TQRLabel;
    QRLReferencia: TQRLabel;
    QRLMotivo: TQRLabel;
    QRLTM: TQRLabel;
    QRLData: TQRLabel;
    QRLFuncionario: TQRLabel;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    qtdMI, qtdTE, qtdME, qtdRE, qtdNR: Integer;
  public
    { Public declarations }
  end;

var
  FrmRelHistMovimFunc: TFrmRelHistMovimFunc;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelHistMovimFunc.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  qtdMI := 0;
  qtdTE := 0;
  qtdME := 0;
  qtdRE := 0;
  qtdNR := 0;
end;

procedure TFrmRelHistMovimFunc.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  QRLFuncionario.Caption := FormatMaskText('0.000-00;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString) + ' - ' + UpperCase(QryRelatorio.FieldByName('NO_COL').AsString);

  if not QryRelatorio.FieldByName('CADASTRO').IsNull then
    QRLData.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('CADASTRO').AsDateTime)
  else
    QRLData.Caption := '-';

  if QryRelatorio.FieldByName('CO_TIPO_MOVIM').AsString = 'ME' then
  begin
    QRLTM.Caption := 'Movimenta��o Externa';
    qtdME := qtdME + 1;
  end
  else if QryRelatorio.FieldByName('CO_TIPO_MOVIM').AsString = 'MI' then
  begin
    QRLTM.Caption := 'Movimenta��o Interna';
    qtdMI := qtdMI + 1;
  end
  else
  begin
    QRLTM.Caption := 'Transfer�ncia Externa';
    qtdTE := qtdTE + 1;
  end;

  if QryRelatorio.FieldByName('MOTIVO').AsString = 'TEX' then
    QRLMotivo.Caption := 'Transfer�ncia Externa'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'DIS' then
    QRLMotivo.Caption := 'Disponibilidade'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'APO' then
    QRLMotivo.Caption := 'Atividade Pontual'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'OUT' then
    QRLMotivo.Caption := 'Outros'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'PRO' then
    QRLMotivo.Caption := 'Promo��o'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'TIN' then
    QRLMotivo.Caption := 'Transfer�ncia Interna'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'FER' then
    QRLMotivo.Caption := 'F�rias'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'LME' then
    QRLMotivo.Caption := 'Licen�a M�dica'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'LMA' then
    QRLMotivo.Caption := 'Licen�a Maternidade'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'LPA' then
    QRLMotivo.Caption := 'Licen�a Paternidade'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'LPR' then
    QRLMotivo.Caption := 'Licen�a Pr�mia'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'LFU' then
    QRLMotivo.Caption := 'Licen�a Funcional'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'OLI' then
    QRLMotivo.Caption := 'Outras Licen�as'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'DEM' then
    QRLMotivo.Caption := 'Demiss�o'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'ECO' then
    QRLMotivo.Caption := 'Encerramento Contrato'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'AFA' then
    QRLMotivo.Caption := 'Afastamento'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'SUS' then
    QRLMotivo.Caption := 'Suspens�o'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'CAP' then
    QRLMotivo.Caption := 'Capacita��o'
  else if QryRelatorio.FieldByName('MOTIVO').AsString = 'TRE' then
    QRLMotivo.Caption := 'Treinamento'
  else
    QRLMotivo.Caption := 'Motivos Outros';

  if not QryRelatorio.FieldByName('INICIO').IsNull then
  begin
    QRLReferencia.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('INICIO').AsDateTime) + ' - ';
    if not QryRelatorio.FieldByName('FIM').IsNull then
      QRLReferencia.Caption := QRLReferencia.Caption + FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('FIM').AsDateTime)
    else
      QRLReferencia.Caption := QRLReferencia.Caption + ' **** ';
  end
  else
    QRLReferencia.Caption := '-';


  QRLReferencia.Caption := QRLReferencia.Caption + ' - ' + QryRelatorio.FieldByName('REMUN').AsString;

  if QryRelatorio.FieldByName('REMUN').AsString <> 'S' then
    qtdRE := qtdRE + 1
  else
    qtdNR := qtdNR + 1;

  if QryRelatorio.FieldByName('CO_TIPO_MOVIM').AsString <> 'TE' then
    QRLDestino.Caption := QryRelatorio.FieldByName('DESTINO_INT').AsString
  else
  begin
    QRLDestino.Caption := QryRelatorio.FieldByName('NO_INSTIT_TRANSF').AsString;
  end;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00F2F2F2
  else
    DetailBand1.Color := clWhite;

end;

procedure TFrmRelHistMovimFunc.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLDescTot.Caption := '( ' + IntToStr(qtdMI + qtdTE + qtdME) + ' Geral ) - ( MI: ' + IntToStr(qtdMI) +
  ' / TE: ' + IntToStr(qtdTE) + ' / ME: ' + IntToStr(qtdME) + ' ) - ( '+ IntToStr(qtdRE) + ' Remunerada(s) - ' +
  IntToStr(qtdNR) + ' N�o Remunerada(s) )';
end;

end.