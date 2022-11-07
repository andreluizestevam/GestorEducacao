unit U_FrmRelReciboEntregDiploma;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelReciboEntregDiploma = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel23: TQRLabel;
    QRLNumProMEC: TQRLabel;
    QRLNumLivProMEC: TQRLabel;
    QRLNumPagProMEC: TQRLabel;
    QRLDtRegProMEC: TQRLabel;
    QRLDtIniDip: TQRLabel;
    QRLDtFimDip: TQRLabel;
    QRLDtSolDip: TQRLabel;
    QRLDtEntDip: TQRLabel;
    QRLDtEncDip: TQRLabel;
    QRLDtColGraDip: TQRLabel;
    QRLAnoConDip: TQRLabel;
    QRLCHSerDip: TQRLabel;
    QRLCHCumDip: TQRLabel;
    QRShape4: TQRShape;
    QRLNumProEsc: TQRLabel;
    QRLNumLivProEsc: TQRLabel;
    QRLNumPagProEsc: TQRLabel;
    QRLDtRegProEsc: TQRLabel;
    QRShape5: TQRShape;
    QRShape1: TQRShape;
    QRLabel21: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape2: TQRShape;
    QRLDescAlu: TQRLabel;
    QRShape3: TQRShape;
    QRLabel2: TQRLabel;
    QRShape6: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel20: TQRLabel;
    QRLDtSolic: TQRLabel;
    QRLNoSolic: TQRLabel;
    QRLabel26: TQRLabel;
    QRLNumRGSolic: TQRLabel;
    QRLabel28: TQRLabel;
    QRLTelSolic: TQRLabel;
    QRLabel30: TQRLabel;
    QRLDtEntr: TQRLabel;
    QRLabel32: TQRLabel;
    QRLNoEntr: TQRLabel;
    QRLabel34: TQRLabel;
    QRLNumRGEntr: TQRLabel;
    QRLabel36: TQRLabel;
    QRLTelEntr: TQRLabel;
    QRLabel38: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelReciboEntregDiploma: TFrmRelReciboEntregDiploma;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelReciboEntregDiploma.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLDescAlu.Caption := 'Módulo: ' + QryRelatorio.FieldByName('DE_MODU_CUR').AsString + ' - Série: ' + QryRelatorio.FieldByName('NO_CUR').AsString +
  ' - Aluno: ' + UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) + ' - NIRE: ' + QryRelatorio.FieldByName('NU_NIRE').AsString;

  if not QryRelatorio.FieldByName('NU_PROC_ESC_DIP').IsNull then
    QRLNumProEsc.Caption := QryRelatorio.FieldByName('NU_PROC_ESC_DIP').AsString
  else
    QRLNumProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_LIVR_ESC_DIP').IsNull then
    QRLNumLivProEsc.Caption := QryRelatorio.FieldByName('NU_LIVR_ESC_DIP').AsString
  else
    QRLNumLivProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PAGI_ESC_DIP').IsNull then
    QRLNumPagProEsc.Caption := QryRelatorio.FieldByName('NU_PAGI_ESC_DIP').AsString
  else
    QRLNumPagProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('DT_REGI_ESC_DIP').IsNull then
    QRLDtRegProEsc.Caption := QryRelatorio.FieldByName('DT_REGI_ESC_DIP').AsString
  else
    QRLDtRegProEsc.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PROC_MEC_DIP').IsNull then
    QRLNumProMEC.Caption := QryRelatorio.FieldByName('NU_PROC_MEC_DIP').AsString
  else
    QRLNumProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('NU_LIVR_MEC_DIP').IsNull then
    QRLNumLivProMEC.Caption := QryRelatorio.FieldByName('NU_LIVR_MEC_DIP').AsString
  else
    QRLNumLivProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('NU_PAGI_MEC_DIP').IsNull then
    QRLNumPagProMEC.Caption := QryRelatorio.FieldByName('NU_PAGI_MEC_DIP').AsString
  else
    QRLNumPagProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('DT_REGI_MEC_DIP').IsNull then
    QRLDtRegProMEC.Caption := QryRelatorio.FieldByName('DT_REGI_MEC_DIP').AsString
  else
    QRLDtRegProMEC.Caption := '-';

  if not QryRelatorio.FieldByName('DT_INIC_CURS_DIP').IsNull then
    QRLDtIniDip.Caption := QryRelatorio.FieldByName('DT_INIC_CURS_DIP').AsString
  else
    QRLDtIniDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_TERM_CURS_DIP').IsNull then
    QRLDtFimDip.Caption := QryRelatorio.FieldByName('DT_TERM_CURS_DIP').AsString
  else
    QRLDtFimDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_SOLI_DIP').IsNull then
    QRLDtSolDip.Caption := QryRelatorio.FieldByName('DT_SOLI_DIP').AsString
  else
    QRLDtSolDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_ENTR_DIP').IsNull then
    QRLDtEntDip.Caption := QryRelatorio.FieldByName('DT_ENTR_DIP').AsString
  else
    QRLDtEntDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_PART_ENC').IsNull then
    QRLDtEncDip.Caption := QryRelatorio.FieldByName('DT_PART_ENC').AsString
  else
    QRLDtEncDip.Caption := '-';

  if not QryRelatorio.FieldByName('DT_COLACAO_GRAU').IsNull then
    QRLDtColGraDip.Caption := QryRelatorio.FieldByName('DT_COLACAO_GRAU').AsString
  else
    QRLDtColGraDip.Caption := '-';

  if not QryRelatorio.FieldByName('CO_ANO_SEM_CURSO').IsNull then
    QRLAnoConDip.Caption := QryRelatorio.FieldByName('CO_ANO_SEM_CURSO').AsString
  else
    QRLAnoConDip.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CARGA_HOR_CURSO').IsNull then
    QRLCHSerDip.Caption := QryRelatorio.FieldByName('NU_CARGA_HOR_CURSO').AsString
  else
    QRLCHSerDip.Caption := '-';

  if not QryRelatorio.FieldByName('NU_CARGA_HOR_CUMP').IsNull then
    QRLCHCumDip.Caption := QryRelatorio.FieldByName('NU_CARGA_HOR_CUMP').AsString
  else
    QRLCHCumDip.Caption := '-';

  //**********Dados solicitantes
  if not QryRelatorio.FieldByName('DT_SOLIC').IsNull then
    QRLDtSolic.Caption := QryRelatorio.FieldByName('DT_SOLIC').AsString
  else
    QRLDtSolic.Caption := '-';

  if QryRelatorio.FieldByName('TP_USU_SOLIC').AsString = 'O' then
  begin
    QRLNoSolic.Caption := QryRelatorio.FieldByName('NO_RESP_SOLIC_DIPLOMA').AsString;
    QRLNumRGSolic.Caption := QryRelatorio.FieldByName('CO_RG_RESP_SOLIC_DIPLOMA').AsString;
    QRLTelSolic.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_RESP_SOLIC_DIPLOMA').AsString);
  end
  else
  begin
    With DM.QrySql do
    begin
      Close;
      SQL.Clear;
      if QryRelatorio.FieldByName('TP_USU_SOLIC').AsString = 'A' then
      begin
        SQL.Text := 'select no_alu as noSolic, nu_tele_celu_alu as telSolic,CO_RG_ALU as numRGSolic from tb07_aluno ' +
                    ' where co_alu = ' + QryRelatorio.FieldByName('usuSol').AsString +
                    ' and co_emp = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString;
      end
      else
      begin
        SQL.Text := 'select no_resp as noSolic, nu_tele_celu_resp as telSolic,CO_RG_RESP as numRGSolic from tb108_responsavel ' +
                    ' where co_resp = ' + QryRelatorio.FieldByName('usuSol').AsString
      end;

      Open;

      if not IsEmpty then
      begin
        QRLNoSolic.Caption := FieldByName('noSolic').AsString;
        if (not FieldByName('numRGSolic').IsNull) and (FieldByName('numRGSolic').AsString <> '') then
          QRLNumRGSolic.Caption := FieldByName('numRGSolic').AsString
        else
          QRLNumRGSolic.Caption := '-';

        if (not FieldByName('telSolic').IsNull) and (FieldByName('telSolic').AsString <> '') then
          QRLTelSolic.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('telSolic').AsString)
        else
          QRLTelSolic.Caption := '-';
      end;
    end;
  end;

  //****************************

  //**********Dados Entrega
  if not QryRelatorio.FieldByName('DT_ENTREGA').IsNull then
    QRLDtEntr.Caption := QryRelatorio.FieldByName('DT_ENTREGA').AsString
  else
    QRLDtEntr.Caption := '-';

  if QryRelatorio.FieldByName('TP_USU').AsString = 'O' then
  begin
    QRLNoEntr.Caption := QryRelatorio.FieldByName('NO_RESP_ENTR_DOCUM').AsString;
    QRLNumRGEntr.Caption := QryRelatorio.FieldByName('CO_RG_RESP_ENTR_DOCUM').AsString;
    QRLTelEntr.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TELE_RESP_ENTR_DOCUM').AsString);
  end
  else
  begin
    With DM.QrySql do
    begin
      Close;
      SQL.Clear;
      if QryRelatorio.FieldByName('TP_USU').AsString = 'A' then
      begin
        SQL.Text := 'select no_alu as noEntr, nu_tele_celu_alu as telEntr,CO_RG_ALU as numRGEntr from tb07_aluno ' +
                    ' where co_alu = ' + QryRelatorio.FieldByName('usuEnt').AsString +
                    ' and co_emp = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString;
      end
      else
      begin
        SQL.Text := 'select no_resp as noSolic, nu_tele_celu_resp as telSolic,CO_RG_RESP as numRGSolic from tb108_responsavel ' +
                    ' where co_resp = ' + QryRelatorio.FieldByName('usuEnt').AsString
      end;

      Open;

      if not IsEmpty then
      begin
        QRLNoEntr.Caption := FieldByName('noEntr').AsString;
        
        if (not FieldByName('numRGEntr').IsNull) and (FieldByName('numRGEntr').AsString <> '') then
          QRLNumRGEntr.Caption := FieldByName('numRGEntr').AsString
        else
          QRLNumRGEntr.Caption := '-';

        if (not FieldByName('telEntr').IsNull) and (FieldByName('telEntr').AsString <> '') then
          QRLTelEntr.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('telEntr').AsString)
        else
          QRLTelEntr.Caption := '-';
      end;
    end;
  end;

  //****************************
end;

end.
