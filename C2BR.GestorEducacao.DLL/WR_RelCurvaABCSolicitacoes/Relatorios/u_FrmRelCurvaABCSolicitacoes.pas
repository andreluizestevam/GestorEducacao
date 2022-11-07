unit U_FrmRelCurvaABCSolicitacoes;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelCurvaABCSolicitacoes = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRBand2: TQRBand;
    qrdbAluno: TQRDBText;
    qrdbQtdeDoc: TQRDBText;
    QRBand3: TQRBand;
    QRLabel5: TQRLabel;
    QRExpr1: TQRExpr;
    QrlSituacao: TQRLabel;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRLabelDataAbreviada: TQRLabel;
    QRSysData4: TQRSysData;
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelCurvaABCSolicitacoes: TFrmRelCurvaABCSolicitacoes;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCurvaABCSolicitacoes.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if QRBand2.Color = $00D8D8D8 then
  begin
     QRBand2.Color := clWhite;
  end
  else
  begin
     QRBand2.Color := $00D8D8D8;
  end;
end;

procedure TFrmRelCurvaABCSolicitacoes.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;
  //adicionado em: sex-06.set.08
  //adicionado por: JJJr

  //código novo
  DecodeDate(Now, ano, mes, dia);
  ano := StrToInt(Copy(IntToStr(ano),2,4));
  QRLabelDataAbreviada.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);
  //fim código novo
end;

end.
