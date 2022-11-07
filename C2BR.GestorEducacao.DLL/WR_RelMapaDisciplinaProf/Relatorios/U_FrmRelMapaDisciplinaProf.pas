unit U_FrmRelMapaDisciplinaProf;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaDisciplinaProf = class(TFrmRelTemplate)
    QRLParametros: TQRLabel;
    QRBand1: TQRBand;
    QRLabel5: TQRLabel;
    QRLPage: TQRLabel;
    QRGroup1: TQRGroup;
    QRLTitMatricula: TQRLabel;
    QRShape3: TQRShape;
    QRLTitProfessor: TQRLabel;
    QrlTitCurso: TQRLabel;
    QRLTitMateria: TQRLabel;
    QRLabel8: TQRLabel;
    QRLColCurso: TQRLabel;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioNO_MATERIA: TStringField;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioCO_ESPEC: TIntegerField;
    QryRelatorioCO_CURFORM: TIntegerField;
    QryRelatorioNO_TPCON: TStringField;
    QryRelatorioNU_TELE_RESI_COL: TStringField;
    QryRelatorioNU_TELE_CELU_COL: TStringField;
    QRDBTMatricula: TQRDBText;
    QRLCurFor: TQRLabel;
    QRLEspec: TQRLabel;
    QRBand2: TQRBand;
    QRLabel11: TQRLabel;
    QrlTotalProf: TQRLabel;
    qryTotalProf: TADOQuery;
    qryTotalProfTotal: TIntegerField;
    QryRelatorioNO_TURMA: TStringField;
    QRLModSerTur: TQRLabel;
    QryRelatorioCO_COL: TIntegerField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QRDBText5: TQRDBText;
    QRDBText4: TQRDBText;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRDBTMateria: TQRDBText;
    QRLabel6: TQRLabel;
    QRLDtIniDtFim: TQRLabel;
    QryRelatorioSITUACAO: TStringField;
    QRLNoCol: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa: string;
  end;

var
  FrmRelMapaDisciplinaProf: TFrmRelMapaDisciplinaProf;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaDisciplinaProf.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_COL.IsNull then
    QRLNoCol.Caption := UpperCase(QryRelatorioNO_COL.AsString)
  else
    QRLNoCol.Caption := '-';
    
  QRLCurFor.Caption := '-';
  QRLEspec.Caption := '-';
  
  QRLModSerTur.Caption := QryRelatorioDE_MODU_CUR.AsString + ' / ' + QryRelatorioNO_CUR.AsString + ' / ' +
  QryRelatorioNO_TURMA.AsString;

  //Pegar 1ªAtiv
  with DM.QrySql do
  begin
    CLose;
    SQL.Clear;
    SQL.Text := 'select DT_PREV_PLA from TB17_PLANO_AULA '+
                'where co_col = ' + QryRelatorioCO_COL.AsString +
                ' and co_emp = ' + codigoEmpresa +
                ' order by DT_PREV_PLA';
    Open;

    First;
    if not IsEmpty then
    begin
      QRLDtIniDtFim.Caption := FormatDateTime('dd/MM/yy',FieldByName('DT_PREV_PLA').AsDateTime);

    //ShowMessage(IntToStr(Fields.Count));

    //if RecordCount > 1 then
    //begin
      Last;
      //if not IsEmpty then
      QRLDtIniDtFim.Caption := QRLDtIniDtFim.Caption + ' - ' + FormatDateTime('dd/MM/yy',FieldByName('DT_PREV_PLA').AsDateTime);
    //end
    //else
     // QRLDtIniDtFim.Caption := QRLDtIniDtFim.Caption + ' - *';
    end;

  end;
 { with DataModuleSGE.QrySql do
  begin
    CLose;
    SQL.Clear;
    SQL.Text := 'select de_espec from tb100_especializacao '+
                'where co_espec = ' + QryRelatorioCO_CURFORM.AsString;
    Open;

    if not IsEmpty then
      QRLCurFor.Caption := FieldByName('de_espec').AsString;
  end;      }

  if not QryRelatorioCO_ESPEC.IsNull then
  begin
    with DM.QrySql do
    begin
      CLose;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioCO_ESPEC.AsString;
      Open;

      if not IsEmpty then
        QRLEspec.Caption := FieldByName('de_espec').AsString;
    end;
  end;

  if not QryRelatorioCO_CURFORM.IsNull then
  begin
    with DM.QrySql do
    begin
      CLose;
      SQL.Clear;
      SQL.Text := 'select de_espec from tb100_especializacao '+
                  'where co_espec = ' + QryRelatorioCO_CURFORM.AsString;
      Open;

      if not IsEmpty then
        QRLCurFor.Caption := FieldByName('de_espec').AsString;
    end;
  end;

  If QRBand1.Color = ClWhite then
    QRBand1.Color := $00D8D8D8
  Else
    QRBand1.Color := ClWhite;

  //QRLTelefones.Caption := FormatMaskText('(##) ####-####;0',QryRelatorioNU_TELE_RESI_COL.AsString) +' / '+ FormatMaskText('(##) ####-####;0',QryRelatorioNU_TELE_CELU_COL.AsString);

//  QRLTotal.Caption := IntToStr(StrToInt(QRLTotal.Caption) + 1);
end;

procedure TFrmRelMapaDisciplinaProf.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //  QRLTotal.Caption := '0';
end;

procedure TFrmRelMapaDisciplinaProf.QRBand2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;

  with qryTotalProf do
  begin
  Close;
  Sql.Clear;
  Sql.Text := ' SELECT COUNT(DISTINCT(P.CO_COL)) as Total '+
              '	FROM TB17_PLANO_AULA P '+
              ' WHERE P.CO_EMP = ' + codigoEmpresa;
  Open;
  end;

  QrlTotalProf.Caption := FormatMaskText('00;0', qryTotalProfTotal.AsString);

end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaDisciplinaProf]);

end.
