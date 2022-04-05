using PerekrestokParser.ApplicationLayer;
using PerekrestokParser.Common;
using PerekrestokParser.Forms;
using PerekrestokParser.ParserSettings;

namespace PerekrestokParser
{
    partial class MainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DownloadOnePage = new System.Windows.Forms.Button();
            this.DownloadAllCatalogue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DownloadOnePage
            // 
            this.DownloadOnePage.Location = new System.Drawing.Point(108, 192);
            this.DownloadOnePage.Name = "DownloadOnePage";
            this.DownloadOnePage.Size = new System.Drawing.Size(210, 180);
            this.DownloadOnePage.TabIndex = 0;
            this.DownloadOnePage.Text = "Загрузить цены с определенной страницы каталога";
            this.DownloadOnePage.UseVisualStyleBackColor = true;
            // 
            // DownloadAllCatalogue
            // 
            this.DownloadAllCatalogue.Location = new System.Drawing.Point(436, 192);
            this.DownloadAllCatalogue.Name = "DownloadAllCatalogue";
            this.DownloadAllCatalogue.Size = new System.Drawing.Size(210, 180);
            this.DownloadAllCatalogue.TabIndex = 1;
            this.DownloadAllCatalogue.Text = "Загрузить цены со всего каталога";
            this.DownloadAllCatalogue.UseVisualStyleBackColor = true;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DownloadAllCatalogue);
            this.Controls.Add(this.DownloadOnePage);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parser v0.1";
            this.ResumeLayout(false);

        }

        private async void DownloadOnePage_Click(object sender, EventArgs e)
        {
            await DownloadOnePageRun();
        }
        private async Task DownloadOnePageRun()
        {
            LinkRequestPage linkRequestPage = new LinkRequestPage();

            if (linkRequestPage.ShowDialog() == DialogResult.OK)
            {
                Parser parser = new Parser(new PerekrestokSettings($"{linkRequestPage.Link}"));
                try
                {
                    await parser.CheckSiteAvailability();
                    await parser.HandlePage();
                }
                catch (ParserException ex)
                {
                    MessageBox.Show($"{ex.Error}");
                }
                finally
                {
                    MessageBox.Show("Работа парсера завершена");
                }
            }
        }

        private async void DownloadAllCatalogue_Click(object sender, EventArgs e)
        {
            await DownloadAllCatalogueRun();
        }
        private async Task DownloadAllCatalogueRun()
        {
            Parser parser = new Parser(new PerekrestokSettings("https://www.perekrestok.ru/cat"));
            try
            {
                await parser.CheckSiteAvailability();
                await parser.HandleAllCatalogue();
            }
            catch (ParserException ex)
            {
                MessageBox.Show($"{ex.Error}");
            }
            finally
            {
                MessageBox.Show("Работа парсера завершена");
            }
        }

        #endregion

        private Button DownloadOnePage;
        private Button DownloadAllCatalogue;
    }
}