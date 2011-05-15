Imports System.Data.SqlClient
Imports System.IO

Public Class ReportsForm

    'setup printing variables
    Dim printFont As New Font("Arial", 10)
    Dim printFont2 As New Font("Arial", 11)
    Dim boldFont As New Font("Arial", 11, FontStyle.Bold)
    Dim headFont As New Font("Arial", 11, FontStyle.Bold)
    Dim fontColor As Color = Color.Black
    Dim intStart As Integer


    'STORE REPORT VARIABLES
    Structure StoreStructure
        Dim Add(), City(), St(), Mgr(), Num(), Zip(), Goal(), Pho(), Bon(), MBon(), SoldEnough() As String
    End Structure
    Dim Store As StoreStructure

    Structure ManagerStructure
        Dim Num(), First(), Last(), Add(), City(), St(), Zip(), Pho(), Bon() As String
    End Structure
    Dim Mgr As ManagerStructure

    Structure EmployeeStructure
        Dim Num(), First(), Last(), Add(), City(), St(), Zip(), Store(), Pho() As String
    End Structure
    Dim Emp As EmployeeStructure


    'INVOICE REPORT VARIABLES
    Structure InvoiceStructure
        Dim Num(), Dat(), Total(), First(), Last(), Buy(), Ins(), Cust(), Add(), City(), St(), Zip(), Pho(), SDate() As String
    End Structure
    Dim Inv As InvoiceStructure


    'PAYCHECK REPORT VARIABLES
    Structure PhonesStructure
        Dim Num(), First(), Last(), Add(), City(), St(), Zip(), Pho(), Sold(), Comm(), Acc() As String
    End Structure
    Dim Pho As PhonesStructure

    Structure AccessoriesStructure
        Dim Num(), Acc() As String
    End Structure
    Dim Access As AccessoriesStructure
    Dim payStore As StoreStructure
    Dim blnGoal As Boolean = False
    Dim intTotalSold As Integer

    Structure EmployeeStores
        Dim Num(), Emp(), Sold() As String
    End Structure
    Dim empStore As EmployeeStores

    Structure StoreCountData
        Dim Num As String
        Dim Sold As Integer
        Dim Bon As Boolean
    End Structure
    Dim storeCount() As StoreCountData


    'OTHER VARIABLES
    Dim selectCommand As SqlCommand


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        WelcomeForm.Show()
        Me.Close()

    End Sub

    Private Sub btnStores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStores.Click

        Dim strNum, strFirst, strLast, strAdd, strCity, strSt, strZip, strPho, strBon, strMBon, strGoal, strMgr, strStore As String
        Dim decMBon, decBon As Decimal


        'fill all structures with info

        ''get store info
        Dim selectStatement As String = "SELECT * FROM Stores"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        Try
            'Open connection and load stores into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("StoreNumber").ToString
                strNum &= ","
                strMgr &= myReader("Manager").ToString
                strMgr &= ","
                strAdd &= myReader("Address").ToString
                strAdd &= ","
                strCity &= myReader("City").ToString
                strCity &= ","
                strSt &= myReader("State").ToString
                strSt &= ","
                strZip &= myReader("Zip").ToString
                strZip &= ","
                strPho &= myReader("Phone").ToString
                strPho &= ","
                strGoal &= myReader("PhoneGoal").ToString
                strGoal &= ","
                decMBon = Decimal.Parse(myReader("ManagerBonus").ToString)
                strMBon &= decMBon.ToString("F2")
                strMBon &= ","
                decBon = Decimal.Parse(myReader("StoreBonus").ToString)
                strBon &= decBon.ToString("F2")
                strBon &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Store data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split store info to arrays
        Store.Num = strNum.Split(CChar(","))
        ReDim Preserve Store.Num(Store.Num.Length - 2)
        Store.Mgr = strMgr.Split(CChar(","))
        ReDim Preserve Store.Mgr(Store.Mgr.Length - 2)
        Store.Add = strAdd.Split(CChar(","))
        ReDim Preserve Store.Add(Store.Add.Length - 2)
        Store.City = strCity.Split(CChar(","))
        ReDim Preserve Store.City(Store.City.Length - 2)
        Store.St = strSt.Split(CChar(","))
        ReDim Preserve Store.St(Store.St.Length - 2)
        Store.Zip = strZip.Split(CChar(","))
        ReDim Preserve Store.Zip(Store.Zip.Length - 2)
        Store.Pho = strPho.Split(CChar(","))
        ReDim Preserve Store.Pho(Store.Pho.Length - 2)
        Store.Goal = strGoal.Split(CChar(","))
        ReDim Preserve Store.Goal(Store.Goal.Length - 2)
        Store.MBon = strMBon.Split(CChar(","))
        ReDim Preserve Store.MBon(Store.MBon.Length - 2)
        Store.Bon = strBon.Split(CChar(","))
        ReDim Preserve Store.Bon(Store.Bon.Length - 2)


        ''get manager info
        selectStatement = "SELECT * FROM Employees WHERE EmpNumber LIKE 'MGR%%%%%%%'"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)
        strNum = ""
        strFirst = ""
        strLast = ""
        strAdd = ""
        strCity = ""
        strSt = ""
        strZip = ""
        strPho = ""
        strBon = ""
        strMBon = ""
        strGoal = ""
        strMgr = ""
        strStore = ""
        Try
            'Open connection and load data into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("EmpNumber").ToString
                strNum &= ","
                strFirst &= myReader("FirstName").ToString
                strFirst &= ","
                strLast &= myReader("LastName").ToString
                strLast &= ","
                strAdd &= myReader("Address").ToString
                strAdd &= ","
                strCity &= myReader("City").ToString
                strCity &= ","
                strSt &= myReader("State").ToString
                strSt &= ","
                strZip &= myReader("Zip").ToString
                strZip &= ","
                strPho &= myReader("Phone").ToString
                strPho &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Manager data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split manager info to arrays
        Mgr.Num = strNum.Split(CChar(","))
        ReDim Preserve Mgr.Num(Mgr.Num.Length - 2)
        Mgr.First = strFirst.Split(CChar(","))
        ReDim Preserve Mgr.First(Mgr.First.Length - 2)
        Mgr.Last = strLast.Split(CChar(","))
        ReDim Preserve Mgr.Last(Mgr.Last.Length - 2)
        Mgr.Add = strAdd.Split(CChar(","))
        ReDim Preserve Mgr.Add(Mgr.Add.Length - 2)
        Mgr.City = strCity.Split(CChar(","))
        ReDim Preserve Mgr.City(Mgr.City.Length - 2)
        Mgr.St = strSt.Split(CChar(","))
        ReDim Preserve Mgr.St(Mgr.St.Length - 2)
        Mgr.Zip = strZip.Split(CChar(","))
        ReDim Preserve Mgr.Zip(Mgr.Zip.Length - 2)
        Mgr.Pho = strPho.Split(CChar(","))
        ReDim Preserve Mgr.Pho(Mgr.Pho.Length - 2)

        ''get employee info
        selectStatement = "SELECT * FROM Employees WHERE EmpNumber LIKE 'EMP%%%%%%%'"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)
        strNum = ""
        strFirst = ""
        strLast = ""
        strAdd = ""
        strCity = ""
        strSt = ""
        strZip = ""
        strPho = ""
        strBon = ""
        strMBon = ""
        strGoal = ""
        strMgr = ""
        strStore = ""
        Try
            'Open connection and load data into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("EmpNumber").ToString
                strNum &= ","
                strFirst &= myReader("FirstName").ToString
                strFirst &= ","
                strLast &= myReader("LastName").ToString
                strLast &= ","
                strAdd &= myReader("Address").ToString
                strAdd &= ","
                strCity &= myReader("City").ToString
                strCity &= ","
                strSt &= myReader("State").ToString
                strSt &= ","
                strZip &= myReader("Zip").ToString
                strZip &= ","
                strPho &= myReader("Phone").ToString
                strPho &= ","
                strStore &= myReader("StoreNumber").ToString
                strStore &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Manager data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split manager info to arrays
        Emp.Num = strNum.Split(CChar(","))
        ReDim Preserve Emp.Num(Emp.Num.Length - 2)
        Emp.First = strFirst.Split(CChar(","))
        ReDim Preserve Emp.First(Emp.First.Length - 2)
        Emp.Last = strLast.Split(CChar(","))
        ReDim Preserve Emp.Last(Emp.Last.Length - 2)
        Emp.Add = strAdd.Split(CChar(","))
        ReDim Preserve Emp.Add(Emp.Add.Length - 2)
        Emp.City = strCity.Split(CChar(","))
        ReDim Preserve Emp.City(Emp.City.Length - 2)
        Emp.St = strSt.Split(CChar(","))
        ReDim Preserve Emp.St(Emp.St.Length - 2)
        Emp.Zip = strZip.Split(CChar(","))
        ReDim Preserve Emp.Zip(Emp.Zip.Length - 2)
        Emp.Pho = strPho.Split(CChar(","))
        ReDim Preserve Emp.Pho(Emp.Pho.Length - 2)
        Emp.Store = strStore.Split(CChar(","))
        ReDim Preserve Emp.Store(Emp.Store.Length - 2)


        intStart = 0
        printStoresClick()

    End Sub

    Private Sub printStoresClick()

        PrintPreviewDialog1.Document = StoresPrintDocument
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub printInvoicesClick()

        PrintPreviewDialog1.Document = InvoicesPrintDocument
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub printPaychecksClick()

        PrintPreviewDialog1.Document = PaychecksPrintDocument
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub StoresPrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles StoresPrintDocument.PrintPage

        'Setup printing variables
        Dim leftPos As Single = e.MarginBounds.Left
        Dim vertPos As Single = e.MarginBounds.Top
        Dim startPos, startPos2, topPos, botPos, centerPos As Single
        Dim strLine, strDateTime As String


        'setup Positions
        centerPos = (leftPos + e.MarginBounds.Right) / 2.0F
        botPos = e.MarginBounds.Bottom - (printFont.GetHeight * 2.0F)


        'print page header
        startPos = e.Graphics.MeasureString("Stores", New Font("Arial", 18)).Width / 2.0F
        e.Graphics.DrawString("Stores", New Font("Arial", 18), New SolidBrush(fontColor), (centerPos - startPos), vertPos)


        'print footer
        strDateTime = DateAndTime.Now.ToString
        startPos2 = e.Graphics.MeasureString(strDateTime, New Font("Arial", 10)).Width / 2.0F
        e.Graphics.DrawString(strDateTime, New Font("Arial", 10), New SolidBrush(fontColor), (centerPos - startPos2), e.MarginBounds.Bottom)


        'line space
        vertPos += New Font("Arial", 18).GetHeight
        vertPos += printFont.GetHeight * 2.0F
        topPos = vertPos


        'layout store block
        For i = intStart To Store.Num.Length - 1

            ''format phone
            Dim strPhone As String = Store.Pho(i)
            strPhone = strPhone.Insert(6, "-")
            strPhone = strPhone.Insert(3, "-")

            ''draw first line word
            strLine = "Store: " & Store.Num(i)
            e.Graphics.DrawString(strLine, headFont, New SolidBrush(fontColor), leftPos, vertPos)
            vertPos += boldFont.GetHeight

            ''draw store info
            strLine = Store.Add(i) & ", " & Store.City(i) & ", " & Store.St(i) & " " & Store.Zip(i) & ", "
            strLine &= strPhone & ", Goal: " & Store.Goal(i) & ", Bonus: " & Store.Bon(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos + 25.0F, vertPos)
            vertPos += printFont.GetHeight

            ''draw third line word
            strLine = "Manager:"
            e.Graphics.DrawString(strLine, boldFont, New SolidBrush(fontColor), leftPos, vertPos)
            vertPos += boldFont.GetHeight

            ''find manager
            Dim m As Integer = -1
            For j = 0 To Mgr.Num.Length - 1
                If Mgr.Num(j) = Store.Mgr(i) Then
                    m = j
                    Exit For
                End If
            Next j
            If m = -1 Then
                MessageBox.Show("Manager not found for Store " & Store.Num(i))
                Exit Sub
            End If

            ''format phone
            strPhone = Mgr.Pho(m)
            strPhone = strPhone.Insert(6, "-")
            strPhone = strPhone.Insert(3, "-")

            ''draw manager info
            strLine = Mgr.First(m) & " " & Mgr.Last(m) & ", " & Mgr.Add(m) & ", " & Mgr.City(m) & ", " & Mgr.St(m) & " " & Mgr.Zip(m) & ", "
            strLine &= strPhone & ", Bonus: " & Store.MBon(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos + 25.0F, vertPos)
            vertPos += printFont.GetHeight

            ''draw fourth line word
            strLine = "Employees:"
            e.Graphics.DrawString(strLine, boldFont, New SolidBrush(fontColor), leftPos, vertPos)
            vertPos += boldFont.GetHeight


            ''layout employee block within store block
            For j = 0 To Emp.Num.Length - 1
                ''if employee store doesn't match current store, skip
                If Emp.Store(j) <> Store.Num(i) Then
                    Continue For
                Else
                    ''else, draw employee info
                    'format phone
                    strPhone = Emp.Pho(j)
                    strPhone = strPhone.Insert(6, "-")
                    strPhone = strPhone.Insert(3, "-")
                    'draw employee line
                    strLine = Emp.First(j) & " " & Emp.Last(j) & ", " & Emp.Add(j) & ", " & Emp.City(j) & ", " & Emp.St(j)
                    strLine &= " " & Emp.Zip(j) & ", " & strPhone
                    e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos + 25.0F, vertPos)
                    vertPos += printFont.GetHeight
                End If
            Next j


            ''add extra vertical spaces
            vertPos += printFont.GetHeight * 2.0F


            ''if at bottom of page, go to next page
            If vertPos > botPos Then
                e.HasMorePages = True
                intStart = i + 1
                Exit Sub
            End If


        Next i

    End Sub

    Private Sub btnInvoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoices.Click


        Dim strNum, strCust, strFirst, strLast, strAdd, strCity, strSt, strZip, strPho, strBuy, strDateOnly, strDat, strSDate, strSDateOnly, strIns, strTotal As String
        Dim decTotal As Decimal


        'fill all structures with info

        ''get invoice info
        Dim selectStatement As String = "SELECT	A1.InvoiceNumber, A1.InvoiceDate, A1.Total, A3.FirstName, A3.LastName, "
        selectStatement &= "A2.PhoneName, A1.Insurance, A4.CustName, A4.CustAddress, A4.CustCity, A4.CustState, A4.CustZip, A4.CustPhone, "
        selectStatement &= "A4.CustCreateDate FROM Invoices A1, Phones A2, Employees A3, Customers A4 WHERE A1.PhoneSN = A2.PhoneSN AND "
        selectStatement &= "A1.EmpNumber = A3.EmpNumber AND A1.CustID = A4.CustID UNION SELECT A1.InvoiceNumber, A1.InvoiceDate, A1.Total, "
        selectStatement &= "A3.FirstName, A3.LastName, A2.AccessoryName, A1.Insurance, A4.CustName, A4.CustAddress, A4.CustCity, A4.CustState, "
        selectStatement &= "A4.CustZip, A4.CustPhone, A4.CustCreateDate FROM Invoices A1, Accessories A2, Employees A3, Customers A4 WHERE "
        selectStatement &= "A1.AccessorySKU = A2.AccessorySKU AND A1.EmpNumber = A3.EmpNumber AND A1.CustID = A4.CustID ORDER BY A1.InvoiceNumber DESC"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        Try
            'Open connection and load stores into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("InvoiceNumber").ToString
                strNum &= ","
                strDateOnly = myReader("InvoiceDate").ToString
                strDat &= strDateOnly.Replace(" 12:00:00 AM", "")
                strDat &= ","
                decTotal = Decimal.Parse(myReader("Total").ToString)
                strTotal &= decTotal.ToString("F2")
                strTotal &= ","
                strFirst &= myReader("FirstName").ToString
                strFirst &= ","
                strLast &= myReader("LastName").ToString
                strLast &= ","
                strBuy &= myReader("PhoneName").ToString
                strBuy &= ","
                strIns &= myReader("Insurance").ToString
                strIns &= ","
                strCust &= myReader("CustName").ToString
                strCust &= ","
                strAdd &= myReader("CustAddress").ToString
                strAdd &= ","
                strCity &= myReader("CustCity").ToString
                strCity &= ","
                strSt &= myReader("CustState").ToString
                strSt &= ","
                strZip &= myReader("CustZip").ToString
                strZip &= ","
                strPho &= myReader("CustPhone").ToString
                strPho &= ","
                strSDateOnly = myReader("CustCreateDate").ToString
                strSDate &= strSDateOnly.Replace(" 12:00:00 AM", "")
                strSDate &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Invoice data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split store info to arrays
        Inv.Num = strNum.Split(CChar(","))
        ReDim Preserve Inv.Num(Inv.Num.Length - 2)
        Inv.Dat = strDat.Split(CChar(","))
        ReDim Preserve Inv.Dat(Inv.Dat.Length - 2)
        Inv.Total = strTotal.Split(CChar(","))
        ReDim Preserve Inv.Total(Inv.Total.Length - 2)
        Inv.First = strFirst.Split(CChar(","))
        ReDim Preserve Inv.First(Inv.First.Length - 2)
        Inv.Last = strLast.Split(CChar(","))
        ReDim Preserve Inv.Last(Inv.Last.Length - 2)
        Inv.Buy = strBuy.Split(CChar(","))
        ReDim Preserve Inv.Buy(Inv.Buy.Length - 2)
        Inv.Ins = strIns.Split(CChar(","))
        ReDim Preserve Inv.Ins(Inv.Ins.Length - 2)
        Inv.Cust = strCust.Split(CChar(","))
        ReDim Preserve Inv.Cust(Inv.Cust.Length - 2)
        Inv.Add = strAdd.Split(CChar(","))
        ReDim Preserve Inv.Add(Inv.Add.Length - 2)
        Inv.City = strCity.Split(CChar(","))
        ReDim Preserve Inv.City(Inv.City.Length - 2)
        Inv.St = strSt.Split(CChar(","))
        ReDim Preserve Inv.St(Inv.St.Length - 2)
        Inv.Zip = strZip.Split(CChar(","))
        ReDim Preserve Inv.Zip(Inv.Zip.Length - 2)
        Inv.Pho = strPho.Split(CChar(","))
        ReDim Preserve Inv.Pho(Inv.Pho.Length - 2)
        Inv.SDate = strSDate.Split(CChar(","))
        ReDim Preserve Inv.SDate(Inv.SDate.Length - 2)


        intStart = 0
        printInvoicesClick()


    End Sub

    Private Sub InvoicesPrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles InvoicesPrintDocument.PrintPage


        'Setup printing variables
        Dim leftPos As Single = e.MarginBounds.Left
        Dim vertPos As Single = e.MarginBounds.Top
        Dim startPos, startPos2, topPos, botPos, centerPos As Single
        Dim strLine, strDateTime As String


        'setup Positions
        centerPos = (leftPos + e.MarginBounds.Right) / 2.0F
        botPos = e.MarginBounds.Bottom - (printFont.GetHeight * 2.0F)


        'print page header
        startPos = e.Graphics.MeasureString("Invoices", New Font("Arial", 18)).Width / 2.0F
        e.Graphics.DrawString("Invoices", New Font("Arial", 18), New SolidBrush(fontColor), (centerPos - startPos), vertPos)


        'print footer
        strDateTime = DateAndTime.Now.ToString
        startPos2 = e.Graphics.MeasureString(strDateTime, New Font("Arial", 10)).Width / 2.0F
        e.Graphics.DrawString(strDateTime, New Font("Arial", 10), New SolidBrush(fontColor), (centerPos - startPos2), e.MarginBounds.Bottom)


        'line space
        vertPos += New Font("Arial", 18).GetHeight
        vertPos += printFont.GetHeight * 2.0F
        topPos = vertPos


        'begin invoice block
        For i = intStart To Inv.Num.Length - 1

            ''draw first line first column
            strLine = "Invoice: " & Inv.Num(i) & " (" & Inv.Dat(i) & ")"
            e.Graphics.DrawString(strLine, headFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''draw first line second column
            strLine = "Invoice Total: $" & Inv.Total(i)
            e.Graphics.DrawString(strLine, printFont2, New SolidBrush(fontColor), centerPos, vertPos)
            vertPos += boldFont.GetHeight

            ''draw second line first column
            strLine = "Sold by: " & Inv.First(i) & " " & Inv.Last(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''draw second line second column
            strLine = "Product Purchased: " & Inv.Buy(i)
            If Inv.Ins(i) = "True" Then strLine &= " & Insurance"
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), centerPos, vertPos)
            vertPos += printFont.GetHeight

            ''format phone
            Dim strPhone As String = Inv.Pho(i)
            strPhone = strPhone.Insert(6, "-")
            strPhone = strPhone.Insert(3, "-")

            ''draw third line
            strLine = "Sold to: " & Inv.Cust(i) & ", " & Inv.Add(i) & ", " & Inv.City(i) & ", " & Inv.St(i) & " "
            strLine &= Inv.Zip(i) & ", " & strPhone
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)
            vertPos += printFont.GetHeight

            ''draw fourth line
            strLine = "Customer Since: " & Inv.SDate(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''add extra vertical spaces
            vertPos += printFont.GetHeight * 2.0F


            ''if at bottom of page, go to next page
            If vertPos > botPos Then
                e.HasMorePages = True
                intStart = i + 1
                Exit Sub
            End If

        Next i


    End Sub

    Private Sub btnPaychecks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaychecks.Click

        Dim strNum, strMgr, strGoal, strMBon, strBon, strFirst, strLast, strAdd, strCity, strSt, strZip, strPho, strSold, strComm, strNum2, strAcc As String
        Dim decComm As Decimal

        'fill all structures with info

        ''get paycheck phone info
        ''RETURNS: EmpNumber First Last Add City State Zip Phone PhonesSold TotalPhoneCommission
        Dim selectStatement As String = "SELECT A1.EmpNumber, A2.FirstName, A2.LastName, A2.Address, A2.City, A2.State, A2.Zip, A2.Phone, COUNT(A1.PhoneSN) AS Phones, SUM(A3.Commission) As Commission "
        selectStatement &= "FROM Invoices A1, Employees A2, Phones A3 "
        selectStatement &= "WHERE A1.EmpNumber = A2.EmpNumber AND A1.PhoneSN = A3.PhoneSN "
        selectStatement &= "GROUP BY A1.EmpNumber, A2.FirstName, A2.LastName, A2.Address, A2.City, A2.State, A2.Zip, A2.Phone "
        selectStatement &= "ORDER BY A2.LastName"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)

        Try
            'Open connection and load paychecks into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("EmpNumber").ToString
                strNum &= ","
                strFirst &= myReader("FirstName").ToString
                strFirst &= ","
                strLast &= myReader("LastName").ToString
                strLast &= ","
                strAdd &= myReader("Address").ToString
                strAdd &= ","
                strCity &= myReader("City").ToString
                strCity &= ","
                strSt &= myReader("State").ToString
                strSt &= ","
                strZip &= myReader("Zip").ToString
                strZip &= ","
                strPho &= myReader("Phone").ToString
                strPho &= ","
                strSold &= myReader("Phones").ToString
                strSold &= ","
                decComm = Decimal.Parse(myReader("Commission").ToString)
                strComm &= decComm.ToString("F2")
                strComm &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Paycheck data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split paycheck info to arrays
        Pho.Num = strNum.Split(CChar(","))
        ReDim Preserve Pho.Num(Pho.Num.Length - 2)
        Pho.First = strFirst.Split(CChar(","))
        ReDim Preserve Pho.First(Pho.First.Length - 2)
        Pho.Last = strLast.Split(CChar(","))
        ReDim Preserve Pho.Last(Pho.Last.Length - 2)
        Pho.Add = strAdd.Split(CChar(","))
        ReDim Preserve Pho.Add(Pho.Add.Length - 2)
        Pho.City = strCity.Split(CChar(","))
        ReDim Preserve Pho.City(Pho.City.Length - 2)
        Pho.St = strSt.Split(CChar(","))
        ReDim Preserve Pho.St(Pho.St.Length - 2)
        Pho.Zip = strZip.Split(CChar(","))
        ReDim Preserve Pho.Zip(Pho.Zip.Length - 2)
        Pho.Pho = strPho.Split(CChar(","))
        ReDim Preserve Pho.Pho(Pho.Pho.Length - 2)
        Pho.Sold = strSold.Split(CChar(","))
        ReDim Preserve Pho.Sold(Pho.Sold.Length - 2)
        Pho.Comm = strComm.Split(CChar(","))
        ReDim Preserve Pho.Comm(Pho.Comm.Length - 2)
        ReDim Pho.Acc(Pho.Num.Length - 1)


        ''get paycheck accessory info
        selectStatement = "SELECT A1.EmpNumber, COUNT(A1.AccessorySKU) AS Accessories "
        selectStatement &= "FROM Invoices A1, Employees A2 "
        selectStatement &= "WHERE A1.EmpNumber = A2.EmpNumber "
        selectStatement &= "GROUP BY A1.EmpNumber"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)


        strNum = ""
        strAcc = ""
        Try
            'Open connection and load paychecks into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("EmpNumber").ToString
                strNum &= ","
                strAcc &= myReader("Accessories").ToString
                strAcc &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Paycheck accessory data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split paycheck info to arrays
        Access.Num = strNum.Split(CChar(","))
        ReDim Preserve Access.Num(Access.Num.Length - 2)
        Access.Acc = strAcc.Split(CChar(","))
        ReDim Preserve Access.Acc(Access.Acc.Length - 2)


        ''get store bonus info
        selectStatement = "SELECT StoreNumber, Manager, StoreBonus, ManagerBonus, PhoneGoal FROM Stores"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)
        strNum = ""
        strMgr = ""
        strBon = ""
        strMBon = ""
        strGoal = ""
        Try
            'Open connection and load paychecks into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("StoreNumber").ToString
                strNum &= ","
                strMgr &= myReader("Manager").ToString
                strMgr &= ","
                strBon &= myReader("StoreBonus").ToString
                strBon &= ","
                strMBon &= myReader("ManagerBonus").ToString
                strMBon &= ","
                strGoal &= myReader("PhoneGoal").ToString
                strGoal &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Store bonus data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split paycheck info to arrays
        payStore.Num = strNum.Split(CChar(","))
        ReDim Preserve payStore.Num(payStore.Num.Length - 2)
        payStore.Mgr = strMgr.Split(CChar(","))
        ReDim Preserve payStore.Mgr(payStore.Mgr.Length - 2)
        payStore.Bon = strBon.Split(CChar(","))
        ReDim Preserve payStore.Bon(payStore.Bon.Length - 2)
        payStore.MBon = strMBon.Split(CChar(","))
        ReDim Preserve payStore.MBon(payStore.MBon.Length - 2)
        payStore.Goal = strGoal.Split(CChar(","))
        ReDim Preserve payStore.Goal(payStore.Goal.Length - 2)
        ReDim payStore.SoldEnough(payStore.Num.Length - 1)
        For i = 0 To payStore.SoldEnough.Length - 1
            payStore.SoldEnough(i) = "False"
        Next i

        ''get store employee info
        selectStatement = "SELECT A1.StoreNumber, A2.EmpNumber, COUNT(A3.PhoneSN) As Phones "
        selectStatement &= "FROM Stores A1, Employees A2, Invoices A3 "
        selectStatement &= "WHERE A1.StoreNumber = A2.StoreNumber AND A2.EmpNumber = A3.EmpNumber "
        selectStatement &= "GROUP BY A1.StoreNumber, A2.EmpNumber, A2.FirstName, A2.LastName"
        selectCommand = New SqlCommand(selectStatement, LoginForm.MyConnection)
        strNum = ""
        strAcc = ""
        strSold = ""
        Try
            'Open connection and load store employee data into variables
            If LoginForm.MyConnection.State = ConnectionState.Closed Then LoginForm.MyConnection.Open()
            Dim myReader As SqlDataReader = selectCommand.ExecuteReader
            Do While myReader.Read
                strNum &= myReader("StoreNumber").ToString
                strNum &= ","
                strAcc &= myReader("EmpNumber").ToString
                strAcc &= ","
                strSold &= myReader("Phones").ToString
                strSold &= ","
            Loop

        Catch ex As SqlException
            MessageBox.Show("Store employee data could not be read", "Database Error")
            If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()
            Exit Sub
        End Try

        If LoginForm.MyConnection.State = ConnectionState.Open Then LoginForm.MyConnection.Close()

        'split store employee info to arrays
        empStore.Num = strNum.Split(CChar(","))
        ReDim Preserve empStore.Num(empStore.Num.Length - 2)
        empStore.Emp = strAcc.Split(CChar(","))
        ReDim Preserve empStore.Emp(empStore.Emp.Length - 2)
        empStore.Sold = strSold.Split(CChar(","))
        ReDim Preserve empStore.Sold(empStore.Sold.Length - 2)


        Dim intCnt, intPos As Integer
        ReDim storeCount(empStore.Num.Length - 1)
        'Compare and count all words except last two.
        For i = 0 To empStore.Num.Length - 2
            intCnt += Integer.Parse(empStore.Sold(i))
            If empStore.Num(i) <> empStore.Num(i + 1) Then
                storeCount(intPos).Num = empStore.Num(i)
                storeCount(intPos).Sold = intCnt
                intPos += 1
                intCnt = 0
            End If
        Next i

        'Resize structure
        ReDim Preserve storeCount(intPos - 1)

        'Compare last two words
        If empStore.Num(empStore.Num.Length - 2) <> empStore.Num(empStore.Num.Length - 1) Then
            ReDim Preserve storeCount(intPos)
            storeCount(storeCount.Length - 1).Num = empStore.Num(empStore.Num.Length - 1)
            storeCount(storeCount.Length - 1).Sold = CInt(empStore.Sold(empStore.Num.Length - 1))
        Else
            storeCount(storeCount.Length - 1).Num = empStore.Num(empStore.Num.Length - 1)
            storeCount(storeCount.Length - 1).Sold += CInt(empStore.Sold(empStore.Num.Length - 1))
        End If

        'set bonus variable
        For i = 0 To storeCount.Length - 1
            For j = 0 To payStore.Num.Length - 1
                If storeCount(i).Num = payStore.Num(j) Then
                    If storeCount(i).Sold >= CInt(payStore.Goal(j)) Then
                        payStore.SoldEnough(j) = "True"
                        Exit For
                    Else
                        payStore.SoldEnough(j) = "False"
                        Exit For
                    End If
                End If
            Next j
        Next i


        'assign accessory count
        For i = 0 To Access.Num.Length - 1
            For j = 0 To Pho.Num.Length - 1
                If Access.Num(i) = Pho.Num(j) Then
                    Pho.Acc(j) = Access.Acc(i)
                    Exit For
                End If

            Next j
        Next i


        intStart = 0
        printPaychecksClick()

    End Sub

    Private Sub PaychecksPrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PaychecksPrintDocument.PrintPage

        'Setup printing variables
        Dim leftPos As Single = e.MarginBounds.Left
        Dim vertPos As Single = e.MarginBounds.Top
        Dim startPos, startPos2, topPos, botPos, centerPos, rightPos As Single
        Dim strLine, strDateTime As String


        'setup Positions
        centerPos = (leftPos + e.MarginBounds.Right) / 2.0F
        botPos = e.MarginBounds.Bottom - (printFont.GetHeight * 2.0F)


        'print page header
        startPos = e.Graphics.MeasureString("Paychecks", New Font("Arial", 18)).Width / 2.0F
        e.Graphics.DrawString("Paychecks", New Font("Arial", 18), New SolidBrush(fontColor), (centerPos - startPos), vertPos)


        'print footer
        strDateTime = DateAndTime.Now.ToString
        startPos2 = e.Graphics.MeasureString(strDateTime, New Font("Arial", 10)).Width / 2.0F
        e.Graphics.DrawString(strDateTime, New Font("Arial", 10), New SolidBrush(fontColor), (centerPos - startPos2), e.MarginBounds.Bottom)


        'line space
        vertPos += New Font("Arial", 18).GetHeight
        vertPos += printFont.GetHeight * 2.0F
        topPos = vertPos


        'begin paychecks block
        For i = intStart To Pho.Num.Length - 1

            ''draw first line first column
            strLine = Pho.Last(i) & ", " & Pho.First(i) & " (" & Pho.Num(i) & ")"
            e.Graphics.DrawString(strLine, headFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''draw first line second column
            strLine = "Phones: " & Pho.Sold(i) & " Accessories: " & Pho.Acc(i)
            rightPos = e.MarginBounds.Right - e.Graphics.MeasureString(strLine, printFont2).Width
            e.Graphics.DrawString(strLine, printFont2, New SolidBrush(fontColor), rightPos, vertPos)
            vertPos += boldFont.GetHeight

            ''draw second line first column
            strLine = Pho.Add(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''calc commission
            Dim decComm As Decimal
            decComm = Decimal.Parse(Pho.Acc(i))
            decComm *= 5D
            decComm += Decimal.Parse(Pho.Comm(i))

            ''draw second line second column
            strLine = "Commission: $" & decComm.ToString("F2")
            rightPos = e.MarginBounds.Right - e.Graphics.MeasureString(strLine, printFont).Width
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), rightPos, vertPos)
            vertPos += printFont.GetHeight

            ''draw third line first column
            strLine = Pho.City(i) & ", " & Pho.St(i) & " " & Pho.Zip(i)
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''calc bonus
            Dim decBonus, decSold, decGoal As Decimal

            If Pho.Num(i).StartsWith("MGR") Then
                'manager bonus calc
                For j = 0 To payStore.Mgr.Length - 1
                    If payStore.Mgr(j) = Pho.Num(i) And payStore.SoldEnough(j) = "True" Then
                        decBonus = Decimal.Parse(payStore.MBon(j))
                        Exit For
                    Else
                        payStore.SoldEnough(j) = "False"
                    End If
                Next j
            Else
                'employee bonus calc
                For j = 0 To empStore.Num.Length - 1
                    If empStore.Emp(j) = Pho.Num(i) Then
                        For k = 0 To payStore.Num.Length - 1
                            If empStore.Num(j) = payStore.Num(k) And payStore.SoldEnough(k) = "True" Then
                                decBonus = Decimal.Parse(payStore.Bon(k))
                                decSold = Decimal.Parse(Pho.Sold(i))
                                decGoal = Decimal.Parse(payStore.Goal(k))
                                decBonus *= (decSold / decGoal)
                                Exit For
                            Else
                                payStore.SoldEnough(k) = "False"
                            End If
                        Next k
                    End If
                Next j
            End If


            ''draw third line second column
            strLine = "Bonus: $" & decBonus.ToString("F2")
            rightPos = e.MarginBounds.Right - e.Graphics.MeasureString(strLine, printFont).Width
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), rightPos, vertPos)
            vertPos += printFont.GetHeight

            ''format phone
            Dim strPhone As String = Pho.Pho(i)
            strPhone = strPhone.Insert(6, "-")
            strPhone = strPhone.Insert(3, "-")


            ''draw fourth line first column
            strLine = strPhone
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), leftPos, vertPos)

            ''calc total
            Dim decTotalpay As Decimal
            decTotalpay = decBonus + decComm

            ''draw fourth line second column
            strLine = "Total: $" & decTotalpay.ToString("F2")
            rightPos = e.MarginBounds.Right - e.Graphics.MeasureString(strLine, printFont).Width
            e.Graphics.DrawString(strLine, printFont, New SolidBrush(fontColor), rightPos, vertPos)
            vertPos += printFont.GetHeight


            ''add extra vertical spaces
            vertPos += printFont.GetHeight * 2.0F


            ''if at bottom of page, go to next page
            If vertPos > botPos Then
                e.HasMorePages = True
                intStart = i + 1
                Exit Sub
            End If


        Next i


    End Sub
End Class