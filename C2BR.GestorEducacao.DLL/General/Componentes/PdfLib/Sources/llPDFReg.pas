unit llPDFReg;

interface

uses
  Classes, PDF;

procedure Register;

implementation

procedure Register;
begin
  RegisterComponents('Util', [TPDFDocument]);
end;

end.
 