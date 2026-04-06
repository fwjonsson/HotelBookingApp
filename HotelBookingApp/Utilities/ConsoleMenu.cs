namespace HotelBookingApp.Utilities
{
    internal class ConsoleMenu<T> where T : Enum
    {
        private readonly string _title;
        private readonly List<MenuChoice<T>> _choices;
        private int _index = 0;

        internal ConsoleMenu(string title, List<MenuChoice<T>> choices)
        {
            _title = title;
            _choices = choices;
        }
        internal T Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_title);
                Console.WriteLine();

                for (int i = 0; i < _choices.Count; i++)
                {
                    if (i == _index) Console.Write("> ");
                    else Console.Write("  ");

                    Console.WriteLine($"{_choices[i].Text}");
                }
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _index--;
                        if (_index < 0) _index = _choices.Count - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        _index++;
                        if (_index >= _choices.Count) _index = 0;
                        break;

                    case ConsoleKey.Enter:
                        return _choices[_index].Value;
                }
            }
        }
        internal void ResetIndex()
        {
            _index = 0;
        }
    }
}
