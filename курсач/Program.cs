using System;
using System.Data;
using System.Collections.Generic;
namespace курсач
{
    class Methods{
        public static void findTree(BinaryTreeElement tree){
            double value = 0;
            try{
                Console.WriteLine("Введите число: ");
                value = Convert.ToDouble(Console.ReadLine());
            }
            catch(System.Exception){
                Console.WriteLine("Неверно указанно число");
            }
            Console.WriteLine("Введите ключ: ");
            string key = Convert.ToString(Console.ReadLine());
            string[] keys = key.Split(">");
            var subtree = tree;
            try{
                foreach (string k in keys){
                    subtree = subtree.find(Convert.ToInt32(k));
                }
                subtree.calculate(value);
            }
            catch(System.Exception){
                Console.WriteLine("Не удалось выполнить функцию!");
            }
        }
        public static BinaryTreeElement addTree(BinaryTreeElement tree){
            Console.WriteLine("Введите ключ: ");
            string key = Convert.ToString(Console.ReadLine());
            string[] keys = key.Split(">");
            var subtree = tree;
            try{
                int last_ind = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if(i==keys.Length-1){
                        last_ind = Convert.ToInt32(keys[i]);
                        break;
                    }
                    subtree = subtree.find(Convert.ToInt32(keys[i]));
                }
                Console.WriteLine("Введите ключ новой ветви: ");
                int key_tree = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите формулу новой ветви(Вводимое число обозначается знаком '$'): ");
                string formula = Convert.ToString(Console.ReadLine());
                subtree.add(new BinaryTreeElement(key_tree,formula));
            }
            catch(System.Exception){
                Console.WriteLine("Не удалось выполнить функцию!");
            }
            return tree;
        }
        public static BinaryTreeElement removeTree(BinaryTreeElement tree){
            Console.WriteLine("Введите ключ: ");
            string key = Convert.ToString(Console.ReadLine());
            string[] keys = key.Split(">");
            var subtree = tree;
            try{
                int last_ind = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if(i==keys.Length-1){
                        last_ind = Convert.ToInt32(keys[i]);
                        break;
                    }
                    subtree = subtree.find(Convert.ToInt32(keys[i]));
                }
                subtree.remove(last_ind);
            }
            catch(System.Exception){
                Console.WriteLine("Не удалось выполнить функцию!");
            }
            return tree;
        }

        public static string EnterTree(BinaryTreeElement tree){
            Console.WriteLine("0 Прервать программу");
            Console.WriteLine("1 Вывести дерево");
            Console.WriteLine("2 Найти ветвь");
            Console.WriteLine("3 Добавить ветвь");
            Console.WriteLine("4 Удалить ветвь");
            string enter = Convert.ToString(Console.ReadLine());
            return enter;
        }
    }
    
    class BinaryTreeElement{
        public int key;
        public string formula;
        public List<dynamic> childrens = new List<dynamic>();
        public double value = 0;

        public BinaryTreeElement(int _key, string _formula, List<dynamic> _childrens = null)
        {
            this.key = _key;
            this.formula = _formula;
			this.childrens = _childrens ?? new List<dynamic>();
        }
        public bool add(BinaryTreeElement child){
            bool has_child = childrens.Exists(item=>item.key==child.key);
            if(has_child){
                Console.WriteLine("Текущий узел существует!");
                return false;
            } else{
                Console.WriteLine("Добавлен узел!");
                childrens.Add(child);
                return true;
            }
        }
        public bool remove(int key_child){
            bool has_child = childrens.Exists(item=>item.key==key_child);
            if(has_child){
                BinaryTreeElement child = childrens.Find(item=>item.key==key_child);
                Console.WriteLine("Удалён узел!");
				childrens = childrens.FindAll(item => item.key != child.key);
                return true;
            } else{
                Console.WriteLine("Данный узел отсутствует!");
                return false;
            }
        }
        public BinaryTreeElement? find(int key_child){
            BinaryTreeElement? child = this.childrens.Find(item => item.key == key_child);
            if(child!=null){
                Console.WriteLine("Найден узел: " + this.formula);
                return child;
            } else{
                Console.WriteLine("Данный узел не существует!");
                return null;
            }
        }
        public double calculate(double data){
            try
            {
                string math = formula.Replace("$", data.ToString());
                string value = new DataTable().Compute(math, null).ToString();
                Console.WriteLine("Применён узел: \n"+this.showTree());
                Console.WriteLine("Применена формула: "+math+"="+value);
                return Convert.ToDouble(value);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Не корректная формула: "+formula.Replace("$", data.ToString()));
                return data;
            }
        }
        public string showTree(){
            return "key {"+this.key+"} --------> "+this.formula+
            "\n"+"childrens"+" --------> "+this.childrens.Count;
        }
        public override string ToString()
        {
            string result = "{";
            foreach (var children in this.childrens)
            {
                result += "\n\t"+children.key+": {\n\t"+children+"\n\t}";
            }
            if(this.childrens!=null&&this.childrens.Count!=0){
                result += "\n}";
            } else{
                result += "}";
            }
            
            return "key {"+this.key+"} --------> "+this.formula+
            "\n"+"childrens = "+this.childrens.Count+" --------> "+result;
        }
        
    }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            var tree = new BinaryTreeElement(1,"$+2",
				new List<dynamic>() {
					new BinaryTreeElement(2,"$*3",
						new List<dynamic>() {
							new BinaryTreeElement(4,"$/2"),
							new BinaryTreeElement(5,"$+13"),
						}
				),
					new BinaryTreeElement(3,"$-1",
						new List<dynamic>() {
							new BinaryTreeElement(6,"$+7"),
							new BinaryTreeElement(7,"$-4"),
						}
					)
				}
			);
			while (true)
            {
                string enter = Methods.EnterTree(tree);
                if(enter=="0"){
                    break;
                }
                switch(enter)
                {
                    case "1":
                        Console.WriteLine(tree);
                        break;
                    case "2":
                        Methods.findTree(tree);
                        break;
                    case "3":
                        Methods.addTree(tree);
                        break;
                    case "4":
                        Methods.removeTree(tree);
                        break;
                    default:
                        Console.WriteLine("Не верная команда");
                        break;
                }
            }
        }
        
    }
}