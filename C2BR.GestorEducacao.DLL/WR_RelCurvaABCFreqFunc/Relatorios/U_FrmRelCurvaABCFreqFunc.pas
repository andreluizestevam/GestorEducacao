unit U_FrmRelCurvaABCFreqFunc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCFreqFunc = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QrlPeriodo: TQRLabel;
    QRLabel10: TQRLabel;
    QRLTipo: TQRLabel;
    QRLabel1: TQRLabel;
    QRShape3: TQRShape;
    QRShape2: TQRShape;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRDBText1: TQRDBText;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QrlPerc: TQRLabel;
    QRLabel27: TQRLabel;
    QRShape4: TQRShape;
    QrlTotal: TQRLabel;
    QRShape5: TQRShape;
    QrlTotalPerc: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_FUN: TAutoIncField;
    QryRelatorioNO_FUN: TStringField;
    QRLQtd: TQRLabel;
    QRLabel5: TQRLabel;
    procedure FormCreate(Sender: TObject);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totalFreq: Integer;
  public
    { Public declarations }
    dtInicial,dtFinal, codigoEmpresa, tipoPonto: String;
  end;

var
  FrmRelCurvaABCFreqFunc: TFrmRelCurvaABCFreqFunc;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCFreqFunc.FormCreate(Sender: TObject);
begin
  inherited;
  totalFreq := 0;
  QrlPerc.Caption := '';
end;

procedure TFrmRelCurvaABCFreqFunc.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //Retorna a qtde de presenças do funcionário
  //with DataModuleSGE.QrySql do
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;

    SQL.Text := 'SET LANGUAGE PORTUGUESE ' +
                   ' SELECT DISTINCT A.DT_FREQ, A.CO_COL'+
                   ' FROM TB199_FREQ_FUNC A '+
                   ' JOIN TB03_COLABOR C ON A.CO_COL = C.CO_COL AND A.CO_EMP = C.CO_EMP' +
                   ' JOIN TB15_FUNCAO F ON C.CO_FUN = F.CO_FUN ' +
                   ' JOIN TB25_EMPRESA E ON A.CO_EMP = E.CO_EMP ' +
                   ' WHERE A.DT_FREQ BETWEEN ' + '''' + dtInicial + '''' + ' and ' + '''' + dtFinal + '''' +
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('N') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + codigoEmpresa +
                   ' AND C.CO_FUN = ' + QryRelatorioCO_FUN.AsString +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';

    Open;
    QRLQtd.Caption := '0';
    if not IsEmpty then
    begin
      QRLQtd.Caption := IntToStr(RecordCount);
    end
    else
    begin
      QRLQtd.Caption := '-';
    end;
  end;

  if QRLQtd.Caption = '-' then
    QrlPerc.Caption := '-'
  else
  begin
    if totalFreq = 0 then
      QrlPerc.Caption := '0,00'
    else
      QrlPerc.Caption := FloatToStrF(((StrToInt(QRLQtd.Caption)/totalFreq)* 100),ffNumber,15,2);
  end;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;
end;

procedure TFrmRelCurvaABCFreqFunc.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlTotalPerc.Caption := '100,0';
  QrlTotal.Caption := IntToStr(totalFreq);
end;

procedure TFrmRelCurvaABCFreqFunc.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //with DataModuleSGE.QrySql do
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;

    SQL.Text :=  'SET LANGUAGE PORTUGUESE ' +
                   ' SELECT DISTINCT A.DT_FREQ, A.CO_COL'+
                   ' FROM TB199_FREQ_FUNC A '+
                   ' JOIN TB03_COLABOR C ON A.CO_COL = C.CO_COL AND A.CO_EMP = C.CO_EMP' +
                   ' JOIN TB15_FUNCAO F ON C.CO_FUN = F.CO_FUN ' +
                   ' JOIN TB25_EMPRESA E ON A.CO_EMP = E.CO_EMP ' +
                   ' WHERE A.DT_FREQ BETWEEN ' + '''' + dtInicial + '''' + ' and ' + '''' + dtFinal + '''' +
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('N') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + codigoEmpresa +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';

    Open;
    totalFreq := 0;
    if not IsEmpty then
    begin
      totalFreq := RecordCount;
    end;

  end;
end;

end.
