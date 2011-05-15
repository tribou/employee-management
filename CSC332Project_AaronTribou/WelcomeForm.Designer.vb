<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WelcomeForm
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
        Me.btnEmp = New System.Windows.Forms.Button()
        Me.btnStore = New System.Windows.Forms.Button()
        Me.btnPos = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnEmp
        '
        Me.btnEmp.Location = New System.Drawing.Point(212, 318)
        Me.btnEmp.Name = "btnEmp"
        Me.btnEmp.Size = New System.Drawing.Size(163, 24)
        Me.btnEmp.TabIndex = 3
        Me.btnEmp.Text = "&Employee Management"
        Me.btnEmp.UseVisualStyleBackColor = True
        '
        'btnStore
        '
        Me.btnStore.Location = New System.Drawing.Point(60, 318)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(134, 24)
        Me.btnStore.TabIndex = 2
        Me.btnStore.Text = "&Store Management"
        Me.btnStore.UseVisualStyleBackColor = True
        '
        'btnPos
        '
        Me.btnPos.Location = New System.Drawing.Point(136, 127)
        Me.btnPos.Name = "btnPos"
        Me.btnPos.Size = New System.Drawing.Size(163, 36)
        Me.btnPos.TabIndex = 0
        Me.btnPos.Text = "&Point-of-Sale System"
        Me.btnPos.UseVisualStyleBackColor = True
        '
        'btnReports
        '
        Me.btnReports.Location = New System.Drawing.Point(136, 187)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(163, 36)
        Me.btnReports.TabIndex = 1
        Me.btnReports.Text = "&Reports"
        Me.btnReports.UseVisualStyleBackColor = True
        '
        'btnLogout
        '
        Me.btnLogout.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnLogout.Location = New System.Drawing.Point(12, 12)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(71, 24)
        Me.btnLogout.TabIndex = 4
        Me.btnLogout.TabStop = False
        Me.btnLogout.Text = "&Logout"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(98, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(238, 29)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Retail Management"
        '
        'WelcomeForm
        '
        Me.AcceptButton = Me.btnPos
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnLogout
        Me.ClientSize = New System.Drawing.Size(435, 387)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.btnReports)
        Me.Controls.Add(Me.btnPos)
        Me.Controls.Add(Me.btnStore)
        Me.Controls.Add(Me.btnEmp)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WelcomeForm"
        Me.Text = "CSC332Project_AaronTribou"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEmp As System.Windows.Forms.Button
    Friend WithEvents btnStore As System.Windows.Forms.Button
    Friend WithEvents btnPos As System.Windows.Forms.Button
    Friend WithEvents btnReports As System.Windows.Forms.Button
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
