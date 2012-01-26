Imports System.Data.SqlClient
Imports RapidTradeWebService.Response

Module General
    Public Sub ProcessResponse(ByVal objBaseResponse As BaseResponse, ByVal objItemResponse As BaseResponse)
        objItemResponse.Status = objBaseResponse.Status
        If Not objBaseResponse.Status Then
            If Not objBaseResponse.Errors Is Nothing AndAlso objBaseResponse.Errors.Length > 0 Then
                If objItemResponse.Errors Is Nothing Then
                    objItemResponse.Errors = New String() {}
                End If
                ReDim Preserve objItemResponse.Errors(objItemResponse.Errors.Length + objBaseResponse.Errors.Length - 1)
                Array.Copy(objBaseResponse.Errors, 0, objItemResponse.Errors, objItemResponse.Errors.Length - objBaseResponse.Errors.Length, objBaseResponse.Errors.Length)
            End If
        End If
    End Sub

    Public Function CheckString(ByVal obj As Object, Optional ByVal defaultValue As String = Nothing) As String
        Dim strResult As String = String.Empty
        If Not obj Is Nothing Then
            strResult = obj.ToString()
        End If
        If String.IsNullOrEmpty(strResult) Then
            '*** returning "" as nulls causes issues on blackberry
            Return CStr(IIf(defaultValue = Nothing, "", defaultValue))
        Else
            Return strResult
        End If

    End Function
    Public Function CheckString2(ByVal obj As Object, Optional ByVal defaultValue As String = Nothing) As String
        Dim strResult As String = String.Empty
        If Not obj Is Nothing Then
            strResult = obj.ToString()
        End If
        If String.IsNullOrEmpty(strResult) Then
            '*** returning "" as nulls causes issues on blackberry
            Return CStr(IIf(defaultValue = Nothing, Nothing, defaultValue))
        Else
            Return strResult
        End If

    End Function
    Public Function CheckBoolean(ByVal obj As Object) As Boolean
        Dim bResult As Boolean
        Try
            bResult = CType(obj, Boolean)
        Catch
            bResult = False
        End Try
        Return bResult
    End Function

    Public Function CheckInteger(ByVal obj As Object) As Integer
        Dim intResult As Integer
        Try
            If IsNumeric(obj) Then
                intResult = CType(obj, Integer)
            End If
        Catch
            intResult = 0
        End Try
        Return intResult
    End Function

    Public Function CheckShort(ByVal obj As Object) As Short
        Dim intResult As Short
        Try
            If IsNumeric(obj) Then
                intResult = CType(obj, Short)
            End If
        Catch
            intResult = 0
        End Try
        Return intResult
    End Function

    Public Function CheckDecimal(ByVal obj As Object) As Decimal
        Dim dResult As Decimal
        Try
            If IsNumeric(obj) Then
                dResult = CType(obj, Decimal)
            End If
        Catch
            dResult = 0
        End Try
        Return dResult
    End Function

    Public Function CheckDouble(ByVal obj As Object) As Double
        Dim dResult As Double
        Try
            If IsNumeric(obj) Then
                dResult = CType(obj, Double)
            End If
        Catch
            dResult = 0
        End Try
        Return dResult
    End Function

    Public Function CheckDate(ByVal obj As String) As DateTime
        Dim dResult As DateTime
        Try
            dResult = CType(obj, DateTime)
        Catch
            dResult = DateTime.Now
        End Try
        Return dResult
    End Function

    Public Function VerifyDate(ByVal obj As Object) As DateTime
        Dim dResult As DateTime
        Try
            dResult = CType(obj, DateTime)
            If dResult.Year <= 1900 Then
                dResult = DateTime.Now
            End If
        Catch
            dResult = DateTime.Now
        End Try
        Return dResult
    End Function

    Public Function FormatDates(ByVal strInput As String) As String
        Dim strResult As String = strInput
        Try
            Dim objReg1 As New Regex("<ChangedDate>([^<]*)</ChangedDate>")
            Dim objReg2 As New Regex("<CreatedDate>([^<]*)</CreatedDate>")
            Dim objMatches As MatchCollection
            objMatches = objReg1.Matches(strInput)
            If Not objMatches Is Nothing AndAlso objMatches.Count > 0 Then
                Dim objMatch As Match
                For Each objMatch In objMatches
                    If Not objMatch.Groups Is Nothing AndAlso objMatch.Groups.Count > 0 Then
                        Dim objDate As DateTime
                        objDate = CheckDate(objMatch.Groups(1).Value)
                        If objDate.Year < 1905 Then
                            objDate = New DateTime(1900, 1, 1)
                        End If
                        strResult = strResult.Replace(objMatch.Value, String.Format("<ChangedDate>{0}</ChangedDate>", objDate.ToString("yyyy-MM-dd HH:mm:ss.fff")))
                    End If
                Next
            End If
            objMatches = objReg2.Matches(strInput)
            If Not objMatches Is Nothing AndAlso objMatches.Count > 0 Then
                Dim objMatch As Match
                For Each objMatch In objMatches
                    If Not objMatch.Groups Is Nothing AndAlso objMatch.Groups.Count > 0 Then
                        Dim objDate As DateTime
                        objDate = CheckDate(objMatch.Groups(1).Value)
                        If objDate.Year < 1905 Then
                            objDate = New DateTime(1900, 1, 1)
                        End If
                        strResult = strResult.Replace(objMatch.Value, String.Format("<CreatedDate>{0}</CreatedDate>", objDate.ToString("yyyy-MM-dd HH:mm:ss.fff")))
                    End If
                Next
            End If
        Catch
        End Try
        Return strResult
    End Function

    Public Function CheckDeletedField(ByVal obj As SqlDataReader) As Boolean
        Dim bResult As Boolean
        Try
            Dim objTable As DataTable = obj.GetSchemaTable()
            If Not objTable Is Nothing AndAlso Not objTable.Columns Is Nothing _
                AndAlso objTable.Columns.Count > 0 Then
                Dim iCount As Integer = objTable.Rows.Count - 1
                Dim iLoop As Integer
                For iLoop = iCount To 0 Step -1
                    If String.Equals(LCase(objTable.Rows(iLoop)("basecolumnname").ToString()), "deleted") Then
                        If Not obj("Deleted") Is Nothing Then
                            bResult = CheckBoolean(obj("Deleted"))
                        End If
                        Exit For
                    End If
                Next
            End If
        Catch
            bResult = False
        End Try
        Return bResult
    End Function
End Module
