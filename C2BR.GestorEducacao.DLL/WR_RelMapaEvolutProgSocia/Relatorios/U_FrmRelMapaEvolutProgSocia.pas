unit U_FrmRelMapaEvolutProgSocia;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaEvolutProgSocia = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    SummaryBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLP1: TQRLabel;
    QRLP2: TQRLabel;
    QRLP3: TQRLabel;
    QRLP4: TQRLabel;
    QRLP5: TQRLabel;
    QRLP6: TQRLabel;
    QRLP7: TQRLabel;
    QRLP8: TQRLabel;
    QRLabel14: TQRLabel;
    QRShape2: TQRShape;
    QRDBText1: TQRDBText;
    QRLTot: TQRLabel;
    QRLTot1: TQRLabel;
    QRLTot2: TQRLabel;
    QRLTot3: TQRLabel;
    QRLTot4: TQRLabel;
    QRLTot5: TQRLabel;
    QRLTot6: TQRLabel;
    QRLTot7: TQRLabel;
    QRLTot8: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel16: TQRLabel;
    QRLParam: TQRLabel;
    QRLN1: TQRLabel;
    QRLN2: TQRLabel;
    QRLN3: TQRLabel;
    QRLN4: TQRLabel;
    QRLN5: TQRLabel;
    QRLN6: TQRLabel;
    QRLN7: TQRLabel;
    QRLN8: TQRLabel;
    QRLP9: TQRLabel;
    QRLN9: TQRLabel;
    QRLTot9: TQRLabel;
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
    anoRefer : Integer;
  end;

var
  FrmRelMapaEvolutProgSocia: TFrmRelMapaEvolutProgSocia;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaEvolutProgSocia.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i, totalPS, j : Integer;
begin
  inherited;
  QRLN1.Caption := '-';
  QRLN2.Caption := '-';
  QRLN3.Caption := '-';
  QRLN4.Caption := '-';
  QRLN5.Caption := '-';
  QRLN6.Caption := '-';
  QRLN7.Caption := '-';
  QRLN8.Caption := '-';
  QRLN9.Caption := '-';
  QRLTot.Caption := '-';
  totalPs := 0;
  j := 1;

  for i := anoRefer - 4 to anoRefer + 4 do
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select count(distinct aps.co_alu) as numPS  from TB136_ALU_PROG_SOCIAIS aps ' +
                  ' where aps.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                  ' and year(aps.DT_CADAS_ALU_PRGSOC) = ' + IntToStr(i) +
                  ' and aps.CO_IDENT_PROGR_SOCIA = ' + QryRelatorio.FieldByName('CO_IDENT_PROGR_SOCIA').AsString;
      Open;

      if not IsEmpty then
      begin
        case j of
          1:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot1.Caption := IntToStr(StrToInt(QRLTot1.Caption) + FieldByName('numPS').AsInteger);
            QRLN1.Caption := FieldByName('numPS').AsString;
          end;
          2:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot2.Caption := IntToStr(StrToInt(QRLTot2.Caption) + FieldByName('numPS').AsInteger);
            QRLN2.Caption := FieldByName('numPS').AsString;
          end;
          3:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot3.Caption := IntToStr(StrToInt(QRLTot3.Caption) + FieldByName('numPS').AsInteger);
            QRLN3.Caption := FieldByName('numPS').AsString;
          end;
          4:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot4.Caption := IntToStr(StrToInt(QRLTot4.Caption) + FieldByName('numPS').AsInteger);
            QRLN4.Caption := FieldByName('numPS').AsString;
          end;
          5:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot5.Caption := IntToStr(StrToInt(QRLTot5.Caption) + FieldByName('numPS').AsInteger);
            QRLN5.Caption := FieldByName('numPS').AsString;
          end;
          6:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot6.Caption := IntToStr(StrToInt(QRLTot6.Caption) + FieldByName('numPS').AsInteger);
            QRLN6.Caption := FieldByName('numPS').AsString;
          end;
          7:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot7.Caption := IntToStr(StrToInt(QRLTot7.Caption) + FieldByName('numPS').AsInteger);
            QRLN7.Caption := FieldByName('numPS').AsString;
          end;
          8:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot8.Caption := IntToStr(StrToInt(QRLTot8.Caption) + FieldByName('numPS').AsInteger);
            QRLN8.Caption := FieldByName('numPS').AsString;
          end;
          9:begin
            totalPS := totalPS + FieldByName('numPS').AsInteger;
            QRLTot9.Caption := IntToStr(StrToInt(QRLTot9.Caption) + FieldByName('numPS').AsInteger);
            QRLN9.Caption := FieldByName('numPS').AsString;
          end;
        end;
      end;
     end;
    j := j + 1;
  end;

   QRLTot.Caption := IntToStr(totalPS);
   QRLTotal.Caption := IntToStr(StrToInT(QRLTotal.Caption) + totalPS);

   if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelMapaEvolutProgSocia.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i, j : Integer;
begin
  inherited;
  j := 1;
  for i := anoRefer - 4 to anoRefer + 4 do
  begin
    case j of
      1:begin
        QRLP1.Caption := IntToStr(i);
      end;
      2:begin
        QRLP2.Caption := IntToStr(i);
      end;
      3:begin
        QRLP3.Caption := IntToStr(i);
      end;
      4:begin
        QRLP4.Caption := IntToStr(i);
      end;
      5:begin
        QRLP5.Caption := IntToStr(i);
      end;
      6:begin
        QRLP6.Caption := IntToStr(i);
      end;
      7:begin
        QRLP7.Caption := IntToStr(i);
      end;
      8:begin
        QRLP8.Caption := IntToStr(i);
      end;
      9:begin
        QRLP9.Caption := IntToStr(i);
      end;
    end;
    j := j + 1;
  end;
end;

procedure TFrmRelMapaEvolutProgSocia.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLP1.Caption := '-';
  QRLP2.Caption := '-';
  QRLP3.Caption := '-';
  QRLP4.Caption := '-';
  QRLP5.Caption := '-';
  QRLP6.Caption := '-';
  QRLP7.Caption := '-';
  QRLP8.Caption := '-';
  QRLP9.Caption := '-';

  QRLTot1.Caption := '0';
  QRLTot2.Caption := '0';
  QRLTot3.Caption := '0';
  QRLTot4.Caption := '0';
  QRLTot5.Caption := '0';
  QRLTot6.Caption := '0';
  QRLTot7.Caption := '0';
  QRLTot8.Caption := '0';
  QRLTot9.Caption := '0';

  QRLTotal.Caption := '0';

end;

end.
