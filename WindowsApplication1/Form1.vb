﻿Imports System.Web.HttpContext
Imports System.Net
Imports System.IO
Imports System.Math


Public Class Form1
    Dim myThread As System.Threading.Thread
    Dim firstRun As Boolean = False
    Dim programName As String
    Dim wasKilled As Boolean = False
    Dim timeout As Integer
    Dim timeoutString As String
    Dim ipRange() As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForFirstRunUpdateAndStartThread()
    End Sub
    Private Sub CheckForFirstRunUpdateAndStartThread()
        GetSettings()

        If programName = "" Then
            MsgBox("Disclaimer: This program is designed for PSU main campus WiFi and VPN. This program will not work on many of the dorm's internet. The author assumes no liability from DMCA violations recieved while using this program. This program is to be used as a backup to manually checking if your file sharing program is running before connecting to campus internet/vpn.")
            MsgBox("Please specify the name of your torrent program")
            firstRun = True
        Else
            UpdateUserControls()
            SetTimeout()
            StartCheckerThread()
        End If
    End Sub
    Private Sub UpdateUserControls()
        TextBox1.Text = programName
        NumericUpDown1.Value = SetValue(timeoutString)
        userIPrange0.Text = ipRange(0)
        userIPrange1.Text = ipRange(1)

    End Sub
    Private Sub SetTimeout()
        timeout = NumericUpDown1.Value * 1000
    End Sub
    Public Shared Function SetValue(timeoutString As String) As Integer
        Return Convert.ToInt32(timeoutString)
    End Function

    Private Sub StartCheckerThread()
        myThread = New System.Threading.Thread(AddressOf IpAddressChecker)
        myThread.Start()
        Label2.Text = "Status: running"
        Button4.Enabled = False
    End Sub
    Private Sub StopCheckerThread()
        If Not firstRun Then
            myThread.Abort()
            Label2.Text = "Status: not running"
            Button4.Enabled = True
            Refresh()
        End If
    End Sub
    Private Function GetIPAddress() As String
        Dim wc As New WebClient
        Dim ipEXT As String
        Try
            ipEXT = wc.DownloadString("http://icanhazip.com")
            Exit Try
        Catch ex As Exception
            Return "0.0.0.0"
        End Try
        Return ipEXT
    End Function
    Private Sub IpAddressChecker()
        Dim ipArray As String()

        While True
            Threading.Thread.Sleep(timeout)
            ipArray = GetIPAddress().Split(".")
            If ipArray(0) = ipRange(0) And ipArray(1) = ipRange(1) Then
                KillFileSharing()
            End If
            GC.Collect()
        End While
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        myThread.Abort()
        Close()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        ShowSettings()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        HideForm()
    End Sub

    Private Sub ShowSettings()
        Show()
        NotifyIcon1.Visible = False
    End Sub
    Private Sub HideForm()
        Hide()
        NotifyIcon1.Visible = True
    End Sub
    Private Sub KillFileSharing()
        Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName(programName)
        For Each p As Process In pProcess
            p.Kill()
            MsgBox("You've been saved from a potential DMCA violation running " + programName.ToUpper + " on campus ip address space", vbSystemModal + vbExclamation)
            wasKilled = True
        Next
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ShowSettings()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim txtinput As String()
        txtinput = TextBox1.Text.Split(".")

        If txtinput.Length > 1 Then
            MsgBox("Please enter only the process name, not the .exe portion. Please check correction and resave.")
            TextBox1.Text = txtinput(0)
        Else

            SaveAndUpdateSettings()

            MsgBox("Setting saved, please launch your program and use the test button")
            StopCheckerThread()
        End If
    End Sub
    Private Sub GetSettings()
        timeoutString = GetSetting("TestApp", "settings", "timeout")
        programName = GetSetting("TestApp", "settings", "program name")
        ipRange = GetSetting("TestApp", "settings", "ipPrefix").Split(".")
    End Sub
    Private Sub SaveAndUpdateSettings()
        SaveSetting("TestApp", "settings", "program name", TextBox1.Text)
        SaveSetting("TestApp", "settings", "timeout", NumericUpDown1.Value.ToString)
        SaveSetting("TestApp", "settings", "ipPrefix", userIPrange0.Text + "." + userIPrange1.Text)
        ipRange = GetSetting("TestApp", "settings", "ipPrefix").Split(".")
        programName = TextBox1.Text
        SetTimeout()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        wasKilled = False
        Dim ipArray = GetIPAddress().Split(".")

        KillFileSharing()
        If wasKilled = False Then
            MsgBox("Please verify the process name of your program, it may not be running. EX: utorrent")
        End If
        If ipArray(0) = ipRange(0) And ipArray(1) = ipRange(1) Then
            MsgBox("You are on campus IP address space and can recieve a DMCA with any file sharing")
        Else
            MsgBox("According to the settings, you are not on campus internet. Your IP address is: " + String.Join(".", ipArray))
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        StartCheckerThread()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim temp As Integer = MsgBox("Are you on campus internet/vpn?", MsgBoxStyle.YesNo)
        If temp = 6 Then
            Dim ipArray() As String = GetIPAddress().Split(".")
            If ipArray(0) = 0 And ipArray(1) = 0 Then
                MsgBox("Internet error, not connected, please verify connection")
            Else
                userIPrange0.Text = ipArray(0)
                userIPrange1.Text = ipArray(1)
                MsgBox("Please save your settings if the information is correct")
            End If

        Else
            MsgBox("Please connect to campus internet and try again.")
        End If
        
    End Sub


    Private Sub Install_Click(sender As Object, e As EventArgs) Handles Install.Click
        Dim exePathAndName As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
        Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim installPath As String = appData + "\DMCA Preventer"
        Dim filePathAndName As String = installPath + "\DMCA.exe"
        Dim curUserRegRun As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run"
        Dim RegKeyName As String = "DMCA Preventer"

        Try
            My.Computer.FileSystem.CreateDirectory(installPath)
            My.Computer.FileSystem.CopyFile(exePathAndName, filePathAndName, overwrite:=True)
            My.Computer.Registry.SetValue(curUserRegRun, RegKeyName, filePathAndName)
            MsgBox("Install Sucessful")
        Catch ex As Exception
            'Debuging for now, proper exception handling needed
            MsgBox(ex.ToString)
        End Try

    End Sub
End Class
