using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handaeap
{
    class Program
    {
        readonly string[] ProfileData = { "대중소", "한두세네", "앞뒤", "동서남북", "전후좌우", "오육칠팔", "신구",
                                     "도레미파솔라시", "시도군구동면읍리", "장단", "유미", "갑을병정무기경신임계", "자축인묘진사오미신유술해",
                                     "고중저", "양음", "안밖", "공사", "개말소닭양범곰쥐새꿩삵뱀게", "생사", "일이삼사", "입퇴", "놈년", "남여", "강중약", "승패",
                                     "부모", "득실", "내외", "춘하추동", "밑위", "다소", "일십백천만억조경", "신고", "군양", "금은동철", "해달", "이그저",
                                     "상하좌우", "나너", "년월일시분초", "기인", "생로병사", "유무", "흑백적녹청황등자", "공수",
                                     "비씨디이엘엠엔오피큐알티유", "캣독", "양한", "한중일미영독러", "차마", "총포", "원근", "명암", "창문", "우돈계",
                                     "평곡", "직곡", "국밥", "공일이삼사오육칠팔구십", "영공", "육해공", "민관", "노사", "월하수목금토일", "1234567890", "최차",
                                        "?!", "☆★♤♠♡♥♧♣", "유무", "#$", "잘안못" };

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        private Program()
        {
            Profile = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Profile { get; set; }

        private void AddPair(string a, string b)
        {
            if (Profile.ContainsKey(a) == false)
            {
                Profile[a] = new List<string>();
            }
            if (Profile[a].Contains(b) == false)
            {
                Profile[a].Add(b);
            }
        }

        private void Build()
        {
            foreach (var data in ProfileData)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = i + 1; j < data.Length; j++)
                    {
                        string a = data[i].ToString();
                        string b = data[j].ToString();

                        AddPair(a, b);
                        AddPair(b, a);
                    }
                }
            }
        }

        private void Generate(int i, List<char> input, List<string> result)
        {
            if (input.Count <= i)
            {
                result.Add(string.Join("", input));
                return;
            }

            char original = input[i];

            if (Profile.ContainsKey(original.ToString()))
            {
                Generate(i + 1, input, result);

                foreach (var variation in Profile[original.ToString()])
                {
                    input[i] = variation[0];
                    Generate(i + 1, input, result);
                }

                input[i] = original;
            }
            else
            {
                Generate(i + 1, input, result);
            }
        }

        private List<string> Process(string input)
        {
            List<string> result = new List<string>();
            Generate(0, new List<char>(input), result);
            return result;
        }

        private void Start()
        {
            Build();

            var input = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter("result.txt"))
            {
                foreach (var original in input)
                {
                    Console.Write(original);
                    writer.Write(original);
                    if (Profile.ContainsKey(original.ToString()))
                    {
                        foreach (var variation in Profile[original.ToString()])
                        {
                            Console.Write(variation);
                            writer.Write(variation);
                        }
                    }
                    Console.WriteLine();
                    writer.WriteLine();
                }

                Console.WriteLine();
                writer.WriteLine();

                writer.Flush();

                var result = Process(input);
                
                foreach (var line in result)
                {
                    Console.WriteLine(line);
                    writer.WriteLine(line);
                }

                writer.WriteLine();
                Console.WriteLine();

                writer.WriteLine(result.Count);
                Console.WriteLine(result.Count);

                writer.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
