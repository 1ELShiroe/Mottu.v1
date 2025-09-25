namespace Shared.Libs;

public class IsBase64PngOrBmp
{
    public static bool Valid(string base64)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64);

            // PNG magic number
            byte[] pngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
            // BMP magic number
            byte[] bmpHeader = "BM"u8.ToArray();

            if (bytes.Length >= pngHeader.Length)
            {
                bool isPng = true;
                for (int i = 0; i < pngHeader.Length; i++)
                {
                    if (bytes[i] != pngHeader[i])
                    {
                        isPng = false;
                        break;
                    }
                }
                if (isPng) return true;
            }

            if (bytes.Length >= bmpHeader.Length)
            {
                bool isBmp = true;
                for (int i = 0; i < bmpHeader.Length; i++)
                {
                    if (bytes[i] != bmpHeader[i])
                    {
                        isBmp = false;
                        break;
                    }
                }
                if (isBmp) return true;
            }

            return false;
        }
        catch
        {
            return false; // Não é um Base64 válido
        }
    }
}