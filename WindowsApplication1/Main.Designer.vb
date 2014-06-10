<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideBtn = New System.Windows.Forms.Button()
        Me.UserProgName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SaveBtn = New System.Windows.Forms.Button()
        Me.TestBtn = New System.Windows.Forms.Button()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckFrequencySec = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.userIPrange0 = New System.Windows.Forms.TextBox()
        Me.userIPrange1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GetIpRng = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.InstallBtn = New System.Windows.Forms.Button()
        Me.GetASN = New System.Windows.Forms.Button()
        Me.WhiteOrBlackListBox = New System.Windows.Forms.ListBox()
        Me.listTypeLabel = New System.Windows.Forms.Label()
        Me.AddASN = New System.Windows.Forms.Button()
        Me.DeleteASN = New System.Windows.Forms.Button()
        Me.WhiteListRadio = New System.Windows.Forms.RadioButton()
        Me.BlackListRadio = New System.Windows.Forms.RadioButton()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.CheckFrequencySec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.SettingsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(172, 48)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
        Me.ToolStripMenuItem1.Text = "Close DMCA saver"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'HideBtn
        '
        Me.HideBtn.Location = New System.Drawing.Point(15, 302)
        Me.HideBtn.Name = "HideBtn"
        Me.HideBtn.Size = New System.Drawing.Size(75, 30)
        Me.HideBtn.TabIndex = 1
        Me.HideBtn.Text = "Hide"
        Me.HideBtn.UseVisualStyleBackColor = True
        '
        'UserProgName
        '
        Me.UserProgName.Location = New System.Drawing.Point(159, 22)
        Me.UserProgName.Name = "UserProgName"
        Me.UserProgName.Size = New System.Drawing.Size(100, 20)
        Me.UserProgName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Name of file sharing program"
        '
        'SaveBtn
        '
        Me.SaveBtn.Location = New System.Drawing.Point(15, 266)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(75, 30)
        Me.SaveBtn.TabIndex = 4
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'Test
        '
        Me.TestBtn.Location = New System.Drawing.Point(98, 266)
        Me.TestBtn.Name = "Test"
        Me.TestBtn.Size = New System.Drawing.Size(75, 30)
        Me.TestBtn.TabIndex = 5
        Me.TestBtn.Text = "Test"
        Me.TestBtn.UseVisualStyleBackColor = True
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(98, 302)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 30)
        Me.StartBtn.TabIndex = 6
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Location = New System.Drawing.Point(178, 311)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(96, 13)
        Me.StatusLabel.TabIndex = 7
        Me.StatusLabel.Text = "Status: not running"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 8
        '
        'CheckFrequencySec
        '
        Me.CheckFrequencySec.Increment = New Decimal(New Integer() {30, 0, 0, 0})
        Me.CheckFrequencySec.Location = New System.Drawing.Point(219, 48)
        Me.CheckFrequencySec.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.CheckFrequencySec.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.CheckFrequencySec.Name = "CheckFrequencySec"
        Me.CheckFrequencySec.Size = New System.Drawing.Size(40, 20)
        Me.CheckFrequencySec.TabIndex = 9
        Me.CheckFrequencySec.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(202, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Check ASN address frequency (seconds)"
        '
        'userIPrange0
        '
        Me.userIPrange0.Location = New System.Drawing.Point(478, 255)
        Me.userIPrange0.MaxLength = 3
        Me.userIPrange0.Name = "userIPrange0"
        Me.userIPrange0.Size = New System.Drawing.Size(30, 20)
        Me.userIPrange0.TabIndex = 11
        '
        'userIPrange1
        '
        Me.userIPrange1.Location = New System.Drawing.Point(477, 281)
        Me.userIPrange1.MaxLength = 3
        Me.userIPrange1.Name = "userIPrange1"
        Me.userIPrange1.Size = New System.Drawing.Size(31, 20)
        Me.userIPrange1.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(368, 273)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 20)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = ".        . * . *"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(344, 262)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Campus IP address range"
        '
        'GetIpRng
        '
        Me.GetIpRng.Location = New System.Drawing.Point(358, 281)
        Me.GetIpRng.Name = "GetIpRng"
        Me.GetIpRng.Size = New System.Drawing.Size(75, 23)
        Me.GetIpRng.TabIndex = 15
        Me.GetIpRng.Text = "Get IP range"
        Me.GetIpRng.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipText = "Campus "
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "DMCA SAVER"
        Me.NotifyIcon1.Visible = True
        '
        'Install
        '
        Me.InstallBtn.Location = New System.Drawing.Point(181, 266)
        Me.InstallBtn.Name = "Install"
        Me.InstallBtn.Size = New System.Drawing.Size(75, 30)
        Me.InstallBtn.TabIndex = 16
        Me.InstallBtn.Text = "Install"
        Me.InstallBtn.UseVisualStyleBackColor = True
        '
        'GetASN
        '
        Me.GetASN.Location = New System.Drawing.Point(98, 230)
        Me.GetASN.Name = "GetASN"
        Me.GetASN.Size = New System.Drawing.Size(75, 30)
        Me.GetASN.TabIndex = 17
        Me.GetASN.Text = "Get ASN(s)"
        Me.GetASN.UseVisualStyleBackColor = True
        '
        'WhiteOrBlackListBox
        '
        Me.WhiteOrBlackListBox.FormattingEnabled = True
        Me.WhiteOrBlackListBox.Location = New System.Drawing.Point(15, 90)
        Me.WhiteOrBlackListBox.Name = "WhiteOrBlackListBox"
        Me.WhiteOrBlackListBox.Size = New System.Drawing.Size(241, 134)
        Me.WhiteOrBlackListBox.TabIndex = 18
        '
        'listTypeLabel
        '
        Me.listTypeLabel.AutoSize = True
        Me.listTypeLabel.Location = New System.Drawing.Point(12, 74)
        Me.listTypeLabel.Name = "listTypeLabel"
        Me.listTypeLabel.Size = New System.Drawing.Size(71, 13)
        Me.listTypeLabel.TabIndex = 19
        Me.listTypeLabel.Text = "ASN Blacklist"
        '
        'AddASN
        '
        Me.AddASN.Location = New System.Drawing.Point(15, 230)
        Me.AddASN.Name = "AddASN"
        Me.AddASN.Size = New System.Drawing.Size(75, 30)
        Me.AddASN.TabIndex = 20
        Me.AddASN.Text = "Add"
        Me.AddASN.UseVisualStyleBackColor = True
        '
        'DeleteASN
        '
        Me.DeleteASN.Location = New System.Drawing.Point(181, 230)
        Me.DeleteASN.Name = "DeleteASN"
        Me.DeleteASN.Size = New System.Drawing.Size(75, 30)
        Me.DeleteASN.TabIndex = 21
        Me.DeleteASN.Text = "Delete"
        Me.DeleteASN.UseVisualStyleBackColor = True
        '
        'WhiteListRadio
        '
        Me.WhiteListRadio.AutoSize = True
        Me.WhiteListRadio.Location = New System.Drawing.Point(104, 72)
        Me.WhiteListRadio.Name = "WhiteListRadio"
        Me.WhiteListRadio.Size = New System.Drawing.Size(65, 17)
        Me.WhiteListRadio.TabIndex = 22
        Me.WhiteListRadio.TabStop = True
        Me.WhiteListRadio.Text = "Whitelist"
        Me.WhiteListRadio.UseVisualStyleBackColor = True
        '
        'BlackListRadio
        '
        Me.BlackListRadio.AutoSize = True
        Me.BlackListRadio.Location = New System.Drawing.Point(175, 72)
        Me.BlackListRadio.Name = "BlackListRadio"
        Me.BlackListRadio.Size = New System.Drawing.Size(64, 17)
        Me.BlackListRadio.TabIndex = 23
        Me.BlackListRadio.TabStop = True
        Me.BlackListRadio.Text = "Blacklist"
        Me.BlackListRadio.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(276, 340)
        Me.ControlBox = False
        Me.Controls.Add(Me.BlackListRadio)
        Me.Controls.Add(Me.WhiteListRadio)
        Me.Controls.Add(Me.DeleteASN)
        Me.Controls.Add(Me.AddASN)
        Me.Controls.Add(Me.listTypeLabel)
        Me.Controls.Add(Me.WhiteOrBlackListBox)
        Me.Controls.Add(Me.GetASN)
        Me.Controls.Add(Me.InstallBtn)
        Me.Controls.Add(Me.GetIpRng)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.userIPrange1)
        Me.Controls.Add(Me.userIPrange0)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CheckFrequencySec)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.TestBtn)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.UserProgName)
        Me.Controls.Add(Me.HideBtn)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Main"
        Me.Text = "Campus DMCA preventer"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.CheckFrequencySec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HideBtn As System.Windows.Forms.Button
    Friend WithEvents UserProgName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SaveBtn As System.Windows.Forms.Button
    Friend WithEvents TestBtn As System.Windows.Forms.Button
    Friend WithEvents StartBtn As System.Windows.Forms.Button
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckFrequencySec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents userIPrange0 As System.Windows.Forms.TextBox
    Friend WithEvents userIPrange1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GetIpRng As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents InstallBtn As System.Windows.Forms.Button
    Friend WithEvents GetASN As System.Windows.Forms.Button
    Friend WithEvents WhiteOrBlackListBox As System.Windows.Forms.ListBox
    Friend WithEvents listTypeLabel As System.Windows.Forms.Label
    Friend WithEvents AddASN As System.Windows.Forms.Button
    Friend WithEvents DeleteASN As System.Windows.Forms.Button
    Friend WithEvents WhiteListRadio As System.Windows.Forms.RadioButton
    Friend WithEvents BlackListRadio As System.Windows.Forms.RadioButton

End Class
