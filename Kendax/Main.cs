using System;
using Sulakore;
using System.Linq;
using Sulakore.Habbo;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sulakore.Protocol.Encryption;
using Sulakore.Communication;

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

        private readonly Dictionary<string, HHotels> _accountEmails;
        private readonly Dictionary<ListViewItem, HSession> _sessions;
        private readonly Dictionary<HSession, HKeyExchange> _exchanges;
        private readonly Dictionary<HSession, ListViewItem> _runningConnections;
        #endregion

        #region Constructor(s)
        public Main()
        {
            InitializeComponent();

            _aSprite = "...";

            _accountEmails = new Dictionary<string, HHotels>();
            _sessions = new Dictionary<ListViewItem, HSession>();
            _exchanges = new Dictionary<HSession, HKeyExchange>();
            _runningConnections = new Dictionary<HSession, ListViewItem>();
        }
        #endregion

        #region User Interface Event Listeners
        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            if (SessionViewer.SelectedItems.Count == 0 && !AllSessionsChckbx.Checked) return;

            StartAnimation("Buffering Account List");
            ListViewItem[] sessions = AllSessionsChckbx.Checked ? SessionViewer.Items.Cast<ListViewItem>().ToArray() : new[] { SessionViewer.SelectedItems[0] };
            SetAnimation(string.Format("Authenticating% | (1/{0})", sessions.Length));

            int loginCount = 1, loginsFailed = 0, loginsSucceeded = 0;
            bool isMultiple = sessions.Length > 1;
            await Task.WhenAll(sessions.Select(async session =>
            {
                HSession account = _sessions[session];
                if (await account.LoginAsync())
                {
                    loginsSucceeded++;

                    session.SubItems[0].Text = account.PlayerName;
                    session.SubItems[1].Text = account.PlayerId.ToString();
                    session.SubItems[3].Text = "Authenticated";
                    session.ToolTipText = string.Format("Player Name: {0}\nPlayer ID#: {1}\nPlayer Email: {2}", account.PlayerName, account.PlayerId, account.Email);

                    SessionViewer.Focus();
                    SessionViewer.EnsureVisible(session.Index);

                    Text = string.Format("Kendax ~ Sessions Connected[{0}/{1}]", _exchanges.Count, _accountEmails.Count);
                }
                else loginsFailed++;

                if (++loginCount >= sessions.Length) StopAnimation(string.Format("Login(s) Succeeded! | ({0}/{1})", sessions.Length - loginsFailed, sessions.Length));
                else SetAnimation(string.Format("Authenticating% | ({0}/{1})", loginCount, sessions.Length));
            }));

            if (loginsSucceeded > 0) ConnectBtn.Enabled = true;
        }
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (SessionViewer.SelectedItems.Count == 0 && !AllSessionsChckbx.Checked) return;

            ListViewItem[] sessions = AllSessionsChckbx.Checked
                ? SessionViewer.Items.Cast<ListViewItem>().Where(session => session.SubItems[3].Text == "Authenticated").ToArray()
                : new[] { SessionViewer.SelectedItems[0] };
            StartAnimation(string.Format("Connecting% | (1/{0})", sessions.Length));

            Task.Factory.StartNew(async () =>
            {
                int connectCount = 1, connectsFailed = 0, connectsSucceeded = 0;
                foreach (ListViewItem session in sessions)
                {
                    bool connected;
                    HSession account = _sessions[session];
                    SessionViewer.BeginInvoke(new Action(() => { session.SubItems[3].Text = "Connecting..."; }));

                    await account.ConnectAsync();
                    if (connected = account.IsConnected)
                    {
                        connectsSucceeded++;
                        account.DataToClient += Bot_DataToClient;
                        account.OnDisconnected += Bot_Disconnected;

                        _runningConnections[account] = session;

                        _exchanges[account] = new HKeyExchange(EXPONENT, MODULUS);
                        account.SendToServer(4000, account.FlashClientBuild);
                        account.SendToServer(946);
                    }
                    else
                    {
                        connectsFailed++;
                        account.Disconnect(true);
                    }

                    SessionViewer.BeginInvoke(new Action(() =>
                    {
                        if (connected)
                        {
                            session.SubItems[3].Text = "Agreeing...";
                            SessionViewer.Focus();
                            SessionViewer.EnsureVisible(session.Index);
                        }
                        else session.SubItems[3].Text = "Authenticated";
                    }));

                    BeginInvoke(new Action(() =>
                    {
                        if (++connectCount >= sessions.Length) StopAnimation(string.Format("Connection(s) Established! | ({0}/{1})", sessions.Length - connectsFailed, sessions.Length));
                        else SetAnimation(string.Format("Connecting% | ({0}/{1})", connectCount, sessions.Length));
                    }));
                }
            });
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
            HSession[] accounts = await HSession.ExtractAsync(((string[])(e.Data.GetData(DataFormats.FileDrop)))[0]);
            SetAnimation(string.Format("Filtering Duplicates% | (1/{0})", accounts.Length));

            var verified = 0;
            SessionViewer.BeginUpdate();
            for (var i = 0; i < accounts.Length; i++)
            {
                HSession account = accounts[i];
                if (await Task.Run(() => AddAccount(account)))
                {
                    verified++;
                    var session = new ListViewItem(new[] { string.Empty, string.Empty, account.Email, "Waiting..." });
                    session.ToolTipText = "Player Name: < ? >\nPlayer ID#: < ? >\nPlayer Email: " + account.Email;

                    _sessions.Add(session, account);
                    SessionViewer.Items.Add(session);
                }
                SetAnimation(string.Format("Filtering Duplicates% | ({0}/{1})", i + 1, accounts.Length));
            }
            SessionViewer.EndUpdate();

            if (_sessions.Count >= 1)
            {
                LoginBtn.Enabled = true;
                AllSessionsChckbx.Enabled = true;
            }

            Text = string.Format("Kendax ~ Sessions Connected[{0}/{1}]", _exchanges.Count, verified);
            StopAnimation(string.Format("Sessions Loaded! - ({0}/{1})", verified, accounts.Length));
        }

        private void ATimer_Tick(object sender, EventArgs e)
        {
            StatusLbl.Text = _aStatus.Replace("%", _aSprite.Substring(0, _aTick + 1));
            _aTick += _aTick == 2 ? -2 : 1;
        }
        private void SessionViewer_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = SessionViewer.Columns[e.ColumnIndex].Width;
        }
        #endregion

        #region Private Methods
        private bool AddAccount(HSession account)
        {
            if (IsAlreadyVerified(account)) return false;
            _accountEmails.Add(account.Email, account.Hotel);
            return true;
        }
        private bool RemoveAccount(HSession account)
        {
            if (!IsAlreadyVerified(account)) return false;
            _accountEmails.Remove(account.Email);
            return true;
        }
        private bool IsAlreadyVerified(HSession account)
        {
            if (account == null) return true;
            return _accountEmails.ContainsKey(account.Email) && _accountEmails[account.Email] == account.Hotel;
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

        private void Bot_OnConnected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void Bot_DataToClient(object sender, DataToEventArgs e)
        {
            //MessageBox.Show(e.Packet.ToString());
        }
        private async void Bot_Disconnected(object sender, DisconnectedEventArgs e)
        {
            HSession account = (HSession)sender;
            e.UnsubscribeFromEvents = true;

            if (_runningConnections.ContainsKey(account))
            {
                ListViewItem session = _runningConnections[account];
                _runningConnections.Remove(account);

                bool isLoggedIn = await Task.Run(() => account.IsLoggedIn);
                session.SubItems[3].Text = isLoggedIn ? "Authenticated" : "Waiting...";

                if (!isLoggedIn)
                {
                    session.SubItems[0].Text = string.Empty;
                    session.SubItems[1].Text = string.Empty;
                    session.ToolTipText = "Player Name: < ? >\nPlayer ID#: < ? >\nPlayer Email: " + account.Email;
                }
            }
        }
        #endregion
    }
}