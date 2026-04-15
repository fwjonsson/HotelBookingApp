namespace HotelBookingApp.Utilities.Menus
{
    internal class ConsoleMenu<T>
    {
        private string _title;
        private List<List<MenuChoice<T>>> _choicesPerPage;
        private List<MenuChoice<T>> _currentChoices;
        private int _index = 0;
        private int _currentPage = 0;



        internal ConsoleMenu(string title, List<MenuChoice<T>> choices)
        {
            _title = title;
            _choicesPerPage = BuildPages(choices);
            _currentChoices = new List<MenuChoice<T>>();
        }
        private List<List<MenuChoice<T>>> BuildPages(List<MenuChoice<T>> choices)
        {
            return choices
                .Chunk(10)
                .Select(chunk =>
                chunk.ToList())
                .ToList();
        }


        internal MenuResult<T> Show()
        {
            if (_choicesPerPage.Count == 0)
                return MenuResult<T>.Cancelled();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(_title);

                _currentChoices = _choicesPerPage[_currentPage];
                for (int choice = 0; choice < _currentChoices.Count; choice++)
                {
                    Console.Write(choice == _index ? "> " : "  ");
                    Console.WriteLine($"{_currentChoices[choice].Text}");
                }

                
                if (_choicesPerPage.Count > 1)
                    PrintPageNumbers();
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Use arrow keys to navigate Up/Down.");
                }
                
                Console.WriteLine("Enter to select.");
                Console.WriteLine("Esc to go back.");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _index--;
                        if (_index < 0) _index = _currentChoices.Count - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        _index++;
                        if (_index >= _currentChoices.Count) _index = 0;
                        break;

                    case ConsoleKey.RightArrow:
                        if (_choicesPerPage.Count > 1)
                        {
                            _currentPage++;
                            if (_currentPage >= _choicesPerPage.Count)
                                _currentPage = 0;

                            ClampIndex();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (_choicesPerPage.Count > 1)
                        {
                            _currentPage--;
                            if (_currentPage < 0)
                                _currentPage = _choicesPerPage.Count - 1;

                            ClampIndex();
                        }
                        break;

                    case ConsoleKey.Enter:
                        return MenuResult<T>.Selected(_currentChoices[_index].Value);

                    case ConsoleKey.Escape:
                        ResetPage();
                        return MenuResult<T>.Cancelled();
                }
            }
        }
        internal void ClampIndex()
        {
            _currentChoices = _choicesPerPage[_currentPage];

            if (_index >= _currentChoices.Count)
                _index = _currentChoices.Count - 1;
            if (_index < 0)
                _index = 0;
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
            _choicesPerPage = BuildPages(choices);

            if (_choicesPerPage.Count == 0)
            {
                _currentPage = 0;
                _index = 0;
                _currentChoices = new List<MenuChoice<T>>();
                return;
            }
            if (_currentPage >= _choicesPerPage.Count)
                _currentPage = _choicesPerPage.Count - 1;

            _currentChoices = _choicesPerPage[_currentPage];

            if (_index >= _currentChoices.Count)
                _index = _currentChoices.Count - 1;

            if (_index < 0)
                _index = 0;
        }
        private void PrintPageNumbers()
        {
            if (_choicesPerPage.Count <= 10)
            {
                for (int page = 0; page < _choicesPerPage.Count; page++)
                    Console.Write(page == _currentPage ? $">{page + 1,2}<" : $" {page + 1,2} ");
                
            }
            else if (_choicesPerPage.Count > 10 && _currentPage > 4 && _choicesPerPage.Count - _currentPage < 5)
            {
                Console.Write("... ");
                for (int page = _currentPage - 4; page < _choicesPerPage.Count; page++)
                    Console.Write(page == _currentPage ? $">{page + 1,2}<" : $" {page + 1,2} ");
            }
            else if (_choicesPerPage.Count > 10 && _currentPage > 4)
            {
                Console.Write("... ");
                for (int page = _currentPage - 4; page < _currentPage + 5; page++)
                    Console.Write(page == _currentPage ? $">{page + 1,2}<" : $" {page + 1,2} ");
                Console.Write(" ...");
            }
            else if (_choicesPerPage.Count > 10)
            {
                for (int page = 0; page < 10; page++)
                    Console.Write(page == _currentPage ? $">{page + 1,2}<" : $" {page + 1,2} ");
                Console.Write(" ...");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Use arrow keys to navigate Up/Down and Left/right to switch page.");
        }
    }
}
