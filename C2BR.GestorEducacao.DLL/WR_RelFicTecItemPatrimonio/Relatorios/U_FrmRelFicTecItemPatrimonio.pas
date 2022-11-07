unit U_FrmRelFicTecItemPatrimonio;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelFicTecItemPatrimonio = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLTModelo: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel19: TQRLabel;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRShape2: TQRShape;
    QRLabel21: TQRLabel;
    QRShape3: TQRShape;
    QRLabel22: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText3: TQRDBText;
    QRLTpPatr: TQRLabel;
    QRDBText6: TQRDBText;
    QRLStatus: TQRLabel;
    QRLModelo: TQRLabel;
    QRDBText9: TQRDBText;
    QRDBText13: TQRDBText;
    QRDBText16: TQRDBText;
    QRDBText17: TQRDBText;
    QRDBText18: TQRDBText;
    QRLVlAqui: TQRLabel;
    QRLDtCadastro: TQRLabel;
    QRLDtEmisNotFis: TQRLabel;
    QRLDtFimGar: TQRLabel;
    QRLTNuPlaca: TQRLabel;
    QRLNuPlaca: TQRLabel;
    QRLabel20: TQRLabel;
    QRDBText2: TQRDBText;
    QRLChassi: TQRLabel;
    QRLTChassi: TQRLabel;
    QRLAno: TQRLabel;
    QRLTAno: TQRLabel;
    QRLQuilom: TQRLabel;
    QRLTQuilom: TQRLabel;
    QRLCor: TQRLabel;
    QRLTCor: TQRLabel;
    QRLT1: TQRLabel;
    QRLD1: TQRLabel;
    QRLD2: TQRLabel;
    QRLT2: TQRLabel;
    QRLD3: TQRLabel;
    QRLT3: TQRLabel;
    QRLNumNtFiscal: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelFicTecItemPatrimonio: TFrmRelFicTecItemPatrimonio;

implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelFicTecItemPatrimonio.QRBand1BeforePrint(Sender: TQRCustomBand;var PrintBand: Boolean);
begin
  inherited;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    if QryRelatorio.FieldByName('TP_PATR').AsInteger = 1 then
    begin
      SQL.Text := 'select pm.NU_PLACA, pm.CO_CHASSI, pm.CO_ANO, pm.NO_MODELO, pm.NU_QUILOMETRAGEM, c.des_cor from TB216_PATR_MOVEL pm ' +
                  'left join TB97_COR c on c.co_cor = pm.co_cor ' +
                  'where pm.CO_PATR_MOVEL = ' + quotedStr(QryRelatorio.FieldByName('COD_PATR').AsString);
    end
    else
    begin
      SQL.Text := 'select pi.NU_REG_CART, pi.NU_ESCRITURA, pi.NO_CARTORIO, pi.CO_UF, c.NO_CIDADE, b.NO_BAIRRO, pi.NO_LOGRADOURO, pi.NU_LOGRADOURO,'+
                  'pi.NU_METRAGEM, pi.NO_CARAC_PATR, pi.NO_COMPLEMENTO, te.DE_TIPO_EDIFI, tec.DE_TIPO_ESTAD_CONSERV ' +
                  ' from TB217_PATR_IMOVEL pi ' +
                  ' left join tb904_cidade c on c.co_cidade = pi.co_cidade ' +
                  ' left join tb905_bairro b on b.co_cidade = pi.co_cidade and b.co_bairro = pi.co_bairro ' +
                  ' left join TB179_TIPO_EDIFI te on te.CO_SIGLA_TIPO_EDIFI = pi.CO_SIGLA_TIPO_EDIFI ' +
                  ' left join TB180_TIPO_ESTAD_CONSERV tec on tec.CO_SIGLA_TIPO_ESTAD_CONSERV = pi.CO_SIGLA_TIPO_ESTAD_CONSERV ' +
                  'where pi.COD_PATR_IMOVEL = ' + quotedStr(QryRelatorio.FieldByName('COD_PATR').AsString);
    end;
    
    Open;

    if not IsEmpty then
    begin
      if QryRelatorio.FieldByName('TP_PATR').AsInteger = 1 then
      begin
        //Movel
        if (not FieldByName('NU_PLACA').IsNull) and (FieldByName('NU_PLACA').AsString <> '') then
          QRLNuPlaca.Caption := FieldByName('NU_PLACA').AsString
        else
          QRLNuPlaca.Caption := '***';

        QRLTChassi.Left := 349;
        QRLChassi.Left := 349;
        if (not FieldByName('CO_CHASSI').IsNull) and (FieldByName('CO_CHASSI').AsString <> '') then
          QRLChassi.Caption := FieldByName('CO_CHASSI').AsString
        else
          QRLChassi.Caption := '***';

        QRLTAno.Left := 465;
        QRLAno.Left := 465;
        if (not FieldByName('CO_ANO').IsNull) and (FieldByName('CO_ANO').AsString <> '') then
          QRLAno.Caption := FieldByName('CO_ANO').AsString
        else
          QRLAno.Caption := '***';

        if (not FieldByName('NO_MODELO').IsNull) and (FieldByName('NO_MODELO').AsString <> '') then
          QRLModelo.Caption := FieldByName('NO_MODELO').AsString
        else
          QRLModelo.Caption := '***';

        if (not FieldByName('NU_QUILOMETRAGEM').IsNull) and (FieldByName('NU_QUILOMETRAGEM').AsString <> '') then
          QRLQuilom.Caption := FloatToStrF(FieldByName('NU_QUILOMETRAGEM').AsFloat, ffNumber, 15,2)
        else
          QRLQuilom.Caption := '***';

        if (not FieldByName('des_cor').IsNull) and (FieldByName('des_cor').AsString <> '') then
          QRLCor.Caption := FieldByName('des_cor').AsString
        else
          QRLCor.Caption := '***';
      end
      else
      begin
        //imovel
        QRLTNuPlaca.Caption := 'Nº Regis.';
        if (not FieldByName('NU_REG_CART').IsNull) and (FieldByName('NU_REG_CART').AsString <> '') then
          QRLNuPlaca.Caption := FieldByName('NU_REG_CART').AsString
        else
          QRLNuPlaca.Caption := '***';

        QRLT1.Enabled := true;
        QRLD1.Enabled := true;
        if (not FieldByName('NU_ESCRITURA').IsNull) and (FieldByName('NU_ESCRITURA').AsString <> '') then
          QRLD1.Caption := FieldByName('NU_ESCRITURA').AsString
        else
          QRLD1.Caption := '***';

        QRLT3.Enabled := true;
        QRLD3.Enabled := true;
        if (not FieldByName('NO_CARTORIO').IsNull) and (FieldByName('NO_CARTORIO').AsString <> '') then
          QRLD3.Caption := FieldByName('NO_CARTORIO').AsString
        else
          QRLD3.Caption := '***';

        QRLTAno.Caption := 'Tp Edif.';
        if (not FieldByName('DE_TIPO_EDIFI').IsNull) and (FieldByName('DE_TIPO_EDIFI').AsString <> '') then
          QRLAno.Caption := FieldByName('DE_TIPO_EDIFI').AsString
        else
          QRLAno.Caption := '***';

        QRLTChassi.Caption := 'Est. Conserv.';
        QRLTChassi.Left := 420;
        QRLChassi.Left := 420;
        if (not FieldByName('DE_TIPO_ESTAD_CONSERV').IsNull) and (FieldByName('DE_TIPO_ESTAD_CONSERV').AsString <> '') then
          QRLChassi.Caption := FieldByName('DE_TIPO_ESTAD_CONSERV').AsString
        else
          QRLChassi.Caption := '***';

        QRLT2.Enabled := true;
        QRLD2.Enabled := true;
        if (not FieldByName('NO_CARAC_PATR').IsNull) and (FieldByName('NO_CARAC_PATR').AsString <> '') then
          QRLD2.Caption := FieldByName('NO_CARAC_PATR').AsString
        else
          QRLD2.Caption := '***';


        QRLTModelo.Caption := 'UF/Cidade/Bairro';
        QRLModelo.Width := 200; 
        if (not FieldByName('CO_UF').IsNull) and (FieldByName('CO_UF').AsString <> '') then
          QRLModelo.Caption := FieldByName('CO_UF').AsString
        else
          QRLModelo.Caption := '***';

        if (not FieldByName('NO_CIDADE').IsNull) and (FieldByName('NO_CIDADE').AsString <> '') then
          QRLModelo.Caption := QRLModelo.Caption + ' / ' + FieldByName('NO_CIDADE').AsString
        else
          QRLModelo.Caption := QRLModelo.Caption + ' / ***';

        if (not FieldByName('NO_BAIRRO').IsNull) and (FieldByName('NO_BAIRRO').AsString <> '') then
          QRLModelo.Caption := QRLModelo.Caption + ' / ' + FieldByName('NO_BAIRRO').AsString
        else
          QRLModelo.Caption := QRLModelo.Caption + ' / ***';

        QRLTQuilom.Left := 445;
        QRLTQuilom.Caption := 'Endereço';
        QRLQuilom.Left := 445;
        if (not FieldByName('NO_LOGRADOURO').IsNull) and (FieldByName('NO_LOGRADOURO').AsString <> '') then
          QRLQuilom.Caption := FieldByName('NO_LOGRADOURO').AsString
        else
          QRLQuilom.Caption := '***';

        if (not FieldByName('NU_LOGRADOURO').IsNull) and (FieldByName('NU_LOGRADOURO').AsString <> '') then
          QRLQuilom.Caption := QRLQuilom.Caption + ' - ' + FieldByName('NU_LOGRADOURO').AsString
        else
          QRLQuilom.Caption := QRLQuilom.Caption + ' - S/Nº';

        if (not FieldByName('NO_COMPLEMENTO').IsNull) and (FieldByName('NO_COMPLEMENTO').AsString <> '') then
          QRLQuilom.Caption := QRLQuilom.Caption + ' / ' + FieldByName('NO_COMPLEMENTO').AsString
        else
          QRLQuilom.Caption := QRLQuilom.Caption + ' / ***';

        QRLTCor.Left := 630;
        QRLTCor.Caption := 'Metragem';
        QRLCor.Left := 630;
        if (not FieldByName('NU_METRAGEM').IsNull) and (FieldByName('NU_METRAGEM').AsString <> '') then
          QRLCor.Caption := FieldByName('NU_METRAGEM').AsString
        else
          QRLCor.Caption := '***';

      end;
    end;
  end;

{  if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'N' then
    QRLEstado.Caption := 'Novo'
  else if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'A' then
    QRLEstado.Caption := 'Com Avarias'
  else if QryRelatorio.FieldByName('CO_ESTADO').AsString = 'M' then
    QRLEstado.Caption := 'Em Manutenção'
  else
    QRLEstado.Caption := 'Normal';    }

  if QryRelatorio.FieldByName('TP_PATR').AsInteger = 1 then
    QRLTpPatr.Caption := 'Móvel'
  else
    QRLTpPatr.Caption := 'Imóvel';

  if QryRelatorio.FieldByName('CO_STATUS').AsString = 'A' then
    QRLStatus.Caption := 'Ativo'
  else
    QRLStatus.Caption := 'Inativo';

  if not QryRelatorio.FieldByName('VL_AQUIS').IsNull then
    QRLVlAqui.Caption := FloatToStrF(QryRelatorio.FieldByName('VL_AQUIS').AsFloat, ffNumber, 15, 2)
  else
    QRLVlAqui.Caption := '-';

  if not QryRelatorio.FieldByName('NU_NOT_FISC').IsNull then
    QRLNumNtFiscal.Caption := QryRelatorio.FieldByName('NU_NOT_FISC').AsString
  else
    QRLNumNtFiscal.Caption := '-';

  if not QryRelatorio.FieldByName('DT_CADASTRO').IsNull then
    QRLDtCadastro.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_CADASTRO').AsDateTime)
  else
    QRLDtCadastro.Caption := '-';

  if not QryRelatorio.FieldByName('DT_EMIS_NF').IsNull then
    QRLDtEmisNotFis.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_EMIS_NF').AsDateTime)
  else
    QRLDtEmisNotFis.Caption := '-';

  if not QryRelatorio.FieldByName('DT_FIM_GARANT').IsNull then
    QRLDtFimGar.Caption := FormatDateTime('dd/MM/yyyy',QryRelatorio.FieldByName('DT_FIM_GARANT').AsDateTime)
  else
    QRLDtFimGar.Caption := '-';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelFicTecItemPatrimonio]);

end.

