Imports RapidTradeWebService.Entity

Namespace Response
    <Serializable()> _
    Public Class ContactNotesReadListResponse
        Inherits BaseResponse

        Private _ContactNotes As ContactNotesInfo()

        Public Sub New()

        End Sub

        Public Property ContactNotes() As ContactNotesInfo()
            Get
                Return _ContactNotes
            End Get
            Set(ByVal value As ContactNotesInfo())
                _ContactNotes = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ContactNotesSync3Response
        Inherits ContactNotesReadListResponse

        Private _LastVersion As Integer

        Public Property LastVersion() As Integer
            Get
                Return _LastVersion
            End Get
            Set(ByVal value As Integer)
                _LastVersion = value
            End Set
        End Property

    End Class
    
End Namespace


