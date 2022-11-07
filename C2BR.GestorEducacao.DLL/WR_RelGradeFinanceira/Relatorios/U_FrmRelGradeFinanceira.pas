unit U_FrmRelGradeFinanceira;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelGradeFinanceira = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRLabel2: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape20: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRGroup2: TQRGroup;
    QRBand1: TQRBand;
    QRShape10: TQRShape;
    Qrl_VR_MES_01: TQRLabel;
    Qrl_VR_MES_02: TQRLabel;
    Qrl_VR_MES_03: TQRLabel;
    Qrl_VR_MES_04: TQRLabel;
    QRLabel24: TQRLabel;
    Qrl_VR_MES_05: TQRLabel;
    Qrl_VR_MES_06: TQRLabel;
    Qrl_VR_MES_07: TQRLabel;
    Qrl_VR_MES_08: TQRLabel;
    Qrl_VR_MES_09: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    Qrl_VR_MES_10: TQRLabel;
    Qrl_VR_MES_11: TQRLabel;
    Qrl_VR_MES_12: TQRLabel;
    QRLabel18: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioVR_MES_PAR_01: TBCDField;
    QryRelatorioVR_MES_PAR_02: TBCDField;
    QryRelatorioVR_MES_PAR_03: TBCDField;
    QryRelatorioVR_MES_PAR_04: TBCDField;
    QryRelatorioVR_MES_PAR_05: TBCDField;
    QryRelatorioVR_MES_PAR_06: TBCDField;
    QryRelatorioVR_MES_PAR_07: TBCDField;
    QryRelatorioVR_MES_PAR_08: TBCDField;
    QryRelatorioVR_MES_PAR_09: TBCDField;
    QryRelatorioVR_MES_PAR_10: TBCDField;
    QryRelatorioVR_MES_PAR_11: TBCDField;
    QryRelatorioVR_MES_PAR_12: TBCDField;
    QryRelatorioIC_SIT_DOC_01: TStringField;
    QryRelatorioIC_SIT_DOC_02: TStringField;
    QryRelatorioIC_SIT_DOC_03: TStringField;
    QryRelatorioIC_SIT_DOC_04: TStringField;
    QryRelatorioIC_SIT_DOC_05: TStringField;
    QryRelatorioIC_SIT_DOC_06: TStringField;
    QryRelatorioIC_SIT_DOC_07: TStringField;
    QryRelatorioIC_SIT_DOC_08: TStringField;
    QryRelatorioIC_SIT_DOC_09: TStringField;
    QryRelatorioIC_SIT_DOC_10: TStringField;
    QryRelatorioIC_SIT_DOC_11: TStringField;
    QryRelatorioIC_SIT_DOC_12: TStringField;
    Qrl_VR_PAGO: TQRLabel;
    Qrl_VR_ABERTO: TQRLabel;
    QRLNomeMatricula: TQRLabel;
    QRLParametros: TQRLabel;
    QRLMes1: TQRLabel;
    QRLMes2: TQRLabel;
    QRLMes3: TQRLabel;
    QRLMes4: TQRLabel;
    QRLMes5: TQRLabel;
    QRLMes6: TQRLabel;
    QRLMes7: TQRLabel;
    QRLMes8: TQRLabel;
    QRLMes9: TQRLabel;
    QRLMes10: TQRLabel;
    QRLMes11: TQRLabel;
    QRLMes12: TQRLabel;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioVR_TOT_PAG: TBCDField;
    QryRelatorioVR_TOT_PAG_DOC: TBCDField;
    QRDBText1: TQRDBText;
    QRLTotAberto: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QRLabel1: TQRLabel;
    procedure QRDBVR_TOT_PAG_DOCPrint(sender: TObject; var Value: String);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRLMes1Print(sender: TObject; var Value: String);
    procedure QRLMes2Print(sender: TObject; var Value: String);
    procedure QRLMes3Print(sender: TObject; var Value: String);
    procedure QRLMes4Print(sender: TObject; var Value: String);
    procedure QRLMes5Print(sender: TObject; var Value: String);
    procedure QRLMes6Print(sender: TObject; var Value: String);
    procedure QRLMes7Print(sender: TObject; var Value: String);
    procedure QRLMes8Print(sender: TObject; var Value: String);
    procedure QRLMes9Print(sender: TObject; var Value: String);
    procedure QRLMes10Print(sender: TObject; var Value: String);
    procedure QRLMes11Print(sender: TObject; var Value: String);
    procedure QRLMes12Print(sender: TObject; var Value: String);
    procedure QRGroup2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    totVrParSemDescto : Double;
  public
    { Public declarations }
   // ano,modulo,curso : String;
  end;

var
  FrmRelGradeFinanceira: TFrmRelGradeFinanceira;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelGradeFinanceira.QRDBVR_TOT_PAG_DOCPrint(sender: TObject;var Value: String);
begin
  inherited;
  if QryRelatorioVR_TOT_PAG.IsNull = False then
     Value := FormatFloat('#0.00',(QryRelatorioVR_TOT_PAG_DOC.AsFloat - QryRelatorioVR_TOT_PAG.AsFloat));

  if (QryRelatorioVR_TOT_PAG_DOC.IsNull = False) and (QryRelatorioVR_TOT_PAG.IsNull)  then
    Qrl_VR_ABERTO.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_ABERTO.Caption) + QryRelatorioVR_TOT_PAG_DOC.AsFloat)
  else
    Qrl_VR_ABERTO.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_ABERTO.Caption) + (QryRelatorioVR_TOT_PAG_DOC.AsFloat - QryRelatorioVR_TOT_PAG.AsFloat));

end;

procedure TFrmRelGradeFinanceira.QRGroup2BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  (*
    Zebrar o relatório!
  *)
  if QRGroup2.Color = clWhite then
    QRGroup2.Color := $00D8D8D8
  else
    QRGroup2.Color := clWhite;

  QRLNomeMatricula.Caption := UpperCase(QryRelatorioNO_ALU.AsString);

  QRLMes1.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_01.AsFloat);
  QRLMes2.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_02.AsFloat);
  QRLMes3.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_03.AsFloat);
  QRLMes4.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_04.AsFloat);
  QRLMes5.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_05.AsFloat);
  QRLMes6.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_06.AsFloat);
  QRLMes7.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_07.AsFloat);
  QRLMes8.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_08.AsFloat);
  QRLMes9.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_09.AsFloat);
  QRLMes10.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_10.AsFloat);
  QRLMes11.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_11.AsFloat);
  QRLMes12.Caption := FormatFloat('#0.00',QryRelatorioVR_MES_PAR_12.AsFloat);

  //calcula os totais

  if QryRelatorioVR_TOT_PAG.IsNull = False then
    Qrl_VR_PAGO.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_PAGO.Caption) + QryRelatorioVR_TOT_PAG.AsFloat);

  if (QryRelatorioVR_TOT_PAG_DOC.IsNull = False) then
  begin
    QRLTotAberto.Caption := FloatToStrF(QryRelatorioVR_TOT_PAG_DOC.AsFloat - totVrParSemDescto,ffNumber,15,2);
  end
  else
    QRLTotAberto.Caption := '-';

  if QryRelatorioVR_TOT_PAG_DOC.IsNull = False then
    Qrl_VR_ABERTO.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_ABERTO.Caption) + QryRelatorioVR_TOT_PAG_DOC.AsFloat - totVrParSemDescto);
end;

procedure TFrmRelGradeFinanceira.QRGroup1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  Qrl_VR_MES_01.Caption := '0,00';
  Qrl_VR_MES_02.Caption := '0,00';
  Qrl_VR_MES_03.Caption := '0,00';
  Qrl_VR_MES_04.Caption := '0,00';
  Qrl_VR_MES_05.Caption := '0,00';
  Qrl_VR_MES_06.Caption := '0,00';
  Qrl_VR_MES_07.Caption := '0,00';
  Qrl_VR_MES_08.Caption := '0,00';
  Qrl_VR_MES_09.Caption := '0,00';
  Qrl_VR_MES_10.Caption := '0,00';
  Qrl_VR_MES_11.Caption := '0,00';
  Qrl_VR_MES_12.Caption := '0,00';
  Qrl_VR_PAGO.Caption := '0,00';
  Qrl_VR_ABERTO.Caption := '0,00';
end;

procedure TFrmRelGradeFinanceira.QRLMes1Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_01.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_01.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_01.Value;
      Qrl_VR_MES_01.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_01.Caption) + QryRelatorioVR_MES_PAR_01.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_01.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_01.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 1';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_01.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_01.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes2Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_02.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_02.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_02.Value;
      Qrl_VR_MES_02.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_02.Caption) + QryRelatorioVR_MES_PAR_02.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_01.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_02.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 2';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_02.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_02.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes3Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_03.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_03.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_03.Value;
      Qrl_VR_MES_03.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_03.Caption) + QryRelatorioVR_MES_PAR_03.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_03.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_03.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 3';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_03.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_03.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes4Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_04.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_04.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_04.Value;
      Qrl_VR_MES_04.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_04.Caption) + QryRelatorioVR_MES_PAR_04.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_04.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_04.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 4';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_04.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_04.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes5Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_05.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_05.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_05.Value;
      Qrl_VR_MES_05.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_05.Caption) + QryRelatorioVR_MES_PAR_05.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_05.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_05.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 5';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_05.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_05.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes6Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_06.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_06.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_06.Value;
      Qrl_VR_MES_06.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_06.Caption) + QryRelatorioVR_MES_PAR_06.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_06.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_06.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 6';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_06.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_06.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes7Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_07.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_07.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_07.Value;
      Qrl_VR_MES_07.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_07.Caption) + QryRelatorioVR_MES_PAR_07.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_07.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_07.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 7';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_07.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_07.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes8Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_08.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_08.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_08.Value;
      Qrl_VR_MES_08.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_08.Caption) + QryRelatorioVR_MES_PAR_08.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_08.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_08.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 8';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_08.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_08.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes9Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_09.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_09.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_09.Value;
      Qrl_VR_MES_09.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_09.Caption) + QryRelatorioVR_MES_PAR_09.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_09.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_09.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 9';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_09.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_09.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes10Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_10.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_10.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_10.Value;
      Qrl_VR_MES_10.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_10.Caption) + QryRelatorioVR_MES_PAR_10.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_10.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_10.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 10';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_10.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_10.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes11Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_11.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_11.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_11.Value;
      Qrl_VR_MES_11.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_11.Caption) + QryRelatorioVR_MES_PAR_11.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_11.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_11.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 11';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_11.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_11.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRLMes12Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if QryRelatorioIC_SIT_DOC_12.AsString = 'A' then
  begin
    if QryRelatorioVR_MES_PAR_12.IsNull = False then
    begin
      //totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_12.Value;
      Qrl_VR_MES_12.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_12.Caption) + QryRelatorioVR_MES_PAR_12.AsFloat);
    end;
  end;
  if QryRelatorioIC_SIT_DOC_12.AsString = 'Q' then
  begin
    totVrParSemDescto := totVrParSemDescto + QryRelatorioVR_MES_PAR_12.Value;
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select vr_pag from tb47_cta_receb ' +
                  'where co_emp = ' + QryRelatorioCO_EMP.AsString +
                  ' and co_alu = ' + QryRelatorioCO_ALU.AsString +
                  ' and ic_sit_doc = ' + QuotedStr('Q') +
                  ' and month(DT_VEN_DOC) = 12';
      Open;
      if not IsEmpty then
      begin
        Value := FormatFloat('#0.00',FieldByName('vr_pag').asFloat);
        Qrl_VR_MES_12.Caption := FormatFloat('#0.00',StrToFloat(Qrl_VR_MES_12.Caption) + StrToFloat(Value));
      end;
    end;
  end;
end;

procedure TFrmRelGradeFinanceira.QRGroup2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  totVrParSemDescto := 0;
end;

procedure TFrmRelGradeFinanceira.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  Qrl_VR_MES_01.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_01.Caption),ffNumber,15,2);
  Qrl_VR_MES_02.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_02.Caption),ffNumber,15,2);
  Qrl_VR_MES_03.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_03.Caption),ffNumber,15,2);
  Qrl_VR_MES_04.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_04.Caption),ffNumber,15,2);
  Qrl_VR_MES_05.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_05.Caption),ffNumber,15,2);
  Qrl_VR_MES_06.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_06.Caption),ffNumber,15,2);
  Qrl_VR_MES_07.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_07.Caption),ffNumber,15,2);
  Qrl_VR_MES_08.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_08.Caption),ffNumber,15,2);
  Qrl_VR_MES_09.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_09.Caption),ffNumber,15,2);
  Qrl_VR_MES_10.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_10.Caption),ffNumber,15,2);
  Qrl_VR_MES_11.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_11.Caption),ffNumber,15,2);
  Qrl_VR_MES_12.Caption := FloatToStrF(StrToFloat(Qrl_VR_MES_12.Caption),ffNumber,15,2);
  Qrl_VR_PAGO.Caption := FloatToStrF(StrToFloat(Qrl_VR_PAGO.Caption),ffNumber,15,2);
  Qrl_VR_ABERTO.Caption := FloatToStrF(StrToFloat(Qrl_VR_ABERTO.Caption),ffNumber,15,2);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelGradeFinanceira]);

end.


// QRDBVR_TOT_PAG_DOCPrint
