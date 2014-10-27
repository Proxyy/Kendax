using Sulakore.Habbo;
using System.Windows.Forms;
using Sulakore.Protocol.Encryption;

namespace Kendax.Services
{
    public class BotSession
    {
        public HKeyExchange Exchange { get; set; }
        public ListViewItem SessionItem { get; private set; }

        public BotSession(ListViewItem sessionItem)
        {
            SessionItem = sessionItem;
        }
    }
}