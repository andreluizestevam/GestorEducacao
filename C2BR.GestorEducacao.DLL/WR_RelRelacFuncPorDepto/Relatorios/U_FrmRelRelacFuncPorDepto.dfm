inherited FrmRelRelacFuncPorDepto: TFrmRelRelacFuncPorDepto
  Left = 208
  Top = 149
  Width = 815
  Height = 588
  Caption = 'FrmRelRelacFuncPorDepto'
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
    inherited PageHeaderBand1: TQRBand
      Width = 1027
      Height = 146
      Frame.Width = 0
      Size.Values = (
        386.291666666666700000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Width = 1027
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'RELA'#199#195'O DE FUNCION'#193'RIOS POR DEPARTAMENTO'
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
        Size.Values = (
          44.979166666666670000
          1640.416666666667000000
          21.166666666666670000
          66.145833333333340000)
        FontSize = 8
      end
      inherited qrlTempleData: TQRLabel
        Size.Values = (
          44.979166666666670000
          1640.416666666667000000
          66.145833333333340000
          68.791666666666680000)
        FontSize = 8
      end
      inherited qrlTempleHora: TQRLabel
        Size.Values = (
          44.979166666666670000
          1640.416666666667000000
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
        Size.Values = (
          44.979166666666670000
          1510.770833333333000000
          108.479166666666700000
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
        Left = 1004
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
          2656.416666666667000000
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
      Top = 272
      Width = 1027
      Size.Values = (
        31.750000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 811
        Width = 216
        Size.Values = (
          29.104166666666670000
          2145.770833333333000000
          0.000000000000000000
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
    object Detail: TQRBand
      Left = 48
      Top = 231
      Width = 1027
      Height = 15
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = DetailBeforePrint
      Color = 14211288
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        39.687500000000000000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText7: TQRDBText
        Left = 334
        Top = 0
        Width = 10
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          883.708333333333400000
          0.000000000000000000
          26.458333333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_SEXO_COL'
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
        Left = 360
        Top = 0
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          952.500000000000000000
          0.000000000000000000
          34.395833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLDeficiencia: TQRLabel
        Left = 393
        Top = 0
        Width = 71
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1039.812500000000000000
          0.000000000000000000
          187.854166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDeficiencia'
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
      object QRLEndereco: TQRLabel
        Left = 589
        Top = 0
        Width = 258
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1558.395833333333000000
          0.000000000000000000
          682.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLEndereco'
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
      object QRLMatricula: TQRLabel
        Left = 5
        Top = 0
        Width = 63
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
          166.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000.0000.XXX'
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
      object QRLSituacao: TQRLabel
        Left = 473
        Top = 0
        Width = 107
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1251.479166666667000000
          0.000000000000000000
          283.104166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Licen'#231'a Maternidade'
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
      object QrlSiglaUnid: TQRLabel
        Left = 868
        Top = 0
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2296.583333333333000000
          0.000000000000000000
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
      object QrlTelMovel: TQRLabel
        Left = 948
        Top = 0
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2508.250000000000000000
          0.000000000000000000
          201.083333333333300000)
        Alignment = taCenter
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
      object QRLNoCol: TQRLabel
        Left = 81
        Top = 0
        Width = 236
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          214.312500000000000000
          0.000000000000000000
          624.416666666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000.0000.XXX'
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
    object QRGroup1: TQRGroup
      Left = 48
      Top = 194
      Width = 1027
      Height = 37
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      Frame.Width = 0
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        97.895833333333340000
        2717.270833333333000000)
      Expression = 'QryRelatorio.no_depto'
      FooterBand = SummaryBand1
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape1: TQRShape
        Left = 0
        Top = 19
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
          50.270833333333330000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel19: TQRLabel
        Left = 330
        Top = 21
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          873.124999999999900000
          55.562500000000000000
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
      object QRLabel22: TQRLabel
        Left = 81
        Top = 21
        Width = 125
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          214.312500000000000000
          55.562500000000000000
          330.729166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME DO FUNCION'#193'RIO'
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
        Left = 3
        Top = 2
        Width = 86
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          7.937500000000000000
          5.291666666666667000
          227.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Departamento:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 10
      end
      object QRDBBairro: TQRDBText
        Left = 102
        Top = 2
        Width = 60
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          269.875000000000000000
          5.291666666666667000
          158.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'no_depto'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 10
      end
      object QRLabel5: TQRLabel
        Left = 352
        Top = 21
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          931.333333333333500000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'IDADE'
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
        Left = 393
        Top = 21
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1039.812500000000000000
          55.562500000000000000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DEFICI'#202'NCIA'
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
      object QrlTitSerieTurma: TQRLabel
        Left = 473
        Top = 21
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1251.479166666667000000
          55.562500000000000000
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
        Left = 589
        Top = 21
        Width = 56
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1558.395833333333000000
          55.562500000000000000
          148.166666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ENDERE'#199'O'
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
        Left = 5
        Top = 21
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          55.562500000000000000
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
      object QRLabel7: TQRLabel
        Left = 868
        Top = 21
        Width = 61
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2296.583333333333000000
          55.562500000000000000
          161.395833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SIGLA UNID'
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
        Left = 948
        Top = 21
        Width = 51
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2508.250000000000000000
          55.562500000000000000
          134.937500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CELULAR'
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
    object SummaryBand1: TQRBand
      Left = 48
      Top = 246
      Width = 1027
      Height = 26
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = SummaryBand1AfterPrint
      AlignToBottom = False
      BeforePrint = SummaryBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        68.791666666666680000
        2717.270833333333000000)
      BandType = rbGroupFooter
      object QRLTotMas: TQRLabel
        Left = 527
        Top = 3
        Width = 47
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1394.354166666667000000
          7.937500000000000000
          124.354166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLTotFem: TQRLabel
        Left = 576
        Top = 3
        Width = 47
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1524.000000000000000000
          7.937500000000000000
          124.354166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLTotGeral: TQRLabel
        Left = 478
        Top = 3
        Width = 47
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1264.708333333333000000
          7.937500000000000000
          124.354166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLTotDef: TQRLabel
        Left = 626
        Top = 3
        Width = 47
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1656.291666666667000000
          7.937500000000000000
          124.354166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      object QRLTotalCompleto: TQRLabel
        Left = 3
        Top = 3
        Width = 396
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          7.937500000000000000
          1047.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 
          'Total de Alunos: 0014 (Masculino: 0005 - Feminino: 0009 - Defici' +
          'ente F'#237'sico: 0002)'
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
      
        'select a.co_alu,a.no_alu,a.nu_nis,a.co_sexo_alu,a.NU_TELE_RESI_A' +
        'LU,a.TP_DEF,a.NU_TELE_CELU_ALU,b.no_bairro,b.co_bairro,e.coduf,c' +
        'i.no_cidade,a.DT_NASC_ALU,a.DE_ENDE_ALU,a.NU_ENDE_ALU,a.DE_COMP_' +
        'ALU,c.no_cur,C.CO_SIGL_CUR,ct.no_turma,ct.CO_SIGLA_TURMA,mm.co_a' +
        'lu_cad,r.no_resp,r.nu_tele_celu_resp from tb07_aluno a   left jo' +
        'in tb74_UF e on e.coduf = a.co_esta_alu   join tb08_matrcur mm o' +
        'n mm.co_alu = a.co_alu and mm.co_emp = a.co_emp   join tb01_curs' +
        'o c on c.co_cur = mm.co_cur and mm.co_emp = c.co_emp   join tb10' +
        '8_responsavel r on r.co_resp = a.co_resp and mm.co_emp = c.co_em' +
        'p   join tb06_turmas t on t.co_tur = mm.co_tur and mm.co_emp = c' +
        '.co_emp  join tb129_cadturmas ct on t.co_tur = ct.co_tur and t.c' +
        'o_modu_cur = ct.co_modu_cur   left join tb905_bairro b on b.co_b' +
        'airro = a.co_bairro and b.co_cidade = a.co_cidade and b.co_uf = ' +
        'a.co_esta_alu   left join tb904_cidade ci on ci.co_cidade = a.co' +
        '_cidade and b.co_uf = a.co_esta_alu   where a.co_emp = 187  and ' +
        'mm.co_sit_mat = '#39'A'#39' and a.co_esta_alu = '#39'PB'#39' and a.co_cidade = 8' +
        ' order by a.co_bairro,a.co_esta_alu,a.no_alu')
  end
end
