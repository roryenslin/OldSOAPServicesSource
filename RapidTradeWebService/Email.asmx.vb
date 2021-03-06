﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response
Imports System.Net.Mail

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Email
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Email))

    'Dim objDBHelper As DBHelper

    Public Sub New()
        'objDBHelper = New DBHelper
    End Sub
    <WebMethod()> _
    Public Function Test(ByVal frm As String, ByVal toemail As String, ByVal subject As String, ByVal message As String) As BaseResponse
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then
            Throw New Exception("Tesling only allowed from via http://localhost")
        End If

        Dim email As New EmailInfo
        email.MailTo = toemail
        email.MailFrom = frm
        email.Subject = subject
        email.MailContent = message
        Return SendMail(email)
    End Function
    <WebMethod()> _
    Public Function SendMail(ByVal objMail As EmailInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug(SerializationManager.Serialize(objMail))

            Dim strMailServer As String = ConfigurationManager.AppSettings("SMTPServer")
            Dim strSMTPPort As String = ConfigurationManager.AppSettings("SMTPPort")
            Dim strUserId As String = ConfigurationManager.AppSettings("UserID")
            Dim strPassword As String = ConfigurationManager.AppSettings("Password")
            Dim strDomain As String = ConfigurationManager.AppSettings("Domain")
            Dim strAuthenitcationReq As String = ConfigurationManager.AppSettings("AuthenticationRequired")
            Dim strEnableSSL As String = ConfigurationManager.AppSettings("EnableSSL")

            Dim smtpPort As Integer = 0

            Integer.TryParse(strSMTPPort, smtpPort)

            If (String.IsNullOrEmpty(strMailServer)) Then
                If _Log.IsDebugEnabled Then _Log.Debug("Mail server not configured correctly...")
                objResponse.Status = False
                ReDim objResponse.Errors(0)
                objResponse.Errors(0) = "Mail server not configured correctly."
            ElseIf (String.IsNullOrEmpty(strSMTPPort)) Then
                If _Log.IsDebugEnabled Then _Log.Debug("SMTP Port not configured correctly...")
                objResponse.Status = False
                ReDim objResponse.Errors(0)
                objResponse.Errors(0) = "SMTP Port not configured correctly."
            Else
                Dim client As New SmtpClient
                Dim message As New MailMessage(New MailAddress(objMail.MailFrom), New MailAddress(objMail.MailTo))
                message.Subject = objMail.Subject
                message.Body = objMail.MailContent
                message.IsBodyHtml = objMail.IsHTML
                '*** now add additional TO addresses
                Dim toemails() As String = Split(objMail.MailTo, ",")
                If toemails.Length > 1 Then
                    For x As Integer = 1 To toemails.Length - 1
                        message.To.Add(toemails(x))
                    Next
                End If

                If (String.Equals(strAuthenitcationReq, "Y", StringComparison.InvariantCultureIgnoreCase)) Then
                    If _Log.IsDebugEnabled Then _Log.Debug("Authentication required is set as true...")
                    client.Credentials = New System.Net.NetworkCredential(strUserId, strPassword, strDomain)
                Else
                    If _Log.IsDebugEnabled Then _Log.Debug("Authentication required is set as false...")
                End If

                If (String.Equals(strAuthenitcationReq, "Y", StringComparison.InvariantCultureIgnoreCase)) Then
                    If _Log.IsDebugEnabled Then _Log.Debug("Enable SSL is set as true...")
                    client.EnableSsl = True
                End If

                If (smtpPort > 0) Then
                    If _Log.IsDebugEnabled Then _Log.Debug(String.Format("SMTP Port used: {0}", smtpPort))
                    client.Port = smtpPort
                End If

                client.Host = strMailServer
                Try
                    client.Send(message)
                Catch exc1 As exception
                    client.Credentials = New System.Net.NetworkCredential("orders103@rapidtrade.biz", "fertig", strDomain)
                    Try
                        client.Send(message)
                    Catch exc2 As Exception
                        client.Credentials = New System.Net.NetworkCredential("orders36@rapidtrade.biz", "fertig", strDomain)
                        Try
                            client.Send(message)
                        Catch exc3 As Exception
                            client.Credentials = New System.Net.NetworkCredential("ordersrapidsync@rapidtrade.biz", "pass@word1", strDomain)
                            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objMail), exc3)
                            objResponse.Status = False
                            Dim intCounter As Integer = 0
                            While Not exc3 Is Nothing
                                ReDim Preserve objResponse.Errors(intCounter)
                                objResponse.Errors(intCounter) = exc3.Message
                                exc3 = exc3.InnerException
                                intCounter = intCounter + 1
                            End While
                        End Try
                    End Try
                End Try

                objResponse.Status = True

                If _Log.IsDebugEnabled Then _Log.Debug("Email Send successfully...")
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objMail), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

End Class