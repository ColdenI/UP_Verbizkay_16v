namespace UP_Verbizkay_16v
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanel.WrapContents = false;
            InitializeRainfallControls();
        }

        private void InitializeRainfallControls()
        {
            for (int day = 1; day <= 28; day++)
            {
                var dayLabel = new Label
                {
                    Text = $"День {day}:",
                    Width = 100,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(3)
                };

                var textBox = new TextBox
                {
                    Width = 100,
                    Margin = new Padding(3),
                    Location = new Point(100,0),
                    Tag = day, // сохраняем номер дня
                    Text = "0"
                };

                var panel = new Panel
                {
                    Width = 200,
                    Height = 30,
                    Margin = new Padding(2)
                };
                panel.Controls.Add(dayLabel);
                panel.Controls.Add(textBox);

                flowLayoutPanel.Controls.Add(panel);
            }
        }

        public void CalculateButton_Click(object sender, EventArgs e)
        {
            double sumEven = 0;     // Чётные дни
            double sumOdd = 0;      // Нечётные дни
            int countEven = 0;
            int countOdd = 0;
            double maxRainfall = double.MinValue;
            double minRainfall = double.MaxValue;
            int dayMax = 1;
            int dayMin = 1;

            foreach (Panel panel in flowLayoutPanel.Controls)
            {
                if (!(panel.Controls[1] is TextBox textBox)) return;
                textBox.Text = textBox.Text.Replace('.', ',');
                if (double.TryParse(textBox.Text, out double rainfall))
                {
                    int day = (int)textBox.Tag;

                    // Суммы по чётным/нечётным
                    if (day % 2 == 0)
                    {
                        sumEven += rainfall;
                        countEven++;
                    }
                    else
                    {
                        sumOdd += rainfall;
                        countOdd++;
                    }

                    // Максимум и минимум
                    if (rainfall > maxRainfall)
                    {
                        maxRainfall = rainfall;
                        dayMax = day;
                    }
                    if (rainfall < minRainfall)
                    {
                        minRainfall = rainfall;
                        dayMin = day;
                    }
                }
                else
                {
                    MessageBox.Show($"Ошибка ввода в день {(int)((TextBox)panel.Controls[1]).Tag}. Проверьте данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            double avgEven = countEven > 0 ? sumEven / countEven : 0;
            double avgOdd = countOdd > 0 ? sumOdd / countOdd : 0;
            double avgTotal = (sumEven + sumOdd) / 28;

            bool moreOnEven = sumEven > sumOdd;

            string resultMessage = $"Результаты анализа осадков за февраль:\n\n" +
                                   $"Сумма по чётным дням: {sumEven:F2}\n" +
                                   $"Сумма по нечётным дням: {sumOdd:F2}\n" +
                                   $"Среднее по чётным: {avgEven:F2} мм\n" +
                                   $"Среднее по нечётным: {avgOdd:F2} мм\n" +
                                   $"Среднее за месяц: {avgTotal:F2} мм\n" +
                                   $"Максимум ({maxRainfall:F2} мм) — день {dayMax}\n" +
                                   $"Минимум ({minRainfall:F2} мм) — день {dayMin}\n\n" +
                                   $"Условие 'по чётным выпало больше': {(moreOnEven ? "Верно" : "Неверно")}";

            MessageBox.Show(resultMessage, "Анализ завершён", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
