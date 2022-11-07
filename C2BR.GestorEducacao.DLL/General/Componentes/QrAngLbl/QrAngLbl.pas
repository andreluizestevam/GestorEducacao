unit QrAngLbl;
{$I QRANGLBL.INC}

interface

uses
  SysUtils, WinTypes, WinProcs, Messages, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, DB, DBTables, QrAConst, QuickRpt, QrCtrls;

type
  TAnchorStyle = (asNone, asTextLeft, asTextCenter, asTextRight);
  TTrueTypeAlert = (ttaNone, ttaMessage, ttaAbort, ttaAbortMessage);

  TAngledValues = record
    fntWidth,
    fntHeight,
    txtWidth,
    txtHeight,
    gapTxtWidth,
    gapTxtHeight,
    totWidth,
    totHeight,
    posLeft,
    posTop,
    posX,
    posY: Integer
  end;

  TQRAngledCustom = class(TQRPrintable)
  private
    FAnchorStyle: TAnchorStyle;
    FAngle: Integer;
    FAutoSize: Boolean;
    FCaption: TCaption;
    FOnPrintEvent: TQRLabelOnPrintEvent;
    FTTFonts: TStringList;
    FTrueTypeAlert: TTrueTypeAlert;
    FValues: TAngledValues;
    procedure AdjustSizes;
    procedure CMFontChanged(var Message: TMessage); message CM_FONTCHANGED;
    procedure CMTextChanged(var Message: TMessage); message CM_TEXTCHANGED;

  protected
    procedure BuildTTFontsList;
    procedure CalculateAngledValues(pCanvas: TCanvas; pCaption: TCaption; pZoom: Integer);
    function GetCaption: TCaption;
    function IsTrueTypeFont(pFont: TFont): Boolean;
    procedure Loaded; override;
    procedure Paint; override;
    procedure Print(X, Y: Integer); override;
    procedure SetAnchorStyle(pValue: TAnchorStyle);
    procedure SetAngle(pValue: Integer);
    procedure SetAutoSize(pValue: Boolean);
    procedure SetCaption(pValue: TCaption);
    procedure SetName(const pValue: TComponentName); override;
    procedure SetTrueTypeAlert(pValue: TTrueTypeAlert);
    procedure CreateWnd; override;

  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    property AnchorStyle: TAnchorStyle read FAnchorStyle write SetAnchorStyle;
    property Angle: Integer read FAngle write SetAngle default 45;
    property AutoSize: Boolean read  FAutoSize write SetAutoSize default True;
    property Caption: TCaption read GetCaption write SetCaption stored True;
    property OnPrint: TQRLabelOnPrintEvent read FOnPrintEvent write FOnPrintEvent;
    property TrueTypeAlert: TTrueTypeAlert read FTrueTypeAlert write SetTrueTypeAlert;
  end;

  TQRAngledLabel = class(TQRAngledCustom)
  published
    { Inherited properties republished }
    property AnchorStyle;
    property Angle;
    property AutoSize;
    property Caption;
    property Color;
    property Font;
    property OnPrint;
    property ParentFont;
    property Transparent;
    property TrueTypeAlert;
  end;

  TQRAngledDBText = class(TQRAngledCustom)
  private
    FDataSourceName: string[30];
    FDataSet: TDataSet;
    FDataField: string;
    FField: TField;
    FFieldNumber: Integer;
    FFieldOK: Boolean;
    FMask: string;
    procedure SetDataField(pValue: string);
    procedure SetDataSet(pValue: TDataSet);
    procedure SetMask(pValue: string);
  protected
    procedure DefineProperties(pFiler: TFiler); override;
    procedure Loaded; override;
    procedure Prepare; override;
    procedure ReadValues(pReader: TReader); virtual;
    procedure Unprepare; override;
    procedure WriteValues(pWriter: TWriter); virtual;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
    procedure Print(X, Y: Integer); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  published
    property DataField: string read FDataField write SetDataField;
    property DataSet: TDataSet read FDataSet write SetDataSet;
    property Mask: string read FMask write SetMask;

    { Inherited properties republished }
    property AnchorStyle;
    property Angle;
    property AutoSize;
    property Color;
    property Font;
    property OnPrint;
    property ParentFont;
    property Transparent;
    property TrueTypeAlert;
  end;

procedure Register;

implementation

procedure Register;
begin
  RegisterComponents('QReport',[TQRAngledLabel,TQRAngledDBText]);
end;

{ Common routines ********************************************************************** }

function DegToRad(pDegrees: Real): Real;
begin
  Result := (pDegrees * PI / 180);
end;

function EnumTTFontsProc(var pLogFont: TLogFont; var pTextMetric: TTextMetric;
 pFontType: Integer; pData: Pointer): Integer; export; StdCall;
begin
  if (pFontType and TRUETYPE_FONTTYPE = TRUETYPE_FONTTYPE) then
    TStringList(pData^).Add(StrPas(pLogFont.lfFaceName)) ;
  Result := 1;
end;

procedure CreateAngledFont(pCanvas: TCanvas; pAngle: Integer);
var
  FntLogRec: TLogFont { Storage area for font information } ;
begin
  { Get the current font information. We only want to modify the angle }
  GetObject(pCanvas.Font.Handle, SizeOf(FntLogRec), Addr(FntLogRec));

  { Modify the angle. "The angle, in tenths of a degrees, between the base
    line of a character and the x-axis." (Windows API Help file.) }
  FntLogRec.lfEscapement := (pAngle * 10);
  FntLogRec.lfOutPrecision := OUT_TT_ONLY_PRECIS;  { Request TrueType precision }

  { Delphi will handle the deallocation of the old font handle }
  pCanvas.Font.Handle := CreateFontIndirect(FntLogRec);
end;

constructor TQRAngledCustom.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FAutoSize := True;
  Transparent := False;
  FTTFonts := TStringList.Create;
  SetTrueTypeAlert(ttaAbort);
  FAnchorStyle := asNone;
  SetAngle(45) { Default angle } ;
end;

destructor TQRAngledCustom.Destroy;
begin
  FTTFonts.Free;
  inherited Destroy;
end;

procedure TQRAngledCustom.AdjustSizes;
begin
   if HandleAllocated then
      if (Parent <> nil) and (Parent.HandleAllocated) then
      begin
           CalculateAngledValues(Canvas, Caption, Zoom);
           if AutoSize then
              SetBounds(FValues.posLeft, FValues.posTop, FValues.totWidth, FValues.totHeight);
      end;
end;

procedure TQRAngledCustom.BuildTTFontsList;
var
  DC: HDC;
  EnumProc: TFarProc;
begin
  if not HandleAllocated then
     if (Parent <> nil) and (not Parent.HandleAllocated) then
        Exit;

  DC := GetDC(0);
  FTTFonts.Clear;
  try
    EnumProc := MakeProcInstance(@EnumTTFontsProc, HInstance);
    try
      EnumFonts(DC, nil, EnumProc, @FTTFonts);
    finally
      FreeProcInstance(EnumProc);
    end;
  finally
    ReleaseDC(0, DC);
  end;
end;

procedure TQRAngledCustom.CalculateAngledValues(pCanvas: TCanvas; pCaption: TCaption;
 pZoom: Integer);
var
  angB: Real;
  nCenterX, nCenterY: Integer;
begin
  pCanvas.Font.Assign(Font);
  if pCanvas.Font.Size <> Round(Font.Size * pZoom / 100) then
     pCanvas.Font.Size := Round(Font.Size * pZoom / 100);

  CreateAngledFont(pCanvas, FAngle);

  { Calculate intermediate values }
  FValues.fntWidth := pCanvas.TextWidth(pCaption);
  FValues.fntHeight := pCanvas.TextHeight(pCaption);
  case FAngle of
    0..89   : angB := DegToRad(90 - FAngle);
    90..179 : angB := DegToRad(FAngle - 90);
    180..269: angB := DegToRad(270 - FAngle);
  else { 270..359 }
    angB := DegToRad(FAngle - 270)
  end;
  FValues.txtWidth := Round(sin(angB) * FValues.fntWidth);
  FValues.gapTxtWidth := Round(cos(angB) * FValues.fntHeight);
  FValues.txtHeight := Round(cos(angB) * FValues.fntWidth);
  FValues.gapTxtHeight := Round(sin(angB) * FValues.fntHeight);

  { Calculate new sizes of component }
  FValues.totWidth := (FValues.txtWidth + FValues.gapTxtWidth);
  FValues.totHeight := (FValues.txtHeight + FValues.gapTxtHeight);

  { Calculate Anchor positon of component }
  if FAnchorStyle in [asNone] then
  begin
    FValues.posLeft := Left;
    FValues.posTop := Top;
  end
  else
    if FAnchorStyle in [asTextLeft] then
    begin
      { Calculate Left position }
      case FAngle of 0..89, 270..359:
        FValues.posLeft := Left
      else { 90..179, 180..269 }
        FValues.posLeft := (Left + Width - FValues.totWidth)
      end;
      { Calculate Top position }
      case FAngle of 180..269, 270..359:
        FValues.posTop := Top
      else { 0..89, 90..179 }
        FValues.posTop := (Top + Height - FValues.totHeight)
      end;
    end
    else
      if FAnchorStyle in [asTextRight] then
      begin
        { Calculate Left position }
        case FAngle of 90..179, 180..269:
          FValues.posLeft := Left
        else { 0..89, 270..359 }
          FValues.posLeft := (Left + Width - FValues.totWidth)
        end;
        { Calculate Top position }
        case FAngle of 0..89, 90..179:
          FValues.posTop := Top
        else { 180..269, 270..359 }
          FValues.posTop := (Top + Height - FValues.totHeight)
        end;
      end
      else { asTextCenter }
      begin
        FValues.posLeft := (Left + Round((Width - FValues.totWidth) / 2));
        FValues.posTop := (Top + Round((Height - FValues.totHeight) / 2));
      end
  ;

  { Calculate draw position of text }
  case FAngle of
    0..89:
    begin
      FValues.posX := 0;
      FValues.posY := FValues.txtHeight
    end;
    90..179:
    begin
      FValues.posX := FValues.txtWidth;
      FValues.posY := FValues.totHeight
    end;
    180..269:
    begin
      FValues.posX := FValues.totWidth;
      FValues.posY := FValues.gapTxtHeight
    end;
  else { 270..359 }
    begin
      FValues.posX := FValues.gapTxtWidth;
      FValues.posY := 0
    end;
  end;

  { Calculate draw position of text inside area of component }
  if (FAnchorStyle in [asTextLeft, asTextRight, asTextCenter]) and not AutoSize then
    if FAnchorStyle in [asTextLeft] then
      case FAngle of
        0..89:
        begin
          FValues.posX := 0;
          FValues.posY := (Height - FValues.gapTxtHeight);
        end;
        90..179:
        begin
          FValues.posX := (Width - FValues.gapTxtWidth);
          FValues.posY := Height;
        end;
        180..279:
        begin
          FValues.posX := Width;
          FValues.posY := FValues.gapTxtHeight;
        end;
      else { 280..359 }
        begin
          FValues.posX := FValues.gapTxtWidth;
          FValues.posY := 0;
        end;
      end
    else
      if FAnchorStyle in [asTextRight] then
        case FAngle of
          0..89:
          begin
            FValues.posX := (Width - FValues.txtWidth - FValues.gapTxtWidth);
            FValues.posY := FValues.txtHeight;
          end;
          90..179:
          begin
            FValues.posX := FValues.txtWidth;
            FValues.posY := (FValues.txtHeight + FValues.gapTxtHeight);
          end;
          180..279:
          begin
            FValues.posX := (FValues.txtWidth + FValues.gapTxtWidth);
            FValues.posY := (Height - FValues.txtHeight);
          end;
        else { 280..359 }
          begin
            FValues.posX := (Width - FValues.txtWidth);
            FValues.posY := (Height - FValues.txtHeight - FValues.gapTxtHeight);
          end;
        end
      else { asTextCenter }
        begin
          nCenterX := Round((Width - FValues.txtWidth - FValues.gapTxtHeight) / 2);
          nCenterY := Round((Height - FValues.txtHeight - FValues.gapTxtHeight) / 2);
          case FAngle of
            0..89:
            begin
              FValues.posX := nCenterX;
              FValues.posY := (nCenterY + FValues.txtHeight);
            end;
            90..179:
            begin
              FValues.posX := (nCenterX + FValues.txtWidth);
              FValues.posY := (nCenterY + FValues.txtHeight + FValues.gapTxtHeight);
            end;
            180..279:
            begin
              FValues.posX := (nCenterX + FValues.txtWidth + FValues.gapTxtWidth);
              FValues.posY := (nCenterY + FValues.gapTxtHeight);
            end;
          else { 280..359 }
            begin
              FValues.posX := (nCenterX + FValues.gapTxtWidth);
              FValues.posY := nCenterY;
            end;
          end
        end
  ;
end;

procedure TQRAngledCustom.CMFontChanged(var Message: TMessage);
begin
  inherited;
  { Prevent non TrueType Fonts }
  if not (FTrueTypeAlert in [ttaNone]) then
    if (not IsTrueTypeFont(Font)) then
      if (FTrueTypeAlert in [ttaMessage]) then
        ShowMessage(LoadStr(STTAlertMessage))
      else
      begin
        { TControl class haven't Font property published!!! }

        if ((Parent is TQRCustomBand) and IsTrueTypeFont((Parent as TQRCustomBand).Font)) or
         ((Parent is TQuickRep) and IsTrueTypeFont((Parent as TQuickRep).Font)) then
          ParentFont := True { Assign parent font }
        else Font.Name := 'Arial';

        if (FTrueTypeAlert in [ttaAbortMessage]) then
          ShowMessage(FmtLoadStr(STTAbortMessage, [Font.Name]));
      end;
  AdjustSizes;
end;

procedure TQRAngledCustom.CMTextChanged(var Message: TMessage);
begin
  AdjustSizes;
  Invalidate;
end;

procedure TQRAngledCustom.CreateWnd;
begin
  inherited CreateWnd;
  BuildTTFontsList;
  AdjustSizes;
end;

function TQRAngledCustom.GetCaption: TCaption;
begin
  Result := FCaption;
end;

function TQRAngledCustom.IsTrueTypeFont(pFont: TFont): Boolean;
begin
  Result := (FTTFonts.IndexOf(pFont.Name) <> (-1));
end;

procedure TQRAngledCustom.Loaded;
begin
  inherited Loaded;
  if not HandleAllocated then
    HandleNeeded;
end;

procedure TQRAngledCustom.Paint;
var
  aRect: TRect;
begin
  CalculateAngledValues(Canvas, Caption, {$IFDEF QR2X} Zoom {$ELSE} 100 {$ENDIF});
  inherited Paint;

  with Canvas do
  begin
    aRect := Rect(0, 0, Width, Height);

    if not Transparent then
    begin
      Brush.Color := Color;
      Brush.Style := bsSolid;
      FillRect(aRect);
    end;

    ExtTextOut(Handle, FValues.posX, FValues.posY, ETO_CLIPPED, @aRect,
     @FCaption[1], Length(FCaption), nil);
  end;
end;

procedure TQRAngledCustom.Print(X, Y: Integer);
var
  aCanvas: TCanvas;
  OldCaption, NewCaption: string;
  aRect: TRect;
  PixFactor: Extended;
  recLf, recTp, recRt, recBt, txtXP, txtYP: Integer;
begin
  OldCaption := Caption;
  NewCaption := Caption;
  if Assigned(FOnPrintEvent) and not (csDesigning in ComponentState) then
  begin
    FOnPrintEvent(Self, NewCaption);
    if NewCaption <> FCaption then
    begin
      OldCaption := Caption;
      SetCaption(NewCaption);
    end;
  end;

  PixFactor := (Height / Size.Height);
  aCanvas := QRPrinter.Canvas;
  aCanvas.Font.Assign(Font);
  CreateAngledFont(aCanvas, Angle);

  with aCanvas do
  begin
    with ParentReport.QRPrinter do
    begin
      recLf := XPos(X + Size.Left);
      recTp := YPos(Y + Size.Top);
      recRt := XPos(X + Size.Left + Size.Width);
      recBt := YPos(Y + Size.Top + Size.Height);
      txtXP := XPos(X + Size.Left + Round(FValues.posX / PixFactor));
      txtYP := YPos(Y + Size.Top + Round(FValues.posY / PixFactor));
    end;
    aRect := Rect(recLf, recTp, recRt, recBt);
    if not Transparent then
    begin
      Brush.Color := Color;
      Brush.Style := bsSolid;
      FillRect(aRect);
    end;
    Brush.Style := bsClear;

    SetTextAlign(Handle, TA_LEFT + TA_TOP + TA_NOUPDATECP);
    ExtTextOut(Handle, txtXP, txtYP, ETO_CLIPPED, @aRect, @NewCaption[1],
    Length(NewCaption), nil);
  end;

  inherited Print(X, Y);

  if OldCaption <> NewCaption then
    SetCaption(OldCaption);
end;

procedure TQRAngledCustom.SetAnchorStyle(pValue: TAnchorStyle);
begin
  if FAnchorStyle <> pValue then
  begin
    FAnchorStyle := pValue;
    AdjustSizes;
    Invalidate { Redraw } ;
  end;
end;

procedure TQRAngledCustom.SetAngle(pValue: Integer);
begin
  if FAngle <> (pValue mod 360) then
  begin
    FAngle := (pValue mod 360);
    AdjustSizes;
    Invalidate { Redraw } ;
  end;
end;

procedure TQRAngledCustom.SetAutoSize(pValue: Boolean);
begin
  if FAutoSize <> pValue then FAutoSize := pValue;
  if FAutoSize then AdjustSizes;
end;

procedure TQRAngledCustom.SetCaption(pValue: TCaption);
begin
  if FCaption <> pValue then
  begin
    FCaption := pValue;
    Perform(CM_TEXTCHANGED, 0, 0);
  end;
end;

procedure TQRAngledCustom.SetName(const pValue: TComponentName);
var
  OldName: TComponentName;
begin
  if Name <> pValue then
  begin
    OldName := Name;
    inherited SetName(pValue);
    if ((Caption = '') or (Caption = OldName)) then
      SetCaption(pValue);
  end;
end;

procedure TQRAngledCustom.SetTrueTypeAlert(pValue: TTrueTypeAlert);
var
  Message: TMessage;
begin
  if FTrueTypeAlert <> pValue then
  begin
    FTrueTypeAlert := pValue;
    CMFontChanged(Message);
  end;
end;

constructor TQRAngledDBText.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  FDataSourceName := '';
end;

destructor TQRAngledDBText.Destroy;
begin
  inherited Destroy;
end;

procedure TQRAngledDBText.DefineProperties(pFiler: TFiler);
begin
  pFiler.DefineProperty('DataSource', ReadValues, WriteValues, False);
  inherited DefineProperties(pFiler);
end;

procedure TQRAngledDBText.Loaded;
var
  aComponent: TComponent;
begin
  inherited Loaded;
  if FDataSourceName <> '' then
  begin
    aComponent := Owner.FindComponent(FDataSourceName);
    if (aComponent <> nil) and (aComponent is TDataSource) then
      DataSet:=TDataSource(aComponent).DataSet
    ;
  end;
end;

procedure TQRAngledDBText.Prepare;
begin
  inherited Prepare;
  if Assigned(FDataSet) then
  begin
    FField := FDataSet.FindField(FDataField);
    if FField <> nil then
    begin
      FFieldNumber := FField.Index;
      FFieldOK := True;
    end;
  end
  else
  begin
    FField := nil;
    FFieldOK := False;
  end;
end;

procedure TQRAngledDBText.Print(X, Y: Integer);
begin
  if FFieldOK then
  begin
    if FDataSet.DefaultFields then
      FField := FDataSet.Fields[FFieldNumber]
    ;
  end
  else
    FField := nil
  ;

  if Assigned(FField) then
    try
      if (Mask = '') or (FField is TStringField) then
        Caption := FField.DisplayText
      else
        if (FField is TNumericField) then
           Caption := FormatFloat(Mask, (FField as TNumericField).AsFloat)
        else
          if (FField is TDateTimeField) then
            Caption := FormatDateTime(Mask, (FField as TDateTimeField).AsDateTime)
      ;
    except
      Caption := '';
    end
  else
    Caption := ''
  ;

  inherited Print(X, Y);
end;

procedure TQRAngledDBText.ReadValues(pReader: TReader);
begin
  FDataSourceName := pReader.ReadIdent;
end;

procedure TQRAngledDBText.SetDataField(pValue: string);
begin
  FDataField := pValue;
  Caption := pValue;
end;

procedure TQRAngledDBText.SetDataSet(pValue: TDataSet);
begin
  FDataSet := pValue;
  if pValue <> nil then
    pValue.FreeNotification(Self);
end;

procedure TQRAngledDBText.SetMask(pValue: string);
begin
  if FMask <> pValue then
    FMask := pValue
  ;
end;

procedure TQRAngledDBText.Unprepare;
begin
  FField := nil;
  inherited Unprepare;
  if FDataField <> '' then
    SetDataField(FDataField) { Reset caption }
  else
    SetDataField(Name)
  ;
end;

procedure TQRAngledDBText.WriteValues(pWriter: TWriter);
begin
{ nothing }
end;

procedure TQRAngledDBText.Notification(AComponent: TComponent; Operation: TOperation);
begin
  inherited Notification(AComponent, Operation);
  if Operation = opRemove then
    if AComponent = FDataSet then
      FDataSet := nil;
end;

end.


