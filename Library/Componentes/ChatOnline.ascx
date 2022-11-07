<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatOnline.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Library.Componentes.ChatOnline" %>

<style>
    .grdLinha
    {
    }
   
</style>
<div id="ChatOnline" style="width: 543px;">
    <div style="width: 543px;">
        <asp:GridView 
            ID="grdMsg" 
            CssClass="grdBusca" 
            runat="server" 
            AutoGenerateColumns="False"
            ShowHeader="false"
            Width="543px"
            Height="15px">

            <RowStyle CssClass="grdLinha" />
            <AlternatingRowStyle CssClass="alternatingRowStyle" />
            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
            <EmptyDataTemplate>
                Nenhum registro encontrado.<br />
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image runat="server" ID="imgFotoUsu" ImageUrl="../IMG/Gestor_Admin.png" Width="15" Height="15" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NO_USU">
                    <ItemStyle Width="25px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DE_MSG">
                    <ItemStyle Width="518px" Wrap="true" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</div>