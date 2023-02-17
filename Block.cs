using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace RootCoinTrial1
{
    class Block
    {
        public int Index { get; set; } //make a block
        public string PreviousHash { get; set; }
        public string TimeStamp { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Block(int index, string timestamp, List<Transaction> transactions, string previousHash = "") //constructor
        {
            this.Index = index;
            this.TimeStamp = timestamp;
            this.Transactions = transactions;
            this.PreviousHash = previousHash;
            this.Hash = CalculateHash();
            this.Nonce = 0;
        }
        public string CalculateHash()
        {
            string blockData = this.Index + this.PreviousHash + this.TimeStamp + this.Transactions.ToString() + this.Nonce;
            byte[] blockBytes = Encoding.ASCII.GetBytes(blockData);
            byte[] hashBytes = SHA256.Create().ComputeHash(blockBytes);
            return BitConverter.ToString(hashBytes).Replace("-", ""); //convert back to hex string
        }
        public void Mine(int difficulty) //vary hash until has is what we want; depending on number of 0
        {
            while (this.Hash.Substring(0, difficulty) != new String('0', difficulty)) //check from 0 to specified value
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
                //Console.WriteLine("Mining: " + this.Hash);
            }

            Console.WriteLine("Block has mined: " + this.Hash);
        }
    }
}
