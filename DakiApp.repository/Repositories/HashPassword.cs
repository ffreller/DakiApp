using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DakiApp.domain.Entities;

namespace DakiApp.repository.Repositories
{
    public class HashPassword
    {
        public string GenerateHash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}