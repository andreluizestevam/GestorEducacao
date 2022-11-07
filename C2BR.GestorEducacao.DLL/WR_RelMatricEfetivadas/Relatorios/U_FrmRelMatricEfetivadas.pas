unit U_FrmRelMatricEfetivadas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMatricEfetivadas = class(TFrmRelTemplate)
    QRShape3: TQRShape;
    QrlPeriodo: TQRLabel;
    QrlSerie: TQRLabel;
    QRBand2: TQRBand;
    QrlTotal: TQRLabel;
    QRBand1: TQRBand;
    QRDBText1: TQRDBText;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioTotal: TIntegerField;
    QRLabel2: TQRLabel;
    QRShape2: TQRShape;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRLResTotal: TQRLabel;
    QRLAno3: TQRLabel;
    QRLAno4: TQRLabel;
    QRLAno5: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLRAno3: TQRLabel;
    QRLRAno4: TQRLabel;
    QRLRAno5: TQRLabel;
    QRLPAno3: TQRLabel;
    QRLPAno4: TQRLabel;
    QRLPAno5: TQRLabel;
    qryAno: TADOQuery;
    QRLabel1: TQRLabel;
    QRLDescModulo: TQRLabel;
    QryRelatorioCO_CUR: TIntegerField;
    QRLTotAno3: TQRLabel;
    QRLTotAno4: TQRLabel;
    QRLTotAno5: TQRLabel;
    QRLAno2: TQRLabel;
    QRShape1: TQRShape;
    QRLAno1: TQRLabel;
    QRShape11: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRLRAno2: TQRLabel;
    QRLRAno1: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPAno2: TQRLabel;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRLabel13: TQRLabel;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRLPAno1: TQRLabel;
    QRLTotAno2: TQRLabel;
    QRLTotAno1: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    matTotal : integer;
    codigoEmpresa : String;
  end;

var
  FrmRelMatricEfetivadas: TFrmRelMatricEfetivadas;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMatricEfetivadas.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
//  QRLabel1.Caption := Sys_DescricaoTipoCurso;
end;

procedure TFrmRelMatricEfetivadas.QRDBText2Print(sender: TObject;var Value: String);
begin
  inherited;
  //QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption)+StrToInt(Value));
end;

procedure TFrmRelMatricEfetivadas.QuickRep1BeforePrint(Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  matTotal := 0;
  QrlTotal.Caption := '0';
  QRLTotAno1.Caption := '0';
  QRLTotAno2.Caption := '0';
  QRLTotAno3.Caption := '0';
  QRLTotAno4.Caption := '0';
  QRLTotAno5.Caption := '0';
end;

procedure TFrmRelMatricEfetivadas.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
Var
  matResTotal : integer;
  pt1,pt2,pt3,pt4,pt5 : Double;
begin
  inherited;
  matResTotal := 0;
  //matTotal := 0;

  if QryRelatorioTotal.AsInteger <> 0 then
  begin
    QRLRAno5.Caption := QryRelatorioTotal.AsString;
    QRLTotAno5.Caption := IntToStr(StrToInt(QRLTotAno5.Caption) + QryRelatorioTotal.AsInteger);
    matResTotal := matResTotal + QryRelatorioTotal.AsInteger;
  end;

  with qryAno do
  begin
    //Pegar Primeiro Ano
    Close;
    SQL.Clear;
    SQL.Text := 'SELECT COUNT(A.CO_ALU) TOTAL'+
              ' FROM TB08_MATRCUR A, TB01_CURSO B'+
              ' WHERE A.CO_EMP = B.CO_EMP'+
              ' AND   A.CO_CUR = B.CO_CUR'+
              ' AND A.CO_MODU_CUR = B.CO_MODU_CUR'+
              ' AND   A.CO_EMP = ' + codigoEmpresa +
              ' AND A.CO_ANO_MES_MAT = ' + QRLAno1.Caption +
              ' AND A.CO_SIT_MAT NOT IN ( '+ quotedStr('C') + ')' +
              ' AND A.CO_CUR = ' + QryRelatorioCO_CUR.AsString;
    Open;

    if not IsEmpty then
    begin
      QRLRAno1.Caption := FieldByName('TOTAL').AsString;
      QRLTotAno1.Caption := IntToStr(StrToInt(QRLTotAno1.Caption) + FieldByName('TOTAL').AsInteger);
      matResTotal := matResTotal + FieldByName('TOTAL').AsInteger;
    end
    else
      QRLRAno1.Caption := '0';
    //

    //Pegar Segundo Ano
    Close;
    SQL.Clear;
    SQL.Text := 'SELECT COUNT(A.CO_ALU) TOTAL'+
              ' FROM TB08_MATRCUR A, TB01_CURSO B'+
              ' WHERE A.CO_EMP = B.CO_EMP'+
              ' AND   A.CO_CUR = B.CO_CUR'+
              ' AND A.CO_MODU_CUR = B.CO_MODU_CUR'+
              ' AND   A.CO_EMP = ' + codigoEmpresa +
              ' AND A.CO_ANO_MES_MAT = ' + QRLAno2.Caption +
              ' AND A.CO_SIT_MAT NOT IN ( '+ quotedStr('C') + ')' +
              ' AND A.CO_CUR = ' + QryRelatorioCO_CUR.AsString;
    Open;

    if not IsEmpty then
    begin
      QRLRAno2.Caption := FieldByName('TOTAL').AsString;
      QRLTotAno2.Caption := IntToStr(StrToInt(QRLTotAno2.Caption) + FieldByName('TOTAL').AsInteger);
      matResTotal := matResTotal + FieldByName('TOTAL').AsInteger;
    end
    else
      QRLRAno2.Caption := '0';
    //

    //Pegar Terceiro Ano
    Close;
    SQL.Clear;
    SQL.Text := 'SELECT COUNT(A.CO_ALU) TOTAL'+
              ' FROM TB08_MATRCUR A, TB01_CURSO B'+
              ' WHERE A.CO_EMP = B.CO_EMP'+
              ' AND   A.CO_CUR = B.CO_CUR'+
              ' AND A.CO_MODU_CUR = B.CO_MODU_CUR'+
              ' AND   A.CO_EMP = ' + codigoEmpresa +
              ' AND A.CO_ANO_MES_MAT = ' + QRLAno3.Caption +
              ' AND A.CO_SIT_MAT NOT IN ( '+ quotedStr('C') + ')' +
              ' AND A.CO_CUR = ' + QryRelatorioCO_CUR.AsString;
    Open;

    if not IsEmpty then
    begin
      QRLRAno3.Caption := FieldByName('TOTAL').AsString;
      QRLTotAno3.Caption := IntToStr(StrToInt(QRLTotAno3.Caption) + FieldByName('TOTAL').AsInteger);
      matResTotal := matResTotal + FieldByName('TOTAL').AsInteger;
    end
    else
      QRLRAno3.Caption := '0';
    //

    //Pegar Quarto Ano
    Close;
    SQL.Clear;
    SQL.Text := 'SELECT COUNT(A.CO_ALU) TOTAL'+
              ' FROM TB08_MATRCUR A, TB01_CURSO B'+
              ' WHERE A.CO_EMP = B.CO_EMP'+
              ' AND   A.CO_CUR = B.CO_CUR'+
              ' AND A.CO_MODU_CUR = B.CO_MODU_CUR'+
              ' AND   A.CO_EMP = ' + codigoEmpresa +
              ' AND A.CO_ANO_MES_MAT = ' + QRLAno4.Caption +
              ' AND A.CO_SIT_MAT NOT IN ( '+ quotedStr('C') + ')' +
              ' AND A.CO_CUR = ' + QryRelatorioCO_CUR.AsString;
    Open;

    if not IsEmpty then
    begin
      QRLRAno4.Caption := FieldByName('TOTAL').AsString;
      QRLTotAno4.Caption := IntToStr(StrToInt(QRLTotAno4.Caption) + FieldByName('TOTAL').AsInteger);
      matResTotal := matResTotal + FieldByName('TOTAL').AsInteger;
    end
    else
      QRLRAno4.Caption := '0';
    //
  end;

  if matResTotal <> 0 then
  begin
    QRLResTotal.Caption := IntToStr(matResTotal);
    matTotal := matTotal + matResTotal;
  end
  else
    QRLResTotal.Caption := '0';

  //Calcular a porcentagem
  if matResTotal > 0 then
  begin
    pt1 := 100 * StrToInt(QRLRAno1.Caption)/matResTotal;
    QRLPAno1.Caption := FormatFloat('0.0',pt1);
    pt2 := 100 * StrToInt(QRLRAno2.Caption)/matResTotal;
    QRLPAno2.Caption := FormatFloat('0.0',pt2);
    pt3 := 100 * StrToInt(QRLRAno3.Caption)/matResTotal;
    QRLPAno3.Caption := FormatFloat('0.0',pt3);
    pt4 := 100 * StrToInt(QRLRAno4.Caption)/matResTotal;
    QRLPAno4.Caption := FormatFloat('0.0',pt4);
    pt5 := 100 * StrToInt(QRLRAno5.Caption)/matResTotal;
    QRLPAno5.Caption := FormatFloat('0.0',pt5);
  end;
  //

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;
end;

procedure TFrmRelMatricEfetivadas.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlTotal.Caption := IntToStr(matTotal);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMatricEfetivadas]);

end.
