unit U_FrmRelBoltetimAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelBoletimAluno = class(TFrmRelTemplate)
    QRBDetail: TQRBand;
    QRBand2: TQRBand;
    QRLabel17: TQRLabel;
    QRShape1: TQRShape;
    QRLResultadoFinal: TQRLabel;
    QRShape2: TQRShape;
    QRLDiretor: TQRLabel;
    QRLNota1: TQRLabel;
    QRLFalta1: TQRLabel;
    QRLNota2: TQRLabel;
    QRLFalta2: TQRLabel;
    QRLNota3: TQRLabel;
    QRLFalta3: TQRLabel;
    QRLNota4: TQRLabel;
    QRLFalta4: TQRLabel;
    QRLMedia: TQRLabel;
    QRDBMateria: TQRDBText;
    QRLFaltTotal: TQRLabel;
    QRGroup1: TQRGroup;
    QRLCaSerie: TQRLabel;
    QRLSerie: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape3: TQRShape;
    QRLN1: TQRLabel;
    QRLAv1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLTurma: TQRLabel;
    QRLabel3: TQRLabel;
    QRLModulo: TQRLabel;
    QRLabel4: TQRLabel;
    QRLAno: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel13: TQRLabel;
    QRLAv4: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel11: TQRLabel;
    QRShape6: TQRShape;
    QRLAv3: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRShape5: TQRShape;
    QRLAv2: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRShape10: TQRShape;
    QRShape4: TQRShape;
    QRLF1: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel5: TQRLabel;
    QRLPage: TQRLabel;
    QRShape25: TQRShape;
    QRLMes: TQRLabel;
    QRShape26: TQRShape;
    QRLabel18: TQRLabel;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRLabel19: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel27: TQRLabel;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QRShape58: TQRShape;
    QRShape59: TQRShape;
    QRShape60: TQRShape;
    QRShape61: TQRShape;
    QRShape70: TQRShape;
    QRShape62: TQRShape;
    QRShape71: TQRShape;
    QRShape72: TQRShape;
    QRShape73: TQRShape;
    QRShape74: TQRShape;
    QRShape75: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QRShape78: TQRShape;
    QRShape79: TQRShape;
    QRShape80: TQRShape;
    QRShape81: TQRShape;
    QRShape82: TQRShape;
    QRShape83: TQRShape;
    QRShape84: TQRShape;
    QRShape85: TQRShape;
    QRLabel41: TQRLabel;
    QRShape87: TQRShape;
    QRShape88: TQRShape;
    QRShape89: TQRShape;
    QRShape90: TQRShape;
    QRShape93: TQRShape;
    QRShape94: TQRShape;
    QRShape95: TQRShape;
    QRShape96: TQRShape;
    QRShape97: TQRShape;
    QRShape98: TQRShape;
    QRShape99: TQRShape;
    QRShape100: TQRShape;
    QRLabel42: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel45: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel49: TQRLabel;
    QRShape101: TQRShape;
    QRLProFinal: TQRLabel;
    QRShape102: TQRShape;
    qrlMat1: TQRLabel;
    qrlMat2: TQRLabel;
    qrlMat3: TQRLabel;
    qrlMat4: TQRLabel;
    qrlMat5: TQRLabel;
    qrlMat6: TQRLabel;
    qrlMat7: TQRLabel;
    qrlMat8: TQRLabel;
    qrlMat9: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel36: TQRLabel;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRLabel37: TQRLabel;
    QRShape46: TQRShape;
    QRLabel38: TQRLabel;
    QRShape47: TQRShape;
    QRLabel39: TQRLabel;
    QRShape48: TQRShape;
    QRLabel40: TQRLabel;
    QRShape103: TQRShape;
    QRLabel28: TQRLabel;
    QRLabel59: TQRLabel;
    QRLabel60: TQRLabel;
    QRLabel61: TQRLabel;
    QRLabel62: TQRLabel;
    QRLabel63: TQRLabel;
    QRLabel64: TQRLabel;
    QRLabel65: TQRLabel;
    QRLabel66: TQRLabel;
    QRLabel67: TQRLabel;
    QRLabel68: TQRLabel;
    QRLabel69: TQRLabel;
    QRLabel70: TQRLabel;
    QRLabel71: TQRLabel;
    QRLabel72: TQRLabel;
    QRLabel73: TQRLabel;
    QRLabel74: TQRLabel;
    QRLabel75: TQRLabel;
    QRShape104: TQRShape;
    QRShape105: TQRShape;
    QRLabel76: TQRLabel;
    QRLabel77: TQRLabel;
    QRShape107: TQRShape;
    QRShape106: TQRShape;
    QRLSinBim: TQRLabel;
    QRShape108: TQRShape;
    QRShape63: TQRShape;
    QRShape64: TQRShape;
    QRShape65: TQRShape;
    QRShape66: TQRShape;
    QRShape67: TQRShape;
    QRShape68: TQRShape;
    QRShape69: TQRShape;
    QRShape110: TQRShape;
    QRShape111: TQRShape;
    QRShape112: TQRShape;
    QRShape113: TQRShape;
    QRShape114: TQRShape;
    QRShape115: TQRShape;
    QRShape86: TQRShape;
    QRShape91: TQRShape;
    QRShape92: TQRShape;
    QRShape116: TQRShape;
    QRShape33: TQRShape;
    qryFaltas: TADOQuery;
    QRLC11: TQRLabel;
    QRLC12: TQRLabel;
    QRLC13: TQRLabel;
    QRLC14: TQRLabel;
    QRLC24: TQRLabel;
    QRLC34: TQRLabel;
    QRLC44: TQRLabel;
    QRLC54: TQRLabel;
    QRLC64: TQRLabel;
    QRLC74: TQRLabel;
    QRLC84: TQRLabel;
    QRLC94: TQRLabel;
    QRLC23: TQRLabel;
    QRLC33: TQRLabel;
    QRLC43: TQRLabel;
    QRLC53: TQRLabel;
    QRLC63: TQRLabel;
    QRLC73: TQRLabel;
    QRLC83: TQRLabel;
    QRLC93: TQRLabel;
    QRLC22: TQRLabel;
    QRLC32: TQRLabel;
    QRLC42: TQRLabel;
    QRLC52: TQRLabel;
    QRLC62: TQRLabel;
    QRLC72: TQRLabel;
    QRLC82: TQRLabel;
    QRLC92: TQRLabel;
    QRLC21: TQRLabel;
    QRLC31: TQRLabel;
    QRLC41: TQRLabel;
    QRLC51: TQRLabel;
    QRLC61: TQRLabel;
    QRLC71: TQRLabel;
    QRLC81: TQRLabel;
    QRLC91: TQRLabel;
    QRLR11: TQRLabel;
    QRLR21: TQRLabel;
    QRLR31: TQRLabel;
    QRLR41: TQRLabel;
    QRLR51: TQRLabel;
    QRLR61: TQRLabel;
    QRLR71: TQRLabel;
    QRLR81: TQRLabel;
    QRLR91: TQRLabel;
    QRLR12: TQRLabel;
    QRLR22: TQRLabel;
    QRLR32: TQRLabel;
    QRLR42: TQRLabel;
    QRLR52: TQRLabel;
    QRLR62: TQRLabel;
    QRLR72: TQRLabel;
    QRLR82: TQRLabel;
    QRLR92: TQRLabel;
    QRLR13: TQRLabel;
    QRLR23: TQRLabel;
    QRLR33: TQRLabel;
    QRLR43: TQRLabel;
    QRLR53: TQRLabel;
    QRLR63: TQRLabel;
    QRLR73: TQRLabel;
    QRLR83: TQRLabel;
    QRLR93: TQRLabel;
    QRLR14: TQRLabel;
    QRLR24: TQRLabel;
    QRLR34: TQRLabel;
    QRLR44: TQRLabel;
    QRLR54: TQRLabel;
    QRLR64: TQRLabel;
    QRLR74: TQRLabel;
    QRLR84: TQRLabel;
    QRLR94: TQRLabel;
    QRLP11: TQRLabel;
    QRLP21: TQRLabel;
    QRLP31: TQRLabel;
    QRLP41: TQRLabel;
    QRLP51: TQRLabel;
    QRLP61: TQRLabel;
    QRLP71: TQRLabel;
    QRLP81: TQRLabel;
    QRLP91: TQRLabel;
    QRLP12: TQRLabel;
    QRLP22: TQRLabel;
    QRLP32: TQRLabel;
    QRLP42: TQRLabel;
    QRLP52: TQRLabel;
    QRLP62: TQRLabel;
    QRLP72: TQRLabel;
    QRLP82: TQRLabel;
    QRLP92: TQRLabel;
    QRLP13: TQRLabel;
    QRLP23: TQRLabel;
    QRLP33: TQRLabel;
    QRLP43: TQRLabel;
    QRLP53: TQRLabel;
    QRLP63: TQRLabel;
    QRLP73: TQRLabel;
    QRLP83: TQRLabel;
    QRLP93: TQRLabel;
    QRLP14: TQRLabel;
    QRLP24: TQRLabel;
    QRLP34: TQRLabel;
    QRLP44: TQRLabel;
    QRLP54: TQRLabel;
    QRLP64: TQRLabel;
    QRLP74: TQRLabel;
    QRLP84: TQRLabel;
    QRLP94: TQRLabel;
    QRSResulFinal: TQRShape;
    QRLMatricula: TQRLabel;
    QrlMatrDir: TQRLabel;
    QryRelatorioNO_COL: TStringField;
    QryRelatorioCO_MAT_COL: TStringField;
    QryRelatoriono_alu: TStringField;
    QryRelatorioco_alu: TAutoIncField;
    QryRelatorioco_alu_cad: TStringField;
    QryRelatoriono_cur: TStringField;
    QryRelatorioco_cur: TAutoIncField;
    QryRelatoriono_materia: TStringField;
    QryRelatorioco_mat: TAutoIncField;
    QryRelatorioco_ano_mes_mat: TStringField;
    QrlDiaMesAno: TQRLabel;
    QryRelatorioNO_CIDADE: TStringField;
    QryRelatoriono_sigla_materia: TStringField;
    QRLMateria: TQRLabel;
    qryMateria: TADOQuery;
    QryRelatorioCO_STA_APROV: TStringField;
    QRLNoAlu: TQRLabel;
    QryRelatorioCO_STA_APROV_FREQ: TStringField;
    procedure QRBDetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    CodigoCurso, CodigoTurma, AnoCurso, CodigoModulo, codigoEmpresa: String;
    numAluno: Integer;
    materia : Array[0..30] of integer;
    numMat,ctMat : integer;
  end;

var
  FrmRelBoletimAluno: TFrmRelBoletimAluno;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelBoletimAluno.QRBDetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var
  mes,ano,dia : word;
  //mediaFinalBimestre,
  somaSintese : Double;
  //ct1,ct2,ct3,ct4,
  numFaltas,qtdMedias : integer;
begin
  inherited;
  qtdMedias := 0;
  QRLNota1.Caption := '-';
  QRLNota2.Caption := '-';
  QRLNota3.Caption := '-';
  QRLNota4.Caption := '-';
  QRLFalta1.Caption := '-';
  QRLFalta2.Caption := '-';
  QRLFalta3.Caption := '-';
  QRLFalta4.Caption := '-';
  QRLFaltTotal.Caption := '-';
  QRLMedia.Caption := '-';
  QRLProFinal.Caption := '-';
  QRLSinBim.Caption := '-';
  //mediaFinalBimestre := 0;
  //ct1 := 0;
  //ct2 := 0;
  //ct3 := 0;
  //ct4 := 0;

  QRLMateria.Caption := '( ' + QryRelatoriono_sigla_materia.AsString + ' ) ' + QryRelatoriono_materia.AsString;

  if QRDBMateria.Caption <> '' then
  begin
    case ctMat of
      1:qrlMat1.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      2:qrlMat2.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      3:qrlMat3.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      4:qrlMat4.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      5:qrlMat5.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      6:qrlMat6.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      7:qrlMat7.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      8:qrlMat8.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
      9:qrlMat9.Caption := QryRelatorio.fieldbyname('no_sigla_materia').AsString;
    end;

    ctMat := ctMat + 1;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Text := ' select h.*,c.med_final_cur from tb079_hist_aluno h ' +
                'JOIN tb01_curso c ON c.co_cur = h.co_cur '+
                'WHERE h.co_alu = ' + QryRelatorio.fieldByName('CO_ALU').AsString +
                ' and h.co_mat = ' + QryRelatorio.fieldbyname('co_mat').AsString  +
                ' and h.co_cur = ' + QryRelatorio.fieldByName('co_cur').AsString +
                ' and h.co_emp = ' + codigoEmpresa +
                ' and h.co_ano_ref = ' + QuotedStr(QryRelatorio.fieldByName('co_ano_mes_mat').AsString);
    Open;

    while not Eof do
    begin
      if not(fieldbyname('VL_CRIT_BIM1').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLC11.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        2:QRLC21.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        3:QRLC31.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        4:QRLC41.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        5:QRLC51.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        6:QRLC61.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        7:QRLC71.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        8:QRLC81.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        9:QRLC91.Caption := fieldbyname('VL_CRIT_BIM1').AsString;
        end;
      end;
      if not(fieldbyname('VL_CRIT_BIM2').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLC12.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        2:QRLC22.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        3:QRLC32.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        4:QRLC42.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        5:QRLC52.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        6:QRLC62.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        7:QRLC72.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        8:QRLC82.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        9:QRLC92.Caption := fieldbyname('VL_CRIT_BIM2').AsString;
        end;
      end;
      if not(fieldbyname('VL_CRIT_BIM3').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLC13.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        2:QRLC23.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        3:QRLC33.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        4:QRLC43.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        5:QRLC53.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        6:QRLC63.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        7:QRLC73.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        8:QRLC83.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        9:QRLC93.Caption := fieldbyname('VL_CRIT_BIM3').AsString;
        end;
      end;
      if not(fieldbyname('VL_CRIT_BIM4').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLC14.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        2:QRLC24.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        3:QRLC34.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        4:QRLC44.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        5:QRLC54.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        6:QRLC64.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        7:QRLC74.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        8:QRLC84.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        9:QRLC94.Caption := fieldbyname('VL_CRIT_BIM4').AsString;
        end;
      end;
      if not(fieldbyname('VL_RESP_BIM1').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLR11.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        2:QRLR21.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        3:QRLR31.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        4:QRLR41.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        5:QRLR51.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        6:QRLR61.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        7:QRLR71.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        8:QRLR81.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        9:QRLR91.Caption := fieldbyname('VL_RESP_BIM1').AsString;
        end;
      end;
      if not(fieldbyname('VL_RESP_BIM2').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLR12.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        2:QRLR22.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        3:QRLR32.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        4:QRLR42.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        5:QRLR52.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        6:QRLR62.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        7:QRLR72.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        8:QRLR82.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        9:QRLR92.Caption := fieldbyname('VL_RESP_BIM2').AsString;
        end;
      end;
      if not(fieldbyname('VL_RESP_BIM3').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLR13.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        2:QRLR23.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        3:QRLR33.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        4:QRLR43.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        5:QRLR53.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        6:QRLR63.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        7:QRLR73.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        8:QRLR83.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        9:QRLR93.Caption := fieldbyname('VL_RESP_BIM3').AsString;
        end;
      end;
      if not(fieldbyname('VL_RESP_BIM4').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLR14.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        2:QRLR24.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        3:QRLR34.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        4:QRLR44.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        5:QRLR54.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        6:QRLR64.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        7:QRLR74.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        8:QRLR84.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        9:QRLR94.Caption := fieldbyname('VL_RESP_BIM4').AsString;
        end;
      end;
      if not(fieldbyname('VL_APRE_BIM1').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLP11.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        2:QRLP21.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        3:QRLP31.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        4:QRLP41.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        5:QRLP51.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        6:QRLP61.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        7:QRLP71.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        8:QRLP81.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        9:QRLP91.Caption := fieldbyname('VL_APRE_BIM1').AsString;
        end;
      end;
      if not(fieldbyname('VL_APRE_BIM2').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLP12.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        2:QRLP22.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        3:QRLP32.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        4:QRLP42.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        5:QRLP52.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        6:QRLP62.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        7:QRLP72.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        8:QRLP82.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        9:QRLP92.Caption := fieldbyname('VL_APRE_BIM2').AsString;
        end;
      end;
      if not(fieldbyname('VL_APRE_BIM3').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLP13.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        2:QRLP23.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        3:QRLP33.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        4:QRLP43.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        5:QRLP53.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        6:QRLP63.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        7:QRLP73.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        8:QRLP83.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        9:QRLP93.Caption := fieldbyname('VL_APRE_BIM3').AsString;
        end;
      end;
      if not(fieldbyname('VL_APRE_BIM4').isNull) then
      begin
        case (ctMat - 1) of
        1:QRLP14.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        2:QRLP24.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        3:QRLP34.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        4:QRLP44.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        5:QRLP54.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        6:QRLP64.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        7:QRLP74.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        8:QRLP84.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        9:QRLP94.Caption := fieldbyname('VL_APRE_BIM4').AsString;
        end;
      end;   
      if not(fieldbyname('VL_NOTA_BIM4').isNull) then
      begin
        QRLNota4.Caption := fieldbyname('VL_NOTA_BIM4').AsString;
        QRLFalta4.Caption := fieldbyname('QT_FALTA_BIM4').AsString;
        somaSintese := somaSintese + fieldbyname('VL_NOTA_BIM4').AsFloat;
        qtdMedias := qtdMedias + 1;
        numFaltas := numFaltas + fieldbyname('QT_FALTA_BIM4').AsInteger;
      end;
      if not(fieldbyname('VL_NOTA_BIM3').isNull) then
      begin
        QRLNota3.Caption := fieldbyname('VL_NOTA_BIM3').AsString;
        QRLFalta3.Caption := fieldbyname('QT_FALTA_BIM3').AsString;
        somaSintese := somaSintese + fieldbyname('VL_NOTA_BIM3').AsFloat;
        qtdMedias := qtdMedias + 1;
        numFaltas := numFaltas + fieldbyname('QT_FALTA_BIM3').AsInteger;
      end;
      if not(fieldbyname('VL_NOTA_BIM2').isNull) then
      begin
        QRLNota2.Caption := fieldbyname('VL_NOTA_BIM2').AsString;
        QRLFalta2.Caption := fieldbyname('QT_FALTA_BIM2').AsString;
        somaSintese := somaSintese + fieldbyname('VL_NOTA_BIM2').AsFloat;
        qtdMedias := qtdMedias + 1;
        numFaltas := numFaltas + fieldbyname('QT_FALTA_BIM2').AsInteger;
      end;
      if not(fieldbyname('VL_NOTA_BIM1').isNull) then
      begin
        QRLNota1.Caption := fieldbyname('VL_NOTA_BIM1').AsString;
        QRLFalta1.Caption := fieldbyname('QT_FALTA_BIM1').AsString;
        somaSintese := somaSintese + fieldbyname('VL_NOTA_BIM1').AsFloat;
        qtdMedias := qtdMedias + 1;
        numFaltas := numFaltas + fieldbyname('QT_FALTA_BIM1').AsInteger;
      end;
      if not(fieldbyname('VL_PROVA_FINAL').isNull) then
        QRLProFinal.Caption := FormatFloat('0.0',fieldbyname('VL_PROVA_FINAL').AsFloat);
      if not(fieldbyname('VL_MEDIA_FINAL').isNull) then
        QRLMedia.Caption := FormatFloat('0.0',fieldbyname('VL_MEDIA_FINAL').AsFloat);
      Next;
    end;
  end;

  if (somaSintese >= 0) and (qtdMedias > 0) then
    QRLSinBim.Caption := FormatFloat('0.0',somaSintese/qtdMedias);

  if numFaltas >= 0 then
    QRLFaltTotal.Caption := IntToStr(numFaltas);

  if (not QryRelatorioCO_STA_APROV.IsNull) and ( not QryRelatorioCO_STA_APROV_FREQ.IsNull)  then
  begin
    QRLResultadoFinal.Enabled := true;
    QRSResulFinal.Enabled := false;

    if (QryRelatorioCO_STA_APROV.AsString = 'A') and (QryRelatorioCO_STA_APROV_FREQ.AsString = 'A') then
      QRLResultadoFinal.Caption := 'APROVADO'
    else
      QRLResultadoFinal.Caption := 'REPROVADO';
  end
  else
  begin
    QRLResultadoFinal.Enabled := false;
    QRSResulFinal.Enabled := true;
  end;


  //Deixar o relatório zebrado
  if QRBDetail.Color = clWhite then
    QRBDetail.Color := $00D8D8D8
  else
    QRBDetail.Color := clWhite;

  DecodeDate(now,ano,mes,dia);
  case mes of
  1:QRLMes.Caption := 'de ' + 'janeiro';
  2:QRLMes.Caption := 'de ' + 'fevereiro';
  3:QRLMes.Caption := 'de ' + 'março';
  4:QRLMes.Caption := 'de ' + 'abril';
  5:QRLMes.Caption := 'de ' + 'maio';
  6:QRLMes.Caption := 'de ' + 'junho';
  7:QRLMes.Caption := 'de ' + 'julho';
  8:QRLMes.Caption := 'de ' + 'agosto';
  9:QRLMes.Caption := 'de ' + 'setembro';
  10:QRLMes.Caption := 'de ' + 'outubro';
  11:QRLMes.Caption := 'de ' + 'novembro';
  12:QRLMes.Caption := 'de ' + 'dezembro';
  end;
  QrlDiaMesAno.Caption := QryRelatorioNO_CIDADE.AsString + ', ' + IntToStr(dia) + ' ' + QRLMes.Caption + ' de ' + IntToStr(ano);

  QRLDiretor.Caption := 'Diretor(a): ' + QryRelatorioNO_COL.AsString;
  QrlMatrDir.Caption := 'Matrícula: ' + FormatMaskText('00.000-0;0', QryRelatorioCO_MAT_COL.AsString);

end;

procedure TFrmRelBoletimAluno.QRGroup1AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  QRLResultadoFinal.Caption := 'APROVADO';
  QRLNota1.Caption := '-';
  QRLNota2.Caption := '-';
  QRLNota3.Caption := '-';
  QRLNota4.Caption := '-';
  QRLFalta1.Caption := '-';
  QRLFalta2.Caption := '-';
  QRLFalta3.Caption := '-';
  QRLFalta4.Caption := '-';
  QRLFaltTotal.Caption := '-';
  QRLMedia.Caption := '-';
end;

procedure TFrmRelBoletimAluno.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  ctMat := 1;
end;

procedure TFrmRelBoletimAluno.QRGroup1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorio.fieldByName('co_alu_cad').IsNull then
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.fieldByName('co_alu_cad').AsString);
end;

procedure TFrmRelBoletimAluno.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  qrlMat1.Caption := '';
  qrlMat2.Caption := '';
  qrlMat3.Caption := '';
  qrlMat4.Caption := '';
  qrlMat5.Caption := '';
  qrlMat6.Caption := '';
  qrlMat7.Caption := '';
  qrlMat8.Caption := '';
  qrlMat9.Caption := '';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelBoletimAluno]);

end.
