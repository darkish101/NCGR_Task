<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="task.Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Items</h1>
    </div>
    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <button runat="server" id="btnAddItem" clientidmode="Static" type="button" class="btn btn-info" data-bs-toggle="collapse" data-bs-target="#AddItem">Add Item</button>
                <div id="AddItem" class="collapse col-sm-12">
                    <div class="row">
                        <asp:HiddenField runat="server" ID="hdnCoustomer_ID" />
                        <div class="col-md-4">

                            <div class=" form-group">
                                <label for="txtName" class="form-label">Name</label>
                                <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" Style="display:block" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">

                            <div class="form-group">
                                <label for="txtAddress" class="form-label">Price</label>
                                <asp:TextBox runat="server" ID="txtPrice" ClientIDMode="Static" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtPhone" class="form-label">Description</label>
                                <textarea runat="server" id="txtDescription1" clientidmode="Static" rows="5" class="form-control"></textarea>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:LinkButton runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-warning" Visible="false" OnClick="btnCancel_Click" Text="Cancel"></asp:LinkButton>
                </div>
        <br />

        <div>
            <asp:GridView runat="server" ID="gvItems" EmptyDataText="There are no Items"
                AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                OnRowCommand="gvItems_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Item_Name" HeaderText="Item Name" />
                    <asp:BoundField DataField="Item_Description" HeaderText="Item Description" />
                    <asp:BoundField DataField="Item_Price" HeaderText="Item Price" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdn_Item_IN" Value='<%# Eval("Item_ID") %>' />
                            <asp:LinkButton runat="server" CssClass="btn btn-Danger" CommandName="coDelete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Delete</asp:LinkButton>
                            <asp:LinkButton runat="server" CssClass="btn btn-primary" CommandName="coEdit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
