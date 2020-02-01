using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Controllers;

namespace Assets.Scripts
{
    public class orderHandler : MonoBehaviour
    {
        public int[] order = new int[2];
        public int[] input = new int[2];
        private int currentPlacement = 0;

        public GameObject objectToSpawn;
        Vector2 whereToSpawn;

        private bool onlyOnce = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {           
            InputIdentifier();
            if (OrderisRight() && onlyOnce)
            {
                whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                Instantiate(objectToSpawn, whereToSpawn, Quaternion.identity);
                onlyOnce = false;
            }
            else
            {
                IsWrongOrder();
            }
                
        }

        private bool OrderisRight()
        {
            return order.SequenceEqual(input);
        }

        private void InputIdentifier()
        {
            Button[] buttonList = FindObjectsOfType<Button>();

            for (int i = 0; i < buttonList.Length; i++)
            {
                Button currentBtn = buttonList[i];
                if (currentBtn.signal && currentBtn.order && !input.Contains(currentBtn.buttonNumber))
                {
                    input[currentPlacement] = currentBtn.buttonNumber;
                    currentPlacement++;
                }
            }
        }

        private void IsWrongOrder()
        {
            Button[] buttonList = FindObjectsOfType<Button>();
            int goodSignalCounter = 0;

            foreach (Button btn in buttonList)
            {
                if (btn.signal && btn.order)
                {
                    goodSignalCounter++;
                }
            }
            if(goodSignalCounter == buttonList.Length && !OrderisRight())
            {
                foreach (Button btn in buttonList)
                {
                    btn.buttonReset();
                }
                for (int i = 0; i < input.Length; i++)
                    input[i] = 10 + i;
                currentPlacement = 0;
            }
        }
    }
}
