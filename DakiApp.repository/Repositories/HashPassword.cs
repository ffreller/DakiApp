using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DakiApp.domain.Entities;

namespace DakiApp.repository.Repositories
{
    public class HashPassword
    {
        // byte[] GenerateSalt(int length){
        //     var bytes = new byte[length];

        //     using (var rng = new RNGCryptoServiceProvider()){
        //         rng.GetBytes(bytes);
        //     }

        //     return bytes;
        // }

        public byte[] GenerateHash(UsuariosDomain usuario, int iterations, int length){
            var salt = Encoding.ASCII.GetBytes(usuario.DataCriacao.ToString());
            using (var deriveBytes = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(usuario.Senha), salt, iterations)){
                return deriveBytes.GetBytes(length);
            }
        }
        // private static string CreateSalt(int size)
        // {
        //     //Generate a cryptographic random number.
        //     RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        //     byte[] buff = new byte[size];
        //     rng.GetBytes(buff);

        //     // Return a Base64 string representation of the random number.
        //     return Convert.ToBase64String(buff);
        // }

        // private static string CreatePasswordHash(string pwd, string salt)
        // {
        //     string saltAndPwd = String.Concat(pwd, salt);
        //     string hashedPwd =
        //         FormsAuthentication.HashPasswordForStoringInConfigFile(
        //         saltAndPwd, "sha1");
        //     return hashedPwd;
        // }
    }
}