<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="EditTeacher.aspx.cs" Inherits="EditTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://code.jquery.com/jquery.js"></script>
    <link href="assets/plugins/toastr/toastr.css" rel="stylesheet" />
    <script src="assets/plugins/toastr/toastr.js"></script>
    <script>
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",

        }
    </script>
    <style>
        ::-webkit-input-placeholder {
            text-align: right;
        }

        .tb {
            text-align: right;
            direction: rtl;
            width: 400px;
            margin-left: auto;
        }

        .rightTB {
            position: relative;
            left: 50px;
        }

        .modaltb {
            text-align: right;
            direction: rtl;
            width: 500px;
            margin: 0 auto;
            position: absolute;
            top: 200px;
        }

        .cbRight {
            text-align: right;
            direction: rtl;
            position: relative;
            right: 20px;
            bottom: 20px;
        }

        .smallwidth {
            width: 250px;
            float: right;
        }

        .grid td, .grid th {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li><a href="ShowTeacher.aspx">מתגברים</a></li>
                <li class="active">עריכת פרטי מתגבר</li>
            </ol>

            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>עריכת פרטי מתגבר</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>

    <!-- Starts contact form 1 -->
    <div class="container">
        <div class="row cbRight">
            <asp:CheckBox ID="statusCB" runat="server" />
            משתמש פעיל
        </div>

        <div class="row">

            <div class="col-sm-6 col-xs-12 col-lg-4">
                <asp:Button ID="addProBTN" runat="server" Text="הוסף מקצוע" CssClass="btn btn-primary rightTB btn-sm" OnClick="addProBTN_Click" />
                <asp:DropDownList ID="ProfessionDDL" runat="server" CssClass="form-control border-color-4 tb smallwidth" placeholder="בחר מקצוע">
                </asp:DropDownList>

                <%--                //insert datasource here--%>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:studentDBConnectionString %>"></asp:SqlDataSource>
                <br />
                <%--            //insert gridview here--%>

                <asp:GridView ID="GridView1" runat="server" CssClass="grid" Style="margin-left: auto; margin-right: auto; margin-top: 30px; direction: rtl; text-align: center; width: 80%" AllowSorting="True" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="pro_title" HeaderText="מקצוע" />
                        <asp:CommandField ShowSelectButton="True" />

                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#245581" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
            </div>


            <div class="col-sm-6 col-xs-12 col-lg-4">
                <div class="homeContactContent">
                    <div class="form-group">
                        <i class="fa fa-phone"></i>
                        <asp:TextBox ID="phoneTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="טלפון"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-home"></i>
                        <asp:TextBox ID="addressTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="כתובת"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <i class="fa fa-envelope"></i>
                        <asp:TextBox ID="mailTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="אימייל"></asp:TextBox>
                    </div>
                    <div class="form-group" style="text-align: center">
                        <asp:Button ID="saveBTN" runat="server" Text="עדכון מתגבר" OnClick="saveBTN_Click" CssClass="btn btn-primary rightTB" data-target="#confirmationModal" />
                        <button id="deleteBTN" class="btn btn-danger" style="float: left;" type="button" data-toggle="modal" data-target="#deleteConfirmationModal">מחיקה</button>
                    </div>
                </div>
            </div>

            <div class="col-sm-6 col-xs-12 col-lg-4">
                <div class="homeContactContent">

                    <div class="form-group">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="idTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="ת.ז" ReadOnly="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="fNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם פרטי"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="LNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם משפחה"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-unlock-alt"></i>
                        <asp:TextBox ID="passwordTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="סיסמא"></asp:TextBox>
                    </div>



                </div>
            </div>

        </div>
        <!-- Ends contact form 1 -->
    </div>
    <div id="confirmationModal" class="modal fade modal-sm modaltb" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content btn">
                <div class="modal-header">
                    <h5>המתגבר עודכן בהצלחה</h5>
                    &nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" onclick="window.location='ShowTeacher.aspx'">חזור למסך מתגברים</button>
                </div>
            </div>

        </div>
    </div>

    <div id="deleteConfirmationModal" class="modal fade modal-md modaltb" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content btn">
                <div class="modal-header">
                    <h5>האם ברצונך למחוק את המתגבר ?</h5>
                    &nbsp
                    <asp:Button ID="Button2" runat="server" Text="מחק" OnClick="deleteBTN_Click" CssClass="btn btn-sm btn-danger" />
                    &nbsp&nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal">ביטול</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

