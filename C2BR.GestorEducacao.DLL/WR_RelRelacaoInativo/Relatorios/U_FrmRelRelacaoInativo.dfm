inherited FrmRelRelacaoInativo: TFrmRelRelacaoInativo
  Left = 138
  Top = 52
  Width = 1191
  Height = 681
  Caption = 'Rela'#231#227'o de Alunos Inativos'
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
      Height = 163
      Frame.DrawBottom = False
      Frame.Width = 0
      Size.Values = (
        431.270833333333400000
        2717.270833333333000000)
      object QRShape6: TQRShape [0]
        Left = 0
        Top = 146
        Width = 1027
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          0.000000000000000000
          386.291666666666700000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      inherited LblTituloRel: TQRLabel
        Top = 104
        Width = 1027
        Height = 22
        Size.Values = (
          58.208333333333340000
          2.645833333333333000
          275.166666666666700000
          2717.270833333333000000)
        Caption = 'RELA'#199#195'O DE ALUNOS - SITUA'#199#195'O DA MATR'#205'CULA'
        Font.Height = -15
        FontSize = 11
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
        Left = 595
        Width = 32
        Size.Values = (
          44.979166666666670000
          1574.270833333333000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 595
        Width = 32
        Size.Values = (
          44.979166666666670000
          1574.270833333333000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 595
        Width = 32
        Size.Values = (
          44.979166666666670000
          1574.270833333333000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 978
        Width = 21
        Size.Values = (
          44.979166666666670000
          2587.625000000000000000
          21.166666666666670000
          55.562500000000000000)
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
      object QRLabel7: TQRLabel
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
      object QRLParametros: TQRLabel
        Left = 0
        Top = 125
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
          330.729166666666700000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Ano de Refer'#234'ncia:'
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
      object QRLabel9: TQRLabel
        Left = 9
        Top = 149
        Width = 53
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          23.812500000000000000
          394.229166666666700000
          140.229166666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel1: TQRLabel
        Left = 80
        Top = 149
        Width = 30
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          211.666666666666700000
          394.229166666666700000
          79.375000000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' NIS'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel5: TQRLabel
        Left = 150
        Top = 149
        Width = 80
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          396.875000000000000000
          394.229166666666700000
          211.666666666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME DO ALUNO'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel4: TQRLabel
        Left = 347
        Top = 149
        Width = 13
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          918.104166666666800000
          394.229166666666700000
          34.395833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel11: TQRLabel
        Left = 369
        Top = 149
        Width = 11
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          976.312500000000000000
          394.229166666666700000
          29.104166666666670000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ID'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel12: TQRLabel
        Left = 386
        Top = 149
        Width = 61
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          1021.291666666667000000
          394.229166666666700000
          161.395833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DEFICI'#202'NCIA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QrlTitSerieTurma: TQRLabel
        Left = 455
        Top = 149
        Width = 69
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          1203.854166666667000000
          394.229166666666700000
          182.562500000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'S'#201'RIE / TURMA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel6: TQRLabel
        Left = 540
        Top = 149
        Width = 70
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          1428.750000000000000000
          394.229166666666700000
          185.208333333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'RESPONS'#193'VEL'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel13: TQRLabel
        Left = 732
        Top = 149
        Width = 50
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          1936.750000000000000000
          394.229166666666700000
          132.291666666666700000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TIPO.RESP'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel10: TQRLabel
        Left = 796
        Top = 149
        Width = 49
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          2106.083333333333000000
          394.229166666666700000
          129.645833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TEL. RESP'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel14: TQRLabel
        Left = 875
        Top = 149
        Width = 37
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          2315.104166666667000000
          394.229166666666700000
          97.895833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'UN.ENS'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLabel8: TQRLabel
        Left = 917
        Top = 149
        Width = 71
        Height = 12
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          31.750000000000000000
          2426.229166666667000000
          394.229166666666700000
          187.854166666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SITUA'#199#195'O/DATA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
    end
    inherited QRBANDSGIE: TQRBand
      Top = 251
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
    object QRGroup3: TQRGroup
      Left = 48
      Top = 211
      Width = 1027
      Height = 17
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = QRGroup3BeforePrint
      Color = clWhite
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        44.979166666666670000
        2717.270833333333000000)
      Expression = 'QryRelatorio.CO_ALU'
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRDBText2: TQRDBText
        Left = 345
        Top = 3
        Width = 18
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          912.812500000000100000
          7.937500000000000000
          47.625000000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_SEXO_ALU'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRDBText4: TQRDBText
        Left = 540
        Top = 3
        Width = 186
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          1428.750000000000000000
          7.937500000000000000
          492.124999999999900000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_RESP'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRExpr1: TQRExpr
        Left = 796
        Top = 3
        Width = 76
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          2106.083333333333000000
          7.937500000000000000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        Color = clWhite
        ParentFont = False
        ResetAfterPrint = False
        Transparent = True
        WordWrap = True
        Expression = 
          'IF(QryRelatorio.NU_TELE_RESP<>'#39#39','#39'('#39'+COPY(QryRelatorio.NU_TELE_R' +
          'ESP,1,2)+'#39')'#39'+'#39' '#39'+COPY(QryRelatorio.NU_TELE_RESP,3,4)+'#39'-'#39'+COPY(Qr' +
          'yRelatorio.NU_TELE_RESP,7,4),'#39#39')'
        FontSize = 7
      end
      object QRLData: TQRLabel
        Left = 917
        Top = 3
        Width = 109
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          2426.229166666667000000
          7.937500000000000000
          288.395833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'Finalizada / 00/00/00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLMatricula: TQRLabel
        Left = 3
        Top = 3
        Width = 70
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          7.937500000000000000
          7.937500000000000000
          185.208333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000.0000.000'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QrlSerieTurma: TQRLabel
        Left = 455
        Top = 2
        Width = 72
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          1203.854166666667000000
          5.291666666666667000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '1'#186' Ano / Turma A'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRDBText3: TQRDBText
        Left = 386
        Top = 2
        Width = 62
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          1021.291666666667000000
          5.291666666666667000
          164.041666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DEFICIENCIA'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QrlIdade: TQRLabel
        Left = 369
        Top = 3
        Width = 11
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          976.312500000000000000
          7.937500000000000000
          29.104166666666670000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRDBText5: TQRDBText
        Left = 875
        Top = 3
        Width = 28
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          2315.104166666667000000
          7.937500000000000000
          74.083333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'SIGLA'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRDBText6: TQRDBText
        Left = 732
        Top = 3
        Width = 60
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          1936.750000000000000000
          7.937500000000000000
          158.750000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'PARENTESCO'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLNuNis: TQRLabel
        Left = 80
        Top = 3
        Width = 37
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          211.666666666666700000
          7.937500000000000000
          97.895833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '99.999-9'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
      object QRLNoAlu: TQRLabel
        Left = 150
        Top = 3
        Width = 185
        Height = 13
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          34.395833333333340000
          396.875000000000000000
          7.937500000000000000
          489.479166666666600000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99.999-9'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        FontSize = 7
      end
    end
    object SummaryBand1: TQRBand
      Left = 48
      Top = 228
      Width = 1027
      Height = 23
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
        60.854166666666680000
        2717.270833333333000000)
      BandType = rbSummary
      object QrlTotal: TQRLabel
        Left = 727
        Top = 3
        Width = 26
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1923.520833333334000000
          7.937500000000000000
          68.791666666666680000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00000'
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
      object QrlTotalMatriculas: TQRLabel
        Left = 696
        Top = 3
        Width = 26
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1841.500000000000000000
          7.937500000000000000
          68.791666666666680000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00000'
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
      object QrlTotalCompleto: TQRLabel
        Left = 800
        Top = 3
        Width = 194
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2116.666666666667000000
          7.937500000000000000
          513.291666666666700000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Total de Matriculas: 000 - Total de Alunos: 000'
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
    end
  end
  object QRLabel16: TQRLabel [1]
    Left = 839
    Top = 4
    Width = 90
    Height = 15
    Frame.Color = clBlack
    Frame.DrawTop = False
    Frame.DrawBottom = False
    Frame.DrawLeft = False
    Frame.DrawRight = False
    Size.Values = (
      39.687500000000000000
      2219.854166666667000000
      10.583333333333330000
      238.125000000000000000)
    Alignment = taLeftJustify
    AlignToBand = False
    AutoSize = True
    AutoStretch = False
    Caption = 'Total de Alunos:'
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
  object QRLabel17: TQRLabel [2]
    Left = 932
    Top = 4
    Width = 31
    Height = 15
    Frame.Color = clBlack
    Frame.DrawTop = False
    Frame.DrawBottom = False
    Frame.DrawLeft = False
    Frame.DrawRight = False
    Size.Values = (
      39.687500000000000000
      2465.916666666667000000
      10.583333333333330000
      82.020833333333340000)
    Alignment = taRightJustify
    AlignToBand = False
    AutoSize = True
    AutoStretch = False
    Caption = '00000'
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
  inherited QryRelatorio: TADOQuery
    CursorType = ctStatic
    Prepared = True
    SQL.Strings = (
      
        'SELECT Distinct RESP.DE_GRAU_PAREN, E.SIGLA, A.CO_ALU, A.NU_NIS,' +
        ' A.DT_NASC_ALU, A.TP_DEF,'
      '       A.NO_ALU,'
      '       MM.CO_ALU_CAD,      '
      '       A.DE_ENDE_ALU,'
      '       A.NU_ENDE_ALU,     '
      '       A.DE_COMP_ALU,     '
      '       A.CO_BAIRRO,'
      '       A.CO_CIDADE,'
      '       A.CO_ESTA_ALU,     '
      '       A.CO_CEP_ALU,'
      '       A.CO_SEXO_ALU,'
      #9#9'('
      #9#9' SELECT COUNT(CO_ALU) FROM TB08_MATRCUR'
      #9#9' WHERE CO_SIT_MAT = '#39'A'#39
      #9#9') MATRICULAS,'
      '       ('
      '         Select RESP.NO_RESP'
      '         From TB108_RESPONSAVEL RESP'
      '         Where RESP.CO_RESP = A.CO_RESP'
      '       ) NO_RESP,'
      '       ('
      
        '         Select Coalesce(RESP.NU_TELE_RESI_RESP, Coalesce(RESP.N' +
        'U_TELE_CELU_RESP, '#39#39'))'
      '         From TB108_RESPONSAVEL RESP'
      '         Where RESP.CO_RESP = A.CO_RESP'
      '       ) NU_TELE_RESP,'
      '       TU.NO_TURMA [NO_TUR],'
      '       C.NO_CUR, C.CO_SIGL_CUR,                  '
      '       G.CO_CUR,          '
      '       G.CO_TUR,          '
      '       G.NU_SEM_LET,            '
      '       G.CO_ANO_MES_MAT,        '
      '       MM.DT_SIT_MAT,  '
      '       MO.DE_MODU_CUR, CD.NO_CIDADE, BB.NO_BAIRRO,  '
      
        '       SITUACAO = (CASE MM.CO_SIT_MAT                           ' +
        '        '
      '                      WHEN '#39'C'#39' THEN '#39'Cancelada'#39
      '                      WHEN '#39'T'#39' THEN '#39'Trancada'#39
      '                      WHEN '#39'F'#39' THEN '#39'Finalizada'#39
      '                      WHEN '#39'R'#39' THEN '#39'Transferido'#39
      '                      WHEN '#39'D'#39' THEN '#39'Desistente'#39
      '                      ELSE '#39#39
      '                   END),'
      '       DEFICIENCIA = (CASE A.TP_DEF'
      '                      WHEN '#39'N'#39' THEN '#39'Nenhuma'#39
      '                      WHEN '#39'A'#39' THEN '#39'Auditiva'#39
      '                      WHEN '#39'V'#39' THEN '#39'Visual'#39
      '                      WHEN '#39'F'#39' THEN '#39'F'#237'sica'#39
      '                      WHEN '#39'M'#39' THEN '#39'Mental'#39
      '                      WHEN '#39'I'#39' THEN '#39'M'#250'ltiplas'#39
      '                      WHEN '#39'O'#39' THEN '#39'Outra'#39
      '                      ELSE '#39#39
      '                   END),'
      '       PARENTESCO = (CASE RESP.DE_GRAU_PAREN'
      '                      WHEN '#39'PM'#39' THEN '#39'Pai/M'#227'e'#39
      '                      WHEN '#39'AV'#39' THEN '#39'Av'#244'/Av'#243#39
      '                      WHEN '#39'IR'#39' THEN '#39'Irm'#227'o/Irm'#227#39
      '                      WHEN '#39'TI'#39' THEN '#39'Tio(a)'#39
      '                      WHEN '#39'PR'#39' THEN '#39'Primo(a)'#39
      '                      WHEN '#39'CN'#39' THEN '#39'Cunhado(a)'#39
      '                      WHEN '#39'TU'#39' THEN '#39'Tutor(a)'#39
      '                      WHEN '#39'OU'#39' THEN '#39'Outros'#39
      '                      ELSE '#39#39
      '                   END)'
      ''
      'FROM TB08_MATRCUR MM, TB25_EMPRESA E, TB108_RESPONSAVEL RESP,'
      '     TB48_GRADE_ALUNO G,        '
      '     TB06_TURMAS T, '
      '     TB129_CADTURMAS TU,            '
      '     TB07_ALUNO A,              '
      '     TB01_CURSO C,              '
      '     TB44_MODULO MO, TB904_CIDADE CD, TB905_BAIRRO BB '
      ''
      'WHERE G.CO_EMP = A.CO_EMP       '
      ' AND  RESP.CO_RESP = A.CO_RESP'
      ' AND  E.CO_EMP = A.CO_EMP'
      ' AND  G.CO_ALU = A.CO_ALU       '
      ' AND  G.CO_EMP = MM.CO_EMP       '
      ' AND  G.CO_CUR = MM.CO_CUR       '
      ' AND  G.CO_ALU = MM.CO_ALU       '
      ' AND  G.CO_ALU = A.CO_ALU       '
      ' AND  G.CO_EMP = T.CO_EMP       '
      ' AND  G.CO_TUR = T.CO_TUR'
      ' AND  G.CO_TUR = TU.CO_TUR       '
      ' AND  G.CO_EMP = C.CO_EMP       '
      ' AND  G.CO_CUR = C.CO_CUR       '
      ' AND  G.CO_MODU_CUR = MO.CO_MODU_CUR'
      ' AND A.CO_CIDADE *= CD.CO_CIDADE'
      ' AND     A.CO_CIDADE *= BB.CO_CIDADE'
      ' AND     A.CO_BAIRRO *= BB.CO_BAIRRO'
      ''
      'order by  C.no_cur, TU.no_turMA,A.NO_ALU')
    object QryRelatorioCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
    object QryRelatorioNO_ALU: TStringField
      FieldName = 'NO_ALU'
      Size = 60
    end
    object QryRelatorioCO_ALU_CAD: TStringField
      FieldName = 'CO_ALU_CAD'
      Size = 15
    end
    object QryRelatorioDE_ENDE_ALU: TStringField
      FieldName = 'DE_ENDE_ALU'
      Size = 40
    end
    object QryRelatorioNU_ENDE_ALU: TIntegerField
      FieldName = 'NU_ENDE_ALU'
    end
    object QryRelatorioDE_COMP_ALU: TStringField
      FieldName = 'DE_COMP_ALU'
      Size = 15
    end
    object QryRelatorioCO_ESTA_ALU: TStringField
      FieldName = 'CO_ESTA_ALU'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioCO_CEP_ALU: TStringField
      FieldName = 'CO_CEP_ALU'
      Size = 12
    end
    object QryRelatorioNO_TUR: TStringField
      FieldName = 'NO_TUR'
      Size = 40
    end
    object QryRelatorioNO_CUR: TStringField
      FieldName = 'NO_CUR'
      Size = 100
    end
    object QryRelatorioCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryRelatorioCO_TUR: TIntegerField
      FieldName = 'CO_TUR'
    end
    object QryRelatorioNU_SEM_LET: TStringField
      FieldName = 'NU_SEM_LET'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_ANO_MES_MAT: TStringField
      FieldName = 'CO_ANO_MES_MAT'
      FixedChar = True
      Size = 6
    end
    object QryRelatorioDE_MODU_CUR: TStringField
      FieldName = 'DE_MODU_CUR'
      Size = 60
    end
    object QryRelatorioSITUACAO: TStringField
      FieldName = 'SITUACAO'
      ReadOnly = True
      Size = 11
    end
    object QryRelatorioCO_BAIRRO: TIntegerField
      FieldName = 'CO_BAIRRO'
    end
    object QryRelatorioCO_CIDADE: TIntegerField
      FieldName = 'CO_CIDADE'
    end
    object QryRelatorioNO_CIDADE: TStringField
      FieldName = 'NO_CIDADE'
      Size = 80
    end
    object QryRelatorioNO_BAIRRO: TStringField
      FieldName = 'NO_BAIRRO'
      Size = 80
    end
    object QryRelatorioCO_SEXO_ALU: TStringField
      FieldName = 'CO_SEXO_ALU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioNO_RESP: TStringField
      FieldName = 'NO_RESP'
      ReadOnly = True
      Size = 60
    end
    object QryRelatorioNU_TELE_RESP: TStringField
      FieldName = 'NU_TELE_RESP'
      ReadOnly = True
      Size = 10
    end
    object QryRelatorioDT_NASC_ALU: TDateTimeField
      FieldName = 'DT_NASC_ALU'
    end
    object QryRelatorioTP_DEF: TStringField
      FieldName = 'TP_DEF'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDEFICIENCIA: TStringField
      FieldName = 'DEFICIENCIA'
      ReadOnly = True
      Size = 9
    end
    object QryRelatorioSIGLA: TWideStringField
      FieldName = 'SIGLA'
      FixedChar = True
      Size = 5
    end
    object QryRelatorioDE_GRAU_PAREN: TStringField
      FieldName = 'DE_GRAU_PAREN'
      Size = 30
    end
    object QryRelatorioPARENTESCO: TStringField
      FieldName = 'PARENTESCO'
      ReadOnly = True
      Size = 10
    end
    object QryRelatorioDT_SIT_MAT: TDateTimeField
      FieldName = 'DT_SIT_MAT'
    end
    object QryRelatorioMATRICULAS: TIntegerField
      FieldName = 'MATRICULAS'
      ReadOnly = True
    end
    object QryRelatorioCO_SIGL_CUR: TStringField
      FieldName = 'CO_SIGL_CUR'
      Size = 8
    end
    object QryRelatorioNU_NIS: TBCDField
      FieldName = 'NU_NIS'
      Precision = 11
      Size = 0
    end
  end
end
