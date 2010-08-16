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
Public Class SupplierCatalogues
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Catalogues))
    Dim objDBHelper As DBHelper


    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Modify(ByVal objSupplierCatalogueInfo As SupplierCatalogueInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(SerializationManager.Serialize(objSupplierCatalogueInfo))
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_suppliercatalogues_modify")
            cmdCommand.Parameters.AddWithValue("@CatalogueID", objSupplierCatalogueInfo.CatalogueID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objSupplierCatalogueInfo.SupplierID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objSupplierCatalogueInfo) & " modify returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows modified in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objSupplierCatalogueInfo), ex)
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
    Public Function Delete(ByVal objCatalogueInfo As SupplierCatalogueInfo) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(SerializationManager.Serialize(objCatalogueInfo))
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_suppliercatalogues_delete")
            cmdCommand.Parameters.AddWithValue("@CatalogueID", objCatalogueInfo.CatalogueID)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objCatalogueInfo.SupplierID)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objCatalogueInfo) & " delete returned " & intResult.ToString())
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows deleted in database. Error returned" + intResult.ToString()
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & SerializationManager.Serialize(objCatalogueInfo), ex)
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
    Public Function ReadList(ByVal strSupplierId As String) As CataloguesReadListResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New CataloguesReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info(strSupplierId)
            Dim objCatalogueInfo As CatalogueInfo()
            Dim cmdCommand As New SqlCommand("usp_suppliercatalogues_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)

            objCatalogueInfo = ReadCatalogues(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objCatalogueInfo Is Nothing AndAlso objCatalogueInfo.GetUpperBound(0) >= 0 Then
                objResponse.Catalogues = objCatalogueInfo
            Else
                If _Log.IsErrorEnabled Then _Log.Info("No rows readlist returned for  " & strSupplierId)
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

    Private Function ReadCatalogues(ByVal objReader As SqlDataReader) As CatalogueInfo()
        Dim objCatalogues As CatalogueInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objCatalogues(intCounter)
                    objCatalogues(intCounter) = New CatalogueInfo
                    With objCatalogues(intCounter)
                        .CatalogueID = CheckString(objReader("CatalogueID"))
                        .CatalogueName = CheckString(objReader("CatalogueName"))
                        .Deleted = CheckDeletedField(objReader)

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objCatalogues
    End Function

End Class