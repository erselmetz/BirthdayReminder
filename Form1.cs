using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media; // Built-in ito, walang error dito!
using System.Threading.Tasks;

namespace BirthdayReminderW
{
    public partial class Form1 : Form
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "birthdays.txt");
        string soundFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");

        // Explicit names para iwas error sa Timer
        System.Windows.Forms.Timer blinkTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer closeCountdownTimer = new System.Windows.Forms.Timer();

        int timeLeft = 5;
        bool isRed = true;

        public Form1()
        {
            InitializeComponent();

            // Initial UI state
            labelSystemCheck.Text = "System check : " + DateTime.Now.ToString("MMMM dd, yyyy");
            labelName.Text = "Initializing...";

            blinkTimer.Interval = 500;
            blinkTimer.Tick += BlinkTimer_Tick;

            closeCountdownTimer.Interval = 1000;
            closeCountdownTimer.Tick += CloseCountdownTimer_Tick;

            this.Shown += Form1_Shown;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            await Task.Delay(2000); // 2 seconds na "pa-suspense" delay
            checkMessage();
        }

        public void checkMessage()
        {
            string today = DateTime.Now.ToString("MM-dd");
            bool found = false;
            string celebrantName = "";

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "Metz, 01-22" + Environment.NewLine);

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && parts[1].Trim() == today)
                {
                    celebrantName = parts[0].Trim();
                    found = true;
                    break;
                }
            }

            if (found)
            {
                // PARTY MODE
                labelName.Text = $"🎉 HAPPY BIRTHDAY!\n{celebrantName} 🎉";
                this.BackColor = Color.Black;

                // Button Styling
                buttonClose.FlatStyle = FlatStyle.Flat;
                buttonClose.BackColor = Color.Black;
                buttonClose.Text = "Close";

                blinkTimer.Start();
                HandleWavMusic();
            }
            else
            {
                // NORMAL DAY
                labelName.Text = "No birthdays today.\nHave a productive day!";
                buttonClose.Text = $"Close ({timeLeft})";
                closeCountdownTimer.Start();
                Console.Beep(880, 150);
            }
        }

        private async void HandleWavMusic()
        {
            if (!Directory.Exists(soundFolder))
            {
                PlayBirthdayBeepTune();
                return;
            }

            try
            {
                // 1. Kunin lahat ng .wav files sa folder
                List<string> allFiles = Directory.GetFiles(soundFolder, "*.wav").ToList();

                if (allFiles.Count == 0)
                {
                    PlayBirthdayBeepTune();
                    return;
                }

                // 2. I-sort para unahin ang default.wav kung meron man
                // Ilalagay natin ang default.wav sa pinaka-unang index
                string defaultPath = Path.Combine(soundFolder, "default.wav");
                if (allFiles.Contains(defaultPath))
                {
                    allFiles.Remove(defaultPath);
                    allFiles.Insert(0, defaultPath);
                }

                // 3. I-play lahat ng kanta sa listahan nang sunod-sunod
                await Task.Run(() =>
                {
                    foreach (string soundFile in allFiles)
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(soundFile);
                            // PlaySync: hihintayin matapos ang kanta bago mag-loop sa next file
                            player.PlaySync();
                        }
                        catch
                        {
                            // Kung may corrupt na file, skip lang at ituloy ang iba
                            continue;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // Fallback beep kung nag-fail ang folder reading
                PlayBirthdayBeepTune();
            }
        }

        private void PlayBirthdayBeepTune()
        {
            Task.Run(() => {
                int[] melody = { 261, 261, 294, 261, 349, 330 };
                int[] duration = { 300, 300, 600, 600, 600, 900 };
                for (int i = 0; i < melody.Length; i++) Console.Beep(melody[i], duration[i]);
            });
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            Color blinkColor = isRed ? Color.Red : Color.DeepSkyBlue;
            labelName.ForeColor = blinkColor;
            buttonClose.ForeColor = blinkColor;
            buttonClose.FlatAppearance.BorderColor = blinkColor;
            isRed = !isRed;
        }

        private void CloseCountdownTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                buttonClose.Text = $"Close ({timeLeft})";
            }
            else
            {
                closeCountdownTimer.Stop();
                Application.Exit();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}