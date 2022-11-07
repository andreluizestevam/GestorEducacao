unit QRPDFFilter;

interface
  uses Windows, Messages, SysUtils, Classes, Graphics, Controls, QRPrntr,
  QuickRpt, Db, StdCtrls, QRCtrls, QR3Const, Printers, forms, PDF;

type

  TPDFDocumentParam = record
    AutoLaunch: Boolean;
    Compression: TCompressionType;
    DocumentInfo_Author: string;
    DocumentInfo_Creator: string;
    DocumentInfo_Keywords: string;
    DocumentInfo_Subject: string;
    DocumentInfo_Title: string;
    PageLayout: TPageLayout;
    PageMode: TPageMode;
    ProtectionEnabled: Boolean;
    ProtectionOptions: TPDFCtiptoOptions;
    OwnerPassword: string;
    UserPassword: string;
    JPEGQuality: Integer;
  end;

  TQRPDFExportFilter = class(TQRExportFilter)
  protected
    function GetDescription : string; override;
    function GetExtension : string; override;
    function GetFilterName : string; override;
    function GetVendorName : string; override;
  public
    procedure Finish; override;
    procedure NewPage; override;
    procedure TextOut(X,Y : extended; Font : TFont; BGColor : TColor; Alignment : TAlignment; Text : string); override;
  end;

  TQRPDFFilter = class(TComponent)
  private
    procedure SetAutoLaunch(Value: Boolean);
    function GetAutoLaunch: Boolean;
    procedure SetCompression(Value: TCompressionType);
    function GetCompression: TCompressionType;
    procedure SetDocumentInfo_Autor(Value: string);
    function GetDocumentInfo_Autor: string;
    procedure SetDocumentInfo_Creator(Value: string);
    function GetDocumentInfo_Creator: string;
    procedure SetDocumentInfo_Keywords(Value: string);
    function GetDocumentInfo_Keywords: string;
    procedure SetDocumentInfo_Subject(Value: string);
    function GetDocumentInfo_Subject: string;
    procedure SetDocumentInfo_Title(Value: string);
    function GetDocumentInfo_Title: string;
    procedure SetPageLayout(Value: TPageLayout);
    function GetPageLayout: TPageLayout;
    procedure SetPageMode(Value: TPageMode);
    function GetPageMode: TPageMode;
    procedure SetProtectionEnabled(Value: Boolean);
    function GetProtectionEnabled: Boolean;
    procedure SetProtectionOptions(Value: TPDFCtiptoOptions);
    function GetProtectionOptions: TPDFCtiptoOptions;
    procedure SetOwnerPassword(Value: string);
    function GetOwnerPassword: string;
    procedure SetUserPassword(Value: string);
    function GetUserPassword: string;
    procedure SetJPEGQuality(Value: Integer);
    function GetJPEGQuality: Integer;
  public
    constructor Create(AOwner : TComponent); override;
    destructor Destroy; override;
  published
    property AutoLaunch: Boolean read GetAutoLaunch Write SetAutoLaunch;
    property Compression: TCompressionType read GetCompression Write SetCompression;
    property DocumentInfo_Author: string read GetDocumentInfo_Autor Write SetDocumentInfo_Autor;
    property DocumentInfo_Creator: string read GetDocumentInfo_Creator Write SetDocumentInfo_Creator;
    property DocumentInfo_Keywords: string read GetDocumentInfo_Keywords Write SetDocumentInfo_Keywords;
    property DocumentInfo_Subject: string read GetDocumentInfo_Subject Write SetDocumentInfo_Subject;
    property DocumentInfo_Title: string read GetDocumentInfo_Title Write SetDocumentInfo_Title;
    property PageLayout: TPageLayout read GetPageLayout Write SetPageLayout;
    property PageMode: TPageMode read GetPageMode Write SetPageMode;
    property ProtectionEnabled: Boolean read GetProtectionEnabled Write SetProtectionEnabled;
    property ProtectionOptions: TPDFCtiptoOptions read GetProtectionOptions Write SetProtectionOptions;
    property OwnerPassword: string read GetOwnerPassword Write SetOwnerPassword;
    property UserPassword: string read GetUserPassword Write SetUserPassword;
    property JPEGQuality: Integer read GetJPEGQuality Write SetJPEGQuality;
  end;

  procedure QRPDFExportPrinter(FileName: string; QRPrinter: TQRPrinter);
  procedure QRPDFExportReport(FileName: string; QRReport: TCustomQuickRep); overload;
  procedure QRPDFExportReport(FileName: string; QRReport: TQRCompositeReport); overload;

var
  PDFDocumentParam: TPDFDocumentParam;


implementation

procedure SetupPDFDocumentparam(PD: TPDFDocument);
begin
  PD.AutoLaunch := PDFDocumentParam.AutoLaunch;
  PD.Compression := PDFDocumentParam.Compression;
  PD.DocumentInfo.Author := PDFDocumentParam.DocumentInfo_Author;
  PD.DocumentInfo.Creator := PDFDocumentParam.DocumentInfo_Creator;
  PD.DocumentInfo.Keywords := PDFDocumentParam.DocumentInfo_Keywords;
  PD.DocumentInfo.Subject := PDFDocumentParam.DocumentInfo_Subject;
  PD.DocumentInfo.Title := PDFDocumentParam.DocumentInfo_Title;
  PD.PageLayout := PDFDocumentParam.PageLayout;
  PD.PageMode := PDFDocumentParam.PageMode;
  PD.ProtectionEnabled := PDFDocumentParam.ProtectionEnabled;
  PD.ProtectionOptions := PDFDocumentParam.ProtectionOptions;
  PD.OwnerPassword := PDFDocumentParam.OwnerPassword;
  PD.UserPassword := PDFDocumentParam.UserPassword;
  PD.JPEGQuality := PDFDocumentParam.JPEGQuality;
end;

procedure QRPDFExportPrinter(FileName: string; QRPrinter: TQRPrinter);
var
  i, Count: Integer;
  MF: TMetafile;
  DC: HDC;
  FPD: TPDFDocument;
begin
  if not Assigned(QRPrinter) then
    Exit;
  FPD := TPDFDocument.Create(nil);
  SetupPDFDocumentParam(FPD);
  try
    DC := GetDC(0);
    FPD.FileName := FileName;
    FPD.Resolution := GetDeviceCaps(DC, LOGPIXELSX);
    FPD.BeginDoc;
    Count := QRPrinter.AvailablePages{PageCount};
    for i := 1 to Count do begin
      if i > 1 then
        FPD.NewPage;
      MF := QRPrinter.GetPage(i);
      if Assigned(MF) then begin
        FPD.CurrentPage.Height := MF.Height;
        FPD.CurrentPage.Width := MF.Width;
        FPD.CurrentPage.PlayMetaFile(MF);
      end;
    end;
    FPD.EndDoc;
  finally
    FPD.Free;
  end;
end;

procedure QRPDFExportReport(FileName: string; QRReport: TCustomQuickRep);
begin
  QRReport.Prepare;
  QRPDFExportPrinter(FileName, QRReport.QRPrinter);
end;

function QRPDFSavePrinterToPDF(PD: TPDFDocument; QRPrinter: TQRPrinter): Boolean;
var
  i, Count: Integer;
  MF: TMetafile;
begin
  if not Assigned(QRPrinter) then begin
    Result := False;
    Exit;
  end;
  Result := True;
  Count := QRPrinter.AvailablePages{PageCount};
  for i := 1 to Count do begin
    if i > 1 then
      PD.NewPage;
    MF := QRPrinter.GetPage(i);
    if Assigned(MF) then begin
      PD.CurrentPage.Height := MF.Height;
      PD.CurrentPage.Width := MF.Width;
      PD.CurrentPage.PlayMetaFile(MF);
    end;
  end;
end;

procedure QRPDFExportReport(FileName: string; QRReport: TQRCompositeReport);
var i: Integer;
    FPD: TPDFDocument;
    DC: HDC;
    prz: Boolean;
    SavePrinter: TQRPrinter;
begin
  FPD := TPDFDocument.Create(nil);
  try
    SetupPDFDocumentparam(FPD);
    DC := GetDC(0);
    FPD.FileName := FileName;
    FPD.Resolution := GetDeviceCaps(DC, LOGPIXELSX);
    QRReport.Prepare;
    FPD.BeginDoc;
    prz := False;
    SavePrinter := nil;
    for i := 0 to QRReport.Reports.Count - 1 do
      if Assigned(TCustomQuickRep(QRReport.Reports[i]).QRPrinter) and
         (SavePrinter <> TCustomQuickRep(QRReport.Reports[i]).QRPrinter) then begin
        if prz then
          FPD.NewPage;
        SavePrinter := TCustomQuickRep(QRReport.Reports[i]).QRPrinter;
        prz := QRPDFSavePrinterToPDF(FPD, TCustomQuickRep(QRReport.Reports[i]).QRPrinter)
      end;
    FPD.EndDoc;
  finally
    FPD.Free;
  end;
end;


{ TQRPDFExportFilter }

function TQRPDFExportFilter.GetDescription : string;
begin
  Result := 'Adobe Acrobat Documents ';
end;

function TQRPDFExportFilter.GetExtension : string;
begin
  Result := 'pdf';
end;

function TQRPDFExportFilter.GetFilterName : string;
begin
  Result := 'Adobe Acrobat Documents';
end;

function TQRPDFExportFilter.GetVendorName : string;
begin
  Result := 'llionsoft www.llion.net'
end;

procedure TQRPDFExportFilter.TextOut(X, Y : extended; Font : TFont; BGColor : TColor; Alignment : TAlignment; Text : string);
var Al: Cardinal;
begin
  if not Assigned(OriginalQRPrinter) then begin
    TCustomQuickRep(Owner).QRPrinter.Canvas.Font.Assign(Font);
    SetBkColor(TCustomQuickRep(Owner).QRPrinter.Canvas.Handle, BGColor);
    case Alignment of
      taRightJustify: Al := TA_RIGHT;
      taCenter: Al := TA_CENTER;
      else
        Al := TA_LEFT;
    end;
    SetTextAlign(TCustomQuickRep(Owner).QRPrinter.Canvas.Handle, Al);
    TCustomQuickRep(Owner).QRPrinter.Canvas.TextOut(TCustomQuickRep(Owner).QRPrinter.XPos(X), TCustomQuickRep(Owner).QRPrinter.YPos(Y), Text);
  end;
end;

procedure TQRPDFExportFilter.Finish;
begin
  if Assigned(OriginalQRPrinter) then
    QRPDFExportPrinter(Filename, OriginalQRPrinter)
  else begin
    TCustomQuickRep(Owner).QRPrinter.NewPage;
    QRPDFExportPrinter(Filename, TCustomQuickRep(Owner).QRPrinter);
  end;
end;

procedure TQRPDFExportFilter.NewPage;
begin
  if not Assigned(OriginalQRPrinter) then
    if TCustomQuickRep(Owner).QRPrinter.PageNumber > 0 then
      TCustomQuickRep(Owner).QRPrinter.NewPage;
end;


{ TQRPDFFilter }

constructor TQRPDFFilter.Create(AOwner : TComponent);
begin
  inherited Create(AOwner);
  QRExportFilterLibrary.AddFilter(TQRPDFExportFilter);
end;

destructor TQRPDFFilter.Destroy;
begin
  QRExportFilterLibrary.RemoveFilter(TQRPDFExportFilter);
  inherited Destroy;
end;

procedure TQRPDFFilter.SetAutoLaunch(Value: Boolean);
begin
  PDFDocumentParam.AutoLaunch := Value;
end;

function TQRPDFFilter.GetAutoLaunch: Boolean;
begin
  Result := PDFDocumentParam.AutoLaunch;
end;

procedure TQRPDFFilter.SetCompression(Value: TCompressionType);
begin
  PDFDocumentParam.Compression := Value;
end;

function TQRPDFFilter.GetCompression: TCompressionType;
begin
  Result := PDFDocumentParam.Compression;
end;

procedure TQRPDFFilter.SetDocumentInfo_Autor(Value: string);
begin
  PDFDocumentParam.DocumentInfo_Author := Value;
end;

function TQRPDFFilter.GetDocumentInfo_Autor: string;
begin
  Result := PDFDocumentParam.DocumentInfo_Author;
end;

procedure TQRPDFFilter.SetDocumentInfo_Creator(Value: string);
begin
  PDFDocumentParam.DocumentInfo_Creator := Value;
end;

function TQRPDFFilter.GetDocumentInfo_Creator: string;
begin
  Result := PDFDocumentParam.DocumentInfo_Creator;
end;

procedure TQRPDFFilter.SetDocumentInfo_Keywords(Value: string);
begin
  PDFDocumentParam.DocumentInfo_Keywords := Value;
end;

function TQRPDFFilter.GetDocumentInfo_Keywords: string;
begin
  Result := PDFDocumentParam.DocumentInfo_Keywords;
end;

procedure TQRPDFFilter.SetDocumentInfo_Subject(Value: string);
begin
  PDFDocumentParam.DocumentInfo_Subject := Value;
end;

function TQRPDFFilter.GetDocumentInfo_Subject: string;
begin
  Result := PDFDocumentParam.DocumentInfo_Subject;
end;

procedure TQRPDFFilter.SetDocumentInfo_Title(Value: string);
begin
  PDFDocumentParam.DocumentInfo_Title := Value;
end;

function TQRPDFFilter.GetDocumentInfo_Title: string;
begin
  Result := PDFDocumentParam.DocumentInfo_Title;
end;

procedure TQRPDFFilter.SetPageLayout(Value: TPageLayout);
begin
  PDFDocumentParam.PageLayout := Value;
end;

function TQRPDFFilter.GetPageLayout: TPageLayout;
begin
  Result := PDFDocumentParam.PageLayout;
end;

procedure TQRPDFFilter.SetPageMode(Value: TPageMode);
begin
  PDFDocumentParam.PageMode := Value;
end;

function TQRPDFFilter.GetPageMode: TPageMode;
begin
  Result := PDFDocumentParam.PageMode;
end;

procedure TQRPDFFilter.SetProtectionEnabled(Value: Boolean);
begin
  PDFDocumentParam.ProtectionEnabled := Value;
end;

function TQRPDFFilter.GetProtectionEnabled: Boolean;
begin
  Result := PDFDocumentParam.ProtectionEnabled;
end;

procedure TQRPDFFilter.SetProtectionOptions(Value: TPDFCtiptoOptions);
begin
  PDFDocumentParam.ProtectionOptions := Value;
end;

function TQRPDFFilter.GetProtectionOptions: TPDFCtiptoOptions;
begin
  Result := PDFDocumentParam.ProtectionOptions;
end;

procedure TQRPDFFilter.SetOwnerPassword(Value: string);
begin
  PDFDocumentParam.OwnerPassword := Value;
end;

function TQRPDFFilter.GetOwnerPassword: string;
begin
  Result := PDFDocumentParam.OwnerPassword;
end;

procedure TQRPDFFilter.SetUserPassword(Value: string);
begin
  PDFDocumentParam.UserPassword := Value;
end;

function TQRPDFFilter.GetUserPassword: string;
begin
  Result := PDFDocumentParam.UserPassword;
end;

procedure TQRPDFFilter.SetJPEGQuality(Value: Integer);
begin
  PDFDocumentParam.JPEGQuality := Value;
end;

function TQRPDFFilter.GetJPEGQuality: Integer;
begin
  Result := PDFDocumentParam.JPEGQuality;
end;

initialization
  PDFDocumentParam.AutoLaunch := False;
  PDFDocumentParam.Compression := ctNone;
  PDFDocumentParam.DocumentInfo_Author := 'Windows 9x/NT/2000/XP User';
  PDFDocumentParam.DocumentInfo_Creator := 'llPDFLib program';
  PDFDocumentParam.DocumentInfo_Keywords := 'llPDFLib';
  PDFDocumentParam.DocumentInfo_Subject := 'None';
  PDFDocumentParam.DocumentInfo_Title := 'No Title';
  PDFDocumentParam.PageLayout := plSinglePage;
  PDFDocumentParam.PageMode := pmUseNone;
  PDFDocumentParam.ProtectionEnabled := False;
  PDFDocumentParam.ProtectionOptions := [];
  PDFDocumentParam.OwnerPassword := '';
  PDFDocumentParam.UserPassword := '';
  PDFDocumentParam.JPEGQuality := 80;
end.

