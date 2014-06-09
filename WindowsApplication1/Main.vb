Imports System.Web.HttpContext
Imports System.Net
Imports System.IO
Imports System.Math


Public Class Main
    Dim myThread As System.Threading.Thread
    Dim firstRun As Boolean = False
    Dim programName As String
    Dim wasKilled As Boolean = False
    Dim timeout As Integer
    Dim timeoutString As String
    Dim ipRange() As String
    Dim installed As Boolean = False


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForFirstRunUpdateAndStartThread()
    End Sub
    Private Sub CheckForFirstRunUpdateAndStartThread()
        GetSettings()

        If programName = "" Then
            MsgBox("Disclaimer: This program is designed for PSU main campus WiFi and VPN. This program will not work on many of the dorm's internet. The author assumes no liability from DMCA violations recieved while using this program. This program is to be used as a backup to manually checking if your file sharing program is running before connecting to campus internet/vpn.")
            MsgBox("Please specify the name of your torrent program")
            firstRun = True
            UpdateUserControls()
        Else
            UpdateUserControls()
            SetTimeout()
            StartCheckerThread()
        End If
    End Sub
    Private Sub UpdateUserControls()
        If Not firstRun Then
            UserProgName.Text = programName
            CheckFrequencySec.Value = SetValue(timeoutString)
            userIPrange0.Text = ipRange(0)
            userIPrange1.Text = ipRange(1)
            Install.Enabled = Not installed
            Test.Enabled = True
        Else
            Test.Enabled = False
        End If
    End Sub
    Private Sub SetTimeout()
        timeout = CheckFrequencySec.Value * 1000
        timeoutString = CheckFrequencySec.Value.ToString
    End Sub
    Public Shared Function SetValue(timeoutString As String) As Integer
        Return Convert.ToInt32(timeoutString)
    End Function

    Private Sub StartCheckerThread()
        myThread = New System.Threading.Thread(AddressOf IpAddressChecker)
        myThread.Start()
        StatusLabel.Text = "Status: running"
        StartBtn.Enabled = False
    End Sub
    Private Sub StopCheckerThread()
        Try
            myThread.Abort()
            StatusLabel.Text = "Status: not running"
            StartBtn.Enabled = True
            Refresh()
        Catch ex As Exception
            'do nothing - temp hack to fix bug
        End Try
        

    End Sub
    Private Function GetIPAddress() As String
        Dim wc As New WebClient
        Dim ipExternal As String

        Try
            ipExternal = wc.DownloadString("http://ipv4.icanhazip.com")
            Exit Try
        Catch ex As Exception
            Return "0.0.0.0"
        End Try

        Return ipExternal
    End Function
    Private Sub IpAddressChecker()
        Dim ipArray As String()

        While True
            Threading.Thread.Sleep(timeout)
            ipArray = GetIPAddress().Split(".")
            If ipArray(0) = ipRange(0) And ipArray(1) = ipRange(1) Then
                KillFileSharing()
            End If

            'Garbage collector is lazy, and this program will slowly consume ALOT of memory before it kicks in (if it does)
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

    Private Sub Hide_Click(sender As Object, e As EventArgs) Handles HideBtn.Click
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

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        Dim txtinput As String()
        txtinput = UserProgName.Text.Split(".")

        If txtinput.Length > 1 Then
            MsgBox("Please enter only the process name, not the .exe portion. Please check correction and resave.")
            UserProgName.Text = txtinput(0)
        Else

            SaveAndUpdateSettings()
            firstRun = False
            UpdateUserControls()

            MsgBox("Setting saved, please launch your program and use the test button")
            StopCheckerThread()
        End If
    End Sub
    Private Sub GetSettings()
        If GetSetting("DMCA Preventer", "settings", "installed") = "True" Then
            installed = True
        Else
            installed = False
        End If

        timeoutString = GetSetting("DMCA Preventer", "settings", "timeout")
        programName = GetSetting("DMCA Preventer", "settings", "program name")
        ipRange = GetSetting("DMCA Preventer", "settings", "ipPrefix").Split(".")
    End Sub
    Private Sub SaveAndUpdateSettings()
        SaveSetting("DMCA Preventer", "settings", "program name", UserProgName.Text)
        SaveSetting("DMCA Preventer", "settings", "installed", installed.ToString)
        SaveSetting("DMCA Preventer", "settings", "timeout", CheckFrequencySec.Value.ToString)
        SaveSetting("DMCA Preventer", "settings", "ipPrefix", userIPrange0.Text + "." + userIPrange1.Text)
        ipRange = GetSetting("DMCA Preventer", "settings", "ipPrefix").Split(".")
        programName = UserProgName.Text
        SetTimeout()
    End Sub
    Private Sub Test_Click(sender As Object, e As EventArgs) Handles Test.Click
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

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        StartCheckerThread()
    End Sub

    Private Sub GetIpRange_Click(sender As Object, e As EventArgs) Handles GetIpRng.Click
        Dim temp As Integer = MsgBox("Are you on campus internet/vpn?", MsgBoxStyle.YesNo)
        If temp = MsgBoxResult.Yes Then
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

        'catches when the program is running in visual studio where it will incorrectly use the progname.vshost.exe
        'rather than the progname.exe
        If exePathAndName.Split(".").Length > 2 Then
            exePathAndName = exePathAndName.Split(".")(0) + "." + exePathAndName.Split(".")(2)
        End If

        Dim appData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim installPath As String = appData + "\DMCA Preventer"
        Dim filePathAndName As String = installPath + "\DMCA.exe"
        Dim curUserRegRun As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run"
        Dim RegKeyName As String = "DMCA Preventer"

        Try
            My.Computer.FileSystem.CreateDirectory(installPath)
            My.Computer.FileSystem.CopyFile(exePathAndName, filePathAndName, overwrite:=True)
            My.Computer.Registry.SetValue(curUserRegRun, RegKeyName, filePathAndName)
            installed = True
            SaveAndUpdateSettings()
            MsgBox("Install Sucessful, current instance will now close and installed copy will be executed")
            System.Diagnostics.Process.Start(filePathAndName)
            End

        Catch ex As Exception
            'Debuging for now, proper exception handling needed
            MsgBox(ex.ToString)
        End Try

    End Sub
End Class

