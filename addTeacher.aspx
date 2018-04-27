<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="addTeacher.aspx.cs" Inherits="addTeacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery.js"></script>
    <%--    <script src="../plugins/bootstrap/js/bootstrap.min.js"></script>--%>
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
            left: 80px;
        }

        .modaltb {
            text-align: right;
            direction: rtl;
            width: 500px;
            margin: 0 auto;
            position: absolute;
            top: 200px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="mainContent full-width clearfix featureSection">
        <div class="container">
            <ol class="breadcrumb">
                <li><a href="admin_calendar.aspx">בית</a></li>
                <li><a href="ShowTeacher.aspx">מתגברים</a></li>
                <li class="active">הוספת מתגבר</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>הוספת מתגבר חדש</span>
                    <span class="shape shape-right bg-color-4"></span>
                </h2>
            </div>

        </div>
    </section>

    <!-- Starts contact form 1 -->
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-xs-12">
                <div class="homeContactContent">


                    <div class="form-group">
                        <i class="fa fa-envelope"></i>
                        <asp:TextBox ID="mailTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="אימייל"></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <i class="fa fa-phone"></i>
                        <asp:TextBox ID="phoneTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="טלפון"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-home"></i>
                        <asp:TextBox ID="addressTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="כתובת"></asp:TextBox>
                    </div>


                    <div class="form-group" style="text-align: center">
                        <asp:Button ID="submitBTN" runat="server" Text="הוסף מתגבר" OnClick="submitBTN_Click" CssClass="btn btn-primary rightTB" data-target="#confirmationModal" />
                    </div>


                </div>
            </div>
            <div class="col-sm-6 col-xs-12">
                <div class="homeContactContent">

                    <div class="form-group">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="idTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="ת.ז"></asp:TextBox>
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

    <%-- Confirmation modal - comment out since we have the toastr notifications --%>

    <%--    <div id="confirmationModal" class="modal fade modal-sm modaltb" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content btn">
                <div class="modal-header">
                    <h5>המתגבר נוסף בהצלחה</h5>
                    &nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" onclick="window.location='ShowTeacher.aspx'">חזור למסך מתגברים</button>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
