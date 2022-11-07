inherited FrmRelDemonstrativoInscricao: TFrmRelDemonstrativoInscricao
  Left = 190
  Top = 111
  Width = 1372
  Height = 753
  Caption = 'FrmRelDemonstrativoInscricao'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 8
    Top = 8
    Width = 1123
    Height = 794
    BeforePrint = QuickRep1BeforePrint
    DataSet = QryRelatorio
    Functions.DATA = (
      '0'
      '0'
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
    object detail: TQRBand [0]
      Left = 48
      Top = 218
      Width = 1027
      Height = 18
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = detailBeforePrint
      Color = 14211288
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        47.625000000000000000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText3: TQRDBText
        Left = 506
        Top = 2
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1338.791666666667000000
          5.291666666666667000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DT_INSC_ALU'
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
      object QRDBText13: TQRDBText
        Left = 931
        Top = 2
        Width = 12
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2463.270833333333000000
          5.291666666666667000
          31.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataField = 'CO_SEXO_RESP'
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
      object QRDBText12: TQRDBText
        Left = 850
        Top = 2
        Width = 77
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2248.958333333333000000
          5.291666666666667000
          203.729166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NU_CPF_RESP'
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
      object QRDBText11: TQRDBText
        Left = 640
        Top = 2
        Width = 204
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1693.333333333333000000
          5.291666666666667000
          539.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_RESP'
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
      object QRLStatusInsc: TQRLabel
        Left = 569
        Top = 2
        Width = 52
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1505.479166666667000000
          5.291666666666667000
          137.583333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Em Aberto'
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
      object QRLNumero: TQRLabel
        Left = 436
        Top = 2
        Width = 64
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1153.583333333333000000
          5.291666666666667000
          169.333333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '000000'
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
      object QRDBText9: TQRDBText
        Left = 334
        Top = 2
        Width = 95
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          883.708333333333400000
          5.291666666666667000
          251.354166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_BAIRRO'
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
      object QRLIdade: TQRLabel
        Left = 311
        Top = 2
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          822.854166666666600000
          5.291666666666667000
          34.395833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
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
      object QRDBText8: TQRDBText
        Left = 246
        Top = 2
        Width = 58
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          650.875000000000000000
          5.291666666666667000
          153.458333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DT_NASC_ALU'
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
      object QRDBText7: TQRDBText
        Left = 227
        Top = 2
        Width = 13
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          600.604166666666800000
          5.291666666666667000
          34.395833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'co_sexo_alu'
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
      object QrlTel: TQRLabel
        Left = 949
        Top = 1
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2510.895833333333000000
          2.645833333333333000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '(00) 0000-0000'
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
      object QRLNoAlu: TQRLabel
        Left = 4
        Top = 2
        Width = 215
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          5.291666666666667000
          568.854166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLNoAlu'
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
    end
    inherited PageHeaderBand1: TQRBand
      Width = 1027
      Height = 121
      Frame.DrawBottom = False
      Size.Values = (
        320.145833333333400000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Left = 166
        Top = 92
        Size.Values = (
          60.854166666666680000
          439.208333333333400000
          243.416666666666700000
          1836.208333333333000000)
        Caption = 'DEMOSTRATIVO DE PR'#201'-MATR'#205'CULA'
        Font.Height = -15
        FontSize = 11
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
        Left = 925
        Width = 32
        Size.Values = (
          44.979166666666670000
          2447.395833333333000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 925
        Width = 32
        Size.Values = (
          44.979166666666670000
          2447.395833333333000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 925
        Width = 32
        Size.Values = (
          44.979166666666670000
          2447.395833333333000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 972
        Size.Values = (
          44.979166666666670000
          2571.750000000000000000
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
      object QRLPage: TQRLabel
        Left = 1008
        Top = 8
        Width = 20
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2667.000000000000000000
          21.166666666666670000
          52.916666666666660000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLabel7: TQRLabel
        Left = 1000
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
          2645.833333333333000000
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
    end
    inherited QRBANDSGIE: TQRBand
      Top = 269
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
      Top = 169
      Width = 1027
      Height = 49
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRGroup1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = True
      Size.Values = (
        129.645833333333300000
        2717.270833333333000000)
      Expression = 'RXMPesquisa.NO_CUR'
      Master = QuickRep1
      ReprintOnNewPage = False
      object QRShape6: TQRShape
        Left = 0
        Top = 30
        Width = 1027
        Height = 20
        Frame.Color = 5329233
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          52.916666666666660000
          0.000000000000000000
          79.375000000000000000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Color = 5329233
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel24: TQRLabel
        Left = 506
        Top = 32
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1338.791666666667000000
          84.666666666666680000
          113.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT INSC'
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
      object QRLabel23: TQRLabel
        Left = 569
        Top = 32
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1505.479166666667000000
          84.666666666666680000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'STATUS'
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
        Left = 640
        Top = 32
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1693.333333333333000000
          84.666666666666680000
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
      object QRLabel3: TQRLabel
        Left = 949
        Top = 32
        Width = 54
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2510.895833333333000000
          84.666666666666680000
          142.875000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TELEFONE'
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
        Left = 929
        Top = 32
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2457.979166666667000000
          84.666666666666680000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
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
        Left = 850
        Top = 32
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2248.958333333333000000
          84.666666666666680000
          58.208333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CPF'
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
      object QRLabel25: TQRLabel
        Left = 436
        Top = 32
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1153.583333333333000000
          84.666666666666680000
          105.833333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' INSC'
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
        Left = 4
        Top = 32
        Width = 72
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          84.666666666666680000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME ALUNO'
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
      object QRLabel1: TQRLabel
        Left = 226
        Top = 32
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          597.958333333333400000
          84.666666666666680000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
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
        Left = 246
        Top = 32
        Width = 48
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          650.875000000000000000
          84.666666666666680000
          127.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT NASC'
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
        Left = 309
        Top = 32
        Width = 19
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          817.562500000000000000
          84.666666666666680000
          50.270833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'IDA'
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
        Left = 334
        Top = 32
        Width = 41
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          883.708333333333400000
          84.666666666666680000
          108.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'BAIRRO'
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
      object qrlParametros: TQRLabel
        Left = 0
        Top = 5
        Width = 1027
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          13.229166666666670000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Per'#237'odo de:'
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
    object SummaryBand1: TQRBand
      Left = 48
      Top = 236
      Width = 1027
      Height = 33
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = SummaryBand1AfterPrint
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        87.312500000000000000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel19: TQRLabel
        Left = 958
        Top = 4
        Width = 38
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2534.708333333333000000
          10.583333333333330000
          100.541666666666700000)
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
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLTotal: TQRLabel
        Left = 1003
        Top = 4
        Width = 7
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2653.770833333333000000
          10.583333333333330000
          18.520833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '0'
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
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      'SET LANGUAGE PORTUGUESE                   '
      
        'SELECT R.NU_TELE_RESI_RESP, R.NO_RESP, R.NU_CPF_RESP, R.CO_SEXO_' +
        'RESP, R.DT_NASC_RESP, B.NO_BAIRRO, C.NO_CIDADE, U.DESCRICAOUF, C' +
        'UR.NO_CUR, '
      'I.*,mo.de_modu_cur '
      'FROM tb46_inscricao i  '
      'JOIN tb108_responsavel r ON i.co_resp = r.co_resp  '
      'JOIN tb904_cidade c ON i.co_cidade = c.co_cidade  '
      
        'JOIN tb905_bairro b ON b.co_bairro = i.co_bairro and b.co_cidade' +
        ' = r.co_cidade  '
      'JOIN tb45_opcao_insc o ON o.nu_insc_alu = i.nu_insc_alu  '
      
        'JOIN tb74_UF u ON u.coduf = i.co_esta_alu and u.coduf = r.co_est' +
        'a_resp  '
      'JOIN tb01_curso cur ON cur.co_cur = o.co_cur  '
      'JOIN tb44_modulo mo on mo.co_modu_cur = i.co_modu_cur  '
      
        'where cur.co_emp = 187 and i.co_emp = 187 and i.co_modu_cur = 1 ' +
        'and i.co_emp = o.co_emp  and i.co_alu = o.co_alu  and '
      
        'i.dt_insc_alu >= '#39'01/01/2009'#39' and i.dt_insc_alu <= '#39'12/01/2010'#39' ' +
        'and o.co_cur = 16 '
      'ORDER BY cur.no_cur,i.no_alu')
    object QryRelatorioNU_TELE_RESI_RESP: TStringField
      FieldName = 'NU_TELE_RESI_RESP'
      EditMask = '!\(99\) 9999\-9999;0;_'
      Size = 10
    end
    object QryRelatorioNO_RESP: TStringField
      FieldName = 'NO_RESP'
      Size = 60
    end
    object QryRelatorioNU_CPF_RESP: TStringField
      FieldName = 'NU_CPF_RESP'
      EditMask = '!999\.999.999\-99;0;_'
      Size = 18
    end
    object QryRelatorioCO_SEXO_RESP: TStringField
      FieldName = 'CO_SEXO_RESP'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDT_NASC_RESP: TDateTimeField
      FieldName = 'DT_NASC_RESP'
    end
    object QryRelatorioNO_BAIRRO: TStringField
      FieldName = 'NO_BAIRRO'
      Size = 80
    end
    object QryRelatorioNO_CIDADE: TStringField
      FieldName = 'NO_CIDADE'
      Size = 80
    end
    object QryRelatorioDESCRICAOUF: TStringField
      FieldName = 'DESCRICAOUF'
      FixedChar = True
      Size = 30
    end
    object QryRelatorioNO_CUR: TStringField
      FieldName = 'NO_CUR'
      Size = 50
    end
    object QryRelatorioCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryRelatorioNU_INSC_ALU: TIntegerField
      FieldName = 'NU_INSC_ALU'
    end
    object QryRelatorioTIPO: TStringField
      FieldName = 'TIPO'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
    object QryRelatorioDT_INSC_ALU: TDateTimeField
      FieldName = 'DT_INSC_ALU'
    end
    object QryRelatorioCO_STAT_ENTR_RG: TStringField
      FieldName = 'CO_STAT_ENTR_RG'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_CPF: TStringField
      FieldName = 'CO_STAT_ENTR_CPF'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_CUR: TStringField
      FieldName = 'CO_STAT_ENTR_CUR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_DIP: TStringField
      FieldName = 'CO_STAT_ENTR_DIP'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_HIS: TStringField
      FieldName = 'CO_STAT_ENTR_HIS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_FOT: TStringField
      FieldName = 'CO_STAT_ENTR_FOT'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_ENTR_CAR: TStringField
      FieldName = 'CO_STAT_ENTR_CAR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_PAGA_INS: TStringField
      FieldName = 'CO_STAT_PAGA_INS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioNU_DOCU_PAGA_INS: TStringField
      FieldName = 'NU_DOCU_PAGA_INS'
      Size = 10
    end
    object QryRelatorioCO_STAT_PS_ENTR: TStringField
      FieldName = 'CO_STAT_PS_ENTR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_PS_DISS: TStringField
      FieldName = 'CO_STAT_PS_DISS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_RESU_PS: TStringField
      FieldName = 'CO_RESU_PS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDT_RESU_PS: TDateTimeField
      FieldName = 'DT_RESU_PS'
    end
    object QryRelatorioCO_RES_INS: TStringField
      FieldName = 'CO_RES_INS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_CURS_ORIG: TBCDField
      FieldName = 'VL_CURS_ORIG'
      Precision = 7
      Size = 2
    end
    object QryRelatorioVL_CURS_CORR: TBCDField
      FieldName = 'VL_CURS_CORR'
      Precision = 7
      Size = 2
    end
    object QryRelatorioCO_SITU_INS: TStringField
      FieldName = 'CO_SITU_INS'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_SITU_MAT: TStringField
      FieldName = 'CO_SITU_MAT'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_INSC_CUR: TBCDField
      FieldName = 'VL_INSC_CUR'
      Precision = 7
      Size = 2
    end
    object QryRelatorioDE_ANO_INSC: TStringField
      FieldName = 'DE_ANO_INSC'
      FixedChar = True
      Size = 4
    end
    object QryRelatorioCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryRelatorioDE_PAR_INSC: TStringField
      FieldName = 'DE_PAR_INSC'
      Size = 200
    end
    object QryRelatorioCO_RESE_MATR: TIntegerField
      FieldName = 'CO_RESE_MATR'
    end
    object QryRelatorioCO_STAT_PS_TEST: TStringField
      FieldName = 'CO_STAT_PS_TEST'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_STAT_PS_TEST_NOTA: TBCDField
      FieldName = 'CO_STAT_PS_TEST_NOTA'
      Precision = 4
      Size = 2
    end
    object QryRelatorioCO_STAT_PS_DISS_NOTA: TBCDField
      FieldName = 'CO_STAT_PS_DISS_NOTA'
      Precision = 4
      Size = 2
    end
    object QryRelatorioNO_ALU: TStringField
      FieldName = 'NO_ALU'
      Size = 60
    end
    object QryRelatorioNU_CPF_ALU: TStringField
      FieldName = 'NU_CPF_ALU'
      EditMask = '!999\.999.999\-99;0;_'
      Size = 18
    end
    object QryRelatorioCO_SEXO_ALU: TStringField
      FieldName = 'CO_SEXO_ALU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDT_NASC_ALU: TDateTimeField
      FieldName = 'DT_NASC_ALU'
    end
    object QryRelatorioDE_END_ALU: TStringField
      FieldName = 'DE_END_ALU'
      Size = 60
    end
    object QryRelatorioNU_END_ALU: TIntegerField
      FieldName = 'NU_END_ALU'
    end
    object QryRelatorioDE_COMP_ALU: TStringField
      FieldName = 'DE_COMP_ALU'
      Size = 40
    end
    object QryRelatorioNU_TELE_RESI_ALU: TStringField
      FieldName = 'NU_TELE_RESI_ALU'
      EditMask = '!\(99\) 9999\-9999;0;_'
      Size = 19
    end
    object QryRelatorioCO_BAIRRO: TIntegerField
      FieldName = 'CO_BAIRRO'
    end
    object QryRelatorioCO_CIDADE: TIntegerField
      FieldName = 'CO_CIDADE'
    end
    object QryRelatorioCO_ESTA_ALU: TStringField
      FieldName = 'CO_ESTA_ALU'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioNU_PREMAT: TStringField
      FieldName = 'NU_PREMAT'
      Size = 16
    end
    object QryRelatorioCO_CEP_ALU: TStringField
      FieldName = 'CO_CEP_ALU'
      Size = 12
    end
    object QryRelatorioNU_TELE_CELU_ALU: TStringField
      FieldName = 'NU_TELE_CELU_ALU'
      EditMask = '!\(99\) 9999\-9999;0;_'
      Size = 50
    end
    object QryRelatorioCO_RESP: TIntegerField
      FieldName = 'CO_RESP'
    end
    object QryRelatorioNU_MATRICULA: TIntegerField
      FieldName = 'NU_MATRICULA'
    end
    object QryRelatorioCO_MODU_CUR: TIntegerField
      FieldName = 'CO_MODU_CUR'
    end
    object QryRelatorioNU_NIS: TBCDField
      FieldName = 'NU_NIS'
      Precision = 11
      Size = 0
    end
    object QryRelatoriode_modu_cur: TStringField
      FieldName = 'de_modu_cur'
      Size = 60
    end
  end
end
