inherited FrmRelMapaLocalizacao: TFrmRelMapaLocalizacao
  Left = 209
  Top = 150
  Width = 813
  Height = 587
  HorzScrollBar.Position = 342
  Caption = 'Mapa por Localiza'#231#227'o'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = -334
    Top = 8
    Width = 1123
    Height = 794
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
      Height = 164
      Frame.DrawBottom = False
      Frame.Width = 0
      Size.Values = (
        433.916666666666700000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Top = 1027
        Height = 19
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          2717.270833333333000000
          1836.208333333333000000)
        Caption = 'RELA'#199#195'O DE PROFESSORES POR LOCALIZA'#199#195'O (BAIRRO)'
        Font.Height = -13
        FontSize = 10
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
        Left = 927
        Width = 32
        Size.Values = (
          44.979166666666670000
          2452.687500000000000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 927
        Width = 32
        Size.Values = (
          44.979166666666670000
          2452.687500000000000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 927
        Width = 32
        Size.Values = (
          44.979166666666670000
          2452.687500000000000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 973
        Size.Values = (
          44.979166666666670000
          2574.395833333333000000
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
        Left = 990
        Top = 25
        Width = 36
        Size.Values = (
          44.979166666666670000
          2619.375000000000000000
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
      object QRLabel10: TQRLabel
        Left = 3
        Top = 145
        Width = 48
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          383.645833333333400000
          127.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Unidade:'
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
      object QRLDescUnidade: TQRLabel
        Left = 55
        Top = 145
        Width = 402
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          145.520833333333300000
          383.645833333333400000
          1063.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLDescUnidade'
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
        Left = 1001
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
          2648.479166666667000000
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
      object QRLabel11: TQRLabel
        Left = 1
        Top = 109
        Width = 1024
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          288.395833333333400000
          2709.333333333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'RELA'#199#195'O DE PROFESSORES POR ESCOLA'
        Color = clWhite
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -15
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 11
      end
    end
    inherited QRBANDSGIE: TQRBand
      Top = 270
      Width = 1027
      Height = 18
      Size.Values = (
        47.625000000000000000
        2717.270833333333000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 732
        Width = 295
        Height = 17
        Size.Values = (
          44.979166666666670000
          1936.750000000000000000
          0.000000000000000000
          780.520833333333400000)
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
    object QRBand1: TQRBand
      Left = 48
      Top = 230
      Width = 1027
      Height = 17
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        44.979166666666670000
        2717.270833333333000000)
      BandType = rbDetail
      object QRDBText5: TQRDBText
        Left = 601
        Top = 1
        Width = 108
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1590.145833333333000000
          2.645833333333333000
          285.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'SITUACAO'
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
        Left = 5
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
          13.229166666666670000
          2.645833333333333000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_MAT_COL'
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
      object QRDBText1: TQRDBText
        Left = 711
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
          1881.187500000000000000
          2.645833333333333000
          58.208333333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NU_CARGA_HORARIA'
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
        Left = 512
        Top = 1
        Width = 84
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1354.666666666667000000
          2.645833333333333000
          222.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_TPCON'
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
      object qrlCidadeBairro: TQRLabel
        Left = 818
        Top = 1
        Width = 164
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2164.291666666667000000
          2.645833333333333000
          433.916666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Jo'#227'o Pessoa - Bairro dos Novais'
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
      object qrlContato: TQRLabel
        Left = 736
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
          1947.333333333334000000
          2.645833333333333000
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
      object QRDBText6: TQRDBText
        Left = 985
        Top = 1
        Width = 32
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
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'SIGLA'
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
      object QrlCurForm: TQRLabel
        Left = 385
        Top = 1
        Width = 122
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1018.645833333333000000
          2.645833333333333000
          322.791666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Curso de Forma'#231#227'o'
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
      object QrlEspec: TQRLabel
        Left = 279
        Top = 1
        Width = 101
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          738.187500000000000000
          2.645833333333333000
          267.229166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Especializa'#231#227'o'
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
        Left = 58
        Top = 1
        Width = 203
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          153.458333333333300000
          2.645833333333333000
          537.104166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Especializa'#231#227'o'
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
    object QRBand2: TQRBand
      Left = 48
      Top = 212
      Width = 1027
      Height = 18
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
        47.625000000000000000
        2717.270833333333000000)
      BandType = rbColumnHeader
      object QRShape3: TQRShape
        Left = 0
        Top = 0
        Width = 1027
        Height = 20
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Frame.Width = 0
        Size.Values = (
          52.916666666666660000
          0.000000000000000000
          0.000000000000000000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Pen.Width = 0
        Shape = qrsRectangle
      end
      object QRLabel1: TQRLabel
        Left = 58
        Top = 2
        Width = 116
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          153.458333333333300000
          5.291666666666667000
          306.916666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME DO PROFESSOR'
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
        Left = 512
        Top = 2
        Width = 78
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1354.666666666667000000
          5.291666666666667000
          206.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TP CONTRATO'
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
        Left = 5
        Top = 2
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          5.291666666666667000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATRIC'
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
        Left = 714
        Top = 2
        Width = 16
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
          42.333333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CH'
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
        Left = 736
        Top = 2
        Width = 54
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1947.333333333334000000
          5.291666666666667000
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
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel7: TQRLabel
        Left = 818
        Top = 2
        Width = 83
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2164.291666666667000000
          5.291666666666667000
          219.604166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CIDADE/BAIRRO'
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
      object QRLabel8: TQRLabel
        Left = 985
        Top = 2
        Width = 14
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
          37.041666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UE'
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
        Left = 601
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
          1590.145833333333000000
          5.291666666666667000
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
        Transparent = False
        WordWrap = True
        FontSize = 8
      end
      object QRLabel12: TQRLabel
        Left = 385
        Top = 2
        Width = 120
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1018.645833333333000000
          5.291666666666667000
          317.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'CURSO DE FORMA'#199#195'O'
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
        Left = 279
        Top = 2
        Width = 96
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          738.187500000000000000
          5.291666666666667000
          254.000000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'GRAU INSTRU'#199#195'O'
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
      Top = 247
      Width = 1027
      Height = 23
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = SummaryBand1BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        60.854166666666680000
        2717.270833333333000000)
      BandType = rbSummary
      object QRLabel14: TQRLabel
        Left = 856
        Top = 3
        Width = 103
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2264.833333333333000000
          7.937500000000000000
          272.520833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Qtde Professores:'
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
      object QRDBText9: TQRDBText
        Left = 963
        Top = 3
        Width = 62
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2547.937500000000000000
          7.937500000000000000
          164.041666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = qryQTDEPROF
        DataField = 'TOTALPROF'
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
        Shape = qrsHorLine
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      'SET LANGUAGE PORTUGUESE'
      
        'SELECT esp.tp_espec,c.co_curform,c.co_espec, E.SIGLA, C.CO_COL, ' +
        'C.CO_MAT_COL, C.NO_COL, b.NO_BAIRRO,'
      
        #9#9#9#9'ci.NO_CIDADE, C.CO_ESTA_ENDE_COL, C.NU_TELE_RESI_COL, C.NU_T' +
        'ELE_CELU_COL,'
      
        #9#9#9#9'C.CO_INST, C.NU_CARGA_HORARIA, TC.NO_TPCON, ESP.DE_ESPEC AS ' +
        'CURFORM,'
      #9#9#9#9'SITUACAO = (CASE C.CO_SITU_COL'
      #9#9#9#9#9#9#9'WHEN '#39'A'#39' THEN '#39'Ativo'#39
      #9#9#9#9#9#9#9'WHEN '#39'I'#39' THEN '#39'Inativo'#39
      #9#9#9#9#9#9#9'END),'
      '                ESPECIALIZA'#199#195'O = (CASE ESP.TP_ESPEC'
      #9#9#9#9#9#9#9#9#9'WHEN '#39'MB'#39' THEN '#39'MBA'#39
      
        '                                    WHEN '#39'PG'#39' THEN '#39'P'#243's-Gradua'#231#227 +
        'o'#39
      
        '                                    WHEN '#39'PD'#39' THEN '#39'P'#243's-Doutorad' +
        'o'#39
      '                                    WHEN '#39'SU'#39' THEN '#39'Superior'#39
      
        '                                    WHEN '#39'ES'#39' THEN '#39'Especializa'#231 +
        #227'o'#39
      '                                    WHEN '#39'ME'#39' THEN '#39'Mestrado'#39
      '                                    WHEN '#39'DO'#39' THEN '#39'Doutorado'#39
      #9#9#9#9#9#9#9#9'    WHEN '#39'TE'#39' THEN '#39'T'#233'cnico'#39
      '                                    WHEN '#39'OU'#39' THEN '#39'Outro'#39
      '                                  END)'
      'FROM TB03_COLABOR C '
      #9'LEFT JOIN TB20_TIPOCON TC ON TC.CO_TPCON = C.CO_TPCON'
      #9'LEFT JOIN TB25_EMPRESA E ON E.CO_EMP = C.CO_EMP'
      '    LEFT join tb904_cidade ci on c.co_cidade = ci.co_cidade'
      '    LEFT join tb905_bairro b on c.co_bairro = b.co_bairro '
      
        '    LEFT JOIN TB100_ESPECIALIZACAO ESP ON ESP.CO_ESPEC = C.CO_ES' +
        'PEC '
      'WHERE C.FLA_PROFESSOR = '#39'S'#39
      #9'AND C.CO_EMP = 2')
    object QryRelatorioCO_COL: TIntegerField
      FieldName = 'CO_COL'
    end
    object QryRelatorioCO_MAT_COL: TStringField
      FieldName = 'CO_MAT_COL'
      EditMask = '!99\.999\-9;0;_'
      Size = 15
    end
    object QryRelatorioNO_COL: TStringField
      FieldName = 'NO_COL'
      Size = 60
    end
    object QryRelatorioCO_ESTA_ENDE_COL: TStringField
      FieldName = 'CO_ESTA_ENDE_COL'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioNU_TELE_RESI_COL: TStringField
      FieldName = 'NU_TELE_RESI_COL'
      Size = 10
    end
    object QryRelatorioNU_TELE_CELU_COL: TStringField
      FieldName = 'NU_TELE_CELU_COL'
      Size = 10
    end
    object QryRelatorioCO_INST: TIntegerField
      FieldName = 'CO_INST'
    end
    object QryRelatorioNU_CARGA_HORARIA: TIntegerField
      FieldName = 'NU_CARGA_HORARIA'
    end
    object QryRelatorioSITUACAO: TStringField
      DisplayWidth = 100
      FieldName = 'SITUACAO'
      ReadOnly = True
      Size = 100
    end
    object QryRelatorioNO_TPCON: TStringField
      FieldName = 'NO_TPCON'
      Size = 40
    end
    object QryRelatorioSIGLA: TWideStringField
      FieldName = 'SIGLA'
      FixedChar = True
      Size = 5
    end
    object QryRelatorioNO_BAIRRO: TStringField
      FieldName = 'NO_BAIRRO'
      Size = 80
    end
    object QryRelatorioNO_CIDADE: TStringField
      FieldName = 'NO_CIDADE'
      Size = 80
    end
    object QryRelatorioCURFORM: TStringField
      FieldName = 'CURFORM'
      Size = 60
    end
    object QryRelatorioESPECIALIZAO: TStringField
      FieldName = 'ESPECIALIZA'#199#195'O'
      ReadOnly = True
      Size = 14
    end
    object QryRelatorioco_espec: TIntegerField
      FieldName = 'co_espec'
    end
    object QryRelatorioco_curform: TIntegerField
      FieldName = 'co_curform'
    end
    object QryRelatoriotp_espec: TStringField
      FieldName = 'tp_espec'
      FixedChar = True
      Size = 2
    end
  end
  object qryQTDEPROF: TADOQuery
    Connection = DataModuleSGE.Conn
    CursorType = ctStatic
    Parameters = <>
    Left = 292
    Top = 24
    object qryQTDEPROFTOTALPROF: TIntegerField
      FieldName = 'TOTALPROF'
      ReadOnly = True
    end
  end
end
