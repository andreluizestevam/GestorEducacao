unit U_FrmRelRelacUsuBiblioteca;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelRelacUsuBiblioteca = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    Detail: TQRBand;
    QRLabel7: TQRLabel;
    QRLPage: TQRLabel;
    QRLTelefones: TQRLabel;
    QRLNumDoc: TQRLabel;
    qrlDtNasc: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel14: TQRLabel;
    QRLTotal: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLDtCadastro: TQRLabel;
    QRDBText2: TQRDBText;
    QRLStatus: TQRLabel;
    QRLabel9: TQRLabel;
    QRLTpUsuario: TQRLabel;
    QRLUsuario: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelRelacUsuBiblioteca: TFrmRelRelacUsuBiblioteca;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelRelacUsuBiblioteca.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_USU_BIB').IsNull then
    QRLUsuario.Caption := UpperCase(QryRelatorio.FieldByName('NO_USU_BIB').AsString)
  else
    QRLUsuario.Caption := '-';

  QRLTelefones.Caption := '';
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
      ' left join tb08_matrcur mm on a.co_alu = mm.co_alu and mm.co_emp = a.co_emp ' +
      ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
      ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
      ' JOIN TB205_USUARIO_BIBLIOT UB on UB.CO_ALU = A.CO_ALU and UB.CO_EMP_ALU = A.CO_EMP ' +
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO').AsString +
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
      ' Where A.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO').AsString +
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
      ' Where UB.ORG_CODIGO_ORGAO = ' + QryRelatorio.FieldByName('ORG_CODIGO_ORGAO').AsString +
      ' and UB.CO_USUARIO_BIBLIOT = ' + QryRelatorio.FieldByName('CO_USUARIO_BIBLIOT').AsString;
    end;
    Open;

    if not IsEmpty then
    begin

      if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
        QRLNumDoc.Caption := FormatMaskText('000.000.000-00;0',FieldByName('numDoc').AsString)
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
      begin
        if not FieldByName('numDoc').IsNull then
          QRLNumDoc.Caption := FormatMaskText('00.000.000000;0',FieldByName('numDoc').AsString)
        else
          QRLNumDoc.Caption := '-';
      end
      else
        QRLNumDoc.Caption := FormatMaskText('99.999-9;0',FieldByName('numDoc').AsString);
      {
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
      
      qrlUsuEndereco.Caption := ' - Bairro: ';

      if not FieldByName('BAIRRO').IsNull then
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + FieldByName('BAIRRO').AsString
      else
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ';

      qrlUsuEndereco.Caption := ' - CEP: ';

      if not FieldByName('CEP').IsNull then
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + FormatMaskText('00000-000;0', FieldByName('CEP').AsString)
      else
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' ***';

      if not FieldByName('CIDADE').IsNull then
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('CIDADE').AsString;
      //else
        //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ***';

      if not FieldByName('UF').IsNull then
        qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('UF').AsString;
      //else
        //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ***';

      if (not FieldByName('teleResi').IsNull) and (FieldByName('teleResi').AsString <> '') then
      begin
        QRLTelefones.Caption := QRLTelefones.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('teleResi').AsString);
        if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleCelu').AsString <> '') then
          QRLTelefones.Caption := QRLTelefones.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString);
      end
      else
      begin
        if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleResi').AsString <> '') then
          QRLTelefones.Caption := QRLTelefones.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString)
        else
          QRLTelefones.Caption := QRLTelefones.Caption + ' -';
      end;   }

      if not QryRelatorio.FieldByName('DT_NASC_USU_BIB').IsNull then
        qrlDtNasc.Caption := FormatDateTime('dd/MM/yyyy', QryRelatorio.FieldByName('DT_NASC_USU_BIB').AsDateTime)
      else
        qrlDtNasc.Caption := '-';

      if not QryRelatorio.FieldByName('DT_CADA_USU_BIB').IsNull then
        QRLDtCadastro.Caption := FormatDateTime('dd/MM/yyyy', QryRelatorio.FieldByName('DT_CADA_USU_BIB').AsDateTime)
      else
        QRLDtCadastro.Caption := '-';

      if QryRelatorio.FieldByName('CO_SITU_USU_BIB').AsString = 'A' then
        QRLStatus.Caption := 'Ativo'
      else
        QRLStatus.Caption := 'Inativo';

      if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'A' then
        QRLTpUsuario.Caption := 'Aluno'
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'P' then
        QRLTpUsuario.Caption := 'Professor'
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'F' then
        QRLTpUsuario.Caption := 'Funcionário'
      else if QryRelatorio.FieldByName('TP_USU_BIB').AsString = 'O' then
        QRLTpUsuario.Caption := 'Outros';

      if (not FieldByName('teleCelu').IsNull) and (FieldByName('teleCelu').AsString <> '') then
        QRLTelefones.Caption := FormatMaskText('(00) 0000-0000;0',FieldByName('teleCelu').AsString)
      else
        QRLTelefones.Caption := '-';

    end;
  end;

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelRelacUsuBiblioteca.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

end.
