unit U_FrmRelComprBaixaEmprBibli;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, jpeg, QuickRpt, ExtCtrls, DateUtil;

type
  TFrmRelComprBaixaEmprBibli = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel8: TQRLabel;
    QRLTotalItens: TQRLabel;
    QRLabel5: TQRLabel;
    qrlNomeUsuario: TQRLabel;
    QRLabel16: TQRLabel;
    qrlUsuEndereco: TQRLabel;
    QRLBairro: TQRLabel;
    qrlUsuCEPCidade: TQRLabel;
    DetailBand1: TQRBand;
    QRLabel21: TQRLabel;
    qrlDtEmprestimo: TQRLabel;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText1: TQRDBText;
    QRLISBN: TQRLabel;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRShape3: TQRShape;
    QRLAssUsuario: TQRLabel;
    QRLDocAss: TQRLabel;
    QRLNumDoc: TQRLabel;
    QRShape4: TQRShape;
    QRLTelefones: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText5: TQRDBText;
    QRLabel3: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    descUsu : String;
  end;

var
  FrmRelComprBaixaEmprBibli: TFrmRelComprBaixaEmprBibli;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelComprBaixaEmprBibli.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLNumDoc.Caption := 'Nº ' + FormatFloat('0000',QryRelatorio.FieldByName('CO_EMP').AsFloat) + '-' +
  FormatDateTime('yy', QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').asDateTime) + '/' + FormatDateTime('MM', QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').asDateTime) +
  '.'+FormatFloat('000000',QryRelatorio.FieldByName('CO_NUM_EMP').AsFloat);

  if not QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').IsNull then
    qrlDtEmprestimo.Caption := QryRelatorio.FieldByName('DT_EMPR_BIBLIOT').AsString
  else
    qrlDtEmprestimo.Caption := '-';

  QRLAssUsuario.Caption := QryRelatorio.FieldByName('no_col').AsString;
  QRLDocAss.Caption := 'Mat.: ' + FormatMaskText('99.999-9;0',QryRelatorio.FieldByName('co_mat_col').AsString);

  { Recupera a descrição do calendário }
  with DM.QrySql do
  begin
    { Grava a cabeça do calendário }
    Close;
    Sql.Clear;
    if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
    begin
      Sql.text := 'Select top 1 A.NO_ALU as NOME, A.CO_SEXO_ALU as SEXO, A.DE_ENDE_ALU as de_ende, A.NU_ENDE_ALU as nu_ende, A.DE_COMP_ALU as de_comp,'+
      'A.NU_TELE_RESI_ALU as teleResi, A.NU_TELE_CELU_ALU as teleCelu, B.NO_BAIRRO as bairro, C.NO_CIDADE as cidade, a.CO_ESTA_ALU as UF, A.CO_CEP_ALU as CEP, mm.co_alu_cad as numDoc  ' +
      ' From TB07_ALUNO A ' +
      ' join tb08_matrcur mm on a.co_alu = mm.co_alu and mm.co_emp = a.co_emp ' +
      ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
      ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_ALU = A.CO_ALU and UB.CO_EMP_ALU = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString +
      ' order by mm.co_ano_mes_mat desc';
    end
    else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
    begin
      Sql.text := 'Select A.NO_USU_BIB as NOME, A.CO_SEXO_USU_BIB as SEXO, A.DE_ENDE_USU_BIB as de_ende, A.NU_ENDE_USU_BIB as nu_ende, A.DE_COMP_ENDE_USU_BIB as de_comp,'+
      'A.NU_TELE_RESI_USU_BIB as teleResi, A.NU_TELE_CELU_USU_BIB as teleCelu, B.NO_BAIRRO as bairro, C.NO_CIDADE as cidade, a.CO_ESTA_ENDE_USU_BIB as UF, a.NU_CEP_ENDE_USU_BIB as CEP,' +
      'a.NU_CPF_USU_BIB as numDoc' +
      ' From TB205_USUARIO_BIBLIOT A ' +
      ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
      ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
      ' Where A.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and A.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end
    else
    begin
      Sql.text := 'Select A.NO_COL as NOME, A.CO_SEXO_COL as SEXO, A.DE_ENDE_COL as de_ende, A.NU_ENDE_COL as nu_ende, A.DE_COMP_ENDE_COL as de_comp,'+
      'A.NU_TELE_RESI_COL as teleResi, A.NU_TELE_CELU_COL as teleCelu, B.NO_BAIRRO as bairro, C.NO_CIDADE as cidade, a.CO_ESTA_ENDE_COL as UF, A.NU_CEP_ENDE_COL as CEP,' +
      'a.CO_MAT_COL as numDoc' +
      ' From TB03_COLABOR A ' +
      ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
      ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_COL = A.CO_COL and UB.CO_EMP_COL = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO_USU').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end;
    Open;

    if not IsEmpty then
    begin
      qrlNomeUsuario.Caption := UpperCase(FieldByName('NOME').AsString);

      if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
      begin
        qrlNomeUsuario.Caption := qrlNomeUsuario.Caption + ' (Outros)';
      end
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
      begin
        qrlNomeUsuario.Caption := qrlNomeUsuario.Caption + ' (Aluno)';
      end
      else
      begin
        qrlNomeUsuario.Caption := qrlNomeUsuario.Caption + ' (Funcionário)';
      end;

      if (not FieldByName('de_ende').IsNull) and (FieldByName('de_ende').AsString <> '') then
      begin
        qrlUsuEndereco.Caption := FieldByName('de_ende').AsString;
        if (not FieldByName('nu_ende').IsNull) and (FieldByName('nu_ende').AsString <> '') then
          qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ', ' + FieldByName('nu_ende').AsString;

        if (not FieldByName('de_comp').IsNull) and (FieldByName('de_comp').AsString <> '') then
          qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('de_comp').AsString;
      end
      else
        qrlUsuEndereco.Caption := '-';

      QRLBairro.Caption := 'Bairro: ';

      if not FieldByName('BAIRRO').IsNull then
        QRLBairro.Caption := QRLBairro.Caption + FieldByName('BAIRRO').AsString
      else
        QRLBairro.Caption := QRLBairro.Caption + ' - ';

      qrlUsuCEPCidade.Caption := 'CEP: ';

      if not FieldByName('CEP').IsNull then
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + FormatMaskText('00000-000;0', FieldByName('CEP').AsString)
      else
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + ' ***';

      if not FieldByName('CIDADE').IsNull then
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + ' - ' + FieldByName('CIDADE').AsString
      else
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + ' - ***';

      if not FieldByName('UF').IsNull then
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + ' - ' + FieldByName('UF').AsString
      else
        qrlUsuCEPCidade.Caption := qrlUsuCEPCidade.Caption + ' - ***';

      QRLTelefones.Caption := 'Telefone(s): ';
      
      if (not FieldByName('teleResi').IsNull) and (FieldByName('teleResi').AsString <> '') then
      begin
        QRLTelefones.Caption := QRLTelefones.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('teleResi').AsString);
        if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleCelu').AsString <> '') then
          QRLTelefones.Caption := QRLTelefones.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString);
      end
      else
      begin
        if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleCelu').AsString <> '') then
          QRLTelefones.Caption := QRLTelefones.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString)
        else
          QRLTelefones.Caption := QRLTelefones.Caption + ' -';
      end;

    end;
  end;
end;

procedure TFrmRelComprBaixaEmprBibli.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('CO_ISBN_ACER').IsNull then
    QRLISBN.Caption := FormatMaskText('000-00-0000-000-0;0',QryRelatorio.FieldByName('CO_ISBN_ACER').AsString)
  else
    QRLISBN.Caption := '-';

  if DetailBand1.Color = clWhite then
     DetailBand1.Color := $00D8D8D8
  else
     DetailBand1.Color := clWhite;

  QRLTotalItens.Caption := IntToStr(StrToInt(QRLTotalItens.Caption) + 1);
end;

procedure TFrmRelComprBaixaEmprBibli.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QRLTotalItens.Caption := '0';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelComprBaixaEmprBibli]);

end.
