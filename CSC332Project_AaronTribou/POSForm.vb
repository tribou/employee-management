Imports System.Data.SqlClient

Public Class POSForm

    'prepare customer structure
    Structure CustData
        Dim CustID, CustZip As Integer
        Dim CustPhone As Int64
        Dim CustName, CustAddress, CustCity, CustState, CustCreateDate As String
    End Structure

    Friend Customer As CustData

    'prepare invoice structure
    Structure InvData
        Dim Number, Emp, Phone, InvoiceDate, Accessory As String
        Dim CustID As Integer
        Dim Total As Decimal
        Dim Ins As Boolean
    End Structure

    Dim Invoice As InvData

    'prepare phone structure
    Structure PhoneData
        Dim PhoneSN(), Name(), Commission(), Price() As String
    End Structure

    Dim Phone As PhoneData

    'prepare accessory structure
    Structure AccessoryData
        Dim AccessorySKU(), Name(), Price() As String
    End Structure

    Dim Accessory As AccessoryData

    'prepare other variables
    Dim blnLoad As Boolean
    Friend blnModify As Boolean
    Dim selectCommand, modifyCommand As SqlCommand
    Dim strCustName, strCustSearch() As String
    Dim decTax, decPrice As Decimal



    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        WelcomeForm.Show()
        Me.Close()

    End Sub

    Private Sub POSForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        blnLoad = True

        'get list of employees, managers, and states
        Dim selectStatement As String = _
            "SELECT EmpNumber FROM Employees"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        'Get Employees
        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strEmpNumber As String
            cboEmp.Items.Clear()
            Do While myReader.Read
                strEmpNumber = myReader("EmpNumber").ToString
                cboEmp.Items.Add(strEmpNumber)
            Loop

        Catch ex As SqlException
            MessageBox.Show("Employees could not be read into form", "Form Load Error")
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
        cboEmp.Items.Insert(0, "Select an Employee...")
        cboEmp.SelectedIndex = 0
        cboState.SelectedIndex = 0
        cboName.Items.Clear()
        cboName.SelectedIndex = -1

        blnLoad = False

    End Sub

    Private Sub cboEmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEmp.SelectedIndexChanged

        If blnLoad = True Then Exit Sub

        'unlock next cbo after employee is selected
        If cboEmp.SelectedIndex <> 0 Then
            cboType.Enabled = True
            cboType.SelectedIndex = 0
        Else
            cboType.Enabled = False
            cboType.SelectedIndex = -1
            cboItem.Enabled = False
            cboItem.SelectedIndex = -1
            chkIns.Checked = False
            chkIns.Enabled = False
        End If

    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged

        Dim strPhone, strName, strComm, strPrice, strAccess As String


        'unlock next cbo after type is selected
        If cboType.SelectedIndex = 0 Then
            cboItem.Enabled = False
            cboItem.SelectedIndex = -1

            'if phone is selected, load phones into item list
        ElseIf cboType.SelectedIndex = 1 Then
            cboItem.Enabled = True
            chkIns.Enabled = True

            'Get Phones

            Dim selectStates As String = "SELECT * FROM Phones"
            selectCommand = New SqlCommand(selectStates, LoginForm.MyConnection)

            Try
                'Open connection and load phones into cbo box
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                Dim myReader As SqlDataReader = selectCommand.ExecuteReader
                cboItem.Items.Clear()
                Do While myReader.Read
                    strPhone &= myReader("PhoneSN").ToString
                    strPhone &= ","
                    strComm &= myReader("Commission").ToString
                    strComm &= ","
                    strPrice &= myReader("Price").ToString
                    strPrice &= ","
                    strName = myReader("PhoneName").ToString
                    cboItem.Items.Add(strName)
                Loop

            Catch ex As SqlException
                MessageBox.Show("Data could not be read into form", "Form Load Error")
                If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                WelcomeForm.Show()
                Me.Close()

            End Try

            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

            'split phonesn and commissions to arrays
            Phone.PhoneSN = strPhone.Split(CChar(","))
            ReDim Preserve Phone.PhoneSN(Phone.PhoneSN.Length - 2)
            Phone.Commission = strComm.Split(CChar(","))
            ReDim Preserve Phone.Commission(Phone.Commission.Length - 2)
            Phone.Price = strPrice.Split(CChar(","))
            ReDim Preserve Phone.Price(Phone.Price.Length - 2)

            'put in top item
            cboItem.Items.Insert(0, "Select an Item...")
            cboItem.SelectedIndex = 0

            'if accessory is selected
        ElseIf cboType.SelectedIndex = 2 Then
            cboItem.Enabled = True
            chkIns.Checked = False
            chkIns.Enabled = False

            'Get Accessories

            Dim selectStates As String = "SELECT * FROM Accessories"
            selectCommand = New SqlCommand(selectStates, LoginForm.MyConnection)

            Try
                'Open connection and load accessories into cbo box
                If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
                Dim myReader As SqlDataReader = selectCommand.ExecuteReader
                cboItem.Items.Clear()
                strPrice = ""
                Do While myReader.Read
                    strAccess &= myReader("AccessorySKU").ToString
                    strAccess &= ","
                    strPrice &= myReader("Price").ToString
                    strPrice &= ","
                    strName = myReader("AccessoryName").ToString
                    cboItem.Items.Add(strName)
                Loop

            Catch ex As SqlException
                MessageBox.Show("Data could not be read into form", "Form Load Error")
                If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                WelcomeForm.Show()
                Me.Close()

            End Try

            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

            'split accessory to array
            Accessory.AccessorySKU = strAccess.Split(CChar(","))
            ReDim Preserve Accessory.AccessorySKU(Accessory.AccessorySKU.Length - 2)
            Accessory.Price = strPrice.Split(CChar(","))
            ReDim Preserve Accessory.Price(Accessory.Price.Length - 2)

            'put in top item
            cboItem.Items.Insert(0, "Select an Item...")
            cboItem.SelectedIndex = 0

        End If

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        'reset all form fields
        cboEmp.SelectedIndex = 0
        cboType.SelectedIndex = -1
        cboType.Enabled = False
        cboItem.Items.Clear()
        cboItem.SelectedIndex = -1
        cboItem.Enabled = False
        cboName.Items.Clear()
        btnSearch.Enabled = False
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtCity.Text = ""
        cboState.SelectedIndex = 0
        txtZip.Text = ""
        chkIns.Checked = False
        chkIns.Enabled = False
        lblTax.Text = "$0.00"
        lblTotal.Text = "$0.00"
        cboEmp.Focus()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim strCustID As String

        'get customer info and load into cboName
        Dim selectStatement As String = _
            "SELECT CustID, CustName FROM Customers WHERE CustName LIKE @Name"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)
        selectCommand.Parameters.AddWithValue("@Name", cboName.Text.PadRight(100, CChar("%")))

        'Get Employees
        Try
            'Open connection and load emps and states into cbo box
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Dim strCustName As String
            cboName.Items.Clear()
            Do While myReader.Read
                strCustID &= myReader("CustID").ToString
                strCustID &= ","
                strCustName = myReader("CustName").ToString
                cboName.Items.Add(strCustName)
            Loop

        Catch ex As SqlException
            MessageBox.Show("Customers could not be read into form" & vbNewLine & ex.ToString, "Customer Search Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            cboName.Focus()
            cboName.SelectAll()
            Exit Sub
        End Try

        'split custIDs
        If strCustID <> "" Then
            strCustSearch = strCustID.Split(CChar(","))
            ReDim Preserve strCustSearch(strCustSearch.Length - 2)
        End If

    End Sub

    Private Sub cboItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboItem.SelectedIndexChanged

        'Make sure first option is not selected
        If cboItem.SelectedIndex = 0 Then

            btnSearch.Enabled = False
            btnNew.Enabled = False
            cboName.Enabled = False
            lblTax.Text = "$0.00"
            lblTotal.Text = "$0.00"
            cboItem.Focus()

        Else
            btnSearch.Enabled = True
            btnNew.Enabled = True
            cboName.Enabled = True

            'fill in pricing
            If cboType.SelectedIndex = 1 Then
                ''if it is a phone
                decPrice = Decimal.Parse(Phone.Price(cboItem.SelectedIndex - 1))
                If chkIns.Checked = True Then decPrice += 5D
                decTax = decPrice * 0.06D
                decPrice += decTax

            ElseIf cboType.SelectedIndex = 2 Then
                ''if it is an accessory
                decPrice = Decimal.Parse(Accessory.Price(cboItem.SelectedIndex - 1))
                decTax = decPrice * 0.06D
                decPrice += decTax

            End If

            lblTax.Text = decTax.ToString("C")
            lblTotal.Text = decPrice.ToString("C")
            cboName.Focus()
        End If


    End Sub

    Private Sub cboName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboName.SelectedIndexChanged
        'if new customer is selected, when submit button is pressed, customer information entered in fields will create a new customer
        'otherwise, other selection will be used
        'if other selection is used, and customer info is modified, user will be prompted to modify existing customer



        btnSearch.Enabled = True
        btnEdit.Enabled = True
        Customer.CustID = Integer.Parse(strCustSearch(cboName.SelectedIndex))
        strCustName = cboName.Text

        'get customer info
        Dim selectStatment As String = "SELECT * FROM Customers WHERE CustID = @CustID"
        selectCommand = New SqlCommand(selectStatment, LoginForm.MyConnection)
        selectCommand.Parameters.AddWithValue("@CustID", Customer.CustID)

        'read info
        Try
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow)
            If myReader.Read Then
                Customer.CustID = CInt(myReader("CustID"))
                Customer.CustName = myReader("CustName").ToString
                Customer.CustAddress = myReader("CustAddress").ToString
                Customer.CustCity = myReader("CustCity").ToString
                Customer.CustState = myReader("CustState").ToString
                Customer.CustZip = CInt(myReader("CustZip"))
                Customer.CustPhone = Convert.ToInt64(myReader("CustPhone"))
                Customer.CustCreateDate = myReader("CustCreateDate").ToString
                myReader.Close()
            Else
                MessageBox.Show("Customer unable to be retrieved")
                cboName.SelectedIndex = 0
                If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
                Exit Sub
            End If
        Catch ex As SqlException
            MessageBox.Show("Customer data unable to be retrieved at this time", "Connection Error")
            cboName.SelectedIndex = 0
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try
        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'Fill in data
        txtAddress.Text = Customer.CustAddress
        txtPhone.Text = Customer.CustPhone.ToString
        txtCity.Text = Customer.CustCity
        txtZip.Text = Customer.CustZip.ToString

        'set state
        Dim intState As Integer
        intState = cboState.FindStringExact(Customer.CustState)
        cboState.SelectedIndex = intState


    End Sub

    Private Sub chkIns_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIns.CheckedChanged

        If chkIns.Checked = True Then
            'add $5 for insurance
            decPrice -= decTax
            decPrice += 5D
            decTax = decPrice * 0.06D
            decPrice += decTax
            lblTax.Text = decTax.ToString("C")
            lblTotal.Text = decPrice.ToString("C")

        ElseIf chkIns.Checked = False Then
            'take $5 insurance off
            decPrice -= decTax
            decPrice -= 5D
            decTax = decPrice * 0.06D
            decPrice += decTax
            lblTax.Text = decTax.ToString("C")
            lblTotal.Text = decPrice.ToString("C")
        End If

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        'submit invoice info


        'Make sure customer selection was made and fields were filled
        Dim blnProceed As Boolean = False

        'make sure all fields are filled
        If cboItem.SelectedIndex = 0 Or cboName.Text = "" Then
            MessageBox.Show("Please fill in all fields before submitting")
            Exit Sub
        End If

        For i = 0 To cboName.Items.Count - 1
            If cboName.Text = cboName.Items(i).ToString Then blnProceed = True
        Next i


        If blnProceed = False Then
            MessageBox.Show("Please select a customer")
            cboName.Focus()
            cboName.SelectAll()
            Exit Sub
        End If

        'populate invoice info
        Invoice.Emp = cboEmp.SelectedItem.ToString
        Invoice.CustID = Customer.CustID
        Invoice.InvoiceDate = DateAndTime.DateString
        Invoice.Ins = chkIns.Checked
        Invoice.Total = Decimal.Parse(lblTotal.Text.Remove(0, 1))

        'if phone is being sold
        If cboType.SelectedIndex = 1 Then
            Invoice.Phone = Phone.PhoneSN(cboItem.SelectedIndex - 1)

            Dim insertStatement As String = _
    "INSERT INTO Invoices (EmpNumber, CustID, PhoneSN, InvoiceDate, Insurance, Total) " & _
"VALUES (@EmpNumber, @CustID, @PhoneSN, @InvoiceDate, @Insurance, @Total)"
            modifyCommand = New SqlCommand(insertStatement, LoginForm.MyConnection)
            modifyCommand.Parameters.AddWithValue("@EmpNumber", Invoice.Emp)
            modifyCommand.Parameters.AddWithValue("@CustID", Invoice.CustID)
            modifyCommand.Parameters.AddWithValue("@PhoneSN", Invoice.Phone)
            modifyCommand.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate)
            modifyCommand.Parameters.AddWithValue("@Insurance", Invoice.Ins)
            modifyCommand.Parameters.AddWithValue("@Total", Invoice.Total)
        Else
            'if accessory is being sold
            Invoice.Accessory = Accessory.AccessorySKU(cboItem.SelectedIndex - 1).ToString

            Dim insertStatement As String = _
    "INSERT INTO Invoices (EmpNumber, CustID, AccessorySKU, InvoiceDate, Insurance, Total) " & _
"VALUES (@EmpNumber, @CustID, @AccessorySKU, @InvoiceDate, @Insurance, @Total)"
            modifyCommand = New SqlCommand(insertStatement, LoginForm.MyConnection)
            modifyCommand.Parameters.AddWithValue("@EmpNumber", Invoice.Emp)
            modifyCommand.Parameters.AddWithValue("@CustID", Invoice.CustID)
            modifyCommand.Parameters.AddWithValue("@AccessorySKU", Invoice.Accessory)
            modifyCommand.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate)
            modifyCommand.Parameters.AddWithValue("@Insurance", Invoice.Ins)
            modifyCommand.Parameters.AddWithValue("@Total", Invoice.Total)
        End If
        


        'execute non query
        Try
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            modifyCommand.ExecuteNonQuery()

        Catch ex As SqlException
            MessageBox.Show("Invoice save failed." & vbNewLine & vbNewLine & ex.ToString)
            LoginForm.MyConnection.Close()
            Exit Sub
        End Try
        LoginForm.MyConnection.Close()

        'let user know something happened
        MessageBox.Show("Invoice was successfully saved")

        Call btnReset_Click(sender, e)
        cboName.Text = ""
        Call POSForm_Load(sender, e)

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'create a new customer
        blnModify = False
        CustomerForm.lblTitle.Text = "New Customer"
        CustomerForm.btnAccept.Text = "Sa&ve"
        Me.Hide()
        CustomerForm.ShowDialog()


    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        'edit current customer

        Dim blnProceed As Boolean = False


        'make sure customer is selected
        For i = 0 To cboName.Items.Count - 1
            If cboName.Text = cboName.Items(i).ToString Then blnProceed = True
        Next i


        If blnProceed = False Then
            MessageBox.Show("Please select a customer to edit")
            cboName.Focus()
            Exit Sub
        End If


        blnModify = True
        'load current info into customerform
        CustomerForm.btnAccept.Text = "&Modify"
        CustomerForm.lblTitle.Text = "Modify Customer"
        Me.Hide()
        CustomerForm.ShowDialog()


    End Sub
End Class