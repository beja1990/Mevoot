<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="AddStudent" %>

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

        
        .tb , .toast{
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
                <li><a href="ShowStudents.aspx">תלמידים</a></li>
                <li class="active">הוספת תלמיד</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>הוספת תלמיד חדש</span>
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

                        <asp:DropDownList ID="InstructorDDL" runat="server" CssClass="form-control border-color-4 tb" placeholder="בחר מדריך">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-birthday-cake"></i>
                        <asp:TextBox ID="BirhtDateTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="תאריך לידה dd/mm/yyyy"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-phone"></i>
                        <asp:TextBox ID="PhoneTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="טלפון"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-home"></i>
                        <asp:TextBox ID="addressTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="כתובת"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-comments" aria-hidden="true"></i>
                        <asp:TextBox ID="NoteTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="הערות"></asp:TextBox>
                    </div>

                    <div class="form-group" style="text-align: center">
                        <asp:Button ID="add" runat="server" Text="הוסף תלמיד" OnClick="submitBTN_Click" CssClass="btn btn-primary rightTB" data-target="#confirmationModal" />
                    </div>


                </div>
            </div>
            <div class="col-sm-6 col-xs-12" style="position: relative; right: 100px;">
                <div class="homeContactContent">

                    <div class="form-group">
                        <i class="fa fa-id-card"></i>
                        <asp:TextBox ID="IdTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="ת.ז"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="FirstNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם פרטי"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="LastNameTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שם משפחה"></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <i class="fa fa-envelope"></i>
                        <asp:TextBox ID="MailTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="אימייל"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-unlock-alt"></i>
                        <asp:TextBox ID="PasswordTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="סיסמא"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:DropDownList ID="gradeDDL" runat="server" Style="text-align: right" CssClass="form-control border-color-4 tb">
                            <asp:ListItem>בחר כיתה</asp:ListItem>
                            <asp:ListItem>כיתה ז</asp:ListItem>
                            <asp:ListItem>כיתה ח</asp:ListItem>
                            <asp:ListItem>כיתה ט</asp:ListItem>
                            <asp:ListItem>כיתה י</asp:ListItem>
                            <asp:ListItem>כיתה יא</asp:ListItem>
                            <asp:ListItem>כיתה יב</asp:ListItem>
                        </asp:DropDownList>

                    </div>

                    <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
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
                    <h5>התלמיד נוסף בהצלחה</h5>
                    &nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" onclick="window.location='ShowStudents.aspx'">חזור למסך תלמידים</button>
                </div>
            </div>

        </div>
    </div>--%>
</asp:Content>

