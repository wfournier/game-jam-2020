using System.Linq;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts
{
    public class orderHandler : MonoBehaviour
    {
        private int currentPlacement;
        public int[] input = new int[2];

        public GameObject objectToSpawn;

        private bool onlyOnce = true;
        public int[] order = new int[2];
        private Vector2 whereToSpawn;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
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
            var buttonList = FindObjectsOfType<Button>();

            for (var i = 0; i < buttonList.Length; i++)
            {
                var currentBtn = buttonList[i];
                if (currentBtn.signal && currentBtn.order && !input.Contains(currentBtn.buttonNumber))
                {
                    input[currentPlacement] = currentBtn.buttonNumber;
                    currentPlacement++;
                }
            }
        }

        private void IsWrongOrder()
        {
            var buttonList = FindObjectsOfType<Button>();
            var goodSignalCounter = 0;

            foreach (var btn in buttonList)
                if (btn.signal && btn.order)
                    goodSignalCounter++;
            if (goodSignalCounter == buttonList.Length && !OrderisRight())
            {
                foreach (var btn in buttonList) btn.buttonReset();
                for (var i = 0; i < input.Length; i++)
                    input[i] = 10 + i;
                currentPlacement = 0;
            }
        }
    }
}