unit U_FrmRelFicPerfilInst;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

const
  OffsetMemoryStream : Int64 = 0;

type
  TFrmRelFicPerfilInst = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRDBText1: TQRDBText;
    QRDBText2: TQRDBText;
    QRLabel3: TQRLabel;
    QRDBText3: TQRDBText;
    QRDBText7: TQRDBText;
    QRLabel8: TQRLabel;
    QRDBText8: TQRDBText;
    QRLabel9: TQRLabel;
    QRDBText9: TQRDBText;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRDBText12: TQRDBText;
    QRLabel14: TQRLabel;
    QRDBText13: TQRDBText;
    QRLabel16: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRDBText26: TQRDBText;
    QRLabel27: TQRLabel;
    QRDBText27: TQRDBText;
    QryPerfilEmpresa: TADOQuery;
    QRLabel62: TQRLabel;
    QRLabel63: TQRLabel;
    QRLNuDtAta: TQRLabel;
    QRLabel5: TQRLabel;
    QRIEscola: TQRImage;
    QRDBIEscola: TQRDBImage;
    QryServico: TADOQuery;
    GroupHeaderBand2: TQRBand;
    QRShape2: TQRShape;
    QRLabel21: TQRLabel;
    QRShape5: TQRShape;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel4: TQRLabel;
    QRSubDetailGestores: TQRSubDetail;
    QRDBText5: TQRDBText;
    QRDBText11: TQRDBText;
    QRLMatriculaGestor: TQRLabel;
    QRLTelGestores: TQRLabel;
    QRLDtInicio: TQRLabel;
    QRDBText6: TQRDBText;
    QRLIdade: TQRLabel;
    QRLabel13: TQRLabel;
    qryGestores: TADOQuery;
    QRSubDetailPerfil: TQRSubDetail;
    GroupHeaderBand1: TQRBand;
    QRLabel45: TQRLabel;
    QRShape4: TQRShape;
    GroupFooterBand1: TQRBand;
    QRLabel66: TQRLabel;
    QRShape7: TQRShape;
    QRDBText49: TQRDBText;
    QRLabel67: TQRLabel;
    QRDBText50: TQRDBText;
    QRLabel68: TQRLabel;
    QRDBText56: TQRDBText;
    QRDBText60: TQRDBText;
    QRLabel17: TQRLabel;
    QRDBText59: TQRDBText;
    QRLabel12: TQRLabel;
    QRShape16: TQRShape;
    QRDBText57: TQRDBText;
    QRLabel6: TQRLabel;
    QRLabel77: TQRLabel;
    QRDBText72: TQRDBText;
    QRLabel78: TQRLabel;
    QRDBText73: TQRDBText;
    QRLabel79: TQRLabel;
    QRDBText74: TQRDBText;
    QRLabel89: TQRLabel;
    QRDBText84: TQRDBText;
    QRDBText79: TQRDBText;
    QRLabel84: TQRLabel;
    QRDBText78: TQRDBText;
    QRLabel83: TQRLabel;
    QRDBText80: TQRDBText;
    QRLabel86: TQRLabel;
    QRDBText81: TQRDBText;
    QRLabel85: TQRLabel;
    QRLabel87: TQRLabel;
    QRDBText82: TQRDBText;
    QRLabel75: TQRLabel;
    QRDBText70: TQRDBText;
    QRLabel88: TQRLabel;
    QRDBText83: TQRDBText;
    QRLabel103: TQRLabel;
    QRDBText98: TQRDBText;
    QRDBText71: TQRDBText;
    QRLabel76: TQRLabel;
    QRLabel32: TQRLabel;
    QRShape8: TQRShape;
    QRLabel69: TQRLabel;
    QRDBText64: TQRDBText;
    QRLabel70: TQRLabel;
    QRDBText65: TQRDBText;
    QRLabel71: TQRLabel;
    QRDBText66: TQRDBText;
    QRLabel73: TQRLabel;
    QRDBText68: TQRDBText;
    QRLabel74: TQRLabel;
    QRDBText69: TQRDBText;
    QRLabel72: TQRLabel;
    QRDBText67: TQRDBText;
    QRLabel96: TQRLabel;
    QRShape15: TQRShape;
    QRLabel95: TQRLabel;
    QRLabel94: TQRLabel;
    QRLabel93: TQRLabel;
    QRLabel92: TQRLabel;
    QRLabel91: TQRLabel;
    QRLabel90: TQRLabel;
    QRDBText75: TQRDBText;
    QRDBText85: TQRDBText;
    QRDBText86: TQRDBText;
    QRDBText76: TQRDBText;
    QRDBText77: TQRDBText;
    QRDBText87: TQRDBText;
    QRDBText88: TQRDBText;
    QRLabel102: TQRLabel;
    QRShape14: TQRShape;
    QRLabel101: TQRLabel;
    QRLabel97: TQRLabel;
    QRLabel100: TQRLabel;
    QRLabel99: TQRLabel;
    QRLabel98: TQRLabel;
    QRLabel82: TQRLabel;
    QRLabel81: TQRLabel;
    QRLabel80: TQRLabel;
    QRDBText95: TQRDBText;
    QRDBText94: TQRDBText;
    QRDBText93: TQRDBText;
    QRDBText96: TQRDBText;
    QRDBText97: TQRDBText;
    QRDBText92: TQRDBText;
    QRDBText91: TQRDBText;
    QRDBText89: TQRDBText;
    QRDBText90: TQRDBText;
    qryEdificacoes: TADOQuery;
    QRLabel36: TQRLabel;
    QRShape3: TQRShape;
    QRLabel48: TQRLabel;
    QRLabel42: TQRLabel;
    QRDBText44: TQRDBText;
    QRDBText45: TQRDBText;
    QRShape9: TQRShape;
    QRLabel41: TQRLabel;
    QRLabel40: TQRLabel;
    QRLabel33: TQRLabel;
    QRDBText42: TQRDBText;
    QRDBText31: TQRDBText;
    QRDBText30: TQRDBText;
    QRLabel49: TQRLabel;
    QRShape10: TQRShape;
    QRLabel38: TQRLabel;
    QRLabel50: TQRLabel;
    QRLabel53: TQRLabel;
    QRLabel52: TQRLabel;
    QRLabel55: TQRLabel;
    QRDBText55: TQRDBText;
    QRLLongitude: TQRLabel;
    QRLLatitude: TQRLabel;
    QRDBText34: TQRDBText;
    QRDBText32: TQRDBText;
    QRShape11: TQRShape;
    QRLabel35: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel51: TQRLabel;
    QRLabel46: TQRLabel;
    QRDBText51: TQRDBText;
    QRDBText52: TQRDBText;
    QRDBText53: TQRDBText;
    QRDBText54: TQRDBText;
    QRLabel54: TQRLabel;
    QRShape12: TQRShape;
    QRLabel34: TQRLabel;
    QRDBText29: TQRDBText;
    QRLabel43: TQRLabel;
    QRDBText38: TQRDBText;
    QRDBText35: TQRDBText;
    QRLabel56: TQRLabel;
    QRLabel57: TQRLabel;
    QRDBText33: TQRDBText;
    QRDBText36: TQRDBText;
    QRLabel58: TQRLabel;
    QRShape13: TQRShape;
    QRLabel60: TQRLabel;
    QRLabel59: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel37: TQRLabel;
    QRDBText37: TQRDBText;
    QRDBText39: TQRDBText;
    QRDBText40: TQRDBText;
    QRDBText43: TQRDBText;
    QRLabel65: TQRLabel;
    QRDBText48: TQRDBText;
    QRLabel64: TQRLabel;
    QRDBText47: TQRDBText;
    QRLabel61: TQRLabel;
    QRDBText46: TQRDBText;
    QRLTpUnidade: TQRLabel;
    QRLEndereco: TQRLabel;
    QRLCEPEmp: TQRLabel;
    QRLTel1Emp: TQRLabel;
    QRLFaxEmp: TQRLabel;
    QRLCNPJEmp: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupHeaderBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupHeaderBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelFicPerfilInst: TFrmRelFicPerfilInst;

implementation

{$R *.dfm}

uses U_DataModuleSGE, MaskUtils, JPEG, DateUtils;

procedure TFrmRelFicPerfilInst.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  SQLString : String;
  Jpg : TJPEGImage;
  Bmp: TBitmap;
  MemoryStream : TMemoryStream;
begin
  inherited;

  if ( not QryRelatorio.FieldByName('CO_CPFCGC_EMP').IsNull) and (QryRelatorio.FieldByName('CO_CEP_EMP').AsString <> '') then
    QRLCNPJEmp.Caption := FormatMaskText('99.999.999/9999-99;0',QryRelatorio.FieldByName('CO_CPFCGC_EMP').AsString)
  else
    QRLCNPJEmp.Caption := '-';

  if ( not QryRelatorio.FieldByName('CO_CEP_EMP').IsNull) and (QryRelatorio.FieldByName('CO_CEP_EMP').AsString <> '') then
    QRLCEPEmp.Caption := FormatMaskText('99999-999;0',QryRelatorio.FieldByName('CO_CEP_EMP').AsString)
  else
    QRLCEPEmp.Caption := '-';

  if ( not QryRelatorio.FieldByName('CO_TEL1_EMP').IsNull) and (QryRelatorio.FieldByName('CO_TEL1_EMP').AsString <> '') then
    QRLTel1Emp.Caption := FormatMaskText('(99) 9999-9999;0',QryRelatorio.FieldByName('CO_TEL1_EMP').AsString)
  else
    QRLTel1Emp.Caption := '-';

  if ( not QryRelatorio.FieldByName('CO_FAX_EMP').IsNull) and (QryRelatorio.FieldByName('CO_FAX_EMP').AsString <> '') then
    QRLFaxEmp.Caption := FormatMaskText('(99) 9999-9999;0',QryRelatorio.FieldByName('CO_FAX_EMP').AsString)
  else
    QRLFaxEmp.Caption := '-';
  
  QRLEndereco.Caption := QryRelatorio.FieldByName('DE_END_EMP').AsString + ' - Cidade: ' + QryRelatorio.FieldByName('NO_CIDADE').AsString +
  ' - Bairro: ' + QryRelatorio.fieldByName('NO_BAIRRO').AsString;

  if not QryRelatorio.FieldByName('fotoEmpresa').IsNull then
  begin
    Try
      try
        MemoryStream := TMemoryStream.Create;
        (QryRelatorio.FieldByName('fotoEmpresa') as TBlobField).SaveToStream(MemoryStream);
        MemoryStream.Position := OffsetMemoryStream;
        Jpg := TJpegImage.Create;
        Jpg.LoadFromStream(MemoryStream);
        QRIEscola.Picture.Assign(Jpg);
        QRIEscola.Enabled := True;
        QRIEscola.Top := 160;
      except
        QRIEscola.Enabled := False;
        QRDBIEscola.Enabled := True;
      end;
      finally
          MemoryStream.Free;
          Jpg.Free;
          Bmp.Free;
      end;
  end
  else
  begin
    QRIEscola.Enabled := False;
    QRDBIEscola.Enabled := False;
  end;

  if QryRelatorio.FieldByName('NU_ATA').IsNull then
    QRLNuDtAta.Caption := ' - '
  else
  begin
    QRLNuDtAta.Caption := QryRelatorio.FieldByName('NU_ATA').AsString;
    if not QryRelatorio.FieldByName('DT_ATA').IsNull then
      QRLNuDtAta.Caption := QRLNuDtAta.Caption + ' - ' + QryRelatorio.FieldByName('DT_ATA').AsString;
  end;

  QryPerfilEmpresa.Close;
  with QryPerfilEmpresa do
  begin
    Sql.Clear;
    SqlString := ' SET LANGUAGE PORTUGUESE '+
                ' SELECT PU.*,'+
                ' BIBLIOTECA = (CASE PU.CO_FLAG_BIBLI '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' ENE_ELETRICA = (CASE PU.CO_FLAG_ENERG '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' CEN_ESPORTIVO = (CASE PU.CO_FLAG_ESPOR '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' ESTACIONAMENTO = (CASE PU.CO_FLAG_ESTAC '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                'tpuni.DE_TIPO_UNIDA,tpocu.DE_TIPO_OCUPA,tpter.DE_TIPO_TERRE,tpdelter.DE_TIPO_DELIM_TERRE'+
                ' FROM TB160_PERFIL_UNIDADE PU '+
                ' LEFT JOIN TB182_TIPO_UNIDA tpuni ON tpuni.CO_SIGLA_TIPO_UNIDA = PU.CO_TIPO_UNIDA ' +
                ' LEFT JOIN TB176_TIPO_OCUPA tpocu ON tpocu.CO_SIGLA_TIPO_OCUPA = PU.CO_TIPO_OCUPA ' +
                ' LEFT JOIN TB177_TIPO_TERRE tpter ON tpter.CO_SIGLA_TIPO_TERRE = PU.CO_TERRE_TIPO ' +
                ' LEFT JOIN TB178_TIPO_DELIM_TERRE tpdelter ON tpdelter.CO_SIGLA_TIPO_DELIM_TERRE = PU.CO_TERRE_TIPO_DELIM ' +
                ' WHERE PU.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    SQL.Text := SqlString;
  end;

 QryPerfilEmpresa.Open;

 if not QryPerfilEmpresa.IsEmpty then
 begin

    QRLTpUnidade.Caption := QryPerfilEmpresa.FieldByName('DE_TIPO_UNIDA').AsString;

    if (not QryPerfilEmpresa.FieldByName('DE_TERRE_LATITU_NUMER').IsNull) and (not QryPerfilEmpresa.FieldByName('DE_TERRE_LATITU_SIGLA').IsNull)then
      QRLLatitude.Caption := QryPerfilEmpresa.FieldByName('DE_TERRE_LATITU_NUMER').AsString + ' / ' + QryPerfilEmpresa.FieldByName('DE_TERRE_LATITU_SIGLA').AsString
    else
      QRLLatitude.Caption := '-';

    if (not QryPerfilEmpresa.FieldByName('DE_TERRE_LONGI_NUMER').IsNull) and (not QryPerfilEmpresa.FieldByName('DE_TERRE_LONGI_SIGLA').IsNull)then
      QRLLongitude.Caption := QryPerfilEmpresa.FieldByName('DE_TERRE_LONGI_NUMER').AsString + ' / ' + QryPerfilEmpresa.FieldByName('DE_TERRE_LONGI_SIGLA').AsString
    else
      QRLLongitude.Caption := '-';
 end
 else
 begin
  QRLLatitude.Caption := '';
  QRLLongitude.Caption := '';
  QRLTpUnidade.Caption := '-';
 end;

 QryServico.Close;
  with QryServico do
  begin
    Sql.Clear;
    SqlString := ' SET LANGUAGE PORTUGUESE ' +
                ' SELECT SE.*,' +
                ' ATIVI_COMUN = (CASE SE.CO_SERVI_ATIVI_COMUN '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' COLET_LIXO = (CASE SE.CO_SERVI_COLET_LIXO '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_CLINI = (CASE SE.CO_SERVI_CLINI '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_ODONT = (CASE SE.CO_SERVI_ODONT '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_TRANSP_ESCOL = (CASE SE.CO_SERVI_TRANSP_ESCOL '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_TRANSP_FUNCI = (CASE SE.CO_SERVI_TRANSP_FUNCI '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_MEREN = (CASE SE.CO_SERVI_MEREN '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_VIGIA = (CASE SE.CO_SERVI_VIGIA '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END), '+
                ' SERVI_ARMAR = (CASE SE.CO_SERVI_ARMAR '+
                   ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                   ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                   ' 			END) '+
                ' FROM TB172_SERVICO SE ' +
                ' WHERE SE.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    SQL.Text := SqlString;
  end;
 QryServico.Open;

end;

procedure TFrmRelFicPerfilInst.GroupHeaderBand2AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryGestores do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select C.NO_COL, C.CO_MAT_COL, C.CO_SEXO_COL, C.DT_NASC_COL, f.NO_FUN, gu.DT_INICIO_ATIVID, c.NU_TELE_CELU_COL from TB59_GESTOR_UNIDAD gu ' +
                'join tb03_colabor c on c.co_col = gu.co_col and c.co_emp = gu.co_emp ' +
                'join tb15_funcao f on f.co_fun = gu.co_fun ' +
                'where gu.CO_EMP_GESTAO = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' and gu.co_situ_gest = ' + QuotedStr('A') +
                ' order by C.NO_COL';
    Open;
  end;
end;

procedure TFrmRelFicPerfilInst.QRSubDetailGestoresBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;
  if QRSubDetailGestores.Color = clWhite then
    QRSubDetailGestores.Color := $00EBEBEB
  else
    QRSubDetailGestores.Color := clWhite;

  // matricula do diretor
  if not qryGestores.FieldByName('CO_MAT_COL').IsNull then
    QRLMatriculaGestor.Caption := FormatMaskText('00.000-0;0', qryGestores.FieldByName('CO_MAT_COL').AsString)
  else
    QRLMatriculaGestor.Caption := '-';

  if (not qryGestores.FieldByName('NU_TELE_CELU_COL').IsNull) and (qryGestores.FieldByName('NU_TELE_CELU_COL').AsString <> '') then
    QRLTelGestores.Caption := FormatMaskText('(99) 9999-9999;0',qryGestores.FieldByName('NU_TELE_CELU_COL').AsString)
  else
    QRLTelGestores.Caption := '-';

  if (not qryGestores.FieldByName('DT_INICIO_ATIVID').IsNull) then
    QRLDtInicio.Caption := FormatDateTime('dd/MM/yy',qryGestores.FieldByName('DT_INICIO_ATIVID').AsDateTime)
  else
    QRLDtInicio.Caption := '-';

  diasnoano := 365.6;

  if (not qryGestores.FieldByName('DT_NASC_COL').IsNull) then
    QRLIdade.Caption := IntToStr(Trunc(DaysBetween(now,qryGestores.fieldbyname('DT_NASC_COL').AsDateTime) / diasnoano))
  else
    QRLIdade.Caption := '-';
end;

procedure TFrmRelFicPerfilInst.GroupHeaderBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;

  with qryEdificacoes do
  begin
    Close;
    SQL.Clear;
    SQL.Text := ' SELECT SIS_AQUEC = (CASE edf.CO_ENERG_FLAG_AQUEC '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' EDF_COMPAR = (CASE edf.CO_EDIFI_FLAG_COMPARTILHADO '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' SAI_EMERG = (CASE edf.CO_EDIFI_FLAG_SAIDA_EMERG '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' ACE_DEFIC = (CASE edf.CO_EDIFI_FLAG_ACESS_DEFIC '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' SIS_SEGUR = (CASE edf.CO_EDIFI_FLAG_SISTE_SEGUR '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' ALA_INCEND = (CASE edf.CO_EDIFI_FLAG_SISTE_ANTI_INCEN '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' SIS_VENTI = (CASE edf.CO_EDIFI_FLAG_SISTE_VENTI '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' POS_GERAD = (CASE edf.CO_ENERG_FLAG_GERADOR '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' ATE_ELETR = (CASE edf.CO_ENERG_FLAG_ELETR_ATERR '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' LUZ_EMERG = (CASE edf.CO_ENERG_FLAG_LUZ_EMERG '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' RES_AGUA = (CASE edf.CO_SANEA_FLAG_RESERV_AGUA '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' RED_ESGOT = (CASE edf.CO_SANEA_FLAG_REDE_ESGOT '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' CAI_RESID = (CASE edf.CO_SANEA_FLAG_CAIXA_RESIDUO '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   ' CAI_GORDU = (CASE edf.CO_SANEA_FLAG_CAIXA_GORDURA '+
                     ' 			WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                     ' 			WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                     ' 			END), '+
                   'edf.*,tpedf.DE_TIPO_EDIFI,tpeco.DE_TIPO_ESTAD_CONSERV,tpcob.DE_TIPO_COBER,tpene.DE_FONTE_ENERG,geene.DE_GERAD_ENERG,' +
                   'sisaq.DE_SISTE_AQUEC,oriag.DE_ORIGE_AGUA, redesg.DE_REDE_ESGOT' +
                   ' FROM TB25_EMPRESA E '+
                   ' left join TB166_EDIFICACAO edf on edf.CO_EMP = E.CO_EMP ' +
                   ' left join TB179_TIPO_EDIFI tpedf on tpedf.CO_SIGLA_TIPO_EDIFI = edf.CO_TIPO_EDIFI ' +
                   ' left join TB180_TIPO_ESTAD_CONSERV tpeco on tpeco.CO_SIGLA_TIPO_ESTAD_CONSERV = edf.CO_EDIFI_TIPO_ESTAD_CONSERV ' +
                   ' left join TB181_TIPO_COBER tpcob on tpcob.CO_SIGLA_TIPO_COBER = edf.CO_EDIFI_TIPO_COBERT ' +
                   ' left join TB184_FONTE_ENERG tpene on tpene.CO_SIGLA_FONTE_ENERG = edf.CO_ENERG_TIPO_FONTE ' +
                   ' left join TB185_GERAD_ENERG geene on geene.CO_SIGLA_GERAD_ENERG = edf.CO_ENERG_TIPO_GERADOR ' +
                   ' left join TB186_SISTE_AQUEC sisaq on sisaq.CO_SIGLA_SISTE_AQUEC = edf.CO_ENERG_TIPO_AQUEC ' +
                   ' left join TB183_ORIGE_AGUA oriag on oriag.CO_SIGLA_ORIGE_AGUA = edf.CO_SANEA_TIPO_ORIGEM_AGUA ' +
                   ' left join TB187_REDE_ESGOT redesg on redesg.CO_SIGLA_REDE_ESGOT = edf.CO_SANEA_TIPO_REDE_ESGOT ' +
                   ' WHERE E.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    Open;
  end;
end;

end.
