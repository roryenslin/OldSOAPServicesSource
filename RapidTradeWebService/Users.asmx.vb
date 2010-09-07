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
Public Class Users
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Users))

    Dim objDBHelper As DBHelper

    Public Sub New()
        objDBHelper = New DBHelper
    End Sub

    <WebMethod()> _
    Public Function Add(ByVal objUserInfo As UserInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim iCounter As Integer = 0
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_user_add")
            cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@PasswordHash", objUserInfo.Password)
            cmdCommand.Parameters.AddWithValue("@PasswordSalt", System.DBNull.Value)
            cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Name)
            cmdCommand.Parameters.AddWithValue("@AddressID", objUserInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@Email", objUserInfo.Email)
            cmdCommand.Parameters.AddWithValue("@Country", objUserInfo.Country)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RepID", objUserInfo.RepID)
            cmdCommand.Parameters.AddWithValue("@Manager", objUserInfo.Manager)
            cmdCommand.Parameters.AddWithValue("@IsAdmin", objUserInfo.IsAdmin)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No User rows inserted in database. Error returned" + intResult.ToString()
                Exit Try
            End If

            'Commented out as part of Roles removal from UserInfo
            ''Insert into the Roles table.
            'For iCounter = 0 To objUserInfo.Roles.GetUpperBound(0)
            '    cmdCommand.CommandText = "usp_role_add"
            '    cmdCommand.Parameters.Clear()
            '    cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Roles(iCounter).SupplierID)
            '    cmdCommand.Parameters.AddWithValue("@RoleID", objUserInfo.Roles(iCounter).RoleId)
            '    cmdCommand.Parameters.AddWithValue("@RoleName", objUserInfo.Roles(iCounter).Name)
            '    cmdCommand.Parameters.AddWithValue("@Description", objUserInfo.Roles(iCounter).Description)
            '    cmdCommand.Parameters.AddWithValue("@CreatedDate", VerifyDate(objUserInfo.Roles(iCounter).CreatedDate))

            '    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            '    oReturnParam.Direction = ParameterDirection.ReturnValue
            '    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            '    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            '    If intResult <> 0 Then
            '        ReDim objResponse.Errors(0)
            '        objResponse.Errors(0) = String.Format("Failed to insert Role: {0}", objUserInfo.Roles(iCounter).RoleId)
            '        Exit Try
            '    End If

            '    'Insert into UserRoles table
            '    cmdCommand.CommandText = "usp_userroles_add"
            '    cmdCommand.Parameters.Clear()
            '    cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Roles(iCounter).SupplierID)
            '    cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            '    cmdCommand.Parameters.AddWithValue("@RoleID", objUserInfo.Roles(iCounter).RoleId)

            '    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            '    oReturnParam.Direction = ParameterDirection.ReturnValue

            '    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

            '    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            '    If intResult <> 0 Then
            '        objResponse.Status = False
            '        ReDim Preserve objResponse.Errors(0)
            '        objResponse.Errors(0) = String.Format("No User roles inserted in database. User Id: {0} Role Id: {1}", objUserInfo.UserID, objUserInfo.Roles(iCounter).RoleId)
            '        Exit Try
            '    End If
            'Next

            'Insert into Accounts table
            For iCounter = 0 To objUserInfo.Accounts.GetUpperBound(0)
                cmdCommand.CommandText = "usp_account_add"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Accounts(iCounter).SupplierID)
                cmdCommand.Parameters.AddWithValue("@AccountID", objUserInfo.Accounts(iCounter).AccountID)
                cmdCommand.Parameters.AddWithValue("@BranchID", objUserInfo.Accounts(iCounter).Branch)
                cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Accounts(iCounter).Name)
                cmdCommand.Parameters.AddWithValue("@VAT", objUserInfo.Accounts(iCounter).VAT)
                cmdCommand.Parameters.AddWithValue("@Pricelist", objUserInfo.Accounts(iCounter).PriceList)
                cmdCommand.Parameters.AddWithValue("@AccountGroup", objUserInfo.Accounts(iCounter).AccountGroup)
                If Not objUserInfo.Accounts(iCounter).UserFields Is Nothing AndAlso objUserInfo.Accounts(iCounter).UserFields.Length > 0 Then
                    Dim intCounter As Integer
                    For intCounter = 0 To objUserInfo.Accounts(iCounter).UserFields.GetUpperBound(0)
                        cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objUserInfo.Accounts(iCounter).UserFields(intCounter))
                    Next
                End If

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to insert Account: {0}", objUserInfo.Accounts(iCounter).AccountID)
                    Exit Try
                End If

                'Insert into UserAccounts table
                cmdCommand.CommandText = "usp_useraccounts_add"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
                cmdCommand.Parameters.AddWithValue("@AccountID", objUserInfo.Accounts(iCounter).AccountID)
                cmdCommand.Parameters.AddWithValue("@BranchID", objUserInfo.Accounts(iCounter).Branch)
                cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Accounts(iCounter).SupplierID)

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue

                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim Preserve objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("No User Accounts inserted in database. User Id: {0} Account Id: {1}", objUserInfo.UserID, objUserInfo.Accounts(iCounter).AccountID)
                    Exit Try
                End If
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
    Public Function Change(ByVal objUserInfo As UserInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_user_change")
            cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@PasswordHash", objUserInfo.Password)
            cmdCommand.Parameters.AddWithValue("@PasswordSalt", System.DBNull.Value)
            cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Name)
            cmdCommand.Parameters.AddWithValue("@AddressID", objUserInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@Email", objUserInfo.Email)
            cmdCommand.Parameters.AddWithValue("@Country", objUserInfo.Country)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RepID", objUserInfo.RepID)
            cmdCommand.Parameters.AddWithValue("@Manager", objUserInfo.Manager)
            cmdCommand.Parameters.AddWithValue("@IsAdmin", objUserInfo.IsAdmin)
            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No rows updated in database. Error returned" + intResult.ToString()
            End If

            'Commented out as part of Roles removal from UserInfo
            ''Insert into the Roles table.
            'For iCounter = 0 To objUserInfo.Roles.GetUpperBound(0)
            '    cmdCommand.CommandText = "usp_role_change2"
            '    cmdCommand.Parameters.Clear()
            '    cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            '    cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Roles(iCounter).SupplierID)
            '    cmdCommand.Parameters.AddWithValue("@RoleID", objUserInfo.Roles(iCounter).RoleId)
            '    cmdCommand.Parameters.AddWithValue("@RoleName", objUserInfo.Roles(iCounter).Name)
            '    cmdCommand.Parameters.AddWithValue("@Description", objUserInfo.Roles(iCounter).Description)
            '    cmdCommand.Parameters.AddWithValue("@ChangedDate", VerifyDate(objUserInfo.Roles(iCounter).ChangedDate))

            '    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            '    oReturnParam.Direction = ParameterDirection.ReturnValue
            '    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            '    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            '    If intResult <> 0 Then
            '        ReDim objResponse.Errors(0)
            '        objResponse.Errors(0) = String.Format("Failed to update Role: {0}", objUserInfo.Roles(iCounter).RoleId)
            '        Exit Try
            '    End If
            'Next

            'Update Accounts table
            For iCounter = 0 To objUserInfo.Accounts.GetUpperBound(0)
                cmdCommand.CommandText = "usp_account_change2"
                cmdCommand.Parameters.Clear()
                cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
                cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Accounts(iCounter).SupplierID)
                cmdCommand.Parameters.AddWithValue("@AccountID", objUserInfo.Accounts(iCounter).AccountID)
                cmdCommand.Parameters.AddWithValue("@BranchID", objUserInfo.Accounts(iCounter).Branch)
                cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Accounts(iCounter).Name)
                cmdCommand.Parameters.AddWithValue("@VAT", objUserInfo.Accounts(iCounter).VAT)
                cmdCommand.Parameters.AddWithValue("@Pricelist", objUserInfo.Accounts(iCounter).PriceList)
                cmdCommand.Parameters.AddWithValue("@AccountGroup", objUserInfo.Accounts(iCounter).AccountGroup)
                If Not objUserInfo.Accounts(iCounter).UserFields Is Nothing AndAlso objUserInfo.Accounts(iCounter).UserFields.Length > 0 Then
                    Dim intCounter As Integer
                    For intCounter = 0 To objUserInfo.Accounts(iCounter).UserFields.GetUpperBound(0)
                        cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objUserInfo.Accounts(iCounter).UserFields(intCounter))
                    Next
                End If

                oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                oReturnParam.Direction = ParameterDirection.ReturnValue
                objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                If intResult <> 0 Then
                    objResponse.Status = False
                    ReDim objResponse.Errors(0)
                    objResponse.Errors(0) = String.Format("Failed to update Account: {0}", objUserInfo.Accounts(iCounter).AccountID)
                    Exit Try
                End If
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
    Public Function Modify(ByVal objUserInfo As UserInfo) As BaseResponse
        Dim objResponse As New BaseResponse
        Dim trnTransaction As SqlTransaction = Nothing
        Dim conConnection As SqlConnection = Nothing
        Try
            If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
            Dim iCounter As Integer = 0
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_user_modify")
            cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            cmdCommand.Parameters.AddWithValue("@PasswordHash", objUserInfo.Password)
            cmdCommand.Parameters.AddWithValue("@PasswordSalt", System.DBNull.Value)
            cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Name)
            cmdCommand.Parameters.AddWithValue("@AddressID", objUserInfo.AddressID)
            cmdCommand.Parameters.AddWithValue("@Email", objUserInfo.Email)
            cmdCommand.Parameters.AddWithValue("@Country", objUserInfo.Country)
            cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.SupplierID)
            cmdCommand.Parameters.AddWithValue("@RepID", objUserInfo.RepID)
            cmdCommand.Parameters.AddWithValue("@Manager", objUserInfo.Manager)
            cmdCommand.Parameters.AddWithValue("@IsAdmin", objUserInfo.IsAdmin)

            oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            oReturnParam.Direction = ParameterDirection.ReturnValue

            conConnection = objDBHelper.GetConnection
            conConnection.Open()
            trnTransaction = conConnection.BeginTransaction
            cmdCommand.Transaction = trnTransaction

            objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)

            intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)
            objResponse.Status = intResult = 0
            If Not objResponse.Status Then
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "No User rows modified in database. Error returned" + intResult.ToString()
                Exit Try
            End If

            'Commented out as part of Roles removal from UserInfo
            ''Insert into the Roles table.
            'If Not objUserInfo.Roles Is Nothing Then
            '    For iCounter = 0 To objUserInfo.Roles.GetUpperBound(0)
            '        If objUserInfo.Roles(iCounter) Is Nothing Then
            '            Continue For
            '        End If
            '        cmdCommand.CommandText = "usp_role_modify2"
            '        cmdCommand.Parameters.Clear()
            '        cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Roles(iCounter).SupplierID)
            '        cmdCommand.Parameters.AddWithValue("@RoleID", objUserInfo.Roles(iCounter).RoleId)
            '        cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
            '        cmdCommand.Parameters.AddWithValue("@RoleName", objUserInfo.Roles(iCounter).Name)
            '        cmdCommand.Parameters.AddWithValue("@Description", objUserInfo.Roles(iCounter).Description)
            '        cmdCommand.Parameters.AddWithValue("@CreatedDate", VerifyDate(objUserInfo.Roles(iCounter).CreatedDate))
            '        cmdCommand.Parameters.AddWithValue("@ChangedDate", VerifyDate(objUserInfo.Roles(iCounter).ChangedDate))

            '        oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
            '        oReturnParam.Direction = ParameterDirection.ReturnValue
            '        objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
            '        intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

            '        If intResult <> 0 Then
            '            ReDim objResponse.Errors(0)
            '            objResponse.Errors(0) = String.Format("Failed to modify Role: {0}", objUserInfo.Roles(iCounter).RoleId)
            '            Exit Try
            '        End If
            '    Next
            'End If

            'Insert into Accounts table
            If Not objUserInfo.Accounts Is Nothing Then
                For iCounter = 0 To objUserInfo.Accounts.GetUpperBound(0)
                    If objUserInfo.Accounts(iCounter) Is Nothing Then
                        Continue For
                    End If
                    cmdCommand.CommandText = "usp_account_modify2"
                    cmdCommand.Parameters.Clear()
                    cmdCommand.Parameters.AddWithValue("@SupplierID", objUserInfo.Accounts(iCounter).SupplierID)
                    cmdCommand.Parameters.AddWithValue("@AccountID", objUserInfo.Accounts(iCounter).AccountID)
                    cmdCommand.Parameters.AddWithValue("@BranchID", objUserInfo.Accounts(iCounter).Branch)
                    cmdCommand.Parameters.AddWithValue("@UserID", objUserInfo.UserID)
                    cmdCommand.Parameters.AddWithValue("@Name", objUserInfo.Accounts(iCounter).Name)
                    cmdCommand.Parameters.AddWithValue("@VAT", objUserInfo.Accounts(iCounter).VAT)
                    cmdCommand.Parameters.AddWithValue("@Pricelist", objUserInfo.Accounts(iCounter).PriceList)
                    cmdCommand.Parameters.AddWithValue("@AccountGroup", objUserInfo.Accounts(iCounter).AccountGroup)
                    If Not objUserInfo.Accounts(iCounter).UserFields Is Nothing AndAlso objUserInfo.Accounts(iCounter).UserFields.Length > 0 Then
                        Dim intCounter As Integer
                        For intCounter = 0 To objUserInfo.Accounts(iCounter).UserFields.GetUpperBound(0)
                            cmdCommand.Parameters.AddWithValue("@Userfield" + (intCounter + 1).ToString().PadLeft(2, "0"c), objUserInfo.Accounts(iCounter).UserFields(intCounter))
                        Next
                    End If

                    oReturnParam = cmdCommand.Parameters.Add("@ReturnValue", SqlDbType.Int)
                    oReturnParam.Direction = ParameterDirection.ReturnValue
                    objDBHelper.ExecuteNonQuery(cmdCommand, conConnection)
                    intResult = CType(cmdCommand.Parameters("@ReturnValue").Value, Integer)

                    If intResult <> 0 Then
                        objResponse.Status = False
                        ReDim objResponse.Errors(0)
                        objResponse.Errors(0) = String.Format("Failed to modify Account: {0}", objUserInfo.Accounts(iCounter).AccountID)
                        Exit Try
                    End If
                Next
            End If

            trnTransaction.Commit()
            objResponse.Status = True

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(RapidTradeWebService.Common.SerializationManager.Serialize(objUserInfo), ex)
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
    Public Function Delete(ByVal objUserInfo As UserInfo) As BaseResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New BaseResponse
        Try
            Dim intResult As Integer
            Dim oReturnParam As SqlParameter
            Dim cmdCommand As New SqlCommand("usp_user_delete")
            cmdCommand.Parameters.AddWithValue("@UserId", objUserInfo.UserID)
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
    Public Function ReadSingle(ByVal strUserId As String) As UserReadSingleResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserReadSingleResponse
        Try
            Dim cmdCommand As New SqlCommand("usp_user_readsingle")
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            Dim objUsers As UserInfo() = Nothing
            objUsers = ReadUsers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUsers Is Nothing AndAlso objUsers.GetUpperBound(0) >= 0 Then
                objResponse.User = objUsers(0)
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strUserId, ex)
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
    Public Function ReadList(ByVal strSupplierId As String) As UserReadListResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserReadListResponse
        Try
            Dim objUserInfo As UserInfo()
            Dim cmdCommand As New SqlCommand("usp_user_readlist")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            objUserInfo = ReadUsers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserInfo Is Nothing AndAlso objUserInfo.GetUpperBound(0) >= 0 Then
                objResponse.Users = objUserInfo
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

    <WebMethod()> _
    Public Function ReadStaff(ByVal strSupplierId As String, ByVal strUserId As String) As UserStaffResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserStaffResponse
        Dim objReader As SqlDataReader = Nothing
        Try
            Dim lstStaff As New List(Of String)
            Dim cmdCommand As New SqlCommand("usp_user_readstaff")
            cmdCommand.Parameters.AddWithValue("@SupplierId", strSupplierId)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            objReader = objDBHelper.ExecuteReader(cmdCommand)
            While objReader.Read()
                If Not objReader("UserId") Is Nothing Then
                    lstStaff.Add(objReader("UserId").ToString())
                End If
            End While
            objResponse.Status = True
            If lstStaff.Count > 0 Then
                objResponse.Staff = lstStaff
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
        Finally
            If Not objReader Is Nothing Then
                objReader.Close()
            End If
        End Try
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync(ByVal strUserId As String, ByVal intVersion As Integer) As UserReadSingleResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserReadSingleResponse
        Try
            Dim objUserInfo As UserInfo()
            Dim cmdCommand As New SqlCommand("usp_user_sync")
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            If intVersion = -2 Then
                cmdCommand.Parameters.AddWithValue("@Version", 0)
            Else
                cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            End If
            objUserInfo = ReadUsers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserInfo Is Nothing AndAlso objUserInfo.GetUpperBound(0) >= 0 Then
                objResponse.User = objUserInfo(0)
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
    Public Function Sync2(ByVal intVersion As Integer) As UserReadListResponse
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserReadListResponse
        Try
            Dim objUserInfo As UserInfo()
            Dim cmdCommand As New SqlCommand("usp_user_sync2")
            If intVersion = -2 Then
                cmdCommand.Parameters.AddWithValue("@Version", 0)
            Else
                cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            End If
            'cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objUserInfo = ReadUsers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not objUserInfo Is Nothing AndAlso objUserInfo.GetUpperBound(0) >= 0 Then
                'Test for BlackBerry
                If intVersion = -2 Then
                    objResponse.Users = New UserInfo() {objUserInfo(2)}
                Else
                    objResponse.Users = objUserInfo
                End If
                'objResponse.Users = objUserInfo
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception ", ex)
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
    Public Function Sync3(ByVal intVersion As Integer, ByVal lstUsers As List(Of UserInfo)) As UserSync3Response
        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        Dim objResponse As New UserSync3Response
        Dim objTempResponse As New UserReadListResponse
        Try
            If _Log.IsDebugEnabled Then _Log.Debug("Version: " & intVersion)
            If _Log.IsDebugEnabled And lstUsers IsNot Nothing Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(lstUsers))

            objTempResponse = Sync2(intVersion)

            If Not lstUsers Is Nothing Then
                For Each objUser As UserInfo In lstUsers
                    If Not objUser Is Nothing Then
                        ProcessResponse(Modify(objUser), objTempResponse)
                    End If
                Next
            End If

            objResponse.Users = objTempResponse.Users
            objResponse.Errors = objTempResponse.Errors
            objResponse.Status = objTempResponse.Status
            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Users)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for ", ex)
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
        If _Log.IsDebugEnabled Then _Log.Debug("exited")
        Return objResponse
    End Function

    <WebMethod()> _
    Public Function Sync4(ByVal strSupplierID As String, ByVal strUserId As String, ByVal intVersion As Integer, ByVal lstUsers As List(Of UserInfo)) As UserSync3Response
        Dim objResponse As New UserSync3Response
        Dim objReader As SqlDataReader = Nothing
        Dim objTempResponse As New UserReadListResponse

        If _Log.IsInfoEnabled Then _Log.Info("Entered----------->")
        If _Log.IsInfoEnabled Then _Log.Info("UserID: " & strUserId & " // Version: " & intVersion)

        Try
            Dim cmdCommand As New SqlCommand("usp_user_sync4")
            cmdCommand.Parameters.AddWithValue("@SupplierID", strSupplierID)
            cmdCommand.Parameters.AddWithValue("@UserId", strUserId)
            cmdCommand.Parameters.AddWithValue("@Version", intVersion)
            objResponse.Users = ReadUsers(objDBHelper.ExecuteReader(cmdCommand))
            objResponse.Status = True
            If Not lstUsers Is Nothing Then
                For Each objUser As UserInfo In lstUsers
                    If Not objUser Is Nothing Then
                        ProcessResponse(Modify(objUser), objTempResponse)
                    End If
                Next
                'objResponse.Users = objTempResponse.Users
                objResponse.Errors = objTempResponse.Errors
                objResponse.Status = objTempResponse.Status
            End If

            Dim objTableVersionResponse As TableVersionResponse = New Tables().GetTableVersion(TableNames.Users)
            If objTableVersionResponse.Status Then
                objResponse.LastVersion = objTableVersionResponse.TableVersion
            Else
                ProcessResponse(objTableVersionResponse, objResponse)
            End If

        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error("Exception for " & strSupplierID & strUserId, ex)
            objResponse.Status = False
            Dim intCounter As Integer = 0
            While Not ex Is Nothing
                ReDim Preserve objResponse.Errors(intCounter)
                objResponse.Errors(intCounter) = ex.Message
                ex = ex.InnerException
                intCounter = intCounter + 1
            End While
        Finally
            If Not objReader Is Nothing Then
                objReader.Close()
            End If
        End Try
        Return objResponse

        If _Log.IsDebugEnabled Then _Log.Debug(RapidTradeWebService.Common.SerializationManager.Serialize(objResponse))
        Return objResponse
    End Function

    Private Function ReadUsers(ByVal objReader As SqlDataReader) As UserInfo()
        Dim objUsers As UserInfo() = Nothing
        Dim intCounter As Integer = 0
        Dim objHash As New Hashtable
        Dim strUserId As String = String.Empty
        Dim objTempUser As UserInfo = Nothing

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objUsers(intCounter)
                    objUsers(intCounter) = New UserInfo
                    With objUsers(intCounter)
                        If IsNumeric(objReader("AddressID")) Then
                            .AddressID = CType(objReader("AddressID"), Integer)
                        End If
                        .Country = CheckString(objReader("Country"))
                        .Email = CheckString(objReader("Email"))
                        .Name = CheckString(objReader("Name"))
                        .Password = CheckString(objReader("PasswordHash"))
                        .UserID = CheckString(objReader("UserId"))
                        objHash.Add(Trim(.UserID), intCounter)
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .RepID = CheckString(objReader("RepID"))
                        .Manager = CheckString(objReader("Manager"))
                        .IsAdmin = CheckBoolean(objReader("IsAdmin"))
                        .Deleted = CheckDeletedField(objReader)
                    End With
                    intCounter = intCounter + 1
                End While

                'Commented out as part of Roles removal from UserInfo
                'If objReader.NextResult() Then
                '    While (objReader.Read())
                '        strUserId = Trim(CheckString(objReader("UserId")))
                '        If objHash.ContainsKey(strUserId) Then
                '            objTempUser = objUsers(CInt(objHash(strUserId)))
                '            If objTempUser.Roles Is Nothing OrElse objTempUser.Roles.Length = 0 Then
                '                ReDim objTempUser.Roles(0)
                '            Else
                '                ReDim Preserve objTempUser.Roles(objTempUser.Roles.Length)
                '            End If
                '            objTempUser.Roles(objTempUser.Roles.GetUpperBound(0)) = New RoleInfo
                '            With objTempUser.Roles(objTempUser.Roles.GetUpperBound(0))
                '                .SupplierID = CheckString(objReader("SupplierId"))
                '                .RoleId = CheckString(objReader("RoleId"))
                '                .Name = CheckString(objReader("RoleName"))
                '                .Description = CheckString(objReader("Description"))
                '                If Not objReader("CreatedDate") Is Nothing AndAlso Not IsDBNull(objReader("CreatedDate")) Then
                '                    .CreatedDate = CType(objReader("CreatedDate"), DateTime)
                '                End If
                '                If Not objReader("ChangedDate") Is Nothing AndAlso Not IsDBNull(objReader("ChangedDate")) Then
                '                    .ChangedDate = CType(objReader("ChangedDate"), DateTime)
                '                End If
                '            End With
                '        End If
                '    End While
                'End If

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strUserId = Trim(CheckString(objReader("UserId")))
                        If objHash.ContainsKey(strUserId) Then
                            objTempUser = objUsers(CInt(objHash(strUserId)))
                            If objTempUser.Accounts Is Nothing OrElse objTempUser.Accounts.Length = 0 Then
                                ReDim objTempUser.Accounts(0)
                            Else
                                ReDim Preserve objTempUser.Accounts(objTempUser.Accounts.Length)
                            End If
                            objTempUser.Accounts(objTempUser.Accounts.GetUpperBound(0)) = New AccountInfo
                            With objTempUser.Accounts(objTempUser.Accounts.GetUpperBound(0))
                                .SupplierID = CheckString(objReader("SupplierId"))
                                .AccountGroup = CheckString(objReader("AccountGroup"))
                                .AccountID = CheckString(objReader("AccountID"))
                                .Branch = CheckString(objReader("BranchID"))
                                .Name = CheckString(objReader("Name"))
                                .PriceList = CheckString(objReader("Pricelist"))
                                If IsNumeric(objReader("VAT")) Then
                                    .VAT = CType(objReader("VAT"), Integer)
                                End If
                                ReDim .UserFields(9)
                                .UserFields(0) = CheckString(objReader("Userfield01"))
                                .UserFields(1) = CheckString(objReader("Userfield02"))
                                .UserFields(2) = CheckString(objReader("Userfield03"))
                                .UserFields(3) = CheckString(objReader("Userfield04"))
                                .UserFields(4) = CheckString(objReader("Userfield05"))
                                .UserFields(5) = CheckString(objReader("Userfield06"))
                                .UserFields(6) = CheckString(objReader("Userfield07"))
                                .UserFields(7) = CheckString(objReader("Userfield08"))
                                .UserFields(8) = CheckString(objReader("Userfield09"))
                                .UserFields(9) = CheckString(objReader("Userfield10"))
                            End With
                        End If
                    End While
                End If

                If objReader.NextResult() Then
                    While (objReader.Read())
                        strUserId = Trim(CheckString(objReader("Manager")))
                        If objHash.ContainsKey(strUserId) Then
                            objTempUser = objUsers(CInt(objHash(strUserId)))
                            'If objTempUser.Staff Is Nothing OrElse objTempUser.Staff.Length = 0 Then
                            '    ReDim objTempUser.Staff(0)
                            'Else
                            '    ReDim Preserve objTempUser.Staff(objTempUser.Staff.Length)
                            'End If
                            'objTempUser.Staff(objTempUser.Staff.GetUpperBound(0)) = New UserInfo
                            'With objTempUser.Staff(objTempUser.Staff.GetUpperBound(0))
                            '    If IsNumeric(objReader("AddressID")) Then
                            '        .AddressID = CType(objReader("AddressID"), Integer)
                            '    End If
                            '    .Country = CheckString(objReader("Country"))
                            '    .Email = CheckString(objReader("Email"))
                            '    .Name = CheckString(objReader("Name"))
                            '    .Password = CheckString(objReader("PasswordHash"))
                            '    .UserID = CheckString(objReader("UserId"))
                            '    .SupplierID = CheckString(objReader("SupplierID"))
                            '    .RepID = CheckString(objReader("RepID"))
                            '    .Manager = CheckString(objReader("Manager"))
                            '    .Deleted = CheckDeletedField(objReader)
                            'End With
                        End If
                    End While
                End If
            End If
        Finally
            objReader.Close()
        End Try
        Return objUsers
    End Function

    Private Function ReadAccounts(ByVal objReader As SqlDataReader) As AccountInfo()
        Dim objAccounts As AccountInfo() = Nothing
        Dim intCounter As Integer = 0

        Try
            If Not objReader Is Nothing AndAlso objReader.HasRows Then
                While (objReader.Read())
                    ReDim Preserve objAccounts(intCounter)
                    objAccounts(intCounter) = New AccountInfo
                    With objAccounts(intCounter)
                        If IsNumeric(objReader("VAT")) Then
                            .VAT = CType(objReader("VAT"), Integer)
                        End If
                        .SupplierID = CheckString(objReader("SupplierID"))
                        .AccountID = CheckString(objReader("AccountID"))
                        .Branch = CheckString(objReader("BranchID"))
                        .Name = CheckString(objReader("Name"))
                        .PriceList = CheckString(objReader("Pricelist"))
                        .AccountGroup = CheckString(objReader("AccountGroup"))
                        ReDim .UserFields(9)
                        .UserFields(0) = CheckString(objReader("Userfield01"))
                        .UserFields(1) = CheckString(objReader("Userfield02"))
                        .UserFields(2) = CheckString(objReader("Userfield03"))
                        .UserFields(3) = CheckString(objReader("Userfield04"))
                        .UserFields(4) = CheckString(objReader("Userfield05"))
                        .UserFields(5) = CheckString(objReader("Userfield06"))
                        .UserFields(6) = CheckString(objReader("Userfield07"))
                        .UserFields(7) = CheckString(objReader("Userfield08"))
                        .UserFields(8) = CheckString(objReader("Userfield09"))
                        .UserFields(9) = CheckString(objReader("Userfield10"))

                    End With
                    intCounter = intCounter + 1
                End While
            End If
        Finally
            objReader.Close()
        End Try
        Return objAccounts
    End Function

    <WebMethod()> _
    Public Function ForgotPassword(ByVal strUserId As String, ByVal strEmailId As String) As BaseResponse
        If _Log.IsDebugEnabled Then _Log.Debug("entered...")
        Dim objResponse As New BaseResponse
        Dim conConnection As SqlConnection = Nothing
        Try
            Dim cmdCommand As New SqlCommand("usp_user_forgotpassword")
            cmdCommand.Parameters.AddWithValue("@UserID", strUserId)
            cmdCommand.Parameters.AddWithValue("@Email", strEmailId)

            Dim dr As SqlDataReader
            dr = objDBHelper.ExecuteReader(cmdCommand)

            objResponse.Status = True

            If Not dr Is Nothing AndAlso dr.Read() Then
                If dr.HasRows Then
                    Dim strMessage As String = String.Format("<br>Hi {0},<br><br>You requested recovery of your Rapidtrade password.<br><b>Password: {1}</b><br>Please keep your password safe to prevent unauthorized access.", dr("Name"), dr("Passwordhash"))
                    Dim strUsersEmailId As String = Convert.ToString(dr("Email"))
                    If Not String.IsNullOrEmpty(strUsersEmailId) Then
                        Dim objEmailInfo As New EmailInfo
                        With objEmailInfo
                            .MailTo = strUsersEmailId
                            .MailFrom = ConfigurationManager.AppSettings("FromEmail")
                            .MailContent = strMessage
                            .Subject = "[RapidTrade] Your Password"
                            .IsHTML = True
                        End With

                        Dim mailComponent As New Email
                        objResponse = mailComponent.SendMail(objEmailInfo)
                    Else
                        ReDim Preserve objResponse.Errors(0)
                        objResponse.Errors(0) = "User's email id is not available in database"
                        Exit Try
                    End If
                Else
                    ReDim Preserve objResponse.Errors(0)
                    objResponse.Errors(0) = "User information matching given user id or email is not available in database"
                    Exit Try
                End If
            Else
                ReDim Preserve objResponse.Errors(0)
                objResponse.Errors(0) = "User information matching given user id or email is not available in database"
                Exit Try
            End If
        Catch ex As Exception
            If _Log.IsErrorEnabled Then _Log.Error(String.Format("Exception for User ID: {0}  Email: {1}", strUserId, strEmailId), ex)
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
End Class

