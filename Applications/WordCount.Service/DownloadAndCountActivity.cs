using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ReactiveMachine;

namespace WordCount.Service
{
    public class DownloadAndCountActivity : IActivity<Dictionary<string, int>>
    {
        #region Inputs

        public Uri DocumentUrl;

        #endregion

        private static readonly char[] separators = { ' ', '\n', '<', '>', '=', '\"', '\'', '/', '\\', '(', ')', '\t', '{', '}', '[', ']', ',', '.', ':', ';' };

        // we don't really want to spend too much time on any one download
        public TimeSpan TimeLimit => TimeSpan.FromMinutes(5);

        public async Task<Dictionary<string, int>> Execute(IContext context)
        {
            var client = new WebClient();
            string doc = await client.DownloadStringTaskAsync(DocumentUrl);
            var result = new Dictionary<string, int>();
            string[] words = doc.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                int currentCount;
                result.TryGetValue(word, out currentCount);
                result[word] = currentCount + 1;
            }
            return result;
        }
    }
}
