Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports RapidTradeWebService.Entity
Imports RapidTradeWebService.DataAccess
Imports RapidTradeWebService.Common
Imports RapidTradeWebService.Response

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Suppliers
    Inherits System.Web.Services.WebService

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objSupplierInfo As SupplierInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_supplier_add")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objSupplierInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Name", objSupplierInfo.Name)
            cmdCommand.Parameters.AddWithValue("@DefaultVAT", objSupplierInfo.DefaultVAT)
            cmdCommand.Parameters.AddWithValue("@DefaultPriceList", objSupplierInfo.DefaultPriceList)
            cmdCommand.Parameters.AddWithValue("@AddressID", objSupplierInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@CurrencyText", objSupplierInfo.CurrencyText)
            cmdCommand.Parameters.AddWithValue("@DontShowLogo", objSupplierInfo.DontShowLogo)
            cmdCommand.Parameters.AddWithValue("@UseCatalogues", objSupplierInfo.UseCatalogues)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows inserted in database. Error returned" + intResult.ToString()
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
    Public Function Change(ByVal objSupplierInfo As SupplierInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_supplier_change")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objSupplierInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Name", objSupplierInfo.Name)
            cmdCommand.Parameters.AddWithValue("@DefaultVAT", objSupplierInfo.DefaultVAT)
            cmdCommand.Parameters.AddWithValue("@DefaultPriceList", objSupplierInfo.DefaultPriceList)
            cmdCommand.Parameters.AddWithValue("@AddressID", objSupplierInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@CurrencyText", objSupplierInfo.CurrencyText)
            cmdCommand.Parameters.AddWithValue("@DontShowLogo", objSupplierInfo.DontShowLogo)
            cmdCommand.Parameters.AddWithValue("@UseCatalogues", objSupplierInfo.UseCatalogues)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue
            objDBHelper.ExecuteNonQuery(cmdCommand)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows updated in database. Error returned" + intResult.ToString()
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
    Public Function Modify(ByVal objSupplierInfo As SupplierInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_supplier_modify")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objSupplierInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@Name", objSupplierInfo.Name)
            cmdCommand.Parameters.AddWithValue("@DefaultVAT", objSupplierInfo.DefaultVAT)
            cmdCommand.Parameters.AddWithValue("@DefaultPriceList", objSupplierInfo.DefaultPriceList)
            cmdCommand.Parameters.AddWithValue("@AddressID", objSupplierInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@CurrencyText", objSupplierInfo.CurrencyText)
            cmdCommand.Parameters.AddWithValue("@DontShowLogo", objSupplierInfo.DontShowLogo)
            cmdCommand.Parameters.AddWithValue("@UseCatalogues", objSupplierInfo.UseCatalogues)

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
    Public Function Delete(ByVal objSupplierInfo As SupplierInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_supplier_delete")
            cmdCommand.Parameters.AddWithValue("@SupplierID", objSupplierInfo.SupplierID)

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
    Public Function ReadSingle(ByVal strSupplierId As String) As SupplierReadSingleResponse
        Dim objResponse As New SupplierReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_supplier_readsingle")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierId)

            Dim objSuppliers As SupplierInfo = Nothing
            objSuppliers = ReadSuppliers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objSuppliers Is Nothing Then
                objResponse.Supplier = objSuppliers
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

    Private Function ReadSuppliers(ByVal objReader As SqlDataReader) As SupplierInfo
        Dim objSuppliers As SupplierInfo = Nothing
        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                If (objReader.Read()) Then
                    objSuppliers = New SupplierInfo
                    With objSuppliers
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .Name = CheckString(objReader("Name"))
                        .DefaultVAT = CheckDecimal(objReader("DefaultVAT"))
                        .DefaultPriceList = CheckString(objReader("DefaultPriceList"))
                        .AddressID = CheckInteger(objReader("AddressID"))
                        .CurrencyText = CheckString(objReader("CurrencyText"))
                        .DontShowLogo = CheckBoolean(objReader("DontShowLogo"))
                        .UseCatalogues = CheckBoolean(objReader("UseCatalogues"))
                    End With
                End If
            End If
        Finally
            objReader.Close()
        End Try
        Return objSuppliers
    End Function

End Class