<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html>

<html lang="en">


<head>
    <meta charset="utf-8">
    <title>Salary Calculator
    </title>
    <meta name="description" content="Login">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no, user-scalable=no, minimal-ui">
    <!-- Call App Mode on ios devices -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <!-- Remove Tap Highlight on Windows Phone IE -->
    <meta name="msapplication-tap-highlight" content="no">
    <!-- base css -->
    <link id="vendorsbundle" rel="stylesheet" media="screen, print" href="~/../assets/css/vendors.bundle.css">
    <link id="appbundle" rel="stylesheet" media="screen, print" href="~/../assets/css/app.bundle.css">
    <link id="mytheme" rel="stylesheet" media="screen, print" href="#">
    <link id="myskin" rel="stylesheet" media="screen, print" href="~/../assets/css/skins/skin-master.css">
    <!-- Place favicon.ico in the root directory -->
    <link rel="apple-touch-icon" sizes="180x180" href="~/../assets/img/favicon/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="~/../assets/img/favicon/favicon-32x32.png">
    <link rel="mask-icon" href="~/../assets/img/favicon/safari-pinned-tab.svg" color="#5bbad5">
    <link rel="stylesheet" media="screen, print" href="~/../assets/css/fa-brands.css">
</head>

<body>
    <div class="page-wrapper auth">
        <div class="page-inner bg-brand-gradient">
            <div class="page-content-wrapper bg-transparent m-0">
                <form runat="server">
                    <div class="height-10 w-100 shadow-lg px-4 bg-brand-gradient">
                        <div class="d-flex align-items-center container p-0">
                            <div class="page-logo width-mobile-auto m-0 align-items-center justify-content-center p-0 bg-transparent bg-img-none shadow-0 height-9 border-0">
                                <a href="javascript:void(0)" class="page-logo-link press-scale-down d-flex align-items-center">
                                    <img src="~/../assets/img/logo.png" alt="SmartAdmin WebApp" aria-roledescription="logo">
                                    <span class="page-logo-text mr-1">Kahwai Salary Predictor</span>
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
                                            <h2>Tax Tables <span class="fw-300"><i>(USD & ZWL)</i></span>
                                            </h2>

                                        </div>
                                        <div class="panel-container show">
                                            <div class="panel-content">
                                                <div class="panel-tag">
                                                    Aids Levy is <code>3</code> % of the Individual's Tax payable. The tax tables below are monthly based.  
                                                </div>
                                                <div class="panel-tag">
                                                    Example 1, If an employee earns US$1800 per month the tax will be calculated thus: (1800*30%)-US$85 = US$455
                                                </div>
                                                <div class="panel-tag">
                                                    Example 2, If an employee earns ZWL$350000 per month the tax will be calculated thus: (350000*25%)-ZWL$34333 = ZWL$53166.67
                                                </div>

                                                <div class="form-group">
                                                    <label class="form-label" for="simpleinput">Currency</label>
                                                    <asp:DropDownList ID="drpCurrency" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCurrency_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select Currency" />
                                                        <asp:ListItem Text="USD" />
                                                        <asp:ListItem Text="ZWL" />
                                                    </asp:DropDownList>
                                                </div>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="12">

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <asp:GridView ID="grdTax" runat="server" Style="background-color: white; color: black;" class="table table-bordered dataTable no-footer"
                                                                        role="grid" aria-describedby="basicExample_info"
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
                                            <asp:DropDownList ID="dropdownCurrency" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCurrency_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Currency" />
                                                <asp:ListItem Text="USD" />
                                                <asp:ListItem Text="ZWL" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label" for="username">Gross Salary</label>
                                            <asp:TextBox ID="txtgrosssalary" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                            <div class="invalid-feedback">No, you missed this one.</div>
                                            <div class="help-block">Enter your gross salary</div>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkIsActive" readonnly="true" runat="server" OnCheckedChanged="chkIsActive_CheckedChanged" AutoPostBack="true" />
                                            <label for="rememberme">Include Medical AID</label>
                                        </div>

                                        <div class="form-group" id="ma" runat="server" visible="false">
                                            <label class="form-label" for="username">Contribution Amount</label>
                                            <asp:TextBox ID="txtmedicalaid" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                            <div class="help-block">Enter contribution amount in USD</div>

                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckIsAidsLevy" readonnly="true" runat="server" />
                                            <label for="rememberme">Include <code>3</code> % Aids Levy</label>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckContributions" readonnly="true" runat="server" />
                                            <label for="rememberme">Include <code>5</code> % Member Contributions</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="form-label" for="username">Net Salary</label>
                                            <asp:TextBox ID="txtnetsalary" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            <div class="invalid-feedback">No, you missed this one.</div>

                                        </div>
                                        <div class="form-group">
                                            <label class="form-label" for="username">Tax Payable (<code>excluding other deductions etc</code>)</label>
                                            <asp:TextBox ID="txtTotalTax" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                            <div class="invalid-feedback">No, you missed this one.</div>

                                        </div>
                                        <div class="row no-gutters">
                                            <div class="col-lg-6 pr-lg-1 my-2">

                                                <asp:Button ID="btnSubmit" runat="server" Text="Calculate" class="btn btn-info btn-block btn-lg" OnClick="btnSubmit_Click" />
                                            </div>
                                            <div class="col-lg-6 pl-lg-1 my-2">

                                                <asp:Button ID="btnClear" runat="server" Text="Reset" class="btn btn-danger btn-block btn-lg" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 text-center">
                                            <asp:Label ID="lblError" runat="server" Font-Size="Small" Style="font-size: 17px;" Text="" ForeColor="Red"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </form>

            </div>
        </div>
    </div>
    <!-- BEGIN Color profile -->
    <!-- this area is hidden and will not be seen on screens or screen readers -->
    <!-- we use this only for CSS color refernce for JS stuff -->
    <p id="js-color-profile" class="d-none">
        <span class="color-primary-50"></span>
        <span class="color-primary-100"></span>
        <span class="color-primary-200"></span>
        <span class="color-primary-300"></span>
        <span class="color-primary-400"></span>
        <span class="color-primary-500"></span>
        <span class="color-primary-600"></span>
        <span class="color-primary-700"></span>
        <span class="color-primary-800"></span>
        <span class="color-primary-900"></span>
        <span class="color-info-50"></span>
        <span class="color-info-100"></span>
        <span class="color-info-200"></span>
        <span class="color-info-300"></span>
        <span class="color-info-400"></span>
        <span class="color-info-500"></span>
        <span class="color-info-600"></span>
        <span class="color-info-700"></span>
        <span class="color-info-800"></span>
        <span class="color-info-900"></span>
        <span class="color-danger-50"></span>
        <span class="color-danger-100"></span>
        <span class="color-danger-200"></span>
        <span class="color-danger-300"></span>
        <span class="color-danger-400"></span>
        <span class="color-danger-500"></span>
        <span class="color-danger-600"></span>
        <span class="color-danger-700"></span>
        <span class="color-danger-800"></span>
        <span class="color-danger-900"></span>
        <span class="color-warning-50"></span>
        <span class="color-warning-100"></span>
        <span class="color-warning-200"></span>
        <span class="color-warning-300"></span>
        <span class="color-warning-400"></span>
        <span class="color-warning-500"></span>
        <span class="color-warning-600"></span>
        <span class="color-warning-700"></span>
        <span class="color-warning-800"></span>
        <span class="color-warning-900"></span>
        <span class="color-success-50"></span>
        <span class="color-success-100"></span>
        <span class="color-success-200"></span>
        <span class="color-success-300"></span>
        <span class="color-success-400"></span>
        <span class="color-success-500"></span>
        <span class="color-success-600"></span>
        <span class="color-success-700"></span>
        <span class="color-success-800"></span>
        <span class="color-success-900"></span>
        <span class="color-fusion-50"></span>
        <span class="color-fusion-100"></span>
        <span class="color-fusion-200"></span>
        <span class="color-fusion-300"></span>
        <span class="color-fusion-400"></span>
        <span class="color-fusion-500"></span>
        <span class="color-fusion-600"></span>
        <span class="color-fusion-700"></span>
        <span class="color-fusion-800"></span>
        <span class="color-fusion-900"></span>
    </p>
    <!-- END Color profile -->
    <!-- base vendor bundle: 
			 DOC: if you remove pace.js from core please note on Internet Explorer some CSS animations may execute before a page is fully loaded, resulting 'jump' animations 
						+ pace.js (recommended)
						+ jquery.js (core)
						+ jquery-ui-cust.js (core)
						+ popper.js (core)
						+ bootstrap.js (core)
						+ slimscroll.js (extension)
						+ app.navigation.js (core)
						+ ba-throttle-debounce.js (core)
						+ waves.js (extension)
						+ smartpanels.js (extension)
						+ src/~/../jquery-snippets.js (core) -->
    <script src="js/vendors.bundle.js"></script>
    <script src="js/app.bundle.js"></script>
</body>
<!-- END Body -->

<!-- Mirrored from www.gotbootstrap.com/themes/smartadmin/4.5.1/page_login.html by HTTrack Website Copier/3.x [XR&CO'2014], Sat, 06 May 2023 14:36:50 GMT -->
</html>
