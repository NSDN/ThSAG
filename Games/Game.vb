Imports ThSAG.EngineCore
Public Class Game
    Private ExitFlag As Boolean = False
    Private Config_Obj As New AVG.Config
    Private AVGGame As AVG.AVGCore
    Private FPS_Obj As Base.FPSCounter
    Private MsgBox As New Message

    Public Sub New(ByRef Config As AVG.Config)
        FPS_Obj = New Base.FPSCounter()
        Config_Obj = Config
    End Sub

    Public Sub Initilize()
        DxVB.DxTitle(Config_Obj.GameName)
        AVGGame = New AVG.AVGCore("DATAs\Image\Cursor.png")
        MsgBox.MsgShow("Loading", "Loading resources...", False, False)
        AVGGame.LoadScenes(AVG.LoadScript(Config_Obj.SavePath))
        AVGGame.ResourcesLoad()
        MsgBox.HideMe()
    End Sub

    Public Sub Work()
        DxVB.DxFlip()
        Render()
        Control()
        FPS_Obj.Update()
        FPS_Obj.WaitTime()
    End Sub

    Public Function GetExitFlag() As Boolean
        Return ExitFlag
    End Function

    Private Sub Render()
        AVGGame.Work()
    End Sub

    Private Sub Control()
        If Base.GetKey(Base.Keys.KeyESCAPE) Then ExitFlag = True
    End Sub

End Class
