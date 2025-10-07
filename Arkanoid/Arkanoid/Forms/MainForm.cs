using Arkanoid.Classes;

namespace Arkanoid
{
    public partial class MainForm : Form
    {
        private Random rdn = new Random();
        private List<Block> BlocksList = [];
        private const int RowsCount = 7;
        private const int ColumnsCount = 7;
        private Platform platform;
        private Ball ball;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var blockWidth = ClientSize.Width / ColumnsCount;
            var blockHeight = 30;
            var startX = 0;
            var startY = 50;

            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    var x = startX + col * (blockWidth);
                    var y = startY + row * (blockHeight);

                    BlocksList.Add(new Block(blockHeight, blockWidth, x, y));
                }
            }

            var platformWidth = 130;
            var platformHeight = 30;
            var platformX = (ClientSize.Width - platformWidth) / 2;
            var platformY = ClientSize.Height - 100;
            platform = new Platform(platformHeight, platformWidth, platformX, platformY);

            var ballSize = 30;
            var ballX = platformX + (platformWidth - ballSize) / 2;
            var ballY = platformY - ballSize;
            ball = new Ball(ballX, ballY, ballSize, ballSize);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            foreach ( var block in BlocksList)
            {
                if (block.Health > 0)
                {
                    Rectangle blockRect = new Rectangle(block.X, block.Y, block.Width, block.Height);
                    e.Graphics.FillRectangle(Brushes.Pink, blockRect);
                    e.Graphics.DrawRectangle(Pens.White, blockRect);
                }
            }

            Rectangle platformRect = new Rectangle(platform.X, platform.Y, platform.Width, platform.Height);
            e.Graphics.FillRectangle(Brushes.LightGray, platformRect);
            e.Graphics.DrawRectangle(Pens.White, platformRect);

            Rectangle ballRect = new Rectangle(ball.X, ball.Y, ball.Width, ball.Height);
            e.Graphics.FillEllipse(Brushes.Pink, ballRect);
            e.Graphics.DrawEllipse(Pens.White, ballRect);
        }
    }
}
