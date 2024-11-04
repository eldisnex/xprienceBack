using System.Security.Cryptography;
using System.Text;

public static class Functions
{
    static T ObtenerValor<T>(Dictionary<string, T> diccionario, string clave, T valorPredeterminado)
    {
        if (diccionario.TryGetValue(clave, out T valor))
        {
            return valor;
        }
        return valorPredeterminado;
    }
    public static string HashString(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convertir el string a un arreglo de bytes
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convertir el arreglo de bytes a un string hexadecimal
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2")); // Formato hexadecimal
            }
            return builder.ToString();
        }
    }


}