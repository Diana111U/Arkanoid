using Arkanoid.Classes;

namespace Arkanoid
{
    public partial class MainForm : Form
    {
        //Объявление переменных
        private List<Block> BlocksList = [];
        private const int RowsCount = 7;
        private const int ColumnsCount = 7;
        private Platform platform = null!;
        private Ball ball = null!;
        private bool IsGameStart = false;

        public MainForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Метод загрузки формы
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Объявление переменных
            var blockWidth = ClientSize.Width / ColumnsCount;
            var blockHeight = 30;
            var startX = 0;
            var startY = 50;

            //Добавление блоков
            for (var row = 0; row < RowsCount; row++)
            {
                for (var col = 0; col < ColumnsCount; col++)
                {
                    var x = startX + col * (blockWidth);
                    var y = startY + row * (blockHeight);

                    BlocksList.Add(new Block(blockHeight, blockWidth, x, y));
                }
            }

            //Параметры платформы
            var platformWidth = 130;
            var platformHeight = 30;
            var platformX = (ClientSize.Width - platformWidth) / 2;
            var platformY = ClientSize.Height - 100;
            platform = new Platform(platformHeight, platformWidth, platformX, platformY);

            //Параметры шарика
            var ballSize = 30;
            var ballX = platformX + (platformWidth - ballSize) / 2;
            var ballY = platformY - ballSize;
            ball = new Ball(ballX, ballY, ballSize, ballSize);
        }

        /// <summary>
        /// Метод отрисовки элементов
        /// </summary>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //Отображение блоков
            foreach (var block in BlocksList)
            {
                if (block.Health > 0)
                {
                    var blockRect = new Rectangle(block.X, block.Y, block.Width, block.Height);
                    e.Graphics.FillRectangle(Brushes.Pink, blockRect);
                    e.Graphics.DrawRectangle(Pens.White, blockRect);
                }
            }

            //Отображение платформы
            var platformRect = new Rectangle(platform.X, platform.Y, platform.Width, platform.Height);
            e.Graphics.FillRectangle(Brushes.LightGray, platformRect);
            e.Graphics.DrawRectangle(Pens.White, platformRect);

            //Отображение шарика
            var ballRect = new Rectangle(ball.X, ball.Y, ball.Width, ball.Height);
            e.Graphics.FillEllipse(Brushes.Pink, ballRect);
            e.Graphics.DrawEllipse(Pens.White, ballRect);
        }

        /// <summary>
        /// Метод передвижения платформы и шарика за мышкой
        /// </summary>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //Координата x платформы по координате х мышки
            var newPlatformX = e.X - platform.Width / 2;

            //Обозначаем границы формы
            if (newPlatformX < 0)
            {
                newPlatformX = 0;
            }
            if (newPlatformX > ClientSize.Width - platform.Width)
            { 
                newPlatformX = ClientSize.Width - platform.Width; 
            }

            //Смена координатов платформы
            platform.X = newPlatformX;

            if (IsGameStart==false)
            {
                //Координата x шарика по координате середины платформы
                ball.X = platform.X + (platform.Width - ball.Width) / 2;
            }

            //Перерисовка формы
            this.Invalidate();
        }

        private void timerBall_Tick(object sender, EventArgs e)
        {
            //Двигаем шарик по координатам 
            ball.X += ball.BallSpeedX;
            ball.Y += ball.BallSpeedY;

            //Столкновение с границами формы
            if (ball.X <= 0 || ball.X + ball.Width >= ClientSize.Width)
            {
                ball.BallSpeedX = -ball.BallSpeedX;
            }
            if (ball.Y <= 0)
            {
                ball.BallSpeedY = -ball.BallSpeedY;
            }

            //Создание шарика с новыми координатами
            var ballRect = new Rectangle(ball.X, ball.Y, ball.Width, ball.Height);

            //Создание платформы с новыми координатами
            var platformRect = new Rectangle(platform.X, platform.Y, platform.Width, platform.Height);
            if (ballRect.IntersectsWith(platformRect))
            {
                ball.BallSpeedY = -ball.BallSpeedY; //Отскок вверх
            }

            //Столкновение шарика с блоками
            foreach (var block in BlocksList)
            {
                if (block.Health > 0)
                {
                    var blockRect = new Rectangle(block.X, block.Y, block.Width, block.Height);
                    if (ballRect.IntersectsWith(blockRect))
                    {
                        block.Punch(); //Уменьшаем здоровье блока
                        ball.BallSpeedY = -ball.BallSpeedY; //Отскок от блока
                        //Проверка выигрыша
                        if (IsWon() == true)
                        {
                            //Перерисовка формы
                            this.Invalidate();
                            timerBall.Stop();
                            MessageBox.Show("Вы выиграли!!!! Сыграйте ещё раз:)", "Выигрыш", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        break;
                    }
                }
            }

            //Перерисовка формы
            this.Invalidate();

            //Проверка проигрыша
            if (ball.Y > ClientSize.Height)
            {
                timerBall.Stop();
                MessageBox.Show("Вы проиграли! Попробуйте ещё раз(\n У вас всё получится :)", "Проигрыш", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        /// <summary>
        /// Метод клика мышки
        /// </summary>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            timerBall.Start();
            IsGameStart = true;
        }

        /// <summary>
        /// Метод проверки выигрыша 
        /// </summary>
        private bool IsWon() => BlocksList.All(x => x.Health == 0);
    }
}
