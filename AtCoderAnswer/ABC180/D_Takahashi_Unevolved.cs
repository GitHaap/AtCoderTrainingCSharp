using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;

namespace AtCoderAnswer.ABC180
{
	class D_Takahashi_Unevolved
	{
		static void Main(string[] args)
		{
			Scanner ss = new Scanner(Console.OpenStandardInput());
			ulong x = (ulong)ss.NextLong();
			ulong y = (ulong)ss.NextLong();
			ulong a = (ulong)ss.NextLong();
			ulong b = (ulong)ss.NextLong();

			ulong str = x;
			ulong exp = 0;

			//HACK: A倍の部分は全探索も間に合ったぽい
			// Bが選択された時点で以降Bが勝つので、Xを何回A倍すればBを上回るかを求める
			double na = Math.Ceiling(Math.Log((double)b / x, (double)a));
			double ny = Math.Ceiling(Math.Log((double)y / x, (double)a));
			if ( na > 0 && ny > 0 && ny < na)
			{
				na = ny;
			}
			if (na > 0)
			{
				str *= (ulong)Math.Pow(a, na);
				exp += 1 * (ulong)na;
			}
			// 上限チェック
			if (y <= str)
			{
				if (exp > 0)
				{
					exp--;
				}
				Console.WriteLine(exp);
				return;
			}

			//Bが選択された時点で以降ずっとB
			ulong nb = (y - str) / b;
			str += b * nb;
			exp += 1 * nb;
			// きっかり割りきれたら1減らす
			if ((y - str) % b == 0)
			{
				if (exp > 0)
				{
					exp--;
				}
			}

			Console.WriteLine(exp);
			return;
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
