unit U_FrmRelMapaGeralMatricula;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, QuickRpt, DB, ADODB, QRCtrls, ExtCtrls;

type
  TFrmRelMapaGeralMatricula = class(TFrmRelTemplate)
    Detail: TQRBand;
    QRLabel6: TQRLabel;
    QRLabel4: TQRLabel;
    QRLabel17: TQRLabel;
    QRLabel2: TQRLabel;
    QRLabel19: TQRLabel;
    QRLabel16: TQRLabel;
    QRLabel25: TQRLabel;
    QRLabel23: TQRLabel;
    QRLabel15: TQRLabel;
    QRLabel22: TQRLabel;
    QRLabel20: TQRLabel;
    QRLabel14: TQRLabel;
    QRLabel31: TQRLabel;
    QRLabel29: TQRLabel;
    QRLabel13: TQRLabel;
    QRLabel28: TQRLabel;
    QRLabel26: TQRLabel;
    QRLabel12: TQRLabel;
    QRLabel40: TQRLabel;
    QRLabel38: TQRLabel;
    QRLabel11: TQRLabel;
    QRLabel37: TQRLabel;
    QRLabel35: TQRLabel;
    QRLabel10: TQRLabel;
    QRLabel34: TQRLabel;
    QRLabel32: TQRLabel;
    QRLabel9: TQRLabel;
    QRLabel49: TQRLabel;
    QRLabel47: TQRLabel;
    QRLabel8: TQRLabel;
    QRLabel46: TQRLabel;
    QRLabel44: TQRLabel;
    QRLabel7: TQRLabel;
    QRLabel41: TQRLabel;
    QRLabel43: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel1: TQRLabel;
    QRShape1: TQRShape;
    QRDBText1: TQRDBText;
    QRLJanRes: TQRLabel;
    QRLJanInsc: TQRLabel;
    QRLJanMat: TQRLabel;
    qryRes: TADOQuery;
    qryInsc: TADOQuery;
    qryMat: TADOQuery;
    QRLFevRes: TQRLabel;
    QRLFevInsc: TQRLabel;
    QRLFevMat: TQRLabel;
    QRLAbrRes: TQRLabel;
    QRLAbrInsc: TQRLabel;
    QRLAbrMat: TQRLabel;
    QRLMarMat: TQRLabel;
    QRLMarRes: TQRLabel;
    QRLMarInsc: TQRLabel;
    QRLAgoMat: TQRLabel;
    QRLAgoInsc: TQRLabel;
    QRLAgoRes: TQRLabel;
    QRLJulMat: TQRLabel;
    QRLJulInsc: TQRLabel;
    QRLJulRes: TQRLabel;
    QRLJunMat: TQRLabel;
    QRLJunInsc: TQRLabel;
    QRLJunRes: TQRLabel;
    QRLMaiMat: TQRLabel;
    QRLMaiInsc: TQRLabel;
    QRLMaiRes: TQRLabel;
    QRLDezMat: TQRLabel;
    QRLDezInsc: TQRLabel;
    QRLDezRes: TQRLabel;
    QRLNovMat: TQRLabel;
    QRLNovInsc: TQRLabel;
    QRLNovRes: TQRLabel;
    QRLOutMat: TQRLabel;
    QRLOutInsc: TQRLabel;
    QRLOutRes: TQRLabel;
    QRLSetMat: TQRLabel;
    QRLSetRes: TQRLabel;
    QRLSetInsc: TQRLabel;
    QryRelatorioco_cur: TAutoIncField;
    QryRelatoriono_cur: TStringField;
    QRLabel50: TQRLabel;
    QRLPage: TQRLabel;
    QRShape16: TQRShape;
    QRLabel51: TQRLabel;
    qrlAno: TQRLabel;
    QRLabel52: TQRLabel;
    SummaryBand1: TQRBand;
    qrlTotRJan: TQRLabel;
    QRLabel53: TQRLabel;
    qrlTotIMar: TQRLabel;
    qrlTotRFev: TQRLabel;
    qrlTotRMar: TQRLabel;
    qrlTotRAbr: TQRLabel;
    qrlTotRMai: TQRLabel;
    qrlTotRJun: TQRLabel;
    qrlTotRJul: TQRLabel;
    qrlTotRAgo: TQRLabel;
    qrlTotRSet: TQRLabel;
    qrlTotROut: TQRLabel;
    qrlTotRNov: TQRLabel;
    qrlTotRDez: TQRLabel;
    qrlTotIJan: TQRLabel;
    qrlTotIFev: TQRLabel;
    qrlTotIAbr: TQRLabel;
    qrlTotIMai: TQRLabel;
    qrlTotIJun: TQRLabel;
    qrlTotIJul: TQRLabel;
    qrlTotIAgo: TQRLabel;
    qrlTotISet: TQRLabel;
    qrlTotIOut: TQRLabel;
    qrlTotINov: TQRLabel;
    qrlTotIDez: TQRLabel;
    qrlTotMJan: TQRLabel;
    qrlTotMFev: TQRLabel;
    qrlTotMMar: TQRLabel;
    qrlTotMAbr: TQRLabel;
    qrlTotMMai: TQRLabel;
    qrlTotMJun: TQRLabel;
    qrlTotMJul: TQRLabel;
    qrlTotMAgo: TQRLabel;
    qrlTotMSet: TQRLabel;
    qrlTotMOut: TQRLabel;
    qrlTotMNov: TQRLabel;
    qrlTotMDez: TQRLabel;
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
    procedure DetailBeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
  private
    { Private declarations }
  public
    { Public declarations }
    Ano, codigoEmpresa : string;
  end;

var
  FrmRelMapaGeralMatricula: TFrmRelMapaGeralMatricula;

implementation

uses U_DataModuleSGE;

{$R *.dfm}

procedure TFrmRelMapaGeralMatricula.DetailBeforePrint(
  Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
// cria vari�veis para os valores dos meses e tipos de reseva;
  ctJanRes,ctFevRes,ctMarRes,ctAbrRes,ctMaiRes,ctJunRes,ctJulRes,ctAgoRes,ctSetRes,ctOutRes,ctNovRes,ctDezRes : integer;
  //ctJanInsc,ctFevInsc,ctMarInsc,ctAbrInsc,ctMaiInsc,ctJunInsc,ctJulInsc,ctAgoInsc,ctSetInsc,ctOutInsc,ctNovInsc,ctDezInsc : integer;
  ctJanMat,ctFevMat,ctMarMat,ctAbrMat,ctMaiMat,ctJunMat,ctJulMat,ctAgoMat,ctSetMat,ctOutMat,ctNovMat,ctDezMat : integer;
  ano2,mes,dia : Word;

begin
// define as vari�veis como n�meros inteiros;
  inherited;

// reserva
  ctJanRes := 0;
  ctFevRes := 0;
  ctMarRes := 0;
  ctAbrRes := 0;
  ctMaiRes := 0;
  ctJunRes := 0;
  ctJulRes := 0;
  ctAgoRes := 0;
  ctSetRes := 0;
  ctOutRes := 0;
  ctNovRes := 0;
  ctDezRes := 0;

// inscri��o
 { ctJanInsc := 0;
  ctFevInsc := 0;
  ctMarInsc := 0;
  ctAbrInsc := 0;
  ctMaiInsc := 0;
  ctJunInsc := 0;
  ctJulInsc := 0;
  ctAgoInsc := 0;
  ctSetInsc := 0;
  ctOutInsc := 0;
  ctNovInsc := 0;
  ctDezInsc := 0;    }

// matricula
  ctJanMat := 0;
  ctFevMat := 0;
  ctMarMat := 0;
  ctAbrMat := 0;
  ctMaiMat := 0;
  ctJunMat := 0;
  ctJulMat := 0;
  ctAgoMat := 0;
  ctSetMat := 0;
  ctOutMat := 0;
  ctNovMat := 0;
  ctDezMat := 0;

  with qryRes do
  begin
    close;
    // monta a SQL RESERVA;
    SQL.Text := 'SET LANGUAGE PORTUGUESE SELECT DT_CADASTRO FROM TB052_RESERV_MATRI' +
      ' WHERE co_emp1 = ' + codigoEmpresa +
      ' and co_cur = ' + QryRelatorio.FieldByName('co_cur').AsString +
      ' and DT_CADASTRO >= ' + QuotedStr('01/01/' + ano) +
      ' and DT_CADASTRO <= ' + QuotedStr('31/12/' + ano);
    Open;

    while not qryRes.Eof do
    begin
      // contador com "Case" das reservas;
      DecodeDate(fieldByName('DT_CADASTRO').Value,ano2,mes,dia);

      Case mes of
        1 : ctJanRes := ctJanRes + 1;
        2 : ctFevRes := ctFevRes + 1;
        3 : ctMarRes := ctMarRes + 1;
        4 : ctAbrRes := ctAbrRes + 1;
        5 : ctMaiRes := ctMaiRes + 1;
        6 : ctJunRes := ctJunRes + 1;
        7 : ctJulRes := ctJulRes + 1;
        8 : ctAgoRes := ctAgoRes + 1;
        9 : ctSetRes := ctSetRes + 1;
        10 : ctOutRes := ctOutRes + 1;
        11 : ctNovRes := ctNovRes + 1;
        12 : ctDezRes := ctDezRes + 1;
      end;
     Next;
    end;

  end;

 { with qryInsc do
  begin
    close;
    // monta a SQL RESERVA;
    SQL.Text := 'SET LANGUAGE PORTUGUESE SELECT dt_insc_alu FROM tb46_inscricao' +
      ' WHERE co_emp = ' + codigoEmpresa +
      ' and co_cur = ' + QryRelatorio.FieldByName('co_cur').AsString +
      ' and dt_insc_alu >= ' + QuotedStr('01/01/' + ano) +
      ' and dt_insc_alu <= ' + QuotedStr('31/12/' + ano);
    Open;

    while not qryInsc.Eof do
    begin
    // contador com "Case" das reservas;
    DecodeDate(fieldByName('dt_insc_alu').Value,ano2,mes,dia);
      Case mes of
        1 : ctJanInsc := ctJanInsc + 1;
        2 : ctFevInsc := ctFevInsc + 1;
        3 : ctMarInsc := ctMarInsc + 1;
        4 : ctAbrInsc := ctAbrInsc + 1;
        5 : ctMaiInsc := ctMaiInsc + 1;
        6 : ctJunInsc := ctJunInsc + 1;
        7 : ctJulInsc := ctJulInsc + 1;
        8 : ctAgoInsc := ctAgoInsc + 1;
        9 : ctSetInsc := ctSetInsc + 1;
        10 : ctOutInsc := ctOutInsc + 1;
        11 : ctNovInsc := ctNovInsc + 1;
        12 : ctDezInsc := ctDezInsc + 1;
      end;
    Next;
    end;

  end;     }

  with qryMat do
  begin
    close;
    // monta a SQL RESERVA;
    SQL.Text := 'SET LANGUAGE PORTUGUESE SELECT dt_efe_mat FROM tb08_matrcur' +
      ' WHERE co_emp = ' + codigoEmpresa +
      ' and co_cur = ' + QryRelatorio.FieldByName('co_cur').AsString +
      ' and dt_efe_mat >= ' + QuotedStr('01/01/' + ano) +
      ' and dt_efe_mat <= ' + QuotedStr('31/12/' + ano);
    Open;

    while not qryMat.Eof do
    begin
    // contador com "Case" das reservas;
    DecodeDate(fieldByName('dt_efe_mat').Value,ano2,mes,dia);
      Case mes of
        1 : ctJanMat := ctJanMat + 1;
        2 : ctFevMat := ctFevMat + 1;
        3 : ctMarMat := ctMarMat + 1;
        4 : ctAbrMat := ctAbrMat + 1;
        5 : ctMaiMat := ctMaiMat + 1;
        6 : ctJunMat := ctJunMat + 1;
        7 : ctJulMat := ctJulMat + 1;
        8 : ctAgoMat := ctAgoMat + 1;
        9 : ctSetMat := ctSetMat + 1;
        10 : ctOutMat := ctOutMat + 1;
        11 : ctNovMat := ctNovMat + 1;
        12 : ctDezMat := ctDezMat + 1;
      end;
    Next;
    end;

  end;

// QRLabel das RESERVAS;
  QRLJanRes.Caption := IntToStr(ctJanRes);
  qrlTotRJan.Caption := IntToStr(StrToInt(qrlTotRJan.Caption) + StrToInt(QRLJanRes.Caption));
  if QRLJanRes.Caption = '0' then
  begin
   QRLJanRes.Caption := '-';
   QRLJanRes.Alignment := taCenter;
  end;
  
  QRLFevRes.Caption := IntToStr(ctFevRes);
  qrlTotRFev.Caption := IntToStr(StrToInt(qrlTotRFev.Caption) + StrToInt(QRLFevRes.Caption));
  if QRLFevRes.Caption = '0' then
  begin
    QRLFevRes.Caption := '-';
    QRLFevRes.Alignment := taCenter;
  end;

  QRLMarRes.Caption := IntToStr(ctMarRes);
  qrlTotRMar.Caption := IntToStr(StrToInt(qrlTotRMar.Caption) + StrToInt(QRLMarRes.Caption));
  if QRLMarRes.Caption = '0' then
  begin
    QRLMarRes.Caption := '-';
    QRLMarRes.Alignment := taCenter;
  end;

  QRLAbrRes.Caption := IntToStr(ctAbrRes);
  qrlTotRAbr.Caption := IntToStr(StrToInt(qrlTotRAbr.Caption) + StrToInt(QRLAbrRes.Caption));
  if QRLAbrRes.Caption = '0' then
  begin
    QRLAbrRes.Caption := '-';
    QRLAbrRes.Alignment := taCenter;
  end;

  QRLMaiRes.Caption := IntToStr(ctMaiRes);
  qrlTotRMai.Caption := IntToStr(StrToInt(qrlTotRMai.Caption) + StrToInt(QRLMaiRes.Caption));
  if QRLMaiRes.Caption = '0' then
  begin
    QRLMaiRes.Caption := '-';
    QRLMaiRes.Alignment := taCenter;
  end;

  QRLJunRes.Caption := IntToStr(ctJunRes);
  qrlTotRJun.Caption := IntToStr(StrToInt(qrlTotRJun.Caption) + StrToInt(QRLJunRes.Caption));
  if QRLJunRes.Caption = '0' then
  begin
    QRLJunRes.Caption := '-';
    QRLJunRes.Alignment := taCenter;
  end;

  QRLJulRes.Caption := IntToStr(ctJulRes);
  qrlTotRJul.Caption := IntToStr(StrToInt(qrlTotRJul.Caption) + StrToInt(QRLJulRes.Caption));
  if QRLJulRes.Caption = '0' then
  begin
    QRLJulRes.Caption := '-';
    QRLJulRes.Alignment := taCenter;
  end;

  QRLAgoRes.Caption := IntToStr(ctAgoRes);
  qrlTotRAgo.Caption := IntToStr(StrToInt(qrlTotRAgo.Caption) + StrToInt(QRLAgoRes.Caption));
  if QRLAgoRes.Caption = '0' then
  begin
    QRLAgoRes.Caption := '-';
    QRLAgoRes.Alignment := taCenter;
  end;

  QRLSetRes.Caption := IntToStr(ctSetRes);
  qrlTotRSet.Caption := IntToStr(StrToInt(qrlTotRSet.Caption) + StrToInt(QRLSetRes.Caption));
  if QRLSetRes.Caption = '0' then
  begin
    QRLSetRes.Caption := '-';
    QRLSetRes.Alignment := taCenter;
  end;

  QRLOutRes.Caption := IntToStr(ctOutRes);
  qrlTotROut.Caption := IntToStr(StrToInt(qrlTotROut.Caption) + StrToInt(QRLOutRes.Caption));
  if QRLOutRes.Caption = '0' then
  begin
    QRLOutRes.Caption := '-';
    QRLOutRes.Alignment := taCenter;
  end;

  QRLNovRes.Caption := IntToStr(ctNovRes);
  qrlTotRNov.Caption := IntToStr(StrToInt(qrlTotRNov.Caption) + StrToInt(QRLNovRes.Caption));
  if QRLNovRes.Caption = '0' then
  begin
    QRLNovRes.Caption := '-';
    QRLNovRes.Alignment := taCenter;
  end;

  QRLDezRes.Caption := IntToStr(ctDezRes);
  qrlTotRDez.Caption := IntToStr(StrToInt(qrlTotRDez.Caption) + StrToInt(QRLDezRes.Caption));
  if QRLDezRes.Caption = '0' then
  begin
    QRLDezRes.Caption := '-';
    QRLDezRes.Alignment := taCenter;
  end;

// QRLabel das INSCRI��ES;
  {QRLJanInsc.Caption := IntToStr(ctJanInsc);
  qrlTotIJan.Caption := IntToStr(StrToInt(qrlTotIJan.Caption) + StrToInt(QRLJanInsc.Caption));
  if QRLJanInsc.Caption = '0' then
  begin
    QRLJanInsc.Caption := '-';
    QRLJanInsc.Alignment := taCenter;
  end;

  QRLFevInsc.Caption := IntToStr(ctFevInsc);
  qrlTotIFev.Caption := IntToStr(StrToInt(qrlTotIFev.Caption) + StrToInt(QRLFevInsc.Caption));
  if QRLFevInsc.Caption = '0' then
  begin
    QRLFevInsc.Caption := '-';
    QRLFevInsc.Alignment := taCenter;
  end;

  QRLMarInsc.Caption := IntToStr(ctMarInsc);
  qrlTotIMar.Caption := IntToStr(StrToInt(qrlTotIMar.Caption) + StrToInt(QRLMarInsc.Caption));
  if QRLMarInsc.Caption = '0' then
  begin
    QRLMarInsc.Caption := '-';
    QRLMarInsc.Alignment := taCenter;
  end;

  QRLAbrInsc.Caption := IntToStr(ctAbrInsc);
  qrlTotIAbr.Caption := IntToStr(StrToInt(qrlTotIAbr.Caption) + StrToInt(QRLAbrInsc.Caption));
  if QRLAbrInsc.Caption = '0' then
  begin
    QRLAbrInsc.Caption := '-';
    QRLAbrInsc.Alignment := taCenter;
  end;

  QRLMaiInsc.Caption := IntToStr(ctMaiInsc);
  qrlTotIMai.Caption := IntToStr(StrToInt(qrlTotIMai.Caption) + StrToInt(QRLMaiInsc.Caption));
  if QRLMaiInsc.Caption = '0' then
  begin
    QRLMaiInsc.Caption := '-';
    QRLMaiInsc.Alignment := taCenter;
  end;

  QRLJunInsc.Caption := IntToStr(ctJunInsc);
  qrlTotIJun.Caption := IntToStr(StrToInt(qrlTotIJun.Caption) + StrToInt(QRLJunInsc.Caption));
  if QRLJunInsc.Caption = '0' then
  begin
    QRLJunInsc.Caption := '-';
    QRLJunInsc.Alignment := taCenter;
  end;

  QRLJulInsc.Caption := IntToStr(ctJulInsc);
  qrlTotIJul.Caption := IntToStr(StrToInt(qrlTotIJul.Caption) + StrToInt(QRLJulInsc.Caption));
  if QRLJulInsc.Caption = '0' then
  begin
    QRLJulInsc.Caption := '-';
    QRLJulInsc.Alignment := taCenter;
  end;

  QRLAgoInsc.Caption := IntToStr(ctAgoInsc);
  qrlTotIAgo.Caption := IntToStr(StrToInt(qrlTotIAgo.Caption) + StrToInt(QRLAgoInsc.Caption));
  if QRLAgoInsc.Caption = '0' then
  begin
    QRLAgoInsc.Caption := '-';
    QRLAgoInsc.Alignment := taCenter;
  end;

  QRLSetInsc.Caption := IntToStr(ctSetInsc);
  qrlTotISet.Caption := IntToStr(StrToInt(qrlTotISet.Caption) + StrToInt(QRLSetInsc.Caption));
  if QRLSetInsc.Caption = '0' then
  begin
    QRLSetInsc.Caption := '-';
    QRLSetInsc.Alignment := taCenter;
  end;

  QRLOutInsc.Caption := IntToStr(ctOutInsc);
  qrlTotIOut.Caption := IntToStr(StrToInt(qrlTotIOut.Caption) + StrToInt(QRLOutInsc.Caption));
  if QRLOutInsc.Caption = '0' then
  begin
    QRLOutInsc.Caption := '-';
    QRLOutInsc.Alignment := taCenter;
  end;

  QRLNovInsc.Caption := IntToStr(ctNovInsc);
  qrlTotINov.Caption := IntToStr(StrToInt(qrlTotINov.Caption) + StrToInt(QRLNovInsc.Caption));
  if QRLNovInsc.Caption = '0' then
  begin
    QRLNovInsc.Caption := '-';
    QRLNovInsc.Alignment := taCenter;
  end;

  QRLDezInsc.Caption := IntToStr(ctDezInsc);
  qrlTotIDez.Caption := IntToStr(StrToInt(qrlTotIDez.Caption) + StrToInt(QRLDezInsc.Caption));
  if QRLDezInsc.Caption = '0' then
  begin
    QRLDezInsc.Caption := '-';
    QRLDezInsc.Alignment := taCenter;
  end;     }

// QRLabel das MATR�CULAS;
  QRLJanMat.Caption := IntToStr(ctJanMat);
  qrlTotMJan.Caption := IntToStr(StrToInt(qrlTotMJan.Caption) + StrToInt(QRLJanMat.Caption));
  if QRLJanMat.Caption = '0' then
  begin
    QRLJanMat.Caption := '-';
    QRLJanMat.Alignment := taCenter;
  end;

  QRLFevMat.Caption := IntToStr(ctFevMat);
  qrlTotMFev.Caption := IntToStr(StrToInt(qrlTotMFev.Caption) + StrToInt(QRLFevMat.Caption));
  if QRLFevMat.Caption = '0' then
  begin
    QRLFevMat.Caption := '-';
    QRLFevMat.Alignment := taCenter;
  end;

  QRLMarMat.Caption := IntToStr(ctMarMat);
  qrlTotMMar.Caption := IntToStr(StrToInt(qrlTotMMar.Caption) + StrToInt(QRLMarMat.Caption));
  if QRLMarMat.Caption = '0' then
  begin
    QRLMarMat.Caption := '-';
    QRLMarMat.Alignment := taCenter;
  end;

  QRLAbrMat.Caption := IntToStr(ctAbrMat);
  qrlTotMAbr.Caption := IntToStr(StrToInt(qrlTotMAbr.Caption) + StrToInt(QRLAbrMat.Caption));
  if QRLAbrMat.Caption = '0' then
  begin
    QRLAbrMat.Caption := '-';
    QRLAbrMat.Alignment := taCenter;
  end;

  QRLMaiMat.Caption := IntToStr(ctMaiMat);
  qrlTotMMai.Caption := IntToStr(StrToInt(qrlTotMMai.Caption) + StrToInt(QRLMaiMat.Caption));
  if QRLMaiMat.Caption = '0' then
  begin
    QRLMaiMat.Caption := '-';
    QRLMaiMat.Alignment := taCenter;
  end;

  QRLJunMat.Caption := IntToStr(ctJunMat);
  qrlTotMJun.Caption := IntToStr(StrToInt(qrlTotMJun.Caption) + StrToInt(QRLJunMat.Caption));
  if QRLJunMat.Caption = '0' then
  begin
    QRLJunMat.Caption := '-';
    QRLJunMat.Alignment := taCenter;
  end;

  QRLJulMat.Caption := IntToStr(ctJulMat);
  qrlTotMJul.Caption := IntToStr(StrToInt(qrlTotMJul.Caption) + StrToInt(QRLJulMat.Caption));
  if QRLJulMat.Caption = '0' then
  begin
    QRLJulMat.Caption := '-';
    QRLJulMat.Alignment := taCenter;
  end;

  QRLAgoMat.Caption := IntToStr(ctAgoMat);
  qrlTotMAgo.Caption := IntToStr(StrToInt(qrlTotMAgo.Caption) + StrToInt(QRLAgoMat.Caption));
  if QRLAgoMat.Caption = '0' then
  begin
    QRLAgoMat.Caption := '-';
    QRLAgoMat.Alignment := taCenter;
  end;

  QRLSetMat.Caption := IntToStr(ctSetMat);
  qrlTotMSet.Caption := IntToStr(StrToInt(qrlTotMSet.Caption) + StrToInt(QRLSetMat.Caption));
  if QRLSetMat.Caption = '0' then
  begin
    QRLSetMat.Caption := '-';
    QRLSetMat.Alignment := taCenter;
  end;

  QRLOutMat.Caption := IntToStr(ctOutMat);
  qrlTotMOut.Caption := IntToStr(StrToInt(qrlTotMOut.Caption) + StrToInt(QRLOutMat.Caption));
  if QRLOutMat.Caption = '0' then
  begin
    QRLOutMat.Caption := '-';
    QRLOutMat.Alignment := taCenter;
  end;

  QRLNovMat.Caption := IntToStr(ctNovMat);
  qrlTotMNov.Caption := IntToStr(StrToInt(qrlTotMNov.Caption) + StrToInt(QRLNovMat.Caption));
  if QRLNovMat.Caption = '0' then
  begin
    QRLNovMat.Caption := '-';
    QRLNovMat.Alignment := taCenter;
  end;

  QRLDezMat.Caption := IntToStr(ctDezMat);
  qrlTotMDez.Caption := IntToStr(StrToInt(qrlTotMDez.Caption) + StrToInt(QRLDezMat.Caption));
  if QRLDezMat.Caption = '0' then
  begin
    QRLDezMat.Caption := '-';
    QRLDezMat.Alignment := taCenter;
  end;

// deixa tudo zebrado;
  if Detail.Color = clWhite then
    Detail.Color := $00D8D8D8
  else
    Detail.Color := clWhite;

end;


procedure TFrmRelMapaGeralMatricula.QuickRep1BeforePrint(
  Sender: TCustomQuickRep; var PrintReport: Boolean);
begin
  inherited;
// vari�veis dos totais da RESERVA como zerado como inteiro;
  qrlTotRJan.Caption := '0';
  qrlTotRFev.Caption := '0';
  qrlTotRMar.Caption := '0';
  qrlTotRAbr.Caption := '0';
  qrlTotRMai.Caption := '0';
  qrlTotRJun.Caption := '0';
  qrlTotRJul.Caption := '0';
  qrlTotRAgo.Caption := '0';
  qrlTotRSet.Caption := '0';
  qrlTotROut.Caption := '0';
  qrlTotRNov.Caption := '0';
  qrlTotRDez.Caption := '0';

// vari�veis dos totais da INSCRI��O como zerado como inteiro;
 { qrlTotIJan.Caption := '0';
  qrlTotIFev.Caption := '0';
  qrlTotIMar.Caption := '0';
  qrlTotIAbr.Caption := '0';
  qrlTotIMai.Caption := '0';
  qrlTotIJun.Caption := '0';
  qrlTotIJul.Caption := '0';
  qrlTotIAgo.Caption := '0';
  qrlTotISet.Caption := '0';
  qrlTotIOut.Caption := '0';
  qrlTotINov.Caption := '0';
  qrlTotIDez.Caption := '0'; }

// vari�veis dos totais da MATR�CULA como zerado como inteiro;
  qrlTotMJan.Caption := '0';
  qrlTotMFev.Caption := '0';
  qrlTotMMar.Caption := '0';
  qrlTotMAbr.Caption := '0';
  qrlTotMMai.Caption := '0';
  qrlTotMJun.Caption := '0';
  qrlTotMJul.Caption := '0';
  qrlTotMAgo.Caption := '0';
  qrlTotMSet.Caption := '0';
  qrlTotMOut.Caption := '0';
  qrlTotMNov.Caption := '0';
  qrlTotMDez.Caption := '0';

end;

end.