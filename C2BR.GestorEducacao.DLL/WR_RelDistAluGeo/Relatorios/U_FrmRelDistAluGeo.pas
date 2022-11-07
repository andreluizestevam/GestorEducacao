unit U_FrmRelDistAluGeo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDistAluGeo = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRDBText7: TQRDBText;
    QRGroup1: TQRGroup;
    QRLabel22: TQRLabel;
    QRShape1: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBBairro: TQRDBText;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLIdade: TQRLabel;
    QRLabel1: TQRLabel;
    QRLDeficiencia: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel9: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLSerTur: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    SummaryBand1: TQRBand;
    QRLTotMas: TQRLabel;
    QRLTotFem: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLNuNis: TQRLabel;
    QRLCidade: TQRLabel;
    QrlTelFixo: TQRLabel;
    QrlTelMovel: TQRLabel;
    QRLTotDef: TQRLabel;
    QRLTotalCompleto: TQRLabel;
    QryRelatorioco_alu: TAutoIncField;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_nis: TBCDField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatorioNU_TELE_RESI_ALU: TStringField;
    QryRelatorioTP_DEF: TStringField;
    QryRelatorioNU_TELE_CELU_ALU: TStringField;
    QryRelatoriono_bairro: TStringField;
    QryRelatoriocoduf: TStringField;
    QryRelatoriono_cidade: TStringField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioDE_ENDE_ALU: TStringField;
    QryRelatorioNU_ENDE_ALU: TIntegerField;
    QryRelatorioDE_COMP_ALU: TStringField;
    QryRelatoriono_resp: TStringField;
    QryRelatorionu_tele_celu_resp: TStringField;
    QryRelatorioco_bairro: TIntegerField;
    QryRelatorioco_emp: TIntegerField;
    QRLabel2: TQRLabel;
    QRLTpRelatorio: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    tpRelatorio : String;
  end;

var
  FrmRelDistAluGeo: TFrmRelDistAluGeo;

implementation

Uses U_DataModuleSGE,U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelDistAluGeo.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  if tpRelatorio <> 'N' then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select top 1 mm.co_alu_cad, c.co_sigl_cur, ct.co_sigla_turma from tb08_matrcur mm ' +
                  'join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp ' +
                  'join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_cur = t.co_cur and mm.co_emp = t.co_emp ' +
                  'join tb129_cadturmas ct on t.co_tur = ct.co_tur and t.co_emp = ct.co_emp ' +
                  ' where mm.co_emp = ' + QryRelatorio.FieldByName('co_emp').AsString +
                  ' and mm.co_alu = ' + QryRelatorio.FieldByName('co_alu').AsString +
                  ' order by mm.co_ano_mes_mat desc';
      Open;

      if not IsEmpty then
      begin
        if (not FieldByName('co_sigl_cur').IsNull) and (not FieldByName('co_sigla_turma').IsNull) then
          QRLSerTur.Caption :=  FieldByName('co_sigl_cur').AsString + ' / ' + FieldByName('co_sigla_turma').AsString
        else
          QRLSerTur.Caption := '-';

        if not FieldByName('co_alu_cad').IsNull then
          QRLMatricula.Caption := FormatMaskText('00.000.0000.000;0',FieldByName('co_alu_cad').AsString)
        else
          QRLMatricula.Caption := '-';
      end
      else
      begin
        QRLSerTur.Caption := '-';
        QRLMatricula.Caption := '-';
      end;
    end;
  end
  else
  begin
    QRLSerTur.Caption := '-';
    QRLMatricula.Caption := '-';
  end;

  if (not QryRelatorioNU_TELE_RESI_ALU.IsNull) and (QryRelatorioNU_TELE_RESI_ALU.AsString <> '') then
  begin
    QrlTelFixo.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_RESI_ALU').AsString);
  end
  else
  begin
    QrlTelFixo.Caption := '-';
  end;

  if (not QryRelatorioNU_TELE_CELU_ALU.IsNull) and (QryRelatorioNU_TELE_CELU_ALU.AsString <> '') then
  begin
    QrlTelMovel.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_ALU').AsString);
  end
  else
  begin
    QrlTelMovel.Caption := '-';
  end;

  QRLNuNis.Caption := QryRelatorioNU_NIS.AsString;

  QRLEndereco.Caption := '';
  QRLDeficiencia.Caption := '-';

  if QryRelatorio.FieldByName('TP_DEF').AsString = 'N' then
    QRLDeficiencia.Caption := 'Nenhuma';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'A' then
    QRLDeficiencia.Caption := 'Auditiva';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'V' then
    QRLDeficiencia.Caption := 'Visual';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'F' then
    QRLDeficiencia.Caption := 'Física';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'M' then
    QRLDeficiencia.Caption := 'Mental';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'I' then
    QRLDeficiencia.Caption := 'Múltiplas';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'O' then
    QRLDeficiencia.Caption := 'Outras';

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';

  if not QryRelatorio.FieldByName('DE_ENDE_ALU').IsNull and (QryRelatorio.FieldByName('DE_ENDE_ALU').AsString <> '')  then
    QRLEndereco.Caption := QryRelatorio.FieldByName('DE_ENDE_ALU').AsString;

  if not QryRelatorio.FieldByName('NU_ENDE_ALU').IsNull and (QryRelatorio.FieldByName('NU_ENDE_ALU').AsString <> '') then
    QRLEndereco.Caption := QRLEndereco.Caption + ', Nº ' + QryRelatorio.FieldByName('NU_ENDE_ALU').AsString;

  if not QryRelatorio.FieldByName('DE_COMP_ALU').IsNull and (QryRelatorio.FieldByName('DE_COMP_ALU').AsString <> '') then
    QRLEndereco.Caption := QRLEndereco.Caption + ',' + QryRelatorio.FieldByName('DE_COMP_ALU').AsString;

  if QryRelatorio.FieldByName('co_sexo_alu').AsString = 'M' then
    QRLTotMas.Caption := IntToStr(StrToInt(QRLTotMas.Caption) + 1)
  else
  if QryRelatorio.FieldByName('co_sexo_alu').AsString = 'F' then
    QRLTotFem.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + 1);

  if QryRelatorio.FieldByName('TP_DEF').AsString <> 'N' then
    QRLTotDef.Caption := IntToStr(StrToInt(QRLTotDef.Caption) +1);

  QRLTotGeral.Caption := IntToStr(StrToInt(QRLTotFem.Caption) + StrToInt(QRLTotMas.Caption));

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelDistAluGeo.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
  QRLTotDef.Caption := '0';
end;

procedure TFrmRelDistAluGeo.SummaryBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotMas.Caption := '0';
  QRLTotFem.Caption := '0';
  QRLTotGeral.Caption := '0';
  QRLTotDef.Caption := '0';
end;

procedure TFrmRelDistAluGeo.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotalCompleto.Caption := 'Total de Alunos: ' + FormatFloat('0000', StrToFloat(QRLTotGeral.Caption)) + ' (Masculino: ' + FormatFloat('0000', StrToFloat(QRLTotMas.Caption)) + ' - Feminino: ' + FormatFloat('0000', StrToFloat(QRLTotFem.Caption)) + ' - Deficientes: ' + FormatFloat('0000', StrToFloat(QRLTotDef.Caption)) + ')';
end;

procedure TFrmRelDistAluGeo.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLCidade.Caption := 'Cidade/UF: ' + QryRelatoriono_cidade.AsString + ' / ' + QryRelatoriocoduf.AsString;
end;

procedure TFrmRelDistAluGeo.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if tpRelatorio = 'M' then
    QRLTpRelatorio.Caption := 'Matriculados'
  else if tpRelatorio = 'N' then
    QRLTpRelatorio.Caption := 'Não Matriculados'
  else
    QRLTpRelatorio.Caption := 'Todos';

end;

end.
