unit U_FrmRelFicCadIndRes;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFicCadIndRes = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel3: TQRLabel;
    QRLSexResp: TQRLabel;
    QRShape2: TQRShape;
    QRLabel5: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRDBText4: TQRDBText;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRShape4: TQRShape;
    QRLabel13: TQRLabel;
    QRDBText6: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText7: TQRDBText;
    QRLabel15: TQRLabel;
    QRLabel17: TQRLabel;
    QRDBText8: TQRDBText;
    QRShape5: TQRShape;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText9: TQRDBText;
    QRShape6: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRDBText10: TQRDBText;
    QRLabel23: TQRLabel;
    QRDBText11: TQRDBText;
    QRLabel24: TQRLabel;
    QRLabel26: TQRLabel;
    QRShape7: TQRShape;
    QRLabel27: TQRLabel;
    QRShape8: TQRShape;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel34: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLNuNis: TQRLabel;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRLSerTur: TQRLabel;
    QRLDeficiencia: TQRLabel;
    QRDBText17: TQRDBText;
    QRLabel35: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLCidUF: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText5: TQRDBText;
    QRDBText19: TQRDBText;
    QRDBText20: TQRDBText;
    QRDBText21: TQRDBText;
    QRDBText22: TQRDBText;
    QrlIdade: TQRLabel;
    QrlRG: TQRLabel;
    QrlIdadeAluno: TQRLabel;
    QRLabel7: TQRLabel;
    QRDBText3: TQRDBText;
    QryRelatorioco_resp: TIntegerField;
    QryRelatoriono_resp: TStringField;
    QryRelatorioco_sexo_resp: TStringField;
    QryRelatoriodt_nasc_resp: TDateTimeField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorionu_cpf_resp: TStringField;
    QryRelatoriode_grau_paren: TStringField;
    QryRelatorionu_tele_resi_resp: TStringField;
    QryRelatoriodes_email_resp: TStringField;
    QryRelatorioGRAUINSTRUCAO: TStringField;
    QryRelatorioPARENTESCO: TStringField;
    QryRelatorioco_rg_resp: TStringField;
    QryRelatorioco_org_rg_resp: TStringField;
    QryRelatorioco_esta_rg_resp: TStringField;
    QryRelatoriode_ende_resp: TStringField;
    QryRelatorionu_ende_resp: TIntegerField;
    QryRelatoriode_comp_resp: TStringField;
    QryRelatorioco_esta_resp: TStringField;
    QryRelatorioco_cep_resp: TStringField;
    QryRelatorionu_tele_celu_resp: TStringField;
    QryRelatoriono_empr_resp: TStringField;
    QryRelatoriono_setor_resp: TStringField;
    QryRelatoriono_funcao_resp: TStringField;
    QryRelatoriodes_email_emp: TStringField;
    QryRelatorionu_tele_come_resp: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatoriodt_nasc_alu: TDateTimeField;
    QryRelatoriotp_def: TStringField;
    QryRelatorionu_nis: TBCDField;
    QryRelatorioco_sexo_alu: TStringField;
    QryRelatoriosigla: TWideStringField;
    QryRelatorioco_alu: TAutoIncField;
    QryRelatorioco_emp: TIntegerField;
    SummaryBand1: TQRBand;
    QRLabel12: TQRLabel;
    QRLTotal: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelFicCadIndRes: TFrmRelFicCadIndRes;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils;

{$R *.dfm}

procedure TFrmRelFicCadIndRes.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
  var diasnoano : real;
begin
  inherited;

  QRLabel4.Left := QRLPage.Left - 4;
  QRSysData1.Left := QRLPage.Left - 28;
  QRShape7.Width := DetailBand1.Width;
  QRShape8.Width := DetailBand1.Width;
  QRShape5.Width := DetailBand1.Width;
  QRShape1.Width := DetailBand1.Width;
  QRShape2.Width := DetailBand1.Width;
  QRShape3.Width := DetailBand1.Width;
  QRShape4.Width := DetailBand1.Width;
  QRShape6.Width := DetailBand1.Width;
  LblTituloRel.Width := DetailBand1.Width;

  QrlRG.Caption := QryRelatorioco_rg_resp.AsString + ' / ' + QryRelatorioco_org_rg_resp.AsString + ' / ' + QryRelatorioco_esta_rg_resp.AsString;

  QRLEndereco.Caption := '';
  QRLCidUF.Caption := '';

  if QryRelatorioco_sexo_resp.AsString = 'M' then
    QRLSexResp.Caption := 'Masculino'
  else
    QRLSexResp.Caption := 'Feminino';

  QRLEndereco.Caption := QryRelatoriode_ende_resp.AsString + ', Nº '+QryRelatorionu_ende_resp.AsString +
  ', ' + QryRelatoriode_comp_resp.AsString;

  QRLCidUF.Caption := QryRelatorioNO_CIDADE.AsString + ' - ' + QryRelatorioco_esta_resp.AsString;

  diasnoano := 365.6;
  QRLIdade.Caption := QryRelatoriodt_nasc_resp.AsString + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('dt_nasc_resp').AsDateTime) / diasnoano)) + ')';
end;

procedure TFrmRelFicCadIndRes.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;

  if not QryRelatorionu_nis.IsNull then
  begin
    QRLNuNis.Caption := QryRelatorionu_nis.AsString;
  end
  else
    QRLNuNis.Caption := '-';

  if not(QryRelatorioco_alu.IsNull) and not(QryRelatorioco_emp.IsNull) then
  begin
    with DM.QrySql do
    begin
      CLose;
      SQL.Clear;
      SQL.Text := 'select mm.co_alu_cad,c.no_cur,C.CO_SIGL_CUR,tu.co_sigla_turma [no_tur] from tb08_matrcur mm ' +
                  'join tb01_curso c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp ' +
                  'join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = c.co_emp and t.co_modu_cur = mm.co_modu_cur '+
                  'join tb129_cadturmas tu on tu.co_tur = t.co_tur and tu.co_modu_cur = t.co_modu_cur '+
                  'where mm.co_alu =' + QryRelatorioco_alu.AsString +
                  ' and mm.co_emp = ' + QryRelatorioco_emp.AsString;
      Open;

      if not IsEmpty then
      begin
        if not(DM.QrySql.FieldByName('CO_SIGL_CUR').IsNull) and not(DM.QrySql.FieldByName('no_tur').IsNull) then
          QRLSerTur.Caption :=  DM.QrySql.FieldByName('CO_SIGL_CUR').AsString + ' / ' + DM.QrySql.FieldByName('no_tur').AsString
        else
          QRLSerTur.Caption := '-';

        if not DM.QrySql.FieldByName('co_alu_cad').IsNull then
          QRLMatricula.Caption := FormatMaskText('00.000.000000;0',DM.QrySql.FieldByName('co_alu_cad').AsString)
        else
          QRLMatricula.Caption := '-';
      end
      else
      begin
        QRLSerTur.Caption := '-';
        QRLMatricula.Caption := '-';
      end;
    end;
    QRDBText3.Enabled := true;
    QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
  end
  else
  begin
    QRLSerTur.Caption := '-';
    QRLMatricula.Caption := '-';
    QRDBText3.Enabled := false;
  end;

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
  if QryRelatorio.FieldByName('TP_DEF').AsString = '' then
    QRLDeficiencia.Caption := '-';

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  diasnoano := 365.6;

  if not QryRelatoriodt_nasc_alu.IsNull then
    QrlIdadeAluno.Caption := FormatDateTime('dd/MM/yy',QryRelatoriodt_nasc_alu.AsDateTime) + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('dt_nasc_alu').AsDateTime) / diasnoano)) + ')'
  else
    QrlIdadeAluno.Caption := '-';

end;

procedure TFrmRelFicCadIndRes.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';  
end;

end.
