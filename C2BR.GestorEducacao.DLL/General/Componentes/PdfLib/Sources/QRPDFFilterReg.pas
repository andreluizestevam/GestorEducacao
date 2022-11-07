unit QRPDFFilterReg;

interface

uses QRPDFFilter, Classes;

procedure Register;

implementation

procedure Register;
begin
  RegisterComponents('QReport', [TQRPDFFilter]);
end;

end.
