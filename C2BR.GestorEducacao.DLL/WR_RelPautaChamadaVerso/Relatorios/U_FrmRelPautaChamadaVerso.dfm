inherited FrmRelPautaChamadaVerso: TFrmRelPautaChamadaVerso
  Left = 205
  Top = 169
  Width = 812
  Height = 588
  Caption = 'FrmRelPautaChamadaVerso'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 16
    Top = 8
    Width = 1111
    Height = 1572
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
    Zoom = 140
    inherited PageHeaderBand1: TQRBand
      Left = 67
      Top = 67
      Width = 977
      Height = 0
      Size.Values = (
        0.000000000000000000
        1846.413690476191000000)
      inherited LblTituloRel: TQRLabel
        Top = 162
        Width = 972
        Height = 32
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          1836.208333333333000000)
        Caption = 'VERSO'
        Font.Height = -19
        FontSize = 14
      end
      inherited QRDBText14: TQRDBText
        Left = 140
        Top = 6
        Width = 665
        Height = 24
        Size.Values = (
          44.979166666666670000
          264.583333333333300000
          10.583333333333330000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText
        Left = 140
        Top = 29
        Width = 665
        Height = 21
        Size.Values = (
          39.687500000000000000
          264.583333333333300000
          55.562500000000000000
          1256.770833333333000000)
        Font.Height = -11
        FontSize = 8
      end
      inherited QRDBImage1: TQRDBImage
        Left = 6
        Top = 6
        Width = 130
        Height = 105
        Size.Values = (
          198.437500000000000000
          10.583333333333300000
          10.583333333333300000
          246.062500000000000000)
      end
      inherited qrlTemplePag: TQRLabel
        Left = 838
        Top = 11
        Width = 45
        Height = 24
        Size.Values = (
          45.357142857142850000
          1583.720238095238000000
          20.788690476190480000
          85.044642857142860000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 839
        Top = 35
        Width = 45
        Height = 24
        Size.Values = (
          45.357142857142850000
          1585.610119047619000000
          66.145833333333340000
          85.044642857142860000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 839
        Top = 59
        Width = 45
        Height = 24
        Size.Values = (
          45.357142857142850000
          1585.610119047619000000
          111.502976190476200000
          85.044642857142860000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 943
        Top = 12
        Width = 34
        Height = 23
        Size.Values = (
          43.467261904761910000
          1782.157738095238000000
          22.678571428571430000
          64.255952380952380000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData2: TQRSysData
        Left = 925
        Top = 58
        Width = 52
        Height = 23
        Size.Values = (
          43.467261904761910000
          1748.139880952381000000
          109.613095238095200000
          98.273809523809540000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData3: TQRSysData
        Left = 927
        Top = 35
        Width = 50
        Height = 23
        Size.Values = (
          43.467261904761910000
          1751.919642857143000000
          66.145833333333340000
          94.494047619047620000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlEnde: TQRLabel
        Left = 140
        Top = 50
        Width = 60
        Height = 21
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          94.494047619047620000
          113.392857142857100000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlComplemento: TQRLabel
        Left = 140
        Top = 71
        Width = 133
        Height = 21
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          134.181547619047600000
          251.354166666666700000)
        Font.Height = -11
        FontSize = 8
      end
      inherited qrlTels: TQRLabel
        Left = 140
        Top = 92
        Width = 46
        Height = 21
        Size.Values = (
          39.687500000000000000
          264.583333333333400000
          174.625000000000000000
          87.312500000000000000)
        FontSize = 8
      end
      inherited QRLabel1000: TQRLabel
        Left = 944
        Top = 36
        Width = 32
        Height = 21
        Size.Values = (
          39.687500000000000000
          1783.291666666667000000
          68.791666666666680000
          60.854166666666680000)
        FontSize = 8
      end
    end
    inherited QRBANDSGIE: TQRBand
      Left = 67
      Top = 251
      Width = 977
      Height = 25
      Size.Values = (
        47.247023809523810000
        1846.413690476191000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 341
        Width = 636
        Height = 24
        Size.Values = (
          45.357142857142850000
          644.449404761904800000
          0.000000000000000000
          1201.964285714286000000)
        Font.Height = -11
        FontSize = 8
      end
      inherited Qrl_IdentificacaoRel: TQRLabel
        Width = 70
        Height = 15
        Size.Values = (
          28.348214285714280000
          0.000000000000000000
          0.000000000000000000
          132.291666666666700000)
        FontSize = 5
      end
    end
    object QRBand1: TQRBand
      Left = 67
      Top = 67
      Width = 977
      Height = 105
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
        198.437500000000000000
        1846.413690476191000000)
      BandType = rbTitle
      object QRShape3: TQRShape
        Left = 0
        Top = 77
        Width = 698
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          0.000000000000000000
          145.520833333333300000
          1319.136904761905000000)
        Brush.Color = 14211288
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape2: TQRShape
        Left = 0
        Top = 1
        Width = 697
        Height = 48
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          90.714285714285710000
          0.000000000000000000
          1.889880952380953000
          1317.247023809524000000)
        Brush.Style = bsClear
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 6
        Top = 20
        Width = 237
        Height = 22
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          41.577380952380950000
          11.339285714285710000
          37.797619047619050000
          447.901785714285700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'HIST'#211'RICO DID'#193'TICO DO M'#202'S'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 9
      end
      object QRLabel2: TQRLabel
        Left = 272
        Top = 19
        Width = 29
        Height = 22
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          41.577380952380950000
          514.047619047619000000
          35.907738095238090000
          54.806547619047620000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DE:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 9
      end
      object QRLabel3: TQRLabel
        Left = 367
        Top = 19
        Width = 15
        Height = 22
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          41.577380952380950000
          693.586309523809500000
          35.907738095238090000
          28.348214285714280000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'A:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 9
      end
      object QRLabel4: TQRLabel
        Left = 463
        Top = 19
        Width = 55
        Height = 22
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          41.577380952380950000
          875.014880952381100000
          35.907738095238090000
          103.943452380952400000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '/_____'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -12
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 9
      end
      object QRLabel5: TQRLabel
        Left = 22
        Top = 80
        Width = 39
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          41.577380952380950000
          151.190476190476200000
          73.705357142857140000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'DIA'
        Color = clWhite
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
      object QRLabel6: TQRLabel
        Left = 104
        Top = 80
        Width = 285
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          196.547619047619100000
          151.190476190476200000
          538.616071428571500000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'CONTE'#218'DO PROGRAMATICO'
        Color = clWhite
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
      object QRLabel8: TQRLabel
        Left = 650
        Top = 80
        Width = 55
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          1228.422619047619000000
          151.190476190476200000
          103.943452380952400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ORDEM'
        Color = clWhite
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
      object QRLabel9: TQRLabel
        Left = 737
        Top = 80
        Width = 169
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          1392.842261904762000000
          151.190476190476200000
          319.389880952381000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TRABALHOS / PROVAS'
        Color = clWhite
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
      object QRLabel10: TQRLabel
        Left = 917
        Top = 80
        Width = 55
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          1733.020833333333000000
          151.190476190476200000
          103.943452380952400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'NOTA'
        Color = clWhite
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
      object QRLabel7: TQRLabel
        Left = 543
        Top = 11
        Width = 168
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          32.127976190476190000
          1026.205357142857000000
          20.788690476190480000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Aulas Previstas:__________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
      object QRLabel18: TQRLabel
        Left = 751
        Top = 11
        Width = 168
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          32.127976190476190000
          1419.300595238095000000
          20.788690476190480000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Aulas Dadas: ____________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
      object QRLabel19: TQRLabel
        Left = 543
        Top = 35
        Width = 167
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          32.127976190476190000
          1026.205357142857000000
          66.145833333333340000
          315.610119047619000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Aulas Pr'#225'ticas:___________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
      object QRLabel20: TQRLabel
        Left = 751
        Top = 35
        Width = 168
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          32.127976190476190000
          1419.300595238095000000
          66.145833333333340000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Aulas T'#233'oricas:___________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
    end
    object QRBand2: TQRBand
      Left = 67
      Top = 172
      Width = 977
      Height = 22
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      Color = clWhite
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = 20
      Font.Name = 'Arial'
      Font.Style = []
      ForceNewColumn = False
      ForceNewPage = False
      ParentFont = False
      Size.Values = (
        41.577380952380950000
        1846.413690476191000000)
      BandType = rbDetail
      object QRShape13: TQRShape
        Left = 78
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          147.410714285714300000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape15: TQRShape
        Left = 638
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1205.744047619048000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape16: TQRShape
        Left = 914
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1727.351190476191000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRLabel11: TQRLabel
        Left = 36
        Top = 3
        Width = 4
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          28.348214285714280000
          68.035714285714290000
          5.669642857142857000
          7.559523809523811000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '/'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -8
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 6
      end
      object qrlCont: TQRLabel
        Left = 654
        Top = 3
        Width = 62
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          28.348214285714280000
          1235.982142857143000000
          5.669642857142857000
          117.172619047619100000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'N'#186
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -8
        Font.Name = 'Arial'
        Font.Style = []
        OnPrint = qrlContPrint
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 6
      end
      object QRShape17: TQRShape
        Left = 755
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1426.860119047619000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape18: TQRShape
        Left = 728
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1375.833333333333000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape19: TQRShape
        Left = 781
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1475.997023809524000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape20: TQRShape
        Left = 808
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1527.023809523810000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape23: TQRShape
        Left = 835
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1578.050595238095000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape24: TQRShape
        Left = 861
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1627.187500000000000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape25: TQRShape
        Left = 886
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1674.434523809524000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape26: TQRShape
        Left = 0
        Top = 21
        Width = 697
        Height = 1
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          1.889880952380953000
          0.000000000000000000
          39.687500000000000000
          1317.247023809524000000)
        Brush.Style = bsClear
        Shape = qrsHorLine
      end
      object QRShape12: TQRShape
        Left = 975
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1842.633928571428000000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape27: TQRShape
        Left = 1
        Top = 0
        Width = 1
        Height = 21
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1.889880952380953000
          0.000000000000000000
          1.889880952380953000)
        Brush.Style = bsClear
        Shape = qrsVertLine
      end
      object QRShape1: TQRShape
        Left = 0
        Top = 0
        Width = 697
        Height = 1
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          1.889880952380953000
          0.000000000000000000
          0.000000000000000000
          1317.247023809524000000)
        Brush.Style = bsClear
        Shape = qrsHorLine
      end
    end
    object QRBand3: TQRBand
      Left = 67
      Top = 194
      Width = 977
      Height = 57
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
        107.723214285714300000
        1846.413690476191000000)
      BandType = rbSummary
      object QRShape21: TQRShape
        Left = 0
        Top = -1
        Width = 697
        Height = 58
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          109.613095238095200000
          0.000000000000000000
          -1.889880952380953000
          1317.247023809524000000)
        Brush.Style = bsClear
        Shape = qrsRectangle
      end
      object QRLabel12: TQRLabel
        Left = 15
        Top = 6
        Width = 120
        Height = 28
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          52.916666666666680000
          28.348214285714280000
          11.339285714285710000
          226.785714285714300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '___/___/_____'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel13: TQRLabel
        Left = 288
        Top = 32
        Width = 168
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          544.285714285714300000
          60.476190476190480000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Assinatura do Professor'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel14: TQRLabel
        Left = 702
        Top = 32
        Width = 158
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          1326.696428571429000000
          60.476190476190480000
          298.601190476190500000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Coordena'#231#227'o de Curso'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel15: TQRLabel
        Left = 55
        Top = 34
        Width = 32
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          37.797619047619050000
          103.943452380952400000
          64.255952380952380000
          60.476190476190480000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Data'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel16: TQRLabel
        Left = 196
        Top = 5
        Width = 335
        Height = 28
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          52.916666666666680000
          370.416666666666700000
          9.449404761904763000
          633.110119047619100000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '__________________________________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel17: TQRLabel
        Left = 604
        Top = 5
        Width = 335
        Height = 28
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          52.916666666666680000
          1141.488095238095000000
          9.449404761904763000
          633.110119047619100000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '__________________________________'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      
        'Select G.*, C.NO_CUR, T.NO_TUR, M.NO_MAT, A.NO_ALU, MO.DE_MODU_C' +
        'UR, MM.CO_SITU_MTR'
      
        '                From TB48_GRADE_ALUNO G, TB01_CURSO C, TB02_MATE' +
        'RIA M, TB07_ALUNO A, '
      
        '               '#9'TB44_MODULO MO, TB06_TURMAS T, TB80_MASTERMATR M' +
        'M'
      '               WHERE G.CO_EMP = C.CO_EMP                      '
      
        '               '#9'AND G.CO_EMP = M.CO_EMP                         ' +
        '  '
      
        '               '#9'AND G.CO_EMP = A.CO_EMP                         ' +
        '   '
      '               '#9'AND G.CO_EMP = T.CO_EMP                       '
      
        '               '#9'AND G.CO_CUR = C.CO_CUR                         ' +
        '   '
      '               '#9'AND G.CO_MODU_CUR = MO.CO_MODU_CUR '
      
        '               '#9'AND G.CO_CUR = M.CO_CUR                         ' +
        ' '
      
        '               '#9'AND G.CO_MAT = M.CO_MAT                         ' +
        ' '
      
        '                AND G.CO_CUR = T.CO_CUR                         ' +
        '  '
      
        '               '#9'AND G.CO_TUR = T.CO_TUR                         ' +
        '  '
      '               '#9'AND G.CO_ALU = A.CO_ALU'
      '                '#9'AND G.CO_EMP = MM.CO_EMP'
      '                  AND G.CO_CUR = MM.CO_CUR'
      
        '                '#9'AND G.CO_ALU = MM.CO_ALU                       ' +
        '   '
      #9'AND G.CO_EMP = 1'
      #9'AND M.CO_MAT = 4')
    Left = 633
    Top = 8
  end
end
