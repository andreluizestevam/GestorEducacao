inherited FrmRelHistoTarefAgendada: TFrmRelHistoTarefAgendada
  Left = 209
  Top = 150
  HorzScrollBar.Position = 157
  Caption = 'FrmRelHistoTarefAgendada'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = -149
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
      Height = 179
      Frame.DrawBottom = False
      Size.Values = (
        473.604166666666700000
        2717.270833333333000000)
      object QRShape2: TQRShape [0]
        Left = 0
        Top = 161
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
          425.979166666666700000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Color = clMedGray
        Shape = qrsRectangle
      end
      inherited LblTituloRel: TQRLabel
        Width = 1027
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'HIST'#211'RICO DE TAREFA AGENDADA'
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
        Left = -16
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
      object QrlParametros: TQRLabel
        Left = -8
        Top = 139
        Width = 1027
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          -21.166666666666670000
          367.770833333333400000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'S'#233'rie:'
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
      object QRLabel5: TQRLabel
        Left = 440
        Top = 162
        Width = 136
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1164.166666666667000000
          428.625000000000000000
          359.833333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RESPONS'#193'VEL / EMISSOR'
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
        Left = 732
        Top = 162
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1936.750000000000000000
          428.625000000000000000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNID'
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
        Left = 782
        Top = 162
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2069.041666666667000000
          428.625000000000000000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SMS'
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
        Top = 162
        Width = 55
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          428.625000000000000000
          145.520833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DTCOMPR'
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
        Left = 827
        Top = 162
        Width = 33
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2188.104166666667000000
          428.625000000000000000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PRIOR'
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
        Left = 69
        Top = 162
        Width = 55
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          182.562500000000000000
          428.625000000000000000
          145.520833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT PRAZO'
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
        Left = 135
        Top = 162
        Width = 56
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          357.187500000000000000
          428.625000000000000000
          148.166666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT CADAS'
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
        Left = 880
        Top = 162
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2328.333333333333000000
          428.625000000000000000
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
      object QRLabel8: TQRLabel
        Left = 203
        Top = 162
        Width = 108
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          537.104166666666800000
          428.625000000000000000
          285.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DESCRI'#199#195'O TAREFA'
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
    inherited QRBANDSGIE: TQRBand
      Top = 313
      Width = 1027
      Size.Values = (
        31.750000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = -316
        Width = 331
        Size.Values = (
          29.104166666666670000
          1841.500000000000000000
          0.000000000000000000
          875.770833333333400000)
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
      Top = 248
      Width = 1027
      Height = 34
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
        89.958333333333340000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText9: TQRDBText
        Left = 880
        Top = 1
        Width = 123
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2328.333333333333000000
          2.645833333333333000
          325.437500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_SITU_TAREF_AGEND'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = False
        FontSize = 8
      end
      object QRDBText10: TQRDBText
        Left = 827
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
          2188.104166666667000000
          2.645833333333333000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_PRIOR_TAREF_AGEND'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = False
        FontSize = 8
      end
      object QRLDtCompr: TQRLabel
        Left = 5
        Top = 1
        Width = 57
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
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRLSMS: TQRLabel
        Left = 782
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
          2069.041666666667000000
          2.645833333333333000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRLDtPrazo: TQRLabel
        Left = 69
        Top = 1
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          182.562500000000000000
          2.645833333333333000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRLDtCadas: TQRLabel
        Left = 135
        Top = 1
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          357.187500000000000000
          2.645833333333333000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRLResponsavel: TQRLabel
        Left = 440
        Top = 1
        Width = 124
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1164.166666666667000000
          2.645833333333333000
          328.083333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRLEmissor: TQRLabel
        Left = 440
        Top = 18
        Width = 124
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1164.166666666667000000
          47.625000000000000000
          328.083333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QRLDtCompromissoLimite'
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
      object QRDBText1: TQRDBText
        Left = 732
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
          1936.750000000000000000
          2.645833333333333000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'siglaResp'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = False
        FontSize = 8
      end
      object QRDBText2: TQRDBText
        Left = 732
        Top = 18
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1936.750000000000000000
          47.625000000000000000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'siglaSol'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = False
        FontSize = 8
      end
      object QRDBText3: TQRDBText
        Left = 203
        Top = 1
        Width = 231
        Height = 32
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          84.666666666666680000
          537.104166666666800000
          2.645833333333333000
          611.187500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_DETAL_TAREF_AGEND'
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
      Top = 227
      Width = 1027
      Height = 21
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
        55.562500000000000000
        2717.270833333333000000)
      Expression = 'QryRelatorio.CO_CHAVE_UNICA_TAREF'
      FooterBand = QRBand2
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape1: TQRShape
        Left = 0
        Top = 1
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
          2.645833333333333000
          2717.270833333333000000)
        Brush.Color = clMedGray
        Pen.Color = clGray
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 0
        Top = 4
        Width = 36
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          0.000000000000000000
          10.583333333333330000
          95.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Tarefa:'
        Color = clMedGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLTarefa: TQRLabel
        Left = 44
        Top = 4
        Width = 70
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          116.416666666666700000
          10.583333333333330000
          185.208333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Chave '#218'nica:'
        Color = clMedGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
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
      Top = 291
      Width = 1027
      Height = 22
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        58.208333333333340000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel2: TQRLabel
        Left = 6
        Top = 5
        Width = 93
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          15.875000000000000000
          13.229166666666670000
          246.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Total de Tarefas:'
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
      object QRLTotTarefas: TQRLabel
        Left = 104
        Top = 5
        Width = 67
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          275.166666666666700000
          13.229166666666670000
          177.270833333333300000)
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
    end
    object QRBand2: TQRBand
      Left = 48
      Top = 282
      Width = 1027
      Height = 9
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
        23.812500000000000000
        2717.270833333333000000)
      BandType = rbGroupFooter
    end
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      'select tar.*, emp.no_fantas_emp, sagend.DE_SITU_TAREF_AGEND,'
      
        'pagend.DE_PRIOR_TAREF_AGEND,res.no_col as responsavel, res.co_ma' +
        't_col as matres, sol.no_col as solicitante, sol.co_mat_col as ma' +
        'tsol,'
      'emp.sigla as siglaResp, empSol.sigla as siglaSol'
      'from tb137_tarefas_agenda tar'
      
        'join tb03_colabor res on tar.co_col = res.co_col and res.co_emp ' +
        '= tar.co_emp'
      
        'join tb03_colabor sol on sol.co_col = tar.co_funci_solic_taref_a' +
        'gend and sol.co_emp = tar.co_emp_solic_taref_agend'
      'join tb25_empresa emp on emp.co_emp = tar.co_emp'
      
        'join TB139_SITU_TAREF_AGEND sagend on sagend.CO_SITU_TAREF_AGEND' +
        ' = tar.CO_SITU_TAREF_AGEND'
      
        'join TB140_PRIOR_TAREF_AGEND pagend on pagend.CO_PRIOR_TAREF_AGE' +
        'ND = tar.CO_PRIOR_TAREF_AGEND'
      
        'join tb25_empresa empSol on empSol.co_emp = tar.co_emp_solic_tar' +
        'ef_agend')
    object QryRelatorioORG_CODIGO_ORGAO: TIntegerField
      FieldName = 'ORG_CODIGO_ORGAO'
    end
    object QryRelatorioCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryRelatorioCO_COL: TIntegerField
      FieldName = 'CO_COL'
    end
    object QryRelatorioCO_IDENT_TAREF: TAutoIncField
      FieldName = 'CO_IDENT_TAREF'
      ReadOnly = True
    end
    object QryRelatorioCO_ORGAO_SOLIC_TAREF_AGEND: TIntegerField
      FieldName = 'CO_ORGAO_SOLIC_TAREF_AGEND'
    end
    object QryRelatorioCO_EMP_SOLIC_TAREF_AGEND: TIntegerField
      FieldName = 'CO_EMP_SOLIC_TAREF_AGEND'
    end
    object QryRelatorioCO_FUNCI_SOLIC_TAREF_AGEND: TIntegerField
      FieldName = 'CO_FUNCI_SOLIC_TAREF_AGEND'
    end
    object QryRelatorioNM_RESUM_TAREF_AGEND: TStringField
      FieldName = 'NM_RESUM_TAREF_AGEND'
      Size = 40
    end
    object QryRelatorioDE_DETAL_TAREF_AGEND: TStringField
      FieldName = 'DE_DETAL_TAREF_AGEND'
      Size = 200
    end
    object QryRelatorioDT_CADAS_TAREF_AGEND: TDateTimeField
      FieldName = 'DT_CADAS_TAREF_AGEND'
      EditMask = '!99/99/00;1;_'
    end
    object QryRelatorioDT_COMPR_TAREF_AGEND: TDateTimeField
      FieldName = 'DT_COMPR_TAREF_AGEND'
      EditMask = '!99/99/00;1;_'
    end
    object QryRelatorioDT_LIMIT_TAREF_AGEND: TDateTimeField
      FieldName = 'DT_LIMIT_TAREF_AGEND'
      EditMask = '!99/99/00;1;_'
    end
    object QryRelatorioDT_REALIZ_TAREF_AGEND: TDateTimeField
      FieldName = 'DT_REALIZ_TAREF_AGEND'
      EditMask = '!99/99/00;1;_'
    end
    object QryRelatorioCO_PRIOR_TAREF_AGEND: TStringField
      FieldName = 'CO_PRIOR_TAREF_AGEND'
      FixedChar = True
      Size = 3
    end
    object QryRelatorioDT_REPAS_TAREF_AGEND: TDateTimeField
      FieldName = 'DT_REPAS_TAREF_AGEND'
      EditMask = '!99/99/00;1;_'
    end
    object QryRelatorioDE_MOTIV_REPAS_TAREF_AGEND: TStringField
      FieldName = 'DE_MOTIV_REPAS_TAREF_AGEND'
      Size = 100
    end
    object QryRelatorioCO_ORGAO_REPAS_TAREF_AGEND: TIntegerField
      FieldName = 'CO_ORGAO_REPAS_TAREF_AGEND'
    end
    object QryRelatorioCO_EMP_REPAS_TAREF_AGEND: TIntegerField
      FieldName = 'CO_EMP_REPAS_TAREF_AGEND'
    end
    object QryRelatorioCO_FUNCI_REPAS_TAREF_AGEND: TIntegerField
      FieldName = 'CO_FUNCI_REPAS_TAREF_AGEND'
    end
    object QryRelatorioCO_SITU_TAREF_AGEND: TStringField
      FieldName = 'CO_SITU_TAREF_AGEND'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioDE_OBSERV_TAREF_AGEND: TStringField
      FieldName = 'DE_OBSERV_TAREF_AGEND'
      Size = 200
    end
    object QryRelatorioCO_FLA_SMS_TAREF_AGEND: TStringField
      FieldName = 'CO_FLA_SMS_TAREF_AGEND'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_FLA_REABERTA: TStringField
      FieldName = 'CO_FLA_REABERTA'
      FixedChar = True
      Size = 1
    end
    object QryRelatoriono_fantas_emp: TStringField
      FieldName = 'no_fantas_emp'
      Size = 80
    end
    object QryRelatorioDE_SITU_TAREF_AGEND: TStringField
      FieldName = 'DE_SITU_TAREF_AGEND'
      Size = 50
    end
    object QryRelatorioDE_PRIOR_TAREF_AGEND: TStringField
      FieldName = 'DE_PRIOR_TAREF_AGEND'
      Size = 50
    end
    object QryRelatorioresponsavel: TStringField
      FieldName = 'responsavel'
      Size = 60
    end
    object QryRelatoriomatres: TStringField
      FieldName = 'matres'
      Size = 15
    end
    object QryRelatoriosolicitante: TStringField
      FieldName = 'solicitante'
      Size = 60
    end
    object QryRelatoriomatsol: TStringField
      FieldName = 'matsol'
      Size = 15
    end
    object QryRelatoriosiglaResp: TWideStringField
      FieldName = 'siglaResp'
      FixedChar = True
      Size = 5
    end
    object QryRelatoriosiglaSol: TWideStringField
      FieldName = 'siglaSol'
      FixedChar = True
      Size = 5
    end
    object QryRelatorioCO_CHAVE_UNICA_TAREF: TFloatField
      FieldName = 'CO_CHAVE_UNICA_TAREF'
    end
  end
end