unit U_FrmRelMapaAnualFaltas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaAnualFaltas = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape1: TQRShape;
    qryFaltas: TADOQuery;
    QRLJan: TQRLabel;
    QRLFev: TQRLabel;
    QRLMar: TQRLabel;
    QRLAbr: TQRLabel;
    QRLMai: TQRLabel;
    QRLJun: TQRLabel;
    QRLJul: TQRLabel;
    QRLAgo: TQRLabel;
    QRLSet: TQRLabel;
    QRLOut: TQRLabel;
    QRLNov: TQRLabel;
    QRLDez: TQRLabel;
    QRLabel15: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel16: TQRLabel;
    QRLPage: TQRLabel;
    QRBand1: TQRBand;
    QRLabel17: TQRLabel;
    QRLTotFaltas: TQRLabel;
    QRLTotJan: TQRLabel;
    QRLTotFev: TQRLabel;
    QRLTotMar: TQRLabel;
    QRLTotAbr: TQRLabel;
    QRLTotMai: TQRLabel;
    QRLTotJun: TQRLabel;
    QRLTotJul: TQRLabel;
    QRLTotAgo: TQRLabel;
    QRLTotSet: TQRLabel;
    QRLTotOut: TQRLabel;
    QRLTotNov: TQRLabel;
    QRLTotDez: TQRLabel;
    QRLParametros: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    AnoCurso,codigoEmpresa : String;
    co_materia : Integer;
  end;

var
  FrmRelMapaAnualFaltas: TFrmRelMapaAnualFaltas;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaAnualFaltas.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
// cria��o das vari�veis para os meses e o total;
  mes,ano,dia : word;
  ctJan,ctFev,ctMar,ctAbr,ctMai,ctJun,ctJul,ctAgo,ctSet,ctOut,ctNov,ctDez : integer;
  Total : integer;
begin
// define as vari�veis como inteiros;
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  ctJan := 0;
  ctFev := 0;
  ctMar := 0;
  ctAbr := 0;
  ctMai := 0;
  ctJun := 0;
  ctJul := 0;
  ctAgo := 0;
  ctSet := 0;
  ctOut := 0;
  ctNov := 0;
  ctDez := 0;
  Total := 0;

  with qryFaltas do
  begin
    close;
    // SQL que define o tratamento dos campos do banco de dados;
    if QryRelatorio.FieldByName('co_param_freq_tipo').AsString = 'M' then
    begin
      SQL.Text := 'SELECT f.dt_fre FROM TB132_FREQ_ALU f ' +
                'JOIN tb02_materia m ON m.co_mat = f.co_mat '+
                'JOIN tb01_curso c ON c.co_cur = f.co_cur '+
                'JOIN tb07_aluno a ON a.co_alu = f.co_alu where ' +
                'f.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and year(f.dt_fre) = ' + quotedStr(AnoCurso) +
                ' and f.CO_FLAG_FREQ_ALUNO = '+ QuotedStr('N') +
                ' and f.co_emp_alu = ' + codigoEmpresa +
                ' and f.CO_MAT = ' + IntToStr(co_materia);
    end
    else
    begin
      SQL.Text := 'SELECT f.dt_fre FROM TB132_FREQ_ALU f ' +
                'JOIN tb01_curso c ON c.co_cur = f.co_cur and c.co_emp = f.co_emp '+
                'JOIN tb07_aluno a ON a.co_alu = f.co_alu where ' +
                'f.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and year(f.dt_fre) = ' + quotedStr(AnoCurso) +
                ' and f.CO_FLAG_FREQ_ALUNO = '+ QuotedStr('N') +
                ' and f.co_cur = ' + QryRelatorio.FieldByName('co_cur').AsString +
                ' and f.co_mat is null ' +
                ' and f.co_emp_alu = ' + codigoEmpresa;
    end;
    Open;

    while not qryFaltas.Eof do
    begin
    // "DecodeDate" seleciona os campos da data de acordo com as vari�veis
    // definidas depois do "Value";
    DecodeDate(fieldByName('dt_fre').Value,ano,mes,dia);
      if ( mes = 1) then
      begin
        ctJan := ctJan + 1;
      end;
      if ( mes = 2) then
      begin
        ctFev := ctFev + 1;
      end;
      if ( mes = 3) then
      begin
        ctMar := ctMar + 1;
      end;
      if ( mes = 4) then
      begin
        ctAbr := ctAbr + 1;
      end;
      if ( mes = 5) then
      begin
        ctMai := ctMai + 1;
      end;
      if ( mes = 6) then
      begin
        ctJun := ctJun + 1;
      end;
      if ( mes = 7) then
      begin
        ctJul := ctJul + 1;
      end;
      if ( mes = 8) then
      begin
        ctAgo := ctAgo + 1;
      end;
      if ( mes = 9) then
      begin
        ctSet := ctSet + 1;
      end;
      if ( mes = 10) then
      begin
        ctOut := ctOut + 1;
      end;
      if ( mes = 11) then
      begin
        ctNov := ctNov + 1;
      end;
      if ( mes = 12) then
      begin
        ctDez := ctDez + 1;
      end;
    Next;
    end;
  end;

  Total := ctJan + ctFev + ctMar + ctAbr + ctMai + ctJun + ctJul + ctAgo + ctSet + ctOut + ctNov + ctDez;
  QRLJan.Caption := IntToStr(ctJan);
  QRLFev.Caption := IntToStr(ctFev);
  QRLMar.Caption := IntToStr(ctMar);
  QRLAbr.Caption := IntToStr(ctAbr);
  QRLMai.Caption := IntToStr(ctMai);
  QRLJun.Caption := IntToStr(ctJun);
  QRLJul.Caption := IntToStr(ctJul);
  QRLAgo.Caption := IntToStr(ctAgo);
  QRLSet.Caption := IntToStr(ctSet);
  QRLOut.Caption := IntToStr(ctOut);
  QRLNov.Caption := IntToStr(ctNov);
  QRLDez.Caption := IntToStr(ctDez);
  QRLTotal.Caption := IntToStr(Total);

  //Escrever os totais de Faltas
  QRLTotJan.Caption := IntToStr(StrToInt(QRLTotJan.Caption) + ctJan);
  QRLTotFev.Caption := IntToStr(StrToInt(QRLTotFev.Caption) + ctFev);
  QRLTotMar.Caption := IntToStr(StrToInt(QRLTotMar.Caption) + ctMar);
  QRLTotAbr.Caption := IntToStr(StrToInt(QRLTotAbr.Caption) + ctAbr);
  QRLTotMai.Caption := IntToStr(StrToInt(QRLTotMai.Caption) + ctMai);
  QRLTotJun.Caption := IntToStr(StrToInt(QRLTotJun.Caption) + ctJun);
  QRLTotJul.Caption := IntToStr(StrToInt(QRLTotJul.Caption) + ctJul);
  QRLTotAgo.Caption := IntToStr(StrToInt(QRLTotAgo.Caption) + ctAgo);
  QRLTotSet.Caption := IntToStr(StrToInt(QRLTotSet.Caption) + ctSet);
  QRLTotOut.Caption := IntToStr(StrToInt(QRLTotOut.Caption) + ctOut);
  QRLTotNov.Caption := IntToStr(StrToInt(QRLTotNov.Caption) + ctNov);
  QRLTotDez.Caption := IntToStr(StrToInt(QRLTotDez.Caption) + ctDez);
  QRLTotFaltas.Caption := IntToStr(StrToInt(QRLTotFaltas.Caption) + Total);
  ////

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelMapaAnualFaltas.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotFaltas.Caption := '0';
  QRLTotJan.Caption := '0';
  QRLTotFev.Caption := '0';
  QRLTotMar.Caption := '0';
  QRLTotAbr.Caption := '0';
  QRLTotMai.Caption := '0';
  QRLTotJun.Caption := '0';
  QRLTotJul.Caption := '0';
  QRLTotAgo.Caption := '0';
  QRLTotSet.Caption := '0';
  QRLTotOut.Caption := '0';
  QRLTotNov.Caption := '0';
  QRLTotDez.Caption := '0';
end;

procedure TFrmRelMapaAnualFaltas.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotFaltas.Caption := '0';
  QRLTotJan.Caption := '0';
  QRLTotFev.Caption := '0';
  QRLTotMar.Caption := '0';
  QRLTotAbr.Caption := '0';
  QRLTotMai.Caption := '0';
  QRLTotJun.Caption := '0';
  QRLTotJul.Caption := '0';
  QRLTotAgo.Caption := '0';
  QRLTotSet.Caption := '0';
  QRLTotOut.Caption := '0';
  QRLTotNov.Caption := '0';
  QRLTotDez.Caption := '0';
end;

end.