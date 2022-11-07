unit U_FrmRelRelacAlunoTurma;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls;

type
  TFrmRelRelacAlunoTurma = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel4: TQRLabel;
    QRExpr1: TQRExpr;
    QRLabel5: TQRLabel;
    QRLPage: TQRLabel;
    DetailBand1: TQRBand;
    QRL_ID: TQRLabel;
    qrIdade: TQRLabel;
    QRLabel10: TQRLabel;
    totMasc: TQRLabel;
    QRLabel11: TQRLabel;
    TotFem: TQRLabel;
    lbSexo: TQRLabel;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape1: TQRShape;
    QRLabel8: TQRLabel;
    QRLParametros: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel13: TQRLabel;
    QRLRACA: TQRLabel;
    QRLabel14: TQRLabel;
    QRLDEF: TQRLabel;
    qrlDate: TQRLabel;
    QRLS: TQRLabel;
    QrlMatricula: TQRLabel;
    QryRelatorioCO_ALU_CAD: TStringField;
    QryRelatorioco_turn_mat: TStringField;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioTP_RACA: TStringField;
    QryRelatorioTP_DEF: TStringField;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QryRelatorioDT_NASC_ALU: TDateTimeField;
    QryRelatorioDT_CAD_MAT: TDateTimeField;
    QryRelatorioCO_SIT_MAT: TStringField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatoriode_modu_cur: TStringField;
    QryRelatorioSERIE: TStringField;
    QryRelatorioTURMA: TStringField;
    QRLabel12: TQRLabel;
    QRLabel9: TQRLabel;
    QRDBText1: TQRDBText;
    QRLNoAlu: TQRLabel;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }

    i:integer;
  public
    { Public declarations }
      NmCurso,NmTurma:String;
  end;

var
  FrmRelRelacAlunoTurma: TFrmRelRelacAlunoTurma;
  contMasc, contFem : integer;

implementation
  uses U_Funcoes, DateUtils, MaskUtils, U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelRelacAlunoTurma.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
   var diasnoano: real;
begin
  inherited;
//  QRLDtCad.Caption := FormatDateTime('dd/mm/yy',QryRelatorio.FieldByName('DT_CADA_MTR').AsDateTime);
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString);

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  Inc(i);
  QRL_ID.Caption := IntToStr(i);
  qrlDate.Visible := false;

  If QryRelatorioCO_SIT_MAT.AsString = 'C' then
  begin
    QRLS.Caption := 'CANCELADO';
    qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'T' then
  begin
   QRLS.Caption := 'TRANCADO';
   qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'X' then
  begin
   QRLS.Caption := 'TRANSFERIDO';
    qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'F' then
  begin
   QRLS.Caption := 'FINALIZADO';
    qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'E' then
  begin
   QRLS.Caption := 'EVADIDO';
    qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'P' then
  begin
   QRLS.Caption := 'PENDENTE';
    qrlDate.Visible := true;
  end
  else
  if QryRelatorioCO_SIT_MAT.AsString = 'A' then
  begin
   QRLS.Caption := 'EM ABERTO';
    qrlDate.Visible := true;
  end;

  diasnoano := 365.6;
  if  not(QryRelatorio.FieldByName('DT_NASC_ALU').IsNull)then
    QRIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano))
  else
    qrIdade.Caption := ' ';

  if (QryRelatorioCO_SEXO_ALU.Value = 'M')then begin
    contMasc := contMasc +1;
    lbSexo.Caption  := 'M';
  end;
  if (QryRelatorioCO_SEXO_ALU.Value = 'F')then begin
    contFem := contFem + 1;
    lbSexo.Caption := 'F';
  end;

    if (QryRelatorio.FieldByName('TP_DEF').AsString = '')then
    QRLDEF.Caption:= '--';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'N')then
    QRLDEF.Caption:= 'Nenhuma';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'A')then
    QRLDEF.Caption:= 'Auditivo';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'V')then
    QRLDEF.Caption:= 'Visual';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'F')then
    QRLDEF.Caption:= 'Fisico';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'M')then
    QRLDEF.Caption:= 'Mental';
    if (QryRelatorio.FieldByName('TP_DEF').AsString = 'I')then
    QRLDEF.Caption:= 'Multiplas';
     if (QryRelatorio.FieldByName('TP_DEF').AsString = 'O')then
    QRLDEF.Caption:= 'Outros';

     if (QryRelatorio.FieldByName('TP_RACA').IsNull)then
    QRLRACA.Caption:= ' ';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'B')then
    QRLRACA.Caption:= 'Branco';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'P')then
    QRLRACA.Caption:= 'Negro';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'A')then
    QRLRACA.Caption:= 'Amarelo';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'D')then
    QRLRACA.Caption:= 'Pardo';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'I')then
    QRLRACA.Caption:= 'Indigena';
    if (QryRelatorio.FieldByName('TP_RACA').AsString = 'N')then
    QRLRACA.Caption:= 'Não Decl.';

    qrlDate.Caption:=FormatDateTime('dd/mm/yy',QryRelatorioDT_CAD_MAT.AsDateTime)


end;

procedure TFrmRelRelacAlunoTurma.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLS.Caption:='';
  i:=0;
  contMasc:=0;
  contFem:=0;
end;

procedure TFrmRelRelacAlunoTurma.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  totMasc.Caption := IntToStr(contMasc);
  totFem.Caption := intTostr(contFem);
end;

procedure TFrmRelRelacAlunoTurma.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  totMasc.Caption:='';
  TotFem.Caption:='';
  contMasc:=0;
  contFem:=0;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelRelacAlunoTurma]);

end.
