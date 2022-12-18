using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FortuneWheelManager : MonoBehaviour
{
    public FortuneWheel fortuneWheel;
    public Text resultLabel;
    public Text money;
    int moneyInt = 0;
    public Text addMoneyText;
    Animator animCoinAdd;
    void Start()
    {
        animCoinAdd = addMoneyText.GetComponent<Animator>();
        animCoinAdd.SetBool("ActivAnimAdd", false);
    }
    public void Spin()
    {
        animCoinAdd.SetBool("ActivAnimAdd", false);
        StartCoroutine(SpinCoroutine());
    }
    IEnumerator SpinCoroutine()
    {
        yield return StartCoroutine(fortuneWheel.StartFortune());

        if (resultLabel == null) yield break;
        resultLabel.text = fortuneWheel.GetLatestResult();
        if (resultLabel.text == "x2")
        {
            moneyInt = int.Parse(money.text);
            moneyInt *= 2;
            money.text = moneyInt.ToString();
            addMoney("x2", Color.blue);
        }
        if (resultLabel.text == "+100")
        {
            moneyInt = int.Parse(money.text);
            moneyInt += 100;
            money.text = moneyInt.ToString();
            addMoney("+100", Color.green);
        }
        if (resultLabel.text == "-100")
        {
            moneyInt = int.Parse(money.text);
            moneyInt -= 100;
            money.text = moneyInt.ToString();
            addMoney("-100", Color.red);
        }
        if (resultLabel.text == "?")
        {
            int rand = Random.Range(-200, 200);
            moneyInt = int.Parse(money.text);
            moneyInt += rand;
            money.text = moneyInt.ToString();
            if (rand < 0)
                addMoney(rand.ToString(), Color.red);
            else
                addMoney("+" + rand.ToString(), Color.green);
        }       

        print(fortuneWheel.GetLatestResult());

    }
    private void addMoney(string number, Color colorNumber)
    {
        addMoneyText.text = number;
        addMoneyText.color = colorNumber;
        animCoinAdd.SetBool("ActivAnimAdd", true);
    }

}
