inherited frmRelSolicRealiz: TfrmRelSolicRealiz
  Left = 209
  Width = 811
  Height = 584
  Caption = 'frmRelSolicRealiz'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 6
    Top = 8
    Width = 1123
    Height = 794
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
      Height = 153
      Frame.Width = 0
      Size.Values = (
        404.812500000000000000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Top = 105
        Width = 1027
        Height = 21
        Size.Values = (
          55.562500000000000000
          2.645833333333333000
          277.812500000000000000
          2717.270833333333000000)
        Caption = 'RELA'#199#195'O DE SOLICITA'#199#213'ES - POR STATUS / TIPO'
        Font.Height = -15
        FontSize = 11
      end
      inherited QRDBText14: TQRDBText
        Left = 100
        Top = 4
        Size.Values = (
          44.979166666666670000
          264.583333333333300000
          10.583333333333330000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText
        Left = 100
        Top = 21
        Height = 15
        Size.Values = (
          39.687500000000000000
          264.583333333333300000
          55.562500000000000000
          1256.770833333333000000)
        Font.Height = -11
        FontSize = 8
      end
      inherited QRDBImage1: TQRDBImage
        Size.Values = (
          198.437500000000000000
          10.583333333333300000
          10.583333333333300000
          246.062500000000000000)
      end
      inherited qrlTemplePag: TQRLabel
        Left = 929
        Width = 32
        Size.Values = (
          44.979166666666670000
          2457.979166666667000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 929
        Width = 32
        Size.Values = (
          44.979166666666670000
          2457.979166666667000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 929
        Width = 32
        Size.Values = (
          44.979166666666670000
          2457.979166666667000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 973
        Size.Values = (
          44.979166666666670000
          2574.395833333333000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData2: TQRSysData
        Left = 990
        Width = 37
        Size.Values = (
          44.979166666666670000
          2619.375000000000000000
          111.125000000000000000
          97.895833333333340000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData3: TQRSysData
        Left = 991
        Top = 25
        Width = 36
        Size.Values = (
          44.979166666666670000
          2622.020833333333000000
          66.145833333333340000
          95.250000000000000000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlEnde: TQRLabel
        Left = 100
        Top = 36
        Width = 37
        Height = 15
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          95.250000000000000000
          97.895833333333340000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlComplemento: TQRLabel
        Left = 100
        Top = 51
        Width = 77
        Height = 15
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          134.937500000000000000
          203.729166666666700000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlTels: TQRLabel
        Left = 100
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          174.625000000000000000
          105.833333333333300000)
        FontSize = 8
      end
      inherited QRLabel1000: TQRLabel
        Left = 1003
        Size.Values = (
          39.687500000000000000
          2653.770833333333000000
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
      object QRLabel1: TQRLabel
        Left = 1001
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
          2648.479166666667000000
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
        Alignment = taLeftJustify
        AlignToBand = False
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
      object QRLPeriodo: TQRLabel
        Left = 4
        Top = 134
        Width = 41
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          10.583333333333330000
          354.541666666666700000
          108.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '          '
        Color = clWhite
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
    end
    inherited QRBANDSGIE: TQRBand
      Top = 297
      Width = 1027
      Height = 18
      Size.Values = (
        47.625000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 573
        Width = 454
        Height = 17
        Size.Values = (
          44.979166666666670000
          1516.062500000000000000
          0.000000000000000000
          1201.208333333333000000)
        Font.Height = -11
        FontSize = 8
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
    object QRGroup1: TQRGroup
      Left = 48
      Top = 201
      Width = 1027
      Height = 43
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
        113.770833333333300000
        2717.270833333333000000)
      Expression = 'QryRelatorio.co_tipo_soli'
      FooterBand = QRBand3
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRDBText2: TQRDBText
        Left = 96
        Top = 7
        Width = 425
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          254.000000000000000000
          18.520833333333330000
          1124.479166666667000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'no_tipo_soli'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 9
      end
      object QRShape1: TQRShape
        Left = 0
        Top = 24
        Width = 1027
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Frame.Style = psClear
        Size.Values = (
          52.916666666666660000
          0.000000000000000000
          63.500000000000000000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object qrlTipo: TQRLabel
        Left = 516
        Top = 27
        Width = 72
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1365.250000000000000000
          71.437500000000000000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'S'#201'RIE/TURMA'
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
        Left = 598
        Top = 27
        Width = 51
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1582.208333333333000000
          71.437500000000000000
          134.937500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' SOLIC.'
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
      object QRLabel4: TQRLabel
        Left = 682
        Top = 27
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1804.458333333333000000
          71.437500000000000000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'EMISS'#195'O'
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
        Left = 799
        Top = 27
        Width = 46
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2114.020833333333000000
          71.437500000000000000
          121.708333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT.PREV'
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
        Left = 968
        Top = 27
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2561.166666666667000000
          71.437500000000000000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SITUA'#199#195'O'
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
      object QRLabel9: TQRLabel
        Left = 852
        Top = 27
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2254.250000000000000000
          71.437500000000000000
          132.291666666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ENTREGA'
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
      object QRLabel11: TQRLabel
        Left = 2
        Top = 7
        Width = 82
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          5.291666666666667000
          18.520833333333330000
          216.958333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SOLICITA'#199#195'O:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 9
      end
      object QRLabel12: TQRLabel
        Left = 3
        Top = 27
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          71.437500000000000000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
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
      object QRLabel10: TQRLabel
        Left = 96
        Top = 27
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          254.000000000000000000
          71.437500000000000000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' NIS'
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
      object QRLabel13: TQRLabel
        Left = 212
        Top = 27
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          560.916666666666700000
          71.437500000000000000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'OBSERVA'#199#195'O'
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
      object QRLabel14: TQRLabel
        Left = 406
        Top = 27
        Width = 39
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1074.208333333333000000
          71.437500000000000000
          103.187500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'LOCAL'
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
        Left = 739
        Top = 27
        Width = 53
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1955.270833333333000000
          71.437500000000000000
          140.229166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RESP.SOL'
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
      object QRLabel16: TQRLabel
        Left = 910
        Top = 27
        Width = 51
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2407.708333333333000000
          71.437500000000000000
          134.937500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RESP.ENT'
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
    end
    object QRBand1: TQRBand
      Left = 48
      Top = 244
      Width = 1027
      Height = 32
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = QRBand1AfterPrint
      AlignToBottom = False
      BeforePrint = QRBand1BeforePrint
      Color = 14211288
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Arial'
      Font.Style = []
      ForceNewColumn = False
      ForceNewPage = False
      ParentFont = False
      Size.Values = (
        84.666666666666680000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText3: TQRDBText
        Left = 968
        Top = 1
        Width = 53
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2561.166666666667000000
          2.645833333333333000
          140.229166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'co_situ_soli'
        OnPrint = QRDBText3Print
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLdt_soli_aten: TQRLabel
        Left = 685
        Top = 1
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1812.395833333333000000
          2.645833333333333000
          113.770833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '01/01/01'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLdt_prev_entr: TQRLabel
        Left = 800
        Top = 1
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2116.666666666667000000
          2.645833333333333000
          113.770833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '01/01/01'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLdt_entr_soli: TQRLabel
        Left = 856
        Top = 1
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2264.833333333333000000
          2.645833333333333000
          113.770833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '01/01/01'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object qrlSolic: TQRLabel
        Left = 598
        Top = 0
        Width = 73
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1582.208333333333000000
          0.000000000000000000
          193.145833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '0000.00.00000'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlNis: TQRLabel
        Left = 96
        Top = 17
        Width = 45
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          254.000000000000000000
          44.979166666666670000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00-000-0'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlSerieTurma: TQRLabel
        Left = 516
        Top = 1
        Width = 78
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1365.250000000000000000
          2.645833333333333000
          206.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2'#186' Ano/Turma B'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlLocal: TQRLabel
        Left = 406
        Top = 0
        Width = 110
        Height = 32
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          84.666666666666680000
          1074.208333333333000000
          0.000000000000000000
          291.041666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Arm'#225'rio 005 - Pasta Declara'#231#227'o Escolaridade EF-1'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlOBS: TQRLabel
        Left = 212
        Top = 0
        Width = 190
        Height = 32
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          84.666666666666680000
          560.916666666666800000
          0.000000000000000000
          502.708333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 
          'Declara'#231#227'o de Matr'#237'cula - Funda'#231#227'o de Tecnologia e Cultura do Es' +
          'tado - Bolsa Integra'#231#227'o Tecnol'#243'gica.'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlMatricula: TQRLabel
        Left = 3
        Top = 17
        Width = 86
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          44.979166666666670000
          227.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000.0000-XXX'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlRespSol: TQRLabel
        Left = 743
        Top = 0
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1965.854166666667000000
          0.000000000000000000
          116.416666666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000-0'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QrlRespEnt: TQRLabel
        Left = 916
        Top = 0
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2423.583333333333000000
          0.000000000000000000
          116.416666666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000-0'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLNoAlu: TQRLabel
        Left = 3
        Top = 1
        Width = 205
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          2.645833333333333000
          542.395833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000.0000-XXX'
        Color = clWhite
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
    end
    object QRBand3: TQRBand
      Left = 48
      Top = 276
      Width = 1027
      Height = 21
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRBand3BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        55.562500000000000000
        2717.270833333333000000)
      BandType = rbGroupFooter
      object QRLabel8: TQRLabel
        Left = 1
        Top = 3
        Width = 152
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2.645833333333333000
          7.937500000000000000
          402.166666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Quantidade de Solicita'#231#245'es:'
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
      object QRLabel6: TQRLabel
        Left = 159
        Top = 3
        Width = 50
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          420.687500000000000000
          7.937500000000000000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLabel6'
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
      object QRShape2: TQRShape
        Left = 0
        Top = 0
        Width = 1027
        Height = 1
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          2.645833333333333000
          0.000000000000000000
          0.000000000000000000
          2717.270833333333000000)
        Brush.Color = clBlack
        Shape = qrsRectangle
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    SQL.Strings = (
      
        'select tb65.co_tipo_soli, tb66.no_tipo_soli, tb07.no_alu, TB07.N' +
        'U_NIS, tb01.no_cur, TB01.CO_SIGL_CUR, TU.CO_SIGLA_TURMA as NO_TU' +
        'RMA, tb64.co_soli_aten,'
      #9'   tb64.mes_soli_aten, tb64.ano_soli_aten,'
      '       tb64.dt_soli_aten, tb64.dt_prev_entr, tb65.dt_entr_soli,'
      '       tb65.va_soli_aten, tb64.co_isen_taxa, tb65.co_situ_soli,'
      
        '       tb08.co_alu_cad, TB64.DE_OBS_SOLI, COL.CO_MAT_COL, TB64.L' +
        'OCALIZACAO,tb65.co_col_ent_sol,tb64.NU_DCTO_SOLIC'
      'from tb07_aluno as tb07 inner join tb64_solic_atend as tb64'
      '     on tb07.co_alu = tb64.co_alu'
      
        #9' inner join tb08_matrcur as tb08 on tb08.co_alu = tb07.co_alu  ' +
        '   '
      #9' inner join tb01_curso as tb01 on tb01.co_cur = tb64.co_cur'
      
        '     inner join tb65_hist_solicit  as tb65 on tb65.co_soli_aten ' +
        '= tb64.co_soli_aten'
      '     and tb65.co_alu = tb64.co_alu and tb65.co_cur = tb64.co_cur'
      
        '     inner join tb66_tipo_solic as tb66 on tb66.co_tipo_soli = t' +
        'b65.co_tipo_soli'
      #9' JOIN TB06_TURMAS TUR ON TUR.CO_TUR = TB08.CO_TUR'
      
        '                 JOIN TB129_CADTURMAS TU ON TUR.CO_TUR = TB08.CO' +
        '_TUR'
      #9' JOIN TB03_COLABOR COL ON COL.CO_COL = TB64.CO_COL'
      
        '     group by tb65.co_tipo_soli, tb66.no_tipo_soli, tb07.no_alu,' +
        ' TB07.NU_NIS, tb01.no_cur, TB01.CO_SIGL_CUR, TU.CO_SIGLA_TURMA, ' +
        'tb64.co_soli_aten,'
      #9'   tb64.mes_soli_aten, tb64.ano_soli_aten,'
      '       tb64.dt_soli_aten, tb64.dt_prev_entr, tb65.dt_entr_soli,'
      
        '       tb65.va_soli_aten, tb64.co_isen_taxa, tb65.co_situ_soli, ' +
        'tb08.co_alu_cad, TB64.DE_OBS_SOLI, COL.CO_MAT_COL, TB64.LOCALIZA' +
        'CAO,tb65.co_col_ent_sol, tb64.NU_DCTO_SOLIC')
    Left = 265
    Top = 16
    object QryRelatorioco_tipo_soli: TIntegerField
      FieldName = 'co_tipo_soli'
    end
    object QryRelatoriono_tipo_soli: TStringField
      FieldName = 'no_tipo_soli'
      Size = 100
    end
    object QryRelatoriono_alu: TStringField
      FieldName = 'no_alu'
      Size = 60
    end
    object QryRelatoriono_cur: TStringField
      FieldName = 'no_cur'
      Size = 100
    end
    object QryRelatorioco_soli_aten: TIntegerField
      FieldName = 'co_soli_aten'
    end
    object QryRelatoriodt_soli_aten: TDateTimeField
      FieldName = 'dt_soli_aten'
      DisplayFormat = 'dd/mm/yyyy'
    end
    object QryRelatoriodt_prev_entr: TDateTimeField
      FieldName = 'dt_prev_entr'
      DisplayFormat = 'dd/mm/yyyy'
    end
    object QryRelatoriodt_entr_soli: TDateTimeField
      FieldName = 'dt_entr_soli'
      DisplayFormat = 'dd/mm/yyyy'
    end
    object QryRelatoriova_soli_aten: TBCDField
      FieldName = 'va_soli_aten'
      DisplayFormat = '###,##0.00'
      Precision = 5
      Size = 2
    end
    object QryRelatorioco_isen_taxa: TStringField
      FieldName = 'co_isen_taxa'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioco_situ_soli: TStringField
      FieldName = 'co_situ_soli'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioco_alu_cad: TStringField
      FieldName = 'co_alu_cad'
    end
    object QryRelatoriomes_soli_aten: TIntegerField
      FieldName = 'mes_soli_aten'
    end
    object QryRelatorioano_soli_aten: TIntegerField
      FieldName = 'ano_soli_aten'
    end
    object QryRelatorioDE_OBS_SOLI: TStringField
      FieldName = 'DE_OBS_SOLI'
      Size = 255
    end
    object QryRelatorioCO_MAT_COL: TStringField
      FieldName = 'CO_MAT_COL'
      Size = 15
    end
    object QryRelatorioLOCALIZACAO: TStringField
      FieldName = 'LOCALIZACAO'
      Size = 200
    end
    object QryRelatorioCO_SIGL_CUR: TStringField
      FieldName = 'CO_SIGL_CUR'
      Size = 6
    end
    object QryRelatorioNU_NIS: TBCDField
      FieldName = 'NU_NIS'
      Precision = 11
      Size = 0
    end
    object QryRelatorioNO_TURMA: TStringField
      FieldName = 'NO_TURMA'
      Size = 10
    end
    object QryRelatorioco_col_ent_sol: TIntegerField
      FieldName = 'co_col_ent_sol'
    end
    object QryRelatorioNU_DCTO_SOLIC: TStringField
      FieldName = 'NU_DCTO_SOLIC'
      Size = 16
    end
  end
  object QryRespEnt: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    Left = 376
    Top = 40
  end
end
