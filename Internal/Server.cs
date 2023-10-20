using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using DynamicRest;
using System.Net;
using System.IO;
using System.Web;

namespace AmteCreator.Internal
{
	public static class Server
	{
		private static CookieContainer _cookies = new CookieContainer();
		public static string DEBUG_LastQuery = "";

		private static string _DownloadPage(string url, IEnumerable<KeyValuePair<string, string>> postData)
		{
			DEBUG_LastQuery = url;
			var hwr = (HttpWebRequest)WebRequest.Create(MainForm.URL + url);
			hwr.CookieContainer = _cookies;

			if (postData != null)
			{
				hwr.Method = "POST";
				hwr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
				DEBUG_LastQuery += "\r\nDATA=";
				using (var sw = new StreamWriter(hwr.GetRequestStream()))
					foreach (var entry in postData)
					{
						sw.Write(HttpUtility.UrlEncode(entry.Key, Encoding.UTF8) + "=" + HttpUtility.UrlEncode(entry.Value, Encoding.UTF8) + "&");
						DEBUG_LastQuery += HttpUtility.UrlEncode(entry.Key, Encoding.UTF8) + "=" +
							HttpUtility.UrlEncode(entry.Value, Encoding.UTF8) + "&";
					}
			}

			HttpWebResponse resp;
			try { resp = (HttpWebResponse)hwr.GetResponse(); }
			catch (WebException e) { resp = (HttpWebResponse)e.Response; }
			if (resp == null)
				return "no response";
			var buffer = new byte[8192];
			var ms = new MemoryStream();
			int count = 0;
			do
			{
				count = resp.GetResponseStream().Read(buffer, 0, buffer.Length);
				ms.Write(buffer, 0, count);
			} while (count > 0);
			Encoding encode = Encoding.GetEncoding(string.IsNullOrEmpty(resp.ContentEncoding) ? "UTF-8" : resp.ContentEncoding);
			string res = encode.GetString(ms.GetBuffer());
			return res;
		}

		public static dynamic Query(string url, IEnumerable<KeyValuePair<string, string>> postData = null, bool login = false)
		{
			var data = _DownloadPage(url, postData);
			try
			{
				dynamic res = new JsonReader(data).ReadValue();
				if (!login && res.login == false)
				{
					Query("", MainForm.Options, true);
					return Query(url, postData, true);
				}
				return res;
			}
			catch
			{
				throw new InvalidDataException("Données invalides envoyées par le serveur:\n" + data);
			}
		}

		public static dynamic QuerySelect(
			string table,
			string where,
			string limit = null,
			string orderBy = null,
			string fields = "*",
			string action = "SELECT")
		{
			action = HttpUtility.UrlEncode(action, Encoding.UTF8);
			table = HttpUtility.UrlEncode(table, Encoding.UTF8);
			fields = HttpUtility.UrlEncode(fields, Encoding.UTF8);
			where = HttpUtility.UrlEncode(where, Encoding.UTF8);
			orderBy = (orderBy != null ? "&orderby=" + HttpUtility.UrlEncode(orderBy, Encoding.UTF8) : "");
			limit = (limit != null ? "&limit=" + HttpUtility.UrlEncode(limit, Encoding.UTF8) : "");
			return Query("?action=" + action + "&table=" + table + "&fields=" + fields + "&where=" + where + orderBy + limit);
		}

		public static string EscapeSql(object data)
		{
			if (data == null)
				return "null";
			if (data is string str)
				return $"'{str.Replace("'", "\\'")}'";
            if (data is object)
				return data.ToString().Replace("'", "\\'");
			if (data is bool b)
				return b ? "1" : "0";
			return data.ToString();
		}

		private static void _Update(byte type, string value)
		{
			var client = new TcpClient(MainForm.GameUrl, Constants.DolPort);
			var buffer = new byte[2048];
			buffer[0] = type;
			var count = Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, 3);
			buffer[1] = (byte)(count >> 8);
			buffer[2] = (byte)count;
			client.GetStream().Write(buffer, 0, buffer.Length);
			client.Close();
		}

		public static void UpdateItem(string item)
		{
			_Update(0, item);
		}

		public static void UpdateLoot(string mob)
		{
			_Update(1, mob);
		}

		public static void UpdateNpcTemplate(string templateId)
		{
			_Update(2, templateId);
		}
	}
}
