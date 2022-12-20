using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmteCreator
{
    public static class Utils
    {
        public static void Parse(object sender, ConvertEventArgs e)
        {
            e.Value = Convert.ChangeType(e.Value, e.DesiredType);
        }

        public static void Format(object sender, ConvertEventArgs e)
        {
            e.Value = Convert.ChangeType(e.Value, e.DesiredType);
        }

		public static void Add<T1, T2>(this List<Tuple<T1, T2>> list, T1 item1, T2 item2)
		{
			list.Add(Tuple.Create(item1, item2));
		}
    }
}
