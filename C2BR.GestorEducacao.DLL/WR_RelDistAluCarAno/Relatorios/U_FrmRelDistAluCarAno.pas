unit U_FrmRelDistAluCarAno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelDistAluCarAno = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRShape26: TQRShape;
    QRShape25: TQRShape;
    QRShape50: TQRShape;
    QRShape24: TQRShape;
    QRShape3: TQRShape;
    QRShape23: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape22: TQRShape;
    QRShape14: TQRShape;
    QRShape13: TQRShape;
    QRShape12: TQRShape;
    QRShape20: TQRShape;
    QRShape19: TQRShape;
    QRShape18: TQRShape;
    QRShape21: TQRShape;
    DetailBand1: TQRBand;
    QRBand2: TQRBand;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText28: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText27: TQRDBText;
    QRDBText26: TQRDBText;
    QRDBText25: TQRDBText;
    QRDBText24: TQRDBText;
    QRDBText23: TQRDBText;
    QRDBText22: TQRDBText;
    QRDBText21: TQRDBText;
    QRLabel33: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel31: TQRLabel;
    lblQtde: TQRLabel;
    lblBE: TQRLabel;
    lblSexoM: TQRLabel;
    lblSexoF: TQRLabel;
    lblTurnoM: TQRLabel;
    lblTurnoT: TQRLabel;
    lblTurnoN: TQRLabel;
    lblSd: TQRLabel;
    lblAu: TQRLabel;
    lblVi: TQRLabel;
    lblFi: TQRLabel;
    lblMe: TQRLabel;
    lblMp: TQRLabel;
    lblOt: TQRLabel;
    QRShape48: TQRShape;
    QRShape47: TQRShape;
    QRShape51: TQRShape;
    QRShape52: TQRShape;
    QRShape46: TQRShape;
    QRShape43: TQRShape;
    QRShape42: TQRShape;
    QRShape41: TQRShape;
    QRShape45: TQRShape;
    QRShape40: TQRShape;
    QRShape44: TQRShape;
    QRShape49: TQRShape;
    QRShape32: TQRShape;
    QRShape31: TQRShape;
    QRShape30: TQRShape;
    QRShape29: TQRShape;
    QRShape28: TQRShape;
    QRShape27: TQRShape;
    QRLPage: TQRLabel;
    QRLabel32: TQRLabel;
    QRShape53: TQRShape;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
    QRShape59: TQRShape;
    QRShape60: TQRShape;
    QRShape61: TQRShape;
    QRLabel11: TQRLabel;
    QRShape9: TQRShape;
    QRLabel12: TQRLabel;
    QRShape6: TQRShape;
    QRLabel17: TQRLabel;
    QRShape7: TQRShape;
    QRLabel16: TQRLabel;
    QRShape8: TQRShape;
    QRLabel15: TQRLabel;
    QRShape11: TQRShape;
    QRLabel14: TQRLabel;
    QRShape10: TQRShape;
    QRLabel13: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape17: TQRShape;
    QRLabel21: TQRLabel;
    QRShape16: TQRShape;
    QRLabel20: TQRLabel;
    QRShape15: TQRShape;
    QRLabel19: TQRLabel;
    QRShape62: TQRShape;
    QRLabel36: TQRLabel;
    QRShape63: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel18: TQRLabel;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText18: TQRDBText;
    QRDBText17: TQRDBText;
    QRDBText29: TQRDBText;
    QRDBText16: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText20: TQRDBText;
    QRDBText19: TQRDBText;
    QRDBText30: TQRDBText;
    lblBr: TQRLabel;
    lblPr: TQRLabel;
    lblAm: TQRLabel;
    lblPa: TQRLabel;
    lblId: TQRLabel;
    lblNIE: TQRLabel;
    lblR1: TQRLabel;
    lblR2: TQRLabel;
    lblR3: TQRLabel;
    lblR4: TQRLabel;
    lblR5: TQRLabel;
    lblR6: TQRLabel;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape64: TQRShape;
    QRShape65: TQRShape;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    TQTDE  : integer;
    TBe : integer;
    TSexoM,TSexoF : integer;
    TTurnoM,TTurnoT,TTurnoN : integer;
    TBr,TPr,TAm,TPa,TId,TNIE : integer;
    TR1,TR2,TR3,TR4,TR5,TR6 : integer;
    TSd,TAu,TVi,TFi,TMe,TMp,TOt : integer;
  end;

var
  FrmRelDistAluCarAno: TFrmRelDistAluCarAno;

implementation

uses U_DataModuleSGE;
{$R *.dfm}

procedure TFrmRelDistAluCarAno.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  TQTDE  := TQTDE + QryRelatorio.FieldByName('Quantidade').AsInteger;
  TSexoM := TSexoM + QryRelatorio.FieldByName('Homens').AsInteger;
  TSexoF := TSexoF + QryRelatorio.FieldByName('Mulheres').AsInteger;
  TTurnoM := TTurnoM + QryRelatorio.FieldByName('Manha').AsInteger;
  TTurnoT := TTurnoT + QryRelatorio.FieldByName('Tarde').AsInteger;
  TTurnoN := TTurnoN + QryRelatorio.FieldByName('Noite').AsInteger;
  TBr := TBr + QryRelatorio.FieldByName('Brancos').AsInteger;
  TPr := TPr + QryRelatorio.FieldByName('Pretos').AsInteger;
  TAm := TAm + QryRelatorio.FieldByName('Amarelos').AsInteger;
  TPa := TPa + QryRelatorio.FieldByName('Pardos').AsInteger;
  TId := TId + QryRelatorio.FieldByName('Indigena').AsInteger;
  TNIE := TNIE + QryRelatorio.FieldByName('Nao_Declarada').AsInteger;
  TR1 := TR1 + QryRelatorio.FieldByName('R1').AsInteger;
  TR2 := TR2 + QryRelatorio.FieldByName('R2').AsInteger;
  TR3 := TR3 + QryRelatorio.FieldByName('R3').AsInteger;
  TR4 := TR4 + QryRelatorio.FieldByName('R4').AsInteger;
  TR5 := TR5 + QryRelatorio.FieldByName('R5').AsInteger;
  TR6 := TR6 + QryRelatorio.FieldByName('R6').AsInteger;
  TSd := TSd + QryRelatorio.FieldByName('Def_Nenhuma').AsInteger;
  TAu := TAu + QryRelatorio.FieldByName('Def_Auditivo').AsInteger;
  TVi := TVi + QryRelatorio.FieldByName('Def_Visual').AsInteger;
  TFi := TFi + QryRelatorio.FieldByName('Def_Fisica').AsInteger;
  TMe := TMe + QryRelatorio.FieldByName('Def_Mental').AsInteger;
  TMp := TMp + QryRelatorio.FieldByName('Def_Multiplas').AsInteger;
  TOt := TOt + QryRelatorio.FieldByName('Def_Outras').AsInteger;
  TBE := TBE + QryRelatorio.FieldByName('Bolsa_Escola').AsInteger;
end;

procedure TFrmRelDistAluCarAno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  lblQtde.Caption := IntToStr(TQTDE);
  lblSexoM.Caption := IntToStr(TSexoM);
  lblSexoF.Caption := IntToStr(TSexoF);
  lblTurnoM.Caption := IntToStr(TTurnoM);
  lblTurnoT.Caption := IntToStr(TTurnoT);
  lblTurnoN.Caption := IntToStr(TTurnoN);
  lblBr.Caption := IntToStr(TBr);
  lblPr.Caption := IntToStr(TPr);
  lblAm.Caption := IntToStr(TAm);
  lblPa.Caption := IntToStr(TPa);
  lblId.Caption := IntToStr(TId);
  lblNIE.Caption := IntToStr(TNIE);
  lblR1.Caption := IntToStr(TR1);
  lblR2.Caption := IntToStr(TR2);
  lblR3.Caption := IntToStr(TR3);
  lblR4.Caption := IntToStr(TR4);
  lblR5.Caption := IntToStr(TR5);
  lblR6.Caption := IntToStr(TR6);
  lblSd.Caption := IntToStr(TSd);
  lblAu.Caption := IntToStr(TAu);
  lblVi.Caption := IntToStr(TVi);
  lblFi.Caption := IntToStr(TFi);
  lblMe.Caption := IntToStr(TMe);
  lblMp.Caption := IntToStr(TMp);
  lblOt.Caption := IntToStr(TOt);
  lblBE.Caption := IntToStr(TBe);
end;

procedure TFrmRelDistAluCarAno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  TQTDE  := 0;
  TSexoM := 0;
  TSexoF := 0;
  TTurnoM := 0;
  TTurnoT := 0;
  TTurnoN := 0;
  TBr := 0;
  TPr := 0;
  TAm := 0;
  TPa := 0;
  TId := 0;
  TNIE := 0;
  TR1 := 0;
  TR2 := 0;
  TR3 := 0;
  TR4 := 0;
  TR5 := 0;
  TR6 := 0;
  TSd := 0;
  TAu := 0;
  TVi := 0;
  TFi := 0;
  TMe := 0;
  TMp := 0;
  TOt := 0;
  TBe := 0;
end;

end.
