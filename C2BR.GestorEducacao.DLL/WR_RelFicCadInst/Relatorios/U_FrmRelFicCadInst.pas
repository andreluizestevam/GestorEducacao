unit U_FrmRelFicCadInst;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

const
  OffsetMemoryStream : Int64 = 0;

type
  TFrmRelFicCadInst = class(TFrmRelTemplate)
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
    QRDBText16: TQRDBText;
    QRLabel15: TQRLabel;
    QRDBText19: TQRDBText;
    QRDBText20: TQRDBText;
    QRLabel19: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRDBText26: TQRDBText;
    QRLabel27: TQRLabel;
    QRDBText27: TQRDBText;
    QryPerfilEmpresa: TADOQuery;
    QryPerfilEmpresaCOMPARTILHA: TStringField;
    QryPerfilEmpresaASSENTAMENTO: TStringField;
    QryPerfilEmpresaANEXO: TStringField;
    QryPerfilEmpresaMURO: TStringField;
    QryPerfilEmpresaCOLETALIXO: TStringField;
    QryPerfilEmpresaEDUCACAOINDIGENA: TStringField;
    QryPerfilEmpresaTRANSPORTEESCOLAR: TStringField;
    QryPerfilEmpresaESGOTO: TStringField;
    QryPerfilEmpresaENERGIA: TStringField;
    QryPerfilEmpresaNU_INEP: TIntegerField;
    QryPerfilEmpresaCO_QUI: TIntegerField;
    QryPerfilEmpresaCO_ABAST: TIntegerField;
    QryPerfilEmpresaNU_VOLT: TIntegerField;
    QryPerfilEmpresaCO_PREDIO: TIntegerField;
    QRLabel62: TQRLabel;
    QRLabel63: TQRLabel;
    QRLNuDtAta: TQRLabel;
    QryAlunoCurso: TADOQuery;
    QryAlunoCursoCO_EMP: TAutoIncField;
    QryAlunoCursoALUINFAN: TIntegerField;
    QryAlunoCursoALU1CINI: TIntegerField;
    QryAlunoCursoALU1CINT: TIntegerField;
    QryAlunoCursoALU1CFIN: TIntegerField;
    QryAlunoCursoALU2CINI: TIntegerField;
    QryAlunoCursoALU2CFIN: TIntegerField;
    QryAlunoCursoALU3CINI: TIntegerField;
    QryAlunoCursoALU3CFIN: TIntegerField;
    QryAlunoCursoALU4CINI: TIntegerField;
    QryAlunoCursoALU4CFIN: TIntegerField;
    QryAlunoCursoMULTI: TIntegerField;
    QryAlunoCursoALUEJA: TIntegerField;
    QryAlunoCursoALUTOTAL: TIntegerField;
    QryAlunoCursoTOTALPROF: TIntegerField;
    QryAlunoCursoTOTALFUN: TIntegerField;
    QryAlunoCursoCO_ANO_MES_MAT: TStringField;
    QryPerfilEmpresaBIBLIOTECA: TStringField;
    QryPerfilEmpresaGINASIO: TStringField;
    QryPerfilEmpresaINST_ESPE: TStringField;
    QryPerfilEmpresaNO_PREDIO: TStringField;
    QryPerfilEmpresaNU_AREA_TOTAL_EMP: TIntegerField;
    QryPerfilEmpresaNU_AREA_CONST_EMP: TIntegerField;
    QryPerfilEmpresaQT_PAVIMENTOS_EMP: TIntegerField;
    QryPerfilEmpresaQT_SALA_AULA_EMP: TIntegerField;
    QryPerfilEmpresaQT_SALA_ADMIN_EMP: TIntegerField;
    QryPerfilEmpresaQT_SALA_APOIO_EMP: TIntegerField;
    QryPerfilEmpresaQT_BANHE_FEM_EMP: TIntegerField;
    QryPerfilEmpresaQT_BANHE_MAS_EMP: TIntegerField;
    QryPerfilEmpresaQT_GINASIO_ESPOR_EMP: TIntegerField;
    QryPerfilEmpresaQT_QUADRA_COBERT_EMP: TIntegerField;
    QryPerfilEmpresaQT_QUADRA_ABERTA_EMP: TIntegerField;
    QryPerfilEmpresaQT_PISCINA_EMP: TIntegerField;
    QRLabel5: TQRLabel;
    QRIEscola: TQRImage;
    QRDBIEscola: TQRDBImage;
    qryAno: TADOQuery;
    QrlAno: TQRLabel;
    QrlINFAN: TQRLabel;
    Qrl1CINI: TQRLabel;
    Qrl1CINT: TQRLabel;
    Qrl1CFIN: TQRLabel;
    Qrl2CINI: TQRLabel;
    Qrl2CFIN: TQRLabel;
    Qrl3CINI: TQRLabel;
    Qrl3CFIN: TQRLabel;
    Qrl4CINI: TQRLabel;
    Qrl4CFIN: TQRLabel;
    QrlMULTI: TQRLabel;
    QrlEJA: TQRLabel;
    QrlTotalAluno: TQRLabel;
    QrlTotProf: TQRLabel;
    QrlTotFun: TQRLabel;
    GroupHeaderBand1: TQRBand;
    GroupHeaderBand2: TQRBand;
    QRLabel45: TQRLabel;
    QRLabel71: TQRLabel;
    QRLMat1: TQRLabel;
    QRShape7: TQRShape;
    QRLMat2: TQRLabel;
    QRLMat3: TQRLabel;
    QRLMat4: TQRLabel;
    QRLMat5: TQRLabel;
    QRLMat6: TQRLabel;
    QRLMat7: TQRLabel;
    QRLMat8: TQRLabel;
    QRLMat9: TQRLabel;
    QRLMat10: TQRLabel;
    QRLMat11: TQRLabel;
    QRLMat12: TQRLabel;
    QRLabel86: TQRLabel;
    QRLabel84: TQRLabel;
    QRShape6: TQRShape;
    QRShape4: TQRShape;
    QRLabel85: TQRLabel;
    QRLabel70: TQRLabel;
    QRShape8: TQRShape;
    QRSubDetailQtde: TQRSubDetail;
    QRLabel21: TQRLabel;
    QRShape2: TQRShape;
    QRShape10: TQRShape;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel28: TQRLabel;
    QRDBText11: TQRDBText;
    qryGestores: TADOQuery;
    QRSubDetailGestores: TQRSubDetail;
    QRLMatriculaGestor: TQRLabel;
    QRLTelGestores: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel12: TQRLabel;
    QRLDtInicio: TQRLabel;
    QRDBText6: TQRDBText;
    QRLIdade: TQRLabel;
    QRLabel2: TQRLabel;
    QRDBText10: TQRDBText;
    QryPerfilEmpresaDE_TIPO_UNIDA: TStringField;
    GroupFooterBand1: TQRBand;
    QRBand1: TQRBand;
    QRShape5: TQRShape;
    QRShape9: TQRShape;
    QRLDisFuncao: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel36: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel40: TQRLabel;
    QRSubDetailFuncao: TQRSubDetail;
    QRLNoFuncao: TQRLabel;
    QRLTotATI: TQRLabel;
    QRLTotATE: TQRLabel;
    QRLTotFCE: TQRLabel;
    QRLTotFES: TQRLabel;
    QRLTotLFR: TQRLabel;
    QRLTotLME: TQRLabel;
    QRLTotLMA: TQRLabel;
    QRLTotSUS: TQRLabel;
    QRLTotTRE: TQRLabel;
    QRLTotFER: TQRLabel;
    QRLTotFuncao: TQRLabel;
    GroupFooterBand2: TQRBand;
    QRLabel4: TQRLabel;
    QRLTotFunPROF: TQRLabel;
    GroupFooterBand3: TQRBand;
    QRMemo1: TQRMemo;
    qryFuncao: TADOQuery;
    QRLabel38: TQRLabel;
    QRLTotalATI: TQRLabel;
    QRLTotalATE: TQRLabel;
    QRLTotalFCE: TQRLabel;
    QRLTotalFES: TQRLabel;
    QRLTotalLFR: TQRLabel;
    QRLTotalLME: TQRLabel;
    QRLTotalLMA: TQRLabel;
    QRLTotalSUS: TQRLabel;
    QRLTotalTRE: TQRLabel;
    QRLTotalFER: TQRLabel;
    QRLTotalPROF: TQRLabel;
    QRLTotalFuncao: TQRLabel;
    QRLCEPEmp: TQRLabel;
    QRLTel1Emp: TQRLabel;
    QRLFaxEmp: TQRLabel;
    QRLCNPJEmp: TQRLabel;
    QRLColabor: TQRLabel;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupHeaderBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure GroupHeaderBand2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailFuncaoBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetailQtdeBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelFicCadInst: TFrmRelFicCadInst;

implementation

{$R *.dfm}

uses U_DataModuleSGE, MaskUtils, JPEG, DateUtils;

procedure TFrmRelFicCadInst.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  SqlString : String;
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
        {Bmp := TBitmap.Create;
        Jpg := TJpegImage.Create;
        MemoryStream := TMemoryStream.Create;
        (QryRelatorio.FieldByName('fotoEmpresa') as TBlobField).SaveToStream(MemoryStream);
        MemoryStream.Position := OffsetMemoryStream;
        Bmp.LoadFromStream(MemoryStream);
        Jpg.CompressionQuality := 100; // Qualidade: 100%
        Jpg.Assign(Bmp);
        //Jpg.LoadFromStream(MemoryStream);
        QRIEscola.Picture.Assign(Jpg); }
      end;
      finally
          MemoryStream.Free;
          Jpg.Free;
          Bmp.Free;
      end;
  end
  else
  begin
    QRIEscola.Enabled := false;
    QRDBIEscola.Enabled := false;
  end;

  QRLMat1.Caption := '';
  QRLMat2.Caption := '';
  QRLMat3.Caption := '';
  QRLMat4.Caption := '';
  QRLMat5.Caption := '';
  QRLMat6.Caption := '';
  QRLMat7.Caption := '';
  QRLMat8.Caption := '';
  QRLMat9.Caption := '';
  QRLMat10.Caption := '';
  QRLMat11.Caption := '';
  QRLMat12.Caption := '';

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select CO_SIGL_CUR,SEQ_IMPRESSAO from tb01_curso ' +
                'where co_emp = ' + codigoEmpresa +
                ' order by SEQ_IMPRESSAO';
    Open;
    First;

    if not IsEmpty then
    begin
      while not Eof do
      begin

        if not FieldByName('SEQ_IMPRESSAO').IsNull then
        begin
          case FieldByName('SEQ_IMPRESSAO').AsInteger of
            1: QRLMat1.Caption := FieldByName('CO_SIGL_CUR').AsString;
            2: QRLMat2.Caption := FieldByName('CO_SIGL_CUR').AsString;
            3: QRLMat3.Caption := FieldByName('CO_SIGL_CUR').AsString;
            4: QRLMat4.Caption := FieldByName('CO_SIGL_CUR').AsString;
            5: QRLMat5.Caption := FieldByName('CO_SIGL_CUR').AsString;
            6: QRLMat6.Caption := FieldByName('CO_SIGL_CUR').AsString;
            7: QRLMat7.Caption := FieldByName('CO_SIGL_CUR').AsString;
            8: QRLMat8.Caption := FieldByName('CO_SIGL_CUR').AsString;
            9: QRLMat9.Caption := FieldByName('CO_SIGL_CUR').AsString;
            10: QRLMat10.Caption := FieldByName('CO_SIGL_CUR').AsString;
            11: QRLMat11.Caption := FieldByName('CO_SIGL_CUR').AsString;
            12: QRLMat12.Caption := FieldByName('CO_SIGL_CUR').AsString;
          end;
        end;

        Next;
      end;
    end;
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
                ' SELECT '+
                ' COMPARTILHA = '+
                ' (CASE PE.FLA_COMP_PREDIO '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                'ASSENTAMENTO = '+
                '(CASE PE.FLA_ASSENTAMENTO '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                'END), '+
                ' ANEXO = '+
                ' (CASE PE.FLA_ANEXO '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' MURO = '+
                ' (CASE PE.TP_MURO '+
                '	WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' COLETALIXO = '+
                ' (CASE PE.FLA_COLETA_LIXO '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' EDUCACAOINDIGENA = '+
                ' (CASE PE.FLA_ED_INDIGENA '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' TRANSPORTEESCOLAR = '+
                ' (CASE PE.FLA_TRANS_ESC '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' ESGOTO = '+
                ' (CASE PE.TP_ESG '+
                '	WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' BIBLIOTECA = '+
                ' (CASE PE.FLA_BIBLIOTECA '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' GINASIO = '+
                ' (CASE PE.FLA_GINASIO_ESPOR '+
                '	WHEN ' + QuotedStr('0') + ' THEN ' + QuotedStr('Não') +
                '	WHEN ' + QuotedStr('1') + ' THEN ' + QuotedStr('Sim') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' INST_ESPE = '+
                ' (CASE PE.TP_INST_ADEQ_ALU_ESP '+
                '	WHEN ' + QuotedStr('S') + ' THEN ' + QuotedStr('Sim') +
                ' WHEN ' + QuotedStr('N') + ' THEN ' + QuotedStr('Não') +
                ' WHEN ' + QuotedStr('P') + ' THEN ' + QuotedStr('Parcial') +
                '	WHEN ' + QuotedStr('F') + ' THEN ' + QuotedStr('Não Declarada') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' ENERGIA = '+
                ' (CASE PE.TP_ENERGIA '+
                '	WHEN ' + QuotedStr('T') + ' THEN ' + QuotedStr('Trifásica') +
                ' WHEN ' + QuotedStr('B') + ' THEN ' + QuotedStr('Bifásica') +
                '	WHEN ' + QuotedStr('M') + ' THEN ' + QuotedStr('Monofásica') +
                '	ELSE ' + QuotedStr('Não declarado') +
                ' END), '+
                ' PE.NU_INEP, PE.CO_QUI, PE.CO_ABAST, PE.NU_VOLT, PE.CO_PREDIO,P.NO_PREDIO,PE.NU_AREA_TOTAL_EMP, '+
                'PE.NU_AREA_CONST_EMP,PE.QT_PAVIMENTOS_EMP,PE.QT_SALA_AULA_EMP,PE.QT_SALA_ADMIN_EMP,'+
                'PE.QT_SALA_APOIO_EMP,PE.QT_BANHE_FEM_EMP,PE.QT_BANHE_MAS_EMP,PE.QT_GINASIO_ESPOR_EMP,'+
                'PE.QT_QUADRA_COBERT_EMP,PE.QT_QUADRA_ABERTA_EMP,PE.QT_PISCINA_EMP,tpuni.DE_TIPO_UNIDA'+
                ' FROM TB173_PERFIL_EMPRESA PE '+
                ' JOIN TB160_PERFIL_UNIDADE PU on PU.CO_EMP = PE.CO_EMP '+
                ' LEFT JOIN TB182_TIPO_UNIDA tpuni ON tpuni.CO_SIGLA_TIPO_UNIDA = PU.CO_TIPO_UNIDA ' +
                ' LEFT JOIN TB171_PREDIO P ON PE.CO_PREDIO = P.CO_PREDIO ' +
                ' WHERE PE.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    SQL.Text := SqlString;
  end;

 QryPerfilEmpresa.Open;

 if not QryPerfilEmpresa.IsEmpty then
 begin

 end;


end;

procedure TFrmRelFicCadInst.GroupHeaderBand1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;

  with qryAno do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct CO_ANO_MES_MAT from tb08_matrcur ' +
                'where co_emp = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
    Open;
  end;
end;

procedure TFrmRelFicCadInst.GroupHeaderBand2AfterPrint(
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

procedure TFrmRelFicCadInst.QRSubDetailGestoresBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  diasnoano : real;
begin
  inherited;

  if QRSubDetailGestores.Color = clWhite then
    QRSubDetailGestores.Color := $00EBEBEB
  else
    QRSubDetailGestores.Color := clWhite;

  if not qryGestores.FieldByName('NO_COL').IsNull then
    QRLColabor.Caption := UpperCase(qryGestores.FieldByName('NO_COL').AsString)
  else
    QRLColabor.Caption := '-';

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

procedure TFrmRelFicCadInst.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLDisFuncao.Caption := 'DISTRIBUIÇÃO FUNCIONÁRIOS - ANO REF ' + FormatDateTime('yyyy',Now);
end;

procedure TFrmRelFicCadInst.QRSubDetailFuncaoBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  SQLString : String;
begin
  inherited;

  // ZEBRADO
  if QRSubDetailFuncao.Color = clWhite then
    QRSubDetailFuncao.Color := $00EBEBEB
  else
    QRSubDetailFuncao.Color := clWhite;

  if not qryFuncao.fieldByName('CO_FUN').IsNull then
  begin
    // CONSULTA DOS TOTAIS - EMPRESA
    with DM.QrySql do
    begin
      Close;
      Sql.Clear;
      SQLString := ' SELECT DISTINCT CO.CO_EMP, CO.CO_FUN, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('ATI') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDATI, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('ATE') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDATE, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('FCE') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDFCE, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('FES') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDFES, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('LFR') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDLFR, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('LME') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDLME, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('LMA') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDLMA, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('SUS') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDSUS, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('TRE') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDTRE, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '		AND C.CO_SITU_COL = ' + QuotedStr('FER') +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDFER, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              '   AND C.FLA_PROFESSOR = ' + QuotedStr('S') +
              ' AND C.CO_SITU_COL is not null ' +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDPRO, '+
              ' (SELECT DISTINCT COUNT(C.CO_COL) FROM TB03_COLABOR C '+
              '	WHERE C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              ' AND C.CO_SITU_COL is not null ' +
              ' AND C.CO_FUN = ' + qryFuncao.fieldByName('CO_FUN').AsString +
              ') QTDTOTAL '+
              'FROM TB03_COLABOR CO '+
              ' JOIN TB15_FUNCAO F ON F.CO_FUN = CO.CO_FUN '+
              'WHERE CO.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
              ' AND CO.CO_FUN = ' + qryFuncao.FieldByName('CO_FUN').AsString;

      SQL.Text := SQLString;
      Open;

      // LEGENDAS DOS TOTAIS
      if FieldByName('QTDATI').AsString <> '0' then
      begin
        QRLTotATI.Caption := FieldByName('QTDATI').AsString;
        QRLTotalATI.Caption := IntToStr(StrToInt(QRLTotalATI.Caption) + FieldByName('QTDATI').AsInteger);
      end
      else
      begin
        QRLTotATI.Caption := '-';
      end;

      if FieldByName('QTDATE').AsString <> '0' then
      begin
        QRLTotATE.Caption := FieldByName('QTDATE').AsString;
        QRLTotalATE.Caption := IntToStr(StrToInt(QRLTotalATE.Caption) + FieldByName('QTDATE').AsInteger);
      end
      else
      begin
        QRLTotATE.Caption := '-';
      end;

      if FieldByName('QTDFCE').AsString <> '0' then
      begin
        QRLTotFCE.Caption := FieldByName('QTDFCE').AsString;
        QRLTotalFCE.Caption := IntToStr(StrToInt(QRLTotalFCE.Caption) + FieldByName('QTDFCE').AsInteger);
      end
      else
      begin
        QRLTotFCE.Caption := '-';
      end;

      if FieldByName('QTDFES').AsString <> '0' then
      begin
        QRLTotFES.Caption := FieldByName('QTDFES').AsString;
        QRLTotalFES.Caption := IntToStr(StrToInt(QRLTotalFES.Caption) + FieldByName('QTDFES').AsInteger);
      end
      else
      begin
        QRLTotFES.Caption := '-';
      end;

      if FieldByName('QTDLFR').AsString <> '0' then
      begin
        QRLTotLFR.Caption := FieldByName('QTDLFR').AsString;
        QRLTotalLFR.Caption := IntToStr(StrToInt(QRLTotalLFR.Caption) + FieldByName('QTDLFR').AsInteger);
      end
      else
      begin
        QRLTotLFR.Caption := '-';
      end;

      if FieldByName('QTDLME').AsString <> '0' then
      begin
        QRLTotLME.Caption := FieldByName('QTDLME').AsString;
        QRLTotalLME.Caption := IntToStr(StrToInt(QRLTotalLME.Caption) + FieldByName('QTDLME').AsInteger);
      end
      else
      begin
        QRLTotLME.Caption := '-';
      end;

      if FieldByName('QTDLMA').AsString <> '0' then
      begin
        QRLTotLMA.Caption := FieldByName('QTDLMA').AsString;
        QRLTotalLMA.Caption := IntToStr(StrToInt(QRLTotalLMA.Caption) + FieldByName('QTDLMA').AsInteger);
      end
      else
      begin
        QRLTotLMA.Caption := '-';
      end;

      if FieldByName('QTDSUS').AsString <> '0' then
      begin
        QRLTotSUS.Caption := FieldByName('QTDSUS').AsString;
        QRLTotalSUS.Caption := IntToStr(StrToInt(QRLTotalSUS.Caption) + FieldByName('QTDSUS').AsInteger);
      end
      else
      begin
        QRLTotSUS.Caption := '-';
      end;

      if FieldByName('QTDTRE').AsString <> '0' then
      begin
        QRLTotTRE.Caption := FieldByName('QTDTRE').AsString;
        QRLTotalTRE.Caption := IntToStr(StrToInt(QRLTotalTRE.Caption) + FieldByName('QTDTRE').AsInteger);
      end
      else
      begin
        QRLTotTRE.Caption := '-';
      end;

      if FieldByName('QTDFER').AsString <> '0' then
      begin
        QRLTotFER.Caption := FieldByName('QTDFER').AsString;
        QRLTotalFER.Caption := IntToStr(StrToInt(QRLTotalFER.Caption) + FieldByName('QTDFER').AsInteger);
      end
      else
      begin
        QRLTotFER.Caption := '-';
      end;

      if FieldByName('QTDPRO').AsString <> '0' then
      begin
        QRLTotFunPROF.Caption := FieldByName('QTDPRO').AsString;
        QRLTotalPROF.Caption := IntToStr(StrToInt(QRLTotalPROF.Caption) + FieldByName('QTDPRO').AsInteger);
      end
      else
      begin
        QRLTotFunPROF.Caption := '-';
      end;

      if FieldByName('QTDTOTAL').AsString <> '0' then
      begin
        QRLTotFuncao.Caption := FieldByName('QTDTOTAL').AsString;
        QRLTotalFuncao.Caption := IntToStr(StrToInt(QRLTotalFuncao.Caption) + FieldByName('QTDTOTAL').AsInteger);
      end
      else
      begin
        QRLTotFuncao.Caption := '-';
      end;
    end;

    QRLNoFuncao.Caption := qryFuncao.fieldByName('NO_FUN').AsString
  end
  else
  begin
    QRLTotATI.Caption := '-';
    QRLTotATE.Caption := '-';
    QRLTotFCE.Caption := '-';
    QRLTotFES.Caption := '-';
    QRLTotLFR.Caption := '-';
    QRLTotLME.Caption := '-';
    QRLTotLMA.Caption := '-';
    QRLTotSUS.Caption := '-';
    QRLTotTRE.Caption := '-';
    QRLTotFER.Caption := '-';
    QRLTotFunPROF.Caption := '-';
    QRLTotFuncao.Caption := '-';
    QRLNoFuncao.Caption := '-';
  end;
end;

procedure TFrmRelFicCadInst.QRSubDetailQtdeBeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if QRSubDetailQtde.Color = clWhite then
    QRSubDetailQtde.Color := $00EBEBEB
  else
    QRSubDetailQtde.Color := clWhite;

  // CONSULTA DOS TOTAIS - EMPRESA
  if not qryAno.fieldByName('CO_ANO_MES_MAT').IsNull then
  begin
    with QryAlunoCurso do
    begin
      Close;
      Sql.Clear;
        //if Sys_TipoEnsino = 'ES' then
        //begin
          Sql.Text := ' SELECT DISTINCT E.CO_EMP, M.CO_ANO_MES_MAT, '+
                    ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '  JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '  JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	 JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat1.Caption) +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALUINFAN, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    ' 	AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat2.Caption) +
                    ' 	AND M.CO_CUR = C.CO_CUR '+
                    ') ALU1CINI, '+
                    ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat3.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU1CINT, '+
                    ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat4.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU1CFIN, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat5.Caption) +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU2CINI, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat6.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU2CFIN, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat7.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU3CINI, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat8.Caption) +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU3CFIN, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat9.Caption) +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU4CINI, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat10.Caption) +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALU4CFIN, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat11.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') MULTI, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND C.CO_SIGL_CUR = ' + QuotedStr(QRLMat12.Caption) +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALUEJA, '+
                    '(SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '+
                    '	JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '+
                    '	JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '+
                    '	JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '+
                    '	WHERE A.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    '		AND M.CO_CUR = C.CO_CUR '+
                    ') ALUTOTAL, '+
                    ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE COL.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND COL.FLA_PROFESSOR = ' + QuotedStr('S') +
                    //'   AND year(COL.DT_INIC_ATIV_COL) = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    ' ) TOTALPROF, '+
                    ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL '+
                    ' 	JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP '+
                    ' 	WHERE COL.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                    '		AND COL.FLA_PROFESSOR = ' + QuotedStr('N') +
                    //'   AND year(COL.DT_INIC_ATIV_COL) = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    ' ) TOTALFUN '+
                    'FROM TB25_EMPRESA E '+
                    ' JOIN TB08_MATRCUR M ON M.CO_EMP = E.CO_EMP '+
                    ' AND M.CO_ANO_MES_MAT = ' + qryAno.fieldByName('CO_ANO_MES_MAT').AsString +
                    'WHERE E.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;
        //end;
      Open;
    end;

    // LEGENDAS DOS TOTAIS
    if QryAlunoCursoALUINFAN.AsString <> '0' then
    begin
      QrlINFAN.Caption := QryAlunoCursoALUINFAN.AsString
    end
    else
    begin
      //QrlINFAN.Alignment := taCenter;
      QrlINFAN.Caption := '-';
    end;

    if QryAlunoCursoALU1CINI.AsString <> '0' then
    begin
      Qrl1CINI.Caption := QryAlunoCursoALU1CINI.AsString
    end
    else
    begin
      //Qrl1CINI.Alignment := taCenter;
      Qrl1CINI.Caption := '-';
    end;

    if QryAlunoCursoALU1CINT.AsString <> '0' then
    begin
      Qrl1CINT.Caption := QryAlunoCursoALU1CINT.AsString
    end
    else
    begin
      //Qrl1CINT.Alignment := taCenter;
      Qrl1CINT.Caption := '-';
    end;

    if QryAlunoCursoALU1CFIN.AsString <> '0' then
    begin
      Qrl1CFIN.Caption := QryAlunoCursoALU1CFIN.AsString
    end
    else
    begin
      //Qrl1CFIN.Alignment := taCenter;
      Qrl1CFIN.Caption := '-';
    end;

    if QryAlunoCursoALU2CINI.AsString <> '0' then
    begin
      Qrl2CINI.Caption := QryAlunoCursoALU2CINI.AsString
    end
    else
    begin
      //Qrl2CINI.Alignment := taCenter;
      Qrl2CINI.Caption := '-';
    end;

    if QryAlunoCursoALU2CFIN.AsString <> '0' then
    begin
      Qrl2CFIN.Caption := QryAlunoCursoALU2CFIN.AsString
    end
    else
    begin
      //Qrl2CFIN.Alignment := taCenter;
      Qrl2CFIN.Caption := '-';
    end;

    if QryAlunoCursoALU3CINI.AsString <> '0' then
    begin
      Qrl3CINI.Caption := QryAlunoCursoALU3CINI.AsString
    end
    else
    begin
      //Qrl3CINI.Alignment := taCenter;
      Qrl3CINI.Caption := '-';
    end;

    if QryAlunoCursoALU3CFIN.AsString <> '0' then
    begin
      Qrl3CFIN.Caption := QryAlunoCursoALU3CFIN.AsString
    end
    else
    begin
      //Qrl3CFIN.Alignment := taCenter;
      Qrl3CFIN.Caption := '-';
    end;

    if QryAlunoCursoALU4CINI.AsString <> '0' then
    begin
      Qrl4CINI.Caption := QryAlunoCursoALU4CINI.AsString
    end
    else
    begin
      //Qrl4CINI.Alignment := taCenter;
      Qrl4CINI.Caption := '-';
    end;

    if QryAlunoCursoALU4CFIN.AsString <> '0' then
    begin
      Qrl4CFIN.Caption := QryAlunoCursoALU4CFIN.AsString
    end
    else
    begin
      //Qrl4CFIN.Alignment := taCenter;
      Qrl4CFIN.Caption := '-';
    end;

    if QryAlunoCursoMULTI.AsString <> '0' then
    begin
      QrlMULTI.Caption := QryAlunoCursoMULTI.AsString
    end
    else
    begin
      //QrlMULTI.Alignment := taCenter;
      QrlMULTI.Caption := '-';
    end;

    if QryAlunoCursoALUEJA.AsString <> '0' then
    begin
      QrlEJA.Caption := QryAlunoCursoALUEJA.AsString
    end
    else
    begin
      //QrlEJA.Alignment := taCenter;
      QrlEJA.Caption := '-';
    end;

    if QryAlunoCursoALUTOTAL.AsString <> '0' then
    begin
      QrlTotalAluno.Caption := QryAlunoCursoALUTOTAL.AsString
    end
    else
    begin
      //QrlTotalAluno.Alignment := taCenter;
      QrlTotalAluno.Caption := '-';
    end;

    if QryAlunoCursoTOTALPROF.AsString <> '0' then
    begin
      QrlTotProf.Caption := QryAlunoCursoTOTALPROF.AsString
    end
    else
    begin
      //QrlTotProf.Alignment := taCenter;
      QrlTotProf.Caption := '-';
    end;

    if QryAlunoCursoTOTALFUN.AsString <> '0' then
    begin
      QrlTotFun.Caption := QryAlunoCursoTOTALFUN.AsString
    end
    else
    begin
      //QrlTotFun.Alignment := taCenter;
      QrlTotFun.Caption := '-';
    end;

    QrlAno.Caption := qryAno.fieldByName('CO_ANO_MES_MAT').AsString;
  end
  else
  begin
    QrlINFAN.Caption := '-';
    Qrl1CINI.Caption := '-';
    Qrl1CINT.Caption := '-';
    Qrl1CFIN.Caption := '-';
    Qrl2CINI.Caption := '-';
    Qrl2CFIN.Caption := '-';
    Qrl3CINI.Caption := '-';
    Qrl3CFIN.Caption := '-';
    Qrl4CINI.Caption := '-';
    Qrl4CFIN.Caption := '-';
    QrlMULTI.Caption := '-';
    QrlEJA.Caption := '-';
    QrlTotalAluno.Caption := '-';
    QrlTotProf.Caption := '-';
    QrlTotFun.Caption := '-';
    QrlAno.Caption := '-';
  end;
end;

procedure TFrmRelFicCadInst.QRBand1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;

  with qryFuncao do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select distinct F.CO_FUN, F.NO_FUN from tb03_colabor C ' +
                'join tb15_funcao F on F.co_fun = C.co_fun ' +
                'where C.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString +
                ' order by F.NO_FUN';
    Open;
  end;
end;

procedure TFrmRelFicCadInst.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRLTotalATI.Caption := '0';
  QRLTotalATE.Caption := '0';
  QRLTotalFCE.Caption := '0';
  QRLTotalFES.Caption := '0';
  QRLTotalLFR.Caption := '0';
  QRLTotalLME.Caption := '0';
  QRLTotalLMA.Caption := '0';
  QRLTotalSUS.Caption := '0';
  QRLTotalTRE.Caption := '0';
  QRLTotalFER.Caption := '0';
  QRLTotalPROF.Caption := '0';
  QRLTotalFuncao.Caption := '0';
end;

end.
