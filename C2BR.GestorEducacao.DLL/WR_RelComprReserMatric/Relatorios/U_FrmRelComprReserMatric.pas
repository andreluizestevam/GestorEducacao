unit U_FrmRelComprReserMatric;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, jpeg, QuickRpt, ExtCtrls, DateUtil;

type
  TFrmRelComprReserMatric = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRLabel30: TQRLabel;
    QRShape8: TQRShape;
    QRLCabAss: TQRLabel;
    QRShape3: TQRShape;
    QRLNumReserva: TQRLabel;
    QRLabel23: TQRLabel;
    QRLObsReserva: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLCandid: TQRLabel;
    QRLDtNascCandid: TQRLabel;
    QRLSxCandid: TQRLabel;
    QRLDefCand: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel40: TQRLabel;
    QRLMaeCandid: TQRLabel;
    QRLEndCandid1: TQRLabel;
    QRLPaiCandid: TQRLabel;
    QRLEndCandid2: TQRLabel;
    QRLEndCandid3: TQRLabel;
    QRShape2: TQRShape;
    QRLabel46: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel50: TQRLabel;
    QRLNoResp: TQRLabel;
    QRLParenResp: TQRLabel;
    QRLCPFResp: TQRLabel;
    QRLRGResp: TQRLabel;
    QRLabel55: TQRLabel;
    QRLabel56: TQRLabel;
    QRLTrabResp3: TQRLabel;
    QRLTrabResp2: TQRLabel;
    QRLTrabResp1: TQRLabel;
    QRLEndResp1: TQRLabel;
    QRLEndResp2: TQRLabel;
    QRLEndResp3: TQRLabel;
    QRLabel63: TQRLabel;
    QRShape4: TQRShape;
    QRLabel64: TQRLabel;
    QRLabel2: TQRLabel;
    QRSubDetailGestores: TQRSubDetail;
    QRLDtPrevMat: TQRLabel;
    QRLContato: TQRLabel;
    QRDBText1: TQRDBText;
    qryDoctos: TADOQuery;
    QRBand1: TQRBand;
    lblNumeroComprovante: TQRLabel;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel3: TQRLabel;
    QRLModulo: TQRLabel;
    QRLSerie: TQRLabel;
    QRLabel7: TQRLabel;
    QRLTpReserva: TQRLabel;
    QRLabel9: TQRLabel;
    QRLDtVencReser: TQRLabel;
    QRLabel12: TQRLabel;
    QRLDtReser: TQRLabel;
    QRLabel11: TQRLabel;
    QRLRespReser: TQRLabel;
    QRLabel10: TQRLabel;
    QRLLocalReser: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel13: TQRLabel;
    QRLEscola1: TQRLabel;
    QRLEscola2: TQRLabel;
    QRLEscola3: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure DetailBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRBANDSGIEBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    contDoc : integer;
  public
    { Public declarations }
  end;

var
  FrmRelComprReserMatric: TFrmRelComprReserMatric;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelComprReserMatric.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLNumReserva.Caption := QryRelatorio.FieldByName('NU_RESERVA').AsString;// FormatFloat('0000',QryRelatorio.FieldByName('NU_ANO_LETIVO').AsFloat) + '.' +
  //FormatFloat('0000', QryRelatorio.FieldByName('CO_EMP').AsFloat) + '.' +
  //FormatFloat('000000',QryRelatorio.FieldByName('CO_NUM_EMP').AsFloat);

  if not QryRelatorio.FieldByName('noEsc1').IsNull then
    QRLEscola1.Caption := '(1)  ' + UpperCase(QryRelatorio.FieldByName('noEsc1').AsString)
  else
    QRLEscola1.Caption := '-';

  if not QryRelatorio.FieldByName('noEsc2').IsNull then
    QRLEscola2.Caption := '(2)  ' + UpperCase(QryRelatorio.FieldByName('noEsc2').AsString)
  else
    QRLEscola2.Caption := '-';

  if not QryRelatorio.FieldByName('noEsc3').IsNull then
    QRLEscola3.Caption := '(3)  ' + UpperCase(QryRelatorio.FieldByName('noEsc3').AsString)
  else
    QRLEscola3.Caption := '-';

  if not QryRelatorio.FieldByName('de_modu_cur').IsNull then
    QRLModulo.Caption := QryRelatorio.FieldByName('de_modu_cur').AsString
  else
    QRLModulo.Caption := '-';

  if not QryRelatorio.FieldByName('no_cur').IsNull then
    QRLSerie.Caption := QryRelatorio.FieldByName('no_cur').AsString
  else
    QRLSerie.Caption := '-';

  if QryRelatorio.FieldByName('TP_RESERVA').AsString = 'N' then
    QRLTpReserva.Caption := 'Matrícula Nova'
  else
    QRLTpReserva.Caption := 'Rematrícula';

  //*********************** Verificar depois *************
  QRLLocalReser.Caption := '-';
  //***********************

  if not QryRelatorio.FieldByName('NO_COL').IsNull then
    QRLRespReser.Caption := '(' + FormatMaskText('99.999-9;0', QryRelatorio.FieldByName('CO_MAT_COL').AsString) + ') ' + UpperCase(QryRelatorio.FieldByName('NO_COL').AsString)
  else
    QRLRespReser.Caption := '-';

  if not QryRelatorio.FieldByName('DT_CADASTRO').IsNull then
    QRLDtReser.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_CADASTRO').AsDateTime)
  else
    QRLDtReser.Caption := '-';

  if not QryRelatorio.FieldByName('DT_VALIDADE_RESERV').IsNull then
    QRLDtVencReser.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_VALIDADE_RESERV').AsDateTime)
  else
    QRLDtVencReser.Caption := '-';
end;

procedure TFrmRelComprReserMatric.QRSubDetailGestoresBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if contDoc = 0 then
  begin

    if not QryRelatorio.FieldByName('DT_PREV_MATR').IsNull then
      QRLDtPrevMat.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_PREV_MATR').AsDateTime)
    else
      QRLDtPrevMat.Caption := '-';

    if not QryRelatorio.FieldByName('NU_TEL_CONTAT').IsNull then
    begin
      QRLContato.Caption := FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TEL_CONTAT').AsString);

      if not QryRelatorio.FieldByName('NU_RAMAL_CONTAT').IsNull then
        QRLContato.Caption := QRLContato.Caption + ' / ' + QryRelatorio.FieldByName('NU_RAMAL_CONTAT').AsString;
    end
    else
      QRLContato.Caption := '-';

    contDoc := 1;
  end
  else
  begin
    QRLDtPrevMat.Caption := '';
    QRLContato.Caption := '';
  end;
end;

procedure TFrmRelComprReserMatric.DetailBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;

  if not QryRelatorio.FieldByName('DE_OBS_RESERV_MATRI').IsNull then
    QRLObsReserva.Caption := QryRelatorio.FieldByName('DE_OBS_RESERV_MATRI').AsString
  else
    QRLObsReserva.Caption := '-';

  if not QryRelatorio.FieldByName('CO_ALU').IsNull then
  begin
    { Recupera a descrição do calendário }
    with DM.QrySql do
    begin
      { Grava a cabeça do calendário }
      Close;
      Sql.Clear;
      Sql.text := 'Select A.NO_ALU, A.DT_NASC_ALU, A.CO_SEXO_ALU, A.TP_DEF, A.NO_MAE_ALU, A.NO_PAI_ALU,' +
        'A.DE_ENDE_ALU, A.NU_ENDE_ALU, A.DE_COMP_ALU,A.CO_GRAU_PAREN_RESP,'+
        'A.NU_TELE_RESI_ALU, A.NU_TELE_CELU_ALU, B.NO_BAIRRO, C.NO_CIDADE, a.CO_ESTA_ALU, A.CO_CEP_ALU' +
        ' From TB07_ALUNO A ' +
        ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
        ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
        ' Where A.CO_ALU = ' + QryRelatorio.FieldByName('CO_ALU').AsString +
        ' and A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP_ALU').AsString;

      Open;

      if not IsEmpty then
      begin
        if not FieldByName('NO_ALU').IsNull then
          QRLCandid.Caption := UpperCase(FieldByName('NO_ALU').AsString)
        else
          QRLCandid.Caption := '-';

        if not FieldByName('DT_NASC_ALU').IsNull then
        begin
          diasnoano := 365.6;
          QRLDtNascCandid.Caption := FieldByName('DT_NASC_ALU').AsString + ' (' +IntToStr(Trunc(DaysBetween(Fieldbyname('DT_NASC_ALU').AsDateTime,Now) / diasnoano)) + ')';
        end
        else
          QRLDtNascCandid.Caption := '-';

        if FieldByName('CO_SEXO_ALU').AsString = 'M' then
          QRLSxCandid.Caption := 'MASCULINO'
        else
          QRLSxCandid.Caption := 'FEMININO';

        if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PM' then
          QRLParenResp.Caption := 'PAI/MÃE'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TI' then
          QRLParenResp.Caption := 'TIO(A)'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'AV' then
          QRLParenResp.Caption := 'AVÔ/AVÓ'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PR' then
          QRLParenResp.Caption := 'PRIMO(A)'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'CN' then
          QRLParenResp.Caption := 'CUNHADO(A)'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TU' then
          QRLParenResp.Caption := 'TUTOR(A)'
        else if FieldByName('CO_GRAU_PAREN_RESP').AsString = 'IR' then
          QRLParenResp.Caption := 'IRMÃO(Ã)'
        else
          QRLParenResp.Caption := 'OUTROS';

        if FieldByName('TP_DEF').AsString = 'N' then
          QRLDefCand.Caption := 'NENHUMA'
        else if FieldByName('TP_DEF').AsString = 'A' then
          QRLDefCand.Caption := 'AUDITIVA'
        else if FieldByName('TP_DEF').AsString = 'V' then
          QRLDefCand.Caption := 'VISUAL'
        else if FieldByName('TP_DEF').AsString = 'F' then
          QRLDefCand.Caption := 'FÍSICA'
        else if FieldByName('TP_DEF').AsString = 'M' then
          QRLDefCand.Caption := 'MENTAL'
        else if FieldByName('TP_DEF').AsString = 'P' then
          QRLDefCand.Caption := 'MÚLTIPLAS'
        else
          QRLDefCand.Caption := 'OUTRAS';

        if not FieldByName('NO_MAE_ALU').IsNull then
          QRLMaeCandid.Caption := '(Mãe) ' + UpperCase(FieldByName('NO_MAE_ALU').AsString)
        else
          QRLMaeCandid.Caption := '-';

        if not FieldByName('NO_PAI_ALU').IsNull then
          QRLPaiCandid.Caption := '(Pai) ' + UpperCase(FieldByName('NO_PAI_ALU').AsString)
        else
          QRLPaiCandid.Caption := '-';

        if (not FieldByName('DE_ENDE_ALU').IsNull) and (FieldByName('DE_ENDE_ALU').AsString <> '') then
        begin
          QRLEndCandid1.Caption := FieldByName('DE_ENDE_ALU').AsString;
          if (not FieldByName('NU_ENDE_ALU').IsNull) and (FieldByName('NU_ENDE_ALU').AsString <> '') then
            QRLEndCandid1.Caption := QRLEndCandid1.Caption + ', ' + FieldByName('NU_ENDE_ALU').AsString;

          //if (not FieldByName('de_comp').IsNull) and (FieldByName('de_comp').AsString <> '') then
            //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('de_comp').AsString;
        end
        else
          QRLEndCandid1.Caption := '****';

        QRLEndCandid1.Caption := QRLEndCandid1.Caption + ' - Bairro ';

        if not FieldByName('NO_BAIRRO').IsNull then
          QRLEndCandid1.Caption := QRLEndCandid1.Caption + FieldByName('NO_BAIRRO').AsString
        else
          QRLEndCandid1.Caption := QRLEndCandid1.Caption + '****';

        QRLEndCandid2.Caption := 'CEP ';

        if not FieldByName('CO_CEP_ALU').IsNull then
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + FormatMaskText('00000-000;0', FieldByName('CO_CEP_ALU').AsString)
        else
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' ***';

        if not FieldByName('NO_CIDADE').IsNull then
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ' + FieldByName('NO_CIDADE').AsString
        else
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ***';

        if not FieldByName('CO_ESTA_ALU').IsNull then
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ' + FieldByName('CO_ESTA_ALU').AsString
        else
          QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - **';

        QRLEndCandid3.Caption := 'Tels ';

        if (not FieldByName('NU_TELE_RESI_ALU').IsNull) and (FieldByName('NU_TELE_RESI_ALU').AsString <> '') then
        begin
          QRLEndCandid3.Caption := QRLEndCandid3.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_RESI_ALU').AsString);
          if (not FieldByName('NU_TELE_CELU_ALU').IsNull) and (FieldByName('NU_TELE_CELU_ALU').AsString <> '') then
            QRLEndCandid3.Caption := QRLEndCandid3.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_ALU').AsString);
        end
        else
        begin
          if (not FieldByName('NU_TELE_CELU_ALU').IsNull) and (FieldByName('NU_TELE_CELU_ALU').AsString <> '') then
            QRLEndCandid3.Caption := QRLEndCandid3.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_ALU').AsString)
          else
            QRLEndCandid3.Caption := QRLEndCandid3.Caption + ' -';
        end;
      end;
    end;
  end
  else
  begin
    if not QryRelatorio.FieldByName('NO_ALU').IsNull then
      QRLCandid.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
    else
      QRLCandid.Caption := '-';

    if not QryRelatorio.FieldByName('DT_NASC_ALU').IsNull then
    begin
      diasnoano := 365.6;
      QRLDtNascCandid.Caption := QryRelatorio.FieldByName('DT_NASC_ALU').AsString + ' (' +IntToStr(Trunc(DaysBetween(now,QryRelatorio.Fieldbyname('DT_NASC_ALU').AsDateTime) / diasnoano)) + ')';
    end
    else
      QRLDtNascCandid.Caption := '-';

    if QryRelatorio.FieldByName('CO_SEXO_ALU').AsString = 'M' then
      QRLSxCandid.Caption := 'MASCULINO'
    else
      QRLSxCandid.Caption := 'FEMININO';

    if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PM' then
      QRLParenResp.Caption := 'PAI/MÃE'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TI' then
      QRLParenResp.Caption := 'TIO(A)'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'AV' then
      QRLParenResp.Caption := 'AVÔ/AVÓ'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'PR' then
      QRLParenResp.Caption := 'PRIMO(A)'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'CN' then
      QRLParenResp.Caption := 'CUNHADO(A)'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'TU' then
      QRLParenResp.Caption := 'TUTOR(A)'
    else if QryRelatorio.FieldByName('CO_GRAU_PAREN_RESP').AsString = 'IR' then
      QRLParenResp.Caption := 'IRMÃO(Ã)'
    else
      QRLParenResp.Caption := 'OUTROS';

    if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'N' then
      QRLDefCand.Caption := 'NENHUMA'
    else if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'A' then
      QRLDefCand.Caption := 'AUDITIVA'
    else if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'V' then
      QRLDefCand.Caption := 'VISUAL'
    else if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'F' then
      QRLDefCand.Caption := 'FÍSICA'
    else if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'M' then
      QRLDefCand.Caption := 'MENTAL'
    else if QryRelatorio.FieldByName('TP_DEFIC_ALU').AsString = 'P' then
      QRLDefCand.Caption := 'MÚLTIPLAS'
    else
      QRLDefCand.Caption := 'OUTRAS';

    if not QryRelatorio.FieldByName('NO_MAE_ALU').IsNull then
      QRLMaeCandid.Caption := '(Mãe) ' + UpperCase(QryRelatorio.FieldByName('NO_MAE_ALU').AsString)
    else
      QRLMaeCandid.Caption := '-';

    if not QryRelatorio.FieldByName('NO_PAI_ALU').IsNull then
      QRLPaiCandid.Caption := '(Pai) ' + UpperCase(QryRelatorio.FieldByName('NO_PAI_ALU').AsString)
    else
      QRLPaiCandid.Caption := '-';

    if (not QryRelatorio.FieldByName('DE_END_ALU').IsNull) and (QryRelatorio.FieldByName('DE_END_ALU').AsString <> '') then
    begin
      QRLEndCandid1.Caption := QryRelatorio.FieldByName('DE_END_ALU').AsString;
      if (not QryRelatorio.FieldByName('NU_END_ALU').IsNull) and (QryRelatorio.FieldByName('NU_END_ALU').AsString <> '') then
        QRLEndCandid1.Caption := QRLEndCandid1.Caption + ', ' + QryRelatorio.FieldByName('NU_END_ALU').AsString;

      //if (not FieldByName('de_comp').IsNull) and (FieldByName('de_comp').AsString <> '') then
        //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('de_comp').AsString;
    end
    else
      QRLEndCandid1.Caption := '****';

    QRLEndCandid1.Caption := QRLEndCandid1.Caption + ' - Bairro ';

    if not QryRelatorio.FieldByName('NO_BAIRRO').IsNull then
      QRLEndCandid1.Caption := QRLEndCandid1.Caption + QryRelatorio.FieldByName('NO_BAIRRO').AsString
    else
      QRLEndCandid1.Caption := QRLEndCandid1.Caption + '****';

    QRLEndCandid2.Caption := 'CEP ';

    if not QryRelatorio.FieldByName('CO_CEP_ALU').IsNull then
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + FormatMaskText('00000-000;0', QryRelatorio.FieldByName('CO_CEP_ALU').AsString)
    else
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' ***';

    if not QryRelatorio.FieldByName('NO_CIDADE').IsNull then
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ' + QryRelatorio.FieldByName('NO_CIDADE').AsString
    else
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ***';

    if not QryRelatorio.FieldByName('CO_UF').IsNull then
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - ' + QryRelatorio.FieldByName('CO_UF').AsString
    else
      QRLEndCandid2.Caption := QRLEndCandid2.Caption + ' - **';

    QRLEndCandid3.Caption := 'Tels ';

    if (not QryRelatorio.FieldByName('NU_TEL_ALU').IsNull) and (QryRelatorio.FieldByName('NU_TEL_ALU').AsString <> '') then
    begin
      QRLEndCandid3.Caption := QRLEndCandid3.Caption + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TEL_ALU').AsString);
      if (not QryRelatorio.FieldByName('NU_CEL_ALU').IsNull) and (QryRelatorio.FieldByName('NU_CEL_ALU').AsString <> '') then
        QRLEndCandid3.Caption := QRLEndCandid3.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_CEL_ALU').AsString);
    end
    else
    begin
      if (not QryRelatorio.FieldByName('NU_CEL_ALU').IsNull) and (QryRelatorio.FieldByName('NU_CEL_ALU').AsString <> '') then
        QRLEndCandid3.Caption := QRLEndCandid3.Caption + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_CEL_ALU').AsString)
      else
        QRLEndCandid3.Caption := QRLEndCandid3.Caption + ' -';
    end;
  end;

  //************** Responsável Candidato ****************
  if not QryRelatorio.FieldByName('CO_RESP').IsNull then
  begin
    { Recupera a descrição do calendário }
    with DM.QrySql do
    begin
      { Grava a cabeça do calendário }
      Close;
      Sql.Clear;
      Sql.text := 'Select A.NO_RESP, A.NU_CPF_RESP, A.CO_RG_RESP, A.CO_ORG_RG_RESP, A.DT_EMIS_RG_RESP,' +
        'A.DE_ENDE_RESP, A.NU_ENDE_RESP, A.DE_COMP_RESP,A.NO_EMPR_RESP, A.NO_FUNCAO_RESP, A.DES_EMAIL_EMP, A.NU_TELE_COME_RESP,'+
        'A.NU_TELE_RESI_RESP, A.NU_TELE_CELU_RESP, B.NO_BAIRRO, C.NO_CIDADE, a.CO_ESTA_RESP, A.CO_CEP_RESP' +
        ' From TB108_RESPONSAVEL A ' +
        ' JOIN TB904_CIDADE C on A.CO_CIDADE = C.CO_CIDADE ' +
        ' JOIN TB905_BAIRRO B on B.CO_CIDADE = A.CO_CIDADE and B.CO_BAIRRO = A.CO_BAIRRO ' +
        ' Where A.CO_RESP = ' + QryRelatorio.FieldByName('CO_RESP').AsString;
        
      Open;

      if not IsEmpty then
      begin
        if not FieldByName('NO_RESP').IsNull then
          QRLNoResp.Caption := UpperCase(FieldByName('NO_RESP').AsString)
        else
          QRLNoResp.Caption := '-';

        if not FieldByName('NU_CPF_RESP').IsNull then
          QRLCPFResp.Caption := FormatMaskText('000.000.000-00;0',FieldByName('NU_CPF_RESP').AsString)
        else
          QRLCPFResp.Caption := '-';

        if not FieldByName('CO_RG_RESP').IsNull then
        begin
          QRLRGResp.Caption := FieldByName('CO_RG_RESP').AsString;

          if not FieldByName('CO_ORG_RG_RESP').IsNull then
            QRLRGResp.Caption := QRLRGResp.Caption + ' - ' + FieldByName('CO_ORG_RG_RESP').AsString;

          if not FieldByName('DT_EMIS_RG_RESP').IsNull then
            QRLRGResp.Caption := QRLRGResp.Caption + ' - ' + FieldByName('DT_EMIS_RG_RESP').AsString;
        end
        else
          QRLRGResp.Caption := '-';

        if (not FieldByName('DE_ENDE_RESP').IsNull) and (FieldByName('DE_ENDE_RESP').AsString <> '') then
        begin
          QRLEndResp1.Caption := FieldByName('DE_ENDE_RESP').AsString;
          if (not FieldByName('NU_ENDE_RESP').IsNull) and (FieldByName('NU_ENDE_RESP').AsString <> '') then
            QRLEndResp1.Caption := QRLEndResp1.Caption + ', ' + FieldByName('NU_ENDE_RESP').AsString;

          //if (not FieldByName('de_comp').IsNull) and (FieldByName('de_comp').AsString <> '') then
            //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('de_comp').AsString;
        end
        else
          QRLEndResp1.Caption := '****';

        QRLEndResp1.Caption := QRLEndResp1.Caption + ' - Bairro ';

        if not FieldByName('NO_BAIRRO').IsNull then
          QRLEndResp1.Caption := QRLEndResp1.Caption + FieldByName('NO_BAIRRO').AsString
        else
          QRLEndResp1.Caption := QRLEndResp1.Caption + '****';

        QRLEndResp2.Caption := 'CEP ';

        if not FieldByName('CO_CEP_RESP').IsNull then
          QRLEndResp2.Caption := QRLEndResp2.Caption + FormatMaskText('00000-000;0', FieldByName('CO_CEP_RESP').AsString)
        else
          QRLEndResp2.Caption := QRLEndResp2.Caption + ' ***';

        if not FieldByName('NO_CIDADE').IsNull then
          QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ' + FieldByName('NO_CIDADE').AsString
        else
          QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ***';

        if not FieldByName('CO_ESTA_RESP').IsNull then
          QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ' + FieldByName('CO_ESTA_RESP').AsString
        else
          QRLEndResp2.Caption := QRLEndResp2.Caption + ' - **';

        QRLEndResp3.Caption := 'Tels ';

        if (not FieldByName('NU_TELE_RESI_RESP').IsNull) and (FieldByName('NU_TELE_RESI_RESP').AsString <> '') then
        begin
          QRLEndResp3.Caption := QRLEndResp3.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_RESI_RESP').AsString);
          if (not FieldByName('NU_TELE_CELU_RESP').IsNull) and (FieldByName('NU_TELE_CELU_RESP').AsString <> '') then
            QRLEndResp3.Caption := QRLEndResp3.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_RESP').AsString);
        end
        else
        begin
          if (not FieldByName('NU_TELE_CELU_RESP').IsNull) and (FieldByName('NU_TELE_CELU_RESP').AsString <> '') then
            QRLEndResp3.Caption := QRLEndResp3.Caption + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_CELU_RESP').AsString)
          else
            QRLEndResp3.Caption := QRLEndResp3.Caption + ' -';
        end;

        if not FieldByName('NO_EMPR_RESP').IsNull then
          QRLTrabResp1.Caption := '(Empresa) ' + UpperCase(FieldByName('NO_EMPR_RESP').AsString)
        else
          QRLTrabResp1.Caption := '(Empresa) *****';

        if not FieldByName('NO_FUNCAO_RESP').IsNull then
          QRLTrabResp2.Caption := '(Função) ' + FieldByName('NO_FUNCAO_RESP').AsString
        else
          QRLTrabResp2.Caption := '(Função) *****';

        if not FieldByName('NU_TELE_COME_RESP').IsNull then
          QRLTrabResp2.Caption := QRLTrabResp2.Caption + ' (Fone) ' + FormatMaskText('(00) 0000-0000;0',FieldByName('NU_TELE_COME_RESP').AsString)
        else
          QRLTrabResp2.Caption := QRLTrabResp2.Caption + ' (Fone) *****';

        if not FieldByName('DES_EMAIL_EMP').IsNull then
          QRLTrabResp3.Caption := '(Email) ' + FieldByName('DES_EMAIL_EMP').AsString
        else
          QRLTrabResp3.Caption := '(Email) *****';
      end;
    end;
  end
  else
  begin
    if not QryRelatorio.FieldByName('NO_RESP').IsNull then
      QRLNoResp.Caption := UpperCase(QryRelatorio.FieldByName('NO_RESP').AsString)
    else
      QRLNoResp.Caption := '-';

    if not QryRelatorio.FieldByName('NU_CPF_RESP').IsNull then
      QRLCPFResp.Caption := FormatMaskText('000.000.000-00;0',QryRelatorio.FieldByName('NU_CPF_RESP').AsString)
    else
      QRLCPFResp.Caption := '-';

    if not QryRelatorio.FieldByName('CO_RG_RESP').IsNull then
    begin
      QRLRGResp.Caption := QryRelatorio.FieldByName('CO_RG_RESP').AsString;

      if not QryRelatorio.FieldByName('CO_ORG_RG_RESP').IsNull then
        QRLRGResp.Caption := QRLRGResp.Caption + ' - ' + QryRelatorio.FieldByName('CO_ORG_RG_RESP').AsString;

      if not QryRelatorio.FieldByName('DT_EMIS_RG_RESP').IsNull then
        QRLRGResp.Caption := QRLRGResp.Caption + ' - ' + QryRelatorio.FieldByName('DT_EMIS_RG_RESP').AsString;
    end
    else
      QRLRGResp.Caption := '-';

    if (not QryRelatorio.FieldByName('DE_END_RESP').IsNull) and (QryRelatorio.FieldByName('DE_END_RESP').AsString <> '') then
    begin
      QRLEndResp1.Caption := QryRelatorio.FieldByName('DE_END_RESP').AsString;
      if (not QryRelatorio.FieldByName('NU_END_RESP').IsNull) and (QryRelatorio.FieldByName('NU_END_RESP').AsString <> '') then
        QRLEndResp1.Caption := QRLEndResp1.Caption + ', ' + QryRelatorio.FieldByName('NU_END_RESP').AsString;

      //if (not FieldByName('de_comp').IsNull) and (FieldByName('de_comp').AsString <> '') then
        //qrlUsuEndereco.Caption := qrlUsuEndereco.Caption + ' - ' + FieldByName('de_comp').AsString;
    end
    else
      QRLEndResp1.Caption := '****';

    QRLEndResp1.Caption := QRLEndResp1.Caption + ' - Bairro ';

    if not QryRelatorio.FieldByName('no_bairro_resp').IsNull then
      QRLEndResp1.Caption := QRLEndResp1.Caption + QryRelatorio.FieldByName('no_bairro_resp').AsString
    else
      QRLEndResp1.Caption := QRLEndResp1.Caption + '****';

    QRLEndResp2.Caption := 'CEP ';

    if not QryRelatorio.FieldByName('CO_CEP_RESP').IsNull then
      QRLEndResp2.Caption := QRLEndResp2.Caption + FormatMaskText('00000-000;0', QryRelatorio.FieldByName('CO_CEP_RESP').AsString)
    else
      QRLEndResp2.Caption := QRLEndResp2.Caption + ' ***';

    if not QryRelatorio.FieldByName('no_cidade_resp').IsNull then
      QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ' + QryRelatorio.FieldByName('no_cidade_resp').AsString
    else
      QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ***';

    if not QryRelatorio.FieldByName('co_uf_resp').IsNull then
      QRLEndResp2.Caption := QRLEndResp2.Caption + ' - ' + QryRelatorio.FieldByName('co_uf_resp').AsString
    else
      QRLEndResp2.Caption := QRLEndResp2.Caption + ' - **';

    QRLEndResp3.Caption := 'Tels ';

    if (not QryRelatorio.FieldByName('NU_TEL_RESP').IsNull) and (QryRelatorio.FieldByName('NU_TEL_RESP').AsString <> '') then
    begin
      QRLEndResp3.Caption := QRLEndResp3.Caption + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TEL_RESP').AsString);
      if (not QryRelatorio.FieldByName('NU_CEL_RESP').IsNull) and (QryRelatorio.FieldByName('NU_CEL_RESP').AsString <> '') then
        QRLEndResp3.Caption := QRLEndResp3.Caption + ' / ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_CEL_RESP').AsString);
    end
    else
    begin
      if (not QryRelatorio.FieldByName('NU_CEL_RESP').IsNull) and (QryRelatorio.FieldByName('NU_CEL_RESP').AsString <> '') then
        QRLEndResp3.Caption := QRLEndResp3.Caption + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_CEL_RESP').AsString)
      else
        QRLEndResp3.Caption := QRLEndResp3.Caption + ' -';
    end;

    if not QryRelatorio.FieldByName('NO_EMP_TRAB_RESP').IsNull then
      QRLTrabResp1.Caption := '(Empresa) ' + UpperCase(QryRelatorio.FieldByName('NO_EMP_TRAB_RESP').AsString)
    else
      QRLTrabResp1.Caption := '(Empresa) *****';

    if not QryRelatorio.FieldByName('NO_FUNC_TRAB_RESP').IsNull then
      QRLTrabResp2.Caption := '(Função) ' + QryRelatorio.FieldByName('NO_FUNC_TRAB_RESP').AsString
    else
      QRLTrabResp2.Caption := '(Função) *****';

    if not QryRelatorio.FieldByName('NU_TEL_TRAB_RESP').IsNull then
      QRLTrabResp2.Caption := QRLTrabResp2.Caption + ' (Fone) ' + FormatMaskText('(00) 0000-0000;0',QryRelatorio.FieldByName('NU_TEL_TRAB_RESP').AsString)
    else
      QRLTrabResp2.Caption := QRLTrabResp2.Caption + ' (Fone) *****';

    if not QryRelatorio.FieldByName('DE_EMAIL_TRAB_RESP').IsNull then
      QRLTrabResp3.Caption := '(Email) ' + QryRelatorio.FieldByName('DE_EMAIL_TRAB_RESP').AsString
    else
      QRLTrabResp3.Caption := '(Email) *****';
  end;
end;

procedure TFrmRelComprReserMatric.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with qryDoctos do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select tdm.DE_TP_DOC_MAT from TB208_CURSO_DOCTOS cd ' +
                'join TB121_TIPO_DOC_MATRICULA tdm on cd.CO_TP_DOC_MAT = tdm.CO_TP_DOC_MAT ' +
                'where cd.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString +
                ' and cd.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP1').AsString +
                ' and cd.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                ' order by tdm.DE_TP_DOC_MAT';
    Open;
  end;

  contDoc := 0;
end;

procedure TFrmRelComprReserMatric.QRBANDSGIEBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLCabAss.Caption := QryRelatorio.FieldByName('cidadeUnid').AsString + ' - ' + QryRelatorio.FieldByName('CO_UF_EMP').AsString + ',' + FormatDateTime('dd/MM/yyyy',Now);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelComprReserMatric]);

end.
