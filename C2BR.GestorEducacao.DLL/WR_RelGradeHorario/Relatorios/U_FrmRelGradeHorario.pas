unit U_FrmRelGradeHorario;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, jpeg, QuickRpt, ExtCtrls;

type
  TFrmRelGradeHorario = class(TFrmRelTemplate)
    QRBand1: TQRBand;
    QRLTempo: TQRLabel;
    QRLDomingo: TQRLabel;
    QRLSegunda: TQRLabel;
    QRLTerca: TQRLabel;
    QRLQuarta: TQRLabel;
    QRLQuinta: TQRLabel;
    QRLSexta: TQRLabel;
    QRLSabado: TQRLabel;
    QRGroup3: TQRGroup;
    QRShape1: TQRShape;
    QRShape2: TQRShape;
    QRShape3: TQRShape;
    QRLabel9: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel15: TQRLabel;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    QRBand2: TQRBand;
    QRMLegenda: TQRMemo;
    QRLabel2: TQRLabel;
    QRLabel3: TQRLabel;
    QRLPage: TQRLabel;
    QRLabel4: TQRLabel;
    QRShape8: TQRShape;
    QRShape9: TQRShape;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    QRShape15: TQRShape;
    QRLParametros: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRBand2BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QRGroup3BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
  private
    { Private declarations }
    vDescricaoMaterias: String;
  public
    { Public declarations }
    codigoEmpresa : String;
  end;

var
  FrmRelGradeHorario: TFrmRelGradeHorario;

implementation

uses U_DataModuleSGE, U_Funcoes;

{$R *.dfm}

procedure TFrmRelGradeHorario.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  SqlString: String;
  //ct: Integer;
begin
  inherited;

  if QRBand1.Color = clWhite then
     QRBand1.Color := $00D8D8D8
  else
     QRBand1.Color := clWhite;

  QRLDomingo.Caption := '';
  QRLSegunda.Caption := '';
  QRLTerca.Caption := '';
  QRLQuarta.Caption := '';
  QRLQuinta.Caption := '';
  QRLSexta.Caption := '';
  QRLSabado.Caption := '';

  QRLTempo.Caption := IntToStr(QryRelatorio.FieldByName('NR_TEMPO').AsInteger) + 'º Tempo - ' + QryRelatorio.FieldByName('HR_INICIO').AsString + ' / ' + QryRelatorio.FieldByName('HR_TERMI').AsString;

  //ct := 0;
  { Recupera as matéria }
    with DM.QrySql do
    begin
      Close;
      Sql.Clear;
      SqlString:= 'Select distinct G.*, CM.NO_MATERIA, CM.NO_SIGLA_MATERIA ' +
                  ' From TB05_GRD_HORAR G, TB02_MATERIA M, TB43_GRD_CURSO GC, TB107_CADMATERIAS CM ' +
                  ' Where G.CO_EMP = GC.CO_EMP AND G.CO_EMP = M.CO_EMP AND ' +
                  '       G.CO_CUR = M.CO_CUR AND ' +
                  '       G.CO_MAT = M.CO_MAT AND ' +
                  '       G.CO_CUR = ' + QryRelatorio.FieldByName('CO_CUR').AsString + ' AND ' +
                  '       G.CO_TUR = ' + QryRelatorio.FieldByName('CO_TUR').AsString + ' AND ' +
                  '       G.CO_ANO_GRADE = ' + QryRelatorio.FieldByName('CO_ANO_GRADE').AsString + ' AND ' +
                  '       G.NR_TEMPO = ' + QryRelatorio.FieldByName('NR_TEMPO').AsString + ' AND ' +
                  '       G.TP_TURNO = ' + quotedStr(QryRelatorio.FieldByName('TP_TURNO').AsString) + ' AND ' +
                  '       G.CO_CUR = GC.CO_CUR AND ' +
                  '       G.CO_MAT = GC.CO_MAT AND ' +
                  '       M.ID_MATERIA = CM.ID_MATERIA AND ' +
                  '       GC.CO_MODU_CUR = ' + QryRelatorio.FieldByName('CO_MODU_CUR').AsString +
                  ' and G.CO_EMP = ' + codigoEmpresa +
                  ' Order By G.CO_DIA_SEMA_GRD ';
      SQL.Text := SqlString;
      Open;

      if not IsEmpty then
      begin
        First;
        while not eof do
        begin
          case FieldByName('CO_DIA_SEMA_GRD').AsInteger of
            0: QRLDomingo.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            1: QRLSegunda.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            2: QRLTerca.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            3: QRLQuarta.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            4: QRLQuinta.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            5: QRLSexta.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
            6: QRLSabado.Caption := FieldByName('NO_SIGLA_MATERIA').AsString;
           end;

          { Recupera a legada das disciplinas }
          if Pos(FieldByName('NO_MATERIA').AsString, vDescricaoMaterias) = 0 then
          begin
//            if ct > 2 then
//              vDescricaoMaterias := vDescricaoMaterias + #13;

             vDescricaoMaterias := vDescricaoMaterias + FieldByName('NO_SIGLA_MATERIA').AsString + ' - ' + FieldByName('NO_MATERIA').AsString + ' / ';// #13;
//             vDescricaoMaterias := vDescricaoMaterias + Replica(' ',15, ' ');
//             ct := ct + 1;
          end;

          Next;
        end;
      end;
    end;

    if QRLDomingo.Caption = '' then QRLDomingo.Caption := '- - -';
    if QRLSegunda.Caption = '' then QRLSegunda.Caption := '- - -';
    if QRLTerca.Caption = '' then QRLTerca.Caption := '- - -';
    if QRLQuarta.Caption = '' then QRLQuarta.Caption := '- - -';
    if QRLQuinta.Caption = '' then QRLQuinta.Caption := '- - -';
    if QRLSexta.Caption = '' then QRLSexta.Caption := '- - -';
    if QRLSabado.Caption = '' then QRLSabado.Caption := '- - -';

end;

procedure TFrmRelGradeHorario.QRBand2BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  QRMLegenda.Lines.Text := vDescricaoMaterias;
end;

procedure TFrmRelGradeHorario.QRGroup3BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
begin
  inherited;
  vDescricaoMaterias := '';
end;

initialization
  { Registra a classe do form }
  RegisterClasses([TFrmRelGradeHorario]);

end.
