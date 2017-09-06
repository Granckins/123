using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace WebUI.Services
{
  public static class PasswordHash {
    private static string goodSymbols = @"[\w\d\!\@\#\$\%\^|&\*\?\+\=\-]*";
    private static string passphrase = "SCNCTQvqZSP3wr9TbblAt1UtWqIXO28NLt";
    private static HashAlgorithm hash = new System.Security.Cryptography.SHA512Managed();

    /// <summary>
    /// Возвращает истину, если пароль состоит из допустимых символов
    /// </summary>
    /// <param name="pwd">Пароль</param>
    /// <returns>true\false</returns>
    public static bool IsValid(string pwd) {
      if (String.IsNullOrEmpty(pwd)) return false;

      Regex regex = new Regex(goodSymbols);
      Match matches = regex.Match(pwd);

      if (matches.Value == pwd)
        return true;
      else
        return false;
    }

    public static string ComputeHash(string pwd) {
      byte[] salt = new byte[new Random().Next(4, 8)];
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      rng.GetNonZeroBytes(salt);
      return ComputeHash(pwd, salt);
    }

    public static string ComputeHash(string pwd, byte[] salt) {
      if (IsValid(pwd)) {

        byte[] password = Encoding.UTF8.GetBytes(pwd);
        byte[] passwordAndSaltBytes = new byte[password.Length + salt.Length];
        Array.Copy(password, passwordAndSaltBytes, password.Length);
        Array.Copy(salt, 0, passwordAndSaltBytes, password.Length, salt.Length);


        byte[] hashBytes = hash.ComputeHash(passwordAndSaltBytes);
        byte[] hashBytesWithSalt = new byte[hashBytes.Length + salt.Length];

        Array.Copy(hashBytes, hashBytesWithSalt, hashBytes.Length);
        Array.Copy(salt, 0, hashBytesWithSalt, hashBytes.Length, salt.Length);


        return Convert.ToBase64String(hashBytesWithSalt);
      }
      else {
        return "";
      }
    }

    public static bool VerifyHash(string pwd, string passwordHash) {
      byte[] pwdHash = Convert.FromBase64String(passwordHash);
      int hashSize = 64;
      if (hashSize > pwdHash.Length) return false;
      byte[] saltBytes = new byte[pwdHash.Length - hashSize];

      for (int i = 0; i < saltBytes.Length; i++)
        saltBytes[i] = pwdHash[hashSize + i];

      string expectedHashString = ComputeHash(pwd, saltBytes);
      int ln = expectedHashString.Length;

      return (passwordHash == expectedHashString);

    }

    public static string GeneratePass(int length) {
      var result = "";
      var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789".ToArray();
      Random rd = new Random();

      int maxRand = allowedChars.Length;
      for (int j = 0; j < length; j++) {
        result += allowedChars[rd.Next(maxRand)];
      }
      return result;
    }

    public static string Encrypt(string message) {
      byte[] results;
      UTF8Encoding utf8 = new UTF8Encoding();
      MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
      byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
      TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
      desalg.Key = deskey;//to  pass encode key
      desalg.Mode = CipherMode.ECB;
      desalg.Padding = PaddingMode.PKCS7;
      byte[] encrypt_data = utf8.GetBytes(message);
      try {
        ICryptoTransform encryptor = desalg.CreateEncryptor();
        results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
      }
      finally {
        desalg.Clear();
        md5.Clear();
      }
      return Convert.ToBase64String(results);

    }

    public static string Decrypt(string message) {
      byte[] results;
      UTF8Encoding utf8 = new UTF8Encoding();
      MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
      byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
      TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
      desalg.Key = deskey;
      desalg.Mode = CipherMode.ECB;
      desalg.Padding = PaddingMode.PKCS7;
      byte[] decrypt_data = Convert.FromBase64String(message);
      try {
        ICryptoTransform decryptor = desalg.CreateDecryptor();
        results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
      }
      finally {
        desalg.Clear();
        md5.Clear();

      }
      return utf8.GetString(results);
    }
  }
}