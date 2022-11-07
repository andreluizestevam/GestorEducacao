unit U_FrmRelAnaliticoResAvaliacao;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QRCtrls, DB, ADODB, QuickRpt, ExtCtrls;

type
  TFrmRelAnaliticoResAvaliacao = class(TFrmRelTemplate)
    QRDBText1: TQRDBText;
    QRLSerie: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel8: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QryRelatorioCO_TIPO_AVAL: TIntegerField;
    QryRelatorioCO_TITU_AVAL: TIntegerField;
    QryRelatorioNO_TITU_AVAL: TStringField;
    QryRelatorioNO_TIPO_AVAL: TStringField;
    QRLabel9: TQRLabel;
    QRGroup2: TQRGroup;
    QRDBText2: TQRDBText;
    QRSubDetail2: TQRSubDetail;
    QryQuestaoTit: TADOQuery;
    QryQuestaoTitCO_TIPO_AVAL: TIntegerField;
    QryQuestaoTitCO_TITU_AVAL: TIntegerField;
    QryQuestaoTitNU_QUES_AVAL: TIntegerField;
    QryQuestaoTitDE_QUES_AVAL: TStringField;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRLabel3: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape7: TQRShape;
    QRShape9: TQRShape;
    QRShape12: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRDBText4: TQRDBText;
    QRShape8: TQRShape;
    QRShape10: TQRShape;
    QRShape14: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QrlSRQuest: TQRLabel;
    QRBand1: TQRBand;
    QrlTotFormulario: TQRLabel;
    QrlMinQue: TQRLabel;
    QrlMaxQue: TQRLabel;
    QrlMediaQ: TQRLabel;
    QrlConceito: TQRLabel;
    QRImage1: TQRImage;
    GroupFooterBand1: TQRBand;
    QRLabel16: TQRLabel;
    QrlSR: TQRLabel;
    QrlMediaTit: TQRLabel;
    QrlConceitoTit: TQRLabel;
    QRShape11: TQRShape;
    QRLabel17: TQRLabel;
    QrlNrPesq: TQRLabel;
    QrlProfessor: TQRLabel;
    QRLabel1: TQRLabel;
    QRLPage: TQRLabel;
    QrlDataInicial: TQRLabel;
    QrlDataFinal: TQRLabel;
    QrlNoCurso: TQRLabel;
    QrlNoTurma: TQRLabel;
    QrlNoDisciplina: TQRLabel;
    QrlNoProfessor: TQRLabel;
    procedure QRGroup2AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRSubDetail2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure GroupFooterBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    MediaTit, MediaGeral: Double;
    TotSR: Integer;
  public
    { Public declarations }
    codModulo,codDisciplina,codCurso,codTurma,codigoEmpresa,strP_CO_PESQ_AVAL : String;
  end;

var
  FrmRelAnaliticoResAvaliacao: TFrmRelAnaliticoResAvaliacao;

implementation

uses U_DataModuleSGE, DBCtrls, Mask,
  ToolEdit;

{$R *.dfm}

procedure TFrmRelAnaliticoResAvaliacao.QRGroup2AfterPrint(Sender: TQRCustomBand;
  BandPrinted: Boolean);
begin
  inherited;
  with QryQuestaoTit do
  begin
    Close;
    Parameters.ParamByName('P_CO_TIPO_AVAL').Value := QryRelatorioCO_TIPO_AVAL.Value;
    Parameters.ParamByName('P_CO_TITU_AVAL').Value := QryRelatorioCO_TITU_AVAL.Value;
    Open;
  end;

  MediaTit := 0;
  TOTSR := 0;
   MediaGeral := 0;
end;

procedure TFrmRelAnaliticoResAvaliacao.QRGroup2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
//var
//  TOTSR: Integer;
begin
  inherited;
  exit;

  { Recupera o total de questões sem responder do titulo }
  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    Sql.Text := 'SELECT DISTINCT NU_SEQ_FORM_AVAL FROM TB70_ITEM_AVAL ' +
                ' WHERE CO_PESQ_AVAL = ' + QrlNrPesq.Caption +
                ' AND   CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                ' AND   CO_TITU_AVAL = ' + QryRelatorioCO_TITU_AVAL.AsString +
                ' AND CO_EMP = ' + codigoEmpresa;
    Open;
    First;

    while not eof do
    begin
      DM.QrySql2.Close;
      DM.QrySql2.Sql.Clear;
      DM.QrySql2.Sql.Text := 'SELECT COUNT(DISTINCT Q.NU_QUES_AVAL) TOTQUES ' +
                                        'FROM TB71_QUEST_AVAL Q  ' +
                                        'WHERE Q.CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                                        ' AND  Q.CO_TITU_AVAL = ' + QryRelatorioCO_TITU_AVAL.AsString + ' AND ' +
                                        '      Q.NU_QUES_AVAL NOT IN (SELECT NU_QUES_AVAL FROM TB70_ITEM_AVAL ' +
                                        '                             WHERE CO_PESQ_AVAL = ' + strP_CO_PESQ_AVAL +
                                        '                             AND   CO_TIPO_AVAL = Q.CO_TIPO_AVAL AND ' +
                                        '                                   CO_TITU_AVAL = Q.CO_TITU_AVAL AND ' +
                                        '                                   CO_EMP = ' + codigoEmpresa;
      DM.QrySql2.Open;

      DM.QrySql2.Close;
      Next;
    end;
    Close;
  end;

end;

procedure TFrmRelAnaliticoResAvaliacao.QRSubDetail2BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  TOTSRQ: Integer;
  vvvNota: String;
  SqlString1, SqlString2, SqlString3: String;
begin
  inherited;
  { Recupera o total de questões sem responder }
  if QryQuestaoTitDE_QUES_AVAL.IsNull then
    QRImage1.Enabled := false
  else
    QRImage1.Enabled := true;
    
  TOTSRQ := 0;
  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    SqlString1 := ' SET LANGUAGE PORTUGUESE ' +
                  ' SELECT DISTINCT B.CO_COL FROM TB70_ITEM_AVAL A, TB78_PESQ_AVAL B' +
                  ' WHERE A.CO_PESQ_AVAL = B.CO_PESQ_AVAL ' +
                  ' AND   A.CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                  ' AND   A.CO_TITU_AVAL = ' + QryRelatorioCO_TITU_AVAL.AsString +
                  ' AND A.CO_EMP = ' + codigoEmpresa +
                  ' AND A.CO_EMP = B.CO_EMP ';

    if codCurso <> 'T' then
      SqlString1 := SqlString1 +' AND B.CO_CUR = ' + codCurso;

    if codModulo <> 'T' then
      SqlString1 := SqlString1 +' AND B.CO_MODU_CUR = ' + codModulo;

    if codTurma <> 'T' then
      SqlString1 := SqlString1 + ' AND B.CO_TUR = ' + codTurma;

    if codDisciplina <> 'T' then
      SqlString1 := SqlString1 + ' AND B.CO_MAT = ' + codDisciplina;

    if QrlProfessor.Caption <> 'T' then
      SqlString1 := SqlString1 + ' AND B.CO_COL = ' + QrlProfessor.Caption; // FrmParamRelResultadoAvaliacao.edtCodProf.Text;

    if (QrlDataInicial.Caption <> '') and (QrlDataFinal.Caption <> '') then
    begin
      SqlString1 := SqlString1 + ' AND (B.DT_AVAL BETWEEN ' + '''' + QrlDataInicial.Caption + '''' +
                                 '       AND ' + '''' + QrlDataFinal.Caption + '''' + ')';
    end;

    Sql.Text := SqlString1;
    Open;

    if not IsEmpty then
    begin
      with DM.QrySql2 do
      begin
        Close;
        SQL.Clear;
        SQl.Text := 'select no_col from tb03_colabor '+
                    'where co_col = ' + DM.QrySql.FieldByName('CO_COL').AsString +
                    ' and co_emp = ' + codigoEmpresa;
        Open;

        if not DM.QrySql2.IsEmpty then
        begin
          QrlNoProfessor.Caption := DM.QrySql2.FieldByName('NO_COL').AsString;
        end
        else
          QrlNoProfessor.Caption := '';
      end;

    end
    else
    begin
      QrlNoProfessor.Caption := '';
    end;
    First;

    while not eof do
    begin
      if not QryQuestaoTitNU_QUES_AVAL.IsNull then
      begin
        DM.QrySql2.Close;
        DM.QrySql2.Sql.Clear;
        SqlString2 := 'SELECT COUNT(DISTINCT Q.NU_QUES_AVAL) TOTQUES ' +
                                          'FROM TB71_QUEST_AVAL Q  ' +
                                          'WHERE Q.CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                                          ' AND  Q.CO_TITU_AVAL = ' + QryRelatorioCO_TITU_AVAL.AsString + ' AND ' +
                                          '      Q.NU_QUES_AVAL = ' + QryQuestaoTitNU_QUES_AVAL.AsString + ' AND ' +
                                          '      Q.NU_QUES_AVAL NOT IN (SELECT A.NU_QUES_AVAL FROM TB70_ITEM_AVAL A, TB78_PESQ_AVAL B' +
                                          '                             WHERE A.CO_PESQ_AVAL = B.CO_PESQ_AVAL ' +
                                          '                              AND  A.CO_TIPO_AVAL = Q.CO_TIPO_AVAL AND ' +
                                          '                                   A.CO_TITU_AVAL = Q.CO_TITU_AVAL AND ' +
                                          '                                   A.CO_EMP = B.CO_EMP';
        if codCurso <> 'T' then
          SqlString2 := SqlString2 +' AND B.CO_CUR = ' + codCurso;

        if codModulo <> 'T' then
          SqlString2 := SqlString2 +' AND B.CO_MODU_CUR = ' + codModulo;

        if codTurma <> 'T' then
          SqlString2 := SqlString2 + ' AND B.CO_TUR = ' + codTurma;

        if codDisciplina <> 'T' then
          SqlString2 := SqlString2 + ' AND B.CO_MAT = ' + codDisciplina;

        if QrlProfessor.Caption <> 'T' then
          SqlString2 := SqlString2 + ' AND B.CO_COL = ' + QrlProfessor.Caption; // FrmParamRelResultadoAvaliacao.edtCodProf.Text;

        DM.QrySql2.Sql.Text := SqlString2 + ')';
        DM.QrySql2.Open;

        TOTSRQ := TOTSRQ + DM.QrySql2.FieldByName('TOTQUES').AsInteger;
        DM.QrySql2.Close;
      end;
      Next;
    end;
    Close;
  end;

  if not QryQuestaoTitNU_QUES_AVAL.IsNull then
  begin
    QrlSRQuest.Caption := IntToStr(TOTSRQ);
    TOTSR := TOTSR + TOTSRQ;
  end
  else
  begin
    QrlSRQuest.Caption := '-';
  end;

  if not QryQuestaoTitNU_QUES_AVAL.IsNull then
  begin
    { Recupera a menor e maior nota da questão e calcula a média da questão }
    with DM.QrySql do
    begin
      Close;
      Sql.Clear;
      SqlString3 := 'SELECT MIN(A.VL_NOT_AVAL) MINNOTA, MAX(A.VL_NOT_AVAL) MAXNOTA, ' +
                    '       SUM(A.VL_NOT_AVAL) / COUNT(*) MEDIA ' +
                    'FROM TB70_ITEM_AVAL A, TB78_PESQ_AVAL B ' +
                    ' WHERE A.CO_PESQ_AVAL = B.CO_PESQ_AVAL ' +
                    ' AND   A.CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                    ' AND   A.CO_TITU_AVAL = ' + QryRelatorioCO_TITU_AVAL.AsString +
                    ' AND   A.NU_QUES_AVAL = ' + QryQuestaoTitNU_QUES_AVAL.AsString +
                    ' AND A.CO_EMP = B.CO_EMP ' +
                    ' AND A.CO_EMP = ' + codigoEmpresa;

      if codCurso <> 'T' then
        SqlString3 := SqlString3 +' AND B.CO_CUR = ' + codCurso;

      if codModulo <> 'T' then
        SqlString3 := SqlString3 +' AND B.CO_MODU_CUR = ' + codModulo;

      if codTurma <> 'T' then
        SqlString3 := SqlString3 + ' AND B.CO_TUR = ' + codTurma;

      if codDisciplina <> 'T' then
        SqlString3 := SqlString3 + ' AND B.CO_MAT = ' + codDisciplina;

      if QrlProfessor.Caption <> 'T' then
        SqlString3 := SqlString3 + ' AND B.CO_COL = ' + QrlProfessor.Caption; // FrmParamRelResultadoAvaliacao.edtCodProf.Text;

      Sql.Text := SqlString3;
      Open;

      QrlMinQue.Caption := FieldByName('MINNOTA').AsString;
      QrlMaxQue.Caption := FieldByName('MAXNOTA').AsString;
      QrlMediaQ.Caption := FormatFloat('#,##0.00', FieldByName('MEDIA').AsFloat);

      MediaTit := MediaTit + FieldByName('MEDIA').AsFloat;
    end;
  end
  else
  begin
    QrlMinQue.Caption := '-';
    QrlMaxQue.Caption := '-';
    QrlMediaQ.Caption := '-';
  end;

  { Recupera o conceito da questão }
  if QrlMediaQ.Caption <> '-' then
  begin
    vvvNota := QrlMediaQ.Caption;
    while Pos(',', vvvNota) > 0 do
      vvvNota[Pos(',', vvvNota)] := '.';

    QrlConceito.Caption := '-----';
    DM.QrySql2.Close;

    { Soma a média geral }

    MediaGeral := MediaGeral + StrToFloat(QrlMediaQ.Caption);
  end;

end;

procedure TFrmRelAnaliticoResAvaliacao.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  SqlString: String;
begin
  with DM.QrySql do
  begin
    Close;
    Sql.Clear;
    SqlString := 'SELECT COUNT(DISTINCT PA.CO_PESQ_AVAL) TOTFORM ' +
                ' FROM TB78_PESQ_AVAL PA, TB70_ITEM_AVAL IA ' +
                ' where PA.CO_PESQ_AVAL = IA.CO_PESQ_AVAL AND ' +
                '       PA.CO_TIPO_AVAL = IA.CO_TIPO_AVAL AND ' +
                '       PA.CO_TIPO_AVAL = ' + QryRelatorioCO_TIPO_AVAL.AsString +
                ' AND PA.CO_EMP = IA.CO_EMP ' +
                ' AND PA.CO_EMP = ' + codigoEmpresa;

    if codCurso <> 'T' then
      SqlString := SqlString +' AND PA.CO_CUR = ' + codCurso;

    if codModulo <> 'T' then
      SqlString := SqlString +' AND PA.CO_MODU_CUR = ' + codModulo;

    if codTurma <> 'T' then
      SqlString := SqlString + ' AND PA.CO_TUR = ' + codTurma;

    if codDisciplina <> 'T' then
      SqlString := SqlString + ' AND PA.CO_MAT = ' + codDisciplina;

    if QrlProfessor.Caption <> 'T' then
      SqlString := SqlString + ' AND PA.CO_COL = ' + QrlProfessor.Caption; // FrmParamRelResultadoAvaliacao.edtCodProf.Text;

    Sql.Text :=  SqlString;
    Open;

    QrlTotFormulario.Caption := 'Qtde Formulario: ' + FieldByname('TOTFORM').AsString;
    Close;
  end;

end;

procedure TFrmRelAnaliticoResAvaliacao.GroupFooterBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  vvvNota: String;
begin

  if QryQuestaoTit.RecordCount > 0 then
  begin
    { Recupera o conceito do titulo }
    vvvNota := FloatToStr(MediaTit / QryQuestaoTit.RecordCount);
    while Pos(',', vvvNota) > 0 do
      vvvNota[Pos(',', vvvNota)] := '.';

    QrlMediaTit.Caption := FormatFloat('#,##0.00', MediaTit / QryQuestaoTit.RecordCount);
    QrlConceitoTit.Caption := '-----';
    DM.QrySql2.Close;

    QrlSR.Caption := IntToStr(TotSR);
  end
  else
  begin
    QrlConceitoTit.Caption := '-----';
    QrlMediaTit.Caption := '-';
    QrlSR.Caption := '-';
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelAnaliticoResAvaliacao]);

end.
