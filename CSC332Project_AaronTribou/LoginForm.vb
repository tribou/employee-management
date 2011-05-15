Imports System.Data.SqlClient

Public Class LoginForm

    Friend MyConnection As New SqlConnection()
    Dim myConnectionString As String


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Dim diaAns As DialogResult

        'Confirm Exit
        diaAns = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If diaAns = DialogResult.No Then Exit Sub

        'Exit the program
        If MyConnection.State = ConnectionState.Open Then MyConnection.Close()
        Me.Close()

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Dim strUser, strPass As String

        'Check for user and pass
        If txtUser.Text = "" Or txtPass.Text = "" Then
            MessageBox.Show("Please fill in both fields", "Input Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUser.Clear()
            txtPass.Clear()
            txtUser.Focus()
            Exit Sub
        End If

        'Create connection string
        strUser = txtUser.Text
        strPass = txtPass.Text

        myConnectionString = "Data Source=AARONTRIBOU-PC\SQLEXPRESS;Initial Catalog=CSC332Project;User ID=" & strUser & ";Password=" & _
            strPass & ";MultipleActiveResultSets=True"
        MyConnection.ConnectionString = myConnectionString


        'Test connection string
        Try
            MyConnection.Open()
        Catch ex As SqlException
            MessageBox.Show(ex.Message & vbNewLine & vbNewLine & "Please re-enter username and password, or try again later.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUser.Clear()
            txtPass.Clear()
            txtUser.Focus()
            MyConnection.Close()
            Exit Sub
        End Try

        'If it works, close the connection and clear login info
        MyConnection.Close()
        txtUser.Clear()
        txtPass.Clear()
        txtUser.Focus()

        'And open the welcome window
        Me.Hide()
        WelcomeForm.ShowDialog()

    End Sub

    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtUser.Text = "atribou"
        txtPass.Text = "M00103152"

    End Sub
End Class