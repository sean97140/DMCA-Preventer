Imports System.Web.HttpContext
Imports System.Net
Imports System.IO
Imports System.Math


Public Class Main
    Dim ASNCheckerThread As System.Threading.Thread
    Dim isFirstRun As Boolean = False
    Dim programName As String
    Dim wasKilled As Boolean = False
    Dim timeout As Integer
    Dim timeoutString As String
    'Dim ipRange() As String
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
            isFirstRun = True
            UpdateUserControls()
        Else
            LoadASNTables()
            UpdateUserControls()
            SetTimeout()
            StartCheckerThread()
        End If

    End Sub

    Private Sub LoadASNTables()
        'http://quaxio.com/bgp/ much credit deserved for finding this method
        LoadAndBuildIpRangeToASNTable()
        LoadAndBuildASNToOwnerTable()
        BuildOwnerToASNsTable()
    End Sub
    Private Sub LoadAndBuildIpRangeToASNTable()
        Dim ipRangeToASNDataFile As String = "data-raw-table.txt"
        Dim ipRangeBinaryReader As BinaryReader

        Try
            ipRangeBinaryReader = New BinaryReader(System.IO.File.OpenRead(ipRangeToASNDataFile))
        Catch ex As Exception
            MsgBox("No ASN data file found, please wait while it is downloaded. File size 10mb")
            My.Computer.Network.DownloadFile("http://thyme.apnic.net/current/data-raw-table", "data-raw-table.txt")
            ipRangeBinaryReader = New BinaryReader(System.IO.File.OpenRead(ipRangeToASNDataFile))
        End Try

        Dim ipRangeBinaryData As Byte() = ipRangeBinaryReader.ReadBytes(ipRangeBinaryReader.BaseStream.Length)
        Dim ms As MemoryStream = New MemoryStream(ipRangeBinaryData, 0, ipRangeBinaryData.Length)
        Dim ipRangeDataFileReader As New System.IO.StreamReader(ms)
        Dim TextLine As String = ""
        Dim ipRange As String
        Dim asn As Integer = -1

        Do While ipRangeDataFileReader.Peek() <> -1
            TextLine = ipRangeDataFileReader.ReadLine()
            ipRange = TextLine.Split()(0)
            asn = ConvertStringToInt(TextLine.Split()(1))
            ipRangeToASN.Add(ipRange, asn)
        Loop

        ipRangeDataFileReader.Close()
    End Sub
    Private Sub LoadAndBuildASNToOwnerTable()
        Dim ASNToOwnerDataFile As String = "data-used-autnums.txt"
        Dim asnOwnerBinaryReader As BinaryReader

        Try
            asnOwnerBinaryReader = New BinaryReader(System.IO.File.OpenRead(ASNToOwnerDataFile))
        Catch ex As Exception
            MsgBox("No ASN to Owner data file found, please wait while it is downloaded. File size 2mb")
            My.Computer.Network.DownloadFile("http://thyme.apnic.net/current/data-used-autnums", "data-used-autnums.txt")
            asnOwnerBinaryReader = New BinaryReader(System.IO.File.OpenRead(ASNToOwnerDataFile))
        End Try

        Dim asnOwnerBinaryData As Byte() = asnOwnerBinaryReader.ReadBytes(asnOwnerBinaryReader.BaseStream.Length)
        Dim ms2 As MemoryStream = New MemoryStream(asnOwnerBinaryData, 0, asnOwnerBinaryData.Length)
        Dim ASNToOwnerDataFileReader As New System.IO.StreamReader(ms2)
        Dim ownerParts As String()
        Dim owner As String = ""
        Dim TextLine As String = ""
        Dim asn As Integer = -1

        Do While ASNToOwnerDataFileReader.Peek() <> -1
            owner = ""
            TextLine = ASNToOwnerDataFileReader.ReadLine().ToString().Trim()
            ownerParts = TextLine.Split()
            For i As Integer = 1 To ownerParts.Length - 1
                owner = owner + " " + ownerParts(i)
            Next

            asn = ConvertStringToInt(TextLine.Split()(0))
            ASNToOwner.Add(asn, owner.Trim())
        Loop

        ASNToOwnerDataFileReader.Close()
    End Sub
    Private Sub BuildOwnerToASNsTable()
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

    End Sub

    Private Sub UpdateUserControls()
        If Not isFirstRun Then
            UserProgName.Text = programName
            CheckFrequencySec.Value = ConvertStringToInt(timeoutString)
            InstallBtn.Enabled = Not installed
            TestBtn.Enabled = True

            If isBlackList Then
                listTypeLabel.Text = "ASN Blacklist"
                BlackListRadio.Checked = True
            Else
                listTypeLabel.Text = "ASN Whitelist"
                WhiteListRadio.Checked = True
            End If

            For i As Integer = 0 To asnBlackWhiteList.Count() - 1
                Dim temp As String = asnBlackWhiteList.Item(i)
                WhiteOrBlackListBox.Items.Add(temp.ToString + " " + ASNToOwner.Item(temp))
            Next

        Else
            TestBtn.Enabled = False
        End If
    End Sub
    Private Sub SetTimeout()
        timeout = CheckFrequencySec.Value * 1000
        timeoutString = CheckFrequencySec.Value.ToString
    End Sub
    Public Shared Function ConvertStringToInt(inputString As String) As Integer
        Try
            Return Convert.ToInt32(inputString)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub StartCheckerThread()
        ASNCheckerThread = New System.Threading.Thread(AddressOf ASNChecker)
        ASNCheckerThread.Start()
        StatusLabel.Text = "Status: running"
        StartBtn.Enabled = False
    End Sub

    Private Sub StopCheckerThread()
        Try
            ASNCheckerThread.Abort()
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
            'temp solution to getting ip
            ipExternal = wc.DownloadString("http://ipv4.icanhazip.com")
            Exit Try
        Catch ex As Exception
            Return "0.0.0.0"
        End Try

        Return ipExternal
    End Function

    Private Sub ASNChecker()
        While True
            Threading.Thread.Sleep(timeout)
            CheckASNAndKillIfNecessary(False)
        End While
    End Sub

    Private Sub CheckASNAndKillIfNecessary(isTestMode As Boolean)
        Dim num As Integer = GetASNumber()

        If isBlackList Then
            If asnBlackWhiteList.Contains(num) Then
                If Not isTestMode Then
                    KillFileSharing()
                Else
                    MsgBox("A black list match was found")
                End If
            Else
                MsgBox("According to blacklist you are not on a black listed network")
            End If
        Else
            If Not asnBlackWhiteList.Contains(num) Then
                If Not isTestMode Then
                    KillFileSharing()
                Else
                    MsgBox("A non white list network was detected: " + ASNToOwner.Item(num))
                End If
            Else
                MsgBox("You are currently on a whitelisted network")
            End If
        End If

        'Garbage collector is lazy, and this program will slowly consume ALOT of memory before it kicks in (if it does)
        GC.Collect()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ASNCheckerThread.Abort()
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
            MsgBox("You've been saved from a potential DMCA violation running " + programName.ToUpper _
                   + " on " + ASNToOwner.Item(GetASNumber()) + "'s network", vbSystemModal + vbExclamation)
            wasKilled = True
        Next
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        ShowSettings()
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        Dim txtinput As String() = UserProgName.Text.Split(".")

        If txtinput.Length > 1 Then
            MsgBox("Please enter only the process name, not the .exe portion. Please check correction and resave.")
            UserProgName.Text = txtinput(0)
        Else
            SaveAndUpdateSettings()
            isFirstRun = False
            'UpdateUserControls() 'fully test to see if this can be removed
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
        Dim asnString As String() = GetSetting("DMCA Preventer", "settings", "asnBlackWhiteList").Split()

        For i As Integer = 0 To asnString.Count() - 1
            asnBlackWhiteList.Add(ConvertStringToInt(asnString.GetValue(i)))
        Next

    End Sub

    Private Sub SaveAndUpdateSettings()
        SaveSetting("DMCA Preventer", "settings", "program name", UserProgName.Text)
        SaveSetting("DMCA Preventer", "settings", "installed", installed.ToString)
        SaveSetting("DMCA Preventer", "settings", "BlackList", isBlackList.ToString)
        SaveSetting("DMCA Preventer", "settings", "timeout", CheckFrequencySec.Value.ToString)

        Dim asnString As String = ""

        For i As Integer = 0 To asnBlackWhiteList.Count() - 1 Step 1
            asnString = asnString + " " + asnBlackWhiteList.Item(i).ToString()
        Next

        SaveSetting("DMCA Preventer", "settings", "asnBlackWhiteList", asnString.Trim())
        programName = UserProgName.Text
        SetTimeout()
    End Sub

    'update for asn
    Private Sub Test_Click(sender As Object, e As EventArgs) Handles TestBtn.Click
        wasKilled = False
        Dim ipArray = GetIPAddress().Split(".")

        KillFileSharing()
        If wasKilled = False Then
            MsgBox("Please verify the process name of your program, it may not be running. EX: utorrent")
        End If

        CheckASNAndKillIfNecessary(True)
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        StartCheckerThread()
    End Sub

    Private Sub Install_Click(sender As Object, e As EventArgs) Handles InstallBtn.Click
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
        'http://www.unixwiz.net/techtips/netmask-ref.html
        'http://quaxio.com/bgp/ much credit deserved for finding this method

        Dim ipAddress As String = GetIPAddress()
        Dim ipAddressArray As String() = ipAddress.Split(".")
        Dim matchWasFound As Boolean = False
        Dim asnNum As Integer = -1

        For maskBits As Integer = 0 To 32 Step 1
            If maskBits <= 8 Then
                Dim firstPrefix As Integer = ConvertStringToInt(ipAddressArray(3))
                firstPrefix = (firstPrefix >> maskBits) << maskBits
                ipAddressArray(3) = firstPrefix.ToString
            ElseIf maskBits <= 16 Then
                Dim secondPrefix As Integer = ConvertStringToInt(ipAddressArray(2))
                secondPrefix = (secondPrefix >> maskBits - 8) << maskBits - 8
                ipAddressArray(2) = secondPrefix.ToString
            ElseIf maskBits <= 24 Then
                Dim thirdPrefix As Integer = ConvertStringToInt(ipAddressArray(1))
                thirdPrefix = (thirdPrefix >> maskBits - 16) << maskBits - 16
                ipAddressArray(1) = thirdPrefix.ToString
            ElseIf maskBits <= 32 Then
                Dim fourthPrefix As Integer = ConvertStringToInt(ipAddressArray(0))
                fourthPrefix = (fourthPrefix >> maskBits - 24) << maskBits - 24
                ipAddressArray(0) = fourthPrefix.ToString
            End If

            Dim ipRangeString As String = String.Join(".", ipAddressArray) + "/" + (32 - maskBits).ToString()

            If ipRangeToASN.ContainsKey(ipRangeString) Then
                asnNum = ipRangeToASN.Item(ipRangeString)
                matchWasFound = True
                Exit For
            Else
                matchWasFound = False
            End If
        Next

        If matchWasFound Then
            Return asnNum
        Else
            Return -1
        End If

    End Function

    Private Sub GetASN_Click(sender As Object, e As EventArgs) Handles GetASN.Click
        Dim num As Integer = GetASNumber()
        Dim owner As String = ASNToOwner.Item(num)
        Dim asnList As List(Of Integer) = OwnerToASNs.Item(owner)
        Dim addString As String = ""

        For i As Integer = 0 To asnList.Count() - 1 Step 1
            addString = asnList.Item(i).ToString + " " + owner

            'prevent duplicates
            If Not WhiteOrBlackListBox.Items.Contains(addString) Then
                WhiteOrBlackListBox.Items.Add(addString)
            End If

            asnBlackWhiteList.Add(asnList.Item(i))
        Next

    End Sub

    Private Sub DeleteASN_Click(sender As Object, e As EventArgs) Handles DeleteASN.Click
        asnBlackWhiteList.Remove(WhiteOrBlackListBox.SelectedItem.ToString.Split()(0))
        WhiteOrBlackListBox.Items.Remove(WhiteOrBlackListBox.SelectedItem)
    End Sub

    Private Sub AddASN_Click(sender As Object, e As EventArgs) Handles AddASN.Click
        Dim asn As Integer = ConvertStringToInt(InputBox("Please enter ASN"))
        WhiteOrBlackListBox.Items.Add(asn.ToString + " " + ASNToOwner.Item(asn))
        asnBlackWhiteList.Add(asn)
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles WhiteListRadio.CheckedChanged
        listTypeLabel.Text = "ASN Whitelist"
        isBlackList = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles BlackListRadio.CheckedChanged
        listTypeLabel.Text = "ASN Blacklist"
        isBlackList = True
    End Sub
End Class

