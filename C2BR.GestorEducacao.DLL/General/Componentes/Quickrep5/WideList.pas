unit WideList;
{---------------------------------------------------------------------

   TWideStrings, TWideStringlist classes

----------------------------------------------------------------------}
interface

uses
  Windows, Classes, RTLConsts;

const
  // definitions of often used characters:
  // Note: Use them only for tests of a certain character not to determine character classes like
  //       white spaces as in Unicode are often many code points defined being in a certain class.
  //       Hence your best option is to use the various UnicodeIs* functions.

  WideNull = WideChar(#0);
  Tabulator = WideChar(#9);
  Space = WideChar(#32);

  // logical line breaks
  LF = WideChar($A);
  LineFeed = WideChar($A);
  VerticalTab = WideChar($B);
  FormFeed = WideChar($C);
  CR = WideChar($D);
  CarriageReturn = WideChar($D);
  CRLF: WideString = #$D#$A;
  LineSeparator = WideChar($2028);
  ParagraphSeparator = WideChar($2029);

  // byte order marks for strings
  // Unicode text files should contain $FFFE as first character to identify such a file clearly. Depending on the system
  // where the file was created on this appears either in big endian or little endian style.
  BOM_LSB_FIRST = WideChar($FEFF); // this is how the BOM appears on x86 systems when written by a x86 system
  BOM_MSB_FIRST = WideChar($FFFE);

type
  // Unicode transformation formats (UTF) data types
  UTF7 = Char;
  UTF8 = Char;
  UTF16 = WideChar;
  UTF32 = Cardinal;

  // UTF conversion schemes (UCS) data types
  PUCS4 = ^UCS4;
  UCS4 = Cardinal;
  PUCS2 = PWideChar;
  UCS2 = WideChar;

const
  ReplacementCharacter: UCS4 = $0000FFFD;
  MaximumUCS2: UCS4 = $0000FFFF;
  MaximumUTF16: UCS4 = $0010FFFF;
  MaximumUCS4: UCS4 = $7FFFFFFF;
                          
  SurrogateHighStart: UCS4 = $D800;
  SurrogateHighEnd: UCS4 = $DBFF;
  SurrogateLowStart: UCS4 = $DC00;
  SurrogateLowEnd: UCS4 = $DFFF;

type
  PCardinal = ^Cardinal;

  TWideStrings = class;
  // Event used to give the application a chance to switch the way of how to save the text in TWideStrings
  // if the text contains characters not only from the ANSI block but the save type is
  // ANSI. On triggering the event the application can change the property SaveUnicode
  // as needed. This property is again checked after the callback returns.
  TConfirmConversionEvent = procedure(Sender: TWideStrings; var Allowed: Boolean) of object;

  TWideStrings = class(TPersistent)
  private
    FUpdateCount: Integer;
    FLanguage: LCID;                   // language can usually left alone, the system's default is used
    FSaved,                            // set in SaveToStream, True in case saving was successfull otherwise False
    FSaveUnicode: Boolean;             // flag set on loading to keep track in which format to save
                                       // (can be set explicitely, but expect losses if there's true Unicode content
                                       // and this flag is set to False)
    FOnConfirmConversion: TConfirmConversionEvent;
    function GetCommaText: WideString;
    function GetName(Index: Integer): WideString;
    function GetValue(const Name: WideString): WideString;
    procedure ReadData(Reader: TReader);
    procedure SetCommaText(const Value: WideString);
    procedure SetValue(const Name, Value: WideString);
    procedure WriteData(Writer: TWriter);
  protected
    procedure DefineProperties(Filer: TFiler); override;
    procedure Error(const Msg: String; Data: Integer);
    function Get(Index: Integer): WideString; virtual; abstract;
    function GetCapacity: Integer; virtual;
    function GetCount: Integer; virtual; abstract;
    function GetObject(Index: Integer): TObject; virtual;
    function GetTextStr: WideString; virtual;
    procedure Put(Index: Integer; const S: WideString); virtual;
    procedure PutObject(Index: Integer; AObject: TObject); virtual;
    procedure SetCapacity(NewCapacity: Integer); virtual;
    procedure SetTextStr(const Value: WideString); virtual;
    procedure SetUpdateState(Updating: Boolean); virtual;
    procedure SetLanguage(Value: LCID); virtual;
  public
    constructor Create;
    destructor Destroy; override;

    function Add(const S: WideString): Integer; virtual;
    function AddObject(const S: WideString; AObject: TObject): Integer; virtual;
    procedure Append(const S: WideString);
    procedure AddStrings(Strings: TStrings); overload; virtual;
    procedure AddStrings(Strings: TWideStrings); overload; virtual; 
    procedure Assign(Source: TPersistent); override;
    procedure AssignTo(Dest: TPersistent); override;
    procedure BeginUpdate;
    procedure Clear; virtual; abstract;
    procedure Delete(Index: Integer); virtual; abstract;
    procedure EndUpdate;
    function Equals(Strings: TWideStrings): Boolean;
    procedure Exchange(Index1, Index2: Integer); virtual;
    function GetText: PWideChar; virtual;
    function IndexOf(const S: WideString): Integer; virtual;
    function IndexOfName(const Name: WideString): Integer;
    function IndexOfObject(AObject: TObject): Integer;
    procedure Insert(Index: Integer; const S: WideString); virtual; abstract;
    procedure InsertObject(Index: Integer; const S: WideString; AObject: TObject);
    procedure LoadFromFile(const FileName: String); virtual;
    procedure LoadFromStream(Stream: TStream); virtual;
    procedure Move(CurIndex, NewIndex: Integer); virtual;
    procedure SaveToFile(const FileName: String); virtual;
    procedure SaveToStream(Stream: TStream); virtual;
    procedure SetText(Text: PWideChar); virtual;

    property Capacity: Integer read GetCapacity write SetCapacity;
    property CommaText: WideString read GetCommaText write SetCommaText;
    property Count: Integer read GetCount;
    property Language: LCID read FLanguage write SetLanguage;
    property Names[Index: Integer]: WideString read GetName;
    property Objects[Index: Integer]: TObject read GetObject write PutObject;
    property Values[const Name: WideString]: WideString read GetValue write SetValue;
    property Saved: Boolean read FSaved;
    property SaveUnicode: Boolean read FSaveUnicode write FSaveUnicode;
    property Strings[Index: Integer]: WideString read Get write Put; default;
    property Text: WideString read GetTextStr write SetTextStr;

    property OnConfirmConversion: TConfirmConversionEvent read FOnConfirmConversion write FOnConfirmConversion;
  end;

  // TWideStringList class
  TWideStringItem = record
    FString: WideString;
    FObject: TObject;
  end;

  TWideStringItemList = array of TWideStringItem;

  TWideStringList = class(TWideStrings)
  private
    FList: TWideStringItemList;
    FCount: Integer;
    FSorted: Boolean;
    FDuplicates: TDuplicates;
    FOnChange: TNotifyEvent;
    FOnChanging: TNotifyEvent;
    procedure ExchangeItems(Index1, Index2: Integer);
    procedure Grow;
    procedure QuickSort(L, R: Integer);
    procedure InsertItem(Index: Integer; const S: WideString);
    procedure SetSorted(Value: Boolean);
  protected
    procedure Changed; virtual;
    procedure Changing; virtual;
    function Get(Index: Integer): WideString; override;
    function GetCapacity: Integer; override;
    function GetCount: Integer; override;
    function GetObject(Index: Integer): TObject; override;
    procedure Put(Index: Integer; const S: WideString); override;
    procedure PutObject(Index: Integer; AObject: TObject); override;
    procedure SetCapacity(NewCapacity: Integer); override;
    procedure SetUpdateState(Updating: Boolean); override;
    procedure SetLanguage(Value: LCID); override;
  public
    destructor Destroy; override;

    function Add(const S: WideString): Integer; override;
    procedure Clear; override;
    procedure Delete(Index: Integer); override;
    procedure Exchange(Index1, Index2: Integer); override;
    function Find(const S: WideString; var Index: Integer): Boolean; virtual;
    function IndexOf(const S: WideString): Integer; override;
    procedure Insert(Index: Integer; const S: WideString); override;
    procedure Sort; virtual;

    property Duplicates: TDuplicates read FDuplicates write FDuplicates;
    property Sorted: Boolean read FSorted write SetSorted;
    property OnChange: TNotifyEvent read FOnChange write FOnChange;
    property OnChanging: TNotifyEvent read FOnChanging write FOnChanging;
  end;

  // result type for number retrival functions
  TUNumber = record
    Numerator,
    Denominator: Integer;
  end;

// functions involving Null-terminated strings
// NOTE: PWideChars as well as WideStrings are NOT managed by reference counting (in opposition to 8 bit strings)!
function StrLenW(Str: PWideChar): Cardinal;
function StrEndW(Str: PWideChar): PWideChar;
function StrMoveW(Dest, Source: PWideChar; Count: Cardinal): PWideChar;
function StrCopyW(Dest, Source: PWideChar): PWideChar;
function StrECopyW(Dest, Source: PWideChar): PWideChar;
function StrLCopyW(Dest, Source: PWideChar; MaxLen: Cardinal): PWideChar;
function StrPCopyW(Dest: PWideChar; const Source: String): PWideChar;
function StrPLCopyW(Dest: PWideChar; const Source: String; MaxLen: Cardinal): PWideChar; 
function StrCatW(Dest, Source: PWideChar): PWideChar;
function StrLCatW(Dest, Source: PWideChar; MaxLen: Cardinal): PWideChar;
function StrCompW(Str1, Str2: PWideChar): Integer;
function StrICompW(Str1, Str2: PWideChar): Integer;
function StrLCompW(Str1, Str2: PWideChar; MaxLen: Cardinal): Integer;
function StrLICompW(Str1, Str2: PWideChar; MaxLen: Cardinal): Integer;
function StrNScanW(S1, S2: PWideChar): Integer;
function StrRNScanW(S1, S2: PWideChar): Integer;
function StrScanW(Str: PWideChar; Chr: WideChar): PWideChar; overload;
function StrScanW(Str: PWideChar; Chr: WideChar; StrLen: Cardinal): PWideChar; overload;
function StrRScanW(Str: PWideChar; Chr: WideChar): PWideChar;
function StrPosW(Str, SubStr: PWideChar): PWideChar; 
function StrUpperW(Str: PWideChar): PWideChar;
function StrLowerW(Str: PWideChar): PWideChar;
function StrTitleW(Str: PWideChar): PWideChar;
function StrAllocW(Size: Cardinal): PWideChar;
function StrBufSizeW(Str: PWideChar): Cardinal;
function StrNewW(Str: PWideChar): PWideChar;
procedure StrDisposeW(Str: PWideChar);
procedure StrSwapByteOrder(Str: PWideChar);

// functions involving Delphi wide strings
function WideAdjustLineBreaks(const S: WideString): WideString;
function WideCharPos(const S: WideString; const Ch: WideChar; const Index: Integer): Integer;  //az
function WideCompose(const S: WideString): WideString;
function WideComposeHangul(Source: WideString): WideString;
function WideDecompose(const S: WideString): WideString;
function WideLoCase(C: WideChar): WideChar;
function WideLowerCase(const S: WideString): WideString;
function WideExtractQuotedStr(var Src: PWideChar; Quote: WideChar): WideString;
function WideQuotedStr(const S: WideString; Quote: WideChar): WideString;
function WideStringOfChar(C: WideChar; Count: Cardinal): WideString;
function WideTitleCaseChar(C: WideChar): WideChar;
function WideTitleCaseString(const S: WideString): WideString;
function WideTrim(const S: WideString): WideString;
function WideTrimLeft(const S: WideString): WideString;
function WideTrimRight(const S: WideString): WideString;
function WideUpCase(C: WideChar): WideChar;
function WideUpperCase(const S: WideString): WideString;

// low level character routines
function UnicodeGetDigit(Code: UCS4): Integer;
function UnicodeGetNumber(Code: UCS4): TUNumber;
function UnicodeToUpper(Code: UCS4): UCS4;
function UnicodeToLower(Code: UCS4): UCS4;
function UnicodeToTitle(Code: UCS4): UCS4;

// character test routines
function UnicodeIsAlpha(C: UCS4): Boolean;
function UnicodeIsDigit(C: UCS4): Boolean;
function UnicodeIsAlphaNum(C: UCS4): Boolean;
function UnicodeIsControl(C: UCS4): Boolean;
function UnicodeIsSpace(C: UCS4): Boolean;
function UnicodeIsWhiteSpace(C: UCS4): Boolean;
function UnicodeIsBlank(C: UCS4): Boolean;
function UnicodeIsPunctuation(C: UCS4): Boolean;
function UnicodeIsGraph(C: UCS4): Boolean;
function UnicodeIsPrintable(C: UCS4): Boolean;
function UnicodeIsUpper(C: UCS4): Boolean;
function UnicodeIsLower(C: UCS4): Boolean;
function UnicodeIsTitle(C: UCS4): Boolean;
function UnicodeIsHexDigit(C: UCS4): Boolean;

function UnicodeIsIsoControl(C: UCS4): Boolean;
function UnicodeIsFormatControl(C: UCS4): Boolean;

function UnicodeIsSymbol(C: UCS4): Boolean;
function UnicodeIsNumber(C: UCS4): Boolean;
function UnicodeIsNonSpacing(C: UCS4): Boolean;
function UnicodeIsOpenPunctuation(C: UCS4): Boolean;
function UnicodeIsClosePunctuation(C: UCS4): Boolean;
function UnicodeIsInitialPunctuation(C: UCS4): Boolean;
function UnicodeIsFinalPunctuation(C: UCS4): Boolean;

function UnicodeIsComposite(C: UCS4): Boolean;
function UnicodeIsQuotationMark(C: UCS4): Boolean;
function UnicodeIsSymmetric(C: UCS4): Boolean;
function UnicodeIsMirroring(C: UCS4): Boolean;
function UnicodeIsNonBreaking(C: UCS4): Boolean;

// Directionality functions
function UnicodeIsRTL(C: UCS4): Boolean;
function UnicodeIsLTR(C: UCS4): Boolean;
function UnicodeIsStrong(C: UCS4): Boolean;
function UnicodeIsWeak(C: UCS4): Boolean;
function UnicodeIsNeutral(C: UCS4): Boolean;
function UnicodeIsSeparator(C: UCS4): Boolean;

// Other character test functions
function UnicodeIsMark(C: UCS4): Boolean;
function UnicodeIsModifier(C: UCS4): Boolean;
function UnicodeIsLetterNumber(C: UCS4): Boolean;
function UnicodeIsConnectionPunctuation(C: UCS4): Boolean;
function UnicodeIsDash(C: UCS4): Boolean;
function UnicodeIsMath(C: UCS4): Boolean;
function UnicodeIsCurrency(C: UCS4): Boolean;
function UnicodeIsModifierSymbol(C: UCS4): Boolean;
function UnicodeIsNonSpacingMark(C: UCS4): Boolean;
function UnicodeIsSpacingMark(C: UCS4): Boolean;
function UnicodeIsEnclosing(C: UCS4): Boolean;
function UnicodeIsPrivate(C: UCS4): Boolean;
function UnicodeIsSurrogate(C: UCS4): Boolean;
function UnicodeIsLineSeparator(C: UCS4): Boolean;
function UnicodeIsParagraphSeparator(C: UCS4): Boolean;

function UnicodeIsIdenifierStart(C: UCS4): Boolean;
function UnicodeIsIdentifierPart(C: UCS4): Boolean;

function UnicodeIsDefined(C: UCS4): Boolean;
function UnicodeIsUndefined(C: UCS4): Boolean;

function UnicodeIsHan(C: UCS4): Boolean;
function UnicodeIsHangul(C: UCS4): Boolean;

// utility functions
function CodePageFromLocale(Language: LCID): Integer;
function KeyboardCodePage: Word;
function KeyUnicode(C: Char): WideChar;
function CodeBlockFromChar(const C: WideChar): Cardinal;
function CodePageToWideString(A: AnsiString; CodePage: Word): WideString;

// WideString Conversion routines
function WideStringToUTF8(S: WideString): AnsiString;
function UTF8ToWideString(S: AnsiString): WideString;

//----------------------------------------------------------------------------------------------------------------------

implementation

// ~67K Unicode data for case mapping, decomposition, numbers etc.
// This data is loaded on demand which means only those parts will be put in memory which are needed
// by one of the lookup functions.
{$R WideList.res}

uses
  Consts, SyncObjs, SysUtils;

resourcestring
  SUREBaseString = 'Error in regular expression: %s' + #13;
  SUREUnexpectedEOS = 'Unexpected end of pattern.';
  SURECharacterClassOpen = 'Character class not closed, '']'' is missing.';
  SUREUnbalancedGroup = 'Unbalanced group expression, '')'' is missing.';
  SUREInvalidCharProperty = 'A character property is invalid';
  SUREInvalidRepeatRange = 'Invalid repeation range.';
  SURERepeatRangeOpen = 'Repeation range not closed, ''}'' is missing.';
  SUREExpressionEmpty = 'Expression is empty.';

type
  TCompareFunc = function (W1, W2: WideString; Locale: LCID): Integer;

var
  WideCompareText: TCompareFunc;

//----------------- Loader routines for resource data ------------------------------------------------------------------

const
  // Values that can appear in the Mask1 parameter of the IsProperty function.
  UC_MN = $00000001; // Mark, Non-Spacing
  UC_MC = $00000002; // Mark, Spacing Combining
  UC_ME = $00000004; // Mark, Enclosing
  UC_ND = $00000008; // Number, Decimal Digit
  UC_NL = $00000010; // Number, Letter
  UC_NO = $00000020; // Number, Other
  UC_ZS = $00000040; // Separator, Space
  UC_ZL = $00000080; // Separator, Line
  UC_ZP = $00000100; // Separator, Paragraph
  UC_CC = $00000200; // Other, Control
  UC_CF = $00000400; // Other, Format
  UC_OS = $00000800; // Other, Surrogate
  UC_CO = $00001000; // Other, private use
  UC_CN = $00002000; // Other, not assigned
  UC_LU = $00004000; // Letter, Uppercase
  UC_LL = $00008000; // Letter, Lowercase
  UC_LT = $00010000; // Letter, Titlecase
  UC_LM = $00020000; // Letter, Modifier
  UC_LO = $00040000; // Letter, Other
  UC_PC = $00080000; // Punctuation, Connector
  UC_PD = $00100000; // Punctuation, Dash
  UC_PS = $00200000; // Punctuation, Open
  UC_PE = $00400000; // Punctuation, Close
  UC_PO = $00800000; // Punctuation, Other
  UC_SM = $01000000; // Symbol, Math
  UC_SC = $02000000; // Symbol, Currency
  UC_SK = $04000000; // Symbol, Modifier
  UC_SO = $08000000; // Symbol, Other
  UC_L  = $10000000; // Left-To-Right
  UC_R  = $20000000; // Right-To-Left
  UC_EN = $40000000; // European Number
  UC_ES = $80000000; // European Number Separator

  // Values that can appear in the Mask2 parameter of the IsProperty function
  UC_ET = $00000001; // European Number Terminator
  UC_AN = $00000002; // Arabic Number
  UC_CS = $00000004; // Common Number Separator
  UC_B  = $00000008; // Block Separator
  UC_S  = $00000010; // Segment (unit) Separator (this includes tab and vertical tab)
  UC_WS = $00000020; // Whitespace
  UC_ON = $00000040; // Other Neutrals

  // Implementation specific character properties.
  UC_CM = $00000080; // Composite
  UC_NB = $00000100; // Non-Breaking
  UC_SY = $00000200; // Symmetric
  UC_HD = $00000400; // Hex Digit
  UC_QM = $00000800; // Quote Mark
  UC_MR = $00001000; // Mirroring
  UC_SS = $00002000; // Space, other

  UC_CP = $00004000; // Defined

  // Added for UnicodeData-2.1.3.
  UC_PI = $00008000; // Punctuation, Initial
  UC_PF = $00010000; // Punctuation, Final

type
  TUHeader = record
    BOM: WideChar;
    Count: Word;
    case Boolean of
      True:
        (Bytes: Cardinal);
      False:
        (Len: array[0..1] of Word);
  end;

  TWordArray = array of Word;
  TCardinalArray = array of Cardinal;

var
  // As the global data can be accessed by several threads it should be guarded
  // while the data is loaded.
  LoadInProgress: TCriticalSection;

//----------------- internal support routines --------------------------------------------------------------------------

function SwapCardinal(C: Cardinal): Cardinal; 

// swaps all bytes in C from MSB to LSB order
// EAX contains both parameter as well as result

asm
              BSWAP EAX
end;

//----------------- support for character properties -------------------------------------------------------------------

var
  PropertyOffsets: TWordArray;
  PropertyRanges: TCardinalArray;

procedure LoadUnicodeTypeData;

// loads the character property data (as saved by the Unicode database extractor into the ctype.dat file)

var
  I, Size: Integer;
  Header: TUHeader;
  Stream: TResourceStream;

begin
  // make sure no other code is currently modifying the global data area
  if LoadInProgress = nil then LoadInProgress := TCriticalSection.Create;
  LoadInProgress.Enter;

  // Data already loaded?
  if PropertyOffsets = nil then
  begin
    Stream := TResourceStream.Create(HInstance, 'TYPE', 'UNICODE');
    Stream.Read(Header, SizeOf(Header));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      Header.Count := Swap(Header.Count);
      Header.Bytes := SwapCardinal(Header.Bytes);
    end;

    // Calculate the offset into the storage for the ranges.  The offsets
    // array is on a 4-byte boundary and one larger than the value provided in
    // the header count field. This means the offset to the ranges must be
    // calculated after aligning the count to a 4-byte boundary.
    Size := (Header.Count + 1) * SizeOf(Word);
    if (Size and 3) <> 0 then Inc(Size, 4 - (Size and 3));

    // fill offsets array
    SetLength(PropertyOffsets, Size div SizeOf(Word));
    Stream.Read(PropertyOffsets[0], Size);

    // Do an endian swap if necessary.  Don't forget there is an extra node on the end with the final index.
    if Header.BOM = BOM_MSB_FIRST then
    begin
      for I := 0 to Header.Count do
          PropertyOffsets[I] := Swap(PropertyOffsets[I]);
    end;

    // Load the ranges.  The number of elements is in the last array position of the offsets.
    SetLength(PropertyRanges, PropertyOffsets[Header.Count]);
    Stream.Read(PropertyRanges[0], PropertyOffsets[Header.Count] * SizeOf(Cardinal));

    // Do an endian swap if necessary.
    if Header.BOM = BOM_MSB_FIRST then
    begin
      for I := 0 to PropertyOffsets[Header.Count] - 1 do
        PropertyRanges[I] := SwapCardinal(PropertyRanges[I]);
    end;
    Stream.Free;
  end;
  LoadInProgress.Leave;
end;

//----------------------------------------------------------------------------------------------------------------------

function PropertyLookup(Code, N: Cardinal): Boolean;

var
  L, R, M: Integer;
  
begin
  // load property data if not already done
  if PropertyOffsets = nil then LoadUnicodeTypeData;
  
  Result := False;
  // There is an extra node on the end of the offsets to allow this routine
  // to work right.  If the index is 0xffff, then there are no nodes for the property.
  L := PropertyOffsets[N];
  if L <> $FFFF then
  begin
    // Locate the next offset that is not 0xffff.  The sentinel at the end of
    // the array is the max index value.
    M := 1;
    while ((Integer(N) + M) < High(PropertyOffsets)) and (PropertyOffsets[Integer(N) + M] = $FFFF) do Inc(M);

    R := PropertyOffsets[Integer(N) + M] - 1;

    while L <= R do
    begin
      // Determine a "mid" point and adjust to make sure the mid point is at
      // the beginning of a range pair.
      M := (L + R) shr 1;
      Dec(M, M and 1);
      if Code > PropertyRanges[M + 1] then L := M + 2
                                      else
        if Code < PropertyRanges[M] then R := M - 2
                                    else
          if (Code >= PropertyRanges[M]) and (Code <= PropertyRanges[M + 1]) then
          begin
            Result := True;
            Break;
          end;
    end;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function IsProperty(Code, Mask1, Mask2: Cardinal): Boolean;

var
  I: Cardinal;
  Mask: Cardinal;
  
begin
  Result := False;
  if Mask1 <> 0 then
  begin
    Mask := 1;
    for I := 0 to 31 do
    begin
      if ((Mask1 and Mask) <> 0) and PropertyLookup(Code, I) then
      begin
        Result := True;
        Exit;
      end;
      Mask := Mask shl 1;
    end;
  end;

  if Mask2 <> 0 then
  begin
    I := 32;
    Mask := 1;
    while I < Cardinal(High(PropertyOffsets)) do
    begin
      if ((Mask2 and Mask) <> 0) and PropertyLookup(Code, I) then
      begin
        Result := True;
        Exit;
      end;
      Inc(I);
      Mask := Mask shl 1;
    end;
  end;
end;

//----------------- support for case mapping ---------------------------------------------------------------------------
var
  CaseMapSize: Cardinal;
  CaseLengths: array[0..1] of Word;
  CaseMap: TCardinalArray;

procedure LoadUnicodeCaseData;

var
  Stream: TResourceStream;
  I: Cardinal;
  Header: TUHeader;
  
begin
  // make sure no other code is currently modifying the global data area
  if LoadInProgress = nil then LoadInProgress := TCriticalSection.Create;
  LoadInProgress.Enter;

  if CaseMap = nil then
  begin
    Stream := TResourceStream.Create(HInstance, 'CASE', 'UNICODE');
    Stream.Read(Header, SizeOf(Header));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      Header.Count := Swap(Header.Count);
      Header.Len[0] := Swap(Header.Len[0]);
      Header.Len[1] := Swap(Header.Len[1]);
    end;

    // Set the node count and lengths of the upper and lower case mapping tables.
    CaseMapSize := Header.Count * 3;
    CaseLengths[0] := Header.Len[0] * 3;
    CaseLengths[1] := Header.Len[1] * 3;

    SetLength(CaseMap, CaseMapSize);

    // Load the case mapping table.
    Stream.Read(CaseMap[0], CaseMapSize * SizeOf(Cardinal));

    // Do an endian swap if necessary.
    if Header.BOM = BOM_MSB_FIRST then
      for I := 0 to CaseMapSize -1 do CaseMap[I] := SwapCardinal(CaseMap[I]);
    Stream.Free;
  end;
  LoadInProgress.Leave;
end;

//----------------------------------------------------------------------------------------------------------------------

function CaseLookup(Code: Cardinal; L, R, Field: Integer): Cardinal;

var
  M: Integer;

begin
  // load case mapping data if not already done
  if CaseMap = nil then LoadUnicodeCaseData;

  // Do the binary search.
  while L <= R do
  begin
    // Determine a "mid" point and adjust to make sure the mid point is at
    // the beginning of a case mapping triple.
    M := (L + R) shr 1;
    Dec(M, M mod 3);
    if Code > CaseMap[M] then L := M + 3
                         else
      if Code < CaseMap[M] then R := M - 3
                           else
        if Code = CaseMap[M] then
        begin
          Result := CaseMap[M + Field];
          Exit;
        end;
  end;

  Result := Code;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeToUpper(Code: UCS4): UCS4;

var
  Field,
  L, R: Integer;

begin
  // load case mapping data if not already done
  if CaseMap = nil then LoadUnicodeCaseData;

  if UnicodeIsUpper(Code) then Result := Code
                          else
  begin
    if UnicodeIsLower(Code) then
    begin
      Field := 2;
      L := CaseLengths[0];
      R := (L + CaseLengths[1]) - 3;
    end
    else
    begin
      Field := 1;
      L := CaseLengths[0] + CaseLengths[1];
      R := CaseMapSize - 3;
    end;
    Result := CaseLookup(Code, L, R, Field);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeToLower(Code: UCS4): UCS4;

var
  Field,
  L, R: Integer;
  
begin
  // load case mapping data if not already done
  if CaseMap = nil then LoadUnicodeCaseData;

  if UnicodeIsLower(Code) then Result := Code
                          else
  begin
    if UnicodeIsUpper(Code) then
    begin
      Field := 1;
      L := 0;
      R := CaseLengths[0] - 3;
    end
    else
    begin
      Field := 2;
      L := CaseLengths[0] + CaseLengths[1];
      R := CaseMapSize - 3;
    end;
    Result := CaseLookup(Code, L, R, Field);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeToTitle(Code: UCS4): UCS4;

var
  Field,
  L, R: Integer;

begin
  // load case mapping data if not already done
  if CaseMap = nil then LoadUnicodeCaseData;

  if UnicodeIsTitle(Code) then Result := Code
                          else
  begin
    // The offset will always be the same for converting to title case.
    Field := 2;

    if UnicodeIsUpper(Code) then
    begin
      L := 0;
      R := CaseLengths[0] - 3;
    end
    else
    begin
      L := CaseLengths[0];
      R := (L + CaseLengths[1]) - 3;
    end;
    Result := CaseLookup(Code, L, R, Field);
  end;
end;

//----------------- Support for decomposition --------------------------------------------------------------------------

const // constants for hangul composition and decomposition (this is done algorithmically
      // saving so significant memory)
  SBase = $AC00;
  LBase = $1100;
  VBase = $1161;
  TBase = $11A7;
  LCount = 19;
  VCount = 21;
  TCount = 28;
  NCount = VCount * TCount;   // 588
  SCount = LCount * NCount;   // 11172
  
var
  DecompositionSize: Cardinal;
  DecompositionNodes,
  Decompositions: TCardinalArray;

//----------------------------------------------------------------------------------------------------------------------

procedure LoadUnicodeDecompositionData;

var
  Stream: TResourceStream;
  I: Cardinal;
  Header: TUHeader;
  
begin
  // make sure no other code is currently modifying the global data area
  if LoadInProgress = nil then LoadInProgress := TCriticalSection.Create;
  LoadInProgress.Enter;

  if Decompositions = nil then
  begin
    Stream := TResourceStream.Create(HInstance, 'DECOMPOSE', 'UNICODE');
    Stream.Read(Header, SizeOf(Header));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      Header.Count := Swap(Header.Count);
      Header.Bytes := SwapCardinal(Header.Bytes);
    end;

    DecompositionSize := Header.Count shl 1; // two values per node
    SetLength(DecompositionNodes, DecompositionSize + 1); // one entry more (the sentinel)
    Stream.Read(DecompositionNodes[0], (DecompositionSize + 1) * SizeOf(Cardinal));
    SetLength(Decompositions, (Header.Bytes div SizeOf(Cardinal)) - DecompositionSize - 1);
    Stream.Read(Decompositions[0], Length(Decompositions) * SizeOf(Cardinal));

    // Do an endian swap if necessary.
    if Header.BOM = BOM_MSB_FIRST then
    begin
      for I := 0 to High(DecompositionNodes) do
          DecompositionNodes[I] := SwapCardinal(DecompositionNodes[I]);
      for I := 0 to High(Decompositions) do
          Decompositions[I] := SwapCardinal(Decompositions[I]);
    end;
    Stream.Free;
  end;

  LoadInProgress.Leave;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeDecomposeHangul(Code: UCS4): TCardinalArray;

// algorithmically decompose hangul character using some predefined contstants

var
  Rest: Integer;                             
  
begin
  if not UnicodeIsHangul(Code) then Result := nil
                               else
  begin
    Dec(Code, SBase);
    Rest := Code mod TCount;
    if Rest = 0 then SetLength(Result, 2)
                else SetLength(Result, 3);
    Result[0] := LBase + (Code div NCount);
    Result[1] := VBase + ((Code mod NCount) div TCount);
    if Rest <> 0 then Result[2] := TBase + Rest;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeDecompose(Code: UCS4): TCardinalArray;

var
  L, R, M: Integer;

begin
  // load decomposition data if not already done
  if Decompositions = nil then LoadUnicodeDecompositionData;

  if not UnicodeIsComposite(Code) then
  begin
    // return the code itself if it is not a composite
    SetLength(Result, 1);
    Result[0] := Code;
  end
  else
  begin
    // if the code is hangul then decomposition is algorithmically 
    Result := UnicodeDecomposeHangul(Code);
    if Result = nil then
    begin
      L := 0;
      R := DecompositionNodes[DecompositionSize] - 1;

      while L <= R do
      begin
        // Determine a "mid" point and adjust to make sure the mid point is at
        // the beginning of a code + offset pair.
        M := (L + R) shr 1;
        Dec(M, M and 1);
        if Code > DecompositionNodes[M] then L := M + 2
                                        else
          if Code < DecompositionNodes[M] then R := M - 2
                                          else
            if Code = DecompositionNodes[M] then
            begin
              // found a decomposition, return the codes
              SetLength(Result, DecompositionNodes[M + 3] - DecompositionNodes[M + 1] - 1);
              Move(Decompositions[DecompositionNodes[M + 1]], Result[0], Length(Result) * SizeOf(Cardinal));
              Break;
            end;
      end;
    end;
  end;
end;

//----------------- Support for combining classes ----------------------------------------------------------------------

var
  CCLSize: Cardinal;
  CCLNodes: TCardinalArray;

//----------------------------------------------------------------------------------------------------------------------

procedure LoadUnicodeCombiningData;

var
  Stream: TResourceStream;
  I: Cardinal;
  Header: TUHeader;

begin
  // make sure no other code is currently modifying the global data area
  if LoadInProgress = nil then LoadInProgress := TCriticalSection.Create;
  LoadInProgress.Enter;

  if CCLNodes = nil then
  begin
    Stream := TResourceStream.Create(HInstance, 'COMBINE', 'UNICODE');
    Stream.Read(Header, SizeOf(Header));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      Header.Count := Swap(Header.Count);
      Header.Bytes := SwapCardinal(Header.Bytes);
    end;

    CCLSize := Header.Count * 3;
    SetLength(CCLNodes, CCLSize);
    Stream.Read(CCLNodes[0], CCLSize * SizeOf(Cardinal));

    if Header.BOM = BOM_MSB_FIRST then
      for I := 0 to CCLSize - 1 do
        CCLNodes[I] := SwapCardinal(CCLNodes[I]);

    Stream.Free;
  end;
  LoadInProgress.Leave;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeCanonicalClass(Code: Cardinal): Cardinal;

var
  L, R, M: Integer;

begin
  // load combination data if not already done
  if CCLNodes = nil then LoadUnicodeCombiningData;

  Result := 0;
  L := 0;
  R := CCLSize - 1;

  while L <= R do
  begin
    M := (L + R) shr 1;
    Dec(M, M mod 3);
    if Code > CCLNodes[M + 1] then L := M + 3
                              else
      if Code < CCLNodes[M] then R := M - 3
                            else
        if (Code >= CCLNodes[M]) and (Code <= CCLNodes[M + 1]) then
        begin
          Result := CCLNodes[M + 2];
          Break;
        end;
  end;
end;

//----------------- Support for numeric values -------------------------------------------------------------------------

var
  NumberSize: Cardinal;
  NumberNodes: TCardinalArray;
  NumberValues: TWordArray;

//----------------------------------------------------------------------------------------------------------------------

procedure LoadUnicodeNumberData;

var
  Stream: TResourceStream;
  I: Cardinal;
  Header: TUHeader;

begin
  // make sure no other code is currently modifying the global data area
  if LoadInProgress = nil then LoadInProgress := TCriticalSection.Create;
  LoadInProgress.Enter;

  if NumberNodes = nil then
  begin
    Stream := TResourceStream.Create(HInstance, 'NUMBERS', 'UNICODE');
    Stream.Read(Header, SizeOf(Header));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      Header.Count := Swap(Header.Count);
      Header.Bytes := SwapCardinal(Header.Bytes);     
    end;

    NumberSize := Header.Count;
    SetLength(NumberNodes, NumberSize);
    Stream.Read(NumberNodes[0], NumberSize * SizeOf(Cardinal));
    SetLength(NumberValues, (Header.Bytes - NumberSize * SizeOf(Cardinal)) div SizeOf(Word));
    Stream.Read(NumberValues[0], Length(NumberValues) * SizeOf(Word));

    if Header.BOM = BOM_MSB_FIRST then
    begin
      for I := 0 to High(NumberNodes) do
        NumberNodes[I] := SwapCardinal(NumberNodes[I]);
      for I := 0 to High(NumberValues) do
        NumberValues[I] := Swap(NumberValues[I]);
    end;
    Stream.Free;
  end;
  LoadInProgress.Leave;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeNumberLookup(Code: UCS4; var num: TUNumber): Boolean;

var
  L, R, M: Integer;
  VP: PWord;

begin
  // load number data if not already done
  if NumberNodes = nil then LoadUnicodeNumberData;

  Result := False;
  L := 0;
  R := NumberSize - 1;
  while L <= R do
  begin
    // Determine a "mid" point and adjust to make sure the mid point is at
    // the beginning of a code+offset pair.
    M := (L + R) shr 1;
    Dec(M, M and 1);
    if Code > NumberNodes[M] then L := M + 2
                             else
      if Code < NumberNodes[M] then R := M - 2
                               else
      begin
        VP := Pointer(Cardinal(@NumberValues[0]) + NumberNodes[M + 1]);
        num.numerator := VP^;
        Inc(VP);
        num.denominator := VP^;
        Result := True;
        Break;
      end;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeDigitLookup(Code: UCS4; var Digit: Integer): Boolean;

var
  L, R, M: Integer;
  VP: PWord;

begin
  // load number data if not already done
  if NumberNodes = nil then LoadUnicodeNumberData;

  Result := False;
  L := 0;
  R := NumberSize - 1;
  while L <= R do
  begin
    // Determine a "mid" point and adjust to make sure the mid point is at
    // the beginning of a code+offset pair.
    M := (L + R) shr 1;
    Dec(M, M and 1);
    if Code > NumberNodes[M] then L := M + 2
                             else
      if Code < NumberNodes[M] then R := M - 2
                               else
      begin
        VP := Pointer(Cardinal(@NumberValues[0]) + NumberNodes[M + 1]);
        M := VP^;
        Inc(VP);
        if M = VP^ then
        begin
          Digit := M;
          Result := True;
        end;
        Break;
      end;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeGetNumber(Code: UCS4): TUNumber;

begin
  // Initialize with some arbitrary value, because the caller simply cannot
  // tell for sure if the code is a number without calling the ucisnumber()
  // macro before calling this function.
  Result.Numerator := -111;
  Result.Denominator := -111;

  UnicodeNumberLookup(Code, Result);
end;

//----------------------------------------------------------------------------------------------------------------------

function UnicodeGetDigit(Code: UCS4): Integer;

begin
  // Initialize with some arbitrary value, because the caller simply cannot
  // tell for sure if the code is a number without calling the ucisdigit()
  //  macro before calling this function.
  Result := -111;

  UnicodeDigitLookup(Code, Result);
end;




//----------------------------------------------------------------------------------------------------------------------

function IsSeparator(C: UCS4): Boolean;

begin
  Result := (C = $D) or (C = $A) or (C = $2028) or (C = $2029);
end;

//----------------------------------------------------------------------------------------------------------------------

const
  PropertyMap: array[0..31] of Cardinal = (
    0, // class ID 1, corresponds to UC_MN
    1, // class ID 2, UC_MC
    3, // 3, UC_ND
    5, // 4, UC_NO
    6, // 5, UC_ZS
    7, // 6, UC_ZL
    8, // 7, UC_ZP
    9, // 8, UC_CC
    12, // 9, UC_CO
    14, // 10, UC_LU
    15, // 11, UC_LL
    16, // 12, UC_LT
    17, // 13, UC_LM
    18, // 14, UC_LO
    20, // 15, UC_PD
    21, // 16, UC_PS
    22, // 17, UC_PE
    23, // 18, UC_PO
    24, // 19, UC_SM
    25, // 20, UC_SC
    26, // 21, UC_SO
    27, // 22, UC_L
    28, // 23, UC_R
    29, // 24, UC_EN
    30, // 25, UC_ES
    32, // 26, UC_ET
    33, // 27, UC_AN
    34, // 28, UC_CS
    35, // 29, UC_B
    36, // 30, UC_S
    37, // 31, UC_WS
    38  // 32, UC_ON
    );


//----------------- TWideStrings ---------------------------------------------------------------------------------------

constructor TWideStrings.Create;

begin
  inherited;
  // there should seldom be the need to use a language other than the one of the system
  FLanguage := GetUserDefaultLCID;
end;

//----------------------------------------------------------------------------------------------------------------------

destructor TWideStrings.Destroy;

begin
  inherited;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetLanguage(Value: LCID);

begin
  FLanguage := Value;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.Add(const S: WideString): Integer;

begin
  Result := GetCount;
  Insert(Result, S);
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.AddObject(const S: WideString; AObject: TObject): Integer;

begin
  Result := Add(S);
  PutObject(Result, AObject);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Append(const S: WideString);

begin
  Add(S);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.AddStrings(Strings: TStrings);

var
  I: Integer;

begin
  BeginUpdate;
  try
    for I := 0 to Strings.Count - 1 do AddObject(Strings[I], Strings.Objects[I]);
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.AddStrings(Strings: TWideStrings);

var
  I: Integer;

begin
  BeginUpdate;
  try
    for I := 0 to Strings.Count - 1 do AddObject(Strings[I], Strings.Objects[I]);
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Assign(Source: TPersistent);

// usual assignment routine, but able to assign wide and small strings

var
  I: Integer;

begin
  if Source is TWideStrings then
  begin
    BeginUpdate;
    try
      Clear;
      AddStrings(TWideStrings(Source));
    finally
      EndUpdate;
    end;
  end
  else
    if Source is TStrings then
    begin
      BeginUpdate;
      try
        Clear;
        for I := 0 to TStrings(Source).Count - 1 do AddObject(TStrings(Source)[I], TStrings(Source).Objects[I]);
      finally
        EndUpdate;
      end;
    end
    else inherited Assign(Source);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.AssignTo(Dest: TPersistent);

// need to do also assignment to old style TStrings, but this class doesn't know TWideStrings, so
// we need to do it from here

var
  I: Integer;

begin
  if Dest is TStrings then
    with Dest as TStrings do
    begin
      BeginUpdate;
      try
        Clear;
        for I := 0 to Self.Count - 1 do AddObject(Self[I], Self.Objects[I]);
      finally
        EndUpdate;
      end;
    end
    else
      if Dest is TWideStrings then
        with Dest as TWideStrings do
        begin
          BeginUpdate;
          try
            Clear;
            AddStrings(Self);
          finally
            EndUpdate;
          end;
        end
        else inherited;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.BeginUpdate;

begin
  if FUpdateCount = 0 then SetUpdateState(True);
  Inc(FUpdateCount);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.DefineProperties(Filer: TFiler);

  function DoWrite: Boolean;

  begin
    if Filer.Ancestor <> nil then
    begin
      Result := True;
      if Filer.Ancestor is TWideStrings then Result := not Equals(TWideStrings(Filer.Ancestor))
    end
    else Result := Count > 0;
  end;

begin
  Filer.DefineProperty('WideStrings', ReadData, WriteData, DoWrite);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.EndUpdate;

begin
  Dec(FUpdateCount);
  if FUpdateCount = 0 then SetUpdateState(False);
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.Equals(Strings: TWideStrings): Boolean;

var
  I, Count: Integer;

begin
  Result := False;
  Count := GetCount;
  if Count <> Strings.GetCount then Exit;
  for I := 0 to Count - 1 do
    if Get(I) <> Strings.Get(I) then Exit;
  Result := True;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Error(const Msg: String; Data: Integer);

  function ReturnAddr: Pointer;

  asm
          MOV EAX, [EBP + 4]
  end;

begin
  raise EStringListError.CreateFmt(Msg, [Data]) at ReturnAddr;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Exchange(Index1, Index2: Integer);

var
  TempObject: TObject;
  TempString: WideString;

begin
  BeginUpdate;
  try
    TempString := Strings[Index1];
    TempObject := Objects[Index1];
    Strings[Index1] := Strings[Index2];
    Objects[Index1] := Objects[Index2];
    Strings[Index2] := TempString;
    Objects[Index2] := TempObject;
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetCapacity: Integer;

begin  // descendants may optionally override/replace this default implementation
  Result := Count;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetCommaText: WideString;

var
  S: WideString;
  P: PWideChar;
  I,
  Count: Integer;

begin
  Count := GetCount;
  if (Count = 1) and (Get(0) = '') then Result := '""'
                                   else
  begin
    Result := '';
    for I := 0 to Count - 1 do
    begin
      S := Get(I);
      P := PWideChar(S);
      while not (P^ in [WideNull..Space, WideChar('"'), WideChar(',')]) do Inc(P);
      if (P^ <> WideNull) then S := WideQuotedStr(S, '"');
      Result := Result + S + ', ';
    end;
    System.Delete(Result, Length(Result), 1);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetName(Index: Integer): WideString;

var
  P: Integer;

begin
  Result := Get(Index);
  P := Pos('=', Result);
  if P > 0 then SetLength(Result, P - 1)
           else Result := '';
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetObject(Index: Integer): TObject;

begin
  Result := nil;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetText: PWideChar;

begin
  Result := StrNewW(PWideChar(GetTextStr));
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetTextStr: WideString;

var
  I, L,
  Size,
  Count: Integer;
  P: PWideChar;
  S: WideString;

begin
  Count := GetCount;
  Size := 0;
  for I := 0 to Count - 1 do Inc(Size, Length(Get(I)) + 2);
  SetLength(Result, Size);
  P := Pointer(Result);
  for I := 0 to Count - 1 do
  begin
    S := Get(I);
    L := Length(S);
    if L <> 0 then
    begin
      System.Move(Pointer(S)^, P^, 2 * L);
      Inc(P, L);
    end;
    P^ := CarriageReturn;
    Inc(P);
    P^ := LineFeed;
    Inc(P);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.GetValue(const Name: WideString): WideString;

var
  I: Integer;

begin
  I := IndexOfName(Name);
  if I >= 0 then Result := Copy(Get(I), Length(Name) + 2, MaxInt)
            else Result := '';
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.IndexOf(const S: WideString): Integer;

begin
  for Result := 0 to GetCount - 1 do
    if WideCompareText(Get(Result), S, FLanguage) = 0 then Exit;
  Result := -1;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.IndexOfName(const Name: WideString): Integer;

var
  P: Integer;
  S: WideString;

begin
  for Result := 0 to GetCount - 1 do
  begin
    S := Get(Result);
    P := Pos('=', S);
    if (P > 0) and (WideCompareText(Copy(S, 1, P - 1), Name, FLanguage) = 0) then Exit;
  end;
  Result := -1;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStrings.IndexOfObject(AObject: TObject): Integer;

begin
  for Result := 0 to GetCount - 1 do
    if GetObject(Result) = AObject then Exit;
  Result := -1;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.InsertObject(Index: Integer; const S: WideString; AObject: TObject);

begin
  Insert(Index, S);
  PutObject(Index, AObject);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.LoadFromFile(const FileName: String);

var
  Stream: TStream;

begin
  try
    Stream := TFileStream.Create(FileName, fmOpenRead or fmShareDenyNone);
    try
      LoadFromStream(Stream);
    finally
      Stream.Free;
    end;
  except
    RaiseLastWin32Error;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.LoadFromStream(Stream: TStream);

// usual loader routine, but enhanced to handle byte order marks in stream

var
  Size,
  BytesRead: Integer;
  Order: WideChar;
  SW: WideString;
  SA: String;
    
begin
  BeginUpdate;
  try
    Size := Stream.Size - Stream.Position;
    BytesRead := Stream.Read(Order, 2);
    if (Order = BOM_LSB_FIRST) or (Order = BOM_MSB_FIRST) then
    begin
      FSaveUnicode := True;
      SetLength(SW, (Size - 2) div 2);
      Stream.Read(PWideChar(SW)^, Size - 2);
      if Order = BOM_MSB_FIRST then StrSwapByteOrder(PWideChar(SW));
      SetTextStr(SW);
    end
    else
    begin
      // without byte order mark it is assumed that we are loading ANSI text
      FSaveUnicode := False;
      Stream.Seek(-BytesRead, soFromCurrent);
      SetLength(SA, Size);
      Stream.Read(PChar(SA)^, Size);
      SetTextStr(SA);
    end;
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Move(CurIndex, NewIndex: Integer);

var
  TempObject: TObject;
  TempString: WideString;

begin
  if CurIndex <> NewIndex then
  begin
    BeginUpdate;
    try
      TempString := Get(CurIndex);
      TempObject := GetObject(CurIndex);
      Delete(CurIndex);
      InsertObject(NewIndex, TempString, TempObject);
    finally
      EndUpdate;
    end;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.Put(Index: Integer; const S: WideString);

var
  TempObject: TObject;

begin
  TempObject := GetObject(Index);
  Delete(Index);
  InsertObject(Index, S, TempObject);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.PutObject(Index: Integer; AObject: TObject);

begin
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.ReadData(Reader: TReader);

begin
  Reader.ReadListBegin;
  BeginUpdate;
  try
    Clear;
    while not Reader.EndOfList do
    Add(Reader.ReadWideString);
  finally
    EndUpdate;
  end;
  Reader.ReadListEnd;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SaveToFile(const FileName: String);

var
  Stream: TStream;

begin
  Stream := TFileStream.Create(FileName, fmCreate);
  try
    SaveToStream(Stream);
  finally
    Stream.Free;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SaveToStream(Stream: TStream);

var
  SW, BOM: WideString;
  SA: String;
  Allowed: Boolean;
  Run: PWideChar;

begin
  // The application can decide in which format to save the content.
  // If FSaveUnicode is False then all strings are saved in standard ANSI format
  // which is also loadable by TStrings but you should be aware that all Unicode
  // strings are then converted to ANSI based on the current system locale.
  // An extra event is supplied to ask the user about the potential loss of information
  // when converting Unicode to ANSI strings.
  SW := GetTextStr;
  Allowed := True;
  FSaved := False; // be pessimistic
  // check for potential information loss makes only sense if the application has set
  // an event to be used as call back to ask about the conversion
  if not FSaveUnicode and Assigned(FOnConfirmConversion) then
  begin
    // application requests to save only ANSI characters, so check the text and
    // call back in case information could be lost
    Run := PWideChar(SW);
    // only ask if there's at least one Unicode character in the text
    while Run^ in [WideChar(#1)..WideChar(#255)] do Inc(Run);
    // Note: The application can still set FSaveUnicode to True in the callback.
    if Run^ <> WideNull then FOnConfirmConversion(Self, Allowed);
  end;

  if Allowed then
  begin
    // only save if allowed
    if FSaveUnicode then
    begin
      BOM := BOM_LSB_FIRST;
      Stream.WriteBuffer(PWideChar(BOM)^, 2);
      // SW has already been filled
      Stream.WriteBuffer(PWideChar(SW)^, 2 * Length(SW));
    end
    else
    begin
      // implicit conversion to ANSI
      SA := SW;
      if Allowed then Stream.WriteBuffer(PWideChar(SA)^, Length(SA));
    end;
    FSaved := True;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetCapacity(NewCapacity: Integer);

begin
  // do nothing - descendants may optionally implement this method
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetCommaText(const Value: WideString);

var
  P, P1: PWideChar;
  S: WideString;

begin
  BeginUpdate;
  try
    Clear;
    P := PWideChar(Value);
    while P^ in [WideChar(#1)..Space] do Inc(P);
    while P^ <> WideNull do
    begin
      if P^ = '"' then  S := WideExtractQuotedStr(P, '"')
                  else
      begin
        P1 := P;
        while (P^ > Space) and (P^ <> ', ') do Inc(P);
        SetString(S, P1, P - P1);
      end;
      Add(S);

      while P^ in [WideChar(#1)..Space] do Inc(P);
      if P^ = ', ' then
        repeat
          Inc(P);
        until not (P^ in [WideChar(#1)..Space]);
    end;
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetText(Text: PWideChar);

begin
  SetTextStr(Text);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetTextStr(const Value: WideString);

var
  Head,
  Tail: PWideChar;
  S: WideString;

begin
  BeginUpdate;
  try
    Clear;
    Head := PWideChar(Value);
    while Head^ <> WideNull do
    begin
      Tail := Head;
      while not (Tail^ in [WideNull, LineFeed, CarriageReturn, VerticalTab, FormFeed]) and
            (Tail^ <> LineSeparator) and
            (Tail^ <> ParagraphSeparator) do Inc(Tail);
      SetString(S, Head, Tail - Head);
      Add(S);
      Head := Tail;
      if Head^ <> WideNull then
      begin
        Inc(Head);
        if (Tail^ = CarriageReturn) and
           (Head^ = LineFeed) then Inc(Head);
      end;
    end;
  finally
    EndUpdate;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetUpdateState(Updating: Boolean);

begin
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.SetValue(const Name, Value: WideString);

var
  I : Integer;

begin
  I := IndexOfName(Name);
  if Value <> '' then
  begin
    if I < 0 then I := Add('');
    Put(I, Name + '=' + Value);
  end
  else
    if I >= 0 then Delete(I);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStrings.WriteData(Writer: TWriter);

var
  I: Integer;

begin
  Writer.WriteListBegin;
  for I := 0 to Count-1 do
  Writer.WriteWideString(Get(I));
  Writer.WriteListEnd;
end;

//----------------- TWideStringList ------------------------------------------------------------------------------------

destructor TWideStringList.Destroy;

begin
  FOnChange := nil;
  FOnChanging := nil;
  FCount := 0;
  FList := nil;
  inherited Destroy;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.Add(const S: WideString): Integer;

begin
  if not Sorted then Result := FCount
                else
    if Find(S, Result) then
      case Duplicates of
        dupIgnore:
          Exit;
        dupError:
          Error(SDuplicateString, 0);
      end;
  InsertItem(Result, S);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Changed;

begin
  if (FUpdateCount = 0) and Assigned(FOnChange) then FOnChange(Self);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Changing;

begin
  if (FUpdateCount = 0) and Assigned(FOnChanging) then FOnChanging(Self);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Clear;

begin
  if FCount <> 0 then
  begin
    Changing;
    // this will automatically finalize the array
    FList := nil;
    FCount := 0;
    SetCapacity(0);
    Changed;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Delete(Index: Integer);

begin
  if (Index < 0) or (Index >= FCount) then Error(SListIndexError, Index);
  Changing;
  FList[Index].FString := '';
  Dec(FCount);
  if Index < FCount then System.Move(FList[Index + 1], FList[Index], (FCount - Index) * SizeOf(TWideStringItem));
  Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Exchange(Index1, Index2: Integer);

begin
  if (Index1 < 0) or (Index1 >= FCount) then Error(SListIndexError, Index1);
  if (Index2 < 0) or (Index2 >= FCount) then Error(SListIndexError, Index2);
  Changing;
  ExchangeItems(Index1, Index2);
  Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.ExchangeItems(Index1, Index2: Integer);

var
  Temp: TWideStringItem;

begin
  Temp := FList[Index1];
  FList[Index1] := FList[Index2];
  FList[Index2] := Temp;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.Find(const S: WideString; var Index: Integer): Boolean;

var
  L, H, I, C: Integer;

begin
  Result := False;
  L := 0;
  H := FCount - 1;
  while L <= H do
  begin
    I := (L + H) shr 1;
    C := WideCompareText(FList[I].FString, S, FLanguage);
    if C < 0 then L := I+1
             else
    begin
      H := I - 1;
      if C = 0 then
      begin
        Result := True;
        if Duplicates <> dupAccept then L := I;
      end;
    end;
  end;
  Index := L;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.Get(Index: Integer): WideString;

begin
  if (Index < 0) or (Index >= FCount) then Error(SListIndexError, Index);
  Result := FList[Index].FString;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.GetCapacity: Integer;

begin
  Result := Length(FList);
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.GetCount: Integer;

begin
  Result := FCount;
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.GetObject(Index: Integer): TObject;

begin
  if (Index < 0) or (Index >= FCount) then Error(SListIndexError, Index);
  Result := FList[Index].FObject;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Grow;

var
  Delta,
  Len: Integer;

begin
  Len := Length(FList);
  if Len > 64 then Delta := Len div 4
              else
    if Len > 8 then Delta := 16
               else Delta := 4;
  SetCapacity(Len + Delta);
end;

//----------------------------------------------------------------------------------------------------------------------

function TWideStringList.IndexOf(const S: WideString): Integer;

begin
  if not Sorted then Result := inherited IndexOf(S)
                else
    if not Find(S, Result) then Result := -1;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Insert(Index: Integer; const S: WideString);

begin
  if Sorted then Error(SSortedListError, 0);
  if (Index < 0) or (Index > FCount) then Error(SListIndexError, Index);
  InsertItem(Index, S);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.InsertItem(Index: Integer; const S: WideString);

begin
  Changing;
  if FCount = Length(FList) then Grow;
  if Index < FCount then
    System.Move(FList[Index], FList[Index + 1], (FCount - Index) * SizeOf(TWideStringItem));
  with FList[Index] do
  begin
    Pointer(FString) := nil;
    FObject := nil;
    FString := S;
  end;
  Inc(FCount);
  Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Put(Index: Integer; const S: WideString);

begin
  if Sorted then Error(SSortedListError, 0);
  if (Index < 0) or (Index >= FCount) then Error(SListIndexError, Index);
  Changing;
  FList[Index].FString := S;
  Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.PutObject(Index: Integer; AObject: TObject);

begin
  if (Index < 0) or (Index >= FCount) then Error(SListIndexError, Index);
  Changing;
  FList[Index].FObject := AObject;
  Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.QuickSort(L, R: Integer);

var
  I, J: Integer;
  P: WideString;
  
begin
  repeat
    I := L;
    J := R;
    P := FList[(L + R) shr 1].FString;
    repeat
      while WideCompareText(FList[I].FString, P, FLanguage) < 0 do Inc(I);
      while WideCompareText(FList[J].FString, P, FLanguage) > 0 do Dec(J);
      if I <= J then
      begin
        ExchangeItems(I, J);
        Inc(I);
        Dec(J);
      end;
    until I > J;
    if L < J then QuickSort(L, J);
    L := I;
  until I >= R;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.SetCapacity(NewCapacity: Integer);

begin
  SetLength(FList, NewCapacity);
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.SetSorted(Value: Boolean);

begin
  if FSorted <> Value then
  begin
    if Value then Sort;
    FSorted := Value;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.SetUpdateState(Updating: Boolean);

begin
  if Updating then Changing
              else Changed;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.Sort;

begin
  if not Sorted and (FCount > 1) then
  begin
    Changing;
    QuickSort(0, FCount - 1);
    Changed;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure TWideStringList.SetLanguage(Value: LCID);

begin
  inherited;
  if Sorted then Sort;
end;

//----------------- functions for null terminated wide strings ---------------------------------------------------------

function StrLenW(Str: PWideChar): Cardinal; 

// returns number of characters in a string excluding the null terminator

asm
         MOV EDX, EDI
         MOV EDI, EAX
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         MOV EAX, 0FFFFFFFEH
         SUB EAX, ECX
         MOV EDI, EDX
end;

//----------------------------------------------------------------------------------------------------------------------

function StrEndW(Str: PWideChar): PWideChar; 

// returns a pointer to the end of a null terminated string

asm
         MOV EDX, EDI
         MOV EDI, EAX
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         LEA EAX, [EDI - 2]
         MOV EDI, EDX
end;

//----------------------------------------------------------------------------------------------------------------------

function StrMoveW(Dest, Source: PWideChar; Count: Cardinal): PWideChar; 

// Copies the specified number of characters to the destination string and returns Dest 
// also as result. Dest must have enough room to store at least Count characters.

asm
         PUSH ESI
         PUSH EDI
         MOV ESI, EDX
         MOV EDI, EAX
         MOV EDX, ECX
         CMP EDI, ESI
         JG @@1
         JE @@2
         SHR ECX, 1
         REP MOVSD
         MOV ECX, EDX
         AND ECX, 1
         REP MOVSW
         JMP @@2

@@1:     LEA ESI, [ESI + 2 * ECX - 2]
         LEA EDI, [EDI + 2 * ECX - 2]
         STD
         AND ECX, 1
         REP MOVSW
         SUB EDI, 2
         SUB ESI, 2
         MOV ECX, EDX
         SHR ECX, 1
         REP MOVSD
         CLD
@@2:     POP EDI
         POP ESI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrCopyW(Dest, Source: PWideChar): PWideChar; 

// copies Source to Dest and returns Dest

asm
         PUSH EDI
         PUSH ESI
         MOV ESI, EAX
         MOV EDI, EDX
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         NOT ECX
         MOV EDI, ESI
         MOV ESI, EDX
         MOV EDX, ECX
         MOV EAX, EDI
         SHR ECX, 1
         REP MOVSD
         MOV ECX, EDX
         AND ECX, 1
         REP MOVSW
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrECopyW(Dest, Source: PWideChar): PWideChar; 

// copies Source to Dest and returns a pointer to the null character ending the string

asm
         PUSH EDI
         PUSH ESI
         MOV ESI, EAX
         MOV EDI, EDX
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         NOT ECX
         MOV EDI, ESI
         MOV ESI, EDX
         MOV EDX, ECX
         SHR ECX, 1
         REP MOVSD
         MOV ECX, EDX
         AND ECX, 1
         REP MOVSW
         LEA EAX, [EDI - 2]
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrLCopyW(Dest, Source: PWideChar; MaxLen: Cardinal): PWideChar; 

// copies a specified maximum number of characters from Source to Dest

asm
         PUSH EDI
         PUSH ESI
         PUSH EBX
         MOV ESI, EAX
         MOV EDI, EDX
         MOV EBX, ECX
         XOR AX, AX
         TEST ECX, ECX
         JZ @@1
         REPNE SCASW
         JNE @@1
         INC ECX
@@1:     SUB EBX, ECX
         MOV EDI, ESI
         MOV ESI, EDX
         MOV EDX, EDI
         MOV ECX, EBX
         SHR ECX, 1
         REP MOVSD
         MOV ECX, EBX
         AND ECX, 1
         REP MOVSW
         STOSW
         MOV EAX, EDX
         POP EBX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrPCopyW(Dest: PWideChar; const Source: String): PWideChar;

// copies a Pascal-style string to a null-terminated wide string

begin
  Result := StrPLCopyW(Dest, Source, Length(Source));
  Result[Length(Source)] := WideNull;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrPLCopyW(Dest: PWideChar; const Source: String; MaxLen: Cardinal): PWideChar; 

// copies characters from a Pascal-style string into a null-terminated wide string

asm
       PUSH EDI
       PUSH ESI
       MOV EDI, EAX
       MOV ESI, EDX
       MOV EDX, EAX
       XOR AX, AX
@@1:   LODSB
       STOSW
       DEC ECX
       JNZ @@1
       MOV EAX, EDX
       POP ESI
       POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrCatW(Dest, Source: PWideChar): PWideChar;

// appends a copy of Source to the end of Dest and returns the concatenated string

begin
  StrCopyW(StrEndW(Dest), Source);
  Result := Dest;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrLCatW(Dest, Source: PWideChar; MaxLen: Cardinal): PWideChar; 

// appends a specified maximum number of WideCharacters to string

asm
         PUSH EDI
         PUSH ESI
         PUSH EBX
         MOV EDI, Dest
         MOV ESI, Source
         MOV EBX, MaxLen
         SHL EBX, 1
         CALL StrEndW
         MOV ECX, EDI
         ADD ECX, EBX
         SUB ECX, EAX
         JBE @@1
         MOV EDX, ESI
         SHR ECX, 1
         CALL StrLCopyW
@@1:     MOV EAX, EDI
         POP EBX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrCompW(Str1, Str2: PWideChar): Integer; 

// compares Str1 to Str2 (binary comparation)
// Note: There's also an extended comparation function which uses a given language to
//       compare unicode strings.

asm
         PUSH EDI
         PUSH ESI
         MOV EDI, EDX
         MOV ESI, EAX
         MOV ECX, 0FFFFFFFFH
         XOR EAX, EAX
         REPNE SCASW
         NOT ECX
         MOV EDI, EDX
         XOR EDX, EDX
         REPE CMPSW
         MOV AX, [ESI - 2]
         MOV DX, [EDI - 2]
         SUB EAX, EDX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrICompW(Str1, Str2: PWideChar): Integer; 

// compares Str1 to Str2 without case sensitivity (binary comparation),
// Note: only ANSI characters are compared case insensitively

asm
         PUSH EDI
         PUSH ESI
         MOV EDI, EDX
         MOV ESI, EAX
         MOV ECX, 0FFFFFFFFH
         XOR EAX, EAX
         REPNE SCASW
         NOT ECX
         MOV EDI, EDX
         XOR EDX, EDX
@@1:     REPE CMPSW
         JE @@4
         MOV AX, [ESI - 2]
         CMP AX, 'a'
         JB @@2
         CMP AX, 'z'
         JA @@2
         SUB AL, 20H
@@2:     MOV DX, [EDI - 2]
         CMP DX, 'a'
         JB @@3
         CMP DX, 'z'
         JA @@3
         SUB DX, 20H
@@3:     SUB EAX, EDX
         JE @@1
@@4:     POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrLCompW(Str1, Str2: PWideChar; MaxLen: Cardinal): Integer;

// compares a specified maximum number of charaters in two strings

asm
         PUSH EDI
         PUSH ESI
         PUSH EBX
         MOV EDI, EDX
         MOV ESI, EAX
         MOV EBX, ECX
         XOR EAX, EAX
         OR ECX, ECX
         JE @@1
         REPNE SCASW
         SUB EBX, ECX
         MOV ECX, EBX
         MOV EDI, EDX
         XOR EDX, EDX
         REPE CMPSW
         MOV AX, [ESI - 2]
         MOV DX, [EDI - 2]
         SUB EAX, EDX
@@1:     POP EBX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrLICompW(Str1, Str2: PWideChar; MaxLen: Cardinal): Integer; 

// compares strings up to a specified maximum number of characters, not case sensitive
// Note: only ANSI characters are compared case insensitively

asm
         PUSH EDI
         PUSH ESI
         PUSH EBX
         MOV EDI, EDX
         MOV ESI, EAX
         MOV EBX, ECX
         XOR EAX, EAX
         OR ECX, ECX
         JE @@4
         REPNE SCASW
         SUB EBX, ECX
         MOV ECX, EBX
         MOV EDI, EDX
         XOR EDX, EDX
@@1:     REPE CMPSW
         JE @@4
         MOV AX, [ESI - 2]
         CMP AX, 'a'
         JB @@2
         CMP AX, 'z'
         JA @@2
         SUB AX, 20H
@@2:     MOV DX, [EDI - 2]
         CMP DX, 'a'
         JB @@3
         CMP DX, 'z'
         JA @@3
         SUB DX, 20H
@@3:     SUB EAX, EDX
         JE @@1
@@4:     POP EBX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrNScanW(S1, S2: PWideChar): Integer;

// determines where (in S1) the first time one of the characters of S2 appear.
// The result is the length of a string part of S1 where none of the characters of
// S2 do appear (not counting the trailing #0 and starting with position 0 in S1).

var
  Run: PWideChar;

begin
  Result := -1;
  if Assigned(S1) and Assigned(S2) then
  begin
    Run := S1;
    while (Run^ <> #0) do
    begin
      if StrScanW(S2, Run^) <> nil then Break;
      Inc(Run);
    end;
    Result := Run - S1;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrRNScanW(S1, S2: PWideChar): Integer;

// This function does the same as StrRNScanW but uses S1 in reverse order. This means S1 points to the last
// character of a string, is traveresed reversely and terminates with a starting #0.
// This is useful for parsing strings stored in reversed macro buffers etc.

var
  Run: PWideChar;

begin
  Result := -1;
  if Assigned(S1) and Assigned(S2) then
  begin
    Run := S1;
    while (Run^ <> #0) do
    begin
      if StrScanW(S2, Run^) <> nil then Break;
      Dec(Run);
    end;
    Result := S1 - Run;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrScanW(Str: PWideChar; Chr: WideChar): PWideChar; 

// returns a pointer to first occurrence of a specified character in a string

asm
         PUSH EDI
         PUSH EAX
         MOV EDI, Str
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         NOT ECX
         POP EDI
         MOV AX, Chr
         REPNE SCASW
         MOV EAX, 0
         JNE @@1
         MOV EAX, EDI
         SUB EAX, 2
@@1:     POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrScanW(Str: PWideChar; Chr: WideChar; StrLen: Cardinal): PWideChar;

// returns a pointer to first occurrence of a specified character in a string
// or nil if not found
// Note: this is just a binary search for the specified character and there's no check for
//       a terminating null. Instead at most StrLen characters are searched. This makes
//       this function extremly fast.
//
// on enter EAX contains Str, EDX contains Chr and ECX StrLen
// on exit EAX contains result pointer or nil

asm
         TEST EAX, EAX
         JZ @@Exit                 // get out if the string is nil or StrLen is 0
         JCXZ @@Exit
@@Loop:
         CMP [EAX], DX             // this unrolled loop is actually faster on modern processors
         JE @@Exit                 // than REP SCASW
         INC EAX
         DEC ECX
         JNZ @@Loop
         XOR EAX, EAX
@@Exit:
end;

//----------------------------------------------------------------------------------------------------------------------

function StrRScanW(Str: PWideChar; Chr: WideChar): PWideChar; 

// returns a pointer to the last occurance of Chr in Str

asm
         PUSH EDI
         MOV EDI, Str
         MOV ECX, 0FFFFFFFFH
         XOR AX, AX
         REPNE SCASW
         NOT ECX
         STD
         SUB EDI, 2
         MOV AX, Chr
         REPNE SCASW
         MOV EAX, 0
         JNE @@1
         MOV EAX, EDI
         ADD EAX, 2
@@1:     CLD
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrPosW(Str, SubStr: PWideChar): PWideChar; 

// returns a pointer to the first occurance of SubStr in Str

asm
         PUSH EDI
         PUSH ESI
         PUSH EBX
         OR EAX, EAX
         JZ @@2
         OR EDX, EDX
         JZ @@2
         MOV EBX, EAX
         MOV EDI, EDX
         XOR AX, AX
         MOV ECX, 0FFFFFFFFH
         REPNE SCASW
         NOT ECX
         DEC ECX
         JZ @@2
         MOV ESI, ECX
         MOV EDI, EBX
         MOV ECX, 0FFFFFFFFH
         REPNE SCASW
         NOT ECX
         SUB ECX, ESI
         JBE @@2
         MOV EDI, EBX
         LEA EBX, [ESI - 1] // Note: 2 would be wrong here, we are dealing with numbers not an address
@@1:     MOV ESI, EDX
         LODSW
         REPNE SCASW
         JNE @@2
         MOV EAX, ECX
         PUSH EDI
         MOV ECX, EBX
         REPE CMPSW
         POP EDI
         MOV ECX, EAX
         JNE @@1
         LEA EAX, [EDI - 2]
         JMP @@3

@@2:     XOR EAX, EAX
@@3:     POP EBX
         POP ESI
         POP EDI
end;

//----------------------------------------------------------------------------------------------------------------------

function StrUpperW(Str: PWideChar): PWideChar;

// converts Str to upper case and returns it

begin
  Result := Str;
  while Str^ <> WideNull do
  begin
    Str^ := WideChar(UnicodeToUpper(Word(Str^)));
    Inc(Str);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrLowerW(Str: PWideChar): PWideChar;

// converts Str to lower case and returns it

begin
  Result := Str;
  while Str^ <> WideNull do
  begin
    Str^ := WideChar(UnicodeToLower(Word(Str^)));
    Inc(Str);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrTitleW(Str: PWideChar): PWideChar;

// converts Str to title case and returns it

begin
  Result := Str;
  while Str^ <> WideNull do
  begin
    Str^ := WideChar(UnicodeToTitle(Word(Str^)));
    Inc(Str);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrAllocW(Size: Cardinal): PWideChar;

// Allocates a buffer for a null-terminated wide string and returns a pointer
// to the first character of the string.

begin
  Size := SizeOf(WideChar) * Size + SizeOf(Cardinal);
  GetMem(Result, Size);
  FillChar(Result^, Size, 0);
  Cardinal(Pointer(Result)^) := Size;
  Inc(Result, SizeOf(Cardinal) div SizeOf(WideChar));
end;

//----------------------------------------------------------------------------------------------------------------------

function StrBufSizeW(Str: PWideChar): Cardinal;

// Returns max number of wide characters that can be stored in a buffer allocated by StrAllocW.

begin
  Dec(Str, SizeOf(Cardinal) div SizeOf(WideChar));
  Result := (Cardinal(Pointer(Str)^) - SizeOf(Cardinal)) div 2;
end;

//----------------------------------------------------------------------------------------------------------------------

function StrNewW(Str: PWideChar): PWideChar;

// Duplicates the given string (if not nil) and returns the address of the new string.

var
  Size: Cardinal;

begin
  if Str = nil then Result := nil
               else
  begin
    Size := StrLenW(Str) + 1;
    Result := StrMoveW(StrAllocW(Size), Str, Size);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure StrDisposeW(Str: PWideChar);

// releases a string allocated with StrNew.

begin
  if Str <> nil then
  begin
    Dec(Str, SizeOf(Cardinal) div SizeOf(WideChar));
    FreeMem(Str, Cardinal(Pointer(Str)^));
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

procedure StrSwapByteOrder(Str: PWideChar); 

// exchanges in each character of the given string the low order and high order
// byte to go from LSB to MSB and vice versa.
// EAX contains address of string

asm
         PUSH ESI
         PUSH EDI
         MOV ESI, EAX
         MOV EDI, ESI
         XOR EAX, EAX  // clear high order byte to be able to use 32bit operand below
@@1:     LODSW
         OR EAX, EAX
         JZ @@2
         XCHG AL, AH
         STOSW
         JMP @@1
         
@@2:     POP EDI
         POP ESI
end;

//----------------------------------------------------------------------------------------------------------------------

function WideAdjustLineBreaks(const S: WideString): WideString;

var
  Source,
  SourceEnd,
  Dest: PWideChar;
  Extra: Integer;

begin
  Source := Pointer(S);
  SourceEnd := Source + Length(S);
  Extra := 0;
  while Source < SourceEnd do
  begin
    case Source^ of
      LF:
        Inc(Extra);
      CR:
        if Source[1] = LineFeed then Inc(Source)
                                else Inc(Extra);
    end;
    Inc(Source);
  end;

  Source := Pointer(S);
  SetString(Result, nil, SourceEnd - Source + Extra);
  Dest := Pointer(Result);
  while Source < SourceEnd do
    case Source^ of
      LineFeed:
        begin
          Dest^ := LineSeparator;
          Inc(Dest);
          Inc(Source);
        end;
      CarriageReturn:
        begin
          Dest^ := LineSeparator;
          Inc(Dest);
          Inc(Source);
          if Source^ = LineFeed then Inc(Source);
        end;
    else
      Dest^ := Source^;
      Inc(Dest);
      Inc(Source);
    end;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideQuotedStr(const S: WideString; Quote: WideChar): WideString;

// works like QuotedStr from SysUtils.pas but can insert any quotation character

var
  P, Src,
  Dest: PWideChar;
  AddCount: Integer;

begin
  AddCount := 0;
  P := StrScanW(PWideChar(S), Quote);
  while Assigned(P) do
  begin
    Inc(P);
    Inc(AddCount);
    P := StrScanW(P, Quote);
  end;

  if AddCount = 0 then Result := Quote + S + Quote
                  else
  begin
    SetLength(Result, Length(S) + AddCount + 2);
    Dest := PWideChar(Result);
    Dest^ := Quote;
    Inc(Dest);
    Src := PWideChar(S);
    P := StrScanW(Src, Quote);
    repeat
      Inc(P);
      Move(Src^, Dest^, P - Src);
      Inc(Dest, P - Src);
      Dest^ := Quote;
      Inc(Dest);
      Src := P;
      P := StrScanW(Src, Quote);
    until P = nil;
    P := StrEndW(Src);
    Move(Src^, Dest^, P - Src);
    Inc(Dest, P - Src);
    Dest^ := Quote;
  end;  
end;

//----------------------------------------------------------------------------------------------------------------------

function WideExtractQuotedStr(var Src: PWideChar; Quote: WideChar): WideString;

// extracts a string enclosed in quote characters given by Quote

var
  P, Dest: PWideChar;
  DropCount: Integer;
    
begin
  Result := '';
  if (Src = nil) or (Src^ <> Quote) then Exit;

  Inc(Src);
  DropCount := 1;
  P := Src;
  Src := StrScanW(Src, Quote);

  while Assigned(Src) do   // count adjacent pairs of quote chars
  begin
    Inc(Src);
    if Src^ <> Quote then Break;
    Inc(Src);
    Inc(DropCount);
    Src := StrScanW(Src, Quote);
  end;

  if Src = nil then Src := StrEndW(P);
  if (Src - P) <= 1 then Exit;
  
  if DropCount = 1 then SetString(Result, P, Src - P - 1)
                   else
  begin
    SetLength(Result, Src - P - DropCount);
    Dest := PWideChar(Result);
    Src := StrScanW(P, Quote);
    while Assigned(Src) do
    begin
      Inc(Src);
      if Src^ <> Quote then Break;
      Move(P^, Dest^, Src - P);
      Inc(Dest, Src - P);
      Inc(Src);
      P := Src;
      Src := StrScanW(Src, Quote);
    end;
    if Src = nil then Src := StrEndW(P);
    Move(P^, Dest^, Src - P - 1);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideStringOfChar(C: WideChar; Count: Cardinal): WideString;

// returns a string of Count characters filled with C

var
  I: Integer;

begin
  SetLength(Result, Count);
  for I := 1 to Count do Result[I] := C;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideTrim(const S: WideString): WideString;

var
  I, L: Integer;

begin
  L := Length(S);
  I := 1;
  while (I <= L) and
        (UnicodeIsWhiteSpace(Word(S[I])) or UnicodeIsControl(Word(S[I]))) do Inc(I);
  if I > L then Result := ''
           else
  begin
    while UnicodeIsWhiteSpace(Word(S[L])) or UnicodeIsControl(Word(S[L])) do Dec(L);
    Result := Copy(S, I, L - I + 1);
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideTrimLeft(const S: WideString): WideString;

var
  I, L: Integer;

begin
  L := Length(S);
  I := 1;
  while (I <= L) and
        (UnicodeIsWhiteSpace(Word(S[I])) or UnicodeIsControl(Word(S[I]))) do Inc(I);
  Result := Copy(S, I, Maxint);
end;

//----------------------------------------------------------------------------------------------------------------------

function WideTrimRight(const S: WideString): WideString;

var
  I: Integer;

begin
  I := Length(S);
  while (I > 0) and
        (UnicodeIsWhiteSpace(Word(S[I])) or UnicodeIsControl(Word(S[I]))) do Dec(I);
  Result := Copy(S, 1, I);
end;

//----------------------------------------------------------------------------------------------------------------------

function WideCharPos(const S: WideString; const Ch: WideChar; const Index: Integer): Integer;

// returns the index of character Ch in S, starts searching at index Index
// Note: This is a quick memory search. No attempt is made to interpret either the given
//       charcter nor the string (ligatures, modifiers, surrogates etc.)

asm
              TEST    EAX,EAX           // make sure we are not null
              JZ      @StrIsNil
              DEC     ECX               // make index zero based
              JL      @IdxIsSmall
              PUSH    EBX
              PUSH    EDI
              MOV     EDI,EAX           // EDI := S
              XOR     EAX,EAX
              MOV     AX, DX            // AX := Ch
              MOV     EDX,[EDI-4]       // EDX := Length(S) * 2
              SHR     EDX,1             // EDX := EDX div 2
              MOV     EBX,EDX           // save the length to calc. result
              SUB     EDX,ECX           // EDX = EDX - Index = # of chars to scan
              JLE     @IdxIsBig
              ADD     EDI,ECX           // point to index'th char
              MOV     ECX,EDX           // loop counter
              CLD
              REPNE   SCASW
              JNE     @NoMatch
              MOV     EAX,EBX           // result := saved length -
              SUB     EAX,ECX           // loop counter value
              POP     EDI
              POP     EBX
              RET
@IdxIsBig:
@NoMatch:
              XOR     EAX,EAX
              POP     EDI
              POP     EBX
              RET
@IdxIsSmall:
              XOR     EAX,EAX
@StrIsNil:
end;

//----------------------------------------------------------------------------------------------------------------------

function WideCompose(const S: WideString): WideString;

// returns a string with all characters of S but if there is a possibility to combine characters
// then they are composed

var
  I: Integer;

begin
  for I := 1 to Length(S) do
  begin
    //UnicodeCompose(
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideComposeHangul(Source: WideString): WideString;

var
  Len: Integer;
  Ch,
  Last: WideChar;
  I, J: Integer;
  LINdex,
  VIndex,
  SIndex,
  TIndex: Integer;

begin
  // copy first char
  Len := Length(Source);
  if Len > 0 then
  begin
    // allocate memory only once and shorten the result when done
    SetLength(Result, Len);
    J := 1;
    Last := Source[J];
    Result := Last;

    for I := 2 to Len do
    begin
      Ch := Source[I];

      // 1. check to see if two current characters are L and V
      LIndex := Word(Last) - LBase;
      if (0 <= LIndex) and (LIndex < LCount) then
      begin
        VIndex := Word(Ch) - VBase;
        if (0 <= VIndex) and (VIndex < VCount) then
        begin
          // make syllable of form LV
          Last := WideChar((SBase + (LIndex * VCount + VIndex) * TCount));
          Result[J] := Last; // reset last
          Continue; // discard Ch
        end;
      end;

      // 2. check to see if two current characters are LV and T
      SIndex := Word(Last) - SBase;
      if (0 <= SIndex) and (SIndex < SCount) and ((SIndex mod TCount) = 0) then
      begin
        TIndex := Word(Ch) - TBase;
        if (0 <= TIndex) and (TIndex <= TCount) then
        begin
          // make syllable of form LVT

          Inc(Word(Last), TIndex);
          Result[J] := Last; // reset last
          Continue; // discard ch
        end;
      end;

      // if neither case was true, just add the character
      Last := Ch;
      Inc(J);
      Result[J] := Ch;
    end;
    // shorten the result to real length
    SetLength(Result, J);
  end
  else Result := '';
end;

//----------------------------------------------------------------------------------------------------------------------

function WideDecompose(const S: WideString): WideString;

// returns a string with all characters of S but decomposed, e.g. Ê is returned as E^ etc.

var
  I, J, K: Integer;
  CClass: Cardinal;
  Decomp: TCardinalArray;

begin
  Result := '';
  Decomp := nil;
  for I := 1 to Length(S) do
  begin
    // no need to dive iteratively into decompositions as this is already done
    // on creation of the data used to lookup the decomposition
    Decomp := UnicodeDecompose(Word(S[I]));
    // We need to sort the returned values according to their canonical class.
    for J := 0 to High(Decomp) do
    begin
      CClass := UnicodeCanonicalClass(Decomp[J]);
      if CClass = 0 then Result := Result + WideChar(Decomp[J])
                    else
      begin
        K := Length(Result);
        // bubble-sort combining marks as necessary
        while K > 1 do
        begin
          if UnicodeCanonicalClass(Word(Result[K])) <= CClass then Break;
          Dec(K);
        end;
        Insert(WideChar(Decomp[J]), Result, K + 1);
      end;
    end;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function WideLoCase(C: WideChar): WideChar;

begin
  Result := WideChar(UnicodeToLower(Word(C)));
end;

//----------------------------------------------------------------------------------------------------------------------

function WideLowerCase(const S: WideString): WideString;

var
  I: Integer;

begin
  Result := S;
  for I := 1 to Length(S) do
    Result[I] := WideChar(UnicodeToLower(Word(Result[I])));
end;

//----------------------------------------------------------------------------------------------------------------------

function WideTitleCaseChar(C: WideChar): WideChar;

begin
  Result := WideChar(UnicodeToTitle(Word(C)));
end;

//----------------------------------------------------------------------------------------------------------------------

function WideTitleCaseString(const S: WideString): WideString;

var
  I: Integer;

begin
  Result := S;
  for I := 1 to Length(S) do
    Result[I] := WideChar(UnicodeToTitle(Word(Result[I])));
end;

//----------------------------------------------------------------------------------------------------------------------

function WideUpCase(C: WideChar): WideChar;

begin
  Result := WideChar(UnicodeToUpper(Word(C)));
end;

//----------------------------------------------------------------------------------------------------------------------

function WideUpperCase(const S: WideString): WideString;

var
  I: Integer;

begin
  Result := S;
  for I := 1 to Length(S) do
    Result[I] := WideChar(UnicodeToUpper(Word(Result[I])));
end;

//----------------- character test routines ----------------------------------------------------------------------------

// Is the character alphabetic?
function UnicodeIsAlpha(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_LU or UC_LL or UC_LM or UC_LO or UC_LT, 0); end;
// Is the character a digit?
function UnicodeIsDigit(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ND, 0); end;
// Is the character alphabetic or a number?
function UnicodeIsAlphaNum(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_LU or UC_LL or UC_LM or UC_LO or UC_LT or UC_ND, 0); end;
// Is the character a control character?
function UnicodeIsControl(C: UCS4): Boolean; begin Result := IsProperty(C, UC_CC or UC_CF, 0); end;
// Is the character a spacing character?
function UnicodeIsSpace(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ZS or UC_SS, 0); end;
// Is the character a white space character (same as UnicodeIsSpace plus tabulator, new line etc.)?
function UnicodeIsWhiteSpace(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ZS or UC_SS, UC_WS or UC_S); end;
// Is the character a space separator?
function UnicodeIsBlank(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ZS, 0); end;
// Is the character a punctuation mark?
function UnicodeIsPunctuation(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_PD or UC_PS or UC_PE or UC_PO, UC_PI or UC_PF); end;
// Is the character graphical?
function UnicodeIsGraph(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_MN or UC_MC or UC_ME or UC_ND or UC_NL or UC_NO or
                           UC_LU or UC_LL or UC_LT or UC_LM or UC_LO or UC_PC or UC_PD or
                           UC_PS or UC_PE or UC_PO or UC_SM or UC_SM or UC_SC or UC_SK or
                           UC_SO, UC_PI or UC_PF); end;
// Is the character printable?
function UnicodeIsPrintable(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_MN or UC_MC or UC_ME or UC_ND or UC_NL or UC_NO or
                           UC_LU or UC_LL or UC_LT or UC_LM or UC_LO or UC_PC or UC_PD or
                           UC_PS or UC_PE or UC_PO or UC_SM or UC_SM or UC_SC or UC_SK or
                           UC_SO or UC_ZS, UC_PI or UC_PF); end;
// Is the character already upper case?
function UnicodeIsUpper(C: UCS4): Boolean; begin Result := IsProperty(C, UC_LU, 0); end;
// Is the character already lower case?
function UnicodeIsLower(C: UCS4): Boolean; begin Result := IsProperty(C, UC_LL, 0); end;
// Is the character already title case?
function UnicodeIsTitle(C: UCS4): Boolean; begin Result := IsProperty(C, UC_LT, 0); end;
// Is the character a hex digit?
function UnicodeIsHexDigit(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_HD); end;

// Is the character a C0 control character (< 32)?
function UnicodeIsIsoControl(C: UCS4): Boolean; begin Result := IsProperty(C, UC_CC, 0); end;
// Is the character a format control character?
function UnicodeIsFormatControl(C: UCS4): Boolean; begin Result := IsProperty(C, UC_CF, 0); end;

// Is the character a symbol?
function UnicodeIsSymbol(C: UCS4): Boolean; begin Result := IsProperty(C, UC_SM or UC_SC or UC_SO or UC_SK, 0); end;
// Is the character a number or digit?
function UnicodeIsNumber(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ND or UC_NO or UC_NL, 0); end;
// Is the character non-spacing? 
function UnicodeIsNonSpacing(C: UCS4): Boolean; begin Result := IsProperty(C, UC_MN, 0); end;
// Is the character an open/left punctuation (i.e. '[')?
function UnicodeIsOpenPunctuation(C: UCS4): Boolean; begin Result := IsProperty(C, UC_PS, 0); end;
// Is the character an close/right punctuation (i.e. ']')?
function UnicodeIsClosePunctuation(C: UCS4): Boolean; begin Result := IsProperty(C, UC_PE, 0); end;
// Is the character an initial punctuation (i.e. U+2018 LEFT SINGLE QUOTATION MARK)?
function UnicodeIsInitialPunctuation(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_PI); end;
// Is the character a final punctuation (i.e. U+2019 RIGHT SINGLE QUOTATION MARK)?
function UnicodeIsFinalPunctuation(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_PF); end;

// Can the character be decomposed into a set of other characters?
function UnicodeIsComposite(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_CM); end;
// Is the character one of the many quotation marks?
function UnicodeIsQuotationMark(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_QM); end;
// Is the character one that has an opposite form (i.e. <>)?
function UnicodeIsSymmetric(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_SY); end;
// Is the character mirroring (superset of symmetric)?
function UnicodeIsMirroring(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_MR); end;
// Is the character non-breaking (i.e. non-breaking space)?
function UnicodeIsNonBreaking(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_NB); end;

// Directionality functions
// Does the character have strong right-to-left directionality (i.e. Arabic letters)?
function UnicodeIsRTL(C: UCS4): Boolean; begin Result := IsProperty(C, UC_R, 0); end;
// Does the character have strong left-to-right directionality (i.e. Latin letters)?
function UnicodeIsLTR(C: UCS4): Boolean; begin Result := IsProperty(C, UC_L, 0); end;
// Does the character have strong directionality?
function UnicodeIsStrong(C: UCS4): Boolean; begin Result := IsProperty(C, UC_L or UC_R, 0); end;
// Does the character have weak directionality (i.e. numbers)?
function UnicodeIsWeak(C: UCS4): Boolean; begin Result := IsProperty(C, UC_EN or UC_ES, UC_ET or UC_AN or UC_CS); end;
// Does the character have neutral directionality (i.e. whitespace)?
function UnicodeIsNeutral(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_B or UC_S or UC_WS or UC_ON); end;
// Is the character a block or segment separator?
function UnicodeIsSeparator(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_B or UC_S); end;

// Other functions inspired by John Cowan.
// Is the character a mark of some kind?
function UnicodeIsMark(C: UCS4): Boolean; begin Result := IsProperty(C, UC_MN or UC_MC or UC_ME, 0); end;
// Is the character a modifier letter?
function UnicodeIsModifier(C: UCS4): Boolean; begin Result := IsProperty(C, UC_LM, 0); end;
// Is the character a number represented by a letter?
function UnicodeIsLetterNumber(C: UCS4): Boolean; begin Result := IsProperty(C, UC_NL, 0); end;
// Is the character connecting punctuation?
function UnicodeIsConnectionPunctuation(C: UCS4): Boolean; begin Result := IsProperty(C, UC_PC, 0); end;
// Is the character a dash punctuation?
function UnicodeIsDash(C: UCS4): Boolean; begin Result := IsProperty(C, UC_PD, 0); end;
// Is the character a math character?
function UnicodeIsMath(C: UCS4): Boolean; begin Result := IsProperty(C, UC_SM, 0); end;
// Is the character a currency character?
function UnicodeIsCurrency(C: UCS4): Boolean; begin Result := IsProperty(C, UC_SC, 0); end;
// Is the character a modifier symbol?
function UnicodeIsModifierSymbol(C: UCS4): Boolean; begin Result := IsProperty(C, UC_SK, 0); end;
// Is the character a non-spacing mark?
function UnicodeIsNonSpacingMark(C: UCS4): Boolean; begin Result := IsProperty(C, UC_MN, 0); end;
// Is the character a spacing mark?
function UnicodeIsSpacingMark(C: UCS4): Boolean; begin Result := IsProperty(C, UC_MC, 0); end;
// Is the character enclosing (i.e. enclosing box)?
function UnicodeIsEnclosing(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ME, 0); end;
// Is the character from the Private Use Area?
function UnicodeIsPrivate(C: UCS4): Boolean; begin Result := IsProperty(C, UC_CO, 0); end;
// Is the character one of the surrogate codes?
function UnicodeIsSurrogate(C: UCS4): Boolean; begin Result := IsProperty(C, UC_OS, 0); end;
// Is the character a line separator?
function UnicodeIsLineSeparator(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ZL, 0); end;
// Is th character a paragraph separator;
function UnicodeIsParagraphSeparator(C: UCS4): Boolean; begin Result := IsProperty(C, UC_ZP, 0); end;

// Can the character begin an identifier?
function UnicodeIsIdenifierStart(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_LU or UC_LL or UC_LT or UC_LO or UC_NL, 0); end;
// Can the character appear in an identifier?
function UnicodeIsIdentifierPart(C: UCS4): Boolean;
begin Result := IsProperty(C, UC_LU or UC_LL or UC_LT or UC_LO or UC_NL or UC_MN or
                           UC_MC or UC_ND or UC_PC or UC_CF, 0); end;

// Is the character defined (appears in one of the data files)?
function UnicodeIsDefined(C: UCS4): Boolean; begin Result := IsProperty(C, 0, UC_CP); end;
// Is the character not defined (non-Unicode)?
function UnicodeIsUndefined(C: UCS4): Boolean; begin Result := not IsProperty(C, 0, UC_CP); end;

// Other miscellaneous character property functions.
// Is the character a Han ideograph?
function UnicodeIsHan(C: UCS4): Boolean;
begin Result := ((C >= $4E00) and (C <= $9FFF))  or ((C >= $F900) and (C <= $FAFF)); end;

// Is the character a pre-composed Hangul syllable?
function UnicodeIsHangul(C: UCS4): Boolean;
begin Result := (C >= $AC00) and (C <= $D7FF); end;

//----------------------------------------------------------------------------------------------------------------------

function CodePageFromLocale(Language: LCID): Integer;

// determines the code page for a given locale

var
  Buf: array[0..6] of Char;

begin
  GetLocaleInfo(Language, LOCALE_IDefaultAnsiCodePage, Buf, 6);
  Result := StrToIntDef(Buf, GetACP);
end;

//----------------------------------------------------------------------------------------------------------------------

function KeyboardCodePage: Word;

begin
  Result := CodePageFromLocale(GetKeyboardLayout(0) and $FFFF);
end;

//----------------------------------------------------------------------------------------------------------------------

function KeyUnicode(C: Char): WideChar;

// converts the given character (as it comes with a WM_CHAR message) into its corresponding
// Unicode character depending on the active keyboard layout

begin
  MultiByteToWideChar(KeyboardCodePage, MB_USEGLYPHCHARS, @C, 1, @Result, 1);
end;

//----------------------------------------------------------------------------------------------------------------------

function CodeBlockFromChar(const C: WideChar): Cardinal;

// returns the Unicode code block to which C belongs

begin
  case C of
    #$0000..#$007F: // Basic Latin
      Result := 0;
    #$0080..#$00FF: // Latin-1 Supplement
      Result := 1;
    #$0100..#$017F: // Latin Extended-A
      Result := 2;
    #$0180..#$024F: // Latin Extended-B
      Result := 3;
    #$0250..#$02AF: // IPA Extensions
      Result := 4;
    #$02B0..#$02FF: // Spacing Modifier Letters
      Result := 5;
    #$0300..#$036F: // Combining Diacritical Marks
      Result := 6;
    #$0370..#$03FF: // Greek
      Result := 7;
    #$0400..#$04FF: // Cyrillic
      Result := 8;
    #$0530..#$058F: // Armenian
      Result := 9;
    #$0590..#$05FF: // Hebrew
      Result := 10;
    #$0600..#$06FF: // Arabic
      Result := 11;
    #$0900..#$097F: // Devanagari
      Result := 12;
    #$0980..#$09FF: // Bengali
      Result := 13;
    #$0A00..#$0A7F: // Gurmukhi
      Result := 14;
    #$0A80..#$0AFF: // Gujarati
      Result := 15;
    #$0B00..#$0B7F: // Oriya
      Result := 16;
    #$0B80..#$0BFF: // Tamil
      Result := 17;
    #$0C00..#$0C7F: // Telugu
      Result := 18;
    #$0C80..#$0CFF: // Kannada
      Result := 19;
    #$0D00..#$0D7F: // Malayalam
      Result := 20;
    #$0E00..#$0E7F: // Thai
      Result := 21;
    #$0E80..#$0EFF: // Lao
      Result := 22;
    #$0F00..#$0FBF: // Tibetan
      Result := 23;
    #$10A0..#$10FF: // Georgian
      Result := 24;
    #$1100..#$11FF: // Hangul Jamo
      Result := 25;
    #$1E00..#$1EFF: // Latin Extended Additional
      Result := 26;
    #$1F00..#$1FFF: // Greek Extended
      Result := 27;
    #$2000..#$206F: // General Punctuation
      Result := 28;
    #$2070..#$209F: // Superscripts and Subscripts
      Result := 29;
    #$20A0..#$20CF: // Currency Symbols
      Result := 30;
    #$20D0..#$20FF: // Combining Marks for Symbols
      Result := 31;
    #$2100..#$214F: // Letterlike Symbols
      Result := 32;
    #$2150..#$218F: // Number Forms
      Result := 33;
    #$2190..#$21FF: // Arrows
      Result := 34;
    #$2200..#$22FF: // Mathematical Operators
      Result := 35;
    #$2300..#$23FF: // Miscellaneous Technical
      Result := 36;
    #$2400..#$243F: // Control Pictures
      Result := 37;
    #$2440..#$245F: // Optical Character Recognition
      Result := 38;
    #$2460..#$24FF: // Enclosed Alphanumerics
      Result := 39;
    #$2500..#$257F: // Box Drawing
      Result := 40;
    #$2580..#$259F: // Block Elements
      Result := 41;
    #$25A0..#$25FF: // Geometric Shapes
      Result := 42;
    #$2600..#$26FF: // Miscellaneous Symbols
      Result := 43;
    #$2700..#$27BF: // Dingbats
      Result := 44;
    #$3000..#$303F: // CJK Symbols and Punctuation
      Result := 45;
    #$3040..#$309F: // Hiragana
      Result := 46;
    #$30A0..#$30FF: // Katakana
      Result := 47;
    #$3100..#$312F: // Bopomofo
      Result := 48;
    #$3130..#$318F: // Hangul Compatibility Jamo
      Result := 49;
    #$3190..#$319F: // Kanbun
      Result := 50;
    #$3200..#$32FF: // Enclosed CJK Letters and Months
      Result := 51;
    #$3300..#$33FF: // CJK Compatibility
      Result := 52;
    #$4E00..#$9FFF: // CJK Unified Ideographs
      Result := 53;
    #$AC00..#$D7A3: // Hangul Syllables
      Result := 54;
    #$D800..#$DB7F: // High Surrogates
      Result := 55;
    #$DB80..#$DBFF: // High Private Use Surrogates
      Result := 56;
    #$DC00..#$DFFF: // Low Surrogates
      Result := 57;
    #$E000..#$F8FF: // Private Use
      Result := 58;
    #$F900..#$FAFF: // CJK Compatibility Ideographs
      Result := 59;
    #$FB00..#$FB4F: // Alphabetic Presentation Forms
      Result := 60;
    #$FB50..#$FDFF: // Arabic Presentation Forms-A
      Result := 61;
    #$FE20..#$FE2F: // Combining Half Marks
      Result := 62;
    #$FE30..#$FE4F: // CJK Compatibility Forms
      Result := 63;
    #$FE50..#$FE6F: // Small Form Variants
      Result := 64;
    #$FE70..#$FEFF: // Arabic Presentation Forms-B
      Result := 65;
    #$FF00..#$FFEF: // Halfwidth and Fullwidth Forms
      Result := 66;
  else
    // #$FFF0..#$FFFF Specials
    Result := 67;
  end;
end;

//----------------------------------------------------------------------------------------------------------------------

function CodePageToWideString(A: AnsiString; CodePage: Word): WideString;

begin
  SetLength(Result, Length(A));
  MultiByteToWideChar(CodePage, 0, PChar(A), Length(A), PWideChar(Result), Length(A) * 2);
end;

//----------------------------------------------------------------------------------------------------------------------

function CompareTextWin95(W1, W2: WideString; Locale: LCID): Integer;

// special comparation function for Win9x since there's no system defined comparation function,
// returns -1 if W1 < W2, 0 if W1 = W2 or 1 if W1 > W2

var
  S1, S2: String;
  CP: Integer;
  L1, L2: Integer;

begin
  L1 := Length(W1);
  L2 := Length(W2);
  SetLength(S1, L1);
  SetLength(S2, L2);
  CP := CodePageFromLocale(Locale);
  WideCharToMultiByte(CP, 0, PWideChar(W1), L1, PChar(S1), L1, nil, nil);
  WideCharToMultiByte(CP, 0, PWideChar(W2), L2, PChar(S2), L2, nil, nil);
  Result := CompareStringA(Locale, NORM_IGNORECASE, PChar(S1), Length(S1), PChar(S2), Length(S2)) - 2;
end;
                                              
//----------------------------------------------------------------------------------------------------------------------

function CompareTextWinNT(W1, W2: WideString; Locale: LCID): Integer;

// Wrapper function for WinNT since there's no system defined comparation function in Win9x and
// we need a central comparation function for TWideStringList.
// Returns -1 if W1 < W2, 0 if W1 = W2 or 1 if W1 > W2

begin
  Result := CompareStringW(Locale, NORM_IGNORECASE, PWideChar(W1), Length(W1), PWideChar(W2), Length(W2)) - 2;
end;

//----------------- Conversion routines --------------------------------------------------------------------------------

const
  halfShift: Integer = 10;

  halfBase: UCS4 = $0010000;
  halfMask: UCS4 = $3FF;

  offsetsFromUTF8: array[0..5] of UCS4 = ($00000000, $00003080, $000E2080, $03C82080, $FA082080, $82082080);

  bytesFromUTF8: array[0..255] of Byte = (
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
	1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
	2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2, 3,3,3,3,3,3,3,3,4,4,4,4,5,5,5,5);

  firstByteMark: array[0..6] of Byte = ($00, $00, $C0, $E0, $F0, $F8, $FC);

//----------------------------------------------------------------------------------------------------------------------

function WideStringToUTF8(S: WideString): AnsiString;

var
  ch: UCS4;
  L, J, T,
  bytesToWrite: Word;
  byteMask: UCS4;
  byteMark: UCS4;

begin
  if Length(S) = 0 then
  begin
    Result := '';
    Exit;
  end;

  SetLength(Result, Length(S) * 6); // assume worst case
  T := 1;
  for J := 1 to Length(S) do
  begin
    byteMask := $BF;
    byteMark := $80;

    ch := UCS4(S[J]);

    if ch < $80 then
      bytesToWrite := 1
    else
    if ch < $800 then
      bytesToWrite := 2
    else
    if ch < $10000 then
      bytesToWrite := 3
    else
    if ch < $200000 then
      bytesToWrite := 4
    else
    if ch < $4000000 then
      bytesToWrite := 5
    else
    if ch <= MaximumUCS4 then
      bytesToWrite := 6
    else
    begin
      bytesToWrite := 2;
      ch := ReplacementCharacter;
    end;

    for L := bytesToWrite downto 2 do
    begin
      Result[T + L - 1] := Char((ch or byteMark) and byteMask);
      ch := ch shr 6;
    end;
    Result[T] := Char(ch or firstByteMark[bytesToWrite]);
    Inc(T, bytesToWrite);
  end;
  SetLength(Result, T - 1); // assume worst case
end;

//----------------------------------------------------------------------------------------------------------------------

function UTF8ToWideString(S: AnsiString): WideString;

var
  L, J, T: Cardinal;
  ch: UCS4;
  extraBytesToWrite: Word;

begin
  if Length(S) = 0 then
  begin
    Result := '';
    Exit;
  end;

  SetLength(Result, Length(S)); // create enough room

  L := 1;
  T := 1;
  while L <= Cardinal(Length(S)) do
  begin
    ch := 0;
    extraBytesToWrite := bytesFromUTF8[Ord(S[L])];

    for J := extraBytesToWrite downto 1 do
    begin
      ch := ch + Ord(S[L]);
      Inc(L);
      ch := ch shl 6;
    end;
    ch := ch + Ord(S[L]);
    Inc(L);
    ch := ch - offsetsFromUTF8[extraBytesToWrite];

    if ch <= MaximumUCS2 then
    begin
      Result[T] := WideChar(ch);
      Inc(T);
    end
    else
    if ch > MaximumUCS4 then
    begin
      Result[T] := WideChar(ReplacementCharacter);
      Inc(T);
    end
    else
    begin
      ch := ch - halfBase;
      Result[T] := WideChar((ch shr halfShift) + SurrogateHighStart);
      Inc(T);
      Result[T] := WideChar((ch and halfMask) + SurrogateLowStart);
      Inc(T);
    end;
  end;
  SetLength(Result, T - 1); // now fix up length
end;

//----------------------------------------------------------------------------------------------------------------------

initialization
  if (Win32Platform and VER_PLATFORM_WIN32_NT) <> 0 then @WideCompareText := @CompareTextWinNT
                                                    else @WideCompareText := @CompareTextWin95;
finalization
  LoadInProgress.Free;
end.
