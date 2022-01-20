<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers_List.aspx.cs" Inherits="task.Customers_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Customers</h1>
    </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <button runat="server" id="btnAddCustomer" clientidmode="Static" type="button" class="btn btn-info" data-bs-toggle="collapse" data-bs-target="#AddCustomer">Add Customer</button>
                <div id="AddCustomer" class="collapse col-sm-12">
                    <asp:HiddenField runat="server" ID="hdnCoustomer_ID" />
                    <div class="mb-3">
                        <label for="txtName" class="col-sm-2 form-label">Name</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtName" clientidmode="Static" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="txtPhone" class="col-sm-2 form-label">Phone</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtPhone" clientidmode="Static" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="txtAddress" class="col-sm-2 form-label">Address</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtAddress" clientidmode="Static" CssClass="form-control" MaxLength="150"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-warning" Visible="false" OnClick="btnCancel_Click" Text="Cancel"></asp:LinkButton>
                </div>
        <br />

            <asp:GridView runat="server" ID="gvCustomers" EmptyDataText="There are no Customers"
                AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                OnRowCommand="gvCustomers_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                    <asp:BoundField DataField="Customer_Phone" HeaderText="Customer Phone" />
                    <asp:BoundField DataField="Customer_Address" HeaderText="Customer Address" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdn_Customer_IN" Value='<%# Eval("Customer_ID") %>' />
                            <asp:LinkButton runat="server" CssClass="btn btn-Danger" CommandName="coDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Delete</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-primary" CommandName="coEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
