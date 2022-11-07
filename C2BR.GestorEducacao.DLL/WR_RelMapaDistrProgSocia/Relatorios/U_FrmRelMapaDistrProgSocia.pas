unit U_FrmRelMapaDistrProgSocia;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaDistrProgSocia = class(TFrmRelTemplate)
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
    coCurso  : Array[1..9] of Integer;
    qtdCurso : Integer;
  public
    { Public declarations }
    modulo, anoRefer : String;
  end;

var
  FrmRelMapaDistrProgSocia: TFrmRelMapaDistrProgSocia;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaDistrProgSocia.DetailBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i, totalPS : Integer;
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

  for i := 1 to qtdCurso do
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select count(distinct m.co_alu) as numPS  from TB08_MATRCUR m '+
                  ' join TB136_ALU_PROG_SOCIAIS aps on aps.CO_ALU = m.CO_ALU and aps.CO_EMP = m.CO_EMP ' +
                  ' where m.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                  ' and m.CO_CUR = ' + IntToStr(coCurso[i]) +
                  ' and m.CO_MODU_CUR = ' + modulo +
                  ' and m.CO_ANO_MES_MAT = ' + anoRefer +
                  ' and aps.CO_IDENT_PROGR_SOCIA = ' + QryRelatorio.FieldByName('CO_IDENT_PROGR_SOCIA').AsString;
      Open;

      if not IsEmpty then
      begin
        case i of
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
   end;

   QRLTot.Caption := IntToStr(totalPS);
   QRLTotal.Caption := IntToStr(StrToInT(QRLTotal.Caption) + totalPS);

   if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
end;

procedure TFrmRelMapaDistrProgSocia.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i : Integer;
begin
  inherited;
  i := 1;
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct c.co_cur, c.co_sigl_cur, c.seq_impressao from tb43_grd_curso gc ' +
                ' join tb01_curso c on c.CO_cur = gc.CO_cur and c.CO_EMP = gc.CO_EMP and gc.co_modu_cur = c.co_modu_cur ' +
                ' where gc.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and gc.CO_MODU_CUR = ' + modulo +
                ' and gc.CO_ANO_GRADE = ' + anoRefer +
                ' order by c.seq_impressao';
    Open;

    qtdCurso := RecordCount;

    while not Eof do
    begin
      case i of
        1:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP1.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        2:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP2.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        3:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP3.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        4:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP4.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        5:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP5.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        6:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP6.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        7:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP7.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        8:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP8.Caption := FieldByName('co_sigl_cur').AsString;
        end;
        9:begin
          coCurso[i] := FieldByName('CO_CUR').AsInteger;
          QRLP9.Caption := FieldByName('co_sigl_cur').AsString;
        end;
      end;

      i := i + 1;
      Next;
    end;
  end;
end;

procedure TFrmRelMapaDistrProgSocia.QuickRep1BeforePrint(
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
