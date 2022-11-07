unit U_FrmRelFinalAlunos;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  QRExport;

type
  TFrmRelFinalAlunos = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLParametro: TQRLabel;
    QRLM1: TQRLabel;
    QRLM2: TQRLabel;
    QRLM3: TQRLabel;
    QRLM4: TQRLabel;
    QRLM5: TQRLabel;
    QRLM6: TQRLabel;
    QRLM7: TQRLabel;
    QRLM8: TQRLabel;
    QRLD1: TQRLabel;
    QRLD2: TQRLabel;
    QRLD3: TQRLabel;
    QRLD4: TQRLabel;
    QRLD5: TQRLabel;
    QRLD6: TQRLabel;
    QRLD7: TQRLabel;
    QRLD8: TQRLabel;
    QRLRes: TQRLabel;
    QryRelatorioNO_ALU: TStringField;
    QryRelatorioCO_ALU: TIntegerField;
    QryRelatorioCO_ALU_CAD: TStringField;
    QRLCoAluno: TQRLabel;
    QRLNomAluno: TQRLabel;
    QRLNumALuno: TQRLabel;
    QRBand1: TQRBand;
    QRMLegenda1: TQRMemo;
    QRLLeg: TQRLabel;
    QRMLegenda2: TQRMemo;
    QryRelatorioCO_SEXO_ALU: TStringField;
    QRDBTSexAluno: TQRDBText;
    QRLResult: TQRLabel;
    QRLD9: TQRLabel;
    QRLD10: TQRLabel;
    QRLM9: TQRLabel;
    QRLM10: TQRLabel;
    QRShape1: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel5: TQRLabel;
    QrlTotal: TQRLabel;
    QRLabel6: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLabel7: TQRLabel;
    QRLNuNIS: TQRLabel;
    QRLLegenda: TQRLabel;
    QRLTMED: TQRLabel;
    QRLMed: TQRLabel;
    QRLDescMedSerTur: TQRLabel;
    QRLMediaSerTur: TQRLabel;
    QryRelatorioNU_NIS: TBCDField;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QryRelatorioNO_TURMA: TStringField;
    QryRelatorioCO_TUR: TIntegerField;
    QryRelatorioCO_STA_APROV: TStringField;
    QRLD11: TQRLabel;
    QRLM11: TQRLabel;
    QRLNoAlu: TQRLabel;
    QryRelatorioCO_STA_APROV_FREQ: TStringField;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRLNumALunoPrint(sender: TObject; var Value: String);
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormActivate(Sender: TObject);
    procedure QuickRep1AfterPreview(Sender: TObject);
    procedure QuickRep1StartPage(Sender: TCustomQuickRep);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    numMateria,numNotas : integer;
    mediaSerTur : double;
  public
    { Public declarations }
    CodigoCurso, CodigoTurma, AnoCurso, CodigoModulo, CodigoEmpresa, TipoEnsino: String;
    NumSemestre,numAluno: Integer;
  end;

var
  FrmRelFinalAlunos: TFrmRelFinalAlunos;
  flgOco : Boolean;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelFinalAlunos.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
  var
  SqlString: String;
begin
  inherited;
  QRMLegenda1.Lines.Clear;
  QRMLegenda2.Lines.Clear;
  QrlTotal.Caption := '0';
  numMateria := 0;

  if flgOco = false then
  begin
    QRLD1.Caption :=  '-';
    QRLD2.Caption :=  '-';
    QRLD3.Caption :=  '-';
    QRLD4.Caption :=  '-';
    QRLD5.Caption :=  '-';
    QRLD6.Caption :=  '-';
    QRLD7.Caption :=  '-';
    QRLD8.Caption :=  '-';
    QRLD9.Caption :=  '-';
    QRLD10.Caption :=  '-';
    QRLD11.Caption :=  '-';
    QRLM1.Caption :=  '-';
    QRLM2.Caption :=  '-';
    QRLM3.Caption :=  '-';
    QRLM4.Caption :=  '-';
    QRLM5.Caption :=  '-';
    QRLM6.Caption :=  '-';
    QRLM7.Caption :=  '-';
    QRLM8.Caption :=  '-';
    QRLM9.Caption :=  '-';
    QRLM10.Caption :=  '-';
    QRLM11.Caption :=  '-';
    QRLResult.Caption := '-';
    QRLMed.Caption := '-';
    QRLLegenda.Caption := '';
    //QRLTrans.Visible := false;
    //QRLTrans.Caption := '';
    numAluno := 1;

    with DM.QrySql do
    begin
      SqlString := ' Select cm.NO_MATeria, cm.no_SIGLa_MATeria,MA.CO_MAT ' +
                   ' from TB02_MATERIA MA ' +
                   ' join TB43_GRD_CURSO GC on ma.co_mat = gc.co_mat ' +
                   ' join tb107_cadmaterias cm on ma.id_materia = cm.id_materia and ma.co_emp = cm.co_emp'+
                   ' where GC.CO_EMP = MA.CO_EMP                  ' +
                   '   AND GC.CO_EMP = MA.CO_EMP                  ' +
                   ' AND GC.CO_CUR = ' + CodigoCurso +
                   ' AND GC.CO_EMP = ' + CodigoEmpresa +
                   ' AND GC.CO_ANO_GRADE = ' + AnoCurso +
                   ' ORDER BY cm.no_materia ';

      SQL.Text := SqlString;
      Open;

      if not IsEmpty then
      begin
        first;

        while not Eof do
        begin
          Case DM.QrySql.RecNo of
            1 : if Fields.FieldByName('no_SIGLa_MATeria').AsString <> '' then  begin QRLD1.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            2 : begin QRLD2.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            3 : begin QRLD3.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            4 : begin QRLD4.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            5 : begin QRLD5.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda1.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            6 : begin QRLD6.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            7 : begin QRLD7.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            8 : begin QRLD8.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            9 : begin QRLD9.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            10 : begin QRLD10.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
            11 : begin QRLD11.Caption := Fields.FieldByName('no_SIGLa_MATeria').AsString;
            QRMLegenda2.Lines.Add(FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('NO_MATeria').AsString);
            QRLLegenda.Caption := QRLLegenda.Caption + FieldByName('no_SIGLa_MATeria').AsString + ' - ' + FieldByName('nO_MATeria').AsString + ' / ';
            numMateria := numMateria + 1;
            end;
          end;
          Next;
        end;
     end;
  end;
 flgOco := True;
 end;
 numAluno := 1;
end;

procedure TFrmRelFinalAlunos.QRLNumALunoPrint(sender: TObject;
  var Value: String);
begin
  inherited;
  if not(Sender is TQRLabel) then
    Exit;

  Value := IntToStr(numAluno);
  numAluno := numAluno + 1;
end;

procedure TFrmRelFinalAlunos.DetailBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
  var
  mediaMateria, mediaAprovacao : Double;
  flagRpr,flagTrans,flagVazio,flagOco : Boolean;
  qtdMat : Integer;
begin
  inherited;
  if not QryRelatorioNO_ALU.IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorioNO_ALU.AsString)
  else
    QRLNoAlu.Caption := '-';

  if not QryRelatorioNU_NIS.IsNull then
    QRLNuNis.Caption := FormatFloat('000000000000;1',QryRelatorioNU_NIS.AsFloat)
  else
    QRLNuNis.Caption := '-';

  QrlMatricula.Caption := FormatMaskText('00.000.000000;0', QryRelatorioCO_ALU_CAD.AsString);
  qtdMat := 0;
  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) +1);

  QRLM1.Caption :=  '-';
  QRLM2.Caption :=  '-';
  QRLM3.Caption :=  '-';
  QRLM4.Caption :=  '-';
  QRLM5.Caption :=  '-';
  QRLM6.Caption :=  '-';
  QRLM7.Caption :=  '-';
  QRLM8.Caption :=  '-';
  QRLM9.Caption :=  '-';
  QRLM10.Caption :=  '-';
  QRLM11.Caption :=  '-';
  QRLMed.Caption := '0';
  QRLResult.Caption := '-';
  flagOco := false;

  with DM.QrySql do
  begin
    Close;

    SQL.Text := ' select MM.CO_SIT_MAT, H.*,C.MED_FINAL_CUR ' +
                ' from TB079_HIST_ALUNO H, TB07_ALUNO A, TB08_MATRCUR MM, TB02_MATERIA MA,    ' +
                '      TB01_CURSO C,TB107_CADMATERIAS CM                                      ' +
                ' where H.CO_EMP = MM.CO_EMP                                                  ' +
                '   AND H.CO_CUR = MM.CO_CUR                                                  ' +
                '   AND H.CO_ALU = MM.CO_ALU                                                  ' +
                '   AND H.CO_EMP = A.CO_EMP                                                   ' +
                '   AND H.CO_ALU = A.CO_ALU                                                   ' +
                '   AND H.CO_EMP = MA.CO_EMP                                                  ' +
                '   AND H.CO_MAT = MA.CO_MAT                                                  ' +
                '   AND H.CO_EMP = ' + CodigoEmpresa +
                '   AND C.CO_EMP = ' + CodigoEmpresa +
                '   AND H.CO_CUR = ' + CodigoCurso +
                '   AND H.CO_MODU_CUR = ' + CodigoModulo +
                '   AND C.CO_CUR = H.CO_CUR ' +
                '   AND MA.ID_MATERIA = CM.ID_MATERIA ' +
                '   AND H.CO_TUR = ' + QryRelatorioCO_TUR.AsString +
                //'   AND MM.CO_SIT_MAT = ' + QuotedStr('A') +
                ' AND mm.co_sit_mat in (' + quotedstr('A') + ',' + quotedstr('T') + ',' + quotedstr('F') +')' +
                //'   AND H.CO_ANO_MES_MAT = ' + QuotedStr(AnoCurso) +
                '   AND H.CO_ANO_REF = ' + QuotedStr(AnoCurso) +
                //'   AND H.CO_MODU_CUR = ' + CodigoModulo +
                //'   AND H.NU_SEM_LET = ' + IntToStr(NumSemestre) +
                '   AND H.CO_ALU = ' + QryRelatorioCO_ALU.AsString +
                '   ORDER BY CM.NO_MATERIA';
    Open;

    flagVazio := false;
    if IsEmpty then
    begin
      flagVazio := true;
      flagOco := true;
    end;

    flagRpr := false;
    flagTrans := false;

    while not Eof do
    begin
      if Fields.FieldByName('CO_SIT_MAT').AsString = 'R' then
      begin
        // QRLTrans.Caption := '********** TRANSFERIDO **********';
        flagTrans := true;
      end
      else
      begin
        Case DM.QrySql.RecNo of
          1 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                      QRLM1.Caption := ' -';
                      flagVazio := true;
                      flagOco := true;
              end
              else
              begin
                QRLM1.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                  // deixa nota menor que 5 vermelha -> Diego Nobre 23/01/2009;
                  if StrToFloat(QRLM1.Caption) >= 5 then
                  begin
                    QRLM1.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM1.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if ( mediaMateria < mediaAprovacao) then
                  flagRpr := true;
              end;
            end;

          end;
          2 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM2.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM2.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM2.Caption) >= 5 then
                  begin
                    QRLM2.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM2.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;
         end;
          3 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM3.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM3.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM3.Caption) >= 5 then
                  begin
                    QRLM3.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM3.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;
          end;
          4 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM4.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM4.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM4.Caption) >= 5 then
                  begin
                    QRLM4.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM4.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          5 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM5.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM5.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM5.Caption) >= 5 then
                  begin
                    QRLM5.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM5.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;
          end;
          6 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM6.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM6.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM6.Caption) >= 5 then
                  begin
                    QRLM6.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM6.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          7 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM7.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM7.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM7.Caption) >= 5 then
                  begin
                    QRLM7.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM7.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if (( mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          8 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM8.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM8.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM8.Caption) >= 5 then
                  begin
                    QRLM8.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM8.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if ((mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          9 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM9.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM9.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM9.Caption) >= 5 then
                  begin
                    QRLM9.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM9.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if ((mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          10 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                QRLM10.Caption := ' -';
                flagOco := true;
              end
              else
              begin
                QRLM10.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                  if StrToFloat(QRLM10.Caption) >= 5 then
                  begin
                    QRLM10.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM10.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if ((mediaMateria < mediaAprovacao)) then
                  flagRpr := true;
              end;
            end;

          end;
          11 :
          begin
            if TipoEnsino = 'ES' then
            begin
              if (Fields.FieldByName('VL_MEDIA_FINAL').IsNull) then
              begin
                      QRLM11.Caption := ' -';
                      flagVazio := true;
                      flagOco := true;
              end
              else
              begin
                QRLM11.Caption := FormatFloat('#0.00', Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                qtdMat := qtdMat + 1;
                //numNotas := numNotas + 1;
                //mediaSerTur := mediaSerTur + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                QRLMed.Caption := FormatFloat('#0.00',StrToFLoat(QRLMed.Caption) + Fields.FieldByName('VL_MEDIA_FINAL').AsFloat);
                  // deixa nota menor que 5 vermelha -> Diego Nobre 23/01/2009;
                  if StrToFloat(QRLM11.Caption) >= 5 then
                  begin
                    QRLM11.Font.Color := clBlack;
                  end
                  else
                  begin
                    QRLM11.Font.Color := clRed;
                  end;

                mediaMateria := Fields.FieldByName('VL_MEDIA_FINAL').AsFloat;
                mediaAprovacao := Fields.FieldByName('MED_FINAL_CUR').AsFloat;
                if ( mediaMateria < mediaAprovacao) then
                  flagRpr := true;
              end;
            end;

          end;

        end;
      Next;
    end;
  end;

  end;

  //Deixar o relatório zebrado
  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;
  //Escrever AP ou REP no result
  //Alterei - André Vinagre - 08/09/2008
  if TipoEnsino = 'ES' then
  begin
    if (not QryRelatorioCO_STA_APROV.IsNull) and (not QryRelatorioCO_STA_APROV_FREQ.IsNull) then
    begin
      if (QryRelatorioCO_STA_APROV.AsString = 'R') or (QryRelatorioCO_STA_APROV_FREQ.AsString = 'R') then
        QRLResult.Caption := 'REPROVADO'
      else
        QRLResult.Caption := 'APROVADO';
    end
    else
      QRLResult.Caption := '-';
  end;
  //if flagTrans then
  //QRLTrans.Visible := true
 // else
  //QRLTrans.Visible := false;
  if flagVazio then
  begin
    QRLResult.Caption := '-';
  end;

  if flagOco then
  begin
    QRLMed.Caption := '-';
    QRLMed.Font.Color := clBlack;
  end
  else
  begin
    if TipoEnsino = 'ES' then
    begin
      if qtdMat <> 0 then
      begin
        mediaSerTur := mediaSerTur  + StrToFloat(QRLMed.Caption) /qtdMat;
        QRLMed.Caption := FormatFloat('#0.00',StrToFloat(QRLMed.Caption) /qtdMat);
        numNotas := numNotas + 1;
      end;
      if StrToFloat(QRLMed.Caption) >= 5 then
      begin
        QRLMed.Font.Color := clBlack;
      end
      else
      begin
        QRLMed.Font.Color := clRed;
      end;
    end;
    
  end;
end;

procedure TFrmRelFinalAlunos.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  inherited;
  flgOco := false;
end;

procedure TFrmRelFinalAlunos.FormActivate(Sender: TObject);
begin
  inherited;
  flgOco := false;
  numNotas := 0;
  mediaSerTur := 0;
end;

procedure TFrmRelFinalAlunos.QuickRep1AfterPreview(Sender: TObject);
begin
  inherited;
  flgOco := false;
end;

procedure TFrmRelFinalAlunos.QuickRep1StartPage(Sender: TCustomQuickRep);
begin
  inherited;
  flgOco := false;
end;

procedure TFrmRelFinalAlunos.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  if TipoEnsino = 'ES' then
  begin
    if numNotas > 0 then
      QRLMediaSerTur.Caption := FloatToStrF(mediaSerTur/numNotas,ffNumber,10,2);
  end;

  if TipoEnsino = 'EP' then
  begin
    QRLMediaSerTur.Caption := '-';
    QRLTMED.Caption := '';
    QRLDescMedSerTur.Enabled := False;
    QRLMediaSerTur.Enabled := False;
  end;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelFinalAlunos]);

end.
