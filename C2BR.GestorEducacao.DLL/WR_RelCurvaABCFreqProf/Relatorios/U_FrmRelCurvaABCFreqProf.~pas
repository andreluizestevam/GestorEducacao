unit U_FrmRelCurvaABCFreqProf;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCFreqProf = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRDBText1: TQRDBText;
    QRLPage: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QrlPerc: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRShape2: TQRShape;
    QRLabel2: TQRLabel;
    QRShape3: TQRShape;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRLTipo: TQRLabel;
    QRLabel10: TQRLabel;
    QrlPeriodo: TQRLabel;
    QRBand1: TQRBand;
    QRLabel27: TQRLabel;
    QRShape4: TQRShape;
    QrlTotal: TQRLabel;
    QRShape5: TQRShape;
    QrlTotalPerc: TQRLabel;
    QryRelatorioCO_FUN: TIntegerField;
    QryRelatorioNO_FUN: TStringField;
    QRLQtd: TQRLabel;
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
    dtInicial,dtFinal,tipoPonto,codigoEmpresa: String;
  end;

var
  FrmRelCurvaABCFreqProf: TFrmRelCurvaABCFreqProf;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCFreqProf.FormCreate(Sender: TObject);
begin
  inherited;
  totalFreq := 0;
  QrlPerc.Caption := '';
end;

procedure TFrmRelCurvaABCFreqProf.DetailBand1BeforePrint(
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
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('S') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + codigoEmpresa +
                   ' AND C.CO_FUN = ' + QryRelatorioCO_FUN.AsString +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';

    Open;
    QRLQtd.Caption := '0';
    if not IsEmpty then
    begin
   //   while not Eof do
    //  begin
        QRLQtd.Caption := IntToStr(RecordCount);
       // Next;
     // end;
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

procedure TFrmRelCurvaABCFreqProf.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlTotalPerc.Caption := '100,0';
  QrlTotal.Caption := IntToStr(totalFreq);
end;

procedure TFrmRelCurvaABCFreqProf.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
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
                   ' AND C.FLA_PROFESSOR = ' + QuotedStr('S') +
                   ' AND A.FLA_PRESENCA = ' + QuotedStr('S') +
                   ' AND A.CO_EMP = ' + codigoEmpresa +
                   ' GROUP BY A.DT_FREQ, A.CO_COL';

    Open;
    totalFreq := 0;
    if not IsEmpty then
    begin
     // while not Eof do
     // begin
        totalFreq := RecordCount;
       // Next;
      //end;
    end;

  end;
end;

end.
