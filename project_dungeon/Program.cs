using System.Numerics;

namespace project_dungeon   // 프로젝트 네임스페이스
{
    internal class Program  //Program 클래서 정의
    {
        private static Character player; // 플레이어 변수 선언 - 플레이어의 정보를 저장할 객체

        static void Main(string[] args)  // 프로그램 메인 화면 (진입점)
        {
            GameDataSetting();  // 게임 데이터 세팅
            DisplayGameIntro();  // 게임 인트로 표시
            DisplayMyInfo(); // 내 상태 창
            InventorySetting(); // 장비착용
            ShopSetting(); // 상점
        }

        static void GameDataSetting()  // 게임 데이터 세팅
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);  // 플레이어 정보 초기화

            Item[] items = new Item[]  // 아이템 정보 배열 활용
            {
                 new Item("무쇠갑옷", "방어", 5, 200),
                 new Item("낡은 검", "공격", 2, 150),
                 new Item("가죽 샌들", "방어", 2, 100),
                 new Item("금 목걸이", "체력", 25, 300),


            };
                        // 아이템을 플레이어의 인벤토리에 추가하세요
            player.Inventory.AddRange(items);
            
        }


        static List<Item> shopInventory = new List<Item>
        {
            new Item("강철 갑옷", "방어", 8, 300),    // 상점 아이템
            new Item("대검", "공격", 5, 250),
            new Item("철 철퇴", "공격", 6, 280),
            new Item("은 목걸이", "체력", 15, 150),
            new Item("동 목걸이", "체력", 5, 100)

        };


        static void DisplayGameIntro()  // 게임 인트로 디스플레이
        {
            Console.Clear();  // 콘솔화면 클리어
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);  // 1 혹은 2를 사용자로부터 입력받는 메서드

            switch (input)  // switch문 (1혹은 2를 선택)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    InventorySetting();
                    break;
                case 3:
                    ShopSetting();
                    break;


            }
        }




        static void DisplayMyInfo()  // 플레이어 상태 표시 메서드
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);  

            switch (input)
            {
                case 0:
                    DisplayGameIntro();  // 0을 입력하면 게임 인트로를 다시 보여주기
                    break;
            }
        }



        static void InventorySetting()  // 장착관리 표시
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("인벤토리");
            Console.WriteLine("장착 혹은 장착 해제할 아이템의 숫자를 입력하세요.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("[아이템 목록]");
            int itemIndex = 1;
            foreach (var item in player.Inventory)  // for each 루프 - 각 요소를 반복적으로 처리. (player.Inventory)의 (현재는 1~4아이템) 리스트를 순회하며 아이템 정보들을 출력함.)
            {
                string equippedText = item.IsEquipped ? "[장착중] " : "";  // 아이템의 isEquipped 속성을 확인해, 장착중인지 아닌지 체크. 장착중이라면 장착중 문자열을 아래 {equippedText}에 할당.
                Console.WriteLine($"{itemIndex}. {item.Name} | {item.Type} +{item.StatBonus} {equippedText}"); // 출력할 내용의 형식.
                itemIndex++;  // 아이템을 순회하면서 0~3번째까지 리스트를 출력.
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine("1. 장착");
            Console.WriteLine("2. 장착 해제");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 2);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    EquipItem();
                    break;

                case 2:
                    UnequipItem();
                    break;
            }
        }





        static void ShopSetting() // 상점
        {

            Console.Clear();  // 콘솔화면 클리어
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("상점");
            Console.WriteLine("구매 혹은 판매 할수 있는 아이템의 목록입니다.");
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("[구매 가능한 아이템 목록]");
            int itemIndex = 1;
            foreach (var item in shopInventory) 
            {
                Console.WriteLine($"{itemIndex}. {item.Name} | {item.Type} +{item.StatBonus} | 가격: {item.Price} G");
                itemIndex++;  // 아이템을 순회하면서 리스트를 출력.
            }
            Console.WriteLine();

            Console.WriteLine("[판매 가능한 아이템 목록]");
            itemIndex = 1;
            foreach (var item in player.Inventory)
            {
                string equippedText = item.IsEquipped ? "[장착중] " : ""; 
                Console.WriteLine($"{itemIndex}. {item.Name} | {item.Type} +{item.StatBonus} | 가격: {item.Price} G {equippedText}");
                itemIndex++;  // 아이템을 순회하면서 리스트를 출력.
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.WriteLine("1. 구매");
            Console.WriteLine("2. 판매");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 2);

            switch (input)
            {
                case 0:
                    DisplayGameIntro();  // 0을 입력하면 게임 인트로를 다시 보여주기
                    break;

                case 1:
                    BuyItem();
                    break;

                case 2:
                    SellItem();
                    break;

            }
        }








        static void EquipItem()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("장착할 아이템의 번호를 입력해주세요");
            Console.WriteLine("(0. 나가기)");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                InventorySetting(); // 0을 입력하면 인벤토리 화면으로 돌아갑니다.
                return; // 함수를 종료합니다.
            }

            Item selected = player.Inventory[input - 1 ];

            // 이미 장착 중인 경우
            if (selected.IsEquipped)
            {
                Console.WriteLine($"{selected.Name}은 이미 장착 중입니다.");
                Console.ReadLine();
            }
            else
            {
                selected.Equip(player); // 아이템 장착
                //Console.WriteLine($"{selected.Name}을(를) 장착하였습니다.");
                Console.ReadLine();
            }

            InventorySetting();

        }





        static void UnequipItem()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("장착 해제할 아이템의 번호를 입력해주세요");
            Console.WriteLine("(0. 나가기)");

            int input = CheckValidInput(0, player.Inventory.Count);

            if (input == 0)
            {
                InventorySetting(); // 0을 입력하면 인벤토리 화면으로 돌아갑니다.
                return; // 함수를 종료합니다.
            }

            Item selected = player.Inventory[input - 1];


            if (selected.IsEquipped)
            {
                selected.Unequip(player);
                Console.WriteLine($"{selected.Name}의 장착을 해제하였습니다.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"{selected.Name}은(는) 이미 장착 해제되어 있습니다.");
                Console.ReadLine();
            }

            InventorySetting();
        }



        static void BuyItem()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("구매할 아이템의 번호를 입력해주세요");
            Console.WriteLine("(0. 나가기)");

            int input = CheckValidInput(0, shopInventory.Count);
            if (input == 0)
            {
                ShopSetting(); // 0을 입력하면 인벤토리 화면으로 돌아갑니다.
                return; // 함수를 종료합니다.
            }

            Item selected = shopInventory[input - 1];

            if (player.Gold >= selected.Price)
            {
                player.Gold -= selected.Price;
                player.Inventory.Add(selected);  // 구매한 아이템을 플레이어의 인벤토리에 추가합니다
                shopInventory.Remove(selected);  // 상점 인벤토리에서 아이템 제거
                Console.WriteLine($"{selected.Name}을(를) 구매하였습니다.");
            }
            else
            {
                Console.WriteLine("소지금이 부족합니다.");
            }

            Console.ReadLine();
            ShopSetting();

        }





        static void SellItem()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("판매할 아이템의 번호를 입력해주세요");
            Console.WriteLine("(0. 나가기)");

            int input = CheckValidInput(0, player.Inventory.Count);
            if (input == 0)
            {
                ShopSetting(); // 0을 입력하면 인벤토리 화면으로 돌아갑니다.
                return; // 함수를 종료합니다.
            }

            Item selected = player.Inventory[input - 1];

            // 아직 장착되지 않은 경우
            if (!selected.IsEquipped)  // 장착 중인 아이템은 판매하지 않도록 확인하기
            {
                player.Gold += selected.Price;  // 아이템 판매로 골드 획득
                player.Inventory.Remove(selected);  // 아이템을 플레이어의 인벤토리에서 제거
                shopInventory.Add(selected);  // 아이템을 상점의 인벤토리에 추가
                Console.WriteLine($"{selected.Name}을(를) 판매하였습니다.");
            }
            else
            {
                Console.WriteLine($"{selected.Name}은(는) 장착 중입니다. 먼저 장착을 해제해주세요.");
            }

            Console.ReadLine();
            ShopSetting();
        }








        static int CheckValidInput(int min, int max)  // 입력을 검증받아 유효하지 않은 입력을 할 시 다시 입력을 요구
        {
            while (true)
            {
                string input = Console.ReadLine();  // 입력을 받는다

                bool parseSuccess = int.TryParse(input, out var ret);  //입력을 정수로 변환 시도한다
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)  // 변환 값이 범위 내에 있다면
                        return ret;      //유효한 값을 반홚한다
                }
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("잘못된 입력입니다.");   // 범위 내에 없다면 다시 입력을 요구한다
            }
        }
    }





    public class Character  // 캐릭터 정보 클래스
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public int gold;
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public List<Item> Inventory { get; } = new List<Item>(); // 인벤토리 추가


        public Character(string name, string job, int level, int atk, int def, int hp, int gold)  
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;

            foreach (var item in Inventory)
            {
                item.IsEquipped = false;
            }

        }

    }            


    public class Item  // 아이템 정보 클래스
    {
        public string Name { get; }
        public string Type { get; }
        public int StatBonus { get; }
        public int Price { get; }  // 아이템 가격 속성
        public bool IsEquipped { get; set; } // 아이템 장착  



        public Item(string name, string type, int statBonus, int price)
        {
            Name = name;
            Type = type;
            StatBonus = statBonus;
            Price = price;

            IsEquipped = false; // IsEquipped 속성 초기화

        }


        public void Equip(Character player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (Type == "공격" && player.Inventory.Any(i => i.IsEquipped && i.Type == "공격"))
            {
                Console.WriteLine("공격 아이템은 이미 착용중입니다!");
            }
            else if (Type == "방어" && player.Inventory.Any(i => i.IsEquipped && i.Type == "방어"))
            {
                Console.WriteLine("방어 아이템은 이미 착용중입니다!");
            }
            else if (Type == "체력" && player.Inventory.Any(i => i.IsEquipped && i.Type == "체력"))
            {
                Console.WriteLine("체력 아이템은 이미 착용중입니다!");
            }
            else
            {
                if (Type == "공격")
                {
                    player.Atk += StatBonus;
                }
                else if (Type == "방어")
                {
                    player.Def += StatBonus;
                }
                else if (Type == "체력")
                {
                    player.Hp += StatBonus;
                }

                IsEquipped = true;
                Console.WriteLine($"{Name}을(를) 장착하였습니다.");
            }
        }

        public void Unequip(Character player)
        {
            if (Type == "공격")
            {
                player.Atk -= StatBonus;
            }
            else if (Type == "방어")
            {
                player.Def -= StatBonus;
            }
            else if (Type == "체력")
            {
                player.Hp -= StatBonus;
            }

            IsEquipped = false; // 아이템 장착 상태 변경

        }
    }
}
