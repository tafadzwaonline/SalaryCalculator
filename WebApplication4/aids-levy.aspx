<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="aids-levy.aspx.cs" Inherits="WebApplication4.aids_levy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="height-10 w-100 shadow-lg px-4 bg-brand-gradient">
        <div class="d-flex align-items-center container p-0">
            <div class="page-logo width-mobile-auto m-0 align-items-center justify-content-center p-0 bg-transparent bg-img-none shadow-0 height-9 border-0">
                <a href="javascript:void(0)" class="page-logo-link press-scale-down d-flex align-items-center">
                    <img src="~/../assets/img/logo.png" alt="Salari WebApp" aria-roledescription="logo">
                    <span class="page-logo-text mr-1">Salari</span>
                </a>
            </div>
           
        </div>
    </div>

    <div class="container py-4 py-lg-5 my-lg-5 px-4 px-sm-0">
        <div class="row">
            <div class="col col-md-12 col-lg-8 hidden-sm-down">
                <div id="panel-1" class="panel">
                    <div class="panel-hdr">
                        <h2>Pension Rate
                                               
                        </h2>

                    </div>
                    <div class="panel-container show">
                        <div class="panel-content">

                            <div class="panel-tag">
                                <div class="form-group row">
                                    <div class="col-md-6">
                                        <label class="form-label" for="txtEffective">Effective Date</label>
                                        <asp:TextBox ID="txtEffective" TextMode="Date" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label" for="txtRate">Rate</label>
                                        <asp:TextBox ID="txtRate" runat="server" class="form-control" />
                                    </div>
                                </div>

                                <div class="row no-gutters">
                                    <div class="col-lg-6 pr-lg-1 my-2">

                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-info btn-block btn-lg" OnClick="btnSubmit_Click" />
                                    </div>


                                </div>
                                <div>
                                    <div class="row no-gutters">
                                        <asp:Label ID="lblError" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="lblSuccess" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="" ForeColor="Green"></asp:Label>
                                    </div>
                                </div>
                                <br />
                            </div>



                            <table style="width: 100%">
                                <tr>
                                    <td colspan="12">

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="grdRate" runat="server" class="table table-bordered table-striped"
                                                    AutoGenerateColumns="False" Width="100%"
                                                    AllowPaging="True" AllowSorting="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="EffectiveDate" HeaderText="EffectiveDate"></asp:BoundField>
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </td>
                                </tr>

                            </table>



                        </div>
                    </div>
                </div>

            </div>

        </div>

    </div>


</asp:Content>

