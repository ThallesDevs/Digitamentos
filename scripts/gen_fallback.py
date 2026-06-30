import json

data = {
    "Prose": [
        "A vida é aquilo que acontece enquanto você está fazendo outros planos.",
        "O único limite para as nossas realizações de amanhã são as nossas dúvidas de hoje.",
        "A persistência realiza o impossível.",
        "O sucesso não é o final, o fracasso não é fatal: é a coragem para continuar que conta.",
        "Não espere por circunstâncias ideais, crie-as.",
        "O talento vence jogos, mas o trabalho em equipe e a inteligência vencem campeonatos.",
        "A imaginação é mais importante que o conhecimento.",
        "Não encontre defeitos, encontre soluções.",
        "Acredite em si mesmo e chegará o dia em que os outros não terão outra escolha.",
        "O futuro pertence àqueles que acreditam na beleza de seus sonhos.",
        "O pessimista vê dificuldade em cada oportunidade; o otimista vê oportunidade em cada dificuldade.",
        "Você perde 100% dos tiros que não dá.",
        "A paciência é amarga, mas seu fruto é doce.",
        "A inovação distingue um líder de um seguidor.",
        "O segredo para avançar é começar.",
        "A educação é a arma mais poderosa que você pode usar para mudar o mundo.",
        "Quando a escrita é sincera, a alma do escritor se reflete na página.",
        "A felicidade não é algo pronto. Ela vem de suas próprias ações.",
        "Uma longa jornada começa com um único passo.",
        "A verdadeira sabedoria está em reconhecer a própria ignorância.",
        "A criatividade é a inteligência se divertindo.",
        "O cérebro é como um músculo. Quando está em uso nós nos sentimos muito bem.",
        "As pessoas não compram o que você faz, elas compram o porquê você faz.",
        "Existem dois tipos de pessoas que vão te dizer que você não pode fazer a diferença.",
        "Coragem não é ausência de medo, mas a capacidade de seguir em frente apesar dele.",
        "Não confunda derrotas com fracasso. Derrota é cair, fracasso é desistir.",
        "Se você traçar metas absurdamente altas e falhar, seu fracasso será muito melhor que o sucesso de todos.",
        "O sucesso normalmente vem para quem está ocupado demais para procurar por ele.",
        "A vida é 10% o que acontece com você e 90% como você reage a isso.",
        "Se você realmente quer algo, não espere. Ensine a si mesmo a ser impaciente.",
        "Não importa o quão devagar você vá, desde que você não pare.",
        "A melhor maneira de prever o futuro é criá-lo.",
        "Um homem de sucesso é aquele que cria uma parede com os tijolos que jogaram nele.",
        "Muitas das falhas da vida ocorrem quando não percebemos o quão próximos estávamos do sucesso.",
        "Tudo o que você sempre quis está do outro lado do seu medo.",
        "Sucesso é caminhar de fracasso em fracasso sem perder o entusiasmo.",
        "A mente que se abre a uma nova ideia jamais voltará ao seu tamanho original.",
        "Somos o que repetidamente fazemos. A excelência, portanto, não é um feito, mas um hábito.",
        "A simplicidade é o último grau de sofisticação.",
        "O conhecimento fala, mas a sabedoria escuta.",
        "Aquilo que não me mata me faz mais forte.",
        "A jornada de mil milhas começa com um pequeno passo, e cada passo é uma nova descoberta.",
        "Seja a mudança que você deseja ver no mundo.",
        "Para ter sucesso, o seu desejo de sucesso deve ser maior do que o seu medo de fracassar.",
        "A lógica vai levar você de A a B. A imaginação vai levar você a qualquer lugar."
    ],
    "Code": {
        "Python": {
            "Basic": [
                "name = input('Name: ')\nprint(f'Hello {name}')",
                "def add(a, b):\n    return a + b",
                "for i in range(10):\n    print(i * 2)",
                "items = [1, 2, 3]\nitems.append(4)",
                "with open('file.txt', 'r') as f:\n    print(f.read())",
                "x = [i**2 for i in range(10) if i % 2 == 0]",
                "import math\nprint(math.sqrt(16))",
                "my_dict = {'key': 'value'}\nprint(my_dict.get('key'))",
                "def greet(name='World'):\n    print(f'Hello, {name}!')",
                "words = ['apple', 'banana', 'cherry']\nfor word in words:\n    print(len(word))",
                "is_valid = True\nif is_valid:\n    print('Valid')",
                "def is_even(num):\n    return num % 2 == 0",
                "import os\nprint(os.getcwd())",
                "def multiply(x, y):\n    return x * y",
                "count = 0\nwhile count < 5:\n    print(count)\n    count += 1",
                "try:\n    x = 1 / 0\nexcept ZeroDivisionError:\n    print('Error')",
                "colors = ('red', 'green', 'blue')\nprint(colors[1])"
            ],
            "Hard": [
                "class Parser:\n    def __init__(self, data):\n        self.data = data\n\n    def extract(self):\n        return [x for x in self.data if x.get('active')]",
                "async def fetch_data(url):\n    async with aiohttp.ClientSession() as session:\n        async with session.get(url) as resp:\n            return await resp.json()",
                "def quicksort(arr):\n    if len(arr) <= 1:\n        return arr\n    pivot = arr[len(arr) // 2]\n    left = [x for x in arr if x < pivot]\n    middle = [x for x in arr if x == pivot]\n    right = [x for x in arr if x > pivot]\n    return quicksort(left) + middle + quicksort(right)",
                "import functools\n\n@functools.lru_cache(maxsize=128)\ndef fibonacci(n):\n    if n < 2:\n        return n\n    return fibonacci(n-1) + fibonacci(n-2)",
                "def decorator_factory(prefix):\n    def decorator(func):\n        def wrapper(*args, **kwargs):\n            print(f'{prefix}: calling {func.__name__}')\n            return func(*args, **kwargs)\n        return wrapper\n    return decorator",
                "from dataclasses import dataclass\n\n@dataclass\nclass Point:\n    x: float\n    y: float\n\n    def distance_to(self, other):\n        return ((self.x - other.x)**2 + (self.y - other.y)**2) ** 0.5",
                "import threading\ndef worker():\n    print('Worker')\nthreads = []\nfor i in range(5):\n    t = threading.Thread(target=worker)\n    threads.append(t)\n    t.start()",
                "def get_primes(n):\n    sieve = [True] * n\n    for p in range(2, n):\n        if sieve[p]:\n            yield p\n            for i in range(p*p, n, p):\n                sieve[i] = False",
                "with sqlite3.connect(':memory:') as conn:\n    cur = conn.cursor()\n    cur.execute('CREATE TABLE users (id INT, name TEXT)')\n    cur.executemany('INSERT INTO users VALUES (?, ?)', [(1, 'Alice')])",
                "import re\ntext = 'Email: test@example.com'\nmatch = re.search(r'[\\w\\.-]+@[\\w\\.-]+', text)\nif match:\n    print(match.group(0))"
            ]
        },
        "C#": {
            "Basic": [
                "Console.WriteLine(\"Hello, World!\");",
                "int a = 5;\nint b = 10;\nint sum = a + b;",
                "for (int i = 0; i < 5; i++)\n{\n    Console.WriteLine(i);\n}",
                "var list = new List<string>();\nlist.Add(\"Test\");",
                "string text = \"TypeEnigma\";\nConsole.WriteLine(text.Length);",
                "int[] numbers = { 1, 2, 3 };\nint first = numbers[0];",
                "public int Add(int x, int y) {\n    return x + y;\n}",
                "if (isValid)\n{\n    Console.WriteLine(\"Valid\");\n}",
                "foreach (var item in items)\n{\n    Console.WriteLine(item);\n}",
                "DateTime now = DateTime.Now;\nConsole.WriteLine(now.ToString(\"yyyy-MM-dd\"));",
                "public string Name { get; set; }",
                "while (count < 10)\n{\n    count++;\n}",
                "try\n{\n    Process();\n}\ncatch (Exception ex)\n{\n    Console.WriteLine(ex.Message);\n}",
                "string joined = string.Join(\", \", names);",
                "bool isMatch = text.Contains(\"Enigma\");"
            ],
            "Hard": [
                "public async Task<User> GetUserAsync(int id)\n{\n    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);\n}",
                "public class Repository<T> where T : class\n{\n    private readonly DbContext _context;\n    public Repository(DbContext context)\n    {\n        _context = context;\n    }\n}",
                "var filtered = items.Where(x => x.IsActive)\n                   .OrderBy(x => x.Date)\n                   .Select(x => x.Name)\n                   .ToList();",
                "public delegate void Notify(string message);\npublic event Notify ProcessCompleted;\nprotected virtual void OnProcessCompleted()\n{\n    ProcessCompleted?.Invoke(\"Done!\");\n}",
                "public static IEnumerable<int> GenerateSequence(int start, int count)\n{\n    for (int i = 0; i < count; i++)\n    {\n        yield return start + i;\n    }\n}",
                "public class Singleton\n{\n    private static readonly Lazy<Singleton> _lazy = new Lazy<Singleton>(() => new Singleton());\n    public static Singleton Instance => _lazy.Value;\n    private Singleton() { }\n}",
                "var query = from student in students\n            group student by student.Year into studentGroup\n            orderby studentGroup.Key\n            select studentGroup;",
                "public IConfiguration Configuration { get; }\npublic Startup(IConfiguration configuration)\n{\n    Configuration = configuration;\n}",
                "[HttpPost(\"upload\")]\npublic async Task<IActionResult> Upload(IFormFile file)\n{\n    if (file == null || file.Length == 0)\n        return BadRequest(\"No file\");\n    return Ok();\n}",
                "Action<int> print = x => Console.WriteLine(x);\nFunc<int, int, int> multiply = (x, y) => x * y;"
            ]
        },
        "JavaScript": {
            "Basic": [
                "console.log('Hello');",
                "const sum = (a, b) => a + b;",
                "let arr = [1, 2, 3];\narr.push(4);",
                "for (let i = 0; i < arr.length; i++) {\n  console.log(arr[i]);\n}",
                "document.getElementById('btn').addEventListener('click', () => alert('Clicked!'));",
                "const obj = { name: 'Alice', age: 25 };\nconsole.log(Object.keys(obj));",
                "let isDone = false;\nif (!isDone) {\n  console.log('Working...');\n}",
                "const name = prompt('What is your name?');\nconsole.log(`Hi, ${name}`);",
                "setTimeout(() => {\n  console.log('Delayed');\n}, 1000);",
                "const squares = [1, 2, 3].map(x => x * x);",
                "let text = \"JavaScript\";\nconsole.log(text.substring(0, 4));",
                "Math.random() * 100;",
                "const currentUrl = window.location.href;",
                "document.body.style.backgroundColor = 'red';",
                "let data = JSON.parse('{\"key\": \"value\"}');"
            ],
            "Hard": [
                "async function fetchUser(id) {\n  const response = await fetch(`/api/users/${id}`);\n  const data = await response.json();\n  return data;\n}",
                "class Animal {\n  constructor(name) {\n    this.name = name;\n  }\n  speak() {\n    console.log(`${this.name} makes a noise.`);\n  }\n}",
                "const processData = (data) => {\n  return data.filter(item => item.active)\n             .map(item => item.value * 2)\n             .reduce((acc, curr) => acc + curr, 0);\n};",
                "const debounce = (func, delay) => {\n  let timeoutId;\n  return (...args) => {\n    clearTimeout(timeoutId);\n    timeoutId = setTimeout(() => func(...args), delay);\n  };\n};",
                "Promise.all([fetch('/api/1'), fetch('/api/2')])\n  .then(responses => Promise.all(responses.map(r => r.json())))\n  .then(data => console.log(data))\n  .catch(err => console.error(err));",
                "const memoize = (fn) => {\n  const cache = {};\n  return (...args) => {\n    const key = JSON.stringify(args);\n    if (key in cache) return cache[key];\n    const result = fn(...args);\n    cache[key] = result;\n    return result;\n  };\n};",
                "function* idMaker() {\n  let index = 0;\n  while (true) yield index++;\n}",
                "const proxy = new Proxy({}, {\n  get: (target, prop) => {\n    return prop in target ? target[prop] : 42;\n  }\n});",
                "export default function useCustomHook() {\n  const [state, setState] = useState(0);\n  useEffect(() => {\n    document.title = `Count: ${state}`;\n  }, [state]);\n  return [state, setState];\n}",
                "const flatten = (arr) => arr.reduce((flat, next) => flat.concat(Array.isArray(next) ? flatten(next) : next), []);"
            ]
        },
        "HTML": {
            "Basic": [
                "<div>\n  <h1>Hello</h1>\n  <p>World</p>\n</div>",
                "<button onclick=\"alert('Hi')\">Click Me</button>",
                "<a href=\"https://example.com\">Link</a>",
                "<img src=\"logo.png\" alt=\"Logo\" width=\"100\" height=\"100\">",
                "<ul>\n  <li>Item 1</li>\n  <li>Item 2</li>\n</ul>",
                "<input type=\"text\" placeholder=\"Enter name\">",
                "<span style=\"color: red;\">Red text</span>",
                "<strong>Bold text</strong> and <em>Italic</em>",
                "<br>\n<hr>",
                "<title>My Page</title>",
                "<link rel=\"stylesheet\" href=\"style.css\">",
                "<script src=\"app.js\"></script>",
                "<footer>&copy; 2026 Company</footer>",
                "<nav>\n  <a href=\"#home\">Home</a>\n</nav>",
                "<label for=\"chk\">Accept:</label>\n<input type=\"checkbox\" id=\"chk\">"
            ],
            "Hard": [
                "<form action=\"/submit\" method=\"POST\">\n  <label for=\"email\">Email:</label>\n  <input type=\"email\" id=\"email\" required>\n  <button type=\"submit\">Send</button>\n</form>",
                "<div class=\"container\">\n  <header class=\"navbar\">\n    <nav>\n      <ul><li>Home</li></ul>\n    </nav>\n  </header>\n</div>",
                "<table class=\"data-table\">\n  <thead>\n    <tr><th>ID</th><th>Name</th></tr>\n  </thead>\n  <tbody>\n    <tr><td>1</td><td>Admin</td></tr>\n  </tbody>\n</table>",
                "<svg width=\"100\" height=\"100\">\n  <circle cx=\"50\" cy=\"50\" r=\"40\" stroke=\"green\" stroke-width=\"4\" fill=\"yellow\" />\n</svg>",
                "<canvas id=\"myCanvas\" width=\"200\" height=\"100\" style=\"border:1px solid #000000;\"></canvas>",
                "<audio controls>\n  <source src=\"audio.mp3\" type=\"audio/mpeg\">\n  Your browser does not support the audio element.\n</audio>",
                "<video width=\"320\" height=\"240\" controls>\n  <source src=\"movie.mp4\" type=\"video/mp4\">\n</video>",
                "<iframe src=\"https://example.com\" width=\"500\" height=\"200\"></iframe>",
                "<details>\n  <summary>More Info</summary>\n  <p>Here are the details you requested.</p>\n</details>",
                "<picture>\n  <source media=\"(min-width: 650px)\" srcset=\"img_large.jpg\">\n  <source media=\"(min-width: 465px)\" srcset=\"img_medium.jpg\">\n  <img src=\"img_small.jpg\" alt=\"Flowers\">\n</picture>"
            ]
        },
        "SQL": {
            "Basic": [
                "SELECT * FROM Users;",
                "UPDATE Users SET Active = 1 WHERE Id = 5;",
                "INSERT INTO Logs (Message) VALUES ('Error');",
                "DELETE FROM Cart WHERE SessionId = 'abc';",
                "SELECT Count(*) FROM Orders WHERE Status = 'Pending';",
                "SELECT Name, Email FROM Customers WHERE Country = 'BR';",
                "SELECT * FROM Products ORDER BY Price DESC;",
                "SELECT DISTINCT Category FROM Items;",
                "CREATE TABLE Test (Id INT, Name VARCHAR(50));",
                "DROP TABLE OldLogs;",
                "ALTER TABLE Users ADD Age INT;",
                "SELECT * FROM Orders WHERE Date > '2026-01-01';",
                "SELECT Name FROM Users WHERE Name LIKE 'A%';",
                "SELECT AVG(Salary) FROM Employees;",
                "TRUNCATE TABLE Cache;"
            ],
            "Hard": [
                "SELECT u.Name, COUNT(o.Id) AS OrderCount\nFROM Users u\nLEFT JOIN Orders o ON u.Id = o.UserId\nGROUP BY u.Name\nHAVING COUNT(o.Id) > 5;",
                "BEGIN TRANSACTION;\n  UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;\n  UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2;\nCOMMIT;",
                "WITH RankedEmployees AS (\n  SELECT Id, Name, Salary, ROW_NUMBER() OVER(PARTITION BY DepartmentId ORDER BY Salary DESC) as Rank\n  FROM Employees\n)\nSELECT * FROM RankedEmployees WHERE Rank = 1;",
                "CREATE NONCLUSTERED INDEX IX_Users_Email ON Users(Email) INCLUDE (FirstName, LastName);",
                "MERGE INTO TargetTable AS t\nUSING SourceTable AS s\nON t.Id = s.Id\nWHEN MATCHED THEN\n  UPDATE SET t.Name = s.Name\nWHEN NOT MATCHED THEN\n  INSERT (Id, Name) VALUES (s.Id, s.Name);",
                "SELECT ProductId,\n  SUM(CASE WHEN Month = 1 THEN Sales ELSE 0 END) AS JanSales,\n  SUM(CASE WHEN Month = 2 THEN Sales ELSE 0 END) AS FebSales\nFROM MonthlySales\nGROUP BY ProductId;",
                "CREATE TRIGGER trg_AfterInsert ON Users\nAFTER INSERT\nAS\nBEGIN\n  INSERT INTO AuditLog (UserId, Action) SELECT Id, 'CREATED' FROM inserted;\nEND;",
                "SELECT * FROM Orders\nWHERE CustomerId IN (SELECT Id FROM Customers WHERE CreditScore > 700);",
                "DECLARE @CurrentDate DATETIME = GETDATE();\nPRINT @CurrentDate;",
                "IF EXISTS (SELECT * FROM sys.objects WHERE name = 'MyTable')\n  DROP TABLE MyTable;"
            ]
        },
        "C++": {
            "Basic": [
                "#include <iostream>\n\nint main() {\n    std::cout << \"Hello, World!\\n\";\n    return 0;\n}",
                "int a = 5;\nint b = 10;\nint sum = a + b;",
                "for (int i = 0; i < 5; ++i) {\n    std::cout << i << std::endl;\n}",
                "int arr[5] = {1, 2, 3, 4, 5};",
                "int* ptr = &a;",
                "std::string name = \"C++\";\nstd::cout << name.length();",
                "if (a > b) {\n    std::cout << \"A is greater\";\n}",
                "while (x > 0) {\n    x--;\n}",
                "#include <vector>\nstd::vector<int> v;",
                "v.push_back(10);",
                "std::cin >> input;",
                "const float PI = 3.14159f;",
                "double divide(double x, double y) {\n    return x / y;\n}",
                "enum Color { RED, GREEN, BLUE };",
                "struct Point {\n    int x;\n    int y;\n};"
            ],
            "Hard": [
                "template <typename T>\nT max(T a, T b) {\n    return (a > b) ? a : b;\n}",
                "class Box {\nprivate:\n    double length;\npublic:\n    Box(double l) : length(l) {}\n    double getVolume() { return length * length * length; }\n};",
                "std::vector<int> v = {1, 2, 3};\nstd::transform(v.begin(), v.end(), v.begin(), [](int x) { return x * 2; });",
                "std::unique_ptr<int> p(new int(10));\nstd::cout << *p;",
                "class Base {\npublic:\n    virtual void show() = 0;\n    virtual ~Base() {}\n};",
                "std::map<std::string, int> ages;\nages[\"Alice\"] = 30;",
                "std::thread t([]() {\n    std::cout << \"Thread running\\n\";\n});\nt.join();",
                "std::mutex mtx;\nstd::lock_guard<std::mutex> lock(mtx);\nshared_var++;",
                "constexpr int factorial(int n) {\n    return n <= 1 ? 1 : (n * factorial(n - 1));\n}",
                "try {\n    throw std::runtime_error(\"Error occurred\");\n} catch (const std::exception& e) {\n    std::cerr << e.what();\n}"
            ]
        },
        "Java": {
            "Basic": [
                "public class Main {\n    public static void main(String[] args) {\n        System.out.println(\"Hello, World!\");\n    }\n}",
                "int[] numbers = {1, 2, 3};\nfor(int n : numbers) {\n    System.out.println(n);\n}",
                "String text = \"Java\";\nSystem.out.println(text.length());",
                "int sum = a + b;",
                "boolean isJavaFun = true;",
                "if (20 > 18) {\n    System.out.println(\"20 is greater than 18\");\n}",
                "while (i < 5) {\n    System.out.println(i);\n    i++;\n}",
                "ArrayList<String> cars = new ArrayList<String>();",
                "cars.add(\"Volvo\");",
                "HashMap<String, String> capitalCities = new HashMap<String, String>();",
                "Scanner myObj = new Scanner(System.in);\nString userName = myObj.nextLine();",
                "try {\n    int[] myNumbers = {1, 2, 3};\n    System.out.println(myNumbers[10]);\n} catch (Exception e) {\n    System.out.println(\"Something went wrong.\");\n}",
                "public int add(int x, int y) {\n    return x + y;\n}",
                "Math.max(5, 10);",
                "final int MAX_AGE = 100;"
            ],
            "Hard": [
                "public interface Drawable {\n    void draw();\n}\n\npublic class Circle implements Drawable {\n    public void draw() {\n        System.out.println(\"Drawing Circle\");\n    }\n}",
                "List<String> filtered = names.stream()\n    .filter(n -> n.startsWith(\"A\"))\n    .map(String::toUpperCase)\n    .collect(Collectors.toList());",
                "CompletableFuture.supplyAsync(() -> performHeavyTask())\n    .thenAccept(result -> processResult(result));",
                "public class Singleton {\n    private static volatile Singleton instance;\n    private Singleton() {}\n    public static Singleton getInstance() {\n        if (instance == null) {\n            synchronized (Singleton.class) {\n                if (instance == null) instance = new Singleton();\n            }\n        }\n        return instance;\n    }\n}",
                "try (BufferedReader br = new BufferedReader(new FileReader(path))) {\n    return br.readLine();\n}",
                "ExecutorService executor = Executors.newFixedThreadPool(10);\nexecutor.submit(() -> {\n    System.out.println(\"Task executed\");\n});\nexecutor.shutdown();",
                "Optional<String> optional = Optional.ofNullable(name);\noptional.ifPresent(System.out::println);",
                "@RestController\n@RequestMapping(\"/api\")\npublic class UserController {\n    @GetMapping(\"/users\")\n    public List<User> getUsers() { return userService.findAll(); }\n}",
                "Pattern pattern = Pattern.compile(\"w3schools\", Pattern.CASE_INSENSITIVE);\nMatcher matcher = pattern.matcher(\"Visit W3Schools!\");",
                "public <T> void printArray(T[] inputArray) {\n    for (T element : inputArray) {\n        System.out.printf(\"%s \", element);\n    }\n}"
            ]
        },
        "Go": {
            "Basic": [
                "package main\n\nimport \"fmt\"\n\nfunc main() {\n    fmt.Println(\"Hello, World!\")\n}",
                "for i := 0; i < 5; i++ {\n    fmt.Println(i)\n}",
                "slice := []int{1, 2, 3}\nslice = append(slice, 4)",
                "var a int = 10",
                "name := \"GoLang\"",
                "if x > 10 {\n    fmt.Println(\"Greater\")\n}",
                "m := make(map[string]int)\nm[\"k1\"] = 7",
                "func add(x int, y int) int {\n    return x + y\n}",
                "_, err := os.Open(\"file.txt\")",
                "defer fmt.Println(\"World\")",
                "fmt.Printf(\"Type: %T\\n\", v)",
                "p := &i\n*p = 21",
                "type Vertex struct {\n    X int\n    Y int\n}",
                "v := Vertex{1, 2}",
                "switch os := runtime.GOOS; os {\ncase \"darwin\":\n    fmt.Println(\"OS X.\")\n}"
            ],
            "Hard": [
                "func worker(id int, jobs <-chan int, results chan<- int) {\n    for j := range jobs {\n        fmt.Println(\"worker\", id, \"processing job\", j)\n        results <- j * 2\n    }\n}",
                "type Shape interface {\n    Area() float64\n}\ntype Circle struct {\n    Radius float64\n}\nfunc (c Circle) Area() float64 {\n    return math.Pi * c.Radius * c.Radius\n}",
                "mutex.Lock()\ndefer mutex.Unlock()\nsharedResource++",
                "go func(msg string) {\n    fmt.Println(msg)\n}(\"going\")",
                "select {\ncase msg1 := <-c1:\n    fmt.Println(\"received\", msg1)\ncase msg2 := <-c2:\n    fmt.Println(\"received\", msg2)\ncase <-time.After(1 * time.Second):\n    fmt.Println(\"timeout\")\n}",
                "file, err := os.Open(\"file.txt\")\nif err != nil {\n    log.Fatal(err)\n}\ndefer file.Close()",
                "http.HandleFunc(\"/\", func(w http.ResponseWriter, r *http.Request) {\n    fmt.Fprintf(w, \"Welcome to my website!\")\n})\nhttp.ListenAndServe(\":80\", nil)",
                "func factorial(n int) int {\n    if n == 0 {\n        return 1\n    }\n    return n * factorial(n-1)\n}",
                "ctx, cancel := context.WithTimeout(context.Background(), 1*time.Second)\ndefer cancel()",
                "var wg sync.WaitGroup\nwg.Add(1)\ngo func() {\n    defer wg.Done()\n    doWork()\n}()\nwg.Wait()"
            ]
        },
        "Rust": {
            "Basic": [
                "fn main() {\n    println!(\"Hello, World!\");\n}",
                "let mut x = 5;\nx += 1;",
                "for i in 0..5 {\n    println!(\"{}\", i);\n}",
                "let y = 10;",
                "let is_active = true;",
                "if number < 5 {\n    println!(\"condition was true\");\n}",
                "let a = [1, 2, 3, 4, 5];",
                "fn add(x: i32, y: i32) -> i32 {\n    x + y\n}",
                "let s = String::from(\"hello\");",
                "println!(\"Length: {}\", s.len());",
                "struct User {\n    username: String,\n    email: String,\n}",
                "enum IpAddrKind {\n    V4,\n    V6,\n}",
                "let v = vec![1, 2, 3];",
                "v.push(4);",
                "let slice = &a[1..3];"
            ],
            "Hard": [
                "impl Rectangle {\n    fn area(&self) -> u32 {\n        self.width * self.height\n    }\n}",
                "fn longest<'a>(x: &'a str, y: &'a str) -> &'a str {\n    if x.len() > y.len() {\n        x\n    } else {\n        y\n    }\n}",
                "let result: Result<i32, ParseIntError> = \"42\".parse();\nmatch result {\n    Ok(n) => println!(\"Parsed: {}\", n),\n    Err(e) => println!(\"Error: {}\", e),\n}",
                "use std::thread;\nuse std::time::Duration;\nthread::spawn(|| {\n    for i in 1..10 {\n        println!(\"hi number {} from the spawned thread!\", i);\n        thread::sleep(Duration::from_millis(1));\n    }\n});",
                "use std::sync::mpsc;\nlet (tx, rx) = mpsc::channel();\nthread::spawn(move || {\n    let val = String::from(\"hi\");\n    tx.send(val).unwrap();\n});",
                "let closure = |z: i32| z + 1;\nlet y = closure(1);",
                "pub trait Summary {\n    fn summarize(&self) -> String;\n}",
                "let s1 = String::from(\"hello\");\nlet s2 = s1;\n// s1 is no longer valid here",
                "use std::collections::HashMap;\nlet mut scores = HashMap::new();\nscores.insert(String::from(\"Blue\"), 10);",
                "macro_rules! my_vec {\n    ( $( $x:expr ),* ) => {\n        {\n            let mut temp_vec = Vec::new();\n            $(\n                temp_vec.push($x);\n            )*\n            temp_vec\n        }\n    };\n}"
            ]
        }
    }
}

with open('C:\\Users\\thall\\Desktop\\TypeEnigma\\texts_fallback.json', 'w', encoding='utf-8') as f:
    json.dump(data, f, ensure_ascii=False, indent=2)

print("Gerado com sucesso, com muito mais opções de código!")
