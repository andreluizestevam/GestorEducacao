inherited FrmRelFicInfoNucGestao: TFrmRelFicInfoNucGestao
  Left = 326
  Top = 149
  Width = 864
  Height = 552
  VertScrollBar.Position = 96
  Caption = 'FrmRelFicInfoNucGestao'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 11
    Top = -88
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
      Height = 160
      Frame.DrawBottom = False
      Size.Values = (
        423.333333333333300000
        1846.791666666667000000)
      inherited LblTituloRel: TQRLabel
        Top = 113
        Height = 19
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          298.979166666666700000
          1836.208333333333000000)
        Caption = 'FICHA DE INFORMA'#199#213'ES DO N'#218'CLEO DE GEST'#195'O'
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
      object QRDBText7: TQRDBText
        Left = 43
        Top = 140
        Width = 230
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          113.770833333333300000
          370.416666666666700000
          608.541666666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DE_NUCLEO'
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
        Left = 0
        Top = 140
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          0.000000000000000000
          370.416666666666700000
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
      object QRLabel2: TQRLabel
        Left = 304
        Top = 140
        Width = 35
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          804.333333333333200000
          370.416666666666700000
          92.604166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SIGLA:'
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
        Left = 347
        Top = 140
        Width = 115
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          918.104166666666800000
          370.416666666666700000
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
    end
    inherited QRBANDSGIE: TQRBand
      Top = 395
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
      Top = 179
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
      Top = 225
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
      DataSet = qryUnidNucleo
      FooterBand = GroupFooterBand1
      HeaderBand = GroupHeaderBand2
      PrintBefore = False
      PrintIfEmpty = True
      object QRDBText8: TQRDBText
        Left = 5
        Top = 1
        Width = 60
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
          158.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryUnidNucleo
        DataField = 'sigla'
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
        Left = 85
        Top = 1
        Width = 257
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          224.895833333333300000
          2.645833333333333000
          679.979166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryUnidNucleo
        DataField = 'NO_FANTAS_EMP'
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
        Left = 349
        Top = 1
        Width = 156
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          923.395833333333400000
          2.645833333333333000
          412.750000000000100000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryUnidNucleo
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
      object QRDBText4: TQRDBText
        Left = 517
        Top = 1
        Width = 172
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1367.895833333333000000
          2.645833333333333000
          455.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryUnidNucleo
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
    end
    object QRSubDetailQtde: TQRSubDetail
      Left = 48
      Top = 292
      Width = 698
      Height = 16
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
        42.333333333333340000
        1846.791666666667000000)
      Master = QuickRep1
      DataSet = qryEquipNuc
      FooterBand = GroupFooterBand2
      HeaderBand = GroupHeaderBand1
      PrintBefore = False
      PrintIfEmpty = True
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
      object QRDBText5: TQRDBText
        Left = 78
        Top = 1
        Width = 261
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          206.375000000000000000
          2.645833333333333000
          690.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryEquipNuc
        DataField = 'NO_COL'
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
        Left = 346
        Top = 1
        Width = 71
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          915.458333333333200000
          2.645833333333333000
          187.854166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryEquipNuc
        DataField = 'sigla'
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
      object QRDBText11: TQRDBText
        Left = 430
        Top = 1
        Width = 195
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1137.708333333333000000
          2.645833333333333000
          515.937500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = qryEquipNuc
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
      object QRLStatusCol: TQRLabel
        Left = 635
        Top = 8
        Width = 58
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1680.104166666667000000
          21.166666666666670000
          153.458333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Inativo'
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
    object GroupHeaderBand1: TQRBand
      Left = 48
      Top = 251
      Width = 698
      Height = 41
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
        108.479166666666700000
        1846.791666666667000000)
      BandType = rbGroupHeader
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
        Width = 102
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
          269.875000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'EQUIPE DO N'#218'CLEO'
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
      object QRLabel29: TQRLabel
        Left = 78
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
          206.375000000000000000
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
      object QRLabel6: TQRLabel
        Left = 346
        Top = 25
        Width = 70
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          915.458333333333200000
          66.145833333333340000
          185.208333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNID ORIGEM'
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
        Left = 430
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
          1137.708333333333000000
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
      object QRLabel12: TQRLabel
        Left = 635
        Top = 25
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1680.104166666667000000
          66.145833333333340000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'STATUS'
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
    object GroupHeaderBand2: TQRBand
      Left = 48
      Top = 183
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
      object QRShape1: TQRShape
        Left = 0
        Top = 4
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
          10.583333333333330000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsRectangle
      end
      object QRShape3: TQRShape
        Left = 0
        Top = 22
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
          58.208333333333340000
          1846.791666666667000000)
        Brush.Color = clMedGray
        Pen.Color = clMedGray
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 5
        Top = 6
        Width = 117
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          15.875000000000000000
          309.562500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNIDADES DO N'#218'CLEO'
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
      object QRLabel9: TQRLabel
        Left = 5
        Top = 25
        Width = 61
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
          161.395833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SIGLA UNID'
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
      object QRLabel10: TQRLabel
        Left = 85
        Top = 25
        Width = 98
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
          259.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME DA UNIDADE'
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
      object QRLabel11: TQRLabel
        Left = 349
        Top = 25
        Width = 74
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          923.395833333333400000
          66.145833333333340000
          195.791666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TIPO UNIDADE'
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
      object QRLabel14: TQRLabel
        Left = 517
        Top = 25
        Width = 41
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1367.895833333333000000
          66.145833333333340000
          108.479166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'BAIRRO'
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
      Top = 241
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
      Top = 318
      Width = 698
      Height = 40
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AfterPrint = QRBand1AfterPrint
      AlignToBottom = False
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        105.833333333333300000
        1846.791666666667000000)
      BandType = rbGroupHeader
      object QRShape4: TQRShape
        Left = 0
        Top = 1
        Width = 698
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          52.916666666666660000
          0.000000000000000000
          2.645833333333333000
          1846.791666666667000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRShape7: TQRShape
        Left = 0
        Top = 20
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
          52.916666666666660000
          1846.791666666667000000)
        Brush.Color = clMedGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel45: TQRLabel
        Left = 5
        Top = 3
        Width = 142
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
          375.708333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'OCORR'#202'NCIAS DO N'#218'CLEO'
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
        Top = 22
        Width = 31
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
          82.020833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DATA'
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
      object QRLMat1: TQRLabel
        Left = 55
        Top = 22
        Width = 46
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          145.520833333333300000
          58.208333333333340000
          121.708333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UNIDADE'
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
      object QRLMat2: TQRLabel
        Left = 115
        Top = 22
        Width = 26
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          304.270833333333400000
          58.208333333333340000
          68.791666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TIPO'
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
      object QRLMat3: TQRLabel
        Left = 185
        Top = 22
        Width = 71
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          489.479166666666600000
          58.208333333333340000
          187.854166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'OCORR'#202'NCIA'
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
    object QRSubDetailFuncao: TQRSubDetail
      Left = 48
      Top = 358
      Width = 698
      Height = 17
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
        44.979166666666670000
        1846.791666666667000000)
      Master = QuickRep1
      DataSet = qryOcorrNuc
      FooterBand = GroupFooterBand3
      HeaderBand = QRBand1
      PrintBefore = False
      PrintIfEmpty = True
      object QRLDtOcorr: TQRLabel
        Left = 4
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
          10.583333333333330000
          2.645833333333333000
          113.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '99/99/99'
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
        Left = 55
        Top = 1
        Width = 23
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          145.520833333333300000
          2.645833333333333000
          60.854166666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = qryOcorrNuc
        DataField = 'sigla'
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
      object QRLTpOcorr: TQRLabel
        Left = 115
        Top = 1
        Width = 55
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          304.270833333333400000
          2.645833333333333000
          145.520833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Ocorr'#234'ncia'
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
      object QrlOcorr: TQRLabel
        Left = 185
        Top = 1
        Width = 504
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          489.479166666666600000
          2.645833333333333000
          1333.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
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
      Top = 308
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
      Top = 375
      Width = 698
      Height = 20
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
        52.916666666666660000
        1846.791666666667000000)
      BandType = rbGroupFooter
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
  object qryOcorrNuc: TADOQuery
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
    Left = 432
    Top = 104
  end
  object qryEquipNuc: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    Left = 259
    Top = 260
  end
  object qryUnidNucleo: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    Left = 539
    Top = 73
  end
end
