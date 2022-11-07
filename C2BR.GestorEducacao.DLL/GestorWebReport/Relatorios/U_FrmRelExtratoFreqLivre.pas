unit U_FrmRelExtratoFreqLivre;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls,
  QRPDFFilter;

type
  TFrmRelExtratoFreqLivre = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel2: TQRLabel;
    QRLabel13: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText3: TQRDBText;
    QRLabel17: TQRLabel;
    QRDBText4: TQRDBText;
    QRLabel16: TQRLabel;
    QRDBText5: TQRDBText;
    QRLabel1: TQRLabel;
    QrlAno: TQRLabel;
    QRShape1: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel18: TQRLabel;
    QRShape2: TQRShape;
    QrlMes1: TQRLabel;
    QRLabel19: TQRLabel;
    QRShape9: TQRShape;
    QRLabel20: TQRLabel;
    QrlMes2: TQRLabel;
    QRShape3: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel21: TQRLabel;
    QRShape4: TQRShape;
    QRLabel5: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape6: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel30: TQRLabel;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape11: TQRShape;
    QRShape8: TQRShape;
    QRShape10: TQRShape;
    QRShape7: TQRShape;
    QRShape5: TQRShape;
    QRDBText6: TQRDBText;
    QryRelatorioFrJaneiro: TIntegerField;
    QryRelatorioFrFevereiro: TIntegerField;
    QryRelatorioFrMarco: TIntegerField;
    QryRelatorioFrAbril: TIntegerField;
    QryRelatorioFrMaio: TIntegerField;
    QryRelatorioFrJunho: TIntegerField;
    QryRelatorioFrJulho: TIntegerField;
    QryRelatorioFrAgosto: TIntegerField;
    QryRelatorioFrSetembro: TIntegerField;
    QryRelatorioFrOutubro: TIntegerField;
    QryRelatorioFrNovembro: TIntegerField;
    QryRelatorioFrDezembro: TIntegerField;
    QryRelatoriono_fun: TStringField;
    QryRelatoriono_col: TStringField;
    QryRelatorioDT_NASC_COL: TDateTimeField;
    QryRelatorionu_cpf_col: TStringField;
    QryRelatorionu_tele_resi_col: TStringField;
    QRLPage: TQRLabel;
    QRLabel31: TQRLabel;
    QRDBText9: TQRDBText;
    QRDBText10: TQRDBText;
    QRDBText12: TQRDBText;
    QRDBText17: TQRDBText;
    QRDBText19: TQRDBText;
    QRDBText21: TQRDBText;
    QRDBText23: TQRDBText;
    QRDBText25: TQRDBText;
    QRDBText27: TQRDBText;
    QRDBText29: TQRDBText;
    QRDBText31: TQRDBText;
    qryHora: TADOQuery;
    QryRelatorioco_col: TIntegerField;
    lblHr1: TQRLabel;
    lblHr2: TQRLabel;
    lblHr4: TQRLabel;
    lblHr3: TQRLabel;
    lblHr8: TQRLabel;
    lblHr7: TQRLabel;
    lblHr6: TQRLabel;
    lblHr5: TQRLabel;
    lblHr12: TQRLabel;
    lblHr11: TQRLabel;
    lblHr10: TQRLabel;
    lblHr9: TQRLabel;
    qrlTotFr: TQRLabel;
    qrlTotHr: TQRLabel;
    QRShape14: TQRShape;
    SummaryBand1: TQRBand;
    qrLegenda: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLNoCol: TQRLabel;
    QryRelatoriofla_professor: TStringField;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa: String;
  end;

var
  FrmRelExtratoFreqLivre: TFrmRelExtratoFreqLivre;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelExtratoFreqLivre.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
//var
  //i : integer;
 // Hora : String;
 // Minutos : Integer;
  //theLbl : TQRLabel;
begin
  inherited;
  if Detail.Color = clWhite then
     Detail.Color := $00D8D8D8
  else
     Detail.Color := clWhite;
  {
  for i:=1 to 12 do
  begin
    with qryHora do
    begin
      Close;
      if i < 12 then
        SQL.Text := 'SET LANGUAGE PORTUGUESE ' +
          'SELECT HR_ACUM FROM tb174_freq_ele ' +
          'WHERE co_emp = ' + codigoEmpresa +
          ' AND co_usu = ' + QryRelatorioco_col.AsString +
          ' AND tp_freq = ' + QuotedStr('S') +
          ' AND dt_freq >= ' + QuotedStr('01-' + IntToStr(i) + '-' + QrlAno.Caption) +
          ' AND dt_freq < ' + QuotedStr('01-' + IntToStr(i + 1) + '-' + QrlAno.Caption)
      else
        SQL.Text := 'SET LANGUAGE PORTUGUESE ' +
          'SELECT HR_ACUM FROM tb174_freq_ele ' +
          'WHERE co_emp = ' + codigoEmpresa +
          ' AND co_usu = ' + QryRelatorioco_col.AsString +
          ' AND tp_freq = ' + QuotedStr('S') +
          ' AND dt_freq >= ' + QuotedStr('01-' + IntToStr(i) + '-' + QrlAno.Caption) +
          ' AND dt_freq <= ' + QuotedStr('31-' + IntToStr(i) + '-' + QrlAno.Caption);
      Open;

      Minutos := 0;
      while not Eof do
      begin
        if FieldByName('HR_ACUM').AsString <> '' then
          Hora := FormatFloat('0000',FieldByName('HR_ACUM').AsInteger)
        else
          Hora := '0000';
        Minutos := Minutos + (StrToInt(Copy(Hora,0,2))*60) + StrToInt(Copy(Hora,3,2));

        Next;
      end;

      Hora := FormatFloat('00',Minutos div 60) + ':' + FormatFloat('00',Minutos mod 60);

      case i of
        1  : lblHr1.Caption  := Hora;
        2  : lblHr2.Caption  := Hora;
        3  : lblHr3.Caption  := Hora;
        4  : lblHr4.Caption  := Hora;
        5  : lblHr5.Caption  := Hora;
        6  : lblHr6.Caption  := Hora;
        7  : lblHr7.Caption  := Hora;
        8  : lblHr8.Caption  := Hora;
        9  : lblHr9.Caption  := Hora;
        10 : lblHr10.Caption := Hora;
        11 : lblHr11.Caption := Hora;
        12 : lblHr12.Caption := Hora;
      end;
    end;
  end;

  //Total de frequ�ncia e horas trabalhadas;

  with qryHora do
  begin
    Close;
    Sql.Text := 'SET LANGUAGE PORTUGUESE ' +
          ' SELECT HR_ACUM FROM tb174_freq_ele ' +
          ' WHERE co_emp = ' + codigoEmpresa +
          '  AND co_usu = ' + QryRelatorioco_col.AsString +
          '  AND tp_freq = ' + QuotedStr('S') +
          '  AND dt_freq >= ' + QuotedStr('01-' + '01' + '-' + QrlAno.Caption) +
          '  AND dt_freq <= ' + QuotedStr('31-' + '12' + '-' + QrlAno.Caption);
    Open;
  end;

  Minutos := 0;
  while not qryHora.Eof do
  begin
    if qryHora.FieldByName('HR_ACUM').AsString <> '' then
      Hora := FormatFloat('0000',qryHora.FieldByName('HR_ACUM').AsInteger)
    else
      Hora := '0000';
    Minutos := Minutos + (StrToInt(Copy(Hora,0,2))*60) + StrToInt(Copy(Hora,3,2));

    qryHora.Next;
  end;

  //Hora := IntToStr(Minutos div 60) + ':' + IntToStr(Minutos mod 60);
  Hora := FormatFloat('00',Minutos div 60) + ':' + FormatFloat('00',Minutos mod 60);

  qrlTotHr.Caption := (Hora);}

  qrlTotFr.Caption := IntToStr(StrToInt(QryRelatorioFrJaneiro.AsString) + StrToInt(QryRelatorioFrFevereiro.AsString) + StrToInt(QryRelatorioFrMarco.AsString) + StrToInt(QryRelatorioFrAbril.AsString) + StrToInt(QryRelatorioFrMaio.AsString) + StrToInt(QryRelatorioFrJunho.AsString) + StrToInt(QryRelatorioFrJulho.AsString) + StrToInt(QryRelatorioFrAgosto.AsString) + StrToInt(QryRelatorioFrSetembro.AsString) + StrToInt(QryRelatorioFrOutubro.AsString) + StrToInt(QryRelatorioFrNovembro.AsString) + StrToInt(QryRelatorioFrDezembro.AsString));

end;

procedure TFrmRelExtratoFreqLivre.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatoriono_col.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatoriono_col.AsString)
  else
    QRLNoCol.Caption := '-';
     
  lblHr1.Caption := '-';
  lblHr2.Caption := '-';
  lblHr3.Caption := '-';
  lblHr4.Caption := '-';
  lblHr5.Caption := '-';
  lblHr6.Caption := '-';
  lblHr7.Caption := '-';
  lblHr8.Caption := '-';
  lblHr9.Caption := '-';
  lblHr10.Caption := '-';
  lblHr11.Caption := '-';
  lblHr12.Caption := '-';
  qrlTotHr.Caption := '-';
end;

procedure TFrmRelExtratoFreqLivre.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('fla_professor').AsString = 'S' then
    LblTituloRel.Caption := 'DEMONSTRATIVO ANUAL DE FREQ��NCIA - PROFESSOR'
  else
    LblTituloRel.Caption := 'DEMONSTRATIVO ANUAL DE FREQ��NCIA - FUNCION�RIO';
end;

end.
