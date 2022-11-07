unit U_FrmRelCalendarioEscolar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, jpeg, QuickRpt, ExtCtrls, DateUtil;

type
  TFrmRelCalendarioEscolar = class(TFrmRelTemplate)
    QRGroup1: TQRGroup;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRShape8: TQRShape;
    QRLabel6: TQRLabel;
    QRShape3: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRLCalD1: TQRLabel;
    QRLCalD2: TQRLabel;
    QRLCalD3: TQRLabel;
    QRLCalD4: TQRLabel;
    QRLCalD5: TQRLabel;
    QRLCalD6: TQRLabel;
    QRLCalS1: TQRLabel;
    QRLCalS2: TQRLabel;
    QRLCalS3: TQRLabel;
    QRLCalS4: TQRLabel;
    QRLCalS5: TQRLabel;
    QRLCalS6: TQRLabel;
    QRLCalT1: TQRLabel;
    QRLCalT2: TQRLabel;
    QRLCalT3: TQRLabel;
    QRLCalT4: TQRLabel;
    QRLCalT5: TQRLabel;
    QRLCalT6: TQRLabel;
    QRLCalQ1: TQRLabel;
    QRLCalQQ1: TQRLabel;
    QRLCalSS1: TQRLabel;
    QRLCalSS2: TQRLabel;
    QRLCalQQ2: TQRLabel;
    QRLCalQ2: TQRLabel;
    QRLCalQ3: TQRLabel;
    QRLCalQQ3: TQRLabel;
    QRLCalSS3: TQRLabel;
    QRLCalSS4: TQRLabel;
    QRLCalQQ4: TQRLabel;
    QRLCalQ4: TQRLabel;
    QRLCalQ5: TQRLabel;
    QRLCalQQ5: TQRLabel;
    QRLCalSS5: TQRLabel;
    QRLCalSS6: TQRLabel;
    QRLCalQQ6: TQRLabel;
    QRLCalQ6: TQRLabel;
    QRLCalSSS1: TQRLabel;
    QRLCalSSS2: TQRLabel;
    QRLCalSSS3: TQRLabel;
    QRLCalSSS4: TQRLabel;
    QRLCalSSS5: TQRLabel;
    QRLCalSSS6: TQRLabel;
    QrlMes: TQRLabel;
    QRShape16: TQRShape;
    QRBand1: TQRBand;
    QRLabel8: TQRLabel;
    QRLTotalDias: TQRLabel;
    QryRelatorioco_alu: TAutoIncField;
    QRLDescricao: TQRLabel;
    QRLTotalDiasFeriados: TQRLabel;
    QRLabel2: TQRLabel;
    QRD1: TQRLabel;
    QRD2: TQRLabel;
    QRD3: TQRLabel;
    QRD4: TQRLabel;
    QRD5: TQRLabel;
    QRD6: TQRLabel;
    QRD7: TQRLabel;
    QRD8: TQRLabel;
    QRLabel1: TQRLabel;
    QRA1: TQRLabel;
    QRA2: TQRLabel;
    QRA3: TQRLabel;
    QRA4: TQRLabel;
    QRA5: TQRLabel;
    QRA6: TQRLabel;
    QRA7: TQRLabel;
    QRA8: TQRLabel;
    QRLabel13: TQRLabel;
    QROB1: TQRLabel;
    QROB2: TQRLabel;
    QROB3: TQRLabel;
    QROB4: TQRLabel;
    QROB5: TQRLabel;
    QROB6: TQRLabel;
    QROB7: TQRLabel;
    QROB8: TQRLabel;
    QRLOrigem: TQRLabel;
    QRO8: TQRLabel;
    QRO7: TQRLabel;
    QRO6: TQRLabel;
    QRO5: TQRLabel;
    QRO4: TQRLabel;
    QRO3: TQRLabel;
    QRO2: TQRLabel;
    QRO1: TQRLabel;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape17: TQRShape;
    QRShape18: TQRShape;
    QRShape19: TQRShape;
    QRD9: TQRLabel;
    QRShape20: TQRShape;
    QRA9: TQRLabel;
    QROB9: TQRLabel;
    QRO9: TQRLabel;
    QRO10: TQRLabel;
    QROB10: TQRLabel;
    QRA10: TQRLabel;
    QRD10: TQRLabel;
    QRShape21: TQRShape;
    QRShape22: TQRShape;
    procedure QRGroup1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup1AfterPrint(Sender: TQRCustomBand;
      BandPrinted: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    TotDiasUteis,TotDiasFeriado, Mes: Integer;
  public
    { Public declarations }
    codigoEmpresa, tpCalendario, anoRefer: String;
  end;

var
  FrmRelCalendarioEscolar: TFrmRelCalendarioEscolar;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelCalendarioEscolar.QRGroup1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
var
  I, X, R: Integer;
  Data2: TDateTime;
  Dia1: Integer;
  Ano: Integer;
  QtdDia, posic: Integer;
  LabelCal: String;
  //Cor: TColor;
begin
  QRD1.Caption := '';
  QRD2.Caption := '';
  QRD3.Caption := '';
  QRD4.Caption := '';
  QRD5.Caption := '';
  QRD6.Caption := '';
  QRD7.Caption := '';
  QRD8.Caption := '';
  QRD9.Caption := '';
  QRD10.Caption := '';
  QRA1.Caption := '';
  QRA2.Caption := '';
  QRA3.Caption := '';
  QRA4.Caption := '';
  QRA5.Caption := '';
  QRA6.Caption := '';
  QRA7.Caption := '';
  QRA8.Caption := '';
  QRA9.Caption := '';
  QRA10.Caption := '';
  QROB1.Caption := '';
  QROB2.Caption := '';
  QROB3.Caption := '';
  QROB4.Caption := '';
  QROB5.Caption := '';
  QROB6.Caption := '';
  QROB7.Caption := '';
  QROB8.Caption := '';
  QROB9.Caption := '';
  QROB10.Caption := '';
  QRO1.Caption := '';
  QRO2.Caption := '';
  QRO3.Caption := '';
  QRO4.Caption := '';
  QRO5.Caption := '';
  QRO6.Caption := '';
  QRO7.Caption := '';
  QRO8.Caption := '';
  QRO9.Caption := '';
  QRO10.Caption := '';
  posic := 1;

  { Limpa os Labels }
    for X := 0 to ComponentCount - 1 do
      if Components[X] is TQRLabel then
        if Copy((Components[X] as TQRLabel).Name, 1,6) = 'QRLCal' then
          (Components[X] as TQRLabel).Caption := '';

  { Recupera a descrição do calendário }
  with DM.QrySql do
  begin
    { Grava a cabeça do calendário }
    Close;
    Sql.Clear;
    if tpCalendario <> 'T' then
    begin
      Sql.text := 'SET LANGUAGE PORTUGUESE Select CA.*, CT.CAT_SIGLA_TIPO_CALEN From TB157_CALENDARIO_ATIVIDADES CA ' +
                  ' JOIN TB152_CALENDARIO_TIPO CT on CA.CAT_ID_TIPO_CALEND = CT.CAT_ID_TIPO_CALEN ' +
                  ' Where CA.CAL_UNIDA_EDUCA_CALEND = ' + codigoEmpresa +
                  ' and  MONTH(CA.CAL_DATA_CALEND) = ' + IntToStr(Mes) +
                  ' and  YEAR(CA.CAL_DATA_CALEND) = ' + anoRefer +
                  ' and CA.CAT_ID_TIPO_CALEND = ' + tpCalendario +
                  ' order by CA.CAL_DATA_CALEND';
    end
    else
      Sql.text := 'SET LANGUAGE PORTUGUESE Select CA.*, CT.CAT_NOME_TIPO_CALEN From TB157_CALENDARIO_ATIVIDADES CA ' +
                  ' JOIN TB152_CALENDARIO_TIPO CT on CA.CAT_ID_TIPO_CALEND = CT.CAT_ID_TIPO_CALEN ' +
                  ' Where CA.CAL_UNIDA_EDUCA_CALEND = ' + codigoEmpresa +
                  ' and  MONTH(CA.CAL_DATA_CALEND) = ' + IntToStr(Mes) +
                  ' and  YEAR(CA.CAL_DATA_CALEND) = ' + anoRefer +
                  ' order by CA.CAL_DATA_CALEND';
    Open;

    while not Eof do
    begin
      if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        TotDiasUteis := TotDiasUteis + 1;

      if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        TotDiasFeriado := TotDiasFeriado + 1;

      if posic = 1 then
      begin
        QRD1.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA1.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB1.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB1.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB1.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB1.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB1.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO1.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 2 then
      begin
        QRD2.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA2.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB2.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB2.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB2.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB2.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB2.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO2.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 3 then
      begin
        QRD3.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA3.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB3.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB3.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB3.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB3.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB3.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO3.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 4 then
      begin
        QRD4.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA4.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB4.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB4.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB4.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB4.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB4.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO4.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 5 then
      begin
        QRD5.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA5.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB5.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB5.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB5.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB5.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB5.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO5.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 6 then
      begin
        QRD6.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA6.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB6.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB6.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB6.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB6.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB6.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO6.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 7 then
      begin
        QRD7.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA7.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB7.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB7.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB7.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB7.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB7.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO7.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 8 then
      begin
        QRD8.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA8.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB8.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB8.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB8.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB8.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB8.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO8.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 9 then
      begin
        QRD9.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA9.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB9.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB9.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB9.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB9.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB9.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO9.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      if posic = 10 then
      begin
        QRD10.Caption := FormatDateTime('dd',FieldByName('CAL_DATA_CALEND').AsDateTime);
        QRA10.Caption := FieldByName('CAL_NOME_ATIVID_CALEND').AsString;

        if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'U' then
        begin
          QROB10.Caption := 'Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'N' then
        begin
          QROB10.Caption := 'Não Útil/Letivo';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'F' then
        begin
          QROB10.Caption := 'Feriado';
        end
        else if FieldByName('CAL_TIPO_DIA_CALEND').AsString = 'R' then
        begin
          QROB10.Caption := 'Recesso Escolar';
        end
        else
        begin
          QROB10.Caption := 'Conselho de Classe';
        end;

        if tpCalendario = 'T' then
        begin
          QRO10.Caption := FieldByName('CAT_NOME_TIPO_CALEN').AsString;
        end;
      end;

      posic := posic + 1;
      Next;
    end;
    Close;
  end;

  //Mes := StrToInt(Copy(QryRelatorioCO_ANOMES_CAL.AsString, 1, 2));
  //Ano := StrToInt(Copy(QryRelatorioCO_ANOMES_CAL.AsString, 3, 4));

  { Imprime o mês }
  case Mes of
    1: QrlMes.Caption := 'Janeiro';
    2: QrlMes.Caption := 'Fevereiro';
    3: QrlMes.Caption := 'Março';
    4: QrlMes.Caption := 'Abril';
    5: QrlMes.Caption := 'Maio';
    6: QrlMes.Caption := 'Junho';
    7: QrlMes.Caption := 'Julho';
    8: QrlMes.Caption := 'Agosto';
    9: QrlMes.Caption := 'Setembro';
    10: QrlMes.Caption := 'Outubro';
    11: QrlMes.Caption := 'Novembro';
    12: QrlMes.Caption := 'Dezembro';
  end;

  Data2 := StrToDate('01' + '/' + IntToStr(Mes)  + '/' + anoRefer);
  Ano := ExtractYear(Data2);
  Mes := ExtractMonth(Data2);
  QtdDia := DaysPerMonth(Ano, Mes);
  //Cor := clWhite;

  R := 1;
  for I := 1 to QtdDia do
  begin
    Dia1 := DayOfWeek(Data2);

    case Dia1 of
      1: LabelCal := 'QRLCalD' + IntToStr(R);
      2: LabelCal := 'QRLCalS' + IntToStr(R);
      3: LabelCal := 'QRLCalT' + IntToStr(R);
      4: LabelCal := 'QRLCalQ' + IntToStr(R);
      5: LabelCal := 'QRLCalQQ' + IntToStr(R);
      6: LabelCal := 'QRLCalSS' + IntToStr(R);
      7: LabelCal := 'QRLCalSSS' + IntToStr(R);
    end;

     // Verifica se o dia é útl ou não útil
   { with DataModuleSGE.QrySql do
    begin
      Close;
      Sql.Clear;
      Sql.Text := 'Select * From TB51_DIACALENDARIO ' +
                  ' Where CO_ANOMES_CAL = ' + '''' + QryRelatorioCO_ANOMES_CAL.AsString + '''' +
                  '   And DIA_CAL = ' + IntToStr(I) +
                  '   and CO_EMP = ' + IntToStr(Sys_CodigoEmpresaAtiva);
      Open;

      if FieldByName('TP_DIA_CAL').AsString = 'N' then
        Cor := $00DDDDDD
      else
      begin
        Cor := clWhite;
        TotDiasUteis := TotDiasUteis + 1;
      end;
    end;  }

    for X := 0 to ComponentCount - 1 do
      if Components[X] is TQRLabel then
        if (Components[X] as TQRLabel).Name = LabelCal then
        begin
          (Components[X] as TQRLabel).Caption := Copy(FormatDateTime('dd/mm/yyyy',Data2), 1, 2);
          //(Components[X] as TQRLabel).Color := Cor;
        end;

    Data2 := Data2 + 1;

    if Dia1 = 7 then
      R := R + 1
  end;
end;

procedure TFrmRelCalendarioEscolar.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  TotDiasUteis := 0;
  TotDiasFeriado := 0;
  Mes := 1;
end;

procedure TFrmRelCalendarioEscolar.QRBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotalDias.Caption := IntToStr(TotDiasUteis);
  QRLTotalDiasFeriados.Caption := IntToStr(TotDiasFeriado);
  TotDiasUteis := 0;
  TotDiasFeriado := 0;
end;

procedure TFrmRelCalendarioEscolar.QRGroup1AfterPrint(
  Sender: TQRCustomBand; BandPrinted: Boolean);
begin
  inherited;
  Mes := Mes + 1;
end;

procedure TFrmRelCalendarioEscolar.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  if tpCalendario = 'T' then
    QRLOrigem.Enabled := true
  else
    QRLOrigem.Enabled := false;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelCalendarioEscolar]);

end.
