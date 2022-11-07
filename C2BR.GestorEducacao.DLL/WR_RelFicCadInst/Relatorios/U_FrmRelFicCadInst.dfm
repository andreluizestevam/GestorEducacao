inherited FrmRelFicCadInst: TFrmRelFicCadInst
  Left = 224
  Top = 56
  Width = 864
  Height = 757
  Caption = 'FrmRelFicCadInst'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 11
    Top = 8
    BeforePrint = QuickRep1BeforePrint
    DataSet = QryRelatorio
    Functions.DATA = (
      '0'
      '0'
      #39#39)
    Page.Values = (
      50.000000000000000000
      2970.000000000000000000
      50.000000000000000000
      2100.000000000000000000
      127.000000000000000000
      127.000000000000000000
      0.000000000000000000)
    Units = MM
    inherited PageHeaderBand1: TQRBand
      Top = 19
      Height = 329
      Frame.DrawBottom = False
      Size.Values = (
        870.479166666666800000
        1846.791666666667000000)
      inherited LblTituloRel: TQRLabel
        Top = 113
        Height = 19
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          298.979166666666700000
          1836.208333333333000000)
        Caption = 'FICHA DE INFORMA'#199#213'ES DA UNIDADE - PERFIL ADMINISTRATIVO'
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
        Size.Values = (
          44.979166666666670000
          1783.291666666667000000
          21.166666666666670000
          63.500000000000000000)
        FontSize = 8
      end
      inherited QRSysData2: TQRSysData
        Size.Values = (
          44.979166666666670000
          1764.770833333333000000
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
      object QRShape1: TQRShape
        Left = 0
        Top = 145
        Width = 698
        Height = 37
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          97.895833333333340000
          0.000000000000000000
          383.645833333333400000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 5
        Top = 147
        Width = 97
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          388.937500000000000000
          256.645833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNIDADE ESCOLAR'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRDBText1: TQRDBText
        Left = 4
        Top = 163
        Width = 220
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          431.270833333333400000
          582.083333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clGray
        DataSet = QryRelatorio
        DataField = 'NO_FANTAS_EMP'
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
      object QRDBText2: TQRDBText
        Left = 77
        Top = 191
        Width = 275
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          203.729166666666700000
          505.354166666666700000
          727.604166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_RAZSOC_EMP'
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
        Left = 4
        Top = 191
        Width = 67
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          505.354166666666700000
          177.270833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Raz'#227'o Social:'
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
      object QRDBText3: TQRDBText
        Left = 231
        Top = 163
        Width = 60
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          611.187500000000000000
          431.270833333333400000
          158.750000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clGray
        DataSet = QryRelatorio
        DataField = 'sigla'
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
      object QRDBText7: TQRDBText
        Left = 403
        Top = 212
        Width = 115
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1066.270833333333000000
          560.916666666666800000
          304.270833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_SIGLA_NUCLEO'
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
      object QRLabel8: TQRLabel
        Left = 360
        Top = 212
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          952.500000000000000000
          560.916666666666700000
          97.895833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#250'cleo:'
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
      object QRDBText8: TQRDBText
        Left = 77
        Top = 233
        Width = 210
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          203.729166666666700000
          616.479166666666800000
          555.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_CLAS'
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
      object QRLabel9: TQRLabel
        Left = 4
        Top = 233
        Width = 69
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          616.479166666666800000
          182.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Classifica'#231#227'o:'
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
      object QRDBText9: TQRDBText
        Left = 77
        Top = 212
        Width = 214
        Height = 16
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          42.333333333333340000
          203.729166666666700000
          560.916666666666800000
          566.208333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_TIPOEMP'
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
      object QRLabel10: TQRLabel
        Left = 4
        Top = 212
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          560.916666666666700000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Categoria:'
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
      object QRLabel11: TQRLabel
        Left = 5
        Top = 253
        Width = 137
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          669.395833333333400000
          362.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Cria'#231#227'o (N'#186' Decreto - Data):'
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
      object QRDBText12: TQRDBText
        Left = 298
        Top = 163
        Width = 100
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          788.458333333333400000
          431.270833333333400000
          264.583333333333400000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clGray
        DataSet = QryRelatorio
        DataField = 'NU_INEP'
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
      object QRLabel14: TQRLabel
        Left = 320
        Top = 233
        Width = 77
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          846.666666666666600000
          616.479166666666800000
          203.729166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Funcionamento:'
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
      object QRDBText13: TQRDBText
        Left = 403
        Top = 233
        Width = 115
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1066.270833333333000000
          616.479166666666800000
          304.270833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'FUNCIONAMENTO'
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
      object QRLabel16: TQRLabel
        Left = 5
        Top = 271
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          717.020833333333400000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Endere'#231'o:'
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
      object QRDBText16: TQRDBText
        Left = 58
        Top = 271
        Width = 305
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          153.458333333333300000
          717.020833333333400000
          806.979166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_END_EMP'
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
      object QRLabel15: TQRLabel
        Left = 196
        Top = 290
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          518.583333333333400000
          767.291666666666800000
          97.895833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Cidade:'
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
      object QRDBText19: TQRDBText
        Left = 236
        Top = 290
        Width = 149
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          624.416666666666800000
          767.291666666666800000
          394.229166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_CIDADE'
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
      object QRDBText20: TQRDBText
        Left = 40
        Top = 290
        Width = 151
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          105.833333333333300000
          767.291666666666800000
          399.520833333333400000)
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
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel19: TQRLabel
        Left = 5
        Top = 290
        Width = 33
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          767.291666666666800000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Bairro:'
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
      object QRLabel20: TQRLabel
        Left = 392
        Top = 290
        Width = 23
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1037.166666666667000000
          767.291666666666800000
          60.854166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CEP:'
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
      object QRLabel22: TQRLabel
        Left = 524
        Top = 290
        Width = 46
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1386.416666666667000000
          767.291666666666800000
          121.708333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Telefone:'
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
      object QRLabel25: TQRLabel
        Left = 524
        Top = 310
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1386.416666666667000000
          820.208333333333400000
          58.208333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Fax:'
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
      object QRLabel26: TQRLabel
        Left = 5
        Top = 310
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          820.208333333333400000
          58.208333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Site:'
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
      object QRDBText26: TQRDBText
        Left = 31
        Top = 310
        Width = 249
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          82.020833333333340000
          820.208333333333500000
          658.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_WEB_EMP'
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
      object QRLabel27: TQRLabel
        Left = 297
        Top = 310
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          785.812500000000000000
          820.208333333333400000
          74.083333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Email:'
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
      object QRDBText27: TQRDBText
        Left = 330
        Top = 310
        Width = 192
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          873.124999999999900000
          820.208333333333500000
          508.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_EMAIL_EMP'
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
      object QRLabel62: TQRLabel
        Left = 259
        Top = 147
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          685.270833333333400000
          388.937500000000000000
          84.666666666666680000)
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
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel63: TQRLabel
        Left = 361
        Top = 147
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          955.145833333333500000
          388.937500000000000000
          97.895833333333340000)
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
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLNuDtAta: TQRLabel
        Left = 145
        Top = 253
        Width = 137
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          383.645833333333400000
          669.395833333333400000
          362.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Cria'#231#227'o (N'#186' Decreto - Data):'
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
        Left = 353
        Top = 191
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          933.979166666666600000
          505.354166666666700000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' CNPJ:'
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
      object QRIEscola: TQRImage
        Left = 523
        Top = 88
        Width = 168
        Height = 121
        Frame.Color = clSilver
        Frame.DrawTop = True
        Frame.DrawBottom = True
        Frame.DrawLeft = True
        Frame.DrawRight = True
        Size.Values = (
          320.145833333333400000
          1383.770833333333000000
          232.833333333333400000
          444.500000000000000000)
        Stretch = True
      end
      object QRDBIEscola: TQRDBImage
        Left = 523
        Top = 160
        Width = 168
        Height = 121
        Enabled = False
        Frame.Color = clSilver
        Frame.DrawTop = True
        Frame.DrawBottom = True
        Frame.DrawLeft = True
        Frame.DrawRight = True
        Size.Values = (
          320.145833333333400000
          1383.770833333333000000
          423.333333333333300000
          444.500000000000000000)
        DataField = 'fotoEmpresa'
        DataSet = QryRelatorio
        Stretch = True
      end
      object QRLabel2: TQRLabel
        Left = 434
        Top = 147
        Width = 70
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1148.291666666667000000
          388.937500000000000000
          185.208333333333300000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TIPO UNIDADE'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRDBText10: TQRDBText
        Left = 404
        Top = 163
        Width = 110
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1068.916666666667000000
          431.270833333333400000
          291.041666666666700000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clGray
        DataSet = QryPerfilEmpresa
        DataField = 'DE_TIPO_UNIDA'
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
      object QRLCEPEmp: TQRLabel
        Left = 420
        Top = 290
        Width = 64
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1111.250000000000000000
          767.291666666666800000
          169.333333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLCEPEmp'
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
      object QRLTel1Emp: TQRLabel
        Left = 576
        Top = 290
        Width = 80
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1524.000000000000000000
          767.291666666666800000
          211.666666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLTel1Emp'
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
      object QRLFaxEmp: TQRLabel
        Left = 553
        Top = 310
        Width = 80
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1463.145833333333000000
          820.208333333333500000
          211.666666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLFaxEmp'
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
      object QRLCNPJEmp: TQRLabel
        Left = 403
        Top = 191
        Width = 115
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1066.270833333333000000
          505.354166666666700000
          304.270833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLCNPJEmp'
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
      Top = 598
      Size.Values = (
        31.750000000000000000
        1846.791666666667000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 482
        Width = 216
        Size.Values = (
          29.104166666666670000
          1275.291666666667000000
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
    object DetailBand1: TQRBand
      Left = 48
      Top = 348
      Width = 698
      Height = 4
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
        10.583333333333330000
        1846.791666666667000000)
      BandType = rbDetail
    end
    object QRSubDetailGestores: TQRSubDetail
      Left = 48
      Top = 394
      Width = 698
      Height = 16
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRSubDetailGestoresBeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        42.333333333333340000
        1846.791666666667000000)
      Master = QuickRep1
      DataSet = qryGestores
      FooterBand = GroupFooterBand1
      HeaderBand = GroupHeaderBand2
      PrintBefore = False
      PrintIfEmpty = True
      object QRDBText11: TQRDBText
        Left = 365
        Top = 1
        Width = 180
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          965.729166666666800000
          2.645833333333333000
          476.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryGestores
        DataField = 'NO_FUN'
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
      object QRLMatriculaGestor: TQRLabel
        Left = 5
        Top = 0
        Width = 50
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
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000-0'
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
      object QRLTelGestores: TQRLabel
        Left = 550
        Top = 0
        Width = 77
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1455.208333333333000000
          0.000000000000000000
          203.729166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMatriculaGestor'
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
      object QRLDtInicio: TQRLabel
        Left = 634
        Top = 0
        Width = 77
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1677.458333333333000000
          0.000000000000000000
          203.729166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLMatriculaGestor'
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
      object QRDBText6: TQRDBText
        Left = 322
        Top = 1
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          851.958333333333400000
          2.645833333333333000
          39.687500000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryGestores
        DataField = 'CO_SEXO_COL'
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
      object QRLIdade: TQRLabel
        Left = 344
        Top = 0
        Width = 14
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          910.166666666666600000
          0.000000000000000000
          37.041666666666670000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000-0'
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
      object QRLColabor: TQRLabel
        Left = 85
        Top = 0
        Width = 230
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          224.895833333333300000
          0.000000000000000000
          608.541666666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000-0'
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
    object QRSubDetailQtde: TQRSubDetail
      Left = 48
      Top = 460
      Width = 698
      Height = 18
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRSubDetailQtdeBeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        47.625000000000000000
        1846.791666666667000000)
      Master = QuickRep1
      DataSet = qryAno
      FooterBand = GroupFooterBand2
      HeaderBand = GroupHeaderBand1
      PrintBefore = False
      PrintIfEmpty = True
      object QrlAno: TQRLabel
        Left = 4
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
          10.583333333333330000
          2.645833333333333000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2009'
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
        Left = 36
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          95.250000000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object Qrl1CINI: TQRLabel
        Left = 71
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          187.854166666666700000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 109
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          288.395833333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 148
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          391.583333333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 184
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          486.833333333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 223
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          590.020833333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 258
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          682.625000000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 300
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          793.750000000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 334
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          883.708333333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 374
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          989.541666666666800000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 413
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1092.729166666667000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
        Left = 460
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1217.083333333333000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QrlTotalAluno: TQRLabel
        Left = 508
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1344.083333333333000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QrlTotProf: TQRLabel
        Left = 573
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1516.062500000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QrlTotFun: TQRLabel
        Left = 643
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1701.270833333333000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
    object GroupHeaderBand1: TQRBand
      Left = 48
      Top = 420
      Width = 698
      Height = 40
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = GroupHeaderBand1AfterPrint
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        105.833333333333300000
        1846.791666666667000000)
      BandType = rbGroupHeader
      object QRShape8: TQRShape
        Left = 549
        Top = 18
        Width = 149
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1452.562500000000000000
          47.625000000000000000
          394.229166666666700000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape4: TQRShape
        Left = 0
        Top = 1
        Width = 545
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
          1441.979166666667000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape7: TQRShape
        Left = 0
        Top = 19
        Width = 545
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
          1441.979166666667000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel45: TQRLabel
        Left = 191
        Top = 3
        Width = 166
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          505.354166666666700000
          7.937500000000000000
          439.208333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QTDE ALUNOS MATRICULADOS'
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
      object QRLabel71: TQRLabel
        Left = 4
        Top = 21
        Width = 24
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          55.562500000000000000
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
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLMat1: TQRLabel
        Left = 33
        Top = 21
        Width = 31
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          87.312500000000000000
          55.562500000000000000
          82.020833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'INFAN'
        Color = clSilver
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
      object QRLMat2: TQRLabel
        Left = 71
        Top = 21
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          187.854166666666700000
          55.562500000000000000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C INI'
        Color = clSilver
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
      object QRLMat3: TQRLabel
        Left = 105
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
          277.812500000000000000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C INT'
        Color = clSilver
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
      object QRLMat4: TQRLabel
        Left = 144
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
          381.000000000000000000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1C FIN'
        Color = clSilver
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
      object QRLMat5: TQRLabel
        Left = 184
        Top = 21
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          486.833333333333400000
          55.562500000000000000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2C INI'
        Color = clSilver
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
      object QRLMat6: TQRLabel
        Left = 219
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
          579.437500000000000000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2C FIN'
        Color = clSilver
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
      object QRLMat7: TQRLabel
        Left = 258
        Top = 21
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          682.625000000000000000
          55.562500000000000000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '3C INI'
        Color = clSilver
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
      object QRLMat8: TQRLabel
        Left = 296
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
          783.166666666666800000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '3C FIN'
        Color = clSilver
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
      object QRLMat9: TQRLabel
        Left = 334
        Top = 21
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          883.708333333333400000
          55.562500000000000000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '4C INI'
        Color = clSilver
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
      object QRLMat10: TQRLabel
        Left = 370
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
          978.958333333333200000
          55.562500000000000000
          84.666666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '4C FIN'
        Color = clSilver
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
      object QRLMat11: TQRLabel
        Left = 411
        Top = 21
        Width = 30
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1087.437500000000000000
          55.562500000000000000
          79.375000000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MULTI'
        Color = clSilver
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
      object QRLMat12: TQRLabel
        Left = 451
        Top = 21
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1193.270833333333000000
          55.562500000000000000
          97.895833333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'JOVAD'
        Color = clSilver
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
      object QRLabel86: TQRLabel
        Left = 501
        Top = 21
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1325.562500000000000000
          55.562500000000000000
          92.604166666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TOTAL'
        Color = clSilver
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
      object QRLabel84: TQRLabel
        Left = 554
        Top = 21
        Width = 63
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1465.791666666667000000
          55.562500000000000000
          166.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PROFESSOR'
        Color = clSilver
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
      object QRShape6: TQRShape
        Left = 549
        Top = 1
        Width = 149
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1452.562500000000000000
          2.645833333333333000
          394.229166666666700000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel85: TQRLabel
        Left = 632
        Top = 21
        Width = 53
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1672.166666666667000000
          55.562500000000000000
          140.229166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SERVIDOR'
        Color = clSilver
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
      object QRLabel70: TQRLabel
        Left = 569
        Top = 3
        Width = 109
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1505.479166666667000000
          7.937500000000000000
          288.395833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QTDE OPERACIONAL'
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
    end
    object GroupHeaderBand2: TQRBand
      Left = 48
      Top = 352
      Width = 698
      Height = 42
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = GroupHeaderBand2AfterPrint
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        111.125000000000000000
        1846.791666666667000000)
      BandType = rbGroupHeader
      object QRShape2: TQRShape
        Left = 0
        Top = 5
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
          13.229166666666670000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsRectangle
      end
      object QRLabel21: TQRLabel
        Left = 5
        Top = 5
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
          13.229166666666670000
          150.812500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'GESTORES'
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
      object QRShape10: TQRShape
        Left = 0
        Top = 24
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
          63.500000000000000000
          1846.791666666667000000)
        Brush.Color = clMedGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel29: TQRLabel
        Left = 85
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
          224.895833333333300000
          66.145833333333340000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME'
        Color = clMedGray
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
      object QRLabel30: TQRLabel
        Left = 5
        Top = 25
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
          66.145833333333340000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
        Color = clMedGray
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
      object QRLabel23: TQRLabel
        Left = 550
        Top = 25
        Width = 54
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1455.208333333333000000
          66.145833333333340000
          142.875000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TELEFONE'
        Color = clMedGray
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
      object QRLabel28: TQRLabel
        Left = 365
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
          965.729166666666800000
          66.145833333333340000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FUN'#199#195'O'
        Color = clMedGray
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
      object QRLabel6: TQRLabel
        Left = 322
        Top = 25
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          851.958333333333400000
          66.145833333333340000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
        Color = clMedGray
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
      object QRLabel7: TQRLabel
        Left = 347
        Top = 25
        Width = 11
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          918.104166666666800000
          66.145833333333340000
          29.104166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ID'
        Color = clMedGray
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
      object QRLabel12: TQRLabel
        Left = 634
        Top = 25
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1677.458333333333000000
          66.145833333333340000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT IN'#205'CIO'
        Color = clMedGray
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
    end
    object GroupFooterBand1: TQRBand
      Left = 48
      Top = 410
      Width = 698
      Height = 10
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
        26.458333333333330000
        1846.791666666667000000)
      BandType = rbGroupFooter
    end
    object QRBand1: TQRBand
      Left = 48
      Top = 488
      Width = 698
      Height = 40
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = QRBand1AfterPrint
      AlignToBottom = False
      BeforePrint = QRBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        105.833333333333300000
        1846.791666666667000000)
      BandType = rbGroupHeader
      object QRShape5: TQRShape
        Left = 0
        Top = 1
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
          2.645833333333333000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape9: TQRShape
        Left = 0
        Top = 19
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
          50.270833333333330000
          1846.791666666667000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLDisFuncao: TQRLabel
        Left = 5
        Top = 3
        Width = 213
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          7.937500000000000000
          563.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DISTRIBUI'#199#195'O FUNCION'#193'RIOS - ANO REF'
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
      object QRLabel13: TQRLabel
        Left = 4
        Top = 22
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          10.583333333333330000
          58.208333333333340000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FUN'#199#195'O'
        Color = clSilver
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
      object QRLabel17: TQRLabel
        Left = 193
        Top = 21
        Width = 17
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          510.645833333333300000
          55.562500000000000000
          44.979166666666670000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ATI'
        Color = clSilver
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
      object QRLabel18: TQRLabel
        Left = 228
        Top = 21
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          603.250000000000000000
          55.562500000000000000
          55.562500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ATE'
        Color = clSilver
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
      object QRLabel24: TQRLabel
        Left = 273
        Top = 21
        Width = 20
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          722.312500000000000000
          55.562500000000000000
          52.916666666666660000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FCE'
        Color = clSilver
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
      object QRLabel31: TQRLabel
        Left = 318
        Top = 21
        Width = 20
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          841.375000000000000000
          55.562500000000000000
          52.916666666666660000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FES'
        Color = clSilver
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
      object QRLabel32: TQRLabel
        Left = 358
        Top = 21
        Width = 20
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          947.208333333333400000
          55.562500000000000000
          52.916666666666660000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'LFR'
        Color = clSilver
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
      object QRLabel33: TQRLabel
        Left = 401
        Top = 21
        Width = 21
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1060.979166666667000000
          55.562500000000000000
          55.562500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'LME'
        Color = clSilver
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
      object QRLabel34: TQRLabel
        Left = 439
        Top = 21
        Width = 23
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1161.520833333333000000
          55.562500000000000000
          60.854166666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'LMA'
        Color = clSilver
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
      object QRLabel35: TQRLabel
        Left = 486
        Top = 21
        Width = 22
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1285.875000000000000000
          55.562500000000000000
          58.208333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SUS'
        Color = clSilver
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
      object QRLabel36: TQRLabel
        Left = 529
        Top = 21
        Width = 20
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1399.645833333333000000
          55.562500000000000000
          52.916666666666660000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TRE'
        Color = clSilver
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
      object QRLabel37: TQRLabel
        Left = 574
        Top = 21
        Width = 20
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1518.708333333333000000
          55.562500000000000000
          52.916666666666660000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FER'
        Color = clSilver
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
      object QRLabel40: TQRLabel
        Left = 653
        Top = 21
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1727.729166666667000000
          55.562500000000000000
          92.604166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TOTAL'
        Color = clSilver
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
      object QRLabel4: TQRLabel
        Left = 614
        Top = 21
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1624.541666666667000000
          55.562500000000000000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PROF'
        Color = clSilver
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
    end
    object QRSubDetailFuncao: TQRSubDetail
      Left = 48
      Top = 528
      Width = 698
      Height = 19
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRSubDetailFuncaoBeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        50.270833333333330000
        1846.791666666667000000)
      Master = QuickRep1
      DataSet = qryFuncao
      FooterBand = GroupFooterBand3
      HeaderBand = QRBand1
      PrintBefore = False
      PrintIfEmpty = True
      object QRLNoFuncao: TQRLabel
        Left = 4
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
          10.583333333333330000
          2.645833333333333000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2009'
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
      object QRLTotATI: TQRLabel
        Left = 182
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          481.541666666666700000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotATE: TQRLabel
        Left = 221
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          584.729166666666800000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotFCE: TQRLabel
        Left = 265
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          701.145833333333400000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotFES: TQRLabel
        Left = 310
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          820.208333333333500000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotLFR: TQRLabel
        Left = 350
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          926.041666666666800000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotLME: TQRLabel
        Left = 394
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1042.458333333333000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotLMA: TQRLabel
        Left = 434
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1148.291666666667000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotSUS: TQRLabel
        Left = 480
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1270.000000000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotTRE: TQRLabel
        Left = 521
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1378.479166666667000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotFER: TQRLabel
        Left = 566
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1497.541666666667000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotFuncao: TQRLabel
        Left = 660
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1746.250000000000000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotFunPROF: TQRLabel
        Left = 614
        Top = 1
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1624.541666666667000000
          2.645833333333333000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
    object GroupFooterBand2: TQRBand
      Left = 48
      Top = 478
      Width = 698
      Height = 10
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
        26.458333333333330000
        1846.791666666667000000)
      BandType = rbGroupFooter
    end
    object GroupFooterBand3: TQRBand
      Left = 48
      Top = 547
      Width = 698
      Height = 51
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
        134.937500000000000000
        1846.791666666667000000)
      BandType = rbGroupFooter
      object QRMemo1: TQRMemo
        Left = 5
        Top = 24
        Width = 684
        Height = 25
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          66.145833333333340000
          13.229166666666670000
          63.500000000000000000
          1809.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        Lines.Strings = (
          
            'ATI - Atividade Interna               LFR - Licen'#231'a Funcional   ' +
            '             TRE - Treinamento           FES - Estagi'#225'rio       ' +
            '             LMA - Licen'#231'a Maternidade  '
          
            'ATE - Atividade Externa             LME - Licen'#231'a M'#233'dica        ' +
            '            FER - F'#233'rias                    FCE - Cedido        ' +
            '                 SUS - Suspenso                                 ' +
            '                                                                ' +
            '          ')
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 7
      end
      object QRLabel38: TQRLabel
        Left = 8
        Top = 1
        Width = 38
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          21.166666666666670000
          2.645833333333333000
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
      object QRLTotalATI: TQRLabel
        Left = 182
        Top = 2
        Width = 28
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
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalATE: TQRLabel
        Left = 221
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          584.729166666666800000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalFCE: TQRLabel
        Left = 265
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          701.145833333333400000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalFES: TQRLabel
        Left = 310
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          820.208333333333500000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalLFR: TQRLabel
        Left = 350
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          926.041666666666800000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalLME: TQRLabel
        Left = 394
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1042.458333333333000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalLMA: TQRLabel
        Left = 434
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1148.291666666667000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalSUS: TQRLabel
        Left = 480
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1270.000000000000000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalTRE: TQRLabel
        Left = 521
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1378.479166666667000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalFER: TQRLabel
        Left = 566
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1497.541666666667000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalPROF: TQRLabel
        Left = 614
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1624.541666666667000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
      object QRLTotalFuncao: TQRLabel
        Left = 660
        Top = 2
        Width = 28
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1746.250000000000000000
          5.291666666666667000
          74.083333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '9.999'
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
  end
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    LockType = ltBatchOptimistic
    SQL.Strings = (
      
        'SELECT TE.NO_TIPOEMP, CI.NO_CLAS, CID.NO_CIDADE, BAI.NO_BAIRRO, ' +
        'NUC.NO_SIGLA_NUCLEO,  (SELECT DE_HISTORICO FROM TB39_HISTORICO  ' +
        ' WHERE FLA_TIPO_HISTORICO = '#39'C'#39'  AND CO_HISTORICO = E.CO_HIST_MA' +
        'T) CTAR_HISTMAT,  (SELECT DE_HISTORICO FROM TB39_HISTORICO   WHE' +
        'RE FLA_TIPO_HISTORICO = '#39'C'#39'  AND CO_HISTORICO = E.CO_HIST_BIB) C' +
        'TAR_HISTBIB,  (SELECT DE_HISTORICO FROM TB39_HISTORICO   WHERE F' +
        'LA_TIPO_HISTORICO = '#39'C'#39'  AND CO_HISTORICO = E.CO_HIST_SOL) CTAR_' +
        'HISTSOL,  (SELECT DE_HISTORICO FROM TB39_HISTORICO   WHERE FLA_T' +
        'IPO_HISTORICO = '#39'C'#39'  AND CO_HISTORICO = E.CO_HIST_INSC) CTAR_HIS' +
        'TINSC,  FUNCIONAMENTO = (CASE E.TP_HORA_FUNC '#9#9#9#9'WHEN '#39'M'#39' THEN '#39 +
        'Manh'#227#39' '#9#9#9#9'WHEN '#39'T'#39' THEN '#39'Tarde'#39#9#9#9#9'WHEN '#39'N'#39' THEN '#39'Noite'#39#9#9#9#9'WHE' +
        'N '#39'MT'#39' THEN '#39'Manh'#227'/Tarde'#39#9#9#9#9'WHEN '#39'MN'#39' THEN '#39'Manh'#227'/Noite'#39' '#9#9#9#9'WH' +
        'EN '#39'TN'#39' THEN '#39'Tarde/Noite'#39#9#9#9#9'WHEN '#39'MTN'#39' THEN '#39'Manh'#227'/Tarde/Noite' +
        #39#9#9#9#9'END),  SITUACAO = (CASE E.CO_SIT_EMP  '#9#9#9'WHEN '#39'A'#39' THEN '#39'Ati' +
        'vo'#39' '#9#9#9'WHEN '#39'I'#39' THEN '#39'Inativo'#39' '#9#9#9'END),  UNIDADEGESTORA = (CASE ' +
        'E.FLA_UNID_GESTORA  '#9#9#9'WHEN '#39'S'#39' THEN '#39'Sim'#39' '#9#9#9'WHEN '#39'N'#39' THEN '#39'N'#227'o' +
        #39' '#9#9#9'END),  UNIDADEATIVA = (CASE E.FLA_UNID_ATIVA  '#9#9#9'WHEN '#39'S'#39' T' +
        'HEN '#39'Sim'#39' '#9#9#9'WHEN '#39'N'#39' THEN '#39'N'#227'o'#39' '#9#9#9'END),  (Select no_fantas_emp' +
        ' from tb25_empresa   where co_emp = e.co_emp_pai) EMPRESAPAI,  E' +
        '.*, img.ImageStream as fotoEmpresa FROM TB25_EMPRESA E  left joi' +
        'n Image img on img.ImageId = E.FOTO_IMAGE_ID  '#9'JOIN TB24_TPEMPRE' +
        'SA TE ON TE.CO_TIPOEMP = E.CO_TIPOEMP  '#9'JOIN TB162_CLAS_INST CI ' +
        'ON CI.CO_CLAS = E.CO_CLAS  '#9'JOIN TB904_CIDADE CID ON CID.CO_CIDA' +
        'DE = E.CO_CIDADE  '#9'JOIN TB905_BAIRRO BAI ON BAI.CO_BAIRRO = E.CO' +
        '_BAIRRO   LEFT JOIN TB_NUCLEO_INST NUC ON NUC.CO_NUCLEO = E.CO_N' +
        'UCLEO  WHERE E.CO_EMP = 187')
  end
  object QryPerfilEmpresa: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      
        ' SET LANGUAGE PORTUGUESE  SELECT  COMPARTILHA =  (CASE PE.FLA_CO' +
        'MP_PREDIO WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '#39'1'#39' '
      
        'THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END), ASSENTAMENTO = (CASE PE.FLA' +
        '_ASSENTAMENTO WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '
      
        #39'1'#39' THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39'END),  ANEXO =  (CASE PE.FLA_A' +
        'NEXO WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '#39'1'#39' THEN'
      
        ' '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  MURO =  (CASE PE.TP_MURO WHEN ' +
        #39'N'#39' THEN '#39'N'#227'o'#39'WHEN '#39'S'#39' THEN '#39'Sim'#39'ELSE'
      
        ' '#39'N'#227'o declarado'#39' END),  COLETALIXO =  (CASE PE.FLA_COLETA_LIXO W' +
        'HEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '#39'1'#39' THEN '#39'Sim'#39
      
        'ELSE '#39'N'#227'o declarado'#39' END),  EDUCACAOINDIGENA =  (CASE PE.FLA_ED_' +
        'INDIGENA WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '#39'1'#39' '
      
        'THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  TRANSPORTEESCOLAR =  (CASE' +
        ' PE.FLA_TRANS_ESC WHEN '#39'0'#39' THEN '#39'N'#227'o'#39
      
        'WHEN '#39'1'#39' THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  ESGOTO =  (CASE P' +
        'E.TP_ESG WHEN '#39'N'#39' THEN '#39'N'#227'o'#39'WHEN '#39'S'#39' '
      
        'THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  BIBLIOTECA =  (CASE PE.FLA' +
        '_BIBLIOTECA WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '
      
        #39'1'#39' THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  GINASIO =  (CASE PE.FL' +
        'A_BIBLIOTECA WHEN '#39'0'#39' THEN '#39'N'#227'o'#39'WHEN '
      
        #39'1'#39' THEN '#39'Sim'#39'ELSE '#39'N'#227'o declarado'#39' END),  INST_ESPE =  (CASE PE.' +
        'TP_INST_ADEQ_ALU_ESP WHEN '#39'S'#39' THEN '
      
        #39'Sim'#39' WHEN '#39'N'#39' THEN '#39'N'#227'o'#39'WHEN '#39'F'#39' THEN '#39'N'#227'o Declarada'#39'ELSE '#39'N'#227'o ' +
        'declarado'#39' END),  ENERGIA =  '
      
        '(CASE PE.TP_ENERGIA WHEN '#39'T'#39' THEN '#39'Trif'#225'sica'#39' WHEN '#39'B'#39' THEN '#39'Bif' +
        #225'sica'#39'WHEN '#39'M'#39' THEN '#39'Monof'#225'sica'#39'ELSE '
      #39'N'#227'o declarado'#39' END),  '
      
        'PE.NU_INEP, PE.CO_QUI, PE.CO_ABAST, PE.NU_VOLT, PE.CO_PREDIO,P.N' +
        'O_PREDIO,PE.NU_AREA_TOTAL_EMP, '
      
        'PE.NU_AREA_CONST_EMP,PE.QT_PAVIMENTOS_EMP,PE.QT_SALA_AULA_EMP,PE' +
        '.QT_SALA_ADMIN_EMP,'
      
        'PE.QT_SALA_APOIO_EMP,PE.QT_BANHE_FEM_EMP,PE.QT_BANHE_MAS_EMP,PE.' +
        'QT_GINASIO_ESPOR_EMP,'
      
        'PE.QT_QUADRA_COBERT_EMP,PE.QT_QUADRA_ABERTA_EMP,PE.QT_PISCINA_EM' +
        'P,tpuni.DE_TIPO_UNIDA '
      'FROM TB173_PERFIL_EMPRESA PE  '
      'JOIN TB160_PERFIL_UNIDADE PU on PU.CO_EMP = PE.CO_EMP'
      
        'LEFT JOIN TB182_TIPO_UNIDA tpuni ON tpuni.CO_SIGLA_TIPO_UNIDA = ' +
        'PU.CO_TIPO_UNIDA'
      'LEFT JOIN TB171_PREDIO P ON PE.CO_PREDIO = P.CO_PREDIO  '
      'WHERE PE.CO_EMP = 2')
    Left = 576
    Top = 67
    object QryPerfilEmpresaCOMPARTILHA: TStringField
      FieldName = 'COMPARTILHA'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaASSENTAMENTO: TStringField
      FieldName = 'ASSENTAMENTO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaANEXO: TStringField
      FieldName = 'ANEXO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaMURO: TStringField
      FieldName = 'MURO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaCOLETALIXO: TStringField
      FieldName = 'COLETALIXO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaEDUCACAOINDIGENA: TStringField
      FieldName = 'EDUCACAOINDIGENA'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaTRANSPORTEESCOLAR: TStringField
      FieldName = 'TRANSPORTEESCOLAR'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaESGOTO: TStringField
      FieldName = 'ESGOTO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaENERGIA: TStringField
      FieldName = 'ENERGIA'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaNU_INEP: TIntegerField
      FieldName = 'NU_INEP'
    end
    object QryPerfilEmpresaCO_QUI: TIntegerField
      FieldName = 'CO_QUI'
    end
    object QryPerfilEmpresaCO_ABAST: TIntegerField
      FieldName = 'CO_ABAST'
    end
    object QryPerfilEmpresaNU_VOLT: TIntegerField
      FieldName = 'NU_VOLT'
    end
    object QryPerfilEmpresaCO_PREDIO: TIntegerField
      FieldName = 'CO_PREDIO'
    end
    object QryPerfilEmpresaBIBLIOTECA: TStringField
      FieldName = 'BIBLIOTECA'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaGINASIO: TStringField
      FieldName = 'GINASIO'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaINST_ESPE: TStringField
      FieldName = 'INST_ESPE'
      ReadOnly = True
      Size = 13
    end
    object QryPerfilEmpresaNO_PREDIO: TStringField
      FieldName = 'NO_PREDIO'
      Size = 50
    end
    object QryPerfilEmpresaNU_AREA_TOTAL_EMP: TIntegerField
      FieldName = 'NU_AREA_TOTAL_EMP'
    end
    object QryPerfilEmpresaNU_AREA_CONST_EMP: TIntegerField
      FieldName = 'NU_AREA_CONST_EMP'
    end
    object QryPerfilEmpresaQT_PAVIMENTOS_EMP: TIntegerField
      FieldName = 'QT_PAVIMENTOS_EMP'
    end
    object QryPerfilEmpresaQT_SALA_AULA_EMP: TIntegerField
      FieldName = 'QT_SALA_AULA_EMP'
    end
    object QryPerfilEmpresaQT_SALA_ADMIN_EMP: TIntegerField
      FieldName = 'QT_SALA_ADMIN_EMP'
    end
    object QryPerfilEmpresaQT_SALA_APOIO_EMP: TIntegerField
      FieldName = 'QT_SALA_APOIO_EMP'
    end
    object QryPerfilEmpresaQT_BANHE_FEM_EMP: TIntegerField
      FieldName = 'QT_BANHE_FEM_EMP'
    end
    object QryPerfilEmpresaQT_BANHE_MAS_EMP: TIntegerField
      FieldName = 'QT_BANHE_MAS_EMP'
    end
    object QryPerfilEmpresaQT_GINASIO_ESPOR_EMP: TIntegerField
      FieldName = 'QT_GINASIO_ESPOR_EMP'
    end
    object QryPerfilEmpresaQT_QUADRA_COBERT_EMP: TIntegerField
      FieldName = 'QT_QUADRA_COBERT_EMP'
    end
    object QryPerfilEmpresaQT_QUADRA_ABERTA_EMP: TIntegerField
      FieldName = 'QT_QUADRA_ABERTA_EMP'
    end
    object QryPerfilEmpresaQT_PISCINA_EMP: TIntegerField
      FieldName = 'QT_PISCINA_EMP'
    end
    object QryPerfilEmpresaDE_TIPO_UNIDA: TStringField
      FieldName = 'DE_TIPO_UNIDA'
      Size = 50
    end
  end
  object QryAlunoCurso: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      
        ' SELECT DISTINCT E.CO_EMP, M.CO_ANO_MES_MAT,  (SELECT DISTINCT C' +
        'OUNT(M.CO_ALU) FROM TB08_MATRCUR M   JOIN TB07_ALUNO A ON A.CO_A' +
        'LU = M.CO_ALU   JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9' JOIN ' +
        'TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND' +
        ' C.CO_SIGL_CUR = '#39'INFAN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALUINFAN,  (' +
        'SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M   JOIN TB07_' +
        'ALUNO A ON A.CO_ALU = M.CO_ALU   JOIN TB01_CURSO C ON C.CO_EMP =' +
        ' A.CO_EMP '#9' JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.' +
        'CO_SIGL_CUR = '#39'INFAN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALUINFAN, (S' +
        'ELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_AL' +
        'UNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.' +
        'CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_E' +
        'MP = 160 '#9'AND C.CO_SIGL_CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ' +
        ') ALU1CINI, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M' +
        ' '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON' +
        ' C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP' +
        ' '#9'WHERE C.CO_SIGL_CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ) TOTA' +
        'LU1CINI,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9 +
        'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C' +
        '.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9 +
        'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR' +
        ' = C.CO_CUR ) ALU1CINT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM T' +
        'B08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB' +
        '01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_E' +
        'MP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR = C.' +
        'CO_CUR ) TOTALU1CINT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB0' +
        '8_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01' +
        '_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP' +
        ' = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'1C FIN'#39#9 +
        #9'AND M.CO_CUR = C.CO_CUR ) ALU1CFIN,  (SELECT DISTINCT COUNT(M.C' +
        'O_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO' +
        '_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRES' +
        'A E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'1C FIN'#39#9#9'AND ' +
        'M.CO_CUR = C.CO_CUR ) TOTALU1CFIN, (SELECT DISTINCT COUNT(M.CO_A' +
        'LU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_AL' +
        'U '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E' +
        ' ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR' +
        ' = '#39'2C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU2CINI, (SELECT DISTINC' +
        'T COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO' +
        '_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN ' +
        'TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND' +
        ' C.CO_SIGL_CUR = '#39'2C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU2CINI' +
        ', (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB0' +
        '7_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP ' +
        '= A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.' +
        'CO_EMP = 160'#9
      #9'AND C.CO_SIGL_CUR '
      
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
        'M.CO_CUR = C.CO_CUR ) TOTALUEJA,'
      ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M'
      ' '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU'
      ' '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP'
      ' '#9'WHERE A.CO_EMP = 160'
      #9#9'AND M.CO_CUR = C.CO_CUR'
      ' ) ALUTOTAL,'
      ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP'
      ' '#9'WHERE COL.CO_EMP = 2'
      #9#9'AND COL.FLA_PROFESSOR = '#39'S'#39
      ' ) TOTALPROF,'
      ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP'
      ' '#9'WHERE COL.CO_EMP = 2'
      #9#9'AND COL.FLA_PROFESSOR = '#39'N'#39
      ' ) TOTALFUN'
      ' FROM TB25_EMPRESA E'
      #9'JOIN TB08_MATRCUR M ON M.CO_EMP = E.CO_EMP'
      ' WHERE E.CO_EMP = 2')
    Left = 320
    Top = 64
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
    object QryAlunoCursoTOTALPROF: TIntegerField
      FieldName = 'TOTALPROF'
      ReadOnly = True
    end
    object QryAlunoCursoTOTALFUN: TIntegerField
      FieldName = 'TOTALFUN'
      ReadOnly = True
    end
    object QryAlunoCursoCO_ANO_MES_MAT: TStringField
      FieldName = 'CO_ANO_MES_MAT'
      FixedChar = True
      Size = 6
    end
  end
  object qryAno: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      
        ' SELECT DISTINCT E.CO_EMP, M.CO_ANO_MES_MAT,  (SELECT DISTINCT C' +
        'OUNT(M.CO_ALU) FROM TB08_MATRCUR M   JOIN TB07_ALUNO A ON A.CO_A' +
        'LU = M.CO_ALU   JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9' JOIN ' +
        'TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND' +
        ' C.CO_SIGL_CUR = '#39'INFAN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALUINFAN,  (' +
        'SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M   JOIN TB07_' +
        'ALUNO A ON A.CO_ALU = M.CO_ALU   JOIN TB01_CURSO C ON C.CO_EMP =' +
        ' A.CO_EMP '#9' JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.' +
        'CO_SIGL_CUR = '#39'INFAN'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALUINFAN, (S' +
        'ELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_AL' +
        'UNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.' +
        'CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_E' +
        'MP = 160 '#9'AND C.CO_SIGL_CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ' +
        ') ALU1CINI, (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M' +
        ' '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON' +
        ' C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP' +
        ' '#9'WHERE C.CO_SIGL_CUR = '#39'1C INI'#39' '#9'AND M.CO_CUR = C.CO_CUR ) TOTA' +
        'LU1CINI,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9 +
        'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C' +
        '.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9 +
        'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR' +
        ' = C.CO_CUR ) ALU1CINT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM T' +
        'B08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB' +
        '01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_E' +
        'MP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'1C INT'#39#9#9'AND M.CO_CUR = C.' +
        'CO_CUR ) TOTALU1CINT,  (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB0' +
        '8_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01' +
        '_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP' +
        ' = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR = '#39'1C FIN'#39#9 +
        #9'AND M.CO_CUR = C.CO_CUR ) ALU1CFIN,  (SELECT DISTINCT COUNT(M.C' +
        'O_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO' +
        '_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRES' +
        'A E ON E.CO_EMP = A.CO_EMP '#9'WHERE C.CO_SIGL_CUR = '#39'1C FIN'#39#9#9'AND ' +
        'M.CO_CUR = C.CO_CUR ) TOTALU1CFIN, (SELECT DISTINCT COUNT(M.CO_A' +
        'LU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_AL' +
        'U '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN TB25_EMPRESA E' +
        ' ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND C.CO_SIGL_CUR' +
        ' = '#39'2C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) ALU2CINI, (SELECT DISTINC' +
        'T COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB07_ALUNO A ON A.CO' +
        '_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP '#9'JOIN ' +
        'TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.CO_EMP = 160'#9#9'AND' +
        ' C.CO_SIGL_CUR = '#39'2C INI'#39#9#9'AND M.CO_CUR = C.CO_CUR ) TOTALU2CINI' +
        ', (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M '#9'JOIN TB0' +
        '7_ALUNO A ON A.CO_ALU = M.CO_ALU '#9'JOIN TB01_CURSO C ON C.CO_EMP ' +
        '= A.CO_EMP '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP '#9'WHERE A.' +
        'CO_EMP = 160'#9
      #9'AND C.CO_SIGL_CUR '
      
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
        'M.CO_CUR = C.CO_CUR ) TOTALUEJA,'
      ' (SELECT DISTINCT COUNT(M.CO_ALU) FROM TB08_MATRCUR M'
      ' '#9'JOIN TB07_ALUNO A ON A.CO_ALU = M.CO_ALU'
      ' '#9'JOIN TB01_CURSO C ON C.CO_EMP = A.CO_EMP'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP'
      ' '#9'WHERE A.CO_EMP = 160'
      #9#9'AND M.CO_CUR = C.CO_CUR'
      ' ) ALUTOTAL,'
      ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP'
      ' '#9'WHERE COL.CO_EMP = 2'
      #9#9'AND COL.FLA_PROFESSOR = '#39'S'#39
      ' ) TOTALPROF,'
      ' (SELECT DISTINCT COUNT(COL.CO_COL) FROM TB03_COLABOR COL'
      ' '#9'JOIN TB25_EMPRESA E ON E.CO_EMP = COL.CO_EMP'
      ' '#9'WHERE COL.CO_EMP = 2'
      #9#9'AND COL.FLA_PROFESSOR = '#39'N'#39
      ' ) TOTALFUN'
      ' FROM TB25_EMPRESA E'
      #9'JOIN TB08_MATRCUR M ON M.CO_EMP = E.CO_EMP'
      ' WHERE E.CO_EMP = 2')
    Left = 472
    Top = 104
  end
  object qryGestores: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    Left = 411
    Top = 76
  end
  object qryFuncao: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    Left = 571
    Top = 513
  end
end
