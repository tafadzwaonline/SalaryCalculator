<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="gross-salary-calculator.aspx.cs" Inherits="WebApplication4.gross_salary_calculator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
                    <div class="height-10 w-100 shadow-lg px-4 bg-brand-gradient">
                        <div class="d-flex align-items-center container p-0">
                            <div class="page-logo width-mobile-auto m-0 align-items-center justify-content-center p-0 bg-transparent bg-img-none shadow-0 height-9 border-0">
                                <a href="javascript:void(0)" class="page-logo-link press-scale-down d-flex align-items-center">
                                    <img src="~/../assets/img/logo.png" alt="SmartAdmin WebApp" aria-roledescription="logo">
                                    <span class="page-logo-text mr-1">Salari</span>
                                </a>
                            </div>

                        </div>
                    </div>
                    <div class="flex-1" style="background: url(~/../assets/img/svg/pattern-1.svg) no-repeat center bottom fixed; background-size: cover;">
                        <div class="container py-4 py-lg-5 my-lg-5 px-4 px-sm-0">
                            <div class="row">
                                <div class="col col-md-6 col-lg-7 hidden-sm-down">
                                    <div id="panel-1" class="panel" style="width: 110%; margin: 0 auto; text-align: left;">
                                        <div class="panel-hdr">
                                            <h2>Tax Tables <span class="fw-300"><i>(USD & ZIG)</i></span>
                                            </h2>

                                        </div>
                                        <div class="panel-container show">
                                            <div class="panel-content">
                                               
                                                <div class="panel-tag">
                                                    <b><code>(Summary)</code></b>
                                                    <br />
                                                    Net Salary: <code><asp:Label ID="lblNetCode" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="0" ></asp:Label>&nbsp <asp:Label ID="lblNetsalary" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="0" ></asp:Label></code>
                                                   
                                                   
                                                    <br />
                                                    <b>Estimated Gross Salary:<code><asp:Label ID="lblGrossCode" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="0" ></asp:Label>&nbsp<asp:Label ID="lblEstimatedGross" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="0" ></asp:Label></code></b> 
                                                    <br />
                                                </div>
                                        

                                                <div class="form-group">
                                                   
                                                    <asp:DropDownList ID="drpCurrency" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCurrency_SelectedIndexChanged">
                                             
                                                    </asp:DropDownList>
                                                     <div class="col-xs-12 text-center">
    <asp:Label ID="lblError2" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="" ForeColor="Red"></asp:Label>
</div>
                                                </div>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="12">

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grdTax" runat="server" class="table table-bordered table-striped"
                                                                        
                                                                        AutoGenerateColumns="False" Width="100%"
                                                                        AllowPaging="True" AllowSorting="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="EffectiveDate" HeaderText="EffectiveDate"></asp:BoundField>
                                                                            <asp:BoundField DataField="BandStart" HeaderText="Salary(From)"></asp:BoundField>
                                                                            <asp:BoundField DataField="BandEnd" HeaderText="Salary(To)"></asp:BoundField>
                                                                            <asp:BoundField DataField="BandRate" HeaderText="Tax %"></asp:BoundField>
                                                                            <asp:BoundField DataField="Cumulative" HeaderText="Cumulative"></asp:BoundField>
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
                                <div class="col-sm-12 col-md-6 col-lg-5 col-xl-4 ml-auto">
                                    <h1 class="text-white fw-300 mb-3 d-sm-block d-md-none">Secure login
                                    </h1>
                                    <div class="card p-4 rounded-plus bg-faded">

                                        <div class="form-group">
                                            <label class="form-label" for="simpleinput">Currency</label>
                                            <asp:DropDownList ID="dropdownCurrency" runat="server" CssClass="form-control" AutoPostBack="false">
                                          
                                            </asp:DropDownList>
                                        </div>
                                         <div class="col-xs-12 text-center">
                                            <asp:Label ID="lblError" runat="server" Font-Size="Small" Style="font-size: 14px;" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label" for="username">Net salary</label>
                                            <asp:TextBox ID="txtnetsalary" runat="server" class="form-control"></asp:TextBox>

                                        </div>

                                       <%-- <div class="form-group">
                                            <asp:CheckBox ID="chkIsActive" readonnly="true" runat="server" OnCheckedChanged="chkIsActive_CheckedChanged" AutoPostBack="true" />
                                            <label for="rememberme">Include Medical AID</label>
                                        </div>

                                        <div class="form-group" id="ma" runat="server" visible="false">
                                            <label class="form-label" for="username">Contribution Amount</label>
                                            <asp:TextBox ID="txtmedicalaid" runat="server" class="form-control"></asp:TextBox>
                                            <div class="help-block">Enter contribution amount in USD</div>

                                        </div>--%>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckIsAidsLevy" Checked="true" runat="server"  Enabled="false"/>
                                            <label for="rememberme">Include <code>3</code> % Aids Levy</label>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckContributions" readonnly="true" runat="server" />
                                            <label for="rememberme">Include <code>5</code> % Member Contributions (pension fund)</label>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckNassa"  Checked="true" runat="server" Enabled="false" />
                                            <label for="rememberme">Include <code>4.5</code> % Nassa Contributions</label>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckNec" readonnly="true" runat="server" />
                                            <label for="rememberme">Include <code>2</code> % NEC Contribution</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label" for="username">Gross Salary (<code>estimated</code>)</label>
                                            <asp:TextBox ID="txtgrosssalary" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>


                                        </div>
                   <%--                     <div class="form-group">
                                            <label class="form-label" for="username">Total Deductions (<code>estimated</code>)</label>
                                            <asp:TextBox ID="txtTotalTax" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>


                                        </div>--%>
                                        <div class="row no-gutters">
                                            <div class="col-lg-6 pr-lg-1 my-2">

                                                <asp:Button ID="btnSubmit" runat="server" Text="Calculate" class="btn btn-info btn-block btn-lg" OnClick="btnSubmit_Click" />
                                            </div>
                                            <div class="col-lg-6 pl-lg-1 my-2">

                                                <asp:Button ID="btnClear" runat="server" Text="Reset" class="btn btn-danger btn-block btn-lg" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                       

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                  
           
</asp:Content>
