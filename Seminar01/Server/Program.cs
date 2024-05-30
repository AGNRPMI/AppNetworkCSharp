using Server;
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
                    UdpClient udpClient = new UdpClient(12345);
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Console.WriteLine("Сервер ожидает сообщение от клиента... ");

                    while (true)
                    {
                        byte[] buffer = udpClient.Receive(ref iPEndPoint);
                        if (buffer == null) break;

                        var messageText = Encoding.UTF8.GetString(buffer);
                        Message? messageServer = Message.DeserializeFromJsonToMessage(messageText);

                        Console.WriteLine(messageServer);

                        if (!string.IsNullOrEmpty(messageText))
                        {
                            Thread.Sleep(1000);
                            byte[] data = Encoding.UTF8.GetBytes("Сообщение доставлено.");
                            udpClient.Send(data, data.Length, iPEndPoint);
                        }
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
            Console.WriteLine("Серверная часть. Попробуйте переработать приложение, добавив подтверждение об отправке сообщений как в сервер, так и в клиент.");
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

