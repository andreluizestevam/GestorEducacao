unit U_FrmRelCurvaABCFreq;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmrelCurvaABCFreq = class(TFrmRelTemplate)
    QRLTipo: TQRLabel;
    QRLDescCurso: TQRLabel;
    QRBand1: TQRBand;
    QRShape14: TQRShape;
    QRDBText2: TQRDBText;
    QrlPerc: TQRLabel;
    QRBand2: TQRBand;
    QrlTotal: TQRLabel;
    QRLabel27: TQRLabel;
    QRShape15: TQRShape;
    QrlPeriodo: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QrlTotalPerc: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLParametros: TQRLabel;
    QRLDesModulo: TQRLabel;
    QRLQtd: TQRLabel;
    procedure FormCreate(Sender: TObject);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QrlTotalPrint(sender: TObject; var Value: String);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    FTotalFrequencia : Integer;
    { Private declarations }
  public
    { Public declarations }
    dtInicial,dtFinal,tpPresenca,codigoEmpresa : String;
    property TotalFrequencia : Integer read FTotalFrequencia write FTotalFrequencia;
  end;

var
  FrmrelCurvaABCFreq: TFrmrelCurvaABCFreq;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmrelCurvaABCFreq.FormCreate(Sender: TObject);
begin
  inherited;
  FTotalFrequencia := 0;
  QrlPerc.Caption := '';
end;

procedure TFrmrelCurvaABCFreq.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  ' SET LANGUAGE PORTUGUESE ' +
                 ' SELECT  CM.NO_MATERIA, COUNT(A.ID_FREQ_ALUNO) QTD '+
                 ' FROM TB132_FREQ_ALU A '+
                 ' JOIN TB02_MATERIA B on A.CO_EMP_ALU = B.CO_EMP and A.CO_MAT = B.CO_MAT AND B.CO_CUR = A.CO_CUR '+
                 ' JOIN TB107_CADMATERIAS CM on B.ID_MATERIA = CM.ID_MATERIA ' +
                 ' WHERE A.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                 ' AND A.CO_EMP_ALU = '+ codigoEmpresa +
                 ' AND A.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                 ' AND A.DT_FRE BETWEEN ' + '''' + dtInicial + '''' + ' and ' + '''' + dtFinal + '''' +
                 ' AND A.CO_FLAG_FREQ_ALUNO = ' + QuotedStr(tpPresenca) +
                 ' AND A.CO_ANO_REFER_FREQ_ALUNO = ' + QryRelatorio.FieldByName('CO_ANO_GRADE').AsString +
                 ' AND B.CO_MAT = ' + QryRelatorio.FieldByName('CO_MAT').AsString +
                 ' GROUP BY CM.NO_MATERIA ';
    Open;

    if not IsEmpty then
    begin
      QRLQtd.Caption := FieldByName('QTD').AsString;
      if FTotalFrequencia > 0 then
        QrlPerc.Caption := FloatToStrF(((FieldByName('QTD').AsInteger/FTotalFrequencia)* 100),ffNumber,15,1);
    end
    else
    begin
      QRLQtd.Caption := '-';
      QrlPerc.Caption := '-';
    end;
  end;

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

end;

procedure TFrmrelCurvaABCFreq.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  //QrlTotalPerc.Caption := FloatToStrF(((FTotalFrequencia/FTotalFrequencia)* 100),ffNumber,15,2);
end;

procedure TFrmrelCurvaABCFreq.QrlTotalPrint(sender: TObject;
  var Value: String);
begin
  inherited;
  QrlTotal.Caption := IntToStr(FTotalFrequencia);
end;

procedure TFrmrelCurvaABCFreq.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLParametros.Caption := 'Módulo: ' + QRLDesModulo.Caption + ' - Série: ' + QRLDescCurso.Caption + ' - Período: ' + QrlPeriodo.Caption +
  ' - Tipo: ' + QRLTipo.Caption;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmrelCurvaABCFreq]);

end.
