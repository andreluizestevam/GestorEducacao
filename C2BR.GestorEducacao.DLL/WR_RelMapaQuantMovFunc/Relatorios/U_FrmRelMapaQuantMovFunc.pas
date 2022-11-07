unit U_FrmRelMapaQuantMovFunc;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls;

type
  TFrmRelMapaQuantMovFunc = class(TFrmRelTemplate)
    QRShape3: TQRShape;
    QrlPeriodo: TQRLabel;
    QrlSerie: TQRLabel;
    QRBand2: TQRBand;
    QrlTotal: TQRLabel;
    QRBand1: TQRBand;
    QRDBText1: TQRDBText;
    QRLabel2: TQRLabel;
    QRShape2: TQRShape;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRShape4: TQRShape;
    QRShape6: TQRShape;
    QRShape8: TQRShape;
    QRShape10: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRShape16: TQRShape;
    QRShape17: TQRShape;
    QRLResTotal: TQRLabel;
    QRLAno3: TQRLabel;
    QRLAno4: TQRLabel;
    QRLAno5: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLMAno5: TQRLabel;
    QRLTAno5: TQRLabel;
    qryAno: TADOQuery;
    QRLParam: TQRLabel;
    QRLAno2: TQRLabel;
    QRLAno1: TQRLabel;
    QRShape11: TQRShape;
    QRLabel11: TQRLabel;
    QRShape21: TQRShape;
    QRLabel13: TQRLabel;
    QRLabel7: TQRLabel;
    QRShape1: TQRShape;
    QRShape7: TQRShape;
    QRShape5: TQRShape;
    QRShape9: TQRShape;
    QRLabel6: TQRLabel;
    QRShape20: TQRShape;
    QRLabel8: TQRLabel;
    QRShape24: TQRShape;
    QRLabel9: TQRLabel;
    QRShape25: TQRShape;
    QRLabel10: TQRLabel;
    QRLabel12: TQRLabel;
    QRShape26: TQRShape;
    QRLabel14: TQRLabel;
    QRShape27: TQRShape;
    QRLabel15: TQRLabel;
    QRShape28: TQRShape;
    QRLabel16: TQRLabel;
    QRLabel17: TQRLabel;
    QRShape29: TQRShape;
    QRLabel18: TQRLabel;
    QRShape30: TQRShape;
    QRLabel19: TQRLabel;
    QRShape31: TQRShape;
    QRLabel20: TQRLabel;
    QRLabel21: TQRLabel;
    QRShape32: TQRShape;
    QRLabel22: TQRLabel;
    QRShape33: TQRShape;
    QRLabel23: TQRLabel;
    QRShape34: TQRShape;
    QRLabel24: TQRLabel;
    QRLEAno5: TQRLabel;
    QRLIAno5: TQRLabel;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRLIAno4: TQRLabel;
    QRShape38: TQRShape;
    QRLEAno4: TQRLabel;
    QRShape39: TQRShape;
    QRLMAno4: TQRLabel;
    QRShape40: TQRShape;
    QRLTAno4: TQRLabel;
    QRLTAno3: TQRLabel;
    QRShape12: TQRShape;
    QRLMAno3: TQRLabel;
    QRShape18: TQRShape;
    QRLEAno3: TQRLabel;
    QRShape19: TQRShape;
    QRLIAno3: TQRLabel;
    QRLTAno2: TQRLabel;
    QRShape22: TQRShape;
    QRLMAno2: TQRLabel;
    QRShape23: TQRShape;
    QRLEAno2: TQRLabel;
    QRShape41: TQRShape;
    QRLIAno2: TQRLabel;
    QRLTAno1: TQRLabel;
    QRShape42: TQRShape;
    QRLMAno1: TQRLabel;
    QRShape43: TQRShape;
    QRLEAno1: TQRLabel;
    QRShape44: TQRShape;
    QRLIAno1: TQRLabel;
    QRShape45: TQRShape;
    QRLTIAno1: TQRLabel;
    QRShape46: TQRShape;
    QRLTEAno1: TQRLabel;
    QRShape47: TQRShape;
    QRLTMAno1: TQRLabel;
    QRShape48: TQRShape;
    QRLTTAno1: TQRLabel;
    QRShape49: TQRShape;
    QRLTIAno2: TQRLabel;
    QRShape50: TQRShape;
    QRLTEAno2: TQRLabel;
    QRShape51: TQRShape;
    QRLTMAno2: TQRLabel;
    QRShape52: TQRShape;
    QRLTTAno2: TQRLabel;
    QRShape53: TQRShape;
    QRLTIAno3: TQRLabel;
    QRLTEAno3: TQRLabel;
    QRShape54: TQRShape;
    QRShape55: TQRShape;
    QRLTMAno3: TQRLabel;
    QRShape56: TQRShape;
    QRLTTAno3: TQRLabel;
    QRShape57: TQRShape;
    QRLTIAno4: TQRLabel;
    QRShape58: TQRShape;
    QRLTEAno4: TQRLabel;
    QRShape59: TQRShape;
    QRLTMAno4: TQRLabel;
    QRShape60: TQRShape;
    QRLTTAno4: TQRLabel;
    QRShape61: TQRShape;
    QRLTIAno5: TQRLabel;
    QRLTEAno5: TQRLabel;
    QRShape62: TQRShape;
    QRShape63: TQRShape;
    QRLTMAno5: TQRLabel;
    QRShape64: TQRShape;
    QRLTTAno5: TQRLabel;
    QRShape65: TQRShape;
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRDBText2Print(sender: TObject; var Value: String);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmRelMapaQuantMovFunc: TFrmRelMapaQuantMovFunc;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaQuantMovFunc.PageHeaderBand1BeforePrint(Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
//  QRLabel1.Caption := Sys_DescricaoTipoCurso;
end;

procedure TFrmRelMapaQuantMovFunc.QRDBText2Print(sender: TObject;var Value: String);
begin
  inherited;
  //QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption)+StrToInt(Value));
end;

procedure TFrmRelMapaQuantMovFunc.QuickRep1BeforePrint(Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  QrlTotal.Caption := '0';
  QRLTIAno1.Caption := '0';
  QRLTEAno1.Caption := '0';
  QRLTMAno1.Caption := '0';
  QRLTTAno1.Caption := '0';
  QRLTIAno2.Caption := '0';
  QRLTEAno2.Caption := '0';
  QRLTMAno2.Caption := '0';
  QRLTTAno2.Caption := '0';
  QRLTIAno3.Caption := '0';
  QRLTEAno3.Caption := '0';
  QRLTMAno3.Caption := '0';
  QRLTTAno3.Caption := '0';
  QRLTIAno4.Caption := '0';
  QRLTEAno4.Caption := '0';
  QRLTMAno4.Caption := '0';
  QRLTTAno4.Caption := '0';
  QRLTIAno5.Caption := '0';
  QRLTEAno5.Caption := '0';
  QRLTMAno5.Caption := '0';
  QRLTTAno5.Caption := '0';
end;

procedure TFrmRelMapaQuantMovFunc.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
Var
  totParc : integer;
  SqlString : String;
begin
  inherited;
  totParc := 0;

  with qryAno do
  begin
    //Pegar Primeiro Ano
    Close;
    SQL.Clear;
    SqlString := 'select ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) '+
                ' from TB286_MOVIM_TRANSF_FUNCI af '+
                ' where af.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and af.CO_TIPO_MOVIM = ' +  QuotedStr('ME') +
                ' and year(af.DT_INI_MOVIM_TRANSF_FUNCI) = ' +  QRLAno1.Caption +') movExt,'+
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfi ' +
                ' where mfi.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfi.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno1.Caption +
                ' and mfi.CO_TIPO_MOVIM = ' +  QuotedStr('MI')  + ') movInt, ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfe ' +
                ' where mfe.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfe.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno1.Caption +
                ' and mfe.CO_TIPO_MOVIM = ' +  quotedStr('TE') + ') transfExt' +
                ' from TB25_EMPRESA e '+
                ' where e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SqlString;

    Open;

    if not IsEmpty then
    begin
      QRLMAno1.Caption := FieldByName('movExt').AsString;
      QRLIAno1.Caption := FieldByName('movInt').AsString;
      QRLEAno1.Caption := FieldByName('transfExt').AsString;
      QRLTAno1.Caption := IntToStr(FieldByName('movExt').AsInteger + FieldByName('movInt').AsInteger + FieldByName('transfExt').AsInteger );
      totParc := totParc + StrToInt(QRLTAno1.Caption);
    end
    else
    begin
      QRLMAno1.Caption := '0';
      QRLIAno1.Caption := '0';
      QRLEAno1.Caption := '0';
      QRLTAno1.Caption := '0';
    end;
    //

    //Pegar Segundo Ano
    Close;
    SQL.Clear;
    SqlString := 'select ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) '+
                ' from TB286_MOVIM_TRANSF_FUNCI af '+
                ' where af.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and af.CO_TIPO_MOVIM = ' +  QuotedStr('ME') +
                ' and year(af.DT_INI_MOVIM_TRANSF_FUNCI) = ' +  QRLAno2.Caption +') movExt,'+
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfi ' +
                ' where mfi.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfi.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno2.Caption +
                ' and mfi.CO_TIPO_MOVIM = ' +  QuotedStr('MI')  + ') movInt, ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfe ' +
                ' where mfe.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfe.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno2.Caption +
                ' and mfe.CO_TIPO_MOVIM = ' +  quotedStr('TE') + ') transfExt' +
                ' from TB25_EMPRESA e '+
                ' where e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SqlString;
    Open;

    if not IsEmpty then
    begin
      QRLMAno2.Caption := FieldByName('movExt').AsString;
      QRLIAno2.Caption := FieldByName('movInt').AsString;
      QRLEAno2.Caption := FieldByName('transfExt').AsString;
      QRLTAno2.Caption := IntToStr(FieldByName('movExt').AsInteger + FieldByName('movInt').AsInteger + FieldByName('transfExt').AsInteger );
      totParc := totParc +  StrToInt(QRLTAno2.Caption);
    end
    else
    begin
      QRLMAno2.Caption := '0';
      QRLIAno2.Caption := '0';
      QRLEAno2.Caption := '0';
      QRLTAno2.Caption := '0';
    end;
    //

    //Pegar Terceiro Ano
    Close;
    SQL.Clear;
    SqlString := 'select ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) '+
                ' from TB286_MOVIM_TRANSF_FUNCI af '+
                ' where af.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and af.CO_TIPO_MOVIM = ' +  QuotedStr('ME') +
                ' and year(af.DT_INI_MOVIM_TRANSF_FUNCI) = ' +  QRLAno3.Caption +') movExt,'+
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfi ' +
                ' where mfi.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfi.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno3.Caption +
                ' and mfi.CO_TIPO_MOVIM = ' +  QuotedStr('MI')  + ') movInt, ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfe ' +
                ' where mfe.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfe.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno3.Caption +
                ' and mfe.CO_TIPO_MOVIM = ' +  quotedStr('TE') + ') transfExt' +
                ' from TB25_EMPRESA e '+
                ' where e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SqlString;
    Open;

    if not IsEmpty then
    begin
      QRLMAno3.Caption := FieldByName('movExt').AsString;
      QRLIAno3.Caption := FieldByName('movInt').AsString;
      QRLEAno3.Caption := FieldByName('transfExt').AsString;
      QRLTAno3.Caption := IntToStr(FieldByName('movExt').AsInteger + FieldByName('movInt').AsInteger + FieldByName('transfExt').AsInteger );
      totParc := totParc +  StrToInt(QRLTAno3.Caption);
    end
    else
    begin
      QRLMAno3.Caption := '0';
      QRLIAno3.Caption := '0';
      QRLEAno3.Caption := '0';
      QRLTAno3.Caption := '0';
    end;
    //

    //Pegar Quarto Ano
    Close;
    SQL.Clear;
    SqlString := 'select ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) '+
                ' from TB286_MOVIM_TRANSF_FUNCI af '+
                ' where af.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and af.CO_TIPO_MOVIM = ' +  QuotedStr('ME') +
                ' and year(af.DT_INI_MOVIM_TRANSF_FUNCI) = ' +  QRLAno4.Caption +') movExt,'+
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfi ' +
                ' where mfi.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfi.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno4.Caption +
                ' and mfi.CO_TIPO_MOVIM = ' +  QuotedStr('MI')  + ') movInt, ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfe ' +
                ' where mfe.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfe.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno4.Caption +
                ' and mfe.CO_TIPO_MOVIM = ' +  quotedStr('TE') + ') transfExt' +
                ' from TB25_EMPRESA e '+
                ' where e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SqlString;
    Open;

    if not IsEmpty then
    begin
      QRLMAno4.Caption := FieldByName('movExt').AsString;
      QRLIAno4.Caption := FieldByName('movInt').AsString;
      QRLEAno4.Caption := FieldByName('transfExt').AsString;
      QRLTAno4.Caption := IntToStr(FieldByName('movExt').AsInteger + FieldByName('movInt').AsInteger + FieldByName('transfExt').AsInteger );
      totParc := totParc +  StrToInt(QRLTAno4.Caption);
    end
    else
    begin
      QRLMAno4.Caption := '0';
      QRLIAno4.Caption := '0';
      QRLEAno4.Caption := '0';
      QRLTAno4.Caption := '0';
    end;
    //

    //Pegar Quinto Ano
    Close;
    SQL.Clear;
    SqlString := 'select ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) '+
                ' from TB286_MOVIM_TRANSF_FUNCI af '+
                ' where af.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and af.CO_TIPO_MOVIM = ' +  QuotedStr('ME') +
                ' and year(af.DT_INI_MOVIM_TRANSF_FUNCI) = ' +  QRLAno5.Caption +') movExt,'+
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfi ' +
                ' where mfi.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfi.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno5.Caption +
                ' and mfi.CO_TIPO_MOVIM = ' +  QuotedStr('MI')  + ') movInt, ' +
                ' (select COUNT(ID_MOVIM_TRANSF_FUNCI) ' +
                ' from TB286_MOVIM_TRANSF_FUNCI mfe ' +
                ' where mfe.CO_EMP_ORIGEM = e.CO_EMP ' +
                ' and year(mfe.DT_INI_MOVIM_TRANSF_FUNCI) = ' + QRLAno5.Caption +
                ' and mfe.CO_TIPO_MOVIM = ' +  quotedStr('TE') + ') transfExt' +
                ' from TB25_EMPRESA e '+
                ' where e.CO_EMP = ' + QryRelatorio.FieldByName('CO_EMP').AsString;

    SQL.Text := SqlString;
    Open;

    if not IsEmpty then
    begin
      QRLMAno5.Caption := FieldByName('movExt').AsString;
      QRLIAno5.Caption := FieldByName('movInt').AsString;
      QRLEAno5.Caption := FieldByName('transfExt').AsString;
      QRLTAno5.Caption := IntToStr(FieldByName('movExt').AsInteger + FieldByName('movInt').AsInteger + FieldByName('transfExt').AsInteger );
      totParc := totParc +  StrToInt(QRLTAno5.Caption);
    end
    else
    begin
      QRLMAno5.Caption := '0';
      QRLIAno5.Caption := '0';
      QRLEAno5.Caption := '0';
      QRLTAno5.Caption := '0';
    end;
    //
  end;

  QRLResTotal.Caption := IntToStr(totParc);

  QrlTotal.Caption := IntToStr(StrToInt(QrlTotal.Caption) + totParc);

  QRLTIAno1.Caption := IntToStr(StrToInt(QRLTIAno1.Caption) + StrToInt(QRLIAno1.Caption));
  QRLTEAno1.Caption := IntToStr(StrToInt(QRLTEAno1.Caption) + StrToInt(QRLEAno1.Caption));
  QRLTMAno1.Caption := IntToStr(StrToInt(QRLTMAno1.Caption) + StrToInt(QRLMAno1.Caption));;
  QRLTTAno1.Caption := IntToStr(StrToInt(QRLTTAno1.Caption) + StrToInt(QRLTAno1.Caption));;
  QRLTIAno2.Caption := IntToStr(StrToInt(QRLTIAno2.Caption) + StrToInt(QRLIAno2.Caption));;
  QRLTEAno2.Caption := IntToStr(StrToInt(QRLTEAno2.Caption) + StrToInt(QRLEAno2.Caption));;
  QRLTMAno2.Caption := IntToStr(StrToInt(QRLTMAno2.Caption) + StrToInt(QRLMAno2.Caption));;
  QRLTTAno2.Caption := IntToStr(StrToInt(QRLTTAno2.Caption) + StrToInt(QRLTAno2.Caption));;
  QRLTIAno3.Caption := IntToStr(StrToInt(QRLTIAno3.Caption) + StrToInt(QRLIAno3.Caption));;
  QRLTEAno3.Caption := IntToStr(StrToInt(QRLTEAno3.Caption) + StrToInt(QRLEAno3.Caption));;
  QRLTMAno3.Caption := IntToStr(StrToInt(QRLTMAno3.Caption) + StrToInt(QRLMAno3.Caption));;
  QRLTTAno3.Caption := IntToStr(StrToInt(QRLTTAno3.Caption) + StrToInt(QRLTAno3.Caption));;
  QRLTIAno4.Caption := IntToStr(StrToInt(QRLTIAno4.Caption) + StrToInt(QRLIAno4.Caption));;
  QRLTEAno4.Caption := IntToStr(StrToInt(QRLTEAno4.Caption) + StrToInt(QRLEAno4.Caption));;
  QRLTMAno4.Caption := IntToStr(StrToInt(QRLTMAno4.Caption) + StrToInt(QRLMAno4.Caption));;
  QRLTTAno4.Caption := IntToStr(StrToInt(QRLTTAno4.Caption) + StrToInt(QRLTAno4.Caption));;
  QRLTIAno5.Caption := IntToStr(StrToInt(QRLTIAno5.Caption) + StrToInt(QRLIAno5.Caption));;
  QRLTEAno5.Caption := IntToStr(StrToInt(QRLTEAno5.Caption) + StrToInt(QRLEAno5.Caption));;
  QRLTMAno5.Caption := IntToStr(StrToInt(QRLTMAno5.Caption) + StrToInt(QRLMAno5.Caption));;
  QRLTTAno5.Caption := IntToStr(StrToInt(QRLTTAno5.Caption) + StrToInt(QRLTAno5.Caption));;

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelMapaQuantMovFunc]);

end.