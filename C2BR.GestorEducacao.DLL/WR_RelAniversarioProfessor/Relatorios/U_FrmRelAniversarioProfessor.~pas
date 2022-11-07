unit U_FrmRelAniversarioProfessor;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAniversarioProfessor = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLPage: TQRLabel;
    QRLabel7: TQRLabel;
    QryRelatoriono_col: TStringField;
    QryRelatorioco_mat_col: TStringField;
    QryRelatorioco_sexo_col: TStringField;
    QryRelatoriodt_nasc_col: TDateTimeField;
    QryRelatorionu_tele_resi_col: TStringField;
    QryRelatorionu_tele_celu_col: TStringField;
    QryRelatorioco_emai_col: TStringField;
    QryRelatoriono_fun: TStringField;
    QryRelatoriono_depto: TStringField;
    QryRelatoriosigla: TWideStringField;
    QRLDiaMes: TQRLabel;
    QRLIdade: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    SummaryBand1: TQRBand;
    QRLabel8: TQRLabel;
    QrlTotal: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel9: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QRShape2: TQRShape;
    QRShape1: TQRShape;
    QRLNuTeleResiCol: TQRLabel;
    QRLNuTeleCeluCol: TQRLabel;
    QRDBText3: TQRDBText;
    QRLNoCol: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAniversarioProfessor: TFrmRelAniversarioProfessor;

implementation

uses U_DataModuleSGE,DateUtils;

{$R *.dfm}

procedure TFrmRelAniversarioProfessor.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if not QryRelatoriono_col.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatoriono_col.AsString)
  else
    QRLNoCol.Caption := '-';

  diasnoano := 365.6;

  QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_COL').AsDateTime) / diasnoano));

  if not QryRelatoriodt_nasc_col.IsNull then
    QRLDiaMes.Caption := FormatDateTime('dd',QryRelatoriodt_nasc_col.AsDateTime) + '/' + FormatDateTime('MM',QryRelatoriodt_nasc_col.AsDateTime);

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  if not (QryRelatorionu_tele_resi_col.isnull) and (QryRelatorionu_tele_resi_col.AsString <> '') then
    QRLNuTeleResiCol.Caption := FormatFloat('(##) ####-####;0;_',QryRelatorionu_tele_resi_col.AsFloat)
  else
    QRLNuTeleResiCol.Caption := '-';

  if not (QryRelatorionu_tele_celu_col.isnull) and (QryRelatorionu_tele_celu_col.AsString <> '') then
    QRLNuTeleCeluCol.Caption := FormatFloat('(##) ####-####;0;_',QryRelatorionu_tele_celu_col.AsFloat)
  else
    QRLNuTeleCeluCol.Caption := '-';

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);

end;

procedure TFrmRelAniversarioProfessor.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';  
end;

end.
