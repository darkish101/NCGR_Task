<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrder.aspx.cs" Inherits="task.PrintOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.4.1.js"></script>
    <style type="text/css">
        @media print {
            @page {
                margin: 0;
            }

            body {
                margin: 1.6cm;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="position-relative">
            <div class="text-center">

                <asp:Label CssClass="position-absolute top-0 end-0" runat="server" ID="lblDate"></asp:Label>
                <br />
                <span>Invoice #</span><asp:Label runat="server" ID="lblInvoiceNum"></asp:Label>
                <br />
                <span>Bill To</span>
                <br />
                <span>Customer Name</span>
                <asp:Label runat="server" ID="lblName"></asp:Label>
                <br />
                <span>Address</span><asp:Label runat="server" ID="lblAddress"></asp:Label>
                <br />
                <span>Mobile #</span><asp:Label runat="server" ID="lblMobileNum"></asp:Label>
                <br />
                <asp:GridView runat="server" ID="gvOrder" AutoGenerateColumns="false" OnDataBinding="gvOrder_DataBinding" CssClass="table">
                    <Columns>
                        <asp:BoundField HeaderText="Item #" DataField="#" />
                        <asp:BoundField HeaderText="Description" DataField="Description" />
                        <asp:BoundField HeaderText="Price" DataField="Item_Price" />
                        <asp:BoundField HeaderText="Amount" DataField="Item_Amount" />
                    </Columns>
                </asp:GridView>
                <span>Total:</span>
                <asp:Label runat="server" ID="lbltotal"></asp:Label>
                SAR
            </div>




        </div>
    </form>
    <script>
        $(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
