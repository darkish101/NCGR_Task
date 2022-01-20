<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="task.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnOrderID" />
    <div class="row">
        <div class="col-md-9">
            <span>Items</span>
            <%-- items --%>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <asp:Repeater ID="rpItems" runat="server" OnItemCommand="rpItems_ItemCommand">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnItemID" Value='<%# Eval("Item_ID") %>' />
                                <asp:LinkButton runat="server" CommandName="coAdd" CommandArgument="<%# Container.DataItem %>">

                                <div class="card overflow-auto" style="width: 300px; height: 110px;">
                                    <div class="card-body">
                                        <b><span class="name">
                                            <%# Eval("Item_Name") %></span></b>
                                        <br />
                                        <b>Description: </b><span class="description"><%# Eval("Item_Description") %></span><br />
                                        <b>Price: </b><span class="price"><%# Eval("Item_Price") %> SAR</span>
                                    </div>
                                </div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3 overflow-auto" style="height: 500px">
            <span>Order</span>
            <%-- items in same order --%>
            <div class="card ">
                <div class="card-body ">
                    <asp:Repeater runat="server" ID="rpOrder" OnItemCommand="rpOrder_ItemCommand">
                        <ItemTemplate>
                            <span>Item: </span><span><%# Eval("Item_Name")  %></span>
                            <br />
                            <span>Price: </span><span><%# Eval("Item_Price")  %> SAR</span><br />
                            <span>Amount: </span><span><%# Eval("Item_Amount")  %></span>
                            <br />
                        </ItemTemplate>
                    </asp:Repeater>
                    <div runat="server" id="dvHasOrderd" visible="false">

                        <asp:DropDownList runat="server" ID="ddlCustomers"></asp:DropDownList>
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="btn btn-primary" OnClick="btnPrint_Click" Text="Print"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--    <script>
        $(function () {
            //$.ajax({
            //    type: "GET",
            //    url: "Order/GetItems",
            //    origen: "*",
            //    data: '{}',
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: OnSuccess,
            //    failure: function (response) {
            //        alert("failed",response.d);
            //    },
            //    error: function (response) {
            //        alert("error",response.d);
            //    }
            //});
            fetch("WebService1.asmx/GetItems", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: "{}"
            })
                .then((response) => response.json())
                .then((response) => {
                    OnSuccess(response.d);
                });
        });

        function OnSuccess(response) {
            //console.log(response);
            var table = $("#dvItems table").eq(0).clone(true);
            var items = response;
            $("#dvItems table").eq(0).remove();
            $(items).each(function () {
                $(".hdnItem_ID", table).attr("value", this.Item_ID);
                $(".name", table).html(this.Item_Name);
                $(".description", table).html(this.Item_Description);
                $(".price", table).html(this.Item_Price);
                $("#dvItems").append(table);//.append("<br />");
                table = $("#dvItems table").eq(0).clone(true);
            });
        }
    </script>--%>
</asp:Content>
