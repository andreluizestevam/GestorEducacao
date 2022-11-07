unit U_frmRelSolicRealiz;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TfrmRelSolicRealiz = class(TFrmRelTemplate)
    QryRelatorioco_tipo_soli: TIntegerField;
    QryRelatoriono_tipo_soli: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatorioco_soli_aten: TIntegerField;
    QryRelatoriodt_soli_aten: TDateTimeField;
    QryRelatoriodt_prev_entr: TDateTimeField;
    QryRelatoriodt_entr_soli: TDateTimeField;
    QryRelatoriova_soli_aten: TBCDField;
    QryRelatorioco_isen_taxa: TStringField;
    QRGroup1: TQRGroup;
    QRDBText2: TQRDBText;
    QRBand1: TQRBand;
    QRBand3: TQRBand;
    QRLabel8: TQRLabel;
    QRShape1: TQRShape;
    qrlTipo: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QryRelatorioco_situ_soli: TStringField;
    QRDBText3: TQRDBText;
    QRLabel6: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel12: TQRLabel;
    QryRelatorioco_alu_cad: TStringField;
    QRLdt_soli_aten: TQRLabel;
    QRLdt_prev_entr: TQRLabel;
    QRLdt_entr_soli: TQRLabel;
    qrlSolic: TQRLabel;
    QryRelatoriomes_soli_aten: TIntegerField;
    QryRelatorioano_soli_aten: TIntegerField;
    QRShape2: TQRShape;
    QRLabel10: TQRLabel;
    QrlNis: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QryRelatorioDE_OBS_SOLI: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QrlSerieTurma: TQRLabel;
    QryRelatorioLOCALIZACAO: TStringField;
    QrlLocal: TQRLabel;
    QrlOBS: TQRLabel;
    QrlMatricula: TQRLabel;
    QRLabel2: TQRLabel;
    QrlRespSol: TQRLabel;
    QRLabel16: TQRLabel;
    QrlRespEnt: TQRLabel;
    QryRespEnt: TADOQuery;
    QryRelatorioCO_SIGL_CUR: TStringField;
    QryRelatorioNU_NIS: TBCDField;
    QryRelatorioNO_TURMA: TStringField;
    QryRelatorioco_col_ent_sol: TIntegerField;
    QRLPeriodo: TQRLabel;
    QryRelatorioNU_DCTO_SOLIC: TStringField;
    QRLNoAlu: TQRLabel;
    procedure QRDBText3Print(sender: TObject; var Value: String);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText4Print(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    TotalGrupo : Integer;
    TotalValor : Real;
    codigoEmpresa : String;
    { Public declarations }
  end;

var
  frmRelSolicRealiz: TfrmRelSolicRealiz;

implementation
  uses U_DataModuleSGE, MaskUtils;
{$R *.dfm}

procedure TfrmRelSolicRealiz.QRDBText3Print(sender: TObject;
  var Value: String);
begin
  inherited;

  if Value = 'A' then Value := 'Em Aberto'
  else if Value = 'T' then Value := 'Em Trâmite'
  else if Value = 'F' then Value := 'Finalizada'
  else if Value = 'E' then Value := 'Entregue'
  else if Value = 'C' then Value := 'Cancelada';


end;

procedure TfrmRelSolicRealiz.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  TotalGrupo := TotalGrupo + 1;
end;

procedure TfrmRelSolicRealiz.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLabel6.Caption := IntToStr(TotalGrupo);
  TotalGrupo := 0;
end;

procedure TfrmRelSolicRealiz.QRDBText4Print(sender: TObject;
  var Value: String);
begin
{  inherited;
  if StrToFloat(Value) <= 0 then
  begin
   QRDBText4.Font.Color := clRed;
   Value := 'Isento';
  end
  else
   QRDBText4.Font.Color := clBlack;
}
end;

procedure TfrmRelSolicRealiz.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';
    
  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioco_alu_cad.AsString);

  QrlNis.Caption := QryRelatorioNU_NIS.AsString;

  if not QryRelatorioDE_OBS_SOLI.IsNull then
  begin
    QrlOBS.Caption := QryRelatorioDE_OBS_SOLI.AsString;
  end
  else
  begin
    QrlOBS.Caption := ' - ';
  end;

  if not QryRelatorioLOCALIZACAO.IsNull then
  begin
    QrlLocal.Caption := QryRelatorioLOCALIZACAO.AsString;
  end
  else
  begin
    QrlLocal.Caption := ' - ';
  end;

  QrlSerieTurma.Caption := QryRelatorioCO_SIGL_CUR.AsString + ' / ' + QryRelatorioNO_TURMA.AsString;

  if not QryRelatorioCO_MAT_COL.IsNull then
  begin
    QrlRespSol.Caption := FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString);
  end
  else
  begin
    QrlRespSol.Caption := ' - ';
  end;
  if not QryRelatorioco_col_ent_sol.IsNull then
    with QryRespEnt do
    begin
      Close;
      Sql.Clear;
      Sql.Text := ' SELECT COL.CO_MAT_COL '+
                  ' FROM TB03_COLABOR COL '+
                  //' JOIN TB65_HIST_SOLICIT HIST ON HIST.CO_COL_ENT_SOL = COL.CO_COL AND HIST.CO_EMP = COL.CO_EMP ' +
                  ' WHERE COL.CO_EMP = ' + codigoEmpresa +
                  ' AND COL.CO_COL = ' + QryRelatorioco_col_ent_sol.AsString;
      Open;
      if not IsEmpty then
      begin
        QrlRespEnt.Caption := FormatMaskText('00.000-0;0', FieldByName('CO_MAT_COL').AsString);
      end
      else
        QrlRespEnt.Caption := '-';
    end;


  qrlSolic.Caption := QryRelatorioNU_DCTO_SOLIC.AsString;
  with QryRelatorio do
  begin
    DecodeDate(FieldByName('dt_soli_aten').Value,ano,mes,dia);
    ano := StrToInt(Copy(IntToStr(ano),2,4));
    QRLdt_soli_aten.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);

    DecodeDate(FieldByName('dt_prev_entr').Value,ano,mes,dia);
    ano := StrToInt(Copy(IntToStr(ano),2,4));
    QRLdt_prev_entr.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);

    if not FieldByName('dt_entr_soli').IsNull then
    begin
      DecodeDate(FieldByName('dt_entr_soli').Value,ano,mes,dia);
      ano := StrToInt(Copy(IntToStr(ano),2,4));
      QRLdt_entr_soli.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);
    end
    else
      QRLdt_entr_soli.Caption := '-';
  end;
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('va_soli_aten').IsNull then
    TotalValor := TotalValor + QryRelatorio.FieldByName('va_soli_aten').AsFloat;
//  QRLTotalValor.Caption := FormatFloat('0.00',TotalValor);
end;

procedure TfrmRelSolicRealiz.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  TotalValor := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TfrmRelSolicRealiz]);

end.
