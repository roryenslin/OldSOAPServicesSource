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
Public Class ProductCategories
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objProductCategoryInfo As ProductCategoryInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productcategory_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductCategoryInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@CategoryID", objProductCategoryInfo.CategoryID)
            cmdCommand.Parameters.AddWithValue("@ParentCategoryID", objProductCategoryInfo.ParentCategoryID)
            cmdCommand.Parameters.AddWithValue("@Path", objProductCategoryInfo.Path)
            cmdCommand.Parameters.AddWithValue("@Name", objProductCategoryInfo.Name)
            cmdCommand.Parameters.AddWithValue("@Label", objProductCategoryInfo.Label)
            cmdCommand.Parameters.AddWithValue("@Description", objProductCategoryInfo.Description)

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
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Delete(ByVal objProductCategoryInfo As ProductCategoryInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_productcategory_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objProductCategoryInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@CategoryID", objProductCategoryInfo.CategoryID)

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
    Public Function ReadSingle(ByVal strSupplierId As String, ByVal iCategoryId As Integer) As ProductCategoryReadSingleResponse
        Dim objResponse As New ProductCategoryReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_productcategory_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@CategoryID", iCategoryId)
            Dim objProductCategorys As ProductCategoryInfo() = Nothing
            objProductCategorys = ReadProductCategorys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductCategorys Is Nothing AndAlso objProductCategorys.GetUpperBound(0) >= 0 Then
                objResponse.ProductCategory = objProductCategorys(0)
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
    Public Function ReadList(ByVal strSupplierId As String) As ProductCategoryReadListResponse
        Dim objResponse As New ProductCategoryReadListResponse
        Try
            Dim objProductCategoryInfo As ProductCategoryInfo()
            Dim cmdCommand As New SqlCommand("usp_productcategory_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            objProductCategoryInfo = ReadProductCategorys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductCategoryInfo Is Nothing AndAlso objProductCategoryInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductCategories = objProductCategoryInfo
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
    Public Function Sync2(ByVal strSupplierId As String, ByVal intVersion As Integer) As ProductCategoryReadListResponse
        Dim objResponse As New ProductCategoryReadListResponse
        Try
            Dim objProductCategoryInfo As ProductCategoryInfo()
            Dim cmdCommand As New SqlCommand("usp_productcategory_sync2")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objProductCategoryInfo = ReadProductCategorys(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductCategoryInfo Is Nothing AndAlso objProductCategoryInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductCategories = objProductCategoryInfo
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
    Public Function Sync3(ByVal strSupplierId As String, ByVal intVersion As Integer, ByVal lstProductCategory As List(Of ProductCategoryInfo)) As ProductCategorySync3Response
        Dim objResponse As New ProductCategorySync3Response
        Dim objTempResponse As New ProductCategoryReadListResponse
        Try
            objTempResponse = Sync2(strSupplierId, intVersion)

            If Not lstProductCategory Is Nothing Then
                For Each objProductCategory As ProductCategoryInfo In lstProductCategory
                    If Not objProductCategory Is Nothing Then
                        ProcessResponse(Modify(objProductCategory), objTempResponse)
                    End If
                Next
            End If

            objResponse.ProductCategories = objTempResponse.ProductCategories
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.ProductCategory)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
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

    Private Function ReadProductCategorys(ByVal objReader As SqlDataReader) As ProductCategoryInfo()
        Dim objProductCategorys As ProductCategoryInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProductCategorys(intCounter)
                    objProductCategorys(intCounter) = New ProductCategoryInfo
                    With objProductCategorys(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .CategoryID = CheckInteger(objReader("CategoryID"))
                        .ParentCategoryID = CheckInteger(objReader("ParentCategoryID"))
                        .Name = CheckString(objReader("Name"))
                        .Description = CheckString(objReader("Description"))
                        .Label = CheckString(objReader("Label"))
                        .Path = CheckString(objReader("Path"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProductCategorys
    End Function

End Class