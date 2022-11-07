unit U_FrmRelExtOcorEstagAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelExtOcorEstagAluno = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRBand2: TQRBand;
    QRLTotalAluno: TQRLabel;
    QRLabel2: TQRLabel;
    QRLPage: TQRLabel;
    QRDBText9: TQRDBText;
    QRLabelDataAbreviada: TQRLabel;
    qrlTpAval: TQRLabel;
    QrlOrigem: TQRLabel;
    QRDBText2: TQRDBText;
    QRGroup1: TQRGroup;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape1: TQRShape;
    QRLabel7: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLCandidato: TQRLabel;
    QRLDtOcorr: TQRLabel;
    QRLDtCadastro: TQRLabel;
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    Cont, ContAlunos : Integer;
    TotalAluno : Integer;
  public
    { Public declarations }
  end;

var
  FrmRelExtOcorEstagAluno: TFrmRelExtOcorEstagAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelExtOcorEstagAluno.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  Cont := 0;
end;

procedure TFrmRelExtOcorEstagAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  ContAlunos := 0;
  TotalAluno := 0;
end;

procedure TFrmRelExtOcorEstagAluno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;

  if not QryRelatorio.FieldByName('DT_OCORR').IsNull then
    QRLDtOcorr.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_OCORR').AsDateTime)
  else
    QRLDtOcorr.Caption := '-';

  if not QryRelatorio.FieldByName('DT_CADASTRO').IsNull then
    QRLDtCadastro.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_CADASTRO').AsDateTime)
  else
    QRLDtCadastro.Caption := '-';

  if QryRelatorio.FieldByName('TP_AVAL').AsString = 'P' then
    qrlTpAval.Caption := 'Positiva'
  else
    qrlTpAval.Caption := 'Negativa';

  if QryRelatorio.FieldByName('ORIGEM_OCORR').AsString = 'E' then
    QrlOrigem.Caption := 'Empresa'
  else
    QrlOrigem.Caption := 'Instituição';

  TotalAluno := TotalAluno + 1;

  if QRBand2.Color = clWhite then
     QRBand2.Color := $00D8D8D8
  else
     QRBand2.Color := clWhite;
end;

procedure TFrmRelExtOcorEstagAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotalAluno.Caption := IntToStr(TotalAluno);
end;

procedure TFrmRelExtOcorEstagAluno.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  ano,mes,dia: word;
begin
  inherited;
  //código novo
  DecodeDate(Now, ano, mes, dia);
  ano := StrToInt(Copy(IntToStr(ano),2,4));
  QRLabelDataAbreviada.Caption := FormatFloat('00',dia) + '/' + FormatFloat('00',mes) + '/' + FormatFloat('00',ano);
  //fim código novo
end;

procedure TFrmRelExtOcorEstagAluno.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'O' then
  begin
    QRLCandidato.Caption := UpperCase(QryRelatorio.FieldByName('NO_CANDID_ESTAG').AsString) + ' (Outros)';
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
        //diasnoano := 365.6;

        QRLCandidato.Caption := UpperCase(FieldByName('nome').AsString);

        if QryRelatorio.FieldByName('TP_CANDID_ESTAG').AsString = 'A' then
        begin
          //QrlMatricula.Caption := FormatMaskText('00.000.000000;0',FieldByName('matric').AsString);
          QRLCandidato.Caption := QRLCandidato.Caption + ' (Aluno)';
        end
        else
        begin
          //QrlMatricula.Caption := FormatMaskText('00.000-00;0',FieldByName('matric').AsString);
          QRLCandidato.Caption := QRLCandidato.Caption + ' (Funcionário/Profesor)'
        end;

        {if FieldByName('sexo').AsString = 'M'  then
          QRLSexo.Caption := 'Masculino'
        else
          QRLSexo.Caption := 'Feminino';

        QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,Fieldbyname('dtNasc').AsDateTime) / diasnoano));

        if (not FieldByName('tel').IsNull) and (FieldByName('tel').AsString <> '')  then
          QRLTelefone.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('tel').AsString)
        else
          QRLTelefone.Caption := ' - ';    }
      end
    end;
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelExtOcorEstagAluno]);

end.
