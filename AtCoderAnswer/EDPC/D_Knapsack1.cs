﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;

namespace AtCoderAnswer.EDPC
{
	class EDPC_E_Knapsack1
	{
		static void Main(string[] args)
		{
			Scanner ss = new Scanner(Console.OpenStandardInput());
			int n = ss.NextInt();
			int w = ss.NextInt();

			// ピック回数、ウェイト、バリュー
			Dictionary<int, Dictionary<int, UInt64>> dp = new Dictionary<int, Dictionary<int, UInt64>>();

			List<(int w, int v)> items = new List<(int, int)>();
			for (int i = 0; i < n; i++)
			{
				items.Add((ss.NextInt(), ss.NextInt()));
			}

			UInt64 maxValue = 0;
			for (int i = 0; i < n; i++)
			{
				int weight = items[i].w;
				int value = items[i].v;

				dp[i] = new Dictionary<int, UInt64>();
				// なにも入らないときの価値
				dp[i][0] = 0;

				int prevIndex = i - 1;
				if (0 <= prevIndex)
				{
					// 前のインデックスに入っているものを総なめする
					foreach (var item in dp[prevIndex])
					{
						int prevWeight = item.Key;
						UInt64 prevValue = item.Value;

						// このアイテム入れなかった場合の価値
						dp[i].TryGetValue(prevWeight, out ulong curVal);
						dp[i][prevWeight] = Math.Max(curVal, prevValue);

						maxValue = Math.Max(maxValue, dp[i][prevWeight]);

						// 1個前の状態からその重さになるようにこのアイテムを入れた場合の価値
						int nextWeight = prevWeight + weight;
						// 重量超過したら次
						if (w < nextWeight)
						{
							continue;
						}
						UInt64 inItemValue = prevValue + (UInt64)value;
						dp[prevIndex].TryGetValue(nextWeight, out ulong currentVal);
						dp[i][nextWeight] = Math.Max(currentVal, inItemValue);

						maxValue = Math.Max(maxValue, dp[i][nextWeight]);
					}
				}
				else
				{
					// 0→その重さになるときの価値
					dp[i].TryGetValue(weight, out ulong currentVal);
					dp[i][weight] = Math.Max(currentVal, (UInt64)value);

					maxValue = Math.Max(maxValue, dp[i][weight]);
				}
			}

			Console.WriteLine(maxValue);
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
