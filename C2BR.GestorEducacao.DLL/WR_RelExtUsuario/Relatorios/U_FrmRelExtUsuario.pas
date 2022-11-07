unit U_FrmRelExtUsuario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelExtUsuario = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRGroup2: TQRGroup;
    QRLabel2: TQRLabel;
    QRLParametros: TQRLabel;
    QRShape6: TQRShape;
    QRBand1: TQRBand;
    QRDBText4: TQRDBText;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLDescUsu: TQRLabel;
    QRBand2: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel5: TQRLabel;
    QRLISBN: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTotal: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel9: TQRLabel;
    QRLTotGeral: TQRLabel;
    QRLDatas: TQRLabel;
    QRLabel7: TQRLabel;
    QRLDtEntrega: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
    total : integer;
  public
    { Public declarations }
  end;

var
  FrmRelExtUsuario: TFrmRelExtUsuario;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelExtUsuario.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',FormatFloat('0000000000000',QryRelatorio.FieldByName('CO_ISBN_ACER').AsFloat))
  else
    QRLISBN.Caption := '-';
    
  if QRBand1.Color = clWhite then
    QRBand1.Color := $00D8D8D8
  else
    QRBand1.Color := clWhite;

  if not QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').IsNull then
    QRLDatas.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').AsDateTime);

  if not QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').IsNull then
    QRLDatas.Caption := QRLDatas.Caption + ' - ' + FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_PREV_DEVO_ACER').AsDateTime);

  if not QryRelatorio.FieldByName('DT_REAL_DEVO_ACER').IsNull then
    QRLDtEntrega.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_REAL_DEVO_ACER').AsDateTime)
  else
    QRLDtEntrega.Caption := '-';

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);

  total := total + 1;
end;

procedure TFrmRelExtUsuario.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLDescUsu.Caption := '';
  
  { Recupera a descrição do calendário }
  with DM.QrySql do
  begin
    { Grava a cabeça do calendário }
    Close;
    Sql.Clear;
    if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
    begin
      Sql.text := 'Select top 1 A.NO_ALU as NOME, mm.co_alu_cad as numDoc  ' +
      ' From TB07_ALUNO A ' +
      ' left join tb08_matrcur mm on a.co_alu = mm.co_alu and mm.co_emp = a.co_emp ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_ALU = A.CO_ALU and UB.CO_EMP_ALU = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString +
      ' order by mm.co_ano_mes_mat desc';
    end
    else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
    begin
      Sql.text := 'Select A.NO_USU_BIB as NOME, a.NU_CPF_USU_BIB as numDoc' +
      ' From TB205_USUARIO_BIBLIOT A ' +
      ' Where A.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and A.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end
    else
    begin
      Sql.text := 'Select A.NO_COL as NOME, a.CO_MAT_COL as numDoc' +
      ' From TB03_COLABOR A ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_COL = A.CO_COL and UB.CO_EMP_COL = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end;
    Open;

    if not IsEmpty then
    begin
      if not FieldByName('numDoc').IsNull then
      begin
        if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
          QRLDescUsu.Caption := FormatMaskText('000.000.000-00;0',FieldByName('numDoc').AsString)
        else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
          QRLDescUsu.Caption := FormatMaskText('00.000.000000;0',FieldByName('numDoc').AsString)
        else
          QRLDescUsu.Caption := FormatMaskText('99.999-9;0',FieldByName('numDoc').AsString);

          QRLDescUsu.Caption := QRLDescUsu.Caption + ' / ' + UpperCase(FieldByName('NOME').AsString);
      end
      else
        QRLDescUsu.Caption := '****** / ' + UpperCase(FieldByName('NOME').AsString);

      if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
        QRLDescUsu.Caption := QRLDescUsu.Caption + ' ( Aluno )'
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
        QRLDescUsu.Caption := QRLDescUsu.Caption + ' ( Usuário Bibioteca )'
      else
        QRLDescUsu.Caption := QRLDescUsu.Caption + ' ( Funcionário/Professor )';

    end;
  end;

end;

procedure TFrmRelExtUsuario.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
  total := 0;
end;

procedure TFrmRelExtUsuario.SummaryBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotGeral.Caption := IntToStr(total);
end;

procedure TFrmRelExtUsuario.QRBand2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelExtUsuario]);

end.
