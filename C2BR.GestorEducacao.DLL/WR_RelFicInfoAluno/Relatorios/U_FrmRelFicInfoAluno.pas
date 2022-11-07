unit U_FrmRelFicInfoAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

const
  OffsetMemoryStream : Int64 = 0;

type
  TFrmRelFicInfoAluno = class(TFrmRelTemplate)
    QrlPage: TQRLabel;
    QRLabel1: TQRLabel;
    DetailBand1: TQRBand;
    QryMatricula: TADOQuery;
    GroupFooterBand1: TQRBand;
    QRSubDetailGestores: TQRSubDetail;
    QRLTpEndereco: TQRLabel;
    QRLEndereco: TQRLabel;
    GroupHeaderBand2: TQRBand;
    QRShape19: TQRShape;
    QRLabel39: TQRLabel;
    QRShape20: TQRShape;
    QRLabel40: TQRLabel;
    QRLabel41: TQRLabel;
    QRLTelefones: TQRLabel;
    QRLabel22: TQRLabel;
    QRShape9: TQRShape;
    QRLabel23: TQRLabel;
    QRLResp: TQRLabel;
    QRLabel21: TQRLabel;
    QRLSexResp: TQRLabel;
    QrlIdadeResp: TQRLabel;
    QRLabel24: TQRLabel;
    QrlGrauParen: TQRLabel;
    QRLabel25: TQRLabel;
    QRDBText22: TQRDBText;
    QrlRespEndereco: TQRLabel;
    QrlRespTelefone: TQRLabel;
    QRLTelsResp: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape12: TQRShape;
    QrlDocLinha1: TQRLabel;
    QRLCertidao: TQRLabel;
    QrlDocLinha2: TQRLabel;
    QRLCPFAlu: TQRLabel;
    QRLCartIdent: TQRLabel;
    QRLabel49: TQRLabel;
    QrlTituloEleitor: TQRLabel;
    GroupHeaderBand1: TQRBand;
    QRSubDetailProgSoc: TQRSubDetail;
    GroupFooterBand2: TQRBand;
    QRLabel34: TQRLabel;
    QRShape15: TQRShape;
    QRLabel35: TQRLabel;
    QRLabel36: TQRLabel;
    QRShape16: TQRShape;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRLTNB2: TQRLabel;
    QRLabel48: TQRLabel;
    qryEnderecos: TADOQuery;
    qryMedias: TADOQuery;
    QRLabel10: TQRLabel;
    QRLSituEnd: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel4: TQRLabel;
    QRBand1: TQRBand;
    QRShape6: TQRShape;
    QRShape8: TQRShape;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel42: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel45: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel50: TQRLabel;
    QRSubDetailMatric: TQRSubDetail;
    QRBand2: TQRBand;
    QRLDocsEntreg: TQRLabel;
    QRLabel20: TQRLabel;
    QRShape17: TQRShape;
    QRDBText30: TQRDBText;
    QRDBText31: TQRDBText;
    QrlMatricula: TQRLabel;
    QRLTpMat: TQRLabel;
    QRDBText32: TQRDBText;
    QrlSerieTurma: TQRLabel;
    QrlBIM1: TQRLabel;
    QrlBIM2: TQRLabel;
    QrlBIM3: TQRLabel;
    QrlBIM4: TQRLabel;
    QrlMD: TQRLabel;
    QrlPF: TQRLabel;
    QrlMF: TQRLabel;
    QrlQF: TQRLabel;
    QRLObs: TQRLabel;
    qryProgSociais: TADOQuery;
    QRLabel29: TQRLabel;
    QRShape18: TQRShape;
    QRLabel51: TQRLabel;
    QrlPasseEscolar: TQRLabel;
    QRLabel52: TQRLabel;
    QRLInstEsp: TQRLabel;
    QRLabel54: TQRLabel;
    QrlEscolaAnterior: TQRLabel;
    QRShape13: TQRShape;
    QRLabel32: TQRLabel;
    QrlOBSAluno: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRDBText8: TQRDBText;
    QRLValorPS: TQRLabel;
    QRLStatusPS: TQRLabel;
    QRLSiglaBolsa: TQRLabel;
    QRLDescBolsa: TQRLabel;
    QRLDtCadBolsa: TQRLabel;
    QRLDtIniBolsa: TQRLabel;
    QRLDtTerBolsa: TQRLabel;
    QRLVlBolsa: TQRLabel;
    QRLStaBolsa: TQRLabel;
    QRLabel8: TQRLabel;
    QRLTpProgBol: TQRLabel;
    QRLTpProg: TQRLabel;
    QRBand3: TQRBand;
    QRSubDetail1: TQRSubDetail;
    QRDBText2: TQRDBText;
    QRDBText9: TQRDBText;
    QRLMB1D: TQRLabel;
    QRLMB2D: TQRLabel;
    QRLMB3D: TQRLabel;
    QRLMB4D: TQRLabel;
    QRLMEDD: TQRLabel;
    QRLFALTD: TQRLabel;
    qryDesempAtual: TADOQuery;
    QRGroup1: TQRGroup;
    QRLTitDesemAtual: TQRLabel;
    QRShape5: TQRShape;
    QRLabel31: TQRLabel;
    QRShape14: TQRShape;
    QRLabel53: TQRLabel;
    QRLabel55: TQRLabel;
    QRLabel56: TQRLabel;
    QRLabel57: TQRLabel;
    QRLabel58: TQRLabel;
    QRLabel59: TQRLabel;
    QRLabel60: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRDBIAluno: TQRDBImage;
    QRIAluno: TQRImage;
    QRLDtCadastro: TQRLabel;
    QRLabel11: TQRLabel;
    QRLFiliacao: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape4: TQRShape;
    QRLabel6: TQRLabel;
    QRDBText7: TQRDBText;
    QrlDeficiencia: TQRLabel;
    QrlEstadoCivil: TQRLabel;
    QrlRendaFamiliar: TQRLabel;
    QRLabel30: TQRLabel;
    QrlNaturalidade: TQRLabel;
    QRDBText6: TQRDBText;
    QRLabel5: TQRLabel;
    QRShape2: TQRShape;
    QRLabel3: TQRLabel;
    QRLNoAlu: TQRLabel;
    QRLabel12: TQRLabel;
    QRLSxAluno: TQRLabel;
    QrlIdade: TQRLabel;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRLParamAluno: TQRLabel;
    QRLabel61: TQRLabel;
    QRLPercFR: TQRLabel;
    QRLPercFalta: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupHeaderBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupFooterBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailMatricBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure GroupHeaderBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetailProgSocBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupFooterBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand4AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetailMatricAfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    ultAno : String;
    media : Double;
    contador : Integer;
  public
    { Public declarations }
  end;

var
  FrmRelFicInfoAluno: TFrmRelFicInfoAluno;

implementation

uses U_DataModuleSGE, MaskUtils, DateUtils, Math, JPEG;

{$R *.dfm}

procedure TFrmRelFicInfoAluno.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano : real;
  //nu_nis : String;
  //tamNis : Integer;
  Jpg : TJPEGImage;
  Bmp: TBitmap;
  MemoryStream : TMemoryStream;
begin
  inherited;
  
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorio.FieldByName('NO_PAI_ALU').IsNull then
    QRLFiliacao.Caption := 'Pai: ' + QryRelatorio.FieldByName('NO_PAI_ALU').AsString
  else
    QRLFiliacao.Caption := 'Pai: ***';

  if not QryRelatorio.FieldByName('NO_MAE_ALU').IsNull then
    QRLFiliacao.Caption := QRLFiliacao.Caption + ' - Mãe: ' + QryRelatorio.FieldByName('NO_MAE_ALU').AsString
  else
    QRLFiliacao.Caption := QRLFiliacao.Caption + ' - Mãe: ***';

  if not QryRelatorio.FieldByName('ImageStream').IsNull then
  begin
    Try
      try
        MemoryStream := TMemoryStream.Create;
        (QryRelatorio.FieldByName('ImageStream') as TBlobField).SaveToStream(MemoryStream);
        MemoryStream.Position := OffsetMemoryStream;
        Jpg := TJpegImage.Create;
        Jpg.LoadFromStream(MemoryStream);
        QRIAluno.Picture.Assign(Jpg);
        QRIAluno.Enabled := True;
      except
        QRIAluno.Enabled := False;
        QRDBIAluno.Enabled := True;
        QRDBIAluno.Top := 2;
      end;
      finally
          MemoryStream.Free;
          Jpg.Free;
          Bmp.Free;
      end;
  end;

  if QryRelatorio.FieldByName('CO_SEXO_ALU').AsString = 'M' then
    QRLSxAluno.Caption := 'Masculino'
  else
    QRLSxAluno.Caption := 'Feminino';

  QRLDtCadastro.Caption := FormatDateTime('dd/MM/yy',QryRelatorio.FieldByName('DT_CADA_ALU').AsDateTime);
  QrlResp.Caption := QryRelatorio.FieldByName('NO_RESP').AsString + ' (CPF: ' + FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString) +' )';

  if (not QryRelatorio.FieldByName('NU_CPF_ALU').IsNull) and (QryRelatorio.FieldByName('NU_CPF_ALU').AsString <> '') then
    QRLCPFAlu.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_ALU').AsString)
  else
    QRLCPFAlu.Caption := '-';

  if QryRelatorio.FieldByName('CO_SEXO_RESP').AsString = 'M' then
    QRLSexResp.Caption := 'Masculino'
  else
    QRLSexResp.Caption := 'Feminino';

  if not QryRelatorio.FieldByName('NU_TELE_RESI_RESP').IsNull then
  begin
    QRLTelsResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_RESI_RESP').AsString) + ' C / ';

    if not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull then
      QRLTelsResp.Caption := QRLTelsResp.Caption + FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString) + ' R';
  end
  else
  begin
    if not QryRelatorio.FieldByName('NU_TELE_CELU_RESP').IsNull then
      QRLTelsResp.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_RESP').AsString) + ' R'
    else
      QRLTelsResp.Caption := '-';
  end;

  if QryRelatorio.FieldByName('CO_SEXO_RESP').IsNull then
    QRLSexResp.Caption := '';
    
  if QryRelatorio.FieldByName('NO_INST_ESP').IsNull then
    QRLInstEsp.Caption := 'Nenhuma'
  else
    QRLInstEsp.Caption := QryRelatorio.FieldByName('NO_INST_ESP').AsString;

  // Data Nascimento + Idade;
  //QRLabel1.Left := QRLPage.Left - 4;
  //QRSysData1.Left := QRLPage.Left - 28;
  diasnoano := 365.6;
  QrlIdade.Caption := 'Nascimento: ' + QryRelatorio.FieldByName('DT_NASC_ALU').AsString + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano)) + ')';

  If QryRelatorio.FieldByName('TP_CERTIDAO').AsString = 'C' then
    QRLCertidao.Caption := 'Casamento';
  If QryRelatorio.FieldByName('TP_CERTIDAO').AsString = 'N' then
    QRLCertidao.Caption := 'Nascimento';
  if QryRelatorio.FieldByName('TP_CERTIDAO').IsNull then
    QRLCertidao.Caption := '-';

  If not QryRelatorio.FieldByName('NU_CERT').IsNull then
    QRLCertidao.Caption := QRLCertidao.Caption + ' ( Nº: ' + QryRelatorio.FieldByName('NU_CERT').AsString
  else
    QRLCertidao.Caption := QRLCertidao.Caption + ' ( Nº: **';

  If not QryRelatorio.FieldByName('DE_CERT_LIVRO').IsNull then
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Livro: ' + QryRelatorio.FieldByName('DE_CERT_LIVRO').AsString
  else
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Livro: ****';

  If not QryRelatorio.FieldByName('NU_CERT_FOLHA').IsNull then
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Folha Nº: ' + QryRelatorio.FieldByName('NU_CERT_FOLHA').AsString
  else
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Folha Nº: ****';

  If not QryRelatorio.FieldByName('DE_CERT_CARTORIO').IsNull then
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Cartório: ' + QryRelatorio.FieldByName('DE_CERT_CARTORIO').AsString + ' )'
  else
    QRLCertidao.Caption := QRLCertidao.Caption + ' - Cartório: **** )';

  // Estado Civil
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'O' then
    QrlEstadoCivil.Caption := 'Estado Civil: Solteiro(a)';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'C' then
    QrlEstadoCivil.Caption := 'Estado Civil: Casado(a)';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'S' then
    QrlEstadoCivil.Caption := 'Estado Civil: Separado Judicialmente';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'D' then
    QrlEstadoCivil.Caption := 'Estado Civil: Divorciado(a)';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'V' then
    QrlEstadoCivil.Caption := 'Estado Civil: Viúvo(a)';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'P' then
    QrlEstadoCivil.Caption := 'Estado Civil: Companheiro(a)';
  if QryRelatorio.FieldByName('CO_ESTADO_CIVIL').AsString = 'U' then
    QrlEstadoCivil.Caption := 'Estado Civil: União Estável';

  // Naturalidade (Cidade/Estado)
  QrlNaturalidade.Caption := 'Naturalidade (Cidade/UF): ' + QryRelatorio.FieldByName('DE_NATU_ALU').AsString + ' / ' + QryRelatorio.FieldByName('CO_UF_NATU_ALU').AsString;

  // Deficiência
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'N' then
    QrlDeficiencia.Caption := 'Deficiência: Nenhuma';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'A' then
    QrlDeficiencia.Caption := 'Deficiência: Auditivo';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'V' then
    QrlDeficiencia.Caption := 'Deficiência: Visual';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'F' then
    QrlDeficiencia.Caption := 'Deficiência: Físico';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'M' then
    QrlDeficiencia.Caption := 'Deficiência: Mental';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'I' then
    QrlDeficiencia.Caption := 'Deficiência: Múltiplas';
  if QryRelatorio.FieldByName('TP_DEF').AsString = 'O' then
    QrlDeficiencia.Caption := 'Deficiência: Outros';

  if not QryRelatorio.FieldByName('CO_RG_ALU').IsNull then
    QRLCartIdent.Caption := 'Nº: ' + QryRelatorio.FieldByName('CO_RG_ALU').AsString
  else
    QRLCartIdent.Caption := 'Nº: **';

  if not QryRelatorio.FieldByName('CO_ORG_RG_ALU').IsNull then
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - Orgão: ' + QryRelatorio.FieldByName('CO_ORG_RG_ALU').AsString
  else
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - Orgão: ****';

  if not QryRelatorio.FieldByName('CO_ESTA_RG_ALU').IsNull then
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - UF: ' + QryRelatorio.FieldByName('CO_ESTA_RG_ALU').AsString
  else
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - UF: **';

  if not QryRelatorio.FieldByName('DT_EMIS_RG_ALU').IsNull then
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - Emissão: ' + QryRelatorio.FieldByName('DT_EMIS_RG_ALU').AsString
  else
    QRLCartIdent.Caption := QRLCartIdent.Caption + ' - Emissão: ****';

  if not QryRelatorio.FieldByName('NU_TIT_ELE').IsNull then
    QrlTituloEleitor.Caption := 'Nº: ' + QryRelatorio.FieldByName('NU_TIT_ELE').AsString
  else
    QrlTituloEleitor.Caption := 'Nº: **';

  if not QryRelatorio.FieldByName('NU_ZONA_ELE').IsNull then
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - Zona: ' + QryRelatorio.FieldByName('NU_ZONA_ELE').AsString
  else
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - Zona: ****';

  if not QryRelatorio.FieldByName('NU_SEC_ELE').IsNull then
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - Seção: ' + QryRelatorio.FieldByName('NU_SEC_ELE').AsString
  else
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - Seção: ****';

  if not QryRelatorio.FieldByName('CO_UF_TIT_ELE').IsNull then
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - UF: ' + QryRelatorio.FieldByName('CO_UF_TIT_ELE').AsString
  else
    QrlTituloEleitor.Caption := QrlTituloEleitor.Caption + ' - UF: **';

  //Idade Responsável
  QrlIdadeResp.Caption := 'Nascimento: ' + QryRelatorio.FieldByName('DT_NASC_RESP').AsString + ' (' + IntToStr(Trunc(DaysBetween(now,QryRelatorio.fieldbyname('DT_NASC_RESP').AsDateTime) / diasnoano)) + ')';

  //Grau de Parentesco
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PM' then
    QrlGrauParen.Caption := 'Pai/Mãe';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TI' then
    QrlGrauParen.Caption := 'Tio(a)';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'AV' then
    QrlGrauParen.Caption := 'Avô/Avó';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PR' then
    QrlGrauParen.Caption := 'Primo(a)';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'CN' then
    QrlGrauParen.Caption := 'Cunhado(a)';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TU' then
    QrlGrauParen.Caption := 'Tutor(a)';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'IR' then
    QrlGrauParen.Caption := 'Irmão(ã)';
  if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'OU' then
    QrlGrauParen.Caption := 'Outros';

  // Endereço Responsável
  QrlRespEndereco.Caption := QryRelatorio.FieldByName('DE_ENDE_RESP').AsString + ' - ' + QryRelatorio.FieldByName('NU_ENDE_RESP').AsString + ' - ' + QryRelatorio.FieldByName('DE_COMP_RESP').AsString;

  // Bairro Responsavel
  QrlRespEndereco.Caption := QrlRespEndereco.Caption + ' - ' + QryRelatorio.FieldByName('BAIRRORESP').AsString;

  //Cidade / Estado Responsavel
  QrlRespEndereco.Caption := QrlRespEndereco.Caption + ' - ' + QryRelatorio.FieldByName('CIDADERESP').AsString + ' - ' + QryRelatorio.FieldByName('CO_ESTA_RESP').AsString;

  // CEP Responsavel
  if not QryRelatorio.FieldByName('CO_CEP_RESP').IsNull then
    QrlRespEndereco.Caption := QrlRespEndereco.Caption + ' - CEP ' + FormatMaskText('00000-000;0',QryRelatorio.FieldByName('CO_CEP_RESP').AsString)
  else
    QrlRespEndereco.Caption := QrlRespEndereco.Caption + ' - CEP ******';

  // Passe Escolar
  if QryRelatorio.FieldByName('FLA_PASSE_ESCOLA').AsString = '0' then
  begin
    QrlPasseEscolar.Caption := 'NÃO';
  end
  else
  begin
    QrlPasseEscolar.Caption := 'SIM';
  end;

  // Renda Familiar
  if QryRelatorio.FieldByName('RENDA_FAMILIAR').AsString = '1' then
    QrlRendaFamiliar.Caption := '1 a 3 SM';
  if QryRelatorio.FieldByName('RENDA_FAMILIAR').AsString = '2' then
    QrlRendaFamiliar.Caption := '3 a 5 SM';
  if QryRelatorio.FieldByName('RENDA_FAMILIAR').AsString = '3' then
    QrlRendaFamiliar.Caption := '+5 SM';
  if QryRelatorio.FieldByName('RENDA_FAMILIAR').AsString = '4' then
    QrlRendaFamiliar.Caption := 'Sem Renda';

  //Escola Anterior
  QrlEscolaAnterior.Caption := QryRelatorio.FieldByName('DE_ESCOLA_ANTERIOR').AsString + ' / ' + QryRelatorio.FieldByName('DE_CIDADE_ESC_ANT').AsString + ' / ' + QryRelatorio.FieldByName('CO_UF_ESC_ANT').AsString + ' / ' + QryRelatorio.FieldByName('CO_ULT_ANO_ESC_ANT').AsString;

  //Observação sobre o Aluno
  QrlOBSAluno.Caption := QryRelatorio.FieldByName('DES_OBS_ALU').AsString;

end;

procedure TFrmRelFicInfoAluno.GroupHeaderBand2AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryEnderecos do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select AE.DS_ENDERECO, AE.NR_ENDERECO, AE.DS_COMPLEMENTO, AE.CO_CEP, BA.NO_BAIRRO, CI.NO_CIDADE, TE.NM_TIPO_ENDERECO, CI.CO_UF, AE.CO_SITUACAO ' +
                'from TB241_ALUNO_ENDERECO ae ' +
                'join TB07_ALUNO a on a.co_alu = ae.co_alu and a.co_emp = ae.co_emp ' +
                'join TB904_CIDADE CI on CI.CO_CIDADE = AE.CO_CIDADE ' +
                'join TB905_BAIRRO BA on BA.CO_BAIRRO = AE.CO_BAIRRO AND BA.CO_BAIRRO = AE.CO_BAIRRO ' +
                'join TB238_TIPO_ENDERECO TE on TE.ID_TIPO_ENDERECO = AE.ID_TIPO_ENDERECO ' +
                'where ae.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and ae.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and ae.CO_SITUACAO = ' + QuotedStr('A') +
                ' order by ae.DS_ENDERECO';
    Open;
  end;
end;

procedure TFrmRelFicInfoAluno.QRSubDetailGestoresBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not qryEnderecos.FieldByName('NM_TIPO_ENDERECO').IsNull then
    QRLTpEndereco.Caption := qryEnderecos.FieldByName('NM_TIPO_ENDERECO').AsString
  else
    QRLTpEndereco.Caption := '';

  QRLEndereco.Caption := '';

  if not qryEnderecos.FieldByName('no_bairro').IsNull then
  begin
    if not qryEnderecos.FieldByName('DS_ENDERECO').IsNull then
      QRLEndereco.Caption := qryEnderecos.FieldByName('DS_ENDERECO').AsString;
    if not qryEnderecos.FieldByName('NR_ENDERECO').IsNull then
      QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + qryEnderecos.FieldByName('NR_ENDERECO').AsString;
    if not qryEnderecos.FieldByName('DS_COMPLEMENTO').IsNull then
      QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + qryEnderecos.FieldByName('DS_COMPLEMENTO').AsString;

    QRLEndereco.Caption := QRLEndereco.Caption + ' - ' + qryEnderecos.FieldByName('no_bairro').AsString + ' - ' + qryEnderecos.FieldByName('no_cidade').AsString + ' - ' + qryEnderecos.FieldByName('CO_UF').AsString;
    QRLEndereco.Caption := QRLEndereco.Caption + ' - CEP ' + FormatMaskText('00000-000;0',qryEnderecos.FieldByName('CO_CEP').AsString);
  end
  else
    QRLEndereco.Caption := '';

  if qryEnderecos.FieldByName('CO_SITUACAO').AsString = 'A' then
    QRLSituEnd.Caption := 'Ativa'
  else
    QRLSituEnd.Caption := 'Inativa';
end;

procedure TFrmRelFicInfoAluno.GroupFooterBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  controle : integer;
  serie, modulo : String;
begin
  inherited;
  controle := 0;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select at.NR_DDD, at.NR_TELEFONE, tt.NM_TIPO_TELEFONE ' +
                'from TB242_ALUNO_TELEFONE at ' +
                'join TB239_TIPO_TELEFONE tt on tt.ID_TIPO_TELEFONE = at.ID_TIPO_TELEFONE ' +
                ' where at.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and at.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and at.CO_SITUACAO = ' + QuotedStr('A');
    Open;

    if IsEmpty then
      QRLTelefones.Caption := '(*****'
    else
      QRLTelefones.Caption := '';

    while not Eof do
    begin

      if controle = 0 then
        QRLTelefones.Caption := QRLTelefones.Caption + '(' + FormatFloat('00',FieldByName('NR_DDD').AsFloat) + ') ' + FormatMaskText('0000-0000;0',FieldByName('NR_TELEFONE').AsString) + ' (' + FieldByName('NM_TIPO_TELEFONE').AsString + ')'
      else
        QRLTelefones.Caption := QRLTelefones.Caption + ' / (' + FormatFloat('00',FieldByName('NR_DDD').AsFloat) + ') ' + FormatMaskText('0000-0000;0',FieldByName('NR_TELEFONE').AsString) + ' (' + FieldByName('NM_TIPO_TELEFONE').AsString + ')';

      controle := 1;
      Next;
    end;

    QRLTelefones.Caption := QRLTelefones.Caption + ')';
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select top 1 mm.co_cur, mm.co_modu_cur, mm.co_ano_mes_mat ' +
                'from TB08_matrcur mm ' +
                ' where mm.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and mm.FLA_REMATRICULADO = ' + QuotedStr('N') +
                ' order by mm.co_ano_mes_mat desc';
    Open;

    if not IsEmpty then
    begin
      serie := FieldByName('CO_CUR').AsString;
      modulo := FieldByName('CO_MODU_CUR').AsString;
      SQL.Clear;
      SQL.Text := 'select at.CO_TP_DOC_MAT, tt.DE_TP_DOC_MAT ' +
                  'from TB208_CURSO_DOCTOS at ' +
                  'join TB121_TIPO_DOC_MATRICULA tt on tt.CO_TP_DOC_MAT = at.CO_TP_DOC_MAT ' +
                  ' where at.co_cur = ' + serie +
                  ' and at.co_modu_cur = ' + modulo +
                  ' order by tt.DE_TP_DOC_MAT';
      Open;

      if IsEmpty then
        QRLDocsEntreg.Caption := '( ***** )'
      else
      begin
        QRLDocsEntreg.Caption := '';
        controle := 0;

        while not Eof do
        begin
          with DM.QrySql2 do
          begin
            SQL.Clear;
            SQL.Text := 'select tt.DE_TP_DOC_MAT ' +
                        'from TB120_DOC_ALUNO_ENT at ' +
                        'join TB121_TIPO_DOC_MATRICULA tt on tt.CO_TP_DOC_MAT = at.CO_TP_DOC_MAT ' +
                        ' where at.co_alu = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                        ' and at.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                        ' and at.CO_TP_DOC_MAT = ' + DM.QrySql.FieldByName('CO_TP_DOC_MAT').AsString;
            Open;

            if controle = 0 then
            begin
              if not IsEmpty then
                QRLDocsEntreg.Caption := DM.QrySql.FieldByName('DE_TP_DOC_MAT').AsString
              else
                QRLDocsEntreg.Caption := DM.QrySql.FieldByName('DE_TP_DOC_MAT').AsString + ' (Falta)';
            end
            else
            begin
              if not IsEmpty then
                QRLDocsEntreg.Caption := QRLDocsEntreg.Caption + ' - ' + DM.QrySql.FieldByName('DE_TP_DOC_MAT').AsString
              else
                QRLDocsEntreg.Caption := QRLDocsEntreg.Caption + ' - ' + DM.QrySql.FieldByName('DE_TP_DOC_MAT').AsString + ' (Falta)';
            end;
          end;

          controle := 1;
          Next;
        end;
      end;
    end
    else
    begin
      QRLDocsEntreg.Caption := '( ***** )';
    end;
  end;
end;

procedure TFrmRelFicInfoAluno.QRSubDetailMatricBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryMatricula.FieldByName('CO_ALU_CAD').IsNull then
  begin
    with qryMedias do
    begin
      Close;
      Sql.Clear;
      Sql.Text := ' SELECT TOP 1 M.CO_ANO_MES_MAT,'+
                  '	(SELECT SUM(VL_NOTA_BIM1)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) BIM1, '+
                  '	(SELECT SUM(VL_NOTA_BIM2)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) BIM2, '+
                  '	(SELECT SUM(VL_NOTA_BIM3)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) BIM3, '+
                  '	(SELECT SUM(VL_NOTA_BIM4)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) BIM4, '+
                  '	(SELECT SUM(VL_PROVA_FINAL)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) PF, '+
                  '	(SELECT SUM(VL_MEDIA_FINAL)/COUNT(CO_ALU) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) MF, '+
                  '	(SELECT SUM(QT_FALTA_BIM1) + SUM(QT_FALTA_BIM2) + SUM(QT_FALTA_BIM3) + SUM(QT_FALTA_BIM4) FROM TB079_HIST_ALUNO '+
                  '	WHERE CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND CO_ANO_REF = M.CO_ANO_MES_MAT ' +
                  '	) QF '+
                  ' FROM TB07_ALUNO A '+
                  '	JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU AND M.CO_EMP = A.CO_EMP '+
                  '	JOIN TB079_HIST_ALUNO HIST ON HIST.CO_ALU = A.CO_ALU '+
                  ' WHERE A.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
                  ' AND M.CO_ANO_MES_MAT = ' + QryMatricula.FieldByName('CO_ANO_MES_MAT').AsString +
                  ' AND M.CO_SIT_MAT = ' + quotedStr('F') +
                  ' AND M.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
      Open;

      if not IsEmpty then
      begin
      // NOTAS DOS BIMESTRES
      //BIM1
      if not qryMedias.FieldByName('BIM1').IsNull then
      begin
        QrlBIM1.Caption := qryMedias.FieldByName('BIM1').AsString;
      end
      else
      begin
        QrlBIM1.Caption := '-';
      end;

      //BIM2
      if not qryMedias.FieldByName('BIM2').IsNull then
      begin
        QrlBIM2.Caption := qryMedias.FieldByName('BIM2').AsString;
      end
      else
      begin
        QrlBIM2.Caption := '-';
      end;

      //BIM3
      if not qryMedias.FieldByName('BIM3').IsNull then
      begin
        QrlBIM3.Caption := qryMedias.FieldByName('BIM3').AsString;
      end
      else
      begin
        QrlBIM3.Caption := '-';
      end;

      //BIM4
      if not qryMedias.FieldByName('BIM4').IsNull then
      begin
        QrlBIM4.Caption := qryMedias.FieldByName('BIM4').AsString;
      end
      else
      begin
        QrlBIM4.Caption := '-';
      end;

      //PROVA FINAL
      if not qryMedias.FieldByName('PF').IsNull then
      begin
        QrlPF.Caption := qryMedias.FieldByName('PF').AsString;
      end
      else
      begin
        QrlPF.Caption := '-';
      end;

      //MÉDIA FINAL
      if not qryMedias.FieldByName('MF').IsNull then
      begin
        QrlMF.Caption := FloatToStrF(qryMedias.FieldByName('MF').AsFloat,ffNumber,10,2);
      end
      else
      begin
        QrlMF.Caption := '-';
      end;

      //QUANTIDADE DE FALTAS
      if not qryMedias.FieldByName('QF').IsNull then
      begin
        QrlQF.Caption := qryMedias.FieldByName('QF').AsString;
        if not QryMatricula.FieldByName('QT_AULA_CUR').IsNull then
          QRLPercFalta.Caption := FloatToStrF((qryMedias.FieldByName('QF').AsInteger * 100)/QryMatricula.FieldByName('QT_AULA_CUR').AsFloat,ffNumber, 10, 1)
        else
          QRLPercFalta.Caption := '-';
      end
      else
      begin
        QrlQF.Caption := '-';
        QRLPercFalta.Caption := '-';
      end;

      //MD
      QrlMD.Caption := FloatToStrF((qryMedias.FieldByName('BIM1').AsFloat + qryMedias.FieldByName('BIM2').AsFloat +
                       qryMedias.FieldByName('BIM3').AsFloat + qryMedias.FieldByName('BIM4').AsFloat)/4,ffNumber,10,2);

      end
      else
      begin
        QrlBIM1.Caption := '-';
        QrlBIM2.Caption := '-';
        QrlBIM3.Caption := '-';
        QrlBIM4.Caption := '-';
        QrlMD.Caption := '-';
        QrlPF.Caption := '-';
        QrlMF.Caption := '-';
        QrlQF.Caption := '-';
      end;
    end;


    QrlMatricula.Caption := FormatMaskText('00.000.0000.###;0', QryMatricula.FieldByName('CO_ALU_CAD').AsString);
    QrlSerieTurma.Caption := QryMatricula.FieldByName('CO_SIGL_CUR').AsString + ' / ' + QryMatricula.FieldByName('CO_SIGLA_TURMA').AsString;

    if not QryMatricula.FieldByName('CO_STA_APROV').IsNull then
    begin
      if QryMatricula.FieldByName('CO_STA_APROV').AsString = 'A' then
      begin
        if not QryMatricula.FieldByName('CO_STA_APROV_FREQ').IsNull then
          if QryMatricula.FieldByName('CO_STA_APROV_FREQ').AsString = 'A' then
            QRLObs.Caption := 'Aprovado'
          else
            QRLObs.Caption := 'Reprovado';
      end
      else
        QrlOBS.Caption := 'Reprovado';
    end
    else
    begin
      QRLObs.Caption := '-';
    end;

    if QryMatricula.FieldByName('FLA_REMATRICULADO').AsString = 'S' then
      QRLTpMat.Caption := 'Renovação'
    else
      QRLTpMat.Caption := 'Nova';
  end
  else
  begin
    QrlMatricula.Caption := '-';
    QrlSerieTurma.Caption := '-';
    QrlBIM1.Caption := '-';
    QrlBIM2.Caption := '-';
    QrlBIM3.Caption := '-';
    QrlBIM4.Caption := '-';
    QrlMD.Caption := '-';
    QrlPF.Caption := '-';
    QrlMF.Caption := '-';
    QrlQF.Caption := '-';
    QRLObs.Caption := '-';
    QRLTpMat.Caption := '-';
    QRLPercFalta.Caption := '-';
  end;

  if QRSubDetailMatric.Color = clWhite then
    QRSubDetailMatric.Color := $00F2F2F2
  else
    QRSubDetailMatric.Color := clWhite;
end;

procedure TFrmRelFicInfoAluno.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with QryMatricula do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' SELECT MM.CO_ANO_MES_MAT,MM.CO_ALU_CAD,MM.CO_STA_APROV,MM.CO_STA_APROV_FREQ,cu.CO_SIGL_CUR, ct.CO_SIGLA_TURMA,MM.DT_EFE_MAT, MM.FLA_REMATRICULADO, cu.QT_AULA_CUR '+
               ' FROM TB08_MATRCUR MM ' +
               ' LEFT JOIN TB01_CURSO cu on cu.co_cur = mm.co_cur and mm.co_emp = cu.co_emp and mm.co_modu_cur = cu.co_modu_cur ' +
               ' LEFT JOIN TB06_turmas tu on tu.co_cur = mm.co_cur and mm.co_emp = tu.co_emp and mm.co_tur = tu.co_tur ' +
               ' LEFT JOIN TB129_cadturmas ct on tu.co_tur = ct.co_tur ' +
               ' WHERE mm.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
               ' and mm.co_sit_mat not in (' + QuotedStr('C') + ')' +
               ' order by mm.co_ano_mes_mat';
    Open;
  end;
end;

procedure TFrmRelFicInfoAluno.GroupHeaderBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryProgSociais do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' SELECT ps.NO_PROGR_SOCIA, ps.CO_SIGLA_PROGR_SOCIA, aps.DT_CADAS_ALU_PRGSOC, aps.DT_INICI_ALU_PRGSOC, aps.DT_TERMI_ALU_PRGSOC, aps.CO_STATUS_ALU_PRGSOC '+
               ' FROM TB136_ALU_PROG_SOCIAIS aps ' +
               ' JOIN TB135_PROG_SOCIAIS ps on ps.CO_IDENT_PROGR_SOCIA = aps.CO_IDENT_PROGR_SOCIA and ps.ORG_CODIGO_ORGAO = aps.ORG_CODIGO_ORGAO ' +
               ' WHERE aps.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' and aps.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
               ' order by ps.NO_PROGR_SOCIA';
    Open;
  end;
end;

procedure TFrmRelFicInfoAluno.QRSubDetailProgSocBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not qryProgSociais.IsEmpty then
  begin
    QRLValorPS.Caption := '-';
    QRLTpProg.Caption := 'PRS';

    if qryProgSociais.FieldByName('CO_STATUS_ALU_PRGSOC').AsString = 'A' then
      QRLStatusPS.Caption := 'Ativo'
    else
      QRLStatusPS.Caption := 'Inativo';
  end
  else
  begin
    QRSubDetailProgSoc.Height := 0;
    QRLValorPS.Caption := '';
    QRLStatusPS.Caption := '';
  end;
end;

procedure TFrmRelFicInfoAluno.GroupFooterBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLSiglaBolsa.Caption := '-';
  if not QryRelatorio.FieldByName('CO_TIPO_BOLSA').IsNull then
  begin
    QRLDescBolsa.Caption := QryRelatorio.FieldByName('descBolsa').AsString;
    QRLTpProg.Caption := 'BOL';

    if not QryRelatorio.FieldByName('DT_CADA_ALU').IsNull then
      QRLDtCadBolsa.Caption := QryRelatorio.FieldByName('DT_CADA_ALU').AsString
    else
      QRLDtCadBolsa.Caption := '-';

    if not QryRelatorio.FieldByName('DT_VENC_BOLSA').IsNull then
      QRLDtIniBolsa.Caption := QryRelatorio.FieldByName('DT_VENC_BOLSA').AsString
    else
      QRLDtIniBolsa.Caption := '-';

    if not QryRelatorio.FieldByName('DT_VENC_BOLSAF').IsNull then
      QRLDtTerBolsa.Caption := QryRelatorio.FieldByName('DT_VENC_BOLSAF').AsString
    else
      QRLDtTerBolsa.Caption := '-';

    if not QryRelatorio.FieldByName('NU_PEC_DESBOL').IsNull then
      QRLVlBolsa.Caption := QryRelatorio.FieldByName('NU_PEC_DESBOL').AsString + '%'
    else
      QRLVlBolsa.Caption := '-';

    if QryRelatorio.FieldByName('FLA_ATIVA').AsString = 'A' then
      QRLStaBolsa.Caption := 'Ativa'
    else
      QRLStaBolsa.Caption := 'Inativa';
  end
  else
  begin
    QRLDescBolsa.Caption := '-';
    QRLDtCadBolsa.Caption := '-';
    QRLDtIniBolsa.Caption := '-';
    QRLDtTerBolsa.Caption := '-';
    QRLVlBolsa.Caption := '-';
    QRLStaBolsa.Caption := '-';
    QRLTpProg.Caption := '-';
  end;
end;

procedure TFrmRelFicInfoAluno.QRBand4AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
var
  turno : String;
begin
  inherited;
  with qryDesempAtual do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' SELECT MM.CO_ALU_CAD,cu.CO_SIGL_CUR,ct.CO_SIGLA_TURMA,MM.CO_TURN_MAT,MO.DE_MODU_CUR,'+
               'cm.NO_SIGLA_MATERIA, cm.NO_MATERIA,' +
               'ha.VL_NOTA_BIM1, ha.VL_NOTA_BIM2, ha.VL_NOTA_BIM3, ha.VL_NOTA_BIM4,' +
               'isnull(ha.QT_FALTA_BIM1,0) + isnull(ha.QT_FALTA_BIM2,0) + isnull(ha.QT_FALTA_BIM3,0) + isnull(ha.QT_FALTA_BIM4,0) as falta,' +
               '(isnull(ha.VL_NOTA_BIM1,0) + isnull(ha.VL_NOTA_BIM2,0) + isnull(ha.VL_NOTA_BIM3,0) + isnull(ha.VL_NOTA_BIM4,0))/4 as media' +
               ' FROM TB08_MATRCUR MM ' +
               ' JOIN TB079_HIST_ALUNO ha on ha.co_cur = mm.co_cur and mm.co_emp = ha.co_emp and mm.co_modu_cur = ha.co_modu_cur ' +
               ' and mm.co_alu = ha.co_alu and mm.co_ano_mes_mat = ha.co_ano_ref ' +
               ' JOIN TB02_materia ma on ma.co_cur = ha.co_cur and ma.co_emp = ha.co_emp and ma.co_modu_cur = ha.co_modu_cur and ma.co_mat = ha.co_mat ' +
               ' JOIN TB107_CADMATERIAS cm on ma.id_materia = cm.id_materia and ma.co_emp = cm.co_emp ' +
               ' JOIN TB01_CURSO cu on cu.co_cur = mm.co_cur and mm.co_emp = cu.co_emp and mm.co_modu_cur = cu.co_modu_cur ' +
               ' JOIN TB06_turmas tu on tu.co_cur = mm.co_cur and mm.co_emp = tu.co_emp and mm.co_tur = tu.co_tur ' +
               ' JOIN TB129_cadturmas ct on tu.co_tur = ct.co_tur ' +
               ' JOIN tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur ' +
               ' WHERE mm.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
               ' and mm.co_ano_mes_mat = ' + ultAno +
               ' order by cm.NO_MATERIA';
    Open;

    if not IsEmpty then
    begin
      if FieldByName('CO_TURN_MAT').AsString = 'M' then
        turno := 'Matutino'
      else if FieldByName('CO_TURN_MAT').AsString = 'N' then
        turno := 'Noturno'
      else
        turno := 'Vespertino';

      QRLTitDesemAtual.Caption := '(Matrícula: ' + FormatMaskText('00.000.000000;0',FieldByName('CO_ALU_CAD').AsString) +
      ' - Modalidade: ' + FieldByName('DE_MODU_CUR').AsString + ' - Série: ' + FieldByName('CO_SIGL_CUR').AsString +
      ' - Turma: ' + FieldByName('CO_SIGLA_TURMA').AsString + ' - Turno: ' + turno + ')';
    end;
  end;
end;

procedure TFrmRelFicInfoAluno.QRSubDetailMatricAfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  if not QryMatricula.IsEmpty then
  begin
    QryMatricula.Last;
    ultAno := QryMatricula.FieldByName('CO_ANO_MES_MAT').AsString;
  end
  else
    ultAno := '0';
end;

procedure TFrmRelFicInfoAluno.QRSubDetail1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not qryDesempAtual.IsEmpty then
  begin    
    if not qryDesempAtual.FieldByName('VL_NOTA_BIM1').IsNull then
      QRLMB1D.Caption := qryDesempAtual.FieldByName('VL_NOTA_BIM1').AsString
    else
      QRLMB1D.Caption := '-';

    if not qryDesempAtual.FieldByName('VL_NOTA_BIM2').IsNull then
      QRLMB2D.Caption := qryDesempAtual.FieldByName('VL_NOTA_BIM2').AsString
    else
      QRLMB2D.Caption := '-';

    if not qryDesempAtual.FieldByName('VL_NOTA_BIM3').IsNull then
      QRLMB3D.Caption := qryDesempAtual.FieldByName('VL_NOTA_BIM3').AsString
    else
      QRLMB3D.Caption := '-';

    if not qryDesempAtual.FieldByName('VL_NOTA_BIM4').IsNull then
      QRLMB4D.Caption := qryDesempAtual.FieldByName('VL_NOTA_BIM4').AsString
    else
      QRLMB4D.Caption := '-';

    if not qryDesempAtual.FieldByName('media').IsNull then
      QRLMEDD.Caption := qryDesempAtual.FieldByName('media').AsString
    else
      QRLMEDD.Caption := '-';

    if not qryDesempAtual.FieldByName('falta').IsNull then
      QRLFALTD.Caption := qryDesempAtual.FieldByName('falta').AsString
    else
      QRLFALTD.Caption := '-';
  end
  else
  begin
    QRLMB1D.Caption := '-';
    QRLMB2D.Caption := '-';
    QRLMB3D.Caption := '-';
    QRLMB4D.Caption := '-';
    QRLMEDD.Caption := '-';
  end;
end;

procedure TFrmRelFicInfoAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  media := 0;
  contador := 0;
end;

procedure TFrmRelFicInfoAluno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  turno : String;
begin
  inherited;
  QRLTitDesemAtual.Caption := '';
  QRLParamAluno.Caption := '-';
  
  with qryDesempAtual do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' SELECT MM.CO_ALU_CAD,cu.CO_SIGL_CUR,ct.CO_SIGLA_TURMA,MM.CO_TURN_MAT,MO.DE_MODU_CUR,'+
               'cm.NO_SIGLA_MATERIA, cm.NO_MATERIA,' +
               'ha.VL_NOTA_BIM1, ha.VL_NOTA_BIM2, ha.VL_NOTA_BIM3, ha.VL_NOTA_BIM4,' +
               'isnull(ha.QT_FALTA_BIM1,0) + isnull(ha.QT_FALTA_BIM2,0) + isnull(ha.QT_FALTA_BIM3,0) + isnull(ha.QT_FALTA_BIM4,0) as falta,' +
               '(isnull(ha.VL_NOTA_BIM1,0) + isnull(ha.VL_NOTA_BIM2,0) + isnull(ha.VL_NOTA_BIM3,0) + isnull(ha.VL_NOTA_BIM4,0))/4 as media' +
               ' FROM TB08_MATRCUR MM ' +
               ' JOIN TB079_HIST_ALUNO ha on ha.co_cur = mm.co_cur and mm.co_emp = ha.co_emp and mm.co_modu_cur = ha.co_modu_cur ' +
               ' and mm.co_alu = ha.co_alu and mm.co_ano_mes_mat = ha.co_ano_ref ' +
               ' JOIN TB02_materia ma on ma.co_cur = ha.co_cur and ma.co_emp = ha.co_emp and ma.co_modu_cur = ha.co_modu_cur and ma.co_mat = ha.co_mat ' +
               ' JOIN TB107_CADMATERIAS cm on ma.id_materia = cm.id_materia and ma.co_emp = cm.co_emp ' +
               ' JOIN TB01_CURSO cu on cu.co_cur = mm.co_cur and mm.co_emp = cu.co_emp and mm.co_modu_cur = cu.co_modu_cur ' +
               ' JOIN TB06_turmas tu on tu.co_cur = mm.co_cur and mm.co_emp = tu.co_emp and mm.co_tur = tu.co_tur ' +
               ' JOIN TB129_cadturmas ct on tu.co_tur = ct.co_tur ' +
               ' JOIN tb44_modulo mo on mo.co_modu_cur = mm.co_modu_cur ' +
               ' WHERE mm.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
               ' and mm.co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
               ' and mm.co_ano_mes_mat = ' + ultAno +
               ' order by cm.NO_MATERIA';
    Open;

    if not IsEmpty then
    begin
      if FieldByName('CO_TURN_MAT').AsString = 'M' then
        turno := 'Matutino'
      else if FieldByName('CO_TURN_MAT').AsString = 'N' then
        turno := 'Noturno'
      else
        turno := 'Vespertino';

      QRLTitDesemAtual.Caption := '(Matrícula: ' + FormatMaskText('00.000.000000;0',FieldByName('CO_ALU_CAD').AsString) +
      ' - Modalidade: ' + FieldByName('DE_MODU_CUR').AsString + ' - Série: ' + FieldByName('CO_SIGL_CUR').AsString +
      ' - Turma: ' + FieldByName('CO_SIGLA_TURMA').AsString + ' - Turno: ' + turno + ')';

      QRLParamAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString) +
      ' - Modalidade: ' + FieldByName('DE_MODU_CUR').AsString + ' - Série: ' + FieldByName('CO_SIGL_CUR').AsString +
      ' - Turma: ' + FieldByName('CO_SIGLA_TURMA').AsString + ' - Turno: ' + turno + ')';
    end
    else
    begin
      QRLParamAluno.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString);
    end;
  end;
end;

procedure TFrmRelFicInfoAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if contador = 0 then
  begin
    QRGroup1.Height := 0;
    contador := contador + 1;
  end
  else
  begin
    QRGroup1.Height := 40;
    contador := contador + 1;
  end;
end;

end.
