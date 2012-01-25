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
Public Class Address
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Address))
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Test(ByVal supplierid As String, ByVal userID As String, ByVal intVersion As Integer, ByVal accountid As String, ByVal addressid As String, ByVal street As String) As AddressSync4Response
        If Context.Request.ServerVariables("remote_addr") <> "127.0.0.1" Then Throw New Exception("Tesling only allowed from via http://localhost")
        Dim newaddress As New AddressInfo
        newaddress.SupplierID = supplierid
        newaddress.AccountID = accountid
        newaddress.AddressID = addressid
        newaddress.Street = street
        Dim lst As New Generic.List(Of AddressInfo)
        lst.Add(newaddress)
        Dim br As AddressSync4Response = Sync4(supplierid, userID, intVersion, lst)
        Return br
    End Function

    <WebMethod()> _
    Public Function Modify(ByVal objAddressInfo As AddressInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_Address_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objAddressInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@AccountID", objAddressInfo.AccountID)
            cmdCommand.Parameters.AddWithValue("@AddressID", objAddressInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@Unit", objAddressInfo.unit)
            cmdCommand.Parameters.AddWithValue("@Street", objAddressInfo.Street)
            cmdCommand.Parameters.AddWithValue("@PostalCode", objAddressInfo.PostalCode)
            cmdCommand.Parameters.AddWithValue("@City", objAddressInfo.City)
            cmdCommand.Parameters.AddWithValue("@Region", objAddressInfo.Region)
            cmdCommand.Parameters.AddWithValue("@Country", objAddressInfo.Country)
            cmdCommand.Parameters.AddWithValue("@Telephone", objAddressInfo.Telephone)
            cmdCommand.Parameters.AddWithValue("@Cell", objAddressInfo.Cell)
            cmdCommand.Parameters.AddWithValue("@Fax", objAddressInfo.Fax)
            cmdCommand.Parameters.AddWithValue("@WebSite", objAddressInfo.WebSite)
            cmdCommand.Parameters.AddWithValue("@Email", objAddressInfo.Email)
            cmdCommand.Parameters.AddWithValue("@Deleted", objAddressInfo.Deleted)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
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
        If _Log.IsInfoEnabled Then _Log.Info("exited")
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function ModifyAll(ByVal list As List(Of AddressInfo)) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing
        If _Log.IsInfoEnabled Then _Log.Info("ModifyAll Entered to update ----------->")
        If List Is Nothing Then Return objResponse

        Try
            If _Log.IsInfoEnabled Then _Log.Info("Update count " & list.Count)
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            If conConnection.State <> ConnectionState.Open Then conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            For Each objAddressInfo In list

                Dim cmdCommand As New SqlCommand("usp_Address_modify", conConnection)
                cmdCommand.Parameters.AddWithValue("@SupplierID", objAddressInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@AccountID", objAddressInfo.AccountID)
                cmdCommand.Parameters.AddWithValue("@AddressID", objAddressInfo.AddressID)
                cmdCommand.Parameters.AddWithValue("@Unit", objAddressInfo.unit)
                cmdCommand.Parameters.AddWithValue("@Street", objAddressInfo.Street)
                cmdCommand.Parameters.AddWithValue("@PostalCode", objAddressInfo.PostalCode)
                cmdCommand.Parameters.AddWithValue("@City", objAddressInfo.City)
                cmdCommand.Parameters.AddWithValue("@Region", objAddressInfo.Region)
                cmdCommand.Parameters.AddWithValue("@Country", objAddressInfo.Country)
                cmdCommand.Parameters.AddWithValue("@Telephone", objAddressInfo.Telephone)
                cmdCommand.Parameters.AddWithValue("@Cell", objAddressInfo.Cell)
                cmdCommand.Parameters.AddWithValue("@Fax", objAddressInfo.Fax)
                cmdCommand.Parameters.AddWithValue("@WebSite", objAddressInfo.WebSite)
                cmdCommand.Parameters.AddWithValue("@Email", objAddressInfo.Email)
                cmdCommand.Parameters.AddWithValue("@Deleted", objAddressInfo.Deleted)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            Next
            trnTransaction.Commit()
            objResponse.Status = True

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
        If _Log.IsInfoEnabled Then _Log.Info("exited")
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function
    <WebMethod()> _
    Public Function ReadList(ByVal strSupplierId As String, ByVal strAccountID As String) As AddressReadListResponse
        Dim objResponse As New AddressReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objAddressInfo As AddressInfo()
            Dim cmdCommand As New SqlCommand("usp_Address_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@AccountID", strAccountID)
            objAddressInfo = ReadAddresss(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objAddressInfo Is Nothing AndAlso objAddressInfo.GetUpperBound(0) >= 0 Then
                objResponse.Addresss = objAddressInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierId & strAccountID, ex)
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
    Public Function Sync4(ByVal strSupplierId As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstAddresss As List(Of AddressInfo)) As AddressSync4Response
        Dim objResponse As New AddressSync4Response
        Dim objTempResponse As New AddressReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            If _Log.IsInfoEnabled Then _Log.Debug("SupplierID: " & strSupplierId & " // UserID: " & strUserId)
            If _Log.IsDebugEnabled And lstAddresss IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstAddresss))

            Dim objAddressInfo As AddressInfo()
            Dim cmdCommand As New SqlCommand("usp_Address_sync4")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objAddressInfo = ReadAddresss(objDBHelper.ExecuteReader(cmdCommand))
            objTempResponse.Status = True
            If Not objAddressInfo Is Nothing AndAlso objAddressInfo.GetUpperBound(0) >= 0 Then
                objTempResponse.Addresss = objAddressInfo
            Else
                objTempResponse.Addresss = New AddressInfo() {}
            End If


            If Not lstAddresss Is Nothing Then
                Me.ModifyAll(lstAddresss)
            End If

            objResponse.Addresss = objTempResponse.Addresss
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Address)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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
        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function ReadAddresss(ByVal objReader As SqlDataReader) As AddressInfo()
        Dim objAddresss As AddressInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objAddresss(intCounter)
                    objAddresss(intCounter) = New AddressInfo
                    With objAddresss(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .AddressID = CheckString(objReader("AddressID"))
                        .unit = CheckString(objReader("unit"))
                        .Street = CheckString(objReader("Street"))
                        .PostalCode = CheckString(objReader("PostalCode"))
                        .City = CheckString(objReader("City"))
                        .Region = CheckString(objReader("Region"))
                        .Country = CheckString(objReader("Country"))
                        .Telephone = CheckString(objReader("Telephone"))
                        .Cell = CheckString(objReader("Cell"))
                        .Fax = CheckString(objReader("Fax"))
                        .WebSite = CheckString(objReader("WebSite"))
                        .Email = CheckString(objReader("Email"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objAddresss
    End Function

End Class