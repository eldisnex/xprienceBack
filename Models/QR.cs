using QRCoder;

class QR : QRCodeGenerator
{
    public string texto;

    public QR(string t)
    {
        texto = t;
    }

    /// <summary>
    /// Genera el código QR y retorna una imagen en un string base64 para poder mostrar como imagen en el front
    /// </summary>
    public string create()
    {
        QRCodeData qr = this.CreateQrCode(this.texto, QRCodeGenerator.ECCLevel.Q);
        using PngByteQRCode qrCode = new PngByteQRCode(qr);
        byte[] qrCodeImage = qrCode.GetGraphic(20);
        string qrCodeBase64 = Convert.ToBase64String(qrCodeImage);
        return "data:image/png;base64," + qrCodeBase64;
    }
}