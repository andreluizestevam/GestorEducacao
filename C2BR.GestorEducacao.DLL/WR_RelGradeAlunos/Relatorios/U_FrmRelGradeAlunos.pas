unit U_FrmRelGradeAlunos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QuickRpt, DB, ADODB, QRCtrls, ExtCtrls;

type
  TFrmRelGradeAlunos = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRDBText4: TQRDBText;
    QRShape1: TQRShape;
    QRLabel11: TQRLabel;
    QRLNumPage: TQRLabel;
    QRLabel6: TQRLabel;
    QRDBText5: TQRDBText;
    QRLParametros: TQRLabel;
    QRLabel9: TQRLabel;
    QrlMatricula: TQRLabel;
    QrlIdade: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QrlCidadeBairro: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatoriono_cidade: TStringField;
    QryRelatoriono_bairro: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatorioco_tur: TAutoIncField;
    QryRelatorioco_cur: TAutoIncField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_resi_resp: TStringField;
    QryRelatorioDEFICIENCIA: TStringField;
    QryRelatorioPARENTESCO: TStringField;
    QRDBText1: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText6: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel10: TQRLabel;
    QrlTotal: TQRLabel;
    QRLNuNIS: TQRLabel;
    QryRelatorionu_nis: TBCDField;
    QryRelatoriono_tur: TStringField;
    QRBand1: TQRBand;
    QrlTotalSerieTurma: TQRLabel;
    QRLabel7: TQRLabel;
    QRLNoAlu: TQRLabel;
    QryRelatoriode_modu_cur: TStringField;
    QryRelatorioco_ano_mes_mat: TStringField;
    QRLParam: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelGradeAlunos: TFrmRelGradeAlunos;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelGradeAlunos.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  QRLNuNIS.caption := QryRelatorionu_nis.AsString;

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioco_alu_cad.AsString);
  QrlCidadeBairro.Caption := QryRelatoriono_cidade.AsString + '/' + QryRelatoriono_bairro.AsString;

  diasnoano := 365.6;
  QrlIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));

  QrlTotalSerieTurma.Caption := IntToStr(StrToInt(QrlTotalSerieTurma.Caption) + 1);
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + 1);

end;

procedure TFrmRelGradeAlunos.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelGradeAlunos.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLParametros.Caption := 'S�rie: ' + QryRelatoriono_cur.AsString + ' - Turma: ' + QryRelatoriono_tur.AsString;
end;

procedure TFrmRelGradeAlunos.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QrlTotalSerieTurma.Caption := '0';
end;

end.
