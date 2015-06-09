Imports ThSAG.EngineCore

Public Class CtrlEdit

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
        Flag = 1
        Me.IsEnabled = False

        MsgBox.MsgShow("Initilizing", "Initilizing engine", False, False)

        If LaunchWindow() Then
            GameObj = New Game(Config_Tmp)
            MsgBox.HideMe()
            Do
                GameObj.Work()
            Loop Until GameObj.GetExitFlag() Or DxVB.DxEndInfo = -1
            DxVB.DxEnd()
        Else
            MsgBox.MsgShow("Warning", "Initilize engine failed")
        End If
        Flag = 0
        Me.IsEnabled = True
    End Sub  

    Private Sub BtnLoadConfig_Click(sender As Object, e As RoutedEventArgs)
        Config_Tmp = AVG.LoadConfig("DATAs\Config.txt")
        MsgBox.MsgShow("Finsih", "Script load finished")
    End Sub

    Private Sub BtnLoadScript_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub BtnScriptHelp_Click(sender As Object, e As RoutedEventArgs)
        MsgBox.MsgShow(860, 860, "Script help", "Public Help" & Chr(10) & _
               "//////////////////////////////////////////////////////////////////////////////////////" & Chr(13) & _
               "开始定义场景 [场景编号] [场景名，仅作注释，可用于Debug]" & Chr(13) & _
               "" & Chr(13) & _
               "        [元素定义]        '即 <结束定义场景> 下面的定义" & Chr(13) & _
               "" & Chr(13) & _
               "结束定义场景" & Chr(13) & _
               "" & Chr(13) & _
               "[背景/音乐] [地址]" & Chr(13) & _
               "" & Chr(13) & _
               "开始定义立绘 [立绘编号] [立绘名，仅作注释，可用于Debug]" & Chr(13) & _
               "" & Chr(13) & _
               "        立绘地址 [地址]" & Chr(13) & _
               "" & Chr(13) & _
               "        立绘位置 [位置]" & Chr(13) & _
               "" & Chr(13) & _
               "结束定义立绘" & Chr(13) & _
               "" & Chr(13) & _
               "开始定义文字 [对话/旁白/标题/存档/读档/启动]" & Chr(13) & _
               "" & Chr(13) & _
               "        选择 [内容]" & Chr(13) & _
               "" & Chr(13) & _
               "        跳转 [场景编号]" & Chr(13) & _
               "" & Chr(13) & _
               "结束定义文字" & Chr(13) & _
               "//////////////////////////////////////////////////////////////////////////////////////" & Chr(13) & _
               "场景编号需要手动设置。" & Chr(13) & _
               "背景为单一背景。" & Chr(13) & _
               "对于文字位置调整，请善用空格。当前还未解决字体问题，待解决。" & Chr(13) & _
               ">>>>>NOTE：制作图片资源时，按照游戏界面为1920x1080的分辨率制作，图片需要压缩至需要的像素大小。" & Chr(13) & _
               "立绘编号需要手动设置，目的是区分多立绘的情况,相同的立绘可以使用固定编号。（资源加载待解决，可能会出现内存占用高）" & Chr(13) & _
               "文件路径（即地址）为：【Audio\audio.wav】" & Chr(13) & _
               "文字定义中，脚本顺序即为显示顺序，选择在前，跳转在后，两者紧挨，一个文字定义中选择数目和跳转数目相同。" & Chr(13) & _
               "某些特殊场景，应当使用诸如【0】、【1】、【2】、【3】这种场景编号，并置于独立的文件中。" & Chr(13) & _
               "脚本文件的建立，最好分为配置文件（启动画面，标题画面），存档文件（存档画面，读档画面），游戏脚本。" & Chr(13) & _
               ">>>>>注意：元素定义中，背景，音乐，文字仅可定义一次；立绘定义内部的语句不允许重复，文字定义内部语句可以重复。" & Chr(13) & _
               "待添加。" & Chr(13) & _
               "End Help" _
                , False)
    End Sub

End Class
