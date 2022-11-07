unit U_FrmRelAvaliacao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelAvaliacao = class(TFrmRelTemplate)
    QryQuestaoTit: TADOQuery;
    QRGroup2: TQRGroup;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRDBText1: TQRDBText;
    qrdbCurso: TQRDBText;
    qrdbDisciplina: TQRDBText;
    QRLabel7: TQRLabel;
    qrdbProfessor: TQRDBText;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRDBText3: TQRDBText;
    QRBand2: TQRBand;
    QRLabel14: TQRLabel;
    QRDBText9: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText10: TQRDBText;
    QRSubDetail1: TQRSubDetail;
    QryRelatorioCO_PESQ_AVAL: TIntegerField;
    QryRelatorioCO_TIPO_AVAL: TIntegerField;
    QryRelatorioDT_AVAL: TDateTimeField;
    QryRelatorioCO_CUR: TIntegerField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioCO_MAT: TIntegerField;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioDE_SUGE_AVAL: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioNO_TUR: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioNO_TIPO_AVAL: TStringField;
    QRSubDetail2: TQRSubDetail;
    QRDBText4: TQRDBText;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QrlConceito: TQRLabel;
    QRDBText2: TQRDBText;
    QRLabel8: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape6: TQRShape;
    QRShape9: TQRShape;
    QryTitulo: TADOQuery;
    QryTituloCO_TIPO_AVAL: TIntegerField;
    QryTituloCO_TITU_AVAL: TIntegerField;
    QryTituloNO_TITU_AVAL: TStringField;
    QryRelatorioDE_OBJE_AVAL: TStringField;
    QryRelatorioDE_OBSE_AVAL: TStringField;
    QRDBText8: TQRDBText;
    QrlNota: TQRLabel;
    QryQuestaoTitCO_TIPO_AVAL: TIntegerField;
    QryQuestaoTitCO_TITU_AVAL: TIntegerField;
    QryQuestaoTitNU_QUES_AVAL: TIntegerField;
    QryQuestaoTitDE_QUES_AVAL: TStringField;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QryRelatorioCO_EMP: TIntegerField;
    QryRelatorioNO_MATERIA: TStringField;
    SummaryBand1: TQRBand;
    QRLabel4: TQRLabel;
    QRShape4: TQRShape;
    QRLabel2: TQRLabel;
    QRShape5: TQRShape;
    QRLabel3: TQRLabel;
    QRShape10: TQRShape;
    QRLabel5: TQRLabel;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QryRelatorioDE_MODU_CUR: TStringField;
    procedure QRSubDetail1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetail2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa: String;
  end;

var
  FrmRelAvaliacao: TFrmRelAvaliacao;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelAvaliacao.QRSubDetail1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
{  if (QryRelatorio.FieldByName('CO_GRAU_AVAL').AsString = 'D') then
  begin
    QRImageDef.Enabled := True;
    QRImageReg.Enabled := False;
    QRImageBom.Enabled := False;
    QRImageMB.Enabled := False;
    QRImageExc.Enabled := False;
  end
  else
  if (QryRelatorio.FieldByName('CO_GRAU_AVAL').AsString = 'R') then
  begin
    QRImageDef.Enabled := False;
    QRImageReg.Enabled := True;
    QRImageBom.Enabled := False;
    QRImageMB.Enabled := False;
    QRImageExc.Enabled := False;
  end
  else
  if (QryRelatorio.FieldByName('CO_GRAU_AVAL').AsString = 'B') then
  begin
    QRImageDef.Enabled := False;
    QRImageReg.Enabled := False;
    QRImageBom.Enabled := True;
    QRImageMB.Enabled := False;
    QRImageExc.Enabled := False;
  end
  else
  if (QryRelatorio.FieldByName('CO_GRAU_AVAL').AsString = 'M') then
  begin
    QRImageDef.Enabled := False;
    QRImageReg.Enabled := False;
    QRImageBom.Enabled := False;
    QRImageMB.Enabled := True;
    QRImageExc.Enabled := False;
  end
  else
  if (QryRelatorio.FieldByName('CO_GRAU_AVAL').AsString = 'E') then
  begin
    QRImageDef.Enabled := False;
    QRImageReg.Enabled := False;
    QRImageBom.Enabled := False;
    QRImageMB.Enabled := False;
    QRImageExc.Enabled := True;
  end;
  }
end;

procedure TFrmRelAvaliacao.QRGroup2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  with QryTitulo do
  begin
    Close;
    Parameters.ParamByName('P_CO_TIPO_AVAL').Value := QryRelatorio.FieldByName('CO_TIPO_AVAL').Value;
    Open;
  end;

end;

procedure TFrmRelAvaliacao.QRSubDetail1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  { Recupera as quest�es do titulo }
  with QryQuestaoTit do
  begin
    Close;
    Parameters.ParamByName('P_CO_TIPO_AVAL').Value := QryRelatorio.FieldByName('CO_TIPO_AVAL').Value;
    Parameters.ParamByName('P_CO_TITU_AVAL').Value := QryTitulo.FieldByName('CO_TITU_AVAL').Value;
    Open;
  end;
end;

procedure TFrmRelAvaliacao.QRSubDetail2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  vvvNota: String;
begin
  inherited;
  { Recupera a nota da quest�o }
  if (not QryQuestaoTitNU_QUES_AVAL.IsNull) then
  begin
    //with DataModuleSGE.QrySql do
    with DM.QrySql do
    begin
      Close;
      Sql.Clear;
      Sql.Text := ' SELECT I.VL_NOT_AVAL FROM TB70_ITEM_AVAL I ' +
                  ' WHERE I.CO_PESQ_AVAL = ' + QryRelatorio.FieldByName('CO_PESQ_AVAL').AsString +
                  '   AND I.CO_TITU_AVAL = ' + QryTitulo.FieldByName('CO_TITU_AVAL').AsString +
                  '   AND I.CO_TIPO_AVAL = ' + QryRelatorio.FieldByName('CO_TIPO_AVAL').AsString +
                  '   AND I.NU_QUES_AVAL = ' + QryQuestaoTit.FieldByName('NU_QUES_AVAL').AsString +
                  '   AND I.CO_EMP = ' + codigoEmpresa;
      Open;
      QrlConceito.Caption := '';
      QrlNota.Caption := '';

      if not IsEmpty then
      begin
        if FieldByName('VL_NOT_AVAL').AsString <> ''  then
        begin
          vvvNota := FormatFloat('0.00', FieldByName('VL_NOT_AVAL').AsFloat);

            while Pos(',', vvvNota) > 0 do
                vvvNota[Pos(',', vvvNota)] := '.';

          QrlNota.Caption := FormatFloat('0.00', FieldByName('VL_NOT_AVAL').AsFloat); //vvvNota;

          QrlConceito.Caption := '-----';
        end;

      end;
      Close;
    end;
  end
  else
  begin
    QrlNota.Caption := '-';
    QrlConceito.Caption := '-----';
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAvaliacao]);

end.