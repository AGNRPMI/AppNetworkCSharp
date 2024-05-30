using Client;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace CSharpExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isOpen = true;
            while (isOpen)
            {
                isOpen = WorkingProcess(isOpen);
                if (isOpen)
                {
                    PrintTaskDescription();
                    // блок кода для исполнения программы
                    string myNickName = args[0];
                    string ip = args[1];

                    UdpClient udpClient = new UdpClient();
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

                    while (true)
                    {
                        string messageTextOut;
                        do
                        {
                            //Console.Clear();
                            Console.WriteLine("Введите сообщение: ");
                            messageTextOut = Console.ReadLine();
                        }
                        while (string.IsNullOrEmpty(messageTextOut));

                        Message message = new Message()
                        {
                            DateTime = DateTime.Now,
                            NickNameFrom = myNickName,
                            NickNameTo = "Server",
                            Text = messageTextOut
                        };
                        string json = message.SerializeMessageToJson();

                        byte[] data = Encoding.UTF8.GetBytes(json);
                        udpClient.Send(data, data.Length, iPEndPoint);

                        byte[] buffer = udpClient.Receive(ref iPEndPoint);
                        if (buffer != null)
                        {
                            var messageTextIn = Encoding.UTF8.GetString(buffer);
                            Console.WriteLine(messageTextIn);
                            Console.WriteLine();
                        }
                        else Console.WriteLine("Сообщение не дошло(((");

                    }
                }
            }
            Console.Write("Нажмите любую клавишу для завершения ...");
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintTaskDescription()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("****************************************************************************");
            Console.WriteLine("Клиентская часть. Попробуйте переработать приложение, добавив подтверждение об отправке сообщений как в сервер, так и в клиент.");
            Console.WriteLine("****************************************************************************");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }


        static bool WorkingProcess(bool _isOpen)
        {
            string _choice = "none";
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Желаете выполнить алгоритм? (y/n)");

            while (_choice != "y" || _choice != "n")
            {
                Console.WriteLine("введите команду: ");
                _choice = Console.ReadLine();
                switch (_choice)
                {
                    case "y":
                        Console.WriteLine("Выполняется функция ...");
                        Console.WriteLine(" ");
                        _isOpen = true;
                        break;
                    case "n":
                        Console.WriteLine("Завершение программы... Нажмите Enter");
                        Console.WriteLine(" ");
                        _isOpen = false;
                        break;
                    default:
                        Console.WriteLine("команда не распознана, введите еще раз");
                        Console.WriteLine(" ");
                        break;
                }
                if (_choice == "y" || _choice == "n") break;
            }

            Console.ForegroundColor = ConsoleColor.White;
            return _isOpen;
        }
    }
}

