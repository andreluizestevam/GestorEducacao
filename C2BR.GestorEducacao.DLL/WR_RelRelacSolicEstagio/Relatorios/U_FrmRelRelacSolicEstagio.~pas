unit U_FrmRelRelacSolicEstagio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacSolicEstagio = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRGroup1: TQRGroup;
    QRLabel10: TQRLabel;
    QRDBText1: TQRDBText;
    QrlMatricula: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel6: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel11: TQRLabel;
    QrlTotal: TQRLabel;
    QRGroupFooter: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel3: TQRLabel;
    QRLNomeCandidato: TQRLabel;
    QRLSexo: TQRLabel;
    QRLIdade: TQRLabel;
    QRLTelefone: TQRLabel;
    QRLStaSolic: TQRLabel;
    QRDBText2: TQRDBText;
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacSolicEstagio: TFrmRelRelacSolicEstagio;

implementation

{$R *.dfm}
uses U_DataModuleSGE, MaskUtils, DateUtils;

procedure TFrmRelRelacSolicEstagio.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
    diasnoano : real;
begin
  inherited;
  // ZEBRADO
  if DetailBand1.Color = clWhite then
    DetailBand1.Color := $00D8D8D8
  else
    DetailBand1.Color := clWhite;

  if QryRelatorio.FieldByName('CO_STATUS').AsString = 'A' then
    QRLStaSolic.Caption := 'Ativo'
  else
    QRLStaSolic.Caption := 'Inativo';
    
  if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'O' then
  begin
    QrlMatricula.Caption := '-';
    QRLNomeCandidato.Caption := UpperCase(QryRelatorio.FieldByName('NO_CANDID_ESTAG').AsString);
    QRLIdade.Caption := '-';
    QRLSexo.Caption := '-';
    QRLTelefone.Caption := '-';
  end
  else
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'A' then
      begin
        SQL.Text := 'select top 1 a.no_alu as nome, a.dt_nasc_alu as dtNasc, a.co_sexo_alu as sexo, a.nu_tele_celu_alu as tel, m.co_alu_cad as matric from tb07_aluno a '+
                  'left join tb08_matrcur m on m.co_emp = a.co_emp and m.co_alu = a.co_alu ' +
                  'where a.co_emp = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString +
                  ' and a.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' order by m.co_ano_mes_mat desc'
      end
      else
      begin
        SQL.Text := 'select no_col as nome, co_mat_col as matric, dt_nasc_col as dtNasc, co_sexo_col as sexo, nu_tele_celu_col as tel from tb03_colabor '+
                  'where co_emp = ' + QryRelatorio.FieldByName('CO_EMP_COL').AsString +
                  ' and co_col = ' +  QryRelatorio.FieldByName('CO_COL').AsString;
      end;

      Open;

      if not IsEmpty then
      begin
        diasnoano := 365.6;

        if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'A' then
          QrlMatricula.Caption := FormatMaskText('00.000.000000;0',FieldByName('matric').AsString)
        else
          QrlMatricula.Caption := FormatMaskText('00.000-00;0',FieldByName('matric').AsString);

        QRLNomeCandidato.Caption := UpperCase(FieldByName('nome').AsString);

        if FieldByName('sexo').AsString = 'M'  then
          QRLSexo.Caption := 'Masculino'
        else
          QRLSexo.Caption := 'Feminino';

        QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,Fieldbyname('dtNasc').AsDateTime) / diasnoano));

        if (not FieldByName('tel').IsNull) and (FieldByName('tel').AsString <> '')  then
          QRLTelefone.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('tel').AsString)
        else
          QRLTelefone.Caption := ' - ';
      end
    end;
  end;

  // TOTAL DE UNIDADES ESCOLARES
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + 1);
end;

procedure TFrmRelRelacSolicEstagio.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
end;

end.
