Imports ThSAG.EngineCore
Public Class Game
    Private ExitFlag As Boolean = False
    Private Config_Obj As New AVG.Config
    Private Scene() As AVG.Scene
    Private FPS_Obj As Base.FPSCounter

    Public Sub New()
        FPS_Obj = New Base.FPSCounter()

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

    End Sub

    Private Sub Control()
        If Base.GetKey(Base.Keys.KeyESCAPE) Then ExitFlag = True
    End Sub

End Class
