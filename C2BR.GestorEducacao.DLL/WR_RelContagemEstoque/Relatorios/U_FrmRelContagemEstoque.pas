unit U_FrmRelContagemEstoque;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelContagemEstoque = class(TFrmRelTemplate)
    QRLabel34: TQRLabel;
    QrlTipoProduto: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRGroup2: TQRGroup;
    QRDBNO_ALU: TQRDBText;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel13: TQRLabel;
    QRShape2: TQRShape;
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
    QRLabel11: TQRLabel;
    QRBand1: TQRBand;
    QRL_DES_PROD: TQRDBText;
    QRL_DES_COR: TQRDBText;
    QryRelatorioQT_SALDO_EST: TBCDField;
    QRLabel8: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QryRelatorioNO_PROD: TStringField;
    QryRelatorioCO_UNID_ITEM: TIntegerField;
    QryRelatorioNO_SIGLA: TStringField;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    ADOQuery1: TADOQuery;
    AutoIncField1: TAutoIncField;
    StringField1: TStringField;
    StringField2: TStringField;
    StringField3: TStringField;
    StringField4: TStringField;
    StringField5: TStringField;
    StringField6: TStringField;
    StringField7: TStringField;
    BlobField1: TBlobField;
    StringField8: TStringField;
    IntegerField1: TIntegerField;
    StringField9: TStringField;
    StringField10: TStringField;
    StringField11: TStringField;
    StringField12: TStringField;
    StringField13: TStringField;
    StringField14: TStringField;
    StringField15: TStringField;
    StringField16: TStringField;
    StringField17: TStringField;
    BCDField1: TBCDField;
    BCDField2: TBCDField;
    BCDField3: TBCDField;
    BCDField4: TBCDField;
    BCDField5: TBCDField;
    StringField18: TStringField;
    StringField19: TStringField;
    IntegerField2: TIntegerField;
    IntegerField3: TIntegerField;
    DateTimeField1: TDateTimeField;
    StringField20: TStringField;
    DateTimeField2: TDateTimeField;
    StringField21: TStringField;
    StringField22: TStringField;
    StringField23: TStringField;
    StringField24: TStringField;
    StringField25: TStringField;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    qrlCOD: TQRLabel;
    QRLUnidade: TQRLabel;
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
  FrmRelContagemEstoque: TFrmRelContagemEstoque;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelContagemEstoque.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
  QrlTotalGrupo.Caption := '0';
  QrlTotal.Caption := '0';
end;

procedure TFrmRelContagemEstoque.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalGrupo.Caption := '0';
end;

procedure TFrmRelContagemEstoque.QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  QrlTotalSubGrupo.Caption := '0';
end;

procedure TFrmRelContagemEstoque.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;
  (*
    Zebrar o relatório!
  *)

  if not QryRelatorioCO_REFE_PROD.IsNull then
  begin
    qrlCOD.Caption := FormatMaskText('##.###.####;0',QryRelatorioCO_REFE_PROD.AsString);
  end;

  if not QryRelatorioCO_UNID_ITEM.IsNull then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select sg_unidade from tb89_unidades '+
                  'where CO_UNID_ITEM = ' +  QryRelatorioCO_UNID_ITEM.AsString;
      Open;

      if not IsEmpty then
      begin
        QRLUnidade.Caption := DM.QrySql.fieldByName('sg_unidade').AsString;
      end
      else
        QRLUnidade.Caption := '-';
    end;
  end
  else
    QRLUnidade.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  QrlTotalSubGrupo.Caption := IntToStr(StrToInt(QrlTotalSubGrupo.Caption) + 1);
  QrlTotalGrupo.Caption := IntToStr(StrToInt(QrlTotalGrupo.Caption) + 1);
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + 1);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelContagemEstoque]);

end.

