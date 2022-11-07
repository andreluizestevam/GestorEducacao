unit U_FrmRelMovimFuncTipoUnid;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMovimFuncTipoUnid = class(TFrmRelTemplate)
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLDescTot: TQRLabel;
    QRLParam: TQRLabel;
    QRLDepto: TQRLabel;
    QRLReferencia: TQRLabel;
    QRLMotivo: TQRLabel;
    QRLData: TQRLabel;
    QRLFuncionario: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel10: TQRLabel;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTpMovim: TQRLabel;
    QRDBText1: TQRDBText;
    QRBand2: TQRBand;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    qtdMI, qtdTE, qtdME, qtdRE, qtdNR: Integer;
  public
    { Public declarations }
  end;

var
  FrmRelMovimFuncTipoUnid: TFrmRelMovimFuncTipoUnid;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelMovimFuncTipoUnid.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  qtdMI := 0;
  qtdTE := 0;
  qtdME := 0;
  qtdRE := 0;
  qtdNR := 0;
end;

procedure TFrmRelMovimFuncTipoUnid.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  QRLFuncionario.Caption := FormatMaskText('0.000-00;0',QryRelatorio.FieldByName('CO_MAT_COL').AsString) + ' - ' + UpperCase(QryRelatorio.FieldByName('NO_COL').AsString);

  if not QryRelatorio.FieldByName('CADASTRO').IsNull then
    QRLData.Caption := FormatDateTime('dd/MM/yy', QryRelatorio.FieldByName('CADASTRO').AsDateTime)
  else
    QRLData.Caption := '-';

  if QryRelatorio.FieldByName('TIPO').AsString = 'ME' then
    qtdME := qtdME + 1
  else if QryRelatorio.FieldByName('TIPO').AsString = 'TE' then
    qtdTE := qtdTE + 1
  else
    qtdMI := qtdMI + 1;


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

  if QryRelatorio.FieldByName('TIPO').AsString = 'TE' then
  begin
    if not QryRelatorio.FieldByName('NO_DEPTO_DESTIN').IsNull then
      QRLDepto.Caption := QryRelatorio.FieldByName('NO_DEPTO_DESTIN').AsString
    else
      QRLDepto.Caption := '-';
  end
  else
  begin
    if not QryRelatorio.FieldByName('NO_DEPTO').IsNull then
      QRLDepto.Caption := QryRelatorio.FieldByName('NO_DEPTO').AsString
    else
      QRLDepto.Caption := '-';
  end;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00F2F2F2
  else
    DetailBand1.Color := clWhite;

end;

procedure TFrmRelMovimFuncTipoUnid.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLDescTot.Caption := '( ' + IntToStr(qtdMI + qtdTE + qtdME) + ' Geral ) - ( MI: ' + IntToStr(qtdMI) +
  ' / TE: ' + IntToStr(qtdTE) + ' / ME: ' + IntToStr(qtdME) + ' ) - ( '+ IntToStr(qtdRE) + ' Remunerada(s) - ' +
  IntToStr(qtdNR) + ' N�o Remunerada(s) )';
end;

procedure TFrmRelMovimFuncTipoUnid.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('TIPO').AsString = 'TE' then
    QRLTpMovim.Caption := 'Transfer�ncia Externa'
  else if QryRelatorio.FieldByName('TIPO').AsString = 'MI' then
    QRLTpMovim.Caption := 'Movimenta��o Interna'
  else
    QRLTpMovim.Caption := 'Movimenta��o Externa';
end;

end.
