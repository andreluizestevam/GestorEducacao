inherited FrmRelRelacTarefAgendadas: TFrmRelRelacTarefAgendadas
  Left = 209
  Top = 150
  Caption = 'FrmRelRelacTarefAgendadas'
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
      Height = 135
      Frame.DrawBottom = False
      Size.Values = (
        357.187500000000000000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Width = 1027
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'RELA'#199#195'O DE TAREFAS AGENDADAS'
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
      Top = 262
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
    object DetailBand1: TQRBand
      Left = 48
      Top = 223
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
      object QRDBText9: TQRDBText
        Left = 815
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
          2156.354166666667000000
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
        Left = 965
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
          2553.229166666667000000
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
      object QRLTarefa: TQRLabel
        Left = 5
        Top = 1
        Width = 54
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          13.229166666666670000
          2.645833333333333000
          142.875000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QRLTarefa'
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
      object QRLDtCompr: TQRLabel
        Left = 367
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
          971.020833333333400000
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
      object QRLDtPrazo: TQRLabel
        Left = 431
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
          1140.354166666667000000
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
        Left = 497
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
          1314.979166666667000000
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
        Left = 775
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
          2050.520833333333000000
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
      object QRLEmissor: TQRLabel
        Left = 670
        Top = 1
        Width = 95
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1772.708333333334000000
          2.645833333333333000
          251.354166666666700000)
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
        Left = 565
        Top = 1
        Width = 95
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1494.895833333333000000
          2.645833333333333000
          251.354166666666700000)
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
    end
    object QRGroup1: TQRGroup
      Left = 48
      Top = 183
      Width = 1027
      Height = 40
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
        105.833333333333300000
        2717.270833333333000000)
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape1: TQRShape
        Left = 0
        Top = 24
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
          63.500000000000000000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QrlParametros: TQRLabel
        Left = 0
        Top = 0
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
          0.000000000000000000
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
      object QRLabel19: TQRLabel
        Left = 5
        Top = 25
        Width = 136
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
          359.833333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'C'#211'DIGO / T'#205'TULO TAREFA'
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
        Left = 565
        Top = 25
        Width = 76
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1494.895833333333000000
          66.145833333333340000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RESPON / UNID'
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
        Left = 965
        Top = 25
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2553.229166666667000000
          66.145833333333340000
          113.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'PRIORID'
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
        Left = 815
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
          2156.354166666667000000
          66.145833333333340000
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
      object QRLabel6: TQRLabel
        Left = 670
        Top = 25
        Width = 82
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1772.708333333334000000
          66.145833333333340000
          216.958333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'EMISSOR / UNID'
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
        Left = 367
        Top = 25
        Width = 58
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          971.020833333333400000
          66.145833333333340000
          153.458333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT COMPR'
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
        Left = 431
        Top = 25
        Width = 55
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1140.354166666667000000
          66.145833333333340000
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
      object QRLabel11: TQRLabel
        Left = 497
        Top = 25
        Width = 56
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1314.979166666667000000
          66.145833333333340000
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
      object QRLabel1: TQRLabel
        Left = 775
        Top = 25
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2050.520833333333000000
          66.145833333333340000
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
    end
    object QRBand1: TQRBand
      Left = 48
      Top = 240
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
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      
        'select tar.*, res.co_mat_col as responsavel, sol.co_mat_col as s' +
        'olicitante, emp.no_fantas_emp, sagend.DE_SITU_TAREF_AGEND,'
      
        'pagend.DE_PRIOR_TAREF_AGEND, emp.sigla as siglaRes, empSol.sigla' +
        ' as siglaSol'
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
      Size = 15
    end
    object QryRelatoriosolicitante: TStringField
      FieldName = 'solicitante'
      Size = 15
    end
    object QryRelatoriosiglaRes: TWideStringField
      FieldName = 'siglaRes'
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
