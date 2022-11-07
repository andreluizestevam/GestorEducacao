inherited FrmRelEspenhoDiscCursadas: TFrmRelEspenhoDiscCursadas
  Left = 254
  Top = 131
  Height = 581
  Caption = 'Espelho de Disciplinas Cursadas'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 6
    Top = 8
    BeforePrint = QuickRep1BeforePrint
    DataSet = QryRelatorio
    Functions.DATA = (
      '0'
      '0'
      #39#39)
    Page.Values = (
      127.000000000000000000
      2970.000000000000000000
      127.000000000000000000
      2100.000000000000000000
      127.000000000000000000
      127.000000000000000000
      0.000000000000000000)
    inherited PageHeaderBand1: TQRBand
      Height = 135
      Frame.DrawBottom = False
      Size.Values = (
        357.187500000000000000
        1846.791666666667000000)
      inherited LblTituloRel: TQRLabel
        Top = 108
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          285.750000000000000000
          1836.208333333333000000)
        Caption = 'ESPELHO DE DISCIPLINAS'
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
        Left = 593
        Width = 32
        Size.Values = (
          44.979166666666670000
          1568.979166666667000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 593
        Width = 32
        Size.Values = (
          44.979166666666670000
          1568.979166666667000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 593
        Width = 32
        Size.Values = (
          44.979166666666670000
          1568.979166666667000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 645
        Size.Values = (
          44.979166666666670000
          1706.562500000000000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        FontSize = 8
      end
      inherited QRSysData2: TQRSysData
        Left = 661
        Width = 37
        Size.Values = (
          44.979166666666670000
          1748.895833333334000000
          111.125000000000000000
          97.895833333333340000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData3: TQRSysData
        Left = 647
        Top = 25
        Width = 36
        Size.Values = (
          44.979166666666670000
          1711.854166666667000000
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
      object QRLabel10: TQRLabel
        Left = 672
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
          1778.000000000000000000
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
        Left = 680
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
          1799.166666666667000000
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
    end
    inherited QRBANDSGIE: TQRBand
      Top = 265
      Height = 18
      Size.Values = (
        47.625000000000000000
        1846.791666666667000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 441
        Top = 3
        Width = 257
        Height = 13
        Size.Values = (
          34.395833333333340000
          1166.812500000000000000
          7.937500000000000000
          679.979166666666800000)
        Font.Height = -9
        FontSize = 7
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
    object QRBand1: TQRBand
      Left = 48
      Top = 229
      Width = 698
      Height = 18
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      Frame.Style = psDot
      AlignToBottom = False
      BeforePrint = QRBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        47.625000000000000000
        1846.791666666667000000)
      BandType = rbDetail
      object QRDBText1: TQRDBText
        Left = 7
        Top = 1
        Width = 33
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          18.520833333333330000
          2.645833333333333000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_ANO_REF'
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
      object QrlMatricula: TQRLabel
        Left = 43
        Top = 1
        Width = 73
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          113.770833333333300000
          2.645833333333333000
          193.145833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000.000000'
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
      object QRDBText2: TQRDBText
        Left = 204
        Top = 1
        Width = 67
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          539.750000000000000000
          2.645833333333333000
          177.270833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_MATERIA'
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
      object QRLMED: TQRLabel
        Left = 475
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1256.770833333333000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMED'
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
      object QRLFaltas: TQRLabel
        Left = 590
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1561.041666666667000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMED'
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
      object QRLStatus: TQRLabel
        Left = 623
        Top = 1
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1648.354166666667000000
          2.645833333333333000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'REPROVADO'
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
      object QRLNPR: TQRLabel
        Left = 531
        Top = 1
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1404.937500000000000000
          2.645833333333333000
          58.208333333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '-'
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
      object QRLMB1: TQRLabel
        Left = 354
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          936.625000000000000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMB1'
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
      object QRLMB2: TQRLabel
        Left = 384
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1016.000000000000000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMB2'
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
      object QRLMB3: TQRLabel
        Left = 414
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1095.375000000000000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMB3'
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
      object QRLMB4: TQRLabel
        Left = 444
        Top = 1
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1174.750000000000000000
          2.645833333333333000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMB4'
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
      object QRLNPF: TQRLabel
        Left = 504
        Top = 1
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1333.500000000000000000
          2.645833333333333000
          55.562500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLNPF'
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
      object QRLMF: TQRLabel
        Left = 558
        Top = 1
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1476.375000000000000000
          2.645833333333333000
          55.562500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMF'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRDBText3: TQRDBText
        Left = 119
        Top = 1
        Width = 72
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          314.854166666666700000
          2.645833333333333000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_SIGL_CUR'
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
      object QRDBText4: TQRDBText
        Left = 157
        Top = 1
        Width = 42
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          415.395833333333400000
          2.645833333333333000
          111.125000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_TUR'
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
    object QRGroup1: TQRGroup
      Left = 48
      Top = 183
      Width = 698
      Height = 46
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
        121.708333333333300000
        1846.791666666667000000)
      Expression = 'QryRelatorio.CO_ALU'
      FooterBand = QRBand2
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape2: TQRShape
        Left = 0
        Top = 10
        Width = 698
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          0.000000000000000000
          26.458333333333330000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape1: TQRShape
        Left = 0
        Top = 28
        Width = 698
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          0.000000000000000000
          74.083333333333340000
          1846.791666666667000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 43
        Top = 30
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          113.770833333333300000
          79.375000000000000000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel13: TQRLabel
        Left = 204
        Top = 30
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          539.750000000000000000
          79.375000000000000000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MAT'#201'RIA'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel2: TQRLabel
        Left = 354
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          936.625000000000000000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MB1'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel5: TQRLabel
        Left = 7
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          18.520833333333330000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ANO'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel6: TQRLabel
        Left = 384
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1016.000000000000000000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MB2'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel7: TQRLabel
        Left = 414
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1095.375000000000000000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MB3'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel8: TQRLabel
        Left = 444
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1174.750000000000000000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MB4'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel9: TQRLabel
        Left = 475
        Top = 30
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1256.770833333333000000
          79.375000000000000000
          63.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MED'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel11: TQRLabel
        Left = 504
        Top = 30
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1333.500000000000000000
          79.375000000000000000
          55.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NPF'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel14: TQRLabel
        Left = 531
        Top = 30
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1404.937500000000000000
          79.375000000000000000
          58.208333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NPR'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel15: TQRLabel
        Left = 560
        Top = 30
        Width = 17
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1481.666666666667000000
          79.375000000000000000
          44.979166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MF'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel16: TQRLabel
        Left = 590
        Top = 30
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1561.041666666667000000
          79.375000000000000000
          58.208333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FAL'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel17: TQRLabel
        Left = 622
        Top = 30
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1645.708333333333000000
          79.375000000000000000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'STATUS'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QrlTitSerie: TQRLabel
        Left = 119
        Top = 30
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          314.854166666666700000
          79.375000000000000000
          79.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'S'#201'RIE'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel19: TQRLabel
        Left = 157
        Top = 30
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          415.395833333333400000
          79.375000000000000000
          105.833333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TURMA'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLALuno: TQRLabel
        Left = 3
        Top = 12
        Width = 267
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          31.750000000000000000
          706.437500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLAluno'
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
    object QRBand2: TQRBand
      Left = 48
      Top = 247
      Width = 698
      Height = 18
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = QRBand2AfterPrint
      AlignToBottom = False
      BeforePrint = QRBand2BeforePrint
      Color = clGradientInactiveCaption
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        47.625000000000000000
        1846.791666666667000000)
      BandType = rbGroupFooter
      object QRLabel18: TQRLabel
        Left = 182
        Top = 2
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          481.541666666666700000
          5.291666666666667000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'M'#233'dia'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMB1: TQRLabel
        Left = 354
        Top = 2
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          936.625000000000000000
          5.291666666666667000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MB1'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMB2: TQRLabel
        Left = 384
        Top = 2
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1016.000000000000000000
          5.291666666666667000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MB2'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMB3: TQRLabel
        Left = 414
        Top = 2
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1095.375000000000000000
          5.291666666666667000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MB3'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMB4: TQRLabel
        Left = 444
        Top = 2
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1174.750000000000000000
          5.291666666666667000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MB4'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMED: TQRLabel
        Left = 475
        Top = 2
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1256.770833333333000000
          5.291666666666667000
          63.500000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MED'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotMF: TQRLabel
        Left = 560
        Top = 2
        Width = 17
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1481.666666666667000000
          5.291666666666667000
          44.979166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'MF'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotFaltas: TQRLabel
        Left = 590
        Top = 2
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1561.041666666667000000
          5.291666666666667000
          66.145833333333320000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'FALTAS'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotStatus: TQRLabel
        Left = 622
        Top = 2
        Width = 62
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1645.708333333333000000
          5.291666666666667000
          164.041666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'APROVADO'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotNPR: TQRLabel
        Left = 531
        Top = 2
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1404.937500000000000000
          5.291666666666667000
          58.208333333333320000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '-'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTotNPF: TQRLabel
        Left = 504
        Top = 2
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1333.500000000000000000
          5.291666666666667000
          55.562500000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '-'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    SQL.Strings = (
      
        'Select Distinct H.*,                 CM.NO_MATERIA,mm.co_sit_mat' +
        ',                 CM.NO_SIGLA_MATERIA,                 M.QT_CARG' +
        '_HORA_MAT,'
      
        'M.QT_CRED_MAT,                 MD.DE_MODU_CUR,                 c' +
        't.co_sigla_turma as no_tur, H.VL_MEDIA_FINAL, a.no_alu, a.nu_nis' +
        ', mm.co_alu_cad, c.no_cur,C.CO_SIGL_CUR '
      
        'From TB079_HIST_ALUNO H,      TB02_MATERIA M,      TB107_CADMATE' +
        'RIAS CM,      TB44_MODULO MD,       '
      
        'TB06_TURMAS T,tb129_cadturmas ct,tb07_aluno a, tb08_matrcur mm, ' +
        'tb01_curso c'
      
        'Where H.CO_EMP = M.CO_EMP   And H.CO_MAT = M.CO_MAT   AND M.ID_M' +
        'ATERIA = CM.ID_MATERIA   '
      'and h.co_alu = a.co_alu and h.co_emp = a.co_emp'
      'and h.co_cur = c.co_cur and h.co_emp = c.co_emp'
      'and h.co_alu = mm.co_alu and h.co_emp = mm.co_emp'
      
        'And T.CO_EMP = H.CO_EMP   And T.CO_CUR = H.CO_CUR   And T.CO_TUR' +
        ' = H.CO_TUR'
      'And T.CO_TUR = ct.CO_TUR')
    object QryRelatorioCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryRelatorioCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
    object QryRelatorioCO_MODU_CUR: TIntegerField
      FieldName = 'CO_MODU_CUR'
    end
    object QryRelatorioCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryRelatorioCO_ANO_REF: TStringField
      FieldName = 'CO_ANO_REF'
      FixedChar = True
      Size = 6
    end
    object QryRelatorioCO_MAT: TIntegerField
      FieldName = 'CO_MAT'
    end
    object QryRelatorioCO_TUR: TIntegerField
      FieldName = 'CO_TUR'
    end
    object QryRelatorioDT_LANC: TDateTimeField
      FieldName = 'DT_LANC'
    end
    object QryRelatorioQT_FALTA_MES1: TIntegerField
      FieldName = 'QT_FALTA_MES1'
    end
    object QryRelatorioVL_NOTA_MES1: TBCDField
      FieldName = 'VL_NOTA_MES1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES1: TBCDField
      FieldName = 'VL_RECU_MES1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES2: TIntegerField
      FieldName = 'QT_FALTA_MES2'
    end
    object QryRelatorioVL_NOTA_MES2: TBCDField
      FieldName = 'VL_NOTA_MES2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES2: TBCDField
      FieldName = 'VL_RECU_MES2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES3: TIntegerField
      FieldName = 'QT_FALTA_MES3'
    end
    object QryRelatorioVL_NOTA_MES3: TBCDField
      FieldName = 'VL_NOTA_MES3'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES3: TBCDField
      FieldName = 'VL_RECU_MES3'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES4: TIntegerField
      FieldName = 'QT_FALTA_MES4'
    end
    object QryRelatorioVL_NOTA_MES4: TBCDField
      FieldName = 'VL_NOTA_MES4'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES4: TBCDField
      FieldName = 'VL_RECU_MES4'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES5: TIntegerField
      FieldName = 'QT_FALTA_MES5'
    end
    object QryRelatorioVL_NOTA_MES5: TBCDField
      FieldName = 'VL_NOTA_MES5'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES5: TBCDField
      FieldName = 'VL_RECU_MES5'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES6: TIntegerField
      FieldName = 'QT_FALTA_MES6'
    end
    object QryRelatorioVL_NOTA_MES6: TBCDField
      FieldName = 'VL_NOTA_MES6'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES6: TBCDField
      FieldName = 'VL_RECU_MES6'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES7: TIntegerField
      FieldName = 'QT_FALTA_MES7'
    end
    object QryRelatorioVL_NOTA_MES7: TBCDField
      FieldName = 'VL_NOTA_MES7'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES7: TBCDField
      FieldName = 'VL_RECU_MES7'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES8: TIntegerField
      FieldName = 'QT_FALTA_MES8'
    end
    object QryRelatorioVL_NOTA_MES8: TBCDField
      FieldName = 'VL_NOTA_MES8'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES8: TBCDField
      FieldName = 'VL_RECU_MES8'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES9: TIntegerField
      FieldName = 'QT_FALTA_MES9'
    end
    object QryRelatorioVL_NOTA_MES9: TBCDField
      FieldName = 'VL_NOTA_MES9'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES9: TBCDField
      FieldName = 'VL_RECU_MES9'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES10: TIntegerField
      FieldName = 'QT_FALTA_MES10'
    end
    object QryRelatorioVL_NOTA_MES10: TBCDField
      FieldName = 'VL_NOTA_MES10'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES10: TBCDField
      FieldName = 'VL_RECU_MES10'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES11: TIntegerField
      FieldName = 'QT_FALTA_MES11'
    end
    object QryRelatorioVL_NOTA_MES11: TBCDField
      FieldName = 'VL_NOTA_MES11'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES11: TBCDField
      FieldName = 'VL_RECU_MES11'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_MES12: TIntegerField
      FieldName = 'QT_FALTA_MES12'
    end
    object QryRelatorioVL_NOTA_MES12: TBCDField
      FieldName = 'VL_NOTA_MES12'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_MES12: TBCDField
      FieldName = 'VL_RECU_MES12'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_BIM1: TIntegerField
      FieldName = 'QT_FALTA_BIM1'
    end
    object QryRelatorioVL_NOTA_BIM1: TBCDField
      FieldName = 'VL_NOTA_BIM1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_BIM1: TBCDField
      FieldName = 'VL_RECU_BIM1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_BIM2: TIntegerField
      FieldName = 'QT_FALTA_BIM2'
    end
    object QryRelatorioVL_NOTA_BIM2: TBCDField
      FieldName = 'VL_NOTA_BIM2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_BIM2: TBCDField
      FieldName = 'VL_RECU_BIM2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_BIM3: TIntegerField
      FieldName = 'QT_FALTA_BIM3'
    end
    object QryRelatorioVL_NOTA_BIM3: TBCDField
      FieldName = 'VL_NOTA_BIM3'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_BIM3: TBCDField
      FieldName = 'VL_RECU_BIM3'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_BIM4: TIntegerField
      FieldName = 'QT_FALTA_BIM4'
    end
    object QryRelatorioVL_NOTA_BIM4: TBCDField
      FieldName = 'VL_NOTA_BIM4'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_BIM4: TBCDField
      FieldName = 'VL_RECU_BIM4'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_SEM1: TIntegerField
      FieldName = 'QT_FALTA_SEM1'
    end
    object QryRelatorioVL_NOTA_SEM1: TBCDField
      FieldName = 'VL_NOTA_SEM1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_SEM1: TBCDField
      FieldName = 'VL_RECU_SEM1'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_FALTA_SEM2: TIntegerField
      FieldName = 'QT_FALTA_SEM2'
    end
    object QryRelatorioVL_NOTA_SEM2: TBCDField
      FieldName = 'VL_NOTA_SEM2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_RECU_SEM2: TBCDField
      FieldName = 'VL_RECU_SEM2'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_MEDIA_ANUAL: TBCDField
      FieldName = 'VL_MEDIA_ANUAL'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_PROVA_FINAL: TBCDField
      FieldName = 'VL_PROVA_FINAL'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_MEDIA_FINAL: TBCDField
      FieldName = 'VL_MEDIA_FINAL'
      Precision = 4
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES1: TStringField
      FieldName = 'VL_CRIT_MES1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES1: TStringField
      FieldName = 'VL_RESP_MES1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES1: TStringField
      FieldName = 'VL_APRE_MES1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES2: TStringField
      FieldName = 'VL_CRIT_MES2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES2: TStringField
      FieldName = 'VL_RESP_MES2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES2: TStringField
      FieldName = 'VL_APRE_MES2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES3: TStringField
      FieldName = 'VL_CRIT_MES3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES3: TStringField
      FieldName = 'VL_RESP_MES3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES3: TStringField
      FieldName = 'VL_APRE_MES3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES4: TStringField
      FieldName = 'VL_CRIT_MES4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES4: TStringField
      FieldName = 'VL_RESP_MES4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES4: TStringField
      FieldName = 'VL_APRE_MES4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES5: TStringField
      FieldName = 'VL_CRIT_MES5'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES5: TStringField
      FieldName = 'VL_RESP_MES5'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES5: TStringField
      FieldName = 'VL_APRE_MES5'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES6: TStringField
      FieldName = 'VL_CRIT_MES6'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES6: TStringField
      FieldName = 'VL_RESP_MES6'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES6: TStringField
      FieldName = 'VL_APRE_MES6'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES7: TStringField
      FieldName = 'VL_CRIT_MES7'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES7: TStringField
      FieldName = 'VL_RESP_MES7'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES7: TStringField
      FieldName = 'VL_APRE_MES7'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES8: TStringField
      FieldName = 'VL_CRIT_MES8'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES8: TStringField
      FieldName = 'VL_RESP_MES8'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES8: TStringField
      FieldName = 'VL_APRE_MES8'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES9: TStringField
      FieldName = 'VL_CRIT_MES9'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES9: TStringField
      FieldName = 'VL_RESP_MES9'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES9: TStringField
      FieldName = 'VL_APRE_MES9'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES10: TStringField
      FieldName = 'VL_CRIT_MES10'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES10: TStringField
      FieldName = 'VL_RESP_MES10'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES10: TStringField
      FieldName = 'VL_APRE_MES10'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES11: TStringField
      FieldName = 'VL_CRIT_MES11'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES11: TStringField
      FieldName = 'VL_RESP_MES11'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES11: TStringField
      FieldName = 'VL_APRE_MES11'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_MES12: TStringField
      FieldName = 'VL_CRIT_MES12'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_MES12: TStringField
      FieldName = 'VL_RESP_MES12'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_MES12: TStringField
      FieldName = 'VL_APRE_MES12'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_BIM1: TStringField
      FieldName = 'VL_CRIT_BIM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_BIM1: TStringField
      FieldName = 'VL_RESP_BIM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_BIM1: TStringField
      FieldName = 'VL_APRE_BIM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_BIM2: TStringField
      FieldName = 'VL_CRIT_BIM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_BIM2: TStringField
      FieldName = 'VL_RESP_BIM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_BIM2: TStringField
      FieldName = 'VL_APRE_BIM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_BIM3: TStringField
      FieldName = 'VL_CRIT_BIM3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_BIM3: TStringField
      FieldName = 'VL_RESP_BIM3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_BIM3: TStringField
      FieldName = 'VL_APRE_BIM3'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_BIM4: TStringField
      FieldName = 'VL_CRIT_BIM4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_BIM4: TStringField
      FieldName = 'VL_RESP_BIM4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_BIM4: TStringField
      FieldName = 'VL_APRE_BIM4'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_SEM1: TStringField
      FieldName = 'VL_CRIT_SEM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_SEM1: TStringField
      FieldName = 'VL_RESP_SEM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_SEM1: TStringField
      FieldName = 'VL_APRE_SEM1'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_CRIT_SEM2: TStringField
      FieldName = 'VL_CRIT_SEM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_RESP_SEM2: TStringField
      FieldName = 'VL_RESP_SEM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioVL_APRE_SEM2: TStringField
      FieldName = 'VL_APRE_SEM2'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioCO_USUARIO: TIntegerField
      FieldName = 'CO_USUARIO'
    end
    object QryRelatorioCO_STA_APROV_MATERIA: TStringField
      FieldName = 'CO_STA_APROV_MATERIA'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I1: TStringField
      FieldName = 'ASAC_1S_I1'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I2: TStringField
      FieldName = 'ASAC_1S_I2'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I3: TStringField
      FieldName = 'ASAC_1S_I3'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I4: TStringField
      FieldName = 'ASAC_1S_I4'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I5: TStringField
      FieldName = 'ASAC_1S_I5'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I6: TStringField
      FieldName = 'ASAC_1S_I6'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I7: TStringField
      FieldName = 'ASAC_1S_I7'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I8: TStringField
      FieldName = 'ASAC_1S_I8'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I9: TStringField
      FieldName = 'ASAC_1S_I9'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I10: TStringField
      FieldName = 'ASAC_1S_I10'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I11: TStringField
      FieldName = 'ASAC_1S_I11'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_1S_I12: TStringField
      FieldName = 'ASAC_1S_I12'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I1: TStringField
      FieldName = 'ASAC_2S_I1'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I2: TStringField
      FieldName = 'ASAC_2S_I2'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I3: TStringField
      FieldName = 'ASAC_2S_I3'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I4: TStringField
      FieldName = 'ASAC_2S_I4'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I5: TStringField
      FieldName = 'ASAC_2S_I5'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I6: TStringField
      FieldName = 'ASAC_2S_I6'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I7: TStringField
      FieldName = 'ASAC_2S_I7'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I8: TStringField
      FieldName = 'ASAC_2S_I8'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I9: TStringField
      FieldName = 'ASAC_2S_I9'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I10: TStringField
      FieldName = 'ASAC_2S_I10'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I11: TStringField
      FieldName = 'ASAC_2S_I11'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioASAC_2S_I12: TStringField
      FieldName = 'ASAC_2S_I12'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioAC_1S: TStringField
      FieldName = 'AC_1S'
      Size = 250
    end
    object QryRelatorioAC_2S: TStringField
      FieldName = 'AC_2S'
      Size = 250
    end
    object QryRelatorioOBS_1S: TStringField
      FieldName = 'OBS_1S'
      Size = 250
    end
    object QryRelatorioOBS_2S: TStringField
      FieldName = 'OBS_2S'
      Size = 250
    end
    object QryRelatorioAM_1S: TStringField
      FieldName = 'AM_1S'
      Size = 250
    end
    object QryRelatorioAM_2S: TStringField
      FieldName = 'AM_2S'
      Size = 250
    end
    object QryRelatorioRS1: TStringField
      FieldName = 'RS1'
      Size = 7
    end
    object QryRelatorioRS2: TStringField
      FieldName = 'RS2'
      Size = 7
    end
    object QryRelatorioRSF: TStringField
      FieldName = 'RSF'
      Size = 7
    end
    object QryRelatorioDT_INI_AVAL_PRO_1S: TDateTimeField
      FieldName = 'DT_INI_AVAL_PRO_1S'
    end
    object QryRelatorioDT_FIN_AVAL_PRO_1S: TDateTimeField
      FieldName = 'DT_FIN_AVAL_PRO_1S'
    end
    object QryRelatorioDT_INI_AVAL_PRO_2S: TDateTimeField
      FieldName = 'DT_INI_AVAL_PRO_2S'
    end
    object QryRelatorioDT_FIN_AVAL_PRO_2S: TDateTimeField
      FieldName = 'DT_FIN_AVAL_PRO_2S'
    end
    object QryRelatorioDT_LANC2: TDateTimeField
      FieldName = 'DT_LANC2'
    end
    object QryRelatorioNO_MATERIA: TStringField
      FieldName = 'NO_MATERIA'
      Size = 100
    end
    object QryRelatorioco_sit_mat: TStringField
      FieldName = 'co_sit_mat'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioNO_SIGLA_MATERIA: TStringField
      FieldName = 'NO_SIGLA_MATERIA'
      Size = 12
    end
    object QryRelatorioQT_CARG_HORA_MAT: TIntegerField
      FieldName = 'QT_CARG_HORA_MAT'
    end
    object QryRelatorioQT_CRED_MAT: TIntegerField
      FieldName = 'QT_CRED_MAT'
    end
    object QryRelatorioDE_MODU_CUR: TStringField
      FieldName = 'DE_MODU_CUR'
      Size = 60
    end
    object QryRelatoriono_tur: TStringField
      FieldName = 'no_tur'
      Size = 10
    end
    object QryRelatorioVL_MEDIA_FINAL_1: TBCDField
      FieldName = 'VL_MEDIA_FINAL_1'
      Precision = 4
      Size = 2
    end
    object QryRelatoriono_alu: TStringField
      FieldName = 'no_alu'
      Size = 80
    end
    object QryRelatorionu_nis: TBCDField
      FieldName = 'nu_nis'
      Precision = 11
      Size = 0
    end
    object QryRelatorioco_alu_cad: TStringField
      FieldName = 'co_alu_cad'
    end
    object QryRelatoriono_cur: TStringField
      FieldName = 'no_cur'
      Size = 50
    end
    object QryRelatorioCO_SIGL_CUR: TStringField
      FieldName = 'CO_SIGL_CUR'
      Size = 8
    end
  end
end
