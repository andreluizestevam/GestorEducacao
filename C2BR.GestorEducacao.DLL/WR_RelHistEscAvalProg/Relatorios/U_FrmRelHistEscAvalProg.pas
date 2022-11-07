unit U_FrmRelHistEscAvalProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls, QrAngLbl;

type
  TFrmRelHistEscAvalProg = class(TFrmRelTemplate)
    QRCompositeReport1: TQRCompositeReport;
    QryRelatorioCO_ALU: TAutoIncField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioNO_PAI_ALU: TStringField;
    QryRelatorioNO_MAE_ALU: TStringField;
    QryRelatorioDES_OBS_ALU: TMemoField;
    QryRelatorioNO_FANTAS_EMP: TStringField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioCO_SIGL_REFER: TStringField;
    QryRelatorioQT_CARG_HORA_CUR: TIntegerField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioCO_UF_EMP: TStringField;
    QRLabel89: TQRLabel;
    QRLabel90: TQRLabel;
    QRLabel91: TQRLabel;
    QRShape71: TQRShape;
    QRShape72: TQRShape;
    QRLabel92: TQRLabel;
    QrlNoAluP2: TQRLabel;
    QRShape73: TQRShape;
    QRShape74: TQRShape;
    QRLabel93: TQRLabel;
    QrlDTnascP2: TQRLabel;
    QRShape75: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QRShape78: TQRShape;
    QRShape79: TQRShape;
    QRShape80: TQRShape;
    QRLabel94: TQRLabel;
    QRShape81: TQRShape;
    QRLabel95: TQRLabel;
    QrlNoPaiP2: TQRLabel;
    QRShape82: TQRShape;
    QrlNoMaeP2: TQRLabel;
    QRLabel96: TQRLabel;
    QRLabel97: TQRLabel;
    QrlObsP2: TQRLabel;
    QRShape83: TQRShape;
    QRShape84: TQRShape;
    QRLabel41: TQRLabel;
    QrlNoEmpresa: TQRLabel;
    QRLabel42: TQRLabel;
    QrlNoAlu: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape42: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape5: TQRShape;
    QRLabel13: TQRLabel;
    QRShape7: TQRShape;
    QRLabel2: TQRLabel;
    QRShape18: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRShape32: TQRShape;
    QRShape12: TQRShape;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRShape8: TQRShape;
    QRShape39: TQRShape;
    QRShape38: TQRShape;
    QRShape17: TQRShape;
    QRShape37: TQRShape;
    QRShape15: TQRShape;
    QRShape14: TQRShape;
    QRShape13: TQRShape;
    QRShape11: TQRShape;
    QRShape10: TQRShape;
    QRShape9: TQRShape;
    QRShape6: TQRShape;
    QrlTotCHA: TQRLabel;
    QrlAno: TQRLabel;
    QRLabel59: TQRLabel;
    QRShape41: TQRShape;
    QRLabel43: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel45: TQRLabel;
    QRShape60: TQRShape;
    QRShape59: TQRShape;
    QRLabel52: TQRLabel;
    QRLabel60: TQRLabel;
    QRLabel62: TQRLabel;
    QRShape4: TQRShape;
    QRLabel61: TQRLabel;
    QRLabel63: TQRLabel;
    QRLabel65: TQRLabel;
    QRLabel64: TQRLabel;
    QRLabel67: TQRLabel;
    QRLabel66: TQRLabel;
    QRLabel88: TQRLabel;
    QRLabel87: TQRLabel;
    QRLabel86: TQRLabel;
    QRLabel85: TQRLabel;
    QRLabel84: TQRLabel;
    QRLabel83: TQRLabel;
    QRLabel82: TQRLabel;
    QRLabel81: TQRLabel;
    QRLabel80: TQRLabel;
    QRLabel79: TQRLabel;
    QRLabel78: TQRLabel;
    QRLabel77: TQRLabel;
    QRLabel55: TQRLabel;
    QRLabel54: TQRLabel;
    QRLabel53: TQRLabel;
    QRShape43: TQRShape;
    QRShape61: TQRShape;
    QRLabel47: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel51: TQRLabel;
    QRLabel50: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel75: TQRLabel;
    QRLabel76: TQRLabel;
    QRLabel74: TQRLabel;
    QRLabel73: TQRLabel;
    QRLabel72: TQRLabel;
    QRLabel71: TQRLabel;
    QRLabel70: TQRLabel;
    QRLabel69: TQRLabel;
    QRLabel68: TQRLabel;
    QRShape45: TQRShape;
    QRShape62: TQRShape;
    QRShape47: TQRShape;
    QRShape63: TQRShape;
    QRShape49: TQRShape;
    QRShape40: TQRShape;
    QRLabel58: TQRLabel;
    QRLabel57: TQRLabel;
    QRLabel56: TQRLabel;
    QRShape57: TQRShape;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRShape19: TQRShape;
    QRLabel30: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel35: TQRLabel;
    QrlCserie: TQRLabel;
    QrlCciclo: TQRLabel;
    QrlCano: TQRLabel;
    QrlCempresa: TQRLabel;
    QrlCcidade: TQRLabel;
    QrlCuf: TQRLabel;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape25: TQRShape;
    QRShape24: TQRShape;
    QRShape31: TQRShape;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRShape33: TQRShape;
    QRLabel39: TQRLabel;
    QRShape35: TQRShape;
    QRLabel40: TQRLabel;
    QRShape34: TQRShape;
    QRLabel1: TQRLabel;
    QRShape16: TQRShape;
    QRShape3: TQRShape;
    QRShape44: TQRShape;
    QRShape46: TQRShape;
    QRShape58: TQRShape;
    QRShape48: TQRShape;
    QRShape50: TQRShape;
    QRShape70: TQRShape;
    QRShape69: TQRShape;
    QRShape67: TQRShape;
    QRShape65: TQRShape;
    QRShape36: TQRShape;
    QRShape56: TQRShape;
    QRShape54: TQRShape;
    QRShape52: TQRShape;
    QRShape20: TQRShape;
    QRShape51: TQRShape;
    QRShape53: TQRShape;
    QRShape55: TQRShape;
    procedure QRCompositeReport1AddReports(Sender: TObject);
    procedure QuickRep1EndPage(Sender: TCustomQuickRep);
    procedure QRSubDetail1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelHistEscAvalProg: TFrmRelHistEscAvalProg;

implementation

{$R *.dfm}

uses U_DataModuleSGE, DateUtils, U_FrmRelHistEscAvalProgP2, U_FrmRelHistEscAvalProgP3,
  U_FrmRelHistEscAvalProgP4;

procedure TFrmRelHistEscAvalProg.QRCompositeReport1AddReports(
  Sender: TObject);
begin
  inherited;
   {
  with QRCompositeReport1 do
  begin
    Reports.Add(FrmRelHistEscAvalProg.QuickRep1);
    Reports.Add(FrmRelHistEscAvalProgP2.QuickRep2);
    Reports.Add(FrmRelHistEscAvalProgP3.QuickRep3);
    Reports.Add(FrmRelHistEscAvalProgP4.QuickRep4);
  end;    }
end;

procedure TFrmRelHistEscAvalProg.QuickRep1EndPage(Sender: TCustomQuickRep);
begin
  inherited;
    //QuickRep1.NewPage;
end;

procedure TFrmRelHistEscAvalProg.QRSubDetail1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlNoAluP2.Caption := QryRelatorioNO_ALU.AsString;
  QrlDTnascP2.Caption := QryRelatorioDT_NASC_ALU.AsString;
  QrlNoPaiP2.Caption := QryRelatorioNO_PAI_ALU.AsString;
  QrlNoMaeP2.Caption := QryRelatorioNO_MAE_ALU.AsString;
end;

end.
