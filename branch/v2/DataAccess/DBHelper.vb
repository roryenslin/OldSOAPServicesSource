Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Namespace DataAccess
    ''' <summary>
    ''' Database helper class. Equivalent to a Data Access Layer (DAL). 
    ''' This class has methods that makes it easy for Data Access 
    ''' Logic Components (DALC) to interact with backend database.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DBHelper
        Dim strConnectionString As String
        Dim conConnection As SqlConnection

        Public Sub New()
            strConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            conConnection = New SqlConnection(strConnectionString)
        End Sub

        Public Function ExecuteCommand(ByVal strCommand As String) As DataSet
            Dim cmdCommand As New SqlCommand(strCommand, conConnection)
            cmdCommand.CommandType = CommandType.StoredProcedure
            Return ExecuteCommand(cmdCommand)
        End Function

        Public Function ExecuteCommand(ByVal cmdCommand As SqlCommand) As DataSet
            cmdCommand.Connection = conConnection
            Dim adpDataAdapter As New SqlDataAdapter(cmdCommand)
            Dim dsData As New DataSet
            adpDataAdapter.Fill(dsData)
            If Not dsData Is Nothing AndAlso dsData.Tables.Count > 0 Then
                Return dsData
            Else
                Return Nothing
            End If
        End Function

        Public Function ExecuteNonQuery(ByVal cmdCommand As SqlCommand) As Integer
            cmdCommand.Connection = conConnection
            cmdCommand.CommandType = CommandType.StoredProcedure
            conConnection.Open()
            Dim intResult As Integer
            Try
                intResult = cmdCommand.ExecuteNonQuery()
            Finally
                conConnection.Close()
            End Try
            Return intResult
        End Function

        Public Function ExecuteNonQuery(ByVal cmdCommand As SqlCommand, ByVal cn As SqlConnection) As Integer
            cmdCommand.Connection = cn
            cmdCommand.CommandType = CommandType.StoredProcedure
            Dim intResult As Integer
            intResult = cmdCommand.ExecuteNonQuery()
            Return intResult
        End Function

        Public Function ExecuteScalar(ByVal cmdCommand As SqlCommand, ByVal cn As SqlConnection) As Object
            cmdCommand.Connection = cn
            cmdCommand.CommandType = CommandType.StoredProcedure
            Return cmdCommand.ExecuteScalar()
        End Function

        Public Function ExecuteScalar(ByVal cmdCommand As SqlCommand) As Object
            cmdCommand.Connection = conConnection
            cmdCommand.CommandType = CommandType.StoredProcedure
            conConnection.Open()
            Dim objResult As Object
            Try
                objResult = cmdCommand.ExecuteScalar()
            Finally
                conConnection.Close()
            End Try
            Return objResult
        End Function
        Public Function ExecuteReader(ByVal cmdCommand As SqlCommand, ByVal con As SqlConnection, Optional ByVal isSP As Boolean = True) As SqlDataReader
            cmdCommand.Connection = con
            cmdCommand.CommandTimeout = 60000
            If isSP Then
                cmdCommand.CommandType = CommandType.StoredProcedure
            Else
                cmdCommand.CommandType = CommandType.Text
            End If

            '            conConnection.Open()
            Dim r As SqlDataReader = cmdCommand.ExecuteReader()

            Return r
        End Function

        Public Function ExecuteReader(ByVal cmdCommand As SqlCommand, Optional ByVal isSP As Boolean = True) As SqlDataReader
            cmdCommand.Connection = conConnection
            cmdCommand.CommandTimeout = 60000
            If isSP Then
                cmdCommand.CommandType = CommandType.StoredProcedure
            Else
                cmdCommand.CommandType = CommandType.Text
            End If

            conConnection.Open()
            Dim r As SqlDataReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection)

            Return r
        End Function

        Public Function GetConnection() As SqlConnection
            Return conConnection
        End Function
    End Class
End Namespace