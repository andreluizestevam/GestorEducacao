unit U_FrmRelMapaFreqAluno;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaFreqAluno = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape9: TQRShape;
    QRLabel11: TQRLabel;
    QRLSemestre: TQRLabel;
    QrlPrec1: TQRLabel;
    QrlFalta1: TQRLabel;
    QrlPrec2: TQRLabel;
    QrlFalta2: TQRLabel;
    QrlPrec3: TQRLabel;
    QrlFalta3: TQRLabel;
    QrlPrec4: TQRLabel;
    QrlFalta4: TQRLabel;
    QrlPrec5: TQRLabel;
    QrlFalta5: TQRLabel;
    QrlPrec6: TQRLabel;
    QrlFalta6: TQRLabel;
    QrlTotalP: TQRLabel;
    QrlTotalF: TQRLabel;
    QRBand2: TQRBand;
    QRShape19: TQRShape;
    QRShape20: TQRShape;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRLTotalP1: TQRLabel;
    QRLTotalF1: TQRLabel;
    QRLTotalP2: TQRLabel;
    QRLTotalF2: TQRLabel;
    QRLTotalP3: TQRLabel;
    QRLTotalF3: TQRLabel;
    QRLTotalP4: TQRLabel;
    QRLTotalF4: TQRLabel;
    QRLTotalP5: TQRLabel;
    QRLTotalF5: TQRLabel;
    QRLTotalP6: TQRLabel;
    QRLTotalF6: TQRLabel;
    QRLTotalGP: TQRLabel;
    QRLTotalGF: TQRLabel;
    QRLabel27: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLPage: TQRLabel;
    QRShape26: TQRShape;
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
    QrlPrec7: TQRLabel;
    QrlFalta7: TQRLabel;
    QrlPrec8: TQRLabel;
    QrlFalta8: TQRLabel;
    QrlPrec9: TQRLabel;
    QrlFalta9: TQRLabel;
    QrlPrec10: TQRLabel;
    QrlFalta10: TQRLabel;
    QrlFalta11: TQRLabel;
    QrlPrec11: TQRLabel;
    QrlFalta12: TQRLabel;
    QrlPrec12: TQRLabel;
    QRLTotalP7: TQRLabel;
    QRLTotalF7: TQRLabel;
    QRLTotalP8: TQRLabel;
    QRLTotalF8: TQRLabel;
    QRLTotalP9: TQRLabel;
    QRLTotalF9: TQRLabel;
    QRLTotalP10: TQRLabel;
    QRLTotalF10: TQRLabel;
    QRLTotalP11: TQRLabel;
    QRLTotalF11: TQRLabel;
    QRLTotalP12: TQRLabel;
    QRLTotalF12: TQRLabel;
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
    QRLParametros: TQRLabel;
    QRLSerTurNIS: TQRLabel;
    QRLabel2: TQRLabel;
    QRShape2: TQRShape;
    QrlMes1: TQRLabel;
    QRLabel3: TQRLabel;
    QRShape3: TQRShape;
    QRShape1: TQRShape;
    QRShape4: TQRShape;
    QrlMes2: TQRLabel;
    QrlMes3: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel4: TQRLabel;
    QrlMes4: TQRLabel;
    QRLabel6: TQRLabel;
    QRShape5: TQRShape;
    QRShape7: TQRShape;
    QRLabel7: TQRLabel;
    QrlMes5: TQRLabel;
    QRShape6: TQRShape;
    QRLabel8: TQRLabel;
    QrlMes6: TQRLabel;
    QRShape28: TQRShape;
    QRLabel16: TQRLabel;
    QrlMes7: TQRLabel;
    QRShape8: TQRShape;
    QRLabel17: TQRLabel;
    QrlMes8: TQRLabel;
    QRShape27: TQRShape;
    QRLabel19: TQRLabel;
    QrlMes9: TQRLabel;
    QRShape29: TQRShape;
    QrlMes10: TQRLabel;
    QRLabel20: TQRLabel;
    QRShape18: TQRShape;
    QRLabel23: TQRLabel;
    QrlMes11: TQRLabel;
    QRShape17: TQRShape;
    QRLabel25: TQRLabel;
    QrlMes12: TQRLabel;
    QRShape41: TQRShape;
    QRShape16: TQRShape;
    QRLabel9: TQRLabel;
    QrlTotal: TQRLabel;
    QRLNoMateria: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
    TotGeralProfessorP: array[1..12] of Integer;
    TotGeralProfessorF: array[1..12] of Integer;
    CTCampo: Integer;
//    QRLTotalP1, QRLTotalP2, QRLTotalP3, QRLTotalP4, QRLTotalP5, QRLTotalP6: Integer;
//    QRLTotalF1, QRLTotalF2, QRLTotalF3, QRLTotalF4, QRLTotalF5, QRLTotalF6: Integer;
    TotalGP, TotalGF: Integer;
  public
    { Public declarations }
    codMateriaDiaria,mesAlteracaoDoAluno : Integer;
    codigoEmpresa : String;
  end;

var
  FrmRelMapaFreqAluno: TFrmRelMapaFreqAluno;

implementation

uses U_DataModuleSGE, U_Funcoes, DateUtils, MaskUtils;

{$R *.dfm}

procedure TFrmRelMapaFreqAluno.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  //SqlString: String;
  I : Integer;
  //MesIni,Mes,MesFim,MesAlterPrec,mesAlterFalta
  //NumFaltas, NumPresencas: Integer;
  //TotFaltas, TotPresencas: Integer;
  //ocoTotalFal,ocoTotalPre : Boolean;
  //ocoFaltas, ocoPresenca
begin
  inherited;
  if (QryRelatorio.FIeldByName('CO_PARAM_FREQUE').AsString = 'M') or (QryRelatorio.FieldByName('CO_PARAM_FREQ_TIPO').AsString <> 'D') then
    QRLNoMateria.Caption := QryRelatorio.FieldByName('NO_RED_MATERIA').AsString
  else
    QRLNoMateria.Caption := '*****************';

  //ocoFaltas := false;
  //ocoPresenca := false;

  QrlPrec1.Caption := '-';
  QrlPrec2.Caption := '-';
  QrlPrec3.Caption := '-';
  QrlPrec4.Caption := '-';
  QrlPrec5.Caption := '-';
  QrlPrec6.Caption := '-';
  QrlPrec7.Caption := '-';
  QrlPrec8.Caption := '-';
  QrlPrec9.Caption := '-';
  QrlPrec10.Caption := '-';
  QrlPrec11.Caption := '-';
  QrlPrec12.Caption := '-';                  

  QrlFalta1.Caption := '-';
  QrlFalta2.Caption := '-';
  QrlFalta3.Caption := '-';
  QrlFalta4.Caption := '-';
  QrlFalta5.Caption := '-';
  QrlFalta6.Caption := '-';
  QrlFalta7.Caption := '-';
  QrlFalta8.Caption := '-';
  QrlFalta9.Caption := '-';
  QrlFalta10.Caption := '-';
  QrlFalta11.Caption := '-';
  QrlFalta12.Caption := '-';

  QrlTotalP.Caption := '-';
  QrlTotalF.Caption := '-';

   if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;
  //MesIni := 1;

  if (qryRelatorio.fieldByName('CO_SIT_MAT').AsString = 'F') or (qryRelatorio.fieldByName('CO_SIT_MAT').AsString = 'T') then
    mesAlteracaoDoAluno := StrToInt(FormatDateTime('MM',QryRelatorio.FieldByName('DT_ALT_REGISTRO').AsDateTime))
  else
    mesAlteracaoDoAluno := 0;
    {
  if mesAlteracaoDoAluno > 0 then
  begin
    MesFim := mesAlteracaoDoAluno;
    MesAlterPrec := MesFim + 1;
    mesAlterFalta := MesFim + 1;
    for I := 0 to ComponentCount - 1 do
    begin
      if Components[I] is TQRLabel then
      begin
        if (Components[I] as TQRLabel).Name = 'QrlPrec' + IntToStr(MesAlterPrec) then
        begin
          (Components[I] as TQRLabel).Caption := '///';
          MesAlterPrec := MesAlterPrec + 1;
        end;

        if (Components[I] as TQRLabel).Name = 'QrlFalta' + IntToStr(mesAlterFalta) then
        begin
          (Components[I] as TQRLabel).Caption := '///';
          mesAlterFalta := mesAlterFalta + 1;
        end;

      end;
    end;
  end
  else
    MesFim := 12;    }
      //Falta********************
      if not qryRelatorio.fieldByName('TotJan').IsNull then
      begin
        QrlFalta1.Caption := qryRelatorio.fieldByName('TotJan').AsString;
      end
      else
        QrlFalta1.Caption := '-';

      if not qryRelatorio.fieldByName('TotFev').IsNull then
      begin
        QrlFalta2.Caption := qryRelatorio.fieldByName('TotFev').AsString;
      end
      else
        QrlFalta2.Caption := '-';

      if not qryRelatorio.fieldByName('TotMar').IsNull then
      begin
        QrlFalta3.Caption := qryRelatorio.fieldByName('TotMar').AsString;
      end
      else
        QrlFalta3.Caption := '-';

      if not qryRelatorio.fieldByName('TotAbr').IsNull then
      begin
        QrlFalta4.Caption := qryRelatorio.fieldByName('TotAbr').AsString;
      end
      else
        QrlFalta4.Caption := '-';

      if not qryRelatorio.fieldByName('TotMai').IsNull then
      begin
        QrlFalta5.Caption := qryRelatorio.fieldByName('TotMai').AsString;
      end
      else
        QrlFalta5.Caption := '-';

      if not qryRelatorio.fieldByName('TotJun').IsNull then
      begin
        QrlFalta6.Caption := qryRelatorio.fieldByName('TotJun').AsString;
      end
      else
        QrlFalta6.Caption := '-';

      if not qryRelatorio.fieldByName('TotJul').IsNull then
      begin
        QrlFalta7.Caption := qryRelatorio.fieldByName('TotJul').AsString;
      end
      else
        QrlFalta7.Caption := '-';

      if not qryRelatorio.fieldByName('TotAgo').IsNull then
      begin
        QrlFalta8.Caption := qryRelatorio.fieldByName('TotAgo').AsString;
      end
      else
        QrlFalta8.Caption := '-';

      if not qryRelatorio.fieldByName('TotSet').IsNull then
      begin
        QrlFalta9.Caption := qryRelatorio.fieldByName('TotSet').AsString;
      end
      else
        QrlFalta9.Caption := '-';

      if not qryRelatorio.fieldByName('TotOut').IsNull then
      begin
        QrlFalta10.Caption := qryRelatorio.fieldByName('TotOut').AsString;
      end
      else
        QrlFalta10.Caption := '-';

      if not qryRelatorio.fieldByName('TotNov').IsNull then
      begin
        QrlFalta11.Caption := qryRelatorio.fieldByName('TotNov').AsString;
      end
      else
        QrlFalta11.Caption := '-';

      if not qryRelatorio.fieldByName('TotDez').IsNull then
      begin
        QrlFalta12.Caption := qryRelatorio.fieldByName('TotDez').AsString;
      end
      else
        QrlFalta12.Caption := '-';

      if not qryRelatorio.FieldByName('TotGeral').IsNull then
        QrlTotalF.Caption := qryRelatorio.FieldByName('TotGeral').AsString
      else
        QrlTotalF.Caption := '-';
      //****************************************

      //Presença******************
      if not qryRelatorio.fieldByName('TotJanP').IsNull then
      begin
        QrlPrec1.Caption := qryRelatorio.fieldByName('TotJanP').AsString;
      end
      else
        QrlPrec1.Caption := '-';

      if not qryRelatorio.fieldByName('TotFevP').IsNull then
      begin
        QrlPrec2.Caption := qryRelatorio.fieldByName('TotFevP').AsString;
      end
      else
        QrlPrec2.Caption := '-';

      if not qryRelatorio.fieldByName('TotMarP').IsNull then
      begin
        QrlPrec3.Caption := qryRelatorio.fieldByName('TotMarP').AsString;
      end
      else
        QrlPrec3.Caption := '-';

      if not qryRelatorio.fieldByName('TotAbrP').IsNull then
      begin
        QrlPrec4.Caption := qryRelatorio.fieldByName('TotAbrP').AsString;
      end
      else
        QrlPrec4.Caption := '-';

      if not qryRelatorio.fieldByName('TotMaiP').IsNull then
      begin
        QrlPrec5.Caption := qryRelatorio.fieldByName('TotMaiP').AsString;
      end
      else
        QrlPrec5.Caption := '-';

      if not qryRelatorio.fieldByName('TotJunP').IsNull then
      begin
        QrlPrec6.Caption := qryRelatorio.fieldByName('TotJunP').AsString;
      end
      else
        QrlPrec6.Caption := '-';

      if not qryRelatorio.fieldByName('TotJulP').IsNull then
      begin
        QrlPrec7.Caption := qryRelatorio.fieldByName('TotJulP').AsString;
      end
      else
        QrlPrec7.Caption := '-';

      if not qryRelatorio.fieldByName('TotAgoP').IsNull then
      begin
        QrlPrec8.Caption := qryRelatorio.fieldByName('TotAgoP').AsString;
      end
      else
        QrlPrec8.Caption := '-';

      if not qryRelatorio.fieldByName('TotSetP').IsNull then
      begin
        QrlPrec9.Caption := qryRelatorio.fieldByName('TotSetP').AsString;
      end
      else
        QrlPrec9.Caption := '-';

      if not qryRelatorio.fieldByName('TotOutP').IsNull then
      begin
        QrlPrec10.Caption := qryRelatorio.fieldByName('TotOutP').AsString;
      end
      else
        QrlPrec10.Caption := '-';

      if not QryRelatorio.fieldByName('TotNovP').IsNull then
      begin
        QrlPrec11.Caption := qryRelatorio.fieldByName('TotNovP').AsString;
      end
      else
        QrlPrec11.Caption := '-';

      if not QryRelatorio.fieldByName('TotDezP').IsNull then
      begin
        QrlPrec12.Caption := QryRelatorio.fieldByName('TotDezP').AsString;
      end
      else
        QrlPrec12.Caption := '-';

      if not QryRelatorio.FieldByName('TotGeralP').IsNull then
        QrlTotalP.Caption := QryRelatorio.FieldByName('TotGeralP').AsString
      else
        QrlTotalP.Caption := '-';

      //********************************
      for I := 1 to 12 do
      begin
        case I of
        1:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotJanP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotJan').AsInteger;
        end;
        2:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotFevP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotFev').AsInteger;
        end;
        3:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotMarP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotMar').AsInteger;
        end;
        4:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotAbrP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotAbr').AsInteger;
        end;
        5:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotMaiP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotMai').AsInteger;
        end;
        6:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotJunP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotJun').AsInteger;
        end;
        7:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotJulP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotJul').AsInteger;
        end;
        8:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotAgoP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotAgo').AsInteger;
        end;
        9:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotSetP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotSet').AsInteger;
        end;
        10:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotOutP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotOut').AsInteger;
        end;
        11:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotNovP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotNov').AsInteger;
        end;
        12:begin
          TotGeralProfessorP[I] := TotGeralProfessorP[I] + QryRelatorio.FieldByName('TotDezP').AsInteger;
          TotGeralProfessorF[I] := TotGeralProfessorF[I] + QryRelatorio.FieldByName('TotDez').AsInteger;
        end;
        end;
      end;

      QrlTotalP.Caption := QryRelatorio.FieldByName('TotGeralP').AsString;

      TotalGP := TotalGP + QryRelatorio.FieldByName('TotGeralP').AsInteger;
      TotalGF := TotalGF + QryRelatorio.FieldByName('TotGeral').AsInteger;

      //if not ocoTotalPre then QrlTotalP.Caption := '-';
      //if not ocoTotalFal then QrlTotalF.Caption := '-';

end;

procedure TFrmRelMapaFreqAluno.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRLTotalP1.Caption := IntToStr(TotGeralProfessorP[1]);
  QRLTotalP2.Caption := IntToStr(TotGeralProfessorP[2]);
  QRLTotalP3.Caption := IntToStr(TotGeralProfessorP[3]);
  QRLTotalP4.Caption := IntToStr(TotGeralProfessorP[4]);
  QRLTotalP5.Caption := IntToStr(TotGeralProfessorP[5]);
  QRLTotalP6.Caption := IntToStr(TotGeralProfessorP[6]);
  QRLTotalP7.Caption := IntToStr(TotGeralProfessorP[7]);
  QRLTotalP8.Caption := IntToStr(TotGeralProfessorP[8]);
  QRLTotalP9.Caption := IntToStr(TotGeralProfessorP[9]);
  QRLTotalP10.Caption := IntToStr(TotGeralProfessorP[10]);
  QRLTotalP11.Caption := IntToStr(TotGeralProfessorP[11]);
  QRLTotalP12.Caption := IntToStr(TotGeralProfessorP[12]);

  QRLTotalF1.Caption := IntToStr(TotGeralProfessorF[1]);
  QRLTotalF2.Caption := IntToStr(TotGeralProfessorF[2]);
  QRLTotalF3.Caption := IntToStr(TotGeralProfessorF[3]);
  QRLTotalF4.Caption := IntToStr(TotGeralProfessorF[4]);
  QRLTotalF5.Caption := IntToStr(TotGeralProfessorF[5]);
  QRLTotalF6.Caption := IntToStr(TotGeralProfessorF[6]);
  QRLTotalF7.Caption := IntToStr(TotGeralProfessorF[7]);
  QRLTotalF8.Caption := IntToStr(TotGeralProfessorF[8]);
  QRLTotalF9.Caption := IntToStr(TotGeralProfessorF[9]);
  QRLTotalF10.Caption := IntToStr(TotGeralProfessorF[10]);
  QRLTotalF11.Caption := IntToStr(TotGeralProfessorF[12]);
  QRLTotalF12.Caption := IntToStr(TotGeralProfessorF[12]);

  QrlTotalGP.Caption := IntToStr(TotalGP);
  QrlTotalGF.Caption := IntToStr(TotalGF);

  if QRLTotalP1.Caption = '0' then QRLTotalP1.Caption := '-';
  if QRLTotalP2.Caption = '0' then QRLTotalP2.Caption := '-';
  if QRLTotalP3.Caption = '0' then QRLTotalP3.Caption := '-';
  if QRLTotalP4.Caption = '0' then QRLTotalP4.Caption := '-';
  if QRLTotalP5.Caption = '0' then QRLTotalP5.Caption := '-';
  if QRLTotalP6.Caption = '0' then QRLTotalP6.Caption := '-';
  if QRLTotalP7.Caption = '0' then QRLTotalP7.Caption := '-';
  if QRLTotalP8.Caption = '0' then QRLTotalP8.Caption := '-';
  if QRLTotalP9.Caption = '0' then QRLTotalP9.Caption := '-';
  if QRLTotalP10.Caption = '0' then QRLTotalP10.Caption := '-';
  if QRLTotalP11.Caption = '0' then QRLTotalP11.Caption := '-';
  if QRLTotalP12.Caption = '0' then QRLTotalP12.Caption := '-';

  if QRLTotalF1.Caption = '0' then QRLTotalF1.Caption := '-';
  if QRLTotalF2.Caption = '0' then QRLTotalF2.Caption := '-';
  if QRLTotalF3.Caption = '0' then QRLTotalF3.Caption := '-';
  if QRLTotalF4.Caption = '0' then QRLTotalF4.Caption := '-';
  if QRLTotalF5.Caption = '0' then QRLTotalF5.Caption := '-';
  if QRLTotalF6.Caption = '0' then QRLTotalF6.Caption := '-';
  if QRLTotalF7.Caption = '0' then QRLTotalF7.Caption := '-';
  if QRLTotalF8.Caption = '0' then QRLTotalF8.Caption := '-';
  if QRLTotalF9.Caption = '0' then QRLTotalF9.Caption := '-';
  if QRLTotalF10.Caption = '0' then QRLTotalF10.Caption := '-';
  if QRLTotalF11.Caption = '0' then QRLTotalF11.Caption := '-';
  if QRLTotalF12.Caption = '0' then QRLTotalF12.Caption := '-';

  if QRLTotalGP.Caption = '0' then QRLTotalGP.Caption := '-';
  if QRLTotalGF.Caption = '0' then QRLTotalGF.Caption := '-';

end;

procedure TFrmRelMapaFreqAluno.QRGroup2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  i: integer;
begin
  inherited;
  if not QryRelatorio.FieldByName('NO_ALU').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('NO_ALU').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  CTCampo := 1;
  if QryRelatorio.FieldByName('CO_SIT_MAT').AsString = 'A' then
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + ' - Turma ' +QryRelatorio.FieldByName('no_turma').AsString+
    ' - Nº NIS '+ FormatFloat('00000000000;0',QryRelatorio.FieldByName('nu_nis').AsFloat) + ' - Nº Matrícula ' + FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString) +
    ' - Em Aberto )';
  if QryRelatorio.FieldByName('CO_SIT_MAT').AsString = 'F' then
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + ' - Turma ' +QryRelatorio.FieldByName('no_turma').AsString+
    ' - Nº NIS '+ FormatFloat('00000000000;0',QryRelatorio.FieldByName('nu_nis').AsFloat) + ' - Nº Matrícula ' + FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString) +
    ' - Finalizado )';
  if QryRelatorio.FieldByName('CO_SIT_MAT').AsString = 'T' then
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + ' - Turma ' +QryRelatorio.FieldByName('no_turma').AsString+
    ' - Nº NIS '+ FormatFloat('00000000000;0',QryRelatorio.FieldByName('nu_nis').AsFloat) + ' - Nº Matrícula ' + FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString) +
    ' - Trancado )';
  if QryRelatorio.FieldByName('CO_SIT_MAT').AsString = 'X' then
    QRLSerTurNIS.Caption := '('+ QryRelatorio.FieldByName('no_cur').AsString + ' - Turma ' +QryRelatorio.FieldByName('no_turma').AsString+
    ' - Nº NIS '+ FormatFloat('00000000000;0',QryRelatorio.FieldByName('nu_nis').AsFloat) + ' - Nº Matrícula ' + FormatMaskText('00.000.000000;0', QryRelatorio.FieldByName('CO_ALU_CAD').AsString) +
    ' - Transferido )';

  TotalGP := 0;
  TotalGF := 0;
  for i := 1 to 12 do
  begin
    TotGeralProfessorP[i] := 0;
    TotGeralProfessorF[i] := 0;
  end;
  
end;

procedure TFrmRelMapaFreqAluno.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  {if (QryRelatorioCO_PARAM_FREQUE.AsString = 'D') and (QryRelatorioCO_PARAM_FREQUE.IsNull) then
  begin
    with DM.QrySql do
    begin
      Close;
      SQL.Clear;
      SQL.Text := 'SELECT top 1 A.CO_MAT,CM.NO_RED_MATERIA ' +
       'FROM   TB02_MATERIA A, TB01_CURSO B, TB48_GRADE_ALUNO C, TB107_CADMATERIAS CM '+
       'WHERE  B.CO_EMP = B.CO_EMP AND B.CO_EMP = C.CO_EMP AND '+
       'C.CO_CUR = B.CO_CUR AND '+
       'C.CO_MAT = A.CO_MAT AND '+
       'A.ID_MATERIA = CM.ID_MATERIA AND '+
       'C.CO_CUR = :P_CO_CUR AND '+
       'C.CO_ANO_MES_MAT = :P_CO_ANO_MES_MAT AND '+
       'C.CO_TUR = :P_CO_TUR AND '+
       'C.CO_MODU_CUR = :P_CO_MODU_CUR AND '+
       'C.CO_EMP = :P_CO_EMP';
       Parameters.ParamByName('P_CO_CUR').Value := QryRelatorioCO_CUR.AsInteger;
       Parameters.ParamByName('P_CO_TUR').Value := QryRelatorioCO_TUR.AsInteger;
       Parameters.ParamByName('P_CO_EMP').Value := codigoEmpresa;
       Parameters.ParamByName('P_CO_MODU_CUR').Value := QryRelatorioCO_MODU_CUR.AsInteger;
       Parameters.ParamByName('P_CO_ANO_MES_MAT').Value := QryRelatorioCO_ANO_MES_MAT.AsString;
       Open;

       if not IsEmpty then
         codMateriaDiaria := FieldByName('CO_MAT').AsInteger;
    end;
  end;   }
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaFreqAluno]);

end.
