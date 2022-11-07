unit U_FrmRelPosicaoEstoque;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelPosicaoEstoque = class(TFrmRelTemplate)
    QRLabel34: TQRLabel;
    QrlTipoProduto: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRGroup2: TQRGroup;
    QRDBNO_ALU: TQRDBText;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel4: TQRLabel;
    QRBand2: TQRBand;
    QRBand3: TQRBand;
    QRBand4: TQRBand;
    QRLabel9: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel7: TQRLabel;
    QrlTotalSubGrupo: TQRLabel;
    QRLabel6: TQRLabel;
    QrlTotalGrupo: TQRLabel;
    QRLabel11: TQRLabel;
    QRBand1: TQRBand;
    QRL_DES_PROD: TQRDBText;
    QRL_DES_MARCA: TQRDBText;
    QRL_DES_TAMANHO: TQRDBText;
    QRL_DES_COR: TQRDBText;
    QRL_QT_SALDO_EST: TQRDBText;
    QryRelatorioCO_GRUPO_ITEM: TIntegerField;
    QryRelatorioNO_GRUPO_ITEM: TStringField;
    QryRelatorioCO_SUBGRP_ITEM: TIntegerField;
    QryRelatorioNO_SUBGRP_ITEM: TStringField;
    QryRelatorioCO_PROD: TIntegerField;
    QryRelatorioCO_REFE_PROD: TStringField;
    QryRelatorioDES_PROD: TStringField;
    QryRelatorioNO_PROD: TStringField;
    QryRelatorioSG_UNIDADE: TStringField;
    QryRelatorioDES_MARCA: TStringField;
    QryRelatorioDES_TAMANHO: TStringField;
    QryRelatorioNO_SIGLA: TStringField;
    QryRelatorioDES_COR: TStringField;
    QryRelatorioQT_SALDO_EST: TBCDField;
    QryRelatorioQT_RES_EST: TBCDField;
    QryRelatorioQT_EST_MIN: TBCDField;
    QryRelatorioQT_EST_SEG: TBCDField;
    QryRelatorioQT_EST_MAX: TBCDField;
    QRShape2: TQRShape;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    qrlCOD: TQRLabel;
    qrlQtdSaldo: TQRLabel;
    qrlPtEq: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelPosicaoEstoque: TFrmRelPosicaoEstoque;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelPosicaoEstoque.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
  QrlTotalGrupo.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelPosicaoEstoque.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalGrupo.Caption := '0';
end;

procedure TFrmRelPosicaoEstoque.QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
end;

procedure TFrmRelPosicaoEstoque.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  qrlCOD.Caption := FormatMaskText('##.###.####;0',QryRelatorioCO_REFE_PROD.AsString);

//  qrlQtdSaldo.Caption := FormatFloat('###,##', QryRelatorioQT_SALDO_EST.AsFloat - QryRelatorioQT_RES_EST.AsFloat);
  qrlQtdSaldo.Caption := FloatToStr(QryRelatorioQT_SALDO_EST.AsFloat - QryRelatorioQT_RES_EST.AsFloat);

  if StrToFloat(qrlQtdSaldo.Caption) <= QryRelatorioQT_EST_MIN.AsFloat then
  begin
    qrlPtEq.Caption := '( - )';
    qrlPtEq.Font.Color := clRed;
  end
  else
  begin
    if StrToFloat(qrlQtdSaldo.Caption) >= QryRelatorioQT_EST_MAX.AsFloat then
    begin
      qrlPtEq.Caption := '( + )';
      qrlPtEq.Font.Color := clRed;
    end;
  end;

  if ( StrToFloat(qrlQtdSaldo.Caption) > QryRelatorioQT_EST_MIN.AsFloat ) and
     (StrToFloat(qrlQtdSaldo.Caption) < QryRelatorioQT_EST_SEG.AsFloat) and
     (StrToFloat(qrlQtdSaldo.Caption) < QryRelatorioQT_EST_MAX.AsFloat) then
  begin
    qrlPtEq.Caption := '( P )';
    qrlPtEq.Font.Color := clGreen;
  end;

  if (StrToFloat(qrlQtdSaldo.Caption) < QryRelatorioQT_EST_MAX.AsFloat) and
     (StrToFloat(qrlQtdSaldo.Caption) >= QryRelatorioQT_EST_SEG.AsFloat) and
     (StrToFloat(qrlQtdSaldo.Caption) > QryRelatorioQT_EST_MIN.AsFloat) then
  begin
    qrlPtEq.Caption := '( N )';
    qrlPtEq.Font.Color := clBlack;
  end;

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  QrlTotalSubGrupo.Caption := IntToStr(StrToInt(QrlTotalSubGrupo.Caption) +1);
  QrlTotalGrupo.Caption := IntToStr(StrToInt(QrlTotalGrupo.Caption) +1);
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelPosicaoEstoque]);

end.

