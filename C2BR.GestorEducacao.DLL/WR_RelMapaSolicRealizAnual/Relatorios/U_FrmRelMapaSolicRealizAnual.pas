unit U_FrmRelMapaSolicRealizAnual;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaSolicRealizAnual = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QrlAno01: TQRLabel;
    QrlAno02: TQRLabel;
    QrlAno03: TQRLabel;
    QrlAno04: TQRLabel;
    QrlAno06: TQRLabel;
    QrlAno07: TQRLabel;
    QrlAno08: TQRLabel;
    QrlAno09: TQRLabel;
    QrlAno10: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel1: TQRLabel;
    QrlAnoRef: TQRLabel;
    QrlAno11: TQRLabel;
    QryRelatorioNO_TIPO_SOLI: TStringField;
    QryRelatorioCO_TIPO_SOLI: TIntegerField;
    QRDBText1: TQRDBText;
    QrySolic: TADOQuery;
    QrlTotAno02: TQRLabel;
    QrlTotAno03: TQRLabel;
    QrlTotAno04: TQRLabel;
    QrlTotAno05: TQRLabel;
    QRLabel16: TQRLabel;
    QrlTotAno06: TQRLabel;
    QrlTotAno10: TQRLabel;
    QrlTotAno11: TQRLabel;
    QrlTotAno09: TQRLabel;
    QrlTotAno07: TQRLabel;
    QrlTotAno08: TQRLabel;
    QrlTOTAL: TQRLabel;
    QrlTotAno01: TQRLabel;
    QRLabel17: TQRLabel;
    QrlPage: TQRLabel;
    QRShape2: TQRShape;
    QrlAno05: TQRLabel;
    QrlNumAno09: TQRLabel;
    QrlNumAno08: TQRLabel;
    QrlNumAno07: TQRLabel;
    QrlNumAno06: TQRLabel;
    QrlNumAno05: TQRLabel;
    QrlNumAno04: TQRLabel;
    QrlNumAno03: TQRLabel;
    QrlNumAno02: TQRLabel;
    QrlNumAno01: TQRLabel;
    QrlNumAno10: TQRLabel;
    QrlNumAno11: TQRLabel;
    QrlTotAnos: TQRLabel;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa, anoIni, anoFim : String;
    total : Integer;
  end;

var
  FrmRelMapaSolicRealizAnual: TFrmRelMapaSolicRealizAnual;

implementation

{$R *.dfm}
uses U_DataModuleSGE;

procedure TFrmRelMapaSolicRealizAnual.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotAno01.Caption := '0';
  QrlTotAno02.Caption := '0';
  QrlTotAno03.Caption := '0';
  QrlTotAno04.Caption := '0';
  QrlTotAno05.Caption := '0';
  QrlTotAno06.Caption := '0';
  QrlTotAno07.Caption := '0';
  QrlTotAno08.Caption := '0';
  QrlTotAno09.Caption := '0';
  QrlTotAno10.Caption := '0';
  QrlTotAno11.Caption := '0';
  QrlTOTAL.Caption := '0';
  total := 0;
end;

procedure TFrmRelMapaSolicRealizAnual.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  i,j,totalParcial : integer;
begin
  inherited;
  // ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  totalParcial := 0;
  j := 1;
  for i := StrToInt(anoIni) to StrToInt(anoFim) do
  begin
    with QrySolic do
    begin
      Close;
      Sql.Clear;
      Sql.Text := 'SELECT COUNT(CO_TIPO_SOLI) as numSolic ' +
                  ' FROM TB65_HIST_SOLICIT '+
                  ' WHERE YEAR(DT_ENTR_SOLI) = '+ quotedStr(IntToStr(i)) +
                  ' AND CO_TIPO_SOLI = ' + QryRelatorioCO_TIPO_SOLI.AsString +
                  ' AND CO_EMP = ' + codigoEmpresa;
      Open;

      if not IsEmpty then
      begin
        case j of
          1:
          begin
            QrlAno01.Caption := IntToStr(i);
            QrlNumAno01.Caption := FieldByName('numSolic').AsString;
            QrlTotAno01.Caption := IntToStr(StrToInt(QrlTotAno01.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          2:
          begin
            QrlAno02.Caption := IntToStr(i);
            QrlNumAno02.Caption := FieldByName('numSolic').AsString;
            QrlTotAno02.Caption := IntToStr(StrToInt(QrlTotAno02.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          3:
          begin
            QrlAno03.Caption := IntToStr(i);
            QrlNumAno03.Caption := FieldByName('numSolic').AsString;
            QrlTotAno03.Caption := IntToStr(StrToInt(QrlTotAno03.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          4:
          begin
            QrlAno04.Caption := IntToStr(i);
            QrlNumAno04.Caption := FieldByName('numSolic').AsString;
            QrlTotAno04.Caption := IntToStr(StrToInt(QrlTotAno04.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          5:
          begin
            QrlAno05.Caption := IntToStr(i);
            QrlNumAno05.Caption := FieldByName('numSolic').AsString;
            QrlTotAno05.Caption := IntToStr(StrToInt(QrlTotAno05.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          6:
          begin
            QrlAno06.Caption := IntToStr(i);
            QrlNumAno06.Caption := FieldByName('numSolic').AsString;
            QrlTotAno06.Caption := IntToStr(StrToInt(QrlTotAno06.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          7:
          begin
            QrlAno07.Caption := IntToStr(i);
            QrlNumAno07.Caption := FieldByName('numSolic').AsString;
            QrlTotAno07.Caption := IntToStr(StrToInt(QrlTotAno07.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          8:
          begin
            QrlAno08.Caption := IntToStr(i);
            QrlNumAno08.Caption := FieldByName('numSolic').AsString;
            QrlTotAno08.Caption := IntToStr(StrToInt(QrlTotAno08.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          9:
          begin
            QrlAno09.Caption := IntToStr(i);
            QrlNumAno09.Caption := FieldByName('numSolic').AsString;
            QrlTotAno09.Caption := IntToStr(StrToInt(QrlTotAno09.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          10:
          begin
            QrlAno10.Caption := IntToStr(i);
            QrlNumAno10.Caption := FieldByName('numSolic').AsString;
            QrlTotAno10.Caption := IntToStr(StrToInt(QrlTotAno10.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
          11:
          begin
            QrlAno11.Caption := IntToStr(i);
            QrlNumAno11.Caption := FieldByName('numSolic').AsString;
            QrlTotAno11.Caption := IntToStr(StrToInt(QrlTotAno11.Caption) + FieldByName('numSolic').AsInteger);
            total := total + FieldByName('numSolic').AsInteger;
            totalParcial := totalParcial + FieldByName('numSolic').AsInteger;
          end;
        end;
      end;
    end;
    j := j + 1;
  end;
  QrlTotAnos.Caption := IntToStr(totalParcial);
end;

procedure TFrmRelMapaSolicRealizAnual.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QrlTOTAL.Caption := FloatToStrF(total,ffNumber,10,0);
end;

end.
