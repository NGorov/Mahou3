using System;
using System.Net;

namespace Mahou {
	static class UrlEncodingCompat {
		public static string UrlEncode(string value) {
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			return WebUtility.UrlEncode(value);
		}

		public static string UrlPathEncode(string value) {
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			return Uri.EscapeDataString(value);
		}
	}
}
