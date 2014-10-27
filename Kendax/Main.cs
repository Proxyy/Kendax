using System;
using Sulakore;
using System.Linq;
using Sulakore.Habbo;
using System.Drawing;
using Kendax.Services;
using System.Windows.Forms;
using Sulakore.Communication;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sulakore.Protocol.Encryption;
using Sulakore.Protocol;

namespace Kendax
{
    public partial class Main : Form
    {
        #region Private Fields
        private int _aTick;
        private string _aStatus;
        private readonly string _aSprite;

        private const int EXPONENT = 3;
        private const string MODULUS = "90e0d43db75b5b8ffc8a77e31cc9758fa43fe69f14184bef64e61574beb18fac32520566f6483b246ddc3c991cb366bae975a6f6b733fd9570e8e72efc1e511ff6e2bcac49bf9237222d7c2bf306300d4dfc37113bcc84fa4401c9e4f2b4c41ade9654ef00bd592944838fae21a05ea59fecc961766740c82d84f4299dfb33dd";

        private readonly Dictionary<ListViewItem, HSession> _accountItems;
        private readonly Dictionary<HSession, BotSession> _accountConnections;
        #endregion

        #region Constructor(s)
        public Main(string[] args)
        {
            InitializeComponent();

            _aSprite = "...";

            _accountItems = new Dictionary<ListViewItem, HSession>();
            _accountConnections = new Dictionary<HSession, BotSession>();

            if (args.Length > 0)
                OnDragDrop(new DragEventArgs(new DataObject(DataFormats.FileDrop, args), 0, 0, 0, (DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link), DragDropEffects.Copy));
        }
        #endregion

        #region Connection Event Listeners
        private void Session_OnConnected(object sender, EventArgs e)
        {
            HSession session = (HSession)sender;
            BotSession botSession = _accountConnections[session];

            session.SendToServer(4000, session.FlashClientBuild);
            session.SendToServer(3291);

            SessionViewer.BeginInvoke(new MethodInvoker(() => botSession.SessionItem.SubItems[3].Text = "Agreeing..."));
        }
        private void Session_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            HSession session = (HSession)sender;
            BotSession botSession = _accountConnections[session];

            e.UnsubscribeFromEvents = true;
            bool isLoggedIn = session.IsLoggedIn;
            botSession.SessionItem.SubItems[3].Text = isLoggedIn ? "Authenticated" : "Inactive";
            _accountConnections.Remove(session);
        }
        private void Session_DataToClient(object sender, DataToEventArgs e)
        {
            HSession session = (HSession)sender;
            BotSession botSession = _accountConnections[session];

            switch (e.Step)
            {
                case 1:
                {
                    botSession.Exchange = new HKeyExchange(EXPONENT, MODULUS);
                    botSession.Exchange.DoHandshake(e.Packet.ReadString(), e.Packet.ReadString());

                    session.SendToServer(3848, botSession.Exchange.PublicKey);
                    break;
                }
                case 2:
                {
                    session.ReceiveData = false;

                    byte[] SharedKey = botSession.Exchange.GetSharedKey(e.Packet.ReadString());
                    session.ClientEncrypt = new Rc4(SharedKey);
                    session.ServerDecrypt = new Rc4(SharedKey);

                    session.SendToServer(3342, 401, session.FlashClientUrl, session.GameData.Variables);
                    session.SendToServer(1788, session.SsoTicket, -1);

                    SessionViewer.BeginInvoke(new MethodInvoker(() => botSession.SessionItem.SubItems[3].Text = "Connected"));
                    BeginInvoke(new MethodInvoker(() => Text = string.Format("Kendax ~ Sessions Connected[{0}/{1}] | Selected: {2}", _accountConnections.Count, _accountItems.Count, SessionViewer.SelectedItems.Count)));
                    break;
                }
            }
        }
        #endregion

        #region User Interface Event Listeners
        private void LoginBtn_Click(object sender, EventArgs e)
        {

        }
        private void ConnectBtn_Click(object sender, EventArgs e)
        {

        }

        private void SessionViewer_DragEnter(object sender, DragEventArgs e)
        {
            if (((string[])(e.Data.GetData(DataFormats.FileDrop)))[0].EndsWith(".txt"))
                e.Effect = DragDropEffects.Copy;
        }
        private async void SessionViewer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect != DragDropEffects.Copy) return;

            StartAnimation("Extracting Sessions");
            HSession[] sessions = await HSession.ExtractAsync(((string[])(e.Data.GetData(DataFormats.FileDrop)))[0]);
            SetAnimation(string.Format("Filtering Duplicates% | (1/{0})", sessions.Length));

            var verified = 0;
            SessionViewer.BeginUpdate();
            for (int i = 0; i < sessions.Length; i++)
            {
                HSession session = sessions[i];
                SetAnimation(string.Format("Filtering Duplicates% | ({0}/{1})", i + 1, sessions.Length));

                if (await Task.Run(() => !IsAlreadyVerified(session)))
                {
                    verified++;

                    var accountItem = new ListViewItem(new[] { string.Empty, string.Empty, session.Email, "Inactive" });
                    SessionViewer.Items.Add(accountItem);
                    _accountItems.Add(accountItem, session);

                }
            }
            SessionViewer.EndUpdate();
            StopAnimation(string.Format("Sessions Loaded! - ({0}/{1})", verified, sessions.Length));
        }

        private void SelectAllBtn_Click(object sender, EventArgs e)
        {
            SelectAllSessions();
        }
        private void SessionViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                SelectAllSessions();
        }

        private async void SessionViewer_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem accountItem = SessionViewer.SelectedItems[0];
            HSession session = _accountItems[accountItem];

            ListViewItem.ListViewSubItem nameItem = accountItem.SubItems[0];
            ListViewItem.ListViewSubItem idItem = accountItem.SubItems[1];
            ListViewItem.ListViewSubItem emailItem = accountItem.SubItems[2];
            ListViewItem.ListViewSubItem statusItem = accountItem.SubItems[3];

            switch (statusItem.Text)
            {
                case "Inactive":
                {
                    statusItem.Text = "Logging In...";
                    if (await session.LoginAsync())
                    {
                        nameItem.Text = session.PlayerName;
                        idItem.Text = session.PlayerId.ToString();
                        statusItem.Text = "Authenticated";
                    }
                    else statusItem.Text = "Inactive";
                    break;
                }
                case "Authenticated":
                {
                    statusItem.Text = "Connecting...";

                    _accountConnections.Add(session, new BotSession(accountItem));

                    session.DataToClient += Session_DataToClient;
                    session.OnConnected += Session_OnConnected;
                    session.OnDisconnected += Session_OnDisconnected;
                    session.Connect();
                    break;
                }
            }
        }

        private void SessionViewer_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = SessionViewer.Columns[e.ColumnIndex].Width;
        }
        private void SessionViewer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Text = string.Format("Kendax ~ Sessions Connected[{0}/{1}] | Selected: {2}", _accountConnections.Count, _accountItems.Count, SessionViewer.SelectedItems.Count);
            LoginBtn.Enabled = SessionViewer.SelectedItems.Count > 0;
        }

        private void ATimer_Tick(object sender, EventArgs e)
        {
            StatusLbl.Text = _aStatus.Replace("%", _aSprite.Substring(0, _aTick + 1));
            _aTick += _aTick == 2 ? -2 : 1;
        }
        #endregion

        #region Private Methods
        private void SendToAll(byte[] Data)
        {
            HSession[] sessions = _accountConnections.Keys.Where(x => x.IsConnected).ToArray();
            foreach (HSession session in sessions)
                session.SendToServer(Data);
        }

        private void SelectAllSessions()
        {
            ListViewItem[] accountItems = SessionViewer.Items.Cast<ListViewItem>().ToArray();
            foreach (ListViewItem accountItem in accountItems)
                accountItem.Selected = true;
        }
        private bool IsAlreadyVerified(HSession session)
        {
            if (session == null) return true;

            HSession[] sessions = _accountItems.Values.ToArray();
            foreach (HSession _session in sessions)
                if (_session.Equals(session)) return true;

            return false;
        }

        private void SetAnimation(string status)
        {
            if (!status.Contains("%")) status += "%";
            _aStatus = status;
        }
        private void StopAnimation(string status)
        {
            ATimer.Stop();
            StatusLbl.Text = status;
        }
        private void StartAnimation(string status)
        {
            if (!status.Contains("%")) status += "%";

            _aStatus = status;
            ATimer.Start();
        }
        #endregion

    }
}