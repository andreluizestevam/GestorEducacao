unit Form_MFPreview;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  QRPrntr, ExtCtrls, StdCtrls, ComCtrls, Buttons, Registry, QRExport, QuickRpt,
  JPEG, ActnList, Spin, Gauges, QRPDFFilter;

const
  TempFile = 'QReport_TMP.qrp';
type
  TQRCustomPreviewInterface = class(TQRPreviewInterface)
  public
    function Show(AQRPrinter: TQRPrinter): TWinControl; override;
    function ShowModal(AQRPrinter: TQRPrinter): TWinControl; override;
  end;

  TZoomTipo = (ztCustom, ztFit, ztWidth);

  TFormMFPreview = class(TForm)
    PanelTop: TPanel;
    PanelClient: TPanel;
    QRPreview1: TQRPreview;
    BotSair: TBitBtn;
    BotZoomFit: TSpeedButton;
    BotZoomWidth: TSpeedButton;
    BotZoom100: TSpeedButton;
    EditZoom: TSpinEdit;
    BotPaginaFirst: TSpeedButton;
    BotPaginaPrior: TSpeedButton;
    EdtPagina: TSpinEdit;
    BotPaginaNext: TSpeedButton;
    BotPaginaLast: TSpeedButton;
    BotPrint: TSpeedButton;
    BotSave: TSpeedButton;
    BotOpen: TSpeedButton;
    Bevel1: TBevel;
    Bevel2: TBevel;
    Bevel3: TBevel;
    Bevel4: TBevel;
    StatusBar1: TStatusBar;
    SaveDialog1: TSaveDialog;
    OpenDialog1: TOpenDialog;
    BotExportarTXT: TSpeedButton;
    Bevel5: TBevel;
    BotExportarFigura: TSpeedButton;
    ActionList1: TActionList;
    ActNextPage: TAction;
    ActPiorPage: TAction;
    ActFirstPage: TAction;
    ActLastPage: TAction;
    ActPrint: TAction;
    ActSave: TAction;
    ActOpen: TAction;
    Gauge1: TGauge;
    ActExportarTXT: TAction;
    QRPDFFilter1: TQRPDFFilter;
    //***** Procedimentos do Formulário *****//
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormKeyDown(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure FormResize(Sender: TObject);
    procedure FormShow(Sender: TObject);
    //***** Ações do Formulário *****//
    // Formulário
    procedure BotSairClick(Sender: TObject);
    // Zoom
    procedure BotZoomFitClick(Sender: TObject);
    procedure BotZoomWidthClick(Sender: TObject);
    procedure BotZoom100Click(Sender: TObject);
    procedure EditZoomChange(Sender: TObject);
    procedure MudarPagina;
    //***** Procedimentos ligados aos componentes do Formulário *****//
    procedure QRPreview1PageAvailable(Sender: TObject; PageNum: Integer);
    procedure QRPreview1ProgressUpdate(Sender: TObject; Progress: Integer);
    procedure QRPreview1Resize(Sender: TObject);
    procedure StatusBar1DrawPanel(StatusBar: TStatusBar;
      Panel: TStatusPanel; const Rect: TRect);
    procedure ActFirstPageExecute(Sender: TObject);
    procedure ActPiorPageExecute(Sender: TObject);
    procedure ActNextPageExecute(Sender: TObject);
    procedure ActLastPageExecute(Sender: TObject);
    procedure ActPrintExecute(Sender: TObject);
    procedure ActOpenExecute(Sender: TObject);
    procedure ActSaveExecute(Sender: TObject);
    procedure EdtPaginaChange(Sender: TObject);
    procedure ActExportarTXTExecute(Sender: TObject);
    procedure BotExportarFiguraClick(Sender: TObject);
  private
    ZoomInicial: Integer;
    // Variáveis vinculadas às propriedades do formulário
    FArquivoQRP: string; // guarda o nome do arquivo QRP para exportação EMF e BMP
    FCarregouImagem: Boolean; // usado para controlar os botões de exportação e a impressão
    FQRPrinter: TQRPrinter;
    FZoomTipo: TZoomTipo;
    // Procedimentos de inicialização
    procedure Inicializa;
    // Procedimentos de atualização da aparência dos componentes do formulário
    procedure AtualizaBotoes;
    procedure AtualizaPagina;
    procedure AtualizaZoom;
    // Funções utilitárias
    function SalvarComo: string;
    // Procedimentos vinculados às propriedades do formulário
    function GetStatus: string;
    function GetZoomAtual: Integer;
    procedure SetArquivoQRP(const Value: string);
    procedure SetCarregouImagem(const Value: Boolean);
    procedure SetStatus(const Value: string);
    procedure SetZoomAtual(const Value: Integer);
    procedure SetZoomTipo(const Value: TZoomTipo);
    procedure EnabledActions(value: Boolean);
  protected
    property ArquivoQRP: string read FArquivoQRP write SetArquivoQRP;
    property CarregouImagem: Boolean read FCarregouImagem write SetCarregouImagem;
    property Status: string read GetStatus write SetStatus;
    property ZoomAtual: Integer read GetZoomAtual write SetZoomAtual;
    property ZoomTipo: TZoomTipo read FZoomTipo write SetZoomTipo;
    function TempDir: String;
  public
    Iniciar: Boolean;
    constructor CreatePreview(AOwner: TComponent; aQRPrinter: TQRPrinter);
    property QRPrinter: TQRPrinter read FQRPrinter write FQRPrinter;
  end;

var
  FormMFPreview: TFormMFPreview;

implementation

{$R *.DFM}

{ TQRCustomPreviewInterface }

function TQRCustomPreviewInterface.Show(
  AQRPrinter: TQRPrinter): TWinControl;
begin
  Result := TFormMFPreview.CreatePreview(Application, AQRPrinter);
  TFormMFPreview(Result).Show;
end;

function TQRCustomPreviewInterface.ShowModal(
  AQRPrinter: TQRPrinter): TWinControl;
begin
  Result := TFormMFPreview.CreatePreview(Application, AQRPrinter);
  TFormMFPreview(Result).ShowModal;
end;

{ TFormMFPreview }

constructor TFormMFPreview.CreatePreview(AOwner: TComponent;
  aQRPrinter: TQRPrinter);
var
  ArqIni: TRegIniFile;
  xTop, xLeft, xHeight, xWidth: Integer;
  Maximizado: Boolean;
begin
  Application.ProcessMessages;
  inherited Create(AOwner);

  // Inicializa o componente de visualização
  QRPrinter := aQRPrinter;
  QRPreview1.QRPrinter := aQRPrinter;

  // Inicializa as variáveis/propriedades do sistema
  CarregouImagem := False;
  ArquivoQRP := '';
  OpenDialog1.Filter := 'Arquivos QuickReport (*.' +cQRPDefaultExt + ')|*.' + cqrpDefaultExt;

  if (QRPrinter <> nil) and (QRPrinter.Title <> '') then
    Caption := QRPrinter.Title;

  {
  Ao fechar o formulário, são gravadas variáveis no Registro do Windows
  para possibilitar a manutenção das suas dimensões a cada nova utilização.
  }
  // Atualiza as dimensões do formlário de acordo com o último uso
  ArqIni := TRegIniFile.Create('MicroFlex');
  try
    Maximizado := ArqIni.ReadBool(Name, 'Maximizado', False);
    xTop := ArqIni.ReadInteger(Name, 'Superior', -1);
    xLeft := ArqIni.ReadInteger(Name, 'Esquerda', -1);
    xHeight := ArqIni.ReadInteger(Name, 'Altura', -1);
    xWidth := ArqIni.ReadInteger(Name, 'Largura', -1);
    if xTop > -1 then
      Top := xTop;
    if xLeft > -1 then
      Left := xLeft;
    if xHeight > -1 then
      Height := xHeight;
    if xWidth > -1 then
      Width := xWidth;
    if Maximizado then
      WindowState := wsMaximized;

    ZoomTipo := ztCustom;
    ZoomInicial := ArqIni.ReadInteger(Name, 'Zoom', 100);
    QRPreview1.Zoom := ZoomInicial;
  finally
    ArqIni.Free;
  end;
end;

procedure TFormMFPreview.FormCreate(Sender: TObject);
begin
  // Coloca a Gauge1 na StatusBar1
  Gauge1.Parent := StatusBar1;
  EdtPagina.value:= 1;
end;

procedure TFormMFPreview.FormClose(Sender: TObject;
  var Action: TCloseAction);
var
  ArqIni: TRegIniFile;
begin
  // Salva as dimensões do formulário no Registro do Windows para o próximo uso
  ArqIni := TRegIniFile.Create('MicroFlex');
  try
    ArqIni.WriteBool(Name, 'Maximizado', WindowState = wsMaximized);
    if WindowState <> wsMaximized then
    begin
      ArqIni.WriteInteger(Name, 'Superior', Top);
      ArqIni.WriteInteger(Name, 'Esquerda', Left);
      ArqIni.WriteInteger(Name, 'Altura', Height);
      ArqIni.WriteInteger(Name, 'Largura', Width);
    end;
    ArqIni.WriteInteger(Name, 'Zoom', QRPreview1.Zoom);
  finally
    ArqIni.Free;
  end;

  // Encerra o Preview
  QRPrinter.ClosePreview(Self);
  // Libera o formulário da memória
  Action := caFree;
end;

procedure TFormMFPreview.FormKeyDown(Sender: TObject; var Key: Word;
  Shift: TShiftState);
begin
  case Key of
    {VK_Up  : with QRPreview1.VertScrollBar do
                 Position := Position + Trunc(Range / 10);
    VK_Down    : with QRPreview1.VertScrollBar do
                 Position := Position - Trunc(Range / 10);}
    VK_Left  : with QRPreview1.HorzScrollBar do
                 Position := Position - Trunc(Range / 10);
    VK_Right : with QRPreview1.HorzScrollBar do
                 Position := Position + Trunc(Range / 10);
    VK_Next  : with QRPreview1.VertScrollBar do
                 Position := Position + QRPreview1.Height;
    VK_Prior : with QRPreview1.VertScrollBar do
                 Position := Position - QRPreview1.Height;
    VK_Home  : with QRPreview1.VertScrollBar do
                 Position := 0;
    VK_End : with QRPreview1.VertScrollBar do
                 Position := Range - 480;
  end;
end;

procedure TFormMFPreview.FormResize(Sender: TObject);
begin
  // Redimensiona a StatusBar1
  StatusBar1.Panels[0].Width := Width - 324;
  // Ajusta o zoom de acordo com a opção selecionada
  case ZoomTipo of
    ztFit: BotZoomFitClick(nil);
    ztWidth: BotZoomWidthClick(nil);
  end;
end;

procedure TFormMFPreview.FormShow(Sender: TObject);
begin
  // Força a atualização da aparência do Preview ao iniciar o processo
  Iniciar := True;
end;

procedure TFormMFPreview.BotSairClick(Sender: TObject);
begin
  Close;
end;

procedure TFormMFPreview.BotZoomFitClick(Sender: TObject);
begin
  ZoomTipo := ztFit;
end;

procedure TFormMFPreview.BotZoomWidthClick(Sender: TObject);
begin
  ZoomTipo := ztWidth;
end;

procedure TFormMFPreview.BotZoom100Click(Sender: TObject);
begin
  ZoomAtual := 100;
end;

procedure TFormMFPreview.EditZoomChange(Sender: TObject);
begin
  Application.ProcessMessages;
  // Atualiza o Zoom do Preview pelo valor do EditZoom
  if EditZoom.value <> QRPreview1.Zoom then
    QRPreview1.Zoom := EditZoom.value;

  // Atualiza o Tipo de Zoom usado
  ZoomTipo := ztCustom;
end;

procedure TFormMFPreview.MudarPagina;
begin
  Application.ProcessMessages;
  try
    if QRPreview1.PageNumber <> StrToInt(EdtPagina.text) then
       QRPreview1.PageNumber := StrToInt(EdtPagina.text);
    AtualizaBotoes;
    AtualizaPagina;
  except
  end;
end;

// Este evento é disparado a cada nova página criada no Preview
procedure TFormMFPreview.QRPreview1PageAvailable(Sender: TObject;
  PageNum: Integer);
var
  Titulo: string;
begin
  // Na primeira página, atualiza a aparência do Preview
  Inicializa;
  if EdtPagina.Value = 0 then
     EdtPagina.Value := 1;

  Titulo := QRPrinter.Title;
  if Titulo = '' then
    Titulo := 'Visualizador de Relatório - [Sem título]';

  if PageNum = 1 then
    Caption := Titulo + ' - 1 página'
  else Caption := Titulo + ' - ' + IntToStr(PageNum) + ' páginas';

  case QRPrinter.Status of
    mpReady:
    begin
      Status := 'Pronto';
      EnabledActions(true);
    end;
    mpBusy: Status := 'Ocupado';
    mpFinished:
    begin
      Gauge1.Progress := 0;
      Gauge1.ShowText:= false;
      Status := 'Concluído';
      EnabledActions(true);
    end;
  end;

  AtualizaBotoes;
  AtualizaPagina;
  if TQuickRep(QRPrinter.Master).DataSet = nil then
     EnabledActions(true);
end;

procedure TFormMFPreview.QRPreview1ProgressUpdate(Sender: TObject;
  Progress: Integer);
begin
  Gauge1.ShowText:= (Progress > 0) and (Progress < 100);
  if (Progress > Gauge1.Progress) and (Progress <= 100) then
    Gauge1.Progress:= Progress;
  if progress = 100 then
     EnabledActions(true);
end;

procedure TFormMFPreview.QRPreview1Resize(Sender: TObject);
begin
  // Verifica a necessidade de redimensionar o Preview caso ZoomTipo seja ztFit ou ztWidth
  AtualizaZoom;
end;

procedure TFormMFPreview.StatusBar1DrawPanel(StatusBar: TStatusBar;
  Panel: TStatusPanel; const Rect: TRect);
var
  aRect: TRect;
begin
  // Desenha a Gauge1 dentro do Panel[2] da StatusBar1
  if Panel.ID = 2 then
  begin
    aRect := Rect;
    InflateRect(aRect, 1, 1);
    Gauge1.BoundsRect := aRect;
  end;
end;

procedure TFormMFPreview.Inicializa;
begin
  if Iniciar then
  begin
    // Força o Preview a utilizar o Zoom definido no CreatePreview
    QRPreview1.Zoom := ZoomInicial;
    // Atualiza a aparência dos botões
    AtualizaBotoes;
    AtualizaPagina;
    // Desliga o Flag ligado no FormShow
    Iniciar := False;
  end;
end;

procedure TFormMFPreview.AtualizaBotoes;
begin
  with QRPreview1 do
  begin
    BotPaginaFirst.Enabled := PageNumber > 1;
    BotPaginaPrior.Enabled := PageNumber > 1;
    BotPaginaNext.Enabled := PageNumber < QRPrinter.PageCount;
    BotPaginaLast.Enabled := PageNumber < QRPrinter.PageCount;
  end;
  BotExportarTXT.Enabled := not CarregouImagem;
end;

procedure TFormMFPreview.AtualizaPagina;
var
  PaginaAtual: Integer;
begin
  EdtPagina.MaxLength:= length(IntToStr(QRPrinter.PageCount));
  EdtPagina.MaxValue:= QRPrinter.PageCount;
  if StrToInt(EdtPagina.text) < 0 then
    PaginaAtual := 0
  else PaginaAtual := StrToInt(EdtPagina.text);

  if PaginaAtual <> QRPreview1.PageNumber then
  begin
    if StrToInt(EdtPagina.text) >= QRPreview1.PageNumber then
      EdtPagina.text := IntToStr(QRPreview1.PageNumber);
  end;
  StatusBar1.Panels[1].Text := Format('Página %d de %d', [QRPreview1.PageNumber, QRPrinter.PageCount]);
end;

procedure TFormMFPreview.AtualizaZoom;
begin
  if EditZoom.value <> QRPreview1.Zoom then
    EditZoom.Value := QRPreview1.Zoom;
end;

function TFormMFPreview.SalvarComo: string;
begin
  Result := '';
  if (ArquivoQRP <> '') and (not AnsiSameText(ArquivoQRP, TempFile)) then
     Result := ChangeFileExt(ExtractFileName(ArquivoQRP),'')
  else if QRPrinter <> nil then
     Result := QRPrinter.Title;
end;

function TFormMFPreview.GetStatus: string;
begin
  Result := StatusBar1.Panels[3].Text;
end;

function TFormMFPreview.GetZoomAtual: Integer;
begin
  Result := EditZoom.Value;
end;

procedure TFormMFPreview.SetArquivoQRP(const Value: string);
begin
  FArquivoQRP := Value;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.SetCarregouImagem(const Value: Boolean);
begin
  FCarregouImagem := Value;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.SetStatus(const Value: string);
begin
  StatusBar1.Panels[3].Text := Value;
end;

procedure TFormMFPreview.SetZoomAtual(const Value: Integer);
begin
  Application.ProcessMessages;
  EditZoom.Value := Value;
  ZoomTipo := ztCustom;
end;

procedure TFormMFPreview.SetZoomTipo(const Value: TZoomTipo);
begin
  FZoomTipo := Value;

  if Value = ztCustom then
    Exit;

  Application.ProcessMessages;
  case Value of
  ztFit: QRPreview1.ZoomToFit;
  ztWidth: QRPreview1.ZoomToWidth;
  end;
  AtualizaZoom;
end;

procedure TFormMFPreview.ActFirstPageExecute(Sender: TObject);
begin
  Application.ProcessMessages;
  if QRPreview1.PageNumber > 1 then
    QRPreview1.PageNumber := 1;
  EdtPagina.value:= QRPreview1.PageNumber;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.ActPiorPageExecute(Sender: TObject);
begin
  Application.ProcessMessages;
  if QRPreview1.PageNumber > 1 then
    QRPreview1.PageNumber := QRPreview1.PageNumber - 1;
  EdtPagina.value:= QRPreview1.PageNumber;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.ActNextPageExecute(Sender: TObject);
begin
  Application.ProcessMessages;
  if QRPreview1.PageNumber < QRPrinter.PageCount then
     QRPreview1.PageNumber := QRPreview1.PageNumber + 1;
  EdtPagina.value:= QRPreview1.PageNumber;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.ActLastPageExecute(Sender: TObject);
begin
  Application.ProcessMessages;
  if QRPreview1.PageNumber < QRPrinter.PageCount then
     QRPreview1.PageNumber := QRPrinter.PageCount;
  EdtPagina.value:= QRPreview1.PageNumber;
  AtualizaBotoes;
  AtualizaPagina;
end;

procedure TFormMFPreview.ActPrintExecute(Sender: TObject);
var
  i: Integer;
begin
  if CarregouImagem then
  begin
    {
    Quando a imagem é carregada não é possível acessar o PrinterSetup
    porque o QRPrinter não tem um TQuickRep como Master.
    Por este motivo, a impressão é disparada diretamente para a
    impressora Default definida no Windows.
    }
    if MessageDlg('Você deseja imprimir o relatório para a impressora Default?', mtConfirmation, [mbYes, mbNo], 0) <> mrYes then
      raise Exception.Create('Operação cancelada!');

    QRPrinter.Print;
  end
  else
  begin
    with TQuickRep(QRPrinter.Master) do
    begin
      {
      Quando o usuário seleciona "Cancelar" no PrinterSetup,
      a propriedade Tag do QuickReport é alterada para "1",
      caso contrário a Tag é definida como "0".
      Neste ponto, podemos verificar se o usuário quer prosseguir
      com a impressão.
      Por isso, considerando que você use a Tag para outra finalidade,
      guardamos o valor da Tag antes de executar o PrinterSetup,
      para podermos retornar este valor após a operação.
      }
      i := Tag;

      PrinterSetup;
      if Tag = 0 then
      begin
        Tag := i;
        Print;
      end
      else
        Tag := i;
    end;
  end;
end;

procedure TFormMFPreview.ActOpenExecute(Sender: TObject);
begin
  if OpenDialog1.Execute then
  begin
    // Atualiza o Master do QRPrinter
    QRPrinter.Master := nil;
    // Carrega o arquivo selecionado
    QRPrinter.Load(OpenDialog1.FileName);
    // Ajusta a propriedade do Preview para a página que será exibida
    QRPreview1.PageNumber := 1;
    // Exibe a primeira página do arquivo carregado
    QRPreview1.PreviewImage.PageNumber := 1;
    // Ajusta o título do formulário
    Caption := 'Visualizando Relatório Salvo - ' + OpenDialog1.FileName + ' - ' + IntToStr(QRPrinter.PageCount) + ' páginas';

    // Ajusta as propriedades do formulário usadas para exportação e impressão
    CarregouImagem := True;
    ArquivoQRP := OpenDialog1.FileName;
  end;
end;

procedure TFormMFPreview.ActSaveExecute(Sender: TObject);
begin
    SaveDialog1.DefaultExt := cQRPDefaultExt;
    SaveDialog1.Filter :=
      'Arquivo de Relatório do QuickReport (*.' +cQRPDefaultExt + ')|*.' + cqrpDefaultExt;
    SaveDialog1.Title := 'Salvar Relatório';
    SaveDialog1.FileName := SalvarComo;
    if SaveDialog1.Execute then
    begin
      QRPrinter.Save(SaveDialog1.FileName);
      // Atualiza a propriedade ArquivoQRP, usada na exportação para Metafile e Bitmap
      ArquivoQRP := SaveDialog1.FileName;
    end;
end;

procedure TFormMFPreview.EdtPaginaChange(Sender: TObject);
begin
  if (EdtPagina.text = '') then
     EdtPagina.text:= IntToStr(QRPreview1.PageNumber);
  MudarPagina;
end;

procedure TFormMFPreview.ActExportarTXTExecute(Sender: TObject);
var
  AExportFilterTXT: TQRAsciiExportFilter;
  AExportFilterCSV: TQRCommaSeparatedFilter;
  AExportFilterHTML: TQRHTMLDocumentFilter;
  PDFFilter: TQRPDFExportFilter;
  ext: ShortString;
begin
    SaveDialog1.DefaultExt := 'pdf';
    SaveDialog1.Filter :=
      'Documento do Adobe Acrobat (*.pdf)|*.pdf|' +
      'Arquivo de Texto (*.txt)|*.txt|' +
      'Documento HTML (*.htm, *.html)|*.htm;*.html|' +
      'Arquivo de Texto com Delimitador (*.csv)|*.csv|';
    SaveDialog1.Title := 'Exportar Relatório';
    SaveDialog1.FileName := SalvarComo;
    if SaveDialog1.Execute then
    begin
      ext:= ExtractFileExt(SaveDialog1.FileName);
      if AnsiSameText(ext, '.pdf') then
      begin
        PDFFilter:= TQRPDFExportFilter.Create(SaveDialog1.FileName);
        try
          QRPrinter.ExportToFilter(PDFFilter);
        finally
          PDFFilter.Free;
        end;
      end
      else if AnsiSameText(Ext,'.txt') then
      begin
        AExportFilterTXT := TQRAsciiExportFilter.Create(SaveDialog1.FileName);
        try
          QRPrinter.ExportToFilter(AExportFilterTXT);
        finally
          AExportFilterTXT.Free;
        end;
      end
      else if AnsiSameText(Ext,'.cxv') then
      begin
        AExportFilterCSV := TQRCommaSeparatedFilter.Create(SaveDialog1.FileName);
        try
          QRPrinter.ExportToFilter(AExportFilterCSV);
        finally
          AExportFilterCSV.Free;
        end;
      end
      else if AnsiSameText(Ext,'.htm') or AnsiSameText(Ext,'.html') then
      begin
        AExportFilterHTML := TQRHTMLDocumentFilter.Create(SaveDialog1.FileName);
        try
          QRPrinter.ExportToFilter(AExportFilterHTML);
        finally
          AExportFilterHTML.Free;
        end;
      end;
    end;
end;

procedure TFormMFPreview.EnabledActions(value: Boolean);
begin
  actPrint.Enabled := value;
  actSave.Enabled := value;
  actOpen.Enabled := value;
end;

procedure TFormMFPreview.BotExportarFiguraClick(Sender: TObject);
  {
   Esta função ExportToPicture (que antes tinha o nome de SaveWMF) foi adaptada da Unit "qrqrp.pas" que acompanha
   o exemplo do Custom Preview disponível na página da QUSoft.
  }
  // This function will save a QRP file out as a series of EMF files
  function ExportToPicture(aQRPfile, aBaseName: string; StartNum: integer): boolean;
  var
    plSrc: TQRPageList;
    nIdx: integer;
    aMetafile: TMetafile;
    // criei esta variável para conversão de Metafile para Bitmap
    aImage: TImage;
    jpeg: TJpegImage;
    ext: ShortString;
  begin
    {
    Some simple error checking to make sure the correct parameters
    have been passed in.
    }
    Result := True;
    if not Result then
      Exit;
    Ext:= ExtractFileExt(aBaseName);
    // Each page of a rendered report is stored in the qrprinter's PageList.
    plSrc := TQRPageList.Create;
    with plSrc do
    try
      // Clear the PageList of the previous file's data
      Clear;
      // Load the QRP file, which will create the TQRStream that it needs
      LoadFromFile(aQRPfile);

      // Loop through each page and save it as a Metafile or Bitmap
      try
        Screen.Cursor:= crHourGlass;
        for nIdx := 1 to PageCount do
        begin
          // Retrieve the current page as a metafile
          aMetaFile := GetPage(nIdx);

          aMetaFile.Enhanced := True; // True cria EMF (32bits), False cria WMF (16bits)

          if AnsiSameText(ext, '.emf') then
             aMetaFile.SaveToFile(ChangeFileExt(aBaseName,'')  + ' - ' + format('%3.3d', [nIdx+StartNum-1]) + '.emf')
          else 
          begin
            // Esta é a minha adaptação
            aImage := TImage.Create(nil);
            try
              aImage.Height := aMetaFile.Height;
              aImage.Width := aMetaFile.Width;
              aImage.Canvas.Draw(0, 0, aMetaFile);
              if AnsiSameText(Ext, '.jpg') then
              begin
                jpeg:= TJpegImage.Create;
                try
                  jpeg.Assign(aImage.Picture.Bitmap);
                  {jpeg.Height := aMetaFile.Height;
                  jpeg.Width := aMetaFile.Width;}
                  jpeg.JPEGNeeded;
                  jpeg.SaveToFile(ChangeFileExt(aBaseName,'') + ' - ' + format('%3.3d', [nIdx+StartNum-1]) + '.jpg');
                finally
                  jpeg.free;
                end;
              end
              else aImage.Picture.Bitmap.SaveToFile(ChangeFileExt(aBaseName,'') + format('%3.3d', [nIdx+StartNum-1]) + '.bmp');
            finally
              aImage.Free;
            end;
          end;
        end;
      finally
        Screen.Cursor:= crDefault;
      end;
    finally
      plSrc.free;
    end;
  end;
  
var Temp: TFileName;
begin // BotExportarFigura
    if ArquivoQRP = '' then
    begin
       Temp:= TempDir + TempFile;
       QRPrinter.Save(Temp);
       ArquivoQRP := Temp;
    end;
    SaveDialog1.Filter :=
       'Imagem JPEG (*.jpg, *.jpeg)|*.jpg;*.jpeg|Arquivos Bitmap (*.bmp)|*.bmp|Arquivos Enhaced Metafile (*.emf)|*.emf';
    SaveDialog1.DefaultExt := 'jpg';
    SaveDialog1.FileName := QRPrinter.Title;
    SaveDialog1.Title := 'Exportar páginas do relatório para arquivos de imagem';
    if SaveDialog1.Execute then
       ExportToPicture(ArquivoQRP, SaveDialog1.FileName, 1);
end;

function TFormMFPreview.TempDir: String;
var
  wdir: array [0..255] of char;
begin
  GetTempPath(255,wdir);
  result := trim(StrPas(wdir));
  if result = '' then
     result:= 'c:\';
  if (result[length(result)] <> '\') then
     result:= result + '\';
end;

end.
