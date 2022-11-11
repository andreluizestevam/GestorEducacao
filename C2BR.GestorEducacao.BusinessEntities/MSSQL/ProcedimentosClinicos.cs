using C2BR.GestorEducacao.BusinessEntities.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class ProcedimentosClinicos
    {
        SQLDirectAcess dir = new SQLDirectAcess();
        public DataTable PreencheGrigProcedimento(int grupo, int subgrupo, string texto)
        {
            string SQL = "";
            if (texto.Length > 0)
                SQL = " select *, ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI, ID_PROC_MEDI_GRUPO, ID_PROC_MEDI_SGRUP  " +
                      " from TBS356_PROC_MEDIC_PROCE where NM_PROC_MEDI like '%"+ texto + "%' and CO_SITU_PROC_MEDI = 'A' ";
            else if (grupo == 0)  // todos 
                SQL = "select ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI, ID_PROC_MEDI_GRUPO, ID_PROC_MEDI_SGRUP from TBS356_PROC_MEDIC_PROCE where CO_SITU_PROC_MEDI = 'A' order by NM_PROC_MEDI";
            else
                SQL = "select ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI, ID_PROC_MEDI_GRUPO, ID_PROC_MEDI_SGRUP " +
                      " from TBS356_PROC_MEDIC_PROCE where ID_PROC_MEDI_GRUPO = 19 and ID_PROC_MEDI_SGRUP = 32 " +
                      " order by NM_PROC_MEDI                                                                 ";
            return dir.retornacolunas(SQL);
        }
        public DropDownList DropGrupo(DropDownList ddlist)
        {
            string SQL = "select * from TBS354_PROC_MEDIC_GRUPO where FL_SITUA_PROC_MEDIC_GRUPO = 'A' order by NM_PROC_MEDIC_GRUPO";
            ddlist.DataSource = dir.retornacolunas(SQL);
            ddlist.DataTextField = "NM_PROC_MEDIC_GRUPO";
            ddlist.DataValueField = "ID_PROC_MEDIC_GRUPO";
            ddlist.DataBind();
            ddlist.Items.Insert(0, new ListItem("Todos", "0"));
            return ddlist;
        }
        public DropDownList DropSubGrupo(DropDownList ddlist, int Grupo)
        {
            string SQL = "select * from TBS355_PROC_MEDIC_SGRUP  WHERE ID_PROC_MEDIC_GRUPO = " + Grupo + " AND FL_SITUA_PROC_MEDIC_GRUP = 'A' order by NM_PROC_MEDIC_SGRUP";
            ddlist.DataSource = dir.retornacolunas(SQL);
            ddlist.DataTextField = "NM_PROC_MEDIC_SGRUP";
            ddlist.DataValueField = "ID_PROC_MEDIC_SGRUP";
            ddlist.DataBind();
            ddlist.Items.Insert(0, new ListItem("Todos", "0"));
            return ddlist;
        }
        public bool InsereProcedimentos(string CO_ALUNO, string ID_PROCEDIMENTO, string CO_ALUNO_ID_AGEND_HORAR, string PROF_ATENDIMENTO, string ID_profissional_enfermagem)
        {
            string SQL = "insert into TBS479_PROCEDIMENTOS_PESSOA(CO_ALUNO, ID_PROCEDIMENTO, CO_ALUNO_ID_AGEND_HORAR, ID_profissional_saude, ID_profissional_enfermagem) values('" + CO_ALUNO + "','" + ID_PROCEDIMENTO + "','" + CO_ALUNO_ID_AGEND_HORAR +"','" + PROF_ATENDIMENTO + "','" + ID_profissional_enfermagem + "')";
            dir.InsereAltera(SQL);
            return true;
        }
    }
}
