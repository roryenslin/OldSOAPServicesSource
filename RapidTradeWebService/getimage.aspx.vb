Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports RapidTradeWebService.DataAccess

Public Class getimage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ' check imagename query string
        If String.IsNullOrEmpty(Request.QueryString("imagename")) Then
            Throw New Exception("imagename is not supplied")
        End If

        Dim height As Integer
        Dim width As Integer

        If Not Integer.TryParse(Request.QueryString("width"), width) Then
            Throw New Exception("width is not supplied or invalid")
        End If

        If Not Integer.TryParse(Request.QueryString("height"), height) Then
            Throw New Exception("height is not supplied or invalid")
        End If

        'Dim sourceImage As String = SearchImageFile(Request.QueryString("imagename"), Request.QueryString("subfolder"))
        Dim sourceImage As String = SearchImageFileInDB(Request.QueryString("imagename"))

        If (String.IsNullOrEmpty(sourceImage)) Then
            sourceImage = Request.QueryString("imagename")
        End If
        sourceImage = SearchImageFile(sourceImage, Request.QueryString("subfolder"))

        If (sourceImage = String.Empty) Then
            ' image not found, use default noimage.jpg set size bigger than noimage.jpg size so that no resizing will be made
            sourceImage = Path.Combine(ConfigurationManager.AppSettings("ImageFolder"), "noimage.jpg")
            width = 500
            height = 500
        End If

        '' get resize image
        Dim resizeImage As Image = Me.ResizeImage(sourceImage, width, height)

        ' push resize image to the browser
        Response.ContentType = "image/jpeg"
        resizeImage.Save(Response.OutputStream, ImageFormat.Jpeg)
        resizeImage.Dispose()
    End Sub

    ''' <summary>
    ''' Search image file. Return an empty string if file is not found.
    ''' </summary>
    Private Function SearchImageFile(ByVal imageName As String, ByVal subFolder As String) As String
        Dim imageFolder As String = ConfigurationManager.AppSettings("ImageFolder")
        Dim searchFolder As String = Path.Combine(imageFolder, subFolder)

        If Not String.IsNullOrEmpty(imageName) Then
            Dim fileExtension As String = Path.GetExtension(imageName)
            If (Not String.IsNullOrEmpty(fileExtension)) Then
                imageName = imageName.Replace(fileExtension, String.Empty)
            End If
        End If

        Dim filename As String = Path.Combine(searchFolder, imageName & ".jpg")
        If File.Exists(filename) Then
            Return filename
        End If

        filename = Path.Combine(searchFolder, imageName & ".png")
        If File.Exists(filename) Then
            Return filename
        End If

        filename = Path.Combine(searchFolder, "noimage.jpg")
        If File.Exists(filename) Then
            Return filename
        End If

        Return String.Empty
    End Function

    ''' <summary>
    ''' Searches the image name in database
    ''' </summary>
    ''' <param name="imageName">ImageName to search</param>
    ''' <returns>Imagename in database</returns>
    ''' <remarks></remarks>
    Private Function SearchImageFileInDB(ByVal imageName As String) As String
        Dim imageNameFromDB As String = String.Empty

        Dim objDBHelper As New DbHelper
        Dim cmdCommand As New SqlCommand("usp_productimages_search")
        cmdCommand.Parameters.AddWithValue("@ImageName", imageName)

        Dim objResult As Object = objDBHelper.ExecuteScalar(cmdCommand)
        If Not objResult Is Nothing Then
            imageNameFromDB = objResult.ToString()
        End If
        
        Return imageNameFromDB
    End Function

    ''' <summary>
    ''' Resize the input image to new size.
    ''' </summary>
    Private Function ResizeImage(ByVal imageFile As String, ByVal newWidth As Integer, ByVal newHeight As Integer) As Image
        Dim original As Image = Image.FromFile(imageFile)

        ' just return the input image 
        If ((original.Width < newWidth) AndAlso (original.Height < newHeight)) Then
            Return original
        End If

        ' determine the correct image size in respect with the original image proportion
        Dim targetW As Integer
        Dim targetH As Integer
        If (original.Width > original.Height) Then
            targetW = newWidth
            targetH = CType((original.Height * (CType(newWidth, Single) / CType(original.Width, Single))), Integer)
        Else
            targetH = newHeight
            targetW = CType((original.Width * (CType(newHeight, Single) / CType(original.Height, Single))), Integer)
        End If

        Dim imgPhoto As Image = Image.FromFile(imageFile)

        ' Create a new blank canvas.  The resized image will be drawn on this canvas.
        Dim bmPhoto As Bitmap = New Bitmap(targetW, targetH, PixelFormat.Format24bppRgb)
        bmPhoto.SetResolution(original.HorizontalResolution, original.VerticalResolution)
        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.SmoothingMode = SmoothingMode.AntiAlias
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic
        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality
        grPhoto.DrawImage(imgPhoto, New Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel)
        original.Dispose()
        imgPhoto.Dispose()
        grPhoto.Dispose()

        Return bmPhoto
    End Function

End Class