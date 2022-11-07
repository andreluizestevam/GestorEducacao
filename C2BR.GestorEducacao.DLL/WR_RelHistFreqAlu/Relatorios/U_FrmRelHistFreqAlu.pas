unit U_FrmRelHistFreqAlu;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelHistFreqAlu = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel7: TQRLabel;
    QRLParametros: TQRLabel;
    QRLAluno: TQRLabel;
    QRLDataPlaAula: TQRLabel;
    QRLHora: TQRLabel;
    QRLJustificativa: TQRLabel;
    QRLPresenca: TQRLabel;
    QRLObjAula: TQRLabel;
    QRLTemAula: TQRLabel;
    QRBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRLTotAulas: TQRLabel;
    QRLabel4: TQRLabel;
    QRLTotPresen: TQRLabel;
    QRLTotPresenPer: TQRLabel;
    QRLabel12: TQRLabel;
    QRLTotFaltaPer: TQRLabel;
    QRLabel14: TQRLabel;
    QRLTotFalta: TQRLabel;
    QRLTotFalJus: TQRLabel;
    QRLTotFalJusPer: TQRLabel;
    QRLabel18: TQRLabel;
    QRLTotFalNaoJus: TQRLabel;
    QRLTotFalNaoJusPer: TQRLabel;
    QRLabel6: TQRLabel;
    QRLPage: TQRLabel;
    QRLTA: TQRLabel;
    QRLNoMateria: TQRLabel;
    QRSDetail: TQRShape;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelHistFreqAlu: TFrmRelHistFreqAlu;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelHistFreqAlu.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').AsString = 'D' then
  begin
    QRLTA.Caption := '**';
    QRLHora.Caption := '**************';
    QRLTemAula.Caption := '*** Aula não Planejada ***';
    QRLObjAula.Caption := '******************************';
    QRLNoMateria.Caption := '*********************';
  end
  else
  begin
    QRLNoMateria.Caption := QryRelatorio.FieldByName('no_materia').AsString;

    if not QryRelatorio.FieldByName('nu_temp_pla').IsNull then
    begin
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '0' then
        QRLTA.Caption := '1º';
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '1' then
        QRLTA.Caption := '2º';
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '2' then
        QRLTA.Caption := '3º';
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '3' then
        QRLTA.Caption := '4º';
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '4' then
        QRLTA.Caption := '5º';
      if QryRelatorio.FieldByName('nu_temp_pla').AsString = '5' then
        QRLTA.Caption := '6º';
      {with QryHorario do
      begin
        Close;
        SQL.Clear;
        Sql.Text := ' SELECT DISTINCT HR_INIC_AULA_GRD, HR_TERM_AULA_GRD ' +
                    ' FROM TB05_GRD_HORAR '+
                    ' WHERE NU_TEMP_AULA_GRD = ' + QryRelatorio.FieldByName('nu_temp_pla').AsString +
                    ' AND CO_EMP = ' + codigoEmpresa +
                    ' AND CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                    ' AND CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString;
        Open;
      end;}
    end
    else
    begin
      QRLTA.Caption := '**';
    end;

    if (not QryRelatorio.FieldByName('HR_INI_ATIV').IsNull) and (not QryRelatorio.FieldByName('HR_TER_ATIV').IsNull) then
      QRLHora.Caption := QryRelatorio.FieldByName('HR_INI_ATIV').AsString + ' / ' + QryRelatorio.FieldByName('HR_TER_ATIV').AsString
    else
      QRLHora.Caption := '**************';

    if not QryRelatorio.FieldByName('DE_TEMA_AULA').IsNull then
      QRLTemAula.Caption := QryRelatorio.FieldByName('DE_TEMA_AULA').AsString
    else
      QRLTemAula.Caption := '*** Aula não Planejada ***';

    if not QryRelatorio.FieldByName('DE_OBJE_AULA').IsNull then
      QRLObjAula.Caption := QryRelatorio.FieldByName('DE_OBJE_AULA').AsString
    else
      QRLObjAula.Caption := '******************************';
  end;

  QRLDataPlaAula.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('dt_fre').AsDateTime);

// presença antigo
{  if QryRelatoriofla_presenca.AsBoolean then
    QRLPresenca.Caption := 'Sim'
  else
    QRLPresenca.Caption := 'Não';
}
// presença novo
  with QryRelatorio do
  begin
    if FieldByName('CO_FLAG_FREQ_ALUNO').AsString = 'S' then
    begin
      QRLPresenca.Caption := 'Sim';
      QRLTotPresen.Caption := IntToStr(StrToInt(QRLTotPresen.Caption) + 1);
      QRLJustificativa.Caption := '';
    end;
    if FieldByName('CO_FLAG_FREQ_ALUNO').AsString = 'N' then
    begin
      QRLPresenca.Caption := 'Não';
      QRLTotFalta.Caption := IntToStr(StrToInt(QRLTotFalta.Caption) + 1);

      if not QryRelatorio.FieldByName('DE_JUSTI_FREQ_ALUNO').IsNull then
      begin
       QRLJustificativa.Caption := QryRelatorio.FieldByName('DE_JUSTI_FREQ_ALUNO').AsString;
       QRLTotFalJus.Caption := IntToStr(StrToInt(QRLTotFalJus.Caption) + 1);
      end
      else
      begin
       QRLTotFalNaoJus.Caption := IntToStr(StrToInt(QRLTotFalNaoJus.Caption) + 1);
       QRLJustificativa.Caption := '***** Falta sem Justificativa *****';
      end;
    end;
  end;

  QRLJustificativa.Height := 31;
  QRLTotAulas.Caption := IntToStr(StrToInt(QRLTotAulas.Caption) + 1);

end;

procedure TFrmRelHistFreqAlu.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotAulas.Caption := '0';
  QRLTotPresen.Caption := '0';
  QRLTotFalta.Caption := '0';
  QRLTotFalJus.Caption := '0';
  QRLTotFalNaoJus.Caption := '0';
end;

procedure TFrmRelHistFreqAlu.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotAulas.Caption := FormatFloat('0000',StrToFloat(QRLTotAulas.caption));
  QRLTotPresen.Caption := FormatFloat('000',StrToFloat(QRLTotPresen.caption));
  QRLTotFalta.Caption := FormatFloat('000',StrToFloat(QRLTotFalta.caption));
  QRLTotFalJus.Caption := FormatFloat('000',StrToFloat(QRLTotFalJus.caption));
  QRLTotFalNaoJus.Caption := FormatFloat('000',StrToFloat(QRLTotFalNaoJus.caption));
  if StrtoInt(QRLTotAulas.Caption) <> 0 then
    QRLTotPresenPer.Caption := '(' + FloatToStrF((StrToInt(QRLTotPresen.Caption) * 100)/StrtoInt(QRLTotAulas.Caption),ffNumber,10,2) +'%)';
  if StrtoInt(QRLTotAulas.Caption) <> 0 then
    QRLTotFaltaPer.Caption := '(' + FloatToStrF((StrToInt(QRLTotFalta.Caption) * 100)/StrtoInt(QRLTotAulas.Caption),ffNumber,10,2) +'%)';
  if StrtoInt(QRLTotFalta.Caption) <> 0 then
    QRLTotFalJusPer.Caption := '(' + FloatToStrF((StrToInt(QRLTotFalJus.Caption) * 100)/StrtoInt(QRLTotFalta.Caption),ffNumber,10,2) +'%)'
  else
    QRLTotFalJusPer.Caption := '(00,00%)';
  if StrtoInt(QRLTotFalta.Caption) <> 0 then
    QRLTotFalNaoJusPer.Caption := '(' + FloatToStrF((StrToInt(QRLTotFalNaoJus.Caption) * 100)/StrtoInt(QRLTotFalta.Caption),ffNumber,10,2) +'%)'
  else
    QRLTotFalNaoJusPer.Caption := '(00,00%)';
end;

procedure TFrmRelHistFreqAlu.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  {if QryRelatorioCO_PARAM_FREQUE.AsString = 'D' then
  begin
    QRSDetail.Top := 18;
    DetailBand1.Height := 20;
  end
  else
  begin
    QRSDetail.Top := 32;
    DetailBand1.Height := 34;
  end;          }
end;

end.
