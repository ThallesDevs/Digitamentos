import json
import os
import random

words = {
    "func": ["process", "calculate", "fetch", "execute", "run", "update", "delete", "create", "read", "write", "parse", "build", "analyze", "validate", "render"],
    "arg": ["x", "y", "index", "count", "total", "result", "item", "node", "data", "value", "key", "param", "obj", "info", "id", "session", "user"],
    "var": ["items", "elements", "users", "records", "lines", "keys", "values", "config", "settings", "buffer", "cache", "list", "array", "matrix"],
    "val": ["0", "1", "2", "10", "42", "100", "256", "1024", "-1", "99", "3", "5", "8", "16", "32", "64", "128", "2048", "999"],
    "str": ["Success", "Error", "Starting", "Done", "Finished", "Loading", "Ready", "Failed", "Empty", "Valid", "Invalid", "Warning", "Critical"],
    "Class": ["User", "Manager", "Service", "Controller", "Model", "Handler", "Parser", "Factory", "Builder", "Provider", "Repository", "Adapter"]
}

prose_words = {
    "subject": ["A paciência", "A coragem", "A sabedoria", "A persistência", "O conhecimento", "A determinação", "A imaginação", "O esforço", "A disciplina", "O foco", "A resiliência", "A educação", "A criatividade"],
    "verb": ["conquista", "supera", "domina", "alcança", "realiza", "transforma", "constrói", "vence", "ilumina", "quebra", "desafia", "revela"],
    "object": ["o impossível", "o medo", "o desafio", "o mundo", "o sucesso", "os obstáculos", "as barreiras", "as dificuldades", "o futuro", "os limites", "as expectativas"],
    "condition": ["todos os dias.", "quando menos esperamos.", "com o tempo.", "na adversidade.", "sempre que tentamos.", "apesar de tudo.", "passo a passo.", "no silêncio."]
}

templates = {
    "Python": {
        "Basic": [
            "def {func}({arg}):\n    return {arg} + {val}",
            "for {arg} in {var}:\n    print({arg})",
            "{var} = [{arg} for {arg} in range({val})]",
            "if {arg} > {val}:\n    {func}({arg})",
            "{arg} = {val}\nprint(f'{str}: {{{arg}}}')"
        ],
        "Hard": [
            "class {Class}:\n    def __init__(self, {arg}):\n        self.{arg} = {arg}\n    def {func}(self):\n        return self.{arg}",
            "try:\n    {var} = {func}({val})\nexcept Exception as e:\n    print(f\"{str}: {{e}}\")",
            "def {func}(*args, **kwargs):\n    for {arg} in args:\n        print({arg})",
            "async def {func}_{arg}(url):\n    async with session.get(url) as resp:\n        return await resp.json()",
            "import {var}\ndef {func}():\n    return {var}.{func}_{arg}({val})"
        ]
    },
    "C#": {
        "Basic": [
            "int {arg} = {val};\nConsole.WriteLine({arg});",
            "var {var} = new List<string>();\n{var}.Add(\"{str}\");",
            "foreach (var {arg} in {var})\n{{\n    {func}({arg});\n}}",
            "if ({arg} == {val})\n{{\n    return true;\n}}",
            "public string {Class}Name {{ get; set; }}"
        ],
        "Hard": [
            "public async Task<{Class}> {func}Async(int {arg})\n{{\n    return await _context.{var}.FindAsync({arg});\n}}",
            "public class {Class} : I{Class}\n{{\n    public void {func}()\n    {{\n        Console.WriteLine(\"{str}\");\n    }}\n}}",
            "var result = {var}.Where({arg} => {arg}.Id == {val})\n                  .Select({arg} => {arg}.Name)\n                  .ToList();",
            "try\n{{\n    await {func}Async();\n}}\ncatch (Exception ex)\n{{\n    _logger.LogError(ex, \"{str}\");\n}}"
        ]
    },
    "JavaScript": {
        "Basic": [
            "const {arg} = {val};\nconsole.log({arg});",
            "let {var}_new = {var}.map(x => x * {val});",
            "function {func}({arg}) {{\n  return {arg} + {val};\n}}",
            "const obj = {{ {arg}: {val}, status: '{str}' }};",
            "setTimeout(() => {func}({arg}), {val}00);"
        ],
        "Hard": [
            "async function {func}() {{\n  const res = await fetch('/api/{var}');\n  const data = await res.json();\n  return data;\n}}",
            "class {Class} extends Base {{\n  constructor({arg}) {{\n    super();\n    this.{arg} = {arg};\n  }}\n}}",
            "const {func} = ({arg}) => {{\n  return new Promise((resolve, reject) => {{\n    if({arg}) resolve('{str}');\n    else reject('Error');\n  }});\n}};"
        ]
    },
    "HTML": {
        "Basic": [
            "<div id=\"{var}\">\n  <p>{str}</p>\n</div>",
            "<button onclick=\"{func}('{str}')\">{str}</button>",
            "<img src=\"{var}.png\" alt=\"{str}\" />",
            "<input type=\"text\" name=\"{arg}\" value=\"{val}\" />",
            "<span class=\"{arg}-class\">{str}</span>"
        ],
        "Hard": [
            "<form action=\"/{func}\" method=\"POST\">\n  <label>{str}</label>\n  <input type=\"text\" name=\"{arg}\">\n  <button type=\"submit\">{str}</button>\n</form>",
            "<table class=\"{var}\">\n  <thead>\n    <tr><th>{Class}</th></tr>\n  </thead>\n  <tbody>\n    <tr><td>{val}</td></tr>\n  </tbody>\n</table>",
            "<div class=\"card\">\n  <div class=\"card-header\">{Class}</div>\n  <div class=\"card-body\">{str}</div>\n</div>"
        ]
    },
    "SQL": {
        "Basic": [
            "SELECT {arg} FROM {var} WHERE {arg} > {val};",
            "UPDATE {var} SET {arg} = {val} WHERE id = {val};",
            "INSERT INTO {var} ({arg}) VALUES ({val});",
            "DELETE FROM {var} WHERE {arg} = {val};",
            "SELECT COUNT(*) FROM {var};"
        ],
        "Hard": [
            "SELECT {arg}, COUNT(*) \nFROM {var}\nGROUP BY {arg}\nHAVING COUNT(*) > {val};",
            "WITH CTE AS (\n  SELECT {arg} FROM {var} WHERE id = {val}\n)\nSELECT * FROM CTE;",
            "SELECT a.{arg}, b.{arg}\nFROM {var} a\nINNER JOIN {var}_details b ON a.id = b.a_id\nWHERE a.status = '{str}';"
        ]
    },
    "C++": {
        "Basic": [
            "int {arg} = {val};\nstd::cout << {arg} << std::endl;",
            "for(int i=0; i<{val}; i++) {{\n    {func}(i);\n}}",
            "std::string {arg} = \"{str}\";",
            "if ({arg} == {val}) return;",
            "float {var}_val = {val}.5f;"
        ],
        "Hard": [
            "class {Class} {{\nprivate:\n    int {arg};\npublic:\n    {Class}(int x) : {arg}(x) {{}}\n}};",
            "std::vector<int> {var};\n{var}.push_back({val});\nstd::sort({var}.begin(), {var}.end());",
            "std::shared_ptr<{Class}> ptr = std::make_shared<{Class}>({val});\nptr->{func}();"
        ]
    },
    "Java": {
        "Basic": [
            "int {arg} = {val};\nSystem.out.println({arg});",
            "String {var} = \"{str}\";\n{var}.length();",
            "for (int i = 0; i < {val}; i++) {{\n    {func}();\n}}",
            "boolean is{str} = true;",
            "double {arg}Val = {val}.0;"
        ],
        "Hard": [
            "public class {Class} {{\n    private int {arg};\n    public {Class}(int {arg}) {{\n        this.{arg} = {arg};\n    }}\n}}",
            "List<String> {var} = new ArrayList<>();\n{var}.add(\"{str}\");\nCollections.sort({var});",
            "public {Class} {func}() throws Exception {{\n    if ({arg} == null) throw new Exception(\"{str}\");\n    return new {Class}();\n}}"
        ]
    },
    "Go": {
        "Basic": [
            "var {arg} int = {val}\nfmt.Println({arg})",
            "for i := 0; i < {val}; i++ {{\n    {func}(i)\n}}",
            "{arg} := \"{str}\"",
            "if {arg} != nil {{\n    return {arg}\n}}",
            "fmt.Printf(\"Value: %d\\n\", {val})"
        ],
        "Hard": [
            "type {Class} struct {{\n    {arg} int\n    {var} string\n}}",
            "func {func}(ch chan int) {{\n    for v := range ch {{\n        fmt.Println(v)\n    }}\n}}",
            "func (c *{Class}) {func}() error {{\n    if c.{arg} == 0 {{\n        return errors.New(\"{str}\")\n    }}\n    return nil\n}}"
        ]
    },
    "Rust": {
        "Basic": [
            "let mut {arg} = {val};\nprintln!(\"{{}}\", {arg});",
            "for i in 0..{val} {{\n    {func}(i);\n}}",
            "let {var} = String::from(\"{str}\");",
            "match {arg} {{\n    {val} => true,\n    _ => false,\n}}",
            "let {arg}: i32 = {val};"
        ],
        "Hard": [
            "struct {Class} {{\n    {arg}: i32,\n    {var}: String,\n}}",
            "impl {Class} {{\n    fn new({arg}: i32) -> Self {{\n        {Class} {{ {arg}, {var}: String::new() }}\n    }}\n}}",
            "pub fn {func}({arg}: &str) -> Result<String, io::Error> {{\n    let mut f = File::open({arg})?;\n    Ok(String::from(\"{str}\"))\n}}"
        ]
    }
}

file_path = "texts_fallback.json"
if os.path.exists(file_path):
    with open(file_path, "r", encoding="utf-8") as f:
        data = json.load(f)
else:
    data = {"Prose": [], "Code": {}}

# Generate Prose until we have at least 500
prose_templates = [
    "{subject} {verb} {object} {condition}",
    "{subject} {verb} {object}.",
    "Sempre que {subject} {verb} {object}, a vida melhora.",
    "Acredite: {subject} {verb} {object}."
]

attempts = 0
while len(data["Prose"]) < 500 and attempts < 10000:
    pt = random.choice(prose_templates)
    sentence = pt.format(
        subject=random.choice(prose_words["subject"]),
        verb=random.choice(prose_words["verb"]),
        object=random.choice(prose_words["object"]),
        condition=random.choice(prose_words["condition"])
    )
    if sentence not in data["Prose"]:
        data["Prose"].append(sentence)
    attempts += 1

# Generate Code snippets until we have at least 300 per language/category
for lang, categories in templates.items():
    if lang not in data.setdefault("Code", {}):
        data["Code"][lang] = {"Basic": [], "Hard": []}
    
    for category, tpl_list in categories.items():
        if category not in data["Code"][lang]:
            data["Code"][lang][category] = []
            
        current_list = data["Code"][lang][category]
        
        attempts = 0
        while len(current_list) < 300 and attempts < 20000:
            tpl = random.choice(tpl_list)
            snippet = tpl.format(
                func=random.choice(words["func"]),
                arg=random.choice(words["arg"]),
                var=random.choice(words["var"]),
                val=random.choice(words["val"]),
                str=random.choice(words["str"]),
                Class=random.choice(words["Class"])
            )
            if snippet not in current_list:
                current_list.append(snippet)
            attempts += 1

with open(file_path, "w", encoding="utf-8") as f:
    json.dump(data, f, ensure_ascii=False, indent=2)

print(f"Generated massively! Prose count: {len(data['Prose'])}")
