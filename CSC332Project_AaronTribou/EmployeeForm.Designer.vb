<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmployeeForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtFirst = New System.Windows.Forms.TextBox()
        Me.txtLast = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.cboState = New System.Windows.Forms.ComboBox()
        Me.StatesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CSC332ProjectDataSet = New CSC332Project_AaronTribou.CSC332ProjectDataSet()
        Me.StoresBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StoresTableAdapter = New CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.StoresTableAdapter()
        Me.StatesTableAdapter = New CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.StatesTableAdapter()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboID = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.FKEmployeesStatesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeesTableAdapter = New CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.EmployeesTableAdapter()
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.cboStore = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        CType(Me.StatesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CSC332ProjectDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StoresBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FKEmployeesStatesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBack.Location = New System.Drawing.Point(80, 381)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(73, 28)
        Me.btnBack.TabIndex = 21
        Me.btnBack.TabStop = False
        Me.btnBack.Text = "Go &Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"Select a Type...", "Employee", "Manager"})
        Me.cboType.Location = New System.Drawing.Point(230, 92)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(165, 24)
        Me.cboType.TabIndex = 17
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(43, 332)
        Me.txtPhone.MaxLength = 10
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(117, 22)
        Me.txtPhone.TabIndex = 15
        '
        'txtFirst
        '
        Me.txtFirst.Location = New System.Drawing.Point(43, 153)
        Me.txtFirst.MaxLength = 50
        Me.txtFirst.Name = "txtFirst"
        Me.txtFirst.Size = New System.Drawing.Size(166, 22)
        Me.txtFirst.TabIndex = 3
        '
        'txtLast
        '
        Me.txtLast.Location = New System.Drawing.Point(230, 153)
        Me.txtLast.MaxLength = 50
        Me.txtLast.Name = "txtLast"
        Me.txtLast.Size = New System.Drawing.Size(166, 22)
        Me.txtLast.TabIndex = 5
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(43, 211)
        Me.txtAddress.MaxLength = 50
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(352, 22)
        Me.txtAddress.TabIndex = 7
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(43, 270)
        Me.txtCity.MaxLength = 30
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(166, 22)
        Me.txtCity.TabIndex = 9
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(307, 270)
        Me.txtZip.MaxLength = 5
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(89, 22)
        Me.txtZip.TabIndex = 13
        '
        'cboState
        '
        Me.cboState.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.StatesBindingSource, "StateCode", True))
        Me.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboState.FormattingEnabled = True
        Me.cboState.Location = New System.Drawing.Point(230, 268)
        Me.cboState.Name = "cboState"
        Me.cboState.Size = New System.Drawing.Size(59, 24)
        Me.cboState.TabIndex = 11
        '
        'StatesBindingSource
        '
        Me.StatesBindingSource.DataMember = "States"
        Me.StatesBindingSource.DataSource = Me.CSC332ProjectDataSet
        '
        'CSC332ProjectDataSet
        '
        Me.CSC332ProjectDataSet.DataSetName = "CSC332ProjectDataSet"
        Me.CSC332ProjectDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StoresBindingSource
        '
        Me.StoresBindingSource.DataMember = "Stores"
        Me.StoresBindingSource.DataSource = Me.CSC332ProjectDataSet
        '
        'StoresTableAdapter
        '
        Me.StoresTableAdapter.ClearBeforeFill = True
        '
        'StatesTableAdapter
        '
        Me.StatesTableAdapter.ClearBeforeFill = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(230, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Employee &Type:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&First Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(230, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "&Last Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(46, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "A&ddress:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 249)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "&City:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(230, 249)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 16)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "&State:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(307, 251)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "&Zip Code:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(46, 313)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "&Phone:"
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(186, 381)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(73, 28)
        Me.btnReset.TabIndex = 20
        Me.btnReset.Text = "&Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(101, 28)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(243, 29)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Manage Employees"
        '
        'cboID
        '
        Me.cboID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboID.FormattingEnabled = True
        Me.cboID.Items.AddRange(New Object() {"Select Employee...", "Add New Employee"})
        Me.cboID.Location = New System.Drawing.Point(43, 92)
        Me.cboID.Name = "cboID"
        Me.cboID.Size = New System.Drawing.Size(166, 24)
        Me.cboID.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(46, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(114, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Select &Employee:"
        Me.Label10.Visible = False
        '
        'FKEmployeesStatesBindingSource
        '
        Me.FKEmployeesStatesBindingSource.DataMember = "FK_Employees_States"
        Me.FKEmployeesStatesBindingSource.DataSource = Me.StatesBindingSource
        '
        'EmployeesTableAdapter
        '
        Me.EmployeesTableAdapter.ClearBeforeFill = True
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(292, 381)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(73, 28)
        Me.btnAccept.TabIndex = 18
        Me.btnAccept.Text = "&Modify"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'cboStore
        '
        Me.cboStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStore.Enabled = False
        Me.cboStore.FormattingEnabled = True
        Me.cboStore.Location = New System.Drawing.Point(230, 330)
        Me.cboStore.Name = "cboStore"
        Me.cboStore.Size = New System.Drawing.Size(165, 24)
        Me.cboStore.TabIndex = 23
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(230, 311)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 16)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Stor&e:"
        '
        'EmployeeForm
        '
        Me.AcceptButton = Me.btnAccept
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnBack
        Me.ClientSize = New System.Drawing.Size(445, 439)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboStore)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboID)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboState)
        Me.Controls.Add(Me.txtZip)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtLast)
        Me.Controls.Add(Me.txtFirst)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.btnBack)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EmployeeForm"
        CType(Me.StatesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CSC332ProjectDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StoresBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FKEmployeesStatesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtFirst As System.Windows.Forms.TextBox
    Friend WithEvents txtLast As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents cboState As System.Windows.Forms.ComboBox
    Friend WithEvents CSC332ProjectDataSet As CSC332Project_AaronTribou.CSC332ProjectDataSet
    Friend WithEvents StoresBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StoresTableAdapter As CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.StoresTableAdapter
    Friend WithEvents StatesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StatesTableAdapter As CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.StatesTableAdapter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboID As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents FKEmployeesStatesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EmployeesTableAdapter As CSC332Project_AaronTribou.CSC332ProjectDataSetTableAdapters.EmployeesTableAdapter
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents cboStore As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
