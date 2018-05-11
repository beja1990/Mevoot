﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MP/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AddLesson.aspx.cs" Inherits="AddLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery.js"></script>
    <script src="../plugins/bootstrap/js/bootstrap.min.js"></script>
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
                <li><a href="ShowLesson.aspx">תבנית תגבורים</a></li>
                <li class="active">הוספת תבנית תגבור</li>
            </ol>
            <div class="sectionTitle text-center">
                <h2>
                    <span class="shape shape-left bg-color-4"></span>
                    <span>הוספת תבנית תגבור</span>
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
                        <asp:DropDownList ID="dayDDL" runat="server" Style="text-align: right" CssClass="form-control border-color-4 tb">
                            <asp:ListItem Enabled="true" Text="בחר יום בשבוע" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="יום א" Value="1"></asp:ListItem>
                            <asp:ListItem Text="יום ב" Value="2"></asp:ListItem>
                            <asp:ListItem Text="יום ג" Value="3"></asp:ListItem>
                            <asp:ListItem Text="יום ד" Value="4"></asp:ListItem>
                            <asp:ListItem Text="יום ה" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <i class="fa fa-user"></i>
                        <asp:TextBox ID="startTimeTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שעת התחלה"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <i class="fa fa-phone"></i>
                        <asp:TextBox ID="endTimeTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="שעת סיום"></asp:TextBox>
                    </div>
                     <div class="form-group" style="text-align: center">
                            <asp:Button ID="add" runat="server" Text="הוסף תבנית תגבור" OnClick="submitBTN_Click" CssClass="btn btn-primary rightTB" data-target="#confirmationModal" />
                        </div>
                </div>
            </div>


            <div class="col-sm-6 col-xs-12">
                <div class="homeContactContent">
                    <div class="form-group">

                        <div class="form-group">

                            <asp:DropDownList ID="teacherDDL" runat="server" AutoPostBack="true " OnSelectedIndexChanged="teacherDDL_SelectedIndexChanged" CssClass="form-control border-color-4 tb" placeholder="בחר מתגבר">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">

                            <asp:DropDownList ID="professionDDL" runat="server" DataTextField="pro_title" DataValueField="pro_title" CssClass="form-control border-color-4 tb" placeholder="בחר מקצוע">
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <i class="fa fa-home"></i>
                            <asp:TextBox ID="maxQuanTB" runat="server" CssClass="form-control border-color-4 tb" placeholder="כמות מקסימלית לתגבור"></asp:TextBox>
                        </div>

                       
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div id="confirmationModal" class="modal fade modal-sm modaltb" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content btn">
                <div class="modal-header">
                    <h5>התבנית נוספה בהצלחה</h5>
                    &nbsp
                    <button type="button" class="btn btn-sm btn-success" data-dismiss="modal" onclick="window.location='ShowLesson.aspx'">חזור למסך תבניות תגבורים</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
