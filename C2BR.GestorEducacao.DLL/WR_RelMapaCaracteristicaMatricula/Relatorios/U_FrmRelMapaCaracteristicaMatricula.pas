unit U_FrmRelMapaCaracteristicaMatricula;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelMapaCaracteristicaMatricula = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel18: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel2: TQRLabel;
    QRLDescSer: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape14: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText16: TQRDBText;
    QRDBText17: TQRDBText;
    QRDBText18: TQRDBText;
    QRDBText19: TQRDBText;
    QRDBText20: TQRDBText;
    QRDBText21: TQRDBText;
    QRDBText22: TQRDBText;
    QRDBText23: TQRDBText;
    QRDBText24: TQRDBText;
    QRDBText25: TQRDBText;
    QRDBText26: TQRDBText;
    QRDBText27: TQRDBText;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRShape45: TQRShape;
    QRShape46: TQRShape;
    QRShape47: TQRShape;
    QRShape48: TQRShape;
    QRShape49: TQRShape;
    QRBand2: TQRBand;
    QRLabel31: TQRLabel;
    lblSexoM: TQRLabel;
    lblSexoF: TQRLabel;
    QRShape50: TQRShape;
    QRShape51: TQRShape;
    QRDBText28: TQRDBText;
    lblQtde: TQRLabel;
    lblBE: TQRLabel;
    QRShape52: TQRShape;
    lblTurnoT: TQRLabel;
    lblTurnoM: TQRLabel;
    lblTurnoN: TQRLabel;
    lblNd: TQRLabel;
    lblPa: TQRLabel;
    lblId: TQRLabel;
    lblAm: TQRLabel;
    lblPr: TQRLabel;
    lblBr: TQRLabel;
    lblMp: TQRLabel;
    lblMe: TQRLabel;
    lblFi: TQRLabel;
    lblVi: TQRLabel;
    lblAu: TQRLabel;
    lblSd: TQRLabel;
    lblOt: TQRLabel;
    lblR4: TQRLabel;
    lblR3: TQRLabel;
    lblR2: TQRLabel;
    lblR1: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel34: TQRLabel;
    QRShape53: TQRShape;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape59: TQRShape;
    QRShape62: TQRShape;
    QRShape68: TQRShape;
    QRShape72: TQRShape;
    QRLParam: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
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
    TBr,TPr,TAm,TPa,TId,TNd : integer;
    TR1,TR2,TR3,TR4 : integer;
    TSd,TAu,TVi,TFi,TMe,TMp,TOt : integer;
  end;

var
  FrmRelMapaCaracteristicaMatricula: TFrmRelMapaCaracteristicaMatricula;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaCaracteristicaMatricula.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;

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
  TNd := TNd + QryRelatorio.FieldByName('Nao_Declarada').AsInteger;
  TR1 := TR1 + QryRelatorio.FieldByName('R1').AsInteger;
  TR2 := TR2 + QryRelatorio.FieldByName('R2').AsInteger;
  TR3 := TR3 + QryRelatorio.FieldByName('R3').AsInteger;
  TR4 := TR4 + QryRelatorio.FieldByName('R4').AsInteger;
  TSd := TSd + QryRelatorio.FieldByName('Def_Nenhuma').AsInteger;
  TAu := TAu + QryRelatorio.FieldByName('Def_Auditivo').AsInteger;
  TVi := TVi + QryRelatorio.FieldByName('Def_Visual').AsInteger;
  TFi := TFi + QryRelatorio.FieldByName('Def_Fisica').AsInteger;
  TMe := TMe + QryRelatorio.FieldByName('Def_Mental').AsInteger;
  TMp := TMp + QryRelatorio.FieldByName('Def_Multiplas').AsInteger;
  TOt := TOt + QryRelatorio.FieldByName('Def_Outras').AsInteger;
  TBE := TBE + QryRelatorio.FieldByName('Bolsa_Escola').AsInteger;
end;

procedure TFrmRelMapaCaracteristicaMatricula.QRBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
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
  lblNd.Caption := IntToStr(TNd);
  lblR1.Caption := IntToStr(TR1);
  lblR2.Caption := IntToStr(TR2);
  lblR3.Caption := IntToStr(TR3);
  lblR4.Caption := IntToStr(TR4);
  lblSd.Caption := IntToStr(TSd);
  lblAu.Caption := IntToStr(TAu);
  lblVi.Caption := IntToStr(TVi);
  lblFi.Caption := IntToStr(TFi);
  lblMe.Caption := IntToStr(TMe);
  lblMp.Caption := IntToStr(TMp);
  lblOt.Caption := IntToStr(TOt);
  lblBE.Caption := IntToStr(TBe);
end;

procedure TFrmRelMapaCaracteristicaMatricula.QuickRep1BeforePrint(
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
  TNd := 0;
  TR1 := 0;
  TR2 := 0;
  TR3 := 0;
  TR4 := 0;
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
