Imports System.Data.SqlClient

Public Class EmployeeForm

    'Prepare Commands
    Dim selectCommand, modifyCommand As New SqlCommand

    'Make form load boolean
    Dim blnLoad As Boolean

    'declare mgr and emp counts
    Dim intEmp, intMgr As Integer

    'Prepare structures
    Structure EmpData
        Dim EmpID, First, Last, Address, City, State As String
        Dim Zip, Store As Integer
        Dim Phone As Int64
    End Structure

    Dim Employee As EmpData


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        'Close the form
        Me.Close()
        WelcomeForm.Show()

    End Sub


    Private Sub EmployeeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        blnLoad = True

        'Get employee numbers and populate cboID and count employees and managers
        Dim selectStatement As String = _
            "SELECT EmpNumber FROM Employees"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        'Get Employees
        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strEmpNumber As String
            cboID.Items.Clear()
            intEmp = 0
            intMgr = 0
            Do While myReader.Read
                strEmpNumber = myReader("EmpNumber").ToString
                cboID.Items.Add(strEmpNumber)
                If strEmpNumber.StartsWith("EMP") Then intEmp += 1
                If strEmpNumber.StartsWith("MGR") Then intMgr += 1
            Loop

        Catch ex As SqlException
            MessageBox.Show("Employees could not be read into form", "Form Load Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            WelcomeForm.Show()
            Me.Close()

        End Try


        'Get employee numbers and populate cboID and count employees and managers
        Dim selectStores As String = _
            "SELECT StoreNumber FROM Stores"
        selectCommand = New SqlCommand(selectStores, LoginForm.MyConnection)

        'Get Employees
        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strStoreNumber As String
            cboStore.Items.Clear()
            Do While myReader.Read
                strStoreNumber = myReader("StoreNumber").ToString
                cboStore.Items.Add(strStoreNumber)
            Loop

        Catch ex As SqlException
            MessageBox.Show("Stores could not be read into form", "Form Load Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            WelcomeForm.Show()
            Me.Close()

        End Try


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

        'Insert first item as placeholder and add new employee option
        cboID.Items.Insert(0, "New Employee...")
        cboID.Items.Insert(0, "Select an Employee...")
        cboStore.Items.Insert(0, "Select a Store...")
        cboStore.SelectedIndex = 0
        cboID.SelectedIndex = 0
        cboType.SelectedIndex = 0
        cboState.SelectedIndex = 0

        blnLoad = False

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        'reset all of the objects
        cboID.SelectedIndex = 0
        cboType.SelectedIndex = 0
        txtFirst.Clear()
        txtLast.Clear()
        txtAddress.Clear()
        txtCity.Clear()
        txtZip.Clear()
        cboState.SelectedIndex = 0
        cboStore.SelectedIndex = 0
        cboStore.Enabled = False
        txtPhone.Clear()
        cboID.Focus()

    End Sub



    Private Sub cboID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboID.SelectedIndexChanged

        If blnLoad = True Then
            Exit Sub
        End If


        'If add new employee is selected
        If cboID.SelectedIndex = 1 Then
            cboType.Enabled = True
            'reset all of the objects
            cboType.SelectedIndex = 0
            txtFirst.Clear()
            txtLast.Clear()
            txtAddress.Clear()
            txtCity.Clear()
            txtZip.Clear()
            cboState.SelectedIndex = 0
            btnAccept.Text = "&Add"
            txtPhone.Clear()
            txtFirst.Focus()

            'if new employee is not selected
        ElseIf cboID.SelectedIndex <> 0 And cboID.SelectedIndex <> 1 Then

            btnAccept.Text = "&Modify"

            
                'get employee info command
                Dim stmtEmployee As String = "SELECT * FROM Employees WHERE EmpNumber = @EmpNumber"
                selectCommand = New SqlCommand(stmtEmployee, LoginForm.MyConnection)
                selectCommand.Parameters.AddWithValue("@EmpNumber", cboID.SelectedItem.ToString)

                
            'read info
            Try
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                Dim empReader As SqlDataReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow)
                If empReader.Read Then
                    Employee.EmpID = empReader("EmpNumber").ToString
                    Employee.First = empReader("FirstName").ToString
                    Employee.Last = empReader("LastName").ToString
                    Employee.Address = empReader("Address").ToString
                    Employee.City = empReader("City").ToString
                    Employee.State = empReader("State").ToString
                    Employee.Zip = CInt(empReader("Zip"))
                    Employee.Phone = Convert.ToInt64(empReader("Phone"))
                    If Employee.EmpID.StartsWith("EMP") Then Employee.Store = CInt(empReader("StoreNumber"))
                    empReader.Close()
                Else
                    MessageBox.Show("Employee unable to be retrieved")
                    cboID.SelectedIndex = 0
                    If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                    Exit Sub
                End If
            Catch ex As SqlException
                MessageBox.Show("Employee data unable to be retrieved at this time", "Connection Error")
                cboID.SelectedIndex = 0
                If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                Exit Sub
            End Try
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

            'set type
            Select Case Employee.EmpID.StartsWith("MGR")
                Case True
                    cboType.SelectedIndex = 2
                    cboStore.Enabled = False
                Case False
                    cboType.SelectedIndex = 1
                    cboStore.Enabled = True
            End Select
            cboType.Enabled = False

            'set state
            Dim intState As Integer
            intState = cboState.FindStringExact(Employee.State)
            cboState.SelectedIndex = intState

            'fill in other fields
            txtFirst.Text = Employee.First
            txtLast.Text = Employee.Last
            txtAddress.Text = Employee.Address
            txtCity.Text = Employee.City
            txtZip.Text = Employee.Zip.ToString
            txtPhone.Text = Employee.Phone.ToString
            If Employee.EmpID.StartsWith("EMP") Then cboStore.SelectedIndex = cboStore.FindStringExact(Employee.Store.ToString)

            End If

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        Dim intZip As Integer
        Dim intPhone As Int64
        Dim changedEmployee As EmpData
        Dim strChanged As String = "Are you sure you want to modify the following data:" & vbNewLine
        Dim diaAns As DialogResult

        'check for missing or invalid info
        If cboID.SelectedIndex = 0 Then
            MessageBox.Show("Please select an employee first")
            cboID.Focus()
            Exit Sub
        End If
        If txtFirst.Text = "" Or _
            txtLast.Text = "" Or _
            txtAddress.Text = "" Or _
            txtCity.Text = "" Or _
            txtPhone.Text = "" Or _
            txtZip.Text = "" Or _
            cboState.SelectedIndex = -1 Or _
            cboType.SelectedIndex = 0 Then
            MessageBox.Show("Please fill in all fields before accepting")
            txtFirst.Focus()
            Exit Sub
        End If
        If cboType.SelectedIndex = 1 Then
            If cboStore.SelectedIndex = 0 Then
                MessageBox.Show("Please assign employee to a store")
                cboStore.Focus()
                Exit Sub
            End If
        End If

        'check zip and phone for numeric data
        Try
            intZip = Integer.Parse(txtZip.Text)
            intPhone = Int64.Parse(txtPhone.Text)
        Catch ex As Exception
            MessageBox.Show("Enter only numbers into Phone or Zip Code fields")
            Exit Sub
        End Try

        'If Add New Employee is chosen
        If cboID.SelectedIndex = 1 Then

            'insert sql command
            If cboType.SelectedIndex = 1 Then
                'if employee type is selected
                Dim strEmpNumber As String
                intEmp += 1
                strEmpNumber = intEmp.ToString
                strEmpNumber = strEmpNumber.PadLeft(7, CChar("0"))
                strEmpNumber = strEmpNumber.Insert(0, "EMP")
                Dim insertStatement As String = _
                    "INSERT INTO Employees (EmpNumber, FirstName, LastName, Address, City, State, Zip, Phone, StoreNumber) " & _
                "VALUES (@EmpNumber, @FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @StoreNumber)"
                modifyCommand = New SqlCommand(insertStatement, LoginForm.MyConnection)
                modifyCommand.Parameters.AddWithValue("@EmpNumber", strEmpNumber)
                modifyCommand.Parameters.AddWithValue("@FirstName", txtFirst.Text)
                modifyCommand.Parameters.AddWithValue("@LastName", txtLast.Text)
                modifyCommand.Parameters.AddWithValue("@Address", txtAddress.Text)
                modifyCommand.Parameters.AddWithValue("@City", txtCity.Text)
                modifyCommand.Parameters.AddWithValue("@State", cboState.SelectedItem.ToString)
                modifyCommand.Parameters.AddWithValue("@Zip", intZip)
                modifyCommand.Parameters.AddWithValue("@Phone", intPhone)
                modifyCommand.Parameters.AddWithValue("@StoreNumber", Integer.Parse(cboStore.SelectedItem.ToString))

            ElseIf cboType.SelectedIndex = 2 Then
                'if manager type is selected
                Dim strMgrNumber As String
                intMgr += 1
                strMgrNumber = intMgr.ToString
                strMgrNumber = strMgrNumber.PadLeft(7, CChar("0"))
                strMgrNumber = strMgrNumber.Insert(0, "MGR")
                Dim insertStatement As String = _
                    "INSERT INTO Employees (EmpNumber, FirstName, LastName, Address, City, State, Zip, Phone) " & _
                "VALUES (@EmpNumber, @FirstName, @LastName, @Address, @City, @State, @Zip, @Phone)"
                modifyCommand = New SqlCommand(insertStatement, LoginForm.MyConnection)
                modifyCommand.Parameters.AddWithValue("@EmpNumber", strMgrNumber)
                modifyCommand.Parameters.AddWithValue("@FirstName", txtFirst.Text)
                modifyCommand.Parameters.AddWithValue("@LastName", txtLast.Text)
                modifyCommand.Parameters.AddWithValue("@Address", txtAddress.Text)
                modifyCommand.Parameters.AddWithValue("@City", txtCity.Text)
                modifyCommand.Parameters.AddWithValue("@State", cboState.SelectedItem.ToString)
                modifyCommand.Parameters.AddWithValue("@Zip", intZip)
                modifyCommand.Parameters.AddWithValue("@Phone", intPhone)
            End If
            'add to database
            Try
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                modifyCommand.ExecuteNonQuery()

            Catch ex As SqlException
                MessageBox.Show("Employee add failed." & vbNewLine & vbNewLine & ex.ToString)
                LoginForm.MyConnection.Close()
                Exit Sub
            End Try
            LoginForm.MyConnection.Close()

            'Let user know employee was added
            MessageBox.Show("Employee was successfully added")


        Else
            'modify the current employee

            'capture current state of fields
            changedEmployee.First = txtFirst.Text
            changedEmployee.Last = txtLast.Text
            changedEmployee.Address = txtAddress.Text
            changedEmployee.City = txtCity.Text
            changedEmployee.State = cboState.SelectedItem.ToString
            changedEmployee.Zip = intZip
            changedEmployee.Phone = intPhone
            If Employee.EmpID.StartsWith("EMP") Then
                changedEmployee.Store = Integer.Parse(cboStore.SelectedItem.ToString)
            End If

            'compare to previous fields
            If changedEmployee.First <> Employee.First Then strChanged &= vbNewLine & "First Name"
            If changedEmployee.Last <> Employee.Last Then strChanged &= vbNewLine & "Last Name"
            If changedEmployee.Address <> Employee.Address Then strChanged &= vbNewLine & "Address"
            If changedEmployee.City <> Employee.City Then strChanged &= vbNewLine & "City"
            If changedEmployee.State <> Employee.State Then strChanged &= vbNewLine & "State"
            If changedEmployee.Zip <> Employee.Zip Then strChanged &= vbNewLine & "Zip Code"
            If changedEmployee.Phone <> Employee.Phone Then strChanged &= vbNewLine & "Phone"
            If Employee.EmpID.StartsWith("EMP") Then
                If changedEmployee.Store <> Employee.Store Then strChanged &= vbNewLine & "Store"
            End If

            'prompt user for changes found
            diaAns = MessageBox.Show(strChanged, "Update Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If diaAns = DialogResult.Yes Then
                'modify employee statement
                If cboType.SelectedIndex = 1 Then
                    Dim updateStatement As String = _
        "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City, " & _
    "State = @State, Zip = @Zip, Phone = @Phone, StoreNumber = @StoreNumber WHERE EmpNumber = @EmpNumber"
                    modifyCommand = New SqlCommand(updateStatement, LoginForm.MyConnection)
                    modifyCommand.Parameters.AddWithValue("@EmpNumber", Employee.EmpID)
                    modifyCommand.Parameters.AddWithValue("@FirstName", txtFirst.Text)
                    modifyCommand.Parameters.AddWithValue("@LastName", txtLast.Text)
                    modifyCommand.Parameters.AddWithValue("@Address", txtAddress.Text)
                    modifyCommand.Parameters.AddWithValue("@City", txtCity.Text)
                    modifyCommand.Parameters.AddWithValue("@State", cboState.SelectedItem.ToString)
                    modifyCommand.Parameters.AddWithValue("@Zip", intZip)
                    modifyCommand.Parameters.AddWithValue("@Phone", intPhone)
                    modifyCommand.Parameters.AddWithValue("@StoreNumber", changedEmployee.Store)

                    'modify manager statement
                ElseIf cboType.SelectedIndex = 2 Then
                    Dim updateStatement As String = _
        "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City, " & _
    "State = @State, Zip = @Zip, Phone = @Phone WHERE EmpNumber = @EmpNumber"
                    modifyCommand = New SqlCommand(updateStatement, LoginForm.MyConnection)
                    modifyCommand.Parameters.AddWithValue("@EmpNumber", Employee.EmpID)
                    modifyCommand.Parameters.AddWithValue("@FirstName", txtFirst.Text)
                    modifyCommand.Parameters.AddWithValue("@LastName", txtLast.Text)
                    modifyCommand.Parameters.AddWithValue("@Address", txtAddress.Text)
                    modifyCommand.Parameters.AddWithValue("@City", txtCity.Text)
                    modifyCommand.Parameters.AddWithValue("@State", cboState.SelectedItem.ToString)
                    modifyCommand.Parameters.AddWithValue("@Zip", intZip)
                    modifyCommand.Parameters.AddWithValue("@Phone", intPhone)

                End If
                'execute non query
                Try
                    If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                    modifyCommand.ExecuteNonQuery()
                Catch ex As SqlException
                    MessageBox.Show("Employee modify failed." & vbNewLine & vbNewLine & ex.ToString)
                    LoginForm.MyConnection.Close()
                    Exit Sub
                End Try
                LoginForm.MyConnection.Close()

                'let user know something happened
                MessageBox.Show("Employee was successfully modified")
            Else
                Exit Sub
            End If

            End If

            'reset and reload form
            Call btnReset_Click(sender, e)
            Call EmployeeForm_Load(sender, e)

    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged

        If cboType.SelectedIndex = 1 Then
            cboStore.Enabled = True
        Else
            cboStore.Enabled = False
            cboStore.SelectedIndex = 0
        End If

    End Sub
End Class