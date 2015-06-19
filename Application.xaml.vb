Imports System.Reflection.Assembly
Class Application

    ' 应用程序级事件(例如 Startup、Exit 和 DispatcherUnhandledException)
    ' 可以在此文件中进行处理。
    Sub Start() Handles Me.Startup
        LoadFrom("DLLs\Core86.dll")
        LoadFrom("DLLs\Core128.dll")

    End Sub

End Class
