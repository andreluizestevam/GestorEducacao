unit U_FrmRelCurvaABCFreqProfInst;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCFreqProfInst = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QrlPerc: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRShape3: TQRShape;
    QRLabel2: TQRLabel;
    QRShape2: TQRShape;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel10: TQRLabel;
    QRLTipo: TQRLabel;
    QrlPeriodo: TQRLabel;
    QrlTit: TQRLabel;
    QRDBText3: TQRDBText;
    QRBand1: TQRBand;
    QRLabel27: TQRLabel;
    QRShape6: TQRShape;
    QrlTotal: TQRLabel;
    QRShape7: TQRShape;
    QrlTotalPerc: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioNO_RAZSOC_EMP: TStringField;
    QryRelatorioCO_EMP: TAutoIncField;
    QryRelatorioCO_COL: TIntegerField;
    QRLQtd: TQRLabel;
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
  FrmRelCurvaABCFreqProfInst: TFrmRelCurvaABCFreqProfInst;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCFreqProfInst.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
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
    begin
      //while not Eof do
      //begin
        QRLQtd.Caption := IntToStr(RecordCount);
        //Next;
      //end;
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

procedure TFrmRelCurvaABCFreqProfInst.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalPerc.Caption := '100,0';
  QrlTotal.Caption := IntToStr(totalFreq);
end;

procedure TFrmRelCurvaABCFreqProfInst.QRGroup1BeforePrint(
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
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('S') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + QryRelatorioCO_EMP.AsString +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';
    Open;
    totalFreq := 0;
    if not IsEmpty then
    begin
      //while not Eof do
      //begin
        totalFreq := RecordCount;
        //Next;
      //end;
    end;

  end;
  
end;

procedure TFrmRelCurvaABCFreqProfInst.QRBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  totalFreq := 0;
  QrlPerc.Caption := '';
  QrlTotal.Caption := '';
end;

end.
