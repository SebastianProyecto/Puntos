using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacePuntos.Controllers {
	public static class Security {
		/// Encripta una cadena
		public static string Encriptar(this string _cadenaAencriptar) {
			string result = string.Empty;
			byte[] encryted = System.Text.Encoding.UTF8.GetBytes(_cadenaAencriptar);
			result = Convert.ToBase64String(encryted);
			return result;
		}

		/// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
		public static string DesEncriptar(this string _cadenaAdesencriptar) {
			string result = string.Empty;
			byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
			//result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
			result = System.Text.Encoding.Unicode.GetString(decryted);
			return result;
		}

		public static byte[] ParseHexString(string value) {
			if(string.IsNullOrEmpty(value)) return null;
			if(1 == (1 & value.Length)) throw new ArgumentException("Invalid length for a hex string.", "value");

			int startIndex = 0;
			int length = value.Length;
			char[] input = value.ToCharArray();
			if('0' == input[0] && 'x' == input[1]) {
				if(2 == length) return null;
				startIndex = 2;
				length -= 2;
			}

			Func<char, byte> charToWord = c =>
			{
				if('0' <= c && c <= '9') return (byte)(c - '0');
				if('A' <= c && c <= 'F') return (byte)(10 + c - 'A');
				if('a' <= c && c <= 'f') return (byte)(10 + c - 'a');
				throw new ArgumentException("Invalid character for a hex string.", "value");
			};

			byte[] result = new byte[length >> 1];
			for(int index = 0, i = startIndex; index < result.Length; index++, i += 2) {
				byte w1 = charToWord(input[i]);
				byte w2 = charToWord(input[i + 1]);
				result[index] = (byte)((w1 << 4) + w2);
			}

			return result;
		}
	}



}