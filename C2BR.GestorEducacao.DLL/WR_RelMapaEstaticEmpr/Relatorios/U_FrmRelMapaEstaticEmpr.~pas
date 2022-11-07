unit U_FrmRelMapaEstaticEmpr;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaEstaticEmpr = class(TFrmRelTemplate)
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLabel4: TQRLabel;
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
    QryAux: TADOQuery;
    QRGroup1: TQRGroup;
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
    qrlAno: TQRLabel;
    QRLabel1: TQRLabel;
    qrlObra: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    TotalParcial, TotalRelatorio : array[1..12] of integer;
    TotalP, TotalR : integer;
    TipoSoli : Integer;
  public
    itemtipo : integer;
    itemvisualiza : integer;
    Ano : integer;
  end;

var
  FrmRelMapaEstaticEmpr: TFrmRelMapaEstaticEmpr;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaEstaticEmpr.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i : integer;
  Tot : integer;
  Inicio : TDateTime;
  Fim : TDateTime;
begin
  inherited;
  
  qrlObra.Caption := QryRelatorio.FieldByName('CO_ISBN_ACER').AsString + ' / ' + QryRelatorio.FieldByName('NO_ACERVO').AsString;


  Tot := 0;
  for  i:=1 to 12 do
  begin
    with QryAux do
    begin
      Close;
      SQL.Clear;
      SQL.Add(' SELECT COUNT(*) AS Soma ');
      SQL.Add(' FROM tb123_empr_bib_itens ebi ');
      SQL.Add(' join TB36_EMPR_BIBLIOT e on ebi.CO_NUM_EMP = e.CO_NUM_EMP ');
      SQL.Add(' WHERE ebi.CO_ISBN_ACER = ' + QryRelatorio.FieldByName('CO_ISBN_ACER').AsString);

      inicio := StrToDate('01/' + IntToStr(i) + '/' + IntToStr(ano));
      if ((i=1) or (i=3) or (i=5) or (i=7) or (i=8) or (i=10) or (i=12)) then
        fim := StrToDate('31/' + IntToStr(i) + '/' + IntToStr(ano))
      else if i=2 then
        fim := StrToDate('28/' + IntToStr(i) + '/' + IntToStr(ano))
      else
        fim := StrToDate('30/' + IntToStr(i) + '/' + IntToStr(ano));

      SQL.Add(' AND e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString);
      SQL.Add(' AND e.DT_EMPR_BIBLIOT >= ' + QuotedStr(DateToStr(inicio)));
      SQL.Add(' AND e.DT_EMPR_BIBLIOT <= ' + QuotedStr(DateToStr(fim)));
      Open;
    end;
    Tot := Tot + QryAux.FieldByName('Soma').AsInteger;
    TotalParcial[i] := TotalParcial[i] + QryAux.FieldByName('Soma').AsInteger;
    TotalRelatorio[i] := TotalRelatorio[i] + QryAux.FieldByName('Soma').AsInteger;
    QryAux.Next;
    case i of
      1  : m1.Caption := QryAux.FieldByName('Soma').AsString;
      2  : m2.Caption := QryAux.FieldByName('Soma').AsString;
      3  : m3.Caption := QryAux.FieldByName('Soma').AsString;
      4  : m4.Caption := QryAux.FieldByName('Soma').AsString;
      5  : m5.Caption := QryAux.FieldByName('Soma').AsString;
      6  : m6.Caption := QryAux.FieldByName('Soma').AsString;
      7  : m7.Caption := QryAux.FieldByName('Soma').AsString;
      8  : m8.Caption := QryAux.FieldByName('Soma').AsString;
      9  : m9.Caption := QryAux.FieldByName('Soma').AsString;
      10 : m10.Caption := QryAux.FieldByName('Soma').AsString;
      11 : m11.Caption := QryAux.FieldByName('Soma').AsString;
      12 : m12.Caption := QryAux.FieldByName('Soma').AsString;
    end;
  end;
  qrlTotAnual.Caption := IntToStr(Tot);
  TotalP := TotalP + Tot;
  qrlTotal.Caption := IntToStr(StrToInt(qrlTotal.Caption) + StrToInt(qrlTotAnual.Caption));
end;

procedure TFrmRelMapaEstaticEmpr.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
var i : integer;
begin
  inherited;
  if itemTipo = 1 then
  begin
    QRBand1.Height := 0;
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

procedure TFrmRelMapaEstaticEmpr.QRBand3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var i : integer;
begin
  inherited;
  for  i:=1 to 12 do
  begin
    case i of
      1  : qrlTotRel1.Caption := IntToStr(TotalRelatorio[i]);
      2  : qrlTotRel2.Caption := IntToStr(TotalRelatorio[i]);
      3  : qrlTotRel3.Caption := IntToStr(TotalRelatorio[i]);
      4  : qrlTotRel4.Caption := IntToStr(TotalRelatorio[i]);
      5  : qrlTotRel5.Caption := IntToStr(TotalRelatorio[i]);
      6  : qrlTotRel6.Caption := IntToStr(TotalRelatorio[i]);
      7  : qrlTotRel7.Caption := IntToStr(TotalRelatorio[i]);
      8  : qrlTotRel8.Caption := IntToStr(TotalRelatorio[i]);
      9  : qrlTotRel9.Caption := IntToStr(TotalRelatorio[i]);
      10  : qrlTotRel10.Caption := IntToStr(TotalRelatorio[i]);
      11  : qrlTotRel11.Caption := IntToStr(TotalRelatorio[i]);
      12  : qrlTotRel12.Caption := IntToStr(TotalRelatorio[i]);
    end;
  end;
  qrlTotal.Caption := IntToStr(StrToInt(qrlTotRel1.Caption) + StrToInt(qrlTotRel2.Caption) + StrToInt(qrlTotRel3.Caption) + StrToInt(qrlTotRel4.Caption) + StrToInt(qrlTotRel5.Caption) + StrToInt(qrlTotRel6.Caption) +
  StrToInt(qrlTotRel7.Caption) + StrToInt(qrlTotRel8.Caption) + StrToInt(qrlTotRel9.Caption) + StrToInt(qrlTotRel10.Caption) + StrToInt(qrlTotRel11.Caption) + StrToInt(qrlTotRel12.Caption));
end;

procedure TFrmRelMapaEstaticEmpr.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  inherited;
  Free;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaEstaticEmpr]);

end.
