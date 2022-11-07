unit U_FrmRelEvolRecEDes;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls, DateUtils;

type
  TFrmRelEvolRecEDes = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QrlVlPR: TQRLabel;
    QRLabel11: TQRLabel;
    QRLPage: TQRLabel;
    SummaryBand1: TQRBand;
    QRLabel2: TQRLabel;
    QRShape1: TQRShape;
    QRLabel4: TQRLabel;
    QRLabel5: TQRLabel;
    QRLabel6: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel10: TQRLabel;
    QRLParametros: TQRLabel;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QrlVlPD: TQRLabel;
    QrlVlPDi: TQRLabel;
    QrlVlRR: TQRLabel;
    QrlVlRD: TQRLabel;
    QrlVlRDi: TQRLabel;
    QRLMes: TQRLabel;
    QRLabel7: TQRLabel;
    QRLTotPR: TQRLabel;
    QRLTotPD: TQRLabel;
    QRLTotPDi: TQRLabel;
    QRLTotRR: TQRLabel;
    QRLTotRD: TQRLabel;
    QRLTotRDi: TQRLabel;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRSControleCor: TQRShape;
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure SummaryBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    VlTotPR, VlTotPD, VlTotRR, VlTotRD: Double;
  public
    { Public declarations }
    codigoEmpresa, anoRef : String;
    mesRef : Integer;
  end;

var
  FrmRelEvolRecEDes: TFrmRelEvolRecEDes;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelEvolRecEDes.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
  mesRef := 1;

  VlTotPR      := 0;
  VlTotPD     := 0;
  VlTotRR      := 0;
  VlTotRD     := 0;
end;

procedure TFrmRelEvolRecEDes.SummaryBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLTotPR.Caption := FloatToStrF(VlTotPR ,ffNumber,15,2);
  QRLTotPD.Caption := FloatToStrF(VlTotPD,ffNumber,15,2);
  QRLTotPDi.Caption := FloatToStrF(VlTotPR - VlTotRR,ffNumber,15,2);
  QRLTotRR.Caption := FloatToStrF(VlTotRR,ffNumber,15,2);
  QRLTotRD.Caption := FloatToStrF(VlTotRD,ffNumber,15,2);
  QRLTotRDi.Caption := FloatToStrF(VlTotPD - VlTotRD,ffNumber,15,2);
end;

procedure TFrmRelEvolRecEDes.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  case mesRef of
    1: QRLMes.Caption := 'Janeiro';
    2: QRLMes.Caption := 'Fevereiro';
    3: QRLMes.Caption := 'Março';
    4: QRLMes.Caption := 'Abril';
    5: QRLMes.Caption := 'Maio';
    6: QRLMes.Caption := 'Junho';
    7: QRLMes.Caption := 'Julho';
    8: QRLMes.Caption := 'Agosto';
    9: QRLMes.Caption := 'Setembro';
    10: QRLMes.Caption := 'Outubro';
    11: QRLMes.Caption := 'Novembro';
    12: QRLMes.Caption := 'Dezembro';
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select E.CO_EMP, (select SUM(pc.VL_PLAN_MES'+ IntToStr(mesRef) +')'+
                'from TB111_PLANCONTAB pc ' +
                'join TB56_PLANOCTA p on pc.CO_SEQU_PC = p.CO_SEQU_PC '+
                'join TB53_GRP_CTA gc on gc.co_grup_cta = p.co_grup_cta '+
                'where pc.CO_EMP = E.CO_EMP and pc.CO_ANO_REF = '+ anoRef +
                'and gc.TP_GRUP_CTA = '+ QuotedStr('D') +') as planDespesa, '+
                '(select SUM(pc.VL_PLAN_MES'+ IntToStr(mesRef) +')'+
                'from TB111_PLANCONTAB pc '+
                'join TB56_PLANOCTA p on pc.CO_SEQU_PC = p.CO_SEQU_PC '+
                'join TB53_GRP_CTA gc on gc.co_grup_cta = p.co_grup_cta '+
                'where pc.CO_EMP = E.CO_EMP and pc.CO_ANO_REF = ' + anoRef +
                'and gc.TP_GRUP_CTA = '+  QuotedStr('C') + ') as planReceita,'+
                '(select SUM(pc.VR_PAR_DOC) ' +
                'from TB47_CTA_RECEB pc ' +
                'where pc.CO_EMP = E.CO_EMP and month(pc.DT_VEN_DOC) = ' + IntToStr(mesRef)  + ' and YEAR(pc.DT_VEN_DOC) = ' + anoRef +
                'and pc.IC_SIT_DOC in ('+ QuotedStr('A') + ',' + QuotedStr('Q') + ') ) as planReaReceita,'+
                '(select SUM(pc.VR_PAR_DOC) ' +
                'from TB38_CTA_PAGAR pc ' +
                'where pc.CO_EMP = E.CO_EMP and month(pc.DT_VEN_DOC) = ' + IntToStr(mesRef)  + ' and YEAR(pc.DT_VEN_DOC) = ' + anoRef +
                'and pc.IC_SIT_DOC in ('+ QuotedStr('A') + ',' + QuotedStr('Q') + ') ) as planReaDespesa ' +
                'from TB25_EMPRESA E ' +
                'where E.CO_EMP = ' + codigoEmpresa;
    Open;

    if not IsEmpty then
    begin
      if not FieldByName('planDespesa').IsNull then
      begin
        QrlVlPD.Caption := FloatToStrF(FieldByName('planDespesa').AsFloat,ffNumber,15,2);
        VlTotPD := VlTotPD + FieldByName('planDespesa').AsFloat;

        if not FieldByName('planReaDespesa').IsNull then
          QrlVlRDi.Caption := FloatToStrF(FieldByName('planDespesa').AsFloat - FieldByName('planReaDespesa').AsFloat, ffNumber, 15, 2)
        else
          QrlVlRDi.Caption := FloatToStrF(FieldByName('planDespesa').AsFloat, ffNumber, 15, 2);
      end
      else
      begin
        QrlVlPD.Caption := '';
      end;

      if not FieldByName('planReceita').IsNull then
      begin
        QrlVlPR.Caption := FloatToStrF(FieldByName('planReceita').AsFloat,ffNumber,15,2);
        VlTotPR := VlTotPR + FieldByName('planReceita').AsFloat;

        if not FieldByName('planReaReceita').IsNull then
          QrlVlPDi.Caption := FloatToStrF(FieldByName('planReceita').AsFloat - FieldByName('planReaReceita').AsFloat, ffNumber, 15, 2)
        else
          QrlVlPDi.Caption := FloatToStrF(FieldByName('planReceita').AsFloat, ffNumber, 15, 2);
      end
      else
      begin
        QrlVlPR.Caption := '';

        if not FieldByName('planReaReceita').IsNull then
          QrlVlPDi.Caption := FloatToStrF(FieldByName('planReaReceita').AsFloat, ffNumber, 15, 2)
        else
          QrlVlPDi.Caption := '-';
      end;

      if not FieldByName('planReaReceita').IsNull then
      begin
        QrlVlRR.Caption := FloatToStrF(FieldByName('planReaReceita').AsFloat,ffNumber,15,2);
        VlTotRR := VlTotRR + FieldByName('planReaReceita').AsFloat;
      end
      else
      begin
        QrlVlRR.Caption := '';

        if not FieldByName('planReaDespesa').IsNull then
          QrlVlRDi.Caption := FloatToStrF(FieldByName('planReaDespesa').AsFloat, ffNumber, 15, 2)
        else
          QrlVlRDi.Caption := '-';
      end;

      if not FieldByName('planReaDespesa').IsNull then
      begin
        QrlVlRD.Caption := FloatToStrF(FieldByName('planReaDespesa').AsFloat,ffNumber,15,2);
        VlTotRD := VlTotRD + FieldByName('planReaDespesa').AsFloat;
      end
      else
      begin
        QrlVlRD.Caption := '';
      end;

    end
    else
    begin
      QrlVlPR.Caption := '-';
      QrlVlPD.Caption := '-';
      QrlVlPDi.Caption := '-';
      QrlVlRR.Caption := '-';
      QrlVlRD.Caption := '-';
      QrlVlRDi.Caption := '-';
    end;
  end;

  if QRSControleCor.Brush.Color = clWhite then
  begin
    QRSControleCor.Brush.Color := $00D8D8D8;
    QRSControleCor.Pen.Color := $00D8D8D8;
  end
  else
  begin
    QRSControleCor.Brush.Color := clWhite;
    QRSControleCor.Pen.Color := clWhite;
  end;

  mesRef := mesRef + 1;
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelEvolRecEDes]);

end.
