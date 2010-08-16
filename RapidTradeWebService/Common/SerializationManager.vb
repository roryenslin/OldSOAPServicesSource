Imports System.Xml
Imports System.IO
Imports System.Xml.Serialization


Namespace Common
    Public Class SerializationManager

        Public Shared Function Serialize(ByVal objSource As Object) As String
            Dim strResult As String = String.Empty
            Dim xsSerializer As New XmlSerializer(objSource.GetType())
            Dim msOutStream As New MemoryStream
            xsSerializer.Serialize(msOutStream, objSource)
            msOutStream.Seek(0, SeekOrigin.Begin)
            Dim srReader As New StreamReader(msOutStream)
            strResult = srReader.ReadToEnd()
            Return strResult
        End Function

    End Class
End Namespace

