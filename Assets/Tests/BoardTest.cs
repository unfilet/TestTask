using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BoardTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void CalculateBestMoveForBoard()
        {
            Board board = new Board(GetModel());
            var move = board.CalculateBestMoveForBoard();
            Debug.Log(move);
        }

        private Level GetModel()
        {
            //string json = "{\"board\": [2,2,1,2,3,2,4,3,1,5,0,2,3,4,6,3,3,6,3,3,1,1,3,1,2], \"width\": 5, \"height\": 5}";
            string json = File.ReadAllText(Path.Combine(Application.dataPath, "level.txt"));
            Debug.Log(json);
            return JsonUtility.FromJson<Level>(json);
        }
}

}
