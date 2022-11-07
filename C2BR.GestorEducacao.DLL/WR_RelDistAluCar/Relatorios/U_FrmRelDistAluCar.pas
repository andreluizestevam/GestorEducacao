unit U_FrmRelDistAluCar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, U_DataModuleSGE,
  StdCtrls;

type
  TFrmRelDistAluCar = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QRLEtnia: TQRLabel;
    QRLRenda: TQRLabel;
    QRLDef: TQRLabel;
    QRLIdade: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel7: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRLDtNasc: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QrlTitSerieTurma: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    qrlMatricula: TQRLabel;
    qrlSerieTurma: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel25: TQRLabel;
    QrlTotal: TQRLabel;
    qrlIEDF: TQRLabel;
    QRBand1: TQRBand;
    QRLabel24: TQRLabel;
    QRLTotalBairro: TQRLabel;
    QRLNuNis: TQRLabel;
    QRLParametros: TQRLabel;
    QRLCPFResp: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelDistAluCar: TFrmRelDistAluCar;

implementation

uses U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelDistAluCar.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano: Real;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
    QRLCPFResp.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString)
  else
    QRLCPFResp.Caption := '-';

  QRLNuNis.Caption := Qryrelatorio.FieldByName('nu_nis').asstring;

  with DM.QrySql do
  begin
   { Close;
    Sql.Clear;
    Sql.Text := 'SELECT 1 FROM TB165_ALU_INST_ESP '+
                '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('co_alu').AsString;
    Open;

    if not IsEmpty then
      qrlIEDF.Caption := 'Sim'
    else }
      qrlIEDF.Caption := 'N�o';

    if (not QryRelatorio.FieldByName('co_cur').IsNull) and (not QryRelatorio.FieldByName('co_tur').IsNull)
    and (not QryRelatorio.FieldByName('co_modu_cur').IsNull) then
    begin
      Close;
      Sql.Clear;
      Sql.Text := 'SELECT top 1 c.CO_SIGL_CUR, ct.co_sigla_turma, mm.co_alu_cad FROM TB08_matrcur mm '+
                  'join tb01_curso c on c.co_cur = mm.co_cur and c.co_emp = mm.co_emp and mm.co_modu_cur = c.co_modu_cur ' +
                  'join tb06_turmas t on t.co_cur = mm.co_cur and t.co_emp = mm.co_emp and mm.co_modu_cur = t.co_modu_cur ' +
                  'join tb129_cadturmas ct on t.co_tur = ct.co_tur ' +
                  '	WHERE mm.CO_CUR = ' + QryRelatorio.FieldByName('co_cur').AsString +
                  ' and mm.co_emp = ' + QryRelatorio.FieldByName('co_emp').AsString +
                  ' and mm.co_tur = ' + QryRelatorio.FieldByName('co_tur').AsString +
                  ' and mm.co_modu_cur = ' + QryRelatorio.FieldByName('co_modu_cur').AsString +
                  ' order by mm.co_ano_mes_mat desc';
      Open;

      if not IsEmpty then
      begin
        qrlMatricula.Caption := FormatMaskText('00.000.000000;0', FieldByName('co_alu_cad').AsString);

        qrlSerieTurma.Caption := FieldByName('CO_SIGL_CUR').AsString + ' / ' + FieldByName('co_sigla_turma').AsString;
      end
      else
      begin
        qrlMatricula.Caption := '-';
        qrlSerieTurma.Caption := '-';
      end
    end
    else
    begin
        qrlMatricula.Caption := '-';
        qrlSerieTurma.Caption := '-';
    end
  end;

  QRLDtNasc.Caption := FormatDateTime('dd/mm/yy', QryRelatorio.FieldByName('dt_nasc_alu').AsFloat);

  if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
  begin
    diasnoano := 365.6;
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.FieldByName('DT_NASC_ALU').AsDateTime) / diasnoano));
  end
  else
    QRLIdade.Caption := '-';
  
  QRLEtnia.Caption := '-';
  QRLRenda.Caption := '-';
  QRLDef.Caption := '-';

  with QryRelatorio do
  begin
    if FieldByName('tp_raca').AsString = 'B' then
      QRLEtnia.Caption := 'Branca';
    if FieldByName('tp_raca').AsString = 'P' then
      QRLEtnia.Caption := 'Preta';
    if FieldByName('tp_raca').AsString = 'A' then
      QRLEtnia.Caption := 'Amarela';
    if FieldByName('tp_raca').AsString = 'D' then
      QRLEtnia.Caption := 'Parda';
    if FieldByName('tp_raca').AsString = 'I' then
      QRLEtnia.Caption := 'Ind�gena';
    if FieldByName('tp_raca').AsString = 'N' then
      QRLEtnia.Caption := 'N�o Declarada';

    if FieldByName('RENDA_FAMILIAR').AsString = '1' then
      QRLRenda.Caption := '1 a 3 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '2' then
      QRLRenda.Caption := '3 a 5 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '3' then
      QRLRenda.Caption := '+5 SM';
    if FieldByName('RENDA_FAMILIAR').AsString = '4' then
      QRLRenda.Caption := 'Sem Renda';

    if FieldByName('TP_DEF').AsString = 'N' then
      QRLDef.Caption := 'Nenhuma';
    if FieldByName('TP_DEF').AsString = 'A' then
      QRLDef.Caption := 'Auditivo';
    if FieldByName('TP_DEF').AsString = 'V' then
      QRLDef.Caption := 'Visual';
    if FieldByName('TP_DEF').AsString = 'F' then
      QRLDef.Caption := 'F�sico';
    if FieldByName('TP_DEF').AsString = 'M' then
      QRLDef.Caption := 'Mental';
    if FieldByName('TP_DEF').AsString = 'I' then
      QRLDef.Caption := 'M�ltiplas';
    if FieldByName('TP_DEF').AsString = 'O' then
      QRLDef.Caption := 'Outros';

// se for em branco mostra "-";
    //if  QRLEtnia.Caption = '' then

  end;

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);

  QRLTotalBairro.Caption := IntToStr(StrToInt(QRLTotalBairro.Caption) +1);

end;

procedure TFrmRelDistAluCar.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

procedure TFrmRelDistAluCar.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotalBairro.Caption := '0';
end;

end.