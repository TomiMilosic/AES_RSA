using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace tripledesanddes
{
    public class RSA
    {

        public static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        private string _publicKey; 
        private string _privateKey; 
        

        public RSA(int keysize)
        {
            GenerateKeyPair(keysize, out _publicKey, out _privateKey);   
        }

        public static void GenerateKeyPair(int keysize, out string _publicKey, out string _privateKey)
        {
            rsa.KeySize = keysize;
            _publicKey = rsa.ToXmlString(false);
            _privateKey= rsa.ToXmlString(true);
        
        }


        public void EncryptFile(string FilePath, int keysize)
        {
            byte[] encypted;



            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML - File | *.xml";
            dialog.Title = "Shrani javni kljuc";
            if (dialog.ShowDialog()==DialogResult.OK)
            {

                var potPublic = Path.Combine(dialog.FileName);

                using (var pisiPublic = new StreamWriter(potPublic))
                {
                    pisiPublic.Write(_publicKey);
                }
            }


            dialog.Title = "Shrani privatni kljuc";

            if (dialog.ShowDialog()==DialogResult.OK)
            {
                var potPrivate = Path.Combine(dialog.FileName);

                using (var pisiprivatni= new StreamWriter(potPrivate))
                {
                    pisiprivatni.Write(_privateKey);
                }

            }



            using (var rsaencrypt= new RSACryptoServiceProvider(keysize))
            {

                byte[] pisi= File.ReadAllBytes(FilePath);
                encypted = rsa.Encrypt(pisi,false);
            }

            File.WriteAllBytes(FilePath, encypted);


        }

        public void DecyrptFile(string FilePath, int keysize)
        {
            byte[] decryped;
            string beriKljuc=null;

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                beriKljuc=File.ReadAllText(dialog.FileName);
                
            }




            using (var rsadecyrpt= new RSACryptoServiceProvider(keysize))
            {
                rsa.FromXmlString(beriKljuc);
                 byte[] beri = File.ReadAllBytes(FilePath);

                decryped = rsa.Decrypt(beri,false);
            }

            File.WriteAllBytes(FilePath,decryped);
            

        }
    }
}
