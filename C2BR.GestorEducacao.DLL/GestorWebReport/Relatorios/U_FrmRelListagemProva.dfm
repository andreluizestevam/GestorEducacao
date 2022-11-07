inherited FrmRelListagemProva: TFrmRelListagemProva
  Left = 211
  Top = 165
  Width = 809
  Height = 431
  VertScrollBar.Position = 219
  Caption = 'Listagem de Prova'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = 8
    Top = -102
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
      80.000000000000000000
      80.000000000000000000
      0.000000000000000000)
    Units = MM
    inherited PageHeaderBand1: TQRBand
      Left = 30
      Width = 734
      Height = 176
      Frame.DrawBottom = False
      Size.Values = (
        465.666666666666800000
        1942.041666666667000000)
      inherited LblTituloRel: TQRLabel
        Top = 103
        Width = 733
        Height = 20
        Size.Values = (
          52.916666666666660000
          2.645833333333333000
          272.520833333333400000
          1939.395833333333000000)
        Caption = 'LISTAGEM DE PROVA'
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
        Left = 633
        Width = 32
        Size.Values = (
          44.979166666666670000
          1674.812500000000000000
          21.166666666666670000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleData: TQRLabel
        Left = 633
        Width = 32
        Size.Values = (
          44.979166666666670000
          1674.812500000000000000
          66.145833333333340000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited qrlTempleHora: TQRLabel
        Left = 633
        Width = 32
        Size.Values = (
          44.979166666666670000
          1674.812500000000000000
          111.125000000000000000
          84.666666666666680000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData1: TQRSysData
        Left = 680
        Size.Values = (
          44.979166666666670000
          1799.166666666667000000
          21.166666666666670000
          63.500000000000000000)
        AlignToBand = False
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData2: TQRSysData
        Left = 697
        Width = 37
        Size.Values = (
          44.979166666666670000
          1844.145833333333000000
          111.125000000000000000
          97.895833333333340000)
        Font.Height = -13
        FontSize = 10
      end
      inherited QRSysData3: TQRSysData
        Left = 698
        Top = 25
        Width = 36
        Size.Values = (
          44.979166666666670000
          1846.791666666667000000
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
        Left = 711
        Size.Values = (
          39.687500000000000000
          1881.187500000000000000
          68.791666666666680000
          60.854166666666680000)
        AlignToBand = True
        FontSize = 8
      end
      inherited QRILogoEscola: TQRImage
        Size.Values = (
          198.437500000000000000
          635.000000000000000000
          10.583333333333330000
          246.062500000000000000)
      end
      object QRLParametros: TQRLabel
        Left = 0
        Top = 137
        Width = 734
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          0.000000000000000000
          362.479166666666700000
          1942.041666666667000000)
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
      object QRLabel2: TQRLabel
        Left = 707
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
          1870.604166666667000000
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
        Left = 715
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
          1891.770833333333000000
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
      Left = 30
      Top = 348
      Width = 734
      Height = 18
      Size.Values = (
        47.625000000000000000
        1942.041666666667000000)
      inherited QRLabelSGIE: TQRLabel
        Left = 280
        Width = 454
        Height = 17
        Size.Values = (
          44.979166666666670000
          740.833333333333400000
          0.000000000000000000
          1201.208333333333000000)
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
    object QRBand2: TQRBand
      Left = 30
      Top = 224
      Width = 734
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
        1942.041666666667000000)
      BandType = rbTitle
      object QRShape4: TQRShape
        Left = 0
        Top = 0
        Width = 735
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          0.000000000000000000
          0.000000000000000000
          1944.687500000000000000)
        Brush.Color = clGray
        Pen.Style = psClear
        Shape = qrsRectangle
      end
      object QRLabel3: TQRLabel
        Left = 5
        Top = 1
        Width = 93
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
          246.062500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Matr'#237'cula - Nome'
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
        Left = 389
        Top = 1
        Width = 18
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1029.229166666667000000
          2.645833333333333000
          47.625000000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MB'
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
        Left = 440
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
          1164.166666666667000000
          2.645833333333333000
          87.312500000000000000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Faltas'
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
        Left = 609
        Top = 1
        Width = 61
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1611.312500000000000000
          2.645833333333333000
          161.395833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'Assinatura'
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
        Left = 487
        Top = 1
        Width = 52
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1288.520833333333000000
          2.645833333333333000
          137.583333333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '% Faltas'
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
      object QRShape1: TQRShape
        Left = 368
        Top = 0
        Width = 3
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          973.666666666666900000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape5: TQRShape
        Left = 426
        Top = 0
        Width = 3
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          1127.125000000000000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape6: TQRShape
        Left = 484
        Top = 0
        Width = 3
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          1280.583333333333000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape7: TQRShape
        Left = 541
        Top = 0
        Width = 3
        Height = 18
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          47.625000000000000000
          1431.395833333333000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
    end
    object QRBand1: TQRBand
      Left = 30
      Top = 242
      Width = 734
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
        1942.041666666667000000)
      BandType = rbDetail
      object QRDBText3: TQRDBText
        Left = 374
        Top = 1
        Width = 49
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
          129.645833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'VL_NOTA'
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
        Left = 432
        Top = 1
        Width = 49
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1143.000000000000000000
          2.645833333333333000
          129.645833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'FALTA'
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
      object QrlPercFaltas: TQRLabel
        Left = 489
        Top = 1
        Width = 49
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1293.812500000000000000
          2.645833333333333000
          129.645833333333300000)
        Alignment = taCenter
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QrlPercFaltas'
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
      object QRShape2: TQRShape
        Left = 368
        Top = 0
        Width = 3
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          973.666666666666900000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape9: TQRShape
        Left = 426
        Top = 0
        Width = 3
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1127.125000000000000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape10: TQRShape
        Left = 484
        Top = 0
        Width = 3
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1280.583333333333000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QRShape11: TQRShape
        Left = 541
        Top = 0
        Width = 3
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1431.395833333333000000
          0.000000000000000000
          7.937500000000000000)
        Shape = qrsVertLine
      end
      object QrlMatNome: TQRLabel
        Left = 5
        Top = 1
        Width = 360
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
          952.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000.0000.XXX - NOME COMPLETO DO ALUNO'
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
    object QRBand3: TQRBand
      Left = 30
      Top = 259
      Width = 734
      Height = 89
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
        235.479166666666700000
        1942.041666666667000000)
      BandType = rbSummary
      object QRShape13: TQRShape
        Left = 6
        Top = 24
        Width = 721
        Height = 46
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          121.708333333333300000
          15.875000000000000000
          63.500000000000000000
          1907.645833333333000000)
        Shape = qrsRectangle
      end
      object QRLabel10: TQRLabel
        Left = 14
        Top = 40
        Width = 475
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          37.041666666666670000
          105.833333333333300000
          1256.770833333333000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 
          'Assinatuda do Professor : ______________________________________' +
          '_______'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = []
        ParentFont = False
        Transparent = False
        WordWrap = True
        FontSize = 10
      end
      object QRLabel9: TQRLabel
        Left = 516
        Top = 40
        Width = 197
        Height = 17
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          44.979166666666670000
          1365.250000000000000000
          105.833333333333300000
          521.229166666666800000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'N'#186' de Alunos Ausentes : _______'
        Color = clWhite
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
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
    CursorType = ctStatic
    SQL.Strings = (
      'SELECT DISTINCT(MM.CO_ALU_CAD), '
      '       A.NO_ALU,'
      '       N.VL_NOTA,'
      '       C.PE_CERT_CUR,'
      '       C.QT_AULA_CUR,'
      '       FALTA = SUM( CASE F.FLA_PRESENCA'
      '                            WHEN '#39'N'#39' THEN 1'
      '                            ELSE 0'
      '                       END),'
      '       C.PE_FALT_CUR'
      'FROM TB49_NOTA_ALUNO N, '
      '     TB07_ALUNO A, '
      '     TB80_MASTERMATR MM, '
      '     TB02_MATERIA MA,'
      '     TB01_CURSO C,'
      '     TB09_FREQUEN F'
      
        'WHERE N.CO_EMP = MM.CO_EMP                                      ' +
        '         '
      
        '  AND N.CO_CUR = MM.CO_CUR                                      ' +
        '         '
      
        '  AND N.CO_ALU = MM.CO_ALU                                      ' +
        '         '
      
        '  AND N.CO_EMP = A.CO_EMP                                       ' +
        '         '
      
        '  AND N.CO_ALU = A.CO_ALU                                       ' +
        '         '
      
        '  AND N.CO_EMP = MA.CO_EMP                                      ' +
        '         '
      '  AND N.CO_MAT = MA.CO_MAT'
      
        '  AND N.CO_EMP = C.CO_EMP                                       ' +
        '        '
      
        '  AND N.CO_CUR = C.CO_CUR                                       ' +
        '         '
      
        '  AND N.CO_EMP = F.CO_EMP                                       ' +
        '        '
      
        '  AND N.CO_ALU = F.CO_USUA_FREQ                                 ' +
        '              '
      
        '  AND N.CO_CUR = F.CO_CUR                                       ' +
        '        '
      
        '  AND N.CO_TUR = F.CO_TUR                                       ' +
        '         '
      '  AND N.CO_MAT = F.CO_MAT '
      '  AND N.CO_BIMESTRE = F.CO_BIMESTRE '
      '  AND N.CO_MAT = 4     '
      
        '  AND F.TP_USUA_FREQ = '#39'A'#39'                                      ' +
        '    '
      '  AND N.TP_AVAL = '#39'M'#39
      'GROUP BY MM.CO_ALU_CAD, '
      '       A.NO_ALU,'
      '       N.VL_NOTA,'
      '       C.QT_AULA_CUR,'
      '       C.PE_CERT_CUR,'
      '       C.PE_FALT_CUR')
    object QryRelatorioCO_ALU_CAD: TStringField
      FieldName = 'CO_ALU_CAD'
      Size = 15
    end
    object QryRelatorioNO_ALU: TStringField
      FieldName = 'NO_ALU'
      Size = 60
    end
    object QryRelatorioVL_NOTA: TBCDField
      FieldName = 'VL_NOTA'
      DisplayFormat = '#,##0.00'
      Precision = 6
      Size = 2
    end
    object QryRelatorioPE_CERT_CUR: TBCDField
      FieldName = 'PE_CERT_CUR'
      Precision = 4
      Size = 2
    end
    object QryRelatorioQT_AULA_CUR: TIntegerField
      FieldName = 'QT_AULA_CUR'
    end
    object QryRelatorioFALTA: TIntegerField
      FieldName = 'FALTA'
      ReadOnly = True
    end
    object QryRelatorioPE_FALT_CUR: TBCDField
      FieldName = 'PE_FALT_CUR'
      Precision = 4
      Size = 2
    end
  end
end
