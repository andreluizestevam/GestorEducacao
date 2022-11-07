unit U_FrmRelTemplate;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  jpeg, Qrctrls, QuickRpt, Db, ADODB, ExtCtrls, QRPrntr, StdCtrls, U_DataModuleSGE;

const
  OffsetMemoryStream : Int64 = 0;

type
    TFrmRelTemplate = class(TForm)
    //TFrmRelTemplate = class(TQuickRep)
    QuickRep1: TQuickRep;
    QryRelatorio: TADOQuery;
    QryCabecalhoRel: TADOQuery;
    PageHeaderBand1: TQRBand;
    LblTituloRel: TQRLabel;
    QRDBText14: TQRDBText;
    QRDBText15: TQRDBText;
    QRDBImage1: TQRDBImage;
    qrlTemplePag: TQRLabel;
    qrlTempleData: TQRLabel;
    qrlTempleHora: TQRLabel;
    QRSysData1: TQRSysData;
    QRSysData2: TQRSysData;
    QRSysData3: TQRSysData;
    qrlEnde: TQRLabel;
    qrlComplemento: TQRLabel;
    qrlTels: TQRLabel;
    QRBANDSGIE: TQRBand;
    QRLabelSGIE: TQRLabel;
    QRLabel1000: TQRLabel;
    Qrl_IdentificacaoRel: TQRLabel;
    QRILogoEscola: TQRImage;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
    procedure QRBANDSGIEBeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
    procedure FormDestroy(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelTemplate: TFrmRelTemplate;
  DM : TDataModuleSGE;

implementation

//uses U_DataModuleSGE;

{$R *.DFM}

procedure TFrmRelTemplate.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  //ShowMessage('1');
  Action := CaFree;
end;

procedure TFrmRelTemplate.FormCreate(Sender: TObject);
begin

  // Cria uma inst�ncia do DataModule.
  DM := TDataModuleSGE.Create(nil);
  //Comentar aqui quando tiver que criar dll apontando para diferentes clientes
  //DM.Conn.Open;

  // Monta a consulta do cabe�alho que � padr�opara todos os relat�rios.
  {QryCabecalhoRel.Close;
  QryCabecalhoRel.Connection := DM.Conn;
  QryCabecalhoRel.Sql.Text := 'Select top 1 E.*,i.ImageStream ' +
                              ' From TB25_EMPRESA E ' +
                              ' join image i on E.LOGO_IMAGE_ID = i.ImageId' +
                              ' where co_emp = 187';
   QryCabecalhoRel.Open;     }

end;

procedure TFrmRelTemplate.PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  Dia,Mes,Ano : word;
  Jpg : TJPEGImage;
  Bmp: TBitmap;
  MemoryStream : TMemoryStream;
begin

   {
  qrlEnde.Caption := QryCabecalhoRelDE_END_EMP.AsString + ' ' + QryCabecalhoRelDE_COM_ENDE_EMP.AsString +
                            ' - ' + QryCabecalhoRelNO_BAI_EMP.AsString;

  qrlComplemento.Caption := 'CEP: ' + QryCabecalhoRelCO_CEP_EMP.AsString + ' - ' + QryCabecalhoRelNO_CID_EMP.AsString +
                     ' - ' + QryCabecalhoRelCO_UF_EMP.AsString;

  qrlTels.Caption := 'Tels: ' + QryCabecalhoRelCO_TEL1_EMP.AsString + ' / ' + QryCabecalhoRelCO_TEL2_EMP.AsString + ' / ' + QryCabecalhoRelCO_FAX_EMP.AsString;
}

  if not QryCabecalhoRel.FieldByName('ImageStream').IsNull then
  begin
    Try
      try
        MemoryStream := TMemoryStream.Create;
        (QryCabecalhoRel.FieldByName('ImageStream') as TBlobField).SaveToStream(MemoryStream);
        MemoryStream.Position := OffsetMemoryStream;
        Jpg := TJpegImage.Create;
        Jpg.LoadFromStream(MemoryStream);
        QRILogoEscola.Picture.Assign(Jpg);
        QRILogoEscola.Enabled := True;
        QRILogoEscola.Left := 4;
      except
        QRILogoEscola.Enabled := False;
        QRDBImage1.Enabled := True;
      end;
      finally
          MemoryStream.Free;
          Jpg.Free;
          Bmp.Free;
      end;
  end;

  QRDBText14.Left := 108;
  QRDBText14.Top := 2;
  QRDBText14.Font.Size := 10;
  QRDBText14.AutoSize := true;
  if not QryCabecalhoRel.FieldByName('cablinha1').IsNull then
    QRDBText14.Caption := QryCabecalhoRel.FieldByName('cablinha1').Value
  else
    QRDBText14.Caption := '';

  QRDBText15.Left := 108;
  QRDBText15.Top := 18;
  QRDBText15.Font.Size := 10;
  QRDBText15.AutoSize := true;
  if not QryCabecalhoRel.FieldByName('cablinha2').IsNull then
    QRDBText15.Caption := QryCabecalhoRel.FieldByName('cablinha2').Value
  else
    QRDBText15.Caption := '';

  qrlEnde.Left := 108;
  qrlEnde.Top := 34;
  qrlEnde.Font.Size := 10;
  qrlEnde.AutoSize := true;
  if not QryCabecalhoRel.FieldByName('cablinha3').IsNull then
    qrlEnde.Caption := QryCabecalhoRel.FieldByName('cablinha3').Value
  else
    qrlEnde.Caption := '';

  qrlComplemento.Left := 108;
  qrlComplemento.Top := 50;
  qrlComplemento.Font.Size := 10;
  qrlComplemento.AutoSize := true;
  if not QryCabecalhoRel.FieldByName('cablinha4').IsNull then
    qrlComplemento.Caption := QryCabecalhoRel.FieldByName('cablinha4').Value
  else
    qrlComplemento.Caption := '';

  qrlTels.Left := 108;
  qrlTels.Top := 66;
  qrlTels.Font.Size := 10;
  qrlTels.Font.Style := [fsBold];
  qrlTels.AutoSize := true;
  qrlTels.Caption := AnsiUpperCase(QryCabecalhoRel.FieldByName('NO_FANTAS_EMP').Value);

  LblTituloRel.Caption := AnsiUpperCase(LblTituloRel.Caption);

  QRSysData1.Text := FormatFloat('000',QuickRep1.PageNumber);
  QRLAbel1000.Caption := FormatDateTime('dd/mm/yy',now);

  qrlTemplePag.Font.Size := 8;
  qrlTempleData.Font.Size := 8;
  qrlTempleHora.Font.Size := 8;

  qrlTemplePag.Left := PageHeaderBand1.Width - 78;
  qrlTempleData.Left := PageHeaderBand1.Width - 78;
  qrlTempleHora.Left := PageHeaderBand1.Width - 78;
  QRLabel1000.Left := PageHeaderband1.Width - QrLabel1000.Width;

  QRSysData1.Font.Size := 8;
  QRSysData2.Font.Size := 8;
  QRSysData3.Font.Size := 8;

  DecodeDate(now,ano,mes,dia);
{  lblAno.Caption := FormatFloat('00',ano);
  lblMes.Caption := FormatFloat('00',mes);
  lblDia.Caption := FormatFloat('00',dia);}

end;

procedure TFrmRelTemplate.QRBANDSGIEBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  qrlabelsgie.Font.Size := 6;
  Qrl_IdentificacaoRel.Font.Size := 6;
end;

procedure TFrmRelTemplate.FormDestroy(Sender: TObject);
begin
  DM.Conn.Close;
end;

end.
