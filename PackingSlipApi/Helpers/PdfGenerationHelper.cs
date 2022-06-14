using System.Text;

namespace PackingSlipApi.Helpers
{
    public class PdfGenerationHelper
    {
        private static string ReplaceHtmlValues(string tag)
        {
            string returnValue = string.Empty;
            tag = tag.Trim();

            switch (tag)
            {
                case "$Employee$":
                    returnValue = "Employee Name";
                    break;
            }
            return returnValue;
        }

        public static string ConvertHtmlToString(TextReader streamToRead, bool isHtml)
        {
            StringBuilder body = new StringBuilder();
            StringBuilder nextTag = new StringBuilder();
            bool inTag = false;
            char nextCharacter = char.MinValue;
            char tagStart = '$';

            while (streamToRead.Peek() >= 0)
            {
                nextCharacter = Convert.ToChar(streamToRead.Peek());
                if (nextCharacter.Equals(tagStart)) inTag = !inTag;

                if (inTag)
                {
                    nextTag.Append(Convert.ToChar(streamToRead.Read()));
                    if (nextTag.Length >= 50)
                    {
                        body.Append(nextTag.ToString());
                        nextTag.Length = 0;
                        inTag = false;
                    }
                }
                else if (nextTag.Length > 0)
                {
                    if (nextCharacter.Equals(tagStart)) nextTag.Append(Convert.ToChar(streamToRead.Read()));
                    body.Append(ReplaceHtmlValues(nextTag.ToString()));
                    nextTag.Length = 0;
                }
                else
                {
                    body.Append(Convert.ToChar(streamToRead.Read()));
                }
            }

            return body.ToString();
        }


    }
}
