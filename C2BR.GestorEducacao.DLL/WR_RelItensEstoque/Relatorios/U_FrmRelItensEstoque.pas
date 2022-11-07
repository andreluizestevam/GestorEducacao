unit U_FrmRelItensEstoque;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelItensEstoque = class(TFrmRelTemplate)
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
    QRLabel13: TQRLabel;
    QRBand1: TQRBand;
    QRL_DES_PROD: TQRDBText;
    QRShape6: TQRShape;
    QRL_SG_UNIDADE: TQRDBText;
    QRL_DES_MARCA: TQRDBText;
    QRL_DES_TAMANHO: TQRDBText;
    QRL_DES_COR: TQRDBText;
    QRLabel4: TQRLabel;
    QryRelatorioCO_GRUPO_ITEM: TIntegerField;
    QryRelatorioNO_GRUPO_ITEM: TStringField;
    QryRelatorioCO_SUBGRP_ITEM: TIntegerField;
    QryRelatorioNO_SUBGRP_ITEM: TStringField;
    QryRelatorioCO_PROD: TIntegerField;
    QryRelatorioDES_PROD: TStringField;
    QryRelatorioSG_UNIDADE: TStringField;
    QryRelatorioDES_MARCA: TStringField;
    QryRelatorioDES_TAMANHO: TStringField;
    QryRelatorioDES_COR: TStringField;
    QRBand2: TQRBand;
    QRBand3: TQRBand;
    QRBand4: TQRBand;
    QRLabel9: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel7: TQRLabel;
    QrlTotalSubGrupo: TQRLabel;
    QRLabel6: TQRLabel;
    QrlTotalGrupo: TQRLabel;
    QryRelatorioCO_REFE_PROD: TStringField;
    QryRelatorioNO_PROD: TStringField;
    QryRelatorioNO_SIGLA: TStringField;
    qrlCOD: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelItensEstoque: TFrmRelItensEstoque;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelItensEstoque.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
  QrlTotalGrupo.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelItensEstoque.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalGrupo.Caption := '0';
end;

procedure TFrmRelItensEstoque.QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
end;

procedure TFrmRelItensEstoque.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  qrlCOD.Caption := FormatMaskText('##.###.####;0',QryRelatorioCO_REFE_PROD.AsString);

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
  RegisterClasses([TFrmRelItensEstoque]);

end.

