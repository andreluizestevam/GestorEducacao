inherited FrmRelInformacaoCurso: TFrmRelInformacaoCurso
  Left = 212
  Top = 166
  Caption = 'FrmRelInformacaoCurso'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 4
    Top = 8
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
      Height = 126
      Frame.DrawBottom = False
      ForceNewPage = True
      Size.Values = (
        333.375000000000000000
        1846.791666666667000000)
      inherited LblTituloRel: TQRLabel
        Left = 104
        Top = 104
        Width = 513
        Height = 20
        Size.Values = (
          52.916666666666670000
          275.166666666666700000
          275.166666666666700000
          1357.312500000000000000)
        Caption = ''
        FontSize = 12
      end
      inherited QRDBText14: TQRDBText
        Left = 100
        Top = 4
        Width = 484
        Size.Values = (
          44.979166666666700000
          264.583333333333000000
          10.583333333333300000
          1280.583333333330000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText
        Left = 100
        Top = 23
        Size.Values = (
          44.979166666666700000
          264.583333333333000000
          60.854166666666700000
          1256.770833333330000000)
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
        Left = 602
        Width = 32
        Size.Values = (
          44.979166666666670000
          1592.791666666667000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 602
        Width = 32
        Size.Values = (
          44.979166666666670000
          1592.791666666667000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 602
        Width = 32
        Size.Values = (
          44.979166666666670000
          1592.791666666667000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 644
        Size.Values = (
          44.979166666666670000
          1703.916666666667000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData2: TQRSysData
        Left = 661
        Width = 37
        Height = 18
        Size.Values = (
          47.625000000000000000
          1748.895833333334000000
          111.125000000000000000
          97.895833333333340000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData3: TQRSysData
        Left = 662
        Top = 25
        Width = 36
        Size.Values = (
          44.979166666666670000
          1751.541666666667000000
          66.145833333333340000
          95.250000000000000000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlEnde: TQRLabel
        Left = 100
        Top = 42
        Width = 37
        Size.Values = (
          44.979166666666670000
          264.583333333333400000
          111.125000000000000000
          97.895833333333340000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlComplemento: TQRLabel
        Left = 100
        Top = 60
        Width = 77
        Size.Values = (
          44.979166666666670000
          264.583333333333400000
          158.750000000000000000
          203.729166666666700000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlTels: TQRLabel
        Left = 100
        Top = 79
        Height = 17
        Size.Values = (
          44.979166666666670000
          264.583333333333400000
          209.020833333333300000
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
      object QRLabel2: TQRLabel
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
      Top = 959
      Height = 18
      Size.Values = (
        47.625000000000000000
        1846.791666666667000000)
      inherited QRLabelSGIE: TQRLabel
        Left = -17
        Width = 715
        Height = 17
        Size.Values = (
          44.979166666666670000
          -44.979166666666670000
          0.000000000000000000
          1891.770833333333000000)
        Font.Height = -11
        FontSize = 8
      end
    end
    object QRGroup2: TQRGroup
      Left = 48
      Top = 174
      Width = 698
      Height = 101
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRGroup2BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = True
      Size.Values = (
        267.229166666666700000
        1846.791666666667000000)
      Expression = 'QryRelatorio.CO_CUR'
      Master = QuickRep1
      ReprintOnNewPage = True
      object QrlTitRel: TQRLabel
        Left = 0
        Top = 1
        Width = 697
        Height = 23
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          60.854166666666700000
          0.000000000000000000
          2.645833333333330000
          1844.145833333330000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'INFORMA'#199#213'ES DA S'#201'RIE'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -15
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 11
      end
      object QRLabel9: TQRLabel
        Left = 4
        Top = 36
        Width = 30
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          10.583333333333330000
          95.250000000000000000
          79.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#237'vel:'
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
      object QRLabel10: TQRLabel
        Left = 4
        Top = 53
        Width = 38
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          10.583333333333330000
          140.229166666666700000
          100.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Coord:'
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
      object QRLabel11: TQRLabel
        Left = 163
        Top = 71
        Width = 78
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          431.270833333333400000
          187.854166666666700000
          206.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Carga Hor'#225'ria:'
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
      object QRLabel12: TQRLabel
        Left = 4
        Top = 70
        Width = 45
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          10.583333333333330000
          185.208333333333300000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'M'#243'dulo:'
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
      object QRLabel14: TQRLabel
        Left = 292
        Top = 36
        Width = 36
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          772.583333333333400000
          95.250000000000000000
          95.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Depto:'
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
      object QRLabel19: TQRLabel
        Left = 292
        Top = 53
        Width = 32
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          772.583333333333400000
          140.229166666666700000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Resp:'
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
      object QRLabel20: TQRLabel
        Left = 292
        Top = 70
        Width = 98
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          772.583333333333400000
          185.208333333333300000
          259.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '% de Certifica'#231#227'o:'
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
      object QRLabel21: TQRLabel
        Left = 492
        Top = 70
        Width = 50
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1301.750000000000000000
          185.208333333333300000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Situa'#231#227'o:'
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
      object QRLSituacao: TQRLabel
        Left = 544
        Top = 70
        Width = 71
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666700000
          1439.333333333330000000
          185.208333333333000000
          187.854166666667000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLSituacao'
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
        Left = 620
        Top = 70
        Width = 69
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1640.416666666667000000
          185.208333333333300000
          182.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DT_SITU_CUR'
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
      object QRLNivel: TQRLabel
        Left = 37
        Top = 36
        Width = 71
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666700000
          97.895833333333300000
          95.250000000000000000
          187.854166666667000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLNivel'
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
      object QRDBText2: TQRDBText
        Left = 44
        Top = 54
        Width = 245
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666700000
          116.416666666667000000
          142.875000000000000000
          648.229166666667000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_SUBDPTO_CUR'
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
      object QRDBText3: TQRDBText
        Left = 332
        Top = 36
        Width = 362
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666700000
          878.416666666667000000
          95.250000000000000000
          957.791666666667000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_DPTO_CUR'
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
      object QRDBText5: TQRDBText
        Left = 243
        Top = 71
        Width = 44
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          642.937500000000000000
          187.854166666666700000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'QT_CARG_HORA_CUR'
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
      object QRDBText6: TQRDBText
        Left = 52
        Top = 70
        Width = 105
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          137.583333333333300000
          185.208333333333300000
          277.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_MODU_CUR'
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
      object QRDBText7: TQRDBText
        Left = 396
        Top = 70
        Width = 61
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666700000
          1047.750000000000000000
          185.208333333333000000
          161.395833333333000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'PE_CERT_CUR'
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
      object QRShape1: TQRShape
        Left = 2
        Top = 96
        Width = 695
        Height = 1
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          2.645833333333330000
          5.291666666666670000
          254.000000000000000000
          1838.854166666670000000)
        Shape = qrsRectangle
      end
      object QRLCoordenador: TQRLabel
        Left = 330
        Top = 54
        Width = 85
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          873.124999999999900000
          142.875000000000000000
          224.895833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QRLCoordenador'
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
    object QRGroup3: TQRGroup
      Left = 48
      Top = 275
      Width = 698
      Height = 644
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
        1703.916666666667000000
        1846.791666666667000000)
      Expression = 'QryRelatorio.CO_CUR'
      Master = QuickRep1
      ReprintOnNewPage = False
      object QRShape8: TQRShape
        Left = 0
        Top = 208
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          550.333333333333400000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape7: TQRShape
        Left = 0
        Top = 547
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          1447.270833333333000000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape6: TQRShape
        Left = 0
        Top = 463
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          1225.020833333333000000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape5: TQRShape
        Left = 0
        Top = 372
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          984.249999999999900000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape4: TQRShape
        Left = 0
        Top = 294
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          777.875000000000000000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape3: TQRShape
        Left = 0
        Top = 119
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          314.854166666666700000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape2: TQRShape
        Left = 0
        Top = 2
        Width = 700
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          0.000000000000000000
          5.291666666666667000
          1852.083333333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRDBText8: TQRDBText
        Left = 5
        Top = 21
        Width = 689
        Height = 93
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          246.062500000000000000
          13.229166666666700000
          55.562500000000000000
          1822.979166666670000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_OBJE_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel13: TQRLabel
        Left = 4
        Top = 120
        Width = 85
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          317.500000000000000000
          224.895833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'#218'BLICO ALVO:'
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
      object QRDBText9: TQRDBText
        Left = 5
        Top = 139
        Width = 689
        Height = 65
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          171.979166666667000000
          13.229166666666700000
          367.770833333333000000
          1822.979166666670000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_PUBL_ALV_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel8: TQRLabel
        Left = 4
        Top = 209
        Width = 168
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          552.979166666666700000
          444.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PR'#201'-REQUISITO PARA O CURSO:'
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
      object QRDBText10: TQRDBText
        Left = 4
        Top = 228
        Width = 691
        Height = 65
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          171.979166666667000000
          10.583333333333300000
          603.250000000000000000
          1828.270833333330000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_PRE_REQU_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel15: TQRLabel
        Left = 4
        Top = 295
        Width = 84
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          780.520833333333400000
          222.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'METODOLOGIA:'
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
      object QRDBText11: TQRDBText
        Left = 4
        Top = 310
        Width = 692
        Height = 59
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          156.104166666667000000
          10.583333333333300000
          820.208333333333000000
          1830.916666666670000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_METO_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel16: TQRLabel
        Left = 4
        Top = 373
        Width = 91
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          986.895833333333400000
          240.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PROGRAMA'#199#195'O:'
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
      object QRDBText12: TQRDBText
        Left = 4
        Top = 391
        Width = 692
        Height = 71
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          187.854166666667000000
          10.583333333333300000
          1034.520833333330000000
          1830.916666666670000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_PROG_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel3: TQRLabel
        Left = 4
        Top = 464
        Width = 120
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          1227.666666666667000000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATERIAL FORNECIDO'
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
      object QRDBText13: TQRDBText
        Left = 5
        Top = 479
        Width = 690
        Height = 68
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          179.916666666667000000
          13.229166666666700000
          1267.354166666670000000
          1825.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_MATE_FORN_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel18: TQRLabel
        Left = 4
        Top = 548
        Width = 81
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          1449.916666666667000000
          214.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CERTIFICA'#199#195'O'
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
      object QRDBText23: TQRDBText
        Left = 4
        Top = 566
        Width = 691
        Height = 74
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          195.791666666667000000
          10.583333333333300000
          1497.541666666670000000
          1828.270833333330000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_CERT_CUR'
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel1: TQRLabel
        Left = 4
        Top = 3
        Width = 57
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          7.937500000000000000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'OBJETIVO:'
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
    object QRGroup4: TQRGroup
      Left = 48
      Top = 919
      Width = 698
      Height = 40
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRGroup4BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = True
      Size.Values = (
        105.833333333333300000
        1846.791666666667000000)
      Master = QuickRep1
      ReprintOnNewPage = False
      object QRDBRichText2: TQRDBRichText
        Left = 4
        Top = 4
        Width = 691
        Height = 31
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          82.020833333333300000
          10.583333333333300000
          10.583333333333300000
          1828.270833333330000000)
        Alignment = taLeftJustify
        AutoStretch = True
        Color = clWindow
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        DataField = 'DE_CONT_PROG_CUR'
        DataSet = QryRelatorio
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    AfterScroll = QryRelatorioAfterScroll
    SQL.Strings = (
      
        'Select C.*, D.NO_DPTO_CUR, CC.NO_COOR_CUR as NO_SUBDPTO_CUR, I.*' +
        ','
      'MO.DE_MODU_CUR'
      
        'From TB01_CURSO C, TB77_DPTO_CURSO D, TB68_COORD_CURSO CC, TB19_' +
        'INFOR_CURSO I,'
      'TB44_MODULO MO'
      'Where C.CO_DPTO_CUR = D.CO_DPTO_CUR AND'
      '      C.CO_DPTO_CUR = CC.CO_DPTO_CUR AND'
      '      C.CO_SUB_DPTO_CUR = CC.CO_COOR_CUR  AND'
      '      C.CO_CUR = I.CO_CUR AND'
      '      C.CO_MODU_CUR = MO.CO_MODU_CUR'
      'order by C.NO_CUR')
    object QryRelatorioCO_CUR: TAutoIncField
      FieldName = 'CO_CUR'
      ReadOnly = True
    end
    object QryRelatorioCO_DPTO_CUR: TIntegerField
      FieldName = 'CO_DPTO_CUR'
    end
    object QryRelatorioCO_SUB_DPTO_CUR: TIntegerField
      FieldName = 'CO_SUB_DPTO_CUR'
    end
    object QryRelatorioNO_CUR: TStringField
      FieldName = 'NO_CUR'
      Size = 100
    end
    object QryRelatorioCO_SIGL_CUR: TStringField
      FieldName = 'CO_SIGL_CUR'
      Size = 6
    end
    object QryRelatorioQT_CARG_HORA_CUR: TIntegerField
      FieldName = 'QT_CARG_HORA_CUR'
    end
    object QryRelatorioNO_MENT_CUR: TStringField
      FieldName = 'NO_MENT_CUR'
      Size = 100
    end
    object QryRelatorioDT_CRIA_CUR: TDateTimeField
      FieldName = 'DT_CRIA_CUR'
    end
    object QryRelatorioVL_TOTA_CUR: TBCDField
      FieldName = 'VL_TOTA_CUR'
      Precision = 7
      Size = 2
    end
    object QryRelatorioCO_IDENDES_CUR: TStringField
      FieldName = 'CO_IDENDES_CUR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioQT_OCOR_CUR: TIntegerField
      FieldName = 'QT_OCOR_CUR'
    end
    object QryRelatorioPE_CERT_CUR: TBCDField
      FieldName = 'PE_CERT_CUR'
      Precision = 4
      Size = 2
    end
    object QryRelatorioPE_FALT_CUR: TBCDField
      FieldName = 'PE_FALT_CUR'
      Precision = 4
      Size = 2
    end
    object QryRelatorioDT_SITU_CUR: TDateTimeField
      FieldName = 'DT_SITU_CUR'
    end
    object QryRelatorioCO_SITU: TStringField
      FieldName = 'CO_SITU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_NIVEL_CUR: TStringField
      FieldName = 'CO_NIVEL_CUR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioQT_MATE_MAT: TIntegerField
      FieldName = 'QT_MATE_MAT'
    end
    object QryRelatorioDE_INF_LEG_CUR: TStringField
      FieldName = 'DE_INF_LEG_CUR'
      Size = 200
    end
    object QryRelatorioNO_DPTO_CUR: TStringField
      FieldName = 'NO_DPTO_CUR'
      Size = 60
    end
    object QryRelatorioDE_OBJE_CUR: TMemoField
      FieldName = 'DE_OBJE_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_PUBL_ALV_CUR: TMemoField
      FieldName = 'DE_PUBL_ALV_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_PRE_REQU_CUR: TMemoField
      FieldName = 'DE_PRE_REQU_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_METO_CUR: TMemoField
      FieldName = 'DE_METO_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_PROG_CUR: TMemoField
      FieldName = 'DE_PROG_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_MATE_FORN_CUR: TMemoField
      FieldName = 'DE_MATE_FORN_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_CERT_CUR: TMemoField
      FieldName = 'DE_CERT_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioDE_CONT_PROG_CUR: TMemoField
      FieldName = 'DE_CONT_PROG_CUR'
      BlobType = ftMemo
    end
    object QryRelatorioCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryRelatorioQT_MAT_DEP_MAT: TIntegerField
      FieldName = 'QT_MAT_DEP_MAT'
    end
    object QryRelatorioCO_MODU_CUR: TIntegerField
      FieldName = 'CO_MODU_CUR'
    end
    object QryRelatorioNO_REFER: TStringField
      FieldName = 'NO_REFER'
      Size = 50
    end
    object QryRelatorioCO_SIGL_REFER: TStringField
      FieldName = 'CO_SIGL_REFER'
      Size = 8
    end
    object QryRelatorioCO_COOR: TIntegerField
      FieldName = 'CO_COOR'
    end
    object QryRelatorioMED_FINAL_CUR: TBCDField
      FieldName = 'MED_FINAL_CUR'
      Precision = 4
      Size = 2
    end
    object QryRelatorioFLA_CAL_PTES: TStringField
      FieldName = 'FLA_CAL_PTES'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioTIP_OPE_PTES: TStringField
      FieldName = 'TIP_OPE_PTES'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_OPE_CTES: TBCDField
      FieldName = 'VL_OPE_CTES'
      Precision = 6
      Size = 2
    end
    object QryRelatorioFLA_CAL_PMEN: TStringField
      FieldName = 'FLA_CAL_PMEN'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioTIP_OPE_PMEN: TStringField
      FieldName = 'TIP_OPE_PMEN'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_OPE_CMEN: TBCDField
      FieldName = 'VL_OPE_CMEN'
      Precision = 6
      Size = 2
    end
    object QryRelatorioFLA_CAL_PBIM: TStringField
      FieldName = 'FLA_CAL_PBIM'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioTIP_OPE_PBIM: TStringField
      FieldName = 'TIP_OPE_PBIM'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_OPE_CBIM: TBCDField
      FieldName = 'VL_OPE_CBIM'
      Precision = 6
      Size = 2
    end
    object QryRelatorioFLA_CAL_PFIN: TStringField
      FieldName = 'FLA_CAL_PFIN'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioTIP_OPE_PFIN: TStringField
      FieldName = 'TIP_OPE_PFIN'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_OPE_CFIN: TBCDField
      FieldName = 'VL_OPE_CFIN'
      Precision = 6
      Size = 2
    end
    object QryRelatorioTIP_OPE_MFIN: TStringField
      FieldName = 'TIP_OPE_MFIN'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioVL_OPE_MFIN: TBCDField
      FieldName = 'VL_OPE_MFIN'
      Precision = 6
      Size = 2
    end
    object QryRelatorioQT_AULA_CUR: TIntegerField
      FieldName = 'QT_AULA_CUR'
    end
    object QryRelatorioNU_PORTA_CUR: TStringField
      FieldName = 'NU_PORTA_CUR'
      Size = 15
    end
    object QryRelatorioNU_DOU_CUR: TStringField
      FieldName = 'NU_DOU_CUR'
      Size = 15
    end
    object QryRelatorioVL_PC_DECPONTO: TBCDField
      FieldName = 'VL_PC_DECPONTO'
      Precision = 8
      Size = 2
    end
    object QryRelatorioVL_PC_DESPROMO: TBCDField
      FieldName = 'VL_PC_DESPROMO'
      Precision = 8
      Size = 2
    end
    object QryRelatorioCO_MDP_VEST: TIntegerField
      FieldName = 'CO_MDP_VEST'
    end
    object QryRelatorioCO_PREDEC_CUR: TIntegerField
      FieldName = 'CO_PREDEC_CUR'
    end
    object QryRelatorioSEQ_IMPRESSAO: TIntegerField
      FieldName = 'SEQ_IMPRESSAO'
    end
    object QryRelatorioDE_MODU_CUR: TStringField
      FieldName = 'DE_MODU_CUR'
      Size = 60
    end
    object QryRelatorioNO_SUBDPTO_CUR: TStringField
      FieldName = 'NO_SUBDPTO_CUR'
      Size = 40
    end
  end
end
