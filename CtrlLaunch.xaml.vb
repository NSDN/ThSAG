Imports ThSAG.EngineCore

Public Class CtrlLaunch
    Private GameTitle As String = My.Resources.GameName & " " & My.Resources.GameVersion
    Public Flag As Integer = -1
    Private GameObj As Game
    Private MsgBox As New Message
    Private Config_Tmp As AVG.Config

    Private Function LaunchWindow() As Boolean
        If DMode1.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 1280, 720, 1, 1) Then GoTo ExitFlag
        If DMode2.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 1440, 900, 1, 1) Then GoTo ExitFlag
        If DMode3.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 1600, 900, 1, 1) Then GoTo ExitFlag
        If DMode4.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 1680, 1050, 1, 1) Then GoTo ExitFlag
        If DMode5.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 1920, 1080, 1, 1) Then GoTo ExitFlag
        If DMode6.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 2160, 1440, 1, 1) Then GoTo ExitFlag
        If DMode7.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 3840, 2160, 1, 1) Then GoTo ExitFlag
        If DMode8.IsChecked Then If DxVB.DxInit(My.Resources.Icon.Handle, GameTitle, IsFullScreen.IsChecked, 800, 450, 1, 1) Then GoTo ExitFlag
        Return False
ExitFlag:
        Return True
    End Function

    Private Sub Launch_Click(sender As Object, e As RoutedEventArgs)
        Config_Tmp = AVG.LoadConfig("DATAs\Config.ini")
        MsgBox.MsgShow("Finsih", "Script load finished")

        Flag = 1
        Me.IsEnabled = False
        Dim Loading As New Message
        Loading.MsgShow("Initilizing", "Initilizing engine", False, False)

        If LaunchWindow() Then

            GameObj = New Game(Config_Tmp)
            Loading.HideMe()
            GameObj.Initilize()
            Do
                GameObj.Work()
            Loop Until GameObj.GetExitFlag() Or DxVB.DxEndInfo = -1
            DxVB.DxEnd()
        Else
            Loading.MsgShow("Warning", "Initilize engine failed")
        End If
        Flag = 0
        Me.IsEnabled = True
    End Sub

    Private Sub BtnNewConfig_Click(sender As Object, e As RoutedEventArgs)
        'Config_Tmp = AVG.LoadConfig("DATAs\Config.txt")
        'MsgBox.MsgShow("Finsih", "Script load finished")
    End Sub
End Class
