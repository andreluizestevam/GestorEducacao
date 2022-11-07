unit U_FrmRelDistAluBolEscola;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelDistAluBolEscola = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRShape1: TQRShape;
    QRLabel22: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText8: TQRDBText;
    QRDBText7: TQRDBText;
    QRLabel1: TQRLabel;
    QRLProgSocial: TQRLabel;
    QRLabel4: TQRLabel;
    QRLPage: TQRLabel;
    QrlTitSerie: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLNuBolEsc: TQRLabel;
    qrIdade: TQRLabel;
    QRDBRenda: TQRDBText;
    QRLCoAluCad: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel8: TQRLabel;
    qrlTotal: TQRLabel;
    QRShape2: TQRShape;
    QrlNuNis: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText3: TQRDBText;
    QRLNoCur: TQRLabel;
    QRLNoTur: TQRLabel;
    QRLNuCPFResp: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDistAluBolEscola: TFrmRelDistAluBolEscola;

implementation

uses U_DataModuleSGE, U_Funcoes, DateUtils,MaskUtils;

{$R *.dfm}

procedure TFrmRelDistAluBolEscola.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano: real;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select top 1 mm.co_alu_cad, c.co_sigl_cur, ct.co_sigla_turma from tb08_matrcur mm ' +
                ' join tb01_curso c on c.co_cur = mm.co_cur and mm.co_modu_cur = c.co_modu_cur and mm.co_emp = c.co_emp ' +
                ' join tb06_turmas t on c.co_cur = mm.co_cur and mm.co_modu_cur = c.co_modu_cur and mm.co_emp = c.co_emp and t.co_tur = mm.co_tur ' +
                ' join tb129_cadturmas ct on ct.co_tur = t.co_tur ' +
                ' where mm.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' order by mm.co_ano_mes_mat desc';
    Open;

    if not IsEmpty then
    begin
      QRLCoAluCad.Caption := FormatMaskText('00.000.000000;0',FieldByName('co_alu_cad').AsString);
      QRLNoCur.Caption := FieldByName('co_sigl_cur').AsString + ' / ' + FieldByName('co_sigla_turma').AsString;
      //QRLNoTur.Caption := FieldByName('co_sigla_turma').AsString;
    end
    else
    begin
      QRLCoAluCad.Caption := '-';
      QRLNoCur.Caption := '-';
      //QRLNoTur.Caption := '-';
    end;
  end;

  if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
    QRLNuCPFResp.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString)
  else
    QRLNuCPFResp.Caption := '-';

  QrlNuNis.Caption := QryRelatorio.FieldByName('nu_nis').AsString;

  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;

  diasnoano := 365.6;

  if  not(QryRelatorio.FieldByName('DT_NASC_ALU').IsNull)then
    qrIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano))
  else
    qrIdade.Caption := ' ';

  qrlTotal.Caption := IntToStr(StrToInt(qrlTotal.Caption) +1);

end;

procedure TFrmRelDistAluBolEscola.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  qrlTotal.Caption := '0';
end;

end.
