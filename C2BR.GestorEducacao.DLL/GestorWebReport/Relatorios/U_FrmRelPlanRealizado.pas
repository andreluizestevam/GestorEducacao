unit U_FrmRelPlanRealizado;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelPlanRealizado = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLabel4: TQRLabel;
    QRBand1: TQRBand;
    QRLTitPlan: TQRLabel;
    QRDBText1: TQRDBText;
    QRBand2: TQRBand;
    qrlTotAnualTipo: TQRLabel;
    QRDBPlano: TQRDBText;
    QRBand3: TQRBand;
    qrlTotRel1: TQRLabel;
    qrlTotRel2: TQRLabel;
    qrlTotRel3: TQRLabel;
    qrlTotRel4: TQRLabel;
    qrlTotRel5: TQRLabel;
    qrlTotRel6: TQRLabel;
    qrlTotRel7: TQRLabel;
    qrlTotRel8: TQRLabel;
    qrlTotRel9: TQRLabel;
    qrlTotRel10: TQRLabel;
    qrlTotRel11: TQRLabel;
    qrlTotRel12: TQRLabel;
    qrlTotal: TQRLabel;
    QRLabel33: TQRLabel;
    QRShapeTotal: TQRShape;
    QRShape1: TQRShape;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText7: TQRDBText;
    QRDBText8: TQRDBText;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText11: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText16: TQRDBText;
    QryAux: TADOQuery;
    QRDBText17: TQRDBText;
    QRLTitRealiz: TQRLabel;
    QRLabel16: TQRLabel;
    qrlParcial1: TQRLabel;
    qrlParcial2: TQRLabel;
    qrlParcial3: TQRLabel;
    qrlParcial4: TQRLabel;
    qrlParcial5: TQRLabel;
    qrlParcial6: TQRLabel;
    qrlParcial7: TQRLabel;
    qrlParcial8: TQRLabel;
    qrlParcial9: TQRLabel;
    qrlParcial10: TQRLabel;
    qrlParcial11: TQRLabel;
    qrlParcial12: TQRLabel;
    QRLabel15: TQRLabel;
    QRLPage: TQRLabel;
    QRLParametros: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLNoConta: TQRLabel;
    m1: TQRDBText;
    m2: TQRDBText;
    m3: TQRDBText;
    m4: TQRDBText;
    m5: TQRDBText;
    m6: TQRDBText;
    m7: TQRDBText;
    m8: TQRDBText;
    m9: TQRDBText;
    m10: TQRDBText;
    m11: TQRDBText;
    m12: TQRDBText;
    qrlTotReal: TQRDBText;
    QRLD1: TQRLabel;
    QRLD2: TQRLabel;
    QRLD3: TQRLabel;
    QRLD4: TQRLabel;
    QRLD5: TQRLabel;
    QRLD6: TQRLabel;
    QRLD7: TQRLabel;
    QRLD8: TQRLabel;
    QRLD9: TQRLabel;
    QRLD10: TQRLabel;
    QRLD11: TQRLabel;
    QRLD12: TQRLabel;
    QRLDTot: TQRLabel;
    QryRelatorioCO_SEQU_PC: TAutoIncField;
    QryRelatorioCO_GRUP_CTA: TIntegerField;
    QryRelatorioCO_SGRUP_CTA: TIntegerField;
    QryRelatorioCO_CONTA_PC: TIntegerField;
    QryRelatorioNU_CONTA_PC: TIntegerField;
    QryRelatorioDE_CONTA_PC: TStringField;
    QryRelatorioNOM_USUARIO: TStringField;
    QryRelatorioDT_ALT_REGISTRO: TDateTimeField;
    QryRelatorioDE_GRUP_CTA: TStringField;
    QryRelatorioTP_GRUP_CTA: TStringField;
    QryRelatorioID_PLANEJ_FINAN: TAutoIncField;
    QryRelatorioCO_ANO_REF: TIntegerField;
    QryRelatorioORG_CODIGO_ORGAO: TIntegerField;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioID_DOTAC_ORCAM: TIntegerField;
    QryRelatorioCO_SEQU_PC_1: TIntegerField;
    QryRelatorioCO_CENT_CUSTO: TIntegerField;
    QryRelatorioVL_PLAN_MES1: TBCDField;
    QryRelatorioVL_PLAN_MES2: TBCDField;
    QryRelatorioVL_PLAN_MES3: TBCDField;
    QryRelatorioVL_PLAN_MES4: TBCDField;
    QryRelatorioVL_PLAN_MES5: TBCDField;
    QryRelatorioVL_PLAN_MES6: TBCDField;
    QryRelatorioVL_PLAN_MES7: TBCDField;
    QryRelatorioVL_PLAN_MES8: TBCDField;
    QryRelatorioVL_PLAN_MES9: TBCDField;
    QryRelatorioVL_PLAN_MES10: TBCDField;
    QryRelatorioVL_PLAN_MES11: TBCDField;
    QryRelatorioVL_PLAN_MES12: TBCDField;
    QryRelatorioVL_REAL_MES_1: TBCDField;
    QryRelatorioVL_REAL_MES_2: TBCDField;
    QryRelatorioVL_REAL_MES_3: TBCDField;
    QryRelatorioVL_REAL_MES_4: TBCDField;
    QryRelatorioVL_REAL_MES_5: TBCDField;
    QryRelatorioVL_REAL_MES_6: TBCDField;
    QryRelatorioVL_REAL_MES_7: TBCDField;
    QryRelatorioVL_REAL_MES_8: TBCDField;
    QryRelatorioVL_REAL_MES_9: TBCDField;
    QryRelatorioVL_REAL_MES_10: TBCDField;
    QryRelatorioVL_REAL_MES_11: TBCDField;
    QryRelatorioVL_REAL_MES_12: TBCDField;
    QryRelatorioDT_CADASTRO: TDateTimeField;
    QryRelatorioCO_EMP_CADAS: TIntegerField;
    QryRelatorioCO_COL_CADAS: TIntegerField;
    QryRelatorioCO_EMP_STATUS: TIntegerField;
    QryRelatorioCO_COL_STATUS: TIntegerField;
    QryRelatorioCO_STATUS: TStringField;
    QryRelatorioDT_STATUS: TDateTimeField;
    QryRelatorioVL_PLAN_TOTAL: TBCDField;
    QryRelatorioVL_REAL_TOTAL: TBCDField;
    QryRelatorioDE_SGRUP_CTA: TStringField;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText1Print(sender: TObject; var Value: String);
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    TotalParcial, TotalRelatorio : array[1..12] of double;
    TotalP, TotalR : Double;
  public
    { Public declarations }
    AnoInicio, AnoFim: String;
    TipoRelatorio, TipoVisualizacao: Integer;
    
  end;

var
  FrmRelPlanRealizado: TFrmRelPlanRealizado;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelPlanRealizado.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i: integer;
  Tot, totSintetico: double;
  Campo, Campo2: String;
begin
  inherited;
  QRLNoConta.Caption := FormatFloat('#0', QryRelatorio.FieldByName('CO_GRUP_CTA').AsFloat) + '.' +
                        FormatFloat('##00', QryRelatorio.FieldByName('CO_SGRUP_CTA').AsFloat) + '.' +
                        FormatFloat('###000', QryRelatorio.FieldByName('CO_CONTA_PC').AsFloat) + ' - ' +
                        QryRelatorioDE_CONTA_PC.AsString;

  totSintetico := 0;

  if TipoRelatorio = 1 then
  begin
    QRLTitRealiz.enabled := false;
    m1.Top:= 22;
    m2.Top:= 22;
    m3.Top:= 22;
    m4.Top:= 22;
    m5.Top:= 22;
    m6.Top:= 22;
    m7.Top:= 22;
    m8.Top:= 22;
    m9.Top:= 22;
    m10.Top:= 22;
    m11.Top:= 22;
    m12.Top:= 22;
    qrlTotReal.Top:= 22;
    m1.Enabled:= False;
    m2.Enabled:= False;
    m3.Enabled:= False;
    m4.Enabled:= False;
    m5.Enabled:= False;
    m6.Enabled:= False;
    m7.Enabled:= False;
    m8.Enabled:= False;
    m9.Enabled:= False;
    m10.Enabled:= False;
    m11.Enabled:= False;
    m12.Enabled:= False;
    qrlTotReal.Enabled := False;
    //***********************************
    QRLD1.Top := 22;
    QRLD2.Top := 22;
    QRLD3.Top := 22;
    QRLD4.Top := 22;
    QRLD5.Top := 22;
    QRLD6.Top := 22;
    QRLD7.Top := 22;
    QRLD8.Top := 22;
    QRLD9.Top := 22;
    QRLD10.Top := 22;
    QRLD11.Top := 22;
    QRLD12.Top := 22;
    QRLDTot.Top := 22;
    QRLD1.Enabled := true;
    QRLD2.Enabled := true;
    QRLD3.Enabled := true;
    QRLD4.Enabled := true;
    QRLD5.Enabled := true;
    QRLD6.Enabled := true;
    QRLD7.Enabled := true;
    QRLD8.Enabled := true;
    QRLD9.Enabled := true;
    QRLD10.Enabled := true;
    QRLD11.Enabled := true;
    QRLD12.Enabled := true;
    QRLDTot.Enabled := true;
    //*********************************
    QRLTitPlan.Enabled := false;
    QRDBText3.Enabled := false;
    QRDBText4.Enabled := false;
    QRDBText5.Enabled := false;
    QRDBText6.Enabled := false;
    QRDBText7.Enabled := false;
    QRDBText8.Enabled := false;
    QRDBText9.Enabled := false;
    QRDBText10.Enabled := false;
    QRDBText11.Enabled := false;
    QRDBText12.Enabled := false;
    QRDBText13.Enabled := false;
    QRDBText16.Enabled := false;
    QRDBText17.Enabled := false;
    qrband1.height := 40;

    for i := 1 to 12 do
    begin
      Campo := 'VL_PLAN_MES' + IntToStr(i);
      Campo2 := 'VL_REAL_MES_' + IntToStr(i);
      Tot := Tot + QryRelatorio.FieldByName(Campo2).AsFloat;
      TotalParcial[i] := TotalParcial[i] + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
      TotalRelatorio[i] := TotalRelatorio[i] + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
      case i of
        1  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD1.Caption := '-' + FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD1.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m1.Font.Color := clRed;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD1.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD1.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m1.Font.Color := clBlue;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD1.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD1.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m1.Font.Color := clBlack;
          end;

        end;
        2  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD2.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD2.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m2.Font.Color := clRed;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD2.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD2.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m2.Font.Color := clBlue;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD2.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD2.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m2.Font.Color := clBlack;
          end;
        end;
        3  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD3.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD3.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m3.Font.Color := clRed;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD3.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD3.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m3.Font.Color := clBlue;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD3.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD3.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m3.Font.Color := clBlack;
          end;

        end;
        4  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD4.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD4.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m4.Font.Color := clRed;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD4.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD4.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m4.Font.Color := clBlue;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD4.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD4.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m4.Font.Color := clBlack;
          end;
        end;
        5  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD5.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD5.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m5.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD5.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD5.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m5.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD5.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD5.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m5.Font.Color := clBlack;
          end;
        end;
        6  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD6.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD6.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m6.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD6.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD6.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m6.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD6.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD6.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m6.Font.Color := clBlack;
          end;
        end;
        7  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD7.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD7.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m7.Font.Color := clRed;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD7.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD7.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m7.Font.Color := clBlue;
          end;

          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD7.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD7.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m7.Font.Color := clBlack;
          end;
        end;
        8  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD8.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD8.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m8.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD8.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD8.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m8.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD8.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD8.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m8.Font.Color := clBlack;
          end;
        end;
        9  : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD9.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD9.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m9.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD9.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD9.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m9.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD9.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD9.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m9.Font.Color := clBlack;
          end;
        end;
        10 : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD10.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD10.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m10.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD10.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD10.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m10.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD10.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD10.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m10.Font.Color := clBlack;
          end;
        end;
        11 : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD11.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD11.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m11.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD11.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD11.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m11.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD11.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD11.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m11.Font.Color := clBlack;
          end;
        end;
        12 : begin
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat > 0 then
          begin
            QRLD12.Caption := '-' +  FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD12.Font.Color := clBlue;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m12.Font.Color := clRed;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat < 0 then
          begin
            QRLD12.Caption := '+' + FloatToStrF((-1)* (QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat),ffNumber,15,2);
            QRLD12.Font.Color := clRed;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m12.Font.Color := clBlue;
          end;
          if QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat = 0 then
          begin
            QRLD12.Caption := FloatToStrF(QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat,ffNumber,15,2);
            QRLD12.Font.Color := clBlack;
            totSintetico := totSintetico + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).AsFloat;
            //m12.Font.Color := clBlack;
          end;
        end;
      end;
    end;
    if totSintetico > 0 then
    begin
      QRLDTot.Caption := FloatToStrF((-1) * totSintetico,ffNumber,15,2);
      QRLDTot.Font.Color := clBlue;
    end
    else if totSintetico < 0 then
    begin
      QRLDTot.Caption := '+' + FloatToStrF((-1) * totSintetico,ffNumber,15,2);
      QRLDTot.Font.Color := clRed;
    end
    else
    begin
      QRLDTot.Caption := FloatToStrF(totSintetico,ffNumber,15,2);
      QRLDTot.Font.Color := clBlack;
    end;
  end
  else
  begin
    for i := 1 to 12 do
    begin
      Campo := 'VL_PLAN_MES' + IntToStr(i);
      Campo2 := 'VL_REAL_MES_' + IntToStr(i);
      Tot := Tot + QryRelatorio.fieldByName(Campo2).AsFloat;
      TotalParcial[i] := TotalParcial[i] + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).asFloat;
      TotalRelatorio[i] := TotalRelatorio[i] + QryRelatorio.FieldByName(Campo).asFloat - QryRelatorio.FieldByName(Campo2).asFloat;
      case i of
        1  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
          begin
            m1.Font.Color := clRed;
          end
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m1.Font.Color := clBlue
          else m1.Font.Color := clBlack;
        end;
        2  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat> QryRelatorio.FieldByName(Campo).asFloat then
          begin
            m2.Font.Color := clRed;
          end
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m2.Font.Color := clBlue
          else m2.Font.Color := clBlack;
        end;
        3  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat> QryRelatorio.FieldByName(Campo).asFloat then
          begin
            m3.Font.Color := clRed;
          end
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m3.Font.Color := clBlue
          else m3.Font.Color := clBlack;
        end;
        4  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m4.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m4.Font.Color := clBlue
          else m4.Font.Color := clBlack;
        end;
        5  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m5.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m5.Font.Color := clBlue
          else m5.Font.Color := clBlack;
        end;
        6  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m6.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m6.Font.Color := clBlue
          else m6.Font.Color := clBlack;
        end;
        7  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m7.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m7.Font.Color := clBlue
          else m7.Font.Color := clBlack;
        end;
        8  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m8.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m8.Font.Color := clBlue
          else m8.Font.Color := clBlack;
        end;
        9  : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m9.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m9.Font.Color := clBlue
          else m9.Font.Color := clBlack;
        end;
        10 : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m10.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m10.Font.Color := clBlue
          else m10.Font.Color := clBlack;
        end;
        11 : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m11.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m11.Font.Color := clBlue
          else m11.Font.Color := clBlack;
        end;
        12 : begin
          if QryRelatorio.FieldByName(Campo2).AsFloat > QryRelatorio.FieldByName(Campo).asFloat then
            m12.Font.Color := clRed
          else if QryRelatorio.FieldByName(Campo2).AsFloat < QryRelatorio.FieldByName(Campo).asFloat then
            m12.Font.Color := clBlue
          else m12.Font.Color := clBlack;
        end;
      end;
    end;
  end;

  TotalP := TotalP + QryRelatorio.FieldByName('VL_PLAN_TOTAL').AsFloat - Tot;
end;

procedure TFrmRelPlanRealizado.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var i : integer;
begin
  inherited;
  for  i:=1 to 12 do
  begin
    case i of
      1  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel1.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel1.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel1.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel1.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel1.Caption := '0';
        end;
      end;
      2  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel2.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel2.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel2.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel2.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel2.Caption := '0';
        end;
      end;
      3  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel3.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel3.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel3.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel3.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel3.Caption := '0';
        end;
      end;
      4  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel4.Caption := '+' + FormatFloat('###,##0.00',(-1) * TotalRelatorio[i]);
          qrlTotRel4.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel4.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel4.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel4.Caption := '0';
        end;
      end;
      5  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel5.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel5.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel5.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel5.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel5.Caption := '0';
        end;
      end;
      6  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel6.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel6.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel6.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel6.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel6.Caption := '0';
        end;
      end;
      7  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel7.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel7.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel7.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel7.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel7.Caption := '0';
        end;
      end;
      8  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel8.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel8.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel8.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel8.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel8.Caption := '0';
        end;
      end;
      9  : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel9.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel9.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel9.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel9.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel9.Caption := '0';
        end;
      end;
      10 : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel10.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel10.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel10.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel10.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel10.Caption := '0';
        end;
      end;
      11 : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel11.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel11.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel11.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel11.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel11.Caption := '0';
        end;
      end;
      12 : begin
        if TotalRelatorio[i] < 0 then
        begin
          qrlTotRel12.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalRelatorio[i]);
          qrlTotRel12.Font.Color := clRed;
        end;
        if TotalRelatorio[i] > 0 then
        begin
          qrlTotRel12.Caption := '-' + FormatFloat('###,##0.00', TotalRelatorio[i]);
          qrlTotRel12.Font.Color := clBlue;
        end;
        if TotalRelatorio[i] = 0 then
        begin
          qrlTotRel12.Caption := '0';
        end;
      end;
      {else
      begin

      1  : qrlTotRel1.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      2  : qrlTotRel2.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      3  : qrlTotRel3.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      4  : qrlTotRel4.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      5  : qrlTotRel5.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      6  : qrlTotRel6.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      7  : qrlTotRel7.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      8  : qrlTotRel8.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      9  : qrlTotRel9.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      10  : qrlTotRel10.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      11  : qrlTotRel11.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);
      12  : qrlTotRel12.Caption := FormatFloat('###,##0.00', TotalRelatorio[i]);  }
      //end;
    end;
  end;

  if TotalR > 0 then
    begin
      qrlTotal.Caption := '-' + FormatFloat('###,##0.00', TotalR);
      qrlTotal.Font.Color := clBlue;
    end
    else if TotalR < 0 then
    begin
      qrlTotal.Caption := FormatFloat('###,##0.00', (-1) * TotalR);
      qrlTotal.Font.Color := clRed;
    end
    else
    begin
      qrlTotal.Caption := FormatFloat('###,##0.00', TotalR);
      qrlTotal.Font.Color := clBlack;
    end;

 { if TipoRelatorio = 1 then
  begin     }
    if QRBand2.Color = clWhite then
      QRBand2.Color := $00D8D8D8
    else
      QRBand2.Color := clWhite;
 // end;
end;

procedure TFrmRelPlanRealizado.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i : integer;
begin
  inherited;

  if TotalP > 0 then
  begin
    qrlTotAnualTipo.Caption := '-' + FormatFloat('###,##0.00', TotalP);
    qrlTotAnualTipo.Font.Color := clBlue;
  end
  else if TotalP < 0 then
  begin
    qrlTotAnualTipo.Caption := FormatFloat('###,##0.00', (-1) * TotalP);
    qrlTotAnualTipo.Font.Color := clRed;
  end
  else
  begin
    qrlTotAnualTipo.Caption := FormatFloat('###,##0.00', TotalP);
    qrlTotAnualTipo.Font.Color := clBlack;
  end;

  for  i:=1 to 12 do
  begin
    //if TipoRelatorio = 1 then
//    if TipoRelatorio = 1 then
   // begin
      case i of
          1  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial1.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial1.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial1.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial1.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial1.Caption := '0';
            end;
          end;
          2  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial2.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial2.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial2.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial2.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial2.Caption := '0';
            end;
          end;
          3  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial3.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial3.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial3.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial3.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial3.Caption := '0';
            end;
          end;
          4  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial4.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial4.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial4.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial4.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial4.Caption := '0';
            end;
          end;
          5  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial5.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial5.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial5.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial5.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial5.Caption := '0';
            end;
          end;
          6  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial6.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial6.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial6.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial6.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial6.Caption := '0';
            end;
          end;
          7  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial7.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial7.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial7.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial7.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial7.Caption := '0';
            end;
          end;
          8  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial8.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial8.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial8.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial8.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial8.Caption := '0';
            end;
          end;
          9  : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial9.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial9.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial9.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial9.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial9.Caption := '0';
            end;
          end;
          10 : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial10.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial10.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial10.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial10.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial10.Caption := '0';
            end;
          end;
          11 : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial11.Caption := '+' + FormatFloat('###,##0.00', (-1) * TotalParcial[i]);
              qrlParcial11.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial11.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial11.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial11.Caption := '0';
            end;
          end;
          12 : begin
            if TotalParcial[i] < 0 then
            begin
              qrlParcial12.Caption := '+' + FormatFloat('###,##0.00',(-1) * TotalParcial[i]);
              qrlParcial12.Font.Color := clRed;
            end;
            if TotalParcial[i] > 0 then
            begin
              qrlParcial12.Caption := '-' + FormatFloat('###,##0.00', TotalParcial[i]);
              qrlParcial12.Font.Color := clBlue;
            end;
            if TotalParcial[i] = 0 then
            begin
              qrlParcial12.Caption := '0';
            end;
          end;
     // end;
    end;

    TotalParcial[i] := 0;
    TotalR := TotalR + TotalP;
    TotalP := 0;
  end;
end;

procedure TFrmRelPlanRealizado.QRDBText1Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := FormatFloat('#0', QryRelatorio.FieldByName('CO_GRUP_CTA').AsFloat) + '.' +
           FormatFloat('##00', QryRelatorio.FieldByName('CO_SGRUP_CTA').AsFloat) + ' - ' +
           QryRelatorioDE_SGRUP_CTA.AsString;
end;

procedure TFrmRelPlanRealizado.QRDBText2Print(sender: TObject;
  var Value: String);
begin
  inherited;
  Value := FormatFloat('#0', QryRelatorio.FieldByName('CO_GRUP_CTA').AsFloat) + '.' +
           FormatFloat('##00', QryRelatorio.FieldByName('CO_SGRUP_CTA').AsFloat) + '.' +
           FormatFloat('###000', QryRelatorio.FieldByName('CO_CONTA_PC').AsFloat) + ' - ' +
           QryRelatorioDE_CONTA_PC.AsString;
end;

procedure TFrmRelPlanRealizado.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
  var i : integer;
begin
  inherited;

  if TipoRelatorio = 0 then
    QRBand3.Enabled := false
  else
    QRBand3.Enabled := true;

  for i:= 1 to 12 do
  begin
    TotalParcial[i] := 0;
    TotalRelatorio[i] := 0;
  end;
  TotalP := 0;
  TotalR := 0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelPlanRealizado]);

end.
