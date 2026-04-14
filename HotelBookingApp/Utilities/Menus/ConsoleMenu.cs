using Azure;

namespace HotelBookingApp.Utilities.Menus
{
    internal class ConsoleMenu<T>
    {
        private string _title;
        private readonly List<MenuChoice<T>> _choices;
        private List<List<MenuChoice<T>>> _choicesPerPage;
        private int _index = 0;
        private int _currentPage = 0;



        internal ConsoleMenu(string title, List<MenuChoice<T>> choices)
        {
            _title = title;
            _choices = choices;
            _choicesPerPage = BuildPages(_choices);
        }
        private static List<List<MenuChoice<T>>> BuildPages(List<MenuChoice<T>> choices)
        {
            var backChoice = choices[choices.Count - 1];
            var choicesPerPage = choices
                .Chunk(9)
                .Select(chunk => chunk.ToList())
                .ToList();

            foreach (var page in choicesPerPage)
                page.Add(backChoice);

            return choicesPerPage;
        }


        internal MenuResult<T> Show()
        {
            if (_choicesPerPage.Count == 0)
                return MenuResult<T>.Cancelled();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(_title);
                Console.WriteLine();

                var currentChoices = _choicesPerPage[_currentPage];
                for (int choice = 0; choice < currentChoices.Count; choice++)
                {
                    Console.Write(choice == _index ? "> " : "  ");
                    Console.WriteLine($"{currentChoices[choice].Text}");
                }

                Console.WriteLine();
                Console.WriteLine();
                if (_choicesPerPage.Count > 1)
                {
                    for (int page = 0; page < _choicesPerPage.Count; page++)
                        Console.Write(page == _currentPage ? $">{page}<" : $" {page} ");
                    Console.WriteLine("Use arrow keys to navigate Up/Down and Left/right to switch page.");
                }
                else
                    Console.WriteLine("Use arrow keys to navigate Up/Down.");
                Console.WriteLine("Enter to select.");
                Console.WriteLine("Esc to go back.");
                Console.WriteLine();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _index--;
                        if (_index < 0) _index = currentChoices.Count - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        _index++;
                        if (_index >= currentChoices.Count) _index = 0;
                        break;

                    case ConsoleKey.RightArrow:
                        if (_choicesPerPage.Count > 1)
                        {
                            _currentPage++;
                            if (_currentPage >= _choicesPerPage.Count)
                                _currentPage = 0;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (_choicesPerPage.Count > 1)
                        {
                            _currentPage--;
                            if (_currentPage < 0) _currentPage = _choicesPerPage.Count - 1;
                        }
                        break;

                    case ConsoleKey.Enter:
                        return MenuResult<T>.Selected(_choicesPerPage[_currentPage][_index].Value);

                    case ConsoleKey.Escape:
                        return MenuResult<T>.Cancelled();
                }
            }
        }
        internal void ResetPage()
        {
            _currentPage = 0;
        }
        internal void SetTitle(string title)
        {
            _title = title;
        }
        internal void ResetIndex()
        {
            _index = 0;
        }
        internal void SetChoices(List<MenuChoice<T>> choices)
        {
            _choices.Clear();
            _choices.AddRange(choices);

            if (_index >= _choices.Count)
            {
                _index = 0;
            }
        }
    }
}
