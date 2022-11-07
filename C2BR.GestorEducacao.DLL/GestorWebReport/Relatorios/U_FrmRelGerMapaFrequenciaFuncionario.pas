unit U_FrmRelGerMapaFrequenciaFuncionario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls, Mask, DBCtrls;

type
  TFrmRelGerMapaFrequenciaFuncionario = class(TFrmRelTemplate)
    QRLabel10: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel1: TQRLabel;
    QRLNomeFunc: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel2: TQRLabel;
    QRBand1: TQRBand;
    QRDBText3: TQRDBText;
    DBEdit1: TDBEdit;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape5: TQRShape;
    QRLabel11: TQRLabel;
    QRShape6: TQRShape;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRShape7: TQRShape;
    QRLabel14: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape12: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRLPeriodo: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText9: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel17: TQRLabel;
    QRLTotPre: TQRLabel;
    QRLDescRes: TQRLabel;
    QRLTotFal: TQRLabel;
    QRLEnt1: TQRLabel;
    QRLSai1: TQRLabel;
    QRLEnt2: TQRLabel;
    QRLSai2: TQRLabel;
    QRLEnt3: TQRLabel;
    QRLSai3: TQRLabel;
    QRLDiaSemana: TQRLabel;
    QryRelatoriodt_freq: TDateTimeField;
    QryRelatorioco_emp: TIntegerField;
    QryRelatoriono_col: TStringField;
    QryRelatorioco_mat_col: TStringField;
    QryRelatorioco_col: TAutoIncField;
    QryRelatorioFLA_PRESENCA: TStringField;
    QryRelatorioFREQUENCIA: TStringField;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    function DiaSemana(Data:TDateTime): String;
  public
    { Public declarations }
  end;

var
  FrmRelGerMapaFrequenciaFuncionario: TFrmRelGerMapaFrequenciaFuncionario;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

function TFrmRelGerMapaFrequenciaFuncionario.DiaSemana(Data:TDateTime): String;
{Retorna dia da semana}
var
  NoDia : Integer;
  DiaDaSemana : array [1..7] of String[13];
begin
{ Dias da Semana }
  DiaDasemana [1]:= 'Domingo';
  DiaDasemana [2]:= 'Segunda-feira';
  DiaDasemana [3]:= 'Terça-feira';
  DiaDasemana [4]:= 'Quarta-feira';
  DiaDasemana [5]:= 'Quinta-feira';
  DiaDasemana [6]:= 'Sexta-feira';
  DiaDasemana [7]:= 'Sábado';
  NoDia:=DayOfWeek(Data);
  DiaSemana:=DiaDasemana[NoDia];
end;


procedure TFrmRelGerMapaFrequenciaFuncionario.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  e,s : integer;
begin
  inherited;
  QRLEnt1.Caption := '-';
  QRLEnt2.Caption := '-';
  QRLEnt3.Caption := '-';
  QRLSai1.Caption := '-';
  QRLSai2.Caption := '-';
  QRLSai3.Caption := '-';
  e := 1;
  s := 1;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'SET LANGUAGE PORTUGUESE select hr_freq,tp_freq from tb199_freq_func ' +
                'where dt_freq = ' + QuotedStr(QryRelatorioDT_FREQ.AsString) +
                ' and co_emp = ' + QryRelatorioCO_EMP.AsString +
                ' and co_col = ' + QryRelatorioCO_COL.AsString;
    Open;

    while not Eof do
    begin

      if FieldByName('tp_freq').AsString = 'E' then
      begin
        case e of
          1  : QRLEnt1.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
          2  : QRLEnt2.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
          3  : QRLEnt3.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
        end;
        e := e + 1;
      end;

      if FieldByName('tp_freq').AsString = 'S' then
      begin
        case s of
          1  : QRLSai1.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
          2  : QRLSai2.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
          3  : QRLSai3.Caption  := FormatFloat('00:00',FieldByName('HR_FREQ').AsInteger);
        end;
        s := s + 1;
      end;

      Next;
    end;
  end;

  // Total presença
  if QryRelatorioFLA_PRESENCA.AsString = 'S' then
  begin
    QRLTotPre.Caption := IntToStr(StrToInt(QRLTotPre.Caption) + 1);
  end;

  // Total falta
  if QryRelatorioFLA_PRESENCA.AsString = 'N' then
  begin
    QRLEnt1.Caption := '-';
    QRLEnt2.Caption := '-';
    QRLEnt3.Caption := '-';
    QRLSai1.Caption := '-';
    QRLSai2.Caption := '-';
    QRLSai3.Caption := '-';
    QRLTotFal.Caption := IntToStr(StrToInt(QRLTotFal.Caption) + 1);
  end;

  QRLDiaSemana.Caption := DiaSemana(QryRelatorioDT_FREQ.AsDateTime);

  QRLNomeFunc.Caption := UpperCase(QryRelatorioNO_COL.AsString) + ' (Matrícula: ' + FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString) + ')';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelGerMapaFrequenciaFuncionario.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotPre.Caption := '0';
  QRLTotFal.Caption := '0';
end;

procedure TFrmRelGerMapaFrequenciaFuncionario.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLDescRes.Caption := IntToStr(StrToInt(QRLTotPre.Caption) + StrToInt(QRLTotFal.Caption)) + ' Dias - ' +
  QRLTotPre.Caption + ' Presenças - ' + QRLTotFal.Caption + ' Faltas'; 
end;

end.
