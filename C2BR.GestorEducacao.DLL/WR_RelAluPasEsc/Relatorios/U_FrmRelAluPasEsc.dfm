inherited FrmRelAluPasEsc: TFrmRelAluPasEsc
  Left = 209
  Top = 150
  HorzScrollBar.Position = 53
  Caption = 'FrmRelAluPasEsc'
  OldCreateOrder = True
  PixelsPerInch = 96
  TextHeight = 13
  inherited QuickRep1: TQuickRep
    Left = -45
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
      Frame.DrawBottom = False
      Size.Values = (
        383.645833333333400000
        2717.270833333333000000)
      inherited LblTituloRel: TQRLabel
        Width = 1027
        Size.Values = (
          60.854166666666680000
          2.645833333333333000
          306.916666666666700000
          2717.270833333333000000)
        Caption = 'RELA'#199#195'O DE ALUNOS - PASSE ESCOLAR'
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
      Top = 274
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
      Top = 235
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
      object QRLMatricula: TQRLabel
        Left = 5
        Top = 1
        Width = 91
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
          240.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '00.000.0000.XXX'
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
      object QRLNuNis: TQRLabel
        Left = 102
        Top = 1
        Width = 44
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          269.875000000000000000
          2.645833333333333000
          116.416666666666700000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = '99.999-9'
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
      object QRDBText7: TQRDBText
        Left = 494
        Top = 1
        Width = 10
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1307.041666666667000000
          2.645833333333333000
          26.458333333333330000)
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
        FontSize = 8
      end
      object QRLCidBai: TQRLabel
        Left = 686
        Top = 1
        Width = 211
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1815.041666666667000000
          2.645833333333333000
          558.270833333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLCidBai'
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
      object QRLSerTur: TQRLabel
        Left = 597
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
          1579.562500000000000000
          2.645833333333333000
          222.250000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = 'QRLSerTur'
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
      object QRDBText1: TQRDBText
        Left = 520
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
          1375.833333333333000000
          2.645833333333333000
          201.083333333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Color = clWhite
        DataSet = QryRelatorio
        DataField = 'DT_NASC_ALU'
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
      object QRLNoAlu: TQRLabel
        Left = 183
        Top = 1
        Width = 298
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          484.187500000000000000
          2.645833333333333000
          788.458333333333400000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = False
        AutoStretch = False
        Caption = '99.999-9'
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
    object QRGroup1: TQRGroup
      Left = 48
      Top = 193
      Width = 1027
      Height = 42
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
        111.125000000000000000
        2717.270833333333000000)
      Master = QuickRep1
      ReprintOnNewPage = True
      object QRShape1: TQRShape
        Left = 0
        Top = 24
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
      object QRLabel10: TQRLabel
        Left = 5
        Top = 26
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
          68.791666666666680000
          174.625000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'MATR'#205'CULA'
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
        Left = 102
        Top = 26
        Width = 32
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          269.875000000000000000
          68.791666666666680000
          84.666666666666680000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'N'#186' NIS'
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
        Left = 183
        Top = 26
        Width = 90
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          484.187500000000000000
          68.791666666666680000
          238.125000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'NOME DO ALUNO'
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
        Left = 491
        Top = 26
        Width = 15
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1299.104166666667000000
          68.791666666666680000
          39.687500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'SX'
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
        Left = 520
        Top = 26
        Width = 51
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1375.833333333333000000
          68.791666666666680000
          134.937500000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'DT. NASC'
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
        Left = 597
        Top = 26
        Width = 72
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1579.562500000000000000
          68.791666666666680000
          190.500000000000000000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'S'#201'RIE/TURMA'
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
        Left = 686
        Top = 26
        Width = 83
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          1815.041666666667000000
          68.791666666666680000
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
        Transparent = True
        WordWrap = True
        FontSize = 8
      end
      object QRLabel8: TQRLabel
        Left = 899
        Top = 26
        Width = 91
        Height = 15
        Frame.Color = clBlack
        Frame.DrawTop = False
        Frame.DrawBottom = False
        Frame.DrawLeft = False
        Frame.DrawRight = False
        Size.Values = (
          39.687500000000000000
          2378.604166666667000000
          68.791666666666680000
          240.770833333333300000)
        Alignment = taLeftJustify
        AlignToBand = False
        AutoSize = True
        AutoStretch = False
        Caption = 'LINHA DE '#212'NIBUS'
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
      Top = 252
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
        Width = 90
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
      object QRLTotAlu: TQRLabel
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
      
        'select a.*,b.no_bairro,e.coduf,ci.no_cidade,c.no_cur,C.CO_SIGL_C' +
        'UR,tu.no_turma [no_tur],mm.co_alu_cad,r.no_resp,r.nu_tele_celu_r' +
        'esp from tb07_aluno a'
      '               join tb74_UF e on e.coduf = a.co_esta_alu'
      '               join tb08_matrcur mm on mm.co_alu = a.co_alu '
      '               join tb01_curso c on c.co_cur = mm.co_cur '
      '               join tb108_responsavel r on r.co_resp = a.co_resp'
      '               join tb06_turmas t on t.co_tur = mm.co_tur '
      '               join tb129_cadturmas tu on tu.co_tur = mm.co_tur '
      '               join tb905_bairro b on b.co_bairro = a.co_bairro'
      
        '               join tb904_cidade ci on ci.co_cidade = a.co_cidad' +
        'e')
    object QryRelatorioCO_ALU: TAutoIncField
      FieldName = 'CO_ALU'
      ReadOnly = True
    end
    object QryRelatorioCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryRelatorioNO_ALU: TStringField
      FieldName = 'NO_ALU'
      Size = 80
    end
    object QryRelatorioNO_APE_ALU: TStringField
      FieldName = 'NO_APE_ALU'
      Size = 15
    end
    object QryRelatorioCO_INST: TIntegerField
      FieldName = 'CO_INST'
    end
    object QryRelatorioDT_NASC_ALU: TDateTimeField
      FieldName = 'DT_NASC_ALU'
    end
    object QryRelatorioNU_CPF_ALU: TStringField
      FieldName = 'NU_CPF_ALU'
      Size = 18
    end
    object QryRelatorioCO_SEXO_ALU: TStringField
      FieldName = 'CO_SEXO_ALU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_RG_ALU: TStringField
      FieldName = 'CO_RG_ALU'
    end
    object QryRelatorioCO_ORG_RG_ALU: TStringField
      FieldName = 'CO_ORG_RG_ALU'
      Size = 12
    end
    object QryRelatorioCO_ESTA_RG_ALU: TStringField
      FieldName = 'CO_ESTA_RG_ALU'
      Size = 10
    end
    object QryRelatorioDT_EMIS_RG_ALU: TDateTimeField
      FieldName = 'DT_EMIS_RG_ALU'
    end
    object QryRelatorioDE_ENDE_ALU: TStringField
      FieldName = 'DE_ENDE_ALU'
      Size = 100
    end
    object QryRelatorioNU_ENDE_ALU: TIntegerField
      FieldName = 'NU_ENDE_ALU'
    end
    object QryRelatorioDE_COMP_ALU: TStringField
      FieldName = 'DE_COMP_ALU'
      Size = 15
    end
    object QryRelatorioCO_BAIRRO: TIntegerField
      FieldName = 'CO_BAIRRO'
    end
    object QryRelatorioCO_CIDADE: TIntegerField
      FieldName = 'CO_CIDADE'
    end
    object QryRelatorioCO_TIPO_BOLSA: TIntegerField
      FieldName = 'CO_TIPO_BOLSA'
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
    object QryRelatorioNU_TELE_RESI_ALU: TStringField
      FieldName = 'NU_TELE_RESI_ALU'
      Size = 10
    end
    object QryRelatorioNU_TELE_CELU_ALU: TStringField
      FieldName = 'NU_TELE_CELU_ALU'
      Size = 10
    end
    object QryRelatorioNO_PROF_ALU: TStringField
      FieldName = 'NO_PROF_ALU'
      Size = 60
    end
    object QryRelatorioNO_EMPR_ALU: TStringField
      FieldName = 'NO_EMPR_ALU'
    end
    object QryRelatorioNO_CARG_EMPR_ALU: TStringField
      FieldName = 'NO_CARG_EMPR_ALU'
      Size = 15
    end
    object QryRelatorioDT_ADMI_EMPR_ALU: TDateTimeField
      FieldName = 'DT_ADMI_EMPR_ALU'
    end
    object QryRelatorioNU_TELE_COME_ALU: TStringField
      FieldName = 'NU_TELE_COME_ALU'
      Size = 10
    end
    object QryRelatorioNU_RAMA_COME_ALU: TStringField
      FieldName = 'NU_RAMA_COME_ALU'
      Size = 4
    end
    object QryRelatorioNO_PAI_ALU: TStringField
      FieldName = 'NO_PAI_ALU'
      Size = 60
    end
    object QryRelatorioNO_MAE_ALU: TStringField
      FieldName = 'NO_MAE_ALU'
      Size = 60
    end
    object QryRelatorioCO_NACI_ALU: TStringField
      FieldName = 'CO_NACI_ALU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDE_NACI_ALU: TStringField
      FieldName = 'DE_NACI_ALU'
      Size = 40
    end
    object QryRelatorioDE_NATU_ALU: TStringField
      FieldName = 'DE_NATU_ALU'
      Size = 40
    end
    object QryRelatorioCO_UF_NATU_ALU: TStringField
      FieldName = 'CO_UF_NATU_ALU'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioNO_ENDE_ELET_ALU: TStringField
      FieldName = 'NO_ENDE_ELET_ALU'
      Size = 50
    end
    object QryRelatorioNO_WEB_ALU: TStringField
      FieldName = 'NO_WEB_ALU'
      Size = 50
    end
    object QryRelatorioDT_CADA_ALU: TDateTimeField
      FieldName = 'DT_CADA_ALU'
    end
    object QryRelatorioCO_SITU_ALU: TStringField
      FieldName = 'CO_SITU_ALU'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDT_SITU_ALU: TDateTimeField
      FieldName = 'DT_SITU_ALU'
    end
    object QryRelatorioNU_TIT_ELE: TStringField
      FieldName = 'NU_TIT_ELE'
      Size = 15
    end
    object QryRelatorioNU_ZONA_ELE: TStringField
      FieldName = 'NU_ZONA_ELE'
      Size = 10
    end
    object QryRelatorioNU_SEC_ELE: TStringField
      FieldName = 'NU_SEC_ELE'
      Size = 10
    end
    object QryRelatorioCO_UF_TIT_ELE: TStringField
      FieldName = 'CO_UF_TIT_ELE'
      FixedChar = True
      Size = 2
    end
    object QryRelatorioFLA_BOLSISTA: TStringField
      FieldName = 'FLA_BOLSISTA'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioNU_PEC_DESBOL: TBCDField
      FieldName = 'NU_PEC_DESBOL'
      Precision = 6
      Size = 2
    end
    object QryRelatorioDE_TIPO_BOLSA: TStringField
      FieldName = 'DE_TIPO_BOLSA'
      Size = 25
    end
    object QryRelatorioDT_VENC_BOLSA: TDateTimeField
      FieldName = 'DT_VENC_BOLSA'
    end
    object QryRelatorioDT_VENC_BOLSAF: TDateTimeField
      FieldName = 'DT_VENC_BOLSAF'
    end
    object QryRelatorioNOM_USUARIO: TStringField
      FieldName = 'NOM_USUARIO'
      Size = 30
    end
    object QryRelatorioDT_ALT_REGISTRO: TDateTimeField
      FieldName = 'DT_ALT_REGISTRO'
    end
    object QryRelatorioCO_EMP_ORIGEM: TIntegerField
      FieldName = 'CO_EMP_ORIGEM'
    end
    object QryRelatorioCO_ESTADO_CIVIL: TStringField
      FieldName = 'CO_ESTADO_CIVIL'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDES_OBSERVACAO: TStringField
      FieldName = 'DES_OBSERVACAO'
      Size = 500
    end
    object QryRelatorioTP_RACA: TStringField
      FieldName = 'TP_RACA'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioNU_CERT: TStringField
      FieldName = 'NU_CERT'
      Size = 10
    end
    object QryRelatorioDE_CERT_LIVRO: TStringField
      FieldName = 'DE_CERT_LIVRO'
      Size = 10
    end
    object QryRelatorioNU_CERT_FOLHA: TStringField
      FieldName = 'NU_CERT_FOLHA'
      Size = 8
    end
    object QryRelatorioDE_CERT_CARTORIO: TStringField
      FieldName = 'DE_CERT_CARTORIO'
      Size = 80
    end
    object QryRelatorioTP_DEF: TStringField
      FieldName = 'TP_DEF'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioDES_DEF: TStringField
      FieldName = 'DES_DEF'
      Size = 50
    end
    object QryRelatorioRENDA_FAMILIAR: TStringField
      FieldName = 'RENDA_FAMILIAR'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioFLA_BOLSA_ESCOLA: TBooleanField
      FieldName = 'FLA_BOLSA_ESCOLA'
    end
    object QryRelatorioDES_OBS_ALU: TMemoField
      FieldName = 'DES_OBS_ALU'
      BlobType = ftMemo
    end
    object QryRelatorioFLA_PASSE_ESCOLA: TBooleanField
      FieldName = 'FLA_PASSE_ESCOLA'
    end
    object QryRelatorioTP_CERTIDAO: TStringField
      FieldName = 'TP_CERTIDAO'
      FixedChar = True
      Size = 1
    end
    object QryRelatorioCO_RESP: TIntegerField
      FieldName = 'CO_RESP'
    end
    object QryRelatoriono_bairro: TStringField
      FieldName = 'no_bairro'
      Size = 80
    end
    object QryRelatoriocoduf: TStringField
      FieldName = 'coduf'
      FixedChar = True
      Size = 2
    end
    object QryRelatoriono_cidade: TStringField
      FieldName = 'no_cidade'
      Size = 80
    end
    object QryRelatoriono_cur: TStringField
      FieldName = 'no_cur'
      Size = 100
    end
    object QryRelatoriono_tur: TStringField
      FieldName = 'no_tur'
      Size = 40
    end
    object QryRelatorioco_alu_cad: TStringField
      FieldName = 'co_alu_cad'
    end
    object QryRelatoriono_resp: TStringField
      FieldName = 'no_resp'
      Size = 60
    end
    object QryRelatorionu_tele_celu_resp: TStringField
      FieldName = 'nu_tele_celu_resp'
      Size = 10
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
