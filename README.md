# EPAM.BSU.01.2016.Makarov.03
Задание 1.

•	Разработать immutable класс «многочлен» для работы с многочленами от одной переменной вещественного типа (в качестве внутренней стркутуры для хранения коэффициетов рекомендуется использовать sz-массив, использование другой структуры должно быть аргументировано). 

•	Переоределить для класса необходимые методы класса Object.

•	Перегрузить для класса операции, допустимые для работы с многочленами (исключая деление многочлена на многочлен).

•	Разработать unit-тесты (рекомедуется использовать NUnit-фреймворк).

<a href="https://github.com/RomanMakarov1002/EPAM.BSU.01.2016.Makarov.03/tree/master/Task1Day3"> Task1 </a>

Комментарии к заданиям 3-4,  второго дня.


К заданию 3. После реализации  HexFormatProvider для целых чисел должны проходить тесты, аналогичные приведенным ниже.

   [TestFixture]
    public class HexFormatProviderTest
    {

        [TestCase(145, Result = "0x91")]
        [TestCase(-145, Result = "-0x91")]
        [TestCase(0, Result = "0x0")]
        [TestCase(41837, Result = "0xA36D")]
        [TestCase(47, Result = "0x2F")]
        [TestCase(47.2, ExpectedException = typeof(ArgumentException))]
        public string Format_Test(object number)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IFormatProvider fp = new HexFormatProvider();
            return string.Format(fp, "{0:H}", number);
        }

        [TestCase(47, "{0:X}", Result = "2F")]
        [TestCase(.473, "{0:P}", Result = "47.30 %")]
        [TestCase(.473, "{0:P0}", Result = "47 %")]
        [TestCase(4.73, "{0:C}", Result = "¤4.73")]
        [TestCase(4.73, "{0:C}", Result = "¤4.73")]
        [TestCase(4.7321, "{0:F2}", Result = "4.73")]
        public string ParentFormat_Test(object number, string format)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            IFormatProvider fp = new HexFormatProvider();
            return string.Format(fp, format, number);
        }
    }
}

<a href="https://github.com/RomanMakarov1002/EPAM.BSU.01.2016.Makarov.02/tree/master/Task3Day2"> Task 3 Update </a>

К заданию 4. Реализовать классы Customer и CustomFormatProvider таким образом, чтобы их можно было использовать, например, так

CultureInfo ci = Thread.CurrentThread.CurrentCulture;
	Customer customer = new Customer()
	{
		Name = "Jeffrey Richter",
		Revenue = 1000000,
		ContactPhone = "+1 (425) 555-0100"
	};
	Console.WriteLine(customer.ToString());
	Console.WriteLine(customer.ToString("nrp"));
	Console.WriteLine($"Customer record: {customer.ToString("nrp", null)}");
	Console.WriteLine($"Customer record: {customer.ToString("nrp", new CustomFormatProvider())}");
	Console.WriteLine($"Customer record: {customer.ToString("nrp", new CultureInfo("de-DE"))}");
	Console.WriteLine($"Customer record: {customer.ToString("nr", null)}");
	Console.WriteLine($"Customer record: {customer.ToString("G", null)}");
	Console.WriteLine($"Customer record: {customer.ToString("r", null)}");
и получить

Jeffrey Richter
Jeffrey Richter $1,000,000.00 +1 (425) 555-0100
Customer record: Jeffrey Richter $1,000,000.00 +1 (425) 555-0100
Customer record: Customer: Jeffrey Richter. Contact phone: +1 (425) 555-0100. Revenue: $1,000,000.00
Customer record: Jeffrey Richter 1.000.000,00 € +1 (425) 555-0100
Customer record: Jeffrey Richter $1,000,000.00 +1 (425) 555-0100
Customer record: Jeffrey Richter
Customer record: $1,000,000.00

<a href="https://github.com/RomanMakarov1002/EPAM.BSU.01.2016.Makarov.02/tree/master/Task4Day2"> Task 4 Update </a>
