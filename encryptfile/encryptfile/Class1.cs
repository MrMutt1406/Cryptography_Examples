using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
namespace encryptfile
{
    //Enumartors for options
    public enum CryptMethod { ENCRYPT, DECRYPT }
    public enum CryptClass { AES, RC2, RC3, DES, TDS }
    class Class1
    {
        
        //The acutal crypting method
        public object Crypt(CryptMethod _method, CryptClass _class,object _input,string _key)
        {
            //Using switch to pick a algorithem method.
            //control is a algorithem. In the switch we change the method of control
            SymmetricAlgorithm control;
            switch (_class)
            {
                case CryptClass.AES:
                    control = new AesManaged();
                    break;
                case CryptClass.RC2:
                    control = new RC2CryptoServiceProvider();
                    break;
                case CryptClass.RC3:
                    control = new RijndaelManaged();
                    break;
                case CryptClass.TDS:
                    control = new TripleDESCryptoServiceProvider();
                    break;
                default:
                    return false;
                    break;

            }

            //Encryption key
            control.Key = UTF8Encoding.UTF8.GetBytes(_key);
            //Padding
            control.Padding = PaddingMode.PKCS7;
            //Ciphermode
            control.Mode = CipherMode.ECB;

            ICryptoTransform cTranfrom = null;
            //The results in an byte array
            byte[] resultArray;
            //Checkes if we are encrypting or decrypting
            if (_method == CryptMethod.ENCRYPT)
            { cTranfrom = control.CreateEncryptor();}
            else if (_method == CryptMethod.DECRYPT)
            { cTranfrom = control.CreateDecryptor();}
            //Checkes if the input is a string 
            if (_input is string)
            {
                //Getting the input as a byte array but then converting to string
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(_input as string);
                //The final result array in a block
                resultArray = cTranfrom.TransformFinalBlock(inputArray, 0, inputArray.Length);
                //Clearing control
                control.Clear();
                //Converting to a string
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);

            }
            //Checkes if the input is a byte
            else if(_input is byte[])
            {
                //Getting the result array and leaving leaving it as byte
                resultArray = cTranfrom.TransformFinalBlock((_input as byte[]), 0, (_input as byte[]).Length);
                //Clearing
                control.Clear();
                //No need to convert so returning the bytes
                return resultArray;
            }
            return false;
        }

    }
}
