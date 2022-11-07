unit U_FrmRelFicInfoNucGestao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

const
  OffsetMemoryStream : Int64 = 0;

type
  TFrmRelFicInfoNucGestao = class(TFrmRelTemplate)
    DetailBand1: TQRBand;
    QRDBText7: TQRDBText;
    QRLabel8: TQRLabel;
    qryOcorrNuc: TADOQuery;
    GroupHeaderBand1: TQRBand;
    GroupHeaderBand2: TQRBand;
    QRSubDetailQtde: TQRSubDetail;
    qryEquipNuc: TADOQuery;
    QRSubDetailGestores: TQRSubDetail;
    GroupFooterBand1: TQRBand;
    QRBand1: TQRBand;
    QRSubDetailFuncao: TQRSubDetail;
    GroupFooterBand2: TQRBand;
    GroupFooterBand3: TQRBand;
    qryUnidNucleo: TADOQuery;
    QRLabel2: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel45: TQRLabel;
    QRShape4: TQRShape;
    QRLabel71: TQRLabel;
    QRShape7: TQRShape;
    QRLMat1: TQRLabel;
    QRLMat2: TQRLabel;
    QRLMat3: TQRLabel;
    QRLDtOcorr: TQRLabel;
    QRDBText9: TQRDBText;
    QRLTpOcorr: TQRLabel;
    QrlOcorr: TQRLabel;
    QRLabel21: TQRLabel;
    QRShape2: TQRShape;
    QRLabel30: TQRLabel;
    QRLabel29: TQRLabel;
    QRShape10: TQRShape;
    QRLabel6: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel12: TQRLabel;
    QRLMatriculaGestor: TQRLabel;
    QRDBText5: TQRDBText;
    QRDBText6: TQRDBText;
    QRDBText11: TQRDBText;
    QRLStatusCol: TQRLabel;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRLabel9: TQRLabel;
    QRShape3: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel14: TQRLabel;
    QRDBText8: TQRDBText;
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    procedure QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailFuncaoBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailQtdeBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure GroupHeaderBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure GroupHeaderBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelFicInfoNucGestao: TFrmRelFicInfoNucGestao;

implementation

{$R *.dfm}

uses U_DataModuleSGE, MaskUtils, JPEG, DateUtils;

procedure TFrmRelFicInfoNucGestao.QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if QRSubDetailGestores.Color = clWhite then
    QRSubDetailGestores.Color := $00EBEBEB
  else
    QRSubDetailGestores.Color := clWhite;
end;

procedure TFrmRelFicInfoNucGestao.QRSubDetailFuncaoBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  // ZEBRADO
  if QRSubDetailFuncao.Color = clWhite then
    QRSubDetailFuncao.Color := $00EBEBEB
  else
    QRSubDetailFuncao.Color := clWhite;
    
  if not qryOcorrNuc.IsEmpty then
  begin
    if not qryOcorrNuc.FieldByName('DT_OCOR_NUCL').IsNull then
      QRLDtOcorr.Caption := FormatDateTime('dd/MM/yy',qryOcorrNuc.FieldByName('DT_OCOR_NUCL').AsDateTime)
    else
      QRLDtOcorr.Caption := '-';

    if qryOcorrNuc.FieldByName('TP_OCOR_NUCL').AsString = 'A' then
      QRLTpOcorr.Caption := 'Ação'
    else
      QRLTpOcorr.Caption := 'Ocorrência';

    if not qryOcorrNuc.FieldByName('DE_RE_OCOR_NUCL').IsNull then
      QrlOcorr.Caption := qryOcorrNuc.FieldByName('DE_RE_OCOR_NUCL').AsString
    else
      QrlOcorr.Caption := '-';
  end
  else
  begin
    QRLDtOcorr.Caption := '-';
    QRLTpOcorr.Caption := '-';
    QrlOcorr.Caption := '-';
  end;
end;

procedure TFrmRelFicInfoNucGestao.QRSubDetailQtdeBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QRSubDetailQtde.Color = clWhite then
    QRSubDetailQtde.Color := $00EBEBEB
  else
    QRSubDetailQtde.Color := clWhite;

  if not qryEquipNuc.IsEmpty then
  begin
    if not qryEquipNuc.FieldByName('CO_MAT_COL').IsNull then
      QRLMatriculaGestor.Caption := FormatMaskText('00.000-0;0', qryEquipNuc.FieldByName('CO_MAT_COL').AsString)
    else
      QRLMatriculaGestor.Caption := '-';

    if qryEquipNuc.FieldByName('CO_STATUS').AsString = 'A' then
      QRLStatusCol.Caption := 'Ativo'
    else
      QRLStatusCol.Caption := 'Inativo';
  end
  else
  begin
    QRLMatriculaGestor.Caption := '-';
    QRLStatusCol.Caption := '-';
  end;
end;

procedure TFrmRelFicInfoNucGestao.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with qryOcorrNuc do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  'Select C.*, e.sigla from TB272_OCOR_NUCL_GESTAO C ' +
                 ' join tb25_empresa e on e.co_emp = C.co_emp ' +
                 'where C.CO_NUCLEO = ' + QryRelatorio.FieldByName('CO_NUCLEO').AsString +
                 ' order by C.DT_OCOR_NUCL';
    Open;
  end;
end;

procedure TFrmRelFicInfoNucGestao.GroupHeaderBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryEquipNuc do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select C.NO_COL, C.CO_MAT_COL, f.NO_FUN, eni.CO_STATUS, e.sigla from TB_EQUIPE_NUCLEO_INST eni ' +
                'join tb03_colabor c on c.co_col = eni.co_col and c.co_emp = eni.co_emp_col ' +
                'join tb15_funcao f on f.co_fun = eni.co_fun ' +
                'join tb25_empresa e on e.co_emp = eni.co_emp_col ' +
                'where eni.CO_NUCLEO = ' + QryRelatorio.FieldByName('CO_NUCLEO').AsString +
                ' order by C.NO_COL';
    Open;
  end;
end;

procedure TFrmRelFicInfoNucGestao.GroupHeaderBand2AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  with qryUnidNucleo do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=  ' SELECT TE.NO_TIPOEMP, BAI.NO_BAIRRO, ' +
                 ' E.NO_FANTAS_EMP, E.SIGLA, NUC.CO_NUCLEO, NUC.NO_SIGLA_NUCLEO, NUC.DE_NUCLEO '+
                 ' FROM TB25_EMPRESA E '+
                 ' 	JOIN TB24_TPEMPRESA TE ON TE.CO_TIPOEMP = E.CO_TIPOEMP '+
                 ' 	JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO_BAIRRO '+
                 '  JOIN TB_NUCLEO_INST NUC ON NUC.CO_NUCLEO = E.CO_NUCLEO '+
                 ' WHERE NUC.CO_NUCLEO = ' + QryRelatorio.FieldByName('CO_NUCLEO').AsString +
                 ' order by E.NO_FANTAS_EMP';
    Open;
  end;
end;

end.
