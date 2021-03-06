﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response
Imports System.IO


<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ProductImages
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(ProductImages))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
        Public Function Modify(ByVal lstProductImageInfo As List(Of ProductImageInfo)) As BaseResponse
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

            For Each objProductImageInfo In lstProductImageInfo

                Dim cmdCommand As New SqlCommand("usp_productimages_modify", conConnection)
                cmdCommand.Transaction = trnTransaction
                cmdCommand.Parameters.AddWithValue("@SupplierID", objProductImageInfo.SupplierID)
                cmdCommand.Parameters.AddWithValue("@ProductID", objProductImageInfo.ProductID)
                cmdCommand.Parameters.AddWithValue("@ImageName", objProductImageInfo.ImageName)
                cmdCommand.Parameters.AddWithValue("@Deleted", objProductImageInfo.Deleted)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            Next

            objResponse.Status = True
            trnTransaction.Commit()
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Error", ex)
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
    Public Function ReadNewImages(ByVal supplierID As String, ByVal lastDate As String) As String()
        Dim imageFolder As String = ConfigurationManager.AppSettings("ImageFolder")
        Dim searchFolder As String = Path.Combine(imageFolder, supplierID)
        Dim dtlastdate As Date = Activities2.ConvertDate(lastDate)
        Dim sfiles As New List(Of String)

        For Each sfile As String In System.IO.Directory.GetFiles(searchFolder)
            Dim tm As Date = System.IO.File.GetLastWriteTime(sfile)
            If tm >= dtlastdate And sfile.ToUpper.Contains("JPG") Then
                sfiles.Add(sfile.Substring(sfile.LastIndexOf("\") + 1))
            End If
        Next
        Return sfiles.ToArray
    End Function

    <WebMethod()> _
        Public Function ReadList(ByVal strSupplierId As String, ByVal strProductId As String) As ProductImagesReadListResponse
        Dim objResponse As New ProductImagesReadListResponse
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim objProductImagesInfo As ProductImageInfo()
            Dim cmdCommand As New SqlCommand("usp_ProductImages_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@ProductID", strProductId)

            objProductImagesInfo = ReadProductImages(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objProductImagesInfo Is Nothing AndAlso objProductImagesInfo.GetUpperBound(0) >= 0 Then
                objResponse.ProductImages = objProductImagesInfo
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

    Private Function ReadProductImages(ByVal objReader As SqlDataReader) As ProductImageInfo()
        Dim objProductImages As ProductImageInfo() = Nothing
        Dim intCounter As Integer = 0

        Try

            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objProductImages(intCounter)
                    objProductImages(intCounter) = New ProductImageInfo
                    With objProductImages(intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .ProductID = CheckString(objReader("ProductID"))
                        .ImageName = CheckString(objReader("ImageName"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objProductImages
    End Function

End Class