unit U_FrmRelEspenhoDiscCursadas;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelEspenhoDiscCursadas = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRLPage: TQRLabel;
    QRGroup1: TQRGroup;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRBand2: TQRBand;
    QRLabel18: TQRLabel;
    QRLTotMB1: TQRLabel;
    QRLTotMB2: TQRLabel;
    QRLTotMB3: TQRLabel;
    QRLTotMB4: TQRLabel;
    QRLTotMED: TQRLabel;
    QRLTotMF: TQRLabel;
    QRLTotFaltas: TQRLabel;
    QRLTotStatus: TQRLabel;
    QRDBText1: TQRDBText;
    QrlMatricula: TQRLabel;
    QRDBText2: TQRDBText;
    QRLMED: TQRLabel;
    QRLFaltas: TQRLabel;
    QRLStatus: TQRLabel;
    QRLNPR: TQRLabel;
    QRLMB1: TQRLabel;
    QRLMB2: TQRLabel;
    QRLMB3: TQRLabel;
    QRLMB4: TQRLabel;
    QRLNPF: TQRLabel;
    QRLMF: TQRLabel;
    QRLTotNPR: TQRLabel;
    QRLTotNPF: TQRLabel;
    QrlTitSerie: TQRLabel;
    QRLabel19: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRLALuno: TQRLabel;
    QRShape2: TQRShape;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioCO_MODU_CUR: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_ANO_REF: TStringField;
    QryRelatorioCO_MAT: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioDT_LANC: TDateTimeField;
    QryRelatorioQT_FALTA_MES1: TIntegerField;
    QryRelatorioVL_NOTA_MES1: TBCDField;
    QryRelatorioVL_RECU_MES1: TBCDField;
    QryRelatorioQT_FALTA_MES2: TIntegerField;
    QryRelatorioVL_NOTA_MES2: TBCDField;
    QryRelatorioVL_RECU_MES2: TBCDField;
    QryRelatorioQT_FALTA_MES3: TIntegerField;
    QryRelatorioVL_NOTA_MES3: TBCDField;
    QryRelatorioVL_RECU_MES3: TBCDField;
    QryRelatorioQT_FALTA_MES4: TIntegerField;
    QryRelatorioVL_NOTA_MES4: TBCDField;
    QryRelatorioVL_RECU_MES4: TBCDField;
    QryRelatorioQT_FALTA_MES5: TIntegerField;
    QryRelatorioVL_NOTA_MES5: TBCDField;
    QryRelatorioVL_RECU_MES5: TBCDField;
    QryRelatorioQT_FALTA_MES6: TIntegerField;
    QryRelatorioVL_NOTA_MES6: TBCDField;
    QryRelatorioVL_RECU_MES6: TBCDField;
    QryRelatorioQT_FALTA_MES7: TIntegerField;
    QryRelatorioVL_NOTA_MES7: TBCDField;
    QryRelatorioVL_RECU_MES7: TBCDField;
    QryRelatorioQT_FALTA_MES8: TIntegerField;
    QryRelatorioVL_NOTA_MES8: TBCDField;
    QryRelatorioVL_RECU_MES8: TBCDField;
    QryRelatorioQT_FALTA_MES9: TIntegerField;
    QryRelatorioVL_NOTA_MES9: TBCDField;
    QryRelatorioVL_RECU_MES9: TBCDField;
    QryRelatorioQT_FALTA_MES10: TIntegerField;
    QryRelatorioVL_NOTA_MES10: TBCDField;
    QryRelatorioVL_RECU_MES10: TBCDField;
    QryRelatorioQT_FALTA_MES11: TIntegerField;
    QryRelatorioVL_NOTA_MES11: TBCDField;
    QryRelatorioVL_RECU_MES11: TBCDField;
    QryRelatorioQT_FALTA_MES12: TIntegerField;
    QryRelatorioVL_NOTA_MES12: TBCDField;
    QryRelatorioVL_RECU_MES12: TBCDField;
    QryRelatorioQT_FALTA_BIM1: TIntegerField;
    QryRelatorioVL_NOTA_BIM1: TBCDField;
    QryRelatorioVL_RECU_BIM1: TBCDField;
    QryRelatorioQT_FALTA_BIM2: TIntegerField;
    QryRelatorioVL_NOTA_BIM2: TBCDField;
    QryRelatorioVL_RECU_BIM2: TBCDField;
    QryRelatorioQT_FALTA_BIM3: TIntegerField;
    QryRelatorioVL_NOTA_BIM3: TBCDField;
    QryRelatorioVL_RECU_BIM3: TBCDField;
    QryRelatorioQT_FALTA_BIM4: TIntegerField;
    QryRelatorioVL_NOTA_BIM4: TBCDField;
    QryRelatorioVL_RECU_BIM4: TBCDField;
    QryRelatorioQT_FALTA_SEM1: TIntegerField;
    QryRelatorioVL_NOTA_SEM1: TBCDField;
    QryRelatorioVL_RECU_SEM1: TBCDField;
    QryRelatorioQT_FALTA_SEM2: TIntegerField;
    QryRelatorioVL_NOTA_SEM2: TBCDField;
    QryRelatorioVL_RECU_SEM2: TBCDField;
    QryRelatorioVL_MEDIA_ANUAL: TBCDField;
    QryRelatorioVL_PROVA_FINAL: TBCDField;
    QryRelatorioVL_MEDIA_FINAL: TBCDField;
    QryRelatorioVL_CRIT_MES1: TStringField;
    QryRelatorioVL_RESP_MES1: TStringField;
    QryRelatorioVL_APRE_MES1: TStringField;
    QryRelatorioVL_CRIT_MES2: TStringField;
    QryRelatorioVL_RESP_MES2: TStringField;
    QryRelatorioVL_APRE_MES2: TStringField;
    QryRelatorioVL_CRIT_MES3: TStringField;
    QryRelatorioVL_RESP_MES3: TStringField;
    QryRelatorioVL_APRE_MES3: TStringField;
    QryRelatorioVL_CRIT_MES4: TStringField;
    QryRelatorioVL_RESP_MES4: TStringField;
    QryRelatorioVL_APRE_MES4: TStringField;
    QryRelatorioVL_CRIT_MES5: TStringField;
    QryRelatorioVL_RESP_MES5: TStringField;
    QryRelatorioVL_APRE_MES5: TStringField;
    QryRelatorioVL_CRIT_MES6: TStringField;
    QryRelatorioVL_RESP_MES6: TStringField;
    QryRelatorioVL_APRE_MES6: TStringField;
    QryRelatorioVL_CRIT_MES7: TStringField;
    QryRelatorioVL_RESP_MES7: TStringField;
    QryRelatorioVL_APRE_MES7: TStringField;
    QryRelatorioVL_CRIT_MES8: TStringField;
    QryRelatorioVL_RESP_MES8: TStringField;
    QryRelatorioVL_APRE_MES8: TStringField;
    QryRelatorioVL_CRIT_MES9: TStringField;
    QryRelatorioVL_RESP_MES9: TStringField;
    QryRelatorioVL_APRE_MES9: TStringField;
    QryRelatorioVL_CRIT_MES10: TStringField;
    QryRelatorioVL_RESP_MES10: TStringField;
    QryRelatorioVL_APRE_MES10: TStringField;
    QryRelatorioVL_CRIT_MES11: TStringField;
    QryRelatorioVL_RESP_MES11: TStringField;
    QryRelatorioVL_APRE_MES11: TStringField;
    QryRelatorioVL_CRIT_MES12: TStringField;
    QryRelatorioVL_RESP_MES12: TStringField;
    QryRelatorioVL_APRE_MES12: TStringField;
    QryRelatorioVL_CRIT_BIM1: TStringField;
    QryRelatorioVL_RESP_BIM1: TStringField;
    QryRelatorioVL_APRE_BIM1: TStringField;
    QryRelatorioVL_CRIT_BIM2: TStringField;
    QryRelatorioVL_RESP_BIM2: TStringField;
    QryRelatorioVL_APRE_BIM2: TStringField;
    QryRelatorioVL_CRIT_BIM3: TStringField;
    QryRelatorioVL_RESP_BIM3: TStringField;
    QryRelatorioVL_APRE_BIM3: TStringField;
    QryRelatorioVL_CRIT_BIM4: TStringField;
    QryRelatorioVL_RESP_BIM4: TStringField;
    QryRelatorioVL_APRE_BIM4: TStringField;
    QryRelatorioVL_CRIT_SEM1: TStringField;
    QryRelatorioVL_RESP_SEM1: TStringField;
    QryRelatorioVL_APRE_SEM1: TStringField;
    QryRelatorioVL_CRIT_SEM2: TStringField;
    QryRelatorioVL_RESP_SEM2: TStringField;
    QryRelatorioVL_APRE_SEM2: TStringField;
    QryRelatorioCO_USUARIO: TIntegerField;
    QryRelatorioCO_STA_APROV_MATERIA: TStringField;
    QryRelatorioASAC_1S_I1: TStringField;
    QryRelatorioASAC_1S_I2: TStringField;
    QryRelatorioASAC_1S_I3: TStringField;
    QryRelatorioASAC_1S_I4: TStringField;
    QryRelatorioASAC_1S_I5: TStringField;
    QryRelatorioASAC_1S_I6: TStringField;
    QryRelatorioASAC_1S_I7: TStringField;
    QryRelatorioASAC_1S_I8: TStringField;
    QryRelatorioASAC_1S_I9: TStringField;
    QryRelatorioASAC_1S_I10: TStringField;
    QryRelatorioASAC_1S_I11: TStringField;
    QryRelatorioASAC_1S_I12: TStringField;
    QryRelatorioASAC_2S_I1: TStringField;
    QryRelatorioASAC_2S_I2: TStringField;
    QryRelatorioASAC_2S_I3: TStringField;
    QryRelatorioASAC_2S_I4: TStringField;
    QryRelatorioASAC_2S_I5: TStringField;
    QryRelatorioASAC_2S_I6: TStringField;
    QryRelatorioASAC_2S_I7: TStringField;
    QryRelatorioASAC_2S_I8: TStringField;
    QryRelatorioASAC_2S_I9: TStringField;
    QryRelatorioASAC_2S_I10: TStringField;
    QryRelatorioASAC_2S_I11: TStringField;
    QryRelatorioASAC_2S_I12: TStringField;
    QryRelatorioAC_1S: TStringField;
    QryRelatorioAC_2S: TStringField;
    QryRelatorioOBS_1S: TStringField;
    QryRelatorioOBS_2S: TStringField;
    QryRelatorioAM_1S: TStringField;
    QryRelatorioAM_2S: TStringField;
    QryRelatorioRS1: TStringField;
    QryRelatorioRS2: TStringField;
    QryRelatorioRSF: TStringField;
    QryRelatorioDT_INI_AVAL_PRO_1S: TDateTimeField;
    QryRelatorioDT_FIN_AVAL_PRO_1S: TDateTimeField;
    QryRelatorioDT_INI_AVAL_PRO_2S: TDateTimeField;
    QryRelatorioDT_FIN_AVAL_PRO_2S: TDateTimeField;
    QryRelatorioDT_LANC2: TDateTimeField;
    QryRelatorioNO_MATERIA: TStringField;
    QryRelatorioco_sit_mat: TStringField;
    QryRelatorioNO_SIGLA_MATERIA: TStringField;
    QryRelatorioQT_CARG_HORA_MAT: TIntegerField;
    QryRelatorioQT_CRED_MAT: TIntegerField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QryRelatoriono_tur: TStringField;
    QryRelatorioVL_MEDIA_FINAL_1: TBCDField;
    QryRelatoriono_alu: TStringField;
    QryRelatorionu_nis: TBCDField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatorioCO_SIGL_CUR: TStringField;
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    ctMat,qtdMedia,qtdMB1,qtdMB2,qtdMB3,qtdMB4,qtdNPF,qtdNPR,qtdMF : Integer;
    ocoMB1,ocoMB2,ocoMB3,ocoMB4,ocoNPF,ocoNPR,ocoMF,ocoMED : Boolean;
    media : Double;
  public
    { Public declarations }
  end;

var
  FrmRelEspenhoDiscCursadas: TFrmRelEspenhoDiscCursadas;

implementation

uses U_DataModuleSGE, U_Funcoes, MaskUtils;

{$R *.dfm}

procedure TFrmRelEspenhoDiscCursadas.QRDBText2Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := Value + 'º';
end;

procedure TFrmRelEspenhoDiscCursadas.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  qtdNotas : Integer;
begin
  inherited;
  qtdNotas := 0;
  media := 0;

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString);


  QRLFaltas.Caption := IntToStr(QryRelatorioQT_FALTA_BIM1.AsInteger + QryRelatorioQT_FALTA_BIM2.AsInteger +
  QryRelatorioQT_FALTA_BIM3.AsInteger + QryRelatorioQT_FALTA_BIM4.AsInteger);

  if QryRelatorioco_sit_mat.AsString = 'A' then
  begin
    QRLStatus.Caption := 'CURSANDO';
    QRLTotStatus.Caption := 'CURSANDO';
  end;
  
  if QryRelatorioco_sit_mat.AsString = 'F' then
  begin
  if not QryRelatorioVL_MEDIA_FINAL.IsNull then
    if QryRelatorioVL_MEDIA_FINAL.AsFloat >= 5 then
      QRLStatus.Caption := 'APROVADO'
    else
    begin
      QRLStatus.Caption := 'REPROVADO';
      QRLTotStatus.Caption := 'REPROVADO';
    end;
  end;

  ctMat := ctMat + 1;

  if not QryRelatorioVL_NOTA_BIM1.IsNull then
  begin
    qtdNotas := qtdNotas + 1;
    qtdMB1 := qtdMB1 + 1;
    media := media + QryRelatorioVL_NOTA_BIM1.AsFloat;
    QRLMB1.Caption := FloatToStrF(QryRelatorioVL_NOTA_BIM1.AsFloat,ffNumber,10,2);
    QRLMB1.Alignment := taRightJustify;
    ocoMB1 := true;
  end
  else
  begin
    QRLMB1.Caption := '-';
    QRLMB1.Alignment := taCenter;
    //ocoMB1 := false;
  end;

  if not QryRelatorioVL_NOTA_BIM2.IsNull then
  begin
    qtdNotas := qtdNotas + 1;
    qtdMB2 := qtdMB2 + 1;
    media := media + QryRelatorioVL_NOTA_BIM2.AsFloat;
    QRLMB2.Caption := FloatToStrF(QryRelatorioVL_NOTA_BIM2.AsFloat,ffNumber,10,2);
    QRLMB2.Alignment := taRightJustify;
    ocoMB2 := true;
  end
  else
  begin
    QRLMB2.Caption := '-';
    QRLMB2.Alignment := taCenter;
    //ocoMB2 := false;
  end;

  if not QryRelatorioVL_NOTA_BIM3.IsNull then
  begin
    qtdNotas := qtdNotas + 1;
    qtdMB3 := qtdMB3 + 1;
    media := media + QryRelatorioVL_NOTA_BIM3.AsFloat;
    QRLMB3.Caption := FloatToStrF(QryRelatorioVL_NOTA_BIM3.AsFloat,ffNumber,10,2);
    QRLMB3.Alignment := taRightJustify;
    ocoMB3 := true;
  end
  else
  begin
    QRLMB3.Caption := '-';
    QRLMB3.Alignment := taCenter;
    //ocoMB3 := false;
  end;

  if not QryRelatorioVL_NOTA_BIM4.IsNull then
  begin
    qtdNotas := qtdNotas + 1;
    qtdMB4 := qtdMB4 + 1;
    media := media + QryRelatorioVL_NOTA_BIM4.AsFloat;
    QRLMB4.Caption := FloatToStrF(QryRelatorioVL_NOTA_BIM4.AsFloat,ffNumber,10,2);
    QRLMB4.Alignment := taRightJustify;
    ocoMB4 := true;
  end
  else
  begin
    QRLMB4.Caption := '-';
    QRLMB4.Alignment := taCenter;
    //ocoMB4 := false;
  end;

  if not QryRelatorioVL_PROVA_FINAL.IsNull then
  begin
    qtdNPF := qtdNPF + 1;
    QRLNPF.Caption := FloatToStrF(QryRelatorioVL_PROVA_FINAL.AsFloat,ffNumber,10,2);
    QRLNPF.Alignment := taRightJustify;
    ocoNPF := true;
  end
  else
  begin
    QRLNPF.Caption := '-';
    QRLNPF.Alignment := taCenter;
    //ocoNPF := false;
  end;

  if not QryRelatorioVL_MEDIA_FINAL.IsNull then
  begin
    qtdMF := qtdMF + 1;
    QRLMF.Caption := FloatToStrF(QryRelatorioVL_MEDIA_FINAL.AsFloat,ffNumber,10,2);
    QRLMF.Alignment := taRightJustify;
    ocoMF := true;
  end
  else
  begin
    QRLMF.Caption := '-';
    QRLMF.Alignment := taCenter;
    //ocoMF := false;
  end;

  if qtdNotas > 0 then
  begin
    QRLMED.Caption := FloatToStrF(media/qtdNotas,ffNumber,10,2);
    QRLMED.Alignment := taRightJustify;
    ocoMED := true;
  end
  else
  begin
    QRLMED.Caption := '-';
    QRLMED.Alignment := taCenter;
  end;

  QRLTotMB1.Caption := FloatToStr(StrToFloat(QRLTotMB1.Caption) + QryRelatorioVL_NOTA_BIM1.asFloat);
  QRLTotMB2.Caption := FloatToStr(StrToFloat(QRLTotMB2.Caption) + QryRelatorioVL_NOTA_BIM2.asFloat);
  QRLTotMB3.Caption := FloatToStr(StrToFloat(QRLTotMB3.Caption) + QryRelatorioVL_NOTA_BIM3.asFloat);
  QRLTotMB4.Caption := FloatToStr(StrToFloat(QRLTotMB4.Caption) + QryRelatorioVL_NOTA_BIM4.asFloat);

  QRLTotNPF.Caption := FloatToStr(StrToFloat(QRLTotNPF.Caption) + QryRelatorioVL_PROVA_FINAL.asFloat);

  if QRLMED.Caption <> '-' then
  begin
    QRLTotMED.Caption := FloatToStrF(StrToFloat(QRLTotMED.Caption) + StrToFloat(QRLMED.Caption),ffNumber,10,2);
    qtdMedia := qtdMedia + 1;
  end;

  QRLTotMF.Caption := FloatToStr(StrToFloat(QRLTotMF.Caption) + QryRelatorioVL_MEDIA_FINAL.asFloat);
  if (QRLTotFaltas.Caption <> '') and (QRLTotFaltas.Caption <> '-') then
  begin
    QRLTotFaltas.Caption :=  FloatToStr(StrToFloat(QRLTotFaltas.Caption) + StrToFloat(QRLFaltas.Caption));
  end;
end;

procedure TFrmRelEspenhoDiscCursadas.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorionu_nis.IsNull then
    QRLALuno.Caption := UpperCase(QryRelatorioNO_ALU.asstring) + ' (Nº NIS '+ QryRelatorionu_nis.AsString + ')'
  else
    QRLALuno.Caption := UpperCase(QryRelatorioNO_ALU.asstring);
end;

procedure TFrmRelEspenhoDiscCursadas.QRBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  if ctMat > 0 then
  begin
    if (ocoMB1) and (qtdMB1 > 0)  then
    begin
      QRLTotMB1.Caption := FloatToStrF(StrToFloat(QRLTotMB1.Caption)/qtdMB1,ffNumber,10,2);
      QRLTotMB1.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMB1.Caption := '-';
      QRLTotMB1.Alignment := taCenter;
    end;
    if (ocoMB2) and (qtdMB2 > 0) then
    begin
      QRLTotMB2.Caption := FloatToStrF(StrToFloat(QRLTotMB2.Caption)/qtdMB2,ffNumber,10,2);
      QRLTotMB2.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMB2.Caption := '-';
      QRLTotMB2.Alignment := taCenter;
    end;
    if (ocoMB3) and (qtdMB3 > 0) then
    begin
      QRLTotMB3.Caption := FloatToStrF(StrToFloat(QRLTotMB3.Caption)/qtdMB3,ffNumber,10,2);
      QRLTotMB3.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMB3.Caption := '-';
      QRLTotMB3.Alignment := taCenter;
    end;
    if (ocoMB4) and (qtdMB4 > 0) then
    begin
      QRLTotMB4.Caption := FloatToStrF(StrToFloat(QRLTotMB4.Caption)/qtdMB4,ffNumber,10,2);
      QRLTotMB4.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMB4.Caption := '-';
      QRLTotMB4.Alignment := taCenter;
    end;

    if (ocoMED) and (qtdMedia > 0) then
    begin
      QRLTotMED.Caption := FloatToStrF(StrToFloat(QRLTotMED.Caption)/qtdMedia,ffNumber,10,2);
      QRLTotMED.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMED.Caption := '-';
      QRLTotMED.Alignment := taCenter;
    end;

    if (ocoMF) and (qtdMF > 0) then
    begin
      QRLTotMF.Caption := FloatToStrF(StrToFloat(QRLTotMF.Caption)/qtdMF,ffNumber,10,2);
      QRLTotMF.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotMF.Caption := '-';
      QRLTotMF.Alignment := taCenter;
    end;

    if (ocoNPF) and (qtdNPF > 0) then
    begin
      QRLTotNPF.Caption := FloatToStrF(StrToFloat(QRLTotNPF.Caption)/qtdNPF,ffNumber,10,2);
      QRLTotNPF.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotNPF.Caption := '-';
      QRLTotNPF.Alignment := taCenter;
    end;

   { if qtdFaltas > 0 then
    begin
      QRLTotFaltas.Caption := FloatToStrF(StrToint(QRLTotFaltas.Caption)/qtdFaltas,ffNumber,10,2);
      QRLTotFaltas.Alignment := taRightJustify;
    end
    else
    begin
      QRLTotFaltas.Caption := '-';
      QRLTotFaltas.Alignment := taCenter;
    end;  }
    
    //Verificar depois
      QRLTotNPR.Caption := '-';
      QRLTotNPR.Alignment := taCenter;
    //
  end;
end;

procedure TFrmRelEspenhoDiscCursadas.QRBand2AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  ocoMED := false;
  ocoMB1 := false;
  ocoMB2 := false;
  ocoMB3 := false;
  ocoMB4 := false;
  ocoNPF := false;
  ocoNPR := false;
  ocoMF := false;
  qtdMB1 := 0;
  qtdMB2 := 0;
  qtdMB3 := 0;
  qtdMB4 := 0;
  qtdNPF := 0;
  qtdNPR := 0;
  qtdMF := 0;
  qtdMedia := 0;

  QRLTotMB1.Caption := '0';
  QRLTotMB2.Caption := '0';
  QRLTotMB3.Caption := '0';
  QRLTotMB4.Caption := '0';
  QRLTotMED.Caption := '0';
  QRLTotMF.Caption := '0';
  QRLTotFaltas.Caption := '0';
  QRLTotNPR.Caption := '0';
  QRLTotNPF.Caption := '0';
  ctMat := 0;
end;

procedure TFrmRelEspenhoDiscCursadas.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  qtdMB1 := 0;
  qtdMB2 := 0;
  qtdMB3 := 0;
  qtdMB4 := 0;
  qtdNPF := 0;
  qtdNPR := 0;
  qtdMF := 0;
  qtdMedia := 0;

  QRLTotMB1.Caption := '0';
  QRLTotMB2.Caption := '0';
  QRLTotMB3.Caption := '0';
  QRLTotMB4.Caption := '0';
  QRLTotMED.Caption := '0';
  QRLTotMF.Caption := '0';
  QRLTotFaltas.Caption := '0';
  QRLTotNPR.Caption := '0';
  QRLTotNPF.Caption := '0';
  ctMat := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelEspenhoDiscCursadas]);

end.
