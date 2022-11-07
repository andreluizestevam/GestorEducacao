unit U_FrmRelRendimentoEscolar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QuickRpt, QrAngLbl, QRCtrls, DB, ADODB,
  ExtCtrls;

type
  TfrmRelRendimentoEscolar = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRLabel5: TQRLabel;
    QrlTitCurso: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRDBText5: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRShape4: TQRShape;
    QRLabel11: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRShape12: TQRShape;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    shAp: TQRShape;
    QRLabel19: TQRLabel;
    shDs: TQRShape;
    QRLabel20: TQRLabel;
    shMC: TQRShape;
    QRLabel21: TQRLabel;
    shRp: TQRShape;
    QRShape18: TQRShape;
    QRLabel22: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    qryAux: TADOQuery;
    QRBand3: TQRBand;
    QRLabel25: TQRLabel;
    QRBand4: TQRBand;
    QRDBText12: TQRDBText;
    L1: TQRLabel;
    L2: TQRLabel;
    L4: TQRLabel;
    L3: TQRLabel;
    lblHoras: TQRLabel;
    lblFaltas: TQRLabel;
    lblTotalFaltas: TQRLabel;
    lblTotHrLet: TQRLabel;
    lblSinteseBimestre: TQRLabel;
    lblProvaFinal: TQRLabel;
    lblMediaFinal: TQRLabel;
    qrySQL: TADOQuery;
    QRLabel27: TQRLabel;
    QRLPage: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText4: TQRDBText;
    QRDBText3: TQRDBText;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape7: TQRShape;
    QRLabel12: TQRLabel;
    QRShape8: TQRShape;
    QRLabel26: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRShape9: TQRShape;
    QRLNuNis: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLCiclo: TQRLabel;
    QRShape10: TQRShape;
    QRLFalPer: TQRLabel;
    QRLabel39: TQRLabel;
    QRLTotL1: TQRLabel;
    QRLTotL2: TQRLabel;
    QRLTotL3: TQRLabel;
    QRLTotL4: TQRLabel;
    lblTotSinteseBimestre: TQRLabel;
    lblTotProvaFinal: TQRLabel;
    lblTotMediaFinal: TQRLabel;
    QRLTotFalPer: TQRLabel;
    QrlTurno: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel40: TQRLabel;
    QRDBText7: TQRDBText;
    QRLProfResponsavel: TQRLabel;
    QRShape5: TQRShape;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand3AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
    numMaterias : integer;
    qtdMedia,qtdMB1,qtdMB2,qtdMB3,qtdMB4,qtdNPF,qtdMF : Integer;
    ocototL1,ocototL2,ocototL3,ocototL4,ocototSB,ocototPF,ocototMF : Boolean;
  public
    { Public declarations }
    faltas : integer;
    codigoEmpresa : String;
  end;

var
  frmRelRendimentoEscolar: TfrmRelRendimentoEscolar;

implementation

uses U_DataModuleSGE, DateUtils, MaskUtils;

{$R *.dfm}

procedure TfrmRelRendimentoEscolar.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  sintese: double;
  ocoSintese : boolean;
  qtdNotas : Integer;
begin
  inherited;
  sintese := 0;
  ocoSintese := false;

  qtdNotas := 0;

  if not QryRelatorio.FieldByName('VL_NOTA_BIM1').IsNull then
  begin
    L1.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat,ffNumber,10,2);
    sintese := sintese + QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat;
    qtdNotas := qtdNotas + 1;
    qtdMB1 := qtdMB1 + 1;
    ocoSintese := true;
    ocototL1 := true;
    QRLTotL1.Caption := FloatToStrF(StrToFloat(QRLTotL1.Caption) + QryRelatorio.FieldByName('VL_NOTA_BIM1').AsFloat,ffNumber,10,2);
  end
  else
  begin
    L1.Caption := '-';
    //ocototL1 := false;
  end;
  if not QryRelatorio.FieldByName('VL_NOTA_BIM2').IsNull then
  begin
    L2.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat,ffNumber,10,2);
    sintese := sintese + QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat;
    qtdNotas := qtdNotas + 1;
    qtdMB2 := qtdMB2 + 1;
    ocoSintese := true;
    ocototL2 := true;
    QRLTotL2.Caption := FloatToStrF(StrToFloat(QRLTotL2.Caption) + QryRelatorio.FieldByName('VL_NOTA_BIM2').AsFloat,ffNumber,10,2);
  end
  else
  begin
    L2.Caption := '-';
    //ocototL2 := false;
  end;
  if not QryRelatorio.FieldByName('VL_NOTA_BIM3').IsNull then
  begin
    L3.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_NOTA_BIM3').AsFloat,ffNumber,10,2);
    sintese := sintese + QryRelatorio.FieldByName('VL_NOTA_BIM3').AsFloat;
    qtdNotas := qtdNotas + 1;
    qtdMB3 := qtdMB3 + 1;
    ocoSintese := true;
    ocototL3 := true;
    //ocoSB := true;
    QRLTotL3.Caption := FloatToStrF(StrToFloat(QRLTotL3.Caption) + QryRelatorio.FieldByName('VL_NOTA_BIM3').AsFloat,ffNumber,10,2);
  end
  else
  begin
    L3.Caption := '-';
    //ocototL3 := false;
  end;
  if not QryRelatorio.FieldByName('VL_NOTA_BIM4').IsNull then
  begin
    L4.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_NOTA_BIM4').AsFloat,ffNumber,10,2);
    sintese := sintese + QryRelatorio.FieldByName('VL_NOTA_BIM4').AsFloat;
    qtdNotas := qtdNotas + 1;
    qtdMB4 := qtdMB4 + 1;
    ocototL4 := true;
    ocoSintese := true;
    QRLTotL4.Caption := FloatToStrF(StrToFloat(QRLTotL4.Caption) + QryRelatorio.FieldByName('VL_NOTA_BIM4').AsFloat,ffNumber,10,2);
  end
  else
  begin
    L4.Caption := '-';
    //ocototL4 := false;
  end;

  if not QryRelatorio.FieldByName('VL_MEDIA_FINAL').IsNull then
  begin
    qtdMF := qtdMF + 1;
    ocototMF := true;
    lblMediaFinal.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,2);
    lblTotMediaFinal.Caption := FloatToStrF(StrToFloat(lblTotMediaFinal.Caption) + QryRelatorio.FieldByName('VL_MEDIA_FINAL').AsFloat,ffNumber,10,2);
  end
  else
  begin
    lblMediaFinal.Caption := '-';
    //ocototMF := false;
  end;

  if not QryRelatorio.FieldByName('VL_PROVA_FINAL').IsNull then
  begin
    qtdNPF := qtdNPF + 1;
    ocototPF := false;
    lblProvaFinal.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_PROVA_FINAL').AsFloat,ffNumber,10,2);
    lblTotProvaFinal.Caption := FloatToStrF(StrToFloat(lblTotProvaFinal.Caption) + QryRelatorio.FieldByName('VL_PROVA_FINAL').AsFloat,ffNumber,10,2);
  end
  else
  begin
    lblProvaFinal.Caption := '-';
    //ocototPF := false;
  end;

  if not ocoSintese then
  begin
    lblSinteseBimestre.Caption := '-';
    //ocototSB := false;
  end
  else
  begin
    ocototSB := true;
    qtdMedia := qtdMedia + 1;
    lblSinteseBimestre.Caption := FloatToStrF(sintese/qtdNotas,ffNumber,10,2);
    lblTotSinteseBimestre.Caption := FloatToStrF(StrToFloat(lblTotSinteseBimestre.Caption) + (sintese/qtdNotas),ffNumber,10,2);
  end;

  if not QryRelatorio.FieldByName('qt_carg_hora_mat').IsNull then
  begin
    lblHoras.Caption := QryRelatorio.FieldByName('qt_carg_hora_mat').AsString;
    lblTotHrLet.caption := IntToStr(StrToInt(lblTotHrLet.caption) + QryRelatorio.FieldByName('qt_carg_hora_mat').asinteger);
  end
  else
  begin
    lblHoras.Caption := '-';
  end;

  lblFaltas.Caption := IntToStr(QryRelatorio.FieldByName('qt_falta_bim1').AsInteger + QryRelatorio.FieldByName('qt_falta_bim2').AsInteger +
  QryRelatorio.FieldByName('qt_falta_bim3').AsInteger + QryRelatorio.FieldByName('qt_falta_bim4').AsInteger);
  faltas := faltas + StrToInt(lblFaltas.Caption);

  if not QryRelatorio.FieldByName('qt_carg_hora_mat').IsNull then
  begin
    QRLFalPer.Caption := FloatToStrF((StrToInt(lblFaltas.Caption)*100)/QryRelatorio.FieldByName('qt_carg_hora_mat').AsInteger,ffNumber,10,1);
    QRLTotFalPer.Caption := IntToStr(StrToInt(QRLTotFalPer.Caption) + QryRelatorio.FieldByName('qt_carg_hora_mat').asInteger);
  end
  else
  begin
    QRLFalPer.Caption := '-';
  end;

  numMaterias := numMaterias + 1;
end;

procedure TfrmRelRendimentoEscolar.QRBand3BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  lblTotalFaltas.Caption := IntToStr(faltas);
  
  if numMaterias > 0 then
  begin
    if (ocototL1) and (qtdMB1 > 0) then
      QRLTotL1.Caption := FloatToStrF(StrToFLoat(QRLTotL1.Caption)/qtdMB1,ffnumber,10,2)
    else
      QRLTotL1.Caption := '-';
    if (ocototL2) and (qtdMB2 > 0) then
      QRLTotL2.Caption := FloatToStrF(StrToFLoat(QRLTotL2.Caption)/qtdMB2,ffnumber,10,2)
    else
      QRLTotL2.Caption := '-';
    if (ocototL3) and (qtdMB3 > 0) then
      QRLTotL3.Caption := FloatToStrF(StrToFLoat(QRLTotL3.Caption)/qtdMB3,ffnumber,10,2)
    else
      QRLTotL3.Caption := '-';
    if (ocototL4) and (qtdMB4 > 0) then
      QRLTotL4.Caption := FloatToStrF(StrToFLoat(QRLTotL4.Caption)/qtdMB4,ffnumber,10,2)
    else
      QRLTotL4.Caption := '-';
    if (ocototSB) and (qtdMedia > 0) then
      lblTotSinteseBimestre.Caption := FloatToStrF(StrToFLoat(lblTotSinteseBimestre.Caption)/qtdMedia,ffnumber,10,2)
    else
      lblTotSinteseBimestre.Caption := '-';
    if (ocototPF) and (qtdNPF > 0) then
      lblTotProvaFinal.Caption := FloatToStrF(StrToFLoat(lblTotProvaFinal.Caption)/qtdNPF,ffnumber,10,2)
    else
      lblTotProvaFinal.Caption := '-';
    if (ocototMF) and (qtdMF > 0) then
      lblTotMediaFinal.Caption := FloatToStrF(StrToFLoat(lblTotMediaFinal.Caption)/qtdMF,ffnumber,10,2)
    else
      lblTotMediaFinal.Caption := '-';
    if (lblTotHrLet.Caption <> '-') and (lblTotalFaltas.Caption <> '-') then
      QRLTotFalPer.Caption := FloatToStrF(StrtoFloat(lblTotalFaltas.Caption)*100/StrToFloat(lblTotHrLet.Caption),ffnumber,10,1)
    else
      QRLTotFalPer.Caption := '-';
  end;
end;

procedure TfrmRelRendimentoEscolar.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('Aluno').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('Aluno').AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorio.FieldByName('nu_nis').IsNull then
    QRLNuNis.Caption := QryRelatorio.FieldByName('nu_nis').AsString
  else
    QRLNuNis.Caption := '-';

  if not QryRelatorio.FieldByName('co_alu_cad').IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.FieldByName('co_alu_cad').AsString);

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select c.no_col as professor_resp from tb_respon_materia rm ' +
                ' left join tb03_colabor c on rm.CO_COL_RESP = c.co_col and c.co_emp = rm.co_emp ' +
                'where rm.co_emp = ' + codigoEmpresa +
                ' and rm.co_cur = ' + QryRelatorio.FieldByName('co_cur').AsString +
                ' and rm.co_modu_cur = ' + QryRelatorio.FieldByName('co_modu_cur').AsString +
                ' and rm.co_tur = ' + QryRelatorio.FieldByName('co_tur').AsString +
                ' and rm.CO_ANO_REF = ' + QryRelatorio.FieldByName('Ano').AsString;
    Open;
    if not IsEmpty then
    begin
      if not FieldByName('professor_resp').IsNull then
        QRLProfResponsavel.Caption := FieldByName('professor_resp').AsString;
    end
    else
      QRLProfResponsavel.Caption := '-';
  end;

end;

procedure TfrmRelRendimentoEscolar.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'F' then
    QRLCiclo.Caption := 'Ensino Fundamental';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'M' then
    QRLCiclo.Caption := 'Ensino M�dio';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'G' then
    QRLCiclo.Caption := 'Gradua��o';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'E' then
    QRLCiclo.Caption := 'Especializa��o';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'P' then
    QRLCiclo.Caption := 'P�s-Gradua��o';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'S' then
    QRLCiclo.Caption := 'Mestrado';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'D' then
    QRLCiclo.Caption := 'Doutorado';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'T' then
    QRLCiclo.Caption := 'T�cnico';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'A' then
    QRLCiclo.Caption := 'Forma��o';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'R' then
    QRLCiclo.Caption := 'Preparat�rio';
  if QryRelatorio.FieldByName('co_nivel_cur').AsString = 'O' then
    QRLCiclo.Caption := 'Outros';

  if QryRelatorio.FieldByName('co_sit_mat').AsString = 'C' then
    shMC.Brush.Color := clBlack
  else
    shMC.Brush.Color := clWhite;

  if QryRelatorio.FieldByName('co_sit_mat').AsString = 'T' then
    shDS.Brush.Color := clBlack
  else
    shDS.Brush.Color := clWhite;

  // APROVA��O
  if (QryRelatorio.FieldByName('CO_STA_APROV').AsString = 'A') and (QryRelatorio.FieldByName('CO_STA_APROV_FREQ').AsString = 'A') then
    shAp.Brush.Color := clBlack
  else
    shAp.Brush.Color := clWhite;

  if (QryRelatorio.FieldByName('CO_STA_APROV').AsString = 'R') or (QryRelatorio.FieldByName('CO_STA_APROV_FREQ').AsString = 'R') then
    shRp.Brush.Color := clBlack
  else
    shRp.Brush.Color := clWhite;

  //TURNO
  if QryRelatorio.FieldByName('Turno').AsString = 'M' then
    QrlTurno.Caption := 'Matutino';
  if QryRelatorio.FieldByName('Turno').AsString = 'V' then
    QrlTurno.Caption := 'Vespertino';
  if QryRelatorio.FieldByName('Turno').AsString = 'N' then
    QrlTurno.Caption := 'Noturno';

  qtdMedia := 0;
  qtdMB1 := 0;
  qtdMB2 := 0;
  qtdMB3 := 0;
  qtdMB4 := 0;
  qtdNPF := 0;
  //qtdNPR := 0;
  qtdMF := 0;

  ocototL1 := False;
  ocototL2 := False;
  ocototL3 := False;
  ocototL4 := False;
  ocototSB := False;
  ocototPF := False;
  ocototMF := False;
end;

procedure TfrmRelRendimentoEscolar.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  numMaterias := 0;
  faltas := 0;
  ocototL1 := true;
  ocototL2 := true;
  ocototL3 := true;
  ocototL4 := true;
  ocototSB := true;
  ocototPF := true;
  ocototMF := true;

end;

procedure TfrmRelRendimentoEscolar.QRBand3AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  numMaterias := 0;
  faltas := 0;
  ocototL1 := true;
  ocototL2 := true;
  ocototL3 := true;
  ocototL4 := true;
  ocototSB := true;
  ocototPF := true;
  ocototMF := true;
  QRLTotL1.Caption := '0';
  QRLTotL2.Caption := '0';
  QRLTotL3.Caption := '0';
  QRLTotL4.Caption := '0';
  lblTotSinteseBimestre.Caption := '0';
  lblTotProvaFinal.Caption := '0';
  lblTotMediaFinal.Caption := '0';
  lblTotalFaltas.Caption := '0';
  lblTotHrLet.Caption := '0';
  QRLTotFalPer.Caption := '0';
  qtdMedia := 0;
  qtdMB1 := 0;
  qtdMB2 := 0;
  qtdMB3 := 0;
  qtdMB4 := 0;
  qtdNPF := 0;
  qtdMF := 0;
end;

end.