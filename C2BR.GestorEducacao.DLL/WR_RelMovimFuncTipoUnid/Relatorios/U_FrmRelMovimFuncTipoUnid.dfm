inherited FrmRelMovimFuncTipoUnid: TFrmRelMovimFuncTipoUnid
  Left = 210
  Width = 812
  Height = 583
  VertScrollBar.Position = 155
  Caption = 'FrmRelMovimFuncTipoUnid'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 8
    Top = -38
    Width = 1123
    Height = 794
    BeforePrint = QuickRep1BeforePrint
    DataSet = QryRelatorio
    Functions.Strings = (
      'PAGENUMBER'
      'COLUMNNUMBER'
      'REPORTTITLE'
      'QRSTRINGSBAND1')
    Functions.DATA = (
      '0'
      '0'
      #39#39
      #39#39)
    Page.Orientation = poLandscape
    Page.Values = (
      127.000000000000000000
      2100.000000000000000000
      127.000000000000000000
      2970.000000000000000000
      127.000000000000000000
      127.000000000000000000
      0.000000000000000000)
    inherited PageHeaderBand1: TQRBand
      Width = 1027
      Height = 151
      Frame.DrawBottom = False
      Size.Values = (
        399.520833333333400000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Top = 110
        Width = 1027
        Height = 20
        Size.Values = (
          52.916666666666660000
          2.645833333333333000
          291.041666666666700000
          2717.270833333333000000)
        Caption = 'HIST'#211'RICO DE MOVIMENTA'#199#213'ES DE FUNCION'#193'RIOS'
        FontSize = 12
      end
      inherited QRDBText14: TQRDBText
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          5.291666666666667000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          47.625000000000000000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBImage1: TQRDBImage
        Size.Values = (
          198.437500000000000000
          10.583333333333300000
          10.583333333333300000
          246.062500000000000000)
      end
      inherited qrlTemplePag: TQRLabel
        Left = 596
        Size.Values = (
          44.979166666666670000
          1576.916666666667000000
          21.166666666666670000
          66.145833333333340000)
        FontSize = 8
      end
      inherited qrlTempleData: TQRLabel
        Left = 596
        Size.Values = (
          44.979166666666670000
          1576.916666666667000000
          66.145833333333340000
          68.791666666666680000)
        FontSize = 8
      end
      inherited qrlTempleHora: TQRLabel
        Left = 596
        Size.Values = (
          44.979166666666670000
          1576.916666666667000000
          111.125000000000000000
          71.437500000000000000)
        FontSize = 8
      end
      inherited QRSysData1: TQRSysData
        Left = 979
        Size.Values = (
          44.979166666666670000
          2590.270833333333000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        Transparent = True
        FontSize = 8
      end
      inherited QRSysData2: TQRSysData
        Left = 996
        Size.Values = (
          44.979166666666670000
          2635.250000000000000000
          111.125000000000000000
          82.020833333333340000)
        FontSize = 8
      end
      inherited QRSysData3: TQRSysData
        Left = 579
        Top = 73
        Size.Values = (
          44.979166666666670000
          1531.937500000000000000
          193.145833333333300000
          82.020833333333340000)
        FontSize = 8
      end
      inherited qrlEnde: TQRLabel
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          89.958333333333340000
          119.062500000000000000)
        FontSize = 10
      end
      inherited qrlComplemento: TQRLabel
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          132.291666666666700000
          251.354166666666700000)
        FontSize = 10
      end
      inherited qrlTels: TQRLabel
        Size.Values = (
          39.687500000000000000
          285.750000000000000000
          174.625000000000000000
          105.833333333333300000)
        FontSize = 8
      end
      inherited QRLabel1000: TQRLabel
        Size.Values = (
          39.687500000000000000
          1783.291666666667000000
          68.791666666666680000
          60.854166666666680000)
        FontSize = 8
      end
      inherited QRILogoEscola: TQRImage
        Size.Values = (
          198.437500000000000000
          635.000000000000000000
          10.583333333333330000
          246.062500000000000000)
      end
      object QRLabel4: TQRLabel
        Left = 1003
        Top = 8
        Width = 4
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2653.770833333333000000
          21.166666666666670000
          10.583333333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '/'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLPage: TQRLabel
        Left = 1008
        Top = 8
        Width = 19
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2667.000000000000000000
          21.166666666666670000
          50.270833333333330000)
        Alignment = taRightJustify
        AlignToBand = True
        AutoSize = True
        AutoStretch = False
        Caption = '000'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLParam: TQRLabel
        Left = 1
        Top = 129
        Width = 1027
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2.645833333333333000
          341.312500000000000000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'RELA'#199#195'O DO TIPO DE PESSOAS'
        Color = clWhite
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
    end
    inherited QRBANDSGIE: TQRBand
      Top = 289
      Width = 1027
      Size.Values = (
        31.750000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 811
        Top = 1
        Width = 216
        Size.Values = (
          29.104166666666670000
          2145.770833333333000000
          2.645833333333333000
          571.500000000000000000)
        Font.Height = -8
        FontSize = 6
      end
      inherited Qrl_IdentificacaoRel: TQRLabel
        Size.Values = (
          29.104166666666670000
          0.000000000000000000
          0.000000000000000000
          132.291666666666700000)
        FontSize = 5
      end
    end
    object DetailBand1: TQRBand
      Left = 48
      Top = 241
      Width = 1027
      Height = 16
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = DetailBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        42.333333333333340000
        2717.270833333333000000)
      BandType = rbDetail
      object QRLDepto: TQRLabel
        Left = 850
        Top = 0
        Width = 96
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2248.958333333333000000
          0.000000000000000000
          254.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Inativa - 99/99/9999'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLReferencia: TQRLabel
        Left = 732
        Top = 0
        Width = 105
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1936.750000000000000000
          0.000000000000000000
          277.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '99/99/99 - 99/99/99 R'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLMotivo: TQRLabel
        Left = 491
        Top = 0
        Width = 232
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1299.104166666667000000
          0.000000000000000000
          613.833333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99/99/99 - 99/99/99 R'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLData: TQRLabel
        Left = 422
        Top = 0
        Width = 48
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1116.541666666667000000
          0.000000000000000000
          127.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99/99/99'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLFuncionario: TQRLabel
        Left = 69
        Top = 0
        Width = 340
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          182.562500000000000000
          0.000000000000000000
          899.583333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99.999-9 ANTONIO CEZAR CORDOVA JUNIOR'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRDBText1: TQRDBText
        Left = 5
        Top = 0
        Width = 23
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          0.000000000000000000
          60.854166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'sigla'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
    end
    object QRBand1: TQRBand
      Left = 48
      Top = 265
      Width = 1027
      Height = 24
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        63.500000000000000000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel1: TQRLabel
        Left = 5
        Top = 1
        Width = 152
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          2.645833333333333000
          402.166666666666600000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Quantidade de Movimenta'#231#245'es:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLDescTot: TQRLabel
        Left = 162
        Top = 1
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          428.625000000000000000
          2.645833333333333000
          92.604166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TOTAL'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
    end
    object QRGroup1: TQRGroup
      Left = 48
      Top = 199
      Width = 1027
      Height = 42
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRGroup1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        111.125000000000000000
        2717.270833333333000000)
      Expression = 'QryRelatorio.TIPO'
      FooterBand = QRBand2
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape1: TQRShape
        Left = 0
        Top = 23
        Width = 1027
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          0.000000000000000000
          60.854166666666680000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel10: TQRLabel
        Left = 69
        Top = 25
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          182.562500000000000000
          66.145833333333340000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel2: TQRLabel
        Left = 422
        Top = 25
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1116.541666666667000000
          66.145833333333340000
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DATA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel3: TQRLabel
        Left = 5
        Top = 25
        Width = 46
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          66.145833333333340000
          121.708333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNIDADE'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel5: TQRLabel
        Left = 491
        Top = 25
        Width = 45
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1299.104166666667000000
          66.145833333333340000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MOTIVO'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel6: TQRLabel
        Left = 732
        Top = 25
        Width = 65
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1936.750000000000000000
          66.145833333333340000
          171.979166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'REFER'#202'NCIA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel7: TQRLabel
        Left = 850
        Top = 25
        Width = 89
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2248.958333333333000000
          66.145833333333340000
          235.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DEPARTAMENTO'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel8: TQRLabel
        Left = 0
        Top = 3
        Width = 24
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          7.937500000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Tipo:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLTpMovim: TQRLabel
        Left = 30
        Top = 3
        Width = 28
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          79.375000000000000000
          7.937500000000000000
          74.083333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Tipo:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
    end
    object QRBand2: TQRBand
      Left = 48
      Top = 257
      Width = 1027
      Height = 8
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        21.166666666666670000
        2717.270833333333000000)
      BandType = rbGroupFooter
    end
  end
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    SQL.Strings = (
      'select OE.*, STATUS = (CASE OE.CO_STA_OFERT_ESTAG '
      '                    '#9#9#9'WHEN '#39'A'#39' THEN '#39'Ativo'#39
      '                    '#9#9#9'WHEN '#39'I'#39' THEN '#39'Inativo'#39
      '                    '#9#9#9'END), CI.NO_CIDADE, BA.NO_BAIRRO'
      '                  from TB188_OFERT_ESTAG OE'
      
        '                   JOIN TB904_CIDADE CI on CI.CO_CIDADE = OE.CO_' +
        'CIDADE_OFERT_ESTAG'
      
        '                   JOIN TB905_BAIRRO BA on BA.CO_BAIRRO = OE.CO_' +
        'BAIRRO_EMP_OFERT_ESTAG')
  end
end