using System;
using System.Data;
using System.Collections.Generic;

namespace курсач
{
    class BinaryTreeElement{
        public int key;
        public string formula;
        public List<dynamic> childrens = new List<dynamic>();
        public double value = 0;
        public BinaryTreeElement(int _key, string _formula, List<dynamic> _childrens = null)
        {
            this.key = _key;
            this.formula = _formula;
			Console.WriteLine(_childrens);
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
            int index = childrens.FindIndex(item=>item.key==key_child);
            if(index>0){
                Console.WriteLine("Удалён узел!");
				childrens = childrens.FindAll(item => item.key != index);
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
                Console.WriteLine("Применена формула: "+math);
                Console.WriteLine("Результат: "+value);
                return Convert.ToDouble(value);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Не корректная формула: "+formula.Replace("$", data.ToString()));
                return data;
            }
            
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
			
			Console.WriteLine();
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
				Console.WriteLine(subtree.calculate(value));
				}
			catch(System.Exception){
				Console.WriteLine("Не удалось выполнить функцию!");
			}
			
        }
    }
}