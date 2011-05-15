Imports System.Data.SqlClient

Public Class CustomerForm

    'prepare customer structure
    Structure CustData
        Dim CustID, CustZip As Integer
        Dim CustPhone As Int64
        Dim CustName, CustAddress, CustCity, CustState, CustCreateDate As String
    End Structure

    Dim modifyCommand, selectCommand As New SqlCommand


    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        Dim changedCustomer As CustData
        Dim strChanged As String = "Are you sure you want to modify the following data:" & vbNewLine
        Dim diaAns As DialogResult

        

            'check to make sure fields are filled
            If txtAddress.Text = "" Or _
    txtCity.Text = "" Or _
    txtPhone.Text = "" Or _
    txtZip.Text = "" Or _
    cboState.SelectedIndex = -1 Or _
    txtName.Text = "" Then
                MessageBox.Show("Please fill in all fields")
                txtName.Focus()
                Exit Sub
            End If

            'try numeric data
            Try
                changedCustomer.CustPhone = Int64.Parse(txtPhone.Text)
                changedCustomer.CustZip = Integer.Parse(txtZip.Text)
            Catch ex As Exception
                MessageBox.Show("Enter only numbers for Phone and Zip Code")
                Exit Sub
            End Try

            'capture rest of customer info
            changedCustomer.CustAddress = txtAddress.Text
            changedCustomer.CustCity = txtCity.Text
            changedCustomer.CustName = txtName.Text
            changedCustomer.CustState = cboState.SelectedItem.ToString
        changedCustomer.CustCreateDate = DateAndTime.DateString

        'if adding new customer
        'check for accept button text
        If btnAccept.Text = "Sa&ve" Then

            'execute sql statement
            Dim insStatement As String = _
    "INSERT INTO Customers (CustName, CustAddress, CustCity, CustState, CustZip, CustPhone, CustCreateDate) " & _
"VALUES (@CustName, @CustAddress, @CustCity, @CustState, @CustZip, @CustPhone, @CustCreateDate)"
            modifyCommand = New SqlCommand(insStatement, LoginForm.MyConnection)
            modifyCommand.Parameters.AddWithValue("@CustName", changedCustomer.CustName)
            modifyCommand.Parameters.AddWithValue("@CustAddress", changedCustomer.CustAddress)
            modifyCommand.Parameters.AddWithValue("@CustCity", changedCustomer.CustCity)
            modifyCommand.Parameters.AddWithValue("@CustState", changedCustomer.CustState)
            modifyCommand.Parameters.AddWithValue("@CustZip", changedCustomer.CustZip)
            modifyCommand.Parameters.AddWithValue("@CustPhone", changedCustomer.CustPhone)
            modifyCommand.Parameters.AddWithValue("@CustCreateDate", changedCustomer.CustCreateDate)


            'execute non query
            Try
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                modifyCommand.ExecuteNonQuery()

            Catch ex As SqlException
                MessageBox.Show("Customer add failed." & vbNewLine & vbNewLine & ex.ToString)
                LoginForm.MyConnection.Close()
                Exit Sub
            End Try
            LoginForm.MyConnection.Close()

            'let user know something happened
            MessageBox.Show("Customer was successfully added")




        ElseIf btnAccept.Text = "&Modify" Then
            'if accept button is "modify" then modify existing customer

            changedCustomer.CustID = POSForm.Customer.CustID

            'compare to previous fields
            If changedCustomer.CustName <> POSForm.Customer.CustName Then strChanged &= vbNewLine & "Customer Name"
            If changedCustomer.CustAddress <> POSForm.Customer.CustAddress Then strChanged &= vbNewLine & "Address"
            If changedCustomer.CustCity <> POSForm.Customer.CustCity Then strChanged &= vbNewLine & "City"
            If changedCustomer.CustState <> POSForm.Customer.CustState Then strChanged &= vbNewLine & "State"
            If changedCustomer.CustZip <> POSForm.Customer.CustZip Then strChanged &= vbNewLine & "Zip Code"
            If changedCustomer.CustPhone <> POSForm.Customer.CustPhone Then strChanged &= vbNewLine & "Phone"

            'prompt user for changes found
            diaAns = MessageBox.Show(strChanged, "Update Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If diaAns = DialogResult.Yes Then
                'if yes then update record
                Dim updateStatement As String = _
        "UPDATE Customers SET CustName = @CustName, CustAddress = @CustAddress, CustCity = @CustCity, " & _
    "CustState = @CustState, CustZip = @CustZip, CustPhone = @CustPhone WHERE CustID = @CustID"
                modifyCommand = New SqlCommand(updateStatement, LoginForm.MyConnection)
                modifyCommand.Parameters.AddWithValue("@CustName", changedCustomer.CustName)
                modifyCommand.Parameters.AddWithValue("@CustAddress", changedCustomer.CustAddress)
                modifyCommand.Parameters.AddWithValue("@CustCity", changedCustomer.CustCity)
                modifyCommand.Parameters.AddWithValue("@CustState", changedCustomer.CustState)
                modifyCommand.Parameters.AddWithValue("@CustZip", changedCustomer.CustZip)
                modifyCommand.Parameters.AddWithValue("@CustPhone", changedCustomer.CustPhone)
                modifyCommand.Parameters.AddWithValue("@CustID", changedCustomer.CustID)


                'execute non query
                Try
                    If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                    modifyCommand.ExecuteNonQuery()

                Catch ex As SqlException
                    MessageBox.Show("Customer modify failed." & vbNewLine & vbNewLine & ex.ToString)
                    LoginForm.MyConnection.Close()
                    Exit Sub
                End Try
                LoginForm.MyConnection.Close()

                'let user know something happened
                MessageBox.Show("Customer was successfully modified")


                'if user selects no from dialog box
            Else
                Exit Sub
            End If

        End If

        POSForm.cboName.Text = changedCustomer.CustName
        POSForm.btnSearch.PerformClick()
        POSForm.txtAddress.Text = changedCustomer.CustAddress
        POSForm.txtPhone.Text = changedCustomer.CustPhone.ToString
        POSForm.txtCity.Text = changedCustomer.CustCity
        POSForm.txtZip.Text = changedCustomer.CustZip.ToString
        POSForm.cboState.SelectedIndex = cboState.FindStringExact(changedCustomer.CustState)

        POSForm.Show()
        Me.Close()

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        'go back to POS Form
        POSForm.Show()
        Me.Close()

    End Sub

    Private Sub CustomerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Get States

        Dim selectStates As String = "SELECT StateCode FROM States"
        selectCommand = New SqlCommand(selectStates, LoginForm.MyConnection)

        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strState As String
            Do While myReader.Read
                strState = myReader("StateCode").ToString
                cboState.Items.Add(strState)
            Loop

        Catch ex As SqlException
            MessageBox.Show("Data could not be read into form", "Form Load Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            WelcomeForm.Show()
            Me.Close()

        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()


        'fill in fields to modify customer
        If POSForm.blnModify = True Then
            txtName.Text = POSForm.cboName.Text
            txtAddress.Text = POSForm.txtAddress.Text
            txtCity.Text = POSForm.txtCity.Text
            txtZip.Text = POSForm.txtZip.Text
            txtPhone.Text = POSForm.txtPhone.Text
            cboState.SelectedIndex = POSForm.cboState.SelectedIndex
        End If

        If POSForm.blnModify = False Then
            cboState.SelectedIndex = 0
            txtName.Text = ""
            txtAddress.Text = ""
            txtCity.Text = ""
            txtZip.Text = ""
            txtPhone.Text = ""
        End If

        txtName.Focus()
        txtName.SelectAll()

    End Sub

End Class