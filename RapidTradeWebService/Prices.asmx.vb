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
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
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
            If Not conConnection Is Nothing AndAlso Not conConnection.State = ConnectionState.Open Then
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
    Public Function Test(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal straccountID As String, ByVal strBranch As String) As String()
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim resultarray As New Generic.List(Of String)
        Dim br As PriceSync3Response = Sync3(strSupplierID, intVersion, Nothing)
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
        Dim resp = New PriceResponse
        resp.Discount = 10
        resp.Gross = gross
        resp.Nett = nett
        resp.Status = True
        Dim messages(0) As String
        messages(0) = "No implemented yet"
        resp.Errors = messages
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