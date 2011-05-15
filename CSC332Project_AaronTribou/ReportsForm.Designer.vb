<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsForm))
        Me.btnStores = New System.Windows.Forms.Button()
        Me.btnInvoices = New System.Windows.Forms.Button()
        Me.btnPaychecks = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StoresPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.InvoicesPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.PaychecksPrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.SuspendLayout()
        '
        'btnStores
        '
        Me.btnStores.Location = New System.Drawing.Point(51, 79)
        Me.btnStores.Name = "btnStores"
        Me.btnStores.Size = New System.Drawing.Size(176, 34)
        Me.btnStores.TabIndex = 0
        Me.btnStores.Text = "&Stores"
        Me.btnStores.UseVisualStyleBackColor = True
        '
        'btnInvoices
        '
        Me.btnInvoices.Location = New System.Drawing.Point(51, 141)
        Me.btnInvoices.Name = "btnInvoices"
        Me.btnInvoices.Size = New System.Drawing.Size(176, 34)
        Me.btnInvoices.TabIndex = 1
        Me.btnInvoices.Text = "&Invoices"
        Me.btnInvoices.UseVisualStyleBackColor = True
        '
        'btnPaychecks
        '
        Me.btnPaychecks.Location = New System.Drawing.Point(51, 203)
        Me.btnPaychecks.Name = "btnPaychecks"
        Me.btnPaychecks.Size = New System.Drawing.Size(176, 34)
        Me.btnPaychecks.TabIndex = 2
        Me.btnPaychecks.Text = "Current &Paychecks"
        Me.btnPaychecks.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(51, 265)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(176, 34)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "Go &Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(186, 31)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Print Reports"
        '
        'StoresPrintDocument
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'InvoicesPrintDocument
        '
        '
        'PaychecksPrintDocument
        '
        '
        'ReportsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 322)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnPaychecks)
        Me.Controls.Add(Me.btnInvoices)
        Me.Controls.Add(Me.btnStores)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ReportsForm"
        Me.Text = "ReportsForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnStores As System.Windows.Forms.Button
    Friend WithEvents btnInvoices As System.Windows.Forms.Button
    Friend WithEvents btnPaychecks As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StoresPrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents InvoicesPrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents PaychecksPrintDocument As System.Drawing.Printing.PrintDocument
End Class
