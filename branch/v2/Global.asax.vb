Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Global_asax))

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        _Log.Info("Application started...")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        Dim ex As Exception = HttpContext.Current.Server.GetLastError()
        While ex.InnerException IsNot Nothing
            ex = ex.InnerException
        End While
        If TypeOf ex Is HttpException AndAlso DirectCast(ex, HttpException).ErrorCode = 404 Then
            If _Log.IsWarnEnabled Then _Log.Warn("404 - Page not found", ex)
        Else
            If _Log.IsFatalEnabled Then _Log.Fatal("Unhandled exception!", ex)
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class