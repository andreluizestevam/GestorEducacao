unit U_FrmRelExtratoFreqFunc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelExtratoFreqFunc = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_APEL_COL: TStringField;
    QryRelatorioDT_NASC_COL: TDateTimeField;
    QryRelatorioCO_SEXO_COL: TStringField;
    QryRelatorioNU_CPF_COL: TStringField;
    QryRelatorioCO_RG_COL: TStringField;
    QryRelatorioCO_EMIS_RG_COL: TStringField;
    QryRelatorioCO_ESTA_RG_COL: TStringField;
    QryRelatorioDT_EMIS_RG_COL: TDateTimeField;
    QryRelatorioDE_ENDE_COL: TStringField;
    QryRelatorioNU_ENDE_COL: TIntegerField;
    QryRelatorioDE_COMP_ENDE_COL: TStringField;
    QryRelatorioNO_BAIR_ENDE_COL: TStringField;
    QryRelatorioNO_CIDA_ENDE_COL: TStringField;
    QryRelatorioCO_ESTA_ENDE_COL: TStringField;
    QryRelatorioNU_CEP_ENDE_COL: TStringField;
    QryRelatorioNU_TELE_RESI_COL: TStringField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QryRelatorioCO_TPCAL: TIntegerField;
    QryRelatorioDT_CADA_COL: TDateTimeField;
    QryRelatorioDT_INIC_ATIV_COL: TDateTimeField;
    QryRelatorioDT_TERM_ATIV_COL: TDateTimeField;
    QryRelatorioCO_EMAI_COL: TStringField;
    QryRelatorioCO_WEB_COL: TStringField;
    QryRelatorioIM_FOTO_COL: TBlobField;
    QryRelatorioCO_FUN: TIntegerField;
    QryRelatorioCO_INST: TIntegerField;
    QryRelatorioCO_TPCON: TIntegerField;
    QryRelatorioCO_DEPTO: TIntegerField;
    QryRelatorioCO_ESPEC: TIntegerField;
    QryRelatorioCO_SITU_COL: TStringField;
    QryRelatorioDT_SITU_COL: TDateTimeField;
    QryRelatorioFLA_PROFESSOR: TStringField;
    QryRelatorioNU_TIT_ELE: TStringField;
    QryRelatorioNU_ZONA_ELE: TStringField;
    QryRelatorioNU_SEC_ELE: TStringField;
    QryRelatorioNO_FUN: TStringField;
    QRLTotMesP1: TQRLabel;
    QRLTotMesF1: TQRLabel;
    QRLTotMesP2: TQRLabel;
    QRLTotMesF2: TQRLabel;
    QRLTotMesP3: TQRLabel;
    QRLTotMesF3: TQRLabel;
    QRLTotMesP4: TQRLabel;
    QRLTotMesF4: TQRLabel;
    QRLTotMesP5: TQRLabel;
    QRLTotMesF5: TQRLabel;
    QRLTotMesP6: TQRLabel;
    QRLTotMesF6: TQRLabel;
    QRLTotMesP7: TQRLabel;
    QRLTotMesF7: TQRLabel;
    QRLTotMesP8: TQRLabel;
    QRLTotMesF8: TQRLabel;
    QRLTotMesP9: TQRLabel;
    QRLTotMesF9: TQRLabel;
    QRLTotMesP10: TQRLabel;
    QRLTotMesF10: TQRLabel;
    QRLTotMesP11: TQRLabel;
    QRLTotMesF11: TQRLabel;
    QRLTotMesP12: TQRLabel;
    QRLTotMesF12: TQRLabel;
    QRLTotalF: TQRLabel;
    QRLTotalP: TQRLabel;
    QRLabel31: TQRLabel;
    QRLPage: TQRLabel;
    QRBand2: TQRBand;
    QRLabel32: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel2: TQRLabel;
    QRLabel1: TQRLabel;
    QrlAno: TQRLabel;
    QRLabel13: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel17: TQRLabel;
    QRDBText4: TQRDBText;
    QRLabel16: TQRLabel;
    QRDBText5: TQRDBText;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape9: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape6: TQRShape;
    QRShape5: TQRShape;
    QRShape7: TQRShape;
    QRShape10: TQRShape;
    QRShape8: TQRShape;
    QRShape11: TQRShape;
    QRShape13: TQRShape;
    QRShape12: TQRShape;
    QRLabel3: TQRLabel;
    QrlMes1: TQRLabel;
    QrlMes2: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel18: TQRLabel;
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
    QRLNoCol: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa: String;
  end;

var
  FrmRelExtratoFreqFunc: TFrmRelExtratoFreqFunc;

implementation

uses U_DataModuleSGE, U_Funcoes;

{$R *.dfm}

procedure TFrmRelExtratoFreqFunc.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  I, Mes: Integer;
  TotFaltas, TotPresencas: Integer;
  TotFaltasAno, TotPresencasAno: Integer;
begin
  inherited;

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;

  TotFaltasAno := 0;
  TotPresencasAno := 0;

  for Mes := 1 to 12 do
  begin
    //with DataModuleSGE.QrySql do
    with DM.QrySql do
    begin
      { Recupera o total de presenças do mês }
      Close;
      Sql.Clear;
      Sql.Text := 'Select Count(*) TotPresencas ' +
                  'From TB109_FREQ_FUNC ' +
                  'Where CO_COL = ' + QryRelatorioCO_COL.AsString +
                  ' And CO_EMP = ' + codigoEmpresa +
                  ' And FLA_PRESENCA = ' + QuotedStr('S') +
                  ' And MONTH(DT_FREQ) = ' + IntToStr(Mes) +
                  ' And YEAR(DT_FREQ) = ' + QrlAno.Caption;
      Open;

      TotPresencas := FieldByName('TotPresencas').Value;
      TotPresencasAno := TotPresencasAno + TotPresencas;

      { Recupera o total de Faltas do mês }      
      Close;
      Sql.Clear;
      Sql.Text := 'Select Count(*) TotFaltas ' +
                  'From TB109_FREQ_FUNC ' +
                  'Where CO_COL = ' + QryRelatorioCO_COL.AsString +
                  ' And CO_EMP = ' + codigoEmpresa +
                  ' And FLA_PRESENCA = ' + QuotedStr('N') +
                  ' And MONTH(DT_FREQ) = ' + IntToStr(Mes) +
                  ' And YEAR(DT_FREQ) = ' + QrlAno.Caption;
      Open;

      TotFaltas := FieldByName('TotFaltas').Value;
      TotFaltasAno := TotFaltasAno + TotFaltas;
      Close;
    end;

    { Procura o label e atribui o valor }
    for I := 0 to ComponentCount - 1 do
    begin
      if Components[I] is TQRLabel then
      begin
        if (Components[I] as TQRLabel).Name = 'QRLTotMesP' + IntToStr(Mes) then
          (Components[I] as TQRLabel).Caption := IntToStr(TotPresencas);

        if (Components[I] as TQRLabel).Name = 'QRLTotMesF' + IntToStr(Mes) then
          (Components[I] as TQRLabel).Caption := IntToStr(TotFaltas);          
      end;
    end;

  end;

  QRLTotalP.Caption := IntToStr(TotPresencasAno);
  QRLTotalF.Caption := IntToStr(TotFaltasAno);

  if QRLTotalP.Caption = '0' then QRLTotalP.Caption := '-';
  if QRLTotalF.Caption = '0' then QRLTotalF.Caption := '-';

  if QRLTotMesP1.Caption = '0' then QRLTotMesP1.Caption := '-';
  if QRLTotMesF1.Caption = '0' then QRLTotMesF1.Caption := '-';

  if QRLTotMesP2.Caption = '0' then QRLTotMesP2.Caption := '-';
  if QRLTotMesF2.Caption = '0' then QRLTotMesF2.Caption := '-';

  if QRLTotMesP3.Caption = '0' then QRLTotMesP3.Caption := '-';
  if QRLTotMesF3.Caption = '0' then QRLTotMesF3.Caption := '-';

  if QRLTotMesP4.Caption = '0' then QRLTotMesP4.Caption := '-';
  if QRLTotMesF4.Caption = '0' then QRLTotMesF4.Caption := '-';

  if QRLTotMesP5.Caption = '0' then QRLTotMesP5.Caption := '-';
  if QRLTotMesF5.Caption = '0' then QRLTotMesF5.Caption := '-';

  if QRLTotMesP6.Caption = '0' then QRLTotMesP6.Caption := '-';
  if QRLTotMesF6.Caption = '0' then QRLTotMesF6.Caption := '-';

  if QRLTotMesP7.Caption = '0' then QRLTotMesP7.Caption := '-';
  if QRLTotMesF7.Caption = '0' then QRLTotMesF7.Caption := '-';

  if QRLTotMesP8.Caption = '0' then QRLTotMesP8.Caption := '-';
  if QRLTotMesF8.Caption = '0' then QRLTotMesF8.Caption := '-';

  if QRLTotMesP9.Caption = '0' then QRLTotMesP9.Caption := '-';
  if QRLTotMesF9.Caption = '0' then QRLTotMesF9.Caption := '-';

  if QRLTotMesP10.Caption = '0' then QRLTotMesP10.Caption := '-';
  if QRLTotMesF10.Caption = '0' then QRLTotMesF10.Caption := '-';

  if QRLTotMesP11.Caption = '0' then QRLTotMesP11.Caption := '-';
  if QRLTotMesF11.Caption = '0' then QRLTotMesF11.Caption := '-';

  if QRLTotMesP12.Caption = '0' then QRLTotMesP12.Caption := '-';
  if QRLTotMesF12.Caption = '0' then QRLTotMesF12.Caption := '-';

end;

procedure TFrmRelExtratoFreqFunc.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatoriono_col.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatoriono_col.AsString)
  else
    QRLNoCol.Caption := '-';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelExtratoFreqFunc]);

end.
