Imports System.Diagnostics

Namespace Common
    Public Class ExceptionManager
        Public Shared Sub LogException(ByVal objException As Exception)
            EventLog.WriteEntry("RWS", objException.ToString())
        End Sub
    End Class
End Namespace

