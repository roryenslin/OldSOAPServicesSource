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
Public Class Prices
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(UserAccounts))

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
        Public Function Modify(ByVal lstPriceInfo As List(Of PriceInfo)) As BaseResponse

        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Price Entered----------->")
            If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstPriceInfo))
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction

            For Each objPriceInfo As PriceInfo In lstPriceInfo
                Dim cmdCommand As New SqlCommand("usp_price_modify", conConnection)
                cmdCommand.Transaction = trnTransaction
                cmdCommand.Parameters.AddWithValue("@SupplierID", objPriceInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objPriceInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@Pricelist", objPriceInfo.PriceList)
                cmdCommand.Parameters.AddWithValue("@Nett", objPriceInfo.Nett)
                cmdCommand.Parameters.AddWithValue("@Gross", objPriceInfo.Gross)
                cmdCommand.Parameters.AddWithValue("@Discount", objPriceInfo.Discount)
                cmdCommand.Parameters.AddWithValue("@Cost", objPriceInfo.Cost)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            Next

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            trnTransaction.Rollback()
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not conConnection Is Nothing And conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
        End Try
        Return objResponse
    End Function

    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As PriceReadListResponse
        Dim objResponse As New PriceReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objPriceInfo As PriceInfo()
            Dim cmdCommand As New SqlCommand("usp_price_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objPriceInfo = ReadPrices(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objPriceInfo Is Nothing AndAlso objPriceInfo.GetUpperBound(0) >= 0 Then
                objResponse.Prices = objPriceInfo
            End If
        Catch ex As Exception
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
    Public Function Test(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal straccountID As String, ByVal strBranch As String, ByVal offset As Integer, ByVal numrows As Integer) As String()
        'If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim pr As New PriceInfo
        pr.SupplierID = "TEST"
        pr.ProductID = "AAA"
        pr.Nett = 21.22
        pr.PriceList = "aA"
        pr.Discount = 0

        Dim lst As New List(Of PriceInfo)
        lst.add(pr)
        Modify(lst)

        Dim resultarray As New Generic.List(Of String)
        Dim br As PriceSync3Response = Sync5(strSupplierID, "", intVersion, Nothing, offset, numrows)
        If br.Status Then
            resultarray.Add("Sync4------> True ")
            resultarray.Add("count: " & br.Prices.Length)
            resultarray.Add("LastVersion: " & br.LastVersion)
        Else
            resultarray.Add("Sync4------> False ")
            For Each serror In br.Errors
                resultarray.Add(serror)
            Next
        End If
        Return resultarray.ToArray
    End Function

    <WebMethod()> _
    Public Function GetPrice(ByVal supplierID As String, ByVal accountID As String, ByVal productID As String, ByVal quantity As Integer, ByVal gross As Double, ByVal nett As Double) As PriceResponse
        If _Log.IsInfoEnabled Then _Log.Info("GetPrice Entered----------->" & supplierID & "/" & accountID & "/" & productID & "/" & quantity)
        Dim discount As Double
        Dim netprice As Double
        Dim conConnection As SqlConnection = Nothing
        Try
            conConnection = objDBHelper.GetConnection
            'conConnection.Open()
            Dim cmdCommand As New SqlCommand("usp_discount_readsingle", conConnection)

            cmdCommand.Parameters.AddWithValue("@SupplierID", supplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", accountID)
            cmdCommand.Parameters.AddWithValue("@ProductID", productID)
            cmdCommand.Parameters.AddWithValue("@Debug", _Log.IsDebugEnabled)
            cmdCommand.Parameters.AddWithValue("@qty", quantity)
            Dim objreader As SqlDataReader = objDBHelper.ExecuteReader(cmdCommand)
            If Not objreader Is Nothing AndAlso objreader.HasRows Then
                While (objreader.Read())
                    If CheckString(objreader("SupplierID")) = "DEBUG" Then
                        If _Log.IsDebugEnabled Then _Log.Debug(CheckString(objreader("DebugSQL")))
                    Else
                        If CheckDecimal(objreader("Discount")) > 0 Then
                            discount = CheckDecimal(objreader("Discount"))
                        ElseIf CheckDecimal(objreader("Price")) > 0 Then
                            netprice = CheckDecimal(objreader("Price"))
                        End If
                    End If
                End While
            End If
        Catch ex As Exception
        Finally
            conConnection.Close()
        End Try

        Dim resp = New PriceResponse
        resp.Discount = 0
        resp.Gross = gross
        resp.Nett = nett

        If discount > 0 Then
            resp.Discount = Integer.Parse(discount.ToString)
            resp.Nett = resp.Gross - (resp.Gross * (discount / 100))
            resp.Status = True
        ElseIf netprice > 0 Then
            resp.Discount = 0
            resp.Gross = netprice
            resp.Status = True
        Else
            resp.Status = False
            Dim messages(0) As String
            messages(0) = "No discount found"
            resp.Errors = messages
        End If

        If _Log.IsInfoEnabled Then _Log.Info("GetPrice ----------->" & accountID & "/disc=" & resp.Discount & "/nett=" & resp.Nett & "/status=" & resp.Status)
        Return resp
    End Function

    <WebMethod()> _
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstPrices As List(Of PriceInfo)) As PriceSync3Response
        Dim objResponse As New PriceSync3Response
        Dim objTempResponse As New PriceReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstPrices Is Nothing Then
                ProcessResponse(Modify(lstPrices), objTempResponse)
            End If

            objResponse.Prices = objTempResponse.Prices
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Price)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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
        If _Log.IsDebugEnabled Then _Log.Debug("exited")

        Return objResponse
    End Function
    <WebMethod()> _
     Public Function Sync5(ByVal strSupplierId As String, ByVal userID As String, ByVal intVersion As Integer, ByVal lstPrices As List(Of PriceInfo), ByVal offset As Integer, ByVal numrows As Integer) As PriceSync3Response
        Dim objResponse As New PriceSync3Response
        Dim objTempResponse As New PriceReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strSupplierId & " // Version: " & intVersion)
            objTempResponse = Sync2(strSupplierId, intVersion)
            Dim size As Integer = 0
            If offset + numrows < objTempResponse.Prices.Length Then
                size = numrows - 1
            Else
                size = objTempResponse.Prices.Length - offset - 1
            End If
            Dim tmp(size) As PriceInfo
            System.Array.Copy(objTempResponse.Prices, offset, tmp, 0, size + 1)
            objTempResponse.Prices = tmp

            If Not lstPrices Is Nothing Then
                ProcessResponse(Modify(lstPrices), objTempResponse)
            End If

            objResponse.Prices = objTempResponse.Prices
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Price)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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
        If _Log.IsDebugEnabled Then _Log.Debug("exited")

        Return objResponse
    End Function
    Private Function ReadPrices(ByVal objReader As SqlDataReader) As PriceInfo()
        Dim objPrices As PriceInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objPrices(intCounter)
                    objPrices(intCounter) = New PriceInfo
                    With objPrices(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .PriceList = CheckString(objReader("PriceList"))
                        .Nett = CheckDouble(objReader("Nett"))
                        .Gross = CheckDouble(objReader("Gross"))
                        .Discount = CheckDouble(objReader("Discount"))
                        .Cost = CheckDecimal(objReader("Cost"))
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objPrices
    End Function

End Class