unit U_FrmRelGerMapaFrequenciaProfessor;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelGerMapaFrequenciaProfessor = class(TFrmRelTemplate)
    QRLPage: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel1: TQRLabel;
    QRLPeriodo: TQRLabel;
    QRBand1: TQRBand;
    QRDBText3: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel5: TQRLabel;
    QRShape1: TQRShape;
    QRLabel7: TQRLabel;
    QRBand2: TQRBand;
    QRLabel8: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText4: TQRDBText;
    QrlMatriculaNome: TQRLabel;
    QrlHRFREQ: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel9: TQRLabel;
    QRLTotPre: TQRLabel;
    QRLTotFal: TQRLabel;
    QRLTotHor: TQRLabel;
    QryRelatoriodt_freq: TDateTimeField;
    QryRelatorioco_emp: TIntegerField;
    QryRelatoriono_col: TStringField;
    QryRelatorioco_mat_col: TStringField;
    QryRelatorioco_col: TAutoIncField;
    QryRelatorioFLA_PRESENCA: TStringField;
    QryRelatorioFREQUENCIA: TStringField;
    QryRelatoriono_fantas_emp: TStringField;
    QryRelatorioFINALIZADO: TStringField;
    QryRelatorioTP_FREQ: TStringField;
    QryRelatorioHR_FREQ: TIntegerField;
    QRLPreFal: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    TotMin : integer;
   // function fHourToMin(lvTime: String): Integer;
  public
    { Public declarations }
  end;

var
  FrmRelGerMapaFrequenciaProfessor: TFrmRelGerMapaFrequenciaProfessor;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

{function TFrmRelGerMapaFrequenciaProfessor.fHourToMin(lvTime: String): Integer;
var
  lvHour, lvMin, lvIdx : Integer;
begin
  if (lvTime[3] = ':') then
    lvIdx := 4
  else
    lvIdx := 3;
  lvHour := StrToInt(Copy(lvTime, 1     , 2));
  lvMin  := StrToInt(Copy(lvTime, lvIdx, 2));
  Result := ((lvHour * MinsPerHour) + lvMin);
end;  }

procedure TFrmRelGerMapaFrequenciaProfessor.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLPreFal.Caption := '';
  // HORAS ACUMULADAS
  {if (not QryRelatorioHR_ACUM.IsNull) and (QryRelatorioREG_AUTO.AsString = 'Não') then
  begin
    TotMin := TotMin + fHourToMin(FormatFloat('00:00',QryRelatorioHR_ACUM.AsFloat));
  end;   }

  // Total presença
  if (QryRelatorioTP_FREQ.AsString = 'E') and (QryRelatorioFLA_PRESENCA.AsString = 'S') then
  begin
    QRLTotPre.Caption := IntToStr(StrToInt(QRLTotPre.Caption) + 1);
    QRLPreFal.Caption := 'Presença';
  end;

  // HR FREQ
  if not QryRelatorioHR_FREQ.IsNull then
  begin
    QrlHRFREQ.Caption := FormatFloat('00:00', QryRelatorioHR_FREQ.AsFloat);
  end
  else
  begin
    QrlHRFREQ.Caption := ' - ';
  end;

  // Total falta
  if QryRelatorioFLA_PRESENCA.AsString = 'N' then
  begin
    QRLPreFal.Caption := 'Falta';
    QrlHRFREQ.Caption := ' - ';
    QRLTotFal.Caption := IntToStr(StrToInt(QRLTotFal.Caption) + 1);
  end;


  // HR ATIV
  {if not QryRelatorioHR_ATIV.IsNull then
  begin
    QrlHRATIV.Caption := FormatMaskText('00:00;0', QryRelatorioHR_ATIV.AsString);
  end
  else
  begin
    QrlHRATIV.Caption := ' - ';
  end;      }

  If QRBand1.Color = clWhite Then
    QRBand1.Color := $00D8D8D8
  Else
    QRBand1.Color := clWhite;
end;

procedure TFrmRelGerMapaFrequenciaProfessor.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlMatriculaNome.Caption := UpperCase(QryRelatorioNO_COL.AsString) + ' (Matrícula: ' + FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString) + ')';
end;

procedure TFrmRelGerMapaFrequenciaProfessor.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotPre.Caption := '0';
  QRLTotFal.Caption := '0';
  QRLTotHor.Caption := '0';
  TotMin := 0;
end;

procedure TFrmRelGerMapaFrequenciaProfessor.QRBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
//var
 // Horas,Minutos : Word;
begin
  inherited;
  //Horas := Trunc( TotMin ) Div 60;
  //Minutos := Trunc( TotMin ) Mod 60;
  //QRLTotHor.Caption := FormatFloat( '00', Horas ) + ':' + FormatFloat( '00', Minutos );
end;

end.
