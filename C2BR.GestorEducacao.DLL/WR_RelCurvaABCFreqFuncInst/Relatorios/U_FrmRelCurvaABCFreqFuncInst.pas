unit U_FrmRelCurvaABCFreqFuncInst;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCFreqFuncInst = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    DetailBand1: TQRBand;
    QRBand1: TQRBand;
    QrlTit: TQRLabel;
    QRDBText3: TQRDBText;
    QrlPeriodo: TQRLabel;
    QRLabel10: TQRLabel;
    QRLTipo: TQRLabel;
    QRLabel1: TQRLabel;
    QRShape3: TQRShape;
    QRShape2: TQRShape;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QrlPerc: TQRLabel;
    QRLabel27: TQRLabel;
    QRShape6: TQRShape;
    QrlTotal: TQRLabel;
    QRShape7: TQRShape;
    QrlTotalPerc: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioNO_RAZSOC_EMP: TStringField;
    QryRelatorioCO_EMP: TAutoIncField;
    QRLQtd: TQRLabel;
    QRLabel5: TQRLabel;
    QRLNoCol: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
    totalFreq: Integer;
  public
    { Public declarations }
    dtInicial,dtFinal, tipoPonto: String;
  end;

var
  FrmRelCurvaABCFreqFuncInst: TFrmRelCurvaABCFreqFuncInst;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCFreqFuncInst.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  
  if not QryRelatorio.FieldByName('NO_COL').IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorio.FieldByName('NO_COL').AsString)
  else
    QRLNoCol.Caption := '-';
    
  //Retorna a qtde de presenças do funcionário
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'SET LANGUAGE PORTUGUESE ' +
                   ' SELECT DISTINCT A.DT_FREQ, A.CO_COL'+
                   ' FROM TB199_FREQ_FUNC A '+
                   ' JOIN TB03_COLABOR C ON A.CO_COL = C.CO_COL AND A.CO_EMP = C.CO_EMP' +
                   ' JOIN TB25_EMPRESA E ON A.CO_EMP = E.CO_EMP ' +
                   ' WHERE A.DT_FREQ BETWEEN ' + '''' + dtInicial + '''' + ' and ' + '''' + dtFinal + '''' +
                   ' AND C.CO_COL = ' + QryRelatorioCO_COL.AsString +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + QryRelatorioCO_EMP.AsString +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';

    Open;
    QRLQtd.Caption := '0';
    if not IsEmpty then
        QRLQtd.Caption := IntToStr(RecordCount)
    else
        QRLQtd.Caption := '-';
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

procedure TFrmRelCurvaABCFreqFuncInst.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalPerc.Caption := '100,0';
  QrlTotal.Caption := IntToStr(totalFreq);
end;

procedure TFrmRelCurvaABCFreqFuncInst.QRGroup1BeforePrint(
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
                   ' JOIN TB25_EMPRESA E ON A.CO_EMP = E.CO_EMP ' +
                   ' WHERE A.DT_FREQ BETWEEN ' + '''' + dtInicial + '''' + ' and ' + '''' + dtFinal + '''' +
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('N') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + QryRelatorioCO_EMP.AsString +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';
    
    Open;
    totalFreq := 0;
    if not IsEmpty then
    begin
      totalFreq := RecordCount;
    end;
    
  end;
end;

procedure TFrmRelCurvaABCFreqFuncInst.QRBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  totalFreq := 0;
  QrlPerc.Caption := '';
  QrlTotal.Caption := '';
end;

end.
