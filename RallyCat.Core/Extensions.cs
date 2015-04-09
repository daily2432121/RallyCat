using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RallyCat.Core.Services;

namespace RallyCat.Core
{
    public static class Extensions
    {
        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static string HtmlToPlainText(this string htmlString)
        {
            HtmlConvertService h = new HtmlConvertService();
            return h.ConvertFromString(htmlString);
        }

        public static List<string> GetAllImageSrcs(this string htmlString)
        {
            HtmlConvertService h = new HtmlConvertService();
            return h.GetAllImageSrcs(htmlString);

        }
    }
}
