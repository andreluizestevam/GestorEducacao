unit U_FrmRelAgendaContatos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelAgendaContatos = class(TFrmRelTemplate)
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    Detail: TQRBand;
    QrlTelCont: TQRLabel;
    QrlCelCont: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel13: TQRLabel;
    QRLTotal: TQRLabel;
    QRLNoCont: TQRLabel;
    QRLTelComerCont: TQRLabel;
    QRLabel7: TQRLabel;
    QRLEmailCont: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLTpCont: TQRLabel;
    QRLDtNasctoCont: TQRLabel;
    QRLSxCont: TQRLabel;
    QRLApeCont: TQRLabel;
    QRLabel6: TQRLabel;
    QRLNomeUsu: TQRLabel;
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelAgendaContatos: TFrmRelAgendaContatos;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelAgendaContatos.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if QryRelatorio.FieldByName('TP_CONTAT').AsString = 'O' then
  begin
    QRLTpCont.Caption := 'Outros';

    QRLNoCont.Caption := UpperCase(QryRelatorio.FieldByName('NO_CONTAT').AsString);

    if not QryRelatorio.FieldByName('NO_APELI_CONTAT').IsNull then
      QRLApeCont.Caption := UpperCase(QryRelatorio.FieldByName('NO_APELI_CONTAT').AsString)
    else
      QRLApeCont.Caption := '*****';

    QRLDtNasctoCont.Caption := QryRelatorio.FieldByName('DT_NASCTO_CONTAT').AsString;

    QRLSxCont.Caption := QryRelatorio.FieldByName('CO_SEXO_CONTAT').AsString;

    // TELEFONE
    if not QryRelatorio.FieldByName('NU_TELE_RESI_CONTAT').IsNull then
    begin
      QrlTelCont.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_RESI_CONTAT').AsString);
    end
    else
    begin
      QrlTelCont.Caption := ' - ';
    end;

    // CELULAR
    if not QryRelatorio.FieldByName('NU_TELE_CELU_CONTAT').IsNull then
    begin
      QrlCelCont.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_CELU_CONTAT').AsString);
    end
    else
    begin
      QrlCelCont.Caption := ' - ';
    end;

    // COMERCIAL
    if not QryRelatorio.FieldByName('NU_TELE_COME_CONTAT').IsNull then
    begin
      QRLTelComerCont.Caption := FormatMaskText('(00) 0000-0000;0', QryRelatorio.FieldByName('NU_TELE_COME_CONTAT').AsString);
    end
    else
    begin
      QRLTelComerCont.Caption := ' - ';
    end;

    if not QryRelatorio.FieldByName('DE_EMAIL_CONTAT').IsNull then
    begin
      QRLEmailCont.Caption := QryRelatorio.FieldByName('DE_EMAIL_CONTAT').AsString;
    end
    else
    begin
      QRLEmailCont.Caption := ' - ';
    end;
  end
  else if (QryRelatorio.FieldByName('TP_CONTAT').AsString = 'F') or (QryRelatorio.FieldByName('TP_CONTAT').AsString = 'P') then
  begin
    if QryRelatorio.FieldByName('TP_CONTAT').AsString = 'F' then
      QRLTpCont.Caption := 'Funcionário'
    else
      QRLTpCont.Caption := 'Professor';

    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select NO_COL, NO_APEL_COL, DT_NASC_COL, CO_SEXO_COL, NU_TELE_RESI_COL, NU_TELE_CELU_COL,' +
                  'NU_TELE_COME_COL, CO_EMAI_COL from tb03_colabor ' +
                  ' where co_col = ' + QryRelatorio.FieldByName('CO_CONTAT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLNoCont.Caption := UpperCase(FieldByName('NO_COL').AsString);

        if not FieldByName('NO_APEL_COL').IsNull then
          QRLApeCont.Caption := UpperCase(FieldByName('NO_APEL_COL').AsString)
        else
          QRLApeCont.Caption := '*****';

        QRLDtNasctoCont.Caption := FieldByName('DT_NASC_COL').AsString;

        QRLSxCont.Caption := FieldByName('CO_SEXO_COL').AsString;

        // TELEFONE
        if not FieldByName('NU_TELE_RESI_COL').IsNull then
        begin
          QrlTelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_RESI_COL').AsString);
        end
        else
        begin
          QrlTelCont.Caption := ' - ';
        end;

        // CELULAR
        if not FieldByName('NU_TELE_CELU_COL').IsNull then
        begin
          QrlCelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_CELU_COL').AsString);
        end
        else
        begin
          QrlCelCont.Caption := ' - ';
        end;

        // COMERCIAL
        if not FieldByName('NU_TELE_COME_COL').IsNull then
        begin
          QRLTelComerCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_COME_COL').AsString);
        end
        else
        begin
          QRLTelComerCont.Caption := ' - ';
        end;

        if not FieldByName('CO_EMAI_COL').IsNull then
        begin
          QRLEmailCont.Caption := FieldByName('CO_EMAI_COL').AsString;
        end
        else
        begin
          QRLEmailCont.Caption := ' - ';
        end;
      end;
    end;
  end
  else if QryRelatorio.FieldByName('TP_CONTAT').AsString = 'A' then
  begin
    QRLTpCont.Caption := 'Aluno';

    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select NO_ALU, NO_APE_ALU, DT_NASC_ALU, CO_SEXO_ALU, NU_TELE_RESI_ALU, NU_TELE_CELU_ALU,' +
                  'NU_TELE_COME_ALU, NO_WEB_ALU from tb07_aluno ' +
                  ' where co_alu = ' + QryRelatorio.FieldByName('CO_CONTAT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLNoCont.Caption := UpperCase(FieldByName('NO_ALU').AsString);

        if not FieldByName('NO_APE_ALU').IsNull then
          QRLApeCont.Caption := UpperCase(FieldByName('NO_APE_ALU').AsString)
        else
          QRLApeCont.Caption := '*****';

        QRLDtNasctoCont.Caption := FieldByName('DT_NASC_ALU').AsString;

        QRLSxCont.Caption := FieldByName('CO_SEXO_ALU').AsString;

        // TELEFONE
        if not FieldByName('NU_TELE_RESI_ALU').IsNull then
        begin
          QrlTelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_RESI_ALU').AsString);
        end
        else
        begin
          QrlTelCont.Caption := ' - ';
        end;

        // CELULAR
        if not FieldByName('NU_TELE_CELU_ALU').IsNull then
        begin
          QrlCelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_CELU_ALU').AsString);
        end
        else
        begin
          QrlCelCont.Caption := ' - ';
        end;

        // COMERCIAL
        if not FieldByName('NU_TELE_COME_ALU').IsNull then
        begin
          QRLTelComerCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_COME_ALU').AsString);
        end
        else
        begin
          QRLTelComerCont.Caption := ' - ';
        end;

        if not FieldByName('NO_WEB_ALU').IsNull then
        begin
          QRLEmailCont.Caption := FieldByName('NO_WEB_ALU').AsString;
        end
        else
        begin
          QRLEmailCont.Caption := ' - ';
        end;
      end;
    end;
  end
  else
  begin
    QRLTpCont.Caption := 'Responsável';

    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'select NO_RESP, DT_NASC_RESP, CO_SEXO_RESP, NU_TELE_RESI_RESP, NU_TELE_CELU_RESP,' +
                  'NU_TELE_COME_RESP, DES_EMAIL_RESP from tb108_responsavel ' +
                  ' where co_resp = ' + QryRelatorio.FieldByName('CO_CONTAT').AsString;
      Open;

      if not IsEmpty then
      begin
        QRLNoCont.Caption := UpperCase(FieldByName('NO_RESP').AsString);

        QRLApeCont.Caption := '*****';

        QRLDtNasctoCont.Caption := FieldByName('DT_NASC_RESP').AsString;

        QRLSxCont.Caption := FieldByName('CO_SEXO_RESP').AsString;

        // TELEFONE
        if not FieldByName('NU_TELE_RESI_RESP').IsNull then
        begin
          QrlTelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_RESI_RESP').AsString);
        end
        else
        begin
          QrlTelCont.Caption := ' - ';
        end;

        // CELULAR
        if not FieldByName('NU_TELE_CELU_RESP').IsNull then
        begin
          QrlCelCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_CELU_RESP').AsString);
        end
        else
        begin
          QrlCelCont.Caption := ' - ';
        end;

        // COMERCIAL
        if not FieldByName('NU_TELE_COME_RESP').IsNull then
        begin
          QRLTelComerCont.Caption := FormatMaskText('(00) 0000-0000;0', FieldByName('NU_TELE_COME_RESP').AsString);
        end
        else
        begin
          QRLTelComerCont.Caption := ' - ';
        end;

        if not FieldByName('DES_EMAIL_RESP').IsNull then
        begin
          QRLEmailCont.Caption := FieldByName('DES_EMAIL_RESP').AsString;
        end
        else
        begin
          QRLEmailCont.Caption := ' - ';
        end;
      end;
    end;
  end;

  if Detail.Color = clWhite then
    Detail.Color := $00F2F2F2
  else
    Detail.Color := clWhite;

  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelAgendaContatos.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotal.Caption := '0';
end;

procedure TFrmRelAgendaContatos.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLNomeUsu.Caption := QryRelatorio.FieldByName('NO_COL').AsString;
end;

end.
