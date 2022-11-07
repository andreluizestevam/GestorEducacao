{$I QRPP.INC}

{$B-}
{$IFDEF WIN32}
  {$A-,H-}
{$ENDIF}
unit wwqrgrid;

interface

{$IFDEF WIN32}
  {$R WWQRGRID.RES}
{$ELSE}
  {$R WWQRGRID.R16}
{$ENDIF}


uses
  Grids, Wwdbigrd, Wwdbgrid, WwTable, Wwquery, Wwqbe, Wwcommon,
  {$IFDEF WIN32} ComCtrls, {$ENDIF}
  {$IFDEF QREXPORTS2} grImgCtrl, {$ENDIF}
  Controls, SysUtils, Forms,
  DBGrids, WinProcs, WinTypes, Classes,
  QuickRpt, QRCtrls, csProp, Graphics,
  QRPExpr, QRPCtrls;

type
  TwwQRGridReport = class(TQRAbstractGridReport)
  private
    FDBGrid                      : TwwDBGrid;
    FUseExprForDatafields        : Boolean;
    FAutoStretchMemos            : Boolean;
    FSpanPagesHorizontally       : Boolean;
  protected
    {$IFDEF WIN32}
    procedure DetailBandBeforePrint(Sender: TQRCustomBand;
                var PrintBand: Boolean); virtual;
    {$ENDIF}
  public
    function CreateReportFromGrid: Boolean; override;
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  published
    property AutoStretchMemos: Boolean Read FAutoStretchMemos Write FAutoStretchMemos;
    property UseExprForDatafields: Boolean Read FUseExprForDatafields Write FUseExprForDatafields;
    property DBGrid: TwwDBGrid Read FDBGrid Write FDBGrid;
    {$IFDEF WIN32}
    property SelectedRecordsOnly: Boolean Read FSelectedRecordsOnly Write FSelectedRecordsOnly;
    {$ENDIF}
    property SpanPagesHorizontally: Boolean Read FSpanPagesHorizontally Write FSpanPagesHorizontally;
  end;

{------------------------------------------------------------------------------------------}
{------------------------------------------------------------------------------------------}

implementation

uses
  {$IFDEF VER80}
  DBIProcs, DBITypes,
  {$ENDIF}
  Dialogs, DB, QRPrntr;

type
  TQRPDBGrid = class(TwwDBGrid)
  end;

procedure TwwQRGridReport.DetailBandBeforePrint(Sender: TQRCustomBand;
  var PrintBand: Boolean);
var
  B      : TBookmark;
  I      : Integer;
  {$IFDEF VER80}
  Result : CmpBkmkRslt;
  {$ENDIF}
begin
  PrintBand:=False;
  B:=FQuickRep.Dataset.GetBookmark;
  For I:=1 to FDBGrid.SelectedList.Count Do
    begin
      {$IFDEF VER80}
      dbiCompareBookmarks(FDBGrid.Datasource.Dataset.Handle,
                          TBookmark(FDBGrid.SelectedList[I-1]),
                          B,Result);
      PrintBand:=Result=0;
      {$ELSE}
      PrintBand:=FQuickRep.Dataset.CompareBookmarks(TBookmark(FDBGrid.SelectedList[I-1]),B)=0;
      {$ENDIF}
      If PrintBand Then Break;
    end;
  FQuickRep.Dataset.FreeBookmark(B);
end;

procedure TwwQRGridReport.Notification(AComponent: TComponent; Operation: TOperation);
begin
  Inherited Notification(AComponent, Operation);
  If (Operation=opRemove) and (AComponent=FQuickRep) Then QuickRep:=NIL
  Else
  If (Operation=opInsert) and (AComponent=FDBGrid) Then DBGrid:=NIL;
end;

{--------------------------------------------------------------------------------}

function TwwQRGridReport.CreateReportFromGrid: Boolean;
var
  DB: TQRCustomBand;
  TB: TQRBand;
  LBL: TQRLabel;
  SHP: {$IFDEF QREXPORTS2} TQRPDFShape {$ELSE} TQRShape {$ENDIF};
  DX: Longint;
  CurrentGridColumn: Integer;
  DSet: TDataset;
  CTypes: TStrings;
  BandCounter,
  PageFirstColumn,
  NextColumn: Integer;

  function GetFieldNum(S: string): Integer;
  var
    TF: TField;
  begin
    Result:=-1;
    TF:=DSet.FindField(Copy(S,1,Pos(#9,S)-1));
    if TF=nil then Exit;
    Result:=DSet.Fields.IndexOf(TF);
  end;

  function GetFieldTypeAndOptions(FieldName: String): String;
  var
    S: String;
    I: Integer;
  begin
    Result:='';
    If CTypes<>NIL Then
      begin
        For I:=1 to CTypes.Count Do
          begin
            S:=CTypes[I-1];
            If Copy(S,1,Pos(';',S)-1)=FieldName Then
              begin
                System.Delete(S,1,Pos(';',S));
                Break;
              end;
          end;
        Result:=Uppercase(S)
      end;
  end;

  procedure CreateReportField(CurrentField: TField; ColWidth: Integer);
  var
    DBT: TQRDBText;
    DBI: TQRDBImage;
    EXPR: TQRExpr;
    DBC: TQRPExprCheckbox;
    ReportFieldType: String;
    Options: String;
    ReportFieldTypeAndOptions: String;
  begin
    ReportFieldTypeAndOptions:=GetFieldTypeAndOptions(CurrentField.FieldName);
    ReportFieldType:=Copy(ReportFieldTypeAndOptions,1,Pos(';',ReportFieldTypeAndOptions)-1);
    Options:=Copy(ReportFieldTypeAndOptions,Pos(';',ReportFieldTypeAndOptions)+1,Length(ReportFieldTypeAndOptions));

    If ReportFieldType='BITMAP' Then
      begin
        DBI:=TQRPDBImage.Create(QuickRep.Owner);
        DBI.Parent:=DB;
        DBI.Center:=False;
        DBI.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'QRDBImage',False);
        DBI.Top:=3;
        DBI.Left:=DX+3;
        DBI.Dataset:=DSet;
        DBI.Datafield:=CurrentField.FieldName;
        DX:=DX+TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn];
        DBI.Height:=DB.Height-4;
        DBI.Width:=ColWidth-5;
        DBI.Stretch:=Pos('STRETCH',Uppercase(Options))<>0;
      end
    Else
    If ReportFieldType='CHECKBOX' Then
      begin
        DBC:=TQRPExprCheckbox.Create(QuickRep.Owner);
        DBC.Parent:=DB;
        DBC.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'QRExprCheckbox',False);
        DBC.Top:=(DB.Height-12) DIV 2;
        DBC.Left:=DX+(ColWidth-12) DIV 2;
        If CurrentField.DataType=ftstring Then
          DBC.Expression:=CurrentField.FieldName+'='''+Copy(Options,1,Pos(';',Options)-1)+''''
        Else
          DBC.Expression:=CurrentField.FieldName+'='+Copy(Options,1,Pos(';',Options)-1);
        DX:=DX+ColWidth;
        DBC.Height:=12;
        DBC.Width:=12;
        DBC.Pen.Width:=2;
        DBC.CheckmarkDistance:=3;
      end
    Else
    If FUseExprForDatafields Then
      begin
        EXPR:=CreateExpr(DB,DX+3,3);
        EXPR.Font.Assign(DBGrid.Font);
        EXPR.Expression:=CurrentField.FieldName;
        DX:=DX+ColWidth;
        EXPR.AutoSize:=False;
        EXPR.WordWrap:=False;
        EXPR.Width:=ColWidth-5;
        EXPR.Alignment:=CurrentField.Alignment;
        EXPR.Transparent:=True;
        EXPR.AutoStretch:=FAutoStretchMemos;
      end
    Else
      begin
        DBT:=CreateDBText(DB,DX+3,3);
        DBT.Font.Assign(DBGrid.Font);
        DBT.Dataset:=DSet;
        DBT.Datafield:=CurrentField.FieldName;
        DX:=DX+ColWidth;
        DBT.AutoSize:=False;
        DBT.Height:=DB.Height-4;
        DBT.WordWrap:=dgWordWrap in DBGrid.Options;
        DBT.Width:=ColWidth-5;
        DBT.Alignment:=CurrentField.Alignment;
        DBT.Transparent:=True;
        DBT.AutoStretch:=FAutoStretchMemos;
      end;
  end;

  function CreateDetailBand: TQRCustomBand;
  var
    CurrentGridColumn: Integer;
    TmpBand: TQRBand;
  begin
    If SpanPagesHorizontally Then
      begin
        Result:=CreateSubdetail(Quickrep,TQRPDBGrid(DBGrid).DefaultRowHeight);
        TQRSubdetail(Result).Dataset:=DSet;
        Result.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'SubdetailBand',True);
        If BandCounter>1 Then
          begin
            TmpBand:=CreateBand(QuickRep, rbGroupHeader, 0);
            TmpBand.ForceNewPage:=True;
            If (FReportTitle<>'') or FReportTitleDate Then TmpBand.Height:=50;
            TmpBand.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'NewPageBand',True);
            TQRSubdetail(Result).HeaderBand:=TmpBand;
          end;
        Quickrep.Dataset:=NIL;
      end
    else
      begin
        Result:=CreateBand(QuickRep, rbDetail, TQRPDBGrid(DBGrid).DefaultRowHeight);
        Result.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'DetailBand',True);
        QuickRep.Dataset:=DSet;
      end;

    Result.Color:=DBGrid.Color;
    Result.Frame.Color:=clGray;
    Result.Frame.Width:=1;
    If FSelectedRecordsOnly Then Result.BeforePrint:=DetailBandBeforePrint;

    If (PrintGridLines) and (wwDBIGRD.dgRowLines in DBGrid.Options) Then
      begin
        DX:=0; 
        for CurrentGridColumn:=PageFirstColumn to TQRPDBGrid(DBGrid).ColCount-1 Do
          begin
            If DX+3+TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn]+1>Result.Width Then Break;
            DX:=DX+TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn];
          end;
        CreateShape(Result,qrsHorLine,0,Result.Height-1,DX,1,clGray);
      end;

    If (PrintGridLines) and
       (wwDBIGRD.dgColLines in DBGrid.Options) Then Result.Frame.DrawLeft:=True;
  end;

  function GetSelectedEntry(Column: Integer): String;
  begin
    If wwDBIGrd.dgIndicator in TQRPDBGrid(DBGrid).Options Then Dec(Column);
    Result:=DBGrid.Selected[Column];
  end;


var
  CurrentFieldNum: Integer;
  CurrentColWidth: Integer;
  GB: TQRGroup;

begin
  Result:=False;
  If (QuickRep=NIL) or (DBGrid=NIL) or
     (DBGrid.Datasource=NIL) or
     (DBGrid.Datasource.Dataset=NIL) Then Exit;
  CreateTitle;
  DSet:=DBGrid.Datasource.Dataset;
  If (DSet is TwwQuery) or (DSet is TwwTable) or
     (DSet is TwwQBE) Then CTypes:=wwGetControlType(DSet)
  Else
    CTypes:=nil;

  If wwDBIGrd.dgIndicator in TQRPDBGrid(DBGrid).Options Then
    NextColumn:=1
  Else
    NextColumn:=0;



  BandCounter:=0;

  Repeat
    Inc(BandCounter);

    PageFirstColumn:=NextColumn;

    DB:=CreateDetailBand;

    If wwDBIGrd.dgTitles in DBGrid.Options Then
      begin

        If SpanPagesHorizontally Then
          begin
            GB:=CreateGroup(Quickrep,TQRPDBGrid(DBGrid).RowHeights[0]);
            GB.Master:=DB;
            {$IFDEF QR3}
            GB.ReprintOnNewPage:=True;
            {$ENDIF}
            TB:=TQRBand(GB);
          end
        else
          begin
            TB:=CreateBand(QuickRep, rbColumnHeader, TQRPDBGrid(DBGrid).RowHeights[0]+2);
          end;
        TB.Name:=FindUniqueComponentName(TForm(QuickRep.Owner),'ColumnHeader',True);

        DX:=0;
        for CurrentGridColumn:=PageFirstColumn to TQRPDBGrid(DBGrid).ColCount-1 Do
          begin
            CurrentFieldNum:=GetFieldNum(GetSelectedEntry(CurrentGridColumn));
            CurrentColWidth:=TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn];

            If DX+3+CurrentColWidth+1>TB.Width Then Break;

            LBL:=CreateLabel(TB,DX+3,2);
            LBL.Font.Assign(DBGrid.TitleFont);
            LBL.Height:=TB.Height-2;
            LBL.Caption:=StringReplace(DSet.Fields[CurrentFieldNum].DisplayLabel,'~',#13,[rfReplaceAll]);
            LBL.Alignment:=TQRPDBGrid(DBGrid).TitleAlignment;
            DX:=DX+CurrentColWidth;
            LBL.AutoSize:=False;
            LBL.WordWrap:=False;
            LBL.Width:=CurrentColWidth-3;
            LBL.Transparent:=True;
            If (PrintGridLines) and (wwDBIGRD.dgColLines in DBGrid.Options) Then
              CreateShape(TB,qrsVertLine,DX,0,1,TB.Height, clGray);
          end;

        SHP:=CreateShape(TB,qrsRectangle,0,0,DX+1,TB.Height,DBGrid.TitleColor);
        SHP.Brush.Color:=DBGrid.TitleColor;
        SHP.SendToBack;

        TB.Frame.Color:=clGray;
        TB.Frame.Width:=1;
        If (PrintGridLines) and (wwDBIGRD.dgRowLines in DBGrid.Options) Then
          begin
            CreateShape(TB,qrsHorLine,0,0,DX,1,clGray);
            CreateShape(TB,qrsHorLine,0,TB.Height-1,DX,1,clGray);
          end;
        If (PrintGridLines) and (wwDBIGRD.dgColLines in DBGrid.Options) Then TB.Frame.DrawLeft:=True;
      end;

    DX:=0;
    for CurrentGridColumn:=PageFirstColumn to TQRPDBGrid(DBGrid).ColCount-1 Do
      begin

        If DX+3+TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn]+1>DB.Width Then Break;

        CurrentFieldNum:=GetFieldNum(GetSelectedEntry(CurrentGridColumn));
        if CurrentFieldNum>=0 Then
          CreateReportField(DSet.Fields[CurrentFieldNum],
                            TQRPDBGrid(DBGrid).ColWidths[CurrentGridColumn]);

        If (PrintGridLines) and (wwDBIGRD.dgColLines in DBGrid.Options) Then
          CreateShape(DB,qrsVertLine,DX,0,1,DB.Height,clGray);

        Inc(NextColumn);
      end;

  until (SpanPagesHorizontally=FALSE) or
        (NextColumn>=TQRPDBGrid(DBGrid).ColCount);

  Result:=True;
end;

{--------------------------------------------------------------------------------}
{--------------------------------------------------------------------------------}

{$IFNDEF Registered}
xxx
begin
  If FindWindow('TAppBuilder',NIL)=0 Then
    begin
      MessageDlg('UNREGISTERED QUICKREPORT POWERPACK  - '
              +'This shareware version will only run if the Delphi IDE is loaded!',mtinformation,[mbok],0);
      Halt;
    end;
{$ENDIF}
end.

