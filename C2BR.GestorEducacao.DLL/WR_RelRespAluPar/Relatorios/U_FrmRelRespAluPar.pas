unit U_FrmRelRespAluPar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRespAluPar = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLParam: TQRLabel;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText1: TQRDBText;
    QryRelatoriono_resp: TStringField;
    QryRelatorioco_sexo_resp: TStringField;
    QryRelatoriodt_nasc_resp: TDateTimeField;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatorioNO_BAIRRO: TStringField;
    QryRelatorionu_cpf_resp: TStringField;
    QryRelatorionu_tele_resi_resp: TStringField;
    QryRelatoriodes_email_resp: TStringField;
    QRDBText2: TQRDBText;
    QrlCidadeBairro: TQRLabel;
    QRDBText3: TQRDBText;
    QrlIdade: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel15: TQRLabel;
    QryRelatorioGRAUINSTRUCAO: TStringField;
    QRLQtaIn: TQRLabel;
    QryRelatorioco_resp: TAutoIncField;
    QRLQtaUE: TQRLabel;
    QRLQtaST: TQRLabel;
    QRDBText7: TQRDBText;
    QryRelatorioPARENTESCO: TStringField;
    QrlTelResp: TQRLabel;
    QrlTotal: TQRLabel;
    QryRelatorionu_tele_celu_resp: TStringField;
    QryRelatorionu_tele_come_resp: TStringField;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    CodigoCurso,CodigoTurma,CodigoModulo, codigoEmpresa : String;
  end;

var
  FrmRelRespAluPar: TFrmRelRespAluPar;

implementation

{$R *.dfm}

uses U_DataModuleSGE, DateUtils, MaskUtils;

procedure TFrmRelRespAluPar.DetailBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var diasnoano : real;
begin
  inherited;

  if not QryRelatorionu_tele_celu_resp.IsNull then
  begin
    QrlTelResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('nu_tele_celu_resp').AsString) + ' C';
  end
  else
  if not QryRelatorionu_tele_resi_resp.IsNull then
  begin
    QrlTelResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('nu_tele_resi_resp').AsString) + ' R';
  end
  else
  if not QryRelatorionu_tele_come_resp.IsNull then
  begin
    QrlTelResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('nu_tele_come_resp').AsString) + ' T';
  end
  else
  begin
    QrlTelResp.Caption := '-';
  end;

  QRLQtaIn.Caption := '0';
  QRLQtaUE.Caption := '0';
  QRLQtaST.Caption := '0';

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select count(mm.co_alu) as total from tb08_matrcur mm ' +
                'join tb07_aluno a on mm.co_alu = a.co_alu ' +
                'join tb108_responsavel r on a.co_resp = r.co_resp ' +
                'where mm.co_sit_mat =' + QuotedStr('A') +
                'and r.co_resp = ' + QryRelatorioco_resp.AsString;
    Open;
    if not IsEmpty then
      QRLQtaIn.Caption := FieldByName('total').AsString;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select count(mm.co_alu) as total from tb08_matrcur mm ' +
                'join tb07_aluno a on mm.co_alu = a.co_alu ' +
                'join tb108_responsavel r on a.co_resp = r.co_resp ' +
                'where mm.co_sit_mat =' + QuotedStr('A') +
                ' and r.co_resp = ' + QryRelatorioco_resp.AsString +
                ' and mm.co_emp = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
      QRLQtaUE.Caption := FieldByName('total').AsString;
  end;

  if (CodigoCurso <> '') and (CodigoTurma <> '') then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select count(mm.co_alu) as total from tb08_matrcur mm ' +
                  'join tb07_aluno a on mm.co_alu = a.co_alu ' +
                  'join tb108_responsavel r on a.co_resp = r.co_resp ' +
                  'where mm.co_sit_mat =' + QuotedStr('A') +
                  ' and r.co_resp = ' + QryRelatorioco_resp.AsString +
                  ' and mm.co_emp = ' + codigoEmpresa +
                  ' and mm.co_cur = ' + CodigoCurso +
                  ' and mm.co_tur = ' + CodigoTurma +
                  ' and mm.co_modu_cur = ' + CodigoModulo;
      Open;

      if not IsEmpty then
        QRLQtaST.Caption := FieldByName('total').AsString;
    end;
  end
  else
    if (CodigoCurso = '') and (CodigoTurma = '') then
    begin
      QRLQtaST.Caption := QRLQtaUE.Caption;
    end
  else
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select count(mm.co_alu) as total from tb08_matrcur mm ' +
                  'join tb07_aluno a on mm.co_alu = a.co_alu ' +
                  'join tb108_responsavel r on a.co_resp = r.co_resp ' +
                  'where mm.co_sit_mat =' + QuotedStr('A') +
                  ' and r.co_resp = ' + QryRelatorioco_resp.AsString +
                  ' and mm.co_emp = ' + codigoEmpresa +
                  ' and mm.co_cur = ' + CodigoCurso +
                  ' and mm.co_modu_cur = ' + CodigoModulo;
      Open;

      if not IsEmpty then
        QRLQtaST.Caption := FieldByName('total').AsString;
    end;
  end;

  QrlCidadeBairro.Caption := QryRelatorioNO_CIDADE.AsString + '/' + QryRelatorioNO_BAIRRO.AsString;

  diasnoano := 365.6;
  QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_RESP').AsDateTime) / diasnoano));

  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00EBEBEB
  else
    DetailBand1.Color := clWhite;

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);

end;

procedure TFrmRelRespAluPar.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

end.
