using System.Security.Cryptography;
using System.Text;
using System.IO;
namespace Common
{
    public class Encrypt
    {
        public static string SHA1_encrypt(string Unsecure)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(Unsecure);

            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = System.BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }
        public static string MD5_encrypt(string Unsecure)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] InBytes = Encoding.GetEncoding("GB2312").GetBytes(Unsecure);

            byte[] OutBytes = md5.ComputeHash(InBytes);

            string OutString = "";

            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }

            return OutString;
        }
    }
}
