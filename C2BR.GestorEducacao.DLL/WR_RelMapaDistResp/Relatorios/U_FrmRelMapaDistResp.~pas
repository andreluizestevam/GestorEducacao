unit U_FrmRelMapaDistResp;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,U_DataModuleSGE;

type
  TFrmRelMapaDistResp = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QRGroup1: TQRGroup;
    QrlCurso: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
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
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRDBText1: TQRDBText;
    QRShape28: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
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
    QRShape45: TQRShape;
    QRLabel26: TQRLabel;
    QRDBText24: TQRDBText;
    QRShape46: TQRShape;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel29: TQRLabel;
    QRLTotResp: TQRLabel;
    QRLTotAlu: TQRLabel;
    QRLTotResMas: TQRLabel;
    QRLTotResFem: TQRLabel;
    QRLTotPais: TQRLabel;
    QRLTotTios: TQRLabel;
    QRLTotAvos: TQRLabel;
    QRLTotPrimos: TQRLabel;
    QRLTotCunhados: TQRLabel;
    QRLTotTutores: TQRLabel;
    QRLTotIrmaos: TQRLabel;
    QRLTotOutros: TQRLabel;
    QRLTotFund: TQRLabel;
    QRLTotMedio: TQRLabel;
    QRLTotGrad: TQRLabel;
    QRLTotEspec: TQRLabel;
    QRLTotPosGrad: TQRLabel;
    QRLTotMestr: TQRLabel;
    QRLTotDoutor: TQRLabel;
    QRLTotAlfab: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRLabel30: TQRLabel;
    QRDBText25: TQRDBText;
    QRShape36: TQRShape;
    QRShape44: TQRShape;
    QRShape47: TQRShape;
    QRShape48: TQRShape;
    QRShape49: TQRShape;
    QRShape50: TQRShape;
    QRShape51: TQRShape;
    QRShape52: TQRShape;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRLParam: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaDistResp: TFrmRelMapaDistResp;

implementation

{$R *.dfm}

procedure TFrmRelMapaDistResp.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  QRLTotResp.Caption := IntToStr(StrToInt(QRLTotResp.caption) + QryRelatorio.fieldByName('QTDRESP').AsInteger);
  QRLTotAlu.Caption := IntToStr(StrToInt(QRLTotAlu.caption) + QryRelatorio.fieldByName('QTDALU').AsInteger);
  QRLTotResMas.Caption := IntToStr(StrToInt(QRLTotResMas.caption) + QryRelatorio.fieldByName('MASCULINO').AsInteger);
  QRLTotResFem.Caption := IntToStr(StrToInt(QRLTotResFem.caption) + QryRelatorio.fieldByName('FEMININO').AsInteger);
  QRLTotPais.Caption := IntToStr(StrToInt(QRLTotPais.caption) + QryRelatorio.fieldByName('QTDPAIS').AsInteger);
  QRLTotTios.Caption := IntToStr(StrToInt(QRLTotTios.caption) + QryRelatorio.fieldByName('QTDTIOS').AsInteger);
  QRLTotAvos.Caption := IntToStr(StrToInt(QRLTotAvos.caption) + QryRelatorio.fieldByName('QTDAVOS').AsInteger);
  QRLTotPrimos.Caption := IntToStr(StrToInt(QRLTotPrimos.caption) + QryRelatorio.fieldByName('QTDPRIMOS').AsInteger);
  QRLTotCunhados.Caption := IntToStr(StrToInt(QRLTotCunhados.caption) + QryRelatorio.fieldByName('QTDCUNHADOS').AsInteger);
  QRLTotTutores.Caption := IntToStr(StrToInt(QRLTotTutores.caption) + QryRelatorio.fieldByName('QTDTUTORES').AsInteger);
  QRLTotIrmaos.Caption := IntToStr(StrToInt(QRLTotIrmaos.caption) + QryRelatorio.fieldByName('QTDIRMAOS').AsInteger);
  QRLTotOutros.Caption := IntToStr(StrToInt(QRLTotOutros.caption) + QryRelatorio.fieldByName('QTDOUTROS').AsInteger);
  QRLTotFund.Caption := IntToStr(StrToInt(QRLTotFund.caption) + QryRelatorio.fieldByName('QTDFUNDAMENTAL').AsInteger);
  QRLTotMedio.Caption := IntToStr(StrToInt(QRLTotMedio.caption) + QryRelatorio.fieldByName('QTDMEDIO').AsInteger);
  QRLTotGrad.Caption := IntToStr(StrToInt(QRLTotGrad.caption) + QryRelatorio.fieldByName('QTDGRADUACAO').AsInteger);
  QRLTotEspec.Caption := IntToStr(StrToInt(QRLTotEspec.caption) + QryRelatorio.fieldByName('QTDESPECIALIZACAO').AsInteger);
  QRLTotPosGrad.Caption := IntToStr(StrToInt(QRLTotPosGrad.caption) + QryRelatorio.fieldByName('QTDPOSGRADUACAO').AsInteger);
  QRLTotMestr.Caption := IntToStr(StrToInt(QRLTotMestr.caption) + QryRelatorio.fieldByName('QTDMESTRADO').AsInteger);
  QRLTotDoutor.Caption := IntToStr(StrToInt(QRLTotDoutor.caption) + QryRelatorio.fieldByName('QTDDOUTORADO').AsInteger);
  QRLTotAlfab.Caption := IntToStr(StrToInt(QRLTotAlfab.caption) + QryRelatorio.fieldByName('QTDALFABETIZADO').AsInteger);

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;
end;

procedure TFrmRelMapaDistResp.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotResp.Caption := '0';
  QRLTotAlu.Caption := '0';
  QRLTotResMas.Caption := '0';
  QRLTotResFem.Caption := '0';
  QRLTotPais.Caption := '0';
  QRLTotTios.Caption := '0';
  QRLTotAvos.Caption := '0';
  QRLTotPrimos.Caption := '0';
  QRLTotCunhados.Caption := '0';
  QRLTotTutores.Caption := '0';
  QRLTotIrmaos.Caption := '0';
  QRLTotOutros.Caption := '0';
  QRLTotFund.Caption := '0';
  QRLTotMedio.Caption := '0';
  QRLTotGrad.Caption := '0';
  QRLTotEspec.Caption := '0';
  QRLTotPosGrad.Caption := '0';
  QRLTotMestr.Caption := '0';
  QRLTotDoutor.Caption := '0';
  QRLTotAlfab.Caption := '0';
end;

procedure TFrmRelMapaDistResp.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotResp.Caption := '0';
  QRLTotAlu.Caption := '0';
  QRLTotResMas.Caption := '0';
  QRLTotResFem.Caption := '0';
  QRLTotPais.Caption := '0';
  QRLTotTios.Caption := '0';
  QRLTotAvos.Caption := '0';
  QRLTotPrimos.Caption := '0';
  QRLTotCunhados.Caption := '0';
  QRLTotTutores.Caption := '0';
  QRLTotIrmaos.Caption := '0';
  QRLTotOutros.Caption := '0';
  QRLTotFund.Caption := '0';
  QRLTotMedio.Caption := '0';
  QRLTotGrad.Caption := '0';
  QRLTotEspec.Caption := '0';
  QRLTotPosGrad.Caption := '0';
  QRLTotMestr.Caption := '0';
  QRLTotDoutor.Caption := '0';
  QRLTotAlfab.Caption := '0';
end;

procedure TFrmRelMapaDistResp.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlCurso.Caption := Sys_DescricaoTipoCurso;
end;

end.
