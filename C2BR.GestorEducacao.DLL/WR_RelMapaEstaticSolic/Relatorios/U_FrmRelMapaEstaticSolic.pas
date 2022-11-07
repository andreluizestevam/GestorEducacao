unit U_FrmRelMapaEstaticSolic;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaEstaticSolic = class(TFrmRelTemplate)
    QRLabel1: TQRLabel;
    QRShape2: TQRShape;
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
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRBand1: TQRBand;
    m1: TQRLabel;
    m2: TQRLabel;
    m3: TQRLabel;
    m4: TQRLabel;
    m5: TQRLabel;
    m6: TQRLabel;
    m7: TQRLabel;
    m8: TQRLabel;
    m9: TQRLabel;
    m10: TQRLabel;
    m11: TQRLabel;
    m12: TQRLabel;
    qrlTotAnual: TQRLabel;
    QRDBText2: TQRDBText;
    QryAux: TADOQuery;
    QRGroup1: TQRGroup;
    QRBand2: TQRBand;
    QRDBText3: TQRDBText;
    QryRelatorioCO_TIPO_SOLI: TIntegerField;
    QryRelatorioANO_SOLI_ATEN: TIntegerField;
    QryRelatorioNO_TIPO_SOLI: TStringField;
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
    qrlTotAnualTipo: TQRLabel;
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
    QRLabel32: TQRLabel;
    QRShape4: TQRShape;
    QRLabel33: TQRLabel;
    QRShape5: TQRShape;
    qrlUnidade: TQRLabel;
    qrlTipoSoli: TQRLabel;
    QRShapeTotal: TQRShape;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    formatipo: TQRLabel;
    QRLabel2: TQRLabel;
    QRLSituacao: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    TotalParcial, TotalRelatorio : array[1..12] of double;
    TotalP, TotalR : Double;
    TipoSoli : Integer;
  public
    itemtipo : integer;
    itemvisualiza : integer;
    codEmpresa : integer;
    anoSelecionado, staSolicitacao : String;
  end;

var
  FrmRelMapaEstaticSolic: TFrmRelMapaEstaticSolic;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaEstaticSolic.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i : integer;
  Tot : double;
begin
  inherited;
  Tot := 0;
  for  i:=1 to 12 do
  begin
    QryAux.Close;
    QryAux.SQL.Clear;

    if itemVisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
      QryAux.SQL.Add(' SELECT COUNT(HS.CO_SOLI_ATEN) AS VA_SOLI_ATEN                        ');

    QryAux.SQL.Add(' FROM TB64_SOLIC_ATEND SA, TB65_HIST_SOLICIT HS, TB66_TIPO_SOLIC TS ');
    QryAux.SQL.Add(' WHERE SA.CO_ALU = HS.CO_ALU                                        ');
    QryAux.SQL.Add('   AND SA.CO_EMP = HS.CO_EMP                                        ');
    QryAux.SQL.Add('   AND SA.CO_CUR = HS.CO_CUR                                        ');
    QryAux.SQL.Add('   AND SA.CO_SOLI_ATEN = HS.CO_SOLI_ATEN                            ');
    QryAux.SQL.Add('   AND HS.CO_TIPO_SOLI = TS.CO_TIPO_SOLI                            ');
    QryAux.SQL.Add('   AND SA.CO_SIT = ' + quotedStr('A'));

    QryAux.SQL.Add('   AND TS.CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString);

    if staSolicitacao <> 'S' then
      QryAux.SQL.Add('   AND HS.CO_SITU_SOLI = ' + quotedStr(staSolicitacao));
      
    QryAux.SQL.Add('   AND SA.MES_SOLI_ATEN = ' + IntToStr(i));

    if anoSelecionado <> '' then
      QryAux.SQL.Add(' AND SA.ANO_SOLI_ATEN = ' + quotedStr(anoSelecionado));

    if codEmpresa <> 0 then
      QryAux.SQL.Add(' AND SA.CO_EMP = ' + IntToStr(codEmpresa));

    QryAux.Open;
    Tot := Tot + QryAux.FieldByName('VA_SOLI_ATEN').AsFloat;
    TotalParcial[i] := TotalParcial[i] + QryAux.FieldByName('VA_SOLI_ATEN').AsFloat;
    TotalRelatorio[i] := TotalRelatorio[i] + QryAux.FieldByName('VA_SOLI_ATEN').AsFloat;
    QryAux.Next;

    //código novo
    if itemvisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
      begin
        case i of
          1  : m1.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          2  : m2.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          3  : m3.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          4  : m4.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          5  : m5.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          6  : m6.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          7  : m7.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          8  : m8.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          9  : m9.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          10 : m10.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          11 : m11.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
          12 : m12.Caption := QryAux.FieldByName('VA_SOLI_ATEN').AsString;
        end;
      end;
      //fim da alteração
  end;

  //código novo
  if itemvisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
    begin
      qrlTotAnual.Caption := FloatTostr(Tot);
      TotalP := TotalP + Tot;
      qrlTotAnualTipo.Caption := FloatToStr(TotalP);
    end;
  //fim da alteração

  if (m1.Caption = '0,00') or (m1.Caption = '0') then m1.Caption := '-';
  if (m2.Caption = '0,00') or (m2.Caption = '0') then m2.Caption := '-';
  if (m3.Caption = '0,00') or (m3.Caption = '0') then m3.Caption := '-';
  if (m4.Caption = '0,00') or (m4.Caption = '0') then m4.Caption := '-';
  if (m5.Caption = '0,00') or (m5.Caption = '0') then m5.Caption := '-';
  if (m6.Caption = '0,00') or (m6.Caption = '0') then m6.Caption := '-';
  if (m7.Caption = '0,00') or (m7.Caption = '0') then m7.Caption := '-';
  if (m8.Caption = '0,00') or (m8.Caption = '0') then m8.Caption := '-';
  if (m9.Caption = '0,00') or (m9.Caption = '0') then m9.Caption := '-';
  if (m10.Caption = '0,00') or (m10.Caption = '0') then m10.Caption := '-';
  if (m11.Caption = '0,00') or (m11.Caption = '0') then m11.Caption := '-';
  if (m12.Caption = '0,00') or (m12.Caption = '0') then m12.Caption := '-';

  if itemTipo = 1 then
  begin
    if QRBand1.Color = clWhite then
      QRBand1.Color := $00D8D8D8
    else
      QRBand1.Color := clWhite;
  end;

end;

procedure TFrmRelMapaEstaticSolic.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i : integer;
begin
  inherited;
  for  i := 1 to 12 do
  begin

    //código novo
      if itemvisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
        begin
          case i of
            1: qrlParcial1.Caption := FloatToStr(TotalParcial[i]);
            2: qrlParcial2.Caption := FloatToStr(TotalParcial[i]);
            3: qrlParcial3.Caption := FloatToStr(TotalParcial[i]);
            4: qrlParcial4.Caption := FloatToStr(TotalParcial[i]);
            5: qrlParcial5.Caption := FloatToStr(TotalParcial[i]);
            6: qrlParcial6.Caption := FloatToStr(TotalParcial[i]);
            7: qrlParcial7.Caption := FloatToStr(TotalParcial[i]);
            8: qrlParcial8.Caption := FloatToStr(TotalParcial[i]);
            9: qrlParcial9.Caption := FloatToStr(TotalParcial[i]);
            10: qrlParcial10.Caption := FloatToStr(TotalParcial[i]);
            11: qrlParcial11.Caption := FloatToStr(TotalParcial[i]);
            12: qrlParcial12.Caption := FloatToStr(TotalParcial[i]);
          end;
        end;
    //fim da alteração

    TotalParcial[i] := 0;
    TotalR := TotalR + TotalP;
    TotalP := 0;
  end;

  if itemTipo = 1 then
    qrlTipoSoli.Caption := QryRelatorioNO_TIPO_SOLI.AsString;

  if (qrlParcial1.Caption = '0,00') or (qrlParcial1.Caption = '0') then qrlParcial1.Caption := '-';
  if (qrlParcial2.Caption = '0,00') or (qrlParcial2.Caption = '0') then qrlParcial2.Caption := '-';
  if (qrlParcial3.Caption = '0,00') or (qrlParcial3.Caption = '0') then qrlParcial3.Caption := '-';
  if (qrlParcial4.Caption = '0,00') or (qrlParcial4.Caption = '0') then qrlParcial4.Caption := '-';
  if (qrlParcial5.Caption = '0,00') or (qrlParcial5.Caption = '0') then qrlParcial5.Caption := '-';
  if (qrlParcial6.Caption = '0,00') or (qrlParcial6.Caption = '0') then qrlParcial6.Caption := '-';
  if (qrlParcial7.Caption = '0,00') or (qrlParcial7.Caption = '0') then qrlParcial7.Caption := '-';
  if (qrlParcial8.Caption = '0,00') or (qrlParcial8.Caption = '0') then qrlParcial8.Caption := '-';
  if (qrlParcial9.Caption = '0,00') or (qrlParcial9.Caption = '0') then qrlParcial9.Caption := '-';
  if (qrlParcial10.Caption = '0,00') or (qrlParcial10.Caption = '0') then qrlParcial10.Caption := '-';
  if (qrlParcial11.Caption = '0,00') or (qrlParcial11.Caption = '0') then qrlParcial11.Caption := '-';
  if (qrlParcial12.Caption = '0,00') or (qrlParcial12.Caption = '0') then qrlParcial12.Caption := '-';

  if itemTipo = 1 then
  begin
    if QRBand2.Color = clWhite then
      QRBand2.Color := $00D8D8D8
    else
      QRBand2.Color := clWhite;
  end;

end;

procedure TFrmRelMapaEstaticSolic.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
var i : integer;
begin
//analitico
  inherited;
  if itemTipo = 1 then
  begin
    formatipo.Caption := '- DIFERENÇA';
    QRDBText3.Enabled := False;
    QRLabel5.Enabled := false;
    QRLabel32.Enabled := False;
    qrlTipoSoli.Enabled := True;
    QRShape4.Enabled := False;
    QRShape5.Enabled := False;
    QRShapeTotal.Enabled := True;
    QRBand1.Height := 0;
  end
  else begin
     formatipo.Caption := '- ANALÍTICO';
  end;

  // Zera variáveis
  for i:= 1 to 12 do
  begin
    TotalParcial[i] := 0;
    TotalRelatorio[i] := 0;
  end;
  TotalP := 0;
  TotalR := 0;
  TipoSoli := -1;
end;

procedure TFrmRelMapaEstaticSolic.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var i : integer;
begin
  inherited;

  for  i:=1 to 12 do
  begin

    //código novo
      if itemvisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
      begin
        case i of
          1  : qrlTotRel1.Caption := FloatToStr(TotalRelatorio[i]);
          2  : qrlTotRel2.Caption := FloatToStr(TotalRelatorio[i]);
          3  : qrlTotRel3.Caption := FloatToStr(TotalRelatorio[i]);
          4  : qrlTotRel4.Caption := FloatToStr(TotalRelatorio[i]);
          5  : qrlTotRel5.Caption := FloatToStr(TotalRelatorio[i]);
          6  : qrlTotRel6.Caption := FloatToStr(TotalRelatorio[i]);
          7  : qrlTotRel7.Caption := FloatToStr(TotalRelatorio[i]);
          8  : qrlTotRel8.Caption := FloatToStr(TotalRelatorio[i]);
          9  : qrlTotRel9.Caption := FloatToStr(TotalRelatorio[i]);
          10  : qrlTotRel10.Caption := FloatToStr(TotalRelatorio[i]);
          11  : qrlTotRel11.Caption := FloatToStr(TotalRelatorio[i]);
          12  : qrlTotRel12.Caption := FloatToStr(TotalRelatorio[i]);
        end;
      end;
    //fim da alteração
  end;

  //código novo
    if itemvisualiza = 0 then //após alteração o itemVisualiza = 0/rgVisualiza.items[0] = "Quantidade"
      qrlTotal.Caption := FloatToStr(TotalR);
  //fim da alteração

  if (qrlTotRel1.Caption = '0,00') or (qrlTotRel1.Caption = '0') then qrlTotRel1.Caption := '-';
  if (qrlTotRel2.Caption = '0,00') or (qrlTotRel2.Caption = '0') then qrlTotRel2.Caption := '-';
  if (qrlTotRel3.Caption = '0,00') or (qrlTotRel3.Caption = '0') then qrlTotRel3.Caption := '-';
  if (qrlTotRel4.Caption = '0,00') or (qrlTotRel4.Caption = '0') then qrlTotRel4.Caption := '-';
  if (qrlTotRel5.Caption = '0,00') or (qrlTotRel5.Caption = '0') then qrlTotRel5.Caption := '-';
  if (qrlTotRel6.Caption = '0,00') or (qrlTotRel6.Caption = '0') then qrlTotRel6.Caption := '-';
  if (qrlTotRel7.Caption = '0,00') or (qrlTotRel7.Caption = '0') then qrlTotRel7.Caption := '-';
  if (qrlTotRel8.Caption = '0,00') or (qrlTotRel8.Caption = '0') then qrlTotRel8.Caption := '-';
  if (qrlTotRel9.Caption = '0,00') or (qrlTotRel9.Caption = '0') then qrlTotRel9.Caption := '-';
  if (qrlTotRel10.Caption = '0,00') or (qrlTotRel10.Caption = '0') then qrlTotRel10.Caption := '-';
  if (qrlTotRel11.Caption = '0,00') or (qrlTotRel11.Caption = '0') then qrlTotRel11.Caption := '-';
  if (qrlTotRel12.Caption = '0,00') or (qrlTotRel12.Caption = '0') then qrlTotRel12.Caption := '-';

end;

procedure TFrmRelMapaEstaticSolic.QRDBText2Print(sender: TObject;
  var Value: String);
begin
  inherited;
  if TipoSoli <> -1 then
    if TipoSoli = QryRelatorioCO_TIPO_SOLI.AsInteger then
      Value := ' ';
  TipoSoli := QryRelatorioCO_TIPO_SOLI.AsInteger;
end;

procedure TFrmRelMapaEstaticSolic.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;

  //código novo
  DecodeDate(Now, ano, mes, dia);
  ano := StrToInt(Copy(IntToStr(ano),2,4));
  //fim código novo
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaEstaticSolic]);

end.
