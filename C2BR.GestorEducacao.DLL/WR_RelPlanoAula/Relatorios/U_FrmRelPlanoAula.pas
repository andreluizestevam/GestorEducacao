unit U_FrmRelPlanoAula;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelPlanoAula = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    qrshape1: TQRShape;
    QRShape2: TQRShape;
    QrlTitCurso: TQRLabel;
    DetailBand1: TQRBand;
    QRDBText1: TQRDBText;
    QryRelatoriodt_prev_pla: TDateTimeField;
    QryRelatorioqt_carg_hora_pla: TBCDField;
    QryRelatoriono_cur: TStringField;
    QryRelatoriono_col: TStringField;
    QRLabel2: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLDataPlaAula: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QryRelatoriode_tema_aula: TStringField;
    QryRelatoriode_obje_aula: TStringField;
    QryRelatoriofla_executada_ativ: TBooleanField;
    QRDBText6: TQRDBText;
    QRLStatus: TQRLabel;
    QRLabel12: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioco_situ_pla: TStringField;
    QRBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel14: TQRLabel;
    QRLTotExe: TQRLabel;
    QRLabel15: TQRLabel;
    QRLTotCan: TQRLabel;
    QRLabel16: TQRLabel;
    QRLTotAbe: TQRLabel;
    QRLabel17: TQRLabel;
    QRLHora: TQRLabel;
    QRDBText8: TQRDBText;
    QryRelatorionu_temp_pla: TIntegerField;
    QryRelatoriono_turma: TStringField;
    QryRelatorioHR_INI_AULA_PLA: TStringField;
    QryRelatorioHR_FIM_AULA_PLA: TStringField;
    QRLParametros: TQRLabel;
    QryRelatoriono_materia: TStringField;
    QRLNoCol: TQRLabel;
    QRLTpAtiv: TQRLabel;
    QryRelatorioFLA_HOMOLOG: TStringField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelPlanoAula: TFrmRelPlanoAula;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelPlanoAula.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
//  QRLHora.Caption := '';
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';


  if not QryRelatorioFLA_HOMOLOG.IsNull then
  begin
    if QryRelatorioFLA_HOMOLOG.AsString = 'S' then
      QRLTpAtiv.Caption := 'Homologada'
    else
      QRLTpAtiv.Caption := 'Não Homologada';
  end
  else
    QRLTpAtiv.Caption := 'Homologação Indefinida';

  QRLDataPlaAula.Caption := FormatDateTime('dd/MM/yy',QryRelatoriodt_prev_pla.AsDateTime);

  if QryRelatoriofla_executada_ativ.AsBoolean then
  begin
    QRLStatus.Caption := 'Executada'; //+ #13 + FormatDateTime('dd/MM/yy',QryRelatoriodt_prev_pla.AsDateTime);
    QRLTotExe.Caption := IntToStr(StrToInt(QRLTotExe.Caption) + 1);
  end;

  if not(QryRelatoriofla_executada_ativ.AsBoolean) and (QryRelatorioco_situ_pla.AsString = 'A') then
  begin
    QRLStatus.Caption := 'Em Aberto';
    QRLTotAbe.Caption := IntToStr(StrToInt(QRLTotAbe.Caption) + 1);
  end;

  if not(QryRelatoriofla_executada_ativ.AsBoolean) and (QryRelatorioco_situ_pla.AsString = 'C') then
  begin
    QRLStatus.Caption := 'Cancelado';
    QRLTotCan.Caption := IntToStr(StrToInt(QRLTotCan.Caption) + 1);
  end;

  if not(QryRelatorioHR_INI_AULA_PLA.IsNull) and not(QryRelatorioHR_FIM_AULA_PLA.IsNull) then
  begin
    QRLHora.Caption := QryRelatorioHR_INI_AULA_PLA.AsString + ' / ' + QryRelatorioHR_FIM_AULA_PLA.AsString;
  end
  else
  begin
    QRLHora.Caption := ' - ';
  end;
     
  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelPlanoAula.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  QRLTotal.Caption := '0';
  QRLTotExe.Caption := '0';
  QRLTotCan.Caption := '0';
  QRLTotAbe.Caption := '0';
end;

end.
