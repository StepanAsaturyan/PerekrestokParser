using LoadingIndicator.WinForms;

namespace PerekrestokParser
{
    public partial class MainPage : Form
    {

        private LongOperation _longOperation;
        public MainPage()
        {
            InitializeComponent();
            _longOperation = new LongOperation(this);
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }
    }
}