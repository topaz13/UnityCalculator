using UnityEditor;
using UnityEngine;

public enum Operator
{
    NONE, ADD, SUB, MUL, DIV, EQ
}

public enum InputType
{
    NUMBEAR, OPERATOR
}
public class CalculatorWindow : EditorWindow
{
    int previousValue = 0;
    int currentValue = 0;
    int buttonHeight = 50;
    int buttonWidth = 50;
    int buttonMarign = 5;
    int leftMargin = 10;
    int topMargin = 50;
    int _fontSize = 32;
    Operator inputOperator = Operator.NONE;
    InputType inputType = InputType.OPERATOR;
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle()
        {
            fontSize = _fontSize,
        };
        EditorGUI.LabelField(new Rect(leftMargin, 10, (buttonWidth + buttonMarign) * 3 - buttonMarign, buttonHeight), currentValue.ToString(), style);

        if (GUI.Button(MakeButtonRect(0, 0), "7"))
        {
            OnClickedNumber(7);
        }
        if (GUI.Button(MakeButtonRect(0, 1), "8"))
        {
            OnClickedNumber(8);
        }
        if (GUI.Button(MakeButtonRect(0, 2), "9"))
        {
            OnClickedNumber(9);
        }
        if (GUI.Button(MakeButtonRect(0, 3), "÷"))
        {
            OnClickedOperator(Operator.DIV);
        }

        if (GUI.Button(MakeButtonRect(1, 0), "4"))
        {
            OnClickedNumber(4);
        }
        if (GUI.Button(MakeButtonRect(1, 1), "5"))
        {
            OnClickedNumber(5);
        }
        if (GUI.Button(MakeButtonRect(1, 2), "6"))
        {
            OnClickedNumber(6);
        }
        if (GUI.Button(MakeButtonRect(1, 3), "×"))
        {
            OnClickedOperator(Operator.MUL);
        }


        if (GUI.Button(MakeButtonRect(2, 0), "1"))
        {
            OnClickedNumber(1);
        }
        if (GUI.Button(MakeButtonRect(2, 1), "2"))
        {
            OnClickedNumber(2);
        }
        if (GUI.Button(MakeButtonRect(2, 2), "3"))
        {
            OnClickedNumber(3);
        }
        if (GUI.Button(MakeButtonRect(2, 3), "-"))
        {
            OnClickedOperator(Operator.SUB);
        }


        if (GUI.Button(MakeButtonRect(3, 0), "0"))
        {
            OnClickedNumber(0);
        }
        if (GUI.Button(MakeButtonRect(3, 1), "="))
        {
            OnClickedOperator(Operator.EQ);
        }
        if (GUI.Button(MakeButtonRect(3, 2), "AC"))
        {
            OnClickedAC();
        }
        if (GUI.Button(MakeButtonRect(3, 3), "+"))
        {
            OnClickedOperator(Operator.ADD);
        }
    }

    public void OnClickedAC()
    {
        this.currentValue = 0;
        this.previousValue = 0;
        this.inputOperator = Operator.NONE;
        inputType = InputType.OPERATOR;
    }

    // i行j列のボタン用のRectを作成する．
    public Rect MakeButtonRect(int i, int j)
    {
        return new Rect(leftMargin + (buttonWidth + buttonMarign) * j, topMargin + (buttonHeight + buttonMarign) * i, buttonWidth, buttonHeight);
    }




    public void OnClickedNumber(int number)
    {
        if (inputType == InputType.OPERATOR)
        {
            previousValue = currentValue;
            currentValue = 0;
        }
        inputType = InputType.NUMBEAR;
        currentValue *= 10;
        currentValue += number;
    }

    public void OnClickedOperator(Operator op)
    {
        if (inputType == InputType.OPERATOR)
        {
            inputOperator = op;
            return;
        }
        inputType = InputType.OPERATOR;
        // 初回の入力
        if (inputOperator == Operator.NONE || inputOperator == Operator.EQ)
        {
            previousValue = currentValue;
            inputOperator = op;
            return;
        }
        switch (inputOperator)
        {
            case Operator.ADD:
                currentValue = previousValue + currentValue;
                break;
            case Operator.SUB:
                currentValue = previousValue - currentValue;
                break;
            case Operator.MUL:
                currentValue = previousValue * currentValue;
                break;
            case Operator.DIV:
                currentValue = previousValue / currentValue;
                break;
        }
        inputOperator = op;
    }

    [MenuItem("Tool/Calculator")]
    public static void CreateWindow()
    {
        // EditorWindowを作成する
        EditorWindow window = EditorWindow.GetWindow(typeof(CalculatorWindow), true, "Calculator");
        Vector2 size = new Vector2(10 + 50 * 4 + 5 * 3 + 10, 270);
        window.minSize = size;
        window.maxSize = size;
        window.Show();
    }
}
