inherited FrmRelDemAluUnid: TFrmRelDemAluUnid
  Left = 353
  Top = 129
  Width = 827
  Height = 571
  HorzScrollBar.Position = 329
  Caption = 'FrmRelDemAluUnid'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = -321
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
      Height = 177
      Frame.DrawBottom = False
      Size.Values = (
        468.312500000000100000
        2717.270833333333000000)
      inherited QRSysData3: TQRSysData [0]
        Left = 995
        Size.Values = (
          44.979166666666670000
          2632.604166666667000000
          108.479166666666700000
          82.020833333333340000)
        FontSize = 8
      end
      inherited LblTituloRel: TQRLabel [1]
        Width = 1027
        Height = 19
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'QUADRO DEMONSTRATIVO DE QUANTIDADE DE ALUNOS POR UNIDADE/S'#201'RIE'
        Font.Height = -13
        FontSize = 10
      end
      inherited QRDBText14: TQRDBText [2]
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          5.291666666666667000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText [3]
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          47.625000000000000000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBImage1: TQRDBImage [4]
        Size.Values = (
          198.437500000000000000
          10.583333333333300000
          10.583333333333300000
          246.062500000000000000)
      end
      inherited qrlTemplePag: TQRLabel [5]
        Left = 940
        Size.Values = (
          44.979166666666670000
          2487.083333333333000000
          21.166666666666670000
          66.145833333333340000)
        FontSize = 8
      end
      inherited qrlTempleData: TQRLabel [6]
        Left = 940
        Size.Values = (
          44.979166666666670000
          2487.083333333333000000
          66.145833333333340000
          68.791666666666680000)
        FontSize = 8
      end
      inherited qrlTempleHora: TQRLabel [7]
        Left = 940
        Size.Values = (
          44.979166666666670000
          2487.083333333333000000
          111.125000000000000000
          71.437500000000000000)
        FontSize = 8
      end
      inherited QRSysData1: TQRSysData [8]
        Left = 975
        Size.Values = (
          44.979166666666670000
          2579.687500000000000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        FontSize = 8
      end
      inherited QRSysData2: TQRSysData [9]
        Left = 996
        Size.Values = (
          44.979166666666670000
          2635.250000000000000000
          111.125000000000000000
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
      object QRShape1: TQRShape
        Left = 0
        Top = 159
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
          420.687500000000000000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 5
        Top = 161
        Width = 126
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          425.979166666666700000
          333.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNIDADE EDUCACIONAL'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel2: TQRLabel
        Left = 985
        Top = 161
        Width = 38
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2606.145833333333000000
          425.979166666666700000
          100.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TOTAL'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRShape2: TQRShape
        Left = 982
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2598.208333333333000000
          420.687500000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRLMat12: TQRLabel
        Left = 967
        Top = 129
        Width = 21
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2558.520833333333000000
          341.312500000000000000
          55.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'EJA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRShape5: TQRShape
        Left = 935
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2473.854166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape6: TQRShape
        Left = 890
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2354.791666666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape7: TQRShape
        Left = 845
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2235.729166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape8: TQRShape
        Left = 800
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2116.666666666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape9: TQRShape
        Left = 755
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1997.604166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape10: TQRShape
        Left = 710
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1878.541666666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape11: TQRShape
        Left = 665
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1759.479166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape12: TQRShape
        Left = 620
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1640.416666666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape13: TQRShape
        Left = 575
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1521.354166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape14: TQRShape
        Left = 530
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1402.291666666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRShape15: TQRShape
        Left = 485
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1283.229166666667000000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRLMat11: TQRLabel
        Left = 941
        Top = 161
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2489.729166666667000000
          425.979166666666700000
          92.604166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MULTI'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat10: TQRLabel
        Left = 896
        Top = 161
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2370.666666666667000000
          425.979166666666700000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '4C FIN'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat9: TQRLabel
        Left = 853
        Top = 161
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2256.895833333333000000
          425.979166666666700000
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '4C INI'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat5: TQRLabel
        Left = 673
        Top = 161
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1780.645833333333000000
          425.979166666666700000
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2C INI'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat6: TQRLabel
        Left = 716
        Top = 161
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1894.416666666667000000
          425.979166666666700000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2C FIN'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat7: TQRLabel
        Left = 763
        Top = 161
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2018.770833333333000000
          425.979166666666700000
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '3C INI'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat8: TQRLabel
        Left = 806
        Top = 161
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2132.541666666667000000
          425.979166666666700000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '3C FIN'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat1: TQRLabel
        Left = 492
        Top = 161
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1301.750000000000000000
          425.979166666666700000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'INFAN'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat2: TQRLabel
        Left = 537
        Top = 161
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1420.812500000000000000
          425.979166666666700000
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C INI'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat3: TQRLabel
        Left = 581
        Top = 161
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1537.229166666667000000
          425.979166666666700000
          92.604166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C INT'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat4: TQRLabel
        Left = 626
        Top = 161
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1656.291666666667000000
          425.979166666666700000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C FIN'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel16: TQRLabel
        Left = 352
        Top = 161
        Width = 74
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          931.333333333333500000
          425.979166666666700000
          195.791666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TIPO UNIDADE'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRShape16: TQRShape
        Left = 337
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          891.645833333333200000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRLabel18: TQRLabel
        Left = 300
        Top = 161
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          793.750000000000000000
          425.979166666666700000
          89.958333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SIGLA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRShape17: TQRShape
        Left = 295
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          780.520833333333400000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRLabel20: TQRLabel
        Left = 248
        Top = 161
        Width = 38
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          656.166666666666800000
          425.979166666666700000
          100.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' INEP'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRShape18: TQRShape
        Left = 238
        Top = 159
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          629.708333333333400000
          420.687500000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRLPage: TQRLabel
        Left = 1009
        Top = 8
        Width = 19
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2669.645833333333000000
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
      object QRLabel3: TQRLabel
        Left = 1002
        Top = 8
        Width = 4
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2651.125000000000000000
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
      Top = 260
      Width = 1027
      Size.Values = (
        31.750000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 696
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
      Top = 225
      Width = 1027
      Height = 17
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
        44.979166666666670000
        2717.270833333333000000)
      BandType = rbDetail
      object QrlTotalAluno: TQRLabel
        Left = 985
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2606.145833333333000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QRShape3: TQRShape
        Left = 982
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2598.208333333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clGray
        Shape = qrsVertLine
      end
      object QrlTPunidade: TQRLabel
        Left = 340
        Top = 1
        Width = 80
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          899.583333333333400000
          2.645833333333333000
          211.666666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Escola Municipal'
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
      object QrlSigla: TQRLabel
        Left = 296
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          783.166666666666800000
          2.645833333333333000
          105.833333333333300000)
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
      object QrlNuINEP: TQRLabel
        Left = 243
        Top = 1
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          642.937500000000000000
          2.645833333333333000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99999999'
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
      object QRShape21: TQRShape
        Left = 935
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2473.854166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape22: TQRShape
        Left = 890
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2354.791666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape23: TQRShape
        Left = 845
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2235.729166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape24: TQRShape
        Left = 800
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2116.666666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape25: TQRShape
        Left = 755
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1997.604166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape26: TQRShape
        Left = 710
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1878.541666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape27: TQRShape
        Left = 665
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1759.479166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape28: TQRShape
        Left = 620
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1640.416666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape29: TQRShape
        Left = 575
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1521.354166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape30: TQRShape
        Left = 530
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1402.291666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRShape31: TQRShape
        Left = 485
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1283.229166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRShape32: TQRShape
        Left = 337
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          891.645833333333200000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRShape33: TQRShape
        Left = 295
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          780.520833333333400000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRShape34: TQRShape
        Left = 238
        Top = 0
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          629.708333333333400000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Shape = qrsVertLine
      end
      object QRDBText1: TQRDBText
        Left = 3
        Top = 1
        Width = 232
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
          613.833333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_FANTAS_EMP'
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
      object Qrl1CINI: TQRLabel
        Left = 533
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1410.229166666667000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlINFAN: TQRLabel
        Left = 488
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1291.166666666667000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl1CINT: TQRLabel
        Left = 578
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1529.291666666667000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl1CFIN: TQRLabel
        Left = 623
        Top = 1
        Width = 40
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
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl2CINI: TQRLabel
        Left = 669
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1770.062500000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl2CFIN: TQRLabel
        Left = 714
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1889.125000000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl3CINI: TQRLabel
        Left = 759
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2008.187500000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl3CFIN: TQRLabel
        Left = 804
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2127.250000000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlEJA: TQRLabel
        Left = 965
        Top = 9
        Width = 40
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2553.229166666667000000
          23.812500000000000000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlMULTI: TQRLabel
        Left = 939
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2484.437500000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl4CFIN: TQRLabel
        Left = 894
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2365.375000000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object Qrl4CINI: TQRLabel
        Left = 849
        Top = 1
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2246.312500000000000000
          2.645833333333333000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
    object SummaryBand1: TQRBand
      Left = 48
      Top = 242
      Width = 1027
      Height = 18
      Frame.Color = clBlack
      Frame.DrawTop = True
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = SummaryBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        47.625000000000000000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel22: TQRLabel
        Left = 444
        Top = 2
        Width = 38
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
      object QrlTotInfan: TQRLabel
        Left = 488
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1291.166666666667000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot1CINI: TQRLabel
        Left = 533
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1410.229166666667000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot1CINT: TQRLabel
        Left = 578
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1529.291666666667000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot1CFIN: TQRLabel
        Left = 623
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1648.354166666667000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot2CINI: TQRLabel
        Left = 669
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1770.062500000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot2CFIN: TQRLabel
        Left = 714
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1889.125000000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot3CINI: TQRLabel
        Left = 759
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2008.187500000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot3CFIN: TQRLabel
        Left = 804
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2127.250000000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot4CINI: TQRLabel
        Left = 849
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2246.312500000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTot4CFIN: TQRLabel
        Left = 894
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2365.375000000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTotMULTI: TQRLabel
        Left = 939
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2484.437500000000000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTotEJA: TQRLabel
        Left = 965
        Top = 10
        Width = 40
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2553.229166666667000000
          26.458333333333330000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
      object QrlTotTOTAL: TQRLabel
        Left = 985
        Top = 2
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2606.145833333333000000
          5.291666666666667000
          105.833333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '999.999'
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
    SQL.Strings = (
      
        'SELECT E.CO_EMP, E.SIGLA, E.NO_FANTAS_EMP, E.NU_INEP, ET.NO_TIPO' +
        'EMP'
      'FROM TB25_EMPRESA E'
      'JOIN TB24_TPEMPRESA ET ON ET.CO_TIPOEMP = E.CO_TIPOEMP'
      'WHERE E.CO_TIPOEMP = 1')
    object QryRelatorioNO_FANTAS_EMP: TStringField
      FieldName = 'NO_FANTAS_EMP'
      Size = 80
    end
    object QryRelatorioNU_INEP: TIntegerField
      FieldName = 'NU_INEP'
    end
    object QryRelatorioNO_TIPOEMP: TStringField
      FieldName = 'NO_TIPOEMP'
      Size = 60
    end
    object QryRelatorioCO_EMP: TAutoIncField
      FieldName = 'CO_EMP'
      ReadOnly = True
    end
    object QryRelatorioSIGLA: TWideStringField
      FieldName = 'SIGLA'
      FixedChar = True
      Size = 5
    end
  end
  object QryAlunoCurso: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      
        ' SELECT DISTINCT E.CO_EMP,  (SELECT DISTINCT COUNT(M.CO_ALU) FRO' +
        'M TB08_MATRCUR M   JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU   JO' +
        'IN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9' JOIN TB25_EMPRESA E ON ' +
        'E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39 +
        'INFAN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALUINFAN,  (SELECT DISTINCT CO' +
        'UNT(M.CO_ALU) FROM TB08_MATRCUR M   JOIN TB07_ALUNO A ON A.CO_AL' +
        'U = M.CO_ALU   JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9' JOIN T' +
        'B25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'INF' +
        'AN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALUINFAN, (SELECT DISTINCT COU' +
        'NT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU ' +
        '= M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_' +
        'EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160 '#9'AND C.CO' +
        '_SIGL_CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ) ALU1CINI, (SELEC' +
        'T DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO ' +
        'A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_E' +
        'MP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_' +
        'CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ) TOTALU1CINI,  (SELECT ' +
        'DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ' +
        'ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP' +
        ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 1' +
        '60'#9#9'AND C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU1' +
        'CINT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOI' +
        'N TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO' +
        '_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHE' +
        'RE C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU1CI' +
        'NT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN ' +
        'TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_E' +
        'MP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE' +
        ' A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'1C FIN'#39#9#9'AND M.CO_CUR = C.' +
        'CO_CUR ) ALU1CFIN,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_M' +
        'ATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CU' +
        'RSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = ' +
        'A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'1C FIN'#39#9#9'AND M.CO_CUR = C.CO_CU' +
        'R ) TOTALU1CFIN, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATR' +
        'CUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO' +
        ' C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.C' +
        'O_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'2C INI'#39#9#9'AND M' +
        '.CO_CUR = C.CO_CUR ) ALU2CINI, (SELECT DISTINCT COUNT(M.CO_ALU) ' +
        'FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'J' +
        'OIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON ' +
        'E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39 +
        '2C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU2CINI, (SELECT DISTINCT' +
        ' COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_' +
        'ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN T' +
        'B25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND ' +
        'C.CO_SIGL_CUR '
      
        '= '#39'2C FIN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU2CFIN, (SELECT DISTINCT' +
        ' COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_' +
        'ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN T' +
        'B25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'2C ' +
        'FIN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU2CFIN, (SELECT DISTINCT CO' +
        'UNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU' +
        ' = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25' +
        '_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.C' +
        'O_SIGL_CUR = '#39'3C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU3CINI, (SELE' +
        'CT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO' +
        ' A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_' +
        'EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL' +
        '_CUR = '#39'3C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU3CINI, (SELECT ' +
        'DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ' +
        'ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP' +
        ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 1' +
        '60'#9#9'AND C.CO_SIGL_CUR = '#39'3C FIN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU3' +
        'CFIN, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN' +
        ' TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_' +
        'EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHER' +
        'E C.CO_SIGL_CUR = '#39'3C FIN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU3CFI' +
        'N, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB' +
        '07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP' +
        ' = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A' +
        '.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'4C INI'#39#9#9'AND M.CO_CUR = C.CO' +
        '_CUR ) ALU4CINI, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATR' +
        'CUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO' +
        ' C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.C' +
        'O_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'4C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR )' +
        ' TOTALU4CINI, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR' +
        ' M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ' +
        'ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_E' +
        'MP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'4C FIN'#39#9#9'AND M.CO' +
        '_CUR = C.CO_CUR ) ALU4CFIN, (SELECT DISTINCT COUNT(M.CO_ALU) FRO' +
        'M TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN' +
        ' TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.C' +
        'O_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'4C FIN'#39#9#9'AND M.CO_CUR =' +
        ' C.CO_CUR ) TOTALU4CFIN, (SELECT DISTINCT COUNT(M.CO_ALU) FROM T' +
        'B08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB' +
        '01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_E' +
        'MP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'MULTI'#39 +
        #9#9'AND M.CO_CUR = C.CO_CUR ) MULTI, (SELECT DISTINCT COUNT(M.CO_A' +
        'LU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_AL' +
        'U '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E' +
        ' ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'MULTI'#39#9#9'AND '
      
        'M.CO_CUR = C.CO_CUR ) TOTMULTI, (SELECT DISTINCT COUNT(M.CO_ALU)' +
        ' FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9 +
        'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON' +
        ' E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = ' +
        #39'EJA'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALUEJA, (SELECT DISTINCT COUNT(' +
        'M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M' +
        '.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMP' +
        'RESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'EJA'#39#9#9'AND ' +
        'M.CO_CUR = C.CO_CUR ) TOTALUEJA, (SELECT DISTINCT COUNT(M.CO_ALU' +
        ') FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU ' +
        #9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E O' +
        'N E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND M.CO_CUR = C.CO' +
        '_CUR ) ALUTOTAL FROM TB25_EMPRESA E WHERE E.CO_EMP = 160')
    Left = 296
    Top = 40
    object QryAlunoCursoCO_EMP: TAutoIncField
      FieldName = 'CO_EMP'
      ReadOnly = True
    end
    object QryAlunoCursoALUINFAN: TIntegerField
      FieldName = 'ALUINFAN'
      ReadOnly = True
    end
    object QryAlunoCursoALU1CINI: TIntegerField
      FieldName = 'ALU1CINI'
      ReadOnly = True
    end
    object QryAlunoCursoALU1CINT: TIntegerField
      FieldName = 'ALU1CINT'
      ReadOnly = True
    end
    object QryAlunoCursoALU1CFIN: TIntegerField
      FieldName = 'ALU1CFIN'
      ReadOnly = True
    end
    object QryAlunoCursoALU2CINI: TIntegerField
      FieldName = 'ALU2CINI'
      ReadOnly = True
    end
    object QryAlunoCursoALU2CFIN: TIntegerField
      FieldName = 'ALU2CFIN'
      ReadOnly = True
    end
    object QryAlunoCursoALU3CINI: TIntegerField
      FieldName = 'ALU3CINI'
      ReadOnly = True
    end
    object QryAlunoCursoALU3CFIN: TIntegerField
      FieldName = 'ALU3CFIN'
      ReadOnly = True
    end
    object QryAlunoCursoALU4CINI: TIntegerField
      FieldName = 'ALU4CINI'
      ReadOnly = True
    end
    object QryAlunoCursoALU4CFIN: TIntegerField
      FieldName = 'ALU4CFIN'
      ReadOnly = True
    end
    object QryAlunoCursoMULTI: TIntegerField
      FieldName = 'MULTI'
      ReadOnly = True
    end
    object QryAlunoCursoALUEJA: TIntegerField
      FieldName = 'ALUEJA'
      ReadOnly = True
    end
    object QryAlunoCursoALUTOTAL: TIntegerField
      FieldName = 'ALUTOTAL'
      ReadOnly = True
    end
  end
end
