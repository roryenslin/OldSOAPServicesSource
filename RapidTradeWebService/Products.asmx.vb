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
Public Class Products
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(UserAccounts))
    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
        Public Function Modify(ByVal lstProductInfo As List(Of ProductInfo)) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Dim trnTransaction As SqlTransaction = Nothing
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            
            For Each objProductInfo As ProductInfo In lstProductInfo
                Dim cmdCommand As New SqlCommand("usp_product_modify", conConnection)
                cmdCommand.Transaction = trnTransaction

                cmdCommand.Parameters.AddWithValue("@SupplierID", objProductInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objProductInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@Description", objProductInfo.Description)
                cmdCommand.Parameters.AddWithValue("@VAT", objProductInfo.VAT)
                cmdCommand.Parameters.AddWithValue("@Barcode", objProductInfo.Barcode)
                cmdCommand.Parameters.AddWithValue("@Unit", objProductInfo.Unit)
                cmdCommand.Parameters.AddWithValue("@CategoryName", objProductInfo.CategoryName)
                cmdCommand.Parameters.AddWithValue("@OptionalProducts", objProductInfo.OptionalProducts)
                cmdCommand.Parameters.AddWithValue("@Components", objProductInfo.Components)
                cmdCommand.Parameters.AddWithValue("@SimilarProducts", objProductInfo.SimilarProducts)
                cmdCommand.Parameters.AddWithValue("@WebSite", objProductInfo.WebSite)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint1", objProductInfo.DeliveryConstraint1)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint2", objProductInfo.DeliveryConstraint2)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint3", objProductInfo.DeliveryConstraint3)
                cmdCommand.Parameters.AddWithValue("@DeliveryConstraint4", objProductInfo.DeliveryConstraint4)
                cmdCommand.Parameters.AddWithValue("@DisplayFields", objProductInfo.DisplayFields)
                cmdCommand.Parameters.AddWithValue("@UserField01", objProductInfo.UserField01)
                cmdCommand.Parameters.AddWithValue("@UserField02", objProductInfo.UserField02)
                cmdCommand.Parameters.AddWithValue("@UserField03", objProductInfo.UserField03)
                cmdCommand.Parameters.AddWithValue("@UserField04", objProductInfo.UserField04)
                cmdCommand.Parameters.AddWithValue("@UserField05", objProductInfo.UserField05)
                cmdCommand.Parameters.AddWithValue("@UserField06", objProductInfo.UserField06)
                cmdCommand.Parameters.AddWithValue("@UserField07", objProductInfo.UserField07)
                cmdCommand.Parameters.AddWithValue("@UserField08", objProductInfo.UserField08)
                cmdCommand.Parameters.AddWithValue("@UserField09", objProductInfo.UserField09)
                cmdCommand.Parameters.AddWithValue("@UserField10", objProductInfo.UserField10)
                cmdCommand.Parameters.AddWithValue("@ImageUrlLarge", objProductInfo.ImageUrlLarge)
                cmdCommand.Parameters.AddWithValue("@ImageUrlSmall", objProductInfo.ImageUrlSmall)

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

    <WebMethod()> _
    Public Function Delete(ByVal objProductInfo As ProductInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_product_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@ProductID", objProductInfo.ProductID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ProductReadListResponse
        Dim objResponse As New ProductReadListResponse
        Try
            Dim objProductInfo As ProductInfo()
            Dim cmdCommand As New SqlCommand("usp_product_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objProductInfo = ReadProducts(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductInfo Is Nothing AndAlso objProductInfo.GetUpperBound(0) >= 0 Then
                objResponse.Products = objProductInfo
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
Public Function Test(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer) As String()
        Dim resultarray As New Generic.List(Of String)
        Dim br As ProductSync3Response = Sync3(strSupplierID, intVersion, Nothing)
        If br.Status Then
            resultarray.Add("Sync4------> True ")
            resultarray.Add("count: " & br.Products.Length)
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstProducts As List(Of ProductInfo)) As ProductSync3Response
        Dim objResponse As New ProductSync3Response
        Dim objTempResponse As New ProductReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstProducts Is Nothing Then
                ProcessResponse(Modify(lstProducts), objTempResponse)
            End If

            objResponse.Products = objTempResponse.Products
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Product)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

            If _Log.IsDebugEnabled Then _Log.Info(strSupplierId & ":" & objResponse.Products.Length & ":" & objResponse.Status & ":" & objResponse.LastVersion)
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Info(ex.Message)
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

    Private Function ReadProducts(ByVal objReader As SqlDataReader) As ProductInfo()
        Dim objProducts As ProductInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProducts(intCounter)
                    objProducts(intCounter) = New ProductInfo
                    With objProducts(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .Description = CheckString(objReader("Description"))
                        .VAT = CheckDecimal(objReader("VAT"))
                        .Barcode = CheckString(objReader("Barcode"))
                        .Unit = CheckString(objReader("Unit"))
                        .CategoryName = CheckString(objReader("CategoryName"))
                        .OptionalProducts = CheckString(objReader("OptionalProducts"))
                        .Components = CheckString(objReader("Components"))
                        .SimilarProducts = CheckString(objReader("SimilarProducts"))
                        .WebSite = CheckString(objReader("WebSite"))
                        .DeliveryConstraint1 = CheckString(objReader("DeliveryConstraint1"))
                        .DeliveryConstraint2 = CheckString(objReader("DeliveryConstraint2"))
                        .DeliveryConstraint3 = CheckString(objReader("DeliveryConstraint3"))
                        .DeliveryConstraint4 = CheckString(objReader("DeliveryConstraint4"))
                        .DisplayFields = CheckString(objReader("DisplayFields"))
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
                        .ImageUrlLarge = CheckString(objReader("ImageUrlLarge"))
                        .ImageUrlSmall = CheckString(objReader("ImageUrlSmall"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProducts
    End Function
End Class