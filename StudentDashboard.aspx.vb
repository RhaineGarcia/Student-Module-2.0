Imports MySqlConnector
Imports System.Configuration
Imports System.Data

Public Class StudentDashboard

    Inherits System.Web.UI.Page


    '==========================================================
    ' PAGE LOAD
    '==========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs) Handles Me.Load

        '------------------------------------------------------
        ' Make sure the student is logged in
        '------------------------------------------------------

        If Session("UserID") Is Nothing Then

            Response.Redirect("Login.aspx")

            Return

        End If


        '------------------------------------------------------
        ' Only load the information once
        '------------------------------------------------------

        If Not IsPostBack Then

            Dim studentID As String =
                Session("UserID").ToString()


            ' Display student ID

            lblStudentID.Text =
                studentID


            ' Load student information

            LoadStudentDetails(studentID)


            ' Load enrolled subjects

            LoadStudentSubjects(studentID)

        End If

    End Sub


    '==========================================================
    ' LOAD STUDENT INFORMATION
    '==========================================================

    Private Sub LoadStudentDetails(
        ByVal studentID As String)

        Try

            Dim connectionString As String =
                ConfigurationManager.ConnectionStrings(
                    "iMarkaDB"
                ).ConnectionString


            Using connection As New MySqlConnection(
                connectionString)

                connection.Open()


                Dim query As String =
                    "SELECT first_name, last_name, course, " &
                    "year_level, section " &
                    "FROM students " &
                    "WHERE student_id = @studentID"


                Using command As New MySqlCommand(
                    query,
                    connection)

                    command.Parameters.AddWithValue(
                        "@studentID",
                        studentID
                    )


                    Using reader As MySqlDataReader =
                        command.ExecuteReader()

                        If reader.Read() Then

                            Dim firstName As String =
                                reader("first_name").ToString()

                            Dim lastName As String =
                                reader("last_name").ToString()

                            Dim course As String =
                                reader("course").ToString()

                            Dim yearLevel As String =
                                reader("year_level").ToString()

                            Dim section As String =
                                reader("section").ToString()


                            lblStudentDetails.Text =
                                firstName & " " &
                                lastName &
                                " | " &
                                course &
                                " | Year " &
                                yearLevel &
                                " | Section " &
                                section

                        Else

                            lblStudentDetails.Text =
                                "Student information not found."

                        End If

                    End Using

                End Using

            End Using


        Catch ex As Exception

            lblStudentDetails.Text =
                "Unable to load student information."

        End Try

    End Sub


    '==========================================================
    ' LOAD ENROLLED SUBJECTS
    '==========================================================

    Private Sub LoadStudentSubjects(
        ByVal studentID As String)

        Try

            Dim connectionString As String =
                ConfigurationManager.ConnectionStrings(
                    "iMarkaDB"
                ).ConnectionString


            Using connection As New MySqlConnection(
                connectionString)

                connection.Open()


                '------------------------------------------------
                ' We intentionally DO NOT use the grades table
                ' here because the old grades structure has been
                ' removed/reworked.
                '
                ' Grade will temporarily display:
                ' "Not Available"
                '
                ' Remarks will temporarily display:
                ' "Pending"
                '------------------------------------------------

                Dim query As String =
                    "SELECT " &
                    "e.enrollment_id, " &
                    "s.subject_code, " &
                    "s.subject_name, " &
                    "s.units, " &
                    "COALESCE(" &
                    "CONCAT(p.first_name, ' ', p.last_name), " &
                    "'Not Assigned'" &
                    ") AS professor_name, " &
                    "'Not Available' AS grade_status, " &
                    "'Pending' AS remarks " &
                    "FROM enrollments e " &
                    "INNER JOIN subjects s " &
                    "ON e.subject_id = s.subject_id " &
                    "LEFT JOIN professor_subjects ps " &
                    "ON ps.subject_id = e.subject_id " &
                    "LEFT JOIN professors p " &
                    "ON p.professor_id = ps.professor_id " &
                    "WHERE e.student_id = @studentID " &
                    "ORDER BY s.subject_code"


                Using command As New MySqlCommand(
                    query,
                    connection)

                    command.Parameters.AddWithValue(
                        "@studentID",
                        studentID
                    )


                    Using adapter As New MySqlDataAdapter(
                        command)

                        Dim table As New DataTable()


                        adapter.Fill(table)


                        gvSubjects.DataSource =
                            table

                        gvSubjects.DataBind()

                    End Using

                End Using

            End Using


        Catch ex As Exception

            '--------------------------------------------------
            ' Clear the GridView if something goes wrong
            '--------------------------------------------------

            gvSubjects.DataSource = Nothing

            gvSubjects.DataBind()

        End Try

    End Sub


    '==========================================================
    ' LOGOUT
    '==========================================================

    Protected Sub btnLogout_Click(
        ByVal sender As Object,
        ByVal e As EventArgs) Handles btnLogout.Click

        Session.Clear()

        Session.Abandon()

        Response.Redirect("Login.aspx")

    End Sub

End Class

