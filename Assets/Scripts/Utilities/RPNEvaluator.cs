using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Linq;
using JetBrains.Annotations;

public class RPN
{

    public static int calculateRPN(string expression, Dictionary<string, int> variables)
    {
        Stack<int> stack = new Stack<int>();
        // Dictionary<string, int> variables = new Dictionary<string, int>();
        string[] operations = {"+", "-", "*", "/", "%"};
        foreach (string token in expression.Split(" "))
        {
            if (variables.ContainsKey(token))
                {stack.Push(variables[token]);}
            else if (operations.Contains(token)){
                int a = stack.Pop();
                int b = stack.Pop();
                stack.Push(applyOperator(token, b, a));
            } else {
                stack.Push(int.Parse(token));
            }
        }
        return stack.Pop();

    }
    public static int applyOperator(string token, int b, int a)
    {
        if(token == "+")
        {
            return b + a;
        }
        else if(token =="-" )
        {
            return b - a;
        } 
        else if(token == "*")
        {
            return b * a;
        }
        else if(token == "/")
        {
            return b / a;
        } 
        else if(token == "%")
        {
            return b % a;
        }
        else
        {
            Debug.Log($"not a valid operation token: {token}");
            return 0;
        }
    }
}