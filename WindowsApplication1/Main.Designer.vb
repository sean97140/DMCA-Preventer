﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Test = New System.Windows.Forms.Button()
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
        Me.Install = New System.Windows.Forms.Button()
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
        Me.HideBtn.Location = New System.Drawing.Point(15, 216)
        Me.HideBtn.Name = "HideBtn"
        Me.HideBtn.Size = New System.Drawing.Size(75, 23)
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
        Me.SaveBtn.Location = New System.Drawing.Point(159, 111)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(40, 23)
        Me.SaveBtn.TabIndex = 4
        Me.SaveBtn.Text = "Save"
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'Test
        '
        Me.Test.Location = New System.Drawing.Point(205, 111)
        Me.Test.Name = "Test"
        Me.Test.Size = New System.Drawing.Size(54, 23)
        Me.Test.TabIndex = 5
        Me.Test.Text = "Test"
        Me.Test.UseVisualStyleBackColor = True
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(96, 216)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 23)
        Me.StartBtn.TabIndex = 6
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Location = New System.Drawing.Point(177, 221)
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
        Me.CheckFrequencySec.Location = New System.Drawing.Point(213, 48)
        Me.CheckFrequencySec.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.CheckFrequencySec.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.CheckFrequencySec.Name = "CheckFrequencySec"
        Me.CheckFrequencySec.Size = New System.Drawing.Size(46, 20)
        Me.CheckFrequencySec.TabIndex = 9
        Me.CheckFrequencySec.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(190, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Check IP address frequency (seconds)"
        '
        'userIPrange0
        '
        Me.userIPrange0.Location = New System.Drawing.Point(159, 77)
        Me.userIPrange0.MaxLength = 3
        Me.userIPrange0.Name = "userIPrange0"
        Me.userIPrange0.Size = New System.Drawing.Size(30, 20)
        Me.userIPrange0.TabIndex = 11
        '
        'userIPrange1
        '
        Me.userIPrange1.Location = New System.Drawing.Point(205, 77)
        Me.userIPrange1.MaxLength = 3
        Me.userIPrange1.Name = "userIPrange1"
        Me.userIPrange1.Size = New System.Drawing.Size(31, 20)
        Me.userIPrange1.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(190, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 20)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = ".        . * . *"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 81)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Campus IP address range"
        '
        'GetIpRng
        '
        Me.GetIpRng.Location = New System.Drawing.Point(15, 111)
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
        Me.Install.Location = New System.Drawing.Point(205, 148)
        Me.Install.Name = "Install"
        Me.Install.Size = New System.Drawing.Size(53, 23)
        Me.Install.TabIndex = 16
        Me.Install.Text = "Install"
        Me.Install.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.ControlBox = False
        Me.Controls.Add(Me.Install)
        Me.Controls.Add(Me.GetIpRng)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.userIPrange1)
        Me.Controls.Add(Me.userIPrange0)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CheckFrequencySec)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.Test)
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
    Friend WithEvents Test As System.Windows.Forms.Button
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
    Friend WithEvents Install As System.Windows.Forms.Button

End Class