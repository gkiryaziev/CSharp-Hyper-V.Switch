using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyper_V.Switch
{
    public class Parser
    {
        public string Parse(RunAction action, string data)
        {
            string result = "";

            switch (action)
            {
                case RunAction.AUTO:

                    break;
                case RunAction.OFF:

                    break;
                case RunAction.STATUS:
                    result = StatusParse(data);
                    break;
                case RunAction.REBOOT:

                    break;
            }

            return result;
        }

        private string StatusParse(string data)
        {
            var lines = data.Split(new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries).Where(l => l.Length > 24);

            foreach (var line in lines)
            {
                string key = line.Substring(0, 24).Replace(" ", string.Empty);

                if (key == "hypervisorlaunchtype")
                {
                    return line.Substring(24).Replace(" ", string.Empty);
                }
            }

            return "";
        }
    }
}
