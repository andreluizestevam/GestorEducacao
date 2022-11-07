unit U_FrmRelMapadePlanejAnualMatricula;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapadePlanejAnualMatricula = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    l1: TQRLabel;
    l2: TQRLabel;
    l6: TQRLabel;
    l5: TQRLabel;
    l4: TQRLabel;
    l3: TQRLabel;
    QRLabel15: TQRLabel;
    l7: TQRLabel;
    l8: TQRLabel;
    l9: TQRLabel;
    l10: TQRLabel;
    l11: TQRLabel;
    l12: TQRLabel;
    Detail: TQRBand;
    QRDBText1: TQRDBText;
    P: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRBand1: TQRBand;
    QRLabel5: TQRLabel;
    totp1: TQRLabel;
    tote1: TQRLabel;
    tote2: TQRLabel;
    totp2: TQRLabel;
    totp3: TQRLabel;
    totp4: TQRLabel;
    tote4: TQRLabel;
    tote3: TQRLabel;
    totp5: TQRLabel;
    totp6: TQRLabel;
    totp7: TQRLabel;
    totp8: TQRLabel;
    tote8: TQRLabel;
    tote7: TQRLabel;
    tote6: TQRLabel;
    tote5: TQRLabel;
    totp9: TQRLabel;
    totp10: TQRLabel;
    totp11: TQRLabel;
    totp12: TQRLabel;
    tote12: TQRLabel;
    tote11: TQRLabel;
    tote10: TQRLabel;
    tote9: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    TotalEfet: TQRLabel;
    TotalPlanej: TQRLabel;
    QRShape2: TQRShape;
    QRLabel8: TQRLabel;
    QRLPage: TQRLabel;
    QRLParametros: TQRLabel;
    QryRelatoriono_cur: TStringField;
    QryRelatorioco_cur: TIntegerField;
    QryRelatorioco_modu_cur: TIntegerField;
    QryRelatorioSG_DPTO_CUR: TStringField;
    QRDBText4: TQRDBText;
    P1: TQRLabel;
    P2: TQRLabel;
    P3: TQRLabel;
    P4: TQRLabel;
    P5: TQRLabel;
    P6: TQRLabel;
    P12: TQRLabel;
    P11: TQRLabel;
    P10: TQRLabel;
    P9: TQRLabel;
    P8: TQRLabel;
    P7: TQRLabel;
    R1: TQRLabel;
    R2: TQRLabel;
    R3: TQRLabel;
    R4: TQRLabel;
    R5: TQRLabel;
    R6: TQRLabel;
    R7: TQRLabel;
    R8: TQRLabel;
    R9: TQRLabel;
    R10: TQRLabel;
    R11: TQRLabel;
    R12: TQRLabel;
    totPlanCurso: TQRLabel;
    totRealiCurso: TQRLabel;
    QryRelatorioco_dpto_cur: TIntegerField;
    QryPesquisa2: TADOQuery;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    tp1,tp2,tp3,tp4,tp5,tp6,tp7,tp8,tp9,tp10,tp11,tp12 : integer;
    te1,te2,te3,te4,te5,te6,te7,te8,te9,te10,te11,te12 : integer;
  public
    anoBase : string;
  end;

var
  FrmRelMapadePlanejAnualMatricula: TFrmRelMapadePlanejAnualMatricula;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapadePlanejAnualMatricula.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  QryPesquisa2.Close;
  QryPesquisa2.SQL.CommaText := 'SELECT pl.* FROM tb155_plan_matr pl ' +
    'JOIN tb01_curso cur ON cur.co_cur = pl.co_cur ' +
    'WHERE pl.co_ano_ref = ' + anoBase +
    ' and pl.co_modu_cur = ' + QryRelatorioco_modu_cur.AsString +
    ' and pl.co_cur = ' + QryRelatorioco_cur.AsString +
    ' and cur.co_dpto_cur = ' + QryRelatorioco_dpto_cur.AsString+
    ' order by cur.no_cur';
  QryPesquisa2.Open;

  if not QryPesquisa2.IsEmpty then
  begin

    if not QryPesquisa2.FieldByName('vl_plan_mes1').IsNull then
      P1.Caption := QryPesquisa2.FieldByName('vl_plan_mes1').AsString
    else
      P1.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes2').IsNull then
      P2.Caption := QryPesquisa2.FieldByName('vl_plan_mes2').AsString
    else
      P2.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes3').IsNull then
      P3.Caption := QryPesquisa2.FieldByName('vl_plan_mes3').AsString
    else
      P3.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes4').IsNull then
      P4.Caption := QryPesquisa2.FieldByName('vl_plan_mes4').AsString
    else
      P4.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes5').IsNull then
      P5.Caption := QryPesquisa2.FieldByName('vl_plan_mes5').AsString
    else
      P5.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes6').IsNull then
      P6.Caption := QryPesquisa2.FieldByName('vl_plan_mes6').AsString
    else
      P6.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes7').IsNull then
      P7.Caption := QryPesquisa2.FieldByName('vl_plan_mes7').AsString
    else
      P7.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes8').IsNull then
      P8.Caption := QryPesquisa2.FieldByName('vl_plan_mes8').AsString
    else
      P8.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes9').IsNull then
      P9.Caption := QryPesquisa2.FieldByName('vl_plan_mes9').AsString
    else
      P9.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes10').IsNull then
      P10.Caption := QryPesquisa2.FieldByName('vl_plan_mes10').AsString
    else
      P10.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes11').IsNull then
      P11.Caption := QryPesquisa2.FieldByName('vl_plan_mes11').AsString
    else
      P11.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_plan_mes12').IsNull then
      P12.Caption := QryPesquisa2.FieldByName('vl_plan_mes12').AsString
    else
      P12.Caption := '0';

    {tp1 := tp1 + QryPesquisa2.FieldByName('vl_plan_mes1').AsInteger;
    tp2 := tp2 + QryPesquisa2.FieldByName('vl_plan_mes2').AsInteger;
    tp3 := tp3 + QryPesquisa2.FieldByName('vl_plan_mes3').AsInteger;
    tp4 := tp4 + QryPesquisa2.FieldByName('vl_plan_mes4').AsInteger;
    tp5 := tp5 + QryPesquisa2.FieldByName('vl_plan_mes5').AsInteger;
    tp6 := tp6 + QryPesquisa2.FieldByName('vl_plan_mes6').AsInteger;
    tp7 := tp7 + QryPesquisa2.FieldByName('vl_plan_mes7').AsInteger;
    tp8 := tp8 + QryPesquisa2.FieldByName('vl_plan_mes8').AsInteger;
    tp9 := tp9 + QryPesquisa2.FieldByName('vl_plan_mes9').AsInteger;
    tp10 := tp10 + QryPesquisa2.FieldByName('vl_plan_mes10').AsInteger;
    tp11 := tp11 + QryPesquisa2.FieldByName('vl_plan_mes11').AsInteger;
    tp12 := tp12 + QryPesquisa2.FieldByName('vl_plan_mes12').AsInteger;}

    tp1 := tp1 + StrToInt(P1.Caption);
    tp2 := tp2 + StrToInt(P2.Caption);
    tp3 := tp3 + StrToInt(P3.Caption);
    tp4 := tp4 + StrToInt(P4.Caption);
    tp5 := tp5 + StrToInt(P5.Caption);
    tp6 := tp6 + StrToInt(P6.Caption);
    tp7 := tp7 + StrToInt(P7.Caption);
    tp8 := tp8 + StrToInt(P8.Caption);
    tp9 := tp9 + StrToInt(P9.Caption);
    tp10 := tp10 + StrToInt(P10.Caption);
    tp11 := tp11 + StrToInt(P11.Caption);
    tp12 := tp12 + StrToInt(P12.Caption);

    {totPlanCurso.Caption := FloattoStrf(QryPesquisa2.FieldByName('vl_plan_mes1').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes2').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes3').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes4').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes5').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes6').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes7').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes8').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes9').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes10').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes11').AsInteger
    + QryPesquisa2.FieldByName('vl_plan_mes12').AsInteger,ffNumber,10,0);}

    totPlanCurso.Caption := FloattoStrf(StrToInt(P1.Caption)
    + StrToInt(P2.Caption)
    + StrToInt(P3.Caption)
    + StrToInt(P4.Caption)
    + StrToInt(P5.Caption)
    + StrToInt(P6.Caption)
    + StrToInt(P7.Caption)
    + StrToInt(P8.Caption)
    + StrToInt(P9.Caption)
    + StrToInt(P10.Caption)
    + StrToInt(P11.Caption)
    + StrToInt(P12.Caption),ffNumber,10,0);

    P1.Caption := FloatToStrF(StrToFloat(p1.Caption),ffNumber,10,0);
    P2.Caption := FloatToStrF(StrToFloat(p2.Caption),ffNumber,10,0);
    P3.Caption := FloatToStrF(StrToFloat(p3.Caption),ffNumber,10,0);
    P4.Caption := FloatToStrF(StrToFloat(p4.Caption),ffNumber,10,0);
    P5.Caption := FloatToStrF(StrToFloat(p5.Caption),ffNumber,10,0);
    P6.Caption := FloatToStrF(StrToFloat(p6.Caption),ffNumber,10,0);
    P7.Caption := FloatToStrF(StrToFloat(p7.Caption),ffNumber,10,0);
    P8.Caption := FloatToStrF(StrToFloat(p8.Caption),ffNumber,10,0);
    P9.Caption := FloatToStrF(StrToFloat(p9.Caption),ffNumber,10,0);
    P10.Caption := FloatToStrF(StrToFloat(p10.Caption),ffNumber,10,0);
    P11.Caption := FloatToStrF(StrToFloat(p11.Caption),ffNumber,10,0);
    P12.Caption := FloatToStrF(StrToFloat(p12.Caption),ffNumber,10,0);

    if not QryPesquisa2.FieldByName('vl_real_mes1').IsNull then
      R1.Caption := QryPesquisa2.FieldByName('vl_real_mes1').AsString
    else
      R1.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes2').IsNull then
      R2.Caption := QryPesquisa2.FieldByName('vl_real_mes2').AsString
    else
      R2.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes3').IsNull then
      R3.Caption := QryPesquisa2.FieldByName('vl_real_mes3').AsString
    else
      R3.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes4').IsNull then
      R4.Caption := QryPesquisa2.FieldByName('vl_real_mes4').AsString
    else
      R4.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes5').IsNull then
      R5.Caption := QryPesquisa2.FieldByName('vl_real_mes5').AsString
    else
      R5.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes6').IsNull then
      R6.Caption := QryPesquisa2.FieldByName('vl_real_mes6').AsString
    else
      R6.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes7').IsNull then
      R7.Caption := QryPesquisa2.FieldByName('vl_real_mes7').AsString
    else
      R7.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes8').IsNull then
      R8.Caption := QryPesquisa2.FieldByName('vl_real_mes8').AsString
    else
      R8.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes9').IsNull then
      R9.Caption := QryPesquisa2.FieldByName('vl_real_mes9').AsString
    else
      R9.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes10').IsNull then
      R10.Caption := QryPesquisa2.FieldByName('vl_real_mes10').AsString
    else
      R10.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes11').IsNull then
      R11.Caption := QryPesquisa2.FieldByName('vl_real_mes11').AsString
    else
      R11.Caption := '0';

    if not QryPesquisa2.FieldByName('vl_real_mes12').IsNull then
      R12.Caption := QryPesquisa2.FieldByName('vl_real_mes12').AsString
    else
      R12.Caption := '0';

    totRealiCurso.Caption := FloatToStrF(StrToInt(r1.Caption) + StrToInt(r2.Caption) + StrToInt(r3.Caption) + StrToInt(r4.Caption) + StrToInt(r5.Caption) +
    StrToInt(r6.Caption) + StrToInt(r7.Caption) + StrToInt(r8.Caption) + StrToInt(r9.Caption) + StrToInt(r10.Caption) + StrToInt(r11.Caption) +
    StrToInt(r12.Caption),ffNumber,10,0);

    te1 := te1 + StrToInt(r1.Caption);
    te2 := te2 + StrToInt(r2.Caption);
    te3 := te3 + StrToInt(r3.Caption);
    te4 := te4 + StrToInt(r4.Caption);
    te5 := te5 + StrToInt(r5.Caption);
    te6 := te6 + StrToInt(r6.Caption);
    te7 := te7 + StrToInt(r7.Caption);
    te8 := te8 + StrToInt(r8.Caption);
    te9 := te9 + StrToInt(r9.Caption);
    te10 := te10 + StrToInt(r10.Caption);
    te11 := te11 + StrToInt(r11.Caption);
    te12 := te12 + StrToInt(r12.Caption);

    {R1.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes1').AsFloat,ffNumber,10,0);
    R2.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes2').AsFloat,ffNumber,10,0);
    R3.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes3').AsFloat,ffNumber,10,0);
    R4.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes4').AsFloat,ffNumber,10,0);
    R5.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes5').AsFloat,ffNumber,10,0);
    R6.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes6').AsFloat,ffNumber,10,0);
    R7.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes7').AsFloat,ffNumber,10,0);
    R8.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes8').AsFloat,ffNumber,10,0);
    R9.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes9').AsFloat,ffNumber,10,0);
    R10.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes10').AsFloat,ffNumber,10,0);
    R11.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes11').AsFloat,ffNumber,10,0);
    R12.Caption := FloatToStrF(QryPesquisa2.FieldByName('vl_real_mes12').AsFloat,ffNumber,10,0);}

    R1.Caption := FloatToStrF(StrToFloat(r1.Caption),ffNumber,10,0);
    R2.Caption := FloatToStrF(StrToFloat(r2.Caption),ffNumber,10,0);
    R3.Caption := FloatToStrF(StrToFloat(r3.Caption),ffNumber,10,0);
    R4.Caption := FloatToStrF(StrToFloat(r4.Caption),ffNumber,10,0);
    R5.Caption := FloatToStrF(StrToFloat(r5.Caption),ffNumber,10,0);
    R6.Caption := FloatToStrF(StrToFloat(r6.Caption),ffNumber,10,0);
    R7.Caption := FloatToStrF(StrToFloat(r7.Caption),ffNumber,10,0);
    R8.Caption := FloatToStrF(StrToFloat(r8.Caption),ffNumber,10,0);
    R9.Caption := FloatToStrF(StrToFloat(r9.Caption),ffNumber,10,0);
    R10.Caption := FloatToStrF(StrToFloat(r10.Caption),ffNumber,10,0);
    R11.Caption := FloatToStrF(StrToFloat(r11.Caption),ffNumber,10,0);
    R12.Caption := FloatToStrF(StrToFloat(r12.Caption),ffNumber,10,0);
  end
  else
  begin
    P1.Caption := '0';
    P2.Caption := '0';
    P3.Caption := '0';
    P4.Caption := '0';
    P5.Caption := '0';
    P6.Caption := '0';
    P7.Caption := '0';
    P8.Caption := '0';
    P9.Caption := '0';
    P10.Caption := '0';
    P11.Caption := '0';
    P12.Caption := '0';

    R1.Caption := '0';
    R2.Caption := '0';
    R3.Caption := '0';
    R4.Caption := '0';
    R5.Caption := '0';
    R6.Caption := '0';
    R7.Caption := '0';
    R8.Caption := '0';
    R9.Caption := '0';
    R10.Caption := '0';
    R11.Caption := '0';
    R12.Caption := '0';

    totPlanCurso.Caption := '0';
    totRealiCurso.Caption := '0';
  end;

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelMapadePlanejAnualMatricula.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if te1 > tp1 then
    tote1.Font.Color := clRed
  else if te1 < tp1 then
    tote1.Font.Color := clBlue
  else
    tote1.Font.Color := clBlack;

  if te2 > tp2 then
    tote2.Font.Color := clRed
  else if te2 < tp2 then
    tote2.Font.Color := clBlue
  else
    tote2.Font.Color := clBlack;

  if te3 > tp3 then
    tote3.Font.Color := clRed
  else if te3 < tp3 then
    tote3.Font.Color := clBlue
  else
    tote3.Font.Color := clBlack;

  if te4 > tp4 then
    tote4.Font.Color := clRed
  else if te4 < tp4 then
    tote4.Font.Color := clBlue
  else
    tote4.Font.Color := clBlack;

  if te5 > tp5 then
    tote5.Font.Color := clRed
  else if te5 < tp5 then
    tote5.Font.Color := clBlue
  else
    tote5.Font.Color := clBlack;

  if te6 > tp6 then
    tote6.Font.Color := clRed
  else if te6 < tp6 then
    tote6.Font.Color := clBlue
  else
    tote6.Font.Color := clBlack;

  if te7 > tp7 then
    tote7.Font.Color := clRed
  else if te7 < tp7 then
    tote7.Font.Color := clBlue
  else
    tote7.Font.Color := clBlack;

  if te8 > tp8 then
    tote8.Font.Color := clRed
  else if te8 < tp8 then
    tote8.Font.Color := clBlue
  else
    tote8.Font.Color := clBlack;

  if te9 > tp9 then
    tote9.Font.Color := clRed
  else if te9 < tp9 then
    tote9.Font.Color := clBlue
  else
    tote9.Font.Color := clBlack;

  if te10 > tp10 then
    tote10.Font.Color := clRed
  else if te10 < tp10 then
    tote10.Font.Color := clBlue
  else
    tote10.Font.Color := clBlack;

  if te11 > tp11 then
    tote11.Font.Color := clRed
  else if te11 < tp11 then
    tote11.Font.Color := clBlue
  else
    tote11.Font.Color := clBlack;

  if te12 > tp12 then
    tote12.Font.Color := clRed
  else if te12 < tp12 then
    tote12.Font.Color := clBlue
  else
    tote12.Font.Color := clBlack;

  totp1.Caption := FloatToStrF(tp1,ffNumber,10,0);
  totp2.Caption := FloatToStrF(tp2,ffNumber,10,0);
  totp3.Caption := FloatToStrF(tp3,ffNumber,10,0);
  totp4.Caption := FloatToStrF(tp4,ffNumber,10,0);
  totp5.Caption := FloatToStrF(tp5,ffNumber,10,0);
  totp6.Caption := FloatToStrF(tp6,ffNumber,10,0);
  totp7.Caption := FloatToStrF(tp7,ffNumber,10,0);
  totp8.Caption := FloatToStrF(tp8,ffNumber,10,0);
  totp9.Caption := FloatToStrF(tp9,ffNumber,10,0);
  totp10.Caption := FloatToStrF(tp10,ffNumber,10,0);
  totp11.Caption := FloatToStrF(tp11,ffNumber,10,0);
  totp12.Caption := FloatToStrF(tp12,ffNumber,10,0);

  tote1.Caption := FloatToStrF(te1,ffNumber,10,0);
  tote2.Caption := FloatToStrF(te2,ffNumber,10,0);
  tote3.Caption := FloatToStrF(te3,ffNumber,10,0);
  tote4.Caption := FloatToStrF(te4,ffNumber,10,0);
  tote5.Caption := FloatToStrF(te5,ffNumber,10,0);
  tote6.Caption := FloatToStrF(te6,ffNumber,10,0);
  tote7.Caption := FloatToStrF(te7,ffNumber,10,0);
  tote8.Caption := FloatToStrF(te8,ffNumber,10,0);
  tote9.Caption := FloatToStrF(te9,ffNumber,10,0);
  tote10.Caption := FloatToStrF(te10,ffNumber,10,0);
  tote11.Caption := FloatToStrF(te11,ffNumber,10,0);
  tote12.Caption := FloatToStrF(te12,ffNumber,10,0);

  TotalPlanej.Caption := IntToStr(tp1 + tp2 + tp3 + tp4 + tp5 + tp6 + tp7 + tp8 + tp9 + tp10 + tp11 + tp12);
  TotalEfet.Caption := IntToStr(te1 + te2 + te3 + te4 + te5 + te6 + te7 + te8 + te9 + te10 + te11 + te12);

  if StrToInt(TotalEfet.Caption) > StrToInt(TotalPlanej.Caption) then
    TotalEfet.Font.Color := clRed
  else if StrToInt(TotalEfet.Caption) < StrToInt(TotalPlanej.Caption) then
    TotalEfet.Font.Color := clBlue
  else
    TotalEfet.Font.Color := clBlack;

  TotalPlanej.Caption := FloatToStrF(tp1 + tp2 + tp3 + tp4 + tp5 + tp6 + tp7 + tp8 + tp9 + tp10 + tp11 + tp12,ffNumber,10,0);
  TotalEfet.Caption := FloatToStrF(te1 + te2 + te3 + te4 + te5 + te6 + te7 + te8 + te9 + te10 + te11 + te12,ffNumber,10,0);
end;

procedure TFrmRelMapadePlanejAnualMatricula.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  tp1 := 0;
  tp2 := 0;
  tp3 := 0;
  tp4 := 0;
  tp5 := 0;
  tp6 := 0;
  tp7 := 0;
  tp8 := 0;
  tp9 := 0;
  tp10 := 0;
  tp11 := 0;
  tp12 := 0;

  te1 := 0;
  te2 := 0;
  te3 := 0;
  te4 := 0;
  te5 := 0;
  te6 := 0;
  te7 := 0;
  te8 := 0;
  te9 := 0;
  te10 := 0;
  te11 := 0;
  te12 := 0;
end;

end.
