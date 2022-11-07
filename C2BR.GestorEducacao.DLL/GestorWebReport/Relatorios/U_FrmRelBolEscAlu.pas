unit U_FrmRelBolEscAlu;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, U_FrmRelTemplate, DB, ADODB, QRCtrls, QuickRpt, ExtCtrls,
  QrAngLbl;

type
  TFrmRelBolEscAlu = class(TFrmRelTemplate)
    QRShape1: TQRShape;
    qrlAluno: TQRLabel;
    qrlSexo: TQRLabel;
    qrlDataNasc: TQRLabel;
    qrdbtDataNasc: TQRDBText;
    qrlNoNis: TQRLabel;
    qrdbtIdade: TQRDBText;
    qrdbtNoNis: TQRDBText;
    qrdbiFotoAluno: TQRDBImage;
    qrlPaisResp: TQRLabel;
    qrlPai: TQRLabel;
    qrlMae: TQRLabel;
    qrlResp: TQRLabel;
    qrdbtPai: TQRDBText;
    qrdbtMae: TQRDBText;
    qrlTitMatricula: TQRLabel;
    qrlSerie: TQRLabel;
    qrlTurma: TQRLabel;
    qrlTurno: TQRLabel;
    qrlModulo: TQRLabel;
    qrlProfessor: TQRLabel;
    qrdbtSerie: TQRDBText;
    qrdbtTurma: TQRDBText;
    qrdbtModulo: TQRDBText;
    QRShape2: TQRShape;
    qrlAproveitamento: TQRLabel;
    qrlDisciplina: TQRLabel;
    QRShape8: TQRShape;
    QRShape4: TQRShape;
    QRShape5: TQRShape;
    QRShape6: TQRShape;
    QRShape7: TQRShape;
    qrlBimestre1: TQRLabel;
    qrlBimestre2: TQRLabel;
    qrlBimestre3: TQRLabel;
    qrlBimestre4: TQRLabel;
    qrlResult: TQRLabel;
    QRShape10: TQRShape;
    QRShape11: TQRShape;
    QRShape12: TQRShape;
    QRShape13: TQRShape;
    QRShape14: TQRShape;
    qraNotaConceito1: TQRAngledLabel;
    qraRecuperacao1: TQRAngledLabel;
    qraMedia1: TQRAngledLabel;
    qraFaltas1: TQRAngledLabel;
    qraFrequencia1: TQRAngledLabel;
    qraComportamento1: TQRAngledLabel;
    QRAngledLabel13: TQRAngledLabel;
    QRShape9: TQRShape;
    QRShape22: TQRShape;
    QRShape21: TQRShape;
    QRShape20: TQRShape;
    QRShape19: TQRShape;
    QRShape18: TQRShape;
    QRShape17: TQRShape;
    QRShape16: TQRShape;
    QRShape15: TQRShape;
    qraResultProvaFinal: TQRAngledLabel;
    qraResultMediaTurma: TQRAngledLabel;
    qraResultSinteseBimestral: TQRAngledLabel;
    qraResultFrequencia: TQRAngledLabel;
    qraResultFaltas: TQRAngledLabel;
    qraResultMediaFinal: TQRAngledLabel;
    qraNotaConceito2: TQRAngledLabel;
    qraRecuperacao2: TQRAngledLabel;
    qraMedia2: TQRAngledLabel;
    qraFaltas2: TQRAngledLabel;
    qraFrequencia2: TQRAngledLabel;
    qraComportamento2: TQRAngledLabel;
    QRShape3: TQRShape;
    QRShape23: TQRShape;
    QRShape24: TQRShape;
    QRShape25: TQRShape;
    QRShape26: TQRShape;
    QRShape27: TQRShape;
    qraNotaConceito3: TQRAngledLabel;
    qraRecuperacao3: TQRAngledLabel;
    qraMedia3: TQRAngledLabel;
    qraFaltas3: TQRAngledLabel;
    qraFrequencia3: TQRAngledLabel;
    qraComportamento3: TQRAngledLabel;
    QRShape28: TQRShape;
    QRShape29: TQRShape;
    QRShape30: TQRShape;
    QRShape31: TQRShape;
    QRShape32: TQRShape;
    QRShape33: TQRShape;
    qraNotaConceito4: TQRAngledLabel;
    qraRecuperacao4: TQRAngledLabel;
    qraMedia4: TQRAngledLabel;
    qraFaltas4: TQRAngledLabel;
    qraFrequencia4: TQRAngledLabel;
    qraComportamento4: TQRAngledLabel;
    QRShape35: TQRShape;
    QRShape36: TQRShape;
    QRShape37: TQRShape;
    QRShape38: TQRShape;
    QRShape39: TQRShape;
    QRShape34: TQRShape;
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
    qraBaseNacional: TQRAngledLabel;
    QRShape54: TQRShape;
    qraParte: TQRAngledLabel;
    qraDiversificada: TQRAngledLabel;
    QRShape55: TQRShape;
    QRShape56: TQRShape;
    QRShape57: TQRShape;
    QRShape58: TQRShape;
    QRShape59: TQRShape;
    qraExtra: TQRAngledLabel;
    QRShape61: TQRShape;
    QRShape62: TQRShape;
    QRShape63: TQRShape;
    QRShape60: TQRShape;
    qrlMediaTotal: TQRLabel;
    band1: TQRBand;
    m1: TQRLabel;
    m111: TQRLabel;
    m112: TQRLabel;
    m113: TQRLabel;
    m114: TQRLabel;
    m115: TQRLabel;
    m116: TQRLabel;
    m121: TQRLabel;
    m122: TQRLabel;
    m123: TQRLabel;
    m124: TQRLabel;
    m125: TQRLabel;
    m126: TQRLabel;
    m131: TQRLabel;
    m132: TQRLabel;
    m133: TQRLabel;
    m134: TQRLabel;
    m135: TQRLabel;
    m136: TQRLabel;
    m141: TQRLabel;
    m142: TQRLabel;
    m143: TQRLabel;
    m144: TQRLabel;
    m145: TQRLabel;
    m146: TQRLabel;
    m151: TQRLabel;
    m152: TQRLabel;
    m153: TQRLabel;
    m154: TQRLabel;
    m155: TQRLabel;
    m156: TQRLabel;
    m256: TQRLabel;
    m255: TQRLabel;
    m254: TQRLabel;
    m253: TQRLabel;
    m252: TQRLabel;
    m251: TQRLabel;
    m246: TQRLabel;
    m245: TQRLabel;
    m244: TQRLabel;
    m243: TQRLabel;
    m242: TQRLabel;
    m241: TQRLabel;
    m236: TQRLabel;
    m235: TQRLabel;
    m234: TQRLabel;
    m233: TQRLabel;
    m232: TQRLabel;
    m231: TQRLabel;
    m226: TQRLabel;
    m225: TQRLabel;
    m224: TQRLabel;
    m223: TQRLabel;
    m222: TQRLabel;
    m221: TQRLabel;
    m216: TQRLabel;
    m215: TQRLabel;
    m214: TQRLabel;
    m213: TQRLabel;
    m212: TQRLabel;
    m211: TQRLabel;
    m2: TQRLabel;
    m356: TQRLabel;
    m355: TQRLabel;
    m354: TQRLabel;
    m353: TQRLabel;
    m352: TQRLabel;
    m351: TQRLabel;
    m346: TQRLabel;
    m345: TQRLabel;
    m344: TQRLabel;
    m343: TQRLabel;
    m342: TQRLabel;
    m341: TQRLabel;
    m336: TQRLabel;
    m335: TQRLabel;
    m334: TQRLabel;
    m333: TQRLabel;
    m332: TQRLabel;
    m331: TQRLabel;
    m326: TQRLabel;
    m325: TQRLabel;
    m324: TQRLabel;
    m323: TQRLabel;
    m322: TQRLabel;
    m321: TQRLabel;
    m316: TQRLabel;
    m315: TQRLabel;
    m314: TQRLabel;
    m313: TQRLabel;
    m312: TQRLabel;
    m311: TQRLabel;
    m3: TQRLabel;
    m456: TQRLabel;
    m455: TQRLabel;
    m454: TQRLabel;
    m453: TQRLabel;
    m452: TQRLabel;
    m451: TQRLabel;
    m446: TQRLabel;
    m445: TQRLabel;
    m444: TQRLabel;
    m443: TQRLabel;
    m442: TQRLabel;
    m441: TQRLabel;
    m436: TQRLabel;
    m435: TQRLabel;
    m434: TQRLabel;
    m433: TQRLabel;
    m432: TQRLabel;
    m431: TQRLabel;
    m426: TQRLabel;
    m425: TQRLabel;
    m424: TQRLabel;
    m423: TQRLabel;
    m422: TQRLabel;
    m421: TQRLabel;
    m416: TQRLabel;
    m415: TQRLabel;
    m414: TQRLabel;
    m413: TQRLabel;
    m412: TQRLabel;
    m411: TQRLabel;
    m4: TQRLabel;
    m556: TQRLabel;
    m555: TQRLabel;
    m554: TQRLabel;
    m553: TQRLabel;
    m552: TQRLabel;
    m551: TQRLabel;
    m546: TQRLabel;
    m545: TQRLabel;
    m544: TQRLabel;
    m543: TQRLabel;
    m542: TQRLabel;
    m541: TQRLabel;
    m536: TQRLabel;
    m535: TQRLabel;
    m534: TQRLabel;
    m533: TQRLabel;
    m532: TQRLabel;
    m531: TQRLabel;
    m526: TQRLabel;
    m525: TQRLabel;
    m524: TQRLabel;
    m523: TQRLabel;
    m522: TQRLabel;
    m521: TQRLabel;
    m516: TQRLabel;
    m515: TQRLabel;
    m514: TQRLabel;
    m513: TQRLabel;
    m512: TQRLabel;
    m511: TQRLabel;
    m5: TQRLabel;
    m756: TQRLabel;
    m755: TQRLabel;
    m754: TQRLabel;
    m753: TQRLabel;
    m752: TQRLabel;
    m751: TQRLabel;
    m746: TQRLabel;
    m745: TQRLabel;
    m744: TQRLabel;
    m743: TQRLabel;
    m742: TQRLabel;
    m741: TQRLabel;
    m736: TQRLabel;
    m735: TQRLabel;
    m734: TQRLabel;
    m733: TQRLabel;
    m732: TQRLabel;
    m731: TQRLabel;
    m726: TQRLabel;
    m725: TQRLabel;
    m724: TQRLabel;
    m723: TQRLabel;
    m722: TQRLabel;
    m721: TQRLabel;
    m716: TQRLabel;
    m715: TQRLabel;
    m714: TQRLabel;
    m713: TQRLabel;
    m712: TQRLabel;
    m711: TQRLabel;
    m6: TQRLabel;
    m7: TQRLabel;
    m856: TQRLabel;
    m855: TQRLabel;
    m854: TQRLabel;
    m853: TQRLabel;
    m852: TQRLabel;
    m851: TQRLabel;
    m846: TQRLabel;
    m845: TQRLabel;
    m844: TQRLabel;
    m843: TQRLabel;
    m842: TQRLabel;
    m841: TQRLabel;
    m836: TQRLabel;
    m835: TQRLabel;
    m834: TQRLabel;
    m833: TQRLabel;
    m832: TQRLabel;
    m831: TQRLabel;
    m826: TQRLabel;
    m825: TQRLabel;
    m824: TQRLabel;
    m823: TQRLabel;
    m822: TQRLabel;
    m821: TQRLabel;
    m816: TQRLabel;
    m815: TQRLabel;
    m814: TQRLabel;
    m813: TQRLabel;
    m812: TQRLabel;
    m811: TQRLabel;
    m8: TQRLabel;
    m956: TQRLabel;
    m955: TQRLabel;
    m954: TQRLabel;
    m953: TQRLabel;
    m952: TQRLabel;
    m951: TQRLabel;
    m946: TQRLabel;
    m945: TQRLabel;
    m944: TQRLabel;
    m943: TQRLabel;
    m942: TQRLabel;
    m941: TQRLabel;
    m936: TQRLabel;
    m935: TQRLabel;
    m934: TQRLabel;
    m933: TQRLabel;
    m932: TQRLabel;
    m931: TQRLabel;
    m925: TQRLabel;
    m924: TQRLabel;
    m923: TQRLabel;
    m922: TQRLabel;
    m921: TQRLabel;
    m916: TQRLabel;
    m915: TQRLabel;
    m914: TQRLabel;
    m913: TQRLabel;
    m912: TQRLabel;
    m911: TQRLabel;
    m9: TQRLabel;
    n156: TQRLabel;
    n155: TQRLabel;
    n154: TQRLabel;
    n153: TQRLabel;
    n152: TQRLabel;
    n151: TQRLabel;
    n146: TQRLabel;
    n145: TQRLabel;
    n144: TQRLabel;
    n143: TQRLabel;
    n142: TQRLabel;
    n141: TQRLabel;
    n136: TQRLabel;
    n135: TQRLabel;
    n134: TQRLabel;
    n133: TQRLabel;
    n132: TQRLabel;
    n131: TQRLabel;
    n126: TQRLabel;
    n125: TQRLabel;
    n124: TQRLabel;
    n123: TQRLabel;
    n122: TQRLabel;
    n121: TQRLabel;
    n116: TQRLabel;
    n115: TQRLabel;
    n114: TQRLabel;
    n113: TQRLabel;
    n112: TQRLabel;
    n111: TQRLabel;
    n1: TQRLabel;
    n256: TQRLabel;
    n255: TQRLabel;
    n254: TQRLabel;
    n253: TQRLabel;
    n252: TQRLabel;
    n251: TQRLabel;
    n246: TQRLabel;
    n245: TQRLabel;
    n244: TQRLabel;
    n243: TQRLabel;
    n242: TQRLabel;
    n241: TQRLabel;
    n236: TQRLabel;
    n235: TQRLabel;
    n234: TQRLabel;
    n233: TQRLabel;
    n232: TQRLabel;
    n231: TQRLabel;
    n226: TQRLabel;
    n225: TQRLabel;
    n224: TQRLabel;
    n223: TQRLabel;
    n222: TQRLabel;
    n221: TQRLabel;
    n216: TQRLabel;
    n215: TQRLabel;
    n214: TQRLabel;
    n213: TQRLabel;
    n212: TQRLabel;
    n211: TQRLabel;
    n2: TQRLabel;
    n356: TQRLabel;
    n355: TQRLabel;
    n354: TQRLabel;
    n353: TQRLabel;
    n352: TQRLabel;
    n351: TQRLabel;
    n346: TQRLabel;
    n345: TQRLabel;
    n344: TQRLabel;
    n343: TQRLabel;
    n342: TQRLabel;
    n341: TQRLabel;
    n336: TQRLabel;
    n335: TQRLabel;
    n334: TQRLabel;
    n333: TQRLabel;
    n332: TQRLabel;
    n331: TQRLabel;
    n326: TQRLabel;
    n325: TQRLabel;
    n324: TQRLabel;
    n323: TQRLabel;
    n322: TQRLabel;
    n321: TQRLabel;
    n316: TQRLabel;
    n315: TQRLabel;
    n314: TQRLabel;
    n313: TQRLabel;
    n312: TQRLabel;
    n311: TQRLabel;
    n3: TQRLabel;
    n456: TQRLabel;
    n454: TQRLabel;
    n453: TQRLabel;
    n452: TQRLabel;
    n451: TQRLabel;
    n446: TQRLabel;
    n445: TQRLabel;
    n444: TQRLabel;
    n443: TQRLabel;
    n442: TQRLabel;
    n441: TQRLabel;
    n436: TQRLabel;
    n435: TQRLabel;
    n434: TQRLabel;
    n433: TQRLabel;
    n432: TQRLabel;
    n431: TQRLabel;
    n426: TQRLabel;
    n425: TQRLabel;
    n424: TQRLabel;
    n423: TQRLabel;
    n422: TQRLabel;
    n421: TQRLabel;
    n416: TQRLabel;
    n415: TQRLabel;
    n414: TQRLabel;
    n413: TQRLabel;
    n412: TQRLabel;
    n411: TQRLabel;
    n4: TQRLabel;
    n556: TQRLabel;
    n555: TQRLabel;
    n554: TQRLabel;
    n553: TQRLabel;
    n552: TQRLabel;
    n551: TQRLabel;
    n546: TQRLabel;
    n545: TQRLabel;
    n544: TQRLabel;
    n543: TQRLabel;
    n542: TQRLabel;
    n541: TQRLabel;
    n536: TQRLabel;
    n535: TQRLabel;
    n534: TQRLabel;
    n533: TQRLabel;
    n532: TQRLabel;
    n531: TQRLabel;
    n526: TQRLabel;
    n525: TQRLabel;
    n524: TQRLabel;
    n523: TQRLabel;
    n522: TQRLabel;
    n521: TQRLabel;
    n516: TQRLabel;
    n515: TQRLabel;
    n514: TQRLabel;
    n513: TQRLabel;
    n512: TQRLabel;
    n511: TQRLabel;
    n5: TQRLabel;
    n656: TQRLabel;
    n655: TQRLabel;
    n654: TQRLabel;
    n653: TQRLabel;
    n652: TQRLabel;
    n651: TQRLabel;
    n646: TQRLabel;
    n645: TQRLabel;
    n644: TQRLabel;
    n643: TQRLabel;
    n642: TQRLabel;
    n641: TQRLabel;
    n636: TQRLabel;
    n635: TQRLabel;
    n634: TQRLabel;
    n633: TQRLabel;
    n632: TQRLabel;
    n631: TQRLabel;
    n626: TQRLabel;
    n625: TQRLabel;
    n624: TQRLabel;
    n623: TQRLabel;
    n622: TQRLabel;
    n621: TQRLabel;
    n616: TQRLabel;
    n615: TQRLabel;
    n614: TQRLabel;
    n613: TQRLabel;
    n612: TQRLabel;
    n611: TQRLabel;
    n6: TQRLabel;
    n756: TQRLabel;
    n755: TQRLabel;
    n754: TQRLabel;
    n753: TQRLabel;
    n752: TQRLabel;
    n751: TQRLabel;
    n746: TQRLabel;
    n745: TQRLabel;
    n744: TQRLabel;
    n743: TQRLabel;
    n742: TQRLabel;
    n741: TQRLabel;
    n736: TQRLabel;
    n735: TQRLabel;
    n734: TQRLabel;
    n733: TQRLabel;
    n732: TQRLabel;
    n731: TQRLabel;
    n726: TQRLabel;
    n725: TQRLabel;
    n724: TQRLabel;
    n723: TQRLabel;
    n722: TQRLabel;
    n721: TQRLabel;
    n716: TQRLabel;
    n715: TQRLabel;
    n714: TQRLabel;
    n713: TQRLabel;
    n712: TQRLabel;
    n711: TQRLabel;
    n7: TQRLabel;
    m656: TQRLabel;
    m655: TQRLabel;
    m654: TQRLabel;
    m653: TQRLabel;
    m652: TQRLabel;
    m651: TQRLabel;
    m646: TQRLabel;
    m645: TQRLabel;
    m644: TQRLabel;
    m643: TQRLabel;
    m642: TQRLabel;
    m641: TQRLabel;
    m636: TQRLabel;
    m635: TQRLabel;
    m634: TQRLabel;
    m633: TQRLabel;
    m632: TQRLabel;
    m631: TQRLabel;
    m626: TQRLabel;
    m625: TQRLabel;
    m624: TQRLabel;
    m623: TQRLabel;
    m622: TQRLabel;
    m621: TQRLabel;
    m616: TQRLabel;
    m615: TQRLabel;
    m611: TQRLabel;
    m612: TQRLabel;
    m613: TQRLabel;
    m614: TQRLabel;
    m926: TQRLabel;
    n455: TQRLabel;
    QRShape75: TQRShape;
    T1: TQRLabel;
    T2: TQRLabel;
    T3: TQRLabel;
    T4: TQRLabel;
    T5: TQRLabel;
    T6: TQRLabel;
    T7: TQRLabel;
    T8: TQRLabel;
    T9: TQRLabel;
    T10: TQRLabel;
    T11: TQRLabel;
    T12: TQRLabel;
    T13: TQRLabel;
    T14: TQRLabel;
    T15: TQRLabel;
    T16: TQRLabel;
    T17: TQRLabel;
    T18: TQRLabel;
    T19: TQRLabel;
    T20: TQRLabel;
    T21: TQRLabel;
    T22: TQRLabel;
    T23: TQRLabel;
    T24: TQRLabel;
    T25: TQRLabel;
    T26: TQRLabel;
    T27: TQRLabel;
    T28: TQRLabel;
    T29: TQRLabel;
    T30: TQRLabel;
    QRLMatricula: TQRLabel;
    QRLResponsavel: TQRLabel;
    qrlResultadoFinal: TQRLabel;
    qrlResultFinal: TQRLabel;
    QRShape65: TQRShape;
    qrlMediaAprovacao: TQRLabel;
    qrlNotaMediaAprovacao: TQRLabel;
    QRShape76: TQRShape;
    QRLNoDiretor: TQRLabel;
    QRLMatDiretor: TQRLabel;
    QRLabel32: TQRLabel;
    QRShape77: TQRShape;
    QRLNoSecretario: TQRLabel;
    QRLMatSecretario: TQRLabel;
    QRLabel34: TQRLabel;
    QRShape74: TQRShape;
    qrlAssPaisRespB4: TQRLabel;
    qrlAssPaisRespB3: TQRLabel;
    QRShape73: TQRShape;
    qrlAssPaisRespB2: TQRLabel;
    QRShape72: TQRShape;
    qrlAssPaisRespB1: TQRLabel;
    QRShape71: TQRShape;
    qrlAssPaisResp: TQRLabel;
    qrlObservacoes: TQRLabel;
    qrlAssProf: TQRLabel;
    QRShape67: TQRShape;
    qrlAssProfB1: TQRLabel;
    qrlAssProfB2: TQRLabel;
    QRShape68: TQRShape;
    qrlAssProfB3: TQRLabel;
    QRShape70: TQRShape;
    qrlAssProfB4: TQRLabel;
    QRShape69: TQRShape;
    QRShape64: TQRShape;
    QRShape66: TQRShape;
    QRLabel1: TQRLabel;
    QRLabel3: TQRLabel;
    QRLabel4: TQRLabel;
    QRDBText1: TQRDBText;
    QRLabel5: TQRLabel;
    QRLabel2: TQRLabel;
    QRLNuNIS: TQRLabel;
    QRLSexoAlu: TQRLabel;
    QRLTurnoDesc: TQRLabel;
    QRLDescProfessor: TQRLabel;
    QRLNoAlu: TQRLabel;
    procedure QRBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);
    procedure QuickRep1BeforePrint(Sender: TCustomQuickRep;
      var PrintReport: Boolean);
    procedure PageHeaderBand1BeforePrint(Sender: TQRCustomBand;
      var PrintBand: Boolean);

  private
    { Private declarations }
    co_alu: integer;
    //Colunas
    c1,c2,c3,c4,c5,c6,c7,c8,c9,c10,c11,c12,c13,c14,c15 : Integer;
    c16,c17,c18,c19,c20,c21,c22,c23,c24,c25,c26,c27,c28,c29,c30 : Integer;
    //Somatório das colunas
    sc1,sc2,sc3,sc4,sc5,sc6,sc7,sc8,sc9,sc10,sc11,sc12,sc13,sc14,sc15 : Double;
    sc16,sc17,sc18,sc19,sc20,sc21,sc22,sc23,sc24,sc25,sc26,sc27,sc28,sc29,sc30 : Double;
  public
    { Public declarations }
    procedure SetCo_Alu(i: integer);
  end;

var
  FrmRelBolEscAlu: TFrmRelBolEscAlu;


implementation

uses U_DataModuleSGE,MaskUtils;

{$R *.dfm}

procedure TFrmRelBolEscAlu.QRBand1BeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  q: TADOQuery;
  c: integer; //contador de registro
  ctPF: integer; //contador de percentual de faltas
begin
  inherited;
  if not QryRelatorio.FieldByName('aluno').IsNull then
    QRLNoAlu.Caption := UpperCase(QryRelatorio.FieldByName('aluno').AsString)
  else
    QRLNoAlu.Caption := '-';
    
  //Escrever Diretor
  c1 := 0;
  c2 := 0;
  c3 := 0;
  c4 := 0;
  c5 := 0;
  c6 := 0;
  c7 := 0;
  c8 := 0;
  c9 := 0;
  c10 := 0;
  c11 := 0;
  c12 := 0;
  c13 := 0;
  c14 := 0;
  c15 := 0;
  c16 := 0;
  c17 := 0;
  c18 := 0;
  c19 := 0;
  c20 := 0;
  c21 := 0;
  c22 := 0;
  c23 := 0;
  c24 := 0;
  c25 := 0;
  c26 := 0;
  c27 := 0;
  c28 := 0;
  c29 := 0;
  c30 := 0;
  sc1 := 0;
  sc2 := 0;
  sc3 := 0;
  sc4 := 0;
  sc5 := 0;
  sc6 := 0;
  sc7 := 0;
  sc8 := 0;
  sc9 := 0;
  sc10 := 0;
  sc11 := 0;
  sc12 := 0;
  sc13 := 0;
  sc14 := 0;
  sc15 := 0;
  sc16 := 0;
  sc17 := 0;
  sc18 := 0;
  sc19 := 0;
  sc20 := 0;
  sc21 := 0;
  sc22 := 0;
  sc23 := 0;
  sc24 := 0;
  sc25 := 0;
  sc26 := 0;
  sc27 := 0;
  sc28 := 0;
  sc29 := 0;
  sc30 := 0;

  QRLResponsavel.Caption := QryRelatorio.FieldByName('responsavel').AsString + ' / ' + FormatMaskText('000.000.000-00;0',qryRelatorio.FieldByName('cpfresponsavel').AsString);
  if QryRelatorio.FieldByName('sexo').AsString = 'M' then
    QRLSexoAlu.Caption := 'Masculino';
  if QryRelatorio.FieldByName('sexo').AsString = 'F' then
    QRLSexoAlu.Caption := 'Feminino';
  if not QryRelatorio.FieldByName('nis').IsNull then
    QRLNuNIS.Caption := FormatFloat('00000000000;0',QryRelatorio.FieldByName('nis').AsFloat);
  if QryRelatorio.FieldByName('turno').AsString = 'M' then
    QRLTurnoDesc.Caption := 'Matutino';
  if QryRelatorio.FieldByName('turno').AsString = 'V' then
    QRLTurnoDesc.Caption := 'Vespertino';
  if QryRelatorio.FieldByName('turno').AsString = 'N' then
    QRLTurnoDesc.Caption := 'Noturno';

  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col from tb25_empresa e ' +
                ' join tb03_colabor c on c.co_col = e.co_dir and c.co_emp = e.co_emp ' +
                ' where e.co_emp = ' + IntToStr(Sys_CodigoEmpresaAtiva);
    Open;

    if not IsEmpty then
    begin
      QRLNoDiretor.Caption := FieldByName('no_col').AsString;
      QRLMatDiretor.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
    end
    else
    begin
      QRLNoDiretor.Caption := '';
      QRLMatDiretor.Caption := '';
    end;
  end;
  //Escrever Secretário
  with DM.QrySql do
  begin
    Close;
    SQl.Clear;
    SQL.Text := 'select c.no_col,c.co_mat_col from tb25_empresa e ' +
                ' join tb03_colabor c on c.co_col = e.co_sec and c.co_emp = e.co_emp ' +
                ' where e.co_emp = ' + IntToStr(Sys_CodigoEmpresaAtiva);
    Open;

    if not IsEmpty then
    begin
      QRLNoSecretario.Caption := FieldByName('no_col').AsString;
      QRLMatSecretario.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString)
    end
    else
    begin
      QRLNoSecretario.Caption := '';
      QRLMatSecretario.Caption := '';
    end;
  end;

  if not QryRelatorio.FieldByName('matricula').IsNull then begin
    QRLMatricula.Caption := FormatMaskText('00.000.000000;0',QryRelatorio.FieldByName('matricula').AsString);
  end
  else
    QRLMatricula.Caption := '';

  if not QryRelatorio.FieldByName('aprovado').IsNull then begin
    qrlResultFinal.Font.Size := 12;
    qrlResultFinal.Top := 166;
    if QryRelatorio.FieldByName('aprovado').AsString = 'A' then
      qrlResultFinal.Caption := '*** APROVADO ***'
    else
      qrlResultFinal.Caption := '*** REPROVADO ***';
  end else
  begin
    qrlResultFinal.Font.Size := 26;
    qrlResultFinal.Top := 160;
    qrlResultFinal.Caption := '**********';
  end;

  q := TADOQuery.Create(nil);
  q.Connection := DM.Conn;

  //Base Nacional
  q.Close;
  q.SQL.Clear;
  with q.SQL do begin
    Add('select ');
    Add('histalu.co_mat [comateria], ');
    Add('cadmat.no_red_materia [materia], ');
    Add('histalu.qt_falta_bim1 [faltas1], histalu.qt_falta_bim2 [faltas2], ');
    Add('histalu.qt_falta_bim3 [faltas3], histalu.qt_falta_bim4 [faltas4], ');
    Add('histalu.vl_recu_bim1 [rec1], histalu.vl_recu_bim2 [rec2], ');
    Add('histalu.vl_recu_bim3 [rec3], histalu.vl_recu_bim4 [rec4], ');
    Add('histalu.vl_nota_bim1 [nota1], histalu.vl_nota_bim2 [nota2], ');
    Add('histalu.vl_nota_bim3 [nota3], histalu.vl_nota_bim4 [nota4], ');
    Add('histalu.VL_CONC_BIM1 [conceito1], histalu.VL_CONC_BIM2 [conceito2], ');
    Add('histalu.VL_CONC_BIM3 [conceito3], histalu.VL_CONC_BIM4 [conceito4], ');
    Add('histalu.vl_nota_bim1 [media1], histalu.vl_nota_bim2 [media2], ');
    Add('histalu.vl_nota_bim3 [media3], histalu.vl_nota_bim4 [media4], histalu.vl_prova_final [provafinal], ');
    Add('histalu.vl_media_final [mediafinal] ,null  [comp1], null  [comp2], null  [comp3], null  [comp4] ');

    Add('from ');
    Add('tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat ');

    Add('where ');
    Add('(histalu.co_mat = mat.co_mat) ');
    Add('and (mat.id_materia = cadmat.id_materia) ');
    Add('and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ');
    Add('and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ');
    Add('and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ');
    Add('and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ');
    Add('and (cadmat.co_class_boletim = '+'1)');
  end;
  q.Open;
  q.First;
  c := 1;
  while not q.Eof do begin
    case c of
      //Materia m1
      1: begin
        m1.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m111.Caption := q.FieldByName('nota1').AsString;
        m112.Caption := q.FieldByName('rec1').AsString;
        m113.Caption := q.FieldByName('media1').AsString;
        m116.Caption := q.FieldByName('conceito1').AsString;
        if m111.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m111.Caption);
        end;
        if m113.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m113.Caption);
        end;
        m114.Caption := q.FieldByName('faltas1').AsString;
        if m114.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m114.Caption);
        end;
        if m116.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m116.Caption);
        end;
      //  m115.Caption := q.FieldByName('freq1').AsString;
        //m116.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m121.Caption := q.FieldByName('nota2').AsString;
        m122.Caption := q.FieldByName('rec2').AsString;
        m123.Caption := q.FieldByName('media2').AsString;
        m126.Caption := q.FieldByName('conceito2').AsString;
        if m121.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m121.Caption);
        end;
        if m123.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m123.Caption);
        end;
        m124.Caption := q.FieldByName('faltas2').AsString;
        if m124.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m124.Caption);
        end;
        if m126.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m126.Caption);
        end;
      //  m125.Caption := q.FieldByName('freq2').AsString;
//        m126.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m131.Caption := q.FieldByName('nota3').AsString;
        m132.Caption := q.FieldByName('rec3').AsString;
        m133.Caption := q.FieldByName('media3').AsString;
        m136.Caption := q.FieldByName('conceito3').AsString;
        if m131.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m131.Caption);
        end;
        if m133.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m133.Caption);
        end;
        m134.Caption := q.FieldByName('faltas3').AsString;
        if m134.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m134.Caption);
        end;
        if m136.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m136.Caption);
        end;
     //   m135.Caption := q.FieldByName('freq3').AsString;
//        m136.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m141.Caption := q.FieldByName('nota4').AsString;
        m142.Caption := q.FieldByName('rec4').AsString;
        m143.Caption := q.FieldByName('media4').AsString;
        m146.Caption := q.FieldByName('conceito4').AsString;
        if m141.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m141.Caption);
        end;
        if m143.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m143.Caption);
        end;
        m144.Caption := q.FieldByName('faltas4').AsString;
        if m144.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m144.Caption);
        end;
        if m146.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m146.Caption);
        end;
       // m145.Caption := q.FieldByName('freq4').AsString;
//        m146.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5 - Sintes
        if not q.FieldByName('nota4').IsNull then
        begin
          m151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m151.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m151.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m151.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m151.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m151.Caption);
        end
        else
          m151.Caption := '';
        //m151.Caption := q.FieldByName('sintbimest').AsString;

        //m152.Caption := q.FieldByName('mediaturmabim').AsString;
        m153.Caption := q.FieldByName('provafinal').AsString;
		
		
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m154.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m154.Caption);
        end;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m155.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m155.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m155.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m155.Caption);
        end
        else
        begin
          m155.Caption := '';
          m156.Caption := '';
        end
        //m155.Caption := q.FieldByName('faltas').AsString;

        //m156.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m2
      2: begin
        m2.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m211.Caption := q.FieldByName('nota1').AsString;
        m212.Caption := q.FieldByName('rec1').AsString;
        m213.Caption := q.FieldByName('media1').AsString;
        m216.Caption := q.FieldByName('conceito1').AsString;
        if m211.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m211.Caption);
        end;
        if m213.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m213.Caption);
        end;
        m214.Caption := q.FieldByName('faltas1').AsString;
        if m214.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m214.Caption);
        end;
        if m216.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m216.Caption);
        end;
       // m215.Caption := q.FieldByName('freq1').AsString;
//        m216.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m221.Caption := q.FieldByName('nota2').AsString;
        m222.Caption := q.FieldByName('rec2').AsString;
        m223.Caption := q.FieldByName('media2').AsString;
        m226.Caption := q.FieldByName('conceito2').AsString;
        if m221.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m221.Caption);
        end;
        if m223.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m223.Caption);
        end;
        m224.Caption := q.FieldByName('faltas2').AsString;
        if m224.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m224.Caption);
        end;
        if m226.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m226.Caption);
        end;
      //  m225.Caption := q.FieldByName('freq2').AsString;
//        m226.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m231.Caption := q.FieldByName('nota3').AsString;
        m232.Caption := q.FieldByName('rec3').AsString;
        m233.Caption := q.FieldByName('media3').AsString;
        m236.Caption := q.FieldByName('conceito3').AsString;
        if m231.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m231.Caption);
        end;
        if m233.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m233.Caption);
        end;
        m234.Caption := q.FieldByName('faltas3').AsString;
        if m234.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m234.Caption);
        end;
        if m236.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m236.Caption);
        end;
    //    m235.Caption := q.FieldByName('freq3').AsString;
//        m236.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m241.Caption := q.FieldByName('nota4').AsString;
        m242.Caption := q.FieldByName('rec4').AsString;
        m243.Caption := q.FieldByName('media4').AsString;
        m246.Caption := q.FieldByName('conceito4').AsString;
        if m241.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m241.Caption);
        end;
        if m243.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m243.Caption);
        end;
        m244.Caption := q.FieldByName('faltas4').AsString;
        if m244.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m244.Caption);
        end;
        if m246.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m246.Caption);
        end;
   //     m245.Caption := q.FieldByName('freq4').AsString;
//        m246.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m251.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m251.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m251.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m251.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m251.Caption);
        end
        else
          m251.Caption := '';
        //m251.Caption := q.FieldByName('sintbimest').AsString;

        //m252.Caption := q.FieldByName('mediaturmabim').AsString;
        m253.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m254.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m254.Caption);
        end;
        //m254.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m255.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m255.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m255.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m255.Caption);
        end
        else
        begin
          m255.Caption := '';
          m256.Caption := '';
        end;
        //m255.Caption := q.FieldByName('faltas').AsString;

        //m256.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m3
      3: begin
        m3.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m311.Caption := q.FieldByName('nota1').AsString;
        m312.Caption := q.FieldByName('rec1').AsString;
        m313.Caption := q.FieldByName('media1').AsString;
        m316.Caption := q.FieldByName('conceito1').AsString;
        if m311.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m311.Caption);
        end;
        if m313.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m313.Caption);
        end;
        m314.Caption := q.FieldByName('faltas1').AsString;
        if m314.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m314.Caption);
        end;
        if m316.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m316.Caption);
        end;
     //   m315.Caption := q.FieldByName('freq1').AsString;
//        m316.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m321.Caption := q.FieldByName('nota2').AsString;
        m322.Caption := q.FieldByName('rec2').AsString;
        m323.Caption := q.FieldByName('media2').AsString;
        m326.Caption := q.FieldByName('conceito2').AsString;
        if m321.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m321.Caption);
        end;
        if m323.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m323.Caption);
        end;
        m324.Caption := q.FieldByName('faltas2').AsString;
        if m324.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m324.Caption);
        end;
        if m326.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m326.Caption);
        end;
     //   m325.Caption := q.FieldByName('freq2').AsString;
//        m326.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m331.Caption := q.FieldByName('nota3').AsString;
        m332.Caption := q.FieldByName('rec3').AsString;
        m333.Caption := q.FieldByName('media3').AsString;
        m336.Caption := q.FieldByName('conceito3').AsString;
        if m331.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m331.Caption);
        end;
        if m333.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m333.Caption);
        end;
        m334.Caption := q.FieldByName('faltas3').AsString;
        if m334.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m334.Caption);
        end;
        if m336.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m336.Caption);
        end;
      //  m335.Caption := q.FieldByName('freq3').AsString;
//        m336.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m341.Caption := q.FieldByName('nota4').AsString;
        m342.Caption := q.FieldByName('rec4').AsString;
        m343.Caption := q.FieldByName('media4').AsString;
        m346.Caption := q.FieldByName('conceito4').AsString;
        if m341.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m341.Caption);
        end;
        if m343.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m343.Caption);
        end;
        m344.Caption := q.FieldByName('faltas4').AsString;
        if m344.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m344.Caption);
        end;
        if m346.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m346.Caption);
        end;
     //   m345.Caption := q.FieldByName('freq4').AsString;
//        m346.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m351.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m351.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m351.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m351.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m351.Caption);
        end
        else
          m351.Caption := '';
        //m351.Caption := q.FieldByName('sintbimest').AsString;

        //m352.Caption := q.FieldByName('mediaturmabim').AsString;
        m353.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m354.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m354.Caption);
        end;
        //m354.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m355.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m355.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m355.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m355.Caption);
        end
        else
          m355.Caption := '';
        //m355.Caption := q.FieldByName('faltas').AsString;

        //m356.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m4
      4: begin
        m4.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m411.Caption := q.FieldByName('nota1').AsString;
        m412.Caption := q.FieldByName('rec1').AsString;
        m413.Caption := q.FieldByName('media1').AsString;
        m416.Caption := q.FieldByName('conceito1').AsString;
        if m411.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m411.Caption);
        end;
        if m413.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m413.Caption);
        end;
        m414.Caption := q.FieldByName('faltas1').AsString;
        if m414.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m414.Caption);
        end;
        if m416.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m416.Caption);
        end;
      //  m415.Caption := q.FieldByName('freq1').AsString;
//        m416.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m421.Caption := q.FieldByName('nota2').AsString;
        m422.Caption := q.FieldByName('rec2').AsString;
        m423.Caption := q.FieldByName('media2').AsString;
        m426.Caption := q.FieldByName('conceito2').AsString;
        if m421.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m421.Caption);
        end;
        if m423.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m423.Caption);
        end;
        m424.Caption := q.FieldByName('faltas2').AsString;
        if m424.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m424.Caption);
        end;
        if m426.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m426.Caption);
        end;
      //  m425.Caption := q.FieldByName('freq2').AsString;
//        m426.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m431.Caption := q.FieldByName('nota3').AsString;
        m432.Caption := q.FieldByName('rec3').AsString;
        m433.Caption := q.FieldByName('media3').AsString;
        m436.Caption := q.FieldByName('conceito3').AsString;
        if m431.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m431.Caption);
        end;
        if m433.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m433.Caption);
        end;
        m434.Caption := q.FieldByName('faltas3').AsString;
        if m434.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m434.Caption);
        end;
        if m436.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m436.Caption);
        end;
      //  m435.Caption := q.FieldByName('freq3').AsString;
//        m436.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m441.Caption := q.FieldByName('nota4').AsString;
        m442.Caption := q.FieldByName('rec4').AsString;
        m443.Caption := q.FieldByName('media4').AsString;
        m446.Caption := q.FieldByName('conceito4').AsString;
        if m441.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m441.Caption);
        end;
        if m443.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m443.Caption);
        end;
        m444.Caption := q.FieldByName('faltas4').AsString;
        if m444.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m444.Caption);
        end;
        if m446.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m446.Caption);
        end;
      //  m445.Caption := q.FieldByName('freq4').AsString;
//        m446.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m451.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m451.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m451.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m451.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m451.Caption);
        end
        else
          m451.Caption := '';
        //m451.Caption := q.FieldByName('sintbimest').AsString;

        //m452.Caption := q.FieldByName('mediaturmabim').AsString;
        m453.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m454.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m454.Caption);
        end;
        //m454.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m455.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m455.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m455.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m455.Caption);
        end
        else
          m455.Caption := '';
        //m455.Caption := q.FieldByName('faltas').AsString;

        //m456.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m5
      5: begin
        m5.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m511.Caption := q.FieldByName('nota1').AsString;
        m512.Caption := q.FieldByName('rec1').AsString;
        m513.Caption := q.FieldByName('media1').AsString;
        m516.Caption := q.FieldByName('conceito1').AsString;
        if m511.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m511.Caption);
        end;
        if m513.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m513.Caption);
        end;
        m514.Caption := q.FieldByName('faltas1').AsString;
        if m514.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m514.Caption);
        end;
        if m516.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m516.Caption);
        end;
      //  m515.Caption := q.FieldByName('freq1').AsString;
//        m516.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m521.Caption := q.FieldByName('nota2').AsString;
        m522.Caption := q.FieldByName('rec2').AsString;
        m523.Caption := q.FieldByName('media2').AsString;
        m526.Caption := q.FieldByName('conceito2').AsString;
        if m521.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m521.Caption);
        end;
        if m523.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m523.Caption);
        end;
        m524.Caption := q.FieldByName('faltas2').AsString;
        if m524.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m524.Caption);
        end;
        if m526.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m526.Caption);
        end;
      //  m525.Caption := q.FieldByName('freq2').AsString;
//        m526.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m531.Caption := q.FieldByName('nota3').AsString;
        m532.Caption := q.FieldByName('rec3').AsString;
        m533.Caption := q.FieldByName('media3').AsString;
        m536.Caption := q.FieldByName('conceito3').AsString;
        if m531.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m531.Caption);
        end;
        if m533.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m533.Caption);
        end;
        m534.Caption := q.FieldByName('faltas3').AsString;
        if m534.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m534.Caption);
        end;
        if m536.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m536.Caption);
        end;
      //  m535.Caption := q.FieldByName('freq3').AsString;
//        m536.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m541.Caption := q.FieldByName('nota4').AsString;
        m542.Caption := q.FieldByName('rec4').AsString;
        m543.Caption := q.FieldByName('media4').AsString;
        m546.Caption := q.FieldByName('conceito4').AsString;
        if m541.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m541.Caption);
        end;
        if m543.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m543.Caption);
        end;
        m544.Caption := q.FieldByName('faltas4').AsString;
        if m544.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m544.Caption);
        end;
        if m546.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m546.Caption);
        end;
      //  m545.Caption := q.FieldByName('freq4').AsString;
//        m546.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m551.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m551.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m551.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m551.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m551.Caption);
        end
        else
          m551.Caption := '';
        //m551.Caption := q.FieldByName('sintbimest').AsString;

        //m552.Caption := q.FieldByName('mediaturmabim').AsString;
        m553.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m554.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m554.Caption);
        end;
        //m554.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m555.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m555.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m555.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m555.Caption);
        end
        else
          m555.Caption := '';
        //m555.Caption := q.FieldByName('faltas').AsString;

        //m556.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m6
      6: begin
        m6.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m611.Caption := q.FieldByName('nota1').AsString;
        m612.Caption := q.FieldByName('rec1').AsString;
        m613.Caption := q.FieldByName('media1').AsString;
        m616.Caption := q.FieldByName('conceito1').AsString;
        if m611.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m611.Caption);
        end;
        if m613.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m613.Caption);
        end;
        m614.Caption := q.FieldByName('faltas1').AsString;
        if m614.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m614.Caption);
        end;
        if m616.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m616.Caption);
        end;
      //  m615.Caption := q.FieldByName('freq1').AsString;
//        m616.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m621.Caption := q.FieldByName('nota2').AsString;
        m622.Caption := q.FieldByName('rec2').AsString;
        m623.Caption := q.FieldByName('media2').AsString;
        m626.Caption := q.FieldByName('conceito2').AsString;
        if m621.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m621.Caption);
        end;
        if m623.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m623.Caption);
        end;
        m624.Caption := q.FieldByName('faltas2').AsString;
        if m624.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m624.Caption);
        end;
        if m626.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m626.Caption);
        end;
     //   m625.Caption := q.FieldByName('freq2').AsString;
//        m626.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m631.Caption := q.FieldByName('nota3').AsString;
        m632.Caption := q.FieldByName('rec3').AsString;
        m633.Caption := q.FieldByName('media3').AsString;
        m636.Caption := q.FieldByName('conceito3').AsString;
        if m631.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m631.Caption);
        end;
        if m633.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m633.Caption);
        end;
        m634.Caption := q.FieldByName('faltas3').AsString;
        if m634.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m634.Caption);
        end;
        if m636.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m636.Caption);
        end;
     //   m635.Caption := q.FieldByName('freq3').AsString;
//        m636.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m641.Caption := q.FieldByName('nota4').AsString;
        m642.Caption := q.FieldByName('rec4').AsString;
        m643.Caption := q.FieldByName('media4').AsString;
        m646.Caption := q.FieldByName('conceito4').AsString;
        if m641.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m641.Caption);
        end;
        if m643.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m643.Caption);
        end;
        m644.Caption := q.FieldByName('faltas4').AsString;
        if m644.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m644.Caption);
        end;
        if m646.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m646.Caption);
        end;
     //   m645.Caption := q.FieldByName('freq4').AsString;
//        m646.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m651.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m651.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m651.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m651.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m651.Caption);
        end
        else
          m651.Caption := '';
        //m651.Caption := q.FieldByName('sintbimest').AsString;

        //m652.Caption := q.FieldByName('mediaturmabim').AsString;
        m653.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m654.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m654.Caption);
        end;
        //m654.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m655.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m655.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m655.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m655.Caption);
        end
        else
          m655.Caption := '';
        //m655.Caption := q.FieldByName('faltas').AsString;

        //m656.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m7
      7: begin
        m7.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m711.Caption := q.FieldByName('nota1').AsString;
        m712.Caption := q.FieldByName('rec1').AsString;
        m713.Caption := q.FieldByName('media1').AsString;
        m716.Caption := q.FieldByName('conceito1').AsString;
        if m711.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m711.Caption);
        end;
        if m713.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m713.Caption);
        end;
        m714.Caption := q.FieldByName('faltas1').AsString;
        if m714.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m714.Caption);
        end;
        if m716.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m716.Caption);
        end;
      //  m715.Caption := q.FieldByName('freq1').AsString;
//        m716.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m721.Caption := q.FieldByName('nota2').AsString;
        m722.Caption := q.FieldByName('rec2').AsString;
        m723.Caption := q.FieldByName('media2').AsString;
        m726.Caption := q.FieldByName('conceito2').AsString;
        if m721.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m721.Caption);
        end;
        if m723.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m723.Caption);
        end;
        m724.Caption := q.FieldByName('faltas2').AsString;
        if m724.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m724.Caption);
        end;
        if m726.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m726.Caption);
        end;
      //  m725.Caption := q.FieldByName('freq2').AsString;
//        m726.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m731.Caption := q.FieldByName('nota3').AsString;
        m732.Caption := q.FieldByName('rec3').AsString;
        m733.Caption := q.FieldByName('media3').AsString;
        m736.Caption := q.FieldByName('conceito3').AsString;
        if m731.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m731.Caption);
        end;
        if m733.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m733.Caption);
        end;
        m734.Caption := q.FieldByName('faltas3').AsString;
        if m734.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m734.Caption);
        end;
        if m736.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m736.Caption);
        end;
      //  m735.Caption := q.FieldByName('freq3').AsString;
//        m736.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m741.Caption := q.FieldByName('nota4').AsString;
        m742.Caption := q.FieldByName('rec4').AsString;
        m743.Caption := q.FieldByName('media4').AsString;
        m746.Caption := q.FieldByName('conceito4').AsString;
        if m741.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m741.Caption);
        end;
        if m743.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m743.Caption);
        end;
        m744.Caption := q.FieldByName('faltas4').AsString;
        if m744.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m744.Caption);
        end;
        if m746.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m746.Caption);
        end;
     //   m745.Caption := q.FieldByName('freq4').AsString;
//        m746.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m751.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m751.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m751.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m751.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m751.Caption);
        end
        else
          m751.Caption := '';
        //m751.Caption := q.FieldByName('sintbimest').AsString;

        //m752.Caption := q.FieldByName('mediaturmabim').AsString;
        m753.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m754.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m754.Caption);
        end;
        //m754.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m755.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m755.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m755.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m755.Caption);
        end
        else
          m755.Caption := '';
        //m755.Caption := q.FieldByName('faltas').AsString;

        //m756.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m8
      8: begin
        m8.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m811.Caption := q.FieldByName('nota1').AsString;
        m812.Caption := q.FieldByName('rec1').AsString;
        m813.Caption := q.FieldByName('media1').AsString;
        m816.Caption := q.FieldByName('conceito1').AsString;
        if m811.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m811.Caption);
        end;
        if m813.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m813.Caption);
        end;
        m814.Caption := q.FieldByName('faltas1').AsString;
        if m814.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m814.Caption);
        end;
        if m816.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m816.Caption);
        end;
      //  m815.Caption := q.FieldByName('freq1').AsString;
//        m816.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m821.Caption := q.FieldByName('nota2').AsString;
        m822.Caption := q.FieldByName('rec2').AsString;
        m823.Caption := q.FieldByName('media2').AsString;
        m826.Caption := q.FieldByName('conceito2').AsString;
        if m821.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m821.Caption);
        end;
        if m823.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m823.Caption);
        end;
        m824.Caption := q.FieldByName('faltas2').AsString;
        if m824.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m824.Caption);
        end;
        if m826.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m826.Caption);
        end;
      //  m825.Caption := q.FieldByName('freq2').AsString;
//        m826.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m831.Caption := q.FieldByName('nota3').AsString;
        m832.Caption := q.FieldByName('rec3').AsString;
        m833.Caption := q.FieldByName('media3').AsString;
        m836.Caption := q.FieldByName('conceito3').AsString;
        if m831.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m831.Caption);
        end;
        if m833.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m833.Caption);
        end;
        m834.Caption := q.FieldByName('faltas3').AsString;
        if m834.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m834.Caption);
        end;
        if m836.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m836.Caption);
        end;
      //  m835.Caption := q.FieldByName('freq3').AsString;
//        m836.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m841.Caption := q.FieldByName('nota4').AsString;
        m842.Caption := q.FieldByName('rec4').AsString;
        m843.Caption := q.FieldByName('media4').AsString;
        m846.Caption := q.FieldByName('conceito4').AsString;
        if m841.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m841.Caption);
        end;
        if m843.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m843.Caption);
        end;
        m844.Caption := q.FieldByName('faltas4').AsString;
        if m844.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m844.Caption);
        end;
        if m846.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m846.Caption);
        end;
      //  m845.Caption := q.FieldByName('freq4').AsString;
//        m846.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m851.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m851.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m851.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m851.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m851.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m851.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m851.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m851.Caption);
        end
        else
          m851.Caption := '';
        //m851.Caption := q.FieldByName('sintbimest').AsString;

        //m852.Caption := q.FieldByName('mediaturmabim').AsString;
        m853.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m854.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m854.Caption);
        end;
        //m854.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m855.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m855.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m855.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m855.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m855.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m855.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m855.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m855.Caption);
        end
        else
          m855.Caption := '';
        //m855.Caption := q.FieldByName('faltas').AsString;

        //m856.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia m9
      9: begin
        m9.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        m911.Caption := q.FieldByName('nota1').AsString;
        m912.Caption := q.FieldByName('rec1').AsString;
        m913.Caption := q.FieldByName('media1').AsString;
        m916.Caption := q.FieldByName('conceito1').AsString;
        if m911.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(m911.Caption);
        end;
        if m913.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(m913.Caption);
        end;
        m914.Caption := q.FieldByName('faltas1').AsString;
        if m914.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(m914.Caption);
        end;
        if m916.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(m916.Caption);
        end;
     //   m915.Caption := q.FieldByName('freq1').AsString;
//        m916.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        m921.Caption := q.FieldByName('nota2').AsString;
        m922.Caption := q.FieldByName('rec2').AsString;
        m923.Caption := q.FieldByName('media2').AsString;
        m926.Caption := q.FieldByName('conceito2').AsString;
        if m921.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(m921.Caption);
        end;
        if m923.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(m923.Caption);
        end;
        m924.Caption := q.FieldByName('faltas2').AsString;
        if m924.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(m924.Caption);
        end;
        if m926.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(m926.Caption);
        end;
      //  m925.Caption := q.FieldByName('freq2').AsString;
//        m926.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        m931.Caption := q.FieldByName('nota3').AsString;
        m932.Caption := q.FieldByName('rec3').AsString;
        m933.Caption := q.FieldByName('media3').AsString;
        m936.Caption := q.FieldByName('conceito3').AsString;
        if m931.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(m931.Caption);
        end;
        if m933.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(m933.Caption);
        end;
        m934.Caption := q.FieldByName('faltas3').AsString;
        if m934.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(m934.Caption);
        end;
        if m936.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(m936.Caption);
        end;
      //  m935.Caption := q.FieldByName('freq3').AsString;
//        m936.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        m941.Caption := q.FieldByName('nota4').AsString;
        m942.Caption := q.FieldByName('rec4').AsString;
        m943.Caption := q.FieldByName('media4').AsString;
        m946.Caption := q.FieldByName('conceito4').AsString;
        if m941.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(m941.Caption);
        end;
        if m943.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(m113.Caption);
        end;
        m944.Caption := q.FieldByName('faltas4').AsString;
        if m944.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(m944.Caption);
        end;
        if m946.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(m946.Caption);
        end;
      //  m945.Caption := q.FieldByName('freq4').AsString;
//        m946.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          m951.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m951.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          m951.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m951.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          m951.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m951.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          m951.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(m951.Caption);
        end
        else
          m951.Caption := '';
        //m951.Caption := q.FieldByName('sintbimest').AsString;

        //m952.Caption := q.FieldByName('mediaturmabim').AsString;
        m953.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          m954.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(m954.Caption);
        end;
        //m954.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          m955.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(m955.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          m955.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(m955.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          m955.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(m955.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          m955.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(m955.Caption);
        end
        else
          m955.Caption := '';
        //m955.Caption := q.FieldByName('faltas').AsString;

        //m956.Caption := q.FieldByName('frequencia').AsString;
      end;
    end;
    inc(c);
    q.Next;
  end;

  //Parte Diversificada
  q.Close;
  q.SQL.Clear;
  with q.SQL do begin
    Add('select ');
    Add('cadmat.no_red_materia [materia], ');
    Add('histalu.qt_falta_bim1 [faltas1], histalu.qt_falta_bim2 [faltas2], ');
    Add('histalu.qt_falta_bim3 [faltas3], histalu.qt_falta_bim4 [faltas4], ');
    Add('histalu.vl_recu_bim1 [rec1], histalu.vl_recu_bim2 [rec2], ');
    Add('histalu.vl_recu_bim3 [rec3], histalu.vl_recu_bim4 [rec4], ');
    Add('histalu.vl_nota_bim1 [nota1], histalu.vl_nota_bim2 [nota2], ');
    Add('histalu.vl_nota_bim3 [nota3], histalu.vl_nota_bim4 [nota4], ');
    Add('histalu.VL_CONC_BIM1 [conceito1], histalu.VL_CONC_BIM2 [conceito2], ');
    Add('histalu.VL_CONC_BIM3 [conceito3], histalu.VL_CONC_BIM4 [conceito4], ');
    Add('histalu.vl_nota_bim1 [media1], histalu.vl_nota_bim2 [media2], ');
    Add('histalu.vl_nota_bim3 [media3], histalu.vl_nota_bim4 [media4], histalu.vl_prova_final [provafinal], ');
    Add('histalu.vl_media_final [mediafinal] ,null  [comp1], null  [comp2], null  [comp3], null  [comp4] ');

    Add('from ');
    Add('tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat ');

    Add('where ');
    Add('(histalu.co_mat = mat.co_mat) ');
    Add('and (mat.id_materia = cadmat.id_materia) ');
    Add('and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ');
    Add('and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ');
    Add('and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ');
    Add('and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ');
    Add('and (cadmat.co_class_boletim = '+'2)');
  end;
  q.Open;
  q.First;
  c := 1;
  while not q.Eof do begin
    case c of
      //Materia n1
      1: begin
        n1.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n111.Caption := q.FieldByName('nota1').AsString;
        n112.Caption := q.FieldByName('rec1').AsString;
        n113.Caption := q.FieldByName('media1').AsString;
        n116.Caption := q.FieldByName('conceito1').AsString;
        if n111.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n111.Caption);
        end;
        if n113.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n113.Caption);
        end;
        n114.Caption := q.FieldByName('faltas1').AsString;
        if n114.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n114.Caption);
        end;
        if n116.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n116.Caption);
        end;
      //  n115.Caption := q.FieldByName('freq1').AsString;
//        n116.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n121.Caption := q.FieldByName('nota2').AsString;
        n122.Caption := q.FieldByName('rec2').AsString;
        n123.Caption := q.FieldByName('media2').AsString;
        n126.Caption := q.FieldByName('conceito2').AsString;
        if n121.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n121.Caption);
        end;
        if n123.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n123.Caption);
        end;
        n124.Caption := q.FieldByName('faltas2').AsString;
        if n124.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n124.Caption);
        end;
        if n126.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n126.Caption);
        end;
      //  n125.Caption := q.FieldByName('freq2').AsString;
//        n126.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n131.Caption := q.FieldByName('nota3').AsString;
        n132.Caption := q.FieldByName('rec3').AsString;
        n133.Caption := q.FieldByName('media3').AsString;
        n136.Caption := q.FieldByName('conceito3').AsString;
        if n131.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n131.Caption);
        end;
        if n133.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n133.Caption);
        end;
        n134.Caption := q.FieldByName('faltas3').AsString;
        if n134.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n134.Caption);
        end;
        if n136.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n136.Caption);
        end;
      //  n135.Caption := q.FieldByName('freq3').AsString;
//        n136.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n141.Caption := q.FieldByName('nota4').AsString;
        n142.Caption := q.FieldByName('rec4').AsString;
        n143.Caption := q.FieldByName('media4').AsString;
        n146.Caption := q.FieldByName('conceito4').AsString;
        if n141.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n141.Caption);
        end;
        if n143.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n143.Caption);
        end;
        n144.Caption := q.FieldByName('faltas4').AsString;
        if n144.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n144.Caption);
        end;
        if n146.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n146.Caption);
        end;
      //  n145.Caption := q.FieldByName('freq4').AsString;
//        n146.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n151.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n151.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n151.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n151.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n151.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n151.Caption);
        end
        else
          n151.Caption := '';
        //n151.Caption := q.FieldByName('sintbimest').AsString;

        //n152.Caption := q.FieldByName('mediaturmabim').AsString;
        n153.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n154.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n154.Caption);
        end;
        //n154.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n155.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n155.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n155.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n155.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n155.Caption);
        end
        else
          n155.Caption := '';
        //n155.Caption := q.FieldByName('faltas').AsString;

        //n156.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia n2
      2: begin
        n2.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n211.Caption := q.FieldByName('nota1').AsString;
        n212.Caption := q.FieldByName('rec1').AsString;
        n213.Caption := q.FieldByName('media1').AsString;
        n216.Caption := q.FieldByName('conceito1').AsString;
        if n211.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n211.Caption);
        end;
        if n213.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n213.Caption);
        end;
        n214.Caption := q.FieldByName('faltas1').AsString;
        if n214.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n214.Caption);
        end;
        if n216.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n216.Caption);
        end;
     //   n215.Caption := q.FieldByName('freq1').AsString;
//        n216.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n221.Caption := q.FieldByName('nota2').AsString;
        n222.Caption := q.FieldByName('rec2').AsString;
        n223.Caption := q.FieldByName('media2').AsString;
        n226.Caption := q.FieldByName('conceito2').AsString;
        if n221.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n221.Caption);
        end;
        if n223.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n223.Caption);
        end;
        n224.Caption := q.FieldByName('faltas2').AsString;
        if n224.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n224.Caption);
        end;
        if n226.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n226.Caption);
        end;
     //   n225.Caption := q.FieldByName('freq2').AsString;
//        n226.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n231.Caption := q.FieldByName('nota3').AsString;
        n232.Caption := q.FieldByName('rec3').AsString;
        n233.Caption := q.FieldByName('media3').AsString;
        n236.Caption := q.FieldByName('conceito3').AsString;
        if n231.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n231.Caption);
        end;
        if n233.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n233.Caption);
        end;
        n234.Caption := q.FieldByName('faltas3').AsString;
        if n234.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n234.Caption);
        end;
        if n236.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n236.Caption);
        end;
     //   n235.Caption := q.FieldByName('freq3').AsString;
//        n236.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n241.Caption := q.FieldByName('nota4').AsString;
        n242.Caption := q.FieldByName('rec4').AsString;
        n243.Caption := q.FieldByName('media4').AsString;
        n246.Caption := q.FieldByName('conceito4').AsString;
        if n241.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n241.Caption);
        end;
        if n243.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n243.Caption);
        end;
        n244.Caption := q.FieldByName('faltas4').AsString;
        if n244.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n244.Caption);
        end;
        if n246.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n246.Caption);
        end;
     //   n245.Caption := q.FieldByName('freq4').AsString;
//        n246.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n251.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n251.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n251.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n251.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n251.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n251.Caption);
        end
        else
          n251.Caption := '';
        //n251.Caption := q.FieldByName('sintbimest').AsString;

        //n252.Caption := q.FieldByName('mediaturmabim').AsString;
        n253.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n254.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n254.Caption);
        end;
        //n254.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n255.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n255.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n255.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n255.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n255.Caption);
        end
        else
          n255.Caption := '';
        //n255.Caption := q.FieldByName('faltas').AsString;

        //n256.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia n3
      3: begin
        n3.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n311.Caption := q.FieldByName('nota1').AsString;
        n312.Caption := q.FieldByName('rec1').AsString;
        n313.Caption := q.FieldByName('media1').AsString;
        n316.Caption := q.FieldByName('conceito1').AsString;
        if n311.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n311.Caption);
        end;
        if n313.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n313.Caption);
        end;
        n314.Caption := q.FieldByName('faltas1').AsString;
        if n314.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n314.Caption);
        end;
        if n316.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n316.Caption);
        end;
      //  n315.Caption := q.FieldByName('freq1').AsString;
//        n316.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n321.Caption := q.FieldByName('nota2').AsString;
        n322.Caption := q.FieldByName('rec2').AsString;
        n323.Caption := q.FieldByName('media2').AsString;
        n326.Caption := q.FieldByName('conceito2').AsString;
        if n321.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n321.Caption);
        end;
        if n323.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n323.Caption);
        end;
        n324.Caption := q.FieldByName('faltas2').AsString;
        if n324.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n324.Caption);
        end;
        if n326.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n326.Caption);
        end;
      //  n325.Caption := q.FieldByName('freq2').AsString;
//        n326.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n331.Caption := q.FieldByName('nota3').AsString;
        n332.Caption := q.FieldByName('rec3').AsString;
        n333.Caption := q.FieldByName('media3').AsString;
        n336.Caption := q.FieldByName('conceito3').AsString;
        if n331.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n331.Caption);
        end;
        if n333.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n333.Caption);
        end;
        n334.Caption := q.FieldByName('faltas3').AsString;
        if n334.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n334.Caption);
        end;
        if n336.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n336.Caption);
        end;
      //  n335.Caption := q.FieldByName('freq3').AsString;
//        n336.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n341.Caption := q.FieldByName('nota4').AsString;
        n342.Caption := q.FieldByName('rec4').AsString;
        n343.Caption := q.FieldByName('media4').AsString;
        n346.Caption := q.FieldByName('conceito4').AsString;
        if n341.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n341.Caption);
        end;
        if n343.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n343.Caption);
        end;
        n344.Caption := q.FieldByName('faltas4').AsString;
        if n344.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n344.Caption);
        end;
        if n346.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n346.Caption);
        end;
      //  n345.Caption := q.FieldByName('freq4').AsString;
//        n346.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n351.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n351.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n351.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n351.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n351.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n351.Caption);
        end
        else
          n351.Caption := '';
        //n351.Caption := q.FieldByName('sintbimest').AsString;

        //n352.Caption := q.FieldByName('mediaturmabim').AsString;
        n353.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n354.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n354.Caption);
        end;
        //n354.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n355.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n355.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n355.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n355.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n355.Caption);
        end
        else
          n355.Caption := '';
        //n355.Caption := q.FieldByName('faltas').AsString;

        //n356.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia n4
      4: begin
        n4.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n411.Caption := q.FieldByName('nota1').AsString;
        n412.Caption := q.FieldByName('rec1').AsString;
        n413.Caption := q.FieldByName('media1').AsString;
        n416.Caption := q.FieldByName('conceito1').AsString;
        if n411.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n411.Caption);
        end;
        if n413.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n413.Caption);
        end;
        n414.Caption := q.FieldByName('faltas1').AsString;
        if n414.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n414.Caption);
        end;
        if n416.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n416.Caption);
        end;
     //   n415.Caption := q.FieldByName('freq1').AsString;
//        n416.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n421.Caption := q.FieldByName('nota2').AsString;
        n422.Caption := q.FieldByName('rec2').AsString;
        n423.Caption := q.FieldByName('media2').AsString;
        n426.Caption := q.FieldByName('conceito2').AsString;
        if n421.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n421.Caption);
        end;
        if n423.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n423.Caption);
        end;
        n424.Caption := q.FieldByName('faltas2').AsString;
        if n424.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n424.Caption);
        end;
        if n426.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n426.Caption);
        end;
     //   n425.Caption := q.FieldByName('freq2').AsString;
//        n426.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n431.Caption := q.FieldByName('nota3').AsString;
        n432.Caption := q.FieldByName('rec3').AsString;
        n433.Caption := q.FieldByName('media3').AsString;
        n436.Caption := q.FieldByName('conceito3').AsString;
        if n431.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n431.Caption);
        end;
        if n433.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n433.Caption);
        end;
        n434.Caption := q.FieldByName('faltas3').AsString;
        if n434.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n434.Caption);
        end;
        if n436.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n436.Caption);
        end;
     //   n435.Caption := q.FieldByName('freq3').AsString;
//        n436.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n441.Caption := q.FieldByName('nota4').AsString;
        n442.Caption := q.FieldByName('rec4').AsString;
        n443.Caption := q.FieldByName('media4').AsString;
        n446.Caption := q.FieldByName('conceito4').AsString;
        if n441.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n441.Caption);
        end;
        if n443.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n443.Caption);
        end;
        n444.Caption := q.FieldByName('faltas4').AsString;
        if n444.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n444.Caption);
        end;
        if n446.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n446.Caption);
        end;
     //   n445.Caption := q.FieldByName('freq4').AsString;
//        n446.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n451.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n451.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n451.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n451.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n451.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n451.Caption);
        end
        else
          n451.Caption := '';
        //n451.Caption := q.FieldByName('sintbimest').AsString;

        //n452.Caption := q.FieldByName('mediaturmabim').AsString;
        n453.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n454.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n454.Caption);
        end;
        //n454.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n455.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n455.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n455.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n455.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n455.Caption);
        end
        else
          n455.Caption := '';
        //n455.Caption := q.FieldByName('faltas').AsString;

        //n456.Caption := q.FieldByName('frequencia').AsString;
      end;
    end;
    inc(c);
    q.Next;
  end;

  //Extra
  q.Close;
  q.SQL.Clear;
  with q.SQL do begin
    Add('select ');
    Add('cadmat.no_red_materia [materia], ');
    Add('histalu.qt_falta_bim1 [faltas1], histalu.qt_falta_bim2 [faltas2], ');
    Add('histalu.qt_falta_bim3 [faltas3], histalu.qt_falta_bim4 [faltas4], ');
    Add('histalu.vl_recu_bim1 [rec1], histalu.vl_recu_bim2 [rec2], ');
    Add('histalu.vl_recu_bim3 [rec3], histalu.vl_recu_bim4 [rec4], ');
    Add('histalu.vl_nota_bim1 [nota1], histalu.vl_nota_bim2 [nota2], ');
    Add('histalu.vl_nota_bim3 [nota3], histalu.vl_nota_bim4 [nota4], ');
    Add('histalu.VL_CONC_BIM1 [conceito1], histalu.VL_CONC_BIM2 [conceito2], ');
    Add('histalu.VL_CONC_BIM3 [conceito3], histalu.VL_CONC_BIM4 [conceito4], ');
    Add('histalu.vl_nota_bim1 [media1], histalu.vl_nota_bim2 [media2], ');
    Add('histalu.vl_nota_bim3 [media3], histalu.vl_nota_bim4 [media4], histalu.vl_prova_final [provafinal], ');
    Add('histalu.vl_media_final [mediafinal] ,null  [comp1], null  [comp2], null  [comp3], null  [comp4] ');

    Add('from ');
    Add('tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat ');

    Add('where ');
    Add('(histalu.co_mat = mat.co_mat) ');
    Add('and (mat.id_materia = cadmat.id_materia) ');
    Add('and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ');
    Add('and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ');
    Add('and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ');
    Add('and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ');
    Add('and (cadmat.co_class_boletim = '+'3)');
  end;
  q.Open;
  q.First;
  c := 1;
  while not q.Eof do begin
    case c of
      //Materia n5
      1: begin
        n5.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n511.Caption := q.FieldByName('nota1').AsString;
        n512.Caption := q.FieldByName('rec1').AsString;
        n513.Caption := q.FieldByName('media1').AsString;
        n516.Caption := q.FieldByName('conceito1').AsString;
        if n511.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n511.Caption);
        end;
        if n513.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n513.Caption);
        end;
        n514.Caption := q.FieldByName('faltas1').AsString;
        if n514.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n514.Caption);
        end;
        if n516.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n516.Caption);
        end;
     //   n515.Caption := q.FieldByName('freq1').AsString;
//        n516.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n521.Caption := q.FieldByName('nota2').AsString;
        n522.Caption := q.FieldByName('rec2').AsString;
        n523.Caption := q.FieldByName('media2').AsString;
        n526.Caption := q.FieldByName('conceito2').AsString;
        if n521.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n521.Caption);
        end;
        if n523.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n523.Caption);
        end;
        n524.Caption := q.FieldByName('faltas2').AsString;
        if n524.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n524.Caption);
        end;
        if n526.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n526.Caption);
        end;
      //  n525.Caption := q.FieldByName('freq2').AsString;
//        n526.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n531.Caption := q.FieldByName('nota3').AsString;
        n532.Caption := q.FieldByName('rec3').AsString;
        n533.Caption := q.FieldByName('media3').AsString;
        n536.Caption := q.FieldByName('conceito3').AsString;
        if n531.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n531.Caption);
        end;
        if n533.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n533.Caption);
        end;
        n534.Caption := q.FieldByName('faltas3').AsString;
        if n534.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n534.Caption);
        end;
        if n536.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n536.Caption);
        end;
      //  n535.Caption := q.FieldByName('freq3').AsString;
//        n536.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n541.Caption := q.FieldByName('nota4').AsString;
        n542.Caption := q.FieldByName('rec4').AsString;
        n543.Caption := q.FieldByName('media4').AsString;
        n546.Caption := q.FieldByName('conceito4').AsString;
        if n541.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n541.Caption);
        end;
        if n543.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n543.Caption);
        end;
        n544.Caption := q.FieldByName('faltas4').AsString;
        if n544.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n544.Caption);
        end;
        if n546.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n546.Caption);
        end;
     //   n545.Caption := q.FieldByName('freq4').AsString;
//        n546.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n551.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n551.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n551.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n551.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n551.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n551.Caption);
        end
        else
          n551.Caption := '';
        //n551.Caption := q.FieldByName('sintbimest').AsString;

        //n552.Caption := q.FieldByName('mediaturmabim').AsString;
        n553.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n554.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n554.Caption);
        end;
        //n554.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n555.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n555.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n555.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n555.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n555.Caption);
        end
        else
          n555.Caption := '';
        //n555.Caption := q.FieldByName('faltas').AsString;

        //n556.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia n6
      2: begin
        n6.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n611.Caption := q.FieldByName('nota1').AsString;
        n612.Caption := q.FieldByName('rec1').AsString;
        n613.Caption := q.FieldByName('media1').AsString;
        n616.Caption := q.FieldByName('conceito1').AsString;
        if n611.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n611.Caption);
        end;
        if n613.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n613.Caption);
        end;
        n614.Caption := q.FieldByName('faltas1').AsString;
        if n614.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n614.Caption);
        end;
        if n616.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n616.Caption);
        end;
       // n615.Caption := q.FieldByName('freq1').AsString;
//        n616.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n621.Caption := q.FieldByName('nota2').AsString;
        n622.Caption := q.FieldByName('rec2').AsString;
        n623.Caption := q.FieldByName('media2').AsString;
        n626.Caption := q.FieldByName('conceito2').AsString;
        if n621.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n621.Caption);
        end;
        if n623.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n623.Caption);
        end;
        n624.Caption := q.FieldByName('faltas2').AsString;
        if n624.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n624.Caption);
        end;
        if n626.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n626.Caption);
        end;
     //   n625.Caption := q.FieldByName('freq2').AsString;
//        n626.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n631.Caption := q.FieldByName('nota3').AsString;
        n632.Caption := q.FieldByName('rec3').AsString;
        n633.Caption := q.FieldByName('media3').AsString;
        n636.Caption := q.FieldByName('conceito3').AsString;
        if n631.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n631.Caption);
        end;
        if n633.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n633.Caption);
        end;
        n634.Caption := q.FieldByName('faltas3').AsString;
        if n634.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n634.Caption);
        end;
        if n636.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n636.Caption);
        end;
       // n635.Caption := q.FieldByName('freq3').AsString;
//        n636.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n641.Caption := q.FieldByName('nota4').AsString;
        n642.Caption := q.FieldByName('rec4').AsString;
        n643.Caption := q.FieldByName('media4').AsString;
        n646.Caption := q.FieldByName('conceito4').AsString;
        if n641.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n641.Caption);
        end;
        if n643.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n643.Caption);
        end;
        n644.Caption := q.FieldByName('faltas4').AsString;
        if n644.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n644.Caption);
        end;
        if n646.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n646.Caption);
        end;
       // n645.Caption := q.FieldByName('freq4').AsString;
//        n646.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n651.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n651.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n651.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n651.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n651.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n651.Caption);
        end
        else
          n651.Caption := '';
        //n651.Caption := q.FieldByName('sintbimest').AsString;

        //n652.Caption := q.FieldByName('mediaturmabim').AsString;
        n653.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n654.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n654.Caption);
        end;
        //n654.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n655.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n655.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n655.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n655.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n655.Caption);
        end
        else
          n655.Caption := '';
        //n655.Caption := q.FieldByName('faltas').AsString;

        //n656.Caption := q.FieldByName('frequencia').AsString;
      end;
      //Materia n7
      3: begin
        n7.Caption := q.FieldByName('materia').AsString;

        //Bimestre 1
        n711.Caption := q.FieldByName('nota1').AsString;
        n712.Caption := q.FieldByName('rec1').AsString;
        n713.Caption := q.FieldByName('media1').AsString;
        n716.Caption := q.FieldByName('conceito1').AsString;
        if n711.Caption <> '' then
        begin
          c1 := c1 + 1;
          sc1 := sc1 + StrToFloat(n711.Caption);
        end;
        if n713.Caption <> '' then
        begin
          c3 := c3 + 1;
          sc3 := sc3 + StrToFloat(n713.Caption);
        end;
        n714.Caption := q.FieldByName('faltas1').AsString;
        if n714.Caption <> '' then
        begin
          sc4 := sc4 + StrToFloat(n714.Caption);
        end;
        if n716.Caption <> '' then
        begin
          c6 := c6 + 1;
          sc6 := sc6 + StrToFloat(n716.Caption);
        end;
      //  n715.Caption := q.FieldByName('freq1').AsString;
//        n716.Caption := q.FieldByName('comp1').AsString;

        //Bimestre 2
        n721.Caption := q.FieldByName('nota2').AsString;
        n722.Caption := q.FieldByName('rec2').AsString;
        n723.Caption := q.FieldByName('media2').AsString;
        n726.Caption := q.FieldByName('conceito2').AsString;
        if n721.Caption <> '' then
        begin
          c7 := c7 + 1;
          sc7 := sc7 + StrToFloat(n721.Caption);
        end;
        if n723.Caption <> '' then
        begin
          c9 := c9 + 1;
          sc9 := sc9 + StrToFloat(n723.Caption);
        end;
        n724.Caption := q.FieldByName('faltas2').AsString;
        if n724.Caption <> '' then
        begin
          sc10 := sc10 + StrToFloat(n724.Caption);
        end;
        if n726.Caption <> '' then
        begin
          c12 := c12 + 1;
          sc12 := sc12 + StrToFloat(n726.Caption);
        end;
     //   n725.Caption := q.FieldByName('freq2').AsString;
//        n726.Caption := q.FieldByName('comp2').AsString;

        //Bimestre 3
        n731.Caption := q.FieldByName('nota3').AsString;
        n732.Caption := q.FieldByName('rec3').AsString;
        n733.Caption := q.FieldByName('media3').AsString;
        n736.Caption := q.FieldByName('conceito3').AsString;
        if n731.Caption <> '' then
        begin
          c13 := c13 + 1;
          sc13 := sc13 + StrToFloat(n731.Caption);
        end;
        if n733.Caption <> '' then
        begin
          c15 := c15 + 1;
          sc15 := sc15 + StrToFloat(n733.Caption);
        end;
        n734.Caption := q.FieldByName('faltas3').AsString;
        if n734.Caption <> '' then
        begin
          sc16 := sc16 + StrToFloat(n734.Caption);
        end;
        if n736.Caption <> '' then
        begin
          c18 := c18 + 1;
          sc18 := sc18 + StrToFloat(n736.Caption);
        end;
      //  n735.Caption := q.FieldByName('freq3').AsString;
//        n736.Caption := q.FieldByName('comp3').AsString;

        //Bimestre 4
        n741.Caption := q.FieldByName('nota4').AsString;
        n742.Caption := q.FieldByName('rec4').AsString;
        n743.Caption := q.FieldByName('media4').AsString;
        n746.Caption := q.FieldByName('conceito4').AsString;
        if n741.Caption <> '' then
        begin
          c19 := c19 + 1;
          sc19 := sc19 + StrToFloat(n741.Caption);
        end;
        if n743.Caption <> '' then
        begin
          c21 := c21 + 1;
          sc21 := sc21 + StrToFloat(n743.Caption);
        end;
        n744.Caption := q.FieldByName('faltas4').AsString;
        if n744.Caption <> '' then
        begin
          sc22 := sc22 + StrToFloat(n744.Caption);
        end;
        if n746.Caption <> '' then
        begin
          c24 := c24 + 1;
          sc24 := sc24 + StrToFloat(n746.Caption);
        end;
      //  n745.Caption := q.FieldByName('freq4').AsString;
//        n746.Caption := q.FieldByName('comp4').AsString;

        //Bimestre 5
        if not q.FieldByName('nota4').IsNull then
        begin
          n751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat+q.FieldByName('nota4').AsFloat)/4);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n751.Caption);
        end
        else if not q.FieldByName('nota3').IsNull then
        begin
          n751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat+
          q.FieldByName('nota3').AsFloat)/3);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n751.Caption);
        end
        else if not q.FieldByName('nota2').IsNull then
        begin
          n751.Caption := FormatFloat('##.#',
          (q.FieldByName('nota1').AsFloat+q.FieldByName('nota2').AsFloat)/2);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n751.Caption);
        end
        else if not q.FieldByName('nota1').IsNull then
        begin
          n751.Caption := FormatFloat('##.#',q.FieldByName('nota1').AsFloat);
          c25 := c25 + 1;
          sc25 := sc25 + StrToFloat(n751.Caption);
        end
        else
          n751.Caption := '';
        //n751.Caption := q.FieldByName('sintbimest').AsString;

        //n752.Caption := q.FieldByName('mediaturmabim').AsString;
        n753.Caption := q.FieldByName('provafinal').AsString;
        if q.FieldByName('mediafinal').AsString <> '' then
        begin
          n754.Caption := FormatFloat('##.#',q.FieldByName('mediafinal').AsFloat);
          c28 := c28 + 1;
          sc28 := sc28 + StrToFloat(n754.Caption);
        end;
        //n754.Caption := q.FieldByName('mediafinal').AsString;

        if not q.FieldByName('faltas4').IsNull then
        begin
          n755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger+q.FieldByName('faltas4').AsInteger);
          sc29 := sc29 + StrToInt(n755.Caption);
        end
        else if not q.FieldByName('faltas3').IsNull then
        begin
          n755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger+
          q.FieldByName('faltas3').AsInteger);
          sc29 := sc29 + StrToInt(n755.Caption);
        end
        else if not q.FieldByName('faltas2').IsNull then
        begin
          n755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger+q.FieldByName('faltas2').AsInteger);
          sc29 := sc29 + StrToInt(n755.Caption);
        end
        else if not q.FieldByName('faltas1').IsNull then
        begin
          n755.Caption := IntToStr(q.FieldByName('faltas1').AsInteger);
          sc29 := sc29 + StrToInt(n755.Caption);
        end
        else
          n755.Caption := '';
        //n755.Caption := q.FieldByName('faltas').AsString;

        //n756.Caption := q.FieldByName('frequencia').AsString;
      end;
    end;
    inc(c);
    q.Next;
  end;
  q.Free;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select c.co_mat_col, c.no_col from tb_respon_materia rm ' +
                ' join tb03_colabor c on c.co_col = rm.co_profes_resp and c.co_emp = rm.co_emp ' +
                'where rm.co_emp = ' + IntToStr(Sys_CodigoEmpresaAtiva) +
                ' and rm.co_cur = ' + QryRelatorio.FieldByName('cocur').AsString +
                ' and rm.co_tur = ' + QryRelatorio.FieldByName('cotur').AsString +
                ' and rm.co_modu_cur = ' + QryRelatorio.FieldByName('comoducur').AsString +
                ' and rm.co_ano_ref = ' + QryRelatorio.FieldByName('anoletivo').AsString +
                ' and rm.co_mat is null ';
    Open;

    if not IsEmpty then
    begin
      QRLDescProfessor.Caption := FormatMaskText('00.000-0;0',FieldByName('co_mat_col').AsString) + ' - ' + FieldByName('no_col').AsString;
    end
    else
      QRLDescProfessor.Caption := '';
  end;

  {**********
  Cálculo do percentual de frequência
  ***********}
  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select gc.qtde_aula_sem from ' +
    'tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat, tb43_grd_curso gc ' +
    'where ' +
    '(histalu.co_mat = gc.co_mat) and (histalu.co_cur = gc.co_cur) and (histalu.co_modu_cur = gc.co_modu_cur) and ' +
    '(histalu.co_ano_ref = gc.co_ano_grade) ' +
    'and (histalu.co_mat = mat.co_mat) ' +
    'and (mat.id_materia = cadmat.id_materia) ' +
    'and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ' +
    'and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ' +
    'and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ' +
    'and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ' +
    'and (cadmat.co_class_boletim = '+'1)';
    Open;

    ctPF := 1;

    while not Eof do
    begin
      if (ctPf = 1) and (m155.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m156.Caption := IntToStr(Round(100 -((StrToInt(m155.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m114.Caption <> '' then
           // m115.Caption := IntToStr(Round(100 -((StrToInt(m114.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m115.Caption := '';
          if m124.Caption <> '' then
           // m125.Caption := IntToStr(Round(100 -((StrToInt(m124.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m125.Caption := '';
          if m134.Caption <> '' then
           // m135.Caption := IntToStr(Round(100 -((StrToInt(m134.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m135.Caption := '';
          if m144.Caption <> '' then
           // m145.Caption := IntToStr(Round(100 -((StrToInt(m144.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m145.Caption := '';
        end
        else
          m156.Caption := '';
      end;

      if (ctPf = 2) and (m255.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m256.Caption := IntToStr(Round(100-((StrToInt(m255.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m214.Caption <> '' then
           // m215.Caption := IntToStr(Round(100 -((StrToInt(m214.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m215.Caption := '';
          if m224.Caption <> '' then
           // m225.Caption := IntToStr(Round(100 -((StrToInt(m224.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m225.Caption := '';
          if m234.Caption <> '' then
          //  m235.Caption := IntToStr(Round(100 -((StrToInt(m234.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m235.Caption := '';
          if m244.Caption <> '' then
           // m245.Caption := IntToStr(Round(100 -((StrToInt(m244.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m245.Caption := '';
        end
        else
          m256.Caption := '';
      end;

      if (ctPf = 3) and (m355.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m356.Caption := IntToStr(Round(100-((StrToInt(m355.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m314.Caption <> '' then
          //  m315.Caption := IntToStr(Round(100 -((StrToInt(m314.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m315.Caption := '';
          if m324.Caption <> '' then
          //  m325.Caption := IntToStr(Round(100 -((StrToInt(m324.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m325.Caption := '';
          if m334.Caption <> '' then
           // m335.Caption := IntToStr(Round(100 -((StrToInt(m334.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m335.Caption := '';
          if m344.Caption <> '' then
          //  m345.Caption := IntToStr(Round(100 -((StrToInt(m344.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m345.Caption := '';
        end
        else
          m356.Caption := '';
      end;

      if (ctPf = 4) and (m455.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m456.Caption := IntToStr(Round(100-((StrToInt(m455.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m414.Caption <> '' then
           // m415.Caption := IntToStr(Round(100 -((StrToInt(m414.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m415.Caption := '';
          if m424.Caption <> '' then
          //  m425.Caption := IntToStr(Round(100 -((StrToInt(m424.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m425.Caption := '';
          if m434.Caption <> '' then
           // m435.Caption := IntToStr(Round(100 -((StrToInt(m434.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m435.Caption := '';
          if m444.Caption <> '' then
           // m445.Caption := IntToStr(Round(100 -((StrToInt(m444.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m445.Caption := '';
        end
        else
          m456.Caption := '';
      end;

      if (ctPf = 5) and (m555.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m556.Caption := IntToStr(Round(100-((StrToInt(m555.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m514.Caption <> '' then
           // m515.Caption := IntToStr(Round(100 -((StrToInt(m514.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m515.Caption := '';
          if m524.Caption <> '' then
           // m525.Caption := IntToStr(Round(100 -((StrToInt(m524.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m525.Caption := '';
          if m534.Caption <> '' then
           // m535.Caption := IntToStr(Round(100 -((StrToInt(m534.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m535.Caption := '';
          if m544.Caption <> '' then
          //  m545.Caption := IntToStr(Round(100 -((StrToInt(m544.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m545.Caption := '';
        end
        else
          m556.Caption := '';
      end;

      if (ctPf = 6) and (m655.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m656.Caption := IntToStr(Round(100-((StrToInt(m655.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m614.Caption <> '' then
           // m615.Caption := IntToStr(Round(100 -((StrToInt(m614.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m615.Caption := '';
          if m624.Caption <> '' then
          //  m625.Caption := IntToStr(Round(100 -((StrToInt(m624.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m625.Caption := '';
          if m634.Caption <> '' then
           // m635.Caption := IntToStr(Round(100 -((StrToInt(m634.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m635.Caption := '';
          if m644.Caption <> '' then
          //  m645.Caption := IntToStr(Round(100 -((StrToInt(m644.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m645.Caption := '';
        end
        else
          m656.Caption := '';
      end;

      if (ctPf = 7) and (m755.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m756.Caption := IntToStr(Round(100-((StrToInt(m755.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m714.Caption <> '' then
          //  m715.Caption := IntToStr(Round(100 -((StrToInt(m714.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m715.Caption := '';
          if m724.Caption <> '' then
           // m725.Caption := IntToStr(Round(100 -((StrToInt(m724.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m725.Caption := '';
          if m734.Caption <> '' then
           // m735.Caption := IntToStr(Round(100 -((StrToInt(m734.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m735.Caption := '';
          if m744.Caption <> '' then
           // m745.Caption := IntToStr(Round(100 -((StrToInt(m744.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m745.Caption := '';
        end
        else
          m756.Caption := '';
      end;

      if (ctPf = 8) and (m855.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m856.Caption := IntToStr(Round(100-((StrToInt(m855.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m814.Caption <> '' then
           // m815.Caption := IntToStr(Round(100 -((StrToInt(m814.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m815.Caption := '';
          if m824.Caption <> '' then
           // m825.Caption := IntToStr(Round(100 -((StrToInt(m824.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m825.Caption := '';
          if m834.Caption <> '' then
           // m835.Caption := IntToStr(Round(100 -((StrToInt(m834.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m835.Caption := '';
          if m844.Caption <> '' then
           // m845.Caption := IntToStr(Round(100 -((StrToInt(m844.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m845.Caption := '';
        end
        else
          m856.Caption := '';
      end;

      if (ctPf = 9) and (m955.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          m956.Caption := IntToStr(Round(100-((StrToInt(m955.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if m914.Caption <> '' then
           // m915.Caption := IntToStr(Round(100 -((StrToInt(m914.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m915.Caption := '';
          if m924.Caption <> '' then
          //  m925.Caption := IntToStr(Round(100 -((StrToInt(m924.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m925.Caption := '';
          if m934.Caption <> '' then
          //  m935.Caption := IntToStr(Round(100 -((StrToInt(m934.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m935.Caption := '';
          if m944.Caption <> '' then
          //  m945.Caption := IntToStr(Round(100 -((StrToInt(m944.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            m945.Caption := '';
        end
        else
          m956.Caption := '';
      end;

      ctPF := ctPf + 1;
      Next;
    end;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select gc.qtde_aula_sem from ' +
    'tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat, tb43_grd_curso gc ' +
    'where ' +
    '(histalu.co_mat = gc.co_mat) and (histalu.co_cur = gc.co_cur) and (histalu.co_modu_cur = gc.co_modu_cur) and ' +
    '(histalu.co_ano_ref = gc.co_ano_grade) ' +
    'and (histalu.co_mat = mat.co_mat) ' +
    'and (mat.id_materia = cadmat.id_materia) ' +
    'and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ' +
    'and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ' +
    'and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ' +
    'and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ' +
    'and (cadmat.co_class_boletim = '+'2)';
    Open;

    ctPF := 1;

    while not Eof do
    begin
      if (ctPf = 1) and (n155.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n156.Caption := IntToStr(Round(100-((StrToInt(n155.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n114.Caption <> '' then
           // n115.Caption := IntToStr(Round(100 -((StrToInt(n114.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n115.Caption := '';
          if n124.Caption <> '' then
           // n125.Caption := IntToStr(Round(100 -((StrToInt(n124.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n125.Caption := '';
          if n134.Caption <> '' then
           // n135.Caption := IntToStr(Round(100 -((StrToInt(n134.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n135.Caption := '';
          if n144.Caption <> '' then
           // n145.Caption := IntToStr(Round(100 -((StrToInt(n144.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n145.Caption := '';
        end
        else
        begin
          n156.Caption := '';
        end;
      end;

      if (ctPf = 2) and (n255.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n256.Caption := IntToStr(Round(100-((StrToInt(n255.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n214.Caption <> '' then
          //  n215.Caption := IntToStr(Round(100 -((StrToInt(n214.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n215.Caption := '';
          if n224.Caption <> '' then
           // n225.Caption := IntToStr(Round(100 -((StrToInt(n224.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n225.Caption := '';
          if n234.Caption <> '' then
           // n235.Caption := IntToStr(Round(100 -((StrToInt(n234.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n235.Caption := '';
          if n244.Caption <> '' then
           // n245.Caption := IntToStr(Round(100 -((StrToInt(n244.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n245.Caption := '';
        end
        else
          n256.Caption := '';
      end;

      if (ctPf = 3) and (n355.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n356.Caption := IntToStr(Round(100-((StrToInt(n355.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n314.Caption <> '' then
          //  n315.Caption := IntToStr(Round(100 -((StrToInt(n314.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n315.Caption := '';
          if n324.Caption <> '' then
           // n325.Caption := IntToStr(Round(100 -((StrToInt(n324.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n325.Caption := '';
          if n334.Caption <> '' then
           // n335.Caption := IntToStr(Round(100 -((StrToInt(n334.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n335.Caption := '';
          if n344.Caption <> '' then
           // n345.Caption := IntToStr(Round(100 -((StrToInt(n344.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n345.Caption := '';
        end
        else
          n356.Caption := '';
      end;

      if (ctPf = 4) and (n455.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n456.Caption := IntToStr(Round(100-((StrToInt(n455.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n414.Caption <> '' then
           // n415.Caption := IntToStr(Round(100 -((StrToInt(n414.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n415.Caption := '';
          if n424.Caption <> '' then
           // n425.Caption := IntToStr(Round(100 -((StrToInt(n424.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n425.Caption := '';
          if n434.Caption <> '' then
           // n435.Caption := IntToStr(Round(100 -((StrToInt(n434.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n435.Caption := '';
          if n444.Caption <> '' then
           // n445.Caption := IntToStr(Round(100 -((StrToInt(n444.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n445.Caption := '';
        end
        else
          n456.Caption := '';
      end;

      ctPF := ctPf + 1;
      Next;
    end;
  end;

  with DM.QrySql do
  begin
    Close;
    SQL.Clear;
    SQL.Text := 'select gc.qtde_aula_sem from ' +
    'tb079_hist_aluno histalu, tb107_cadmaterias cadmat, tb02_materia mat, tb43_grd_curso gc ' +
    'where ' +
    '(histalu.co_mat = gc.co_mat) and (histalu.co_cur = gc.co_cur) and (histalu.co_modu_cur = gc.co_modu_cur) and ' +
    '(histalu.co_ano_ref = gc.co_ano_grade) ' +
    'and (histalu.co_mat = mat.co_mat) ' +
    'and (mat.id_materia = cadmat.id_materia) ' +
    'and (histalu.co_cur = '+QryRelatorio.FieldByName('cocur').AsString+') ' +
    'and (histalu.co_alu = '+QryRelatorio.FieldByName('coalu').AsString+') ' +
    'and (histalu.co_ano_ref = '+QryRelatorio.FieldByName('anoletivo').AsString+') ' +
    'and (histalu.co_modu_cur = '+QryRelatorio.FieldByName('comoducur').AsString+') ' +
    'and (cadmat.co_class_boletim = '+'3)';
    Open;

    ctPF := 1;

    while not Eof do
    begin
      if (ctPf = 1) and (n555.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n556.Caption := IntToStr(Round(100-((StrToInt(n555.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n514.Caption <> '' then
           // n515.Caption := IntToStr(Round(100 -((StrToInt(n514.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n515.Caption := '';
          if n524.Caption <> '' then
           // n525.Caption := IntToStr(Round(100 -((StrToInt(n524.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n525.Caption := '';
          if n534.Caption <> '' then
          //  n535.Caption := IntToStr(Round(100 -((StrToInt(n534.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n535.Caption := '';
          if n544.Caption <> '' then
          //  n545.Caption := IntToStr(Round(100 -((StrToInt(n544.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n545.Caption := '';
        end
        else
          n556.Caption := '';
      end;

      if (ctPf = 2) and (n655.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n656.Caption := IntToStr(Round(100-((StrToInt(n655.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n614.Caption <> '' then
           // n615.Caption := IntToStr(Round(100 -((StrToInt(n614.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n615.Caption := '';
          if n624.Caption <> '' then
           // n625.Caption := IntToStr(Round(100 -((StrToInt(n624.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n625.Caption := '';
          if n634.Caption <> '' then
          //  n635.Caption := IntToStr(Round(100 -((StrToInt(n634.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n635.Caption := '';
          if n644.Caption <> '' then
          //  n645.Caption := IntToStr(Round(100 -((StrToInt(n644.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n645.Caption := '';
        end
        else
          n656.Caption := '';
      end;

      if (ctPf = 3) and (n755.Caption <> '') then
      begin
        if not (FieldByName('qtde_aula_sem').IsNull) then
        begin
          n756.Caption := IntToStr(Round(100-((StrToInt(n755.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)));
          if n714.Caption <> '' then
           // n715.Caption := IntToStr(Round(100 -((StrToInt(n714.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n715.Caption := '';
          if n724.Caption <> '' then
          //  n725.Caption := IntToStr(Round(100 -((StrToInt(n724.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n725.Caption := '';
          if n734.Caption <> '' then
           // n735.Caption := IntToStr(Round(100 -((StrToInt(n734.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n735.Caption := '';
          if n744.Caption <> '' then
           // n745.Caption := IntToStr(Round(100 -((StrToInt(n744.Caption)*100)/FieldByName('qtde_aula_sem').AsInteger)))
          else
            n745.Caption := '';
        end
        else
          n756.Caption := '';
      end;

      ctPF := ctPf + 1;
      Next;
    end;
  end;

  //****************************************************************

  if c1 > 0 then
    T1.Caption := FloatToStrF(sc1/c1,ffNumber,10,1);
  if c3 > 0 then
    T3.Caption := FloatToStrF(sc3/c3,ffNumber,10,1);
  if c6 > 0 then
    T6.Caption := FloatToStrF(sc6/c6,ffNumber,10,1);
  if c7 > 0 then
    T7.Caption := FloatToStrF(sc7/c7,ffNumber,10,1);
  if c9 > 0 then
    T9.Caption := FloatToStrF(sc9/c9,ffNumber,10,1);
  if c12 > 0 then
    T12.Caption := FloatToStrF(sc12/c12,ffNumber,10,1);
  if c13 > 0 then
    T13.Caption := FloatToStrF(sc13/c13,ffNumber,10,1);
  if c15 > 0 then
    T15.Caption := FloatToStrF(sc15/c15,ffNumber,10,1);
  if c18 > 0 then
    T18.Caption := FloatToStrF(sc18/c18,ffNumber,10,1);
  if c19 > 0 then
    T19.Caption := FloatToStrF(sc19/c19,ffNumber,10,1);
  if c21 > 0 then
    T21.Caption := FloatToStrF(sc21/c21,ffNumber,10,1);
  if c24 > 0 then
    T24.Caption := FloatToStrF(sc24/c24,ffNumber,10,1);
  if c25 > 0 then
    T25.Caption := FloatToStrF(sc25/c25,ffNumber,10,1);
  if c28 > 0 then
    T28.Caption := FloatToStrF(sc28/c28,ffNumber,10,1);
  T29.Caption := FloatToStr(sc29);
  T4.Caption := FloatToStr(sc4);
  T10.Caption := FloatToStr(sc10);
  T16.Caption := FloatToStr(sc16);
  T22.Caption := FloatToStr(sc22);
end;

procedure TFrmRelBolEscAlu.SetCo_Alu(i: integer);
begin
  co_alu := i;
end;
procedure TFrmRelBolEscAlu.QuickRep1BeforePrint(Sender: TCustomQuickRep;
  var PrintReport: Boolean);
begin
  inherited;
  QRBANDSGIE.Enabled := false;
  m1.Caption := '';
  m2.Caption := '';
  m3.Caption := '';
  m4.Caption := '';
  m5.Caption := '';
  m6.Caption := '';
  m7.Caption := '';
  m8.Caption := '';
  m9.Caption := '';

  //1ºBimestre M
  m111.Caption := '';
  m211.Caption := '';
  m311.Caption := '';
  m411.Caption := '';
  m511.Caption := '';
  m611.Caption := '';
  m711.Caption := '';
  m811.Caption := '';
  m911.Caption := '';
  m112.Caption := '';
  m212.Caption := '';
  m312.Caption := '';
  m412.Caption := '';
  m512.Caption := '';
  m612.Caption := '';
  m712.Caption := '';
  m812.Caption := '';
  m912.Caption := '';
  m113.Caption := '';
  m213.Caption := '';
  m313.Caption := '';
  m413.Caption := '';
  m513.Caption := '';
  m613.Caption := '';
  m713.Caption := '';
  m813.Caption := '';
  m913.Caption := '';
  m114.Caption := '';
  m214.Caption := '';
  m314.Caption := '';
  m414.Caption := '';
  m514.Caption := '';
  m614.Caption := '';
  m714.Caption := '';
  m814.Caption := '';
  m914.Caption := '';
  m115.Caption := '';
  m215.Caption := '';
  m315.Caption := '';
  m415.Caption := '';
  m515.Caption := '';
  m615.Caption := '';
  m715.Caption := '';
  m815.Caption := '';
  m915.Caption := '';
  m116.Caption := '';
  m216.Caption := '';
  m316.Caption := '';
  m416.Caption := '';
  m516.Caption := '';
  m616.Caption := '';
  m716.Caption := '';
  m816.Caption := '';
  m916.Caption := '';
  //2º Bimestre M
  m121.Caption := '';
  m221.Caption := '';
  m321.Caption := '';
  m421.Caption := '';
  m521.Caption := '';
  m621.Caption := '';
  m721.Caption := '';
  m821.Caption := '';
  m921.Caption := '';
  m122.Caption := '';
  m222.Caption := '';
  m322.Caption := '';
  m422.Caption := '';
  m522.Caption := '';
  m622.Caption := '';
  m722.Caption := '';
  m822.Caption := '';
  m922.Caption := '';
  m123.Caption := '';
  m223.Caption := '';
  m323.Caption := '';
  m423.Caption := '';
  m523.Caption := '';
  m623.Caption := '';
  m723.Caption := '';
  m823.Caption := '';
  m923.Caption := '';
  m124.Caption := '';
  m224.Caption := '';
  m324.Caption := '';
  m424.Caption := '';
  m524.Caption := '';
  m624.Caption := '';
  m724.Caption := '';
  m824.Caption := '';
  m924.Caption := '';
  m125.Caption := '';
  m225.Caption := '';
  m325.Caption := '';
  m425.Caption := '';
  m525.Caption := '';
  m625.Caption := '';
  m725.Caption := '';
  m825.Caption := '';
  m925.Caption := '';
  m126.Caption := '';
  m226.Caption := '';
  m326.Caption := '';
  m426.Caption := '';
  m526.Caption := '';
  m626.Caption := '';
  m726.Caption := '';
  m826.Caption := '';
  m926.Caption := '';
  //3º Bimestre M
  m131.Caption := '';
  m231.Caption := '';
  m331.Caption := '';
  m431.Caption := '';
  m531.Caption := '';
  m631.Caption := '';
  m731.Caption := '';
  m831.Caption := '';
  m931.Caption := '';
  m132.Caption := '';
  m232.Caption := '';
  m332.Caption := '';
  m432.Caption := '';
  m532.Caption := '';
  m632.Caption := '';
  m732.Caption := '';
  m832.Caption := '';
  m932.Caption := '';
  m133.Caption := '';
  m233.Caption := '';
  m333.Caption := '';
  m433.Caption := '';
  m533.Caption := '';
  m633.Caption := '';
  m733.Caption := '';
  m833.Caption := '';
  m933.Caption := '';
  m134.Caption := '';
  m234.Caption := '';
  m334.Caption := '';
  m434.Caption := '';
  m534.Caption := '';
  m634.Caption := '';
  m734.Caption := '';
  m834.Caption := '';
  m934.Caption := '';
  m135.Caption := '';
  m235.Caption := '';
  m335.Caption := '';
  m435.Caption := '';
  m535.Caption := '';
  m635.Caption := '';
  m735.Caption := '';
  m835.Caption := '';
  m935.Caption := '';
  m136.Caption := '';
  m236.Caption := '';
  m336.Caption := '';
  m436.Caption := '';
  m536.Caption := '';
  m636.Caption := '';
  m736.Caption := '';
  m836.Caption := '';
  m936.Caption := '';
  //4º Bimestre M
  m141.Caption := '';
  m241.Caption := '';
  m341.Caption := '';
  m441.Caption := '';
  m541.Caption := '';
  m641.Caption := '';
  m741.Caption := '';
  m841.Caption := '';
  m941.Caption := '';
  m142.Caption := '';
  m242.Caption := '';
  m342.Caption := '';
  m442.Caption := '';
  m542.Caption := '';
  m642.Caption := '';
  m742.Caption := '';
  m842.Caption := '';
  m942.Caption := '';
  m143.Caption := '';
  m243.Caption := '';
  m343.Caption := '';
  m443.Caption := '';
  m543.Caption := '';
  m643.Caption := '';
  m743.Caption := '';
  m843.Caption := '';
  m943.Caption := '';
  m144.Caption := '';
  m244.Caption := '';
  m344.Caption := '';
  m444.Caption := '';
  m544.Caption := '';
  m644.Caption := '';
  m744.Caption := '';
  m844.Caption := '';
  m944.Caption := '';
  m145.Caption := '';
  m245.Caption := '';
  m345.Caption := '';
  m445.Caption := '';
  m545.Caption := '';
  m645.Caption := '';
  m745.Caption := '';
  m845.Caption := '';
  m945.Caption := '';
  m146.Caption := '';
  m246.Caption := '';
  m346.Caption := '';
  m446.Caption := '';
  m546.Caption := '';
  m646.Caption := '';
  m746.Caption := '';
  m846.Caption := '';
  m946.Caption := '';
  //Resultados
  m151.Caption := '';
  m251.Caption := '';
  m351.Caption := '';
  m451.Caption := '';
  m551.Caption := '';
  m651.Caption := '';
  m751.Caption := '';
  m851.Caption := '';
  m951.Caption := '';
  m152.Caption := '';
  m252.Caption := '';
  m352.Caption := '';
  m452.Caption := '';
  m552.Caption := '';
  m652.Caption := '';
  m752.Caption := '';
  m852.Caption := '';
  m952.Caption := '';
  m153.Caption := '';
  m253.Caption := '';
  m353.Caption := '';
  m453.Caption := '';
  m553.Caption := '';
  m653.Caption := '';
  m753.Caption := '';
  m853.Caption := '';
  m953.Caption := '';
  m154.Caption := '';
  m254.Caption := '';
  m354.Caption := '';
  m454.Caption := '';
  m554.Caption := '';
  m654.Caption := '';
  m754.Caption := '';
  m854.Caption := '';
  m954.Caption := '';
  m155.Caption := '';
  m255.Caption := '';
  m355.Caption := '';
  m455.Caption := '';
  m555.Caption := '';
  m655.Caption := '';
  m755.Caption := '';
  m855.Caption := '';
  m955.Caption := '';
  m156.Caption := '';
  m256.Caption := '';
  m356.Caption := '';
  m456.Caption := '';
  m556.Caption := '';
  m656.Caption := '';
  m756.Caption := '';
  m856.Caption := '';
  m956.Caption := '';

  /////////////////////
  n1.Caption := '';
  n2.Caption := '';
  n3.Caption := '';
  n4.Caption := '';
  n5.Caption := '';
  n6.Caption := '';
  n7.Caption := '';
  //1ºBimestre M
  n111.Caption := '';
  n211.Caption := '';
  n311.Caption := '';
  n411.Caption := '';
  n511.Caption := '';
  n611.Caption := '';
  n711.Caption := '';
  n112.Caption := '';
  n212.Caption := '';
  n312.Caption := '';
  n412.Caption := '';
  n512.Caption := '';
  n612.Caption := '';
  n712.Caption := '';
  n113.Caption := '';
  n213.Caption := '';
  n313.Caption := '';
  n413.Caption := '';
  n513.Caption := '';
  n613.Caption := '';
  n713.Caption := '';
  n114.Caption := '';
  n214.Caption := '';
  n314.Caption := '';
  n414.Caption := '';
  n514.Caption := '';
  n614.Caption := '';
  n714.Caption := '';
  n115.Caption := '';
  n215.Caption := '';
  n315.Caption := '';
  n415.Caption := '';
  n515.Caption := '';
  n615.Caption := '';
  n715.Caption := '';
  n116.Caption := '';
  n216.Caption := '';
  n316.Caption := '';
  n416.Caption := '';
  n516.Caption := '';
  n616.Caption := '';
  n716.Caption := '';
  //2º Bimestre M
  n121.Caption := '';
  n221.Caption := '';
  n321.Caption := '';
  n421.Caption := '';
  n521.Caption := '';
  n621.Caption := '';
  n721.Caption := '';
  n122.Caption := '';
  n222.Caption := '';
  n322.Caption := '';
  n422.Caption := '';
  n522.Caption := '';
  n622.Caption := '';
  n722.Caption := '';
  n123.Caption := '';
  n223.Caption := '';
  n323.Caption := '';
  n423.Caption := '';
  n523.Caption := '';
  n623.Caption := '';
  n723.Caption := '';
  n124.Caption := '';
  n224.Caption := '';
  n324.Caption := '';
  n424.Caption := '';
  n524.Caption := '';
  n624.Caption := '';
  n724.Caption := '';
  n125.Caption := '';
  n225.Caption := '';
  n325.Caption := '';
  n425.Caption := '';
  n525.Caption := '';
  n625.Caption := '';
  n725.Caption := '';
  n126.Caption := '';
  n226.Caption := '';
  n326.Caption := '';
  n426.Caption := '';
  n526.Caption := '';
  n626.Caption := '';
  n726.Caption := '';
  //3º Bimestre M
  n131.Caption := '';
  n231.Caption := '';
  n331.Caption := '';
  n431.Caption := '';
  n531.Caption := '';
  n631.Caption := '';
  n731.Caption := '';
  n132.Caption := '';
  n232.Caption := '';
  n332.Caption := '';
  n432.Caption := '';
  n532.Caption := '';
  n632.Caption := '';
  n732.Caption := '';
  n133.Caption := '';
  n233.Caption := '';
  n333.Caption := '';
  n433.Caption := '';
  n533.Caption := '';
  n633.Caption := '';
  n733.Caption := '';
  n134.Caption := '';
  n234.Caption := '';
  n334.Caption := '';
  n434.Caption := '';
  n534.Caption := '';
  n634.Caption := '';
  n734.Caption := '';
  n135.Caption := '';
  n235.Caption := '';
  n335.Caption := '';
  n435.Caption := '';
  n535.Caption := '';
  n635.Caption := '';
  n735.Caption := '';
  n136.Caption := '';
  n236.Caption := '';
  n336.Caption := '';
  n436.Caption := '';
  n536.Caption := '';
  n636.Caption := '';
  n736.Caption := '';
  //4º Bimestre M
  n141.Caption := '';
  n241.Caption := '';
  n341.Caption := '';
  n441.Caption := '';
  n541.Caption := '';
  n641.Caption := '';
  n741.Caption := '';
  n142.Caption := '';
  n242.Caption := '';
  n342.Caption := '';
  n442.Caption := '';
  n542.Caption := '';
  n642.Caption := '';
  n742.Caption := '';
  n143.Caption := '';
  n243.Caption := '';
  n343.Caption := '';
  n443.Caption := '';
  n543.Caption := '';
  n643.Caption := '';
  n743.Caption := '';
  n144.Caption := '';
  n244.Caption := '';
  n344.Caption := '';
  n444.Caption := '';
  n544.Caption := '';
  n644.Caption := '';
  n744.Caption := '';
  n145.Caption := '';
  n245.Caption := '';
  n345.Caption := '';
  n445.Caption := '';
  n545.Caption := '';
  n645.Caption := '';
  n745.Caption := '';
  n146.Caption := '';
  n246.Caption := '';
  n346.Caption := '';
  n446.Caption := '';
  n546.Caption := '';
  n646.Caption := '';
  n746.Caption := '';
  //Resultados
  n151.Caption := '';
  n251.Caption := '';
  n351.Caption := '';
  n451.Caption := '';
  n551.Caption := '';
  n651.Caption := '';
  n751.Caption := '';
  n152.Caption := '';
  n252.Caption := '';
  n352.Caption := '';
  n452.Caption := '';
  n552.Caption := '';
  n652.Caption := '';
  n752.Caption := '';
  n153.Caption := '';
  n253.Caption := '';
  n353.Caption := '';
  n453.Caption := '';
  n553.Caption := '';
  n653.Caption := '';
  n753.Caption := '';
  n154.Caption := '';
  n254.Caption := '';
  n354.Caption := '';
  n454.Caption := '';
  n554.Caption := '';
  n654.Caption := '';
  n754.Caption := '';
  n155.Caption := '';
  n255.Caption := '';
  n355.Caption := '';
  n455.Caption := '';
  n555.Caption := '';
  n655.Caption := '';
  n755.Caption := '';
  n156.Caption := '';
  n256.Caption := '';
  n356.Caption := '';
  n456.Caption := '';
  n556.Caption := '';
  n656.Caption := '';
  n756.Caption := '';

  //Médias Totais
  T1.Caption := '';
  T2.Caption := '';
  T3.Caption := '';
  T4.Caption := '';
  T5.Caption := '';
  T6.Caption := '';
  T7.Caption := '';
  T8.Caption := '';
  T9.Caption := '';
  T10.Caption := '';
  T11.Caption := '';
  T12.Caption := '';
  T13.Caption := '';
  T14.Caption := '';
  T15.Caption := '';
  T16.Caption := '';
  T17.Caption := '';
  T18.Caption := '';
  T19.Caption := '';
  T20.Caption := '';
  T21.Caption := '';
  T22.Caption := '';
  T23.Caption := '';
  T24.Caption := '';
  T25.Caption := '';
  T26.Caption := '';
  T27.Caption := '';
  T28.Caption := '';
  T29.Caption := '';
  T30.Caption := '';

end;

procedure TFrmRelBolEscAlu.PageHeaderBand1BeforePrint(
  Sender: TQRCustomBand; var PrintBand: Boolean);
begin
  inherited;
  QRLabel1000.Left := QRSysData2.Left - 12;
  qrlTempleData.Left := QRLabel1000.Left - 30;
  qrlTempleHora.Left := QRSysData2.Left - 30;
  
end;

end.
