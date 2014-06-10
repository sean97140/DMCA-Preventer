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

    Dim ipRangeToASN As New Dictionary(Of String, Integer)
    Dim ASNToOwner As New Dictionary(Of Integer, String)
    Dim OwnerToASNs As New Dictionary(Of String, List(Of Integer))
    Dim asnBlackWhiteList As New List(Of Integer)
    Dim isBlackList As Boolean = True

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
            LoadASNTable()
            UpdateUserControls()
            SetTimeout()
            StartCheckerThread()
        End If
    End Sub
    Private Sub LoadASNTable()
        'http://quaxio.com/bgp/ much credit deserved for finding this method

        Dim FILE_NAME As String = "data-raw-table.txt"
        Dim FILE_NAME1 As String = "data-used-autnums.txt"
        '
        Dim objReader
        Dim objReader1

        Try
            objReader = New System.IO.StreamReader(FILE_NAME)

        Catch ex As Exception
            '    StatusLabel.Text = "Downloading ASN lookup table"
            MsgBox("No ASN data file found, please wait while it is downloaded. File size 10mb")
            My.Computer.Network.DownloadFile("http://thyme.apnic.net/current/data-raw-table", "data-raw-table.txt")
            objReader = New System.IO.StreamReader(FILE_NAME)
        End Try

        Try
            objReader1 = New System.IO.StreamReader(FILE_NAME1)

        Catch ex As Exception
            '    StatusLabel.Text = "Downloading ASN lookup table"
            MsgBox("No ASN to Owner data file found, please wait while it is downloaded. File size 2mb")
            My.Computer.Network.DownloadFile("http://thyme.apnic.net/current/data-used-autnums", "data-used-autnums.txt")
            objReader1 = New System.IO.StreamReader(FILE_NAME1)
        End Try

        Dim TextLine As String = ""
        Dim ipRange As String
        Dim asn As Integer = -1
        'StatusLabel.Text = "Loading ASN lookup table"

        Do While objReader.Peek() <> -1
            TextLine = objReader.ReadLine()
            ipRange = TextLine.Split()(0)
            asn = SetValue(TextLine.Split()(1))
            ipRangeToASN.Add(ipRange, asn)
        Loop

        Dim ownerParts As String()
        Dim owner As String = ""

        Try
            Do While objReader1.Peek() <> -1
                owner = ""
                TextLine = objReader1.ReadLine().ToString().Trim()
                ownerParts = TextLine.Split()
                For i As Integer = 1 To ownerParts.Length - 1
                    owner = owner + " " + ownerParts(i)
                Next

                asn = SetValue(TextLine.Split()(0))
                ASNToOwner.Add(asn, owner.Trim())
            Loop
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        objReader.Close()
        objReader1.Close()

        For Each i As Integer In ASNToOwner.Keys()
            Dim owner1 As String = ASNToOwner.Item(i)
            If OwnerToASNs.ContainsKey(owner1) Then
                Dim asnList As List(Of Integer) = OwnerToASNs.Item(owner1)
                asnList.Add(i)
            Else
                Dim asnList As New List(Of Integer)
                asnList.Add(i)
                OwnerToASNs.Add(owner1, asnList)
            End If

        Next
        'MsgBox("done")

    End Sub
    Private Sub UpdateUserControls()
        If Not firstRun Then
            UserProgName.Text = programName
            CheckFrequencySec.Value = SetValue(timeoutString)
            userIPrange0.Text = ipRange(0)
            userIPrange1.Text = ipRange(1)
            Install.Enabled = Not installed
            Test.Enabled = True

            If isBlackList Then
                Label2.Text = "ASN Blacklist"
                RadioButton2.Checked = True
            Else
                Label2.Text = "ASN Whitelist"
                RadioButton1.Checked = True
            End If

            For i As Integer = 0 To asnBlackWhiteList.Count() - 1
                Dim temp As String = asnBlackWhiteList.Item(i)
                ListBox1.Items.Add(temp.ToString + " " + ASNToOwner.Item(temp))
            Next
        Else
            Test.Enabled = False
        End If
    End Sub
    Private Sub SetTimeout()
        timeout = CheckFrequencySec.Value * 1000
        timeoutString = CheckFrequencySec.Value.ToString
    End Sub
    Public Shared Function SetValue(timeoutString As String) As Integer
        Try
            Return Convert.ToInt32(timeoutString)
        Catch ex As Exception
            Return -1
        End Try

    End Function

    Private Sub StartCheckerThread()
        myThread = New System.Threading.Thread(AddressOf ASNChecker)
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
    Private Sub ASNChecker()
        While True
            Threading.Thread.Sleep(timeout)
            Dim num As Integer = GetASNumber()
            If isBlackList Then
                If asnBlackWhiteList.Contains(num) Then
                    KillFileSharing()
                End If
            Else
                If Not asnBlackWhiteList.Contains(num) Then
                    KillFileSharing()
                End If
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

            'UpdateUserControls()

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

        If GetSetting("DMCA Preventer", "settings", "BlackList") = "True" Then
            isBlackList = True
        Else
            isBlackList = False
        End If

        timeoutString = GetSetting("DMCA Preventer", "settings", "timeout")
        programName = GetSetting("DMCA Preventer", "settings", "program name")
        ipRange = GetSetting("DMCA Preventer", "settings", "ipPrefix").Split(".")
        Dim asnString As String() = GetSetting("DMCA Preventer", "settings", "asnBlackWhiteList").Split()

        'asnBlackWhiteList.Add(SetValue(asnString.GetValue(0)))
        For i As Integer = 0 To asnString.Count() - 1
            asnBlackWhiteList.Add(SetValue(asnString.GetValue(i)))
        Next

    End Sub
    Private Sub SaveAndUpdateSettings()
        SaveSetting("DMCA Preventer", "settings", "program name", UserProgName.Text)
        SaveSetting("DMCA Preventer", "settings", "installed", installed.ToString)
        SaveSetting("DMCA Preventer", "settings", "BlackList", isBlackList.ToString)
        SaveSetting("DMCA Preventer", "settings", "timeout", CheckFrequencySec.Value.ToString)
        SaveSetting("DMCA Preventer", "settings", "ipPrefix", userIPrange0.Text + "." + userIPrange1.Text)
        ipRange = GetSetting("DMCA Preventer", "settings", "ipPrefix").Split(".")

        Dim asnString As String = ""

        For i As Integer = 0 To asnBlackWhiteList.Count() - 1 Step 1
            asnString = asnString + " " + asnBlackWhiteList.Item(i).ToString()
        Next

        SaveSetting("DMCA Preventer", "settings", "asnBlackWhiteList", asnString.Trim())


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
    Private Function GetASNumber() As Integer
        Dim test As String = GetIPAddress()
        Dim test1 As String() = test.Split(".")
        Dim found As Boolean = False
        Dim asnNum As Integer = -1

        For maskBits As Integer = 0 To 32 Step 1
            If maskBits <= 8 Then
                Dim firstPrefix As Integer = SetValue(test1(3))
                firstPrefix = (firstPrefix >> maskBits) << maskBits
                test1(3) = firstPrefix.ToString
            ElseIf maskBits <= 16 Then
                Dim secondPrefix As Integer = SetValue(test1(2))
                secondPrefix = (secondPrefix >> maskBits - 8) << maskBits - 8
                test1(2) = secondPrefix.ToString

            ElseIf maskBits <= 24 Then
                Dim thirdPrefix As Integer = SetValue(test1(1))
                thirdPrefix = (thirdPrefix >> maskBits - 16) << maskBits - 16
                test1(1) = thirdPrefix.ToString
            ElseIf maskBits <= 32 Then
                Dim fourthPrefix As Integer = SetValue(test1(0))
                fourthPrefix = (fourthPrefix >> maskBits - 24) << maskBits - 24
                test1(0) = fourthPrefix.ToString
            End If
            Dim testString As String = String.Join(".", test1) + "/" + (32 - maskBits).ToString()

            If ipRangeToASN.ContainsKey(testString) Then
                ' Write value of the key.
                asnNum = ipRangeToASN.Item(testString)
                'MsgBox(testString + " AS" + num.ToString + " " + owner)
                found = True
                Exit For
                'MsgBox("Other ASNs for: " + Owner + " are: " + asnListString)
            Else
                found = False
            End If
        Next
        If found Then
            Return asnNum
        Else
            Return -1
        End If
        'Return -1
    End Function
    Private Sub GetASN_Click(sender As Object, e As EventArgs) Handles GetASN.Click
        'http://www.unixwiz.net/techtips/netmask-ref.html
        'http://quaxio.com/bgp/ much credit deserved for finding this method

        Dim num As Integer = GetASNumber()

        Dim owner As String = ASNToOwner.Item(num)
        Dim asnList As List(Of Integer) = OwnerToASNs.Item(owner)

        Dim asnListString As String = ""
        For i As Integer = 0 To asnList.Count() - 1 Step 1
            asnListString = (asnListString + " " + asnList.Item(i).ToString).Trim
            ListBox1.Items.Add(asnList.Item(i).ToString + " " + owner)
            asnBlackWhiteList.Add(asnList.Item(i))
        Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        asnBlackWhiteList.Remove(ListBox1.SelectedItem.ToString.Split()(0))
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim temp As Integer = SetValue(InputBox("Please enter ASN"))
        ListBox1.Items.Add(temp.ToString + " " + ASNToOwner.Item(temp))
        asnBlackWhiteList.Add(temp)

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Label2.Text = "ASN Whitelist"
        isBlackList = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Label2.Text = "ASN Blacklist"
        isBlackList = True
    End Sub
End Class

