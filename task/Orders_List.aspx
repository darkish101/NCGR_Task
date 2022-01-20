<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders_List.aspx.cs" Inherits="task.Orders_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <asp:GridView runat="server" ID="gvOrders" CssClass="table text-center" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Name" DataField="Customer_Name" />
            <asp:BoundField HeaderText="Phone" DataField="Customer_Phone" />
            <asp:BoundField HeaderText="Address" DataField="Customer_Address" />
            <asp:BoundField HeaderText="Date" DataField="Created_On" />
            <asp:TemplateField>
                <ItemTemplate>
                <a href='PrintOrder?Invoice_ID=<%# Eval("Invoice_ID") %>' class="btn btn-primary">Print</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>




</asp:Content>
