using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace RootCoinTrial1
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey(); //a private key constructor creates a private key public key pair
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain rootcoin = new Blockchain(4, 100); //new method; difficulty 2 (nonce) and reward 200

            Console.WriteLine("Start the Miner: ");
            rootcoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet 1 is $" + rootcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            rootcoin.addPendingTransaction(tx1);

            Console.WriteLine("Start the Miner: ");
            rootcoin.MinePendingTransactions(wallet2);

            Console.WriteLine("\nBalance of wallet 1 is $" + rootcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet 2 is $" + rootcoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(rootcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            //rootcoin.GetLatestBlock().PreviousHash = "12345";

            if (rootcoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is Valid!!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid!!");
            }
        }
    }
}



