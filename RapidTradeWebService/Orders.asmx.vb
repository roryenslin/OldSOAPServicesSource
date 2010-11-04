Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Orders
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Orders))
    Private orderToEmail As OrderInfo
    Private notificationEmail As String
    Private email As New EmailInfo

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Test(ByVal orderID As String, ByVal supplierID As String, ByVal userid As String, ByVal accountid As String, ByVal comments As String) As BaseResponse
        Dim order As New OrderInfo
        order.SupplierID = supplierID
        order.OrderID = orderID
        order.UserID = userid
        order.AccountID = accountid
        order.CreateDate = Now
        order.RequiredByDate = Now
        order.Comments = comments
        order.Reference = "My reference" & Now.ToString("yyyyMMddhhmmss")

        Dim line As New OrderItemInfo
        line.OrderID = order.OrderID
        line.SupplierID = order.SupplierID
        line.ProductID = "AAA"
        line.Quantity = 1
        line.ItemID = 1
        order.OrderItems = New Generic.List(Of OrderItemInfo)
        order.OrderItems.Add(line)
        Return Modify(order)
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objOrderInfo As OrderInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            '*** always log an order
            If _Log.IsWarnEnabled Then _Log.Warn(RapidTradeWebService.Common.SerializationManager.Serialize(objOrderInfo))

            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_orders_modify")

            '***this is temp
            'objOrderInfo.RequiredByDate = Now
            'objOrderInfo.CreateDate = Now

            cmdCommand.Parameters.AddWithValue("@OrderID", objOrderInfo.OrderID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objOrderInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objOrderInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objOrderInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objOrderInfo.BranchID)
            cmdCommand.Parameters.AddWithValue("@Status", objOrderInfo.Status)
            'cmdCommand.Parameters.AddWithValue("@OrderNumber", objOrderInfo.OrderNumber)
            cmdCommand.Parameters.AddWithValue("@CreateDate", objOrderInfo.CreateDate)
            cmdCommand.Parameters.AddWithValue("@RequiredByDate", objOrderInfo.RequiredByDate)
            cmdCommand.Parameters.AddWithValue("@Reference", objOrderInfo.Reference)
            cmdCommand.Parameters.AddWithValue("@DeliveryMethod", objOrderInfo.DeliveryMethod)
            cmdCommand.Parameters.AddWithValue("@Comments", objOrderInfo.Comments)
            cmdCommand.Parameters.AddWithValue("@Type", objOrderInfo.Type)
            cmdCommand.Parameters.AddWithValue("@DeliveryName", objOrderInfo.DeliveryName)
            cmdCommand.Parameters.AddWithValue("@UserField01", objOrderInfo.UserField01)
            cmdCommand.Parameters.AddWithValue("@UserField02", objOrderInfo.UserField02)
            cmdCommand.Parameters.AddWithValue("@UserField03", objOrderInfo.UserField03)
            cmdCommand.Parameters.AddWithValue("@UserField04", objOrderInfo.UserField04)
            cmdCommand.Parameters.AddWithValue("@UserField05", objOrderInfo.UserField05)
            cmdCommand.Parameters.AddWithValue("@UserField06", objOrderInfo.UserField06)
            cmdCommand.Parameters.AddWithValue("@UserField07", objOrderInfo.UserField07)
            cmdCommand.Parameters.AddWithValue("@UserField08", objOrderInfo.UserField08)
            cmdCommand.Parameters.AddWithValue("@UserField09", objOrderInfo.UserField09)
            cmdCommand.Parameters.AddWithValue("@UserField10", objOrderInfo.UserField10)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress1", objOrderInfo.DeliveryAddress1)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress2", objOrderInfo.DeliveryAddress2)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress3", objOrderInfo.DeliveryAddress3)
            cmdCommand.Parameters.AddWithValue("@DeliveryPostCode", objOrderInfo.DeliveryPostCode)
            cmdCommand.Parameters.AddWithValue("@PostedToERP", objOrderInfo.PostedToERP)
            cmdCommand.Parameters.AddWithValue("@RapidTradeID", objOrderInfo.RapidTradeID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            If (Not objOrderInfo.OrderItems Is Nothing AndAlso objOrderInfo.OrderItems.Count > 0) Then
                Dim cmdCommand1 As New SqlCommand("usp_orderitems_modify")
                For Each objOrderItem As OrderItemInfo In objOrderInfo.OrderItems
                    cmdCommand1.Parameters.Clear()
                    cmdCommand1.Parameters.AddWithValue("@OrderID", objOrderInfo.OrderID)
                    cmdCommand1.Parameters.AddWithValue("@AccountID", objOrderInfo.AccountID)
                    cmdCommand1.Parameters.AddWithValue("@SupplierID", objOrderInfo.SupplierID)

                    cmdCommand1.Parameters.AddWithValue("@ItemID", objOrderItem.ItemID)
                    cmdCommand1.Parameters.AddWithValue("@ProductID", objOrderItem.ProductID)
                    cmdCommand1.Parameters.AddWithValue("@Warehouse", objOrderItem.Warehouse)
                    cmdCommand1.Parameters.AddWithValue("@Unit", objOrderItem.Unit)
                    cmdCommand1.Parameters.AddWithValue("@Quantity", objOrderItem.Quantity)
                    cmdCommand1.Parameters.AddWithValue("@Nett", objOrderItem.Nett)
                    cmdCommand1.Parameters.AddWithValue("@Gross", objOrderItem.Gross)
                    cmdCommand1.Parameters.AddWithValue("@Discount", objOrderItem.Discount)
                    cmdCommand1.Parameters.AddWithValue("@ValueUnit", objOrderItem.ValueUnit)
                    cmdCommand1.Parameters.AddWithValue("@Value", objOrderItem.Value)

                    objDBHelper.ExecuteNonQuery(cmdCommand1)
                Next
            End If

            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "Error creating order. Error returned" + intResult.ToString()
            Else
                '*** now email the admin user in another thread
                If mustEmail(objOrderInfo.SupplierID) Then
                    orderToEmail = objOrderInfo
                    createEmailOrder()
                End If
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objOrderInfo), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse
    End Function

    Private Function mustEmail(ByVal supplierID As String) As Boolean
        Try
            Dim options As New Options
            Dim opt As OptionInfo = options.ReadSingle(supplierID, "OrderNotificationEmail", "security").OptionData
            notificationEmail = opt.Value
        Catch ex As Exception
            If supplierID = "TEST" Then
                notificationEmail = "shaunenslin@gmail.com" '*** used to test on live
                Return True
            Else
                Return False
            End If
        End Try
        If String.IsNullOrEmpty(notificationEmail) Then
            If supplierID = "TEST" Then
                notificationEmail = "shaunenslin@gmail.com" '*** used to test on live
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Private Sub createEmailOrder()
        Try
            '*** create email
            If _Log.IsDebugEnabled Then _Log.Debug("Creating email")
            email.MailFrom = "no-reply@rapidtrade.biz"
            email.Subject = "Incoming Order for " & orderToEmail.AccountID
            email.IsHTML = True
            email.MailContent = createEmailText()
            email.MailTo = notificationEmail

            '*** send email
            'Dim thrd As New System.Threading.Thread(AddressOf SendEmail)
            'thrd.Priority = Threading.ThreadPriority.Highest
            'thrd.Start()
            'Threading.Thread.Sleep(5000)
            SendEmail()
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error  creating Emailing", ex)
        End Try
    End Sub

    Private Sub SendEmail()
        Try
            '*** send email
            If _Log.IsDebugEnabled Then _Log.Debug("Sending email")
            Dim ctrlemail As New Email
            Dim br As BaseResponse = ctrlemail.SendMail(email)
            If _Log.IsDebugEnabled Then _Log.Debug("Email Response:" & br.Status)

            If br.Status = False Then
                If _Log.IsErrorEnabled Then _Log.Error("Error Emailing:" & IIf(br.Errors.Length > 0, br.Errors(0), "").ToString)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error sending Emailing", ex)
        End Try

    End Sub

    Private Function createEmailText() As String
        '*** get folder
        Dim folder As String = Context.Request.PhysicalApplicationPath & "bin\"
        '*** convert object to xml
        Dim xml As New System.Xml.XmlDocument
        Dim xmlfilename As String = folder & "Order_" & Now.ToString("yyMMddhhmmss") & ".xml"
        xml.LoadXml(RapidTradeWebService.Common.SerializationManager.Serialize(orderToEmail))
        xml.Save(xmlfilename)

        '*** apply stylesheet
        Dim htmlfilename As String = folder & "Order_" & Now.ToString("yyMMddhhmmss") & ".html"
        Dim xsltfilename As String = folder & "XSL\CreateOrder.xslt"
        If Not System.IO.File.Exists(xsltfilename) Then Throw New Exception("XSL file not found:" & xsltfilename)
        Dim xsl As New System.Xml.Xsl.XslCompiledTransform
        xsl.Load(xsltfilename)
        xsl.Transform(xmlfilename, htmlfilename)

        '*** return html for email and delete temp files
        Dim rslt As String = System.IO.File.ReadAllText(htmlfilename)

        '*** delete old files
        Try
            System.IO.File.Delete(htmlfilename)
            System.IO.File.Delete(xmlfilename)
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error Deleting temp files", ex)
        End Try
        Return rslt
    End Function

    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As OrderReadListResponse
        Dim objResponse As New OrderReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objOrderInfo As OrderInfo()
            Dim cmdCommand As New SqlCommand("usp_order_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)

            objOrderInfo = ReadOrders(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOrderInfo Is Nothing AndAlso objOrderInfo.GetUpperBound(0) >= 0 Then
                objResponse.Orders = objOrderInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strUserId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ReadUnPosted(ByVal strSupplierId As String) As OrderReadListResponse
        Dim objResponse As New OrderReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objOrderInfo As OrderInfo()
            Dim cmdCommand As New SqlCommand("usp_order_readunposted")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)

            objOrderInfo = ReadOrders(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOrderInfo Is Nothing AndAlso objOrderInfo.GetUpperBound(0) >= 0 Then
                objResponse.Orders = objOrderInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        End Try
        Return objResponse
    End Function

    Private Function ReadOrders(ByVal objReader As SqlDataReader) As OrderInfo()
        Dim objOrders As OrderInfo() = Nothing
        Dim intCounter As Integer = 0

        Dim objHash As New Hashtable
        Dim strFormTypeKey As String
        Dim objTempOrder As OrderInfo = Nothing
        Dim objTempOrderItem As OrderItemInfo = Nothing

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objOrders(intCounter)
                    objOrders(intCounter) = New OrderInfo
                    With objOrders(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .OrderID = CheckString(objReader("OrderID"))
                        .UserID = CheckString(objReader("UserID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .BranchID = CheckString(objReader("BranchID"))
                        .Status = CheckString(objReader("Status"))
                        '.OrderNumber = CheckString(objReader("OrderNumber"))
                        If Not objReader("CreateDate") Is Nothing Then
                            .CreateDate = CheckDate(objReader("CreateDate").ToString())
                        End If
                        If Not objReader("RequiredByDate") Is Nothing Then
                            .RequiredByDate = CheckDate(objReader("RequiredByDate").ToString())
                        End If
                        .Reference = CheckString(objReader("Reference"))
                        .DeliveryMethod = CheckString(objReader("DeliveryMethod"))
                        .Comments = CheckString(objReader("Comments"))
                        .Type = CheckString(objReader("Type"))
                        .DeliveryName = CheckString(objReader("DeliveryName"))
                        .UserField01 = CheckString(objReader("UserField01"))
                        .UserField02 = CheckString(objReader("UserField02"))
                        .UserField03 = CheckString(objReader("UserField03"))
                        .UserField04 = CheckString(objReader("UserField04"))
                        .UserField05 = CheckString(objReader("UserField05"))
                        .UserField06 = CheckString(objReader("UserField06"))
                        .UserField07 = CheckString(objReader("UserField07"))
                        .UserField08 = CheckString(objReader("UserField08"))
                        .UserField09 = CheckString(objReader("UserField09"))
                        .UserField10 = CheckString(objReader("UserField10"))
                        .DeliveryAddress1 = CheckString(objReader("DeliveryAddress1"))
                        .DeliveryAddress2 = CheckString(objReader("DeliveryAddress2"))
                        .DeliveryAddress3 = CheckString(objReader("DeliveryAddress3"))
                        .DeliveryPostCode = CheckString(objReader("DeliveryPostCode"))
                        .PostedToERP = CheckBoolean(objReader("PostedToERP"))
                        .RapidTradeID = CheckString(objReader("RapidTradeID"))
                        objHash.Add(String.Format("{0}", Trim(.OrderID)), intCounter)
                    End With
                    intCounter = intCounter + 1
                End While

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strFormTypeKey = String.Format("{0}", Trim(CheckString(objReader("OrderID"))))
                        If objHash.ContainsKey(strFormTypeKey) Then
                            objTempOrder = objOrders(CInt(objHash(strFormTypeKey)))
                            If objTempOrder.OrderItems Is Nothing OrElse objTempOrder.OrderItems.Count = 0 Then
                                objTempOrder.OrderItems = New List(Of OrderItemInfo)
                            End If
                            objTempOrderItem = New OrderItemInfo
                            With objTempOrderItem
                                .OrderID = CheckString(objReader("OrderID"))
                                .ItemID = CheckInteger(objReader("ItemID"))
                                .ProductID = CheckString(objReader("ProductID"))
                                .Warehouse = CheckString(objReader("Warehouse"))
                                .Unit = CheckString(objReader("Unit"))
                                .Quantity = CheckDecimal(objReader("Quantity"))
                                .Nett = CheckDecimal(objReader("Nett"))
                                .Gross = CheckDecimal(objReader("Gross"))
                                .Discount = CheckDecimal(objReader("Discount"))
                                .ValueUnit = CheckDecimal(objReader("ValueUnit"))
                                .Value = CheckDecimal(objReader("Value"))
                            End With
                            objTempOrder.OrderItems.Add(objTempOrderItem)
                        End If
                    End While
                End If
            End If
       
        Finally
            objReader.Close()
        End Try
        Return objOrders
    End Function

End Class