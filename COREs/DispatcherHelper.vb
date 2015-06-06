Imports System.Windows.Threading
Imports System.Security.Permissions

Public Class DispatcherHelper
    <SecurityPermissionAttribute(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
    Public Shared Sub DoEvents()
        Dim frame As New DispatcherFrame()
        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, New DispatcherOperationCallback(AddressOf ExitFrames), frame)
        Try
            Dispatcher.PushFrame(frame)
        Catch generatedExceptionName As InvalidOperationException
        End Try
    End Sub
    Private Shared Function ExitFrames(frame As Object) As Object
        DirectCast(frame, DispatcherFrame).Continue = False
        Return Nothing
    End Function
End Class
