unit U_FrmRelMovimEstoque;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMovimEstoque = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRBand1: TQRBand;
    QRL_DES_PROD: TQRDBText;
    QRBand3: TQRBand;
    QRBand4: TQRBand;
    QRLabel9: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel6: TQRLabel;
    QrlTotalGrupo: TQRLabel;
    QRLabel5: TQRLabel;
    QRShape6: TQRShape;
    QRLabel10: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_MOV: TAutoIncField;
    QryRelatorioCO_PROD: TIntegerField;
    QryRelatorioCO_TIPO_MOV: TIntegerField;
    QryRelatorioDT_MOV_PROD: TDateTimeField;
    QryRelatorioNU_DOC_PROD: TStringField;
    QryRelatorioQT_MOV_PROD: TBCDField;
    QryRelatorioDE_MOV_PROD: TStringField;
    QryRelatorioNOM_USUARIO: TStringField;
    QryRelatorioDT_ALT_REGISTRO: TDateTimeField;
    QryRelatorioCO_GRUPO_ITEM: TIntegerField;
    QryRelatorioNO_GRUPO_ITEM: TStringField;
    QryRelatorioCO_SUBGRP_ITEM: TIntegerField;
    QryRelatorioCO_PROD_1: TAutoIncField;
    QryRelatorioCO_REFE_PROD: TStringField;
    QryRelatorioDES_PROD: TStringField;
    QryRelatorioQT_SALDO_EST: TBCDField;
    QRLabel2: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel7: TQRLabel;
    QRDBText6: TQRDBText;
    QryRelatorioNO_PROD_RED: TStringField;
    QryRelatorioDE_TIPO_MOV: TStringField;
    QRLabel8: TQRLabel;
    QRLTipo: TQRLabel;
    QryRelatorioFLA_TP_MOV: TStringField;
    QRLDtMov: TQRLabel;
    QRLQtdMov: TQRLabel;
    QRLQtdEstoq: TQRLabel;
    QRLabel17: TQRLabel;
    QrlPage: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMovimEstoque: TFrmRelMovimEstoque;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelMovimEstoque.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalGrupo.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelMovimEstoque.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalGrupo.Caption := '0';
end;

procedure TFrmRelMovimEstoque.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QRLQtdMov.Caption := FloatToStrF(QryRelatorioQT_MOV_PROD.AsFloat,ffNumber,15,0);
  QRLQtdEstoq.Caption := FloatToStrF(QryRelatorioQT_SALDO_EST.AsFloat,ffNumber,15,0);

  QRLDtMov.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorioDT_MOV_PROD.AsDateTime);
  if QryRelatorioFLA_TP_MOV.AsString = 'E' then
    QRLTipo.Caption := 'Entrada'
  else
    QRLTipo.Caption := 'Saída';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  QrlTotalGrupo.Caption := IntToStr(StrToInt(QrlTotalGrupo.Caption) +1);
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMovimEstoque]);

end.

