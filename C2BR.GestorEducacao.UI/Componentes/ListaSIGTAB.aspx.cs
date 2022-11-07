using C2BR.GestorEducacao.BusinessEntities.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace C2BR.GestorEducacao.UI.Componentes
{
    public partial class ListaSIGTAB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SQLDirectAcess direc = new SQLDirectAcess();
                DataTable dt = new DataTable();
                dt = direc.retornacolunas("select ID_PROC_MEDI_PROCE, CO_PROC_MEDI, NM_PROC_MEDI from TBS356_PROC_MEDIC_PROCE where LEN(co_proc_medi) = 10");

                grdListarSIGTAP.DataSource = dt;
                grdListarSIGTAP.DataBind();
            }
        }

        protected void btnincluir_Click(object sender, EventArgs e)
        {
            DataTable mDataTable = new DataTable();

            DataColumn mDataColumn;
            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "Codigo";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "Nome";
            mDataTable.Columns.Add(mDataColumn);

            DataRow linha;
            foreach (GridViewRow linha2 in grdListarSIGTAP.Rows)
            {
                if (((CheckBox)linha2.Cells[0].FindControl("chkselectEn")).Checked)
                {
                    linha = mDataTable.NewRow();
                    linha["Codigo"] = linha2.Cells[1].Text;
                    linha["Nome"] = linha2.Cells[2].Text;
                    mDataTable.Rows.Add(linha);
                }
            }
            Session["dtsigtab"] = mDataTable;
        }

        protected void grdListarSIGTAP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}