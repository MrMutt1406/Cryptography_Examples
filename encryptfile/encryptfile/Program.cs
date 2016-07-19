using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace encryptfile
{
    static class Program
    {
        static void Main(string[] args)
        {
            //at is a thing we go to
            at:
            //The console writes a line that askes for a input e is for encrypting d is for decrypting
            Console.WriteLine("Write e to encrypt and write d to decrypt");
            //user is the input we get
            string user = Console.ReadLine();
            //Checking if the user wrote e for encrypting
            if (user == "e")
            {
                //myCrypt is the class to encrypting and encrypthing
                encryptfile.Class1 myCrypt = new Class1();
                //Encryption key can be changed but has to be the same length
                string key = "123456789portbv";
                //Getting all the bytes from TestFile.txt (its a text file so the bytes are text)
                byte[] fromFile = File.ReadAllBytes("TestFile.txt");
                //Now it changes the bytes(text) to the encrypted text. As you can see we are using the myCrypt and we pass the agruments.
                //We are doing ENCRYPT because the user wrote e and we are using AES but you can use TDES or rc2.
                //We are passing the file bytes (text) and we use the key then we give the result as byte
                File.WriteAllBytes("TestFile.txt",
                    (myCrypt.Crypt(encryptfile.CryptMethod.ENCRYPT, encryptfile.CryptClass.AES, fromFile, key) as byte[]));
            }
            //User wrote d
            if (user == "d")
            {
                // Making an myCrypt again
                encryptfile.Class1 myCrypt = new Class1();
                //The encryption key
                string key = "123456789qwertyt";
                //Everything asbefore but decrypting
                byte[] fromFile = File.ReadAllBytes("TestFile.txt");
                File.WriteAllBytes("TestFile.txt",
                    (myCrypt.Crypt(encryptfile.CryptMethod.DECRYPT, encryptfile.CryptClass.AES, fromFile, key) as byte[]));
            }
            //going to at
            goto at;

        }
    }
}
