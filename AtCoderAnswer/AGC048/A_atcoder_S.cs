using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;

namespace AtCoderAnswer.AGC048
{
	class A_atcoder_S
	{
		static void Main(string[] args)
		{
			Scanner ss = new Scanner(Console.OpenStandardInput());
			int t = ss.NextInt();
			string[] sArray = ss.NextWords(t);

			for (int i = 0; i < sArray.Length; i++)
			{
				string s = sArray[i];

				if (string.Compare("atcoder", s) == -1)
				{
					Console.WriteLine($"0");
					continue;
				}

				long minSwapNum = long.MaxValue;
				long swapCnt = 0;
				for (int j = 0; j < "atcoder".Length; j++)
				{
					char c = "atcoder"[j];

					int index = search(s, c, j);
					if (index != -1)
					{
						if (index != 0)
						{
							minSwapNum = Math.Min(minSwapNum, (long)index);
						}
						swapCnt++;
					}
				}
				if (minSwapNum == long.MaxValue)
				{
					minSwapNum = 0;
				}


				if (swapCnt == 0)
				{
					Console.WriteLine($"-1");
				}
				else
				{
					Console.WriteLine($"{minSwapNum}");
				}
			}

		}

		// 指定の文字より大きい数が出てくるまでの最短距離を返す
		private static int search(string s, char c, int targetindex)
		{
			if (s.Length <= targetindex)
			{
				return -1;
			}

			// ターゲットからうしろを探索
			int distA = int.MaxValue;
			for (int i = targetindex; i < s.Length; i++)
			{
				if (c < s[i] && string.Compare("atcoder", swap(s, i, targetindex)) == -1)
				{
					distA = i - targetindex;
					break;
				}
			}

			// ターゲットの前を探索
			int distB = int.MaxValue;
			for (int i = targetindex; i >= 0; i--)
			{
				if (c < s[i] && string.Compare("atcoder", swap(s, i, targetindex)) == -1)
				{
					distB = targetindex - i;
					break;
				}
			}

			int dist = Math.Min(distA, distB);
			if (dist == int.MaxValue)
			{
				return -1;
			}

			return dist;

		}

		private static string swap(string s, int i, int j)
		{
			char[] ss = s.ToArray();

			char temp = ss[i];
			ss[i] = ss[j];
			ss[j] = temp;

			return new string(ss);
		}

		#region Scanner
		/// <summary>
		/// javaのscannerクラスインスパイア
		/// </summary>
		public class Scanner
		{
			/// <summary>streamから各種値を読み込むクラスです</summary>
			/// <param name="stream">任意のストリーム</param>
			public Scanner(Stream stream)
			{
				m_stream = stream;
			}

			public readonly Stream m_stream;
			private readonly byte[] buf = new byte[1024];
			private int len, ptr;
			private bool isEof = false;
			/// <summary>ストリームの終端であるかを取得します</summary>
			public bool IsEndOfStream { get { return isEof; } }

			private byte read()
			{
				if (isEof) return 0;
				if (ptr >= len) { ptr = 0; if ((len = m_stream.Read(buf, 0, 1024)) <= 0) { isEof = true; return 0; } }
				return buf[ptr++];
			}
			private T[] enumerate<T>(int n, Func<T> f)
			{
				var a = new T[n];
				for (int i = 0; i < n; ++i) a[i] = f();
				return a;
			}

			/// <summary>1個のChar値を取得します</summary>
			public char NextChar() { byte b = 0; do b = read(); while ((b < 33 || 126 < b) && !isEof); return (char)b; }
			/// <summary>1個の単語を取得します</summary>
			public string NextWord()
			{
				var sb = new StringBuilder();
				for (var b = NextChar(); b >= 33 && b <= 126; b = (char)read())
					sb.Append(b);
				return sb.ToString();
			}
			/// <summary>行末まで丸ごと取得します</summary>
			public string NextLine()
			{
				var sb = new StringBuilder();
				for (var b = NextChar(); b != '\n'; b = (char)read())
					if (b == 0) break;
					else if (b != '\r') sb.Append(b);
				return sb.ToString();
			}
			/// <summary>1個のLong値を取得します</summary>
			public long NextLong()
			{
				if (isEof) return long.MinValue;
				long ret = 0; byte b = 0; var ng = false;
				do b = read();
				while (b != '-' && (b < '0' || '9' < b));
				if (b == '-') { ng = true; b = read(); }
				for (; true; b = read())
				{
					if (b < '0' || '9' < b)
						return ng ? -ret : ret;
					else ret = ret * 10 + b - '0';
				}
			}
			/// <summary>1個のInt値を取得します</summary>
			public int NextInt() { return (isEof) ? int.MinValue : (int)NextLong(); }
			/// <summary>1個のDouble値取得します</summary>
			public double NextDouble() { return double.Parse(NextWord(), CultureInfo.InvariantCulture); }

			/// <summary>N個のChar値を取得します</summary>
			public char[] NextChars(int n) { return enumerate(n, NextChar); }
			/// <summary>N個の単語を取得します</summary>
			public string[] NextWords(int n) { return enumerate(n, NextWord); }
			/// <summary>N個のDouble値を取得します</summary>
			public double[] NextDoubles(int n) { return enumerate(n, NextDouble); }
			/// <summary>N個のInt値を取得します</summary>
			public int[] NextInts(int n) { return enumerate(n, NextInt); }
			/// <summary>N個のLong値を取得します</summary>
			public long[] NextLongs(int n) { return enumerate(n, NextLong); }
		}
		#endregion
	}
}
