unit U_FrmRelUnidEsc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelUnidEsc = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QryRelatorioNU_INEP: TIntegerField;
    QryRelatorioCO_CPFCGC_EMP: TStringField;
    QryRelatorioNO_FANTAS_EMP: TStringField;
    QryRelatorioDE_NUCLEO: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioNO_CLAS: TStringField;
    QryRelatorioCO_TEL1_EMP: TStringField;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QryRelatorioSIGLA: TWideStringField;
    QRDBText4: TQRDBText;
    QRDBText6: TQRDBText;
    QrlTel1: TQRLabel;
    QrlClas: TQRLabel;
    QrlNucleo: TQRLabel;
    QrlNuINEP: TQRLabel;
    QrlDiretor: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel10: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel11: TQRLabel;
    QrlTotal: TQRLabel;
    QryRelatorioCO_EMP: TAutoIncField;
    QryRelatorioCO_DIR: TIntegerField;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRShape2: TQRShape;
    QRLabel12: TQRLabel;
    QRLTpUnidade: TQRLabel;
    QryRelatorioNO_TIPOEMP: TStringField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelUnidEsc: TFrmRelUnidEsc;

implementation

{$R *.dfm}
uses U_DataModuleSGE, MaskUtils;

procedure TFrmRelUnidEsc.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
// ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

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

//CLASSFICIAÇÃO
  if not QryRelatorioNO_CLAS.IsNull then
  begin
    QrlClas.Caption := QryRelatorioNO_CLAS.AsString;
  end
  else
  begin
    QrlClas.Alignment := taCenter;
    QrlClas.Caption := ' - ';
  end;

//NUCLEOS
  if not QryRelatorioDE_NUCLEO.IsNull then
  begin
    QrlNucleo.Caption := QryRelatorioDE_NUCLEO.AsString;
  end
  else
    QrlNucleo.Caption := ' - ';
  begin
  end;

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

// TOTAL DE UNIDADES ESCOLARES
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + 1);
end;

procedure TFrmRelUnidEsc.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelUnidEsc.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTpUnidade.Caption := QryRelatorioNO_TIPOEMP.AsString;
end;

procedure TFrmRelUnidEsc.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

end.
