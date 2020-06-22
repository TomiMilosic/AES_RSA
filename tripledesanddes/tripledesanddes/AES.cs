using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tripledesanddes
{
    public class AES
    {
        public AesCryptoServiceProvider aes;
        

        public AES(int keysize)
        {




            aes = new AesCryptoServiceProvider();
            aes.KeySize = keysize;///16 znakov == 128, 32 znakov pa je 256 , 24 znakov pa je 192
            aes.BlockSize = 128;
           
            //shraniKljuc
            //Shrani IV

            //neki random text samo tolko da mam ke za preverit semsno lol test123

           
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;




            
        }



        public void EncryptFile(string FilePath) 
        {
            aes.GenerateIV();
            aes.GenerateKey();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            aes.Padding = PaddingMode.Zeros;


            string imedatoteke = string.Empty;




            saveFileDialog.Title = "Shrani kljuc";
            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                imedatoteke = Path.GetFileName(saveFileDialog.FileName);

                if (imedatoteke == null || imedatoteke == string.Empty)
                {
                    imedatoteke = "NeimenovanaDatoteka";
                }
                var potkljuca =    Path.Combine(saveFileDialog.FileName);

                using (var pisiKljuc = new FileStream(potkljuca, FileMode.Create, FileAccess.Write))
                {
                    pisiKljuc.Write(aes.Key, 0, aes.Key.Length);
                }


                
            }




            saveFileDialog.Title = "Shrani IV:";

            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                imedatoteke = Path.GetFileName(saveFileDialog.FileName);


                if (imedatoteke == null || imedatoteke == string.Empty)
                {
                    imedatoteke = "NeimenovanaDatoteka";
                }

                var potIV = Path.Combine(saveFileDialog.FileName);

                using (var pisiIV = new FileStream(potIV, FileMode.Create, FileAccess.Write))
                {
                    pisiIV.Write(aes.IV, 0, aes.IV.Length);
                }
            }
            byte[] bytes = File.ReadAllBytes(FilePath);
            byte[] ebyte = aes.CreateEncryptor(aes.Key,aes.IV).TransformFinalBlock(bytes,0,bytes.Length);
            File.WriteAllBytes(FilePath, ebyte);
        }

        public void DecryptFile(string FilePath)
        {


            //TODO da lahko uporabnik uploata kljuce pa shranjuje

            aes.Padding = PaddingMode.Zeros;
            byte[] kljuc=null;
            byte[] IV=null;


            //branje kljucev pa iV
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            saveFileDialog.Title = "Izberi ključ: ";


            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                kljuc = File.ReadAllBytes(saveFileDialog.FileName);
            }

            saveFileDialog.Title = "Izberi IV:";
            if (saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                IV = File.ReadAllBytes(saveFileDialog.FileName);
            }
           


            byte[] bytes = File.ReadAllBytes(FilePath);
            byte[] ebyte = aes.CreateDecryptor(kljuc,IV).TransformFinalBlock(bytes, 0, bytes.Length);
            File.WriteAllBytes(FilePath, ebyte);

        }

       
    }
}
