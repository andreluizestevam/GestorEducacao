object DataModuleSGE: TDataModuleSGE
  OldCreateOrder = False
  OnCreate = DataModuleCreate
  Left = 7
  Top = 8
  Height = 552
  Width = 793
  object Conn: TADOConnection
    CommandTimeout = 20
    ConnectionString = 
      'Provider=SQLOLEDB.1;Password=@meninar#2011;Persist Security Info' +
      '=True;User ID=gestoradmin;Initial Catalog=Gestor;Data Source=.\S' +
      'QLEXPRESS;Use Procedure for Prepare=1;Auto Translate=True;Packet' +
      ' Size=4096;Workstation ID=VAIO-AVINAGRE;Use Encryption for Data=' +
      'False;Tag with column collation when possible=False'
    ConnectionTimeout = 10
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'SQLOLEDB.1'
    Left = 20
    Top = 11
  end
  object QrySql: TADOQuery
    Connection = Conn
    Parameters = <>
    Left = 80
    Top = 3
  end
  object QryCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select *'
      'From TB01_CURSO'
      'WHERE CO_EMP = :P_CO_EMP')
    Left = 24
    Top = 56
    object QryCursoCO_CUR: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_CUR'
      ReadOnly = True
    end
    object QryCursoNO_CUR: TStringField
      DisplayLabel = 'Curso'
      FieldName = 'NO_CUR'
      Size = 40
    end
    object QryCursoQT_CARG_HORA_CUR: TIntegerField
      DisplayLabel = 'Carga Hor'#225'ria'
      FieldName = 'QT_CARG_HORA_CUR'
    end
    object QryCursoNO_MENT_CUR: TStringField
      DisplayLabel = 'Respons'#225'vel'
      FieldName = 'NO_MENT_CUR'
      Size = 100
    end
    object QryCursoDT_CRIA_CUR: TDateTimeField
      DisplayLabel = 'Data Cria'#231#227'o'
      FieldName = 'DT_CRIA_CUR'
    end
    object QryCursoVL_TOTA_CUR: TBCDField
      DisplayLabel = 'Valor do Curso'
      FieldName = 'VL_TOTA_CUR'
      Precision = 7
      Size = 2
    end
    object QryCursoQT_OCOR_CUR: TIntegerField
      DisplayLabel = 'Ocorr'#234'ncias'
      FieldName = 'QT_OCOR_CUR'
    end
    object QryCursoPE_CERT_CUR: TBCDField
      FieldName = 'PE_CERT_CUR'
      Precision = 4
      Size = 2
    end
    object QryCursoDT_SITU_CUR: TDateTimeField
      DisplayLabel = 'Data da Situa'#231#227'o'
      FieldName = 'DT_SITU_CUR'
    end
    object QryCursoCO_SITU: TStringField
      FieldName = 'CO_SITU'
      FixedChar = True
      Size = 1
    end
    object QryCursoCO_IDENDES_CUR: TStringField
      FieldName = 'CO_IDENDES_CUR'
      FixedChar = True
      Size = 1
    end
    object QryCursoCO_NIVEL_CUR: TStringField
      FieldName = 'CO_NIVEL_CUR'
      FixedChar = True
      Size = 1
    end
    object QryCursoLkpDepartamento: TStringField
      DisplayLabel = 'Departamento'
      FieldKind = fkLookup
      FieldName = 'LkpDepartamento'
      LookupDataSet = QryPesqDeparCurso
      LookupKeyFields = 'CO_DPTO_CUR'
      LookupResultField = 'NO_DPTO_CUR'
      KeyFields = 'CO_DPTO_CUR'
      LookupCache = True
      Size = 40
      Lookup = True
    end
    object QryCursoLkpSubDepto: TStringField
      DisplayLabel = 'Sub-Depto'
      FieldKind = fkLookup
      FieldName = 'LkpSubDepto'
      LookupDataSet = QryPesqCoordCurso
      LookupKeyFields = 'CO_COOR_CUR'
      LookupResultField = 'NO_COOR_CUR'
      KeyFields = 'CO_SUB_DPTO_CUR'
      LookupCache = True
      Size = 40
      Lookup = True
    end
    object QryCursoCO_DPTO_CUR: TIntegerField
      FieldName = 'CO_DPTO_CUR'
    end
    object QryCursoCO_SIGL_CUR: TStringField
      FieldName = 'CO_SIGL_CUR'
      Size = 6
    end
    object QryCursoCO_SUB_DPTO_CUR: TIntegerField
      FieldName = 'CO_SUB_DPTO_CUR'
    end
    object QryCursoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryCursoDE_INF_LEG_CUR: TStringField
      FieldName = 'DE_INF_LEG_CUR'
      Size = 200
    end
    object QryCursoPE_FALT_CUR: TBCDField
      FieldName = 'PE_FALT_CUR'
      Precision = 4
      Size = 2
    end
    object QryCursoQT_MATE_MAT: TIntegerField
      FieldName = 'QT_MATE_MAT'
    end
    object QryCursoQT_MAT_DEP_MAT: TIntegerField
      FieldName = 'QT_MAT_DEP_MAT'
    end
    object QryCursoCO_MODU_CUR: TIntegerField
      FieldName = 'CO_MODU_CUR'
    end
    object QryCursoNO_REFER: TStringField
      FieldName = 'NO_REFER'
      Size = 50
    end
    object QryCursoCO_SIGL_REFER: TStringField
      FieldName = 'CO_SIGL_REFER'
      Size = 8
    end
    object QryCursoCO_COOR: TIntegerField
      FieldName = 'CO_COOR'
    end
    object QryCursoMED_FINAL_CUR: TBCDField
      FieldName = 'MED_FINAL_CUR'
      Precision = 4
      Size = 2
    end
    object QryCursoFLA_CAL_PTES: TStringField
      FieldName = 'FLA_CAL_PTES'
      FixedChar = True
      Size = 1
    end
    object QryCursoTIP_OPE_PTES: TStringField
      FieldName = 'TIP_OPE_PTES'
      FixedChar = True
      Size = 1
    end
    object QryCursoVL_OPE_CTES: TBCDField
      FieldName = 'VL_OPE_CTES'
      Precision = 6
      Size = 2
    end
    object QryCursoFLA_CAL_PMEN: TStringField
      FieldName = 'FLA_CAL_PMEN'
      FixedChar = True
      Size = 1
    end
    object QryCursoTIP_OPE_PMEN: TStringField
      FieldName = 'TIP_OPE_PMEN'
      FixedChar = True
      Size = 1
    end
    object QryCursoVL_OPE_CMEN: TBCDField
      FieldName = 'VL_OPE_CMEN'
      Precision = 6
      Size = 2
    end
    object QryCursoFLA_CAL_PBIM: TStringField
      FieldName = 'FLA_CAL_PBIM'
      FixedChar = True
      Size = 1
    end
    object QryCursoTIP_OPE_PBIM: TStringField
      FieldName = 'TIP_OPE_PBIM'
      FixedChar = True
      Size = 1
    end
    object QryCursoVL_OPE_CBIM: TBCDField
      FieldName = 'VL_OPE_CBIM'
      Precision = 6
      Size = 2
    end
    object QryCursoFLA_CAL_PFIN: TStringField
      FieldName = 'FLA_CAL_PFIN'
      FixedChar = True
      Size = 1
    end
    object QryCursoTIP_OPE_PFIN: TStringField
      FieldName = 'TIP_OPE_PFIN'
      FixedChar = True
      Size = 1
    end
    object QryCursoVL_OPE_CFIN: TBCDField
      FieldName = 'VL_OPE_CFIN'
      Precision = 6
      Size = 2
    end
    object QryCursoTIP_OPE_MFIN: TStringField
      FieldName = 'TIP_OPE_MFIN'
      FixedChar = True
      Size = 1
    end
    object QryCursoVL_OPE_MFIN: TBCDField
      FieldName = 'VL_OPE_MFIN'
      Precision = 6
      Size = 2
    end
    object QryCursoQT_AULA_CUR: TIntegerField
      FieldName = 'QT_AULA_CUR'
    end
    object QryCursoNU_PORTA_CUR: TStringField
      FieldName = 'NU_PORTA_CUR'
      Size = 15
    end
    object QryCursoNU_DOU_CUR: TStringField
      FieldName = 'NU_DOU_CUR'
      Size = 15
    end
    object QryCursoVL_PC_DECPONTO: TBCDField
      FieldName = 'VL_PC_DECPONTO'
      Precision = 8
      Size = 2
    end
    object QryCursoVL_PC_DESPROMO: TBCDField
      FieldName = 'VL_PC_DESPROMO'
      Precision = 8
      Size = 2
    end
    object QryCursoCO_MDP_VEST: TIntegerField
      FieldName = 'CO_MDP_VEST'
    end
    object QryCursoCO_PREDEC_CUR: TIntegerField
      FieldName = 'CO_PREDEC_CUR'
    end
    object QryCursoSEQ_IMPRESSAO: TIntegerField
      FieldName = 'SEQ_IMPRESSAO'
    end
  end
  object QryPesqCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_MODU_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      
        'Select CO_EMP, CO_CUR, NO_CUR, VL_TOTA_CUR, CO_IDENDES_CUR,CO_PA' +
        'RAM_FREQUE'
      'From TB01_CURSO'
      'WHERE CO_EMP = :P_CO_EMP'
      'and CO_MODU_CUR = :P_CO_MODU_CUR'
      'order by CO_NIVEL_CUR, NO_CUR')
    Left = 532
    Top = 9
    object AutoIncField1: TAutoIncField
      FieldName = 'CO_CUR'
      ReadOnly = True
    end
    object StringField3: TStringField
      FieldName = 'NO_CUR'
      Size = 40
    end
    object QryPesqCursoVL_TOTA_CUR: TBCDField
      FieldName = 'VL_TOTA_CUR'
      Precision = 7
      Size = 2
    end
    object QryPesqCursoCO_IDENDES_CUR: TStringField
      FieldName = 'CO_IDENDES_CUR'
      FixedChar = True
      Size = 1
    end
    object QryPesqCursoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesqCursoCO_PARAM_FREQUE: TStringField
      FieldName = 'CO_PARAM_FREQUE'
      FixedChar = True
      Size = 1
    end
  end
  object QryMateria: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'p_co_cur'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'p_co_mat'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select *'
      'from TB02_MATERIA'
      'where co_cur = :p_co_cur and'
      '           co_mat = :p_co_mat and'
      '           CO_EMP = :P_CO_EMP')
    Left = 24
    Top = 120
    object QryMateriaCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryMateriaCO_MAT: TAutoIncField
      DisplayLabel = 'C'#243'd Materia'
      FieldName = 'CO_MAT'
      ReadOnly = True
    end
    object QryMateriaQT_CARG_HORA_MAT: TIntegerField
      DisplayLabel = 'Carga Hor'#225'ria'
      FieldName = 'QT_CARG_HORA_MAT'
    end
    object QryMateriaDT_INCL_MAT: TDateTimeField
      DisplayLabel = 'Data de Inclus'#227'o'
      FieldName = 'DT_INCL_MAT'
    end
    object QryMateriaDT_SITU_MAT: TDateTimeField
      DisplayLabel = 'Data da Situa'#231#227'o'
      FieldName = 'DT_SITU_MAT'
    end
    object QryMateriaLkpCurso: TStringField
      DisplayLabel = 'Curso'
      FieldKind = fkLookup
      FieldName = 'LkpCurso'
      LookupDataSet = QryPesqCurso
      LookupKeyFields = 'CO_CUR'
      LookupResultField = 'NO_CUR'
      KeyFields = 'CO_CUR'
      LookupCache = True
      Size = 60
      Lookup = True
    end
    object QryMateriaCO_SITU_MAT: TStringField
      FieldName = 'CO_SITU_MAT'
      FixedChar = True
      Size = 1
    end
    object QryMateriaQT_CRED_MAT: TIntegerField
      DisplayLabel = 'Qtde Cr'#233'dito'
      FieldName = 'QT_CRED_MAT'
    end
    object QryMateriaNO_MAT: TStringField
      DisplayLabel = 'Nome da Disciplina'
      FieldName = 'NO_MAT'
      Size = 80
    end
    object QryMateriaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryMateriaVL_CRED_MAT: TBCDField
      FieldName = 'VL_CRED_MAT'
      DisplayFormat = '#,##0.00'
      Precision = 7
      Size = 2
    end
    object QryMateriaCO_SIGL_MAT: TStringField
      DisplayLabel = 'Sigla'
      FieldName = 'CO_SIGL_MAT'
      FixedChar = True
      Size = 7
    end
    object QryMateriaDE_MAT: TStringField
      DisplayLabel = 'Descri'#231#227'o da Materia'
      FieldName = 'DE_MAT'
      Size = 500
    end
  end
  object QryPesqMateria: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB02_MATERIA'
      'order by NO_MAT')
    Left = 464
    Top = 112
    object QryPesqMateriaCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryPesqMateriaCO_MAT: TAutoIncField
      FieldName = 'CO_MAT'
      ReadOnly = True
    end
    object QryPesqMateriaNO_MAT: TStringField
      FieldName = 'NO_MAT'
      Size = 80
    end
    object QryPesqMateriaDE_MAT: TStringField
      FieldName = 'DE_MAT'
      Size = 200
    end
    object QryPesqMateriaQT_CARG_HORA_MAT: TIntegerField
      FieldName = 'QT_CARG_HORA_MAT'
    end
    object QryPesqMateriaDT_INCL_MAT: TDateTimeField
      FieldName = 'DT_INCL_MAT'
    end
    object QryPesqMateriaDT_SITU_MAT: TDateTimeField
      FieldName = 'DT_SITU_MAT'
    end
    object QryPesqMateriaCO_SIGL_MAT: TStringField
      DisplayLabel = 'Sigla'
      FieldName = 'CO_SIGL_MAT'
      FixedChar = True
      Size = 5
    end
    object QryPesqMateriaQT_CRED_MAT: TIntegerField
      DisplayLabel = 'Qtde Cr'#233'dito'
      FieldName = 'QT_CRED_MAT'
    end
    object QryPesqMateriaCO_SITU_MAT: TStringField
      FieldName = 'CO_SITU_MAT'
      FixedChar = True
      Size = 1
    end
    object QryPesqMateriaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesqMateriaVL_CRED_MAT: TBCDField
      FieldName = 'VL_CRED_MAT'
      Precision = 7
      Size = 2
    end
  end
  object QryPesqPreRequisito: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_MAT_PRE'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select *'
      'from TB02_MATERIA M, TB107_CADMATERIAS CM'
      'where  M.CO_MAT <> :P_CO_MAT_PRE AND'
      '            M.CO_EMP = :P_CO_EMP AND'
      '            M.CO_CUR = :P_CO_CUR AND'
      '            CM.CO_EMP = M.CO_EMP AND'
      '            CM.ID_MATERIA = M.ID_MATERIA'
      'ORDER BY CM.NO_MATERIA')
    Left = 708
    Top = 136
    object QryPesqPreRequisitoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesqPreRequisitoCO_MAT: TAutoIncField
      FieldName = 'CO_MAT'
      ReadOnly = True
    end
    object QryPesqPreRequisitoQT_CARG_HORA_MAT: TIntegerField
      FieldName = 'QT_CARG_HORA_MAT'
    end
    object QryPesqPreRequisitoQT_CRED_MAT: TIntegerField
      FieldName = 'QT_CRED_MAT'
    end
    object QryPesqPreRequisitoCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryPesqPreRequisitoDT_INCL_MAT: TDateTimeField
      FieldName = 'DT_INCL_MAT'
    end
    object QryPesqPreRequisitoCO_SITU_MAT: TStringField
      FieldName = 'CO_SITU_MAT'
      FixedChar = True
      Size = 1
    end
    object QryPesqPreRequisitoDT_SITU_MAT: TDateTimeField
      FieldName = 'DT_SITU_MAT'
    end
    object QryPesqPreRequisitoVL_CRED_MAT: TBCDField
      FieldName = 'VL_CRED_MAT'
      Precision = 7
      Size = 2
    end
    object QryPesqPreRequisitoID_MATERIA: TIntegerField
      FieldName = 'ID_MATERIA'
    end
    object QryPesqPreRequisitoCO_EMP_1: TIntegerField
      FieldName = 'CO_EMP_1'
    end
    object QryPesqPreRequisitoID_MATERIA_1: TAutoIncField
      FieldName = 'ID_MATERIA_1'
      ReadOnly = True
    end
    object QryPesqPreRequisitoNO_SIGLA_MATERIA: TStringField
      FieldName = 'NO_SIGLA_MATERIA'
      Size = 6
    end
    object QryPesqPreRequisitoNO_MATERIA: TStringField
      FieldName = 'NO_MATERIA'
      Size = 30
    end
    object QryPesqPreRequisitoDE_MATERIA: TStringField
      FieldName = 'DE_MATERIA'
      Size = 100
    end
    object QryPesqPreRequisitoCO_STATUS: TStringField
      FieldName = 'CO_STATUS'
      FixedChar = True
      Size = 1
    end
    object QryPesqPreRequisitoDT_STATUS: TDateTimeField
      FieldName = 'DT_STATUS'
    end
  end
  object QryPesqTipoColaborador: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB21_TIPOCAL')
    Left = 602
    Top = 144
    object QryPesqTipoColaboradorCO_TPCAL: TAutoIncField
      FieldName = 'CO_TPCAL'
      ReadOnly = True
    end
    object QryPesqTipoColaboradorNO_TPCAL: TStringField
      FieldName = 'NO_TPCAL'
      Size = 40
    end
  end
  object QryPesqTipoCalc: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB21_TIPOCAL')
    Left = 628
    Top = 296
    object QryPesqTipoCalcCO_TPCAL: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TPCAL'
      ReadOnly = True
    end
    object QryPesqTipoCalcNO_TPCAL: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TPCAL'
      Size = 40
    end
    object QryPesqTipoCalcCO_SIGLA_TPCAL: TStringField
      FieldName = 'CO_SIGLA_TPCAL'
      FixedChar = True
      Size = 1
    end
  end
  object QryPesqFuncao: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB15_FUNCAO'
      'order by NO_FUN')
    Left = 532
    Top = 248
    object QryPesqFuncaoCO_FUN: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_FUN'
      ReadOnly = True
    end
    object QryPesqFuncaoNO_FUN: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_FUN'
      Size = 40
    end
  end
  object QryPesqDepartamento: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB14_DEPTO')
    Left = 628
    Top = 248
    object QryPesqDepartamentoCO_DEPTO: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_DEPTO'
      ReadOnly = True
    end
    object QryPesqDepartamentoNO_DEPTO: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_DEPTO'
      Size = 40
    end
  end
  object QryPesqGrauInst: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB18_GRAUINS')
    Left = 540
    Top = 304
    object QryPesqGrauInstCO_INST: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_INST'
      ReadOnly = True
    end
    object QryPesqGrauInstNO_INST: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_INST'
      Size = 40
    end
    object QryPesqGrauInstCO_SIGLA_INST: TStringField
      FieldName = 'CO_SIGLA_INST'
      FixedChar = True
      Size = 2
    end
  end
  object QryPesqContrato: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB20_TIPOCON')
    Left = 628
    Top = 352
    object QryPesqContratoCO_TPCON: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TPCON'
      ReadOnly = True
    end
    object QryPesqContratoNO_TPCON: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TPCON'
      Size = 40
    end
  end
  object QryPesqUFCidade: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select * From TB74_UF')
    Left = 556
    Top = 188
    object QryPesqUFCidadeCODUF: TStringField
      DisplayLabel = 'UF'
      FieldName = 'CODUF'
      FixedChar = True
      Size = 2
    end
    object QryPesqUFCidadeDESCRICAOUF: TStringField
      FieldName = 'DESCRICAOUF'
      FixedChar = True
      Size = 30
    end
  end
  object QryPesqUFRG: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT * FROM TB74_UF')
    Left = 452
    Top = 288
    object QryPesqUFRGCODUF: TStringField
      DisplayLabel = 'UF'
      FieldName = 'CODUF'
      FixedChar = True
      Size = 2
    end
    object QryPesqUFRGDESCRICAOUF: TStringField
      FieldName = 'DESCRICAOUF'
      FixedChar = True
      Size = 30
    end
  end
  object QryPesqTipoPag: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB22_TIPOPAG')
    Left = 540
    Top = 408
    object QryPesqTipoPagCO_TIPO_PAGA: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_PAGA'
      ReadOnly = True
    end
    object QryPesqTipoPagNO_TIPO_PAGA: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPO_PAGA'
      Size = 40
    end
    object QryPesqTipoPagNU_PARC_PAGA: TIntegerField
      DisplayLabel = 'Nr'#186' Parcela(s)'
      FieldName = 'NU_PARC_PAGA'
    end
    object QryPesqTipoPagPE_CORR_PARC: TBCDField
      FieldName = 'PE_CORR_PARC'
      Precision = 7
      Size = 3
    end
    object QryPesqTipoPagQT_DIAS_PARC: TIntegerField
      DisplayLabel = 'Qtde Dias Parcela'
      FieldName = 'QT_DIAS_PARC'
    end
    object QryPesqTipoPagCO_INDIENT_PARC: TStringField
      FieldName = 'CO_INDIENT_PARC'
      FixedChar = True
      Size = 1
    end
  end
  object QrySituacao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB16_SITUACA')
    Left = 24
    Top = 240
    object QrySituacaoCO_SITU: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_SITU'
      ReadOnly = True
    end
    object QrySituacaoNO_SITU: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_SITU'
      Size = 15
    end
    object QrySituacaoDT_SITU_SIT: TDateTimeField
      DisplayLabel = 'Data'
      FieldName = 'DT_SITU_SIT'
      DisplayFormat = 'dd/mm/yyyy'
    end
  end
  object QryFuncao: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    OnCalcFields = QryFuncaoCalcFields
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB15_FUNCAO'
      'order by NO_FUN')
    Left = 88
    Top = 240
    object QryFuncaoCO_FUN: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_FUN'
      ReadOnly = True
    end
    object QryFuncaoMAG: TStringField
      FieldKind = fkCalculated
      FieldName = 'MAG'
      Calculated = True
    end
    object QryFuncaoOPE: TStringField
      FieldKind = fkCalculated
      FieldName = 'OPE'
      Calculated = True
    end
    object QryFuncaoADM: TStringField
      FieldKind = fkCalculated
      FieldName = 'ADM'
      Calculated = True
    end
    object QryFuncaoNUC: TStringField
      FieldKind = fkCalculated
      FieldName = 'NUC'
      Calculated = True
    end
    object QryFuncaoNO_FUN: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_FUN'
      Size = 40
    end
    object QryFuncaoCO_FLAG_CLASSI_MAGIST: TBooleanField
      FieldName = 'CO_FLAG_CLASSI_MAGIST'
    end
    object QryFuncaoCO_FLAG_CLASSI_ADMINI: TBooleanField
      FieldName = 'CO_FLAG_CLASSI_ADMINI'
    end
    object QryFuncaoCO_FLAG_CLASSI_OPERAC: TBooleanField
      FieldName = 'CO_FLAG_CLASSI_OPERAC'
    end
    object QryFuncaoCO_FLAG_CLASSI_NUCLEO: TBooleanField
      FieldName = 'CO_FLAG_CLASSI_NUCLEO'
    end
  end
  object QryDerpatamento: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB14_DEPTO')
    Left = 88
    Top = 72
    object QryDerpatamentoCO_DEPTO: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_DEPTO'
      ReadOnly = True
    end
    object QryDerpatamentoNO_DEPTO: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_DEPTO'
      Size = 40
    end
    object QryDerpatamentoCO_SIGLA_DEPTO: TStringField
      FieldName = 'CO_SIGLA_DEPTO'
      Size = 12
    end
  end
  object QryGrauInst: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB18_GRAUINS')
    Left = 24
    Top = 304
    object QryGrauInstCO_INST: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_INST'
      ReadOnly = True
    end
    object QryGrauInstNO_INST: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_INST'
      Size = 40
    end
  end
  object QryTipoColaborador: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB19_TIPOCOL')
    Left = 96
    Top = 288
    object QryTipoColaboradorCO_TPCOL: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TPCOL'
      ReadOnly = True
    end
    object QryTipoColaboradorNO_TPCOL: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TPCOL'
      Size = 40
    end
  end
  object QryTipoContrato: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB20_TIPOCON')
    Left = 104
    Top = 336
    object QryTipoContratoCO_TPCON: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TPCON'
      ReadOnly = True
    end
    object QryTipoContratoNO_TPCON: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TPCON'
      Size = 40
    end
  end
  object QryTipoCalculo: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB21_TIPOCAL'
      'order by NO_TPCAL')
    Left = 31
    Top = 352
    object QryTipoCalculoCO_TPCAL: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TPCAL'
      ReadOnly = True
    end
    object QryTipoCalculoNO_TPCAL: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TPCAL'
      Size = 40
    end
  end
  object QryTipoParcela: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB22_TIPOPAG'
      'order by NO_TIPO_PAGA')
    Left = 24
    Top = 408
    object QryTipoParcelaCO_TIPO_PAGA: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_PAGA'
      ReadOnly = True
    end
    object QryTipoParcelaNO_TIPO_PAGA: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPO_PAGA'
      Size = 40
    end
    object QryTipoParcelaNU_PARC_PAGA: TIntegerField
      DisplayLabel = 'Nr'#186' de Parcela'
      FieldName = 'NU_PARC_PAGA'
    end
    object QryTipoParcelaPE_CORR_PARC: TBCDField
      DisplayLabel = '% Corre'#231#227'o Parcela'
      FieldName = 'PE_CORR_PARC'
      DisplayFormat = '#,##0.00'
      Precision = 7
      Size = 3
    end
    object QryTipoParcelaCO_INDIENT_PARC: TStringField
      DisplayLabel = 'Receber Valor de Entrada'
      FieldName = 'CO_INDIENT_PARC'
      FixedChar = True
      Size = 1
    end
    object QryTipoParcelaQT_DIAS_PARC: TIntegerField
      DisplayLabel = 'Qtde Dias Parcela'
      FieldName = 'QT_DIAS_PARC'
    end
  end
  object QryGrauDificuldade: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB23_GRAUDIF')
    Left = 96
    Top = 392
    object QryGrauDificuldadeCO_GRAUDIF: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_GRAUDIF'
      ReadOnly = True
    end
    object QryGrauDificuldadeNO_GRAUDIF: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_GRAUDIF'
    end
  end
  object QryPesqGrauDif: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB23_GRAUDIF')
    Left = 540
    Top = 360
    object QryPesqGrauDifCO_GRAUDIF: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_GRAUDIF'
      ReadOnly = True
    end
    object QryPesqGrauDifNO_GRAUDIF: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_GRAUDIF'
    end
  end
  object QryTipoEmpresa: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB24_TPEMPRESA')
    Left = 159
    Top = 210
    object QryTipoEmpresaCO_TIPOEMP: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPOEMP'
    end
    object QryTipoEmpresaNO_TIPOEMP: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPOEMP'
      Size = 30
    end
    object QryTipoEmpresaCL_CLAS_EMP: TStringField
      FieldName = 'CL_CLAS_EMP'
      FixedChar = True
      Size = 1
    end
  end
  object QryPesqTipoEmpresa: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB24_TPEMPRESA')
    Left = 636
    Top = 400
    object QryPesqTipoEmpresaCO_TIPOEMP: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPOEMP'
    end
    object QryPesqTipoEmpresaNO_TIPOEMP: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPOEMP'
      Size = 30
    end
    object QryPesqTipoEmpresaCL_CLAS_EMP: TStringField
      FieldName = 'CL_CLAS_EMP'
      FixedChar = True
      Size = 1
    end
  end
  object QryPesqMotivoCancel: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB57_MOTIVCANC'
      'order by de_moti_canc')
    Left = 628
    Top = 200
    object QryPesqMotivoCancelCO_MOTI_CANC: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MOTI_CANC'
    end
    object QryPesqMotivoCancelDE_MOTI_CANC: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MOTI_CANC'
      Size = 60
    end
  end
  object QryMotivoCancel: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB57_MOTIVCANC')
    Left = 95
    Top = 130
    object QryMotivoCancelCO_MOTI_CANC: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MOTI_CANC'
    end
    object QryMotivoCancelDE_MOTI_CANC: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MOTI_CANC'
      Size = 60
    end
  end
  object QryTipoReserva: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB29_TIPORESERVA')
    Left = 159
    Top = 258
    object QryTipoReservaCO_RESERVA: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_RESERVA'
      ReadOnly = True
    end
    object QryTipoReservaNO_RESERVA: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_RESERVA'
      Size = 35
    end
  end
  object QryPesqTipoReserva: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB29_TIPORESERVA')
    Left = 708
    Top = 184
    object QryPesqTipoReservaCO_RESERVA: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_RESERVA'
      ReadOnly = True
    end
    object QryPesqTipoReservaNO_RESERVA: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_RESERVA'
      Size = 35
    end
  end
  object QryAutor: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB34_AUTOR')
    Left = 143
    Top = 50
    object QryAutorCO_AUTOR: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_AUTOR'
    end
    object QryAutorNO_AUTOR: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_AUTOR'
      Size = 50
    end
  end
  object QryClassificacao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB32_CLASSIF_ACER')
    Left = 175
    Top = 106
    object QryClassificacaoCO_CLAS_ACER: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_CLAS_ACER'
      ReadOnly = True
    end
    object QryClassificacaoNO_CLAS_ACER: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_CLAS_ACER'
      Size = 60
    end
  end
  object QryAreaConhecimento: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB31_AREA_CONHEC')
    Left = 232
    Top = 54
    object QryAreaConhecimentoCO_AREACON: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_AREACON'
      ReadOnly = True
    end
    object QryAreaConhecimentoNO_AREACON: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_AREACON'
      Size = 40
    end
  end
  object QryEditora: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB33_EDITORA')
    Left = 175
    Top = 154
    object QryEditoraCO_EDITORA: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_EDITORA'
    end
    object QryEditoraNO_EDITORA: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_EDITORA'
      Size = 30
    end
  end
  object QryPesqAutor: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB34_AUTOR'
      'order by NO_AUTOR')
    Left = 727
    Top = 256
    object IntegerField5: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_AUTOR'
    end
    object StringField2: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_AUTOR'
      Size = 50
    end
  end
  object QryPesqClass: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB32_CLASSIF_ACER')
    Left = 727
    Top = 360
    object AutoIncField2: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_CLAS_ACER'
      ReadOnly = True
    end
    object StringField6: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_CLAS_ACER'
      Size = 60
    end
  end
  object QryPesqAreaCon: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB31_AREA_CONHEC')
    Left = 719
    Top = 304
    object AutoIncField3: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_AREACON'
      ReadOnly = True
    end
    object StringField7: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_AREACON'
      Size = 40
    end
  end
  object QryPesqEditora: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT *'
      'FROM TB33_EDITORA')
    Left = 719
    Top = 416
    object IntegerField6: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_EDITORA'
    end
    object StringField8: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_EDITORA'
      Size = 30
    end
  end
  object QryRecQtdPendFin: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_ALU'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      ''
      'select CO_EMP, COUNT(*)  AS TOTPENDFIN'
      'from TB47_CTA_RECEB'
      'where CO_ALU = :P_CO_ALU AND'
      '      DT_VEN_DOC < GETDATE() AND'
      '      DT_REC_DOC IS NULL AND'
      '      CO_EMP = :P_CO_EMP AND'
      '      IC_SIT_DOC = '#39'A'#39
      'GROUP BY CO_EMP')
    Left = 703
    object QryRecQtdPendFinTOTPENDFIN: TIntegerField
      FieldName = 'TOTPENDFIN'
      ReadOnly = True
    end
    object QryRecQtdPendFinCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryRecQtdPendBibli: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_USUA_ACER'
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      ''
      'SELECT COUNT(*) TOTPENDBIBLI'
      'FROM TB123_EMPR_BIB_ITENS'
      'where CO_USUA_ACER = :P_CO_USUA_ACER AND'
      '      DT_REAL_DEVO_ACER IS NULL AND'
      '      DT_PREV_DEVO_ACER < GETDATE() AND'
      '      TP_USUA_ACER = '#39'A'#39
      ' ')
    Left = 703
    Top = 42
    object QryRecQtdPendBibliTOTPENDBIBLI: TIntegerField
      FieldName = 'TOTPENDBIBLI'
      ReadOnly = True
    end
  end
  object QryTotMatRes: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_ALUMAT'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_ALURES'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      ''
      'SELECT'
      '(select count(*) TOTMAT'
      'from TB08_MATRCUR'
      'where CO_ALU = :P_CO_ALUMAT)  AS TOTMAT,'
      '('
      'select count(*) TOTRES'
      'from TB52_RESERVMAT'
      'where CO_ALU = :P_CO_ALURES AND'
      '           CO_SIT_RESMAT = '#39'A'#39')  AS TOTRES')
    Left = 707
    Top = 87
    object QryTotMatResTOTMAT: TIntegerField
      FieldName = 'TOTMAT'
      ReadOnly = True
    end
    object QryTotMatResTOTRES: TIntegerField
      FieldName = 'TOTRES'
      ReadOnly = True
    end
  end
  object QryMotTrancamento: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB67_MOTIVTRANC')
    Left = 287
    Top = 1
    object QryMotTrancamentoCO_MOTI_TRAN_MAT: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MOTI_TRAN_MAT'
    end
    object QryMotTrancamentoDE_MOTI_TRAN_MAT: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MOTI_TRAN_MAT'
      Size = 60
    end
  end
  object QryPesqMotTrancamento: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB67_MOTIVTRANC')
    Left = 607
    Top = 49
    object QryPesqMotTrancamentoCO_MOTI_TRAN_MAT: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MOTI_TRAN_MAT'
    end
    object QryPesqMotTrancamentoDE_MOTI_TRAN_MAT: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MOTI_TRAN_MAT'
      Size = 60
    end
  end
  object QryModuloCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB44_MODULO')
    Left = 32
    Top = 176
    object QryModuloCursoCO_MODU_CUR: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MODU_CUR'
    end
    object QryModuloCursoDE_MODU_CUR: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MODU_CUR'
      Size = 60
    end
  end
  object QryPesqModuloCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB44_MODULO')
    Left = 441
    Top = 8
    object IntegerField1: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_MODU_CUR'
    end
    object StringField1: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_MODU_CUR'
      Size = 60
    end
  end
  object QryDataAtualServidor: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select GetDate() DataAtual')
    Left = 608
    Top = 96
    object QryDataAtualServidorDataAtual: TDateTimeField
      FieldName = 'DataAtual'
      ReadOnly = True
    end
  end
  object QryPesquisaCurso: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_MODU_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select CO_EMP,  CO_CUR, NO_CUR'
      'From TB01_CURSO'
      'WHERE CO_EMP = :P_CO_EMP'
      'AND CO_MODU_CUR = :P_CO_MODU_CUR'
      'order by CO_NIVEL_CUR, NO_CUR')
    Left = 608
    object QryPesquisaCursoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesquisaCursoCO_CUR: TAutoIncField
      FieldName = 'CO_CUR'
      ReadOnly = True
    end
    object QryPesquisaCursoNO_CUR: TStringField
      FieldName = 'NO_CUR'
      Size = 100
    end
  end
  object QryPesquisaTurma: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'CO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_MODU_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      
        'SELECT B.CO_EMP,  B.CO_TUR, CT.NO_TURMA, B.CO_PERI_TUR, CT.CO_SI' +
        'GLA_TURMA,'
      '   TURNO = (CASE B.CO_PERI_TUR'
      #9#9'WHEN '#39'M'#39' THEN '#39'Matutino'#39
      #9#9'WHEN '#39'V'#39' THEN '#39'Vespertino'#39
      #9#9'WHEN '#39'N'#39' THEN '#39'Noturno'#39
      #9' END)'
      '  FROM TB06_TURMAS B'
      '  JOIN TB129_CADTURMAS CT ON B.CO_TUR = CT.CO_TUR'
      '  WHERE B.CO_CUR = :CO_CUR AND'
      '  B.CO_EMP = :P_CO_EMP AND'
      '  B.CO_MODU_CUR = :P_CO_MODU_CUR AND'
      '  B.CO_MODU_CUR = CT.CO_MODU_CUR'
      'order by ct.no_turma')
    Left = 505
    Top = 60
    object QryPesquisaTurmaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesquisaTurmaCO_TUR: TAutoIncField
      FieldName = 'CO_TUR'
      ReadOnly = True
    end
    object QryPesquisaTurmaCO_PERI_TUR: TStringField
      FieldName = 'CO_PERI_TUR'
      FixedChar = True
      Size = 1
    end
    object QryPesquisaTurmaTURNO: TStringField
      FieldName = 'TURNO'
      ReadOnly = True
      Size = 10
    end
    object QryPesquisaTurmaNO_TURMA: TStringField
      FieldName = 'NO_TURMA'
      Size = 40
    end
    object QryPesquisaTurmaCO_SIGLA_TURMA: TStringField
      FieldName = 'CO_SIGLA_TURMA'
      Size = 10
    end
  end
  object QryTipoIdioma: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB60_TIPO_IDIOMA')
    Left = 96
    Top = 192
    object QryTipoIdiomaCO_IDIOM: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_IDIOM'
      ReadOnly = True
    end
    object QryTipoIdiomaNO_IDIOM: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_IDIOM'
      Size = 40
    end
  end
  object QryCursoFormacao: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_COL'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_ESPEC'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select *'
      'from TB62_CURSO_FORM'
      'where co_emp = :P_CO_EMP'
      'and co_col = :P_CO_COL'
      'and co_espec = :P_CO_ESPEC')
    Left = 160
    Top = 416
    object QryCursoFormacaoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryCursoFormacaoCO_COL: TIntegerField
      FieldName = 'CO_COL'
    end
    object QryCursoFormacaoCO_ESPEC: TIntegerField
      FieldName = 'CO_ESPEC'
    end
    object QryCursoFormacaoNU_CARGA_HORARIA: TIntegerField
      FieldName = 'NU_CARGA_HORARIA'
    end
    object QryCursoFormacaoCO_MESANO_INICIO: TStringField
      FieldName = 'CO_MESANO_INICIO'
      FixedChar = True
      Size = 7
    end
    object QryCursoFormacaoCO_MESANO_FIM: TStringField
      FieldName = 'CO_MESANO_FIM'
      FixedChar = True
      Size = 7
    end
    object QryCursoFormacaoNO_INSTIT_CURSO: TStringField
      FieldName = 'NO_INSTIT_CURSO'
      Size = 65
    end
    object QryCursoFormacaoNO_SIGLA_INSTIT_CURSO: TStringField
      FieldName = 'NO_SIGLA_INSTIT_CURSO'
      Size = 12
    end
    object QryCursoFormacaoNO_CIDADE_CURSO: TStringField
      FieldName = 'NO_CIDADE_CURSO'
      Size = 60
    end
    object QryCursoFormacaoCO_UF_CURSO: TStringField
      FieldName = 'CO_UF_CURSO'
      FixedChar = True
      Size = 2
    end
    object QryCursoFormacaoCO_FLAG_CURSO_PRINCIPAL: TStringField
      FieldName = 'CO_FLAG_CURSO_PRINCIPAL'
      FixedChar = True
      Size = 1
    end
  end
  object QryPesqCursoTurma: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_MODU_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select DISTINCT G.CO_EMP, G.CO_CUR, C.NO_CUR '
      ' from TB06_TURMAS G,  TB01_CURSO C'
      ' where G.CO_CUR = C.CO_CUR  AND'
      '      C.CO_EMP = G.CO_EMP AND'
      '      C.CO_EMP = :P_CO_EMP AND'
      '      C.CO_MODU_CUR = :P_CO_MODU_CUR'
      ' order by C.NO_CUR ')
    Left = 528
    Top = 128
    object QryPesqCursoTurmaCO_CUR: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_CUR'
    end
    object QryPesqCursoTurmaNO_CUR: TStringField
      DisplayLabel = 'Curso'
      FieldName = 'NO_CUR'
      Size = 40
    end
    object QryPesqCursoTurmaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryPesqCursoMatricula: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select DISTINCT M.CO_EMP, M.CO_CUR, C.NO_CUR'
      'from TB08_MATRCUR M, TB01_CURSO C'
      'WHERE M.CO_CUR = C.CO_CUR AND'
      '               M.CO_EMP = C.CO_EMP AND'
      '               M.CO_EMP = :P_CO_EMP')
    Left = 464
    Top = 224
    object QryPesqCursoMatriculaCO_CUR: TIntegerField
      DisplayLabel = 'Curso'
      FieldName = 'CO_CUR'
    end
    object QryPesqCursoMatriculaNO_CUR: TStringField
      DisplayLabel = 'Matr'#237'cula'
      FieldName = 'NO_CUR'
      Size = 40
    end
    object QryPesqCursoMatriculaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryPesqTurmaMatricula: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      ''
      'select DISTINCT M.CO_EMP, M.CO_TUR, T.NO_TUR'
      'from TB08_MATRCUR M, TB01_CURSO C, TB06_TURMAS T'
      'WHERE M.CO_CUR = C.CO_CUR AND'
      '      M.CO_CUR = T.CO_CUR AND'
      '      M.CO_TUR = T.CO_TUR AND '
      '      M.CO_CUR = :P_CO_CUR AND'
      '      M.CO_EMP = :P_CO_EMP AND'
      '      M.CO_EMP = C.CO_EMP AND'
      '      M.CO_EMP = T.CO_EMP')
    Left = 458
    Top = 166
    object QryPesqTurmaMatriculaCO_TUR: TAutoIncField
      FieldName = 'CO_TUR'
      ReadOnly = True
    end
    object QryPesqTurmaMatriculaNO_TUR: TStringField
      FieldName = 'NO_TUR'
      Size = 40
    end
    object QryPesqTurmaMatriculaCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryPesqTipoAvaliacao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select CO_TIPO_AVAL, NO_TIPO_AVAL, CO_ESTI_AVAL'
      'from TB73_TIPO_AVAL'
      'order by NO_TIPO_AVAL')
    Left = 456
    Top = 392
    object QryPesqTipoAvaliacaoCO_TIPO_AVAL: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_AVAL'
    end
    object QryPesqTipoAvaliacaoNO_TIPO_AVAL: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPO_AVAL'
      Size = 30
    end
    object QryPesqTipoAvaliacaoCO_ESTI_AVAL: TStringField
      FieldName = 'CO_ESTI_AVAL'
      FixedChar = True
      Size = 1
    end
  end
  object QryPesqTipoQuestao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'From TB99_TIPO_QUEST'
      'Order by NO_TITU_QUES')
    Left = 456
    Top = 344
    object QryPesqTipoQuestaoCO_TITU_QUES: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TITU_QUES'
    end
    object QryPesqTipoQuestaoNO_TITU_QUES: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TITU_QUES'
      Size = 60
    end
  end
  object QryPesqDeparCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select *'
      'From TB77_DPTO_CURSO'
      'WHERE CO_EMP = :P_CO_EMP')
    Left = 376
    Top = 56
    object QryPesqDeparCursoCO_DPTO_CUR: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_DPTO_CUR'
      ReadOnly = True
    end
    object QryPesqDeparCursoSG_DPTO_CUR: TStringField
      DisplayLabel = 'Sigla'
      FieldName = 'SG_DPTO_CUR'
      Size = 6
    end
    object QryPesqDeparCursoNO_DPTO_CUR: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_DPTO_CUR'
      Size = 40
    end
  end
  object QryPesqCoordCurso: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_DPTO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select *'
      'From TB68_COORD_CURSO'
      'where CO_DPTO_CUR = :P_CO_DPTO_CUR'
      '          AND CO_EMP = :P_CO_EMP')
    Left = 376
    Top = 104
    object QryPesqCoordCursoCO_DPTO_CUR: TIntegerField
      FieldName = 'CO_DPTO_CUR'
    end
    object QryPesqCoordCursoCO_COOR_CUR: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_COOR_CUR'
      ReadOnly = True
    end
    object QryPesqCoordCursoNO_COOR_CUR: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_COOR_CUR'
      Size = 40
    end
    object QryPesqCoordCursoSG_COOR_CUR: TStringField
      FieldName = 'SG_COOR_CUR'
      Size = 6
    end
    object QryPesqCoordCursoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryPesqDepto: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select *'
      'From TB77_DPTO_CURSO'
      'WHERE CO_EMP = :P_CO_EMP')
    Left = 352
    Top = 160
    object AutoIncField6: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_DPTO_CUR'
      ReadOnly = True
    end
    object StringField4: TStringField
      DisplayLabel = 'Sigla'
      FieldName = 'SG_DPTO_CUR'
      Size = 6
    end
    object StringField5: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_DPTO_CUR'
      Size = 40
    end
    object QryPesqDeptoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
  end
  object QryPesqCoordenacao: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_DPTO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select *'
      'From TB68_COORD_CURSO'
      'where CO_DPTO_CUR = :P_CO_DPTO_CUR'
      'AND CO_EMP = :P_CO_EMP')
    Left = 352
    Top = 208
    object IntegerField2: TIntegerField
      FieldName = 'CO_DPTO_CUR'
    end
    object AutoIncField7: TAutoIncField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_COOR_CUR'
      ReadOnly = True
    end
    object StringField10: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_COOR_CUR'
      Size = 40
    end
    object QryPesqCoordenacaoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesqCoordenacaoSG_COOR_CUR: TStringField
      FieldName = 'SG_COOR_CUR'
      Size = 6
    end
  end
  object QryPesqQtdeEmpAcervo: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'P_CO_ACERVO'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select count(CO_ACERVO) QtdeEmprestimo'
      'from TB123_EMPR_BIB_ITENS'
      'where CO_ACERVO = :P_CO_ACERVO AND'
      '           DT_REAL_DEVO_ACER IS NULL')
    Left = 360
    Top = 264
    object QryPesqQtdeEmpAcervoQtdeEmprestimo: TIntegerField
      FieldName = 'QtdeEmprestimo'
      ReadOnly = True
    end
  end
  object QryPesqTipoSolicitacao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB66_TIPO_SOLIC')
    Left = 360
    Top = 328
    object QryPesqTipoSolicitacaoCO_TIPO_SOLI: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_SOLI'
    end
    object QryPesqTipoSolicitacaoNO_TIPO_SOLI: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPO_SOLI'
      Size = 100
    end
    object QryPesqTipoSolicitacaoVL_UNIT_SOLI: TBCDField
      DisplayLabel = 'Valor'
      FieldName = 'VL_UNIT_SOLI'
      Precision = 5
      Size = 2
    end
    object QryPesqTipoSolicitacaoCO_SITU_SOLI: TStringField
      DisplayLabel = 'Situa'#231#227'o'
      FieldName = 'CO_SITU_SOLI'
      FixedChar = True
      Size = 1
    end
    object QryPesqTipoSolicitacaoDT_SITU_SOLI: TDateTimeField
      DisplayLabel = 'Data Situa'#231#227'o'
      FieldName = 'DT_SITU_SOLI'
    end
    object QryPesqTipoSolicitacaoNO_PROC_EXTE_SOLI: TStringField
      DisplayLabel = 'Procedure Externa'
      FieldName = 'NO_PROC_EXTE_SOLI'
      Size = 40
    end
  end
  object QryTipoSolicitacao: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'from TB66_TIPO_SOLIC'
      'Order by DT_SITU_SOLI DESC')
    Left = 200
    Top = 376
    object IntegerField3: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_SOLI'
    end
    object StringField11: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'NO_TIPO_SOLI'
      Size = 100
    end
    object BCDField1: TBCDField
      DisplayLabel = 'Valor'
      FieldName = 'VL_UNIT_SOLI'
      currency = True
      Precision = 5
      Size = 2
    end
    object StringField13: TStringField
      DisplayLabel = 'Situa'#231#227'o'
      FieldName = 'CO_SITU_SOLI'
      FixedChar = True
      Size = 1
    end
    object DateTimeField1: TDateTimeField
      DisplayLabel = 'Data Situa'#231#227'o'
      FieldName = 'DT_SITU_SOLI'
    end
    object StringField14: TStringField
      DisplayLabel = 'Procedure Externa'
      FieldName = 'NO_PROC_EXTE_SOLI'
      Size = 40
    end
  end
  object QryPesqAval: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <
      item
        Name = 'CO_CUR'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'CO_TUR'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'CO_COL'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'CO_ALU'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'CO_MAT'
        Attributes = [paSigned, paNullable]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select * From TB78_PESQ_AVAL'
      'Where CO_CUR  =  :CO_CUR'
      'And      CO_TUR =  :CO_TUR'
      'And      CO_COL  =  :CO_COL'
      'And      CO_ALU  =  :CO_ALU'
      'And      CO_MAT  =  :CO_MAT')
    Left = 176
    Top = 327
    object QryPesqAvalCO_PESQ_AVAL: TIntegerField
      FieldName = 'CO_PESQ_AVAL'
    end
    object QryPesqAvalDT_AVAL: TDateTimeField
      FieldName = 'DT_AVAL'
    end
    object QryPesqAvalCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryPesqAvalCO_MAT: TIntegerField
      FieldName = 'CO_MAT'
    end
    object QryPesqAvalCO_TUR: TIntegerField
      FieldName = 'CO_TUR'
    end
    object QryPesqAvalCO_COL: TIntegerField
      FieldName = 'CO_COL'
    end
    object QryPesqAvalCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
    object QryPesqAvalDE_SUGE_AVAL: TStringField
      FieldName = 'DE_SUGE_AVAL'
      Size = 500
    end
  end
  object QryPesqDisciplina: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'CO_CUR'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'Select G.CO_CUR, G.CO_MAT, CM.NO_MATERIA'
      
        'From TB43_GRD_CURSO G,  TB01_CURSO C, TB02_MATERIA M, TB107_CADM' +
        'ATERIAS CM'
      'WHERE G.CO_EMP = C.CO_EMP AND G.CO_EMP = M.CO_EMP AND'
      '     G.CO_CUR = C.CO_CUR AND '
      '     G.CO_MAT = M.CO_MAT AND'
      '     G.CO_CUR =  :CO_CUR AND'
      '     M.ID_MATERIA = CM.ID_MATERIA AND'
      '     G.CO_EMP = :P_CO_EMP'
      'Order By CM.NO_MATERIA')
    Left = 360
    Top = 384
    object QryPesqDisciplinaCO_CUR: TIntegerField
      FieldName = 'CO_CUR'
    end
    object QryPesqDisciplinaCO_MAT: TIntegerField
      FieldName = 'CO_MAT'
    end
    object QryPesqDisciplinaNO_MATERIA: TStringField
      FieldName = 'NO_MATERIA'
      Size = 30
    end
  end
  object QrySql2: TADOQuery
    Connection = Conn
    Parameters = <>
    Left = 130
    Top = 3
  end
  object QrySqlInscricao: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select NU_INSC_ALU from TB46_INSCRICAO')
    Left = 200
  end
  object QrySql3: TADOQuery
    Connection = Conn
    Parameters = <>
    Left = 240
    Top = 160
  end
  object QryTipoDocumento: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select *'
      'From TB086_TIPO_DOC'
      'Order by DES_TIPO_DOC')
    Left = 240
    Top = 248
    object QryTipoDocumentoCO_TIPO_DOC: TIntegerField
      DisplayLabel = 'C'#243'digo'
      FieldName = 'CO_TIPO_DOC'
    end
    object QryTipoDocumentoSIG_TIPO_DOC: TStringField
      DisplayLabel = 'Sigla'
      FieldName = 'SIG_TIPO_DOC'
      FixedChar = True
      Size = 3
    end
    object QryTipoDocumentoDES_TIPO_DOC: TStringField
      FieldName = 'DES_TIPO_DOC'
      Size = 80
    end
  end
  object QrySqlEstoque: TADOQuery
    Connection = Conn
    Parameters = <>
    Left = 232
    Top = 312
  end
  object QryPesqEspecializacao: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'Select *'
      'from TB100_ESPECIALIZACAO'
      'order by DE_ESPEC')
    Left = 280
    Top = 408
    object QryPesqEspecializacaoCO_ESPEC: TIntegerField
      DisplayLabel = 'C'#243'digo'
      DisplayWidth = 10
      FieldName = 'CO_ESPEC'
    end
    object QryPesqEspecializacaoDE_ESPEC: TStringField
      DisplayLabel = 'Descri'#231#227'o'
      FieldName = 'DE_ESPEC'
      Size = 60
    end
  end
  object QryPesqAluno: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select * FROM TB07_aluno')
    Left = 32
    Top = 464
    object QryPesqAlunoCO_ALU: TIntegerField
      FieldName = 'CO_ALU'
    end
    object QryPesqAlunoCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object QryPesqAlunoNO_ALU: TStringField
      FieldName = 'NO_ALU'
      Size = 60
    end
    object QryPesqAlunoCO_ALU_CAD: TStringField
      FieldName = 'CO_ALU_CAD'
      Size = 15
    end
    object QryPesqAlunoNO_APE_ALU: TStringField
      FieldName = 'NO_APE_ALU'
      Size = 15
    end
    object QryPesqAlunoCO_INST: TIntegerField
      FieldName = 'CO_INST'
    end
    object QryPesqAlunoDT_NASC_ALU: TDateTimeField
      FieldName = 'DT_NASC_ALU'
    end
    object QryPesqAlunoNU_CPF_ALU: TStringField
      FieldName = 'NU_CPF_ALU'
      Size = 18
    end
    object QryPesqAlunoCO_SEXO_ALU: TStringField
      FieldName = 'CO_SEXO_ALU'
      FixedChar = True
      Size = 1
    end
    object QryPesqAlunoCO_RG_ALU: TStringField
      FieldName = 'CO_RG_ALU'
    end
    object QryPesqAlunoCO_ORG_RG_ALU: TStringField
      FieldName = 'CO_ORG_RG_ALU'
      Size = 12
    end
    object QryPesqAlunoCO_ESTA_RG_ALU: TStringField
      FieldName = 'CO_ESTA_RG_ALU'
      Size = 10
    end
    object QryPesqAlunoDT_EMIS_RG_ALU: TDateTimeField
      FieldName = 'DT_EMIS_RG_ALU'
    end
    object QryPesqAlunoDE_ENDE_ALU: TStringField
      FieldName = 'DE_ENDE_ALU'
      Size = 40
    end
    object QryPesqAlunoNU_ENDE_ALU: TIntegerField
      FieldName = 'NU_ENDE_ALU'
    end
    object QryPesqAlunoDE_COMP_ALU: TStringField
      FieldName = 'DE_COMP_ALU'
      Size = 15
    end
    object QryPesqAlunoNO_BAIR_ALU: TStringField
      FieldName = 'NO_BAIR_ALU'
    end
    object QryPesqAlunoNO_CIDA_ALU: TStringField
      FieldName = 'NO_CIDA_ALU'
      Size = 30
    end
    object QryPesqAlunoCO_ESTA_ALU: TStringField
      FieldName = 'CO_ESTA_ALU'
      FixedChar = True
      Size = 2
    end
    object QryPesqAlunoCO_CEP_ALU: TStringField
      FieldName = 'CO_CEP_ALU'
      Size = 12
    end
    object QryPesqAlunoNU_TELE_RESI_ALU: TStringField
      FieldName = 'NU_TELE_RESI_ALU'
      Size = 10
    end
    object QryPesqAlunoNU_TELE_CELU_ALU: TStringField
      FieldName = 'NU_TELE_CELU_ALU'
      Size = 10
    end
    object QryPesqAlunoNO_PROF_ALU: TStringField
      FieldName = 'NO_PROF_ALU'
      Size = 60
    end
    object QryPesqAlunoNO_EMPR_ALU: TStringField
      FieldName = 'NO_EMPR_ALU'
    end
    object QryPesqAlunoNO_CARG_EMPR_ALU: TStringField
      FieldName = 'NO_CARG_EMPR_ALU'
      Size = 15
    end
    object QryPesqAlunoDT_ADMI_EMPR_ALU: TDateTimeField
      FieldName = 'DT_ADMI_EMPR_ALU'
    end
    object QryPesqAlunoNU_TELE_COME_ALU: TStringField
      FieldName = 'NU_TELE_COME_ALU'
      Size = 10
    end
    object QryPesqAlunoNU_RAMA_COME_ALU: TStringField
      FieldName = 'NU_RAMA_COME_ALU'
      Size = 4
    end
    object QryPesqAlunoNO_PAI_ALU: TStringField
      FieldName = 'NO_PAI_ALU'
      Size = 60
    end
    object QryPesqAlunoNO_MAE_ALU: TStringField
      FieldName = 'NO_MAE_ALU'
      Size = 60
    end
    object QryPesqAlunoCO_NACI_ALU: TStringField
      FieldName = 'CO_NACI_ALU'
      FixedChar = True
      Size = 1
    end
    object QryPesqAlunoDE_NACI_ALU: TStringField
      FieldName = 'DE_NACI_ALU'
      Size = 40
    end
    object QryPesqAlunoDE_NATU_ALU: TStringField
      FieldName = 'DE_NATU_ALU'
      Size = 40
    end
    object QryPesqAlunoNO_ENDE_ELET_ALU: TStringField
      FieldName = 'NO_ENDE_ELET_ALU'
      Size = 50
    end
    object QryPesqAlunoNO_WEB_ALU: TStringField
      FieldName = 'NO_WEB_ALU'
      Size = 50
    end
    object QryPesqAlunoDT_CADA_ALU: TDateTimeField
      FieldName = 'DT_CADA_ALU'
    end
    object QryPesqAlunoIM_FOTO_ALU: TBlobField
      FieldName = 'IM_FOTO_ALU'
    end
    object QryPesqAlunoCO_SITU_ALU: TStringField
      FieldName = 'CO_SITU_ALU'
      FixedChar = True
      Size = 1
    end
    object QryPesqAlunoDT_SITU_ALU: TDateTimeField
      FieldName = 'DT_SITU_ALU'
    end
    object QryPesqAlunoNU_TIT_ELE: TStringField
      FieldName = 'NU_TIT_ELE'
      Size = 15
    end
    object QryPesqAlunoNU_ZONA_ELE: TStringField
      FieldName = 'NU_ZONA_ELE'
      Size = 10
    end
    object QryPesqAlunoNU_SEC_ELE: TStringField
      FieldName = 'NU_SEC_ELE'
      Size = 10
    end
    object QryPesqAlunoFLA_BOLSISTA: TStringField
      FieldName = 'FLA_BOLSISTA'
      FixedChar = True
      Size = 1
    end
    object QryPesqAlunoNU_PEC_DESBOL: TBCDField
      FieldName = 'NU_PEC_DESBOL'
      Precision = 6
      Size = 2
    end
  end
  object comando: TADOCommand
    Connection = Conn
    Parameters = <>
    Left = 288
    Top = 112
  end
  object qryClassInst: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'SELECT * FROM tb162_clas_inst')
    Left = 368
    Top = 446
  end
  object qryInstEsp: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'SELECT * FROM tb164_inst_esp')
    Left = 464
    Top = 464
  end
  object QryQuilombo: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select * from tb167_quilombo'
      'order by co_qui')
    Left = 608
    Top = 464
    object QryQuilomboCO_QUI: TAutoIncField
      FieldName = 'CO_QUI'
      ReadOnly = True
    end
    object QryQuilomboNO_QUI: TStringField
      FieldName = 'NO_QUI'
      Size = 50
    end
  end
  object QryPredio: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select * from TB171_PREDIO')
    Left = 672
    Top = 464
    object QryPredioCO_PREDIO: TAutoIncField
      FieldName = 'CO_PREDIO'
      ReadOnly = True
    end
    object QryPredioNO_PREDIO: TStringField
      FieldName = 'NO_PREDIO'
      Size = 50
    end
  end
  object QryAbastAgua: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select * from TB168_ABST_AGUA')
    Left = 536
    Top = 464
    object QryAbastAguaCO_ABAST: TAutoIncField
      FieldName = 'CO_ABAST'
      ReadOnly = True
    end
    object QryAbastAguaNO_ABAST: TStringField
      FieldName = 'NO_ABAST'
      Size = 50
    end
  end
  object QryCor: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select * from tb97_cor')
    Left = 280
    Top = 456
    object QryCorCO_COR: TIntegerField
      FieldName = 'CO_COR'
    end
    object QryCorDES_COR: TStringField
      FieldName = 'DES_COR'
      Size = 80
    end
    object QryCorno_sigla: TStringField
      FieldName = 'no_sigla'
      Size = 4
    end
  end
  object qryTamanho: TADOQuery
    Connection = Conn
    Parameters = <>
    SQL.Strings = (
      'select * from tb98_tamanho')
    Left = 216
    Top = 464
    object qryTamanhoCO_TAMANHO: TIntegerField
      FieldName = 'CO_TAMANHO'
    end
    object qryTamanhoDES_TAMANHO: TStringField
      FieldName = 'DES_TAMANHO'
      Size = 80
    end
    object qryTamanhono_sigla: TStringField
      FieldName = 'no_sigla'
      Size = 4
    end
  end
  object QryNucleoInst: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    SQL.Strings = (
      'select * from TB_NUCLEO_INST'
      'order by NO_SIGLA_NUCLEO')
    Left = 360
    Top = 8
    object QryNucleoInstCO_NUCLEO: TAutoIncField
      FieldName = 'CO_NUCLEO'
      ReadOnly = True
    end
    object QryNucleoInstNO_SIGLA_NUCLEO: TStringField
      FieldName = 'NO_SIGLA_NUCLEO'
      Size = 10
    end
    object QryNucleoInstDE_NUCLEO: TStringField
      FieldName = 'DE_NUCLEO'
      Size = 100
    end
  end
  object qryPesqCadTurmas: TADOQuery
    Connection = Conn
    Parameters = <
      item
        Name = 'P_CO_EMP'
        Attributes = [paSigned]
        DataType = ftInteger
        Precision = 10
        Size = 4
        Value = Null
      end>
    SQL.Strings = (
      'select * from tb129_cadturmas'
      'where co_emp = :P_CO_EMP')
    Left = 272
    Top = 200
    object qryPesqCadTurmasNO_TURMA: TStringField
      FieldName = 'NO_TURMA'
      Size = 40
    end
    object qryPesqCadTurmasCO_SIGLA_TURMA: TStringField
      FieldName = 'CO_SIGLA_TURMA'
      Size = 10
    end
    object qryPesqCadTurmasCO_EMP: TIntegerField
      FieldName = 'CO_EMP'
    end
    object qryPesqCadTurmasCO_STATUS_TURMA: TStringField
      FieldName = 'CO_STATUS_TURMA'
      FixedChar = True
      Size = 1
    end
    object qryPesqCadTurmasDT_STATUS_TURMA: TDateTimeField
      FieldName = 'DT_STATUS_TURMA'
    end
    object qryPesqCadTurmasCO_TUR: TAutoIncField
      FieldName = 'CO_TUR'
      ReadOnly = True
    end
  end
end
