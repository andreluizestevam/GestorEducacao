inherited FrmRelFreqAluno: TFrmRelFreqAluno
  Left = 210
  Top = 107
  Width = 942
  Height = 663
  Caption = 'FrmRelFreqAluno'
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
      Height = 257
      Frame.DrawBottom = False
      Size.Values = (
        679.979166666666700000
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
      object QRShape1: TQRShape [1]
        Left = 0
        Top = 221
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
          584.729166666666800000
          2717.270833333333000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
        VertAdjust = 0
      end
      inherited LblTituloRel: TQRLabel [2]
        Width = 1027
        Height = 19
        Size.Values = (
          50.270833333333330000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'FICHA DE FREQ'#220#202'NCIA MENSAL DE ALUNOS POR TURMA'
        Font.Height = -15
        FontSize = 11
      end
      inherited QRDBText14: TQRDBText [3]
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          5.291666666666667000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBText15: TQRDBText [4]
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          47.625000000000000000
          1256.770833333333000000)
        FontSize = 10
      end
      inherited QRDBImage1: TQRDBImage [5]
        Size.Values = (
          198.437500000000000000
          10.583333333333300000
          10.583333333333300000
          246.062500000000000000)
      end
      inherited qrlTemplePag: TQRLabel [6]
        Left = 932
        Size.Values = (
          44.979166666666670000
          2465.916666666667000000
          21.166666666666670000
          66.145833333333340000)
        FontSize = 8
      end
      inherited qrlTempleData: TQRLabel [7]
        Left = 932
        Size.Values = (
          44.979166666666670000
          2465.916666666667000000
          66.145833333333340000
          68.791666666666680000)
        FontSize = 8
      end
      inherited qrlTempleHora: TQRLabel [8]
        Left = 932
        Size.Values = (
          44.979166666666670000
          2465.916666666667000000
          111.125000000000000000
          71.437500000000000000)
        FontSize = 8
      end
      inherited QRSysData1: TQRSysData [9]
        Left = 977
        Size.Values = (
          44.979166666666670000
          2584.979166666667000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        FontSize = 8
      end
      inherited QRSysData2: TQRSysData [10]
        Left = 996
        Size.Values = (
          44.979166666666670000
          2635.250000000000000000
          111.125000000000000000
          82.020833333333330000)
        FontSize = 8
      end
      inherited qrlEnde: TQRLabel
        Size.Values = (
          44.979166666666670000
          285.750000000000000000
          89.958333333333330000
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
      object QrlTPunidade: TQRLabel
        Left = 0
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
          0.000000000000000000
          367.770833333333400000
          2717.270833333333000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '(Tipo de Unidade: Escola Municipal)'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlCurso: TQRLabel
        Left = 8
        Top = 166
        Width = 29
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          21.166666666666670000
          439.208333333333300000
          76.729166666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
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
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel3: TQRLabel
        Left = 8
        Top = 182
        Width = 34
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          21.166666666666670000
          481.541666666666700000
          89.958333333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Turma:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel4: TQRLabel
        Left = 8
        Top = 198
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          21.166666666666670000
          523.875000000000000000
          84.666666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Turno:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel1: TQRLabel
        Left = 296
        Top = 166
        Width = 50
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          783.166666666666700000
          439.208333333333300000
          132.291666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Diretor(a):'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel2: TQRLabel
        Left = 296
        Top = 182
        Width = 61
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          783.166666666666700000
          481.541666666666700000
          161.395833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Educadora I:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel5: TQRLabel
        Left = 296
        Top = 198
        Width = 63
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          783.166666666666700000
          523.875000000000000000
          166.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Educadora II:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel6: TQRLabel
        Left = 879
        Top = 166
        Width = 52
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2325.687500000000000000
          439.208333333333400000
          137.583333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Ano Base:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel7: TQRLabel
        Left = 879
        Top = 182
        Width = 80
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2325.687500000000000000
          481.541666666666700000
          211.666666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'M'#234's Refer'#234'ncia:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel8: TQRLabel
        Left = 879
        Top = 198
        Width = 52
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2325.687500000000000000
          523.875000000000000000
          137.583333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Dias '#218'teis:'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape2: TQRShape
        Left = 0
        Top = 239
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
          632.354166666666800000
          2717.270833333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
        VertAdjust = 0
      end
      object QRLabel9: TQRLabel
        Left = 5
        Top = 223
        Width = 139
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          13.229166666666670000
          590.020833333333300000
          367.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'REGISTRO DE FREQU'#202'NCIA'
        Color = clGray
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWhite
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel10: TQRLabel
        Left = 3
        Top = 240
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          635.000000000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NR'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel11: TQRLabel
        Left = 23
        Top = 240
        Width = 38
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          60.854166666666670000
          635.000000000000000000
          100.541666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'ALUNO'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel12: TQRLabel
        Left = 253
        Top = 240
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          669.395833333333300000
          635.000000000000000000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel14: TQRLabel
        Left = 275
        Top = 240
        Width = 66
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          727.604166666666700000
          635.000000000000000000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel18: TQRLabel
        Left = 1007
        Top = 240
        Width = 16
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2664.354166666667000000
          635.000000000000000000
          42.333333333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '%F'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel19: TQRLabel
        Left = 983
        Top = 240
        Width = 14
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2600.854166666667000000
          635.000000000000000000
          37.041666666666670000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'TF'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel20: TQRLabel
        Left = 961
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2542.645833333333000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '31'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape3: TQRShape
        Left = 1002
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2651.125000000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape4: TQRShape
        Left = 977
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2584.979166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape5: TQRShape
        Left = 937
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2479.145833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape6: TQRShape
        Left = 957
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2532.062500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel21: TQRLabel
        Left = 941
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2489.729166666667000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '30'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel22: TQRLabel
        Left = 921
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2436.812500000000000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '29'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape7: TQRShape
        Left = 917
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2426.229166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel23: TQRLabel
        Left = 901
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2383.895833333333000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '28'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape8: TQRShape
        Left = 897
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2373.312500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape9: TQRShape
        Left = 817
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2161.645833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel24: TQRLabel
        Left = 821
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2172.229166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '24'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape10: TQRShape
        Left = 837
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2214.562500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel25: TQRLabel
        Left = 841
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2225.145833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '25'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape11: TQRShape
        Left = 857
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2267.479166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel26: TQRLabel
        Left = 861
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2278.062500000000000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '26'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape12: TQRShape
        Left = 877
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2320.395833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel27: TQRLabel
        Left = 881
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2330.979166666667000000
          635.000000000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '27'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape13: TQRShape
        Left = 737
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1949.979166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel28: TQRLabel
        Left = 741
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1960.562500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '20'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape14: TQRShape
        Left = 757
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2002.895833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel29: TQRLabel
        Left = 761
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2013.479166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '21'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape15: TQRShape
        Left = 777
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2055.812500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel30: TQRLabel
        Left = 781
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2066.395833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '22'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape16: TQRShape
        Left = 797
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          2108.729166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel31: TQRLabel
        Left = 801
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2119.312500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '23'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape17: TQRShape
        Left = 657
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1738.312500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel32: TQRLabel
        Left = 661
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1748.895833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '16'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape18: TQRShape
        Left = 677
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1791.229166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel33: TQRLabel
        Left = 681
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1801.812500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '17'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape19: TQRShape
        Left = 697
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1844.145833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel34: TQRLabel
        Left = 701
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1854.729166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '18'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape20: TQRShape
        Left = 717
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1897.062500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel35: TQRLabel
        Left = 721
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1907.645833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '19'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape21: TQRShape
        Left = 577
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1526.645833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel36: TQRLabel
        Left = 581
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1537.229166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '12'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape22: TQRShape
        Left = 597
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1579.562500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel37: TQRLabel
        Left = 601
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1590.145833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '13'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape23: TQRShape
        Left = 617
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1632.479166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel38: TQRLabel
        Left = 621
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1643.062500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '14'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape24: TQRShape
        Left = 637
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1685.395833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel39: TQRLabel
        Left = 641
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1695.979166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '15'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape25: TQRShape
        Left = 497
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1314.979166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel40: TQRLabel
        Left = 501
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1325.562500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '08'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape26: TQRShape
        Left = 517
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1367.895833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel41: TQRLabel
        Left = 521
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1378.479166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '09'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape27: TQRShape
        Left = 537
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1420.812500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel42: TQRLabel
        Left = 541
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1431.395833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '10'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape28: TQRShape
        Left = 557
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1473.729166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel43: TQRLabel
        Left = 561
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1484.312500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '11'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel15: TQRLabel
        Left = 361
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          955.145833333333300000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '01'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape29: TQRShape
        Left = 377
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          997.479166666666900000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel16: TQRLabel
        Left = 381
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1008.062500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '02'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape30: TQRShape
        Left = 397
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1050.395833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel17: TQRLabel
        Left = 401
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1060.979166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '03'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape31: TQRShape
        Left = 417
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1103.312500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel44: TQRLabel
        Left = 421
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1113.895833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '04'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape32: TQRShape
        Left = 437
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1156.229166666667000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel45: TQRLabel
        Left = 441
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1166.812500000000000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '05'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape33: TQRShape
        Left = 457
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1209.145833333333000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel46: TQRLabel
        Left = 461
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1219.729166666667000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '06'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape34: TQRShape
        Left = 477
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          1262.062500000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRLabel47: TQRLabel
        Left = 481
        Top = 240
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1272.645833333333000000
          635.000000000000000000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '07'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape35: TQRShape
        Left = 358
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          947.208333333333400000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clSilver
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape36: TQRShape
        Left = 270
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          714.375000000000000000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape37: TQRShape
        Left = 20
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          52.916666666666660000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape39: TQRShape
        Left = 250
        Top = 239
        Width = 1
        Height = 19
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          50.270833333333330000
          661.458333333333400000
          632.354166666666800000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRDBText1: TQRDBText
        Left = 45
        Top = 166
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          119.062500000000000000
          439.208333333333300000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_CUR'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRDBText2: TQRDBText
        Left = 45
        Top = 182
        Width = 43
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          119.062500000000000000
          481.541666666666700000
          113.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_TUR'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlTurno: TQRLabel
        Left = 45
        Top = 198
        Width = 37
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          119.062500000000000000
          523.875000000000000000
          97.895833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Manh'#227
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel50: TQRLabel
        Left = 1003
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
          2653.770833333333000000
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
        ExportAs = exptText
        FontSize = 8
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
        ExportAs = exptText
        FontSize = 8
      end
      object QrlAnoBase: TQRLabel
        Left = 968
        Top = 166
        Width = 25
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2561.166666666667000000
          439.208333333333400000
          66.145833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '2008'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlMesRef: TQRLabel
        Left = 968
        Top = 182
        Width = 42
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2561.166666666667000000
          481.541666666666700000
          111.125000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MAR'#199'O'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlDiasUteis: TQRLabel
        Left = 968
        Top = 198
        Width = 19
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2561.166666666667000000
          523.875000000000000000
          50.270833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '999'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlEduc01: TQRLabel
        Left = 363
        Top = 182
        Width = 33
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          960.437500000000000000
          481.541666666666700000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '- - - - -'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlEduc02: TQRLabel
        Left = 363
        Top = 198
        Width = 33
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          960.437500000000000000
          523.875000000000000000
          87.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '- - - - -'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlMes: TQRLabel
        Left = 861
        Top = 183
        Width = 13
        Height = 15
        Enabled = False
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2278.062500000000000000
          484.187500000000000000
          34.395833333333340000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '99'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRDBText3: TQRDBText
        Left = 363
        Top = 166
        Width = 45
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          960.437500000000000000
          439.208333333333300000
          119.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'NO_COL'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
    end
    inherited QRBANDSGIE: TQRBand
      Top = 354
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
      Top = 305
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
      TransparentBand = False
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        44.979166666666670000
        2717.270833333333000000)
      PreCaluculateBandHeight = False
      KeepOnOnePage = False
      BandType = rbDetail
      object QrlMatricula: TQRLabel
        Left = 273
        Top = 1
        Width = 82
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          722.312500000000000000
          2.645833333333333000
          216.958333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00.000.0000.###'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRDBText5: TQRDBText
        Left = 251
        Top = 1
        Width = 17
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          664.104166666666800000
          2.645833333333333000
          44.979166666666670000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'CO_SEXO_ALU'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP01: TQRLabel
        Left = 364
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          963.083333333333300000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape41: TQRShape
        Left = 358
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
          947.208333333333400000
          0.000000000000000000
          2.645833333333333000)
        Brush.Color = clSilver
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape42: TQRShape
        Left = 377
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
          997.479166666666900000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QrlP02: TQRLabel
        Left = 384
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1016.000000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape43: TQRShape
        Left = 417
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
          1103.312500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape44: TQRShape
        Left = 397
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
          1050.395833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape45: TQRShape
        Left = 457
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
          1209.145833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape46: TQRShape
        Left = 437
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
          1156.229166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QrlP03: TQRLabel
        Left = 404
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1068.916666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape47: TQRShape
        Left = 477
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
          1262.062500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QrlP06: TQRLabel
        Left = 464
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1227.666666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP05: TQRLabel
        Left = 444
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1174.750000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP04: TQRLabel
        Left = 424
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1121.833333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape48: TQRShape
        Left = 597
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
          1579.562500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape49: TQRShape
        Left = 577
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
          1526.645833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape50: TQRShape
        Left = 557
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
          1473.729166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape51: TQRShape
        Left = 537
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
          1420.812500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape52: TQRShape
        Left = 517
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
          1367.895833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape53: TQRShape
        Left = 497
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
          1314.979166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape54: TQRShape
        Left = 717
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
          1897.062500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape55: TQRShape
        Left = 697
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
          1844.145833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape56: TQRShape
        Left = 677
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
          1791.229166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape57: TQRShape
        Left = 657
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
          1738.312500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape58: TQRShape
        Left = 637
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
          1685.395833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape59: TQRShape
        Left = 617
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
          1632.479166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape60: TQRShape
        Left = 837
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
          2214.562500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape61: TQRShape
        Left = 817
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
          2161.645833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape62: TQRShape
        Left = 797
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
          2108.729166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape63: TQRShape
        Left = 777
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
          2055.812500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape64: TQRShape
        Left = 757
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
          2002.895833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape65: TQRShape
        Left = 737
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
          1949.979166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape66: TQRShape
        Left = 957
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
          2532.062500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape67: TQRShape
        Left = 937
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
          2479.145833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape68: TQRShape
        Left = 917
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
          2426.229166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape69: TQRShape
        Left = 897
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
          2373.312500000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape70: TQRShape
        Left = 877
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
          2320.395833333333000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape71: TQRShape
        Left = 857
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
          2267.479166666667000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape72: TQRShape
        Left = 977
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
          2584.979166666667000000
          0.000000000000000000
          2.645833333333333000)
        Brush.Color = clSilver
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape73: TQRShape
        Left = 1002
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
          2651.125000000000000000
          0.000000000000000000
          2.645833333333333000)
        Pen.Color = clSilver
        Pen.Style = psDot
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QrlP07: TQRLabel
        Left = 484
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1280.583333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP08: TQRLabel
        Left = 504
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1333.500000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP09: TQRLabel
        Left = 524
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1386.416666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP10: TQRLabel
        Left = 544
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1439.333333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP11: TQRLabel
        Left = 564
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1492.250000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP12: TQRLabel
        Left = 584
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1545.166666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP24: TQRLabel
        Left = 824
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2180.166666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP23: TQRLabel
        Left = 804
        Top = 1
        Width = 7
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
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP22: TQRLabel
        Left = 784
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2074.333333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP21: TQRLabel
        Left = 764
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2021.416666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP20: TQRLabel
        Left = 744
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1968.500000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP19: TQRLabel
        Left = 724
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1915.583333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP18: TQRLabel
        Left = 704
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1862.666666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP17: TQRLabel
        Left = 684
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1809.750000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP16: TQRLabel
        Left = 664
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1756.833333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP15: TQRLabel
        Left = 644
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1703.916666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP14: TQRLabel
        Left = 624
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1651.000000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP13: TQRLabel
        Left = 604
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1598.083333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP31: TQRLabel
        Left = 964
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2550.583333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP30: TQRLabel
        Left = 944
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2497.666666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP29: TQRLabel
        Left = 924
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2444.750000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP28: TQRLabel
        Left = 904
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2391.833333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP27: TQRLabel
        Left = 884
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2338.916666666667000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP26: TQRLabel
        Left = 864
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2286.000000000000000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlP25: TQRLabel
        Left = 844
        Top = 1
        Width = 7
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2233.083333333333000000
          2.645833333333333000
          18.520833333333330000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'P'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRShape74: TQRShape
        Left = 270
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
          714.375000000000000000
          0.000000000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape76: TQRShape
        Left = 250
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
          661.458333333333400000
          0.000000000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QRShape77: TQRShape
        Left = 20
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
          52.916666666666660000
          0.000000000000000000
          2.645833333333333000)
        Brush.Color = clGray
        Pen.Color = clGray
        Shape = qrsVertLine
        VertAdjust = 0
      end
      object QrlTotalFaltas: TQRLabel
        Left = 984
        Top = 1
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2603.500000000000000000
          2.645833333333333000
          34.395833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlTFPorcent: TQRLabel
        Left = 1008
        Top = 1
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2667.000000000000000000
          2.645833333333333000
          34.395833333333340000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlNumAluno: TQRLabel
        Left = 4
        Top = 1
        Width = 13
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
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLNoAlu: TQRLabel
        Left = 23
        Top = 1
        Width = 225
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          60.854166666666680000
          2.645833333333333000
          595.312500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = True
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
    end
    object SummaryBand1: TQRBand
      Left = 48
      Top = 322
      Width = 1027
      Height = 32
      Frame.Color = clBlack
      Frame.DrawTop = False
      Frame.DrawBottom = False
      Frame.DrawLeft = False
      Frame.DrawRight = False
      AlignToBottom = False
      BeforePrint = SummaryBand1BeforePrint
      Color = clWhite
      TransparentBand = False
      ForceNewColumn = False
      ForceNewPage = False
      Size.Values = (
        84.666666666666670000
        2717.270833333333000000)
      PreCaluculateBandHeight = False
      KeepOnOnePage = False
      BandType = rbSummary
      object QRShape40: TQRShape
        Left = 0
        Top = 0
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
          0.000000000000000000
          2717.270833333333000000)
        Brush.Color = clSilver
        Pen.Style = psClear
        Shape = qrsRectangle
        VertAdjust = 0
      end
      object QrlTotAluno: TQRLabel
        Left = 79
        Top = 2
        Width = 13
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          209.020833333333300000
          5.291666666666667000
          34.395833333333330000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '00'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlTotFaltTurmaPorcent: TQRLabel
        Left = 1006
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
          2661.708333333333000000
          5.291666666666667000
          42.333333333333340000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '0%'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlQTsexo: TQRLabel
        Left = 107
        Top = 2
        Width = 174
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          283.104166666666700000
          5.291666666666667000
          460.375000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '(Masculino: 00   -   Feminino: 00)'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QRLabel48: TQRLabel
        Left = 3
        Top = 2
        Width = 72
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          7.937500000000000000
          5.291666666666667000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'QTDE ALUNO:'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
      object QrlTotFaltTurma: TQRLabel
        Left = 849
        Top = 2
        Width = 123
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
          325.437500000000000000)
        Alignment = taRightJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'FALTAS TURMA: 000   -'
        Color = clSilver
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clBlack
        Font.Height = -11
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        Transparent = False
        WordWrap = True
        ExportAs = exptText
        FontSize = 8
      end
    end
  end
  inherited QryRelatorio: TADOQuery
    SQL.Strings = (
      
        'SELECT RESP.CO_PROFES_RESP, RESP.CO_PROFES_ADJUN, A.CO_ALU, M.CO' +
        '_ANO_MES_MAT, A.NO_ALU, A.CO_SEXO_ALU, A.DT_NASC_ALU, M.CO_ALU_C' +
        'AD, C.NO_CUR, C.CO_CUR, T.CO_TUR, CT.CO_SIGLA_TURMA AS NO_TUR, T' +
        '.CO_PERI_TUR, ET.NO_TIPOEMP, COL.NO_COL,M.CO_MODU_CUR'
      'FROM TB07_ALUNO A'
      'JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU'
      'JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR'
      'JOIN TB06_TURMAS T ON T.CO_TUR = M.CO_TUR'
      'JOIN TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR'
      '  JOIN TB25_EMPRESA E ON E.CO_EMP = A.CO_EMP'
      '  JOIN TB24_TPEMPRESA ET ON ET.CO_TIPOEMP = E.CO_TIPOEMP'
      '  JOIN TB03_COLABOR COL ON COL.CO_COL = E.CO_DIR'
      '  JOIN TB_RESPON_MATERIA RESP ON RESP.CO_CUR = M.CO_CUR'
      'WHERE A.CO_EMP = 2'
      '  AND C.CO_CUR = 1'
      '  AND T.CO_TUR = 5')
  end
  object QryFreqAluno: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      
        ' SELECT DISTINCT F.CO_USUA_FREQ,  ( SELECT F.FLA_PRESENCA FROM T' +
        'B09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.D' +
        'T_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'01'#39#9'and F.CO_USUA_FREQ' +
        ' = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39' ) P01,  ( SELECT F.FLA_PRESENCA' +
        ' FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MO' +
        'NTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'02'#39#9'and F.CO_US' +
        'UA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P02, ( SELECT F.FLA_PRE' +
        'SENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'a' +
        'nd MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'03'#39#9'and F.' +
        'CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P03, ( SELECT F.FL' +
        'A_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 200' +
        '8  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'04'#39#9'a' +
        'nd F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P04, ( SELECT' +
        ' F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) ' +
        '= 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39 +
        '05'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P05, ( S' +
        'ELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_' +
        'PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA' +
        ') = '#39'06'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P06' +
        ', ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_' +
        'PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PRE' +
        'V_PLA) = '#39'07'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39 +
        ') P07, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(' +
        'F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.D' +
        'T_PREV_PLA) = '#39'08'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ ' +
        '= '#39'A'#39') P08, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE ' +
        'YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DA' +
        'Y(F.DT_PREV_PLA) = '#39'09'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_' +
        'FREQ = '#39'A'#39') P09, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'W' +
        'HERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'a' +
        'nd DAY(F.DT_PREV_PLA) = '#39'10'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_' +
        'USUA_FREQ = '#39'A'#39') P10, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN ' +
        'F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) ' +
        '= 3'#9'and DAY(F.DT_PREV_PLA) = '#39'11'#39#9'and F.CO_USUA_FREQ = 1399'#9'and ' +
        'F.TP_USUA_FREQ = '#39'A'#39') P11, ( SELECT F.FLA_PRESENCA FROM TB09_FRE' +
        'QUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_' +
        'PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'12'#39#9'and F.CO_USUA_FREQ = 1399' +
        #9'and F.TP_USUA_FREQ = '#39'A'#39') P12, ( SELECT F.FLA_PRESENCA FROM TB0' +
        '9_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_' +
        'PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'13'#39#9'and F.CO_USUA_FREQ =' +
        ' 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P13, ( SELECT F.FLA_PRESENCA FRO' +
        'M TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(' +
        'F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'14'#39#9'and F.CO_USUA_F' +
        'REQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P14, ( SELECT F.FLA_PRESENC' +
        'A FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and M' +
        'ONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'15'#39#9'and F.CO_U' +
        'SUA_FREQ = 1399'
      
        #9'and F.TP_USUA_FREQ = '#39'A'#39') P15, ( SELECT F.FLA_PRESENCA FROM TB0' +
        '9_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_' +
        'PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'16'#39#9'and F.CO_USUA_FREQ =' +
        ' 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P16, ( SELECT F.FLA_PRESENCA FRO' +
        'M TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(' +
        'F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'17'#39#9'and F.CO_USUA_F' +
        'REQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P17, ( SELECT F.FLA_PRESENC' +
        'A FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and M' +
        'ONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'18'#39#9'and F.CO_U' +
        'SUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P18, ( SELECT F.FLA_PR' +
        'ESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9 +
        'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'19'#39#9'and F' +
        '.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P19, ( SELECT F.F' +
        'LA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 20' +
        '08  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'20'#39#9 +
        'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P20, ( SELEC' +
        'T F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA)' +
        ' = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = ' +
        #39'21'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P21, ( ' +
        'SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV' +
        '_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PL' +
        'A) = '#39'22'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P2' +
        '2, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT' +
        '_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PR' +
        'EV_PLA) = '#39'23'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ = '#39'A' +
        #39') P23, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE YEAR' +
        '(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and DAY(F.' +
        'DT_PREV_PLA) = '#39'24'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA_FREQ' +
        ' = '#39'A'#39') P24, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9'WHERE' +
        ' YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9'and D' +
        'AY(F.DT_PREV_PLA) = '#39'25'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP_USUA' +
        '_FREQ = '#39'A'#39') P25, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN F  '#9 +
        'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA) = 3'#9 +
        'and DAY(F.DT_PREV_PLA) = '#39'26'#39#9'and F.CO_USUA_FREQ = 1399'#9'and F.TP' +
        '_USUA_FREQ = '#39'A'#39') P26, ( SELECT F.FLA_PRESENCA FROM TB09_FREQUEN' +
        ' F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_PLA)' +
        ' = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'27'#39#9'and F.CO_USUA_FREQ = 1399'#9'and' +
        ' F.TP_USUA_FREQ = '#39'A'#39') P27, ( SELECT F.FLA_PRESENCA FROM TB09_FR' +
        'EQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV' +
        '_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'28'#39#9'and F.CO_USUA_FREQ = 139' +
        '9'#9'and F.TP_USUA_FREQ = '#39'A'#39') P28, ( SELECT F.FLA_PRESENCA FROM TB' +
        '09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT' +
        '_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'29'#39#9'and F.CO_USUA_FREQ ' +
        '= 1399'#9'and F.TP_USUA_FREQ = '#39'A'#39') P29, ( SELECT F.FLA_PRESENCA FR' +
        'OM TB09_FREQUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH' +
        '(F.DT_PREV_PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'30'#39#9'and F.CO_USUA_' +
        'FREQ = 1399'#9'and '
      
        'F.TP_USUA_FREQ = '#39'A'#39') P30, ( SELECT F.FLA_PRESENCA FROM TB09_FRE' +
        'QUEN F  '#9'WHERE YEAR(F.DT_PREV_PLA) = 2008  '#9'and MONTH(F.DT_PREV_' +
        'PLA) = 3'#9'and DAY(F.DT_PREV_PLA) = '#39'31'#39#9'and F.CO_USUA_FREQ = 1399' +
        #9'and F.TP_USUA_FREQ = '#39'A'#39') P31 FROM TB09_FREQUEN F WHERE F.CO_US' +
        'UA_FREQ = 1399')
    Left = 272
    Top = 24
    object QryFreqAlunoP01: TStringField
      FieldName = 'P01'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP02: TStringField
      FieldName = 'P02'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP03: TStringField
      FieldName = 'P03'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP04: TStringField
      FieldName = 'P04'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP05: TStringField
      FieldName = 'P05'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP06: TStringField
      FieldName = 'P06'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP07: TStringField
      FieldName = 'P07'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP08: TStringField
      FieldName = 'P08'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP09: TStringField
      FieldName = 'P09'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP10: TStringField
      FieldName = 'P10'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP11: TStringField
      FieldName = 'P11'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP12: TStringField
      FieldName = 'P12'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP13: TStringField
      FieldName = 'P13'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP14: TStringField
      FieldName = 'P14'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP15: TStringField
      FieldName = 'P15'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP16: TStringField
      FieldName = 'P16'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP17: TStringField
      FieldName = 'P17'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP18: TStringField
      FieldName = 'P18'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP19: TStringField
      FieldName = 'P19'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP20: TStringField
      FieldName = 'P20'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP21: TStringField
      FieldName = 'P21'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP22: TStringField
      FieldName = 'P22'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP23: TStringField
      FieldName = 'P23'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP24: TStringField
      FieldName = 'P24'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP25: TStringField
      FieldName = 'P25'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP26: TStringField
      FieldName = 'P26'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP27: TStringField
      FieldName = 'P27'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP28: TStringField
      FieldName = 'P28'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP29: TStringField
      FieldName = 'P29'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP30: TStringField
      FieldName = 'P30'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoP31: TStringField
      FieldName = 'P31'
      ReadOnly = True
      FixedChar = True
      Size = 1
    end
    object QryFreqAlunoCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
  end
  object QrySexo: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT DISTINCT CT.NO_TURMA,'
      '( SELECT COUNT (A.CO_SEXO_ALU)'
      #9'FROM TB07_ALUNO A'
      #9'JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU'
      #9'JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR'
      #9'JOIN TB06_TURMAS T ON T.CO_TUR = M.CO_TUR'
      #9'WHERE A.CO_EMP = 2'
      #9'AND C.CO_CUR = 1'
      #9'AND T.CO_TUR = 5'
      #9'AND A.CO_SEXO_ALU = '#39'M'#39
      ') MASC,'
      '( SELECT COUNT (A.CO_SEXO_ALU)'
      #9'FROM TB07_ALUNO A'
      #9'JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU'
      #9'JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR'
      #9'JOIN TB06_TURMAS T ON T.CO_TUR = M.CO_TUR'
      #9'WHERE A.CO_EMP = 2'
      #9'AND C.CO_CUR = 1'
      #9'AND T.CO_TUR = 5'
      #9'AND A.CO_SEXO_ALU = '#39'F'#39
      ') FEM'
      'FROM TB07_ALUNO A'
      '  JOIN TB08_MATRCUR M ON M.CO_ALU = A.CO_ALU'
      '  JOIN TB01_CURSO C ON C.CO_CUR = M.CO_CUR'
      '  JOIN TB06_TURMAS T ON T.CO_TUR = M.CO_TUR'
      'JOIN TB129_CADTURMAS CT ON T.CO_TUR = CT.CO_TUR'
      'WHERE A.CO_EMP = 2'
      '   AND C.CO_CUR = 1'
      '   AND T.CO_TUR = 5')
    Left = 312
    Top = 32
    object QrySexoMASC: TIntegerField
      FieldName = 'MASC'
      ReadOnly = True
    end
    object QrySexoFEM: TIntegerField
      FieldName = 'FEM'
      ReadOnly = True
    end
    object QrySexoNO_TURMA: TStringField
      FieldName = 'NO_TURMA'
      Size = 40
    end
  end
  object QryTF: TADOQuery
    Connection = DataModuleSGE.Conn
    Parameters = <>
    SQL.Strings = (
      'SET LANGUAGE PORTUGUESE'
      '  SELECT DISTINCT QTA.*, F.CO_USUA_FREQ, C.QT_AULA_CUR,'
      '  (SELECT COUNT(*) FROM TB09_FREQUEN F'
      '   WHERE YEAR(F.DT_PREV_PLA) = 2008'
      '  '#9'  and MONTH(F.DT_PREV_PLA) = '#39'03'#39
      #9'  and F.CO_USUA_FREQ = 1296'
      '  '#9'and F.TP_USUA_FREQ = '#39'A'#39
      ' '#9'and F.FLA_PRESENCA = '#39'N'#39
      ' ) TFMES,'
      '  (SELECT COUNT(*) FROM TB09_FREQUEN F'
      '   WHERE YEAR(F.DT_PREV_PLA) = 2008'
      '   and MONTH(F.DT_PREV_PLA) = '#39'03'#39
      '   and F.CO_CUR = 1'
      '   and F.CO_TUR = 5'
      '   and F.TP_USUA_FREQ = '#39'A'#39
      '   and F.FLA_PRESENCA = '#39'N'#39
      '  ) TFTURMA'
      '  FROM TB09_FREQUEN F'
      '  JOIN TB01_CURSO C ON C.CO_CUR = F.CO_CUR'
      '  JOIN TB_QTDE_AULAS QTA ON QTA.CO_CUR = C.CO_CUR'
      '  WHERE F.CO_USUA_FREQ = 1296')
    Left = 700
    Top = 264
    object QryTFCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryTFCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryTFCO_MAT: TIntegerField
      FieldName = 'CO_MAT'
    end
    object QryTFCO_ANO_REF: TIntegerField
      FieldName = 'CO_ANO_REF'
    end
    object QryTFQT_DIAS_LETIVO_JAN: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_JAN'
    end
    object QryTFQT_AULAS_PROG_JAN: TIntegerField
      FieldName = 'QT_AULAS_PROG_JAN'
    end
    object QryTFQT_AULAS_REAL_JAN: TIntegerField
      FieldName = 'QT_AULAS_REAL_JAN'
    end
    object QryTFQT_DIAS_LETIVO_FEV: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_FEV'
    end
    object QryTFQT_AULAS_PROG_FEV: TIntegerField
      FieldName = 'QT_AULAS_PROG_FEV'
    end
    object QryTFQT_AULAS_REAL_FEV: TIntegerField
      FieldName = 'QT_AULAS_REAL_FEV'
    end
    object QryTFQT_DIAS_LETIVO_MAR: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_MAR'
    end
    object QryTFQT_AULAS_PROG_MAR: TIntegerField
      FieldName = 'QT_AULAS_PROG_MAR'
    end
    object QryTFQT_AULAS_REAL_MAR: TIntegerField
      FieldName = 'QT_AULAS_REAL_MAR'
    end
    object QryTFQT_DIAS_LETIVO_ABR: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_ABR'
    end
    object QryTFQT_AULAS_PROG_ABR: TIntegerField
      FieldName = 'QT_AULAS_PROG_ABR'
    end
    object QryTFQT_AULAS_REAL_ABR: TIntegerField
      FieldName = 'QT_AULAS_REAL_ABR'
    end
    object QryTFQT_DIAS_LETIVO_MAI: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_MAI'
    end
    object QryTFQT_AULAS_PROG_MAI: TIntegerField
      FieldName = 'QT_AULAS_PROG_MAI'
    end
    object QryTFQT_AULAS_REAL_MAI: TIntegerField
      FieldName = 'QT_AULAS_REAL_MAI'
    end
    object QryTFQT_DIAS_LETIVO_JUN: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_JUN'
    end
    object QryTFQT_AULAS_PROG_JUN: TIntegerField
      FieldName = 'QT_AULAS_PROG_JUN'
    end
    object QryTFQT_AULAS_REAL_JUN: TIntegerField
      FieldName = 'QT_AULAS_REAL_JUN'
    end
    object QryTFQT_DIAS_LETIVO_JUL: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_JUL'
    end
    object QryTFQT_AULAS_PROG_JUL: TIntegerField
      FieldName = 'QT_AULAS_PROG_JUL'
    end
    object QryTFQT_AULAS_REAL_JUL: TIntegerField
      FieldName = 'QT_AULAS_REAL_JUL'
    end
    object QryTFQT_DIAS_LETIVO_AGO: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_AGO'
    end
    object QryTFQT_AULAS_PROG_AGO: TIntegerField
      FieldName = 'QT_AULAS_PROG_AGO'
    end
    object QryTFQT_AULAS_REAL_AGO: TIntegerField
      FieldName = 'QT_AULAS_REAL_AGO'
    end
    object QryTFQT_DIAS_LETIVO_SET: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_SET'
    end
    object QryTFQT_AULAS_PROG_SET: TIntegerField
      FieldName = 'QT_AULAS_PROG_SET'
    end
    object QryTFQT_AULAS_REAL_SET: TIntegerField
      FieldName = 'QT_AULAS_REAL_SET'
    end
    object QryTFQT_DIAS_LETIVO_OUT: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_OUT'
    end
    object QryTFQT_AULAS_PROG_OUT: TIntegerField
      FieldName = 'QT_AULAS_PROG_OUT'
    end
    object QryTFQT_AULAS_REAL_OUT: TIntegerField
      FieldName = 'QT_AULAS_REAL_OUT'
    end
    object QryTFQT_DIAS_LETIVO_NOV: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_NOV'
    end
    object QryTFQT_AULAS_PROG_NOV: TIntegerField
      FieldName = 'QT_AULAS_PROG_NOV'
    end
    object QryTFQT_AULAS_REAL_NOV: TIntegerField
      FieldName = 'QT_AULAS_REAL_NOV'
    end
    object QryTFQT_DIAS_LETIVO_DEZ: TIntegerField
      FieldName = 'QT_DIAS_LETIVO_DEZ'
    end
    object QryTFQT_AULAS_PROG_DEZ: TIntegerField
      FieldName = 'QT_AULAS_PROG_DEZ'
    end
    object QryTFQT_AULAS_REAL_DEZ: TIntegerField
      FieldName = 'QT_AULAS_REAL_DEZ'
    end
    object QryTFQT_AULA_CUR: TIntegerField
      FieldName = 'QT_AULA_CUR'
    end
    object QryTFTFMES: TIntegerField
      FieldName = 'TFMES'
      ReadOnly = True
    end
    object QryTFTFTURMA: TIntegerField
      FieldName = 'TFTURMA'
      ReadOnly = True
    end
    object QryTFCO_MODU_CUR: TIntegerField
      FieldName = 'CO_MODU_CUR'
    end
    object QryTFCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
  end
end
