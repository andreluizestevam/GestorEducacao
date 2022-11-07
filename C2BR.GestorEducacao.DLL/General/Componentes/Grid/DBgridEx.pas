unit DBGridEx; 
{DBGrid which displays a checkbox instead of the default editor for 
all boolean fields. 

Created by Simon Carter (simon.carter@orcka.com) 
} 

interface 

uses 
  Windows, Classes, Grids, DBGrids; 

type 
  TDBGridEx = class(TDBGrid) 
  private 
    FBoolFields: TStringList; 
    procedure SetBoolFields(const Value: TStringList); 
  protected 
    procedure DrawColumnCell(const Rect: TRect; DataCol: Integer; 
      Column: TColumn; State: TGridDrawState); override; 
    procedure CellClick(Column: TColumn); override; 
    procedure ColEnter; override; 
    procedure ColExit; override; 
  public 
    constructor Create(AOwner: TComponent); override; 
    destructor Destroy; override; 
  published 
    property BoolFields: TStringList read FBoolFields write SetBoolFields; 
  end; 

procedure Register; 

implementation 

uses DB; 

procedure Register; 
begin 
  RegisterComponents('Data Controls', [TDBGridEx]); 
end; 

constructor TDBGridEx.Create(AOwner: TComponent); 
begin 
  inherited Create(AOwner); 
  FBoolFields := TStringList.Create; 
end; 

destructor TDBGridEx.Destroy; 
begin 
  FBoolFields.Free; 
  inherited Destroy; 
end; 

procedure TDBGridEx.DrawColumnCell(const Rect: TRect; DataCol: Integer; 
  Column: TColumn; State: TGridDrawState);
var
  bRect: TRect;
begin
  inherited DrawColumnCell(Rect, DataCol, Column, State);
  if (Column.Field.DataType = ftBoolean) or
    (FBoolFields.IndexOf(Column.Field.FieldName) > -1) then
  begin
    with Canvas do
    begin
      bRect := Rect;
      with bRect do
      begin
        Top := Top + 2;
        Bottom := Bottom - 2;
        Left := Left + 2;
        Right := Right - 2;
      end;
      FillRect(Rect);
      if Column.Field.AsBoolean then
        DrawFrameControl(Handle, bRect, DFC_BUTTON,
          DFCS_BUTTONCHECK or DFCS_CHECKED)
      else
        DrawFrameControl(Handle, bRect, DFC_BUTTON, DFCS_BUTTONCHECK);
    end; 
  end; 
end; 

procedure TDBGridEx.CellClick(Column: TColumn); 
begin 
  if (Column.Field.DataType = ftBoolean) or 
    (FBoolFields.IndexOf(Column.Field.FieldName) > -1) then 
  begin 
    if not (DataSource.DataSet.State in [dsEdit]) then 
      DataSource.DataSet.Edit; 
    Column.Field.AsBoolean := not Column.Field.AsBoolean; 
  end; 
  inherited CellClick(Column); 
end; 

procedure TDBGridEx.ColEnter; 
begin 
  if (SelectedField.DataType = ftBoolean) or 
    (FBoolFields.IndexOf(SelectedField.FieldName) > -1) then 
  begin
    if InplaceEditor <> nil then
      InplaceEditor.Hide; 
    Options := Options - [dgEditing, dgAlwaysShowEditor]; 
  end; 
  inherited ColEnter; 
end; 

procedure TDBGridEx.ColExit; 
begin 
  inherited ColExit; 
  Options := Options + [dgEditing, dgAlwaysShowEditor]; 
end; 

procedure TDBGridEx.SetBoolFields(const Value: TStringList); 
begin 
  FBoolFields.Assign(Value); 
end; 

end. 
