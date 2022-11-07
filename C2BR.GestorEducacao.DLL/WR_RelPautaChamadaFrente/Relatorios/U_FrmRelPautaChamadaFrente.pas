unit U_FrmRelPautaChamadaFrente;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  StdCtrls, QrAngLbl;

type
  TFrmRelPautaChamadaFrente = class(TFrmRelTemplate)
    QRDBText2: TQRDBText;
    QRDBText3: TQRDBText;
    QRDBText4: TQRDBText;
    QRDBText5: TQRDBText;
    QRLabel1: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel11: TQRLabel;
    QRBand1: TQRBand;
    QRLabel22: TQRLabel;
    QRLabel4: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel10: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel13: TQRLabel;
    QrlN1: TQRLabel;
    QrlAluno1: TQRLabel;
    QrlAluno2: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel18: TQRLabel;
    QrlAluno3: TQRLabel;
    QRLabel20: TQRLabel;
    QrlAluno4: TQRLabel;
    QrlAluno5: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel30: TQRLabel;
    QrlAluno10: TQRLabel;
    QrlAluno9: TQRLabel;
    QrlAluno8: TQRLabel;
    QrlAluno7: TQRLabel;
    QrlAluno6: TQRLabel;
    QRLabel36: TQRLabel;
    QrlAluno11: TQRLabel;
    QrlAluno12: TQRLabel;
    QrlAluno13: TQRLabel;
    QrlAluno14: TQRLabel;
    QrlAluno15: TQRLabel;
    QrlAluno16: TQRLabel;
    QrlAluno17: TQRLabel;
    QrlAluno18: TQRLabel;
    QrlAluno19: TQRLabel;
    QrlAluno20: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel48: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel50: TQRLabel;
    QRLabel51: TQRLabel;
    QRLabel52: TQRLabel;
    QRLabel53: TQRLabel;
    QRLabel54: TQRLabel;
    QRLabel55: TQRLabel;
    QRLabel56: TQRLabel;
    QrlAluno21: TQRLabel;
    QrlAluno22: TQRLabel;
    QrlAluno23: TQRLabel;
    QrlAluno24: TQRLabel;
    QrlAluno25: TQRLabel;
    QrlAluno26: TQRLabel;
    QrlAluno27: TQRLabel;
    QrlAluno28: TQRLabel;
    QrlAluno29: TQRLabel;
    QrlAluno30: TQRLabel;
    QRLabel67: TQRLabel;
    QRLabel68: TQRLabel;
    QRLabel69: TQRLabel;
    QRLabel70: TQRLabel;
    QRLabel71: TQRLabel;
    QRLabel72: TQRLabel;
    QRLabel73: TQRLabel;
    QRLabel74: TQRLabel;
    QRLabel75: TQRLabel;
    QrlAluno45: TQRLabel;
    QrlAluno44: TQRLabel;
    QrlAluno43: TQRLabel;
    QrlAluno42: TQRLabel;
    QrlAluno41: TQRLabel;
    QrlAluno40: TQRLabel;
    QrlAluno39: TQRLabel;
    QrlAluno38: TQRLabel;
    QrlAluno37: TQRLabel;
    QrlAluno36: TQRLabel;
    QrlAluno35: TQRLabel;
    QrlAluno34: TQRLabel;
    QrlAluno33: TQRLabel;
    QrlAluno32: TQRLabel;
    QrlAluno31: TQRLabel;
    QRLabel91: TQRLabel;
    QRLabel92: TQRLabel;
    QRLabel93: TQRLabel;
    QRLabel94: TQRLabel;
    QRLabel95: TQRLabel;
    QRLabel96: TQRLabel;
    QRLabel97: TQRLabel;
    QRLabel98: TQRLabel;
    QRLabel99: TQRLabel;
    QRLabel100: TQRLabel;
    QRLabel101: TQRLabel;
    QRLabel102: TQRLabel;
    QRLabel103: TQRLabel;
    QRLabel104: TQRLabel;
    QRLabel105: TQRLabel;
    QryRelatorioNO_CUR: TStringField;
    QryRelatorioDE_MODU_CUR: TStringField;
    QryRelatorioCO_MAT: TAutoIncField;
    QryRelatorioCO_CUR: TAutoIncField;
    QryRelatorioNO_SIGLA_MATERIA: TStringField;
    QryRelatorioCO_TUR: TAutoIncField;
    QryRelatorioCO_ANO_MES_MAT: TStringField;
    QryRelatorioCO_MODU_CUR: TAutoIncField;
    QryRelatorioNO_TURMA: TStringField;
    QryRelatorioNO_MATERIA: TStringField;
    QRLTitMesReferencia: TQRLabel;
    QRLMesReferencia: TQRLabel;
    QRShape109: TQRShape;
    QRLProfessor: TQRLabel;
    QRLabel8: TQRLabel;
    QryProfessor: TADOQuery;
    QryProfessorNO_COL: TStringField;
    QryProfessorCO_MAT_COL: TStringField;
    QRLabel14: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel21: TQRLabel;
    QRLabel24: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel33: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel38: TQRLabel;
    QRLabel39: TQRLabel;
    QRLabel40: TQRLabel;
    QRLabel41: TQRLabel;
    QRLabel42: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel45: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel57: TQRLabel;
    QRLabel58: TQRLabel;
    QRLabel59: TQRLabel;
    QRLabel60: TQRLabel;
    QRLabel61: TQRLabel;
    QRLabel62: TQRLabel;
    QRLabel63: TQRLabel;
    QRLabel64: TQRLabel;
    QRLabel65: TQRLabel;
    QRLabel66: TQRLabel;
    QRLabel76: TQRLabel;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
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
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    QRShape34: TQRShape;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape40: TQRShape;
    QRShape41: TQRShape;
    QRShape42: TQRShape;
    QRShape43: TQRShape;
    QRShape44: TQRShape;
    QRShape45: TQRShape;
    QRShape46: TQRShape;
    QRShape47: TQRShape;
    QRShape48: TQRShape;
    QRShape49: TQRShape;
    QRShape50: TQRShape;
    QRShape51: TQRShape;
    QRShape52: TQRShape;
    QRShape53: TQRShape;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
    QRShape59: TQRShape;
    QRShape60: TQRShape;
    QRShape61: TQRShape;
    QRShape62: TQRShape;
    QRShape63: TQRShape;
    QRShape64: TQRShape;
    QRShape65: TQRShape;
    QRShape66: TQRShape;
    QRShape67: TQRShape;
    QRShape68: TQRShape;
    QRShape69: TQRShape;
    QRShape70: TQRShape;
    QRShape71: TQRShape;
    QRShape73: TQRShape;
    QRShape74: TQRShape;
    QRShape75: TQRShape;
    QRShape76: TQRShape;
    QRShape77: TQRShape;
    QRShape78: TQRShape;
    QRShape72: TQRShape;
    QRShape79: TQRShape;
    QRShape80: TQRShape;
    QRShape81: TQRShape;
    QRLMateria: TQRLabel;
    procedure QRLabel19Print(sender: TObject; var Value: String);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    Cont : Integer;
  public
    { Public declarations }
    numMes : Integer;
    codigoEmpresa : String;
  end;

var
  FrmRelPautaChamadaFrente: TFrmRelPautaChamadaFrente;

implementation

uses U_DataModuleSGE, MaskUtils;

{$R *.dfm}

procedure TFrmRelPautaChamadaFrente.QRLabel19Print(sender: TObject;
  var Value: String);
  var
    SqlString : String;
begin
  inherited;
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SqlString := ' SELECT C.NO_COL                        ' +
                 ' FROM TB_RESPON_MATERIA P, TB03_COLABOR C ' +
                 ' WHERE P.CO_COL_RESP = C.CO_COL          ' +
                 ' AND P.CO_EMP = C.CO_EMP                ' +
                 ' AND P.CO_EMP = ' + codigoEmpresa +
                 ' AND P.CO_CUR = ' + QryRelatorioCO_CUR.AsString +
                 ' AND P.CO_TUR = ' + QryRelatorioCO_TUR.AsString +
                 ' AND P.CO_MAT = ' + QryRelatorioCO_MAT.AsString +
                 ' AND P.CO_MODU_CUR = ' + QryRelatorioCO_MODU_CUR.AsString;
    SQL.Text := SqlString;
    Open;
    Value := FieldByName('NO_COL').AsString;
  end;
end;

procedure TFrmRelPautaChamadaFrente.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
 NumAlu: Integer;
begin
  inherited;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text :=' Select A.NO_ALU,A.NU_NIRE,MM.CO_ALU_CAD, MM.CO_SIT_MAT,MM.DT_SIT_MAT ' +
               ' From TB48_GRADE_ALUNO G, TB01_CURSO C, TB02_MATERIA M, TB07_ALUNO A, ' +
               ' TB44_MODULO MO, TB06_TURMAS T, TB08_MATRCUR MM, TB107_CADMATERIAS CM ' +
               ' WHERE G.CO_EMP = C.CO_EMP ' +
               ' AND G.CO_EMP = MM.CO_EMP ' +
               ' AND G.CO_EMP = M.CO_EMP ' +
               ' AND G.CO_EMP = A.CO_EMP ' +
               ' AND G.CO_EMP = T.CO_EMP ' +
               ' AND G.CO_CUR = MM.CO_CUR ' +
               ' AND G.CO_ALU = MM.CO_ALU ' +
               ' AND G.CO_ANO_MES_MAT = MM.CO_ANO_MES_MAT ' +
               ' AND G.CO_CUR = C.CO_CUR ' +
               ' AND G.CO_MODU_CUR = MO.CO_MODU_CUR ' +
               ' AND G.CO_CUR = M.CO_CUR ' +
               ' AND G.CO_MAT = M.CO_MAT ' +
               ' AND G.CO_CUR = T.CO_CUR ' +
               ' AND G.CO_TUR = T.CO_TUR ' +
               ' AND G.CO_ALU = A.CO_ALU ' +
               ' AND M.ID_MATERIA = CM.ID_MATERIA ' +
               ' AND G.CO_EMP = ' + codigoEmpresa +
               ' AND M.CO_MAT = ' + QryRelatorioCO_MAT.AsString +
               ' AND G.CO_CUR = ' + QryRelatorioCO_CUR.AsString +
               ' AND G.CO_TUR = ' + QryRelatorioCO_TUR.AsString +
               ' AND G.CO_ANO_MES_MAT = '+ QuotedStr(QryRelatorioCO_ANO_MES_MAT.AsString) +
               '   AND G.CO_MODU_CUR = '+ QryRelatorioCO_MODU_CUR.AsString +
               ' AND G.NU_SEM_LET = ' + IntToStr(1) +
               ' AND MM.CO_SIT_MAT IN (' + QuotedStr('A') + ',' + QuotedStr ('T') + ',' + QuotedStr ('F') + ')' +
               ' ORDER BY A.NO_ALU ';
    Open;

    if not IsEmpty then
    begin
      NumAlu := 1;
      while not Eof do
      begin

        case NumAlu of
          1:
          begin
            QrlAluno1.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          2:
          begin
            QrlAluno2.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          3:
          begin
            QrlAluno3.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          4:
          begin
            QrlAluno4.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          5:
          begin
            QrlAluno5.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          6:
          begin
            QrlAluno6.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          7:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS7.Enabled := true
          else
            QRS7.Enabled := false;   }
          QrlAluno7.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          8:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS8.Enabled := true
          else
            QRS8.Enabled := false; }
          QrlAluno8.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          9:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS9.Enabled := true
          else
            QRS9.Enabled := false;}
          QrlAluno9.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          10:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS10.Enabled := true
          else
            QRS10.Enabled := false; }
          QrlAluno10.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          11:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS11.Enabled := true
          else
            QRS11.Enabled := false; }
          QrlAluno11.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          12:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS12.Enabled := true
          else
            QRS12.Enabled := false; }
          QrlAluno12.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          13:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS13.Enabled := true
          else
            QRS13.Enabled := false; }
          QrlAluno13.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          14:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS14.Enabled := true
          else
            QRS14.Enabled := false;}
          QrlAluno14.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          15:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS15.Enabled := true
          else
            QRS15.Enabled := false;}
          QrlAluno15.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          16:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS16.Enabled := true
          else
            QRS16.Enabled := false;}
          QrlAluno16.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          17:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS17.Enabled := true
          else
            QRS17.Enabled := false; }
          QrlAluno17.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          18:
          begin
         {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS18.Enabled := true
          else
            QRS18.Enabled := false;}
          QrlAluno18.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          19:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS19.Enabled := true
          else
            QRS19.Enabled := false;}
          QrlAluno19.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          20:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS20.Enabled := true
          else
            QRS20.Enabled := false;}
          QrlAluno20.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          21:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS21.Enabled := true
          else
            QRS21.Enabled := false;   }
          QrlAluno21.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          22:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS22.Enabled := true
          else
            QRS22.Enabled := false;   }
          QrlAluno22.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          23:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS23.Enabled := true
          else
            QRS23.Enabled := false; }
          QrlAluno23.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          24:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS24.Enabled := true
          else
            QRS24.Enabled := false;  }
          QrlAluno24.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          25:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS25.Enabled := true
          else
            QRS25.Enabled := false; }
          QrlAluno25.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          26:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS26.Enabled := true
          else
            QRS26.Enabled := false; }
          QrlAluno26.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          27:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS27.Enabled := true
          else
            QRS27.Enabled := false; }
          QrlAluno27.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          28:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS28.Enabled := true
          else
            QRS28.Enabled := false; }
          QrlAluno28.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          29:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS29.Enabled := true
          else
            QRS29.Enabled := false; }
          QrlAluno29.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          30:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS30.Enabled := true
          else
            QRS30.Enabled := false;  }
          QrlAluno30.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          31:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS31.Enabled := true
          else
            QRS31.Enabled := false; }
          QrlAluno31.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          32:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS32.Enabled := true
          else
            QRS32.Enabled := false; }
          QrlAluno32.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          33:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS33.Enabled := true
          else
            QRS33.Enabled := false; }
          QrlAluno33.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          34:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS34.Enabled := true
          else
            QRS34.Enabled := false;  }
          QrlAluno34.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          35:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS35.Enabled := true
          else
            QRS35.Enabled := false; }
          QrlAluno35.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          36:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS36.Enabled := true
          else
            QRS36.Enabled := false;}
          QrlAluno36.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          37:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS37.Enabled := true
          else
            QRS37.Enabled := false;}
          QrlAluno37.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          38:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS38.Enabled := true
          else
            QRS38.Enabled := false;  }
          QrlAluno38.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          39:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS39.Enabled := true
          else
            QRS39.Enabled := false; }
          QrlAluno39.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          40:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS40.Enabled := true
          else
            QRS40.Enabled := false; }
          QrlAluno40.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          41:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS41.Enabled := true
          else
            QRS41.Enabled := false;}
          QrlAluno41.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          42:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS42.Enabled := true
          else
            QRS42.Enabled := false; }
          QrlAluno42.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          43:
          begin
          {if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS43.Enabled := true
          else
            QRS43.Enabled := false; }
          QrlAluno43.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          44:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS44.Enabled := true
          else
            QRS44.Enabled := false;}
          QrlAluno44.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
          45:
          begin
         { if (FieldByName('CO_SIT_MAT').AsString <> 'A') and ( not FieldByName('NO_ALU').IsNull) and (StrToInt(FormatDateTime('MM',(FieldByName('DT_SIT_MAT').AsDateTime))) < numMes) then
            QRS45.Enabled := true
          else
            QRS45.Enabled := false; }
          QrlAluno45.Caption := FormatFloat('000000000',FieldByName('NU_NIRE').AsFloat) + ' - ' + UpperCase(FieldByName('NO_ALU').AsString);
          end;
        end;
        NumAlu := NumAlu + 1;
        Next;
      end;
    end;
  end;

   cont := cont + 1;
end;

procedure TFrmRelPautaChamadaFrente.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  Cont := 1;
end;

procedure TFrmRelPautaChamadaFrente.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  //qrlTemplePag.Left := QRSysData1.Left - 33;
  qrlTempleData.Left := QRSysData3.Left - 75;

  if not QryProfessor.IsEmpty then
    QRLProfessor.Caption := 'Professor: ' + FormatMaskText('00.000-0;0',QryProfessorCO_MAT_COL.AsString) + ' - ' + QryProfessorNO_COL.AsString
  else
    QRLProfessor.Caption := '';

  QRLMateria.Caption := UpperCase(QryRelatorioNO_MATERIA.AsString);
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelPautaChamadaFrente]);

end.
