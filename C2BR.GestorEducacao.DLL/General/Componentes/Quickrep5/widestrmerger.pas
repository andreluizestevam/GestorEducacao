unit widestrmerger;

interface
uses classes, sysutils, widelist;

type

  TQRAnsiMerger = class
  private
    FOrgLines : TWideStringList;
    FStrippedLines : TWideStringList;
    FMergedLines : TWideStringList;
    FMerged : boolean;
    FPrepared : boolean;
    Expressions : TList;
    FDataSets : TList;
  protected
    function GetOrgLines : TWideStringList;
    function GetMergedLines : TWideStringList;
    procedure SetOrgLines(Value : TWideStringList);
  public
    constructor Create;
    destructor Destroy; override;
    procedure Prepare;
    procedure Merge;
    procedure UnPrepare;
    property Lines : TWideStringList read GetOrgLines write SetOrgLines;
    property MergedLines : TWideStringList read GetMergedLines;
    property Merged : boolean read FMerged;
    property Prepared : boolean read FPrepared;
    property DataSets : TList read FDataSets write FDataSets;
  end;
  
implementation
uses qrexpr;

{TQRAnsiMerger}
constructor TQRAnsiMerger.Create;
begin
  FOrgLines := TWideStringList.Create;
  FMergedLines := nil;
  FMerged := false;
  FPrepared := false;
  Expressions := nil;
  FDataSets := TList.Create;
end;

destructor TQRAnsiMerger.Destroy;
begin
  if Prepared then UnPrepare;
  if FOrgLines <> nil then
    FOrgLines.Free;
  if FDataSets <> nil then
    FDataSets.Free;
  inherited Destroy;
end;

function TQRAnsiMerger.GetOrgLines : TWideStringList;
begin
  Result := FOrgLines;
end;

function TQRAnsiMerger.GetMergedLines : TWideStringList;
begin
  if Merged then
    Result := FMergedLines
  else
    Result := nil;
end;

procedure TQRAnsiMerger.SetOrgLines(Value : TWideStringList);
begin
  if FOrgLines <> nil then
    FOrgLines.Free;
  FOrgLines := Value;
end;

type
  TMemoEvaluator = class(TQREvaluator)
  public
    Line : integer;
    Position : integer;
  end;

procedure TQRAnsiMerger.Prepare;
var
  I, start, stop, Len : integer;
  Expr : String;
  aLine : string;
  aEvaluator : TMemoEvaluator;
begin
  if Prepared then UnPrepare;
  Expressions := TList.Create;
  FMergedLines := TWideStringList.Create;
  if Lines.Count > 0 then
  begin
    FStrippedLines := TWideStringList.Create;
    try
      for I := 0 to Lines.Count - 1 do
      begin
        aLine := FOrgLines[I];
        Start := AnsiPos('{', aLine);
        while Start > 0 do
        begin
          stop := AnsiPos('}', aLine);
          Len := Stop - Start - 1;
          if Len > 0 then
          begin
            Expr := copy(aLine, start + 1, Len);
            Delete(aLine, Start, Len + 2);
            aEvaluator := TMemoEvaluator.Create;
            aEvaluator.DataSets := DataSets;
            aEvaluator.Prepare(Expr);
            aEvaluator.Line := I;
            aEvaluator.Position := Start;
            Expressions.Add(aEvaluator);
          end;
          Start := AnsiPos('{', aLine);
        end;
        FStrippedLines.Add(aLine);
      end;
    finally
      FPrepared := true;
    end;
  end;
end;

procedure TQRAnsiMerger.UnPrepare;
var
  I : integer;
begin
  if Prepared then
  try
    for I := 0 to Expressions.Count - 1 do
      TMemoEvaluator(Expressions[I]).Free;
    FStrippedLines.Free;
  finally
    FPrepared := false;
    FMerged := false;
  end;
  FMergedLines.Free;
  Expressions.Free;
end;

procedure TQRAnsiMerger.Merge;
var
  I : integer;
  aResult : TQREvResult;
  Replacement : string;
  aLine : string;
begin
  if (Expressions.Count > 0) then
  begin
    if Merged then
      FMergedLines.Clear;
    FMergedLines.Assign(FStrippedLines);
    for I := Expressions.Count - 1 downto 0 do
    begin
      with TMemoEvaluator(Expressions[I]) do
      begin
        aResult := Value;
        case aResult.Kind of
          resInt : Replacement := IntToStr(aResult.IntResult);
          resDouble : Replacement := FloatToStr(aResult.dblResult);
          resString : Replacement := aResult.StrResult;
          resError : Replacement := '';
        end;
        aLine := FMergedLines[Line];
        Insert(Replacement, aLine, Position);
        FMergedLines[Line] := aLine;
      end;
    end
  end else
    if not Merged then
      FMergedLines.Assign(FOrgLines);
  FMerged := true;
end;

end.
