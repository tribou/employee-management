Imports System.Data.SqlClient

Public Class WelcomeForm

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click

        'Exit the program
        LoginForm.Show()
        Me.Close()

    End Sub

    Private Sub btnEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmp.Click

        'show employee form
        Me.Hide()
        EmployeeForm.ShowDialog()

    End Sub

    Private Sub btnStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStore.Click

        'show store form
        Me.Hide()
        StoreForm.ShowDialog()

    End Sub

    Private Sub btnPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPos.Click

        Me.Hide()
        POSForm.ShowDialog()

    End Sub

    Private Sub btnReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReports.Click

        Me.Hide()
        ReportsForm.ShowDialog()

    End Sub

End Class
