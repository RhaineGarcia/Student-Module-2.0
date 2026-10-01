<%@ Page Title="Student Dashboard"
    Language="VB"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="StudentDashboard.aspx.vb"
    Inherits="iMarkaOfficial.StudentDashboard" %>

<asp:Content ID="HeadContent"
    ContentPlaceHolderID="HeadContent"
    runat="server">

    <link rel="stylesheet"
        href="StudentDashboard.css"
        runat="server" />

</asp:Content>


<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="student-dashboard">

        <!-- =========================
             HEADER
             ========================= -->

        <header class="dashboard-header">

            <div class="header-left">

                <div class="brand-name">
                    <span class ="brand-i">i</span><span class="brand-MARKA">MARKA</span>
                </div>

                <div class="dashboard-title">
                    Student Dashboard
                </div>

            </div>

            <div class="header-right">

                <asp:Label ID="lblStudentID"
                    runat="server"
                    CssClass="student-id">
                </asp:Label>

                <asp:Button ID="btnLogout"
                    runat="server"
                    Text="Logout"
                    CssClass="btn btn-logout" />

            </div>

        </header>


        <!-- =========================
             MAIN CONTENT
             ========================= -->

        <main class="dashboard-content">


            <!-- =========================
                 STUDENT INFORMATION
                 ========================= -->

            <section class="student-info-card">

                <div class="section-heading">

                    <div>
                        <h2>Student Information</h2>

                        <p>
                            Your registered student information
                        </p>
                    </div>

                </div>


                <div class="student-details">

                    <asp:Label ID="lblStudentDetails"
                        runat="server"
                        CssClass="student-details-text">
                    </asp:Label>

                </div>

            </section>


            <!-- =========================
                 SUBJECTS
                 ========================= -->

            <section class="subjects-card">

                <div class="section-heading">

                    <div>
                        <h2>My Subjects</h2>

                        <p>
                            Subjects you are currently enrolled in
                        </p>
                    </div>

                </div>


                <div class="table-container">

                    <asp:GridView ID="gvSubjects"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="subjects-table"
                        GridLines="None"
                        EmptyDataText="You are not enrolled in any subjects."
                        ShowHeaderWhenEmpty="True">

                        <RowStyle CssClass="subject-row" />
                        <Columns>

                            <asp:BoundField
                                DataField="subject_code"
                                HeaderText="Subject Code" />

                            <asp:BoundField
                                DataField="subject_name"
                                HeaderText="Subject Name" />

                            <asp:BoundField
                                DataField="units"
                                HeaderText="Units" />

                            <asp:BoundField
                                DataField="professor_name"
                                HeaderText="Professor" />

                            <asp:BoundField
                                DataField="grade_status"
                                HeaderText="Grade" />

                            <asp:BoundField
                                DataField="remarks"
                                HeaderText="Remarks" />


                       <asp:TemplateField HeaderText="" ItemStyle-CssClass="expand-cell">
    <ItemTemplate>
        <!-- Keep the checkbox inside the active row cell -->
        <input type="checkbox" id='chk-<%# Eval("subject_code") %>' class="toggle-checkbox" style="display: none;" />
        
        <!-- Label wrapping the interactive chevron -->
        <label for='chk-<%# Eval("subject_code") %>' class="chevron-label">
            <span class="chevron">❮</span>
        </label>
        
        <!-- Safely close out the active row to inject our full-width nested row -->
        </td>
        </tr>
        <tr class="nested-details-row">
            <td colspan="7">
                <div class="dropdown-panel">
                    
                    <!-- Written Works -->
                    <div class="grade-category">
                        <div class="category-header">
                            <span>Written Works</span>
                            <span class="category-weight">Score</span>
                        </div>
                        <div class="category-items">
                            <div class="grade-item"><span>Quiz 1</span><span>18/20</span></div>
                            <div class="grade-item"><span>Quiz 2</span><span>10/10</span></div>
                            <div class="grade-item"><span>Assignment 1</span><span>35/40</span></div>
                        </div>
                    </div>
                    
                    <!-- Performance Task -->
                    <div class="grade-category">
                        <div class="category-header">
                            <span>Performance Task</span>
                            <span class="category-weight">Score</span>
                        </div>
                        <div class="category-items">
                            <div class="grade-item"><span>Project 1</span><span>95/100</span></div>
                        </div>
                    </div>

                </div>
    </ItemTemplate>
</asp:TemplateField>




                        </Columns>

                    </asp:GridView>

                </div>

            </section>


        </main>


        <!-- =========================
             FOOTER
             ========================= -->

        <footer class="dashboard-footer">

            <span>
                iMarka Academic Grade Management System
            </span>

        </footer>

    </div>

</asp:Content>

