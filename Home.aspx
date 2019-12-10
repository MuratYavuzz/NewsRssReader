<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RSSParsing.RSSparsing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
</head>
<body style="height: 419px">
    <form id="form1" runat="server">
        <div overflow: auto" style="height: 492px">

            <asp:SqlDataSource ID="NewsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [ImageURL], [Title] FROM [News]"></asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="NewsDataSource" ForeColor="Black" GridLines="Horizontal" Height="199px" Width="714px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-left: 237px">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" >
                    <FooterStyle ForeColor="#3333FF" />
                    <HeaderStyle ForeColor="#3366FF" />
                    <ItemStyle ForeColor="Blue" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="ImageURL" SortExpression="ImageURL">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ImageURL") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Height="109px" ImageUrl='<%# Eval("ImageURL") %>' Width="128px" />
                        </ItemTemplate>
                        <ItemStyle ForeColor="Blue" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" >
                    <ControlStyle ForeColor="#3366FF" />
                    <FooterStyle ForeColor="#3366FF" />
                    <HeaderStyle ForeColor="White" />
                    <ItemStyle ForeColor="#3333FF" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

        </div>
    </form>
</body>
</html>
