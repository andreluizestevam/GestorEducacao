unit U_FrmRelUnidEscNuc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QuickRpt, DB, ADODB, QRCtrls, ExtCtrls;

type
  TFrmRelUnidEscNuc = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRShape2: TQRShape;
    QRLabel10: TQRLabel;
    QryRelatorioNU_INEP: TIntegerField;
    QryRelatorioCO_CPFCGC_EMP: TStringField;
    QryRelatorioSIGLA: TWideStringField;
    QryRelatorioNO_FANTAS_EMP: TStringField;
    QryRelatorioDE_NUCLEO: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioCO_TEL1_EMP: TStringField;
    QrlNuINEP: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QrlDiretor: TQRLabel;
    QrlTel1: TQRLabel;
    QrlCoordNucleo: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    SummaryBand1: TQRBand;
    QRLabel11: TQRLabel;
    QrlTotal: TQRLabel;
    QrlNucleo: TQRLabel;
    QryRelatorioCO_EMP: TAutoIncField;
    QryRelatorioCO_DIR: TIntegerField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelUnidEscNuc: TFrmRelUnidEscNuc;

implementation

{$R *.dfm}
uses U_DataModuleSGE, MaskUtils;

procedure TFrmRelUnidEscNuc.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  // ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  // INEP
  if not QryRelatorioNU_INEP.IsNull then
  begin
    QrlNuINEP.Caption := QryRelatorioNU_INEP.AsString;
  end
  else
  begin
    QrlNuINEP.Alignment := taCenter;
    QrlNuINEP.Caption := ' - ';
  end;

// NOME DO DIRETOR(A)
  if not QryRelatorioCO_DIR.IsNull then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select no_col from tb03_colabor '+
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_col = ' + QryRelatorioCO_DIR.AsString;
      Open;

      if not IsEmpty then
        QrlDiretor.Caption := FieldByName('no_col').AsString;
    end;
  end
  else
  begin
    QrlDiretor.Caption := ' - ';
  end;

  // TELEFONE
  if not QryRelatorioCO_TEL1_EMP.IsNull then
  begin
    QrlTel1.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorioCO_TEL1_EMP.AsString);
  end
  else
  begin
    QrlTel1.Alignment := taCenter;
    QrlTel1.Caption := ' - ';
  end;

  // COORDENADOR (ainda está sendo definida a seleção do coordenador - Diego Nobre 27/03/2009)
  QrlCoordNucleo.Caption := ' - ';

  // TOTAL DE REGISTROS
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);
end;

procedure TFrmRelUnidEscNuc.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelUnidEscNuc.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorioDE_NUCLEO.IsNull then
  begin
    QrlNucleo.Caption := QryRelatorioDE_NUCLEO.AsString;
  end
  else
  begin
    QrlNucleo.Caption := '000 - Unidades Sem Registro de Núcleo';
  end;

end;

end.
