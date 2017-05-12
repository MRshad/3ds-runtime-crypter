Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dial As New OpenFileDialog
        Dim dia As OpenFileDialog = dial
        If dia.ShowDialog = DialogResult.OK Then
            TextBox1.Text = dia.FileName
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim tarek As New FolderBrowserDialog
        Dim ept As Byte() = File.ReadAllBytes(TextBox1.Text)
        Dim Message As String = Convert.ToBase64String(ept)
        Dim Passphrase As String = "testingvm"
        Dim Results() As Byte
        Dim UTF8 As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
        ' Step 1. We hash the Passphrase using MD5
        ' We use the MD5 hash generator as the result is a 128 bit byte array
        ' which is a valid length for the TripleDES encoder we use below
        Using HashProvider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            Dim TDESKey() As Byte = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase))

            ' Step 2. Create a new TripleDESCryptoServiceProvider object

            ' Step 3. Setup the encoder
            Using TDESAlgorithm As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider() With {.Key = TDESKey, .Mode = CipherMode.ECB, .Padding = PaddingMode.PKCS7}
                ' Step 4. Convert the input string to a byte[]

                Dim DataToEncrypt() As Byte = UTF8.GetBytes(Message)

                ' Step 5. Attempt to encrypt the string
                Try
                    Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
                Finally
                    ' Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear()
                    HashProvider.Clear()
                End Try
            End Using
        End Using

        ' Step 6. Return the encrypted string as a base64 encoded string
        Dim akk As String = Convert.ToBase64String(Results)






        Dim abs2 As String = My.Resources.dllclass.Replace("manchester", akk)
        If tarek.ShowDialog = DialogResult.OK Then
            Directory.CreateDirectory(tarek.SelectedPath & "/tarek folder")
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/program.cs"), abs2)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/cons.csproj"), My.Resources.conscsproj)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/App.config"), My.Resources.appconfig)
            Directory.CreateDirectory(tarek.SelectedPath & "/tarek folder/Properties")
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/Properties/AssemblyInfo.cs"), My.Resources.assemblyinfo)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/Properties/Resources.Designer.cs"), My.Resources.recourcesdesigner)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/Properties/Resources.resx"), My.Resources.resour)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/Properties/Settings.Designer.cs"), My.Resources.settingsdesigner)
            File.WriteAllText((tarek.SelectedPath & "/tarek folder/Properties/Settings.settings"), My.Resources.settings)

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
