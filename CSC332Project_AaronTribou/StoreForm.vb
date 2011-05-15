Imports System.Data.SqlClient

Public Class StoreForm

    'Prepare Commands
    Dim selectCommand, modifyCommand As New SqlCommand

    'Make form load boolean
    Dim blnLoad As Boolean

    'declare store counts
    Dim intStores As Integer

    'Prepare structures
    Structure StoreData
        Dim Manager, Address, City, State As String
        Dim StoreNum, Zip, Goal As Integer
        Dim Phone As Int64
        Dim mBonus, sBonus As Decimal
    End Structure

    Dim Store As StoreData

    'declare manager info
    Dim intMatch As Integer
    Dim strMatch, strMgrNum, strMgrs() As String



    Private Sub StoreForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        blnLoad = True

        'Get store numbers and populate cboStores and count stores
        Dim selectStatement As String = _
            "SELECT StoreNumber, Manager FROM Stores"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        'Get Stores
        Try
            'Open connection and load stores and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strStoreNum As String
            cboStores.Items.Clear()
            intStores = 0
            Do While myReader.Read
                strStoreNum = myReader("StoreNumber").ToString
                cboStores.Items.Add(strStoreNum)
                intStores += 1
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


        'Get Managers
        Dim selectManagers As String = "SELECT EmpNumber, FirstName, LastName FROM Employees"
        selectCommand = New SqlCommand(selectManagers, LoginForm.MyConnection)

        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strName, strNum As String
            cboMgr.Items.Clear()
            Do While myReader.Read
                strNum = myReader("EmpNumber").ToString
                If strNum.StartsWith("MGR") Then strMgrNum &= strNum & ","
                strName = myReader("FirstName").ToString
                strName &= " "
                strName &= myReader("LastName").ToString
                If strNum.StartsWith("MGR") Then cboMgr.Items.Add(strName)
            Loop

        Catch ex As SqlException
            MessageBox.Show("Data could not be read into form", "Form Load Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            WelcomeForm.Show()
            Me.Close()

        End Try

        'split manager numbers into array
        strMgrs = strMgrNum.Split(CChar(","))
        ReDim Preserve strMgrs(strMgrs.Length - 2)


        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'Insert first item as placeholder and add new store option
        cboStores.Items.Insert(0, "New Store...")
        cboStores.Items.Insert(0, "Select a Store...")
        cboMgr.Items.Insert(0, "Select a Manager...")
        cboStores.SelectedIndex = 0
        cboMgr.SelectedIndex = -1
        cboState.SelectedIndex = 0

        blnLoad = False


    End Sub

    Private Sub cboStores_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStores.SelectedIndexChanged

        'prevent event during form load
        If blnLoad = True Then
            Exit Sub
        End If

        'if add new store is selected
        If cboStores.SelectedIndex = 1 Then
            'reset all of the objects
            cboMgr.SelectedIndex = 0
            txtAddress.Clear()
            txtCity.Clear()
            txtZip.Clear()
            cboState.SelectedIndex = 0
            btnAccept.Text = "&Add"
            txtPhone.Clear()
            txtGoal.Clear()
            txtMBonus.Clear()
            txtSBonus.Clear()
            cboMgr.Focus()

        ElseIf cboStores.SelectedIndex <> 0 And cboStores.SelectedIndex <> 1 Then
            'if new store is not selected
            btnAccept.Text = "&Modify"

            'get store info
            Dim stmtStore As String = "SELECT * FROM Stores WHERE StoreNumber = @StoreNumber"
            selectCommand = New SqlCommand(stmtStore, LoginForm.MyConnection)
            selectCommand.Parameters.AddWithValue("@StoreNumber", cboStores.SelectedItem.ToString)

            'read info
            Try
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                Dim myReader As SqlDataReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow)
                If myReader.Read Then
                    Store.StoreNum = CInt(myReader("StoreNumber"))
                    Store.Manager = myReader("Manager").ToString
                    Store.Address = myReader("Address").ToString
                    Store.City = myReader("City").ToString
                    Store.State = myReader("State").ToString
                    Store.Zip = CInt(myReader("Zip"))
                    Store.Phone = Convert.ToInt64(myReader("Phone"))
                    Store.Goal = CInt(myReader("PhoneGoal"))
                    Store.mBonus = Decimal.Parse(CStr(myReader("ManagerBonus")))
                    Store.sBonus = Decimal.Parse(CStr(myReader("StoreBonus")))
                    myReader.Close()
                Else
                    MessageBox.Show("Store unable to be retrieved")
                    cboStores.SelectedIndex = 0
                    If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                    Exit Sub
                End If
            Catch ex As SqlException
                MessageBox.Show("Store data unable to be retrieved at this time", "Connection Error")
                cboStores.SelectedIndex = 0
                If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                Exit Sub
            End Try
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()


            'fill in information
            'set Manager
            For i = 0 To strMgrs.Length - 1
                If strMgrs(i) = Store.Manager Then intMatch = i + 1
            Next i
            cboMgr.SelectedIndex = intMatch

            'set state
            Dim intState As Integer
            intState = cboState.FindStringExact(Store.State)
            cboState.SelectedIndex = intState

            'fill in other fields
            txtAddress.Text = Store.Address
            txtCity.Text = Store.City
            txtZip.Text = Store.Zip.ToString
            txtPhone.Text = Store.Phone.ToString
            txtGoal.Text = Store.Goal.ToString
            txtMBonus.Text = Store.mBonus.ToString("F2")
            txtSBonus.Text = Store.sBonus.ToString("F2")

        End If



    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        'reset all of the objects
        cboStores.SelectedIndex = 0
        cboMgr.SelectedIndex = -1
        txtAddress.Clear()
        txtCity.Clear()
        txtZip.Clear()
        cboState.SelectedIndex = 0
        txtPhone.Clear()
        txtGoal.Clear()
        txtMBonus.Clear()
        txtSBonus.Clear()
        cboStores.Focus()

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Me.Close()
        WelcomeForm.Show()

    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click


        Dim changedStore As StoreData
        Dim strChanged As String = "Are you sure you want to modify the following data:" & vbNewLine
        Dim diaAns As DialogResult

        'check for missing or invalid info
        If cboStores.SelectedIndex = 0 Then
            MessageBox.Show("Please select a store first")
            cboStores.Focus()
            Exit Sub
        End If
        If txtAddress.Text = "" Or _
            txtCity.Text = "" Or _
            txtPhone.Text = "" Or _
            txtZip.Text = "" Or _
            cboState.SelectedIndex = -1 Or _
            txtGoal.Text = "" Or _
            txtMBonus.Text = "" Or _
            txtSBonus.Text = "" Or _
            cboMgr.SelectedIndex = 0 Then
            MessageBox.Show("Please fill in all fields before accepting")
            txtAddress.Focus()
            Exit Sub
        End If

        'check numeric fields for numeric data
        Try
            changedStore.Zip = Integer.Parse(txtZip.Text)
            changedStore.Goal = Integer.Parse(txtGoal.Text)
            changedStore.Phone = Int64.Parse(txtPhone.Text)
            changedStore.mBonus = Decimal.Parse(txtMBonus.Text)
            changedStore.sBonus = Decimal.Parse(txtSBonus.Text)
        Catch ex As Exception
            MessageBox.Show("Enter only numbers into Phone, Zip Code, Phone Goal, or either bonus field")
            Exit Sub
        End Try

        'capture other information
        If cboStores.SelectedIndex <> 1 Then changedStore.StoreNum = CInt(cboStores.SelectedItem)
        changedStore.Address = txtAddress.Text
        changedStore.City = txtCity.Text
        changedStore.State = cboState.SelectedItem.ToString
        changedStore.Manager = strMgrs(cboMgr.SelectedIndex - 1)


        'If Add New Store is selected
        If cboStores.SelectedIndex = 1 Then
            Dim insertStatement As String = _
    "INSERT INTO Stores (Manager, Address, City, State, Zip, Phone, PhoneGoal, ManagerBonus, StoreBonus) " & _
"VALUES (@Manager, @Address, @City, @State, @Zip, @Phone, @PhoneGoal, @ManagerBonus, @StoreBonus)"
            modifyCommand = New SqlCommand(insertStatement, LoginForm.MyConnection)
            modifyCommand.Parameters.AddWithValue("@Manager", changedStore.Manager)
            modifyCommand.Parameters.AddWithValue("@Address", changedStore.Address)
            modifyCommand.Parameters.AddWithValue("@City", changedStore.City)
            modifyCommand.Parameters.AddWithValue("@State", changedStore.State)
            modifyCommand.Parameters.AddWithValue("@Zip", changedStore.Zip)
            modifyCommand.Parameters.AddWithValue("@Phone", changedStore.Phone)
            modifyCommand.Parameters.AddWithValue("@PhoneGoal", changedStore.Goal)
            modifyCommand.Parameters.AddWithValue("@ManagerBonus", changedStore.mBonus)
            modifyCommand.Parameters.AddWithValue("@StoreBonus", changedStore.sBonus)


            'execute non query
            Try
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                modifyCommand.ExecuteNonQuery()

            Catch ex As SqlException
                MessageBox.Show("Store add failed." & vbNewLine & vbNewLine & ex.ToString)
                LoginForm.MyConnection.Close()
                Exit Sub
            End Try
            LoginForm.MyConnection.Close()

            'let user know something happened
            MessageBox.Show("Store was successfully added")

        Else
            'If a store is selected to be modified

            'compare to previous fields
            If changedStore.Manager <> Store.Manager Then strChanged &= vbNewLine & "Store Manager"
            If changedStore.Address <> Store.Address Then strChanged &= vbNewLine & "Address"
            If changedStore.City <> Store.City Then strChanged &= vbNewLine & "City"
            If changedStore.State <> Store.State Then strChanged &= vbNewLine & "State"
            If changedStore.Zip <> Store.Zip Then strChanged &= vbNewLine & "Zip Code"
            If changedStore.Phone <> Store.Phone Then strChanged &= vbNewLine & "Phone"
            If changedStore.Goal <> Store.Goal Then strChanged &= vbNewLine & "Phone Goal"
            If changedStore.mBonus <> Store.mBonus Then strChanged &= vbNewLine & "Manager Bonus"
            If changedStore.sBonus <> Store.sBonus Then strChanged &= vbNewLine & "Store Bonus"

            'prompt user for changes found
            diaAns = MessageBox.Show(strChanged, "Update Store", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If diaAns = DialogResult.Yes Then
                'if yes then update record
                Dim updateStatement As String = _
        "UPDATE Stores SET Manager = @Manager, Address = @Address, City = @City, " & _
    "State = @State, Zip = @Zip, Phone = @Phone, PhoneGoal = @PhoneGoal, ManagerBonus = @ManagerBonus, " & _
    "StoreBonus = @StoreBonus WHERE StoreNumber = @StoreNumber"
                modifyCommand = New SqlCommand(updateStatement, LoginForm.MyConnection)
                modifyCommand.Parameters.AddWithValue("@Manager", changedStore.Manager)
                modifyCommand.Parameters.AddWithValue("@Address", changedStore.Address)
                modifyCommand.Parameters.AddWithValue("@City", changedStore.City)
                modifyCommand.Parameters.AddWithValue("@State", changedStore.State)
                modifyCommand.Parameters.AddWithValue("@Zip", changedStore.Zip)
                modifyCommand.Parameters.AddWithValue("@Phone", changedStore.Phone)
                modifyCommand.Parameters.AddWithValue("@PhoneGoal", changedStore.Goal)
                modifyCommand.Parameters.AddWithValue("@ManagerBonus", changedStore.mBonus)
                modifyCommand.Parameters.AddWithValue("@StoreBonus", changedStore.sBonus)
                modifyCommand.Parameters.AddWithValue("@StoreNumber", changedStore.StoreNum)


                'execute non query
                Try
                    If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                    modifyCommand.ExecuteNonQuery()

                Catch ex As SqlException
                    MessageBox.Show("Store modify failed." & vbNewLine & vbNewLine & ex.ToString)
                    LoginForm.MyConnection.Close()
                    Exit Sub
                End Try
                LoginForm.MyConnection.Close()

                'let user know something happened
                MessageBox.Show("Store was successfully modified")


                'if user selects no from dialog box
            Else
                Exit Sub
            End If


        End If

        'clear form and reload
        Call btnReset_Click(sender, e)
        Call StoreForm_Load(sender, e)

    End Sub

End Class