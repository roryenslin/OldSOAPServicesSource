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
Public Class Orders4
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Orders))
    Private orderToEmail As OrderInfo4
    Private notificationEmail As String
    Private email As New EmailInfo

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Test(ByVal orderID As String, ByVal supplierID As String, ByVal userid As String, ByVal accountid As String, ByVal comments As String) As BaseResponse
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim order As New OrderInfo4
        order.SupplierID = supplierID
        order.OrderID = orderID
        order.UserID = userid
        order.AccountID = accountid
        order.CreateDate = Now.ToString("yyyyMMdd HH:mm:ss")
        order.RequiredByDate = Now.ToString("yyyyMMdd HH:mm:ss")
        order.Comments = comments
        order.Reference = "My reference" & Now.ToString("yyyyMMddhhmmss")
        order.ERPStatus = "Sent"
        order.ERPOrderNumber = "123"

        Dim line As New OrderItemInfo4
        line.OrderID = order.OrderID
        line.SupplierID = order.SupplierID
        line.ProductID = "ELC0500"
        line.RepChangedPrice = True
        line.RepDiscount = 10.5
        line.RepNett = 12.99
        line.Quantity = 1
        line.ItemID = 1
        order.OrderItems = New Generic.List(Of OrderItemInfo4)
        order.OrderItems.Add(line)
        Return Modify(order)
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objOrderInfo4 As OrderInfo4) As BaseResponse

        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered 4----------->")
            '*** always log an order
            If _Log.IsWarnEnabled Then _Log.Warn(RapidTradeWebService.Common.SerializationManager.Serialize(objOrderInfo4))

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction

            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_orders_modify", conConnection)
            cmdCommand.Transaction = trnTransaction
            '***this is temp
            'objOrderInfo4.RequiredByDate = Now
            'objOrderInfo4.CreateDate = Now
            If String.IsNullOrEmpty(objOrderInfo4.OrderID) Then objOrderInfo4.OrderID = Now.ToString("yyMMddhhmmss") & New Random().Next(50)

            cmdCommand.Parameters.AddWithValue("@OrderID", objOrderInfo4.OrderID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objOrderInfo4.SupplierID)
            cmdCommand.Parameters.AddWithValue("@UserID", objOrderInfo4.UserID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objOrderInfo4.AccountID)
            cmdCommand.Parameters.AddWithValue("@BranchID", objOrderInfo4.BranchID)
            cmdCommand.Parameters.AddWithValue("@Status", objOrderInfo4.Status)
            'cmdCommand.Parameters.AddWithValue("@OrderNumber", objOrderInfo4.OrderNumber)
            cmdCommand.Parameters.AddWithValue("@CreateDate", Activities2.ConvertDate(objOrderInfo4.CreateDate))
            cmdCommand.Parameters.AddWithValue("@RequiredByDate", Activities2.ConvertDate(objOrderInfo4.RequiredByDate))
            cmdCommand.Parameters.AddWithValue("@Reference", objOrderInfo4.Reference)
            cmdCommand.Parameters.AddWithValue("@DeliveryMethod", objOrderInfo4.DeliveryMethod)
            cmdCommand.Parameters.AddWithValue("@Comments", objOrderInfo4.Comments)
            cmdCommand.Parameters.AddWithValue("@Type", objOrderInfo4.Type)

            Try
                If String.IsNullOrEmpty(objOrderInfo4.DeliveryName) Then objOrderInfo4.DeliveryName = ""
                If String.IsNullOrEmpty(objOrderInfo4.DeliveryName) Or objOrderInfo4.DeliveryName.ToLower.Contains("no name") Then
                    Dim ob As AccountReadSingleResponse = New Accounts().ReadSingle(objOrderInfo4.SupplierID, objOrderInfo4.AccountID)
                    If ob.Status Then
                        cmdCommand.Parameters.AddWithValue("@DeliveryName", ob.Account.Name)
                    Else
                        cmdCommand.Parameters.AddWithValue("@DeliveryName", objOrderInfo4.DeliveryName)
                    End If
                Else
                    cmdCommand.Parameters.AddWithValue("@DeliveryName", objOrderInfo4.DeliveryName)
                End If
            Catch ex As Exception
                cmdCommand.Parameters.AddWithValue("@DeliveryName", objOrderInfo4.DeliveryName)
                If _Log.IsInfoEnabled Then _Log.Info("Warning: Problem reading account name:" & ex.Message)
            End Try

            cmdCommand.Parameters.AddWithValue("@UserField01", objOrderInfo4.UserField01)
            cmdCommand.Parameters.AddWithValue("@UserField02", objOrderInfo4.UserField02)
            cmdCommand.Parameters.AddWithValue("@UserField03", objOrderInfo4.UserField03)
            cmdCommand.Parameters.AddWithValue("@UserField04", objOrderInfo4.UserField04)
            cmdCommand.Parameters.AddWithValue("@UserField05", objOrderInfo4.UserField05)
            cmdCommand.Parameters.AddWithValue("@UserField06", objOrderInfo4.UserField06)
            cmdCommand.Parameters.AddWithValue("@UserField07", objOrderInfo4.UserField07)
            cmdCommand.Parameters.AddWithValue("@UserField08", objOrderInfo4.UserField08)
            cmdCommand.Parameters.AddWithValue("@UserField09", objOrderInfo4.UserField09)
            cmdCommand.Parameters.AddWithValue("@UserField10", objOrderInfo4.UserField10)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress1", objOrderInfo4.DeliveryAddress1)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress2", objOrderInfo4.DeliveryAddress2)
            cmdCommand.Parameters.AddWithValue("@DeliveryAddress3", objOrderInfo4.DeliveryAddress3)
            cmdCommand.Parameters.AddWithValue("@DeliveryPostCode", objOrderInfo4.DeliveryPostCode)
            cmdCommand.Parameters.AddWithValue("@PostedToERP", objOrderInfo4.PostedToERP)
            cmdCommand.Parameters.AddWithValue("@ERPOrderNumber", objOrderInfo4.ERPOrderNumber)
            cmdCommand.Parameters.AddWithValue("@ERPStatus", objOrderInfo4.ERPStatus)
            objOrderInfo4.UserField06 = "http://23.21.227.179/images/SILICONE/" & objOrderInfo4.SupplierID.ToLower & ".png"

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            If _Log.IsInfoEnabled Then _Log.Info("Items 4----------->")
            If (Not objOrderInfo4.OrderItems Is Nothing AndAlso objOrderInfo4.OrderItems.Count > 0) Then
                Dim cmdCommand1 As New SqlCommand("usp_orderitems_modify", conConnection)
                cmdCommand1.Transaction = trnTransaction
                For Each objOrderItem As OrderItemInfo4 In objOrderInfo4.OrderItems
                    '*** get description if needed
                    If String.IsNullOrEmpty(objOrderItem.Description) Then
                        Try
                            Dim cmdCommand3 As New SqlCommand("usp_product_readsingle")
                            cmdCommand3.Parameters.AddWithValue("@ProductID", objOrderItem.ProductID)
                            cmdCommand3.Parameters.AddWithValue("@SupplierID", objOrderInfo4.SupplierID)

                            Dim objReader As SqlDataReader = objDBHelper.ExecuteReader(cmdCommand3, conConnection)
                            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                                While (objReader.Read())
                                    objOrderItem.Description = objReader.GetString(0)
                                End While
                            End If
                            cmdCommand3.Connection.Close()
                        Catch ex As Exception
                            If _Log.IsErrorEnabled Then _Log.Error("Error finding description" & ex.Message)
                        End Try
                    End If

                    cmdCommand1.Parameters.Clear()
                    cmdCommand1.Parameters.AddWithValue("@OrderID", objOrderInfo4.OrderID)
                    cmdCommand1.Parameters.AddWithValue("@AccountID", objOrderInfo4.AccountID)
                    cmdCommand1.Parameters.AddWithValue("@SupplierID", objOrderInfo4.SupplierID)

                    cmdCommand1.Parameters.AddWithValue("@ItemID", objOrderItem.ItemID)
                    cmdCommand1.Parameters.AddWithValue("@ProductID", objOrderItem.ProductID)
                    cmdCommand1.Parameters.AddWithValue("@Warehouse", objOrderItem.Warehouse)
                    cmdCommand1.Parameters.AddWithValue("@Description", objOrderItem.Description)
                    cmdCommand1.Parameters.AddWithValue("@Unit", objOrderItem.Unit)
                    cmdCommand1.Parameters.AddWithValue("@Quantity", objOrderItem.Quantity)
                    cmdCommand1.Parameters.AddWithValue("@Nett", objOrderItem.Nett)
                    cmdCommand1.Parameters.AddWithValue("@Gross", objOrderItem.Gross)
                    cmdCommand1.Parameters.AddWithValue("@Discount", objOrderItem.Discount)
                    cmdCommand1.Parameters.AddWithValue("@ValueUnit", objOrderItem.ValueUnit)
                    cmdCommand1.Parameters.AddWithValue("@Value", objOrderItem.Value)
                    cmdCommand1.Parameters.AddWithValue("@RepNett", objOrderItem.RepNett)
                    cmdCommand1.Parameters.AddWithValue("@RepDiscount", objOrderItem.RepDiscount)
                    cmdCommand1.Parameters.AddWithValue("@RepChangedPrice", objOrderItem.RepChangedPrice)



                    objDBHelper.ExecuteNonQuery(cmdCommand1, conConnection)
                Next
            End If
            trnTransaction.Commit()
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "Error creating order. Error returned" + intResult.ToString()
            Else
                If objOrderInfo4.PostedToERP = True Then
                    If _Log.IsInfoEnabled Then _Log.Info("Not emailing as postedtoerp=true")
                Else
                    '*** now email the admin user in another thread
                    If mustEmail(objOrderInfo4.SupplierID, objOrderInfo4.UserID, objOrderInfo4.UserField05) Then
                        orderToEmail = objOrderInfo4
                        If _Log.IsInfoEnabled Then _Log.Info("Email 4----------->")
                        createEmailOrder()
                    End If
                End If
                
            End If

        Catch ex As Exception
            trnTransaction.Rollback()
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objOrderInfo4), ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            Try
                conConnection.Close()
            Catch ex As Exception
            End Try
        End Try
        If _Log.IsInfoEnabled Then _Log.Info("Order End 4----------->")
        Return objResponse
    End Function

    Private Function mustEmail(ByVal supplierID As String, ByVal userID As String, ByVal userfield05 As String) As Boolean
        Dim comma As String
        Dim hasemails As Boolean
        hasemails = False
        comma = ""
        Try
            Dim options As New Options
            Dim opt As OptionInfo = options.ReadSingle(supplierID, "OrderNotificationEmail", "security").OptionData
            notificationEmail = opt.Value
            hasemails = True
            comma = ","
        Catch ex As Exception
            If _Log.IsInfoEnabled Then _Log.Info("mustEmail could not find ordernotificationemail")
        End Try

        Try
            Dim users As New Users
            Dim resp As UserReadSingleResponse = users.ReadSingle(userID)
            If resp.Status = True Then
                If resp.User.Email.Contains("@") Then
                    notificationEmail = notificationEmail & comma & resp.User.Email
                    comma = ","
                    hasemails = True
                End If
            End If
        Catch ex As Exception
            If _Log.IsInfoEnabled Then _Log.Info("mustEmail could not find user email")
        End Try

        Try
            If userfield05.Contains("@") Then
                notificationEmail = notificationEmail & comma & userfield05
                hasemails = True
            End If
        Catch ex As Exception
            If _Log.IsInfoEnabled Then _Log.Info("mustEmail could not find userfield05 email")
        End Try
        If _Log.IsInfoEnabled Then _Log.Info("Email: " & notificationEmail)
        Return hasemails
    End Function


    Private Sub createEmailOrder()
        Try
            Dim accnt As New Accounts
            Dim resp As AccountReadSingleResponse = accnt.ReadSingle(orderToEmail.SupplierID, orderToEmail.AccountID)
            If resp.Status Then
                If String.IsNullOrEmpty(orderToEmail.DeliveryName) Then orderToEmail.DeliveryName = resp.Account.Name
            Else
                If _Log.IsErrorEnabled Then _Log.Info("No account found")
            End If
        Catch ex As Exception
            If _Log.IsInfoEnabled Then _Log.Info("Exception getting account name: " & ex.Message)
        End Try
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
        Dim xsltfilename As String = folder & "XSL\CreateOrder4.xslt"
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
    Public Function ReadList(ByVal strSupplierId As String, ByVal strUserId As String) As OrderReadListResponse4
        Dim objResponse As New OrderReadListResponse4
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objOrderInfo4 As OrderInfo4()
            Dim cmdCommand As New SqlCommand("usp_order_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)

            objOrderInfo4 = ReadOrders(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOrderInfo4 Is Nothing AndAlso objOrderInfo4.GetUpperBound(0) >= 0 Then
                objResponse.Orders = objOrderInfo4
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
    Public Function ReadUnPosted(ByVal strSupplierId As String) As OrderReadListResponse4
        Dim objResponse As New OrderReadListResponse4
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered 4----------->" & strSupplierId)
            Dim objOrderInfo4 As OrderInfo4()
            Dim cmdCommand As New SqlCommand("usp_order_readunposted")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)

            objOrderInfo4 = ReadOrders(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objOrderInfo4 Is Nothing AndAlso objOrderInfo4.GetUpperBound(0) >= 0 Then
                objResponse.Orders = objOrderInfo4
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

    Private Function ReadOrders(ByVal objReader As SqlDataReader) As OrderInfo4()
        Dim objOrders As OrderInfo4() = Nothing
        Dim intCounter As Integer = 0

        Dim objHash As New Hashtable
        Dim strFormTypeKey As String
        Dim objTempOrder As OrderInfo4 = Nothing
        Dim objTempOrderItem As OrderItemInfo4 = Nothing

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objOrders(intCounter)
                    objOrders(intCounter) = New OrderInfo4
                    With objOrders(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .OrderID = CheckString(objReader("OrderID"))
                        .UserID = CheckString(objReader("UserID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .BranchID = CheckString(objReader("BranchID"))
                        .Status = CheckString(objReader("Status"))
                        '.OrderNumber = CheckString(objReader("OrderNumber"))
                        If Not objReader("CreateDate") Is Nothing Then
                            .CreateDate = CheckDate(objReader("CreateDate").ToString()).ToString("yyyyMMdd HH:mm:ss")

                        End If
                        If Not objReader("RequiredByDate") Is Nothing Then
                            .RequiredByDate = CheckDate(objReader("RequiredByDate").ToString()).ToString("yyyyMMdd HH:mm:ss")
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
                        .ERPOrderNumber = CheckString(objReader("ERPOrderNumber"))
                        .ERPStatus = CheckString(objReader("ERPStatus"))
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
                                objTempOrder.OrderItems = New List(Of OrderItemInfo4)
                            End If
                            objTempOrderItem = New OrderItemInfo4
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
                                .Description = CheckString(objReader("Description"))
                                .RepNett = CheckDecimal(objReader("RepNett"))
                                .RepDiscount = CheckDecimal(objReader("RepDiscount"))
                                .RepChangedPrice = CheckBoolean(objReader("RepChangedPrice"))
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