inherited FrmRelMapaCaracteristicaMatricula: TFrmRelMapaCaracteristicaMatricula
  Left = 212
  Top = 164
  Width = 1280
  Caption = 'FrmRelMapaCaracteristicaMatricula'
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
      Height = 191
      Frame.DrawBottom = False
      Size.Values = (
        505.354166666666700000
        2717.270833333333000000)
      object QRShape1: TQRShape [0]
        Left = 0
        Top = 157
        Width = 1027
        Height = 35
        Frame.Color = clGray
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          0.000000000000000000
          415.395833333333400000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape2: TQRShape [1]
        Left = 257
        Top = 175
        Width = 770
        Height = 17
        Frame.Color = clSilver
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          679.979166666666800000
          463.020833333333400000
          2037.291666666667000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      inherited LblTituloRel: TQRLabel
        Width = 1024
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          2709.333333333333000000)
        Caption = 'DISTRIBUI'#199#195'O DE ALUNO POR CARACTER'#205'STICA - S'#201'RIE'
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
        Left = 948
        Size.Values = (
          44.979166666666670000
          2508.250000000000000000
          21.166666666666670000
          66.145833333333340000)
        FontSize = 8
      end
      inherited qrlTempleData: TQRLabel
        Left = 948
        Size.Values = (
          44.979166666666670000
          2508.250000000000000000
          66.145833333333340000
          68.791666666666680000)
        FontSize = 8
      end
      inherited qrlTempleHora: TQRLabel
        Left = 948
        Size.Values = (
          44.979166666666670000
          2508.250000000000000000
          111.125000000000000000
          71.437500000000000000)
        FontSize = 8
      end
      inherited QRSysData1: TQRSysData
        Left = 977
        Size.Values = (
          44.979166666666670000
          2584.979166666667000000
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
        Left = 1002
        Size.Values = (
          39.687500000000000000
          2651.125000000000000000
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
      object QRLabel18: TQRLabel
        Left = 667
        Top = 160
        Width = 91
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1764.770833333333000000
          423.333333333333300000
          240.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RENDA FAMILIAR'
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
        Left = 523
        Top = 160
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1383.770833333333000000
          423.333333333333300000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ETNIA'
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
        Left = 618
        Top = 177
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1635.125000000000000000
          468.312500000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ND'
        Color = clSilver
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
      object QRLabel14: TQRLabel
        Left = 585
        Top = 177
        Width = 11
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1547.812500000000000000
          468.312500000000000000
          29.104166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'IN'
        Color = clSilver
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
      object QRLabel15: TQRLabel
        Left = 547
        Top = 177
        Width = 16
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1447.270833333333000000
          468.312500000000000000
          42.333333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PA'
        Color = clSilver
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
      object QRLabel16: TQRLabel
        Left = 511
        Top = 177
        Width = 19
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1352.020833333333000000
          468.312500000000000000
          50.270833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'AM'
        Color = clSilver
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
      object QRLabel17: TQRLabel
        Left = 478
        Top = 177
        Width = 14
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1264.708333333333000000
          468.312500000000000000
          37.041666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NE'
        Color = clSilver
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
      object QRLabel12: TQRLabel
        Left = 443
        Top = 177
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1172.104166666667000000
          468.312500000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'BR'
        Color = clSilver
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
        Left = 411
        Top = 177
        Width = 8
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1087.437500000000000000
          468.312500000000000000
          21.166666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'
        Color = clSilver
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
        Left = 377
        Top = 177
        Width = 8
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          997.479166666666900000
          468.312500000000000000
          21.166666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'T'
        Color = clSilver
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
        Left = 358
        Top = 160
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          947.208333333333400000
          423.333333333333300000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TURNOS'
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
        Left = 280
        Top = 160
        Width = 29
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          740.833333333333400000
          423.333333333333300000
          76.729166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SEXO'
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
        Left = 271
        Top = 177
        Width = 11
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          717.020833333333400000
          468.312500000000000000
          29.104166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'M'
        Color = clSilver
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
        Left = 307
        Top = 177
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          812.270833333333400000
          468.312500000000000000
          18.520833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'F'
        Color = clSilver
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
        Left = 340
        Top = 177
        Width = 11
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          899.583333333333400000
          468.312500000000000000
          29.104166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'M'
        Color = clSilver
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
      object QRLabel3: TQRLabel
        Left = 190
        Top = 160
        Width = 31
        Height = 29
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          76.729166666666680000
          502.708333333333400000
          423.333333333333300000
          82.020833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QT ALU'
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
        Left = 142
        Top = 168
        Width = 40
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          375.708333333333400000
          444.500000000000000000
          105.833333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TURMA'
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
      object QRLDescSer: TQRLabel
        Left = 9
        Top = 168
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          23.812500000000000000
          444.500000000000000000
          79.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'S'#201'RIE'
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
      object QRLabel19: TQRLabel
        Left = 758
        Top = 177
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2005.541666666667000000
          468.312500000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SR'
        Color = clSilver
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
      object QRLabel20: TQRLabel
        Left = 724
        Top = 177
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1915.583333333333000000
          468.312500000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '+5'
        Color = clSilver
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
      object QRLabel21: TQRLabel
        Left = 683
        Top = 177
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1807.104166666667000000
          468.312500000000000000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '3 a 5'
        Color = clSilver
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
      object QRLabel22: TQRLabel
        Left = 648
        Top = 177
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1714.500000000000000000
          468.312500000000000000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1 a 3'
        Color = clSilver
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
      object QRLabel23: TQRLabel
        Left = 794
        Top = 177
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2100.791666666667000000
          468.312500000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SD'
        Color = clSilver
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
      object QRLabel24: TQRLabel
        Left = 827
        Top = 177
        Width = 16
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2188.104166666667000000
          468.312500000000000000
          42.333333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'AU'
        Color = clSilver
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
      object QRLabel25: TQRLabel
        Left = 865
        Top = 177
        Width = 12
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2288.645833333333000000
          468.312500000000000000
          31.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'VI'
        Color = clSilver
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
      object QRLabel26: TQRLabel
        Left = 901
        Top = 177
        Width = 10
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2383.895833333333000000
          468.312500000000100000
          26.458333333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FI'
        Color = clSilver
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
      object QRLabel27: TQRLabel
        Left = 932
        Top = 177
        Width = 17
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2465.916666666667000000
          468.312500000000100000
          44.979166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ME'
        Color = clSilver
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
      object QRLabel28: TQRLabel
        Left = 967
        Top = 177
        Width = 18
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2558.520833333333000000
          468.312500000000100000
          47.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MU'
        Color = clSilver
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
      object QRLabel29: TQRLabel
        Left = 1002
        Top = 177
        Width = 16
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2651.125000000000000000
          468.312500000000100000
          42.333333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'OU'
        Color = clSilver
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
      object QRLabel30: TQRLabel
        Left = 872
        Top = 160
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2307.166666666667000000
          423.333333333333300000
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
      object QRShape3: TQRShape
        Left = 292
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          772.583333333333400000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape4: TQRShape
        Left = 362
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          957.791666666666800000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape5: TQRShape
        Left = 397
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1050.395833333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape6: TQRShape
        Left = 467
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1235.604166666667000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape7: TQRShape
        Left = 502
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1328.208333333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape8: TQRShape
        Left = 537
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1420.812500000000000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape9: TQRShape
        Left = 642
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          1698.625000000000000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape10: TQRShape
        Left = 607
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1606.020833333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape11: TQRShape
        Left = 572
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1513.416666666667000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape12: TQRShape
        Left = 852
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2254.250000000000000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape13: TQRShape
        Left = 817
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2161.645833333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape15: TQRShape
        Left = 747
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1976.437500000000000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape16: TQRShape
        Left = 712
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1883.833333333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape17: TQRShape
        Left = 677
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1791.229166666667000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape18: TQRShape
        Left = 957
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2532.062500000000000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape19: TQRShape
        Left = 922
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2439.458333333333000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape20: TQRShape
        Left = 887
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2346.854166666667000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape21: TQRShape
        Left = 992
        Top = 175
        Width = 1
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          2624.666666666667000000
          463.020833333333400000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape22: TQRShape
        Left = 432
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          1143.000000000000000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape23: TQRShape
        Left = 327
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          865.187500000000000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape14: TQRShape
        Left = 782
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          2069.041666666667000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape24: TQRShape
        Left = 257
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          679.979166666666800000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape25: TQRShape
        Left = 187
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          494.770833333333400000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape26: TQRShape
        Left = 135
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          357.187500000000000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape50: TQRShape
        Left = 222
        Top = 157
        Width = 2
        Height = 35
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          92.604166666666680000
          587.375000000000000000
          415.395833333333400000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRLPage: TQRLabel
        Left = 1007
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
          2664.354166666667000000
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
      object QRLabel32: TQRLabel
        Left = 1002
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
      object QRLabel34: TQRLabel
        Left = 225
        Top = 160
        Width = 31
        Height = 29
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          76.729166666666680000
          595.312500000000000000
          423.333333333333300000
          82.020833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QT PBE'
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
      object QRLParam: TQRLabel
        Left = 0
        Top = 132
        Width = 1027
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          0.000000000000000000
          349.250000000000000000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLParam'
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
      Top = 319
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
      Top = 239
      Width = 1027
      Height = 16
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = DetailBeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        42.333333333333340000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText1: TQRDBText
        Left = 3
        Top = 1
        Width = 127
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
          336.020833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Curso'
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
        Left = 139
        Top = 1
        Width = 45
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          367.770833333333400000
          2.645833333333333000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Turma'
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
      object QRDBText3: TQRDBText
        Left = 189
        Top = 1
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          500.062500000000100000
          2.645833333333333000
          82.020833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Quantidade'
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
        Left = 262
        Top = 1
        Width = 27
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          693.208333333333400000
          2.645833333333333000
          71.437500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Homens'
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
      object QRDBText5: TQRDBText
        Left = 298
        Top = 1
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          788.458333333333400000
          2.645833333333333000
          66.145833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Mulheres'
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
      object QRDBText6: TQRDBText
        Left = 332
        Top = 1
        Width = 27
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          878.416666666666800000
          2.645833333333333000
          71.437500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Manha'
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
        Left = 366
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          968.375000000000000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Tarde'
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
        Left = 401
        Top = 1
        Width = 29
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1060.979166666667000000
          2.645833333333333000
          76.729166666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Noite'
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
        Left = 435
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1150.937500000000000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Brancos'
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
      object QRDBText10: TQRDBText
        Left = 470
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1243.541666666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Pretos'
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
        Left = 540
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1428.750000000000000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Pardos'
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
        Left = 505
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1336.145833333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Amarelos'
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
        Left = 680
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1799.166666666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'R2'
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
      object QRDBText16: TQRDBText
        Left = 646
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1709.208333333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'R1'
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
      object QRDBText17: TQRDBText
        Left = 610
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1613.958333333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Nao_Declarada'
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
      object QRDBText18: TQRDBText
        Left = 575
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1521.354166666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Indigena'
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
      object QRDBText19: TQRDBText
        Left = 749
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1981.729166666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'R4'
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
      object QRDBText20: TQRDBText
        Left = 715
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1891.770833333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'R3'
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
      object QRDBText21: TQRDBText
        Left = 994
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2629.958333333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Outras'
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
      object QRDBText22: TQRDBText
        Left = 960
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2540.000000000000000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Multiplas'
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
      object QRDBText23: TQRDBText
        Left = 925
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2447.395833333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Mental'
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
      object QRDBText24: TQRDBText
        Left = 890
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2354.791666666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Fisica'
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
      object QRDBText25: TQRDBText
        Left = 855
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2262.187500000000000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Visual'
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
      object QRDBText26: TQRDBText
        Left = 820
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2169.583333333333000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Auditivo'
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
      object QRDBText27: TQRDBText
        Left = 785
        Top = 1
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2076.979166666667000000
          2.645833333333333000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Def_Nenhuma'
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
      object QRShape27: TQRShape
        Left = 992
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2624.666666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape28: TQRShape
        Left = 957
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2532.062500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape29: TQRShape
        Left = 922
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2439.458333333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape30: TQRShape
        Left = 887
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2346.854166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape31: TQRShape
        Left = 852
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2254.250000000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape32: TQRShape
        Left = 817
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2161.645833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape33: TQRShape
        Left = 747
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1976.437500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape34: TQRShape
        Left = 712
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1883.833333333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape35: TQRShape
        Left = 677
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1791.229166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape36: TQRShape
        Left = 607
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1606.020833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape37: TQRShape
        Left = 572
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1513.416666666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape38: TQRShape
        Left = 537
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1420.812500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape39: TQRShape
        Left = 502
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1328.208333333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape40: TQRShape
        Left = 467
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1235.604166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape41: TQRShape
        Left = 397
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1050.395833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape42: TQRShape
        Left = 362
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          957.791666666666800000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape43: TQRShape
        Left = 292
        Top = 0
        Width = 1
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          772.583333333333400000
          0.000000000000000000
          2.645833333333333000)
        Pen.Style = psDot
        Shape = qrsVertLine
      end
      object QRShape44: TQRShape
        Left = 642
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1698.625000000000000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape45: TQRShape
        Left = 432
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1143.000000000000000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape46: TQRShape
        Left = 327
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          865.187500000000000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape47: TQRShape
        Left = 187
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          494.770833333333400000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape48: TQRShape
        Left = 135
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          357.187500000000000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape49: TQRShape
        Left = 782
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2069.041666666667000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape51: TQRShape
        Left = 222
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          587.375000000000000000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRDBText28: TQRDBText
        Left = 224
        Top = 1
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          592.666666666666800000
          2.645833333333333000
          82.020833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'Bolsa_Escola'
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
      object QRShape52: TQRShape
        Left = 257
        Top = 0
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          679.979166666666800000
          0.000000000000000000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
    end
    object QRBand2: TQRBand
      Left = 48
      Top = 255
      Width = 1027
      Height = 64
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRBand2BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        169.333333333333300000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel31: TQRLabel
        Left = 144
        Top = 3
        Width = 41
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          381.000000000000000000
          7.937500000000000000
          108.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TOTAL:'
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
      object lblSexoM: TQRLabel
        Left = 262
        Top = 3
        Width = 27
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          693.208333333333400000
          7.937500000000000000
          71.437500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblSexoF: TQRLabel
        Left = 298
        Top = 3
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          788.458333333333400000
          7.937500000000000000
          66.145833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblQtde: TQRLabel
        Left = 189
        Top = 3
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          500.062500000000100000
          7.937500000000000000
          82.020833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblBE: TQRLabel
        Left = 224
        Top = 3
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          592.666666666666800000
          7.937500000000000000
          82.020833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblTurnoT: TQRLabel
        Left = 366
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          968.375000000000000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblTurnoM: TQRLabel
        Left = 332
        Top = 3
        Width = 27
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          878.416666666666800000
          7.937500000000000000
          71.437500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblTurnoN: TQRLabel
        Left = 401
        Top = 3
        Width = 29
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1060.979166666667000000
          7.937500000000000000
          76.729166666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblNd: TQRLabel
        Left = 610
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1613.958333333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblPa: TQRLabel
        Left = 540
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1428.750000000000000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblId: TQRLabel
        Left = 575
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1521.354166666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblAm: TQRLabel
        Left = 505
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1336.145833333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblPr: TQRLabel
        Left = 470
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1243.541666666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblBr: TQRLabel
        Left = 435
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1150.937500000000000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblMp: TQRLabel
        Left = 960
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2540.000000000000000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblMe: TQRLabel
        Left = 925
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2447.395833333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblFi: TQRLabel
        Left = 890
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2354.791666666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblVi: TQRLabel
        Left = 855
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2262.187500000000000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblAu: TQRLabel
        Left = 820
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2169.583333333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblSd: TQRLabel
        Left = 785
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2076.979166666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblOt: TQRLabel
        Left = 994
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2629.958333333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblR4: TQRLabel
        Left = 749
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1981.729166666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblR3: TQRLabel
        Left = 715
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1891.770833333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblR2: TQRLabel
        Left = 680
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1799.166666666667000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object lblR1: TQRLabel
        Left = 646
        Top = 3
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1709.208333333333000000
          7.937500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '0'
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
      object QRLabel33: TQRLabel
        Left = 14
        Top = 31
        Width = 52
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          37.041666666666670000
          82.020833333333340000
          137.583333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Legenda:'
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
      object QRLabel35: TQRLabel
        Left = 71
        Top = 31
        Width = 955
        Height = 25
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          66.145833333333340000
          187.854166666666700000
          82.020833333333340000
          2526.770833333333000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 
          'ALU (Aluno) - PBE (Programa Bolsa Escola) - Sexo: (M)asculino, (' +
          'F)eminino - Turnos: (M)anh'#227', (T)arde, (N)oite - Etnia: (BR)anco,' +
          ' (NE)gro, (AM)arelo, (PA)rdo, (IN)d'#237'gena, (ND) N'#227'o Declarado Def' +
          'ici'#234'ncia: (SD) Sem Defici'#234'ncia, (AU)ditiva, (VI)sual, (FI)sica, ' +
          '(ME)ntal, (MU)ltipla, (OU)tra'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
      object QRShape53: TQRShape
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
      object QRShape54: TQRShape
        Left = 135
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          357.187500000000000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape55: TQRShape
        Left = 187
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          494.770833333333400000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape56: TQRShape
        Left = 222
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          587.375000000000000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape57: TQRShape
        Left = 257
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          679.979166666666800000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape59: TQRShape
        Left = 327
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          865.187500000000000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape62: TQRShape
        Left = 432
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1143.000000000000000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape68: TQRShape
        Left = 642
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          1698.625000000000000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
      object QRShape72: TQRShape
        Left = 782
        Top = -1
        Width = 2
        Height = 16
        Frame.Color = clWhite
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          2069.041666666667000000
          -2.645833333333333000
          5.291666666666667000)
        Pen.Color = clWhite
        Pen.Width = 2
        Shape = qrsRectangle
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      'SELECT NO_CUR as Curso, ct.CO_SIGLA_TURMA as Turma,'
      ''
      #9'COUNT(co_alu) as '#39'Quantidade'#39', '
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE co_sexo_alu = '#39'M'#39
      #9#9#9'and m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      
        '                                                and m.co_ano_mes' +
        '_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Homens'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE co_sexo_alu = '#39'F'#39
      #9#9#9'and m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Mulheres'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(CO_ALU) FROM tb08_matrcur'
      #9#9'WHERE co_turn_mat = '#39'M'#39
      #9#9#9'and co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and co_cur = cur.co_cur'
      #9#9#9'and co_tur = tur.co_tur'
      'and co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Manha'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(CO_ALU) FROM tb08_matrcur'
      #9#9'WHERE co_turn_mat = '#39'V'#39
      #9#9#9'and co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and co_cur = cur.co_cur'
      #9#9#9'and co_tur = tur.co_tur'
      'and co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Tarde'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(CO_ALU) FROM tb08_matrcur'
      #9#9'WHERE co_turn_mat = '#39'N'#39
      #9#9#9'and co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and co_cur = cur.co_cur'
      #9#9#9'and co_tur = tur.co_tur'
      'and co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Noite'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'B'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Brancos'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'P'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Pretos'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'A'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_tur = tur.co_tur'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Amarelos'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'D'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Pardos'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'I'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Indigena'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_raca = '#39'N'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_tur = tur.co_tur'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Nao_Declarada'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and renda_familiar = 1'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'R1'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and renda_familiar = 2'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'R2'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and renda_familiar = 3'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'R3'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and renda_familiar = 4'
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'R4'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'N'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Nenhuma'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'A'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_tur = tur.co_tur'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Auditivo'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'V'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Visual'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'F'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Fisica'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'M'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Mental'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'I'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Multiplas'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and tp_def = '#39'O'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      #9#9#9'and m.co_tur = tur.co_tur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Def_Outras'#39','
      ''
      #9'('
      #9#9'SELECT COUNT(m.CO_ALU) FROM tb08_matrcur m'
      #9#9'JOIN tb07_aluno a ON a.co_alu = m.co_alu'
      #9#9'WHERE m.co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9#9#9'and fla_bolsa_escola = '#39'true'#39
      #9#9#9'and m.co_cur = cur.co_cur'
      'and m.co_ano_mes_mat = anoSelecionado'
      #9#9#9'and m.co_tur = tur.co_tur'
      #9#9#9'and m.co_emp = substitui'
      #9') as '#39'Bolsa_Escola'#39
      ''
      'FROM tb08_matrcur m'
      ''
      
        'JOIN tb01_curso cur ON cur.co_cur = m.co_cur and m.co_modu_cur =' +
        ' cur.co_modu_cur and cur.co_emp = m.co_emp'
      
        'JOIN tb06_turmas tur ON tur.co_tur = m.co_tur and tur.co_modu_cu' +
        'r = m.co_modu_cur and m.co_cur = tur.co_cur and m.co_emp = tur.c' +
        'o_emp'
      'join tb129_cadturmas ct on ct.co_tur = tur.co_tur'
      'WHERE co_sit_mat not in ('#39'C'#39','#39'X'#39')'
      #9'and m.co_emp = substitui'
      'and m.co_ano_mes_mat = anoSelecionado'
      
        'Group By m.co_emp,cur.co_cur,cur.no_cur,tur.co_tur,ct.co_sigla_t' +
        'urma'
      'order by cur.no_cur, ct.co_sigla_turma')
  end
  inherited QryCabecalhoRel: TADOQuery
    Left = 312
    Top = 64
  end
end
