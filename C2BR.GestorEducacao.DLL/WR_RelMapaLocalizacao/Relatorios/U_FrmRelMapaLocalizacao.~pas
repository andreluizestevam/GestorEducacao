unit U_FrmRelMapaLocalizacao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaLocalizacao = class(TFrmRelTemplate)
    QRLabel10: TQRLabel;
    QRLDescUnidade: TQRLabel;
    QRBand1: TQRBand;
    QRBand2: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText1: TQRDBText;
    QRLabel2: TQRLabel;
    QRDBText4: TQRDBText;
    QRLabel5: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_ESTA_ENDE_COL: TStringField;
    QryRelatorioNU_TELE_RESI_COL: TStringField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QryRelatorioCO_INST: TIntegerField;
    QryRelatorioNU_CARGA_HORARIA: TIntegerField;
    QryRelatorioSITUACAO: TStringField;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    qrlCidadeBairro: TQRLabel;
    qrlContato: TQRLabel;
    QRDBText5: TQRDBText;
    QRShape3: TQRShape;
    QryRelatorioNO_TPCON: TStringField;
    QryRelatorioSIGLA: TWideStringField;
    QRDBText6: TQRDBText;
    QRLabel11: TQRLabel;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorioNO_CIDADE: TStringField;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QryRelatorioCURFORM: TStringField;
    QryRelatorioESPECIALIZAO: TStringField;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    qryQTDEPROF: TADOQuery;
    qryQTDEPROFTOTALPROF: TIntegerField;
    QRDBText9: TQRDBText;
    QrlCurForm: TQRLabel;
    QrlEspec: TQRLabel;
    QryRelatorioco_espec: TIntegerField;
    QryRelatorioco_curform: TIntegerField;
    QryRelatoriotp_espec: TStringField;
    QRShape1: TQRShape;
    QRLNoCol: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : string;
  end;

var
  FrmRelMapaLocalizacao: TFrmRelMapaLocalizacao;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaLocalizacao.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QrlCurForm.Caption := '-';
  QrlEspec.Caption := '-';

  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';

  if not QryRelatorioco_curform.IsNull then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioco_curform.AsString;
      Open;
        if not IsEmpty then
          QrlCurForm.Caption := FieldByName('de_espec').AsString;
    end;
  end;

  {if not QryRelatorioco_espec.IsNull then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioco_espec.AsString;
      Open;
        if not IsEmpty then
          QrlEspec.Caption := FieldByName('de_espec').AsString;
    end;
  end;    }

  if not QryRelatorioESPECIALIZAO.IsNull then
    QrlEspec.Caption := QryRelatorioESPECIALIZAO.AsString
  else
    QrlEspec.Caption := '-';

  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

    qrlCidadeBairro.caption := '';

    if (not QryRelatorioNO_CIDADE.IsNull) and (not QryRelatorioNO_BAIRRO.IsNull) then
      qrlCidadeBairro.Caption := QryRelatorioNO_CIDADE.AsString + ' - ' + QryRelatorioNO_BAIRRO.AsString;

    if (not QryRelatorioNU_TELE_RESI_COL.IsNull) and (QryRelatorioNU_TELE_RESI_COL.AsString <> '') then
    begin
      qrlContato.Caption := FormatFloat('(##) ####-####', QryRelatorioNU_TELE_RESI_COL.AsFloat);
    end
    else
      qrlContato.Caption := '-';

end;

procedure TFrmRelMapaLocalizacao.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  with qryQTDEPROF do
  begin
  Close;
  Sql.Clear;
  Sql.Text := ' SELECT COUNT (*) as TOTALPROF ' +
                 ' FROM TB03_COLABOR C ' +
                 ' WHERE FLA_PROFESSOR = ' + QuotedStr('S') +
                 ' AND C.CO_EMP = ' + codigoEmpresa;
  Open;
  end;

end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaLocalizacao]);

end.
