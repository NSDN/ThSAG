Imports ThSAG.EngineCore

Public Class CtrlLaunch
    Private GameTitle As String = My.Resources.GameName & " " & My.Resources.GameVersion
    Public Flag As Integer = -1
    Private GameObj As Game
    Private MsgBox As New Message
    Private MsgStr As String = ""
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

    Private Sub Reset_Click(sender As Object, e As RoutedEventArgs)
        If MsgBox.MsgShow("Warning", "Are you sure?", True) Then
            AVG.ResetConfig("DATAs\Config.ini")
            MsgBox.MsgShow("Finish", "Game has reset")
        End If
    End Sub

    Private Sub Launch_Click(sender As Object, e As RoutedEventArgs)
        Config_Tmp = AVG.LoadConfig("DATAs\Config.ini")
        'MsgBox.MsgShow("Finish", "Script load finished")

        Flag = 1
        Me.IsEnabled = False
        'MsgBox.MsgShow("Initilizing", "Initilizing engine", False, False)

        If LaunchWindow() Then
            DxVB.DxVolume(CtrlVol.Value)
            GameObj = New Game(Config_Tmp)
            'MsgBox.HideMe()
            GameObj.Initilize()
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

    Private Sub BtnSaveHelp_Click(sender As Object, e As RoutedEventArgs)
        MsgStr = "存档功能是类似东方绀珠传完美模式的那种即时存档机制。" & vbCr & _
                 "保存的场景号在Config.ini里面"
        MsgBox.MsgShow(420, 240, "Save help", MsgStr, False)
    End Sub

    Private Sub BtnScriptHelp_Click(sender As Object, e As RoutedEventArgs)
        MsgStr = "给出一个例子，以下所有的句法格式都以 [命令] [数据] 为准" & vbCr & _
                 "[开始定义场景" & "/" & "defsc" & "/" & "def.scene]" & " " & "[场景编号/不能为负数]" & vbCr & _
                 "  [背景" & "/" & "backg" & "/" & "set.back]" & " " & "[背景路径/相对于主程序的。按照1080P制作]" & vbCr & _
                 "  [音乐" & "/" & "music" & "/" & "set.bgm]" & " " & "[音乐路径/相对于主程序的，只需要在一个场景设置一次就可以持续播放]" & vbCr & _
                 "  [开始定义立绘" & "/" & "defcg" & "/" & "def.cg]" & " " & "[立绘编号/不能为负数。注意控制大小]" & vbCr & _
                 "      [立绘地址" & "/" & "cgloc" & "/" & "set.path]" & " " & "[立绘路径/相对于主程序的]" & vbCr & _
                 "      [立绘位置" & "/" & "cgpos" & "/" & "set.pos]" & " " & "[立绘位置/0~1920]" & vbCr & _
                 "  [结束定义立绘" & "/" & "endcg" & "/" & "end.cg]" & vbCr & _
                 "  [开始定义文字" & "/" & "defwd" & "/" & "def.word]" & " " & "[文字类型/以下有说明]" & vbCr & _
                 "      [选择" & "/" & "cword" & "/" & "set.word]" & " " & "[文字内容/注意控制长度]" & vbCr & _
                 "      [跳转" & "/" & "jmpto" & "/" & "set.jump]" & " " & "[跳转场景号/和上面的成对使用。两者可以重复使用]" & vbCr & _
                 "  [结束定义文字" & "/" & "endwd" & "/" & "end.word]" & vbCr & _
                 "[结束定义场景" & "/" & "endsc" & "/" & "end.scene]" & vbCr & _
                 vbCr & "文字类型：" & vbCr & _
                 "[对话" & "/" & "comm" & "/" & "type.comm]" & " -> " & "可选择，靠屏幕下方" & vbCr & _
                 "[旁白" & "/" & "scpt" & "/" & "type.script]" & " -> " & "不可选择，全屏" & vbCr & _
                 "[标题" & "/" & "titl" & "/" & "type.title]" & " -> " & "可选择，全屏" & vbCr & _
                 "[启动" & "/" & "stup" & "/" & "type.start]" & " -> " & "不可选择，屏幕中间" & vbCr & vbCr & _
                 "所有资源请按照1080P的分辨率级别制作，脚本请以UTF-8编码保存。" & vbCr & _
                 "注意场景重定义，如果场景构建出错请检查脚本。可以在多个脚本文件里定义场景。"
        MsgBox.MsgShow(720, 640, "Script help", MsgStr, False)
    End Sub

    Private Sub BtnConfigHelp_Click(sender As Object, e As RoutedEventArgs)
        MsgStr = "给出一个例子，以下所有的句法格式都以 [命令] [数据] 为准" & vbCr & _
                 "[def.name" & "/" & "游戏名]" & " " & "[游戏名/请使用英文]" & vbCr & _
                 "[def.font" & "/" & "字体]" & " " & "[字体名/正确的字体名]" & vbCr & _
                 "[def.save" & "/" & "存档]" & " " & "[上次游戏退出时的场景编号]" & vbCr & _
                 "[def.script" & "/" & "脚本]" & " " & "[脚本路径/相对于主程序的。可以重复使用]" & vbCr & vbCr & _
                 "注意这里使用中文书写后，当游戏运行一次会被覆盖为英文。" & vbCr & _
                 "请以UTF-8编码保存为Config.ini并且放置在： DATAs\ 下" & vbCr & vbCr & _
                 "其他：" & vbCr & _
                 "DATAs\Audio下的两个WAV文件在修改的时候不要改变原文件名。" & vbCr & _
                 "DATAs\Image下的Cursor.png在修改的时候不要改变原文件名。"
        MsgBox.MsgShow(480, 360, "Config help", MsgStr, False)
    End Sub

    

End Class
