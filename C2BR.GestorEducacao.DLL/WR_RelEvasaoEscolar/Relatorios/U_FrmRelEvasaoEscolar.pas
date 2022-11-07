unit U_FrmRelEvasaoEscolar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelEvasaoEscolar = class(TFrmRelTemplate)
    QrlParametros: TQRLabel;
    QRBand2: TQRBand;
    QRLabel27: TQRLabel;
    QRLParam1: TQRLabel;
    QrlJan: TQRLabel;
    QrlFev: TQRLabel;
    QrlAbr: TQRLabel;
    QrlMar: TQRLabel;
    QrlAgo: TQRLabel;
    QrlJul: TQRLabel;
    QrlJun: TQRLabel;
    QrlMai: TQRLabel;
    QrlSete: TQRLabel;
    QrlOutu: TQRLabel;
    QrlNov: TQRLabel;
    QrlDez: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape1: TQRShape;
    QRBand3: TQRBand;
    QRDBTParam1: TQRDBText;
    QRLabel15: TQRLabel;
    QRLPage: TQRLabel;
    QRLMJan: TQRLabel;
    QRLMFev: TQRLabel;
    QRLMMar: TQRLabel;
    QRLMAbr: TQRLabel;
    QRLMMai: TQRLabel;
    QRLMJun: TQRLabel;
    QRLMJul: TQRLabel;
    QRLMAgo: TQRLabel;
    QRLMSet: TQRLabel;
    QRLMOut: TQRLabel;
    QRLMNov: TQRLabel;
    QRLMDez: TQRLabel;
    QRLMTot: TQRLabel;
    QRLDescFalta: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    dtInicial,dtFinal,id_materia, codigoEmpresa : String;
  end;

var
  FrmRelEvasaoEscolar: TFrmRelEvasaoEscolar;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelEvasaoEscolar.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  SqlString : String;
begin
  inherited;
  QrlMJan.Caption   := '-';
  QrlMFev.Caption   := '-';
  QrlMMar.Caption   := '-';
  QrlMAbr.Caption   := '-';
  QrlMMai.Caption   := '-';
  QrlMJun.Caption   := '-';
  QrlMJul.Caption   := '-';
  QrlMAgo.Caption   := '-';
  QrlMSet.Caption  := '-';
  QrlMOut.Caption  := '-';
  QrlMNov.Caption   := '-';
  QrlMDez.Caption   := '-';
  QrlMTot.Caption := '-';

  if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').AsString = 'M' then
    QRLDescFalta.Caption := 'Faltas por Matérias'
  else
    QRLDescFalta.Caption := 'Faltas Diárias';

  if QRBand3.Color = clWhite then
    QRBand3.Color := $00D8D8D8
  else
    QRBand3.Color := clWhite;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    if id_materia = '0' then
    begin
      SqlString :=' SET LANGUAGE PORTUGUESE ' +
                 ' select b.no_cur, jan = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '1' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        fev = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '2' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        mar = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '3' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        abr = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '4' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        mai = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '5' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        jun = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '6' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        jul = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '7' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        ago = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '8' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        sete = sum(case month(a.dt_fre) '+
                 '                    when ' + '''' + '9' + '''' + ' then 1     '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        Outu = sum(case month(a.dt_fre) '+
                 '                    when ' + '''' + '10' + '''' + ' then 1    '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        Nov = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '11' + '''' + ' then 1    '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        Dez = sum(case month(a.dt_fre)  '+
                 '                    when ' + '''' + '12' + '''' + ' then 1    '+
                 '                    else 0              '+
                 '                  end),                 '+
                 '        Count(a.co_cur) Total           '+
                 ' from TB132_FREQ_ALU a,                   '+
                 '      TB01_CURSO b                      '+
                 ' where a.CO_EMP_ALU = b.co_emp              '+
                 ' and a.co_cur = b.co_cur              '+
                 ' and a.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
                 ' and a.CO_EMP_ALU = '+ codigoEmpresa +
                 ' and a.co_cur = ' + QryRelatorio.fieldByName('co_cur').AsString +
                 ' and year(a.dt_fre) >= '+ quotedStr(dtInicial) +
                 ' and year(a.dt_fre) <= '+ quotedStr(dtFinal) +
                 ' group by b.no_cur ';
    end
    else
    begin
      SqlString :=' SET LANGUAGE PORTUGUESE ' +
               ' select cm.no_red_materia, jan = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '1' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        fev = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '2' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        mar = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '3' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        abr = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '4' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        mai = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '5' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        jun = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '6' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        jul = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '7' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        ago = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '8' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        sete = sum(case month(a.dt_fre) '+
               '                    when ' + '''' + '9' + '''' + ' then 1     '+
               '                    else 0              '+
               '                  end),                 '+
               '        outu = sum(case month(a.dt_fre) '+
               '                    when ' + '''' + '10' + '''' + ' then 1    '+
               '                    else 0              '+
               '                  end),                 '+
               '        nov = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '11' + '''' + ' then 1    '+
               '                    else 0              '+
               '                  end),                 '+
               '        dez = sum(case month(a.dt_fre)  '+
               '                    when ' + '''' + '12' + '''' + ' then 1    '+
               '                    else 0              '+
               '                  end),                 '+
               '        Count(a.co_cur) Total           '+
               ' from TB132_FREQ_ALU a, tb02_materia m,   '+
               '      tb107_cadmaterias cm'+
               ' where a.co_mat = m.co_mat ' +
               ' and m.id_materia = cm.id_materia ' +
               ' and a.CO_EMP_ALU = m.co_emp ' +
               ' and a.CO_FLAG_FREQ_ALUNO = ' + QuotedStr('N') +
               ' and a.CO_EMP_ALU = '+ codigoEmpresa +
               ' and a.co_cur = ' + QryRelatorio.fieldByName('co_cur').AsString +
               ' and cm.id_materia = ' + id_materia +
               ' and year(a.dt_fre) >= '+ dtInicial +
               ' and year(a.dt_fre) <= '+ dtFinal +
               ' group by cm.no_red_materia ';
    end;
    SQL.Text := SqlString;
    Open;

    if not IsEmpty then
    begin
     if not FieldByName('jan').IsNull then
      QRLMJan.Caption := FieldByName('jan').AsString
     else
      QRLMJan.Caption := '-';
     if not FieldByName('fev').IsNull then
      QRLMFev.Caption := FieldByName('fev').AsString
     else
      QRLMFev.Caption := '-';
     if not FieldByName('mar').IsNull then
      QRLMMar.Caption := FieldByName('mar').AsString
     else
      QRLMMar.Caption := '-';
     if not FieldByName('abr').IsNull then
      QRLMAbr.Caption := FieldByName('abr').AsString
     else
      QRLMAbr.Caption := '-';
     if not FieldByName('mai').IsNull then
      QRLMMai.Caption := FieldByName('mai').AsString
     else
      QRLMMai.Caption := '-';
     if not FieldByName('jun').IsNull then
      QRLMJun.Caption := FieldByName('jun').AsString
     else
      QRLMJun.Caption := '-';
     if not FieldByName('jul').IsNull then
      QRLMJul.Caption := FieldByName('jul').AsString
     else
      QRLMJul.Caption := '-';
     if not FieldByName('ago').IsNull then
      QRLMAgo.Caption := FieldByName('ago').AsString
     else
      QRLMAgo.Caption := '-';
     if not FieldByName('sete').IsNull then
      QRLMSet.Caption := FieldByName('sete').AsString
     else
      QRLMSet.Caption := '-';
     if not FieldByName('outu').IsNull then
      QRLMOut.Caption := FieldByName('outu').AsString
     else
      QRLMOut.Caption := '-';
     if not FieldByName('nov').IsNull then
      QRLMNov.Caption := FieldByName('nov').AsString
     else
      QRLMNov.Caption := '-';
     if not FieldByName('dez').IsNull then
      QRLMDez.Caption := FieldByName('dez').AsString
     else
      QRLMDez.Caption := '-';
     if not FieldByName('Total').IsNull then
      QRLMTot.Caption := FieldByName('Total').AsString
     else
      QRLMTot.Caption := '-';
    end;
  end;

  if QRLMJan.Caption <> '-' then
    QrlJan.Caption := IntToStr(StrToInt(QrlJan.Caption) + StrToInt(QRLMJan.Caption));
  if QRLMFev.Caption <> '-' then
    QrlFev.Caption := IntToStr(StrToInt(QrlFev.Caption) + StrToInt(QRLMFev.Caption));
  if QRLMMar.Caption <> '-' then
    QrlMar.Caption := IntToStr(StrToInt(QrlMar.Caption) + StrToInt(QRLMMar.Caption));
  if QRLMAbr.Caption <> '-' then
    QrlAbr.Caption := IntToStr(StrToInt(QrlAbr.Caption) + StrToInt(QRLMAbr.Caption));
  if QRLMMai.Caption <> '-' then
    QrlMai.Caption := IntToStr(StrToInt(QrlMai.Caption) + StrToInt(QRLMmai.Caption));
  if QRLMJun.Caption <> '-' then
    QrlJun.Caption := IntToStr(StrToInt(QrlJun.Caption) + StrToInt(QRLMJun.Caption));
  if QRLMJul.Caption <> '-' then
    QrlJul.Caption := IntToStr(StrToInt(QrlJul.Caption) + StrToInt(QRLMJul.Caption));
  if QRLMAgo.Caption <> '-' then
    QrlAgo.Caption := IntToStr(StrToInt(QrlAgo.Caption) + StrToInt(QRLMAgo.Caption));
  if QRLMSet.Caption <> '-' then
    QrlSete.Caption := IntToStr(StrToInt(QrlSete.Caption) + StrToInt(QRLMSet.Caption));
  if QRLMOut.Caption <> '-' then
    QrlOutu.Caption := IntToStr(StrToInt(QrlOutu.Caption) + StrToInt(QRLMOut.Caption));
  if QRLMNov.Caption <> '-' then
    QrlNov.Caption := IntToStr(StrToInt(QrlNov.Caption) + StrToInt(QRLMNov.Caption));
  if QRLMDez.Caption <> '-' then
    QrlDez.Caption := IntToStr(StrToInt(QrlDez.Caption) + StrToInt(QRLMDez.Caption));
  if QRLMTot.Caption <> '-' then
    QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + StrToInt(QRLMTot.Caption));
end;

procedure TFrmRelEvasaoEscolar.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlJan.Caption   := '0';
  QrlFev.Caption   := '0';
  QrlMar.Caption   := '0';
  QrlAbr.Caption   := '0';
  QrlMai.Caption   := '0';
  QrlJun.Caption   := '0';
  QrlJul.Caption   := '0';
  QrlAgo.Caption   := '0';
  QrlSete.Caption  := '0';
  QrlOutu.Caption  := '0';
  QrlNov.Caption   := '0';
  QrlDez.Caption   := '0';
  QrlTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelEvasaoEscolar]);

end.
